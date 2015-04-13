using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;
using System.Threading;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;
using Discuz.Forum.ScheduledEvents;

namespace Discuz.Forum
{
    /// <summary>
    /// 今日十大话题
    /// </summary>
    public class PTTopTen
    {
        /// <summary>
        /// lock变量，防止重复执行
        /// </summary>
        private static object SynObjectTopTen = new object();
        /// <summary>
        /// lock计数器
        /// </summary>
        private static int SynObjectCount = 0;
        /// <summary>
        /// 上次更新时间，初始值为当前时间，防止程序首次运行时，导致大量并发
        /// </summary>
        private static DateTime LastUpdateTime_bonus = DateTime.Now.AddMinutes(-10);
        private static DateTime LastUpdateTime_topics = DateTime.Now.AddMinutes(-10);
        private static DateTime LastUpdateTime_seeds = DateTime.Now.AddMinutes(-10);

        private static int SysObjectConCurrent = 0;
        /// <summary>
        /// 由于Tracker和Web分离，需要在Web读取的信息不能在Tracker更新了 
        /// 【取消：更新十大，由计划任务触发（by Tracker）避免影响用户浏览体验】
        /// </summary>
        public static void UpdateTopTenbyScheduleTask()
        {
            //最小更新间隔 5分钟
            lock (SynObjectTopTen)
            {
                
                if (SysObjectConCurrent > 0)
                {
                    PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Detail, "UpdateTopTenEvent", string.Format("更新10大 跳过 ConCurrent:{0}", SysObjectConCurrent));
                    return;
                }
                if ((DateTime.Now - LastUpdateTime_bonus).TotalMinutes < 4 && (DateTime.Now - LastUpdateTime_topics).TotalMinutes < 4 && (DateTime.Now - LastUpdateTime_seeds).TotalMinutes < 4)
                {
                    PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Detail, "UpdateTopTenEvent", string.Format("更新10大 跳过 Last:bonus:{0} topics:{1} seeds:{2}", LastUpdateTime_bonus, LastUpdateTime_topics, LastUpdateTime_seeds));
                    return;
                }
                SysObjectConCurrent++;
            }


            #region 检测是否需要更新、防止重复执行(功能已取消)
            
            //bool needupdate = false;

            //如果内容为空，则置上次更新时间为1990 这将导致并发更新
            //if (DNTCache.GetCacheService().RetrieveObject("/PTTopTen/Topics") == null ||
            //    DNTCache.GetCacheService().RetrieveObject("/PTTopTen/Seeds") == null ||
            //    DNTCache.GetCacheService().RetrieveObject("/PTTopTen/Bonus") == null)
            //    LastUpdateTime = new DateTime(1990, 1, 1);



            //lock (SynObjectTopTen)
            //{
            //    //防止重复执行，同时防止更新不成功导致无法更新，超过30分钟强制更新SynObjectCount == 0 || 
                
            //    if ((DateTime.Now - LastUpdateTime).TotalMinutes > 30)
            //    {
            //        LastUpdateTime = DateTime.Now;
            //        //SynObjectCount++;
            //        needupdate = true;
            //    }
            //}

            //if (needupdate)

            #endregion

