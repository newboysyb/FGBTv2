using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
    public partial class PrivateBT
    {



        /// <summary>
        /// 获取指定时间后，某个种子新增流量记录数，可反映种子在指定事件后的下载次数
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int GetNewTrafficRecordCount(int seedid, DateTime datetime)
        {
            if (seedid < 1) return -1;

            return DatabaseProvider.GetInstance().GetNewTrafficRecordCount(seedid, datetime);
        }

        /// <summary>
        /// 获得对应SeedId的种子的历史Peer节点信息/历史流量信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetPeerHistoryList(int seedid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetPeerHistoryList(seedid);
            dt.Columns.Add("up");
            dt.Columns.Add("down");
            dt.Columns.Add("rat");
            foreach (DataRow dr in dt.Rows)
            {
                dr["up"] = PTTools.Upload2Str(decimal.Parse(dr["upload"].ToString()), false);
                dr["down"] = PTTools.Upload2Str(decimal.Parse(dr["download"].ToString()), false);
                dr["rat"] = PTTools.Ratio2Str(decimal.Parse(dr["extcredits3"].ToString()), decimal.Parse(dr["extcredits4"].ToString()));

                if (dr["finishtime"] == null) dr["finishtime"] = "";
            }
            dt.Dispose();
            return dt;
        }

        /// <summary>
        /// 获得对应SeedId的种子的完成节点信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetUserIdListActiveInSeed(int seedid, DateTime lastupdate)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetUserIdListActiveInSeed(seedid, lastupdate);
            dt.Dispose();
            return dt;
        }


        /// <summary>
        /// 更新单种上传下载数据
        /// </summary>
        public static int UpdatePerUserSeedTraffic(int pid, int seedid, int userid, decimal addupload, int addkeeptime)
        {
            int result = DatabaseProvider.GetInstance().UpdatePerUserSeedTraffic(seedid, userid, addupload, addkeeptime);

            if (result > 0) return result;

            PTPeerLog.InsertPeerHistoryLog(pid, seedid, userid, 200, "Err SIMPLE", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -RESULT:{3}", seedid, userid, addupload, result));
            //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "Err SIMPLE", string.Format("SEED:{0} -UID:{1} -ADDUP:{2}", seedid, userid, addupload));
            return -1;
        }
        /// <summary>
        /// 更新单种上传下载数据
        /// </summary>
        public static int UpdatePerUserSeedTraffic_WithIP(int pid, int seedid, int userid, decimal addupload, int addkeeptime, string ipv4, string ipv6)
        {
            int result = DatabaseProvider.GetInstance().UpdatePerUserSeedTraffic_WithIP(seedid, userid, addupload, addkeeptime, ipv4, ipv6);

            if (result > 0) return result;

            PTPeerLog.InsertPeerHistoryLog(pid, seedid, userid, 200, "Err WithIP", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -IPv4:{3} -IPv6:{4} -RESULT:{5}", seedid, userid, addupload, ipv4, ipv6, result));
            //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "Err WithIP", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -IPv4:{3} -IPv6:{4}", seedid, userid, addupload, ipv4, ipv6));
            return -1;
        }
        /// <summary>
        /// 更新单种上传下载数据
        /// </summary>
        public static int UpdatePerUserSeedTraffic_Download(int pid, int seedid, int userid, decimal addupload, decimal adddownload, decimal lastleft, decimal lastdownload)
        {
            int result = DatabaseProvider.GetInstance().UpdatePerUserSeedTraffic_Download(seedid, userid, addupload, adddownload, lastleft, lastdownload);

            if (result > 0) return result;

            PTPeerLog.InsertPeerHistoryLog(pid, seedid, userid, 200, "Err Download", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -LEFT:{4} -DOWN:{5} -RESULT:{6}", seedid, userid, addupload, adddownload, lastleft, lastdownload, result));
            //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "Err Download", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -LEFT:{4} -DOWN:{5}", seedid, userid, addupload, adddownload, lastleft, lastdownload));
            return -1;
        }
        /// <summary>
        /// 更新单种上传下载数据
        /// </summary>
        public static int UpdatePerUserSeedTraffic_DownloadWithIP(int pid, int seedid, int userid, decimal addupload, decimal adddownload, decimal lastleft, decimal lastdownload, string ipv4, string ipv6)
        {
            int result = DatabaseProvider.GetInstance().UpdatePerUserSeedTraffic_DownloadWithIP(seedid, userid, addupload, adddownload, lastleft, lastdownload, ipv4, ipv6);

            if (result > 0) return result;

            PTPeerLog.InsertPeerHistoryLog(pid, seedid, userid, 200, "Err Download", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -LEFT:{4} -DOWN:{5} -IPv4:{6} -IPv6:{7} -RESULT:{8}", seedid, userid, addupload, adddownload, lastleft, lastdownload, ipv4, ipv6, result));
            //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "Err Download", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -LEFT:{4} -DOWN:{5} -IPv4:{6} -IPv6:{7}", seedid, userid, addupload, adddownload, lastleft, lastdownload, ipv4, ipv6));
            return -1;
        }
        /// <summary>
        /// 更新单种上传下载数据，种子首次更新，可能需要插入
        /// </summary>
        public static int UpdatePerUserSeedTraffic_SeedFirst(int pid, int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6, decimal lastleft, decimal lastdownload, string peerid, float firstratio)
        {
            int result = DatabaseProvider.GetInstance().UpdatePerUserSeedTraffic_SeedFirst(seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid, firstratio);

            if (result > 0) return result;
            else
            {
                if (TryInsertPerUserSeedTraffic(seedid, userid, 0, 0, ipv4, ipv6) > 0)
                {
                    result = DatabaseProvider.GetInstance().UpdatePerUserSeedTraffic_SessionFirst(seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid);
                    if (result > 0) return result;

                    PTPeerLog.InsertPeerHistoryLog(pid, seedid, userid, 200, "Err SeedF1", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -IPv4:{4} -IPv6:{5} -LEFT:{6} -DOWN:{7} -PEERID:{8} -RESULT:{9}", seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid, result));
                    //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "Err SeedF1", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -IPv4:{4} -IPv6:{5} -LEFT:{6} -DOWN:{7} -PEERID:{8}", seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid));
                    return -1;
                }

                PTPeerLog.InsertPeerHistoryLog(pid, seedid, userid, 200, "Err SeedF2", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -IPv4:{4} -IPv6:{5} -LEFT:{6} -DOWN:{7} -PEERID:{8} -RESULT:{9}", seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid, result));
                //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "Err SeedF2", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -IPv4:{4} -IPv6:{5} -LEFT:{6} -DOWN:{7} -PEERID:{8}", seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid));
                return -1;
            }
        }

        /// <summary>
        /// 更新单种上传下载数据
        /// </summary>
        public static int UpdatePerUserSeedTraffic_SessionFirst(int pid, int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6, decimal lastleft, decimal lastdownload, string peerid)
        {
            int result = DatabaseProvider.GetInstance().UpdatePerUserSeedTraffic_SessionFirst(seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid);

            if (result > 0) return result;

            PTPeerLog.InsertPeerHistoryLog(pid, seedid, userid, 200, "Err SessionF", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -IPv4:{4} -IPv6:{5} -LEFT:{6} -DOWN:{7} -PEERID:{8} -RESULT:{9}", seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid, result));
            //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "Err SessionF", string.Format("SEED:{0} -UID:{1} -ADDUP:{2} -ADDDOWN:{3} -IPv4:{4} -IPv6:{5} -LEFT:{6} -DOWN:{7} -PEERID:{8}", seedid, userid, addupload, adddownload, ipv4, ipv6, lastleft, lastdownload, peerid));
            return -1;
        }




        private static int TryInsertPerUserSeedTraffic(int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6)
        {
            //防止同时插入造成唯一列冲突
            int result = 0;
            try
            {
                return InsertPerUserSeedTraffic(seedid, userid, addupload, adddownload, ipv4, ipv6);
            }
            catch
            {
                System.Threading.Thread.Sleep(500);
                result = InsertPerUserSeedTraffic(seedid, userid, addupload, adddownload, ipv4, ipv6);
                if (result > 0) return result;
                else return -1;
            }
        }

        private static object SynObjectTraffic = new object();
        /// <summary>
        /// 插入单种上传下载数据
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public static int InsertPerUserSeedTraffic(int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6)
        {

            int rst = -1;
            try
            {
                if(System.Threading.Monitor.TryEnter(SynObjectTraffic, 5000))
                {
                    try
                    {
                        decimal up = 0M;
                        decimal down = 0M;
                        decimal left = 0M;
                        int keep = 0;
                        if (GetPerUserSeedTraffic(seedid, userid, ref up, ref down, ref left, ref keep) < 1)
                        {
                            rst = DatabaseProvider.GetInstance().InsertPerUserSeedTraffic(seedid, userid, addupload, adddownload, ipv4, ipv6);
                        }
                        else rst = -2;
                        System.Threading.Monitor.Exit(SynObjectTraffic);
                        return rst;
                    }
                    catch
                    {
                        System.Threading.Monitor.Exit(SynObjectTraffic);
                        return -1;
                    }
                }
                return -3;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
            
            return -4;  
        }

        /// <summary>
        /// 获取用户种子上传下载量，返回1读取成功，返回0读取失败
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <returns></returns>
        public static int GetPerUserSeedTraffic(int seedid, int userid, ref decimal upload, ref decimal download, ref decimal left, ref int keeptime)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetPerUserSeedTraffic(seedid, userid);
            if (reader.Read())
            {
                upload = TypeConverter.ObjectToDecimal(reader["upload"]);
                download = TypeConverter.ObjectToDecimal(reader["download"]);
                left = TypeConverter.ObjectToDecimal(reader["lastleft"]);
                keeptime = TypeConverter.ObjectToInt(reader["keeptime"]);
                reader.Close();
                reader.Dispose();
                return 1;
            }
            else
            {
                upload = 0;
                download = 0;
                left = 0;
                reader.Close();
                reader.Dispose();
                return 0;
            }

        }
    }
}