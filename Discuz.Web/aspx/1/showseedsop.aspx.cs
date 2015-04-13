using System;
using System.Data;
using System.Text;
using System.Web;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Plugin.Mall;

namespace Discuz.Web
{
    /// <summary>
    /// 查看版块页面
    /// </summary>
    public class showseedsop : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 当前版块在线用户列表
        /// </summary>
        public List<OnlineUserInfo> onlineuserlist;
        /// <summary>
        /// 主题列表
        /// </summary>
        public Discuz.Common.Generic.List<TopicInfo> topiclist = new Discuz.Common.Generic.List<TopicInfo>();
        /// <summary>
        /// 置顶主题列表
        /// </summary>
        public Discuz.Common.Generic.List<TopicInfo> toptopiclist = new Discuz.Common.Generic.List<TopicInfo>();
        /// <summary>
        /// 子版块列表
        /// </summary>
        public List<IndexPageForumInfo> subforumlist;
        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist;
        /// <summary>
        /// 在线图例列表
        /// </summary>
        public string onlineiconlist = Caches.GetOnlineGroupIconList();
        /// <summary>
        /// 公告列表
        /// </summary>
        public DataTable announcementlist = Announcements.GetSimplifiedAnnouncementList(Utils.GetDateTime(), "2999-01-01 00:00:00");
        /// <summary>
        /// 页内文字广告
        /// </summary>
        public string[] pagewordad = new string[0];
        /// <summary>
        /// 页内横幅广告
        /// </summary>
        public List<string> pagead = new List<string>();
        /// <summary>
        /// 对联广告
        /// </summary>
        public string doublead;
        /// <summary>
        /// 浮动广告
        /// </summary>
        public string floatad;
        /// <summary>
        /// Silverlight广告
        /// </summary>
        public string mediaad;
        /// <summary>
        /// 快速发帖广告
        /// </summary>
        public string quickeditorad = "";
        /// <summary>
        /// 快速编辑器背景广告
        /// </summary>
        public string[] quickbgad;
        /// <summary>
        /// 当前版块信息
        /// </summary>
        public ForumInfo forum = new ForumInfo();
        /// <summary>
        /// 购买主题积分策略
        /// </summary>
        public UserExtcreditsInfo topicextcreditsinfo = new UserExtcreditsInfo();
        /// <summary>
        /// 悬赏积分策略
        /// </summary>
        public UserExtcreditsInfo bonusextcreditsinfo = new UserExtcreditsInfo();
        /// <summary>
        /// 当前版块总在线用户数
        /// </summary>
        public int forumtotalonline;
        /// <summary>
        /// 当前版块总在线注册用户数
        /// </summary>
        public int forumtotalonlineuser;
        /// <summary>
        /// 当前版块总在线游客数
        /// </summary>
        public int forumtotalonlineguest;
        /// <summary>
        /// 当前版块在线隐身用户数
        /// </summary>
        public int forumtotalonlineinvisibleuser;
        /// <summary>
        /// 当前版块ID
        /// </summary>
        public int forumid = DNTRequest.GetInt("forumid", -1);
        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav = "";
        /// <summary>
        /// 是否显示版块密码提示 1为显示, 0不显示
        /// </summary>
        public int showforumlogin;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = DNTRequest.GetInt("page", 1);
        /// <summary>
        /// 当前版块列表页码
        /// </summary>
        public int forumpageid = DNTRequest.GetInt("page", 1);
        /// <summary>
        /// 主题总数
        /// </summary>
        public int topiccount = 0;
        /// <summary>
        /// 分页总数
        /// </summary>
        public int pagecount = 1;
        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers = "";
        /// <summary>
        /// 置顶主题数
        /// </summary>
        public int toptopiccount = 0;
        /// <summary>
        /// 版块跳转链接选项
        /// </summary>
        public string forumlistboxoptions;
        /// <summary>
        /// 是否显示在线列表
        /// </summary>
        public bool showforumonline = false;
        /// <summary>
        /// 是否受发帖控制限制
        /// </summary>
        public int disablepostctrl = 0;
        /// <summary>
        /// 是否解析URL
        /// </summary>
        public int parseurloff = 0;
        /// <summary>
        /// 是否解析表情
        /// </summary>
        public int smileyoff;
        /// <summary>
        /// 是否解析 Discuz!NT 代码
        /// </summary>
        public int bbcodeoff;
        /// <summary>
        /// 是否使用签名
        /// </summary>
        public int usesig = ForumUtils.GetCookie("sigstatus") == "0" ? 0 : 1;
        /// <summary>
        /// 每页显示主题数
        /// </summary>
        public int tpp = TypeConverter.StrToInt(ForumUtils.GetCookie("tpp"));
        /// <summary>
        /// 每页显示帖子数
        /// </summary>
        public int ppp = TypeConverter.StrToInt(ForumUtils.GetCookie("ppp"));
        /// <summary>
        /// 是否是管理者
        /// </summary>
        public bool ismoder = false;
        /// <summary>
        /// 主题分类选项
        /// </summary>
        public string topictypeselectoptions;
        /// <summary>
        /// 主题分类Id
        /// </summary>
        public int topictypeid = DNTRequest.GetInt("typeid", -1);
        /// <summary>
        /// 过滤主题类型
        /// </summary>
        public string filter = Utils.HtmlEncode(DNTRequest.GetString("filter"));
        /// <summary>
        /// 是否允许发表主题
        /// </summary>
        public bool canposttopic = false;
        /// <summary>
        /// 是否允许快速发表主题
        /// </summary>
        public bool canquickpost = false;
        /// <summary>
        /// 是否显示需要登录后访问的错误提示
        /// </summary>
        public bool needlogin = false;
        /// <summary>
        /// 排序方式
        /// </summary>
        public int order = DNTRequest.GetInt("order", 1); //排序字段
        /// <summary>
        /// 时间范围
        /// </summary>
        public int interval = DNTRequest.GetInt("interval", 0);
        /// <summary>
        /// 排序方向
        /// </summary>
        public int direct = DNTRequest.GetInt("direct", 1); //排序方向[默认：降序]      
        /// <summary>
        /// 获取绑定相关版块的商品分类信息
        /// </summary>
        public string goodscategoryfid = GeneralConfigs.GetConfig().Enablemall <= 0 ? "{}" : Discuz.Plugin.Mall.MallPluginProvider.GetInstance().GetGoodsCategoryWithFid();
        /// <summary>
        /// 当前版块的主题类型链接串
        /// </summary>
        public string topictypeselectlink;
        /// <summary>
        /// 下一页
        /// </summary>
        public string nextpage = "";
        /// <summary>
        /// 弹出导航菜单的HTML代码
        /// </summary>
        public string navhomemenu = "";
        /// <summary>
        /// 获取访问过的版块列表
        /// </summary>
        public SimpleForumInfo[] visitedforums = Forums.GetVisitedForums();
        /// <summary>
        /// 是否显示访问过的版块列表菜单
        /// </summary>
        public bool showvisitedforumsmenu = true;
        /// <summary>
        /// 当前用户是否在新手见习期
        /// </summary>
        public bool isnewbie = false;
        //private string msg = "";//提示信息

