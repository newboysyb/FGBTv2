using System;
using System.Data;
using System.Text;
using System.Web;
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
    public class buaasso : PageBase
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

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //【BT修改】BUAA-SSO新增变量

        /// <summary>
        /// BUAA-SSO操作类型，sso跳转指令，gettoken获取令牌，login登录，create创建绑定，createok返回成功
        /// </summary>
        public string ssoMethod = DNTRequest.GetString("m").ToLower();
        /// <summary>
        /// BUAA-SSO KEY值，用来传递用户名
        /// </summary>
        public string ssoKey = DNTRequest.GetString("key").ToUpper();
        /// <summary>
        /// BUAA-SSO KEY1值，用来传递UID
        /// </summary>
        public string ssoKey1 = DNTRequest.GetString("key1").ToUpper();
        /// <summary>
        /// BUAA-SSO TOKEN值，用来传递令牌
        /// </summary>
        public string ssoToken = DNTRequest.GetString("token").ToUpper();
        /// <summary>
        /// BUAA-SSO TO值，用来传递目的站点
        /// </summary>
        public string ssoTO = DNTRequest.GetString("to");

        public string EncryptionKeyA = "";

        public string EncryptionKeyB = "";

        public string EncryptionKeyC = "";

        public string ssoUrl = "http://xxx.xxx.xxx.xxx/buaasso.php";

        public int servertext = 0;
        /// <summary>
        /// 请求来自IP地址
        /// </summary>
        public string serverip = HttpContext.Current.Request.UserHostAddress;


        protected override void ShowPage()
        {
            //调试安全。。。。
            //if (HttpContext.Current.Request.Url.Host.IndexOf("dnt3") < 0 && HttpContext.Current.Request.Url.Host.IndexOf("localhost") < 0)
            //    return;


            //DEBUG

            //string abc = AES.Encode2HEX("buaa", EncryptionKeyA);
            //abc = AES.Encode2HEX("abc", EncryptionKeyA);
            //abc = AES.DecodeFromHEX("26994B102D5B3F068682D4DB31E0F369", EncryptionKeyA);
            //abc = AES.Encode2HEX("a23456789012345678910", EncryptionKeyA);
            //abc = AES.Encode2HEX("a23456789012345678910123456789012", EncryptionKeyA);
            //abc = AES.Encode2HEX("buaa1", EncryptionKeyA);
            //abc = AES.Encode2HEX("buaa1", EncryptionKeyA);
            //abc = AES.Encode2HEX("buaa1", EncryptionKeyA);

            //string bbb = PTTools.GetRandomHex(32);

            //END DEBUG

            //此页面仅供服务器访问或者输出跳转及跳转信息，不用作登陆、注册页面
            switch (ssoMethod)
            {
                case "sso": //请求来自：客户端【暂时不用】
                    {
                        pagetitle = "通过 未来花园BT 账号登陆 i北航（i.buaa.edu.cn）";
                        TransfertoiHome(); return;
                    }
                case "createok": //请求来自：iHome服务器【暂时不用】
                    {
                        servertext = 1;
                        ConfirmBT2iHomeUserLink(); return;
                    }
                case "gettoken": //请求来自：iHome服务器 跳转登陆步骤①
                    {
                        servertext = 1;
                        SendiHome2BTToken(); return;
                    }
                case "login": //请求来自：客户端 跳转登陆步骤②
                    {
                        pagetitle = "通过 i北航（i.buaa.edu.cn）登陆 未来花园BT";
                        ValidateiHome2BTToken(); return;
                    }
                case "create": //请求来自：客户端或iHome服务器
                    {
                        pagetitle = "绑定 未来花园BT 账号到 i北航（i.buaa.edu.cn）";
                        if (ssoKey != "") { SendiHome2BTCreateToken(); servertext = 1; return; } //请求来自：iHome服务器 绑定步骤①
                        else { ValidateiHome2BTCreateToken(); return; } //请求来自：客户端 绑定步骤②
                    }
                default: //错误，按客户端处理
                    {
                        pagetitle = "未定义操作";
                        AddErrLine("未定义操作");
                        break;
                    }
                    //绑定步骤③->register.aspx，直接注册新账号
                    //绑定步骤④（可选）->login.aspx，绑定现有账号
                    
                    //跳转登陆步骤③ ->login.aspx，登陆
            }

        }

        /// <summary>
        /// 判断当前请求是否是真正服务器地址来的
        /// </summary>
        /// <returns></returns>
        protected bool IsServerIP()
        {

            //return true;

            //检测是否为iHome服务器地址

            if (serverip == "xxx.xxx.xxx.xxx" || serverip == "xxx.xxx.xxx.xxx" || serverip == "xxx.xxx.xxx.xxx" || serverip == "xxx.xxx.xxx.xxx"
                || serverip == "xxx.xxx.xxx.xxx" || serverip == "xxx.xxx.xxx.xxx"
                || serverip == "xxx.xxx.xxx.xxx") return true;
            else return false;
        }
        /// <summary>
        /// 输入text内容
        /// </summary>
        /// <param name="textcontent"></param>
        protected void OutText(string textcontent)
        {
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(textcontent);
            PageBase_ResponseEnd();
        }
        /// <summary>
        /// 输出页面跳转及信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Message"></param>
        /// <param name="err"></param>
        protected void JumpUrl(string url, string Message, bool err)
        {
            if (!err)
            {
                try
                {
                    HttpContext.Current.Response.Redirect(url);
                    return;
                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                }
            }

            if (err)
                AddErrLine(Message);
            else
                AddMsgLine(Message);
            
            SetUrl(url);

            if (err)
                SetMetaRefresh(3);
            else
                SetMetaRefresh(0);

            SetShowBackLink(false);  
        }

        /// <summary>
        /// 跳转到iHome，需登陆状态访问，校验userid
        /// </summary>
        /// <param name="ssoinfo"></param>
        protected void TransfertoiHome()
        {
            ////登陆状态才能访问
            //if (userid < 1)
            //{
            //    JumpUrl("login.aspx", "请先登陆", true);
            //    HttpContext.Current.Response.Redirect("login.aspx");
            //    return;
            //}

            //Regex Regex64HEX = new Regex(@"^[0-9a-fA-F]{64}$");
            //Regex Regex128HEX = new Regex(@"^[0-9a-fA-F]{128}$");

            //if (ssoTO == "ihome")
            //{
            //    PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyUid(userid);//【*数据库读*】
            //    if (ssoinfo.Uid > 0 && (ssoinfo.ssoStatus == 4 || ssoinfo.ssoStatus == 5))
            //    {
            //        //用户已经禁止此方法跳转
            //        JumpUrl("http://i.buaa.edu.cn/", "您已禁用授权登陆方式，正在为您跳转到 i北航(i.buaa.edu.cn)", false);
            //    }
            //    else if (ssoinfo.Uid < 0 || ssoinfo.ssoStatus != 2 && ssoinfo.ssoStatus != 3)
            //    {
            //        //用户尚未绑定
            //        ssoinfo.Token1 = PTTools.GetRandomHex(64);
            //        ssoinfo.Uid = userid;
            //        ssoinfo.ssoName = AES.Encode2HEX(username, EncryptionKeyA);
            //        ssoinfo.ssoStatus = 0;
            //        if (PTBuaaSSO.CreateBuaaSSOInfobyUid(ssoinfo) > 0)//先删除后添加//【*数据库写*】
            //        {
            //            ssoToken = PTTools.GetRemoteResponse(string.Format(ssoUrl + "?m=create&key={0}&token={1}", ssoinfo.ssoName, ssoinfo.Token1));
            //            if (Regex128HEX.IsMatch(ssoToken))
            //            {
            //                ssoToken = AES.DecodeFromHEX(ssoToken, EncryptionKeyB);
            //                if (Regex64HEX.IsMatch(ssoToken))
            //                {
            //                    JumpUrl(string.Format(ssoUrl + "?m=create&token={0}", ssoToken), "正在为您跳转到 i北航(i.buaa.edu.cn)", false);
            //                    return;
            //                }
            //            }
            //        }
            //        JumpUrl("forumindex.aspx", "与 i北航(i.buaa.edu.cn) 通信出错，如多次重试仍有问题，请联系管理员，并附上以下错误代码 E001", true);
            //        return;
            //    }
            //    else if (ssoinfo.Uid > 0 && (ssoinfo.ssoStatus == 2 || ssoinfo.ssoStatus == 3))
            //    {
            //        //当前可以直接跳转，无需绑定
            //        ssoinfo.ssoName = AES.Encode2HEX(username, EncryptionKeyA);
            //        ssoToken = PTTools.GetRemoteResponse(string.Format(ssoUrl + "?m=gettoken&key={0}&token={1}", ssoinfo.ssoName, ssoinfo.Token1));
            //        if (Regex128HEX.IsMatch(ssoToken))
            //        {
            //            ssoToken = AES.DecodeFromHEX(ssoToken, EncryptionKeyB);
            //            if (Regex64HEX.IsMatch(ssoToken))
            //            {
            //                JumpUrl(string.Format(ssoUrl + "?m=login&token={0}", ssoToken), "正在为您跳转到 i北航(i.buaa.edu.cn)", false);
            //                return;
            //            }
            //        }
            //        JumpUrl("forumindex.aspx", "与 i北航(i.buaa.edu.cn) 通信出错，如多次重试仍有问题，请联系管理员，并附上以下错误代码 E002", true);
            //        return;
            //    }
            //    else //理论上不会出现的情况
            //    {
            //        JumpUrl("forumindex.aspx", "系统发生错误，如多次重试仍有问题，请联系管理员，并附上以下错误代码 E003", true);
            //    }
            //}
            //else
            //{
            //    JumpUrl("forumindex.aspx", "不支持的授权登陆跳转地址", true);
            //}          
        }
        /// <summary>
        /// 接收 createok，接收ihome服务器绑定完成后返回的ihome用户名，保存为ssoname
        /// </summary>
        protected void ConfirmBT2iHomeUserLink()
        {
            ////ihome返回bt->ihome绑定信息
            ////此段落用户为未登录状态，userid无效
            
            ////安全校验
            //if (!IsServerIP())
            //{
            //    OutText("FAILURE:ILLEGAL ADDRESS " + HttpContext.Current.Request.UserHostAddress);
            //    return;
            //}

            //Regex Regex64HEX = new Regex(@"^[0-9a-fA-F]{64}$");
            //Regex Regex128HEX = new Regex(@"^[0-9a-fA-F]{128}$");
            //if (!Regex64HEX.IsMatch(ssoKey) || !Regex128HEX.IsMatch(ssoToken))
            //{
            //    OutText("FAILURE:MALFORMAT");
            //    return;
            //}
            //else
            //{
            //    //此处token为明文 ssoToken = AES.DecodeFromHEX(ssoToken, EncryptionKeyB);
            //    PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyToken1(ssoToken);

            //    if (ssoinfo.Uid > 0 && ssoinfo.ssoStatus == 0)
            //    {
            //        ssoKey = AES.DecodeFromHEX(ssoKey, EncryptionKeyA);
            //        if (ssoKey == "" || !CheckUsername(ref ssoKey))
            //        {
            //            OutText("FAILURE:DECODE");
            //        }
            //        else if (PTBuaaSSO.UpdateBuaaSSOInfossoNamebyUid(ssoinfo.Uid, ssoKey) > 0)
            //        {
            //            OutText("OK");
            //        }
            //        else
            //        {
            //            OutText("FAILURE:DUPLICATION OR INTERNAL");
            //        }
            //    }
            //    else
            //    {
            //        OutText("FAILURE:NOTMATCH");
            //    }
            //}
        }
        /// <summary>
        /// 接收gettoken，接收iHome服务器传送来的ssoname，生成token，保存并发送
        /// </summary>
        protected void SendiHome2BTToken()
        {
            //ihome->bt访问，请求令牌
            //此段落用户为未登录状态，userid无效
            
            //安全校验
            if (!IsServerIP())
            {
                OutText("FAILURE:ILLEGAL ADDRESS " + HttpContext.Current.Request.UserHostAddress);
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Critical, "SSO_ERROR E0", 
                    string.Format("SAFTY:ILLEGAL SERVER ADDRESS --IP:{0}",HttpContext.Current.Request.UserHostAddress));
                return;
            }

            Regex Regex64HEX = new Regex(@"^[0-9a-fA-F]{64}$");
            if (!Regex64HEX.IsMatch(ssoKey) || !Regex64HEX.IsMatch(ssoKey1))
            {
                OutText("FAILURE:FORMAT");
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E1", 
                    string.Format("FAILURE:FORMAT --SSOKEY:{0} --KEY1:{1} --IP:{2}",ssoKey,ssoKey1,serverip));
            }
            else
            {
                //用户名 SSONAME
                ssoKey = AES.DecodeFromHEX(ssoKey, EncryptionKeyA);
                //用户ID SSOUID
                ssoKey1 = AES.DecodeFromHEX(ssoKey1, EncryptionKeyC);

                ssoKey = GetUsrename(ssoKey);

                if (CheckUid(ref ssoKey1) && ssoKey != "")
                {
                    PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyssoUid(TypeConverter.StrToInt(ssoKey1));//【*数据库读*】
                    
                    //ssoUid未能获取，则尝试使用ssoName获取，仅限日期之前的部分
                    if (ssoinfo.Uid < 1 || (ssoinfo.ssoStatus != 2 && ssoinfo.ssoStatus != 3))
                    {
                        if (CheckUsername(ssoKey)) //符合规则的username才可以进行此查找（之前写入数据库的用户名均符合此规则）
                        {
                            ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyssoNameOldData(ssoKey);//【*数据库读*】
                            if (ssoinfo.ssoUid < 0 && ssoinfo.Uid > 0 && ssoinfo.ssoName.Trim() != "")
                            {
                                //成功获取信息，更新数据库，增加ssoUid
                                ssoinfo.ssoUid = TypeConverter.StrToInt(ssoKey1);
                                if (PTBuaaSSO.UpdateBuaaSSOInfossoUidbyssoName(ssoinfo.Uid, ssoinfo.ssoName, ssoinfo.ssoUid) != 1)
                                {
                                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E5", 
                                        string.Format("FAILURE: UPDATE SSOUID --SSONAME:{0} --UID:{1} --SSOUID:{2} --IP:{3}", ssoKey, ssoinfo.Uid, ssoinfo.ssoUid, serverip));
                                }
                                else
                                {
                                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Normal, "SSO_UPDATE SSOUID",
                                        string.Format("UPDATE SSOUID --SSONAME:{0} --UID:{1} --SSOUID:{2} --IP:{3}", ssoKey, ssoinfo.Uid, ssoinfo.ssoUid, serverip));
                                }
                            }
                        }
                    }
                    
                    //尚未添加ssoName的用户，添加ssoName，此部分在ihome变更程序后启用
                    if (!Utils.StrIsNullOrEmpty(ssoKey) && ssoinfo.ssoName != ssoKey && ssoinfo.Uid > 0 && ssoinfo.ssoUid > 0)
                    {
                        ssoinfo.ssoName = ssoKey;
                        if (PTBuaaSSO.UpdateBuaaSSOInfossoNamebyssoUid(ssoinfo.Uid, ssoinfo.ssoUid, ssoinfo.ssoName) != 1)
                        {
                            PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E6",
                                string.Format("FAILURE: UPDATE SSONAME --SSONAME:{0} --UID:{1} --SSOUID:{2} --IP:{3}", ssoKey, ssoinfo.Uid, ssoinfo.ssoUid, serverip));
                        }
                        else
                        {
                            PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Normal, "SSO_UPDATE SSONAME",
                                string.Format("UPDATE SSONAME --SSONAME:{0} --UID:{1} --SSOUID:{2} --IP:{3}", ssoKey, ssoinfo.Uid, ssoinfo.ssoUid, serverip));
                        }
                    }
                    
                    if (ssoinfo.Uid > 0 && (ssoinfo.ssoStatus == 2 || ssoinfo.ssoStatus == 3))
                    {
                        ssoinfo.Token = PTTools.GetRandomHex(64);
                        if (PTBuaaSSO.UpdateBuaaSSOInfoTokenbyssoUid(ssoinfo.ssoUid, ssoinfo.Token) > 0) //【*数据库写*】
                        {
                            OutText(AES.Encode2HEX(ssoinfo.Token, EncryptionKeyB));
                            PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Normal, "SSO_SEND LOGINKEY",
                               string.Format("SEND LOGINKEY --SSONAME:{0} --UID:{1} --SSOUID:{2} --IP:{3}", ssoKey, ssoinfo.Uid, ssoinfo.ssoUid, serverip));
                        }
                        else
                        {
                            OutText("FAILURE:SERVER ERROR");
                            PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E2",
                                string.Format("FAILURE:SERVER ERROR --UID:{0} --SSOSTATUS:{1} --SSONAME:{2} --NEW TOKEN:{3} --IP:{4}", ssoinfo.Uid, ssoinfo.ssoStatus, ssoinfo.ssoName, ssoinfo.Token, serverip));
                        }
                    }
                    else
                    {
                        OutText("FAILURE:NOMATCH");
                        PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E3", 
                            string.Format("FAILURE:NOMATCH --SSOKEY:{0} --SSOKEY1:{1} --SSOUID:{2} --IP:{3}", ssoKey, ssoKey1, ssoinfo.Uid, serverip));
                    }
                }
                else
                {
                    OutText("FAILURE:DECODE");
                    PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E4", 
                        string.Format("FAILURE:DECODE --SSOKEY:{0} --SSOKEY1:{1} --IP:{2}", ssoKey, ssoKey1, serverip));
                }

            }
        }

        /// <summary>
        /// iHome->BT访问，用户携带token访问此页面，验证后登陆用户
        /// 此函数由用户浏览器请求
        /// </summary>
        protected void ValidateiHome2BTToken()
        {
            //如果用户已经登陆了
            if (userid > 0)
            {
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Normal, "SSO_LOGIN PASS",
                   string.Format("LOGIN PASS CURUSER:{0} --SSOTOKEN:{1} --IP:{2}", userid, ssoToken, serverip));

                JumpUrl("forumindex.aspx", "您已登陆，即将为您跳转到首页", false);
                return;
            }

            //格式确认，未来更改为正则验证，此处token为明文
            Regex Regex64HEX = new Regex(@"^[0-9a-fA-F]{64}$");
            if (!Regex64HEX.IsMatch(ssoToken))
            {
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E21", 
                    "LOGIN TOKEN MALFORAMT --SSOTOKEN:" + ssoToken + "--IP:" + DNTRequest.GetIP());
                JumpUrl("login.aspx", "错误的访问格式", true);
                return;
            }

            PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyToken(ssoToken);//【*数据库读*】【时间确认更改点】

            if ((ssoinfo.ssoStatus != 2 && ssoinfo.ssoStatus != 3) || ssoinfo.Uid < 2 || (DateTime.Now - ssoinfo.TokenDate).TotalSeconds > 60 || ssoinfo.TokenStatus != 1)
            {
                //禁止使用此方式登陆，或者无此token，或者token时间超过60秒，则直接跳转到登陆页面
                PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E22", 
                    string.Format("LOGIN TOKEN ERROR --SSOTOKEN:{0}--SSOSTATUSL{1}--UID:{2}--TOKENDATE:{3}--TOKENSTATUS:{4} --IP:{5}", ssoToken, ssoinfo.ssoStatus, ssoinfo.Uid, ssoinfo.TokenDate, ssoinfo.TokenStatus, serverip));
                
                JumpUrl("login.aspx", "访问超时 或 验证访问信息失败或授权登陆被禁用，正在为您跳转到登陆页面，您可以从i北航点击跳转链接重试", true);
            }
            else
            {
                //重新设置token，设置tokenstatus为2，跳转到登陆页面，
                ssoinfo.Token = PTTools.GetRandomHex(64);
                PTBuaaSSO.UpdateBuaaSSOInfoTokenbyUid(ssoinfo.Uid, ssoinfo.Token);//【*数据库写*】
                PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyUid(ssoinfo.Uid, 2);//【*数据库写*】

                PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Normal, "LOGIN_SUCCESS", 
                    string.Format("LOGIN SUCCESS: UID:{0} --IP:{1}", ssoinfo.Uid, serverip));//日志【*数据库写*】
                
                JumpUrl("login.aspx?token=" + ssoinfo.Token, "验证成功，正在为您跳转到登陆页面", false);
            }
        }
        /// <summary>
        /// iHome->BT，申请绑定的令牌
        /// 此函数由iHome服务器请求
        /// </summary>
        protected void SendiHome2BTCreateToken()
        {
            //ihome->bt绑定申请，请求令牌
            //此段落用户为未登录状态，userid无效
            
            //安全校验
            if (!IsServerIP())
            {
                OutText("FAILURE:ILLEGAL ADDRESS " + HttpContext.Current.Request.UserHostAddress);
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Critical, "SSO_ERROR E10", 
                    string.Format("SAFTY:CTOKEN ILLEGAL ADDRESS --IP:{0}", serverip));
                return;
            }

            Regex Regex64HEX = new Regex(@"^[0-9a-fA-F]{64}$");
            Regex Regex128HEX = new Regex(@"^[0-9a-fA-F]{128}$");
            if (!Regex64HEX.IsMatch(ssoKey) || !Regex128HEX.IsMatch(ssoToken))
            {
                OutText("FAILURE:MALFORMAT");
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E11",
                    string.Format("FAILURE:CTOKEN MALFORMAT --SSOKEY:{0} --SSOTOKEN:{1} --IP:{2}", ssoKey, ssoToken, serverip));
                return;
            }

            ssoKey = AES.DecodeFromHEX(ssoKey, EncryptionKeyA);
            ssoToken = AES.DecodeFromHEX(ssoToken, EncryptionKeyB);

            if (!CheckUid(ref ssoKey) || !Regex64HEX.IsMatch(ssoToken))
            {
                OutText("FAILURE:DECODE ERROR");
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E12",
                    string.Format("FAILURE:CTOKEN DECODE ERROR --SSOKEY:{0} --SSOTOKEN:{1} --IP:{2}", ssoKey, ssoToken, serverip));
                return;
            }
            int ssouid = TypeConverter.StrToInt(ssoKey, -1);
            if (ssouid < 1)
            {
                OutText("FAILURE:DECODE ERROR");
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E12-2",
                    string.Format("FAILURE:CTOKEN DECODE ERROR --SSOKEY:{0} --SSOTOKEN:{1} --IP:{2}", ssoKey, ssoToken, serverip));
                return;
            }

            PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyssoUid(ssouid);//【*数据库读*】
            //表中已经存在此ssoname的条目
            if (ssoinfo.ssoStatus > -1) 
            {
                //不应该出现的状态
                if (ssoinfo.Uid < 0 && ssoinfo.ssoStatus != 1)
                {
                    OutText("FAILURE:SERVER ERROR E201");
                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E13", 
                        string.Format("FAILURE:SERVER ERROR E201 --SSOKEY:{0} --SSOTOKEN:{1} --SSOSTATUS:{2} --IP:{3}", ssoKey, ssoToken, ssoinfo.ssoStatus, serverip));
                    return;
                }
                    
                if (ssoinfo.ssoStatus == 2 || ssoinfo.ssoStatus == 3 || ssoinfo.ssoStatus == 1)
                {
                    //表中已经存在此用户名的记录且状态正常，按照正常流程进行，等用户访问链接时直接跳转登陆
                    ssoinfo.Token = PTTools.GetRandomHex(64);
                    ssoinfo.Token1 = ssoToken;
                    if (PTBuaaSSO.UpdateBuaaSSOInfoToken1byssoUid(ssoinfo.ssoUid, ssoinfo.Token, ssoinfo.Token1) > 0) //【*数据库写*】
                    {
                        OutText(AES.Encode2HEX(ssoinfo.Token, EncryptionKeyB));
                        PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Warning, "SSO_PASSTH", 
                            string.Format("PASSTH:EXIST --SSONAME:{0} --UID:{1} --SSOTOKEN:{2} --IP:{3}", ssoinfo.ssoName, ssoinfo.Uid, ssoToken, serverip));
                    }
                    else
                    {
                        OutText("FAILURE:SERVER ERROR E202");
                        PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E14", 
                            string.Format("FAILURE:SERVER ERROR E202 --SSONAME:{0} --SSOUID:{1} --SSOTOKEN:{2} --IP:{3}", ssoinfo.ssoName, ssoinfo.Uid, ssoToken, serverip));
                    }
                }
                else if (ssoinfo.ssoStatus == 4 || ssoinfo.ssoStatus == 5)
                {
                    OutText("FAILURE:USERDEINED");
                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Normal, "SSO_DEINED", 
                        string.Format("DEINED:USERDEINED --SSONAME:{0} --SSOUID:{1} --SSOTOKEN:{2} --SSOSTATUS:{3} --IP:{4}", ssoinfo.ssoName, ssoinfo.Uid, ssoToken, ssoinfo.ssoStatus, serverip));
                }
                else
                {
                    OutText("FAILURE:SERVER ERROR E203");
                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E15",
                        string.Format("DEINED:USERDEINED --SSONAME:{0} --SSOUID:{1} --SSOTOKEN:{2} --SSOSTATUS:{3} --IP:{4}", ssoinfo.ssoName, ssoinfo.Uid, ssoToken, ssoinfo.ssoStatus, serverip));
                }

            }
            //表没有此用户名的信息【插入信息，状态1】
            else
            {
                ssoinfo.Token = PTTools.GetRandomHex(64);
                ssoinfo.Token1 = ssoToken;
                ssoinfo.Uid = -ssouid;
                ssoinfo.ssoUid = ssouid;
                ssoinfo.ssoName = "NULL_" + PTTools.GetRandomString(4) + "_" + ssouid.ToString();
                ssoinfo.ssoStatus = 1;
                ssoinfo.TokenStatus = 1;
                ssoinfo.TokenDate = DateTime.Now;
                if (PTBuaaSSO.CreateBuaaSSOInfobyssoUid(ssoinfo) > 0) //插入新记录【*数据库写*】
                {
                    OutText(AES.Encode2HEX(ssoinfo.Token, EncryptionKeyB));
                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Normal, "CTOKEN",
                        string.Format("CTOKEN OK --SSONAME:{0} --SSOUID:{1} --SSOTOKEN:{2} --SSOSTATUS:{3} --SSOKEY:{4} --IP:{5}", ssoinfo.ssoName, ssoinfo.Uid, ssoToken, ssoinfo.ssoStatus, ssoKey, serverip));
                }
                else
                {
                    //ssouid或者ssoname重复都将导致插入失败
                    OutText("FAILURE:SERVER ERROR E204 DUPLICATE UID OR USERNAME, PLEASE RETRY");
                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E16", 
                        string.Format("FAILURE:SERVER ERROR E204 DUPLICATE UID OR USERNAME --SSONAME:{0} --SSOUID:{1} --SSOTOKEN:{2} --SSOSTATUS:{3} --SSOKEY:{4} --IP:{5}", ssoinfo.ssoName, ssoinfo.Uid, ssoToken, ssoinfo.ssoStatus, ssoKey, serverip));
                }
            }
        }

        /// <summary>
        /// iHome->BT访问，用户携带token访问此页面，验证后跳转到登陆界面并附带token参数，登陆成功后发送所绑定的用户名
        /// 此函数由用户浏览器请求
        /// </summary>
        protected void ValidateiHome2BTCreateToken()
        {
            //格式确认，此处为明文发送
            Regex Regex64HEX = new Regex(@"^[0-9a-fA-F]{64}$");
            if (!Regex64HEX.IsMatch(ssoToken))
            {
                PTLog.InsertBuaaSSOLog(-1, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E31", 
                    "错误的链接访问 --SSOTOKEN:" + ssoToken + "--IP:" + DNTRequest.GetIP());
                
                JumpUrl("login.aspx", "错误的链接访问", true);
                return;
            }

            PTBuaaSSOinfo ssoinfo = PTBuaaSSO.GetBuaaSSOInfobyToken(ssoToken);

            if (ssoinfo.ssoStatus > -1)
            {

                //不应该出现的状态
                if (ssoinfo.ssoUid < 1 || ssoinfo.ssoStatus == 0 || (ssoinfo.Uid < 0 && ssoinfo.ssoStatus != 1) || ssoinfo.ssoName.Trim() == "")
                {
                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E32", 
                        "错误的链接访问 --SSOTOKEN:" + ssoToken + "--IP:" + DNTRequest.GetIP());
                    
                    JumpUrl("login.aspx", "错误的链接访问，如多次重试仍有问题，请联系管理员，并附上以下错误代码 E301", true);
                    return;
                }

                if ((DateTime.Now - ssoinfo.TokenDate).TotalSeconds > 3000 || ssoinfo.TokenStatus != 1)
                {
                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E33", 
                        "验证访问信息失败或授权登陆被禁用 --SSOTOKEN:" + ssoToken + "--UID:" + ssoinfo.Uid.ToString() + "--SSONAME:" + ssoinfo.ssoName + "--IP:" + DNTRequest.GetIP() + "--TOKENSTATUS:" + ssoinfo.TokenStatus.ToString() + "--TOKENDATE:" + ssoinfo.TokenDate.ToString());
                    
                    JumpUrl("login.aspx", "验证访问信息失败或授权登陆被禁用，正在为您跳转到登陆页面", true);
                    return;
                }

                if (ssoinfo.ssoStatus == 1 || ssoinfo.ssoStatus == 2 || ssoinfo.ssoStatus == 3)
                {
                    //正常流程，（或者表中已经存在此用户名的记录且状态正常，按照正常流程进行，重新向ihome服务器发送记录），等用户访问链接时直接跳转登陆
                    ssoinfo.Token = PTTools.GetRandomHex(64);
                    if (PTBuaaSSO.UpdateBuaaSSOInfoTokenbyssoUid(ssoinfo.ssoUid, ssoinfo.Token) > 0 && PTBuaaSSO.UpdateBuaaSSOInfoTokenStatusbyssoUid(ssoinfo.ssoUid, 2) > 0)
                    {
                        JumpUrl("register.aspx?ctoken=" + ssoinfo.Token, "正在为您跳转到登陆页面", false);
                    }
                    else
                    {
                        PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E34", 
                            "创建跳转更新Token和Status --SSOTOKEN:" + ssoToken + "--SSOUID:" + ssoinfo.ssoUid.ToString() + "--UID:" + ssoinfo.Uid.ToString() + "--SSONAME:" + ssoinfo.ssoName + "--IP:" + DNTRequest.GetIP() + "--TOKENSTATUS:" + ssoinfo.TokenStatus.ToString() + "--TOKENDATE:" + ssoinfo.TokenDate.ToString());
                        
                        JumpUrl("login.aspx", "错误的链接访问，如多次重试仍有问题，请联系管理员，并附上以下错误代码 E302", true);
                    }
                }
                else if (ssoinfo.ssoStatus == 4 || ssoinfo.ssoStatus == 5)
                {
                    //表中已经存在此用户名的记录且禁止访问
                    PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Normal, "SSO_ERROR E35", 
                        "授权登陆被禁用 --SSOTOKEN:" + ssoToken + "--UID:" + ssoinfo.Uid.ToString() + "--SSONAME:" + ssoinfo.ssoName + "--IP:" + DNTRequest.GetIP() + "--TOKENSTATUS:" + ssoinfo.TokenStatus.ToString() + "--TOKENDATE:" + ssoinfo.TokenDate.ToString());
                    
                    JumpUrl("login.aspx", "您已经选择为不允许通过此方式登陆，将为您跳转登陆页面", true);
                }
            }
            else
            {
                PTLog.InsertBuaaSSOLog(ssoinfo.ssoUid, PTLog.BuaaSSOLogType.SSOToken, PTLog.LogStatus.Error, "SSO_ERROR E36", 
                    "STATUS状态错误 --SSOTOKEN:" + ssoToken + "--UID:" + ssoinfo.Uid.ToString() + "--SSONAME:" + ssoinfo.ssoName + "--IP:" + DNTRequest.GetIP() + "--TOKENSTATUS:" + ssoinfo.TokenStatus.ToString() + "--TOKENDATE:" + ssoinfo.TokenDate.ToString());
                
                JumpUrl("login.aspx", "错误的链接访问，如多次重试仍有问题，请联系管理员，并附上以下错误代码 E304", true);
            }
        }

        /// <summary>
        /// 检查Uid是否合乎规则
        /// </summary>
        /// <param name="ssouid"></param>
        /// <returns></returns>
        protected bool CheckUid(ref string ssouid)
        {
            if (ssouid.Length != 24)
            {
                return false;
            }

            if (ssouid.Substring(0, 2) != "<@" || ssouid.Substring(ssouid.Length - 2, 2) != "@>")
            {
                return false;
            }
            else ssouid = ssouid.Substring(2, ssouid.Length - 4).Trim();

            //前面加了随机字符串的情况，去掉前面10个随机字符串
            if (ssouid.Length == 20)
            {
                ssouid = ssouid.Substring(10).Trim();
            }

            if (Utils.IsInt(ssouid)) return true;
            else return false;
        }

        protected string GetUsrename(string username)
        {
            if (Utils.GetStringLength(username) > 24)
            {
                //AddErrLine("用户名不得超过20个字符");
                return "";
            }
            if (Utils.GetStringLength(username) < 7)
            {
                //AddErrLine("用户名不得小于3个字符");
                return "";
            }

            //前面已经保证username最小长度为7
            if (username.Substring(0, 2) != "<@" || username.Substring(username.Length - 2, 2) != "@>")
            {
                return "";
            }
            else username = username.Substring(2, username.Length - 4).Trim();

            return username;
        }

        /// <summary>
        /// 检测用户名是否合乎规则
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        protected bool CheckUsername(string username)
        {
            //此处已经完成用户名提取，进入校验阶段

            if (string.IsNullOrEmpty(username))
            {
                //AddErrLine("用户名不能为空");
                return false;
            }

            if (username.IndexOf("　") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1 || username.IndexOf("") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                //AddErrLine("用户名中不允许包含全角空格符");
                return false;
            }
            if (username.IndexOf(" ") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                //AddErrLine("用户名中不允许包含空格");
                return false;
            }
            if (username.IndexOf(":") != -1)
            {
                //如果用户名符合注册规则, 则判断是否已存在
                //AddErrLine("用户名中不允许包含冒号");
                return false;
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】

            if (username.IndexOf(",") != -1 || username.IndexOf("，") != -1)
            {
                //AddErrLine("用户名中不允许包含逗号");
                return false;
            }
            if (username.IndexOf("@") != -1)
            {
                //AddErrLine("用户名中不允许包含@字符");
                return false;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 

            if ((!Utils.IsSafeSqlString(username)) || (!Utils.IsSafeUserInfoString(username)))
            {
                //AddErrLine("用户名中存在非法字符");
                return false;
            }
            return true;
        }

    }
}