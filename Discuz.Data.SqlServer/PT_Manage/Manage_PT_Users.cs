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
        /// 获取PTUserInfo用，基页面，由uid获取
        /// </summary>
        public const string SQL_PT_USER_INFO_PAGEBASE = " [vip],[credits],[extcredits1],[extcredits2],[extcredits3],[extcredits4],[extcredits5],[extcredits6],[extcredits7],[extcredits8],[extcredits9],[extcredits10],[extcredits11],[extcredits12],[ratio],[ratioprotection],[uploadcount],[downloadcount],[finishcount],[publishcount],[joindate]";
        /// <summary>
        /// 获取PTUserInfo用，Tracker用，由passkey获取
        /// </summary>
        public const string SQL_PT_USER_INFO_TRACKER = " [uid],[groupid],[vip],[extcredits3],[extcredits4],[ratioprotection],[uploadcount],[downloadcount],[lasttrackerupdate],[lastcreditsupdate]";
        /// <summary>
        /// 获取PTUserInfo用，包含passkey等信息，详细显示用
        /// </summary>
        public const string SQL_PT_USER_INFO = " [groupid],[vip],[passkey],[credits],[extcredits1],[extcredits2],[extcredits3],[extcredits4],[extcredits5],[extcredits6],[extcredits7],[extcredits8],[extcredits9],[extcredits10],[ratio],[uploadcount],[downloadcount],[finishcount],[publishcount],[joindate]";

        /// <summary>
        /// 执行签到
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="lastcheckincount"></param>
        /// <param name="lastcheckintime"></param>
        /// <returns></returns>
        public int DoCheckIn(int uid, int lastcheckincount, DateTime lastcheckintime, int newcheckincount, DateTime newcheckintime)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@lastcheckincount",(DbType)SqlDbType.Int, 4, lastcheckincount),
                                        DbHelper.MakeInParam("@lastcheckintime",(DbType)SqlDbType.DateTime, 8, lastcheckintime),
                                        DbHelper.MakeInParam("@newcheckincount",(DbType)SqlDbType.Int, 4, newcheckincount),
                                        DbHelper.MakeInParam("@newcheckintime",(DbType)SqlDbType.DateTime, 8, newcheckintime),
                                  };
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_user_docheckin", parms);
        }

        /// <summary>
        /// 添加签到日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int AddCheckInLog(int uid, DateTime date, DateTime time, int count)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@date",(DbType)SqlDbType.Date, 4, date),
                                        DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, time),
                                        DbHelper.MakeInParam("@count",(DbType)SqlDbType.Int, 4, count),
                                  };
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_user_addcheckinlog", parms));
        }
        /// <summary>
        /// 添加签到日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetCheckInRank(int uid, DateTime date, DateTime time, int count)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@date",(DbType)SqlDbType.Date, 4, date),
                                        DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, time),
                                        DbHelper.MakeInParam("@count",(DbType)SqlDbType.Int, 4, count),
                                  };
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_user_getcheckinrank", parms));
        }

        /// <summary>
        /// 获得今天签到次序
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int GetCheckInRank(int uid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@date",(DbType)SqlDbType.Date, 4, DateTime.Now.Date),
                                  };
            string sqlstring = string.Format("SELECT TOP 1 [rank] FROM [bt_checkin] WITH(NOLOCK) WHERE [checkindate] = @date AND [userid] = @uid");
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms));
        }

        /// <summary>
        /// 校验用户复活校验码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="VFCode"></param>
        /// <param name="invitecodeid"></param>
        /// <returns></returns>
        public int VerifyUserReActiveVFCode(int uid, string VFCode, int invitecodeid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@logtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddMinutes(-10)),
	                                    DbHelper.MakeInParam("@vfcode",(DbType)SqlDbType.Char, 20, VFCode),
                                        DbHelper.MakeInParam("@invitecodeid",(DbType)SqlDbType.Int, 4, invitecodeid),
                                  };
            string sqlstring = string.Format("UPDATE [bt_reactiveuserlog] SET [invitecodeid] = @invitecodeid WHERE [uid] = @uid AND [vfcode] = @vfcode AND [logtime] > @logtime AND [invitecodeid] = 0");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 记录用户复活校验码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="VFCode"></param>
        /// <returns></returns>
        public int CreateUserReActiveVFCode(int uid, string VFCode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@logtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
	                                    DbHelper.MakeInParam("@vfcode",(DbType)SqlDbType.Char, 20, VFCode),
                                        DbHelper.MakeInParam("@invitecodeid",(DbType)SqlDbType.Int, 4, 0),
                                  };
            string sqlstring = string.Format("INSERT INTO [bt_reactiveuserlog] ([uid],[logtime],[vfcode],[invitecodeid]) VALUES(@uid,@logtime,@vfcode,@invitecodeid)");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        public int ReActiveUser(int uid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                  };
            string sqlstring = string.Format("UPDATE [{0}users] SET [vip] = 10, [groupid] = 10 WHERE [uid] = @uid AND [vip] = -1 AND [groupid] = 5", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        
        /// <summary>
        /// 获取用户随机RKey（登陆一次改变一次）
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetUserRKey(int uid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
			                        };
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 [rkey] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [" + BaseConfigs.GetTablePrefix + "users].[uid]=@uid", parms);

        }
        /// <summary>
        /// 更新用户的RatioProtection
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratioprotection"></param>
        /// <returns></returns>
        public int SetUserRatioProtection(int uid, int ratioprotection)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
                                       DbHelper.MakeInParam("@ratioprotection",(DbType)SqlDbType.Int, 4, ratioprotection),
			                        };
            string sqlstring = "UPDATE [" + BaseConfigs.GetTablePrefix + "users] SET [ratioprotection] = @ratioprotection WHERE [uid] = @uid ";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新用户随机RKey
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rkey"></param>
        /// <returns></returns>
        public int SetUserRKey(int uid, string rkey)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
                                       DbHelper.MakeInParam("@rkey",(DbType)SqlDbType.Char, 10, rkey),
			                        };
            string sqlstring = "UPDATE [" + BaseConfigs.GetTablePrefix + "users] SET [rkey] = @rkey WHERE [uid] = @uid ";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新用户随机RKey
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rkey"></param>
        /// <returns></returns>
        public int SetOnlineRKey(int uid, string rkey)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
                                       DbHelper.MakeInParam("@rkey",(DbType)SqlDbType.Char, 10, rkey),
                                       DbHelper.MakeInParam("@exptime",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddMinutes(5)),
			                        };
            string sqlstring = "UPDATE [" + BaseConfigs.GetTablePrefix + "online] SET [rkey] = @rkey, [rkeyexpire] = [rkey], [rkeyexpiretime] = @exptime WHERE [userid] = @uid ";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }



        /// <summary>
        /// 获取所有用户id表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllUserID()
        {
            string sqlstring = "SELECT [uid] FROM [dnt_users]";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring).Tables[0];
        }
        /// <summary>
        /// 检查该Passkey是否存在，返回该Passkey存在数量（存在行数）
        /// </summary>
        /// <param name="passkey"></param>
        /// <returns></returns>
        public int CheckPasskey(string passkey)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@passkey",(DbType)SqlDbType.Char, 32, passkey)	   
                                  };
            string sqlstring = string.Format("SELECT COUNT(1) FROM [{0}users] WHERE [passkey] = @passkey", BaseConfigs.GetTablePrefix);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), 0);
        }
        /// <summary>
        /// 为指定uid的用户更新passkey，返回影响行数
        /// </summary>
        /// <param name="passkey"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int UpdatePasskey(string passkey, int uid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@passkey",(DbType)SqlDbType.Char, 32, passkey),
	                                    DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid)
                                  };
            string sqlstring = string.Format("UPDATE [{0}users] SET [passkey] = @passkey WHERE [uid] = @uid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }


        /// <summary>
        /// 由username获得用户密码信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetUserPasswordSHAInfoToReader(string username)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar, 20, username)
									   };
            string sqlstring = "SELECT TOP (1) [uid],[passwordsha],[salt],[password],[username],[secques] FROM [dnt_users] WHERE [username] = @username";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 由uid获得用户密码信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetUserPasswordSHAInfoToReader(int uid)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid)
									   };
            string sqlstring = "SELECT TOP (1) [uid],[passwordsha],[salt],[password],[username],[secques] FROM [dnt_users] WHERE [uid] = @uid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新用户sha密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="passwordsha"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public int UpdateUserPasswordSHAInfo(int uid, string passwordsha, string salt)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@passwordsha",(DbType)SqlDbType.Char, 43, passwordsha),
                                        DbHelper.MakeInParam("@salt",(DbType)SqlDbType.Char, 16, salt),
	                                    DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid)
                                  };
            string sqlstring = string.Format("UPDATE [{0}users] SET [passwordsha] = @passwordsha, [salt] = @salt WHERE [uid] = @uid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }



        /// <summary>
        /// 由passkey获得用户信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetPTUserInfoToReaderForTracker(string passkey)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@passkey",(DbType)SqlDbType.Char, 32, passkey)
									   };
            //string sqlstring = "SELECT TOP (1) " + SQL_PT_USER_INFO_TRACKER + " FROM [dnt_users] WHERE [passkey] = @passkey";
            //return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);

            return DbHelper.ExecuteReader(CommandType.StoredProcedure, "bt_users_getptuserinfo_tracker", parms);
        }
        /// <summary>
        /// 由uid获得用户信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetPTUserInfoToReaderForPagebase(int uid)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid)
									   };
            string sqlstring = "SELECT TOP (1) " + SQL_PT_USER_INFO_PAGEBASE + " FROM [dnt_users] WHERE [uid] = @uid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 由uid获得用户信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public IDataReader GetPTUserInfoToReader(int uid)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid)
									   };
            string sqlstring = "SELECT TOP (1) " + SQL_PT_USER_INFO + " FROM [dnt_users] WHERE [uid] = @uid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获得今天总下载量
        /// </summary>
        /// <returns></returns>
        public decimal GetUserTodayDownload()
        {
            string sqlstring = "SELECT ISNULL(SUM([extcredits10]),0) FROM [dnt_users]"; //可能是空集，输出为0
            return decimal.Parse(DbHelper.ExecuteScalar(CommandType.Text, sqlstring).ToString());
        }
        /// <summary>
        /// 获得历史总下载量,1为全部物理，2为今天物理，3为全部统计下载，4为统计今天下载，5为统计全部上传，6为统计今天上传,7为金币
        /// </summary>
        /// <returns></returns>
        public decimal GetUserTotalDownload(int totaltype)
        {
            string sqlstring = "";
            if (totaltype == 1) sqlstring = "SELECT ISNULL(SUM([extcredits8]),0) FROM [dnt_users]"; //可能是空集，输出为0
            else if (totaltype == 2) sqlstring = "SELECT ISNULL(SUM([extcredits10]),0) FROM [dnt_users]"; //可能是空集，输出为0
            else if (totaltype == 3) sqlstring = "SELECT ISNULL(SUM([extcredits4]),0) FROM [dnt_users]"; //可能是空集，输出为0
            else if (totaltype == 4) sqlstring = "SELECT ISNULL(SUM([extcredits6]),0) FROM [dnt_users]"; //可能是空集，输出为0
            else if (totaltype == 5) sqlstring = "SELECT ISNULL(SUM([extcredits3]),0) FROM [dnt_users]"; //可能是空集，输出为0
            else if (totaltype == 6) sqlstring = "SELECT ISNULL(SUM([extcredits5]),0) FROM [dnt_users]"; //可能是空集，输出为0
            else if (totaltype == 7) sqlstring = "SELECT ISNULL(SUM([extcredits2]),0) FROM [dnt_users]"; //可能是空集，输出为0
            else sqlstring = "SELECT ISNULL(SUM([extcredits8]),0) FROM [dnt_users]"; //可能是空集，输出为0
            return decimal.Parse(DbHelper.ExecuteScalar(CommandType.Text, sqlstring).ToString());
        }
        /// <summary>
        /// 【EX除去指定uid】获得指定种子的指定uid或者ip的peer节点数，0或者空为不限
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 更新BT用户信息，realupload/realdownload/extcredits3/extcredits4,5,6,7,8/finished均为增量,此外还更新ratio,upcout,downcount,lastactivity
        /// </summary>
        /// <param name="btuserinfo"></param>
        /// <returns></returns>
        public int UpdateUserInfo_Tracker(PTUserInfo btuserinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, btuserinfo.Uid),
                                        DbHelper.MakeInParam("@ratio",(DbType)SqlDbType.Float, 8, btuserinfo.Ratio),
                                        DbHelper.MakeInParam("@uploadcount",(DbType)SqlDbType.Int, 4, btuserinfo.UploadCount),
                                        DbHelper.MakeInParam("@downloadcount",(DbType)SqlDbType.Int, 8, btuserinfo.DownloadCount),
                                        DbHelper.MakeInParam("@finishcount",(DbType)SqlDbType.Int, 4, btuserinfo.FinishCount),
                                        DbHelper.MakeInParam("@lastactivity",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@extcredits3",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits3),
                                        DbHelper.MakeInParam("@extcredits4",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits4),
                                        DbHelper.MakeInParam("@extcredits5",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits5),
                                        DbHelper.MakeInParam("@extcredits6",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits6),
                                        DbHelper.MakeInParam("@extcredits7",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits7),
                                        DbHelper.MakeInParam("@extcredits8",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits8),
                                        DbHelper.MakeInParam("@extcredits9",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits9),
                                        DbHelper.MakeInParam("@extcredits10",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits10),
                                        DbHelper.MakeInParam("@extcredits11",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits11),
                                        DbHelper.MakeInParam("@extcredits12",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits12),
                                  };
            //string sqlstring = "UPDATE [dnt_users] SET [ratio] = @ratio,[uploadcount] = @uploadcount,[downloadcount] = @downloadcount, [finishcount] = [finishcount] + @finishcount, [lastactivity] = @lastactivity, ";
            //sqlstring += "[extcredits3] = [extcredits3] + @extcredits3,[extcredits4] = [extcredits4] + @extcredits4, [extcredits5] = [extcredits5] + @extcredits5, [extcredits6] = [extcredits6] + @extcredits6, ";
            //sqlstring += "[extcredits7] = [extcredits7] + @extcredits7, [extcredits8] = [extcredits8] + @extcredits8, [extcredits9] = [extcredits9] + @extcredits9, [extcredits10] = [extcredits10] + @extcredits10  ";
            //sqlstring += " WHERE [uid] = @userid";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_user_updateuserinfo_tracker", parms);
        }
        /// <summary>
        /// 更新BT用户信息，realupload/realdownload/extcredits3/extcredits4,5,6,7,8/finished均为增量,此外还更新ratio,upcout,downcount,lastactivity
        /// </summary>
        /// <param name="btuserinfo"></param>
        /// <returns></returns>
        public int UpdateUserInfo_TrackerWithCredits(PTUserInfo btuserinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, btuserinfo.Uid),
                                        DbHelper.MakeInParam("@ratio",(DbType)SqlDbType.Float, 8, btuserinfo.Ratio),
                                        DbHelper.MakeInParam("@uploadcount",(DbType)SqlDbType.Int, 4, btuserinfo.UploadCount),
                                        DbHelper.MakeInParam("@downloadcount",(DbType)SqlDbType.Int, 8, btuserinfo.DownloadCount),
                                        DbHelper.MakeInParam("@finishcount",(DbType)SqlDbType.Int, 4, btuserinfo.FinishCount),
                                        DbHelper.MakeInParam("@lastactivity",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@extcredits3",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits3),
                                        DbHelper.MakeInParam("@extcredits4",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits4),
                                        DbHelper.MakeInParam("@extcredits5",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits5),
                                        DbHelper.MakeInParam("@extcredits6",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits6),
                                        DbHelper.MakeInParam("@extcredits7",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits7),
                                        DbHelper.MakeInParam("@extcredits8",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits8),
                                        DbHelper.MakeInParam("@extcredits9",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits9),
                                        DbHelper.MakeInParam("@extcredits10",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits10),
                                        DbHelper.MakeInParam("@extcredits11",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits11),
                                        DbHelper.MakeInParam("@extcredits12",(DbType)SqlDbType.Decimal, 32, btuserinfo.Extcredits12),
                                  };
            //string sqlstring = "UPDATE [dnt_users] SET [ratio] = @ratio,[uploadcount] = @uploadcount,[downloadcount] = @downloadcount, [finishcount] = [finishcount] + @finishcount, [lastactivity] = @lastactivity, ";
            //sqlstring += "[extcredits3] = [extcredits3] + @extcredits3,[extcredits4] = [extcredits4] + @extcredits4, [extcredits5] = [extcredits5] + @extcredits5, [extcredits6] = [extcredits6] + @extcredits6, ";
            //sqlstring += "[extcredits7] = [extcredits7] + @extcredits7, [extcredits8] = [extcredits8] + @extcredits8, [extcredits9] = [extcredits9] + @extcredits9, [extcredits10] = [extcredits10] + @extcredits10  ";
            //sqlstring += " WHERE [uid] = @userid";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_user_updateuserinfo_trackerwithcredits", parms);
        }
        /// <summary>
        /// 更新BT用户信息Ratio
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public int UpdateUserInfo_Ratio(int uid, double ratio)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@ratio",(DbType)SqlDbType.Float, 8, ratio),
                                  };
            string sqlstring = "UPDATE [dnt_users] SET [ratio] = @ratio WHERE [uid] = @userid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新BT用户信息lastkeepreward
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public int UpdateUserInfo_LastKeepReward(int uid, DateTime updatetime)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@lastkeeprewardtime",(DbType)SqlDbType.DateTime, 8, updatetime),
                                  };
            string sqlstring = "UPDATE [dnt_users] SET [lastkeeprewardtime] = @lastkeeprewardtime WHERE [uid] = @userid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新BT用户信息Ratio，UploadCount，DownloadCount
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public int UpdateUserInfo_Ratio(int uid, double ratio, int uploadcount, int downloadcount)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@ratio",(DbType)SqlDbType.Float, 8, ratio),
                                        DbHelper.MakeInParam("@uploadcount",(DbType)SqlDbType.Int, 4, uploadcount),
                                        DbHelper.MakeInParam("@downloadcount",(DbType)SqlDbType.Int, 4, downloadcount),
                                  };
            string sqlstring = "UPDATE [dnt_users] SET [ratio] = @ratio, [uploadcount] = @uploadcount, [downloadcount] = @downloadcount WHERE [uid] = @userid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新BT用户发布数
        /// </summary>
        /// <param name="btuserinfo"></param>
        /// <returns></returns>
        public int UpdateUserInfo_RefreashPublish(int userid, int publishcount)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@publishcount",(DbType)SqlDbType.Int, 4, publishcount),
                                  };
            string sqlstring = "UPDATE [dnt_users] SET [publishcount] = @publishcount ";
            sqlstring += " WHERE [uid] = @userid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 禁封2周内无真实上传的用户
        /// </summary>
        /// <returns></returns>
        public int UpdateUser2WeeksBan()
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@timelimit",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddDays(-30)),
                                  };
            string sqlstring = "UPDATE [dnt_users] WITH(ROWLOCK) SET [groupid] = 5, [vip] = -1 WHERE [lastactivity] < @timelimit AND [extcredits8] = 0 AND ([groupid] != 5 AND [vip] = 0)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 禁封2010.09.30日之后注册，注册两个月之后下载达不到20G的用户
        /// </summary>
        /// <returns></returns>
        public int UpdateUser60Ban()
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@timelimit",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddDays(-65)),
                                        DbHelper.MakeInParam("@timelimitstart",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddDays(-165)),
                                  };
            string sqlstring = "UPDATE [dnt_users] WITH(ROWLOCK) SET [groupid] = 5, [vip] = -1 WHERE ([groupid] != 5 AND [vip] = 0) AND [extcredits4] < 21474836480 AND [joindate] > @timelimitstart AND [joindate] < @timelimit";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 修改用户VIP SET
        /// </summary>
        /// <returns></returns>
        public int UpdateUserVIPSet()
        {
            string sqlstring = "UPDATE [dnt_users] SET [vip] = '1' WHERE [extcredits3] > '10995116277760' AND [vip] = '0'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
        }
        /// <summary>
        /// 修改用户DailyTraffic SET
        /// </summary>
        /// <returns></returns>
        public int UpdateUserDailyTraffic()
        {
            //string sqlstring = "UPDATE [dnt_users] SET [extcredits5] = '0', [extcredits6] = '0', [extcredits9] = '0', [extcredits10] = '0'";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_user_resetusertodaytraffic");
        }
        /// <summary>
        /// 修改用户VIP REMOVE
        /// </summary>
        /// <returns></returns>
        public int UpdateUserVIPRemove()
        {
            string sqlstring = "UPDATE [dnt_users] SET [vip] = '0' WHERE [extcredits3] < '10995116277760' AND [vip] = '1'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
        }
        /// <summary>
        /// 获取共享率排行
        /// </summary>
        /// <param name="bASC"></param>
        /// <returns></returns>
        public DataTable GetUserRatioList(bool bASC)
        {
            string sqlstring = "";
            if (bASC)
                sqlstring = string.Format("SELECT TOP (200) [uid], [ratio], [username], [extcredits4], [extcredits3] FROM [dnt_users] WHERE [extcredits4] > 21474836480 AND ([groupid] < 5 OR [groupid] > 9) ORDER BY [ratio] asc");
            else
                sqlstring = string.Format("SELECT TOP (200) [uid], [ratio], [username], [extcredits4], [extcredits3] FROM [dnt_users] WHERE [extcredits4] > 21474836480 AND ([groupid] < 5 OR [groupid] > 9) ORDER BY [ratio] desc"); ;
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring).Tables[0];
        }
    }
}