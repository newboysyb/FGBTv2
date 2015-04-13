using System;
using System.Data;
using System.Text;

using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Web.UI;
using Discuz.Plugin.PasswordMode;

namespace Discuz.Web
{
    /// <summary>
    /// 登录
    /// </summary>
    public class cngilogin : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 登录所使用的用户名
        /// </summary>
        public string postusername = Utils.HtmlEncode(DNTRequest.GetString("postusername")).Trim();
        /// <summary>
        /// 登陆时的密码验证信息
        /// </summary>
        public string loginauth = DNTRequest.GetString("loginauth");
        /// <summary>
        /// 登陆时提交的密码
        /// </summary>
        public string postpassword = "";
        /// <summary>
        /// 登陆成功后跳转的链接
        /// </summary>
        public string referer = Utils.HtmlEncode(DNTRequest.GetString("referer"));
        /// <summary>
        /// 是否跨页面提交
        /// </summary>
        public bool loginsubmit = DNTRequest.GetString("loginsubmit") == "true" ? true : false;
        /// <summary>
        /// 重设Email的加密校验，确保是该用户在当前页面操作的
        /// </summary>
        public string authstr = "";
        /// <summary>
        /// 需要激活的用户id
        /// </summary>
        public int needactiveuid = -1;
        /// <summary>
        /// 重置的Email信息的有效时间
        /// </summary>
        public string timestamp = "";
        /// <summary>
        /// 需要激活的用户Email
        /// </summary>
        public string email = "";

        public int inapi = 0;

        #endregion

        protected override void ShowPage()
        {

            //此页面用于CNGI绑定登陆

            inapi = DNTRequest.GetInt("inapi", 0);
            
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //【BT修改】必须通过CNGI08访问   此页面通过login.aspx修改而来

            pagetitle = "绑定未来花园BT账号到 CNGI08教育科研网统一身份认证平台";
            
            if (!cngi_user)
            {
                AddErrLine("非法的页面访问，您必须通过CNGI08教育科研网统一身份认证平台访问此页面");
                AddErrLine("如果您是通过正常的CNGI08链接访问此页面（地址栏域名为sp-bbs.buaa6.edu.cn），请将此问题反馈给我们，电子邮件到system@buaabt.cn 或 使用用户名密码登录后在意见反馈板块发帖，谢谢！");
                PTLog.InsertCNGILog("", "", PTLog.CNGILogType.CNGILogin, PTLog.LogStatus.Error, "CNGI_ILLEGAL", "CNGILOGIN FROM " + ipaddress + "---" + System.Web.HttpContext.Current.Request.Headers.Get("USERNAME") + "---" + System.Web.HttpContext.Current.Request.Headers.Get("INSTITUTION") + "---" + System.Web.HttpContext.Current.Request.Headers.Get("x-forwarded-for"));
                loginsubmit = false;
                ispost = true;
                return;
            }
            if (!cngi_login)
            {
                SetUrl("http://sp-bbs.buaa6.edu.cn/Shibboleth.sso/DS?target=http%3A%2F%2Fsp-bbs.buaa6.edu.cn%2Fcngilogin.aspx");
                SetMetaRefresh(3);
                SetShowBackLink(false);
                AddMsgLine("请先登陆CNGI高校BBS联盟认证！正在为您自动跳转");
                ispost = true;
                SetLeftMenuRefresh();
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////



            
            if (userid > 0)
            {

                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////
                //【BT修改】安全增强
                if (btuserinfo != null)
                {
                    if (btuserinfo.Groupid == 5 && userid > 2)
                    {
                        OnlineUsers.DeleteUserByUid(userid);
                        AddMsgLine("您的账户已经被禁止登陆");
                        loginsubmit = false;
                        return;
                    }
                }
                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////

                SetUrl(BaseConfigs.GetForumPath);
                AddMsgLine("您已经成功登录，正在为您跳转到首页");
                ispost = true;
                SetLeftMenuRefresh();

                if (APIConfigs.GetConfig().Enable)
                    APILogin(APIConfigs.GetConfig());
            }

            if (LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), false) >= 5)
            {
                AddErrLine("您已经多次输入密码错误, 请15分钟后再登录");
                loginsubmit = false;
                return;
            }

            SetReUrl();