        //private string condition = ""; //查询条件

        //private string orderStr = "";//排序方式

        public int topicid = 0;

        public bool needaudit = false;


        #endregion

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //【BT修改】种子显示相关变量

        public List<PTSeedOPinfo> seedopinfolist = new List<PTSeedOPinfo>();
        public List<PTLogMessage> logmessagelist = new List<PTLogMessage>();

        public int seedopinfocount = 0;
        /// <summary>
        /// 种子分类，1电影，2剧集....
        /// </summary>
        public int seedtype = DNTRequest.GetInt("seedtype", -1);

        //操作记录页面
        public int operatorid = DNTRequest.GetInt("operatorid", -1);
        public int seeduserid = DNTRequest.GetInt("seeduserid", -1);
        public int operattype = DNTRequest.GetInt("operattype", -1);
        public string showmode = DNTRequest.GetString("s");

        public int logtype = DNTRequest.GetInt("type", 0);
        public int logstatus = DNTRequest.GetInt("status", -1);
        public string logaction = DNTRequest.GetString("action");
        public string timestart = DNTRequest.GetString("start");
        public string timeend = DNTRequest.GetString("end");

        /// <summary>
        /// 种子状态，1活种，2IPv4，3IPv6，4死种，0全部
        /// </summary>
        //public int seedstat = 0;
        /// <summary>
        /// 排序：0种子id，1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间
        /// </summary>
        //public int orderby = 0;
        /// <summary>
        /// 排序正反
        /// </summary>
        //public bool asc = false;
        /// <summary>
        /// 用户状态：1上传，2下载，3发布，4完成
        /// </summary>
        //public int userstat = 0;
        //public int[] toparray;
        //public int[] uploadarray;
        //public int[] downloadarray;
        //public int[] finishedarray;

