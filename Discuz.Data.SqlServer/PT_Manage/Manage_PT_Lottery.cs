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
        public int InsertLotteryInfo(LotteryInfo lottery)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, lottery.Tid),
                                        DbHelper.MakeInParam("@basewager",(DbType)SqlDbType.Int, 4, lottery.BaseWager),
                                        DbHelper.MakeInParam("@poster",(DbType)SqlDbType.NVarChar, 20, lottery.Poster),
                                        DbHelper.MakeInParam("@posterid",(DbType)SqlDbType.Int, 4, lottery.PosterId),
                                        DbHelper.MakeInParam("@starttime",(DbType)SqlDbType.DateTime, 8, lottery.StartTime),
                                        DbHelper.MakeInParam("@endtime",(DbType)SqlDbType.DateTime, 8, lottery.EndTime),
                                        DbHelper.MakeInParam("@ended",(DbType)SqlDbType.TinyInt, 1, lottery.Ended),  
                                  };
            string sqlstring = "INSERT INTO [bt_lottery] ([tid],[basewager],[poster],[posterid],[starttime],[endtime],[ended]) ";
            sqlstring += "VALUES(@tid,@basewager,@poster,@posterid,@starttime,@endtime,@ended)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        public IDataReader GetLotteryInfo(int tid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                  };
            string sqlstring = "SELECT TOP (1) [id],[tid],[basewager],[poster],[posterid],[starttime],[endtime],[wagercount],[ended] FROM [bt_lottery] WHERE [tid] = @tid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }

        public int UpdateLotteryInfo(int tid, int basewager, DateTime endtime, int ended)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                        DbHelper.MakeInParam("@basewager",(DbType)SqlDbType.Int, 4, basewager),
                                        DbHelper.MakeInParam("@endtime",(DbType)SqlDbType.DateTime, 8, endtime),
                                        DbHelper.MakeInParam("@ended",(DbType)SqlDbType.TinyInt, 1, ended),
                                  };
            string sqlstring = "UPDATE [bt_lottery] SET [ended] = @ended, [basewager] = @basewager, [endtime] = @endtime WHERE [tid] = @tid AND [ended] = 0";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }


        public int UpdateLotteryInfoSumCount(int tid, int optionid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                        DbHelper.MakeInParam("@optionid",(DbType)SqlDbType.Int, 4, optionid),
                                  };
            string sqlstring = "UPDATE T1 SET T1.[wagercount] = (SELECT SUM(T2.[wagercount]) FROM [bt_lottery_wager] AS T2 WHERE T2.[tid] = T1.[tid]) FROM [bt_lottery] AS T1 WHERE T1.[tid] = @tid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            
            sqlstring = "UPDATE T1 SET T1.[wagercount] = (SELECT SUM(T2.[wagercount]) FROM [bt_lottery_wager] AS T2 WHERE T2.[tid] = T1.[tid] AND T2.[optionid] = T1.[optionid]) FROM [bt_lottery_option] AS T1 WHERE T1.[tid] = @tid AND T1.[optionid] = @optionid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        //////////////////////////////////////////////////////////////////////////



        public int InsertLotteryOption(LotteryOption lotteryoption)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, lotteryoption.Tid),
                                        DbHelper.MakeInParam("@optionid",(DbType)SqlDbType.Int, 4, lotteryoption.OptionId),
                                        DbHelper.MakeInParam("@optionname",(DbType)SqlDbType.NVarChar, 100, lotteryoption.OptionName),
                                        DbHelper.MakeInParam("@wagercount",(DbType)SqlDbType.Int, 4, lotteryoption.WagerCount),
                                        DbHelper.MakeInParam("@wagerusercount",(DbType)SqlDbType.Int, 4, lotteryoption.WagerUserCount),
                                        DbHelper.MakeInParam("@win",(DbType)SqlDbType.TinyInt, 1, lotteryoption.Win),
                                  };
            string sqlstring = "INSERT INTO [bt_lottery_option] ([tid],[optionid],[optionname],[wagercount],[wagerusercount],[win]) ";
            sqlstring += "VALUES(@tid,@optionid,@optionname,@wagercount,@wagerusercount,@win)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        public DataTable GetLotteryOptionList(int tid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                  };
            string sqlstring = "SELECT [tid],[optionid],[optionname],[wagercount],[wagerusercount],[win] FROM [bt_lottery_option] WHERE [tid] = @tid ORDER BY [optionid] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        public int UpdateLotteryOption(int tid, int optionid, int win)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                        DbHelper.MakeInParam("@optionid",(DbType)SqlDbType.Int, 4, optionid),
                                        DbHelper.MakeInParam("@win",(DbType)SqlDbType.TinyInt, 1, win),
                                  };
            string sqlstring = "UPDATE [bt_lottery_option] SET [win] = @win WHERE [tid] = @tid AND [optionid] = @optionid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        public int DeleteLotteryOption(int tid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                  };
            string sqlstring = "DELETE FROM [bt_lottery_option] WHERE [tid] = @tid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }



        //////////////////////////////////////////////////////////////////////////



        public int InsertLotteryWager(LotteryWager lotterywager)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, lotterywager.Tid),
                                        DbHelper.MakeInParam("@optionid",(DbType)SqlDbType.Int, 4, lotterywager.OptionId),
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, lotterywager.Userid),
                                        DbHelper.MakeInParam("@wagertime",(DbType)SqlDbType.DateTime, 8, lotterywager.WagerTime),
                                        DbHelper.MakeInParam("@wagercount",(DbType)SqlDbType.Int, 4, lotterywager.WagerCount),
                                        DbHelper.MakeInParam("@win",(DbType)SqlDbType.Decimal, 18, lotterywager.Win),
                                  };
            string sqlstring = "INSERT INTO [bt_lottery_wager] ([tid],[optionid],[userid],[wagertime],[wagercount],[win]) ";
            sqlstring += "VALUES(@tid,@optionid,@userid,@wagertime,@wagercount,@win)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }


        public DataTable GetLotteryWagerList(int tid, int optionid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                        DbHelper.MakeInParam("@optionid",(DbType)SqlDbType.Int, 4, optionid),
                                  };
            string sqlstring = "SELECT [id],[tid],[optionid],T1.[userid],[wagertime],[wagercount],[win],T2.[username] FROM [bt_lottery_wager] AS T1 LEFT JOIN [dnt_users] AS T2 ON T2.[uid] = T1.[userid] WHERE [tid] = @tid AND [optionid] = @optionid";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }


        public DataTable GetLotteryWagerListbyUid(int tid, int userid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                                  };
            string sqlstring = "SELECT TOP(1) [id],[tid],[optionid],T1.[userid],[wagertime],[wagercount],[win],T2.[username] FROM [bt_lottery_wager] AS T1 LEFT JOIN [dnt_users] AS T2 ON T2.[uid] = T1.[userid] WHERE [tid] = @tid AND [userid] = @userid";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }


        public int UpdateLotteryWager(int id, decimal win)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id),

                                        DbHelper.MakeInParam("@win",(DbType)SqlDbType.Decimal, 18, win),
                                  };
            string sqlstring = "UPDATE [bt_lottery_wager] SET [win] = @win WHERE [id] = @id";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }


        //////////////////////////////////////////////////////////////////////////


        public int InsertLotteryLog(int tid, int uid, string action ,string message)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int, 4, tid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@action",(DbType)SqlDbType.VarChar, 20, action),
                                        DbHelper.MakeInParam("@logtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@message",(DbType)SqlDbType.NVarChar, 200, message),
                                  };
            string sqlstring = "INSERT INTO [bt_lottery_log] ([tid],[uid],[logtime],[action],[message]) ";
            sqlstring += "VALUES(@tid,@uid,@logtime,@action,@message)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

    }
}