            //如果提交...
            if (DNTRequest.IsPost())
            {
                SetBackLink();

                //如果没输入验证码就要求用户填写
                if (isseccode && DNTRequest.GetString("vcode") == "")
                {
                    postusername = DNTRequest.GetString("username");
                    loginauth = DES.Encode(DNTRequest.GetString("password"), config.Passwordkey).Replace("+", "[");
                    loginsubmit = true;
                    return;
                }

                if (config.Emaillogin == 1 && Utils.IsValidEmail(DNTRequest.GetString("username")))
                {
                    DataTable dt = Users.GetUserInfoByEmail(DNTRequest.GetString("username"));
                    if (dt.Rows.Count == 0)
                    {
                        AddErrLine("用户不存在");
                        return;
                    }
                    if (dt.Rows.Count > 1)
                    {
                        AddErrLine("您所使用Email不唯一，请使用用户名登陆");
                        return;
                    }
                    if (dt.Rows.Count == 1)
                    {
                        postusername = dt.Rows[0]["username"].ToString();
                    }
                }

                if (config.Emaillogin == 0)
                {
                    if ((Users.GetUserId(DNTRequest.GetString("username")) == 0))
                        AddErrLine("用户不存在");
                }

                if (Utils.StrIsNullOrEmpty(DNTRequest.GetString("password")) && Utils.StrIsNullOrEmpty(DNTRequest.GetString("loginauth")))
                    AddErrLine("密码不能为空");

                if (IsErr()) return;

                ShortUserInfo userInfo = GetShortUserInfo();

                if (userInfo != null)
                {
                    #region 当前用户所在用户组为"禁止访问"或"等待激活"时

                    if ((userInfo.Groupid == 4 || userInfo.Groupid == 5) && userInfo.Groupexpiry != 0 && userInfo.Groupexpiry <= Utils.StrToInt(DateTime.Now.ToString("yyyyMMdd"), 0))
                    {
                        //根据当前用户的积分获取对应积分用户组
                        UserGroupInfo groupInfo = UserCredits.GetCreditsUserGroupId(userInfo.Credits);
                        usergroupid = groupInfo.Groupid != 0 ? groupInfo.Groupid : usergroupid;
                        userInfo.Groupid = usergroupid;
                        Users.UpdateUserGroup(userInfo.Uid, usergroupid);
                    }

                    if (userInfo.Groupid == 5)// 5-禁止访问
                    {
                        AddErrLine("您所在的用户组，已经被禁止访问");
                        return;
                    }

                    if (userInfo.Groupid == 8)
                    {
                        if (config.Regverify == 1)
                        {

                            //////////////////////////////////////////////////////////////////////////
                            ////////////////////////////////////////////////////////////////////////// 
                            //【BT修改】重新发送验证连接
                            UserInfo userinfofull = Users.GetUserInfo(userInfo.Uid);
                            if (Emails.DiscuzSmtpMail(userinfofull.Username, userinfofull.Email, "NULL", userinfofull.Authstr))

                                AddMsgLine("系统已经再次向您的注册邮箱 " + userinfofull.Email + " 中发送验证链接<br/><br/>");

                            //【END BT修改】
                            //////////////////////////////////////////////////////////////////////////
                            //////////////////////////////////////////////////////////////////////////

                            needactiveuid = userInfo.Uid;
                            email = userInfo.Email;
                            timestamp = DateTime.Now.Ticks.ToString();
                            authstr = Utils.MD5(string.Concat(userInfo.Password, config.Passwordkey, timestamp));
                            AddMsgLine("请您到您的邮箱中点击激活链接来激活您的帐号");
                        }
                        else if (config.Regverify == 2)
                            AddMsgLine("您需要等待一些时间, 待系统管理员审核您的帐户后才可登录使用");
                        else
                            AddErrLine("抱歉, 您的用户身份尚未得到验证");

                        loginsubmit = false;
                        return;
                    }
                    #endregion

                    if (!Utils.StrIsNullOrEmpty(userInfo.Secques) && loginsubmit && Utils.StrIsNullOrEmpty(DNTRequest.GetString("loginauth")))
                    {
                        loginauth = DES.Encode(DNTRequest.GetString("password"), config.Passwordkey).Replace("+", "[");
                    }
                    else
                    {

                        //////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////// 
                        //【BT修改】

                        //删除当前游客的olineuser info
                        OnlineUsers.DeleteRows(oluserinfo.Olid);

                        oluserinfo = OnlineUsers.GetOnlineUser(OnlineUsers.GetOlidByUid(userInfo.Uid));
                        if (oluserinfo != null && oluserinfo.Userid == userInfo.Uid && oluserinfo.Ip.Trim() == DNTRequest.GetIP().Trim())
                        {
                            //之前已经有账号登陆，但是是同IP的
                        }
                        else if (OnlineUsers.DeleteUserByUid(userInfo.Uid) > 0)
                        {
                            //AddMsgLine("您的账户在其他地点已经登陆，已将在其强制下线，请勿在多个地点同时登陆未来花园BT账号<br/>");
                        }

                        string passwordmd5 = Utils.MD5(postpassword);

                        //添加随机RKey（联盟登陆，不重新生成rkey）
                        //string rkey = PTTools.GetRandomString(10);
                        //PrivateBT.SetUserRKey(userInfo.Uid, rkey);
                        //System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = rkey;
                        //System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;


                        //【END BT修改】
                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////


                        //通过api整合的程序登录
                        if (APIConfigs.GetConfig().Enable)
                            APILogin(APIConfigs.GetConfig());


                        AddMsgLine("登录成功, 返回登录前页面");


                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////
                        //【BT修改】联盟登陆，修改
                        //


                        ForumUtils.ClearUserCookie();
                        System.Web.HttpContext.Current.Response.Cookies.Clear();


                        //添加随机RKey，HTTPONLY
                        string rkey = PTUsers.SetUserRKey(userInfo.Uid);
                        PTUsers.UpdateCookiePassword(userInfo.Uid);
                        System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = rkey;
                        System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;
                        System.Web.HttpContext.Current.Response.Cookies["rkey"].HttpOnly = true;

                        //联盟登陆，关闭浏览器cookie作废
                        ForumUtils.WriteUserCookie(userInfo.Uid, 0, config.Passwordkey, DNTRequest.GetInt("templateid", 0), DNTRequest.GetInt("loginmode", -1));

                        if (PrivateBT.SetCNGIUserID(userInfo.Uid, cngi_name, cngi_school) != 1)
                        {
                            AddErrLine("验证未来花园BT账号成功，但是绑定CNGI 联盟登陆信息失败，请重试！");
                            return;
                        }

                        //赠送一个邀请码
                        PrivateBTInvitation.AddInviteCode(userInfo.Uid, 1);
                        PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                        privatemessageinfo.Message = "发放邀请时请注意，如果被邀请人违反论坛相关规定被禁封，您将受到连带处罚";
                        privatemessageinfo.Subject = "成功完成CNGI08教育科研网统一身份认证绑定，系统赠送您1个邀请码";
                        privatemessageinfo.Msgto = Users.GetUserName(userInfo.Uid);
                        privatemessageinfo.Msgtoid = userInfo.Uid;
                        privatemessageinfo.Msgfrom = "系统";
                        privatemessageinfo.Msgfromid = 0;
                        privatemessageinfo.New = 1;
                        privatemessageinfo.Postdatetime = Utils.GetDateTime(); ;
                        privatemessageinfo.Folder = 0;
                        PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0); //向购买邀请码的人发送短消息

                        //无延迟更新在线信息和相关用户信息
                        // 由于修改过OnlineUsers.UpdateInfo，下面的语句不能加，否则在执行时，由于rkey不符，将重置用户为游客。
                        // 如果使用原始函数不传递md5，则仍为游客，毫无意义
                        //oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout, userInfo.Uid, "");
                        //olid = oluserinfo.Olid;
                        //username = DNTRequest.GetString("username");
                        //userid = userInfo.Uid;
                        //usergroupinfo = UserGroups.GetUserGroupInfo(userInfo.Groupid);
                        //useradminid = usergroupinfo.Radminid; // 根据用户组得到相关联的管理组id
                        //OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0);

                        //安全增强：此处“登录错误重置”功能，修改为只删除错误计数为0的条目，防止绕过15分钟限制
                        LoginLogs.DeleteLoginLog(DNTRequest.GetIP());
                        Users.UpdateUserCreditsAndVisit(userInfo.Uid, DNTRequest.GetIP());

                        //CNGI绑定验证密码登陆成功日志
                        OnlineUsers.AddUserLoginRecord(userInfo.Uid, postusername, userInfo.Groupid, DNTRequest.GetIP(), 23, 1, passwordmd5, Utils.GetCookie("rkey"));
                        //OnlineUsers.AddUserLoginRecord(userInfo.Uid, DNTRequest.GetIP(), 2, 1, "密码登陆通过");

                        //原始
                        //#region 无延迟更新在线信息和相关用户信息
                        //ForumUtils.WriteUserCookie(userInfo.Uid, TypeConverter.StrToInt(DNTRequest.GetString("expires"), -1), config.Passwordkey, DNTRequest.GetInt("templateid", 0), DNTRequest.GetInt("loginmode", -1));
                        ////oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                        //oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout, userInfo.Uid, "");
                        //olid = oluserinfo.Olid;
                        //username = DNTRequest.GetString("username");
                        //userid = userInfo.Uid;
                        //usergroupinfo = UserGroups.GetUserGroupInfo(userInfo.Groupid);
                        //useradminid = usergroupinfo.Radminid; // 根据用户组得到相关联的管理组id


                        //OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0);
                        //LoginLogs.DeleteLoginLog(DNTRequest.GetIP());
                        //Users.UpdateUserCreditsAndVisit(userInfo.Uid, DNTRequest.GetIP());
                        //#endregion

                        //
                        //【END BT修改】
                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////


                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////
                        //【BT修改】增加 读取用户信息
                        //

                        if (userInfo.Uid > 0) btuserinfo = PTUsers.GetBtUserInfoForPagebase(userInfo.Uid);

                        //
                        //【END BT修改】
                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////

                        loginsubmit = false;
                        string reurl = Utils.UrlDecode(ForumUtils.GetReUrl());
                        SetUrl(reurl.IndexOf("register.aspx") < 0 ? reurl : forumpath + "index.aspx");

                        SetLeftMenuRefresh();

                        //同步登录到第三方应用
                        if (APIConfigs.GetConfig().Enable)
                            AddMsgLine(Sync.GetLoginScript(userid, username));

                        if (!APIConfigs.GetConfig().Enable || !Sync.NeedAsyncLogin())
                            MsgForward("login_succeed", true);
                    }
                }
                else
                {
                    int errcount = LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), true);
                    if (errcount > 5)
                        AddErrLine("您已经输入密码5次错误, 请15分钟后再试");
                    else
                        AddErrLine(string.Format("密码或安全提问第{0}次错误, 您最多有5次机会重试", errcount));
                }
                if (IsErr()) return;

                ForumUtils.WriteUserCreditsCookie(userInfo, usergroupinfo.Grouptitle);
            }
        }

        /// <summary>
        /// 设置BackLink
        /// </summary>
        private void SetBackLink()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string key in System.Web.HttpContext.Current.Request.QueryString.AllKeys)
            {
                //if (key != "postusername")
                if (!string.IsNullOrEmpty(key) && !Utils.InArray(key, "postusername"))
                    builder.AppendFormat("&{0}={1}", key, DNTRequest.GetQueryString(key));
            }
            question = DNTRequest.GetFormInt("question", 0);
            if (question > 0)
                builder.AppendFormat("&question={0}", question);
            base.SetBackLink("login.aspx?postusername=" + Utils.UrlEncode(DNTRequest.GetString("username")) + builder);
        }

        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <returns></returns>
        private ShortUserInfo GetShortUserInfo()
        {
            postpassword = !Utils.StrIsNullOrEmpty(loginauth) ?
                    DES.Decode(loginauth.Replace("[", "+"), config.Passwordkey) :
                    DNTRequest.GetString("password");

            postusername = Utils.StrIsNullOrEmpty(postusername) ? DNTRequest.GetString("username") : postusername;

            int uid = -1;
            //switch (config.Passwordmode)
            //{
            //    case 1://动网兼容模式
            //        {
            //            if (config.Secques == 1 && (!Utils.StrIsNullOrEmpty(loginauth) || !loginsubmit))
            //                uid = Users.CheckDvBbsPasswordAndSecques(postusername, postpassword, DNTRequest.GetFormInt("question", 0), DNTRequest.GetString("answer"));
            //            else
            //                uid = Users.CheckDvBbsPassword(postusername, postpassword);
            //            break;
            //        }
            //    case 0://默认模式
            //        {
            //            if (config.Secques == 1 && (!Utils.StrIsNullOrEmpty(loginauth) || !loginsubmit))
            //                uid = Users.CheckPasswordAndSecques(postusername, postpassword, true, DNTRequest.GetFormInt("question", 0), DNTRequest.GetString("answer"));
            //            else
            //                uid = Users.CheckPassword(postusername, postpassword, true);
            //            break;
            //        }
            //    default: //第三方加密验证模式
            //        {
            //            return (ShortUserInfo)Users.CheckThirdPartPassword(postusername, postpassword, DNTRequest.GetFormInt("question", 0), DNTRequest.GetString("answer"));
            //        }
            //}

            //新密码体系验证用户密码，暂时为原始密码
            if (config.Secques == 1 && (!Utils.StrIsNullOrEmpty(loginauth) || !loginsubmit))
                uid = PTUsers.CheckPasswordSHA512(postusername, postpassword, true, false, ForumUtils.GetUserSecques(DNTRequest.GetFormInt("question", 0), DNTRequest.GetString("answer")), true);
            else
                uid = PTUsers.CheckPasswordSHA512(postusername, postpassword, true, false, "", true);

            if (uid > 0)
                Users.UpdateTrendStat(TrendType.Login);
            return uid > 0 ? Users.GetShortUserInfo(uid) : null;
        }



        /// <summary>
        /// 设置reurl
        /// </summary>
        private void SetReUrl()
        {
            //未提交或跨页提交时
            if (!DNTRequest.IsPost() || referer != "")
            {
                string r = "";
                if (referer != "")
                    r = DNTRequest.GetUrlReferrer();
                else
                {
                    if ((DNTRequest.GetUrlReferrer() == "") || (DNTRequest.GetUrlReferrer().IndexOf("login") > -1) || DNTRequest.GetUrlReferrer().IndexOf("logout") > -1)
                        r = "index.aspx";
                    else
                        r = DNTRequest.GetUrlReferrer();
                }
                Utils.WriteCookie("reurl", (DNTRequest.GetQueryString("reurl") == "" || DNTRequest.GetQueryString("reurl").IndexOf("login.aspx") > -1) ? r : DNTRequest.GetQueryString("reurl"));
            }
        }

        private void APILogin(APIConfigInfo apiInfo)
        {
            ApplicationInfo appInfo = null;
            ApplicationInfoCollection appcollection = apiInfo.AppCollection;
            foreach (ApplicationInfo newapp in appcollection)
            {
                if (newapp.APIKey == DNTRequest.GetString("api_key"))
                    appInfo = newapp;
            }

            if (appInfo == null)
                return;

            this.Load += delegate
            {
                RedirectAPILogin(appInfo);
                this.Load += delegate { };
            };
        }


        private void RedirectAPILogin(ApplicationInfo appInfo)
        {
            string expires = DNTRequest.GetFormString("expires");
            DateTime expireUTCTime;
            if (Utils.StrIsNullOrEmpty(expires))
                expireUTCTime = DateTime.Parse(Users.GetShortUserInfo(userid).Lastvisit).ToUniversalTime().AddSeconds(
                    Convert.ToDouble(Request.Cookies["dnt"]["expires"].ToString()));
            else
                expireUTCTime = DateTime.UtcNow.AddSeconds(Convert.ToDouble(expires));

            expires = Utils.ConvertToUnixTimestamp(expireUTCTime).ToString();

            //CreateToken
            OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0);
            string next = DNTRequest.GetString("next");
            string time = "";
            OnlineUserInfo oui = OnlineUsers.GetOnlineUser(olid);
            if (oui == null)
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            else
                time = DateTime.Parse(oui.Lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss");

            string authToken = DES.Encode(string.Format("{0},{1},{2}", olid, time, expires), appInfo.Secret.Substring(0, 10)).Replace("+", "[");
            Response.Redirect(string.Format("{0}{1}auth_token={2}{3}", appInfo.CallbackUrl, appInfo.CallbackUrl.IndexOf("?") > 0 ? "&" : "?", authToken, next == "" ? next : "&next=" + next));
        }

        private void SetLeftMenuRefresh()
        {
            SetMetaRefresh();
            SetShowBackLink(false);
            AddScript("if (top.document.getElementById('leftmenu')){top.frames['leftmenu'].location.reload();}");
        }
    }
}