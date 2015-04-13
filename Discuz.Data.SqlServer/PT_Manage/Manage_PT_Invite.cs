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
        /// 获得邀请人
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetInviter(int userid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                  };
            string sqlstring = "SELECT [uid] FROM [bt_invite] WHERE [registerid]=@uid ";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), 0);
        }
        /// <summary>
        /// 获得对应userid的用户的所有邀请码
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>邀请码DataTable</returns>
        public DataTable GetInviteCodeList(int userid, int pagesize, int currentpage)
        {
            string sqlstring;
            int pagetop = (currentpage - 1) * pagesize;
            if (currentpage == 1)
            {
                sqlstring = string.Format("SELECT TOP {2} [c].* FROM [bt_invite] AS [c] WHERE [c].[uid] = {1} AND [c].[used] = 'False' ORDER BY [id] DESC", BaseConfigs.GetTablePrefix, userid, pagesize);
            }
            else
            {
                sqlstring = string.Format("SELECT TOP {2} [c].* FROM [bt_invite] AS [c] WHERE [c].[id] > (SELECT MAX([id]) FROM (SELECT TOP {3} [id] FROM [bt_invite] WHERE [bt_invite].[uid] = {1} AND [bt_invite].[used] = 'False' ORDER BY [id] ASC) AS tblTmp) AND [c].[uid] = {1}  AND [c].[used] = 'False' ORDER BY [id] DESC", BaseConfigs.GetTablePrefix, userid, pagesize, pagetop);
            }

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring).Tables[0];
        }
        /// <summary>
        /// 获得对应userid的用户的所有邀请码
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>邀请码DataTable</returns>
        public DataTable GetInviteCodeListUsed(int userid, int pagesize, int currentpage, DateTime buytime)
        {
            int pagetop = (currentpage - 1) * pagesize;
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int, 4, pagesize),
                                        DbHelper.MakeInParam("@currentpage",(DbType)SqlDbType.Int, 4, currentpage),
                                        DbHelper.MakeInParam("@pagetop",(DbType)SqlDbType.Int, 4, pagetop),
                                        DbHelper.MakeInParam("@buytime",(DbType)SqlDbType.DateTime, 8, buytime),
                                  };

            string sqlstring;
            if (currentpage == 1)
            {
                sqlstring = string.Format("SELECT TOP {0} [c].* FROM [bt_invite] AS [c] WHERE [c].[uid] = @uid AND ( [c].[used] = 'True' OR [c].[buytime] < @buytime ) ORDER BY [id] DESC", pagesize);
            }
            else
            {
                sqlstring = string.Format("SELECT TOP {0} [c].* FROM [bt_invite] AS [c] WHERE [c].[id] < (SELECT MIN([id]) FROM (SELECT TOP {1} [id] FROM [bt_invite] WHERE [bt_invite].[uid] = @uid AND ( [bt_invite].[used] = 'True' OR [bt_invite].[buytime] < @buytime ) ORDER BY [id] DESC) AS tblTmp) AND [c].[uid] = @uid  AND ( [c].[used] = 'True' OR [c].[buytime] < @buytime ) ORDER BY [id] DESC", pagesize, pagetop);
            }

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获得对应userid的用户的所有邀请码
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>邀请码DataTable</returns>
        public DataTable GetInviteCodeList(int userid, int pagesize, int currentpage, DateTime buytime)
        {
            int pagetop = (currentpage - 1) * pagesize;
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int, 4, pagesize),
                                        DbHelper.MakeInParam("@currentpage",(DbType)SqlDbType.Int, 4, currentpage),
                                        DbHelper.MakeInParam("@pagetop",(DbType)SqlDbType.Int, 4, pagetop),
                                        DbHelper.MakeInParam("@buytime",(DbType)SqlDbType.DateTime, 8, buytime),
                                  };

            string sqlstring;
            
            if (currentpage == 1)
            {
                sqlstring = string.Format("SELECT TOP {0} [c].* FROM [bt_invite] AS [c] WHERE [c].[uid] = @uid AND [c].[used] = 'False' AND [c].[buytime] > @buytime ORDER BY [id] ASC", pagesize);
            }
            else
            {
                sqlstring = string.Format("SELECT TOP {0} [c].* FROM [bt_invite] AS [c] WHERE [c].[id] > (SELECT MAX([id]) FROM (SELECT TOP {1} [id] FROM [bt_invite] WHERE [bt_invite].[uid] = @uid AND [bt_invite].[used] = 'False' AND [bt_invite].[buytime] > @buytime ORDER BY [id] ASC) AS tblTmp) AND [c].[uid] = @uid  AND [c].[used] = 'False' AND [c].[buytime] > @buytime ORDER BY [id] ASC", pagesize, pagetop);
            }

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获得对应userid的用户的已经使用或过期邀请码数量
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>邀请码DataTable</returns>
        public int GetInviteCodeListUsedCount(int userid, DateTime buytime)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@buytime",(DbType)SqlDbType.DateTime, 8, buytime),
                                  };
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(1) FROM [bt_invite] AS [c] WHERE [c].[uid] = @uid AND ( [c].[used] = 'True' OR [c].[buytime] < @buytime )"), parms), 0);
        }
        /// <summary>
        /// 获得对应userid的用户的未使用邀请码数量
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>邀请码DataTable</returns>
        public int GetInviteCodeListCount(int userid, DateTime buytime)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@buytime",(DbType)SqlDbType.DateTime, 8, buytime),
                                  };
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(1) FROM [bt_invite] AS [c] WHERE [c].[uid] = @uid AND [c].[used] = 'False' AND [c].[buytime] > @buytime "), parms), 0);
        }
        /// <summary>
        /// 获得对应id用户的邀请码数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetInviteCodeCount(int userid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                  };
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(1) FROM [bt_invite] WHERE [uid]= @uid AND [used] = 'False'"), parms), 0);
        }
        /// <summary>
        /// 获得对应userid的用户所邀请的用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pagesize"></param>
        /// <param name="currentpage"></param>
        /// <returns></returns>
        public DataTable GetInviteUsersList(int userid, int pagesize, int currentpage)
        {

            //sqlstring = string.Format("SELECT TOP {0} [c].*, [ufrom].[username] AS [fromuser], [uto].[username] AS [touser] 
            //FROM [{1}creditslog] AS [c], [{1}users] AS [ufrom], [{1}users] AS [uto] 
            //WHERE [id] < 
            //(SELECT MIN([id])  FROM 
            //(SELECT TOP {2} [id] FROM [{1}creditslog] WHERE [{1}creditslog].[uid]={3}  OR [{1}creditslog].[fromto]={3} ORDER BY [id] DESC)
            //AS tblTmp )
            //AND [c].[uid]=[ufrom].[uid] AND [c].[fromto]=[uto].[uid] AND ([c].[uid]={3} OR [c].[fromto] = {3}) 
            //ORDER BY [c].[id] DESC", pagesize, BaseConfigs.GetTablePrefix, pagetop, uid);
            

            int pagetop = (currentpage - 1) * pagesize;
            string sqlstring;

            DbParameter[] parms = {
									  DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int,4, userid),
                                      DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4, pagesize),
                                      DbHelper.MakeInParam("@pagetop", (DbType)SqlDbType.Int,4, pagetop),
			                      };

            if (currentpage == 1)
            {
                sqlstring = string.Format("SELECT TOP (@pagesize) [c].*, [u].[username] AS [username], [u].[joindate] AS [joindate], [u].[extcredits3], [u].[extcredits4], [u].[ratio] AS [ratio], [u].[groupid] AS [groupid], [u].[lastvisit] AS [lastvisit] FROM [bt_invite] AS [c], [{0}users] AS [u] WHERE [c].[uid] = @uid AND [c].[used] = 'True' AND [c].[registerid] = [u].[uid] ORDER BY [c].[registerid] DESC", 
                    BaseConfigs.GetTablePrefix);
            }
            else
            {
                sqlstring = string.Format("SELECT TOP (@pagesize) [c].*, [u].[username] AS [username], [u].[joindate] AS [joindate], [u].[extcredits3], [u].[extcredits4], [u].[ratio] AS [ratio], [u].[groupid] AS [groupid], [u].[lastvisit] AS [lastvisit] FROM [bt_invite] AS [c], [{0}users] AS [u] WHERE [c].[registerid] < (SELECT MIN([registerid]) FROM (SELECT TOP (@pagetop) [bt_invite].[registerid] FROM [bt_invite],[{0}users] WHERE [bt_invite].[uid] = @uid AND [bt_invite].[used] = 'True' AND [bt_invite].[registerid] = [{0}users].[uid] ORDER BY [registerid] DESC) AS tblTmp) AND [c].[uid] = @uid AND [c].[used] = 'True' AND [c].[registerid] = [u].[uid] ORDER BY [c].[registerid] DESC", 
                    BaseConfigs.GetTablePrefix);

            }
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获得对应id用户的邀请用户数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetInviteUserCount(int userid)
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(1) FROM [bt_invite] WHERE [uid]={0} AND [used] = 'True'", userid)), 0);
        }
        /// <summary>
        /// 为对应userid的用户添加指定的邀请码
        /// </summary>
        /// <param name="userid">id</param>
        /// <param name="invitecode">邀请码</param>
        /// <returns>数据库更改行数</returns>
        public int AddInviteCode(int userid, string invitecode)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@code",(DbType)SqlDbType.NChar, 32, invitecode),
										   DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid),
                                           DbHelper.MakeInParam("@buytime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                           DbHelper.MakeInParam("@awardlevel",(DbType)SqlDbType.Int, 4, 10),
									   };
            //验证这个邀请码是否存在
            if (Utils.StrToInt(DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("SELECT COUNT(1) FROM [bt_invite] WHERE [invitekey] = @code"), parms), 0) < 1)
            {
                string sqlstring = string.Format("INSERT INTO [bt_invite] ([uid],[used],[invitekey],[buytime],[awardlevel]) VALUES(@userid, 'False', @code, @buytime, @awardlevel)");
                return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            }
            else return 0;
        }
        /// <summary>
        /// 验证邀请码是否存在，并将使用标记为True
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns>数据库更改行数</returns>
        public int VerifyInviteCode(string invitecode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@code",(DbType)SqlDbType.NChar, 32, invitecode)	   
                                  };
            string sqlstring = string.Format("UPDATE [bt_invite] SET [used] = 'True' WHERE [invitekey] = @code AND [used] = 'False'");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 将邀请码标记为已经使用
        /// </summary>
        /// <param name="invitecode"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int UseInviteCode(string invitecode, int userid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@code",(DbType)SqlDbType.NChar, 32, invitecode),	
                                        DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, userid)
                                  };
            string sqlstring = string.Format("UPDATE [bt_invite] SET [used] = 'True', [registerid] = @userid  WHERE [invitekey] = @code");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 注册用户失败时，将邀请码恢复
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        public int RestoreInviteCode(string invitecode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@code",(DbType)SqlDbType.NChar, 32, invitecode)	   
                                  };
            string sqlstring = string.Format("UPDATE [bt_invite] SET [used] = 'False' WHERE [invitekey] = @code");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取该注册码的主人id
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        public IDataReader GetInviteCodeBuyer(string invitecode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@code",(DbType)SqlDbType.NChar, 32, invitecode)	   
                                  };
            string sqlstring = string.Format("SELECT TOP 1 [uid] FROM [bt_invite] WHERE [invitekey] = @code");
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取该注册码的购买时间
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        public IDataReader GetInviteCodeTime(string invitecode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@code",(DbType)SqlDbType.NChar, 32, invitecode)	   
                                  };
            string sqlstring = string.Format("SELECT TOP 1 [buytime] FROM [bt_invite] WHERE [invitekey] = @code");
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取被邀请用户中指定奖励级别和流量的用户
        /// </summary>
        /// <param name="AwardLevel"></param>
        /// <param name="AwardUpload"></param>
        /// <returns></returns>
        public DataTable GetInviteUser(int AwardLevel, decimal AwardUpload)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@awardupload",(DbType)SqlDbType.Decimal, 32, AwardUpload),
                                        DbHelper.MakeInParam("@awardlevel",(DbType)SqlDbType.Int, 4, AwardLevel),
                                  };
            //string sqlstring = string.Format("SELECT [bt_invite].[uid], [bt_invite].[registerid], [dnt_users].[username] FROM [dnt_users],[bt_invite] WHERE [bt_invite].[registerid] = [dnt_users].[uid] AND [bt_invite].[used] > 0 AND [bt_invite].[awardlevel] = @awardlevel AND [dnt_users].[extcredits7] > @awardupload");
            //return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];

            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_invite_getinviteuser", parms).Tables[0];
        }
        /// <summary>
        /// 设置指定邀请用户的奖励级别
        /// </summary>
        /// <param name="RegisterId"></param>
        /// <param name="AwardLevel"></param>
        /// <returns></returns>
        public int SetInviteUserLevel(int RegisterId, int AwardLevel)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@registerid",(DbType)SqlDbType.Int, 4, RegisterId),
                                        DbHelper.MakeInParam("@awardlevel",(DbType)SqlDbType.Int, 4, AwardLevel),
                                  };
            string sqlstring = string.Format("UPDATE [bt_invite] SET [bt_invite].[awardlevel] = @awardlevel WHERE [bt_invite].[registerid] = @registerid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
    }
}