        //public string totalsize = "";
        //public string keywords = "";
        //public string searchstat = "";
        public int numperpage = 50;
        public string pageurl = "";

        public string searchusername = "";  //按用户名搜索种子发布者
        public int searchuserid = -1;       //按用户名搜索种子发布者，id
        public string searchoperator = "";  //按用户名搜索操作者
        public int searchoperatorid = -1;   //按用户名搜索操作者，id


        public string preurl = ""; //前一页链接
        public string nexturl = ""; //下一页链接
        public string prepage = "";//上一页按钮
        //public string typedescription = "";


        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        protected override void ShowPage()
        {
            
            #region 前置信息获取及访问权限校验

            GetPostAds(forumid);

            forumnav = "";
            if (userid < 1)
            {
                AddErrLine("您尚未登录");
                return;
            }

            //只允许版主和管理员查看此页面
            if (userid < 1 || useradminid < 1)
            {
                AddErrLine("对不起，您无权查看此页面");
                return;
            }
            if (userid > 0 && useradminid > 0)
            {
                AdminGroupInfo admingroupinfo = AdminGroups.GetAdminGroupInfo(usergroupid);
                if (admingroupinfo != null)
                    disablepostctrl = admingroupinfo.Disablepostctrl;
            }

            #endregion

            if (showmode == "")
            {
                #region 获取版块信息

                bool DoNotSearch = false;

                if (seedtype < 0) seedtype = 0;
                forumid = PrivateBT.Type2Forum(seedtype);

                if (forumid == -1)
                {
                    AddLinkRss(forumpath + "tools/rss.aspx", "最新主题");
                    AddErrLine("无效的版块ID");
                    return;
                }
                forum = Forums.GetForumInfo(forumid);
                if (forum == null || forum.Fid < 1)
                {
                    if (config.Rssstatus == 1)
                        AddLinkRss(forumpath + "tools/rss.aspx", Utils.EncodeHtml(config.Forumtitle) + " 最新主题");

                    AddErrLine("不存在的版块ID");
                    return;
                }
                #endregion

                #region 校验及准备

                if (config.Rssstatus == 1)
                    AddLinkRss(forumpath + "tools/" + base.RssAspxRewrite(forum.Fid), Utils.EncodeHtml(forum.Name) + " 最新主题");

                if (JumpUrl(forum)) return;

                needaudit = UserAuthority.NeedAudit(forum, useradminid, userid, usergroupinfo);

                // 检查是否具有版主的身份
                if (useradminid > 0)
                    ismoder = Moderators.IsModer(useradminid, userid, forumid);

                //【BT修改】种子列表显示相关处理

                forum.Layer = 1; //显示种子需要
                forum.Subforumcount = -1;

                //种子分类筛选信息（按用户名搜索）
                searchusername = DNTRequest.GetString("username").Trim();
                searchoperator = DNTRequest.GetString("operator").Trim();
                if (searchusername != "") searchuserid = Users.GetUserId(searchusername);
                if (searchoperator != "") searchoperatorid = Users.GetUserId(searchoperator);
                if ((searchoperatorid <= 0 && searchoperator != "") || (searchuserid <= 0 && searchusername != "")) DoNotSearch = true;
                seeduserid = searchuserid;
                operatorid = searchoperatorid;

                if (searchoperator == "系统") operatorid = 0;

                #endregion

                #region 数据及分页

                int newnumperpage = numperpage;

                //获得每页显示的种子数Cookie
                if (System.Web.HttpContext.Current.Request.Cookies["numperpage"] != null)
                    newnumperpage = Utils.StrToInt(System.Web.HttpContext.Current.Request.Cookies["numperpage"].Value, numperpage);
                if (newnumperpage != numperpage)
                {
                    if (newnumperpage == 20 || newnumperpage == 30 || newnumperpage == 40 || newnumperpage == 50 || newnumperpage == 70 || newnumperpage == 100)
                    {
                        numperpage = newnumperpage;
                    }
                }
                //每页显示种子数更改Cookie
                newnumperpage = DNTRequest.GetInt("numperpage", numperpage);
                if (newnumperpage != numperpage)
                {
                    if (newnumperpage == 20 || newnumperpage == 30 || newnumperpage == 40 || newnumperpage == 50 || newnumperpage == 70 || newnumperpage == 100)
                    {
                        System.Web.HttpContext.Current.Response.Cookies["numperpage"].Value = newnumperpage.ToString();
                        System.Web.HttpContext.Current.Response.Cookies["numperpage"].Expires = DateTime.MaxValue;
                        numperpage = newnumperpage;
                    }
                }

                //不能查看其他人完成的种子以及其他错误的请求数值
                //if ((userstat > 3 && (searchusername != username || searchuserid != userid)) || seedstat > 7 || seedstat < 0 || orderby < 0 || orderby > 7)
                //{
                //    AddErrLine("错误的请求提交");
                //    return;
                //}
                //if (searchusername != "" && searchuserid < 1)
                //{
                //    AddErrLine("用户名不存在");
                //    return;
                //}

                //得到当前用户请求的页数
                pageid = DNTRequest.GetInt("page", 1);

                //forumname = forum.Name;
                pagetitle = PrivateBT.Type2Name(PrivateBT.Forum2Type(forumid)) + "种子操作记录";

                forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname).Replace("\"showforum", "\"" + forumurl + "showforum");
                forumnav = ShowForumAspxRewrite(forumnav, forumid, pageid);

                //显示全部种子的特殊情况的处理  板块为6
                //if (forumid == 6)
                {
                    forumnav += "&raquo; <a href=\"\">种子操作记录</a>";
                    forum.Name = pagetitle;
                }

                //板块描述部分

                //获取全站上传下载因子
                //PrivateBTConfigInfo btconfig = PrivateBTConfig.GetPrivateBTConfig();
                //if (btconfig.UploadMulti > uploadratio && DateTime.Now > btconfig.UpMultiBeginTime && DateTime.Now < btconfig.UpMultiEndTime) uploadratio = btconfig.UploadMulti;
                //if (btconfig.DownloadMulti < downloadratio && DateTime.Now > btconfig.DownMultiBeginTime && DateTime.Now < btconfig.DownMultiEndTime) downloadratio = btconfig.DownloadMulti;
                navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);

