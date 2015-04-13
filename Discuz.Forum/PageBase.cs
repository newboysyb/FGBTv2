using System;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

using Discuz.Forum;
using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Plugin.Space;
using Discuz.Plugin.Album;
using Discuz.Plugin.Mall;

namespace Discuz.Forum
{
    /// <summary>
    /// Discuz!NT页面基类
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        #region 公共变量定义
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //【BT修改】添加公共需要的信息
        //


        /// <summary>
        /// 简洁用户信息，包括BT信息，PageBase中定义并赋值
        /// </summary>
        protected internal PTUserInfo btuserinfo;
        /// <summary>
        /// 当前用户是否来自IPv6
        /// </summary>
        protected internal bool isipv6 = PTTools.IsIPv6(System.Web.HttpContext.Current.Request.UserHostAddress);
        /// <summary>
        /// 当前访问的IP类型，1.校内局域网IPv4，2.校内公网IPv4，10.校内原生IPv6，11.校外原生IPv6，21.校内ISATAP，22.校外ISATAP，31.Teredo
        /// </summary>
        protected internal int ipaddress_type = -1;
        /// <summary>
        /// 显示在工具栏上的IP地址信息
        /// </summary>
        protected internal string ipaddress_note = "";
        /// <summary>
        /// 当前用户是否使用HTTPS访问本站
        /// </summary>
        protected internal bool IsHttps = (System.Web.HttpContext.Current.Request.ServerVariables["HTTPS"] == "on");
        /// <summary>
        /// 当前用户的IP，如果是通过手机或CNGI访问，则获取X-Forward地址，确保是用户原始地址
        /// </summary>
        protected internal string ipaddress = System.Web.HttpContext.Current.Request.UserHostAddress;
        /// <summary>
        /// BT状态，更新统计用
        /// </summary>
        //protected internal PrivateBTServerStats btstats;
        /// <summary>
        /// 当前完整的URL地址
        /// </summary>
        protected string url = DNTRequest.GetUrl();
        /// <summary>
        /// 当前时间
        /// </summary>
        protected internal long timenow;
        /// <summary>
        /// CNGI用户名
        /// </summary>
        protected internal string cngi_name = "";
        /// <summary>
        /// CNGI学校
        /// </summary>
        protected internal string cngi_school = "";
        /// <summary>
        /// 通过CNGI访问
        /// </summary>
        protected internal bool cngi_user = false;
        /// <summary>
        /// 用户是否在CNGI已经登陆
        /// </summary>
        protected internal bool cngi_login = false;
        /// <summary>
        /// 新手任务提示
        /// </summary>
        protected internal string newuseralert = "";
        //
        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 论坛配置信息
        /// </summary>
        protected internal GeneralConfigInfo config;
        /// <summary>
        /// 当前用户的用户组信息
        /// </summary>
        protected internal UserGroupInfo usergroupinfo;
        /// <summary>
        /// 在线用户信息
        /// </summary>
        protected internal OnlineUserInfo oluserinfo;
        /// <summary>
        /// 当前用户的用户名
        /// </summary>
        protected internal string username;
        /// <summary>
        /// 当前用户的密码
        /// </summary>
        protected internal string password;
        /// <summary>
        /// 当前用户的特征串
        /// </summary>
        protected internal string userkey;
        /// <summary>
        /// 当前用户的用户ID
        /// </summary>
        protected internal int userid = -1;
        /// <summary>
        /// 当前用户的在线表ID
        /// </summary>
        protected internal int olid;
        /// <summary>
        /// 当前用户的用户组ID
        /// </summary>
        protected internal int usergroupid;
        /// <summary>
        /// 当前用户的管理权限，1为管理员，2为超版，3为版主，-1为特殊组。
        /// 如果需要获得admingroup信息，请勿使用此值，使用usergroupid作为条件查询即可
        /// </summary>
        protected internal int useradminid;
        /// <summary>
        /// 当前用户的最后发帖时间
        /// </summary>
        protected internal string lastposttime;
        /// <summary>
        /// 当前用户的最后发短消息时间
        /// </summary>
        protected internal string lastpostpmtime;
        /// <summary>
        /// 当前用户的最后搜索时间
        /// </summary>
        protected internal string lastsearchtime;
        /// <summary>
        /// 当前用户所使用的短信息铃声编号
        /// </summary>
        protected internal int pmsound;
        /// <summary>
        /// 当前页面是否被POST请求
        /// </summary>
        protected internal bool ispost;
        /// <summary>
        /// 当前页面是否被GET请求
        /// </summary>
        protected internal bool isget;
        /// <summary>
        /// 当前用户的短消息数目
        /// </summary>
        protected internal int newpmcount = 0;
        /// <summary>
        /// 当前用户的短消息数目
        /// </summary>
        protected internal int realnewpmcount = 0;
        /// <summary>
        /// 当前页面标题
        /// </summary>
        protected internal string pagetitle = "页面";
        /// <summary>
        /// 模板id
        /// </summary>
        protected internal int templateid;
        /// <summary>
        /// 模板风格选择列表框选项
        /// </summary>
        protected internal string templatelistboxoptions;
        /// <summary>
        /// 当前模板路径
        /// </summary>
        protected internal string templatepath;
        /// <summary>
        /// 当前日期
        /// </summary>
        protected internal string nowdate;
        /// <summary>
        /// 当前时间
        /// </summary>
        protected internal string nowtime;
        /// <summary>
        /// 当前日期时间
        /// </summary>
        protected internal string nowdatetime;
        /// <summary>
        /// 当前页面Meta字段内容
        /// </summary>
        protected internal string meta = "";
        /// <summary>
        /// 当前页面Link字段内容
        /// </summary>
        protected internal string link;
        /// <summary>
        /// 当前页面中增加script
        /// </summary>
        protected internal string script;
        /// <summary>
        /// 当前页面检查到的错误数
        /// </summary>
        protected internal int page_err = 0;
        /// <summary>
        /// 提示文字
        /// </summary>
        protected internal string msgbox_text = "";
        /// <summary>
        /// 用户中心提示文字
        /// </summary>
        protected internal string usercpmsgbox_text = "";
        /// <summary>
        /// 是否显示回退的链接
        /// </summary>
        protected internal string msgbox_showbacklink = "true";
        /// <summary>
        /// 回退链接的内容
        /// </summary>
        protected internal string msgbox_backlink = "javascript:history.back();";
        /// <summary>
        /// 返回到的页面url地址
        /// </summary>
        protected internal string msgbox_url = "";
        /// <summary>
        /// 多少分钟之前的帖子为新帖
        /// </summary>
        protected internal int newtopicminute = 120;
        /// <summary>
        /// 当前在线人数
        /// </summary>
        protected internal int onlineusercount = 1;
        /// <summary>
        ///	头部广告
        /// </summary>
        protected internal string headerad = "";
        /// <summary>
        /// 底部广告
        /// </summary>
        protected internal string footerad = "";
        /// <summary>
        /// 页面内容
        /// </summary>
        protected internal System.Text.StringBuilder templateBuilder = new System.Text.StringBuilder();
        /// <summary>
        /// 是否为需检测校验码页面
        /// </summary>
        protected bool isseccode = true;
        /// <summary>
        /// 是否为游客缓存页
        /// </summary>
        protected int isguestcachepage = 0;
        /// <summary>
        /// 导航主菜单
        /// </summary>
        protected string mainnavigation;
        /// <summary>
        /// 导航子菜单
        /// </summary>
        protected System.Data.DataTable subnavigation;
        /// <summary>
        /// 带有子菜单的主导航菜单
        /// </summary>
        protected string[] mainnavigationhassub;
        /// <summary>
        /// 当前页面开始载入时间(毫秒)
        /// </summary>
        private DateTime m_starttick;
        /// <summary>
        /// 当前页面执行时间(毫秒)
        /// </summary>
        private double m_processtime;
        /// <summary>
        /// 当前页面名称
        /// </summary>
        public string pagename = DNTRequest.GetPageName();
        /// <summary>
        /// 空间地址
        /// </summary>
        public string spaceurl = "";
        /// <summary>
        /// 相册地址
        /// </summary>
        public string albumurl = "";
        /// <summary>
        /// 论坛地址
        /// </summary>
        public string forumurl = "";
        /// <summary>
        /// 查询次数统计
        /// </summary>
        public int querycount = 0;
        /// <summary>
        /// 论坛相对根的路径
        /// </summary>
        public string forumpath = BaseConfigs.GetForumPath;
        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        public string rooturl = Utils.GetRootUrl(BaseConfigs.GetForumPath);
        /// <summary>
        /// 模板图片目录(可以是绝对或相对地址)
        /// </summary>
        public string imagedir = "default/images";
        /// <summary>
        /// 模板CSS目录(可以是绝对或相对地址)
        /// </summary>
        public string cssdir = "default";
        /// <summary>
        /// js目录(可以是绝对或相对地址)
        /// </summary>
        public string jsdir = "javascript";
        /// <summary>
        /// 主题鉴定图片目录(可以是绝对或相对地址)
        /// </summary>
        public string topicidentifydir = "images/identify/";
        /// <summary>
        /// 主题图标目录(可以是绝对或相对地址)
        /// </summary>
        public string posticondir = "images/posticons/";
        /// <summary>
        /// 用户头像
        /// </summary>
        public string useravatar = "";
        /// <summary>
        /// 获取主题(附件)买卖使用的积分字段
        /// </summary>
        public int topicattachscorefield = Scoresets.GetTopicAttachCreditsTrans();
        /// <summary>
        /// 返回ajax格式结果
        /// </summary>
        public int inajax = DNTRequest.GetInt("inajax", 0);
        /// <summary>
        /// 浮动窗体
        /// </summary>
        public int infloat = DNTRequest.GetInt("infloat", 0);
        /// <summary>
        /// 暂用于显示用户积分和对应的组别
        /// </summary>
        public string userinfotips = "";
        /// <summary>
        /// 用于检测头部登录时是否启用验证码
        /// </summary>
        public bool isLoginCode = true;
        /// <summary>
        /// 是否加载窄屏样式
        /// </summary>
        public bool isnarrowpage = false;
        /// <summary>
        /// 安全提问问题id
        /// </summary>
        public int question = DNTRequest.GetQueryInt("question", 0);

        public string CSSJSVersion = "0001";

        #endregion
        
#if DEBUG
        public string querydetail = "";
#endif

