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
        //不带表名的
        //public const string BT_SQL_SEEDLIST_NOTABLE = " [seedid],[uid],[upload],[download],[finished],[downloadratio],[uploadratio],[filecount],[filesize],[live],[lastlive],[topicid],[type],[topseed],[topictitle],[username],[traffic],[uptraffic],[ipv6],[bluehour],[uploadratioexpiredate],[downloadratioexpiredate] ";
        //public const string BT_SQL_TOPSEEDLIST_NOTABLE = " [seedid],[uid],[upload],[download],[finished],[downloadratio],[uploadratio],[filecount],[filesize],[live],[lastlive],[topicid],[type],[topseed],[topictitle],[username],[traffic],[uptraffic],[ipv6],[bluehour],[uploadratioexpiredate],[downloadratioexpiredate] ";
        /// <summary>
        /// bt_seed表，用于种子列表显示时，选择全部表项目时的列名，不包括infohash_c
        /// </summary>
        public const string SQL_SEED_LIST = " [seedid],[uid],[status],[upload],[download],[finished],[downloadratio],[downloadratioexpiredate],[uploadratio],[uploadratioexpiredate],[filecount],[filesize],[live],[lastlive],[topicid],[type],[topseed],[topictitle],[username],[views],[replies],[postdatetime],[traffic],[uptraffic],[ipv6],[accrss],[keeprss],[pubrss] ";
        /// <summary>
        /// bt_seed表，用于种子列表显示时，选择全部表项目时的列名，不包括infohash_c
        /// </summary>
        public const string SQL_SEED_LIST_b = " [b].[seedid],[b].[uid],[b].[status],[b].[upload],[b].[download],[b].[finished],[b].[downloadratio],[b].[downloadratioexpiredate],[b].[uploadratio],[b].[uploadratioexpiredate],[b].[filecount],[b].[filesize],[b].[live],[b].[lastlive],[b].[topicid],[b].[type],[b].[topseed],[b].[topictitle],[b].[username],[b].[views],[b].[replies],[b].[postdatetime],[b].[traffic],[b].[uptraffic],[b].[ipv6],[b].[accrss],[b].[keeprss],[b].[pubrss] ";
        /// <summary>
        /// bt_seed表，用于种子列表显示时，选择全部表项目时的列名
        /// </summary>
        public const string SQL_SEED_LIST_FULL = " [seedid],[uid],[status],[upload],[download],[finished],[downloadratio],[downloadratioexpiredate],[uploadratio],[uploadratioexpiredate],[filecount],[filesize],[live],[lastlive],[topicid],[type],[topseed],[topictitle],[username],[views],[replies],[postdatetime],[traffic],[uptraffic],[ipv6],[infohash_c],[accrss],[keeprss],[pubrss] ";
        /// <summary>
        /// bt_seed_tracker表，用于tracker访问时，选择的列名
        /// </summary>
        public const string SQL_SEED_TRACKER_LIST = " [seedid],[uid],[status],[downloadratio],[uploadratio],[filesize] ";
        /// <summary>
        /// bt_seed表，用于贴内详细信息显示的时候，全部表项目的列名，包含表名，不包括infohash_c
        /// </summary>
        public const string SQL_SEED_LIST_WITHTABLE = " [bt_seed].[seedid],[bt_seed].[uid],[bt_seed].[status],[bt_seed].[upload],[bt_seed].[download],[bt_seed].[finished],[bt_seed].[downloadratio],[bt_seed].[downloadratioexpiredate],[bt_seed].[uploadratio],[bt_seed].[uploadratioexpiredate],[bt_seed].[filecount],[bt_seed].[filesize],[bt_seed].[live],[bt_seed].[lastlive],[bt_seed].[topicid],[bt_seed].[type],[bt_seed].[topseed],[bt_seed].[topictitle],[bt_seed].[username],[bt_seed].[views],[bt_seed].[replies],[bt_seed].[postdatetime],[bt_seed].[traffic],[bt_seed].[uptraffic],[bt_seed].[ipv6],[bt_seed].[accrss],[bt_seed].[keeprss],[bt_seed].[pubrss] ";
        /// <summary>
        /// bt_seed表，用于贴内详细信息显示的时候，全部表项目的列名，包含表名
        /// </summary>
        public const string SQL_SEED_LIST_WITHTABLE_FULL = " [bt_seed].[seedid],[bt_seed].[uid],[bt_seed].[status],[bt_seed].[upload],[bt_seed].[download],[bt_seed].[finished],[bt_seed].[downloadratio],[bt_seed].[downloadratioexpiredate],[bt_seed].[uploadratio],[bt_seed].[uploadratioexpiredate],[bt_seed].[filecount],[bt_seed].[filesize],[bt_seed].[live],[bt_seed].[lastlive],[bt_seed].[topicid],[bt_seed].[type],[bt_seed].[topseed],[bt_seed].[topictitle],[bt_seed].[username],[bt_seed].[views],[bt_seed].[replies],[bt_seed].[postdatetime],[bt_seed].[traffic],[bt_seed].[uptraffic],[bt_seed].[ipv6],[bt_seed].[infohash_c],[bt_seed].[accrss],[bt_seed].[keeprss],[bt_seed].[pubrss] ";
        /// <summary>
        /// bt_seed_detail表，用于贴内详细信息显示的时候，部分列名，包含表名
        /// </summary>
        public const string SQL_SEED_DETAIL_SHORT_WITHTABLE = " [bt_seed_detail].[filename],[bt_seed_detail].[infohash],[bt_seed_detail].[lastseederid],[bt_seed_detail].[lastseedername],[bt_seed_detail].[path] ";
        /// <summary>
        /// bt_seed_detail表，用于编辑等，全部列名，包含表名
        /// </summary>
        public const string SQL_SEED_DETAIL_FULL_WITHTABLE = " [bt_seed_detail].[path],[bt_seed_detail].[createdby],[bt_seed_detail].[createddate],[bt_seed_detail].[info1],[bt_seed_detail].[info2],[bt_seed_detail].[info3],[bt_seed_detail].[info4],[bt_seed_detail].[info5],[bt_seed_detail].[info6],[bt_seed_detail].[info7],[bt_seed_detail].[info8],[bt_seed_detail].[info9],[bt_seed_detail].[info10],[bt_seed_detail].[info11],[bt_seed_detail].[info12],[bt_seed_detail].[info13],[bt_seed_detail].[info14],[bt_seed_detail].[filename],[bt_seed_detail].[infohash],[bt_seed_detail].[singlefile],[bt_seed_detail].[foldername],[bt_seed_detail].[lastseederid],[bt_seed_detail].[lastseedername],[bt_seed_detail].[award] ";
        
        /// <summary>
        /// bt_seed表，插入值，列名
        /// </summary>
        public const string SQL_INSERT_SEED_LIST = " [uid],[status],[upload],[download],[finished],[downloadratio],[downloadratioexpiredate],[uploadratio],[uploadratioexpiredate],[filecount],[filesize],[live],[lastlive],[topicid],[type],[topseed],[topictitle],[username],[views],[replies],[postdatetime],[traffic],[uptraffic],[ipv6],[infohash_c] ";
        /// <summary>
        /// bt_seed表，插入值，数值
        /// </summary>
        public const string SQL_INSERT_SEED_VALUE = " @uid,@status,@upload,@download,@finished,@downloadratio,@downloadratioexpiredate,@uploadratio,@uploadratioexpiredate,@filecount,@filesize,@live,@lastlive,@topicid,@type,@topseed,@topictitle,@username,@views,@replies,@postdatetime,@traffic,@uptraffic,@ipv6,@infohash_c ";
        /// <summary>
        /// bt_seed_detail表，插入值，列名
        /// </summary>
        public const string SQL_INSERT_SEED_DETAIL_LIST = " [seedid],[path],[createdby],[createddate],[info1],[info2],[info3],[info4],[info5],[info6],[info7],[info8],[info9],[info10],[info11],[info12],[info13],[info14],[filename],[infohash],[singlefile],[foldername],[lastseederid],[lastseedername],[award] ";
        /// <summary>
        /// bt_seed_detail表，插入值，数值
        /// </summary>
        public const string SQL_INSERT_SEED_DETAIL_VALUE = " @seedid,@path,@createdby,@createddate,@info1,@info2,@info3,@info4,@info5,@info6,@info7,@info8,@info9,@info10,@info11,@info12,@info13,@info14,@filename,@infohash,@singlefile,@foldername,@lastseederid,@lastseedername,@award ";
        /// <summary>
        /// bt_seed_tracker表，插入值，列名
        /// </summary>
        public const string SQL_INSERT_SEED_TRACKER_LIST = " [seedid],[uid],[status],[downloadratio],[uploadratio],[filesize],[infohash] ";
        /// <summary>
        /// bt_seed_tracker表，插入值，数值
        /// </summary>
        public const string SQL_INSERT_SEED_TRACKER_VALUE = " @seedid,@uid,@status,@downloadratio,@uploadratio,@filesize,@infohash ";
        
        /// <summary>
        /// bt_seed表，编辑种子信息，包括更新种子
        /// </summary>
        public const string SQL_EDIT_SEED_WITHSEED = " [status]=@status,[filecount]=@filecount,[filesize] = @filesize,[topicid]=@topicid,[type]=@type,[topictitle]=@topictitle,[infohash_c]=@infohash_c ";
        /// <summary>
        /// bt_seed_tracker表，编辑种子信息，包含更新种子
        /// </summary>
        public const string SQL_EDIT_SEED_TRACKER_WITHSEED = " [status]=@status,[filesize]=@filesize ";
        /// <summary>
        /// bt_seed_detail表，编辑种子信息，包含更新种子
        /// </summary>
        public const string SQL_EDIT_SEED_DETAIL_WITHSEED = " [createdby]=@createdby,[createddate]=@createddate,[path]=@path,[info1]=@info1,[info2]=@info2,[info3]=@info3,[info4]=@info4,[info5]=@info5,[info6]=@info6,[info7]=@info7,[info8]=@info8,[info9]=@info9,[info10]=@info10,[info11]=@info11,[info12]=@info12,[info13]=@info13,[info14]=@info14,[filename]=@filename,[infohash]=@infohash,[singlefile]=@singlefile,[foldername]=@foldername ";
        /// <summary>
        /// bt_seed表，编辑种子信息，种子不变
        /// </summary>
        public const string SQL_EDIT_SEED_WITHOUTSEED = " [topictitle]=@topictitle ";
        /// <summary>
        /// bt_seed_detail表，编辑种子信息，种子不变
        /// </summary>
        public const string SQL_EDIT_SEED_DETAIL_WITHOUTSEED = " [info1]=@info1,[info2]=@info2,[info3]=@info3,[info4]=@info4,[info5]=@info5,[info6]=@info6,[info7]=@info7,[info8]=@info8,[info9]=@info9,[info10]=@info10,[info11]=@info11,[info12]=@info12,[info13]=@info13,[info14]=@info14 ";



        public int UpdateSeedTrafficLog()
        {
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_seed_updatetrafficlog");
        }

        //////////////////////////////////////////////////////////////////////////
        //info


        /// <summary>
        /// 【存储过程】获取状态正常（status为2/3）SeedinfoTracker
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetSeedInfoTracker(string infohash)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@infohash",(DbType)SqlDbType.Char, 40, infohash)
                                    };
            //string sqlstring = "SELECT " + SQL_SEED_TRACKER_LIST + " FROM [bt_seed_tracker] WHERE [infohash] = @infohash AND [status] BETWEEN 2 AND 3";
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, "bt_seed_getseedinfo_tracker", parms);
        }
        /// <summary>
        /// 【临时解决】获取状态正常（status为2/3）Seedinfo，简单信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetSeedInfoShort(string infohash)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@infohash",(DbType)SqlDbType.Char, 40, infohash)
                                    };
            //string sqlstring = "SELECT TOP(1) " + SQL_SEED_LIST_WITHTABLE + " FROM [bt_seed],[bt_seed_tracker] WHERE [bt_seed].[seedid] = [bt_seed_tracker].[seedid] AND [bt_seed_tracker].[infohash] = @infohash AND [bt_seed_tracker].[status] BETWEEN 2 AND 3";
            //return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);

            return DbHelper.ExecuteReader(CommandType.StoredProcedure, "bt_seed_getseedinfoshort", parms);
        }
        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo，简单信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetSeedInfoShort(int seedid)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                    };
            string sqlstring = "SELECT TOP(1) " + SQL_SEED_LIST + " FROM [bt_seed] WHERE [seedid] = @seedid AND [status] BETWEEN 2 AND 3";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo，基本信息，用户贴内信息显示
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetSeedInfo(int seedid)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                    };
            string sqlstring = "SELECT TOP(1) " + SQL_SEED_LIST_WITHTABLE + "," + SQL_SEED_DETAIL_SHORT_WITHTABLE +
                " FROM [bt_seed] WITH(NOLOCK) LEFT JOIN [bt_seed_detail] WITH(NOLOCK) ON [bt_seed].[seedid] = [bt_seed_detail].[seedid] WHERE [bt_seed].[seedid] = @seedid AND [bt_seed].[status] BETWEEN 2 AND 3";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo完全版信息，用于发种信息检测
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetSeedInfoFull(string infohash_c)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@infohash_c",(DbType)SqlDbType.Char, 40, infohash_c)
                                    };
            string sqlstring = "SELECT TOP(1) " + SQL_SEED_LIST_WITHTABLE_FULL + "," + SQL_SEED_DETAIL_FULL_WITHTABLE +
                " FROM [bt_seed] LEFT JOIN [bt_seed_detail] ON [bt_seed].[seedid] = [bt_seed_detail].[seedid] WHERE [bt_seed].[infohash_c] = @infohash_c AND [bt_seed].[status] BETWEEN 2 AND 3 ORDER BY [bt_seed].[seedid]";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 获取任意状态的Seedinfo完全版信息，用于发种信息检测
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetSeedInfoFullAllStatus(string infohash_c)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@infohash_c",(DbType)SqlDbType.Char, 40, infohash_c)
                                    };
            string sqlstring = "SELECT TOP(1) " + SQL_SEED_LIST_WITHTABLE_FULL + "," + SQL_SEED_DETAIL_FULL_WITHTABLE +
                " FROM [bt_seed] LEFT JOIN [bt_seed_detail] ON [bt_seed].[seedid] = [bt_seed_detail].[seedid] WHERE [bt_seed].[infohash_c] = @infohash_c";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }


        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo完全版信息，用于编辑等
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetSeedInfoFull(int seedid)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                    };
            string sqlstring = "SELECT TOP(1) " + SQL_SEED_LIST_WITHTABLE_FULL + "," + SQL_SEED_DETAIL_FULL_WITHTABLE +
                " FROM [bt_seed] LEFT JOIN [bt_seed_detail] ON [bt_seed].[seedid] = [bt_seed_detail].[seedid] WHERE [bt_seed].[seedid] = @seedid AND [bt_seed].[status] BETWEEN 2 AND 3";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取Seedinfo完全版信息，用于编辑等
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetSeedInfoFullAllStatus(int seedid)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                    };
            string sqlstring = "SELECT TOP(1) " + SQL_SEED_LIST_WITHTABLE_FULL + "," + SQL_SEED_DETAIL_FULL_WITHTABLE +
                " FROM [bt_seed] LEFT JOIN [bt_seed_detail] ON [bt_seed].[seedid] = [bt_seed_detail].[seedid] WHERE [bt_seed].[seedid] = @seedid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }



        //////////////////////////////////////////////////////////////////////////
        //list
        

        /// <summary>
        /// 获得种子数
        /// </summary>
        /// <param name="seedstat">种子状态：1活种，2IPv4，3IPv6，4死种,0全部</param>
        /// <returns></returns>
        public int GetSeedInfoCount(int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, string notin)
        {
            DataTable dt = PTTools.GetIntTableFromString(keywords);
            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                                        //DbHelper.MakeInParam("@keywords",(DbType)SqlDbType.NVarChar, 500, keywords),
                                         DbHelper.MakeInParam("@notin",(DbType)SqlDbType.NVarChar, 500, notin),
                                  };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";

            //可能是空集，输出为0
            string sqlstring = "SELECT COUNT([seedid]) FROM [bt_seed] WITH(NOLOCK) WHERE " + GetBtSQLStringFromWhere(seedtype, userid, userstat, seedstat, keywords, keywordsmode, notin);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), 0);
        }

        /// <summary>
        /// 获取热门种子列表
        /// </summary>
        /// <param name="num"></param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public DataTable GetHotSeedinfoList(int num, int seedtype)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@num",(DbType)SqlDbType.Int, 4, num),
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                                  };

            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_seed_getnewhotseedlist", parms).Tables[0];
        }

        /// <summary>
        /// 获得种子列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSeedInfoList(int numperpage, int pageindex, int topseedcount, int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, int orderby, bool asc, string notin)
        {
            //首页默认排序方式，首页增加20个推荐位置
            if (orderby == -1) topseedcount += 20;
            
            //当查看的userstat为负数时，获取保种相关信息
            string leftjoin = "";
            string leftjoinlist = "";
            if (userstat < 0)
            {
                userstat = -userstat;
                leftjoin = "LEFT JOIN [bt_seed_detail] AS [d] WITH(NOLOCK) ON [d].[seedid] = [b].[seedid]";
                leftjoin += " LEFT JOIN [bt_traffic] AS [t] WITH(NOLOCK) ON [t].[seedid] = [b].[seedid] AND [t].[uid] = @userid";
                leftjoinlist = ",[d].[lastfinish],[t].[keeptime] AS [userkeeptime],[t].[upload] AS [useruptraffic]";
            }

            DataTable dt = PTTools.GetIntTableFromString(keywords);
            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
                                        DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                                        DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int, 4, pageindex),
                                        DbHelper.MakeInParam("@topseedcount",(DbType)SqlDbType.Int, 4, topseedcount),
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                                        //DbHelper.MakeInParam("@keywords",(DbType)SqlDbType.NVarChar, 500, keywords),
                                        DbHelper.MakeInParam("@notin",(DbType)SqlDbType.NVarChar, 500, notin),
                                  };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";

            string sqlstring = "";

            if (pageindex != 1)
            {
                //不是第一页
                sqlstring = "SELECT TOP (@numperpage) " + SQL_SEED_LIST_b + leftjoinlist + " FROM [bt_seed] AS [b] WITH(NOLOCK) " + leftjoin + " WHERE ";
                sqlstring += GetBtSQLStringFromWhere(seedtype, userid, userstat, seedstat, keywords, keywordsmode, notin, "[b].");

                if (orderby > 0)
                {
                    string section = "[filecount]";
                    string vartype = "int";
                    //分页算法：NOT IN 包含排序：1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间
                    switch (orderby)
                    {
                        case 1: section = "[filecount]"; vartype = "int"; break;
                        case 2: section = "[filesize]"; vartype = "decimal(32)"; break;
                        case 3: section = "[upload]"; vartype = "int"; break;
                        case 4: section = "[download]"; vartype = "int"; break;
                        case 5: section = "[finished]"; vartype = "int"; break;
                        case 6: section = "[traffic]"; vartype = "decimal(32)"; break;
                        case 7: section = "[live]"; vartype = "int"; break;
                        default: section = "[filecount]"; break;
                    }
                    sqlstring += "AND (([b]." + section + " = @maxvalue AND [b].[seedid] < @maxseedid) OR [b]." + section + (asc ? " >" : " <") + " @maxvalue) ";

                    //获取maxvalue
                    sqlstring = "DECLARE @maxvalue " + vartype + "; SET @maxvalue = (SELECT " + (asc ? "MAX " : "MIN ") + "(" + section + 
                        ") FROM (SELECT TOP (@numperpage * @pageindex - @numperpage - @topseedcount) " + section + " FROM [bt_seed] WITH(NOLOCK) WHERE " +
                     GetBtSQLStringFromWhere(seedtype, userid, userstat, seedstat, keywords, keywordsmode, notin) +  GetBTSQLStringOrderBy(orderby, asc) + 
                     ") AS Tmp1); " + sqlstring;

                    //获取maxseedid
                    sqlstring = "DECLARE @maxseedid int; SET @maxseedid = (SELECT TOP(1) [seedid] FROM (SELECT TOP(@numperpage * @pageindex - @numperpage - @topseedcount)" + section +
                        ",[seedid] FROM [bt_seed] WITH(NOLOCK) WHERE" + GetBtSQLStringFromWhere(seedtype, userid, userstat, seedstat, keywords, keywordsmode, notin) + GetBTSQLStringOrderBy(orderby, asc) +
                        ") AS Tmp0 ORDER BY Tmp0." + section + (asc ? " DESC" : " ASC") + ", Tmp0.[seedid] ASC); " + sqlstring;


                }
                else
                {
                    //分页算法 MAX 包含排序：-1默认排序，0发布时间
                    sqlstring += "AND [b].[seedid] " + (asc ? ">" : "<") + " @maxseedid ";

                    //获取maxseedid
                    sqlstring = "DECLARE @maxseedid int; SET @maxseedid = (SELECT " + (asc ? "MAX" : "MIN") + "([seedid]) FROM (SELECT TOP(@numperpage * @pageindex - @numperpage - @topseedcount) [seedid] FROM [bt_seed] WITH(NOLOCK) WHERE " +
                        GetBtSQLStringFromWhere(seedtype, userid, userstat, seedstat, keywords, keywordsmode, notin) + GetBTSQLStringOrderBy(orderby, asc) +
                        ") AS tblTmp); " + sqlstring;
                }
            }
            else
            {
                //第一页
                sqlstring = "SELECT TOP (@numperpage - @topseedcount) " + SQL_SEED_LIST_b + leftjoinlist + " FROM [bt_seed] AS [b] WITH(NOLOCK) " + leftjoin + " WHERE ";
                sqlstring += GetBtSQLStringFromWhere(seedtype, userid, userstat, seedstat, keywords, keywordsmode, notin, "[b].");
            }

            sqlstring += GetBTSQLStringOrderBy(orderby, asc, "[b].");

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 由条件获得查询种子所需要的SQL指令中的From...Where段
        /// </summary>
        /// <param name="seedtype">种子种类, 0为全部</param>
        /// <param name="userid">用户id，0为全部</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成，0时用户id失效</param>
        /// <param name="seedstat">种子状态：1活种，2IPv4，3IPv6，4死种，0为全部</param>
        /// <param name="keywords">查询关键词</param>
        /// <returns></returns>
        private string GetBtSQLStringFromWhere(int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, string notin)
        {
            return GetBtSQLStringFromWhere(seedtype, userid, userstat, seedstat, keywords, keywordsmode, notin, "");
        }
        /// <summary>
        /// 种子搜索
        /// </summary>
        /// <param name="searchmode"></param>
        /// <param name="seedstatus"></param>
        /// <param name="seedtype"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public DataTable GetSeedSearchList(int searchmode, int seedstatus, int seedtype, string keywords, int userid, int userstat)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                            DbHelper.MakeInParam("@keywords",(DbType)SqlDbType.NVarChar, 500, keywords),
                            DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                        };

            
            string conditions = "";
            //种子状态，0 全部，2 正常，3死种
            switch (seedstatus)
            {
                case 0: break;
                case 2: conditions += " AND s.[status] = 2"; break;
                case 3: conditions += " AND s.[status] = 3"; break;
                default: break;
            }
            //分类
            if (seedtype > 0 && seedtype < 13) conditions += " AND s.[type] = @seedtype";

            //用户状态限定条件
            if (userid > 0 && userstat > 0)
            {
                switch (userstat)
                {
                    case 1: conditions += " AND s.[seedid] IN (SELECT (pp.[seedid]) FROM [bt_peer] AS pp WITH(NOLOCK) WHERE pp.[uid] = @userid AND pp.[seed] = 'True') "; break;
                    case 2: conditions += " AND s.[seedid] IN (SELECT (pp.[seedid]) FROM [bt_peer] AS pp WITH(NOLOCK) WHERE pp.[uid] = @userid AND pp.[seed] = 'False') "; break;
                    case 3: conditions += " AND s.[uid] = @userid "; break;
                    case 4: conditions += " AND s.[seedid] IN (SELECT (ff.[seedid]) FROM [bt_finished] AS ff WITH(NOLOCK) WHERE ff.[uid] = @userid) "; break;
                    default: conditions += " AND s.[uid] = @userid "; break;
                }
            }
            
            string sqlstring1 = "SELECT TOP 5000 s.[seedid] FROM [bt_seed] AS s WITH(NOLOCK) WHERE CONTAINS([topictitle], @keywords)" + conditions;
            string sqlstring2 = "SELECT TOP 5000 f.[seedid] FROM [bt_seedfile] AS f WITH(NOLOCK INDEX=IX_bt_seedfile_id_with_seedid FORCESEEK) INNER JOIN [bt_seed] as s WITH(NOLOCK) ON s.seedid = f.seedid" + conditions + " WHERE CONTAINS([filename], @keywords) GROUP BY f.[seedid]";

            string sqlstring = "";
            if (searchmode == 3) sqlstring = "SELECT TOP 5000 [seedid] FROM (" + sqlstring1 + " UNION " + sqlstring2 + ") AS Tmptbl";
            else if (searchmode == 4) sqlstring = sqlstring2;
            else sqlstring = sqlstring1;

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 由条件获得查询种子所需要的SQL指令中的From...Where段
        /// </summary>
        /// <param name="seedtype">种子种类, 0为全部</param>
        /// <param name="userid">用户id，0为全部</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成，0时用户id失效</param>
        /// <param name="seedstat">种子状态：1活种，2IPv4，3IPv6，4死种，0为全部</param>
        /// <param name="keywords">查询关键词</param>
        /// <returns></returns>
        private string GetBtSQLStringFromWhere(int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, string notin, string prefix)
        {
            string sqlstring = "";

            //种子状态，0 全部，1 在线，2 IPv4，3 IPv6，4 离线，5 蓝种，6 在线蓝种，7 IPv6蓝种，8 死种
            switch (seedstat)
            {
                case 1: sqlstring = " " + prefix + "[status] = 2 AND " + prefix + "[upload] > 0 "; break;
                case 3: sqlstring = " " + prefix + "[status] = 2 AND " + prefix + "[ipv6] > 0 "; break;
                case 4: sqlstring = " " + prefix + "[status] = 2 AND " + prefix + "[upload] = 0 "; break;
                case 5: sqlstring = " " + prefix + "[status] = 2 AND " + prefix + "[downloadratio] = 0 "; break;
                case 6: sqlstring = " " + prefix + "[status] = 2 AND " + prefix + "[downloadratio] = 0 AND " + prefix + "[upload] > 0 "; break;
                case 7: sqlstring = " " + prefix + "[status] = 2 AND " + prefix + "[downloadratio] = 0 AND " + prefix + "[ipv6] > 0 "; break;
                case 8: sqlstring = " (" + prefix + "[status] = 2 OR " + prefix + "[status] = 3) "; break;
                case 9: sqlstring = " " + prefix + "[status] = 3 "; break;
                default: sqlstring = " " + prefix + "[status] = 2 "; break;
            }

            //按用户查看种子（1上传中，2下载中，3发布的，4完成的）
            if (keywords == "" && userid > 0 && userstat > 0) 
            {
                switch (userstat)
                {
                    case 1: sqlstring += " AND " + prefix + "[seedid] IN (SELECT ([seedid]) FROM [bt_peer] WITH(NOLOCK) WHERE [uid] = @userid AND [seed] = 'True') "; break;
                    case 2: sqlstring += " AND " + prefix + "[seedid] IN (SELECT ([seedid]) FROM [bt_peer] WITH(NOLOCK) WHERE [uid] = @userid AND [seed] = 'False') "; break;
                    case 3: sqlstring += " AND " + prefix + "[uid] = @userid "; break;
                    case 4: sqlstring += " AND " + prefix + "[seedid] IN (SELECT ([seedid]) FROM [bt_finished] WITH(NOLOCK) WHERE [uid] = @userid) "; break;
                    default: sqlstring += " AND " + prefix + "[uid] = @userid "; break;
                }
            }
            
            //分类
            if (seedtype > 0 && seedtype < 13) sqlstring += "AND " + prefix + "[type] = @seedtype ";
            
            //搜索关键词 修改此处必须同时修改关键词处理函数
            if (keywords != "")
            {
                sqlstring += " AND " + prefix + "[seedid] IN (SELECT [IntValue] FROM @TVP)";
            }
            //if (keywordsmode == 0 || keywordsmode == 1)
            //{
            //    //正常搜索，只包括种子标题的搜索结果
            //    if (keywords != "") sqlstring += " AND CONTAINS(" + prefix + "[topictitle], @keywords) ";
            //}
            //else if (keywordsmode == 2)
            //{
            //    //like 模糊查询 只包括种子标题
            //    if (keywords != "") sqlstring += " AND (" + prefix + "[topictitle] LIKE @keywords) ";
            //}
            //else if (keywordsmode == 3)
            //{
            //    //包括种子标题 和 种子内文件搜索结果
            //    if (keywords != "") sqlstring += " AND (CONTAINS(" + prefix + "[topictitle], @keywords) OR " + prefix + "[seedid] IN (SELECT [seedid] FROM [bt_seedfile] WITH(NOLOCK INDEX=IX_bt_seedfile_id_with_seedid FORCESEEK) WHERE CONTAINS([filename], @keywords) GROUP BY [seedid]))";
            //}
            //else if (keywordsmode == 4)
            //{
            //    //只搜索种子内文件结果
            //    if (keywords != "") sqlstring += " AND " + prefix + "[seedid] IN (SELECT [seedid] FROM [bt_seedfile] WITH(NOLOCK INDEX=IX_bt_seedfile_id_with_seedid FORCESEEK) WHERE CONTAINS([filename], @keywords) GROUP BY [seedid])";
            //}

            if (notin != "") sqlstring += " AND " + prefix + "[seedid] NOT IN (SELECT [seedid] FROM [bt_seed] WITH(NOLOCK) WHERE CONTAINS([topictitle], @notin)) ";

            return sqlstring;
        }
                /// <summary>
        /// 由条件获得查询种子所需要的SQL指令中的Order by段
        /// </summary>
        /// <param name="orderby">排序：0种子id，1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间</param>
        /// <returns></returns>
        private string GetBTSQLStringOrderBy(int orderby, bool asc)
        {
            return GetBTSQLStringOrderBy(orderby, asc, "[bt_seed].");
        }
        /// <summary>
        /// 由条件获得查询种子所需要的SQL指令中的Order by段
        /// </summary>
        /// <param name="orderby">排序：0种子id，1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间</param>
        /// <returns></returns>
        private string GetBTSQLStringOrderBy(int orderby, bool asc, string prefix)
        {
            switch (orderby)
            {
                case 0: return " ORDER BY " + prefix + "[seedid] " + (asc ? "ASC " : "DESC ");
                case 1: return " ORDER BY " + prefix + "[filecount] " + (asc ? "ASC " : "DESC ") + "," + prefix + "[seedid] DESC ";
                case 2: return " ORDER BY " + prefix + "[filesize] " + (asc ? "ASC " : "DESC ") + "," + prefix + "[seedid] DESC ";
                case 3: return " ORDER BY " + prefix + "[upload] " + (asc ? "ASC " : "DESC ") + "," + prefix + "[seedid] DESC ";
                case 4: return " ORDER BY " + prefix + "[download] " + (asc ? "ASC " : "DESC ") + "," + prefix + "[seedid] DESC ";
                case 5: return " ORDER BY " + prefix + "[finished] " + (asc ? "ASC " : "DESC ") + "," + prefix + "[seedid] DESC ";
                case 6: return " ORDER BY " + prefix + "[traffic] " + (asc ? "ASC " : "DESC ") + "," + prefix + "[seedid] DESC ";
                case 7: return " ORDER BY " + prefix + "[live] " + (asc ? "ASC " : "DESC ") + "," + prefix + "[seedid] DESC ";
                default: return " ORDER BY " + prefix + "[seedid] " + (asc ? "ASC " : "DESC ");
            }
        }
        /// <summary>
        /// 获得相应分类的置顶种子表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTopSeedInfoList(int type)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, type),
                                  };
            string sqlstring = "SELECT " + SQL_SEED_LIST + " FROM [bt_seed] WHERE [seedid] IN ( SELECT [seedid] FROM [bt_seed_top] WITH(NOLOCK) WHERE [type] = @type) ORDER BY [topseed] DESC";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 按seedid列表获取种子信息
        /// </summary>
        /// <param name="seedidlist"></param>
        /// <returns></returns>
        public DataTable GetSeedInfoList(string seedidlist)
        {
            DataTable dt = PTTools.GetIntTableFromString(seedidlist);
            //if (dt.Rows.Count > 0) 
            seedidlist = "SELECT [IntValue] FROM @TVP";

            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
                                   };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";

            string sqlstring = "SELECT " + SQL_SEED_LIST_b + " FROM @TVP AS T LEFT JOIN [bt_seed] AS b ON [b].[seedid] = [t].[IntValue]";

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 获取指定seedidlist中，与用户状态相关的种子id列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成</param>
        /// <param name="seedidlist">种子id列表字符串</param>
        /// <returns></returns>
        public DataTable GetSeedIdList(int userid, int userstat, string seedidlist)
        {
            DataTable dt = PTTools.GetIntTableFromString(seedidlist);
            //if (dt.Rows.Count > 0) 
                seedidlist = "SELECT [IntValue] FROM @TVP";

            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid)
                                   };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";

            string sqlstring = "";
            switch (userstat)
            {
                //case 1: sqlstring = "SELECT [seedid] FROM [bt_peer] WITH(NOLOCK) WHERE [uid] = @userid AND [seed] = 'True' AND [seedid] IN (" + seedidlist + ")"; break;
                //case 2: sqlstring = "SELECT [seedid] FROM [bt_peer] WITH(NOLOCK) WHERE [uid] = @userid AND [seed] = 'False' AND [seedid] IN (" + seedidlist + ")"; break;
                //case 3: sqlstring = "SELECT [seedid] FROM [bt_seed] WITH(NOLOCK) WHERE [uid] = @userid AND [seedid] IN (" + seedidlist + ")"; break;
                //case 4: sqlstring = "SELECT [seedid] FROM [bt_finished] WITH(NOLOCK) WHERE [uid] = @userid AND [seedid] IN (" + seedidlist + ")"; break;
                //default: sqlstring = "SELECT [seedid] FROM [bt_seed] WITH(NOLOCK) WHERE [uid] = @userid AND [seedid] IN (" + seedidlist + ")"; break;
                case 1: sqlstring = "SELECT T.[IntValue] AS [seedid] FROM @TVP AS T WHERE EXISTS(SELECT 1 FROM [bt_peer] AS P WITH(NOLOCK) WHERE P.[seedid] = T.[IntValue] AND P.[uid] = @userid AND P.[seed] = 'True')"; break;
                case 2: sqlstring = "SELECT T.[IntValue] AS [seedid] FROM @TVP AS T WHERE EXISTS(SELECT 1 FROM [bt_peer] AS P WITH(NOLOCK) WHERE P.[seedid] = T.[IntValue] AND P.[uid] = @userid AND P.[seed] = 'False')"; break;
                case 3: sqlstring = "SELECT T.[IntValue] AS [seedid] FROM @TVP AS T WHERE EXISTS(SELECT 1 FROM [bt_seed] AS S WITH(NOLOCK) WHERE S.[uid] = @userid AND S.[seedid] = T.[IntValue])"; break;
                case 4: sqlstring = "SELECT T.[IntValue] AS [seedid] FROM @TVP AS T WHERE EXISTS(SELECT 1 FROM [bt_finished] AS F WITH(NOLOCK) WHERE F.[seedid] = T.[IntValue] AND F.[uid] = @userid)"; break;
                default: sqlstring = "SELECT T.[IntValue] AS [seedid] FROM @TVP AS T WHERE EXISTS(SELECT 1 FROM [bt_seed] AS S WITH(NOLOCK) WHERE S.[uid] = @userid AND S.[seedid] = T.[IntValue])"; break;
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 重新获得相应分类的置顶种子ID数组
        /// </summary>
        /// <returns></returns>
        public DataTable GetTopSeedIdListNew(int seedtype)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, seedtype),  
                                  };

            string sqlstring = "";
            if(seedtype > 0 && seedtype < 13) sqlstring = "SELECT TOP (10) [seedid] FROM [bt_seed] WHERE [status] = 2 AND [type] = @type ORDER BY [topseed] DESC";
            else
            {
                sqlstring = "SELECT [seedid] FROM " +
                    "( SELECT TOP (2) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 1 ORDER BY [topseed] DESC ) A UNION ALL " +
                    "( SELECT TOP (2) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 2 ORDER BY [topseed] DESC ) B UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 3 ORDER BY [topseed] DESC ) C UNION ALL " +
                    "( SELECT TOP (2) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 4 ORDER BY [topseed] DESC ) D UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 5 ORDER BY [topseed] DESC ) E UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 6 ORDER BY [topseed] DESC ) F UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 7 ORDER BY [topseed] DESC ) G UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 8 ORDER BY [topseed] DESC ) H UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 9 ORDER BY [topseed] DESC ) I UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 10 ORDER BY [topseed] DESC ) J UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 11 ORDER BY [topseed] DESC ) K UNION ALL " +
                    "( SELECT TOP (1) [seedid],[topseed] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = 12 ORDER BY [topseed] DESC ) L " +
                    " ORDER BY [topseed] desc";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 获取指定类别种子id列表
        /// </summary>
        /// <param name="seedtype"></param>
        /// <param name="numperpage"></param>
        /// <returns></returns>
        public DataTable GetSeedIdListIntTablebyType(int seedtype, int numperpage)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                            DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                        };

            string sqlstring = string.Format("SELECT TOP (@numperpage) [seedid] AS IntValue FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 ORDER BY [seedid] DESC");

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 获取目前总优惠操作点数
        /// </summary>
        /// <param name="seedtype"></param>
        /// <param name="numperpage"></param>
        /// <returns></returns>
        public int GetSeedOpValueSUM(int seedtype, int numperpage)
        {
                        DbParameter[] parms = {
                            DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                            DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                        };

                        string sqlstring = string.Format("SELECT SUM([operatvalue]) FROM [bt_seedop] AS OP WITH(NOLOCK) WHERE OP.[seedid] IN (SELECT TOP (@numperpage) [seedid] FROM [bt_seed] WITH(NOLOCK) WHERE [status] = 2 AND [type] = @seedtype ORDER BY [seedid] DESC)");

            return TypeConverter.ObjectToInt(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 获得种子列表RSS专用
        /// </summary>
        /// <returns></returns>
        public DataTable GetSeedRssList(int numperpage, int pageindex, int seedtype, int withinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                                        DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int, 4, pageindex),
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                                  };
            string sqlstring = "";
            string sqlwhere = seedtype > 0 ? " WHERE [status] = 2 AND [type] = @seedtype " : " WHERE [status] = 2 ";
            string sqlouterapply = " OUTER APPLY( SELECT TOP 1 [firstupdate] FROM [bt_traffic] WITH(NOLOCK) WHERE [bt_traffic].[seedid] = [bt_seed].[seedid] ORDER BY [id] DESC) AS FSD ";

            if (pageindex == 1)
            {
                if(withinfo == 1)
                    sqlstring += string.Format("SELECT TOP (@numperpage) [bt_seed].[seedid],[topictitle],[type],[accrss],[info3],[FSD].[firstupdate] FROM [bt_seed] LEFT JOIN [bt_seed_detail] ON [bt_seed].[seedid] = [bt_seed_detail].[seedid] " + sqlouterapply + sqlwhere + " ORDER BY [seedid] desc");
                else if(withinfo == 0)
                    sqlstring += string.Format("SELECT TOP (@numperpage) [seedid],[topictitle],[type] FROM [bt_seed] " + sqlwhere + " ORDER BY [seedid] desc");
                else
                    sqlstring += string.Format("SELECT TOP (@numperpage) [seedid],[topictitle],[type] FROM [bt_seed] " + sqlwhere + " ORDER BY [seedid] desc");
            }
            else
            {
                if (withinfo == 1)
                    sqlstring += string.Format("SELECT TOP (@numperpage) [bt_seed].[seedid],[topictitle],[type],[accrss],[info3],[FSD].[firstupdate] FROM [bt_seed] LEFT JOIN [bt_seed_detail] ON [bt_seed].[seedid] = [bt_seed_detail].[seedid] " + sqlouterapply + sqlwhere);
                else if (withinfo == 0)
                    sqlstring += string.Format("SELECT TOP (@numperpage) [seedid],[topictitle],[type] FROM [bt_seed] " + sqlwhere);
                else
                    sqlstring += string.Format("SELECT TOP (@numperpage) [seedid],[topictitle],[type] FROM [bt_seed] " + sqlwhere);
                if (sqlwhere != "") sqlstring += " AND ";
                else sqlstring += " WHERE ";
                sqlstring += " [bt_seed].[seedid] < (SELECT MIN([seedid]) FROM (SELECT TOP (@numperpage * @pageindex - @numperpage) [seedid] FROM [bt_seed] " + sqlwhere;
                sqlstring += " ORDER BY [seedid] desc) AS tblTmp) ORDER BY [bt_seed].[seedid] desc";

            }
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获得种子总大小
        /// </summary>
        /// <param name="seedtype">种子种类</param>
        /// <param name="userid">用户id</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成</param>
        /// <param name="seedstat">种子状态：1活种，2IPv4，3IPv6，4死种，0全部</param>
        /// <param name="orderby">排序：1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间</param>
        /// <param name="keywords">搜索关键词</param>
        /// <returns></returns>
        public decimal GetSeedSumSize(int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, string notin)
        {
            DataTable dt = PTTools.GetIntTableFromString(keywords);
            DbParameter[] parms = {
                                      new SqlParameter("@TVP", dt),
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                                        //DbHelper.MakeInParam("@keywords",(DbType)SqlDbType.NVarChar, 500, keywords),
                                         DbHelper.MakeInParam("@notin",(DbType)SqlDbType.NVarChar, 500, notin),
                                  };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";
            string sqlstring = "SELECT ISNULL(SUM([filesize]),0) FROM [bt_seed] WITH(NOLOCK) WHERE " + GetBtSQLStringFromWhere(seedtype, userid, userstat, seedstat, keywords, keywordsmode, notin);
            return decimal.Parse(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString());
        }


        
        //////////////////////////////////////////////////////////////////////////
        //list schedule task


        /// <summary>
        /// 获得指定时间内，存活时间不足的种子列表（不包括有正在做种的种子，不会误删）
        /// </summary>
        /// <returns></returns>
        public DataTable GetSeedIdListNoSeed(DateTime postdatetime, int live)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@postdatetime",(DbType)SqlDbType.DateTime, 4, postdatetime),
                                        DbHelper.MakeInParam("@live",(DbType)SqlDbType.Int, 4, live),
                                  };
            string sqlstring = "SELECT [seedid] FROM [bt_seed] WHERE [status] = 2 AND [upload] = 0 AND [postdatetime] < @postdatetime AND [live] < @live AND [status] BETWEEN 2 AND 3";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获得指定时间之后一直无种子的种子列表（不包括有正在做种的种子，不会误删）
        /// </summary>
        /// <returns></returns>
        public DataTable GetSeedIdListNoSeed(DateTime lastlive)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@lastlive",(DbType)SqlDbType.DateTime, 4, lastlive),
                                  };
            string sqlstring = "SELECT [seedid] FROM [bt_seed] WHERE [status] = 2 AND [upload] = 0 AND [lastlive] < @lastlive AND [status] BETWEEN 2 AND 3";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获取指定downloadratioexpiredate日期前的优惠种子列表，用于自动调整流量系数
        /// </summary>
        /// <param name="timelimit"></param>
        /// <returns></returns>
        public DataTable GetSeedIdListDownloadRatioExpire(DateTime downloadratioexpiredate)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@downloadratioexpiredate",(DbType)SqlDbType.DateTime, 8, downloadratioexpiredate),
                                  };
            string sqlstring;
            sqlstring = string.Format("SELECT [seedid] FROM [bt_seed] WHERE [downloadratioexpiredate] < @downloadratioexpiredate AND [status] BETWEEN 2 AND 3");
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获取指定ratioexpiredate日期前的优惠种子列表，用于自动调整流量系数
        /// </summary>
        /// <param name="timelimit"></param>
        /// <returns></returns>
        public DataTable GetSeedIdListUploadRatioExpire(DateTime uploadratioexpiredate)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uploadratioexpiredate",(DbType)SqlDbType.DateTime, 8, uploadratioexpiredate),
                                  };
            string sqlstring;
            sqlstring = string.Format("SELECT [seedid] FROM [bt_seed] WHERE [uploadratioexpiredate] < @uploadratioexpiredate AND [status] BETWEEN 2 AND 3");
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }





        //////////////////////////////////////////////////////////////////////////
        //get parms


        /// <summary>
        /// 获取seedinfo中所有包含的项目列表
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        private DbParameter[] GetSeedParameterFull(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedinfo.SeedId),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Int, 4, seedinfo.Download),
                                        DbHelper.MakeInParam("@downloadratio",(DbType)SqlDbType.Float, 8, seedinfo.DownloadRatio),
                                        DbHelper.MakeInParam("@downloadratioexpiredate",(DbType)SqlDbType.DateTime, 8, seedinfo.DownloadRatioExpireDate),
                                        DbHelper.MakeInParam("@filecount",(DbType)SqlDbType.Int, 4, seedinfo.FileCount),
                                        DbHelper.MakeInParam("@filesize",(DbType)SqlDbType.Decimal, 16, seedinfo.FileSize),
                                        DbHelper.MakeInParam("@finished",(DbType)SqlDbType.Int, 4, seedinfo.Finished),
                                        DbHelper.MakeInParam("@infohash_c",(DbType)SqlDbType.Char, 40, seedinfo.InfoHash_c),
                                        DbHelper.MakeInParam("@ipv6",(DbType)SqlDbType.Int, 4, seedinfo.IPv6),
                                        DbHelper.MakeInParam("@lastlive",(DbType)SqlDbType.DateTime, 8, seedinfo.LastLive),
                                        DbHelper.MakeInParam("@live",(DbType)SqlDbType.Int, 4, seedinfo.Live),
                                        DbHelper.MakeInParam("@postdatetime",(DbType)SqlDbType.DateTime, 8, seedinfo.PostDateTime),
                                        DbHelper.MakeInParam("@replies",(DbType)SqlDbType.Int, 4, seedinfo.Replies),
                                        DbHelper.MakeInParam("@status",(DbType)SqlDbType.Int, 4, seedinfo.Status),
                                        DbHelper.MakeInParam("@topicid",(DbType)SqlDbType.Int, 4, seedinfo.TopicId),
                                        DbHelper.MakeInParam("@topictitle",(DbType)SqlDbType.NChar, 255, seedinfo.TopicTitle),
                                        DbHelper.MakeInParam("@topseed",(DbType)SqlDbType.Int, 4, seedinfo.TopSeed),
                                        DbHelper.MakeInParam("@traffic",(DbType)SqlDbType.Decimal, 16, seedinfo.Traffic),
                                        DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, seedinfo.Type),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, seedinfo.Uid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Int, 4, seedinfo.Upload),
                                        DbHelper.MakeInParam("@uploadratio",(DbType)SqlDbType.Float, 8, seedinfo.UploadRatio),
                                        DbHelper.MakeInParam("@uploadratioexpiredate",(DbType)SqlDbType.DateTime, 8, seedinfo.UploadRatioExpireDate),
                                        DbHelper.MakeInParam("@uptraffic",(DbType)SqlDbType.Decimal, 16, seedinfo.UpTraffic),
                                        DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar, 20, seedinfo.UserName),
                                        DbHelper.MakeInParam("@views",(DbType)SqlDbType.Int, 4, seedinfo.Views),

                                        DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NChar, 255, seedinfo.FolderName),
                                        DbHelper.MakeInParam("@infohash",(DbType)SqlDbType.Char, 40, seedinfo.InfoHash),
                                        DbHelper.MakeInParam("@lastseederid",(DbType)SqlDbType.Int, 4, seedinfo.LastSeederId),
                                        DbHelper.MakeInParam("@lastseedername",(DbType)SqlDbType.NChar, 20, seedinfo.LastSeederName),
                                        DbHelper.MakeInParam("@path",(DbType)SqlDbType.NChar, 50, seedinfo.Path),
                                        
                                        DbHelper.MakeInParam("@award",(DbType)SqlDbType.Decimal, 16, seedinfo.Award),
                                        DbHelper.MakeInParam("@createdby",(DbType)SqlDbType.NChar, 20, seedinfo.CreatedBy),
                                        DbHelper.MakeInParam("@createddate",(DbType)SqlDbType.DateTime, 8, seedinfo.CreatedDate),
                                        DbHelper.MakeInParam("@foldername",(DbType)SqlDbType.NChar, 255, seedinfo.FolderName),
                                        DbHelper.MakeInParam("@info1",(DbType)SqlDbType.NChar, 200, seedinfo.Info1),
                                        DbHelper.MakeInParam("@info2",(DbType)SqlDbType.NChar, 200, seedinfo.Info2),
                                        DbHelper.MakeInParam("@info3",(DbType)SqlDbType.NChar, 200, seedinfo.Info3),
                                        DbHelper.MakeInParam("@info4",(DbType)SqlDbType.NChar, 200, seedinfo.Info4),
                                        DbHelper.MakeInParam("@info5",(DbType)SqlDbType.NChar, 200, seedinfo.Info5),
                                        DbHelper.MakeInParam("@info6",(DbType)SqlDbType.NChar, 200, seedinfo.Info6),
                                        DbHelper.MakeInParam("@info7",(DbType)SqlDbType.NChar, 200, seedinfo.Info7),
                                        DbHelper.MakeInParam("@info8",(DbType)SqlDbType.NChar, 200, seedinfo.Info8),
                                        DbHelper.MakeInParam("@info9",(DbType)SqlDbType.NChar, 200, seedinfo.Info9),
                                        DbHelper.MakeInParam("@info10",(DbType)SqlDbType.NChar, 200, seedinfo.Info10),
                                        DbHelper.MakeInParam("@info11",(DbType)SqlDbType.NChar, 200, seedinfo.Info11),
                                        DbHelper.MakeInParam("@info12",(DbType)SqlDbType.NChar, 200, seedinfo.Info12),
                                        DbHelper.MakeInParam("@info13",(DbType)SqlDbType.NChar, 200, seedinfo.Info13),
                                        DbHelper.MakeInParam("@info14",(DbType)SqlDbType.NChar, 200, seedinfo.Info14),
                                        DbHelper.MakeInParam("@singlefile",(DbType)SqlDbType.Bit, 1, seedinfo.SingleFile),
                                  };
            return parms;
        }

        /// <summary>
        /// 获取seedinfo中所有seedinfoshort包含的项目列表，bt_seed表中列名，全26项
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        private DbParameter[] GetSeedParameterShort(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedinfo.SeedId),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Int, 4, seedinfo.Download),
                                        DbHelper.MakeInParam("@downloadratio",(DbType)SqlDbType.Float, 8, seedinfo.DownloadRatio),
                                        DbHelper.MakeInParam("@downloadratioexpiredate",(DbType)SqlDbType.DateTime, 8, seedinfo.DownloadRatioExpireDate),
                                        DbHelper.MakeInParam("@filecount",(DbType)SqlDbType.Int, 4, seedinfo.FileCount),
                                        DbHelper.MakeInParam("@filesize",(DbType)SqlDbType.Decimal, 16, seedinfo.FileSize),
                                        DbHelper.MakeInParam("@finished",(DbType)SqlDbType.Int, 4, seedinfo.Finished),
                                        DbHelper.MakeInParam("@infohash_c",(DbType)SqlDbType.NChar, 40, seedinfo.InfoHash_c),
                                        DbHelper.MakeInParam("@ipv6",(DbType)SqlDbType.Int, 4, seedinfo.IPv6),
                                        DbHelper.MakeInParam("@lastlive",(DbType)SqlDbType.DateTime, 8, seedinfo.LastLive),
                                        DbHelper.MakeInParam("@live",(DbType)SqlDbType.Int, 4, seedinfo.Live),
                                        DbHelper.MakeInParam("@postdatetime",(DbType)SqlDbType.DateTime, 8, seedinfo.PostDateTime),
                                        DbHelper.MakeInParam("@replies",(DbType)SqlDbType.Int, 4, seedinfo.Replies),
                                        DbHelper.MakeInParam("@status",(DbType)SqlDbType.Int, 4, seedinfo.Status),
                                        DbHelper.MakeInParam("@topicid",(DbType)SqlDbType.Int, 4, seedinfo.TopicId),
                                        DbHelper.MakeInParam("@topictitle",(DbType)SqlDbType.NChar, 255, seedinfo.TopicTitle),
                                        DbHelper.MakeInParam("@topseed",(DbType)SqlDbType.Int, 4, seedinfo.TopSeed),
                                        DbHelper.MakeInParam("@traffic",(DbType)SqlDbType.Decimal, 16, seedinfo.Traffic),
                                        DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, seedinfo.Type),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, seedinfo.Uid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Int, 4, seedinfo.Upload),
                                        DbHelper.MakeInParam("@uploadratio",(DbType)SqlDbType.Float, 8, seedinfo.UploadRatio),
                                        DbHelper.MakeInParam("@uploadratioexpiredate",(DbType)SqlDbType.DateTime, 8, seedinfo.UploadRatioExpireDate),
                                        DbHelper.MakeInParam("@uptraffic",(DbType)SqlDbType.Decimal, 16, seedinfo.UpTraffic),
                                        DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar, 20, seedinfo.UserName),
                                        DbHelper.MakeInParam("@views",(DbType)SqlDbType.Int, 4, seedinfo.Views),
                                  };
            return parms;
        }
        /// <summary>
        /// 获取seedinfo中所有seedinfoshort不包含的项目列表(除seedid)，bt_seed_detail表中列名，全25项
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        private DbParameter[] GetSeedParameterDetail(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedinfo.SeedId),

                                        DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NChar, 255, seedinfo.FolderName),
                                        DbHelper.MakeInParam("@infohash",(DbType)SqlDbType.Char, 40, seedinfo.InfoHash),
                                        DbHelper.MakeInParam("@lastseederid",(DbType)SqlDbType.Int, 4, seedinfo.LastSeederId),
                                        DbHelper.MakeInParam("@lastseedername",(DbType)SqlDbType.NChar, 20, seedinfo.LastSeederName),
                                        DbHelper.MakeInParam("@path",(DbType)SqlDbType.NChar, 255, seedinfo.Path),
                                        
                                        DbHelper.MakeInParam("@award",(DbType)SqlDbType.Decimal, 16, seedinfo.Award),
                                        DbHelper.MakeInParam("@createdby",(DbType)SqlDbType.NChar, 20, seedinfo.CreatedBy),
                                        DbHelper.MakeInParam("@createddate",(DbType)SqlDbType.DateTime, 8, seedinfo.CreatedDate),
                                        DbHelper.MakeInParam("@foldername",(DbType)SqlDbType.NChar, 255, seedinfo.FolderName),
                                        DbHelper.MakeInParam("@info1",(DbType)SqlDbType.NChar, 200, seedinfo.Info1),
                                        DbHelper.MakeInParam("@info2",(DbType)SqlDbType.NChar, 200, seedinfo.Info2),
                                        DbHelper.MakeInParam("@info3",(DbType)SqlDbType.NChar, 200, seedinfo.Info3),
                                        DbHelper.MakeInParam("@info4",(DbType)SqlDbType.NChar, 200, seedinfo.Info4),
                                        DbHelper.MakeInParam("@info5",(DbType)SqlDbType.NChar, 200, seedinfo.Info5),
                                        DbHelper.MakeInParam("@info6",(DbType)SqlDbType.NChar, 200, seedinfo.Info6),
                                        DbHelper.MakeInParam("@info7",(DbType)SqlDbType.NChar, 200, seedinfo.Info7),
                                        DbHelper.MakeInParam("@info8",(DbType)SqlDbType.NChar, 200, seedinfo.Info8),
                                        DbHelper.MakeInParam("@info9",(DbType)SqlDbType.NChar, 200, seedinfo.Info9),
                                        DbHelper.MakeInParam("@info10",(DbType)SqlDbType.NChar, 200, seedinfo.Info10),
                                        DbHelper.MakeInParam("@info11",(DbType)SqlDbType.NChar, 200, seedinfo.Info11),
                                        DbHelper.MakeInParam("@info12",(DbType)SqlDbType.NChar, 200, seedinfo.Info12),
                                        DbHelper.MakeInParam("@info13",(DbType)SqlDbType.NChar, 200, seedinfo.Info13),
                                        DbHelper.MakeInParam("@info14",(DbType)SqlDbType.NChar, 200, seedinfo.Info14),
                                        DbHelper.MakeInParam("@singlefile",(DbType)SqlDbType.Bit, 1, seedinfo.SingleFile),
                                  };
            return parms;
        }
        /// <summary>
        /// 获取seedinfo中所有seedinfotracker包含的项目列表，bt_seed_tracker表中列名，全7项
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        private DbParameter[] GetSeedParameterTracker(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedinfo.SeedId),
                                        DbHelper.MakeInParam("@downloadratio",(DbType)SqlDbType.Float, 8, seedinfo.DownloadRatio),
                                        DbHelper.MakeInParam("@filesize",(DbType)SqlDbType.Decimal, 16, seedinfo.FileSize),
                                        DbHelper.MakeInParam("@status",(DbType)SqlDbType.Int, 4, seedinfo.Status),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, seedinfo.Uid),
                                        DbHelper.MakeInParam("@uploadratio",(DbType)SqlDbType.Float, 8, seedinfo.UploadRatio),
                                        DbHelper.MakeInParam("@infohash",(DbType)SqlDbType.Char, 40, seedinfo.InfoHash),
                                  };
            return parms;
        }




        //////////////////////////////////////////////////////////////////////////
        //insert


        /// <summary>
        /// 【存储过程】插入种子，并返回种子id
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public int InsertSeed(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = GetSeedParameterFull(seedinfo);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_insertseed", parms), -1);
        }

        /// <summary>
        /// 插入种子，并返回种子id
        /// 表格共26项，插入25项，不包括seedid
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public int InsertSeedShort(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = GetSeedParameterShort(seedinfo);
            string sqlstring = "INSERT INTO [bt_seed] ( " + SQL_INSERT_SEED_LIST + " ) VALUES( " + SQL_INSERT_SEED_VALUE + " ) SELECT SCOPE_IDENTITY()";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }
        /// <summary>
        /// 插入seed_detail表，种子附加项目
        /// 表格共25项，插入1+5+19=25项，全部内容
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public int InsertSeedDetail(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = GetSeedParameterDetail(seedinfo);
            string sqlstring = "INSERT INTO [bt_seed_detail] ( " + SQL_INSERT_SEED_DETAIL_LIST + " ) VALUES( " + SQL_INSERT_SEED_DETAIL_VALUE + " )";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }
        /// <summary>
        /// 插入seed_tracker表，种子附加项目
        /// 表格共7项，插入7项，全部内容
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public int InsertSeedTracker(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = GetSeedParameterTracker(seedinfo);
            string sqlstring = "INSERT INTO [bt_seed_tracker] ( " + SQL_INSERT_SEED_TRACKER_LIST + " ) VALUES( " + SQL_INSERT_SEED_TRACKER_VALUE + " )";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }




        //////////////////////////////////////////////////////////////////////////
        //update

        /// <summary>
        /// 设置置顶种子
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="top">true置顶，false取消置顶</param>
        /// <returns></returns>
        public int UpdateSeedTop(int seedid, bool top)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@operatetime",(DbType)SqlDbType.Int, 4, top ? (int)((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds) : 0)
                                  };
            string sqlstring = "UPDATE [bt_seed] set [topseed] = @operatetime WHERE [seedid] = @seedid ";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新种子状态bt_seed.status，0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns>1.成功，其他失败</returns>
        public int UpdateSeedStatus(int seedid, int status)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@status",(DbType)SqlDbType.Int, 4, status),
                                  };
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_updateseedstatus", parms);
        }

        /// <summary>
        /// 根据RSS类型更新bt_seed表相应字段accrss、keeprss和pubrss
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <param name="rssstatus"></param>
        /// <returns></returns>
        public int UpdateSeedbyRssType(int seedid, int rsstype, int rssstatus)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@rssstatus",(DbType)SqlDbType.Int, 4, rssstatus),
                                  };
            string sqlstring = "";
            if(rsstype == 1)
                sqlstring = "UPDATE [bt_seed] set [accrss] = @rssstatus WHERE [seedid] = @seedid ";
            else if (rsstype == 1)
                sqlstring = "UPDATE [bt_seed] set [keeprss] = @rssstatus WHERE [seedid] = @seedid ";
            else if (rsstype == 1)
                sqlstring = "UPDATE [bt_seed] set [pubrss] = @rssstatus WHERE [seedid] = @seedid ";

            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }


        ///// <summary>
        ///// 更新种子状态bt_seed.status，0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //public int UpdateSeedStatus(int seedid, int status)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
        //                                DbHelper.MakeInParam("@status",(DbType)SqlDbType.Int, 4, status),
        //                          };
        //    string sqlstring = "UPDATE [bt_seed] SET [status] = @status WHERE [seedid] = @seedid ";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        ///// <summary>
        ///// 更新种子状态bt_seed_tracker.status，0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //public int UpdateSeedStatusTracker(int seedid, int status)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
        //                                DbHelper.MakeInParam("@status",(DbType)SqlDbType.Int, 4, status),
        //                          };
        //    string sqlstring = "UPDATE [bt_seed_tracker] SET [status] = @status WHERE [seedid] = @seedid ";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        /// <summary>
        /// 更新bt_seed表，指定种子的下载系数和过期时间
        /// </summary>
        /// <returns></returns>
        public int UpdateSeedRatio(int seedid, float downloadratio, DateTime downloadratioexpiredate, float uploadratio, DateTime uploadratioexpiredate)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@downloadratio",(DbType)SqlDbType.Float, 8, downloadratio),
                                        DbHelper.MakeInParam("@uploadratio",(DbType)SqlDbType.Float, 8, uploadratio),
                                        DbHelper.MakeInParam("@downloadratioexpiredate",(DbType)SqlDbType.DateTime, 8, downloadratioexpiredate),
                                        DbHelper.MakeInParam("@uploadratioexpiredate",(DbType)SqlDbType.DateTime, 8, uploadratioexpiredate),
                                  };
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_updateseedratio", parms);
        }
        ///// <summary>
        ///// 更新bt_seed表，指定种子的下载系数和过期时间
        ///// </summary>
        ///// <returns></returns>
        //public int UpdateSeedRatio(int seedid, double downloadratio, DateTime downloadratioexpiredate, double uploadratio, DateTime uploadratioexpiredate)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
        //                                DbHelper.MakeInParam("@downloadratio",(DbType)SqlDbType.Float, 8, downloadratio),
        //                                DbHelper.MakeInParam("@uploadratio",(DbType)SqlDbType.Float, 8, uploadratio),
        //                                DbHelper.MakeInParam("@downloadratioexpiredate",(DbType)SqlDbType.DateTime, 8, downloadratioexpiredate),
        //                                DbHelper.MakeInParam("@uploadratioexpiredate",(DbType)SqlDbType.DateTime, 8, uploadratioexpiredate),
        //                          };
        //    string sqlstring = "UPDATE [bt_seed] SET [downloadratio] = @downloadratio,[uploadratio] = @uploadratio,[downloadratioexpiredate] = @downloadratioexpiredate,[uploadratioexpiredate] = @uploadratioexpiredate WHERE [seedid] = @seedid";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        ///// <summary>
        ///// 更新bt_seed表，指定种子的下载系数和过期时间
        ///// </summary>
        ///// <returns></returns>
        //public int UpdateSeedRatioTracker(int seedid, double downloadratio, double uploadratio)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
        //                                DbHelper.MakeInParam("@downloadratio",(DbType)SqlDbType.Float, 8, downloadratio),
        //                                DbHelper.MakeInParam("@uploadratio",(DbType)SqlDbType.Float, 8, uploadratio),
        //                          };
        //    string sqlstring = "UPDATE [bt_seed_tracker] SET [downloadratio] = @downloadratio,[uploadratio] = @uploadratio WHERE [seedid] = @seedid";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        /// <summary>
        /// 【存储过程】更新种子，全部表，包含更新种子信息
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public int UpdateSeedEditWithSeed(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = GetSeedParameterFull(seedinfo);
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_updateseededitwithseed", parms);
        }
        /// <summary>
        /// 【存储过程】更新种子，全部表，不包含更新种子信息
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public int UpdateSeedEditWithOutSeed(PTSeedinfo seedinfo)
        {
            DbParameter[] parms = GetSeedParameterFull(seedinfo);
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_updateseededitwithoutseed", parms);
        }
        ///// <summary>
        ///// 更新种子状态，bt_seed表，包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public int UpdateSeedEditShortWithSeed(PTSeedinfo seedinfo)
        //{
        //    DbParameter[] parms = GetSeedParameterShort(seedinfo);
        //    string sqlstring = " UPDATE [bt_seed] SET " + SQL_EDIT_SEED_WITHSEED+ " WHERE [seedid] = @seedid";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        ///// <summary>
        ///// 更新种子状态，bt_seed表，不包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public int UpdateSeedEditShortWithOutSeed(PTSeedinfo seedinfo)
        //{
        //    DbParameter[] parms = GetSeedParameterShort(seedinfo);
        //    string sqlstring = " UPDATE [bt_seed] SET " + SQL_EDIT_SEED_WITHOUTSEED + " WHERE [seedid] = @seedid";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        ///// <summary>
        ///// 更新种子状态，bt_seed_detail表，包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public int UpdateSeedEditDetailWithSeed(PTSeedinfo seedinfo)
        //{
        //    DbParameter[] parms = GetSeedParameterDetail(seedinfo);
        //    string sqlstring = " UPDATE [bt_seed_detail] SET " + SQL_EDIT_SEED_DETAIL_WITHSEED + " WHERE [seedid] = @seedid";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        ///// <summary>
        ///// 更新种子状态，bt_seed_detail表，不包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public int UpdateSeedEditDetailWithOutSeed(PTSeedinfo seedinfo)
        //{
        //    DbParameter[] parms = GetSeedParameterShort(seedinfo);
        //    string sqlstring = " UPDATE [bt_seed_detail] SET " + SQL_EDIT_SEED_DETAIL_WITHOUTSEED + " WHERE [seedid] = @seedid";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        ///// <summary>
        ///// 更新种子状态，bt_seed_tracker表，包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public int UpdateSeedEditTrackerWithSeed(PTSeedinfo seedinfo)
        //{
        //    DbParameter[] parms = GetSeedParameterTracker(seedinfo);
        //    string sqlstring = " UPDATE [bt_seed_tracker] SET " + SQL_EDIT_SEED_TRACKER_WITHSEED + " WHERE [seedid] = @seedid";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}

        /// <summary>
        /// 更新种子最后完成时间
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public int UpdateSeedLastFinish(int seedid, int coincount)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@coincount",(DbType)SqlDbType.Int, 4, coincount),
                                        DbHelper.MakeInParam("@lastfinish",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "UPDATE [bt_seed_detail] WITH (ROWLOCK) SET [lastfinish] = @lastfinish, [award] = [award] + @coincount WHERE [seedid] = @seedid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        
        /// <summary>
        /// announce页面更新bt_seed表，种子（开始、完成、停止）动作，需要更新上传下载数，traffic、finished为增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public int UpdateSeedAnnounce(int seedid, int upload, int download, int finished, int ipv6, decimal uploadtraffic, decimal downloadtraffic, bool add, int oldup, int olddown)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Int, 4, upload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Int, 4, download),
                                        DbHelper.MakeInParam("@finished",(DbType)SqlDbType.Int, 4, finished),
                                        DbHelper.MakeInParam("@uptraffic",(DbType)SqlDbType.Decimal, 16, uploadtraffic),
                                        DbHelper.MakeInParam("@traffic",(DbType)SqlDbType.Decimal, 16, downloadtraffic),
                                        DbHelper.MakeInParam("@ipv6",(DbType)SqlDbType.Int, 4, ipv6),
                                        DbHelper.MakeInParam("@oldup",(DbType)SqlDbType.Int, 4, oldup),
                                        DbHelper.MakeInParam("@olddown",(DbType)SqlDbType.Int, 4, olddown),
                                  };
            //string sqlstring = "UPDATE [bt_seed] SET [upload] = @upload, [download] = @download, [finished] = [finished] + @finished, [traffic] = [traffic] + @traffic, [uptraffic] = [uptraffic] + @uptraffic, [ipv6] = @ipv6 WHERE [seedid] = @seedid";
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            if (add) return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_seed_update_announce_add", parms);
            else return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_seed_update_announce", parms);
        }
        /// <summary>
        /// announce页面更新bt_seed表，非增量，均为绝对数值
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public int UpdateSeedAnnounce(int seedid, int upload, int download, int finished, int ipv6)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Int, 4, upload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Int, 4, download),
                                        DbHelper.MakeInParam("@finished",(DbType)SqlDbType.Int, 4, finished),
                                        DbHelper.MakeInParam("@ipv6",(DbType)SqlDbType.Int, 4, ipv6),
                                  };
            string sqlstring = "UPDATE [bt_seed] SET [upload] = @upload, [download] = @download, [finished] = @finished, [ipv6] = @ipv6 WHERE [seedid] = @seedid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// announce页面更新bt_seed表，非增量，均为绝对数值
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public int UpdateSeedAnnounce(int seedid, int upload, int download, int ipv6)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Int, 4, upload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Int, 4, download),
                                        DbHelper.MakeInParam("@ipv6",(DbType)SqlDbType.Int, 4, ipv6),
                                  };
            string sqlstring = "UPDATE [bt_seed] SET [upload] = @upload, [download] = @download, [ipv6] = @ipv6 WHERE [seedid] = @seedid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// announce页面更新bt_seed表，种子中间状态更新，只更新上传数，增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public int UpdateSeedAnnounceUpTrafficOnly(int seedid, decimal uploadtraffic)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uptraffic",(DbType)SqlDbType.Decimal, 16, uploadtraffic),
                                  };
            string sqlstring = "UPDATE [bt_seed] SET [uptraffic] = [uptraffic] + @uptraffic WHERE [seedid] = @seedid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// announce页面更新bt_seed表，种子中间状态更新，只更新下载数，增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public int UpdateSeedAnnounceTrafficOnly(int seedid, decimal uploadtraffic, decimal downloadtraffic)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uptraffic",(DbType)SqlDbType.Decimal, 16, uploadtraffic),
                                        DbHelper.MakeInParam("@downtraffic",(DbType)SqlDbType.Decimal, 16, downloadtraffic),
                                  };
            //string sqlstring = "UPDATE [bt_seed] SET [traffic] = [traffic] + @downtraffic, [uptraffic] = [uptraffic] + @uptraffic WHERE [seedid] = @seedid";
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_seed_updatetrafficonly_announce", parms);
        }
        /// <summary>
        /// 【存储过程】更新seed_top表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int UpdateTopSeedList(int type)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, type),
                                  };
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_updatetopseedlist", parms);
        }
        /// <summary>
        /// 更新seed和detail表，live和lastseeder
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="live"></param>
        /// <param name="uid"></param>
        /// <param name="usernmae"></param>
        /// <returns></returns>
        public int UpdateSeedLive(int seedid, int live, int uid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@live",(DbType)SqlDbType.Int, 4, live),
                                        DbHelper.MakeInParam("@lastlive",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@lastlivelimit",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddMinutes(-5)),

                                  };
            //string sqlstring = "UPDATE [bt_seed] SET [live] = @live, [lastlive] = @lastlive WHERE [seedid] = @seedid AND [lastlive] < @lastlivelimit";
            //if (DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms) > 0)

            if (DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_seed_updateseedlive", parms) > 0)
            {
                //sqlstring = "UPDATE [bt_seed_detail] SET [lastseederid] = @uid, [lastseedername] = ( SELECT [username] from [dnt_users] WHERE [uid] = @uid ) WHERE [seedid] = @seedid";
                //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_seed_updatelastseeder", parms);
            }
            else
            {
                //sqlstring = "UPDATE [bt_seed] SET [lastlive] = [lastlive] WHERE [seedid] = @seedid";
                //DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

                //sqlstring = "UPDATE [bt_seed_detail] SET [lastseederid] = @uid, [lastseedername] = ( SELECT [username] from [dnt_users] WHERE [uid] = @uid ) WHERE [seedid] = @seedid";
                //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            }
            return 0;
        }

        //////////////////////////////////////////////////////////////////////////
        //out update views,reply


        /// <summary>
        /// 更新种子浏览量，增量
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="viewcount">浏览量</param>
        /// <returns>成功返回1，否则返回0</returns>
        public int UpdateSeedTopicViewCount(int tid, int viewCount)
        {
            DbParameter[] parms = {
										DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,tid),	
										DbHelper.MakeInParam("@viewcount",(DbType)SqlDbType.Int,4,viewCount)			   
									};
            string commandText = string.Format("UPDATE [bt_seed] SET [views] = [views] + @viewcount WHERE [topicid] = @tid",
                                    BaseConfigs.GetTablePrefix,
                                    tid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 更新种子浏览量(准确值，从topic表获取)
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <returns>成功返回1，否则返回0</returns>
        public int UpdateSeedTopicViewCountAccurate(int tid, int seedid)
        {
            DbParameter[] parms = {
										DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,tid),			   
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int,4,seedid),	
									};
            string commandText = "UPDATE [bt_seed] SET [views] = (SELECT [views] FROM [dnt_topics] WHERE [tid] = @tid) WHERE [seedid] = @seedid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 列新种子的回复数
        /// </summary>
        /// <param name="tid">主题ID</param>
        /// <param name="postTableid">回复表ID</param>
        public int UpdateSeedTopicReplyCount(int tid)
        {
            DbParameter[] parms = {
										DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,tid),		
									};
            string commandText = "UPDATE [bt_seed] SET [replies] = (SELECT [replies] FROM [dnt_topics] WHERE [tid] = @tid) WHERE [topicid] = @tid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 增加种子的当天完成数
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int AddSeedFinishedToday(int seedid, int uid)
        {
            DbParameter[] parms = {		   
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int,4,seedid),	
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),	
									};
            string commandText = "UPDATE [bt_seed] SET [bt_seed].[finishedtoday] = [bt_seed].[finishedtoday] + 1 WHERE [bt_seed].[seedid] = @seedid AND NOT EXISTS (SELECT [bt_finished].[id] FROM [bt_finished] WITH(NOLOCK) WHERE [bt_finished].[seedid]= @seedid AND [bt_finished].[uid]= @uid AND DATEDIFF(D, [bt_finished].[finishtime], GETDATE())=0)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 清零种子当天完成数
        /// </summary>
        /// <returns></returns>
        public int ClearSeedFinishedToday()
        {
            string sqlstring = "UPDATE [bt_seed] SET [finishedtoday] = 0";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
        }

        /// <summary>
        /// 获取种子搜索缓存
        /// </summary>
        /// <param name="seedstatus"></param>
        /// <param name="seedtype"></param>
        /// <param name="keywords"></param>
        /// <param name="notin"></param>
        /// <returns></returns>
        public string GetSeedSearchCache(int searchmode, int seedstatus, int seedtype, string keywords)
        {
            DbParameter[] parms = {		   
                                      DbHelper.MakeInParam("@searchmode",(DbType)SqlDbType.Int, 4, searchmode),
                                        DbHelper.MakeInParam("@seedstatus",(DbType)SqlDbType.Int, 4, seedstatus),
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                                        DbHelper.MakeInParam("@expire",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@keywords",(DbType)SqlDbType.NVarChar, 500, keywords),
									};
            string sqlstring = "SELECT TOP 1 [result] FROM [bt_seed_searchcache] WITH(NOLOCK) WHERE [searchmode] = @searchmode AND [seedstatus] = @seedstatus AND [seedtype] = @seedtype AND [expire] > @expire AND [keywords] = @keywords";
            object obj = DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms);
            if (obj == null) return "null";
            else return obj.ToString();
        }
        /// <summary>
        /// 插入种子搜索缓存
        /// </summary>
        /// <param name="seedstatus"></param>
        /// <param name="seedtype"></param>
        /// <param name="keywords"></param>
        /// <param name="notin"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public int InsertSeedSearchCache(int searchmode, int seedstatus, int seedtype, string keywords, string result)
        {
            DbParameter[] parms = {		   
                                      DbHelper.MakeInParam("@searchmode",(DbType)SqlDbType.Int, 4, searchmode),
                                        DbHelper.MakeInParam("@seedstatus",(DbType)SqlDbType.Int, 4, seedstatus),
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                                        DbHelper.MakeInParam("@expire",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddHours(6)),
                                        DbHelper.MakeInParam("@keywords",(DbType)SqlDbType.NVarChar, 500, keywords),
                                        DbHelper.MakeInParam("@result",(DbType)SqlDbType.Text, 0, result),
									};
            string sqlstring = "INSERT INTO [bt_seed_searchcache] ([searchmode],[seedstatus],[seedtype],[expire],[keywords],[result]) VALUES (@searchmode, @seedstatus, @seedtype, @expire, @keywords, @result)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 清理种子搜索缓存
        /// </summary>
        /// <returns></returns>
        public int CleanSeedSearchCache()
        {
            DbParameter[] parms = {		   
                                        DbHelper.MakeInParam("@expire",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddDays(-3)),
									};
            string sqlstring = "DELETE FROM [bt_seed_searchcache] WHERE [expire] < @expire";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }


    }
}