                //修正请求页数中可能的错误
                if (pageid < 1)
                {
                    pageid = 1;
                }

                //获取种子计数
                if (!DoNotSearch) seedopinfocount = PrivateBT.GetSeedOPCount(operatorid, operattype, seedtype, seeduserid);
                pagecount = (seedopinfocount) % numperpage == 0 ? (seedopinfocount) / numperpage : (seedopinfocount) / numperpage + 1;
                if (pagecount == 0)
                {
                    pagecount = 1;
                }
                if (pageid > pagecount)
                {
                    pageid = pagecount;
                }
                //获得种子列表【实际使用参数,type,operatorid,operattype,seeduserid】
                if (!DoNotSearch) seedopinfolist = PrivateBT.GetSeedOPList(operatorid, operattype, seedtype, seeduserid, numperpage, pageid);

                //得到页码链接
                //showseedsop.aspx?seedtype={seedtype}&operatorid={operatorid}&seeduserid={seeduserid}&operattype={operattype}&username={searchusername}&operator={searchoperator}&page=1;
                if (seedtype > 0) pageurl = forumurl + "showseedsop.aspx?seedtype=" + seedtype.ToString();
                else pageurl = forumurl + "showseedsop.aspx?seedtype=all";
                if (operatorid > -1) pageurl += "&operatorid=" + operatorid.ToString();
                else pageurl += "&operatorid=";
                if (seeduserid > -1) pageurl += "&seeduserid=" + seeduserid;
                else pageurl += "&seeduserid=";
                if (operattype > -1) pageurl += "&operattype=" + operattype.ToString();
                else pageurl += "&operattype=";
                if (searchusername.Trim() != "") pageurl += "&username=" + PTTools.Escape(searchusername);
                else pageurl += "&username=";
                if (searchoperator.Trim() != "") pageurl += "&operator=" + PTTools.Escape(searchoperator);
                else pageurl += "&operator=";