        public static void PageBase_ResponseEnd()
        {
            try
            {
                System.Web.HttpContext.Current.Response.End();
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Detail, "Response.End", System.Web.HttpContext.Current.Request.UserHostAddress + " -PAGE:" + System.Web.HttpContext.Current.Request.RawUrl + " -EX:" + ex.ToString());
            }
        }
        public static void PageBase_ResponseFlushEnd()
        {
            try
            {
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Detail, "Response.EndFlush", System.Web.HttpContext.Current.Request.UserHostAddress + " -PAGE:" + System.Web.HttpContext.Current.Request.RawUrl + " -EX:" + ex.ToString());
            }
        }
        public static void PageBase_ResponseFlush()
        {
            try
            {
                System.Web.HttpContext.Current.Response.Flush();
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Detail, "Response.Flush", System.Web.HttpContext.Current.Request.UserHostAddress + " -PAGE:" + System.Web.HttpContext.Current.Request.RawUrl + " -EX:" + ex.ToString());
            }
        }
        
        /// <summary>
        /// 获得游客缓存
        /// </summary>
        /// <param name="pagename"></param>
        /// <returns></returns>
        private int GetCachePage(string pagename)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            //Discuz.Cache.ICacheStrategy ics = new ForumCacheStrategy();
            //ics.TimeOut = config.Guestcachepagetimeout * 60;
            //cache.LoadCacheStrategy(ics);
            string str = cache.RetrieveObject("/Forum/GuestCachePage/" + pagename) as string;
            //cache.LoadDefaultCacheStrategy();
            if (str != null && str.Length > 1)
            {
                System.Web.HttpContext.Current.Response.Write(str);
                PageBase_ResponseEnd();
                return 2;
            }
            return 1;
        }

        /// <summary>
        /// 判断页面是否需要游客缓存页
        /// </summary>
        /// <param name="pagename"></param>
        /// <returns>不需要返回false</returns>
        private bool GetUserCachePage(string pagename)
        {
            switch (pagename)
            {
                case "website.aspx":
                    isguestcachepage = GetCachePage(pagename);
                    break;
                case "forumindex.aspx":
                    isguestcachepage = GetCachePage(pagename);
                    break;
                case "spaceindex.aspx":
                    isguestcachepage = GetCachePage(pagename);
                    break;
                case "albumindex.aspx":
                    isguestcachepage = GetCachePage(pagename);
                    break;
                case "showtopic.aspx":
                    {
                        int pageid = DNTRequest.GetQueryInt("page", 1);
                        int topicid = DNTRequest.GetQueryInt("topicid", 0);
                        //参数数目为2或0表示当前页面可能为第一页帖子列表
                        if ((DNTRequest.GetParamCount() == 2 || DNTRequest.GetParamCount() == 3) && topicid > 0 && ForumUtils.ResponseShowTopicCacheFile(topicid, pageid))
                        {
                            TopicStats.Track(topicid, 1);
                            return true;
                        }
                        break;
                    }
                case "showforum.aspx":
                    {
                        int pageid = DNTRequest.GetQueryInt("page", 1);
                        int forumid = DNTRequest.GetQueryInt("forumid", 0);
                        //参数数目为2或0表示当前页面可能为第一页帖子列表
                        if ((DNTRequest.GetParamCount() == 2 || DNTRequest.GetParamCount() == 3) && forumid > 0 && ForumUtils.ResponseShowForumCacheFile(forumid, pageid))
                            return true;

                        break;
                    }
                default:
                    break;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //【BT修改】

        /// <summary>
        /// 页面是否必须是登陆用户才能查看
        /// </summary>
        /// <param name="currentpagename"></param>
        /// <returns></returns>
        private bool IsPageNeedLogin(string currentpagename)
        {

            //currentpagename = currentpagename.ToLower();
            if (currentpagename == "attachupload.aspx" || currentpagename == "cngilogin.aspx" || currentpagename == "getpassword.aspx" 
                || currentpagename == "setnewpassword.aspx" || currentpagename == "login.aspx" || currentpagename == "register.aspx"
                || currentpagename == "logout.aspx" || currentpagename == "activationuser.aspx")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 当前访问的IP类型，1.校内局域网IPv4，2.校内公网IPv4，10.校内原生IPv6，11.校外原生IPv6，21.校内ISATAP，22.校外ISATAP，31.Teredo
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private string GetIPAddressNote(string ip)
        {
            string rst = "";
            PTTools.IPType iptype = PTTools.GetIPType(ip);
            if (iptype == PTTools.IPType.InBuaaNative_IPv6 || iptype == PTTools.IPType.Out_IPv6) rst = "<span title=\"IP地址:[" + ip + "]\">您正在使用<font color=Red><b>&nbsp;[&nbsp;IPv6&nbsp;]&nbsp;</b></font>地址访问本站</span>";
            else if (iptype == PTTools.IPType.InBuaaISATAP_IPv6) rst = "<span title=\"IP地址:[" + ip + "]\">您正在使用<font color=Black><b>&nbsp;[&nbsp;ISATAP&nbsp;]&nbsp;</b></font>地址访问本站</span>";
            else if (iptype == PTTools.IPType.Teredo_IPv6 || iptype == PTTools.IPType.Out_IPv6) rst = "<span title=\"IP地址:[" + ip + "]\">您正在使用<font color=Black><b>&nbsp;[&nbsp;Teredo&nbsp;]&nbsp;</b></font>地址访问本站</span>";
            else if (iptype == PTTools.IPType.InBuaa_IPv4 || iptype == PTTools.IPType.Out_IPv4) rst = "<span title=\"IP地址:[" + ip + "]\">您正在使用<font color=Blue><b>&nbsp;[&nbsp;IPv4&nbsp;]&nbsp;</b></font>地址访问本站</span>";
            else rst = "<span title=\"IP地址:[" + ip + "]\">您正在使用<font color=Black><b>&nbsp;[&nbsp;未知&nbsp;]&nbsp;</b></font>地址访问本站</span>";

            return rst;
        }


        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 校验用户是否可以访问论坛
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserPermission()
        {
            //已登录用户检测用户组状态,如果是禁言或禁止访问状态时间到期,则自动解禁
            if (usergroupid == 4) //|| usergroupid == 5 只解除禁言状态
            {
                ShortUserInfo userInfo = Users.GetShortUserInfo(userid);
                if (userInfo.Groupexpiry != 0 && userInfo.Groupexpiry <= Utils.StrToInt(DateTime.Now.ToString("yyyyMMdd"), 0))
                {
                    UserGroupInfo groupInfo = UserCredits.GetCreditsUserGroupId(userInfo.Credits);
                    usergroupid = groupInfo.Groupid != 0 ? groupInfo.Groupid : usergroupid;
                    Users.UpdateUserGroup(userid, usergroupid);
                }
            }

            //如果论坛关闭且当前用户请求页面不是登录页面且用户非管理员, 则跳转至论坛关闭信息页
            if (config.Closed == 1 && useradminid != 1 && pagename != "getpassword.aspx" && pagename != "setnewpassword.aspx" && pagename != "login.aspx" && pagename != "logout.aspx" && pagename != "register.aspx")
            {
                try { ForumUtils.ClearUserCookie(); }
                catch (System.Exception ex) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Exception, "CLEAR COOKIES", "M1:" + ex); }
                
                SetMetaRefresh(10, "https://buaabt.cn/login.aspx");
                ShowMessage(1);
                OnlineUsers.AddUserLoginRecord(userid, username, usergroupid, ipaddress, 202, 202, password, Utils.GetCookie("rkey"));
                return false;
            }

            //被禁封的用户不能访问论坛
            if (userid > 0 && btuserinfo != null && (btuserinfo.Groupid == 5 || btuserinfo.Vip < 0 || usergroupinfo.Allowvisit != 1))
            {
                try { ForumUtils.ClearUserCookie(); }
                catch (System.Exception ex) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Exception, "CLEAR COOKIES", "M2:" + ex); }
                SetMetaRefresh(10, "https://buaabt.cn/login.aspx");
                ShowMessage("对不起，您的账号已经被禁封，不能访问论坛。 <br/><br/>导致账号被禁封的原因可能有：<br/>" +
                    "① 严重违反论坛管理规定，如多次发布未经许可的广告、人身攻击等。<br/>② 未完成论坛新手任务。 新手任务包括包括必须达到20G统计下载流量，蓝种、流量优惠部分不计算在内。 " + 
                    "<br/><br/>如果您是因为没有完成新手任务被禁封，未来将可能通过重新激活方式重新获得访问权限。 此功能目前尚未开放，暂时请重新注册获取新账号。" +
                    "<br/><br/>注册方式有 1.向同学所要邀请码注册。 2.通过 i北航 实名认证免邀请注册。 3.通过CNGI验证校园卡账号密码实名认证注册。 <br/>" + 
                    "请注意：所有免邀请注册方式均只能使用一次，使用后账号和相应的实名认证信息绑定，绑定后不能解除绑定。"
                    , 2);
                OnlineUsers.AddUserLoginRecord(userid, username, usergroupid, ipaddress, 202, 203, password, Utils.GetCookie("rkey"));
                return false;
            }

            //在线人数过多
            if (onlineusercount >= config.Maxonlines && useradminid != 1 && pagename != "login.aspx" && pagename != "logout.aspx")
            {
                try { ForumUtils.ClearUserCookie(); }
                catch (System.Exception ex) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Exception, "CLEAR COOKIES", "M4:" + ex); }
                SetMetaRefresh(10, "https://buaabt.cn/login.aspx");
                ShowMessage("抱歉，目前访问人数太多，你暂时无法访问论坛，请稍后再试。", 0);
                OnlineUsers.AddUserLoginRecord(userid, username, usergroupid, ipaddress, 202, 204, password, Utils.GetCookie("rkey"));
                return false;
            }

            //未登录不能查看的页面，列表中为 未登录也可以查看的页面
            if (pagename != "getpassword.aspx" && pagename.IndexOf("setnewpassword.aspx") != 0 && pagename != "login.aspx" && 
                pagename.LastIndexOf("register.aspx") != 0 && pagename != "logout.aspx" && pagename != "activationuser.aspx" &&
                pagename != "buaasso.aspx" && pagename != "cngilogin.aspx" && pagename != "attachupload.aspx")
            {
                if (userid < 1 || userid != Utils.StrToInt(ForumUtils.GetCookie("userid"), userid) || btuserinfo == null)
                {
                    //判断用户Cookie是否正常
                    if (pagename == "forumindex.aspx" || pagename == "index.aspx")
                    {
                        if (DNTRequest.GetString("s") == "login")
                        {
                            string refurl = DNTRequest.GetUrlReferrer();
                            if (refurl.IndexOf("login.aspx") > 0)
                            {
                                ShowMessage("Cookie丢失或失效，登录失败！<br/>花园需要使用浏览器Cookie保存登录凭据，请检查您的浏览器是否启用了Cookie！ <br/>以及检查系统时间、时区是否设置正确，不正确的系统时间也会导致Cookie异常失效<br/><br/>建议使用官方原版的IE、Firefox、Chrome浏览器以避免各种莫名其妙的问题", 3);
                                PTLog.InsertSystemLog_WithPageIP(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Error, "CookieLost", string.Format("-UID:{0} -USERNAME:{1}", userid, username));
                                return false;
                            }
                            else PTLog.InsertSystemLog_WithPageIP(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Error, "CookieLostFake", string.Format("-UID:{0} -USERNAME:{1}", userid, username));
                        }
                    }
                    
                    //如果用户未登录或登陆信息失效
                    OnlineUsers.AddUserLoginRecord(userid, username, usergroupid, ipaddress, 202, 205, password, Utils.GetCookie("rkey"));

                    //sessionajax页面特殊处理，不清空cookie，经常出现此错误？？？
                    if (pagename == "sessionajax.aspx")
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Warning, "UnLoginSessionAjax",
                                        string.Format("UID:{0} -OP:{1} -RKEY:{2} -Pass:{3}", Utils.StrToInt(ForumUtils.GetCookie("userid"), -1), ipaddress, Utils.GetCookie("rkey"), password));
                        AddErrLine("登陆信息失效");
                        return false;
                    }


                    try
                    {
                        try { ForumUtils.ClearUserCookie(); }
                        catch (System.Exception ex) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Exception, "CLEAR COOKIES", "M5:" + ex); }

                        System.Web.HttpContext.Current.Response.Redirect((IsHttps ? "https://" : "http://") + System.Web.HttpContext.Current.Request.Url.Authority + "/login.aspx");
                    }
                    catch (Exception ex)
                    {
                        try { ForumUtils.ClearUserCookie(); }
                        catch (System.Exception ex1) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Exception, "CLEAR COOKIES", "M6:" + ex1); }

                        SetMetaRefresh(0, "https://buaabt.cn/login.aspx");
                        ShowMessage("您尚未登录，正在为您跳转到登陆界面 [ERROR: NOT LOGINED (2)]", 2);
                        PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Exception, "BASE-REDIRECT", "ValidateUserPermission-0:" + ex.ToString());
                    }
                    return false;
                }
            }

            // 如果IP访问列表有设置则进行判断
            if (config.Ipaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Ipaccess, "\n");
                foreach (string regipden in regctrl)
                {
                    //有通配符，则前段匹配，长度必须不同。 否则必须完整匹配
                    if ((regipden.IndexOf("*") > 0 && ipaddress.IndexOf(regipden) == 0 && regipden.Length != ipaddress.Length) || (regipden.IndexOf("*") < 0 && regipden == ipaddress))
                    {
                        try { ForumUtils.ClearUserCookie(); }
                        catch (System.Exception ex) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Exception, "CLEAR COOKIES", "M7:" + ex); }

                        SetMetaRefresh(10, "https://buaabt.cn/login.aspx");
                        ShowMessage("由于您所在IP区段严重违反未来花园禁封规则, 已被禁止访问", 2);
                        OnlineUsers.AddUserLoginRecord(userid, username, usergroupid, ipaddress, 202, 206, password, Utils.GetCookie("rkey"));
                        return false;
                    }
                }
                //if (!Utils.InIPArray(DNTRequest.GetIP(), regctrl))
                //{

                //}
            }


            // 如果IP访问列表有设置则进行判断
            if (config.Ipdenyaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Ipdenyaccess, "\n");
                foreach (string regipden in regctrl)
                {
                    //有通配符，则前段匹配，长度必须不同。 否则必须完整匹配
                    if ((regipden.IndexOf("*") > 0 && ipaddress.IndexOf(regipden) == 0 && regipden.Length != ipaddress.Length) || (regipden.IndexOf("*") < 0 && regipden == ipaddress))
                    {
                        try { ForumUtils.ClearUserCookie(); }
                        catch (System.Exception ex) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Exception, "CLEAR COOKIES", "M8:" + ex); }

                        SetMetaRefresh(10, "https://buaabt.cn/login.aspx");
                        ShowMessage("由于您所在IP区段严重违反未来花园禁封规则, 已被禁止访问.", 2);
                        OnlineUsers.AddUserLoginRecord(userid, username, usergroupid, ipaddress, 202, 207, password, Utils.GetCookie("rkey"));
                        return false;
                    }
                }
            }

            //如果当前用户请求页面不是登录页面并且当前用户非管理员并且论坛设定了时间段,当时间在其中的一个时间段内,则跳转到论坛登录页面
            if (useradminid != 1 && pagename != "login.aspx" && pagename != "logout.aspx" && usergroupinfo.Disableperiodctrl != 1)
            {
                if (Scoresets.BetweenTime(config.Visitbanperiods))
                {
                    try { ForumUtils.ClearUserCookie(); }
                    catch (System.Exception ex) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Exception, "CLEAR COOKIES", "M9:" + ex); }

                    SetMetaRefresh(10, "https://buaabt.cn/login.aspx");
                    ShowMessage("在此时间段内不允许访问本论坛", 2);
                    OnlineUsers.AddUserLoginRecord(userid, username, usergroupid, ipaddress, 202, 208, password, Utils.GetCookie("rkey"));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 校验已登录用户密码是否正确，直接读取页面相应值。 传入的密码为RSA加密后的密码，正确返回true，否则false
        /// </summary>
        /// <param name="userpass"></param>
        /// <returns></returns>
        public bool ValidateUserPassword(bool isOriginalPassword)
        {
            if (!DNTRequest.IsPost()) return false;

            string UserPassword = DNTRequest.GetString("password");
            string UserPasswordKey = DNTRequest.GetString("passwordkey");
            
            if(!isOriginalPassword)
                UserPassword = PTRsa.DecryptUserPassword(userid, UserPasswordKey, UserPassword);
            
            if (UserPassword == "")
            {
                AddErrLine("您的密码验证页面已经过期失效，请刷新后重试！");
                return false;
            }

            if (PTUsers.CheckPasswordSHA512(userid, UserPassword, isOriginalPassword, false, "", false) == userid)
                return true;
            else return false;
        }


        /// <summary>
        /// 校验验证码
        /// </summary>
        private bool ValidateVerifyCode()
        {
            isLoginCode = config.Seccodestatus.Contains("login.aspx");
            //当该页面设置了验证码检验，并且当前用户的用户组没有给予忽略验证码的权限，则isseccode=true;
            isseccode = Utils.InArray(pagename, config.Seccodestatus) && usergroupinfo.Ignoreseccode == 0;

            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //【BT修改】验证码检测增加页面

            //if (pagename == "cngilogin.aspx") isseccode = true;

            //修改验证，登陆界面默认不需要验证码，密码输入错误之后需要
            //Session.Add("PasswordTry");
            if(pagename == "login.aspx")
            {
                if (LoginLogs.UpdateLoginLog(ipaddress, false) > 0) isseccode = true;
            }

            //特殊IP的登录请求不进行验证码验证
            if (isseccode && ((ipaddress == "xxx.xxx.xxx.xxx" && templateid != 5) || ipaddress == "xxx.xxx.xxx.xxx" || ipaddress == "xxx.xxx.xxx.xxx" || ipaddress == "xxx.xxx.xxx.xxx"
                || ipaddress == "xxx.xxx.xxx.xxx" || ipaddress == "xxx.xxx.xxx.xxx" || ipaddress == "xxx.xxx.xxx.xxx"))
            {
                if (DNTRequest.IsPost() && pagename == "login.aspx")
                {

                    string postusername = DNTRequest.GetString("username");
                    if (postusername.ToLower() == "imax" || postusername.ToLower() == "amphetamine" || postusername.ToLower() == "core2")
                        isseccode = false;
                }
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            if ((!isseccode) || (!ispost)) return true;

            return DoValidateVerifyCode();
        }

        public bool DoValidateVerifyCode()
        {
            if (DNTRequest.GetString("vcode") == "")
            {
                if (pagename == "showforum.aspx")
                {
                    //版块如不设置密码,必无校验码
                    //return;
                }
                else if (pagename.EndsWith("ajax.aspx"))
                {
                    if (DNTRequest.GetString("t") == "quickreply")
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Warning, "VCode_EMPTY",
    string.Format("未输入验证码 ajax：UID:{0} -PAGE:{1} -IP:{2} -VCODEPOST:{3} -VCODE:{4}", userid, pagename, ipaddress, DNTRequest.GetString("vcode"), oluserinfo.Verifycode));

                        ResponseAjaxVcodeError();
                        return false;
                    }
                }
                else
                {
                    if (DNTRequest.GetString("loginsubmit") == "true" && pagename == "login.aspx")//添加快捷登陆方式的验证码判断
                    {
                        //快速登录时不报错
                    }
                    else if (DNTRequest.GetFormString("agree") == "true" && pagename == "register.aspx")
                    {
                        //同意注册协议也不受此限制
                    }
                    else
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Warning, "VCode_EMPTY",
                            string.Format("未输入验证码：UID:{0} -PAGE:{1} -IP:{2} -VCODEPOST:{3} -VCODE:{4}", userid, pagename, ipaddress, DNTRequest.GetString("vcode"), oluserinfo.Verifycode));
                        
                        AddErrLine("请输入验证码");
                        return false;
                    }
                }
            }
            else
            {

                if (!OnlineUsers.CheckUserVerifyCode(olid, DNTRequest.GetString("vcode")))
                {
                    if (pagename.EndsWith("ajax.aspx"))
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Warning, "VCode_ERR",
                            string.Format("验证码错误 ajax：UID:{0} -PAGE:{1} -IP:{2} -VCODE:{3} -POST:{4}", userid, pagename, ipaddress, DNTRequest.GetString("vcode"), oluserinfo.Verifycode));
                        

                        ResponseAjaxVcodeError();
                        return false;
                    }
                    else
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.PageBaseValidate, PTLog.LogStatus.Warning, "VCode_ERR",
                            string.Format("验证码错误：UID:{0} -PAGE:{1} -IP:{2} -VCODE:{3} -POST:{4}", userid, pagename, ipaddress, DNTRequest.GetString("vcode"), oluserinfo.Verifycode));
                        

                        AddErrLine("验证码错误");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 从config文件中获取并设置论坛, 空间, 相册的不带页面名称的url路径
        /// (返回绝对或相对地址)
        /// </summary>
        private void LoadUrlConfig()
        {
            spaceurl = config.Spaceurl.ToLower();
            if (spaceurl.IndexOf("http://") == 0)
            {
                if (spaceurl.EndsWith("aspx"))
                    spaceurl = spaceurl.Substring(0, spaceurl.LastIndexOf('/')) + "/";
                else if (!spaceurl.EndsWith("/"))
                    spaceurl = spaceurl + "/";
            }
            else
                spaceurl = "";

            albumurl = config.Albumurl.ToLower();
            if (albumurl.IndexOf("http://") == 0)
            {
                if (albumurl.EndsWith("aspx"))
                    albumurl = albumurl.Substring(0, albumurl.LastIndexOf('/')) + "/";
                else if (!albumurl.EndsWith("/"))
                    albumurl = albumurl + "/";
            }
            else
                albumurl = "";

            forumurl = config.Forumurl.ToLower();
            if (forumurl.IndexOf("http://") == 0)
            {
                if (forumurl.EndsWith("aspx") || forumurl.EndsWith("htm") || forumurl.EndsWith("html"))
                    forumurl = forumurl.Substring(0, forumurl.LastIndexOf('/')) + "/";
                else if (!forumurl.EndsWith("/"))
                    forumurl = forumurl + "/";
            }
            else
                forumurl = BaseConfigs.GetForumPath;
        }

        #region 负载均衡环境下PV统计
        private static bool recordPageView = LoadBalanceConfigs.GetConfig() != null &&
                                             LoadBalanceConfigs.GetConfig().AppLoadBalance &&
                                             LoadBalanceConfigs.GetConfig().RecordPageView;

        private static Dictionary<string, int> pageViewStatistic = new Dictionary<string, int>();

        public static void PageViewStatistic(string pagename)
        {
            if (pageViewStatistic.Count == 0)
                pageViewStatistic.Add(Utils.GetDateTime(), -1); //统计开启时间          

            if (pageViewStatistic.ContainsKey(pagename))
                pageViewStatistic[pagename] = pageViewStatistic[pagename] + 1;
            else
                pageViewStatistic.Add(pagename, 1);
        }

        public static Dictionary<string, int> PageViewSatisticInfo
        {
            get { return pageViewStatistic; }
            set { pageViewStatistic = value; }

        }
        #endregion


        /// <summary>
        /// 判断是否通过CNGI登陆成功，修改cngi_user（通过CNGI访问），cngi_login（CNGI登陆成功），cngi_user，cngi_school，及ip（替换为CNGI获取的IP）
        /// 返回值：True：检测完成，继续执行；False：已跳转，结束执行
        /// password值未修改，为空
        /// </summary>
        /// <returns>True：检测完成，继续执行；False：已跳转，结束执行</returns>
        private bool CheckCNGILogin()
        {
            //CNGI联盟登陆判断
            if (ipaddress == "xxx.xxx.xxx.xxx" || ipaddress == "2001:da8:203:502:2e0:81ff:feb9:da86" || ipaddress == "2001:da8:203:503:2e0:81ff:feb9:da86" || ipaddress == "xxx.xxx.xxx.xxx")
            {
                //访问来自CNGI服务器
                cngi_user = true;
                try
                {
                    //联盟登陆方式，判断检测，再次确认访问来自CNGI
                    if (System.Web.HttpContext.Current.Request.Headers.Get("Host") != "bt.buaa6.edu.cn" || System.Web.HttpContext.Current.Request.Headers.Get("X-Forwarded-Host") != "sp-bbs.buaa6.edu.cn" || System.Web.HttpContext.Current.Request.Headers.Get("X-Forwarded-Server") != "sp-bbs.buaa6.edu.cn")
                    {
                        ShowMessage("CNGI登陆出错，代码CNGI_E2，请重试，如仍有问题，请联系system@buaabt.cn并附上详细访问信息", 3);
                        return false;
                    }
                    else
                    {
                        //确认当前访问来自CNGI
                        if (System.Web.HttpContext.Current.Request.Headers.Get("USERNAME") == null || System.Web.HttpContext.Current.Request.Headers.Get("USERNAME") == "" ||
                            System.Web.HttpContext.Current.Request.Headers.Get("INSTITUTION") == null || System.Web.HttpContext.Current.Request.Headers.Get("INSTITUTION") == "")
                        {
                            //CNGI未登录，跳转到登陆中间页面cngilogin.aspx，由cngilogin.aspx跳转到CNGI登陆页面
                            if (pagename != "cngilogin.aspx")
                            {
                                string redirecturl = "http://sp-bbs.buaa6.edu.cn/Shibboleth.sso/DS?target=http%3A%2F%2Fsp-bbs.buaa6.edu.cn" + Utils.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl);
                                //string redirecturl = "http://sp-bbs.buaa6.edu.cn/cngilogin.aspx";
                                try
                                {

                                    System.Web.HttpContext.Current.Response.Redirect(redirecturl);
                                    return false;
                                }
                                catch (Exception ex)
                                {
                                    SetMetaRefresh(0, redirecturl);
                                    ShowMessage("您尚未登录CNGI认证，请登录，正在为您自动跳转，如不能自动跳转，请检查Javascript是否开启", 2);
                                    PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Exception, "BASE-REDIRECT", "CheckCNGILogin-1" + ex.ToString());
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //CNGI已经登陆，可以获取cngi_name和cngi_school
                            cngi_name = System.Web.HttpContext.Current.Request.Headers.Get("USERNAME");
                            cngi_school = System.Web.HttpContext.Current.Request.Headers.Get("INSTITUTION");
                            cngi_login = true;

                            //判断用户是否已经绑定
                            userid = PrivateBT.GetCNGIUserID(cngi_name, cngi_school);

                            //用户尚未绑定，跳转到cngilogin.aspx，进行绑定
                            if ((userid < 3) && pagename != "cngilogin.aspx" && pagename != "register.aspx")
                            {
                                string redirecturl = "http://sp-bbs.buaa6.edu.cn/Shibboleth.sso/DS?target=http%3A%2F%2Fsp-bbs.buaa6.edu.cn" + Utils.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl);
                                //string redirecturl = "http://sp-bbs.buaa6.edu.cn/cngilogin.aspx";
                                try
                                {
                                    System.Web.HttpContext.Current.Response.Redirect(redirecturl);
                                    return false;
                                }
                                catch (Exception ex)
                                {
                                    SetMetaRefresh(0, redirecturl);
                                    ShowMessage("您尚未绑定CNGI认证账号，请绑定账号，正在为您自动跳转，如不能自动跳转，请检查Javascript是否开启", 2);
                                    PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Exception, "BASE-REDIRECT", "CheckCNGILogin-2" + ex.ToString());
                                    return false;
                                }
                            }

                            //检测当前Cookie中的uid与现在访问的是否相同
                            if (userid > 2)
                            {
                                //当前用户已经登陆，且已经绑定，可以检测
                                if (userid != Utils.StrToInt(ForumUtils.GetCookie("userid"), -1))
                                {
                                    //由于CNGI登陆和普通登陆访问的域名不同，因此用户Cookie也不相同，不会造成影响原有登陆用户的情况
                                    //cookie中用户信息不存在或不符合，重新写入cookie信息（联盟登陆，关闭浏览器cookie作废）
                                    ForumUtils.ClearUserCookie();
                                    ForumUtils.WriteUserCookie(userid, 0, config.Passwordkey, 0, -1);

                                    //CNGI登陆，RKey以“CN_”开头
                                    string rkey = "CN_" + PTTools.GetRandomString(7, true);
                                    PTUsers.SetUserRKey(userid, rkey);
                                    System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = rkey;
                                    System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;
                                    System.Web.HttpContext.Current.Response.Cookies["rkey"].HttpOnly = true;

                                    //Cookie需要下一次页面访问才能生效。已经在验证密码的UPdateonlineuser屏蔽cngi用户password完整性检测，因此可以登录，无需再次跳转。
                                    //SetMetaRefresh(0, url);
                                    //ShowMessage("正在为您自动跳转，请稍后，如不能自动跳转，请手动访问" + url, 3);
                                    //return false;
                                }
                            }
                            else
                            {
                                //当前用户未绑定（只能出现在cngilogin.aspx和register页面）
                                if (Utils.StrToInt(ForumUtils.GetCookie("userid"), -1) > 0)
                                {
                                    //Cookie中存有用户信息，清空
                                    ForumUtils.ClearUserCookie();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("CNGI登陆出错，代码CNGI_E3，请重试，如仍有问题，请联系system@buaabt.cn并附上详细访问信息", 3);
                    PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Exception, "BASE-REDIRECT", "CheckCNGILogin-3" + ex.ToString());
                    return false;
                }

                //从反向代理服务器获取IP信息
                if (System.Web.HttpContext.Current.Request.Headers.Get("X-Forwarded-For") == null)
                {
                    ShowMessage("CNGI登陆出错，代码CNGI_E1，请重试，如仍有问题，请联系system@buaabt.cn并附上详细访问信息", 3);
                    return false;
                }
                else
                {
                    ipaddress = System.Web.HttpContext.Current.Request.Headers.Get("X-Forwarded-For");
                    isipv6 = PTTools.IsIPv6(ipaddress);
                }


                //当前状态：①通过CNGI登陆成功，获取userid，②CNGI已经登陆，但是未绑定userid，则只能访问cngilogin.aspx或register.aspx游客页面
            }

            return true;
        }

        /// <summary>
        /// 检查访问所用域名，true正常执行，false终止执行
        /// </summary>
        /// <returns></returns>
        private bool CheckDomainName()
        {
            try
            {
                //if (url.IndexOf("bt.buaa.edu.cn") > -1)
                //{
                //    try
                //    {
                //        System.Web.HttpContext.Current.Response.Redirect("http://buaabt.cn");
                //    }
                //    catch
                //    {
                //        SetMetaRefresh(0, "http://buaabt.cn");
                //        ShowMessage("正在为您自动跳转，如不能自动跳转，请检查Javascript是否开启，或手动访问https://buaabt.cn", 2);
                //    }
                //    return false;
                //}

                //else if (url.IndexOf("www.buaabt.cn") > -1)
                //{
                //    try
                //    {
                //        System.Web.HttpContext.Current.Response.Redirect("http://buaabt.cn");
                //    }
                //    catch
                //    {
                //        SetMetaRefresh(0, "http://buaabt.cn");
                //        ShowMessage("正在为您自动跳转，如不能自动跳转，请检查Javascript是否开启，或手动访问https://buaabt.cn", 2);
                //    }
                //    return false;
                //}

            }
            catch
            {
                ShowMessage("错误的论坛访问，请重试", 3);
                return false;
            }

            return true;

            //else if (url.IndexOf("buaabt.cn") > -1)
            //    System.Web.HttpContext.Current.Response.Redirect("http://bt.buaa6.edu.cn");
            //return;
            //urlred = System.Web.HttpContext.Current.Request.Url.;
            //if (isipv6)
            //{
            //    System.Web.HttpContext.Current.Response.Redirect("http://[xxx.xxx.xxx.xxx]");
            //    //System.Web.HttpContext.Current.Response.Redirect(urlred.Replace("buaabt.cn", "xxx.xxx.xxx.xxx").Replace("bt.buaa.edu.cn", "xxx.xxx.xxx.xxx").Replace("bt.buaa6.edu.cn", "[xxx.xxx.xxx.xxx]"));
            //}
            //else
            //{
            //    System.Web.HttpContext.Current.Response.Redirect("http://xxx.xxx.xxx.xxx");
            //    //System.Web.HttpContext.Current.Response.Redirect(urlred.Replace("buaabt.cn", "xxx.xxx.xxx.xxx").Replace("bt.buaa.edu.cn", "xxx.xxx.xxx.xxx").Replace("bt.buaa6.edu.cn", "xxx.xxx.xxx.xxx"));
            //}

        }

        /// <summary>
        /// 获取用户积分信息列表
        /// </summary>
        /// <param name="tips"></param>
        /// <returns></returns>
        private string GetUserInfoTips(PTUserInfo btuser, string grouptitle)
        {
            if (btuser == null || btuser.Uid < 1) return "";

            string tips = "";

            tips = "<p><a class=\"drop\" onmouseover=\"showMenu(this.id);\" href=\"" + BaseConfigs.GetForumPath + "usercpcreaditstransferlog.aspx\" id=\"extcreditmenu\">积分: " + btuser.Credits + "</a> ";
            tips += "<span class=\"pipe\">|</span>用户组: <a class=\"xi2\" id=\"g_upmine\" href=\"" + BaseConfigs.GetForumPath + "usercp.aspx\">" + grouptitle + "</a></p>";
            tips += "<ul id=\"extcreditmenu_menu\" class=\"p_pop\" style=\"display:none;\">";

            tips += string.Format("<li><a><span class=\"PrivateBTNavList\">威望: {0}</span></a></li>", btuser.Extcredits1);
            tips += string.Format("<li><a><span class=\"PrivateBTNavList\">金币: {0}</span></a></li>", btuser.Extcredits2);
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">统计上传: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits3));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">统计下载: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits4));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">今天统计上传: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits5));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">今天统计下载: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits6));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">真实上传: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits7));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">真实下载: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits8));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">今天真实上传: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits9));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">今天真实下载: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits10));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">保种奖励: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits11));
            //tips += string.Format("<li><a><span class=\"PrivateBTNavList\">今天保种奖励: {0}</span></a></li>", PTTools.Upload2Str(btuser.Extcredits12));

            tips += "</ul>";
            return tips;
            
        }
        /// <summary>
        /// 新手任务警告
        /// </summary>
        /// <param name="btuser"></param>
        /// <returns></returns>
        private string GetNewUserAlert(PTUserInfo btuser)
        {
            if (btuser == null || btuser.Uid < 1) return "";
            else if (btuser.Uid > 0)
            {
                string rtvstr = "";
                int newuserday = (int)(DateTime.Now - DateTime.Parse(btuser.Joindate)).TotalDays;
                if (newuserday > 66 || btuser.Vip > 0)
                {
                    rtvstr = "";
                }
                else if (newuserday < 40)
                {
                    if (btuser.Extcredits4 <= 20 * 1024M * 1024 * 1024)
                    {
                        rtvstr = "&nbsp;&nbsp;<a href=\"showtopic-97755.aspx\"><span style=\"font-weight:bold; color:#C80\" title=\"需要下载 20GB，已完成下载 " + PTTools.Upload2Str(btuserinfo.Extcredits4) + "，请确保在考核结束日期前完成，逾期将被冻结访问\">[ 新手考核：未完成，剩余：" + (60 - newuserday).ToString() + "天 ]</span></a>";
                    }
                    else if (btuser.Ratio < 0.8)
                    {
                        rtvstr = "&nbsp;&nbsp;<a href=\"showtopic-97755.aspx\"><span style=\"font-weight:bold; color:#0A0\">[ 新手考核：已完成 请尽快提高共享率 ]</span></a>";
                    }
                    else
                    {
                        rtvstr = "&nbsp;&nbsp;<a href=\"showtopic-97755.aspx\"><span style=\"font-weight:bold; color:#069\">[ 新手考核：已完成 ]</span></a>";
                    }
                }
                else if ((DateTime.Now - DateTime.Parse(btuser.Joindate)).TotalDays < 67)
                {
                    if (btuser.Extcredits4 <= 20 * 1024M * 1024 * 1024)
                    {
                        newuserday = 60 - newuserday;
                        if (newuserday < 0) newuserday = 0;
                        rtvstr = "&nbsp;&nbsp;<a href=\"showtopic-97755.aspx\"><span style=\"font-weight:bold; color:#F00\" title=\"需要下载 20GB，已完成下载 " + PTTools.Upload2Str(btuserinfo.Extcredits4) + "，请确保在考核结束日期前完成，逾期将被冻结访问\">[ 严重警告 新手考核：未完成，剩余：" + newuserday.ToString() + "天，逾期将被冻结访问 ]</span></a>";
                    }
                    else if (btuser.Ratio < 0.8)
                    {
                        rtvstr = "&nbsp;&nbsp;<a href=\"showtopic-97755.aspx\"><span style=\"font-weight:bold; color:#0A0\">[ 新手考核：已完成 请尽快提高共享率 ]</span></a>";
                    }
                    else
                    {
                        rtvstr = "&nbsp;&nbsp;<a href=\"showtopic-97755.aspx\"><span style=\"font-weight:bold; color:#069\">[ 新手考核：已完成 ]</span></a>";
                    }
                }
                else
                {
                    rtvstr = "";
                }

                if ((btuserinfo.RatioProtection & 1) == 0)
                {
                    rtvstr += "&nbsp;&nbsp;<a href=\"usercpratioprotect.aspx\"><span style=\"font-weight:bold; color:#F00\" title=\"共享率过低将导致完全无法下载，只能拷贝资源续种！取消共享率保护后请务必注意！\">共享率保护关闭，请密切关注共享率变化</span></a>";
                }

                return rtvstr;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// BasePage类构造函数
        /// </summary>
        public PageBase()
        {
            try 
            {
                //页面执行开始计时器
                m_starttick = DateTime.Now;

                #region 页面禁止浏览器缓存，启用服务器输出缓存

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.BufferOutput = true;
                //System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
                System.Web.HttpContext.Current.Response.CacheControl = "private";
                System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                System.Web.HttpContext.Current.Response.Expires = 0;
                //System.Web.HttpContext.Current.Response.Cache.SetNoStore();
                Page.EnableViewState = false;

                #endregion

                #region [DEBUG]清空当前页面查询统计
                //清空当前页面查询统计
#if DEBUG
            Discuz.Data.DbHelper.QueryCount = 0;
            Discuz.Data.DbHelper.QueryDetail = "";
#endif

                #endregion

                //检查访问所用域名
                if (!CheckDomainName()) return;

                #region 加载配置文件

                if (recordPageView)
                    PageViewStatistic(pagename);

                config = GeneralConfigs.GetConfig();
                if (SpacePluginProvider.GetInstance() == null)
                    config.Enablespace = 0;
                if (AlbumPluginProvider.GetInstance() == null)
                    config.Enablealbum = 0;
                if (MallPluginProvider.GetInstance() == null)
                    config.Enablemall = 0;

                LoadUrlConfig();

                #endregion

                //检测CNGI是否登录，若返回False，则结束页面
                //执行后状态：
                // ①通过CNGI登陆成功，获取userid，并更新Cookie信息，但不检查userid相同的cookie密码是否正确，任意页面
                // ②CNGI已经登陆，但是未绑定，userid=0，此情况仅存在于cngilogin.aspx或register.aspx游客页面，
                // ③普通访问状态userid=0，任意页面
                if (!CheckCNGILogin()) return;

                #region 移动设备访问，IP地址获取

                if (ipaddress == "xxx.xxx.xxx.xxx" || ipaddress == "2001:da8:203:502:a8a1:1cad:5de5:47d1")
                {
                    if (!Utils.StrIsNullOrEmpty(System.Web.HttpContext.Current.Request.Headers.Get("X-Forwarded-For")))
                    {
                        ipaddress = System.Web.HttpContext.Current.Request.Headers.Get("X-Forwarded-For");
                        if (ipaddress.IndexOf(",") > 0) ipaddress = ipaddress.Substring(0, ipaddress.IndexOf(","));
                        if (ipaddress.IndexOf(":") > 0) ipaddress = ipaddress.Substring(0, ipaddress.IndexOf(":"));
                        string agent = HttpContext.Current.Request.UserAgent;
                        if (!agent.Contains("Windows NT") && !agent.Contains("Macintosh"))
                        {
                            templateid = 5;
                        }
                        else
                        {
                            try
                            {
                                System.Web.HttpContext.Current.Response.Redirect("www.buaa.edu.cn");
                            }
                            catch(Exception ex)
                            {
                                SetMetaRefresh(0, "www.buaa.edu.cn");
                                AddErrLine("正在跳转");
                                PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Exception, "MobileRedirect", ex.ToString());
                            }
                        }
                    }
                }

                #endregion

                #region 从Cookie中获取userid

                //获取userid
                if (userid < 1)
                {
                    userid = Utils.StrToInt(ForumUtils.GetCookie("userid"), -1);
                }

                //未登录用户跳转到登陆页面（Cookie中不存在用户信息且未CNGI登陆，此时尚未校验Cookie中的用户密码信息）
                if (userid < 0)
                {
                    //【重要】attachupload页面不验证登陆情况。。。否则flash上传会有问题，flash上传不带cookie?
                    if (pagename != "buaasso.aspx" && pagename != "attachupload.aspx" && pagename != "cngilogin.aspx" && pagename != "getpassword.aspx" &&
                        pagename.IndexOf("setnewpassword.aspx") != 0 && pagename != "login.aspx" && pagename.LastIndexOf("register.aspx") != 0 &&
                        pagename != "logout.aspx" && pagename != "activationuser.aspx")
                    {
                        string redirecturl = (IsHttps ? "https://" : "http://") + System.Web.HttpContext.Current.Request.Url.Host +
                            (System.Web.HttpContext.Current.Request.Url.Port == 80 ? "" : (":" + System.Web.HttpContext.Current.Request.Url.Port.ToString())) +
                            "/login.aspx";
                        try
                        {
                            System.Web.HttpContext.Current.Response.Redirect(redirecturl);
                        }
                        catch (Exception ex)
                        {
                            SetMetaRefresh(0, redirecturl);
                            ShowMessage("正在为您自动跳转到登陆页面，如不能自动跳转，请手动访问" + redirecturl, 3);
                            PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Exception, "UseridRedirect", ex.ToString());
                        }
                        return;
                    }
                }

                #endregion

                #region 游客缓存 SEO 等不需要的功能 已经注释掉
                // 如果启用游客页面缓存，则对游客输出缓存页
                //if (userid == -1 && config.Guestcachepagetimeout > 0 && GetUserCachePage(pagename))
                //    return;

                //AddMetaInfo(config.Seokeywords, config.Seodescription, config.Seohead);
                #endregion

                #region 获取或生成在线用户信息

                //当为forumlist.aspx或forumindex.aspx,可能出现在线并发问题,这时系统会延时2秒
                //更新在线用户信息，检查用户密码是否正确，若不正确，则置为游客，uid=-1
                if ((pagename != "forumlist.aspx") && (pagename != "forumindex.aspx"))
                    oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout, ipaddress, userid, password, cngi_login);
                else
                {
                    try
                    {
                        oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout, ipaddress, userid, password, cngi_login);
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(2048);
                        oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout, ipaddress, userid, password, cngi_login);
                    }
                }

                if (oluserinfo.Olid == -2048 && oluserinfo.Username == "[2048]")
                {
                    if (pagename != "sessionajax.aspx")
                    {
                        AddErrLine("您已在其他IP上线，当前登陆信息失效。 ");
                        return;
                    }
                    else
                    {
                        //目前仅供PMCOUNT功能使用
                        ShowPage();
                        return;
                    }
                }

                if (userid < 0)
                {
                    //判断用户Cookie是否正常
                    if (pagename == "forumindex.aspx" || pagename == "index.aspx")
                    {
                        if (DNTRequest.GetString("s") == "login")
                        {
                            string refurl = DNTRequest.GetUrlReferrer();
                            if (refurl.IndexOf("login.aspx") > 0)
                            {
                                ShowMessage("浏览器 Cookie 丢失，登录已失效！<br/>请检查您的浏览器是否启用了 Cookie ！花园需要 Cookie 才能正常工作<br/>建议使用官方原版的IE、Firefox、Chrome浏览器以避免各种莫名其妙的问题", 3);
                                PTLog.InsertSystemLog_WithPageIP(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Error, "CookieLost", string.Format("-UID:{0} -USERNAME:{1}", userid, username));
                                return;
                            }
                            else PTLog.InsertSystemLog_WithPageIP(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Error, "CookieLostFake", string.Format("-UID:{0} -USERNAME:{1}", userid, username));
                        }
                    }
                }

                #endregion

                #region Cookie存储发帖时间 等不需要的功能 已经注释掉
                //如果最后发帖时间cookie不为空，则在此修改用户的该属性
                //if (config.PostTimeStorageMedia == 1 && Utils.GetCookie("lastposttime") != "")
                //    oluserinfo.Lastposttime = Utils.GetCookie("lastposttime");
                #endregion

                #region 更新Pagebase公共变量信息1

                //更新在线用户基本信息，更新完毕后，userid > 0 即为已登录用户，未登录用户userid = 0 ****************************
                userid = oluserinfo.Userid;
                usergroupid = oluserinfo.Groupid;
                username = oluserinfo.Username;
                password = oluserinfo.Password;
                userkey = password.Length > 16 ? password.Substring(4, 8).Trim() : "";
                lastposttime = oluserinfo.Lastposttime;
                lastpostpmtime = oluserinfo.Lastpostpmtime;
                lastsearchtime = oluserinfo.Lastsearchtime;
                olid = oluserinfo.Olid;

                //获取用户组信息，取得用户权限id,1管理员,2超版,3版主,0普通组,-1特殊组
                usergroupinfo = UserGroups.GetUserGroupInfo(usergroupid);
                useradminid = usergroupinfo.Radminid;

                //读取BT用户信息
                if (userid > 0) btuserinfo = PTUsers.GetBtUserInfoForPagebase(userid);

                #endregion

                //校验用户是否可以访问论坛
                if (!ValidateUserPermission()) return;

                #region 更新Pagebase公共变量信息2

                if (userid > 0)
                {
                    //userid>0确保头像可以取到
                    useravatar = Avatars.GetAvatarUrl(userid, AvatarSize.Small);

                    //更新用户在线时长
                    if (!Utils.InArray(pagename, "attachment.aspx"))//加入附件页面判断减少性能消耗
                        OnlineUsers.UpdateOnlineTime(config.Oltimespan, userid);
                }
                //在线用户数 if (!Utils.InArray(pagename, "attachment.aspx"))
                if (Utils.InArray(pagename, "forumindex.aspx"))//仅在forumindex页面需要在线用户数
                    onlineusercount = (userid > 0) ? OnlineUsers.GetOnlineAllUserCount() : OnlineUsers.GetCacheOnlineAllUserCount();

                //页面显示信息，积分显示，导航栏等
                userinfotips = GetUserInfoTips(btuserinfo, usergroupinfo.Grouptitle);
                mainnavigation = Navs.GetNavigationString(userid, useradminid);
                subnavigation = Navs.GetSubNavigation();
                mainnavigationhassub = Navs.GetMainNavigationHasSub();
                pmsound = Utils.StrToInt(ForumUtils.GetCookie("pmsound"), 0);
                timenow = (DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0)).Ticks / 10000;
                nowdate = Utils.GetDate();
                nowtime = Utils.GetTime();
                nowdatetime = Utils.GetDateTime();
                ispost = DNTRequest.IsPost();
                isget = DNTRequest.IsGet();
                link = "";
                script = "";
                newtopicminute = config.Viewnewtopicminute;
                newuseralert = GetNewUserAlert(btuserinfo);
                ipaddress_note = GetIPAddressNote(ipaddress);

                #endregion

                #region 模板 模板和图片JS路径 广告 信息获取

                //模板选择选项
                templatelistboxoptions = Caches.GetTemplateListBoxOptionsCache();
                string originalTemplate = string.Format("<li><a href=\"###\" onclick=\"window.location.href='{0}showtemplate.aspx?templateid={1}'\">",
                           "", BaseConfigs.GetForumPath, templateid);
                string newTemplate = string.Format("<li class=\"current\"><a href=\"###\" onclick=\"window.location.href='{0}showtemplate.aspx?templateid={1}'\">",
                                         BaseConfigs.GetForumPath, templateid);
                templatelistboxoptions = templatelistboxoptions.Replace(originalTemplate, newTemplate);

                //当前选择的模板
                if (templateid != 5)
                {
                    if (Utils.InArray(DNTRequest.GetString("selectedtemplateid"), Templates.GetValidTemplateIDList()))
                        templateid = DNTRequest.GetInt("selectedtemplateid", 0);
                    else if (Utils.InArray(Utils.GetCookie(Utils.GetTemplateCookieName()), Templates.GetValidTemplateIDList()))
                        templateid = Utils.StrToInt(Utils.GetCookie(Utils.GetTemplateCookieName()), config.Templateid);

                    if (templateid == 0)
                        templateid = config.Templateid;
                }

                //【外网57代理，手机版专用】来自外网的访问，一律变为5
                if ((System.Web.HttpContext.Current.Request.UserHostAddress == "xxx.xxx.xxx.xxx" || System.Web.HttpContext.Current.Request.UserHostAddress == "2001:da8:203:502:a8a1:1cad:5de5:47d1") && templateid != 5
                    && System.Web.HttpContext.Current.Request.Url.Host != "ipv6.buaabt.cn" && System.Web.HttpContext.Current.Request.Url.Host != "buaabt.cn"
                    && System.Web.HttpContext.Current.Request.Url.Host != "tracker4.buaabt.cn" && System.Web.HttpContext.Current.Request.Url.Host != "tracker6.buaabt.cn"
                    && !HttpContext.Current.Request.UserAgent.Contains("FGBT AutoSeeder"))
                {
                    templateid = 5;
                    try
                    {
                        HttpContext.Current.Response.Redirect("http://www.buaa6.edu.cn");
                    }
                    catch
                    {
                        SetMetaRefresh(0, "http://www.buaa6.edu.cn");
                        AddErrLine("您无权访问此页面！");
                        ShowMessage("您无权访问此页面！", 2);
                    }
                }
                //手机版页面校验
                if (templateid == 5 && System.Web.HttpContext.Current.Request.Url.Host != "buaabt.cn" && !HttpContext.Current.Request.UserAgent.Contains("FGBT AutoSeeder"))
                {
                    if (pagename != "login.aspx" && pagename != "forumindex.aspx" && pagename != "showforum.aspx" && pagename != "showtopic.aspx" && pagename != "userinfo.aspx" && pagename != "posttopic.aspx" &&
                        pagename != "postreply.aspx" && pagename != "attachment.aspx" && pagename != "usercpinbox.aspx" && pagename != "usercpshowpm.aspx" && pagename != "usercpnotice.aspx" &&
                        pagename != "usercppostpm.aspx" && pagename != "logout.aspx" && pagename != "editpost.aspx" && pagename != "edittopic.aspx" && pagename != "showseeds.aspx" &&
                        pagename != "buytopic.aspx" && pagename != "usercpsentbox.aspx" && pagename != "index.aspx")
                    {
                        try
                        {
                            HttpContext.Current.Response.Redirect("http://www.buaa6.edu.cn");
                        }
                        catch
                        {
                            SetMetaRefresh(0, "http://www.buaa6.edu.cn");
                            AddErrLine("错误的页面访问，访问被禁止！");
                            ShowMessage("错误的页面访问，访问被禁止！", 2);
                        }
                        //return;
                    }
                }

                //模板路径
                TemplateInfo templateInfo = Templates.GetTemplateItem(templateid);
                templatepath = templateInfo.Directory;
                if (templateInfo.Templateurl.ToLower().StartsWith("http://"))
                {
                    imagedir = templateInfo.Templateurl.TrimEnd('/') + "/images";
                    cssdir = templateInfo.Templateurl.TrimEnd('/');
                }
                else
                {
                    imagedir = forumpath + "templates/" + templateInfo.Directory + "/images";
                    cssdir = forumpath + "templates/" + templateInfo.Directory;
                }

                //帖子鉴定路径
                //if (EntLibConfigs.GetConfig() != null && !Utils.StrIsNullOrEmpty(EntLibConfigs.GetConfig().Topicidentifydir))
                //    topicidentifydir = EntLibConfigs.GetConfig().Topicidentifydir.TrimEnd('/');
                //else
                topicidentifydir = forumpath + "images/identify";

                //帖子图标路径
                //if (EntLibConfigs.GetConfig() != null && !Utils.StrIsNullOrEmpty(EntLibConfigs.GetConfig().Posticondir))
                //    posticondir = EntLibConfigs.GetConfig().Posticondir.TrimEnd('/');
                //else
                posticondir = forumpath + "images/posticons";

                //Javascript路径
                //if (EntLibConfigs.GetConfig() != null && !Utils.StrIsNullOrEmpty(EntLibConfigs.GetConfig().Jsdir))
                //    jsdir = EntLibConfigs.GetConfig().Jsdir.TrimEnd('/');
                //else
                jsdir = rooturl + "javascript";

                //设定当前页面的显示样式（宽窄），强制允许选择
                //if (config.Allowchangewidth == 0)
                //    Utils.WriteCookie("allowchangewidth", "");

                if (pagename != "website.aspx")
                {
                    if (Utils.GetCookie("allowchangewidth") == "0" || (string.IsNullOrEmpty(Utils.GetCookie("allowchangewidth")) && config.Showwidthmode == 1))
                        isnarrowpage = true;
                }

                //广告信息
                headerad = Advertisements.GetOneHeaderAd("", 0);
                footerad = Advertisements.GetOneFooterAd("", 0);

                #endregion

                //校验验证码
                if (!ValidateVerifyCode()) return;

                #region  执行页面

                try { ShowPage(); }
                catch (System.Exception ex)
                {
                    PTLog.InsertSystemLog(PTLog.LogType.PageBaseOnInit, PTLog.LogStatus.Exception, "BASE SHOWPAGE", System.Web.HttpContext.Current.Request.UserHostAddress + " -PAGE:" + System.Web.HttpContext.Current.Request.RawUrl + " -EX:" + ex.ToString());
                    if (!ispost)
                    {
                        try { ShowPage(); }
                        catch (System.Exception ex1)
                        {
                            AddErrLine("页面发生了错误，请重试");
                            PTLog.InsertSystemLog(PTLog.LogType.PageBaseOnInit, PTLog.LogStatus.Exception, "BASE SHOWPAGE2", System.Web.HttpContext.Current.Request.UserHostAddress + " -PAGE:" + System.Web.HttpContext.Current.Request.RawUrl + " -EX:" + ex1.ToString());
                        }
                    }
                    else AddErrLine("页面发生了错误，请重试");
                }

                #endregion

                #region 性能统计和数据库执行统计
                querycount = Discuz.Data.DbHelper.QueryCount;
                Discuz.Data.DbHelper.QueryCount = 0;

