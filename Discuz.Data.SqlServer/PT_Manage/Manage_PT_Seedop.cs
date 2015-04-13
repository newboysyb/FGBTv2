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
        /// bt_seedop表，用于种子操作列表显示
        /// </summary>
        public const string SQL_SEEDOP_LIST_WITHTABLE = " [bt_seedop].[operation],[bt_seedop].[operator],[bt_seedop].[date],[bt_seedop].[operatreason],[bt_seedop].[operatorid],[bt_seedop].[operattype] ";


        /// <summary>
        /// 获得种子操作记录数
        /// </summary>
        /// <returns></returns>
        public int GetSeedOPCount(int OperatorId, int OperatType, int SeedType, int userid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, SeedType),
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@operatorid",(DbType)SqlDbType.Int, 4, OperatorId),
                                        DbHelper.MakeInParam("@operattype",(DbType)SqlDbType.Int, 4, OperatType),
                                  };
            string sqlstring = "SELECT COUNT([date]) ";
            sqlstring += " FROM [bt_seedop] LEFT JOIN [bt_seed] ON [bt_seedop].[seedid] = [bt_seed].[seedid] ";

            string fromwhere = "";
            if (OperatorId > -1) fromwhere += " [bt_seedop].[operatorid] = @operatorid ";
            if (userid > 0) fromwhere += (fromwhere == "" ? "" : " AND ") + " [bt_seed].[uid] = @userid ";
            if (SeedType > 0) fromwhere += (fromwhere == "" ? "" : " AND ") + " [bt_seed].[type] = @seedtype ";
            if (OperatType > -1) fromwhere += (fromwhere == "" ? "" : " AND ") + " [bt_seedop].[operattype] = @operattype ";
            sqlstring += fromwhere == "" ? "" : " WHERE " + fromwhere;

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), 0);
        }
        /// <summary>
        /// 获得种子操作记录表
        /// </summary>
        /// <param name="OperatorId">操作者ID</param>
        /// <param name="OperatType">操作类型</param>
        /// <param name="SeedType">种子类型</param>
        /// <param name="userid">种子发布者ID</param>
        /// <param name="numperpage"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public DataTable GetSeedOPList(int OperatorId, int OperatType, int SeedType, int userid, int numperpage, int pageindex)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, SeedType),
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@operatorid",(DbType)SqlDbType.Int, 4, OperatorId),
                                        DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                                        DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int, 4, pageindex),
                                        DbHelper.MakeInParam("@operattype",(DbType)SqlDbType.Int, 4, OperatType),
                                  };
            string sqlstring = "SELECT TOP(@numperpage) " + SQL_SEEDOP_LIST_WITHTABLE + ", " + SQL_SEED_LIST_WITHTABLE;
            sqlstring += " FROM [bt_seedop] LEFT JOIN [bt_seed] ON [bt_seedop].[seedid] = [bt_seed].[seedid] ";
            
            string fromwhere = "";
            if (OperatorId > -1) fromwhere += " [bt_seedop].[operatorid] = @operatorid ";
            if (userid > 0) fromwhere += (fromwhere == "" ? "" : " AND ") + " [bt_seed].[uid] = @userid ";
            if (SeedType > 0) fromwhere += (fromwhere == "" ? "" : " AND ") + " [bt_seed].[type] = @seedtype ";
            if (OperatType > -1) fromwhere += (fromwhere == "" ? "" : " AND ") + " [bt_seedop].[operattype] = @operattype ";

            sqlstring += fromwhere == "" ? "" : " WHERE " + fromwhere;

            if (pageindex > 1)
            {
                sqlstring += (fromwhere == "" ? " WHERE " : " AND ") + " [bt_seedop].[date] < (SELECT MIN([date]) FROM (";
                sqlstring += "SELECT TOP(@numperpage * @pageindex - @numperpage) [bt_seedop].[date] FROM [bt_seedop] LEFT JOIN [bt_seed] ON [bt_seedop].[seedid] = [bt_seed].[seedid] ";
                sqlstring += fromwhere == "" ? "" : " WHERE " + fromwhere;
                sqlstring += "  ORDER BY [bt_seedop].[date] DESC";
                sqlstring += ") AS tblTmp) ";
            }

            sqlstring += "  ORDER BY [bt_seedop].[date] DESC";

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 插入种子操作记录，置顶0，取消置顶1，流量系数2，取消流量系数3，删除4，自删除5，编辑6，自编辑7，替换种子8，自替换种子9，移动种子10，自移动种子11，屏蔽种子12，取消屏蔽13，奖励流量14，扣除流量15，禁止种子16
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="op"></param>
        /// <param name="opr"></param>
        /// <returns></returns>
        public int InsertSeedModLog(int seedid, string op, string opr, string operatreason, int operatorid, int operattype, int operatvalue)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@op",(DbType)SqlDbType.NVarChar, 100, op),
                                        DbHelper.MakeInParam("@opr",(DbType)SqlDbType.NChar, 20, opr),
                                        DbHelper.MakeInParam("@modtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@operatreason",(DbType)SqlDbType.NVarChar, 500, operatreason),
                                        DbHelper.MakeInParam("@operatorid",(DbType)SqlDbType.Int, 4, operatorid),
                                        DbHelper.MakeInParam("@operattype",(DbType)SqlDbType.Int, 4, operattype),
                                        DbHelper.MakeInParam("@operatvalue",(DbType)SqlDbType.Int, 4, operatvalue),
                                  };
            string sqlstring = "INSERT INTO [bt_seedop] ([seedid],[operation],[operator],[date],[operatreason],[operatorid],[operattype],[operatvalue]) VALUES(@seedid, @op, @opr, @modtime,@operatreason,@operatorid,@operattype,@operatvalue)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获得种子操作记录
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetSeedModLog(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                  };
            string sqlstring = "SELECT [bt_seedop].* FROM [bt_seedop] WHERE [bt_seedop].[seedid] = @seedid AND [bt_seedop].[operattype] < 100 ORDER BY [bt_seedop].[date] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
    }
}