                pagenumbers = Utils.GetPageNumbers(pageid, pagecount, pageurl, 8);
                //pagenumbers = pagenumbers.Replace("<a href=", "<a style=\"cursor:pointer\" onclick=\"PrivateBTSearchSubmitPage(this.title)\" title=");

                //////////////////////////////////////////////////////////////////////////【DEBUG】
                //keywords = PrivateBT.KeywordsProcess(keywords);

                if (pageid < pagecount) nexturl = pageurl + "&page=" + (pageid + 1).ToString();
                else nexturl = "";
                if (pageid > 1) preurl = pageurl + "&page=" + (pageid - 1).ToString();
                else preurl = "";
                if (nexturl != "") nextpage = string.Format("<a href=\"{0}\" class=\"next\">下一页</a>", nexturl);
                if (preurl != "") prepage = string.Format("<a href=\"{0}\" class=\"pageback\">上一页</a>", preurl);

                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////

                #endregion

                #region 替换版规中的UBB
                forum.Description = UBB.ParseSimpleUBB(forum.Description);
                #endregion

                #region 更新在线信息
                OnlineUsers.UpdateAction(olid, UserAction.ShowForum.ActionID, forumid, forum.Name, -1, "");

                if ((forumtotalonline < config.Maxonlinelist && (config.Whosonlinestatus == 2 || config.Whosonlinestatus == 3)) || DNTRequest.GetString("showonline") == "yes")
                {

                    showforumonline = true;
                    onlineuserlist = OnlineUsers.GetForumOnlineUserCollection(forumid, out forumtotalonline, out forumtotalonlineguest,
                                                                 out forumtotalonlineuser, out forumtotalonlineinvisibleuser);
                }
                //if (DNTRequest.GetString("showonline") != "no")
                //{
                //     showforumonline = false;
                //}

                if (DNTRequest.GetString("showonline") == "no")
                {
                    showforumonline = false;
                }
                #endregion

                #region 修正版主列表
                
                if (forum.Moderators.Trim() != "")
                {
                    string moderHtml = string.Empty;
                    foreach (string m in forum.Moderators.Split(','))
                    {
                        moderHtml += string.Format("<a href=\"{0}userinfo.aspx?username={1}\">{2}</a>,", forumpath, Utils.UrlEncode(m), m);
                    }

                    forum.Moderators = moderHtml.TrimEnd(',');
                }