            try
            {
                int t10topics = -1, t10seeds = -1, t10bonus = -1;
                //更新十大帖子
                DataTable dtt = DatabaseProvider.GetInstance().GetTopTenTopics();
                t10topics = dtt.Rows.Count;
                DNTCache.GetCacheService().RemoveObject("/PTTopTen/Topics");
                DNTCache.GetCacheService().AddObject("/PTTopTen/Topics", dtt, 3600);
                dtt.Dispose();

                //更新十大种子
                DataTable dts = DatabaseProvider.GetInstance().GetTopTenSeeds();
                dts.Columns.Add("typenameen");
                dts.Columns.Add("typename");
                foreach (DataRow dr in dts.Rows)
                {
                    int typeid = TypeConverter.ObjectToInt(dr["type"]);
                    dr["typenameen"] = PrivateBT.Type2Str(typeid);
                    dr["typename"] = PrivateBT.Type2Name(typeid);
                }
                t10seeds = dts.Rows.Count;
                DNTCache.GetCacheService().RemoveObject("/PTTopTen/Seeds");
                DNTCache.GetCacheService().AddObject("/PTTopTen/Seeds", dts, 3600);

                //更新十大悬赏
                DataTable dtb = DatabaseProvider.GetInstance().GetTopTenBonus();
                t10bonus = dtb.Rows.Count;
                DNTCache.GetCacheService().RemoveObject("/PTTopTen/Bonus");
                DNTCache.GetCacheService().AddObject("/PTTopTen/Bonus", dtb, 3600);

                //完成更新
                PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Normal, "UpdateTopTenEvent", string.Format("更新10大完毕 Update: -Top10 Topics:{0} -Top10 Seeds:{1} -Top10 Bonus:{2} -SYNC:{3}", t10topics, t10seeds, t10bonus, SynObjectCount.ToString()));
                
                LastUpdateTime_bonus = DateTime.Now;
                LastUpdateTime_topics = DateTime.Now;
                LastUpdateTime_seeds = DateTime.Now;

