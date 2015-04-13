using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;


using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
    /// <summary>
    /// 邀请码操作类
    /// </summary>
    public class PrivateBTInvitation
    {
        /// <summary>
        /// 获得邀请人
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetInviter(int userid)
        {
            return DatabaseProvider.GetInstance().GetInviter(userid);
        }
        /// <summary>
        /// 获得对应userid的用户的所有邀请码
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>邀请码表</returns>
        public static DataTable GetInviteCodeList(int userid, int pagesize, int currentpage, bool used)
        {
            DataTable dt;
            if(used) dt = DatabaseProvider.GetInstance().GetInviteCodeListUsed(userid, pagesize, currentpage, DateTime.Now.AddDays(-14));
            else dt = DatabaseProvider.GetInstance().GetInviteCodeList(userid, pagesize, currentpage, DateTime.Now.AddDays(-14));
            dt.Columns.Add("sn", System.Type.GetType("System.Int32"));
            dt.Columns.Add("valid");
            dt.Columns.Add("status");
            int i = 1 + (currentpage - 1) * pagesize;
            DateTime buytime = DateTime.Now;
            foreach (DataRow dr in dt.Rows)
            {
                dr["sn"] = i++;
                buytime = DateTime.Parse(dr["buytime"].ToString());
                if (buytime < DateTime.Parse("3/1/2011 0:0:0"))
                {
                    dr["valid"] = DateTime.Parse("3/15/2011 0:0:0").ToString();
                    if (TypeConverter.ObjectToInt(dr["registerid"], -1) > 0) dr["status"] = "<font color=\"#00AA00\"><strong>已注册</strong></font>";
                    else if (TypeConverter.ObjectToInt(dr["registerid"], 1) < 0) dr["status"] = "<font color=\"#00AA00\"><strong>已复活</strong></font>";
                    else if (dr["used"].ToString() == "True") dr["status"] = "<font color=\"#AA0000\"><strong>注册出错</strong></font>";
                    else if (DateTime.Now < DateTime.Parse("3/15/2011 0:0:0")) dr["status"] = "<font color=\"#00AA00\"><strong>有效</strong></font>";
                    else dr["status"] = "<font color=\"#AA0000\"><strong>已失效</strong></font>";
                }
                else
                {
                    dr["valid"] = (buytime.AddDays(14)).ToString();
                    if (TypeConverter.ObjectToInt(dr["registerid"], -1) > 0) dr["status"] = "<font color=\"#00AA00\"><strong>已注册</strong></font>";
                    else if (TypeConverter.ObjectToInt(dr["registerid"], 1) < 0) dr["status"] = "<font color=\"#00AA00\"><strong>已复活</strong></font>";
                    else if (dr["used"].ToString() == "True") dr["status"] = "<font color=\"#AA0000\"><strong>注册出错</strong></font>";
                    else if (DateTime.Now < buytime.AddDays(14)) dr["status"] = "<font color=\"#00AA00\"><strong>有效</strong></font>";
                    else dr["status"] = "<font color=\"#AA0000\"><strong>已失效</strong></font>";
                }
            }
            dt.Dispose();
            return dt;
        }
        /// <summary>
        /// 获得对应id用户的邀请码数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetInviteCodeCount(int userid, bool used)
        {
            if(used)
                return DatabaseProvider.GetInstance().GetInviteCodeListUsedCount(userid, DateTime.Now.AddDays(-14));
            else
                return DatabaseProvider.GetInstance().GetInviteCodeListCount(userid, DateTime.Now.AddDays(-14));
        }
        /// <summary>
        /// 获得对应userid的用户所邀请的用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pagesize"></param>
        /// <param name="currentpage"></param>
        /// <returns></returns>
        public static DataTable GetInviteUsersList(int userid, int pagesize, int currentpage)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetInviteUsersList(userid, pagesize, currentpage);
            dt.Columns.Add("group");    //用户级别描述
            dt.Columns.Add("up");       //上传描述
            dt.Columns.Add("down");     //下载描述
            dt.Columns.Add("rat");      //共享率描述
            dt.Columns.Add("sn", System.Type.GetType("System.Int32"));

            int i = 1 + (currentpage - 1) * pagesize;
            foreach (DataRow dr in dt.Rows)
            {
                UserGroupInfo group = UserGroups.GetUserGroupInfo(Utils.StrToInt(dr["groupid"], 0));
                if (group.Color == string.Empty)
                {
                    dr["group"] = group.Grouptitle;
                }
                else
                {
                    dr["group"] = string.Format("<font color='{1}'>{0}</font>", group.Grouptitle, group.Color);
                }
                dr["up"] = PTTools.Upload2Str(Convert.ToDecimal(dr["extcredits3"].ToString()));
                dr["down"] = PTTools.Upload2Str(Convert.ToDecimal(dr["extcredits4"].ToString()));
                dr["rat"] = PTTools.Ratio2Str(decimal.Parse(dr["extcredits3"].ToString()), decimal.Parse(dr["extcredits4"].ToString()));
                dr["sn"] = i++;
            }
            dt.Dispose();
            return dt;
        }
        /// <summary>
        /// 获得对应id用户的邀请用户数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetInviteUserCount(int userid)
        {
            return DatabaseProvider.GetInstance().GetInviteUserCount(userid);
        }
        /// <summary>
        /// 为对应userid的用户添加指定数量的邀请码
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="count">数量</param>
        /// <returns>实际添加的邀请码的数量</returns>
        public static int AddInviteCode(int userid, int count)
        {
            int i = 0, e = 0;
            for (i = 0; i < count;)
            {
                if (DatabaseProvider.GetInstance().AddInviteCode(userid, PTTools.GetRandomString(32)) < 1) e++;
                else i++;
                if(e > 3) break;
            }
            return i;
        }

        /// <summary>
        /// 验证邀请码是否存在，并将使用标记为True
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns>数据库更改行数</returns>
        public static int VerifyInviteCode(string invitecode)
        {
            //确认邀请人账号正常
            int uid = GetInviteCodeBuyer(invitecode);
            if (uid > 0)
            {
                UserInfo buyer = Users.GetUserInfo(uid);
                if (buyer.Groupid == 5 || buyer.VIP < 0) return -1;
            }
            else return -1;

            DateTime buytime = GetInviteCodeTime(invitecode);
            if (buytime < DateTime.Parse("3/1/2011 0:0:0"))
            {
                if (DateTime.Now > DateTime.Parse("3/15/2011 0:0:0")) return -1;
            }
            else
            {
                if (DateTime.Now > buytime.AddDays(14)) return -1;
            }

            return DatabaseProvider.GetInstance().VerifyInviteCode(invitecode);
        }
        /// <summary>
        /// 将邀请码标记为已经使用
        /// </summary>
        /// <param name="invitecode"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int UseInviteCode(string invitecode, int userid)
        {
            return DatabaseProvider.GetInstance().UseInviteCode(invitecode, userid);
        }
        /// <summary>
        /// 注册用户失败时，将邀请码恢复
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        public static int RestoreInviteCode(string invitecode)
        {
            return DatabaseProvider.GetInstance().RestoreInviteCode(invitecode);
        }
        /// <summary>
        /// 获取该注册码的主人id
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        public static int GetInviteCodeBuyer(string invitecode)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetInviteCodeBuyer(invitecode);
            string uid = "";
            if (reader.Read())
            {
                uid = reader[0].ToString();
            }
            else uid = "-1";
            reader.Close();
            return int.Parse(uid);
        }
        /// <summary>
        /// 获取该注册码的购买时间
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        public static DateTime GetInviteCodeTime(string invitecode)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetInviteCodeTime(invitecode);
            string buytime = "";
            if (reader.Read())
            {
                buytime = reader[0].ToString();
            }
            else buytime = "1/1/2000 0:0:0";
            reader.Close();
            return DateTime.Parse(buytime);
        }
        /// <summary>
        /// 为每位用户发放3枚邀请码
        /// </summary>
        public static void EveryAddInviteCode()
        {
            //return; //此功能已经取消，安全起见
            DataTable dt = DatabaseProvider.GetInstance().GetAllUserID();
            PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
            string curdatetime = Utils.GetDateTime();
            int adduserid = 0;
            foreach (DataRow dr in dt.Rows)
            {
                adduserid = int.Parse(dr["uid"].ToString());
                AddInviteCode(adduserid, 2);

                //privatemessageinfo.Message = "发放邀请时请注意，如果被邀请人违反论坛相关规定被禁封，您将受到连带处罚";
                //privatemessageinfo.Subject = "成功完成校园统一认证绑定，系统赠送您1个邀请码";
                //privatemessageinfo.Msgto = Users.GetUserName(adduserid);
                //privatemessageinfo.Msgtoid = adduserid;
                //privatemessageinfo.Msgfrom = PrivateMessages.SystemUserName;
                //privatemessageinfo.Msgfromid = 0;
                //privatemessageinfo.New = 1;
                //privatemessageinfo.Postdatetime = curdatetime;
                //privatemessageinfo.Folder = 0;
                //PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0); //向购买邀请码的人发送短消息
            }
            dt.Dispose();

        }
        /// <summary>
        /// 为每位用户发送短消息（config表allmsg的值）
        /// </summary>
        public static void EverySendMsg()
        {
            string allmsg = "";
            string allmsgtitle = "";
            IDataReader reader = DatabaseProvider.GetInstance().GetPrivateBTConfigToReader();
            if (reader.Read())
            {
                allmsg = reader["allmsg"].ToString();
                allmsgtitle = reader["allmsgtitle"].ToString();
                reader.Close();
            }
            else
            {
                reader.Close();
                return;
            }
            
            DataTable dt = DatabaseProvider.GetInstance().GetAllUserID();
            PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
            string curdatetime = Utils.GetDateTime();
            int adduserid = 0;
            foreach (DataRow dr in dt.Rows)
            {
                adduserid = int.Parse(dr["uid"].ToString());
                privatemessageinfo.Message = allmsg;
                privatemessageinfo.Subject = allmsgtitle;
                privatemessageinfo.Msgto = Users.GetUserName(adduserid);
                privatemessageinfo.Msgtoid = adduserid;
                privatemessageinfo.Msgfrom = PrivateMessages.SystemUserName;
                privatemessageinfo.Msgfromid = 0;
                privatemessageinfo.New = 1;
                privatemessageinfo.Postdatetime = curdatetime;
                privatemessageinfo.Folder = 0;
                PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0); 
            }
            dt.Dispose();
        }
        /// <summary>
        /// 为邀请用户上传达到一定值时奖励邀请者
        /// </summary>
        public static void InviteAward()
        {
            //上传达到60G者 level 0
            int awardid = 0;
            int registerid = 0;
            string registerusername = "";
            DataTable dt = DatabaseProvider.GetInstance().GetInviteUser(0, 60 * 1024 * 1024 * 1024M); 
            foreach (DataRow dr in dt.Rows)
            {
                awardid = int.Parse(dr["uid"].ToString());
                registerid = int.Parse(dr["registerid"].ToString());
                registerusername = dr["username"].ToString().Trim();

                Users.UpdateUserExtCredits(awardid, 3, 30 * 1024 * 1024 * 1024f);
                CreditsLogs.AddCreditsLog(awardid, registerid, 3, 3, 0, 30 * 1024 * 1024 * 1024f, Utils.GetDateTime(), 6);
                
                PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                string curdatetime = Utils.GetDateTime();
                privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您邀请的用户 <a href = \"userinfo-" + registerid.ToString() + ".aspx\">" + registerusername + "</a> 的真实上传已经达到60GB，现奖励您上传30GB，以感谢您对未来花园BT站发展所作出的贡献\n\n未来花园BT站";
                privatemessageinfo.Subject = "邀请用户" + registerusername + "上传达到60GB奖励通知";
                privatemessageinfo.Msgfrom = "系统";
                privatemessageinfo.Msgfromid = 0;
                privatemessageinfo.New = 1;
                privatemessageinfo.Postdatetime = curdatetime;
                privatemessageinfo.Folder = 0;
                privatemessageinfo.Msgtoid = awardid;
                privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                DatabaseProvider.GetInstance().SetInviteUserLevel(registerid, 1);
                UserCredits.UpdateUserCreditsBackground(awardid);
            }
            dt.Dispose();

            //上传达到120G者 level 1
            dt = DatabaseProvider.GetInstance().GetInviteUser(1, 120M * 1024 * 1024 * 1024M);
            foreach (DataRow dr in dt.Rows)
            {
                awardid = int.Parse(dr["uid"].ToString());
                registerid = int.Parse(dr["registerid"].ToString());
                registerusername = dr["username"].ToString().Trim();

                Users.UpdateUserExtCredits(awardid, 3, 30f * 1024 * 1024 * 1024);
                CreditsLogs.AddCreditsLog(awardid, registerid, 3, 3, 0, 30 * 1024 * 1024 * 1024f, Utils.GetDateTime(), 6);

                PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                string curdatetime = Utils.GetDateTime();
                privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您邀请的用户 <a href = \"userinfo-" + registerid.ToString() + ".aspx\">" + registerusername + "</a> 的真实上传已经达到120GB，现再次奖励您上传30GB，以感谢您对未来花园BT站发展所作出的贡献\n\n未来花园BT站";
                privatemessageinfo.Subject = "邀请用户" + registerusername + "上传达到120GB奖励通知";
                privatemessageinfo.Msgfrom = "系统";
                privatemessageinfo.Msgfromid = 0;
                privatemessageinfo.New = 1;
                privatemessageinfo.Postdatetime = curdatetime;
                privatemessageinfo.Folder = 0;
                privatemessageinfo.Msgtoid = awardid;
                privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                DatabaseProvider.GetInstance().SetInviteUserLevel(registerid, 2);
                UserCredits.UpdateUserCreditsBackground(awardid);
            }
            dt.Dispose();

            //上传达到240G者 level 2
            dt = DatabaseProvider.GetInstance().GetInviteUser(2, 240M * 1024 * 1024 * 1024M);
            foreach (DataRow dr in dt.Rows)
            {
                awardid = int.Parse(dr["uid"].ToString());
                registerid = int.Parse(dr["registerid"].ToString());
                registerusername = dr["username"].ToString().Trim();

                Users.UpdateUserExtCredits(awardid, 3, 30f * 1024 * 1024 * 1024);
                CreditsLogs.AddCreditsLog(awardid, registerid, 3, 3, 0, 30 * 1024 * 1024 * 1024f, Utils.GetDateTime(), 6);

                PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                string curdatetime = Utils.GetDateTime();
                privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您邀请的用户 <a href = \"userinfo-" + registerid.ToString() + ".aspx\">" + registerusername + "</a> 的真实上传已经达到240GB，现再次奖励您上传30GB，以感谢您对未来花园BT站发展所作出的贡献\n\n未来花园BT站";
                privatemessageinfo.Subject = "邀请用户" + registerusername + "上传达到240GB奖励通知";
                privatemessageinfo.Msgfrom = "系统";
                privatemessageinfo.Msgfromid = 0;
                privatemessageinfo.New = 1;
                privatemessageinfo.Postdatetime = curdatetime;
                privatemessageinfo.Folder = 0;
                privatemessageinfo.Msgtoid = awardid;
                privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                DatabaseProvider.GetInstance().SetInviteUserLevel(registerid, 3);
                UserCredits.UpdateUserCreditsBackground(awardid);
            }
            dt.Dispose();

            //上传达到480G者 level 3
            dt = DatabaseProvider.GetInstance().GetInviteUser(3, 480M * 1024 * 1024 * 1024M);
            foreach (DataRow dr in dt.Rows)
            {
                awardid = int.Parse(dr["uid"].ToString());
                registerid = int.Parse(dr["registerid"].ToString());
                registerusername = dr["username"].ToString().Trim();

                Users.UpdateUserExtCredits(awardid, 3, 60f * 1024 * 1024 * 1024f);
                CreditsLogs.AddCreditsLog(awardid, registerid, 3, 3, 0, 60 * 1024 * 1024 * 1024f, Utils.GetDateTime(), 6);

                PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                string curdatetime = Utils.GetDateTime();
                privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您邀请的用户 <a href = \"userinfo-" + registerid.ToString() + ".aspx\">" + registerusername + "</a> 的真实上传已经达到480GB，现再次奖励您上传60GB，以感谢您对未来花园BT站发展所作出的贡献\n\n未来花园BT站";
                privatemessageinfo.Subject = "邀请用户" + registerusername + "上传达到480GB奖励通知";
                privatemessageinfo.Msgfrom = "系统";
                privatemessageinfo.Msgfromid = 0;
                privatemessageinfo.New = 1;
                privatemessageinfo.Postdatetime = curdatetime;
                privatemessageinfo.Folder = 0;
                privatemessageinfo.Msgtoid = awardid;
                privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                DatabaseProvider.GetInstance().SetInviteUserLevel(registerid, 4);
                UserCredits.UpdateUserCreditsBackground(awardid);
            }
            dt.Dispose();

            //上传达到1024G者 level 4
            dt = DatabaseProvider.GetInstance().GetInviteUser(4, 1024M * 1024 * 1024 * 1024M);
            foreach (DataRow dr in dt.Rows)
            {
                awardid = int.Parse(dr["uid"].ToString());
                registerid = int.Parse(dr["registerid"].ToString());
                registerusername = dr["username"].ToString().Trim();

                Users.UpdateUserExtCredits(awardid, 3, 120f * 1024 * 1024 * 1024f);
                CreditsLogs.AddCreditsLog(awardid, registerid, 3, 3, 0, 120 * 1024 * 1024 * 1024f, Utils.GetDateTime(), 6);

                PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                string curdatetime = Utils.GetDateTime();
                privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您邀请的用户 <a href = \"userinfo-" + registerid.ToString() + ".aspx\">" + registerusername + "</a> 的真实上传已经达到1TB，现再次奖励您上传120GB，以感谢您对未来花园BT站发展所作出的贡献\n\n未来花园BT站";
                privatemessageinfo.Subject = "邀请用户" + registerusername + "上传达到1TB奖励通知";
                privatemessageinfo.Msgfrom = "系统";
                privatemessageinfo.Msgfromid = 0;
                privatemessageinfo.New = 1;
                privatemessageinfo.Postdatetime = curdatetime;
                privatemessageinfo.Folder = 0;
                privatemessageinfo.Msgtoid = awardid;
                privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                DatabaseProvider.GetInstance().SetInviteUserLevel(registerid, 5);
                UserCredits.UpdateUserCreditsBackground(awardid);
            }
            dt.Dispose();


            //上传达到10240G者 level 5
            dt = DatabaseProvider.GetInstance().GetInviteUser(5, 10M * 1024 * 1024 * 1024 * 1024M);
            foreach (DataRow dr in dt.Rows)
            {
                awardid = int.Parse(dr["uid"].ToString());
                registerid = int.Parse(dr["registerid"].ToString());
                registerusername = dr["username"].ToString().Trim();

                Users.UpdateUserExtCredits(awardid, 3, 240f * 1024 * 1024 * 1024f);
                CreditsLogs.AddCreditsLog(awardid, registerid, 3, 3, 0, 240f * 1024 * 1024 * 1024f, Utils.GetDateTime(), 6);

                PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                string curdatetime = Utils.GetDateTime();
                privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您邀请的用户 <a href = \"userinfo-" + registerid.ToString() + ".aspx\">" + registerusername + "</a> 的真实上传已经达到10TB，现再次奖励您上传240GB，以感谢您对未来花园BT站发展所作出的贡献\n\n未来花园BT站";
                privatemessageinfo.Subject = "邀请用户" + registerusername + "上传达到10TB奖励通知";
                privatemessageinfo.Msgfrom = "系统";
                privatemessageinfo.Msgfromid = 0;
                privatemessageinfo.New = 1;
                privatemessageinfo.Postdatetime = curdatetime;
                privatemessageinfo.Folder = 0;
                privatemessageinfo.Msgtoid = awardid;
                privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                DatabaseProvider.GetInstance().SetInviteUserLevel(registerid, 6);
                UserCredits.UpdateUserCreditsBackground(awardid);
            }
            dt.Dispose();
        }
    }
}