                #endregion

                ForumUtils.UpdateVisitedForumsOptions(forumid);
            }
            else 
            {
                #region 只允许管理员访问
#if DEBUG
                if (userid != 1 && !PrivateBT.IsServerUser(userid))
                {
                    AddErrLine("错误的链接访问");
                    return;
                }
#else
                if (userid != 1)
                {
                    AddErrLine("错误的链接访问");
                    return;
                }
#endif
                #endregion

                if (showmode == "syslog")
                {
                    #region 信息获取

                    //得到当前用户请求的页数
                    pageid = DNTRequest.GetInt("page", 1);

                    //forumname = forum.Name;
                    pagetitle = "系统日志";
                    forumnav += " <a href=\"\">系统日志</a>";
                    forum.Name = pagetitle;
                    
                    navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);

                    //修正请求页数中可能的错误
                    if (pageid < 1) { pageid = 1; }

                    //获取数据计数

                    bool stabove = false;
                    bool timelimit = false;
                    DateTime start = TypeConverter.StrToDateTime(timestart);
                    DateTime end = TypeConverter.StrToDateTime(timeend);

                    if (timestart != "" || timeend != "")
                    {
                        timelimit = true;
                        timestart = start.ToString();
                        timeend = end.ToString();
                    }
                    #endregion

                    #region 读取列表

                    if (logstatus < 0) { stabove = true; logstatus = -logstatus; }
                    seedopinfocount = PTLog.GetSysLogCount(logtype, logstatus, logaction, stabove, timelimit, start, end);
                    
                    pagecount = (seedopinfocount) % numperpage == 0 ? (seedopinfocount) / numperpage : (seedopinfocount) / numperpage + 1;
                    if (pagecount == 0) { pagecount = 1; }
                    if (pageid > pagecount) { pageid = pagecount; }

                    //
                    logmessagelist = PTLog.GetSysLogTable(pageid, 200, logtype, logstatus, logaction, stabove, timelimit, start, end);

                    //恢复状态
                    if (stabove) logstatus = -logstatus;

                    #endregion

                    #region 得到页码链接

                    pageurl = forumurl + "showseedsop.aspx?s=" + showmode;
                    if (logtype > 0) pageurl += "&type=" + logtype;
                    if (logstatus != -1) pageurl += "&status=" + logstatus;
                    if (logaction != "") pageurl += "&action=" + logaction;
                    pagenumbers = Utils.GetPageNumbers(pageid, pagecount, pageurl, 8);

                    if (pageid < pagecount) nexturl = pageurl + "&page=" + (pageid + 1).ToString();
                    else nexturl = "";
                    if (pageid > 1) preurl = pageurl + "&page=" + (pageid - 1).ToString();
                    else preurl = "";
                    if (nexturl != "") nextpage = string.Format("<a href=\"{0}\" class=\"next\">下一页</a>", nexturl);
                    if (preurl != "") prepage = string.Format("<a href=\"{0}\" class=\"pageback\">上一页</a>", preurl);

                    #endregion
                }
                else if (showmode == "sysdebuglog")
                {
                    #region 信息获取

                    //得到当前用户请求的页数
                    pageid = DNTRequest.GetInt("page", 1);

                    //forumname = forum.Name;
                    pagetitle = "系统调试日志";
                    forumnav += " <a href=\"\">系统调试日志</a>";
                    forum.Name = pagetitle;

                    navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);

                    //修正请求页数中可能的错误
                    if (pageid < 1) { pageid = 1; }

                    //获取数据计数

                    bool stabove = false;
                    bool timelimit = false;
                    DateTime start = TypeConverter.StrToDateTime(timestart);
                    DateTime end = TypeConverter.StrToDateTime(timeend);

                    if (timestart != "" || timeend != "")
                    {
                        timelimit = true;
                        timestart = start.ToString();
                        timeend = end.ToString();
                    }
                    #endregion

                    #region 读取列表

                    if (logstatus < 0) { stabove = true; logstatus = -logstatus; }
                    seedopinfocount = PTLog.GetSysDebugLogCount(logtype, logstatus, logaction, stabove, timelimit, start, end);

                    pagecount = (seedopinfocount) % numperpage == 0 ? (seedopinfocount) / numperpage : (seedopinfocount) / numperpage + 1;
                    if (pagecount == 0) { pagecount = 1; }
                    if (pageid > pagecount) { pageid = pagecount; }

                    //
                    logmessagelist = PTLog.GetSysDebugLogTable(pageid, 200, logtype, logstatus, logaction, stabove, timelimit, start, end);

                    //恢复状态
                    if (stabove) logstatus = -logstatus;

                    #endregion

                    #region 得到页码链接

                    pageurl = forumurl + "showseedsop.aspx?s=" + showmode;
                    if (logtype > 0) pageurl += "&type=" + logtype;
                    if (logstatus != -1) pageurl += "&status=" + logstatus;
                    if (logaction != "") pageurl += "&action=" + logaction;
                    pagenumbers = Utils.GetPageNumbers(pageid, pagecount, pageurl, 8);

                    if (pageid < pagecount) nexturl = pageurl + "&page=" + (pageid + 1).ToString();
                    else nexturl = "";
                    if (pageid > 1) preurl = pageurl + "&page=" + (pageid - 1).ToString();
                    else preurl = "";
                    if (nexturl != "") nextpage = string.Format("<a href=\"{0}\" class=\"next\">下一页</a>", nexturl);
                    if (preurl != "") prepage = string.Format("<a href=\"{0}\" class=\"pageback\">上一页</a>", preurl);

                    #endregion
                }
                else if (showmode == "ssolog")
                {

                }
                else
                {
                    AddErrLine("错误的链接访问");
                    return;
                }

            }
        }

        /// <summary>
        /// 是否跳转链接
        /// </summary>
        /// <param name="forum"></param>
        /// <returns></returns>
        private bool JumpUrl(ForumInfo forumInfo)
        {
            //当版块有外部链接时,则直接跳转
            if (!Utils.StrIsNullOrEmpty(forumInfo.Redirect))
            {
                HttpContext.Current.Response.Redirect(forumInfo.Redirect);
                return true;
            }
            //当允许发表交易帖时,则跳转到相应的商品列表页
            if (config.Enablemall == 1 && forumInfo.Istrade == 1)
            {
                MallPluginBase mpb = MallPluginProvider.GetInstance();
                int categoryid = mpb.GetGoodsCategoryIdByFid(forumInfo.Fid);
                if (categoryid > 0)
                {
                    HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + base.ShowGoodsListAspxRewrite(categoryid, 1));
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取帖子广告信息
        /// </summary>
        public void GetPostAds(int forumid)
        {
            ///得到广告列表
            headerad = Advertisements.GetOneHeaderAd("", forumid);
            footerad = Advertisements.GetOneFooterAd("", forumid);

            pagewordad = Advertisements.GetPageWordAd("", forumid);
            pagead = Advertisements.GetPageAd("", forumid);
            doublead = Advertisements.GetDoubleAd("", forumid);
            floatad = Advertisements.GetFloatAd("", forumid);
            mediaad = Advertisements.GetMediaAd(templatepath, "", forumid);
            //快速发帖广告
            quickeditorad = Advertisements.GetQuickEditorAD("", forumid);

            //快速编辑器背景广告
            quickbgad = Advertisements.GetQuickEditorBgAd("", forumid);
            if (quickbgad.Length <= 1)
                quickbgad = new string[2] { "", "" };
        }

    }
}
