using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Discuz.Data;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;

namespace Discuz.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {

        public int GetTopicCount()
        {
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}gettotaltopiccount", BaseConfigs.GetTablePrefix)));
        }
        public IDataReader GetTopicTids(int statCount, int lastTid)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@lasttid", (DbType)SqlDbType.Int, 4, lastTid),
                DbHelper.MakeInParam("@statcount", (DbType)SqlDbType.Int, 4, statCount)
			};
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopictids", BaseConfigs.GetTablePrefix), parms);
        }


        public void UpdateTopic(int tid, int postCount, int lastPostId, string lastPost, int lastPosterId, string poster)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid", (DbType)SqlDbType.Int, 4, tid),
                                        DbHelper.MakeInParam("@postcount", (DbType)SqlDbType.Int, 4, postCount),
                                        DbHelper.MakeInParam("@lastpostid", (DbType)SqlDbType.Int, 4, lastPostId),
                                        DbHelper.MakeInParam("@lastpost", (DbType)SqlDbType.VarChar, 20, lastPost),
                                        DbHelper.MakeInParam("@lastposterid", (DbType)SqlDbType.Int, 4, lastPosterId),
                                        DbHelper.MakeInParam("@lastposter", (DbType)SqlDbType.VarChar, 20, poster)
                                    };
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatelastpostoftopic", BaseConfigs.GetTablePrefix), parms);
        }

        public void UpdateTopicLastPosterId(int tid)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@tid", (DbType)SqlDbType.Int, 4, tid)
			};
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatetopiclastposterid", BaseConfigs.GetTablePrefix), parms);
        }




        public IDataReader GetTopicInfo(int tid, int fid, byte mode)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, fid),
									   DbHelper.MakeInParam("@tid", (DbType)SqlDbType.Int, 4, tid),
                                       DbHelper.MakeInParam("@mode", (DbType)SqlDbType.Int, 4, mode)
			};
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopicinfo", BaseConfigs.GetTablePrefix), parms);
        }


        public IDataReader GetTopTopics(int fid, int pageSize, int pageIndex, string tids)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,fid),
									   DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pageSize),
									   DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageIndex),
									   DbHelper.MakeInParam("@tids",(DbType)SqlDbType.VarChar,500,tids)									   
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettoptopiclist", BaseConfigs.GetTablePrefix), parms);
        }


        ///// <summary>
        ///// 获得主题列表，，只在Archive页面使用，取消函数20140428
        ///// </summary>
        ///// <param name="forumId"></param>
        ///// <param name="pageId"></param>
        ///// <param name="tpp"></param>
        ///// <returns></returns>
        //public DataTable GetTopicList(int forumId, int pageId, int tpp)
        //{
        //    string commandText;
        //    if (pageId == 1)
        //    {
        //        commandText = string.Format("SELECT TOP {0} [tid],[title],[replies] FROM [{1}topics] WHERE [fid]={2} AND [displayorder]>=0 ORDER BY [lastpostid] DESC",
        //                                     tpp,
        //                                     BaseConfigs.GetTablePrefix,
        //                                     forumId);
        //    }
        //    else
        //    {
        //        commandText = string.Format("SELECT TOP {0} [tid],[title],[replies] FROM [{1}topics] WHERE [lastpostid] < (SELECT MIN([lastpostid])  FROM (SELECT TOP {2} [lastpostid] FROM [{1}topics] WHERE [fid]={3} AND [displayorder]>=0 ORDER BY [lastpostid] DESC) AS tblTmp ) AND [fid]={3} AND [displayorder]>=0 ORDER BY [lastpostid] DESC",
        //                                     tpp,
        //                                     BaseConfigs.GetTablePrefix,
        //                                     (pageId - 1) * tpp,
        //                                     forumId);
        //    }
        //    return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        //}

        /// <summary>
        ///  获取主题队列，用于管理页面：重建主题帖数
        /// </summary>
        /// <param name="startTid"></param>
        /// <param name="endTid"></param>
        /// <returns></returns>
        public IDataReader GetTopicList(int startTid, int endTid)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@start_tid", (DbType)SqlDbType.Int, 4, startTid),
				DbHelper.MakeInParam("@end_tid", (DbType)SqlDbType.Int, 4, endTid)
			};
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopics", BaseConfigs.GetTablePrefix), parms);
        }


        /// <summary>
        ///  获取板块热门主题队列
        /// </summary>
        /// <returns></returns>
        public DataTable GetForumHotTopicsList()
        {
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, string.Format("bt_forum_gethottopiclist")).Tables[0];
        }

        /// <summary>
        /// 获得指定的主题列表，用于管理页面：记录板块热门帖子列表
        /// </summary>
        /// <param name="topicList">主题ID列表</param>
        /// <param name="displayOrder">order的下限( WHERE [displayorder]>此值)</param>
        /// <returns></returns>
        public DataTable GetTopicList(string topicList, int displayOrder)
        {
            string commandText = string.Format("SELECT {0} FROM [{1}topics] WHERE [displayorder]>{2} AND [tid] IN ({3})",
                                                DbFields.TOPICS,
                                                BaseConfigs.GetTablePrefix,
                                                displayOrder,
                                                topicList);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获取最精华帖子列表，showtopiclist页面使用
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="startNum"></param>
        /// <param name="forumlist"></param>
        /// <param name="digest"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascDesc"></param>
        /// <returns></returns>
        public IDataReader GetTopicListGlobalDigest(int pageSize, int pageIndex, int startNum, string forumlist, int digest, string orderBy, int ascDesc)
        {
            DataTable dt = PTTools.GetIntTableFromString(forumlist);
            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
									   DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4, pageSize),
									   DbHelper.MakeInParam("@pagepad",(DbType)SqlDbType.Int,4, (pageIndex - 1) * pageSize - startNum),
			                           DbHelper.MakeInParam("@digest",(DbType)SqlDbType.Int,4, digest),
								   };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";

            string commandText = string.Format("SELECT TOP (@pagesize) {0} FROM [{1}topics] WITH(NOLOCK) WHERE ", SQL_TOPIC_SHOWFORUM_LIST, BaseConfigs.GetTablePrefix);

            string conditionText = string.Format("[displayorder] >= 0 AND [fid] IN (SELECT [IntValue] FROM @TVP) AND [digest] > @digest ");

            string orderbyText = string.Format("ORDER BY {0} {1}",
                                     orderBy == "lastpostid" ? "[lastpostid]" : "[tid]",
                                     ascDesc == 1 ? "DESC" : "ASC");

            string pageText = "";
            if (pageIndex > 1) pageText = string.Format("AND {0} {1} (SELECT {2}({0}) FROM (SELECT TOP (@pagepad) {0} FROM [{3}topics] WITH(NOLOCK) WHERE {4}{5}) AS tblTmp) ",
                                      orderBy == "lastpostid" ? "[lastpostid]" : "[tid]",
                                      ascDesc == 1 ? "<" : ">",
                                      ascDesc == 1 ? "MIN" : "MAX",
                                      BaseConfigs.GetTablePrefix,
                                      conditionText,
                                      orderbyText
                                     );

            commandText = commandText + conditionText + pageText + orderbyText;

            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }


        public const string SQL_TOPIC_SHOWFORUM_LIST = "[tid],[seedid],[fid],[iconid],[typeid],[title],[special],[price],[hide],[readperm],[poster],[posterid],[replies],[views],[postdatetime],[lastpost],[lastposter],[lastpostid],[lastposterid],[highlight],[digest],[displayorder],[closed],[attachment],[magic],[rate]";

        /// <summary>
        /// 获取最新帖子列表，showtopiclist页面使用
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="startNum"></param>
        /// <param name="forumlist"></param>
        /// <param name="lastpost"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascDesc">0 ASC  1 DESC</param>
        /// <returns></returns>
        public IDataReader GetTopicListGlobalNew(int pageSize, int pageIndex, int startNum, string forumlist, DateTime lastpost, string orderBy, int ascDesc)
        {
            DataTable dt = PTTools.GetIntTableFromString(forumlist);
            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
									   DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4, pageSize),
									   DbHelper.MakeInParam("@pagepad",(DbType)SqlDbType.Int,4, (pageIndex - 1) * pageSize - startNum),
			                           DbHelper.MakeInParam("@lastpost",(DbType)SqlDbType.DateTime,8, lastpost),
								   };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";

            string commandText = string.Format("SELECT TOP (@pagesize) {0} FROM [{1}topics] WITH(NOLOCK) WHERE ", SQL_TOPIC_SHOWFORUM_LIST, BaseConfigs.GetTablePrefix);

            string conditionText = string.Format("[displayorder] >= 0 AND [fid] IN (SELECT [IntValue] FROM @TVP) AND [lastpost] > @lastpost ");

            string orderbyText = string.Format("ORDER BY {0} {1}",
                                     orderBy == "lastpostid" ? "[lastpostid]" : "[tid]",
                                     ascDesc == 1 ? "DESC" : "ASC");

            string pageText = "";
            if (pageIndex > 1) pageText = string.Format("AND {0} {1} (SELECT {2}({0}) FROM (SELECT TOP (@pagepad) {0} FROM [{3}topics] WITH(NOLOCK) WHERE {4}{5}) AS tblTmp) ",
                                      orderBy == "lastpostid" ? "[lastpostid]" : "[tid]",
                                      ascDesc == 1 ? "<" : ">",
                                      ascDesc == 1 ? "MIN" : "MAX",
                                      BaseConfigs.GetTablePrefix,
                                      conditionText,
                                      orderbyText
                                     );

            commandText = commandText + conditionText + pageText + orderbyText;

            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }



        /// <summary>
        /// 获取正常帖子列表（不包括置顶帖）
        /// </summary>
        /// <param name="tlf"></param>
        /// <returns></returns>
        public IDataReader GetTopicList(TopicListFilterInfo tlf)
        {
            //筛选【typeid】int,【special】int, 【postdatetime】datetime
            //排序【tid】，【views】，【replies】以及默认的【lastpostid】
            //常用 筛选：默认，typeid：排序lastpostid和tid，使用存储过程
            //不常用使用组合SQL、参数化的方式

            //参数校验
            tlf.Displayorder = 0; //正常的主题，不获取置顶帖
            tlf.Orderby = tlf.Orderby.Trim().ToLower();
            if (tlf.Orderby == "" || tlf.Orderby.IndexOf(",") > -1) tlf.Orderby = "lastpostid";
            if (",lastpostid,tid,views,replies,".IndexOf("," + tlf.Orderby + ",") < 0) tlf.Orderby = "lastpostid";

            //正常帖子，回帖发帖逆序（默认）排序，使用存储过程
            if (tlf.Closed == -1 && tlf.Special == -1 && tlf.Interval < 1 && (tlf.Orderby == "lastpostid" || tlf.Orderby == "tid") && tlf.Desc == true && tlf.Pageindex == 1)
            {
                if (tlf.Topictypeid == -1) //不分类
                {
                    DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,tlf.Fid),
                                       DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,tlf.Pagesize - tlf.Pagestart),
                                       DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.Int,4, tlf.Displayorder),
								   };
                    if (tlf.Orderby == "lastpostid")
                        return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyfid_lastpostid", BaseConfigs.GetTablePrefix), parms);
                    else
                        return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyfid_tid", BaseConfigs.GetTablePrefix), parms);
                }
                else //分类
                {
                    DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,tlf.Fid),
                                       DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,tlf.Pagesize),
                                       DbHelper.MakeInParam("@topictypeid",(DbType)SqlDbType.Int,4,tlf.Topictypeid - tlf.Pagestart),
                                       DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.Int,4, tlf.Displayorder),
								   };
                    if (tlf.Orderby == "lastpostid")
                        return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyfidtopictypeid_lastpostid", BaseConfigs.GetTablePrefix), parms);
                    else
                        return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyfidtopictypeid_tid", BaseConfigs.GetTablePrefix), parms);
                }

            }
            else if (tlf.Closed == -1 && tlf.Special == -1 && tlf.Interval < 1 && (tlf.Orderby == "lastpostid" || tlf.Orderby == "tid") && tlf.Desc == true)
            {
                if (tlf.Topictypeid == -1) //不分类
                {
                    DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,tlf.Fid),
                                       DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,tlf.Pagesize),
                                       DbHelper.MakeInParam("@pagepad",(DbType)SqlDbType.Int,4,(tlf.Pageindex - 1) * tlf.Pagesize - tlf.Pagestart),
                                       DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.Int,4, tlf.Displayorder),
								   };
                    if (tlf.Orderby == "lastpostid")
                        return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyfid_lastpostidpage", BaseConfigs.GetTablePrefix), parms);
                    else
                        return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyfid_tidpage", BaseConfigs.GetTablePrefix), parms);
                }
                else //分类
                {
                    DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,tlf.Fid),
                                       DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,tlf.Pagesize),
                                       DbHelper.MakeInParam("@pagepad",(DbType)SqlDbType.Int,4,(tlf.Pageindex - 1) * tlf.Pagesize - tlf.Pagestart),
                                       DbHelper.MakeInParam("@topictypeid",(DbType)SqlDbType.Int,4,tlf.Topictypeid),
                                       DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.Int,4, tlf.Displayorder),
								   };
                    if (tlf.Orderby == "lastpostid")
                        return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyfidtopictypeid_lastpostidpage", BaseConfigs.GetTablePrefix), parms);
                    else
                        return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyfidtopictypeid_tidpage", BaseConfigs.GetTablePrefix), parms);
                }
            }
            //其他情况，拼接SQL
            else
            {
                string commandText = string.Format("SELECT TOP(@pagesize) {1} FROM [{0}topics] WITH(NOLOCK) WHERE",
                                                    BaseConfigs.GetTablePrefix,
                                                    DbFields.TOPICS_GETLIST1);

                string conditionText = string.Format(" [fid] = @fid AND [displayorder] = @displayorder {0}{1}{2}{3}{4}", //{5}{6}{7}
                                                    (tlf.Topictypeid > 0) ? " AND [typeid] = @topictypeid" : "",
                                                    (tlf.Special > 0) ? " AND [special] = @special" : "",
                                                    (tlf.Special == -2) ? " AND ([special] = 2 OR [special] = 3)" : "",
                                                    (tlf.Special == -3) ? " AND [digest] > 0" : "", 
                                                    (tlf.Topictypeid < 0 && tlf.Interval > 0) ? " AND [postdatetime] > @postdatetime" : ""); //当有分类是不加入日期查询？？  

                string orderbyText = string.Format(" ORDER BY {0}{1}{2}{3} {4}",
                                                     tlf.Orderby == "lastpostid" ? "[lastpostid]" : "",
                                                     tlf.Orderby == "tid" ? "[tid]" : "",
                                                     tlf.Orderby == "views" ? "[views]" : "",
                                                     tlf.Orderby == "replies" ? "[replies]" : "",
                                                     tlf.Desc == true ? "DESC" : "ASC");

                string pageText = "";
                if (tlf.Pageindex > 1) pageText = string.Format(" AND {0}{1}{2}{3} {4} (SELECT {5}({0}{1}{2}{3}) FROM (SELECT TOP (@pagepad) {0}{1}{2}{3} FROM [{6}topics] WITH(NOLOCK) WHERE{7}{8}) AS tblTmp) ",
                                                     tlf.Orderby == "lastpostid" ? "[lastpostid]" : "",
                                                     tlf.Orderby == "tid" ? "[tid]" : "",
                                                     tlf.Orderby == "views" ? "[views]" : "",
                                                     tlf.Orderby == "replies" ? "[replies]" : "",
                                                     tlf.Desc == true ? "<" : ">",
                                                     tlf.Desc == true ? "MIN" : "MAX",
                                                     BaseConfigs.GetTablePrefix,
                                                     conditionText,
                                                     orderbyText);

                commandText = commandText + conditionText + pageText + orderbyText;

                DbParameter[] parms = {
                                       DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,tlf.Fid),
                                       DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.Int,4, tlf.Displayorder),
                                       DbHelper.MakeInParam("@topictypeid",(DbType)SqlDbType.Int,4,tlf.Topictypeid),
									   DbHelper.MakeInParam("@closed",(DbType)SqlDbType.Int,4, tlf.Closed),
									   DbHelper.MakeInParam("@special",(DbType)SqlDbType.Int,4, tlf.Special),
                                       DbHelper.MakeInParam("@postdatetime",(DbType)SqlDbType.DateTime,8, DateTime.Now.AddDays(-tlf.Interval)),
                                       DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,pageText == "" ? tlf.Pagesize - tlf.Pagestart : tlf.Pagesize),
                                       DbHelper.MakeInParam("@pagepad",(DbType)SqlDbType.Int,4,(tlf.Pageindex - 1) * tlf.Pagesize - tlf.Pagestart),
								   };

                return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
            }
        }

        //取消函数20140428 由统一的GetTopicList函数代替
        ///// <summary>
        ///// 获取帖子列表
        ///// </summary>
        ///// <param name="fid"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="startNum"></param>
        ///// <param name="condition"></param>
        ///// <returns></returns>
        //public IDataReader GetTopicList(int fid, int pageSize, int pageIndex, int startNum, string condition)
        //{
        //    if (condition.Trim() == string.Empty)
        //    {
        //        DbParameter[] parms = {
        //                               DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,fid),
        //                               DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pageSize),
        //                               DbHelper.MakeInParam("@pageindex", (DbType)SqlDbType.Int,4,pageIndex),
        //                               DbHelper.MakeInParam("@startnum",(DbType)SqlDbType.Int,4,startNum)
        //                           };
        //        return DbHelper.ExecuteReader(CommandType.StoredProcedure,
        //                                      string.Format("{0}gettopiclist", BaseConfigs.GetTablePrefix),
        //                                      parms);
        //    }
        //    else
        //    {
        //        DbParameter[] parms = {
        //                               DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,fid),
        //                               DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pageSize),
        //                               DbHelper.MakeInParam("@pageindex", (DbType)SqlDbType.Int,4,pageIndex),
        //                               DbHelper.MakeInParam("@condition", (DbType)SqlDbType.VarChar,80,condition),
        //                               DbHelper.MakeInParam("@startnum", (DbType)SqlDbType.VarChar,80,startNum)
        //                           };
        //        return DbHelper.ExecuteReader(CommandType.StoredProcedure,
        //                                      string.Format("{0}gettopiclistbycondition", BaseConfigs.GetTablePrefix),
        //                                      parms);
        //    }
        //}
        //public IDataReader GetTopicListByDate(int fid, int pageSize, int pageIndex, int startNumber, string condition, string orderFields, int sortType)
        //{
        //    DbParameter[] parms = {
        //                               DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,fid),
        //                               DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pageSize),
        //                               DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageIndex),
        //                               DbHelper.MakeInParam("@startnum",(DbType)SqlDbType.Int,4,startNumber),
        //                               DbHelper.MakeInParam("@condition",(DbType)SqlDbType.VarChar,80,condition),
        //                               DbHelper.MakeInParam("@orderby",(DbType)SqlDbType.VarChar,80,orderFields),
        //                               DbHelper.MakeInParam("@ascdesc",(DbType)SqlDbType.Int,4,sortType)				                    
        //                           };
        //    return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbydate", BaseConfigs.GetTablePrefix), parms);
        //}
        //public IDataReader GetTopicListByViewsOrReplies(int fid, int pageSize, int pageIndex, int startNumber, string condition, string orderFields, int sortType)
        //{
        //    DbParameter[] parms = {
        //                               DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,fid),
        //                               DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pageSize),
        //                               DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageIndex),
        //                               DbHelper.MakeInParam("@startnum",(DbType)SqlDbType.Int,4,startNumber),
        //                               DbHelper.MakeInParam("@condition",(DbType)SqlDbType.VarChar,80,condition),
        //                               DbHelper.MakeInParam("@orderby",(DbType)SqlDbType.VarChar,80,orderFields),
        //                               DbHelper.MakeInParam("@ascdesc",(DbType)SqlDbType.Int,4,sortType)				                    
        //                           };
        //    return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettopiclistbyvieworreplies", BaseConfigs.GetTablePrefix), parms);
        //}



        /// <summary>
        /// 列新主题的回复数
        /// </summary>
        /// <param name="tid">主题ID</param>
        /// <param name="postTableid">回复表ID</param>
        public int UpdateTopicReplyCount(int tid, string postTableid)
        {
            DbParameter[] parms = {
									  DbHelper.MakeInParam("@tid", (DbType)SqlDbType.Int,4, tid)
			                      };
            string commandText = string.Format("UPDATE [{0}topics] SET [replies]=(SELECT COUNT([pid]) FROM [{0}posts{1}] WHERE [tid]= @tid) - 1 WHERE [displayorder]>=0 AND [tid]= @tid",
                                                BaseConfigs.GetTablePrefix,
                                                postTableid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }


        /// <summary>
        /// 得到当前版块内主题总数（从forum表读取记录的数值）
        /// </summary>
        /// <param name="fid">版块ID</param>
        /// <returns>主题总数</returns>
        public int GetTopicCount(int fid)
        {
            DbParameter param = DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, fid);
            return TypeConverter.ObjectToInt(
                                 DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                        string.Format("{0}gettopiccount", BaseConfigs.GetTablePrefix),
                                                        param));
        }

        /// <summary>
        /// 得到当前版块内(包括子版)正常(未关闭)主题总数
        /// </summary>
        /// <param name="subfidList">子版块列表</param>
        /// <returns>主题总数</returns>
        public int GetTopicCountOfForumWithSub(string subfidList)
        {
            DbParameter param = DbHelper.MakeInParam("@subfidList", (DbType)SqlDbType.NChar, 400, subfidList);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                        string.Format("{0}getalltopiccount", BaseConfigs.GetTablePrefix),
                                                        param));
        }

        /// <summary>
        /// 得到当前版块内主题总数
        /// </summary>
        /// <param name="fid">版块ID</param>
        /// <returns>主题总数</returns>
        public int GetTopicCount(TopicListFilterInfo tlf)
        {
            //筛选【typeid】int,【special】int, 【postdatetime】datetime
            //排序【tid】，【views】，【replies】以及默认的【lastpostid】
            //常用 筛选：默认，typeid：排序lastpostid和tid，使用存储过程
            //不常用使用组合SQL、参数化的方式

            tlf.Displayorder = -1;

            if (tlf.Closed == -1 && tlf.Special == -1 && tlf.Interval < 1)
            {
                if (tlf.Topictypeid == -1)
                {
                    DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,tlf.Fid),
								   };
                    return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                             string.Format("{0}gettopiccount", BaseConfigs.GetTablePrefix),
                                                                             parms), -1);
                }
                else
                {
                    DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,tlf.Fid),
                                       DbHelper.MakeInParam("@topictypeid",(DbType)SqlDbType.Int,4,tlf.Topictypeid),
								   };
                    return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                             string.Format("{0}gettopiccountbytopictypeid", BaseConfigs.GetTablePrefix),
                                                                             parms), -1);
                }

            }
            else
            {
                DbParameter[] parms = {
                                       DbHelper.MakeInParam("@fid",(DbType)SqlDbType.Int,4,tlf.Fid),
                                       DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.Int,4, tlf.Displayorder),
                                       DbHelper.MakeInParam("@topictypeid",(DbType)SqlDbType.Int,4,tlf.Topictypeid),
									   DbHelper.MakeInParam("@closed",(DbType)SqlDbType.Int,4, tlf.Closed),
									   DbHelper.MakeInParam("@special",(DbType)SqlDbType.Int,4, tlf.Special),
                                       DbHelper.MakeInParam("@postdatetime",(DbType)SqlDbType.DateTime,8, DateTime.Now.AddDays(-tlf.Interval))
								   };
                string commandText = string.Format("SELECT COUNT(tid) FROM [{0}topics] WITH(NOLOCK) WHERE [fid] = @fid AND [displayorder] > @displayorder AND [closed] {1}{2}{3}{4}{5}{6}",
                                                    BaseConfigs.GetTablePrefix,
                                                    (tlf.Closed == -1) ? "<= 1" : "= @closed",
                                                    (tlf.Topictypeid > 0) ? " AND [typeid] = @topictypeid" : "",
                                                    (tlf.Special > 0) ? " AND [special] = @special" : "",
                                                    (tlf.Special == -2) ? " AND ([special] = 2 OR [special] = 3)" : "",
                                                    (tlf.Special == -3) ? " AND [digest] > 0" : "",
                                                    (tlf.Topictypeid < 0 && tlf.Interval > 0) ? " AND [postdatetime] > @postdatetime" : ""   //当有分类是不加入日期查询？？      
                                                    );
                return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
            }
        }

        //取消函数20140426
        ///// <summary>
        ///// 得到符合条件的主题总数
        ///// </summary>
        ///// <param name="condition">条件</param>
        ///// <returns>主题总数</returns>
        //public int GetTopicCount(string condition)
        //{
        //    DbParameter param = DbHelper.MakeInParam("@condition", (DbType)SqlDbType.VarChar, 4000, condition);
        //    return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
        //                                                             string.Format("{0}gettopiccountbytype", BaseConfigs.GetTablePrefix),
        //                                                             param), -1);
        //}

        /// <summary>
        /// 得到符合条件的主题总数:条件 lastpost，forumlist
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>主题总数</returns>
        public int GetTopicCount(string forumlist, DateTime lastpost)
        {
            DataTable dt = PTTools.GetIntTableFromString(forumlist);
            if (dt.Rows.Count < 1) return -1;
            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
									  DbHelper.MakeInParam("@lastpost", (DbType)SqlDbType.DateTime,8, lastpost)
			                      };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";

            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}gettopiccountbylastpost", BaseConfigs.GetTablePrefix), parms), -1);
        }
        /// <summary>
        /// 得到符合条件的主题总数:条件 digest，forumlist
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>主题总数</returns>
        public int GetTopicCount(string forumlist, int digest)
        {
            DataTable dt = PTTools.GetIntTableFromString(forumlist);
            if (dt.Rows.Count < 1) return -1;
            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
									  DbHelper.MakeInParam("@digest", (DbType)SqlDbType.Int,4, digest)
			                      };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";

            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}gettopiccountbydigest", BaseConfigs.GetTablePrefix), parms), -1);
        }

        /// <summary>
        /// 更新主题为已被管理
        /// </summary>
        /// <param name="topiclist">主题id列表</param>
        /// <param name="moderated">管理操作id</param>
        /// <returns>成功返回1，否则返回0</returns>
        public int UpdateTopicModerated(string topicList, int moderated)
        {
            string commandText = string.Format("UPDATE [{0}topics] SET [moderated] = {1} WHERE [tid] IN ({2})",
                                                BaseConfigs.GetTablePrefix,
                                                moderated,
                                                topicList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新主题
        /// </summary>
        /// <param name="topicinfo">主题信息</param>
        /// <returns>成功返回1，否则返回0</returns>
        public int UpdateTopic(TopicInfo topicInfo)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4, topicInfo.Tid),
									   DbHelper.MakeInParam("@fid", (DbType)SqlDbType.SmallInt, 2, topicInfo.Fid), 
									   DbHelper.MakeInParam("@iconid", (DbType)SqlDbType.SmallInt, 2, topicInfo.Iconid), 
                                       
                                       //////////////////////////////////////////////////////////////////////////
                                       ////////////////////////////////////////////////////////////////////////// 
                                       //【BT修改 60-255】
                                       
                                       DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 255, topicInfo.Title), 
                                       
                                       //【END BT修改】
                                       ////////////////////////////////////////////////////////////////////////// 
                                       ////////////////////////////////////////////////////////////////////////// 
									   
									   DbHelper.MakeInParam("@typeid", (DbType)SqlDbType.SmallInt, 2, topicInfo.Typeid), 
									   DbHelper.MakeInParam("@readperm", (DbType)SqlDbType.Int, 4, topicInfo.Readperm), 
									   DbHelper.MakeInParam("@price", (DbType)SqlDbType.SmallInt, 2, topicInfo.Price), 
									   DbHelper.MakeInParam("@poster", (DbType)SqlDbType.NChar, 15, topicInfo.Poster), 
									   DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.Int, 4, topicInfo.Posterid), 
									   DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.SmallDateTime, 4, DateTime.Parse(topicInfo.Postdatetime)), 
									   DbHelper.MakeInParam("@lastpost", (DbType)SqlDbType.SmallDateTime, 0, topicInfo.Lastpost), 
                                       DbHelper.MakeInParam("@lastpostid", (DbType)SqlDbType.Int, 4, topicInfo.Lastpostid), 
									   DbHelper.MakeInParam("@lastposter", (DbType)SqlDbType.NChar, 15, topicInfo.Lastposter), 
									   DbHelper.MakeInParam("@replies", (DbType)SqlDbType.Int, 4, topicInfo.Replies), 
									   DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, topicInfo.Displayorder), 
									   DbHelper.MakeInParam("@highlight", (DbType)SqlDbType.VarChar, 500, topicInfo.Highlight), 
									   DbHelper.MakeInParam("@digest", (DbType)SqlDbType.Int, 4, topicInfo.Digest), 
									   DbHelper.MakeInParam("@rate", (DbType)SqlDbType.Int, 4, topicInfo.Rate), 
									   DbHelper.MakeInParam("@hide", (DbType)SqlDbType.Int, 4, topicInfo.Hide), 
									   DbHelper.MakeInParam("@special", (DbType)SqlDbType.Int, 4, topicInfo.Special), 
									   DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.Int, 4, topicInfo.Attachment), 
									   DbHelper.MakeInParam("@moderated", (DbType)SqlDbType.Int, 4, topicInfo.Moderated), 
									   DbHelper.MakeInParam("@closed", (DbType)SqlDbType.Int, 4, topicInfo.Closed),
									   DbHelper.MakeInParam("@magic", (DbType)SqlDbType.Int, 4, topicInfo.Magic),
								   };
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatetopic", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 判断帖子列表是否都在当前板块
        /// </summary>
        /// <param name="topicidlist"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        public int GetTopicCountInForumAndTopicIdList(string topicIdList, int fid)
        {
            string commandText = string.Format("SELECT COUNT([tid]) FROM [{0}topics] WHERE [fid]={1} AND [tid] IN ({2})",
                                                BaseConfigs.GetTablePrefix,
                                                fid,
                                                topicIdList);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 将主题设置为隐藏主题
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public int UpdateTopicHide(int tid)
        {
            string commandText = string.Format(@"UPDATE [{0}topics] SET [hide] = 1 WHERE [tid] = {1}",
                                                 BaseConfigs.GetTablePrefix,
                                                 tid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }



        public DataTable GetTopicFidByTid(string tidList)
        {
            string commandText = string.Format("SELECT DISTINCT [fid] FROM [{0}topics] WHERE [tid] IN({1})",
                                                BaseConfigs.GetTablePrefix,
                                                tidList);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public DataTable GetTopicTidByFid(string tidList, int fid)
        {
            DbParameter[] parms = 
			{
				DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int,4, fid)
            };
            string commandText = string.Format("SELECT [tid] FROM [{0}topics] WHERE [tid] IN({1}) AND [fid]=@fid",
                                                BaseConfigs.GetTablePrefix,
                                                tidList);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText, parms).Tables[0];
        }

        /// <summary>
        /// 更新主题浏览量
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="viewcount">浏览量</param>
        /// <returns>成功返回1，否则返回0</returns>
        public int UpdateTopicViewCount(int tid, int viewCount)
        {
            DbParameter[] parms = {
										DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,tid),	
										DbHelper.MakeInParam("@viewcount",(DbType)SqlDbType.Int,4,viewCount)			   
									};
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatetopicviewcount", BaseConfigs.GetTablePrefix), parms);
        }

        public string SearchTopics(int forumId, string keyWord, string displayOrder, string digest, string attachment, string poster, bool lowerUpper, string viewsMin, string viewsMax,
                                        string repliesMax, string repliesMin, string rate, string lastPost, DateTime postDateTimeStart, DateTime postDateTimeEnd)
        {
            StringBuilder commandText = new StringBuilder(" [tid]>0");

            if (forumId != 0)
                commandText.AppendFormat(" AND [fid]={0}", forumId);

            if (keyWord != "")
            {
                commandText.Append(" AND (");
                foreach (string word in keyWord.Split(','))
                {
                    if (word.Trim() != "")
                        commandText.AppendFormat(" [title] LIKE '%{0}%' OR ", RegEsc(word));
                }
                commandText.Remove(commandText.Length - 3, 3).Append(")");
            }

            switch (displayOrder)
            {
                case "0":
                    break;
                case "1":
                    commandText.Append(" AND [displayorder]>0");
                    break;
                case "2":
                    commandText.Append(" AND [displayorder]<=0");
                    break;
            }

            switch (digest)
            {
                case "0":
                    break;
                case "1":
                    commandText.Append(" AND [digest]>=1");
                    break;
                case "2":
                    commandText.Append(" AND [digest]<1");
                    break;
            }

            switch (attachment)
            {
                case "0":
                    break;
                case "1":
                    commandText.Append(" AND [attachment]>0");
                    break;
                case "2":
                    commandText.Append(" AND [attachment]<=0");
                    break;
            }

            if (poster != "")
            {
                commandText.AppendFormat(" AND (");
                foreach (string postername in poster.Split(','))
                {
                    if (postername.Trim() != "")
                    {
                        //不区别大小写
                        if (lowerUpper)
                            commandText.AppendFormat(" [poster]='{0}' OR", postername);
                        else
                            commandText.AppendFormat(" [poster] COLLATE Chinese_PRC_CS_AS_WS ='{0}' OR", postername);
                    }
                }
                commandText.Remove(commandText.Length - 3, 3).Append(")");
            }

            if (viewsMax != "")
                commandText.AppendFormat(" AND [views]>{0}", viewsMax);

            if (viewsMin != "")
                commandText.AppendFormat(" AND [views]<{0}", viewsMin);

            if (repliesMax != "")
                commandText.AppendFormat(" AND [replies]>{0}", repliesMax);

            if (repliesMin != "")
                commandText.AppendFormat(" AND [replies]<{0}", repliesMin);

            if (rate != "")
                commandText.AppendFormat(" AND [rate]>{0}", rate);

            if (lastPost != "")
                commandText.AppendFormat(" AND DATEDIFF(day,[lastpost],GETDATE())>={0}", lastPost);

            return GetSqlstringByPostDatetime(commandText.ToString(), postDateTimeStart, postDateTimeEnd);
        }


        /// <summary>
        /// 按照用户Id获取主题总数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetTopicCountByUserId(int userId)
        {
            string commandText = string.Format("SELECT COUNT(1) FROM [{0}mytopics] WHERE [uid] = {1}",
                                                BaseConfigs.GetTablePrefix,
                                                userId);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 按照用户Id获取主题列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IDataReader GetTopicListByUserId(int userId, int pageIndex, int pageSize)
        {
            DbParameter[] parms = {
										DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, userId),
										DbHelper.MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, pageIndex),
										DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int, 4, pageSize)
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getmytopics", BaseConfigs.GetTablePrefix), parms);
        }

        public int CreateTopic(TopicInfo topicInfo)
        {
            DbParameter[] parms = {
										DbHelper.MakeInParam("@fid", (DbType)SqlDbType.SmallInt, 2, topicInfo.Fid), 
										DbHelper.MakeInParam("@iconid", (DbType)SqlDbType.SmallInt, 2, topicInfo.Iconid), 
                                        
                                        //////////////////////////////////////////////////////////////////////////
                                        ////////////////////////////////////////////////////////////////////////// 
                                        //【BT修改】60->255

                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 255, topicInfo.Title), 
                                        DbHelper.MakeInParam("@seedid", (DbType)SqlDbType.Int, 4, topicInfo.SeedId), 

                                        //【END BT修改】
                                        //////////////////////////////////////////////////////////////////////////
                                        //////////////////////////////////////////////////////////////////////////
										
										DbHelper.MakeInParam("@typeid", (DbType)SqlDbType.SmallInt, 2, topicInfo.Typeid), 
										DbHelper.MakeInParam("@readperm", (DbType)SqlDbType.Int, 4, topicInfo.Readperm), 
										DbHelper.MakeInParam("@price", (DbType)SqlDbType.SmallInt, 2, topicInfo.Price), 
										DbHelper.MakeInParam("@poster", (DbType)SqlDbType.NChar, 15, topicInfo.Poster), 
										DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.Int, 4, topicInfo.Posterid), 
										DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime,8, DateTime.Parse(topicInfo.Postdatetime)), 
										DbHelper.MakeInParam("@lastpost", (DbType)SqlDbType.VarChar, 0, topicInfo.Lastpost), 
										DbHelper.MakeInParam("@lastpostid", (DbType)SqlDbType.Int, 4, topicInfo.Lastpostid),
										DbHelper.MakeInParam("@lastposter", (DbType)SqlDbType.NChar, 15, topicInfo.Lastposter), 
										DbHelper.MakeInParam("@views", (DbType)SqlDbType.Int, 4, topicInfo.Views), 
										DbHelper.MakeInParam("@replies", (DbType)SqlDbType.Int, 4, topicInfo.Replies), 
										DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, topicInfo.Displayorder), 
										DbHelper.MakeInParam("@highlight", (DbType)SqlDbType.VarChar, 500, topicInfo.Highlight), 
										DbHelper.MakeInParam("@digest", (DbType)SqlDbType.Int, 4, topicInfo.Digest), 
										DbHelper.MakeInParam("@rate", (DbType)SqlDbType.Int, 4, topicInfo.Rate), 
										DbHelper.MakeInParam("@hide", (DbType)SqlDbType.Int, 4, topicInfo.Hide), 
										DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.Int, 4, topicInfo.Attachment), 
										DbHelper.MakeInParam("@moderated", (DbType)SqlDbType.Int, 4, topicInfo.Moderated), 
										DbHelper.MakeInParam("@closed", (DbType)SqlDbType.Int, 4, topicInfo.Closed),
										DbHelper.MakeInParam("@magic", (DbType)SqlDbType.Int, 4, topicInfo.Magic),
                                        DbHelper.MakeInParam("@special", (DbType)SqlDbType.TinyInt, 1, topicInfo.Special),
                                        DbHelper.MakeInParam("@attention", (DbType)SqlDbType.Int, 4, topicInfo.Attention)
								   };
            int tid = TypeConverter.ObjectToInt(DbHelper.ExecuteDataset(CommandType.StoredProcedure,
                                                                     string.Format("{0}createtopic", BaseConfigs.GetTablePrefix),
                                                                     parms).Tables[0].Rows[0][0], -1);
            if (tid != -1)
            {
                TrendType trendType = TrendType.Topic;
                switch (topicInfo.Special)
                {
                    case 0:
                        trendType = TrendType.Topic;
                        break;
                    case 1:
                        trendType = TrendType.Poll;
                        break;
                    case 2:
                        trendType = TrendType.Bonus;
                        break;
                    case 4:
                        trendType = TrendType.Debate;
                        break;
                }
                UpdateTrendStat(trendType);
            }
            return tid;
        }


        /// <summary>
        /// 更新主题所属版块,会将主题分类至为0
        /// </summary>
        /// <param name="topiclist"></param>
        /// <param name="fid"></param>
        /// <param name="topicType">要绑定的主题类型</param>
        /// <returns></returns>
        public int UpdateTopic(string topicList, int fid, int topicType)
        {
            DbParameter[] parms =
					{
						DbHelper.MakeInParam("@fid", (DbType)SqlDbType.SmallInt, 1, fid),
                        DbHelper.MakeInParam("@typeid", (DbType)SqlDbType.SmallInt, 1, topicType)
					};
            string commandText = string.Format("UPDATE [{0}topics] SET [fid]=@fid, [typeid]=@typeid WHERE [tid] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                topicList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        //取消
        //public int DeleteTopic(int tid)
        //{
        //    return DbHelper.ExecuteNonQuery(CommandType.Text,
        //                                    string.Format("DELETE FROM [{0}topics] WHERE [tid] = {1}", BaseConfigs.GetTablePrefix, tid));
        //}

        ///// <summary>
        ///// 取消 根据主题ID删除相应的主题信息
        ///// </summary>
        ///// <param name="tid"></param>
        ///// <returns></returns>
        //public bool DeleteTopicByTid(int tid, string postTableName)
        //{
        //    SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
        //    conn.Open();
        //    using (SqlTransaction trans = conn.BeginTransaction())
        //    {
        //        try
        //        {
        //            DbHelper.ExecuteNonQuery(trans, string.Format("DELETE FROM [{0}attachments] WHERE [tid]={1}", BaseConfigs.GetTablePrefix, tid));
        //            DbHelper.ExecuteNonQuery(trans, string.Format("DELETE FROM [{0}favorites] WHERE [tid]={1}", BaseConfigs.GetTablePrefix, tid));
        //            DbHelper.ExecuteNonQuery(trans, string.Format("DELETE FROM [{0}polls] WHERE [tid]={1}", BaseConfigs.GetTablePrefix, tid));
        //            DbHelper.ExecuteNonQuery(trans, string.Format("DELETE FROM [{0}] WHERE [tid]={1}", postTableName, tid));
        //            DbHelper.ExecuteNonQuery(trans, string.Format("DELETE FROM [{0}topics] WHERE [tid]={1}", BaseConfigs.GetTablePrefix, tid));
        //            trans.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            trans.Rollback();
        //            throw ex;
        //        }
        //    }
        //    conn.Close();
        //    return true;
        //}

        /// <summary>
        /// 删除指定主题
        /// </summary>
        /// <param name="topiclist">要删除的主题ID列表</param>
        /// <param name="posttableid">所以分表的ID</param>
        /// <param name="chanageposts">删除帖时是否要减版块帖数</param>
        /// <returns></returns>
        public int DeleteTopicByTidList(string topicList, string postTableId, bool changePosts)
        {
            DbParameter[] parms = {
					DbHelper.MakeInParam("@tidlist", (DbType)SqlDbType.VarChar, 2000, topicList),
                    DbHelper.MakeInParam("@chanageposts",(DbType)SqlDbType.Bit,1,changePosts)
				};
            string commandText = string.Format("{0}deletetopicbytidlist{1}", BaseConfigs.GetTablePrefix, postTableId);
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, commandText, parms);
        }

        /// <summary>
        /// 更新主题是否包含附件
        /// </summary>
        /// <param name="tid">主题Id</param>
        /// <param name="hasAttachment">是否包含附件,0不包含,1包含</param>
        /// <returns></returns>
        public int UpdateTopicAttachment(int tid, int hasAttachment)
        {
            string commandText = string.Format("UPDATE [{0}topics] SET [attachment]={1} WHERE [tid]={2}",
                                                BaseConfigs.GetTablePrefix,
                                                hasAttachment,
                                                tid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }


        public void UpdateTopicLastPoster(int lastPosterId, string lastPoster)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@lastposter", (DbType)SqlDbType.VarChar, 20, lastPoster),
                                        DbHelper.MakeInParam("@lastposterid", (DbType)SqlDbType.Int, 4, lastPosterId)
                                    };
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}getunauditpost", BaseConfigs.GetTablePrefix), parms);
        }

        public void UpdateTopicPoster(int posterId, string poster)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.Int, 4, posterId),
                                        DbHelper.MakeInParam("@poster", (DbType)SqlDbType.VarChar, 20, poster)
                                    };
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatetopicposter", BaseConfigs.GetTablePrefix), parms);
        }


    }
}
