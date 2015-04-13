using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Discuz.Data;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;
using System.Text.RegularExpressions;
//using SQLDMO;
using System.Collections.Generic;

//////////////////////////////////////////////////////////////////////////
//BT相关的SQL数据库操作

namespace Discuz.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        /// <summary>
        /// 获得指定TopicId对应的seedid
        /// </summary>
        /// <param name="topicid"></param>
        /// <returns></returns>
        public int GetSeedIdByTopicId(int topicid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@topicid",(DbType)SqlDbType.Int, 4, topicid)
                                  };
            string sqlstring = string.Format("SELECT [seedid] FROM [{0}topics] WHERE [tid] = @topicid", BaseConfigs.GetTablePrefix);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), 0);
        }

        //public IDataReader PrivateBTGetLastPostByFid(int fid, string posttablename)
        //{
        //    DbParameter parm = DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, fid);
        //    return DbHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 [tid], [title], [postdatetime], [posterid], [poster], [pid] FROM [" + posttablename + "] WHERE [fid] = @fid ORDER BY [pid] DESC", parm);
        //}
        /// <summary>
        /// 获取指定条件的帖子pid在帖子中的排序
        /// </summary>
        /// <returns></returns>
        public int GetPostCountInTopic(int Tid, int Pid, string posttablename)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,Tid),
                                       DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int,4,Pid),
								   };
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT (1) FROM [" + posttablename + "] WHERE [tid]=@tid AND [pid] < @pid", parms), 0);

        }
        /// <summary>
        ///  更新指定uid和nid的通知的状态
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newType"></param>
        public void UpdateNoticeNewByUidNid(int uid, int nid, int newType)
        {
            DbParameter[] parms = {                                    
                                    DbHelper.MakeInParam("@new", (DbType)SqlDbType.Int, 4, newType),
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
                                    DbHelper.MakeInParam("@nid", (DbType)SqlDbType.Int, 4, nid),
                                };
            string commandText = string.Format("Update [{0}notices] SET [new] = @new WHERE [uid] = @uid AND [nid] = @nid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        ///  更新指定uid和type的通知的状态
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newType"></param>
        public void UpdateNoticeNewByUidType(int uid, int type, int newType)
        {
            DbParameter[] parms = {                                    
                                    DbHelper.MakeInParam("@new", (DbType)SqlDbType.Int, 4, newType),
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
                                    DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, type),
                                };
            string commandText = string.Format("Update [{0}notices] SET [new] = @new WHERE [uid] = @uid AND [type] = @type", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 清零帖子当天回复数
        /// </summary>
        /// <returns></returns>
        public int ClearTopicRepliesToday()
        {
            string sqlstring = string.Format("UPDATE [{0}topics] SET [repliestoday] = 0", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
        }
        /// <summary>
        /// 获取TOPTEN Topics 十大主题
        /// </summary>
        /// <returns></returns>
        public DataTable GetTopTenTopics()
        {
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_gettoptentopics").Tables[0];
        }
        /// <summary>
        /// 获取TOPTEN Seeds 十大种子
        /// </summary>
        /// <returns></returns>
        public DataTable GetTopTenSeeds()
        {
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_gettoptenseeds").Tables[0];
        }
        /// <summary>
        /// 获取TOPTEN Bonus 十大悬赏
        /// </summary>
        /// <returns></returns>
        public DataTable GetTopTenBonus()
        {
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_gettoptenbonus").Tables[0];
        }

    }
}