#if DEBUG
                //只在本地访问时显示，避免操作失误造成数据库执行统计被显示
                if (ipaddress == "::1" || ipaddress == "127.0.0.1")
                {
                    querydetail = Discuz.Data.DbHelper.QueryDetail;
                }
                Discuz.Data.DbHelper.QueryDetail = "";

#endif

                //页面执行时间（不包括显示层？）
                m_processtime = DateTime.Now.Subtract(m_starttick).TotalMilliseconds / 1000;

                if (pagename != "attachment.aspx" && pagename != "downloadseed.aspx" && pagename != "downloadseedrssd.aspx")
                {
                    if (m_processtime > 4) PTLog.InsertSystemLog_WithPageIP(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Warning, "PageSlow3", string.Format("页面执行异常缓慢：{0} -UID:{1} -USERNAME:{2}", m_processtime, userid, username), true);
                    else if (m_processtime > 1) PTLog.InsertSystemLog_WithPageIP(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Warning, "PageSlow1", string.Format("页面执行缓慢：{0} -UID:{1} -USERNAME:{2}", m_processtime, userid, username), true);
                }
                
                #endregion
            }
            catch (System.Exception ex) { PTLog.InsertSystemLog_WithPageIP(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Exception, "BASE PAGEBASE", ex.ToString()); }    
        }

        #region 子方法


        /// <summary>
        /// 输出Ajax验证码错误信息
        /// </summary>
        private static void ResponseAjaxVcodeError()
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "Text/XML";
            System.Web.HttpContext.Current.Response.Expires = 0;

            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Text.StringBuilder xmlnode = new System.Text.StringBuilder();
            xmlnode.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
            xmlnode.Append("<error>验证码错误</error>");
            System.Web.HttpContext.Current.Response.Write(xmlnode.ToString());
            PageBase_ResponseEnd();
        }



        /// <summary>
        /// 设置页面定时转向
        /// </summary>
        public void SetMetaRefresh()
        {
            SetMetaRefresh(2, msgbox_url);
        }

        /// <summary>
        /// 设置页面定时转向
        /// </summary>
        /// <param name="sec">时间(秒)</param>
        public void SetMetaRefresh(int sec)
        {
            SetMetaRefresh(sec, msgbox_url);
        }

        /// <summary>
        /// 设置页面定时转向
        /// </summary>
        /// <param name="sec">时间(秒)</param>
        /// <param name="url">转向地址</param>
        public void SetMetaRefresh(int sec, string url)
        {
            if (infloat != 1)
            {
                meta = meta + "\r\n<meta http-equiv=\"refresh\" content=\"" + sec.ToString() + "; url=" + url + "\" />";
                meta = meta + "\r\n<script type=\"text/javascript\">setTimeout(function(){location = \"" + url + "\";}, " + (sec * 1000).ToString() + ");</script>";
            }
        }

        /// <summary>
        /// 插入指定路径的CSS
        /// </summary>
        /// <param name="url">CSS路径</param>
        public void AddLinkCss(string url)
        {
            link = link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />";//测试link
        }

        public void AddLinkRss(string url, string title)
        {
            link = link + "\r\n<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" + Utils.RemoveHtml(title) + "\" href=\"" + url + "\" />";
        }


        /// <summary>
        /// 插入指定路径的CSS
        /// </summary>
        /// <param name="url">CSS路径</param>
        public void AddLinkCss(string url, string linkid)
        {
            link = link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" id=\"" + linkid + "\" />";
        }

        /// <summary>
        /// 插入脚本内容到页面head中
        /// </summary>
        /// <param name="scriptstr">不包括<script></script>的脚本主体字符串</param>
        public void AddScript(string scriptstr)
        {
            AddScript(scriptstr, "javascript");
        }

        /// <summary>
        /// 插入脚本内容到页面head中
        /// </summary>
        /// <param name="scriptstr">不包括<script>
        /// <param name="scripttype">脚本类型(值为：vbscript或javascript,默认为javascript)</param>
        public void AddScript(string scriptstr, string scripttype)
        {
            if (!scripttype.ToLower().Equals("vbscript") && !scripttype.ToLower().Equals("vbscript"))
            {
                scripttype = "javascript";
            }
            script = script + "\r\n<script type=\"text/" + scripttype + "\">" + scriptstr + "</script>";
        }

        /// <summary>
        /// 插入指定Meta
        /// </summary>
        /// <param name="metastr">Meta项</param>
        public void AddMeta(string metastr)
        {
            meta = meta + "\r\n<meta " + metastr + " />";
        }

        /// <summary>
        /// 更新页面Meta
        /// </summary>
        /// <param name="Seokeywords">关键词</param>
        /// <param name="Seodescription">说明</param>
        /// <param name="Seohead">其它增加项</param>
        public void UpdateMetaInfo(string Seokeywords, string Seodescription, string Seohead)
        {
            string[] metaArray = Utils.SplitString(meta, "\r\n");
            //设置为空,并在下面代码中进行重新赋值
            meta = "";
            foreach (string metaoption in metaArray)
            {
                //找出keywords关键字
                if (metaoption.ToLower().IndexOf("name=\"keywords\"") > 0)
                {
                    if (Seokeywords != null && Seokeywords.Trim() != "")
                    {
                        meta += "<meta name=\"keywords\" content=\"" + Utils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
                        continue;
                    }
                }

                //找出description关键字
                if (metaoption.ToLower().IndexOf("name=\"description\"") > 0)
                {
                    if (Seodescription != null && Seodescription.Trim() != "")
                    {
                        meta += "<meta name=\"description\" content=\"" + Utils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
                        continue;
                    }
                }

                meta = meta + metaoption + "\r\n";
            }
        }


        /// <summary>
        /// 添加页面Meta信息
        /// </summary>
        /// <param name="Seokeywords">关键词</param>
        /// <param name="Seodescription">说明</param>
        /// <param name="Seohead">其它增加项</param>
        public void AddMetaInfo(string Seokeywords, string Seodescription, string Seohead)
        {
            if (Seokeywords != "")
            {
                meta = meta + "<meta name=\"keywords\" content=\"" + Utils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
            }
            if (Seodescription != "")
            {
                meta = meta + "<meta name=\"description\" content=\"" + Utils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
            }
            meta = meta + Seohead;
        }

        /// <summary>
        /// 增加错误提示
        /// </summary>
        /// <param name="errinfo">错误提示信息</param>
        public void AddErrLine(string errinfo)
        {
            if (msgbox_text.Length == 0)
            {
                msgbox_text = msgbox_text + errinfo;
            }
            else
            {
                msgbox_text = msgbox_text + "<br />" + errinfo;
            }
            page_err++;
        }

        /// <summary>
        /// 增加提示信息
        /// </summary>
        /// <param name="strinfo">提示信息</param>
        public void AddMsgLine(string strinfo)
        {
            if (msgbox_text.Length == 0)
            {
                msgbox_text = msgbox_text + strinfo;
            }
            else
            {
                msgbox_text = msgbox_text + "<br />" + strinfo;
            }
        }

        /// <summary>
        /// 增加提示信息（不需要提示信息的页面直接跳转，一般不要使用此功能，会造成用户困扰）
        /// </summary>
        /// <param name="strinfo">提示信息</param>
        public void MsgForward(string forwardName, bool spJump)
        {
            if (config.Quickforward == 1 && infloat == 0 && Utils.InArray(forwardName, config.Msgforwardlist))
                System.Web.HttpContext.Current.Response.Redirect(spJump ? msgbox_url : forumpath + msgbox_url, true);
        }

        public void MsgForward(string forwardName)
        {
            MsgForward(forwardName, false);
        }



        /// <summary>
        /// 格式化字节格式
        /// </summary>
        /// <param name="byteStr"></param>
        /// <returns></returns>
        public string FormatBytes(double bytes)
        {
            if (bytes > 1073741824)
            {
                return ((Math.Round((bytes / 1073741824) * 100) / 100).ToString() + " G");
            }
            else if (bytes > 1048576)
            {
                return ((Math.Round((bytes / 1048576) * 100) / 100).ToString() + " M");
            }
            else if (bytes > 1024)
            {
                return ((Math.Round((bytes / 1024) * 100) / 100).ToString() + " K");
            }
            else
            {
                return (bytes.ToString() + " Bytes");
            }
        }

        /// <summary>
        /// 格式化字节格式
        /// </summary>
        /// <param name="byteStr"></param>
        /// <returns></returns>
        public string FormatBytes(string byteStr)
        {
            return FormatBytes((double)TypeConverter.StrToInt(byteStr));
        }


        /// <summary>
        /// 是否已经发生错误
        /// </summary>
        /// <returns>有错误则返回true, 无错误则返回false</returns>
        public bool IsErr()
        {
            return page_err > 0;
        }

        /// <summary>
        /// 设置要转向的url
        /// </summary>
        /// <param name="strurl">要转向的url</param>
        public void SetUrl(string strurl)
        {
            msgbox_url = strurl;
        }
        /// <summary>
        /// 设置回退链接的内容
        /// </summary>
        /// <param name="strback">回退链接的内容</param>
        public void SetBackLink(string strback)
        {
            msgbox_backlink = strback;
        }

        /// <summary>
        /// 设置是否显示回退链接
        /// </summary>
        /// <param name="link">要显示则为true, 否则为false</param>
        public void SetShowBackLink(bool link)
        {
            if (link)
            {
                msgbox_showbacklink = "true";
            }
            else
            {
                msgbox_showbacklink = "false";
            }
        }

        public void ShowMessage(byte mode)
        {
            ShowMessage("", mode);
        }

        /// <summary>
        /// 显示“白屏”简单错误信息1.论坛已关闭，2.禁止访问，3.提示
        /// </summary>
        /// <param name="hint"></param>
        /// <param name="mode">1.论坛已关闭，2.禁止访问，3.提示</param>
        public void ShowMessage(string hint, byte mode)
        {
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //【BT修改】试图寻找各种异常的来源
            //

            try
            {
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head><title>");
                string title;
                string body;
                switch (mode)
                {
                    case 1:
                        title = "论坛已关闭";
                        body = config.Closedreason;
                        break;
                    case 2:
                        title = "禁止访问";
                        body = hint;
                        break;
                    default:
                        title = "提示";
                        body = hint;
                        break;
                }
                System.Web.HttpContext.Current.Response.Write(title);
                System.Web.HttpContext.Current.Response.Write(" - ");
                System.Web.HttpContext.Current.Response.Write("未来花园BT - Powered by Discuz!NT</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                System.Web.HttpContext.Current.Response.Write(meta);
                System.Web.HttpContext.Current.Response.Write("<style type=\"text/css\"><!-- body { margin: 20px; font-family: Tahoma, Verdana; font-size: 14px; color: #333333; background-color: #FFFFFF; }a {color: #1F4881;text-decoration: none;}--></style></head><body><div style=\"border: #cccccc solid 1px; padding: 20px; width: 500px; margin:auto\" align=\"center\">");
                System.Web.HttpContext.Current.Response.Write(body);
                System.Web.HttpContext.Current.Response.Write("</div><br /><br /><br /><div style=\"border: 0px; padding: 0px; width: 500px; margin:auto\"><strong>当前服务器时间:</strong> ");
                System.Web.HttpContext.Current.Response.Write(Utils.GetDateTime());
                System.Web.HttpContext.Current.Response.Write("<br /><strong>当前页面</strong> ");
                System.Web.HttpContext.Current.Response.Write(pagename);
                System.Web.HttpContext.Current.Response.Write("<br /><strong>可选择操作:</strong> ");
                if (userkey == "")
                {
                    System.Web.HttpContext.Current.Response.Write("<a href=");
                    System.Web.HttpContext.Current.Response.Write(forumpath);
                    System.Web.HttpContext.Current.Response.Write("login.aspx>登录</a> | <a href=");
                    System.Web.HttpContext.Current.Response.Write(forumpath);
                    System.Web.HttpContext.Current.Response.Write("register.aspx>注册</a>");
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<a href=\"logout.aspx?userkey=" + userkey + "\">退出</a>");
                    if (useradminid == 1)
                    {
                        System.Web.HttpContext.Current.Response.Write(" | <a href=\"logout.aspx?userkey=" + userkey + "\">系统管理</a>");
                    }
                }
                System.Web.HttpContext.Current.Response.Write("</div></body></html>");
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                
            }
            catch (System.Exception ex)
            {
                //【BT修改】试图寻找各种异常的来源
                PTLog.InsertSystemLog(PTLog.LogType.PageBaseLog, PTLog.LogStatus.Exception, "PAGEBASE-SHOWMESSAGE ERROR", string.Format("{0} - {1} - {2}", hint, mode, ex.ToString()));
            }
            
            //PageBase_ResponseEnd();

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// 得到当前页面的载入时间供模板中调用(单位:毫秒)
        /// </summary>
        /// <returns>当前页面的载入时间</returns>
        public double Processtime
        {
            get { return m_processtime; }
        }

        #endregion


        /// <summary>
        /// 判断今天是否已经签到过
        /// </summary>
        /// <returns></returns>
        protected bool IsHaveCheckIn()
        {
            if (DateTime.Now.Date == oluserinfo.LastCheckInTime.Date) return true;
            else return false;
        }

        /// <summary>
        /// 页面处理虚方法
        /// </summary>
        protected virtual void ShowPage()
        {
            return;
        }

        /// <summary>
        /// OnUnload事件处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnload(EventArgs e)
        {
            //【所有页面执行完毕都会调用此函数？？？】
            //if (isguestcachepage == 1)
            //{
            //    //游客缓存页面，禁止游客缓存？
            //    switch (pagename)
            //    {
            //        case "index.aspx":
            //            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            //            //Discuz.Cache.ICacheStrategy ics = new ForumCacheStrategy();
            //            //ics.TimeOut = config.Guestcachepagetimeout * 60;
            //            //cache.LoadCacheStrategy(ics);
            //            string str = cache.RetrieveObject("/Forum/GuestCachePage/" + pagename) as string;
            //            if (str == null && templateBuilder.Length > 1 && templateid == config.Templateid)
            //            {
            //                templateBuilder.Append("\r\n\r\n<!-- Discuz!NT CachedPage (Created: " + Utils.GetDateTime() + ") -->");
            //                cache.AddObject("/Forum/GuestCachePage/" + pagename, templateBuilder.ToString());
            //            }
            //            //cache.LoadDefaultCacheStrategy();

            //            break;

            //        case "showtopic.aspx":
            //            {
            //                int topicid = DNTRequest.GetQueryInt("topicid", 0);
            //                int pageid = DNTRequest.GetQueryInt("page", 1);
            //                //参数数目为2或0表示当前页面可能为第一页帖子列表
            //                if ((DNTRequest.GetParamCount() == 2 || DNTRequest.GetParamCount() == 3) && topicid > 0 && templateid == config.Templateid)
            //                {
            //                    ForumUtils.CreateShowTopicCacheFile(topicid, pageid, templateBuilder.ToString());
            //                }
            //                break;
            //            }
            //        case "showforum.aspx":
            //            {
            //                int forumid = DNTRequest.GetQueryInt("forumid", 0);
            //                int pageid = DNTRequest.GetQueryInt("page", 1);
            //                //参数数目为2或0表示当前页面可能为第一页帖子列表
            //                if ((DNTRequest.GetParamCount() == 2 || DNTRequest.GetParamCount() == 3) && forumid > 0 && templateid == config.Templateid)
            //                {
            //                    ForumUtils.CreateShowForumCacheFile(forumid, pageid, templateBuilder.ToString());
            //                }
            //                break;
            //            }
            //        default:
            //            //
            //            break;
            //    }
            //}

#if DEBUG
            
            {
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Write(templateBuilder.Replace("</body>", "<div>注意: 以下为数据查询分析工具，正式站点使用请使用官方发布版本或自行Release编译。</div>" + querydetail + "</body>").ToString());
                System.Web.HttpContext.Current.Response.End();
                //C//此处不能使用函数调用？？？PageBase_ResponseEnd();
            }
#else
            {
                try
                {
                    System.Web.HttpContext.Current.Response.Flush();
                }
                catch
                {
                }
            }
#endif
            base.OnUnload(e);
        }

        /// <summary>
        /// 控件初始化时计算执行时间
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //if (isguestcachepage == 1)
            //{
            //    m_processtime = 0;
            //}
            try { base.OnInit(e); }
            catch (System.Exception ex) { PTLog.InsertSystemLog(PTLog.LogType.PageBaseOnInit, PTLog.LogStatus.Exception, "BASE OnInit", ex.ToString()); } 
        }

        protected string aspxrewriteurl = "";

        #region aspxrewrite 配置

        /// <summary>
        /// 设置关于showforum页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowForumAspxRewrite(string forumid, int pageid)
        {
            return ShowForumAspxRewrite(Utils.StrToInt(forumid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }


        protected string ShowForumAspxRewrite(int forumid, int pageid)
        {
            return Urls.ShowForumAspxRewrite(forumid, pageid);
        }

        protected string ShowForumAspxRewrite(string pathlist, int forumid, int pageid)
        {
            return Urls.ShowForumAspxRewrite(pathlist, forumid, pageid);
        }

        protected string ShowForumAspxRewrite(int forumid, int pageid, string rewritename)
        {
            return Urls.ShowForumAspxRewrite(forumid, pageid, rewritename);
        }

        /// <summary>
        /// 设置关于showtopic页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowTopicAspxRewrite(string topicid, int pageid)
        {
            return ShowTopicAspxRewrite(Utils.StrToInt(topicid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }

        protected string ShowTopicAspxRewrite(int topicid, int pageid)
        {
            return Urls.ShowTopicAspxRewrite(topicid, pageid);
        }

        protected string ShowDebateAspxRewrite(string topicid)
        {
            return ShowDebateAspxRewrite(Utils.StrToInt(topicid, 0));
        }

        protected string ShowDebateAspxRewrite(int topicid)
        {
            return Urls.ShowDebateAspxRewrite(topicid);
        }

        /// <summary>
        /// 设置关于showbonus页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowBonusAspxRewrite(string topicid, int pageid)
        {
            return ShowBonusAspxRewrite(Utils.StrToInt(topicid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }

        /// <summary>
        /// 设置关于showbonus页面链接的显示样式
        /// </summary>
        /// <param name="topicid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowBonusAspxRewrite(int topicid, int pageid)
        {
            return Urls.ShowBonusAspxRewrite(topicid, pageid);
        }


        protected string UserInfoAspxRewrite(int userid)
        {
            return Urls.UserInfoAspxRewrite(userid);
        }

        /// <summary>
        /// 设置关于userinfo页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string UserInfoAspxRewrite(string userid)
        {
            return UserInfoAspxRewrite(Utils.StrToInt(userid, 0));
        }

        protected string RssAspxRewrite(int forumid)
        {
            return Urls.RssAspxRewrite(forumid);
        }

        /// <summary>
        /// 设置关于userinfo页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string RssAspxRewrite(string forumid)
        {
            return RssAspxRewrite(Utils.StrToInt(forumid, 0));
        }

        /// <summary>
        /// 设置关于showgoods页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowGoodsAspxRewrite(string goodsid)
        {
            return ShowGoodsAspxRewrite(Utils.StrToInt(goodsid, 0));
        }

        protected string ShowGoodsAspxRewrite(int goodsid)
        {
            return Urls.ShowGoodsAspxRewrite(goodsid);
        }

        /// <summary>
        /// 设置关于showgoods页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowGoodsListAspxRewrite(string categoryid, int pageid)
        {
            return ShowGoodsListAspxRewrite(Utils.StrToInt(categoryid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }

        protected string ShowGoodsListAspxRewrite(int categoryid, int pageid)
        {
            return Urls.ShowGoodsListAspxRewrite(categoryid, pageid);
        }
        #endregion

    }
}
