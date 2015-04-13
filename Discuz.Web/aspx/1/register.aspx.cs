using System;
using System.Data;
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
    /// 注册页
    /// </summary>
    public class register : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 可用的模板列表
        /// </summary>
        public DataTable templatelist = Templates.GetValidTemplateList();
        /// <summary>
        /// 此变量等于1时创建用户,否则显示填写用户信息界面
        /// </summary>
        public string createuser = DNTRequest.GetString("createuser");
        /// <summary>
        /// 是否同意注册协议
        /// </summary>
        public string agree = (GeneralConfigs.GetConfig().Rules == 0 ? "true" : DNTRequest.GetFormString("agree"));
        /// <summary>
        /// 邀请码
        /// </summary>
        public string invitecode = DNTRequest.GetString("invitecode");
        public bool allowinvite = false;
        #endregion

        /// <summary>
        /// 是否需要邀请码来完成注册
        /// </summary>
        public bool needinvitecode = true;
        /// <summary>
        /// 是否允许用户注册（包括邀请注册、CNGI、iHome或自由注册）
        /// </summary>
        public bool allowregister = true;

        /// <summary>
        /// 给ihome返回用户名所用的加密密钥
        /// </summary>
        public string EncryptionKeyA = "1E968607E47C5BB91E20CE9A5C4CCD81";
        /// <summary>
        /// 创建新用户令牌，此令牌由BT服务器发放给用户（通过buaasso.aspx页面）
        /// </summary>
        public string ssoCreateToken = DNTRequest.GetString("ctoken").Trim();
        /// <summary>
        /// ihome用户名（此处实际为uid字符串）
        /// </summary>
        public string ssoName = "";
        /// <summary>
        /// ihome用户uid
        /// </summary>
        public int ssoUid = -1;
        /// <summary>
        /// 默认创建用户的邀请码
        /// </summary>
        public string DEFAULT_invitecode = "";
        /// <summary>
        /// 默认创建用户的用户名
        /// </summary>
        public string DEFAULT_username = "";
        /// <summary>
        /// 默认创建用户的用户密码
        /// </summary>
        public string DEFAULT_password = "";
        /// <summary>
        /// 默认创建用户的email
        /// </summary>
        public string DEFAULT_email = "";
        /// <summary>
        /// 注册模式，0开放注册，-1 i北航，-2 cngi，大于零，邀请注册
        /// </summary>
        public int reginviterid = 0;
        /// <summary>
        /// iHome SSO链接
        /// </summary>
        public string ssoUrl = "http://211.71.14.214/buaasso.php";


        protected override void ShowPage()
        {
            pagetitle = "用户注册";

            #region 当前有用户登录，则直接跳转登录

            if (userid > 0)
            {
                SetUrl(BaseConfigs.GetForumPath);
                SetMetaRefresh();
                SetShowBackLink(false);
                AddMsgLine("您已登录，无需再次注册或登陆");
                ispost = true;
                createuser = "1";
                agree = "yes";
                return;
            }

            #endregion

            #region 判断是否可以开放注册/邀请注册

            PrivateBTConfigInfo btconfig = PrivateBTConfig.GetPrivateBTConfig();
            //是否需要邀请码
            needinvitecode = !(btconfig.AllowFreeRegister && DateTime.Now > btconfig.FreeRegBeginTime && DateTime.Now < btconfig.FreeRegEndTime);
            //是否允许自由或邀请注册
            allowregister = (btconfig.AllowFreeRegister || btconfig.AllowInviteRegister) && DateTime.Now > btconfig.FreeRegBeginTime && DateTime.Now < btconfig.FreeRegEndTime;
            //总人数是否满足要求
            if (Stats.GetMemberCount() >= btconfig.TotalUserLimit && allowregister) allowregister = false;

            #endregion

            #region CNGI登录状态检测

            if (cngi_user && !cngi_login)
            {
                SetUrl("http://sp-bbs.buaa6.edu.cn/Shibboleth.sso/DS?target=http%3A%2F%2Fsp-bbs.buaa6.edu.cn%2Fcngilogin.aspx");
                SetMetaRefresh(3);
                SetShowBackLink(false);
                AddMsgLine("请先登陆CNGI高校BBS联盟认证！正在为您自动跳转");
                createuser = "1";
                agree = "yes";
                ispost = true;
            }
            if (cngi_user)
            {
                if (reginviterid == 0) reginviterid = -2;
                else reginviterid = -100;
            }
            if (cngi_login)
            {
                if (cngi_name == "" || cngi_name == null || cngi_school == "" || cngi_school == null)
                {
                    AddErrLine("未能通过CNGI08获取您的身份信息，请重新登陆CNGI08重试，如多次重试仍无法注册，请向system@buaabt.cn反馈，并包含您的学校和CNGI用户名");
                    //PTError.InsertErrorLog("CNGI ERROR -3", string.Format("CNGI_NAME-{0}, CNGI_SCHOOL-{1}", cngi_name == null ? "<NULL>" : cngi_name, cngi_school == null ? "<NULL>" : cngi_school));
                    return;
                }
                //不能重复绑定
                if (PrivateBT.GetCNGIUserID(cngi_name, cngi_school) > 0)
                {
                    SetUrl("http://sp-bbs.buaa6.edu.cn/cngilogin.aspx");
                    SetMetaRefresh(3);
                    SetShowBackLink(false);
                    AddErrLine("您已经绑定过CNGI08身份认证信息，不能再次绑定，正在为您登陆已绑定账号");
                    //PTError.InsertErrorLog("CNGI ERROR -2", string.Format("CNGI_NAME-{0}, CNGI_SCHOOL-{1}", cngi_name == null ? "<NULL>" : cngi_name, cngi_school == null ? "<NULL>" : cngi_school));
                    return;
                }
                allowregister = true;
                needinvitecode = false;
            }

            #endregion

            #region iHome SSO状态检测，若sso创建用户验证成功，则ssoUid为大于零，此为判断标准

            //iHome的uid
            int ssoLoginId = 0;
            //用来反馈给ihome成功注册或绑定的用户名的确认令牌，此令牌将从数据库token1中读取，之前ihome发送过来的ctoken解密结果。
            string ssoToken1 = "";  

            //仅在用户未登录的时候检测
            if (userid < 1)
            {
                if (ssoCreateToken != "")
                {
                    //创建登陆，输入密码登陆成功后需要发送反馈信息

                    //格式确认，未来更改为正则验证
                    Regex Regex64HEX = new Regex(@"^[0-9a-fA-F]{64}$");

                    if (Regex64HEX.IsMatch(ssoCreateToken))
                    {
                        PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyToken(ssoCreateToken);//【*数据库读*】【时间确认更改点】
                        if (ssoinfo.ssoUid < 1 || (ssoinfo.ssoStatus != 2 && ssoinfo.ssoStatus != 3 && ssoinfo.ssoStatus != 1) || (DateTime.Now - ssoinfo.TokenDate).TotalSeconds > 300 || ssoinfo.TokenStatus != 2 || ssoinfo.ssoName.Trim() == "")
                        {
                            //禁止使用此方式登陆，或者无此token，或者token时间超过300秒，则直接跳转到登陆页面
                            PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO_ERROR LERR01", string.Format("TOKEN失效 --UID:{0}--SSOSTATUS:{1}--TOKENDATE:{2}", ssoinfo.Uid, ssoinfo.ssoStatus, ssoinfo.TokenDate));
                            ssoLoginId = -1;
                            ssoCreateToken = "";
                            AddErrLine("重复提交 或 等候超时 或 错误的链接访问 ER3，请从i北航重新点击跳转链接，并尽快完成填写提交！（限时5分钟）");
                        }
                        else
                        {
                            if ((ssoinfo.ssoStatus == 2 || ssoinfo.ssoStatus == 3) && ssoinfo.Uid > 2)
                            {
                                //已经绑定成功的情况，重新发送绑定信息，然后跳转登陆，不再继续执行
                                string confirmstr = PTCommon.GetRemoteResponse(string.Format("{2}?m=createok&key={0}&token={1}", AES.Encode2HEX("<@" + Users.GetUserName(ssoinfo.Uid).PadRight(20, ' ') + "@>", EncryptionKeyA), ssoinfo.Token1, ssoUrl)).Trim().ToLower();
                                if (confirmstr == "ok")
                                {
                                    PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSOLink, PTLog.LogStatus.Normal, "LINK PASSTH OK ", ssoinfo.Uid.ToString());
                                    AddMsgLine("您已经成功绑定未来花园BT账号，正在为您自动跳转登陆，请稍后");
                                    PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyssoUid(ssoinfo.ssoUid, 1);//将token重新置为有效状态【*数据库写*】
                                    SetUrl("http://buaabt.cn/buaasso.aspx?m=login&token=" + ssoinfo.Token);
                                    SetMetaRefresh(5);
                                    SetShowBackLink(false);
                                }
                                else if (confirmstr == "failure")
                                {
                                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO_ERROR REG-1", string.Format("FAILURE:重新发送绑定信息失败 --UID:{0}--SSOUID:{1}--SSONAME:{2}--SSOSTATUS:{3}--SSOTOEKN1:{4}", ssoinfo.Uid, ssoinfo.ssoUid, ssoinfo.ssoName, ssoinfo.ssoStatus, ssoinfo.Token1));
                                    AddErrLine("与 i北航(i.buaa.edu.cn) 通信发生错误，请重试");
                                }
                                else
                                {
                                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO_ERROR REG-2", string.Format("FAILURE:重新发送绑定信息失败 --UID:{0}--SSOUID:{1}--SSONAME:{2}--SSOSTATUS:{3}--SSOTOEKN1:{4}--FEEDBACK:{5}", ssoinfo.Uid, ssoinfo.ssoUid, ssoinfo.ssoName, ssoinfo.ssoStatus, ssoinfo.Token1, confirmstr));
                                    AddErrLine("与 i北航(i.buaa.edu.cn) 通信失败，请重试，错误信息：" + confirmstr);
                                }

                                //临时登录解决方案=================================================
                                AddMsgLine("您已经成功绑定未来花园BT账号，正在为您自动跳转登陆，请稍后");
                                PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyssoUid(ssoinfo.ssoUid, 1);//将token重新置为有效状态【*数据库写*】
                                SetUrl("http://buaabt.cn/buaasso.aspx?m=login&token=" + ssoinfo.Token);
                                SetMetaRefresh(5);
                                SetShowBackLink(false);
                                //=================================================================

                                ssoLoginId = -1;
                                ssoCreateToken = "";
                            }
                            else if (ssoinfo.ssoStatus == 1 && ssoinfo.Uid < 0 && ssoinfo.ssoName != "" && ssoinfo.Token1.Length == 64)
                            {
                                //可以注册
                                needinvitecode = false; //通过ihome SSO注册不需要邀请码
                                ssoToken1 = ssoinfo.Token1;
                                ssoName = ssoinfo.ssoUid.ToString();
                                ssoUid = ssoinfo.ssoUid;
                                if (reginviterid == 0) reginviterid = -1;
                                else reginviterid = -100;

                                pagetitle = "用户注册  i北航(i.buaa.edu.cn) 用户 免邀请注册新账号，如有账号，也可以直接登陆";
                                allowregister = true;
                                if (!ispost)
                                {

                                    string datastr = "";
                                    for (int i = 0; i < 100; i++)
                                    {
                                        datastr = PTTools.GetDateNowString() + PTTools.GetRandomString("0123456789", 3);
                                        if (Users.GetUserId(datastr + "_iHome") < 1) break;
                                    }
                                        
                                    DEFAULT_invitecode = "IHOME_INVITE";
                                    DEFAULT_username = datastr + "_iHome";
                                    DEFAULT_password = "IHOME_DEFAULTPASS";
                                    DEFAULT_email = datastr + "@ihome.buaa.edu.cn";
                                }
                            }
                            else
                            {
                                //尚未未绑定
                                PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO_ERROR REG-3", string.Format("FAILURE:校验SSOTOKEN失败 --UID:{0}--SSOUID:{1}--SSONAME:{2}--SSOSTATUS:{3}--SSOTOEKN1:{4}", ssoinfo.Uid, ssoinfo.ssoUid, ssoinfo.ssoName, ssoinfo.ssoStatus, ssoinfo.Token1));
                                ssoLoginId = -1;
                                ssoCreateToken = "";
                                AddErrLine("错误的链接访问 ER4，请从 i北航 重新点击跳转链接");
                            }
                        }

                    }
                    else
                    {
                        PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO_ERROR REG-4", string.Format("FAILURE:SSOTOKEN格式错误 --CTOKEN:{0}", ssoCreateToken));
                        ssoLoginId = -1;
                        ssoCreateToken = "";
                        AddErrLine("错误的链接访问 ER5，请从 i北航 重新点击跳转链接");
                       
                    }
                }
            }
            //

            #endregion

            if (config.Regstatus < 1 || !allowregister)
            {
                //不进行错误提示，显示正常页面，包含了iHome和CNGI注册链接
                //AddErrLine("论坛当前禁止新用户注册");
                return;
            }

            #region 用户IP判断
            string msg = Users.CheckRegisterDateDiff(DNTRequest.GetIP());
            if (msg != null)
            {
                AddErrLine(msg);
                return;
            }
            //if (config.Regctrl > 0)
            //{
            //    ShortUserInfo userinfo = Users.GetShortUserInfoByIP(DNTRequest.GetIP());
            //    if (userinfo != null)
            //    {
            //        int Interval = Utils.StrDateDiffHours(userinfo.Joindate, config.Regctrl);
            //        if (Interval <= 0)
            //        {
            //            AddErrLine("抱歉, 系统设置了IP注册间隔限制, 您必须在 " + (Interval * -1) + " 小时后才可以注册");
            //            return;
            //        }
            //    }
            //}

            //if (config.Ipregctrl.Trim() != "")
            //{
            //    string[] regctrl = Utils.SplitString(config.Ipregctrl, "\n");
            //    if (Utils.InIPArray(DNTRequest.GetIP(), regctrl))
            //    {
            //        ShortUserInfo userinfo = Users.GetShortUserInfoByIP(DNTRequest.GetIP());
            //        if (userinfo != null)
            //        {
            //            int Interval = Utils.StrDateDiffHours(userinfo.Joindate, 72);
            //            if (Interval < 0)
            //            {
            //                AddErrLine("抱歉, 系统设置了特殊IP注册限制, 您必须在 " + (Interval * -1) + " 小时后才可以注册");
            //                return;
            //            }
            //        }
            //    }
            //}
            #endregion

            ///得到广告列表
            headerad = Advertisements.GetOneHeaderAd("registerad", 0);
            footerad = Advertisements.GetOneFooterAd("registerad", 0);

            //如果提交了用户注册信息...
            if (!Utils.StrIsNullOrEmpty(createuser) && ispost)
            {
                SetShowBackLink(true);
                
                #region 系统原有的邀请码体系，弃用
                //InviteCodeInfo inviteCode = null;
                //if (allowinvite)
                //{
                //    if (config.Regstatus == 3 && invitecode == "")
                //    {
                //        AddErrLine("邀请码不能为空！");
                //        return;
                //    }
                //    if (invitecode != "")
                //    {
                //        inviteCode = Invitation.GetInviteCodeByCode(invitecode.ToUpper());
                //        if (!Invitation.CheckInviteCode(inviteCode))
                //        {
                //            AddErrLine("邀请码不合法或已过期！");
                //            return;
                //        }
                //    }
                //}
                #endregion

                string tmpUserName = DNTRequest.GetString(config.Antispamregisterusername);
                string email = DNTRequest.GetString(config.Antispamregisteremail).Trim().ToLower();
                string tmpBday = DNTRequest.GetString("bday").Trim();    
                string fgbtinvitecode = DNTRequest.GetString("registercode").Trim();
                
                if (tmpBday == "")
                {
                    tmpBday = string.Format("{0}-{1}-{2}", DNTRequest.GetString("bday_y").Trim(),
                           DNTRequest.GetString("bday_m").Trim(), DNTRequest.GetString("bday_d").Trim());
                }
                tmpBday = (tmpBday == "--" ? "" : tmpBday);

                ValidateUserInfo(tmpUserName, email, tmpBday);

                if (IsErr()) return;

                //如果用户名符合注册规则, 则判断是否已存在
                if (Users.GetUserId(tmpUserName) > 0)
                {
                    AddErrLine("请不要重复提交！");
                    return;
                }

                //【BT修改】添加 验证邀请码是否存在|| cngi_school != "buaa"

                //如果需要邀请码，或者通过CNGI登陆，或者通过ihome SSO注册（不需要邀请）则可以注册
                //否则，验证邀请码
                if (needinvitecode && cngi_login == false)
                {
                    if (!allowregister)
                    {
                        AddErrLine("当前禁止邀请注册");
                        return;
                    }
                    if (fgbtinvitecode.Length != 32)
                    {
                        AddErrLine("邀请码输入不正确");
                        return;
                    }
                    if (PrivateBTInvitation.VerifyInviteCode(fgbtinvitecode) < 1)
                    {
                        AddErrLine("邀请码不存在！请重新检查邀请码是否复制完整");
                        return;
                    }

                    if (reginviterid == 0) reginviterid = PrivateBTInvitation.GetInviteCodeBuyer(fgbtinvitecode);
                    else reginviterid = -100;

                }
                else if (!needinvitecode)
                {
                    //ihome sso 注册
                }
                else if (cngi_login == true)
                {
                    if (cngi_name == "" || cngi_name == null || cngi_school == "" || cngi_school == null)
                    {
                        AddErrLine("未能通过CNGI08获取您的身份信息，请重新登陆CNGI08重试，如多次重试仍无法注册，请向system@buaabt.cn反馈，并包含您的学校和CNGI用户名");
                        PTLog.InsertCNGILog("", "", PTLog.CNGILogType.CNGIRegister, PTLog.LogStatus.Error, "CNGI ERROR 0", string.Format("NULL INFO: CNGI_NAME-{0}, CNGI_SCHOOL-{1}", cngi_name == null ? "<NULL>" : cngi_name, cngi_school == null ? "<NULL>" : cngi_school));
                        return;
                    }
                    //不能重复绑定
                    if (PrivateBT.GetCNGIUserID(cngi_name, cngi_school) > 0)
                    {
                        SetUrl("http://sp-bbs.buaa6.edu.cn/cngilogin.aspx");
                        SetMetaRefresh(3);
                        SetShowBackLink(false);
                        AddErrLine("您已经绑定过CNGI08身份认证信息，不能再次绑定，正在为您登陆已绑定账号");
                        PTLog.InsertCNGILog(cngi_school, cngi_name, PTLog.CNGILogType.CNGIRegister, PTLog.LogStatus.Error, "CNGI ERROR -1", string.Format("DUPLACATE: CNGI_NAME-{0}, CNGI_SCHOOL-{1}", cngi_name == null ? "<NULL>" : cngi_name, cngi_school == null ? "<NULL>" : cngi_school));
                        return;
                    }
                }
                else
                {
                    AddErrLine("您的邀请码输入不正确");
                    return;
                }

                //{//TEST CODE
                //    AddErrLine(string.Format("现在可以注册!----TEST:---config.Regstatus:{0}----allowregister:{1}----needinvitecode:{2}", config.Regstatus, allowregister, needinvitecode));
                //    return;
                //}
                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////


                UserInfo userInfo = CreateUser(tmpUserName, email, tmpBday);

                #region 发送欢迎信息
                if (config.Welcomemsg == 1)
                {
                    // 收件箱
                    PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                    privatemessageinfo.Message = string.Format(config.Welcomemsgtxt, userInfo.Username);
                    privatemessageinfo.Subject = "欢迎您加入未来花园BT站! 请务必仔细阅读这条短消息【 这非常重要！！！ 】";
                    privatemessageinfo.Msgto = userInfo.Username;
                    privatemessageinfo.Msgtoid = userInfo.Uid;
                    privatemessageinfo.Msgfrom = PrivateMessages.SystemUserName;
                    privatemessageinfo.Msgfromid = 0;
                    privatemessageinfo.New = 1;
                    privatemessageinfo.Postdatetime = Utils.GetDateTime();
                    privatemessageinfo.Folder = 0;
                    PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                    //同时发送到邮箱
                    if (userInfo.Email.Trim() != "" && userInfo.Email.IndexOf("@ihome.buaa.edu.cn") < 0)
                    {
                        Emails.SendEmailNotify(userInfo.Email, privatemessageinfo.Subject, privatemessageinfo.Message);
                    }
                    
                }
                #endregion


                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 
                //【BT修改】添加

                PrivateMessageInfo btmessageinfo = new PrivateMessageInfo();
                string curdatetime = Utils.GetDateTime();

                //完成注册

                #region 邀请码注册

                if (needinvitecode)
                {
                    if (cngi_login == true && cngi_school == "buaa")
                    {
                        PTLog.InsertCNGILog(cngi_school, cngi_name, PTLog.CNGILogType.CNGIRegister, PTLog.LogStatus.Normal, "CNGI INVITATION", string.Format("CNGI INVITATION: CNGI_NAME-{0}, CNGI_SCHOOL-{1}", cngi_name == null ? "<NULL>" : cngi_name, cngi_school == null ? "<NULL>" : cngi_school));
                    }
                    else
                    {
                        PrivateBTInvitation.UseInviteCode(fgbtinvitecode, userInfo.Uid);

                        int buyerid = PrivateBTInvitation.GetInviteCodeBuyer(fgbtinvitecode);

                        // 收件箱
                        btmessageinfo.Message = "您的邀请码\"" + fgbtinvitecode + "\"被用户\"" + userInfo.Username + "\"注册\n\n\n本消息由系统自动发送，请勿回复";
                        btmessageinfo.Subject = "您的邀请码\"" + fgbtinvitecode + "\"被用户\"" + userInfo.Username + "\"注册(请勿回复本消息)";
                        btmessageinfo.Msgto = Users.GetUserName(buyerid);
                        btmessageinfo.Msgtoid = buyerid;
                        btmessageinfo.Msgfrom = PrivateMessages.SystemUserName;
                        btmessageinfo.Msgfromid = 0;
                        btmessageinfo.New = 1;
                        btmessageinfo.Postdatetime = curdatetime;
                        btmessageinfo.Folder = 0;
                        PrivateMessages.CreatePrivateMessage(btmessageinfo, 0); //向购买邀请码的人发送短消息
                    }
                }
                else
                {

                }

                #endregion

                if (cngi_login)
                {
                    int setcngi = PrivateBT.SetCNGIUserID(userInfo.Uid, cngi_name, cngi_school);

                    if (setcngi < 1)
                    {
                        AddErrLine("创建未来花园BT账号成功，但是绑定CNGI 联盟登陆信息失败，请重新绑定账号！");
                        PTLog.InsertCNGILog(cngi_school, cngi_name, PTLog.CNGILogType.CNGIRegister, PTLog.LogStatus.Error, "CNGI ERROR 2", string.Format("LINK ERROR: CNGI_NAME-{0}, CNGI_SCHOOL-{1}, USERID-{2}, RESULT-{3}", cngi_name == null ? "<NULL>" : cngi_name, cngi_school == null ? "<NULL>" : cngi_school, userInfo.Uid, setcngi));
                        return;
                    }
                    else
                    {
                        PTLog.InsertCNGILog(cngi_school, cngi_name, PTLog.CNGILogType.CNGILink, PTLog.LogStatus.Normal, "LINK SUCCESS", string.Format("CNGI_NAME-{0}, CNGI_SCHOOL-{1}, USERID-{2}, RESULT-{3}", cngi_name == null ? "<NULL>" : cngi_name, cngi_school == null ? "<NULL>" : cngi_school, userInfo.Uid, setcngi));
                        if (cngi_school == "buaa" && cngi_login == true)
                        {
                            btmessageinfo.Message = "您已完成校园网校园网统一认证绑定，绑定后不能更改或取消，请珍惜您的未来花园BT站账号";
                            btmessageinfo.Subject = "成功完成校园统一认证绑定";
                            btmessageinfo.Msgto = Users.GetUserName(userInfo.Uid);
                            btmessageinfo.Msgtoid = userInfo.Uid;
                            btmessageinfo.Msgfrom = "系统";
                            btmessageinfo.Msgfromid = 0;
                            btmessageinfo.New = 1;
                            btmessageinfo.Postdatetime = Utils.GetDateTime(); ;
                            btmessageinfo.Folder = 0;
                            PrivateMessages.CreatePrivateMessage(btmessageinfo, 0);
                        }
                        else
                        {
                            //赠送一个邀请码
                            PrivateBTInvitation.AddInviteCode(userInfo.Uid, 1);
                            btmessageinfo.Message = "发放邀请时请注意，如果被邀请人违反论坛相关规定被禁封，您将受到连带处罚";
                            btmessageinfo.Subject = "成功完成校园统一认证绑定，系统赠送您1个邀请码";
                            btmessageinfo.Msgto = Users.GetUserName(userInfo.Uid);
                            btmessageinfo.Msgtoid = userInfo.Uid;
                            btmessageinfo.Msgfrom = "系统";
                            btmessageinfo.Msgfromid = 0;
                            btmessageinfo.New = 1;
                            btmessageinfo.Postdatetime = Utils.GetDateTime(); ;
                            btmessageinfo.Folder = 0;
                            PrivateMessages.CreatePrivateMessage(btmessageinfo, 0); //向购买邀请码的人发送短消息
                        }
                    }
                }

                //通过BUAASSO注册的用户
                if (ssoCreateToken != "" && ssoToken1.Length == 64)
                {
                    //【BUAASSO】之前未绑定的情况，绑定信息
                    if (ssoLoginId < 2 && ssoUid > 0)
                    {

                        if (PTBuaaSSO.UpdateBuaaSSOInfoUidbyssoUid(userInfo.Uid, ssoUid) < 1)//【*数据库写*】
                        {
                            PTBuaaSSO.UpdateBuaaSSOInfoUidbyssoUid(userInfo.Uid, ssoUid);//【*数据库写*】
                            PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO ERROR REG-101", string.Format("绑定用户信息出错:--UID:{0}--SSOUID:{1}--SSOTOKEN:{2}, ", userInfo.Uid, ssoUid, ssoCreateToken));
                        }
                        else
                        {
                            PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSOLink, PTLog.LogStatus.Normal, "LINK SUCCESS1", userInfo.Uid.ToString());
                        }
                    }
                    else
                    {
                        PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO ERROR REG-102", string.Format("注册用户信息出错:--UID:{0}--SSOUID:{1}--SSOTOKEN:{2}, ", userInfo.Uid, ssoUid, ssoCreateToken));//【*数据库写*】
                    }

                    PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyssoUid(ssoUid, -1);//【*数据库写*】
                    

                    //返回信息
                    string confirmstr = PTCommon.GetRemoteResponse(string.Format("{2}?m=createok&key={0}&token={1}", AES.Encode2HEX("<@" + userInfo.Username.PadRight(20, ' ') + "@>", EncryptionKeyA), ssoToken1, ssoUrl)).Trim().ToLower();
                    if (confirmstr == "ok")
                    {
                        AddMsgLine("成功授权您通过 i北航(i.buaa.edu.cn) 直接访问");
                        PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSOLink, PTLog.LogStatus.Normal, "LINK FEEDBACK OK1", userInfo.Uid.ToString());
                    }
                    else if(confirmstr == "failure")
                    {
                        AddMsgLine("与 i北航(i.buaa.edu.cn) 通信发生错误");
                        PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO ERROR REG-103", string.Format("反馈iHome绑定信息出错:--UID:{0}--SSOUID:{1}--SSOTOKEN:{2}, ", userInfo.Uid, ssoUid, ssoCreateToken));
                    }
                    else
                    {
                        AddMsgLine("与 i北航(i.buaa.edu.cn) 通信失败，请重试，错误信息：" + confirmstr);
                        PTLog.InsertBuaaSSOLog(ssoUid, PTLog.BuaaSSOLogType.SSORegister, PTLog.LogStatus.Error, "SSO ERROR REG104", string.Format("反馈iHome绑定信息出错:--{3}--UID:{0}--SSOUID:{1}--SSOTOKEN:{2}, ", userInfo.Uid, ssoUid, ssoCreateToken, confirmstr));
                    }
                }

                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////
                




                //发送同步数据给应用程序
                Sync.UserRegister(userInfo.Uid, userInfo.Username, userInfo.Password, "");



                SetUrl("index.aspx");
                SetShowBackLink(false);
                SetMetaRefresh(config.Regverify == 0 ? 2 : 5);
                Statistics.ReSetStatisticsCache();

                //if (inviteCode != null)
                //{
                //    Invitation.UpdateInviteCodeSuccessCount(inviteCode.InviteId);
                //    if (config.Regstatus == 3)
                //    {
                //        if (inviteCode.SuccessCount + 1 >= inviteCode.MaxCount)
                //            Invitation.DeleteInviteCode(inviteCode.InviteId);
                //    }
                //}

                if (config.Regverify == 0)
                {

                    UserCredits.UpdateUserCredits(userInfo.Uid);

                    //////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////// 
                    //【BT修改】

                    if (!cngi_user) ForumUtils.WriteUserCookie(userInfo, -1, config.Passwordkey);
                    else ForumUtils.WriteUserCookie(userInfo, 0, config.Passwordkey);


                    //添加随机RKey
                    string rkey = PTUsers.SetUserRKey(userInfo.Uid);


                    //原始
                    //ForumUtils.WriteUserCookie(userInfo, -1, config.Passwordkey);

                    //【END BT修改】
                    //////////////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////
                    
                    
                    OnlineUsers.UpdateAction(olid, UserAction.Register.ActionID, 0, config.Onlinetimeout);
                    MsgForward("register_succeed");
                    AddMsgLine("注册成功, 返回登录页");
                }
                else
                {
                    if (config.Regverify == 1)
                        AddMsgLine("注册成功, 请您到您的邮箱中点击激活链接来激活您的帐号");
                    else if (config.Regverify == 2)
                        AddMsgLine("注册成功, 但需要系统管理员审核您的帐户后才可登录使用");
                }
                //ManyouApplications.AddUserLog(userInfo.Uid, UserLogActionEnum.Add);
                agree = "yes";
            }
        }

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="tmpUsername"></param>
        /// <param name="email"></param>
        /// <param name="tmpBday"></param>
        /// <returns></returns>
        private UserInfo CreateUser(string tmpUsername, string email, string tmpBday)
        {
            // 如果找不到0积分的用户组则用户自动成为待验证用户
            UserInfo userinfo = new UserInfo();
            userinfo.Username = tmpUsername;
            userinfo.Nickname = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("nickname")));
            userinfo.Password = DNTRequest.GetString("password");
            userinfo.Secques = ForumUtils.GetUserSecques(DNTRequest.GetInt("question", 0), DNTRequest.GetString("answer"));
            userinfo.Gender = DNTRequest.GetInt("gender", 0);
            userinfo.Adminid = 0;
            userinfo.Groupexpiry = 0;
            userinfo.Extgroupids = "";


            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】修改

            userinfo.Regip = ipaddress;
            userinfo.Inviterid = reginviterid;

            //原始
            //userinfo.Regip = DNTRequest.GetIP();
            
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            userinfo.Joindate = Utils.GetDateTime();
            userinfo.Lastip = DNTRequest.GetIP();
            userinfo.Lastvisit = Utils.GetDateTime();
            userinfo.Lastactivity = Utils.GetDateTime();
            userinfo.Lastpost = Utils.GetDateTime();
            userinfo.Lastpostid = 0;
            userinfo.Lastposttitle = "";
            userinfo.Posts = 0;
            userinfo.Digestposts = 0;
            userinfo.Oltime = 0;
            userinfo.Pageviews = 0;
            userinfo.Credits = 0;

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】

            userinfo.Extcredits1 = (decimal)Scoresets.GetScoreSet(1).Init;
            userinfo.Extcredits2 = (decimal)Scoresets.GetScoreSet(2).Init;
            userinfo.Extcredits3 = (decimal)Scoresets.GetScoreSet(3).Init;
            userinfo.Extcredits4 = (decimal)Scoresets.GetScoreSet(4).Init;
            userinfo.Extcredits5 = (decimal)Scoresets.GetScoreSet(5).Init;
            userinfo.Extcredits6 = (decimal)Scoresets.GetScoreSet(6).Init;
            userinfo.Extcredits7 = (decimal)Scoresets.GetScoreSet(7).Init;
            userinfo.Extcredits8 = (decimal)Scoresets.GetScoreSet(8).Init;

            //原始
            //userinfo.Extcredits1 = Scoresets.GetScoreSet(1).Init;
            //userinfo.Extcredits2 = Scoresets.GetScoreSet(2).Init;
            //userinfo.Extcredits3 = Scoresets.GetScoreSet(3).Init;
            //userinfo.Extcredits4 = Scoresets.GetScoreSet(4).Init;
            //userinfo.Extcredits5 = Scoresets.GetScoreSet(5).Init;
            //userinfo.Extcredits6 = Scoresets.GetScoreSet(6).Init;
            //userinfo.Extcredits7 = Scoresets.GetScoreSet(7).Init;
            //userinfo.Extcredits8 = Scoresets.GetScoreSet(8).Init;

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            //userinfo.Avatarshowid = 0;
            userinfo.Email = email;
            userinfo.Bday = tmpBday;
            userinfo.Sigstatus = DNTRequest.GetInt("sigstatus", 1) != 0 ? 1 : 0;
            userinfo.Tpp = DNTRequest.GetInt("tpp", 0);
            userinfo.Ppp = DNTRequest.GetInt("ppp", 0);
            userinfo.Templateid = DNTRequest.GetInt("templateid", 0);
            userinfo.Pmsound = DNTRequest.GetInt("pmsound", 0);
            userinfo.Showemail = DNTRequest.GetInt("showemail", 0);
            userinfo.Salt = "";

            int receivepmsetting = config.Regadvance == 0 ? 3 : DNTRequest.GetInt("receivesetting", 3);//关于短信息枚举值的设置看ReceivePMSettingType类型注释，此处不禁止用户接受系统短信息
            //foreach (string rpms in DNTRequest.GetString("receivesetting").Split(','))
            //{
            //    if (!Utils.StrIsNullOrEmpty(rpms))
            //        receivepmsetting = receivepmsetting | int.Parse(rpms);
            //}

            //if (config.Regadvance == 0)
            //    receivepmsetting = 7;

            userinfo.Newsletter = (ReceivePMSettingType)receivepmsetting;
            userinfo.Invisible = DNTRequest.GetInt("invisible", 0);
            userinfo.Newpm = config.Welcomemsg == 1 ? 1 : 0;
            userinfo.Medals = "";
            userinfo.Accessmasks = DNTRequest.GetInt("accessmasks", 0);
            userinfo.Website = Utils.HtmlEncode(DNTRequest.GetString("website"));
            userinfo.Icq = Utils.HtmlEncode(DNTRequest.GetString("icq"));
            userinfo.Qq = Utils.HtmlEncode(DNTRequest.GetString("qq"));
            userinfo.Yahoo = Utils.HtmlEncode(DNTRequest.GetString("yahoo"));
            userinfo.Msn = Utils.HtmlEncode(DNTRequest.GetString("msn"));
            userinfo.Skype = Utils.HtmlEncode(DNTRequest.GetString("skype"));
            userinfo.Location = Utils.HtmlEncode(DNTRequest.GetString("location"));
            userinfo.Customstatus = (usergroupinfo.Allowcstatus == 1) ? Utils.HtmlEncode(DNTRequest.GetString("customstatus")) : "";
            //userinfo.Avatar = @"avatars\common\0.gif";
            //userinfo.Avatarwidth = 0;
            //userinfo.Avatarheight = 0;
            userinfo.Bio = ForumUtils.BanWordFilter(DNTRequest.GetString("bio"));
            userinfo.Signature = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("signature")));

            PostpramsInfo postpramsinfo = new PostpramsInfo();
            postpramsinfo.Usergroupid = usergroupid;
            postpramsinfo.Attachimgpost = config.Attachimgpost;
            postpramsinfo.Showattachmentpath = config.Showattachmentpath;
            postpramsinfo.Hide = 0;
            postpramsinfo.Price = 0;
            postpramsinfo.Sdetail = userinfo.Signature;
            postpramsinfo.Smileyoff = 1;
            postpramsinfo.Bbcodeoff = 1 - usergroupinfo.Allowsigbbcode;
            postpramsinfo.Parseurloff = 1;
            postpramsinfo.Showimages = usergroupinfo.Allowsigimgcode;
            postpramsinfo.Allowhtml = 0;
            postpramsinfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
            postpramsinfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
            postpramsinfo.Smiliesmax = config.Smiliesmax;
            userinfo.Sightml = UBB.UBBToHTML(postpramsinfo);

            userinfo.Authtime = Utils.GetDateTime();

            //邮箱激活链接验证
            if (config.Regverify == 1)
            {
                userinfo.Authstr = ForumUtils.CreateAuthStr(20);
                userinfo.Authflag = 1;
                userinfo.Groupid = 8;
                SendEmail(tmpUsername, DNTRequest.GetString("password").Trim(), DNTRequest.GetString(config.Antispamregisteremail).Trim(), userinfo.Authstr);
            }
            //系统管理员进行后台验证
            else if (config.Regverify == 2)
            {
                userinfo.Authstr = DNTRequest.GetString("website");
                userinfo.Groupid = 8;
                userinfo.Authflag = 1;
            }
            else
            {
                userinfo.Authstr = "";
                userinfo.Authflag = 0;
                userinfo.Groupid = UserCredits.GetCreditsUserGroupId(0).Groupid;
            }
            userinfo.Realname = DNTRequest.GetString("realname");
            userinfo.Idcard = DNTRequest.GetString("idcard");
            userinfo.Mobile = DNTRequest.GetString("mobile");
            userinfo.Phone = DNTRequest.GetString("phone");

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】生成passkey
            
            userinfo.Passkey = PTTools.GetRandomString(32);

            //第三方加密验证模式
            if (config.Passwordmode > 1 && PasswordModeProvider.GetInstance() != null)
            {
                userinfo.Uid = PasswordModeProvider.GetInstance().CreateUserInfo(userinfo);
            }
            else if (ssoCreateToken.Length == 64 && DNTRequest.GetString("password") == "IHOME_DEFAULTPASS")
            {
                //userinfo.Password = "0123456789abcdef" + PTTools.GetRandomHex(16).ToLower();
                userinfo.Password = PTTools.GetRandomString(32, true);
                userinfo.Uid = Users.CreateUser(userinfo);
            }
            else
            {
                string originalpass = userinfo.Password;
                //userinfo.Password = Utils.MD5(userinfo.Password);
                userinfo.Password = PTTools.GetRandomString(32, true);
                userinfo.Uid = Users.CreateUser(userinfo);

                PTUsers.UpdatePasswordSHA512(userinfo.Uid, originalpass, true);
            }
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            return userinfo;
        }

        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="birthday"></param>
        private void ValidateUserInfo(string username, string email, string birthday)
        {

            #region CheckUserName
            if (string.IsNullOrEmpty(username))
            {
                AddErrLine("用户名不能为空");
                return;
            }
            if (Utils.GetStringLength(username) > 20)
            {
                AddErrLine("用户名不得超过20个字符");
                return;
            }
            if (Utils.GetStringLength(username) < 3)
            {
                AddErrLine("用户名不得小于3个字符");
                return;
            }
            if (username.IndexOf("　") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                AddErrLine("用户名中不允许包含全角空格符");
                return;
            }
            if (username.IndexOf(" ") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                AddErrLine("用户名中不允许包含空格");
                return;
            }
            if (username.IndexOf(":") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                AddErrLine("用户名中不允许包含冒号");
                return;
            }
            if (username.ToLower().IndexOf("ihome") > -1)
            {
                if (username.Length != 15 || (username.Substring(0, 6) != PTTools.GetDateNowString() && DateTime.Now.Hour != 0))
                {
                    AddErrLine("自定义的用户名中不允许包含“iHome”");
                    return;
                }
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】

            if (username.IndexOf(",") != -1 || username.IndexOf("，") != -1)
            {
                AddErrLine("用户名中不允许包含逗号");
                return;
            }
            if (username.IndexOf("@") != -1)
            {
                AddErrLine("用户名中不允许包含@字符");
                return;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 

            if (Users.GetUserId(username) > 0)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                AddErrLine("该用户名已存在");
                return;
            }
            if ((!Utils.IsSafeSqlString(username)) || (!Utils.IsSafeUserInfoString(username)))
            {
                AddErrLine("用户名中存在非法字符");
                return;
            }
            // 如果用户名属于禁止名单, 或者与负责发送新用户注册欢迎信件的用户名称相同...
            if (username.Trim() == PrivateMessages.SystemUserName ||
                     ForumUtils.IsBanUsername(username, config.Censoruser))
            {
                AddErrLine("用户名 \"" + username + "\" 不允许在本论坛使用");
                return;
            }
            #endregion

            #region CheckPassword
            // 检查密码
            if (DNTRequest.GetString("password").Equals(""))
            {
                AddErrLine("密码不能为空");
                return;
            }
            if (!DNTRequest.GetString("password").Equals(DNTRequest.GetString("password2")))
            {
                AddErrLine("两次密码输入必须相同");
                return;
            }
            if (DNTRequest.GetString("password").Length < 6)
            {
                AddErrLine("密码不得少于6个字符");
                return;
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】不允许使用IHOME_DEFAULTPASS作为password

            if (ssoCreateToken == "" && DNTRequest.GetString("password") == "IHOME_DEFAULTPASS")
            {
                AddErrLine("密码中包含系统关键词，请修改");
                return;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 

            #endregion

            #region CheckMail
            if (string.IsNullOrEmpty(email))
            {
                AddErrLine("Email不能为空");
                return;
            }
            if (!Utils.IsValidEmail(email))
            {
                AddErrLine("Email格式不正确");
                return;
            }
            if (!Users.ValidateEmail(email))
            {
                AddErrLine("Email: \"" + email + "\" 已经被其它用户注册使用");
                return;
            }

            string emailhost = Utils.GetEmailHostName(email);
            // 允许名单规则优先于禁止名单规则
            if (config.Accessemail.Trim() != "")
            {
                // 如果email后缀 不属于 允许名单
                if (!Utils.InArray(emailhost, config.Accessemail.Replace("\r\n", "\n"), "\n"))
                {
                    AddErrLine("Email: \"" + email + "\" 不在本论坛允许范围之类, 本论坛只允许用户使用这些Email地址注册: " +
                               config.Accessemail.Replace("\n", ",").Replace("\r", ""));
                    return;
                }
            }
            if (config.Censoremail.Trim() != "")
            {
                // 如果email后缀 属于 禁止名单
                if (Utils.InArray(emailhost, config.Censoremail.Replace("\r\n", "\n"), "\n"))
                {
                    AddErrLine("Email: \"" + email + "\" 不允许在本论坛使用, 本论坛不允许用户使用的Email地址包括: " +
                               config.Censoremail.Replace("\n", ",").Replace("\r", ""));
                    return;
                }
            }
            #endregion
            //实名验证
            #region CheckRealInfo

            string realName = DNTRequest.GetString("realname").Trim();
            string idCard = DNTRequest.GetString("idcard").Trim();
            string mobile = DNTRequest.GetString("mobile").Trim();
            string phone = DNTRequest.GetString("phone").Trim();

            if (config.Realnamesystem == 1)
            {
                if (string.IsNullOrEmpty(realName)||Utils.GetStringLength(realName) > 10)
                {
                    AddErrLine("真实姓名不能为空且不能大于10个字符");
                    return;
                }
                if (string.IsNullOrEmpty(idCard) || idCard.Length > 20)
                {
                    AddErrLine("身份证号码不能为空且不能大于20个字符");
                    return;
                }
                if (string.IsNullOrEmpty(mobile) && string.IsNullOrEmpty(phone))
                {
                    AddErrLine("移动电话号码或固定电话号码必须填写其中一项");
                    return;
                }
                if (mobile.Length > 20)
                {
                    AddErrLine("移动电话号码不能大于20个字符");
                    return;
                }
                if (phone.Length > 20)
                {
                    AddErrLine("固定电话号码不能大于20个字符");
                    return;
                }
            }
            #endregion
            if (!string.IsNullOrEmpty(idCard) && !Regex.IsMatch(idCard, @"^[\x20-\x80]+$"))
            {
                AddErrLine("身份证号码中含有非法字符");
                return;
            }
            if (!string.IsNullOrEmpty(mobile) && !Regex.IsMatch(mobile, @"^[\d|-]+$"))
            {
                AddErrLine("移动电话号码中含有非法字符");
                return;
            }
            if (!string.IsNullOrEmpty(phone) && !Regex.IsMatch(phone, @"^[\d|-]+$"))
            {
                AddErrLine("固定电话号码中含有非法字符");
                return;
            }

            //用户注册模板中,生日可以单独用一个名为bday的文本框, 也可以分别用bday_y bday_m bday_d三个文本框, 用户可不填写
            if (!Utils.IsDateString(birthday) && !string.IsNullOrEmpty(birthday))
            {
                AddErrLine("生日格式错误, 如果不想填写生日请置空");
                return;
            }
            if (Utils.GetStringLength(DNTRequest.GetString("bio").Trim()) > 500)
            {
                //如果自我介绍超过500...
                AddErrLine("自我介绍不得超过500个字符");
                return;
            }
            if (Utils.GetStringLength(DNTRequest.GetString("signature").Trim()) > 500)
            {
                //如果签名超过500...
                AddErrLine("签名不得超过500个字符");
                return;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="emailaddress"></param>
        /// <param name="authstr"></param>
        private void SendEmail(string username, string password, string emailaddress, string authstr)
        {
            Emails.DiscuzSmtpMail(username, emailaddress, password, authstr);
        }
    }
}