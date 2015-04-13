using System;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

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
    public class login : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 登录所使用的用户名
        /// </summary>
        public string postusername = Utils.HtmlEncode(DNTRequest.GetString("postusername")).Trim();
        /// <summary>
        /// 登陆时的密码验证信息，从其他页面带来的密码信息，经过DES加密的
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
        /// 是否跨页面提交（即从其他页面登陆）
        /// </summary>
        public bool loginsubmit = DNTRequest.GetString("loginsubmit") == "true" ? true : false;
        /// <summary>
        /// 重设Email的加密校验，确保是该用户在当前页面操作的
        /// </summary>
        public string authstr = "";
        /// <summary>
        /// 需要激活的用户id
        /// </summary>
        public int needactiveuid = 0;
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

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //【BT修改】
        
        /// <summary>
        /// 给ihome返回用户名所用的加密密钥
        /// </summary>
        public string EncryptionKeyA = "1E968607E47C5BB91E20CE9A5C4CCD81";
        /// <summary>
        /// 普通登陆令牌（通过iHome登陆）
        /// </summary>
        public string ssoToken = DNTRequest.GetString("token").Trim();
        /// <summary>
        /// 创建登陆令牌（按正常模式密码登陆账号，然后绑定信息）
        /// </summary>
        public string ssoCreateToken = DNTRequest.GetString("ctoken").Trim();
        /// <summary>
        /// ihome用户名
        /// </summary>
        public string ssoName = "";
        /// <summary>
        /// ihome用户uid
        /// </summary>
        public int ssoUid = -1;

        public string ssoUrl = "http://211.71.14.214/buaasso.php";
        
        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        protected override void ShowPage()
        {
            pagetitle = "用户登录";
            inapi = DNTRequest.GetInt("inapi", 0);
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
                AddMsgLine("您已经登录，无须重复登录");
                ispost = true;
                SetLeftMenuRefresh();

                if (APIConfigs.GetConfig().Enable)
                    APILogin(APIConfigs.GetConfig());
                return;
            }

            //此处返回不影响登陆页面的展现
            if (LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), false) >= 5)
            {
                AddErrLine("您已经多次输入密码错误, 请20分钟后再登录");
                loginsubmit = false;
                return;
            }

            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //【BT修改】 处理buaasso登陆的情况

            if (DNTRequest.GetInt("gettoken", -1) == 1)
            {
                Response.Clear();
                Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                Response.Write("<token>");
                Response.Write("abcdef");
                Response.Write("</token>");
                //Response.Flush();
                PageBase_ResponseFlushEnd();
                return;
            }

            //通过SSO登陆的用户ID，-1为未登陆或登陆错误，必须>2
            int ssoLoginId = 0;
            //用来反馈给ihome成功注册或绑定的用户名的确认令牌，此令牌将从数据库token1中读取，之前ihome发送过来的ctoken解密结果。
            string ssoToken1 = "";


            #region IHOME SSO 跳转登录检测 仅在用户未登录且不是复活提交的时候检测

            if (userid < 1 && -(DNTRequest.GetInt("reactiveuid", 1)) < 0)
            {
                Regex Regex64HEX = new Regex(@"^[0-9a-fA-F]{64}$");

                if (ssoToken != "")
                {
                    //普通登陆
                    
                    //格式确认，未来更改为正则验证
                    if (Regex64HEX.IsMatch(ssoToken))
                    {
                        PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyToken(ssoToken);//【*数据库读*】【时间确认更改点】
                        ssoName = ssoinfo.ssoName;
                        ssoUid = ssoinfo.ssoUid;
                        if (ssoinfo.Uid < 3 || (ssoinfo.ssoStatus != 2 && ssoinfo.ssoStatus != 3) || ssoinfo.Uid < 2 || (DateTime.Now - ssoinfo.TokenDate).TotalSeconds > 60 || ssoinfo.TokenStatus != 2 || ssoinfo.ssoName.Trim() == "")
                        {
                            //禁止使用此方式登陆，或者无此token，或者token时间超过30秒，则直接跳转到登陆页面
                            PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyUid(ssoinfo.Uid, -1);//【*数据库写*】
                            ssoLoginId = -1;
                            referer = "login.aspx";
                            AddErrLine("重复访问 或 链接访问超时 或 错误的链接访问E1，请从 i北航 重新点击跳转链接");
                            PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOLogin, PTLog.LogStatus.Error, "SSO_ERROR LERR01", string.Format("TOKEN失效 --UID:{0}--SSOSTATUS:{1}--TOKENDATE:{2}", ssoinfo.Uid, ssoinfo.ssoStatus, ssoinfo.TokenDate));
                            ispost = true; loginsubmit = false;
                            ssoToken = "";
                            return;
                        }
                        else
                        {
                            pagetitle = "用户登陆  通过已授权 i北航(i.buaa.edu.cn)";
                            //重新设置token，设置tokenstatus为2，跳转到登陆页面，
                            PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyUid(ssoinfo.Uid, -1);//【*数据库写*】
                            ssoLoginId = ssoinfo.Uid;
                            ispost = true; loginsubmit = false;
                            referer = "forumindex.aspx";
                        }
                    }
                    else
                    {
                        ssoLoginId = -1;
                        referer = "login.aspx";
                        AddErrLine("错误的链接访问E2，请从 i北航 重新点击跳转链接");
                        PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOLogin, PTLog.LogStatus.Error, "SSO_ERROR LERR02", string.Format("TOKEN格式错误{0}", ssoToken));
                        ispost = true; loginsubmit = false;
                        ssoToken = "";
                        return;
                    }
                }
                else if (ssoCreateToken != "")
                {
                    if (Regex64HEX.IsMatch(ssoCreateToken))
                    {
                        //创建登陆，输入密码登陆成功后需要发送反馈信息

                        PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyToken(ssoCreateToken);//【*数据库读*】【时间确认更改点】
                        ssoToken1 = ssoinfo.Token1;
                        ssoName = ssoinfo.ssoName;
                        ssoUid = ssoinfo.ssoUid;

                        //正常绑定状态，uid<0
                        if ((ssoinfo.ssoStatus != 2 && ssoinfo.ssoStatus != 3 && ssoinfo.ssoStatus != 1) || (DateTime.Now - ssoinfo.TokenDate).TotalSeconds > 300 || ssoinfo.TokenStatus != 2 || ssoinfo.ssoName.Trim() == "")
                        {
                            //禁止使用此方式登陆，或者无此token，或者token时间超过30秒，则直接跳转到登陆页面
                            PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOLogin, PTLog.LogStatus.Error, "SSO_ERROR LREG-0", string.Format("TOKEN无效 --UID:{0}--SSOUID:{1}--SSONAME:{2}--SSOSTATUS:{3}--SSOTOEKN:{4}", ssoinfo.Uid, ssoinfo.ssoUid, ssoinfo.ssoName, ssoinfo.ssoStatus, ssoCreateToken));
                            ssoLoginId = -1;
                            referer = "login.aspx";
                            AddErrLine("等候超时 或 错误的链接访问E3，请从i北航重新点击跳转链接重试，并尽快完成填写提交！（限时5分钟）");
                            ispost = true; loginsubmit = false;
                            ssoCreateToken = "";
                            return;
                        }
                        else
                        {
                            pagetitle = "用户登陆 验证密码以授权 i北航(i.buaa.edu.cn) 用户可以直接登陆，或选择直接注册新账号";
                            if ((ssoinfo.ssoStatus == 2 || ssoinfo.ssoStatus == 3) && ssoinfo.Uid > 2)
                            {
                                //已经绑定成功的情况
                                PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyssoUid(ssoUid, -1);//【*数据库写*】
                                PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOLogin, PTLog.LogStatus.Error, "SSO_ERROR LREG-2", string.Format("重复绑定 --UID:{0}--SSOUID:{1}--SSONAME:{2}--SSOSTATUS:{3}--SSOTOEKN:{4}", ssoinfo.Uid, ssoinfo.ssoUid, ssoinfo.ssoName, ssoinfo.ssoStatus, ssoCreateToken));
                                ssoLoginId = ssoinfo.Uid;
                                ispost = true; loginsubmit = false;
                                referer = "forumindex.aspx";
                            }
                            else
                            {
                                //尚未未绑定，等待用户输入密码绑定
                                ssoLoginId = -1;
                            }
                            referer = "forumindex.aspx";
                        }
                    }
                    else
                    {
                        ssoLoginId = -1;
                        referer = "login.aspx";
                        AddErrLine("错误的链接访问E4，请从 i北航 重新点击跳转链接！");
                        ispost = true; loginsubmit = false;
                        PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOLogin, PTLog.LogStatus.Error, "SSO_ERROR LREG-3", string.Format("TOKEN无效 --SSOTOEKN:{0}--IP:{1}", ssoCreateToken, DNTRequest.GetIP()));
                        ssoCreateToken = "";
                        return;
                    }
                }

            }

            #endregion

            //得到广告列表
            headerad = Advertisements.GetOneHeaderAd("loginad", 0);
            footerad = Advertisements.GetOneFooterAd("loginad", 0);

            SetReUrl();

            //如果提交...
            if (DNTRequest.IsPost() || ssoLoginId > 1)//【BT修改】
            {
                SetBackLink();

                #region 复活用户（因新手任务未完成导致的禁封）

                if (-(DNTRequest.GetInt("reactiveuid", 1)) > 0)
                {
                    //SetUrl("login.aspx");

                    int re_uid = -(DNTRequest.GetInt("reactiveuid", 1));
                    string re_code = DNTRequest.GetString("invitecode");

                    //前置合理性验证和安全验证
                    if (re_code == null || re_code.Length != 32)
                    {
                        AddErrLine("邀请码输入不正确");
                        return;
                    }
                    int errcount = LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), false);
                    if (errcount >= 5)
                    {
                        AddErrLine("您由于连续输入错误, IP地址已被锁定，请等候20分钟后再试");
                        return;
                    }

                    //验证页面是否有效
                    if (PTUsers.VerifyUserReActiveVFCode(re_uid, 0) < 1)
                    {
                        AddErrLine("页面已失效，请重新登录");
                        errcount = LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), true);
                        if (errcount >= 5)
                            AddErrLine("您由于连续输入错误, IP地址已被锁定，请等候20分钟后再试");
                        return;
                    }

                    //验证邀请码是否有效、用户是否需要复活
                    if (PrivateBTInvitation.VerifyInviteCode(re_code) < 1)
                    {
                        AddErrLine("邀请码不存在！请重新检查邀请码是否复制完整");
                        return;
                    }
                    int reginviterid = PrivateBTInvitation.GetInviteCodeBuyer(re_code);
                    if (PTUsers.VerifyUserReActiveVFCode(re_uid, reginviterid) < 1)
                    {
                        AddErrLine("复活用户发生错误！");
                        return;
                    }
                    UserInfo tmpuser = Users.GetUserInfo(re_uid);
                    if (tmpuser == null || tmpuser.VIP != -1 || tmpuser.Groupid != 5)
                    {
                        AddErrLine("复活用户发生错误！用户不存在或状态不符，请重新登录");
                        return;
                    }

                    //复活用户
                    if (PTUsers.ReActiveUser(tmpuser.Uid) > 0)
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.ReActiveUser, PTLog.LogStatus.Normal, "ReActiveUser", string.Format("{0}  -USERNAME:{1} -INVITEER:{2} -CODE:{3}", tmpuser.Uid, tmpuser.Username, reginviterid, re_code));
                        
                        AddMsgLine("复活用户成功，请重新登录");

                        PrivateBTInvitation.UseInviteCode(re_code, -tmpuser.Uid);

                        //赠送20G上传流量并发送短消息通知

                        Users.UpdateUserExtCredits(tmpuser.Uid, 3, 20 * 1024 * 1024 * 1024f);
                        CreditsLogs.AddCreditsLog(tmpuser.Uid, 0, 3, 3, 0, 20 * 1024 * 1024 * 1024f, Utils.GetDateTime(), 17);

                        //邀请人通知
                        PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                        string curdatetime = Utils.GetDateTime();
                        privatemessageinfo.Message = "您的邀请码已被用户 <a href = \"userinfo-" + tmpuser.Uid + ".aspx\">" + tmpuser.Username + "</a> 用于复活帐号";
                        privatemessageinfo.Subject = "邀请码已被用于复活帐号";
                        privatemessageinfo.Msgfrom = "系统";
                        privatemessageinfo.Msgfromid = 0;
                        privatemessageinfo.New = 1;
                        privatemessageinfo.Postdatetime = curdatetime;
                        privatemessageinfo.Folder = 0;
                        privatemessageinfo.Msgtoid = reginviterid;
                        privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                        if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                        //复活用户本人通知
                        privatemessageinfo.Message = "用户复活成功！已赠送20G上传流量，请努力保持状态！并在未来60天内继续完成新手任务";
                        privatemessageinfo.Subject = "用户复活成功！已赠送20G上传流量，请努力保持状态！并在未来60天内继续完成新手任务";
                        privatemessageinfo.Msgtoid = tmpuser.Uid;
                        privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                        if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                        UserCredits.UpdateUserCreditsBackground(tmpuser.Uid);

                        AddMsgLine("已赠送20G上传流量，请努力保持帐号状态！");
                        SetLeftMenuRefresh();
                    }
                    else
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.ReActiveUser, PTLog.LogStatus.Normal, "ReActiveUserF", string.Format("FAILED: -UID:{0}  -USERNAME:{1} -INVITEER:{2} -CODE:{3}", tmpuser.Uid, tmpuser.Username, reginviterid, re_code));
                        AddErrLine("复活用户失败，请重新检查用户状态，如果存在问题请联系管理员");
                        return;
                    }
                    
                    return;
                }
                else if (DNTRequest.GetString("action") == "reactive")
                {
                    //SetUrl("login.aspx");

                    int re_uid = -(DNTRequest.GetInt("reactiveuid", 1));
                    string re_code = DNTRequest.GetString("invitecode");
                    string re_user = DNTRequest.GetString("user");
                    AddErrLine("复活用户请求提交信息缺失，请确认浏览器为IE、Firefox 或 Chrome 的正式版并确保Cookies启用，请勿使用各种加壳修改版浏览器");
                    PTLog.InsertSystemLog(PTLog.LogType.ReActiveUser, PTLog.LogStatus.Error, "ReActiveUser", string.Format("{0}  -REUID:{1} -REUSER:{2} -RECODE:{3}", ipaddress, re_uid, re_user, re_code));
                }

                #endregion

                #region 正常的用户登录验证用户名和密码，如果ssologin，则跳过此部分

                if (ssoLoginId < 2)
                {
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
                        if (DNTRequest.GetString("username") != "admin_max")
                        if ((Users.GetUserId(DNTRequest.GetString("username")) == 0))
                            AddErrLine("用户不存在");
                    }

                    if (Utils.StrIsNullOrEmpty(DNTRequest.GetString("password")) && Utils.StrIsNullOrEmpty(DNTRequest.GetString("loginauth")))
                        AddErrLine("密码不能为空");

                    if (IsErr()) return;
                }

                #endregion

                //登录完毕，获取用户信息
                ShortUserInfo userInfo = (ssoLoginId < 2) ? GetShortUserInfo() : Users.GetShortUserInfo(ssoLoginId);

                if (userInfo != null)
                {
                    #region 当前用户所在用户组为"禁止访问"或"等待激活"时，显示禁止访问页面或复活页面

                    // 5-禁止访问
                    if (userInfo.Groupid == 5)
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.ReActiveUser, PTLog.LogStatus.Normal, "BANED SSO", string.Format("BANED: {0}  -USERNAME:{1} -VIP:{2} -GROUPID:{3}", userInfo.Uid, userInfo.Username, userInfo.VIP, userInfo.Groupid));

                        if (userInfo.VIP == -1)
                        {
                            AddErrLine("对不起，此用户因为未能完成新手任务已经被禁封，不能访问本站。 <br/><br/>您可以通过一下方式继续使用本站<br/>" +
                            "① 向在站内的同学索要邀请码，通过下方复活选项，复活当前用户。 <br/>② 通过其他方式重新注册新用户 " +
                            "<br/><br/>本站注册方式有  1. 向同学所要邀请码注册。  2. 通过 i北航 实名认证免邀请注册。  3. 通过CNGI验证校园卡账号密码实名认证注册。 <br/>" +
                            "请注意：所有免邀请注册方式均只能使用一次，使用后用户和相应的实名认证信息绑定，绑定后不能解除绑定。");
                            needactiveuid = -userInfo.Uid;
                            postusername = userInfo.Username;
                            PTUsers.CreateUserReActiveVFCode(userInfo.Uid);
                            //ispost = true;
                            //loginsubmit = false;
                            return;
                        }
                        else
                        {
                            AddErrLine("对不起，此用户已被禁封，不能访问本站");
                            ispost = true;
                            loginsubmit = false;
                            return;
                        }
                    }
                    
                    //D//PTLog.InsertSystemLogDebug(PTLog.LogType.ReActiveUser, PTLog.LogStatus.Normal, "LOGIN OK", string.Format("UID:{0}  -USERNAME:{1} -VIP:{2} -GROUPID:{3} -SSOUID:{4}", userInfo.Uid, userInfo.Username, userInfo.VIP, userInfo.Groupid, ssoLoginId));

                    //等待验证用户
                    if (userInfo.Groupid == 8)
                    {
                        if (config.Regverify == 1)
                        {
                            //重新发送验证连接
                            UserInfo userinfofull = Users.GetUserInfo(userInfo.Uid);
                            if (Emails.DiscuzSmtpMail(userinfofull.Username, userinfofull.Email, "NULL", userinfofull.Authstr))
                            {
                                AddMsgLine("系统已经再次向您的注册邮箱 " + userinfofull.Email + " 中发送验证链接<br/><br/>");
                            }
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

                    //此条原用于禁止访问或等待激活用户组用户，超期后自动解除禁止访问状态？？？似乎没什么用处
                    if ((userInfo.Groupid == 4 || userInfo.Groupid == 5) && userInfo.Groupexpiry != 0 && userInfo.Groupexpiry <= Utils.StrToInt(DateTime.Now.ToString("yyyyMMdd"), 0))
                    {
                        //根据当前用户的积分获取对应积分用户组
                        UserGroupInfo groupInfo = UserCredits.GetCreditsUserGroupId(userInfo.Credits);
                        usergroupid = groupInfo.Groupid != 0 ? groupInfo.Groupid : usergroupid;
                        PTLog.InsertSystemLogDebug(PTLog.LogType.ReActiveUser, PTLog.LogStatus.Normal, "UpdateGroupId", string.Format("WWWW UID:{0} -USERNAME:{1} -OLDGROUPID:{2} -NEWGROUPID:{3}", userInfo.Uid, userInfo.Username, userInfo.Groupid, usergroupid));
                        userInfo.Groupid = usergroupid;
                        Users.UpdateUserGroup(userInfo.Uid, usergroupid);      
                    }


                    //跨页面提交（从页面上方的快速登陆登陆，花园无此功能），但是用户启用了密码验证问题，无法填写，因此跳转到登陆页面
                    if (!Utils.StrIsNullOrEmpty(userInfo.Secques) && loginsubmit && Utils.StrIsNullOrEmpty(DNTRequest.GetString("loginauth")))
                    {
                        loginauth = DES.Encode(DNTRequest.GetString("password"), config.Passwordkey).Replace("+", "[");
                    }
                    else
                    {
                        //登陆成功 删除当前游客的olineuser info 写新cookie之前先清空老cookie
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

                        ForumUtils.ClearUserCookie();
                        System.Web.HttpContext.Current.Response.Cookies.Clear();

                        //添加随机RKey，HTTPONLY
                        string rkey = PTUsers.SetUserRKey(userInfo.Uid);
                        PTUsers.UpdateCookiePassword(userInfo.Uid);

                        //D//PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Normal, "Login_NewRKey", string.Format("UID:{0} -IP:{1} -NewRKEY:{2}", userInfo.Uid, ipaddress, rkey));

                        #region BUAASSO的验证登陆 登录成功，绑定信息，通知iHome

                        if (ssoCreateToken != "")
                        {
                            //之前未绑定的情况，绑定信息
                            if (ssoLoginId < 3 && ssoName != "" && ssoUid > 0)
                            {
                                PTBuaaSSO.UpdateBuaaSSOInfoUidbyssoUid(userInfo.Uid, ssoUid);
                                PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSOLink, PTLog.LogStatus.Normal, "LINK SUCCESS2", userInfo.Uid.ToString());
                            }
                            PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyssoUid(ssoUid, -1);//【*数据库写*】

                            //记录登录成功
                            PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSOLogin, PTLog.LogStatus.Normal, "LOGIN SUCCESS", userInfo.Uid.ToString());
                            
                            //返回信息
                            string confirmstr = PTCommon.GetRemoteResponse(string.Format("{2}?m=createok&key={0}&token={1}", AES.Encode2HEX("<@" + userInfo.Username.PadRight(20, ' ') + "@>", EncryptionKeyA), ssoToken1, ssoUrl)).Trim().ToLower();
                            if (confirmstr == "ok")
                            {
                                AddMsgLine("成功授权您通过 i北航(i.buaa.edu.cn) 直接访问");
                                PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSOLink, PTLog.LogStatus.Normal, "LINK FEEDBACK OK2", userInfo.Uid.ToString());
                            }
                            else if (confirmstr == "failure")
                            {
                                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOLogin, PTLog.LogStatus.Error, "SSO_ERROR LREG-101", string.Format("FAILURE:发送绑定信息失败 --UID:{0}--SSOUID:{1}--SSONAME:{2}", userInfo.Uid, ssoUid, ssoName));
                                AddMsgLine("与 i北航(i.buaa.edu.cn) 通信发生错误");
                            }
                            else
                            {
                                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOLogin, PTLog.LogStatus.Error, "SSO_ERROR LREG-102", string.Format("FAILURE:发送绑定信息失败 --UID:{0}--SSOUID:{1}--SSONAME:{2}", userInfo.Uid, ssoUid, ssoName));
                                AddMsgLine("与 i北航(i.buaa.edu.cn) 通信失败");
                            }
                        }

                        #endregion


                        //通过api整合的程序登录
                        if (APIConfigs.GetConfig().Enable)
                            APILogin(APIConfigs.GetConfig());


                        AddMsgLine("登录成功, 返回登录前页面");

                        ForumUtils.WriteUserCookie(userInfo.Uid, TypeConverter.StrToInt(DNTRequest.GetString("expires"), -1), config.Passwordkey, DNTRequest.GetInt("templateid", 0), DNTRequest.GetInt("loginmode", -1));

                        //无延迟更新在线信息和相关用户信息
                        // 由于修改过OnlineUsers.UpdateInfo，下面的语句不能加，否则在执行时，由于rkey不符，将重置用户为游客。
                        // 如果使用原始函数不传递md5，则仍为游客，毫无意义
                        //oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                        // 
                        //oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout, userInfo.Uid, passwordmd5);
                        //olid = oluserinfo.Olid;
                        //username = DNTRequest.GetString("username");
                        //userid = userInfo.Uid;
                        //usergroupinfo = UserGroups.GetUserGroupInfo(userInfo.Groupid);
                        //useradminid = usergroupinfo.Radminid; // 根据用户组得到相关联的管理组id
                        //OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0);

                        //安全增强：此处“登录错误重置”功能，修改为只删除错误计数为0的条目，防止绕过15分钟限制
                        LoginLogs.DeleteLoginLog(DNTRequest.GetIP());
                        Users.UpdateUserCreditsAndVisit(userInfo.Uid, DNTRequest.GetIP());

                        //////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////
                        //【BT修改】读取用户信息
                        //

                        if (userInfo.Uid > 0) btuserinfo = PTUsers.GetBtUserInfoForPagebase(userInfo.Uid);

                        loginsubmit = false;
                        string reurl = Utils.UrlDecode(ForumUtils.GetReUrl());
                        
                        //if (ssoCreateToken != "" || ssoToken != "")
                        //{
                        //    reurl = "forumindex.aspx?s=login";
                        //}
                        reurl = "index.aspx?s=login";

                        SetUrl(reurl.IndexOf("register.aspx") < 0 ? reurl : forumpath + "index.aspx?s=login");


                        if (ssoLoginId < 2)
                        {
                            OnlineUsers.AddUserLoginRecord(userInfo.Uid, postusername, userInfo.Groupid, DNTRequest.GetIP(), 21, 1, passwordmd5, Utils.GetCookie("rkey"));
                            //OnlineUsers.AddUserLoginRecord(userInfo.Uid, DNTRequest.GetIP(), 2, 1, "密码登陆通过");
                        }
                        else
                        {
                            OnlineUsers.AddUserLoginRecord(userInfo.Uid, postusername, userInfo.Groupid, DNTRequest.GetIP(), 15, 1, passwordmd5, Utils.GetCookie("rkey"));
                            AddMsgLine("通过已授权 i北航(i.buaa.edu.cn) 登陆成功");
                            ispost = true;
                        }

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
                    //登陆错误，如果用户名存在，则记录所登陆用户名对应的uid和groupid，否则只记录登录名
                    UserInfo tryuserinfo = Users.GetUserInfo(postusername);
                    string passwordmd5 = Utils.MD5(postpassword);

                    if(tryuserinfo != null)
                        OnlineUsers.AddUserLoginRecord(tryuserinfo.Uid, postusername, tryuserinfo.Groupid, DNTRequest.GetIP(), 21, 2, passwordmd5, Utils.GetCookie("rkey"));
                    else
                        OnlineUsers.AddUserLoginRecord(-1, postusername, -1, DNTRequest.GetIP(), 21, 2, passwordmd5, Utils.GetCookie("rkey"));
                    
                    //连续错误处理
                    int errcount = LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), true);
                    if (errcount >= 5)
                        AddErrLine("您已经输入密码5次错误, IP地址已被锁定，请等候20分钟后再试");
                    else
                        AddErrLine(string.Format("密码或安全提问第{0}次错误, 您最多有5次机会重试。 <br/><br/>如果没有设置安全提问，安全提问项目请不要选择任何问题！<br/>如果设置了安全提问，必须正确选择提问并填写答案。 <br/>安全提问是最高等级的安全选项，遗忘意味着失去帐号", errcount));
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
            if (DNTRequest.GetString("action") == "reactive")
            {
                base.SetBackLink("login.aspx");
            }
            else
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

            postusername = Utils.StrIsNullOrEmpty(DNTRequest.GetString("username")) ? postusername : DNTRequest.GetString("username");

            //////////////////////////////////////////////////////////////////////////
            //临时安全措施，admin_max -> admin，admin -> admin_danger
            if (postusername == "admin")
            {
                OnlineUsers.AddUserLoginRecord(1, "admin", 1, DNTRequest.GetIP(), 21, 2, postpassword, Utils.GetCookie("rkey"));
                return null;
            }
            else if (postusername == "admin_max")
            {
                postusername = "admin";
            }

            int uid = -1;

            #region 密码模式（无用，已注释）
            
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

            #endregion

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
                if (DNTRequest.GetString("action") == "reactive")
                    r = "login.aspx";
                else if (referer != "")
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