                // SynObjectCount = 0;
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Exception, "UpdateTopTenEvent", string.Format("更新10大 异常 EX:{0}", ex));
            }
            SysObjectConCurrent = 0;
        }

        private delegate void Top10TaskEventDelegate();
        private static void UpdateTopTenbyScheduleTaskDo()
        {
            UpdateTopTenbyScheduleTask();
            SynObjectCount = 0;
        }
        /// <summary>
        /// 获得十大帖子列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTopTenTopics()
        {
            DataTable dt = new DataTable();

            dt = DNTCache.GetCacheService().RetrieveObject("/PTTopTen/Topics") as DataTable;

            if (dt == null || (dt.Rows.Count > 0 && (DateTime.Now - TypeConverter.ObjectToDateTime(dt.Rows[0]["lastupdate"])).TotalMinutes > 30))
            {
                double overtime = dt != null && dt.Rows.Count > 0 ? (DateTime.Now - TypeConverter.ObjectToDateTime(dt.Rows[0]["lastupdate"])).TotalMinutes : -1;
                bool needupdate = false;
                lock (SynObjectTopTen)
                {
                    //防止重复执行，同时防止更新不成功导致无法更新SynObjectCount == 0 && 
                    if ((DateTime.Now - LastUpdateTime_topics).TotalMinutes > 30)
                    {
                        //SynObjectCount++;
                        needupdate = true;
                        LastUpdateTime_topics = DateTime.Now;
                    }
                }
                if (needupdate)
                {
                    dt = DatabaseProvider.GetInstance().GetTopTenTopics();
                    DNTCache.GetCacheService().RemoveObject("/PTTopTen/Topics");
                    DNTCache.GetCacheService().AddObject("/PTTopTen/Topics", dt, 3600);
                    PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Warning, "UpdateTopTopics", string.Format("强制更新：-Count:{0} -SYNC:{1} -No1:Count:{2} -Tid{3} -OVERTIME:{4} -Last:{5}", dt.Rows.Count, SynObjectCount.ToString(), dt.Rows[0]["repliestoday"], dt.Rows[0]["tid"], overtime, LastUpdateTime_topics));
                    //SynObjectCount = 0;
                }
                else
                {
                    //lock (SynObjectTopTen)
                    {
                        //if (SynObjectCount == 0)
                        {
                            //SynObjectCount++;
                            Top10TaskEventDelegate dotask = new Top10TaskEventDelegate(UpdateTopTenbyScheduleTaskDo);
                            dotask.BeginInvoke(null, null);
                            PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Warning, "UpdateTopTen", string.Format("前台更新：TOPICS -OVERTIME:{0} -Last:{1}", overtime, LastUpdateTime_topics));
                        }
                    }
                    return GetEmptyTopTenTopics();
                }
                
            }
           
            return dt;
        }
        
        /// <summary>
        /// 获得十大种子列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTopTenSeeds()
        {
            DataTable dt = new DataTable();

            dt = DNTCache.GetCacheService().RetrieveObject("/PTTopTen/Seeds") as DataTable;

            if (dt == null || (dt.Rows.Count > 0 && (DateTime.Now - TypeConverter.ObjectToDateTime(dt.Rows[0]["lastupdate"])).TotalMinutes > 30))
            {
                double overtime = dt != null && dt.Rows.Count > 0 ? (DateTime.Now - TypeConverter.ObjectToDateTime(dt.Rows[0]["lastupdate"])).TotalMinutes : -1;
                bool needupdate = false;
                int curcount = 0;
                lock (SynObjectTopTen)
                {
                    //防止重复执行，同时防止更新不成功导致无法更新SynObjectCount == 0 || 
                    if ((DateTime.Now - LastUpdateTime_seeds).TotalMinutes > 30)
                    {
                        curcount = SynObjectCount++;
                        needupdate = true;
                        LastUpdateTime_seeds = DateTime.Now;
                    }
                }

                //直接强制更新
                if (needupdate)
                {
                    dt = DatabaseProvider.GetInstance().GetTopTenSeeds();
                    dt.Columns.Add("typenameen");
                    dt.Columns.Add("typename");

                    foreach (DataRow dr in dt.Rows)
                    {
                        int typeid = TypeConverter.ObjectToInt(dr["type"]);
                        dr["typenameen"] = PrivateBT.Type2Str(typeid);
                        dr["typename"] = PrivateBT.Type2Name(typeid);
                    }

                    DNTCache.GetCacheService().RemoveObject("/PTTopTen/Seeds");
                    DNTCache.GetCacheService().AddObject("/PTTopTen/Seeds", dt, 3600);
                    PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Warning, "UpdateTopSeeds", string.Format("强制更新：Count:{0} -SYNC:{1} -No1:Count:{2} -Tid{3} -OVERTIME:{4} -Last:{5}", dt.Rows.Count, SynObjectCount.ToString(), dt.Rows[0]["finishedtoday"], dt.Rows[0]["topicid"], overtime, LastUpdateTime_seeds));
                    //SynObjectCount = 0;
                }
                else
                {
                    //发送后台更新
                    //lock (SynObjectTopTen)
                    {
                        //if (SynObjectCount == 0)
                        {
                            //SynObjectCount++;
                            Top10TaskEventDelegate dotask = new Top10TaskEventDelegate(UpdateTopTenbyScheduleTaskDo);
                            dotask.BeginInvoke(null, null);
                            PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Warning, "UpdateTopTen", string.Format("前台更新：SEEDS -OVERTIME:{0} -Last:{1}", overtime, LastUpdateTime_seeds));
                        }
                    }
                    return GetEmptyTopTenSeeds();
                }
                    
                
            }
            return dt;
        }
        
        /// <summary>
        /// 获得十大悬赏列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTopTenBonus()
        {
            DataTable dt = new DataTable();

            dt = DNTCache.GetCacheService().RetrieveObject("/PTTopTen/Bonus") as DataTable;

            if (dt == null || (dt.Rows.Count > 0 && (DateTime.Now - TypeConverter.ObjectToDateTime(dt.Rows[0]["lastupdate"])).TotalMinutes > 30))
            {
                double overtime = dt != null && dt.Rows.Count > 0 ? (DateTime.Now - TypeConverter.ObjectToDateTime(dt.Rows[0]["lastupdate"])).TotalMinutes : -1;
                bool needupdate = false;
                lock (SynObjectTopTen)
                {
                    //防止重复执行，同时防止更新不成功导致无法更新SynObjectCount == 0 || 
                    if ((DateTime.Now - LastUpdateTime_bonus).TotalMinutes > 30)
                    {
                        //SynObjectCount++;
                        needupdate = true;
                        LastUpdateTime_bonus = DateTime.Now;
                    }
                }
                if (needupdate)
                {
                    dt = DatabaseProvider.GetInstance().GetTopTenBonus();
                    DNTCache.GetCacheService().RemoveObject("/PTTopTen/Bonus");
                    DNTCache.GetCacheService().AddObject("/PTTopTen/Bonus", dt, 3600);
                    PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Warning, "UpdateTopBonus", string.Format("强制更新：Count:{0} -SYNC:{1} -No1:Count:{2} -Tid{3} -OVERTIME:{4} -Last:{5}", dt.Rows.Count, SynObjectCount.ToString(), dt.Rows[0]["price"], dt.Rows[0]["tid"], overtime, LastUpdateTime_bonus));
                    //SynObjectCount = 0;
                }
                else
                {
                    //lock (SynObjectTopTen)
                    {
                        //if (SynObjectCount == 0)
                        {
                            //SynObjectCount++;
                            Top10TaskEventDelegate dotask = new Top10TaskEventDelegate(UpdateTopTenbyScheduleTaskDo);
                            dotask.BeginInvoke(null, null);
                            PTLog.InsertSystemLog(PTLog.LogType.TopTenEvent, PTLog.LogStatus.Warning, "UpdateTopTen", string.Format("前台更新：BONUS -OVERTIME:{0} -Last:{1}", overtime, LastUpdateTime_bonus));
                        }
                    } 
                    return GetEmptyTopTenBonus();
                }
            }
            
            return dt;
        }


        /// <summary>
        /// 获取一个空的TopTenSeeds
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmptyTopTenSeeds()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("seedid");
            dt.Columns.Add("topictitle");
            dt.Columns.Add("finishedtoday");
            dt.Columns.Add("type");
            dt.Columns.Add("topicid");
            dt.Columns.Add("lastupdate");
            dt.Columns.Add("typenameen");
            dt.Columns.Add("typename");

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["seedid"] = "0";
                dr["topictitle"] = "十大种子正在更新中，请稍后刷新页面";
                dr["finishedtoday"] = "0";
                dr["type"] = "10";
                dr["typenameen"] = "staff";
                dr["typename"] = "学习";
                dr["topicid"] = "105734";
                dr["lastupdate"] = DateTime.Now.ToString();
                dt.Rows.Add(dr);
            }


            return dt;
        }
        /// <summary>
        /// 获取一个空的TopTenBonus
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmptyTopTenBonus()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("tid");
            dt.Columns.Add("title");
            dt.Columns.Add("price");
            dt.Columns.Add("fid");
            dt.Columns.Add("typeid");
            dt.Columns.Add("typename");
            dt.Columns.Add("lastupdate");

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["tid"] = "105734";
                dr["title"] = "十大悬赏正在更新中，请稍后刷新页面";
                dr["price"] = "0";
                dr["fid"] = "4";
                dr["typeid"] = "0";
                dr["typename"] = "";
                dr["lastupdate"] = DateTime.Now.ToString();
                dt.Rows.Add(dr);
            }


            return dt;
        }

        /// <summary>
        /// 获取一个空的TopTenTopics
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmptyTopTenTopics()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("tid");
            dt.Columns.Add("title");
            dt.Columns.Add("repliestoday");
            dt.Columns.Add("fid");
            dt.Columns.Add("typeid");
            dt.Columns.Add("forumname");
            dt.Columns.Add("typename");
            dt.Columns.Add("lastupdate");

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["tid"] = "105734";
                dr["title"] = "十大主题正在更新中，请稍后刷新页面";
                dr["repliestoday"] = "0";
                dr["fid"] = "4";
                dr["typeid"] = "0";
                dr["forumname"] = "公告";
                dr["typename"] = "";
                dr["lastupdate"] = DateTime.Now.ToString();
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
