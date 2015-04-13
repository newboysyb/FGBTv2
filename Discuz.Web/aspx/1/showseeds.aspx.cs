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
    public class showseeds : PageBase
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
        /// 主题分类Id showseed不使用
        /// </summary>
        public int topictypeid = 0; //DNTRequest.GetInt("typeid", -1);
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
        public string nextpageurl = "";
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

        private string orderStr = "";//排序方式

        public int topicid = 0;

        public bool needaudit = false;
        #endregion

        //【BT修改】种子显示相关变量

        public List<PTSeedinfoShort> topseedinfolist = new List<PTSeedinfoShort>();
        public List<PTSeedinfoShort> seedinfolist = new List<PTSeedinfoShort>();
        public int topseedinfocount = 0;
        public int seedinfocount = 0;
        /// <summary>
        /// 种子分类，1电影，2剧集....
        /// </summary>
        public int seedtype = 0;
        /// <summary>
        /// 种子状态，1活种，2IPv4，3IPv6，4死种，0全部
        /// </summary>
        public int seedstat = 0;
        /// <summary>
        /// 排序：0种子id，1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间
        /// </summary>
        public int orderby = 0;
        /// <summary>
        /// 排序正反
        /// </summary>
        public bool asc = false;
        /// <summary>
        /// 用户状态：1上传，2下载，3发布，4完成
        /// </summary>
        public int userstat = 0;
        public string totalsize = "";
        public string keywords = "";
        public string notinkeywords = "";
        /// <summary>
        /// 关键词搜索模式
        /// </summary>
        public int keywordsmode = 0;
        public string searchstat = "";
        public int numperpage = 50;
        public string pageurl = "";
        public string searchusername = "";
        public int searchuserid = 0;
        public ShortUserInfo searchuserinfo = null;
        public float uploadratio = 0;
        public float downloadratio = 10;
        public string typestr = "";
        public string preurl = ""; //前一页链接
        public string nexturl = ""; //下一页链接
        public string prepage = "";//上一页按钮
        public string typedescription = "";

        /// <summary>
        /// 显示mode2链接，搜索模式
        /// </summary>
        public int showmode2link = 0;
        public string addmessage = "";

        protected override void ShowPage()
        {
            GetPostAds(forumid);

            //相关信息获取及访问权限校验

            forumnav = "";
            string type_input = DNTRequest.GetString("type");
            if (type_input == "myupload") { seedtype = 0; searchusername = username; userstat = 1; }
            else seedtype = PrivateBT.Str2Type(type_input);
            if (seedtype < 0) seedtype = 0;
            forumid = PrivateBT.Type2Forum(seedtype);
            typestr = PrivateBT.Type2Str(seedtype);

            if (userid < 1)
            {
                AddErrLine("您尚未登录");
                return;
            }

            if (userid > 0 && useradminid > 0)
            {
                AdminGroupInfo admingroupinfo = AdminGroups.GetAdminGroupInfo(usergroupid);
                if (admingroupinfo != null)
                    disablepostctrl = admingroupinfo.Disablepostctrl;
            }

            #region 获取版块信息
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

            if (config.Rssstatus == 1)
                AddLinkRss(forumpath + "tools/" + base.RssAspxRewrite(forum.Fid), Utils.EncodeHtml(forum.Name) + " 最新主题");

            if (JumpUrl(forum)) return;

            needaudit = UserAuthority.NeedAudit(forum, useradminid, userid, usergroupinfo);

            // 检查是否具有版主的身份
            if (useradminid > 0)
                ismoder = Moderators.IsModer(useradminid, userid, forumid);

            //种子列表显示相关处理

            forum.Layer = 1; //显示种子需要
            forum.Subforumcount = -1;

            //种子分类筛选信息
            seedstat = DNTRequest.GetInt("seedstat", 0);
            orderby = DNTRequest.GetInt("orderby", -1);
            asc = DNTRequest.GetString("asc") == "True" ? true : false;
            keywords = DNTRequest.GetString("keywords").Trim();
            notinkeywords = DNTRequest.GetString("notin").Trim();
            keywordsmode = DNTRequest.GetInt("keywordsmode", 0);
            if(searchusername == "") searchusername = DNTRequest.GetString("username").Trim();
            if(userstat == 0) userstat = DNTRequest.GetInt("userstat", 0);
            if (searchusername != "")
            {
                searchuserinfo = Discuz.Data.Users.GetShortUserInfoByName(searchusername);
                if (searchuserinfo != null) searchuserid = searchuserinfo.Uid;
                else searchuserid = -1;
            }
            int newnumperpage = numperpage;
           

            //不能查看其他人完成的种子以及其他错误的请求数值
            if ((userstat > 3 && (searchusername != username || searchuserid != userid)) || seedstat > 9 || seedstat < 0 || orderby < -1 || orderby > 7)
            {
                AddErrLine("错误的请求提交");
                return;
            }
            if (searchusername != "" && searchuserid < 1)
            {
                AddErrLine("搜索的用户名不存在");
                return;
            }

            //得到当前用户请求的页数
            pageid = DNTRequest.GetInt("page", 1);

            //forumname = forum.Name;
            typedescription = PrivateBT.Type2Name(PrivateBT.Forum2Type(forumid));
            pagetitle = typedescription + "发布区";

            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname).Replace("\"showforum", "\"" + forumurl + "showforum");
            forumnav = ShowForumAspxRewrite(forumnav, forumid, pageid);

            //显示全部种子的特殊情况的处理  板块为6
            if (forumid == 6)
            {
                forumnav += "&raquo; <a href=\"\">发布区</a>";
                forum.Name = "发布区";
            }
            navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);

            //获取全站上传下载因子
            PrivateBTConfigInfo btconfig = PrivateBTConfig.GetPrivateBTConfig();
            if (btconfig.UploadMulti > uploadratio && DateTime.Now > btconfig.UpMultiBeginTime && DateTime.Now < btconfig.UpMultiEndTime) uploadratio = btconfig.UploadMulti;
            if (btconfig.DownloadMulti < downloadratio && DateTime.Now > btconfig.DownMultiBeginTime && DateTime.Now < btconfig.DownMultiEndTime) downloadratio = btconfig.DownloadMulti;

            //修正请求页数中可能的错误
            if (pageid < 1)
            {
                pageid = 1;
            }

            //获取当前页置顶种子列表
            if (keywords == "" && notinkeywords == "" && orderby == -1)
            {
                topseedinfolist = PTSeeds.GetTopSeedInfoList(seedtype, userid);
                topseedinfocount = topseedinfolist.Count;
            }
            else if (orderby == -1) orderby = 0;


            //种子搜索模式切换，仅当模式为0时，自动切换至2
            if (keywordsmode == 2) keywordsmode = 0;
            if (keywordsmode == 0) keywordsmode = 3;
            //{
            //    if (keywords.IndexOf("+") > -1 || keywords.IndexOf("'") > -1 || keywords.IndexOf(",") > -1 || keywords.IndexOf("，") > -1 || keywords.IndexOf("+") > -1)
            //        keywordsmode = 2;
            //}
            //if (keywordsmode == 0 && seedinfocount < 50)
            //{
            //    int newcount = PTSeeds.GetSeedInfoCount(seedtype, searchuserid, userstat, seedstat, keywords, 3, notinkeywords);
            //    seedinfocount = newcount;
            //    keywordsmode = 3;
            //}

            //是否计算单种保种奖励值
            int userstat_load = userstat;
            if (userstat == 1 && ((searchuserid == userid) || userid == 1 && searchuserid > 1)) userstat_load = -userstat;

            //关键词预处理
            keywords = PreProcessKeyWords(keywords);
            string seedsearchlist = "";

            //获得种子列表
            if (keywords == "" && notinkeywords == "" && seedstat == 0 && searchuserid == 0)
            {
                seedinfolist = PTSeeds.GetSeedInfoList(numperpage, pageid, topseedinfocount, seedtype, searchuserid, userstat_load, seedstat, keywords, keywordsmode, orderby, asc, userid, notinkeywords, ref seedsearchlist, ref seedinfocount);
                seedinfocount = PTSeeds.GetSeedInfoCount(seedtype, searchuserid, userstat, seedstat, seedsearchlist, keywordsmode, notinkeywords);
                totalsize = PTTools.Upload2Str(PTSeeds.GetSeedSumSize(seedtype, searchuserid, userstat, seedstat, seedsearchlist, keywordsmode, notinkeywords), false);
                if (orderby == -1 && pageid == 1)
                {
                    //如果是默认首页，增加近期热门种子
                    List<PTSeedinfoShort> hotseedinfolist = PTSeeds.GetHotSeedinfoList(20, seedtype, userid);
                    seedinfolist.AddRange(hotseedinfolist);
                }
            }
            else if(pageid == 1)
            {
                seedinfolist = PTSeeds.GetSeedInfoList(numperpage + 1, pageid, topseedinfocount, seedtype, searchuserid, userstat_load, seedstat, keywords, keywordsmode, orderby, asc, userid, notinkeywords, ref seedsearchlist, ref seedinfocount);
                if (seedinfocount >= 5000) addmessage = "只显示了部分种子，请修改或增加关键词";
                if (seedinfolist.Count > numperpage)
                {
                    //存在第二页，获取总数和总大小
                    seedinfolist.RemoveAt(numperpage);
                    //if (notinkeywords != "" || (searchuserid > 0 && userstat > 0)) 
                    seedinfocount = PTSeeds.GetSeedInfoCount(seedtype, searchuserid, userstat, seedstat, seedsearchlist, keywordsmode, notinkeywords);
                    totalsize = PTTools.Upload2Str(PTSeeds.GetSeedSumSize(seedtype, searchuserid, userstat, seedstat, seedsearchlist, keywordsmode, notinkeywords), false);
                }
                else GetSeedlistCountSize(seedinfolist, ref seedinfocount, ref totalsize);

                if(keywords != "" || notinkeywords != "") PTLog.InsertSystemLogDebug_WithPageIP(PTLog.LogType.SearchPage, PTLog.LogStatus.Normal, "Search", string.Format("关键词：{0} 排除：{1} 类别：{2} 结果：{3}", keywords, notinkeywords, seedtype, seedinfocount), true);
            }
            else
            {

                seedinfolist = PTSeeds.GetSeedInfoList(numperpage, pageid, topseedinfocount, seedtype, searchuserid, userstat_load, seedstat, keywords, keywordsmode, orderby, asc, userid, notinkeywords, ref seedsearchlist, ref seedinfocount);
                if (seedinfocount >= 5000) addmessage = "只显示了部分种子，请修改或增加关键词";
                //if (notinkeywords != "" || (searchuserid > 0 && userstat > 0))
                seedinfocount = PTSeeds.GetSeedInfoCount(seedtype, searchuserid, userstat, seedstat, seedsearchlist, keywordsmode, notinkeywords);
                totalsize = PTTools.Upload2Str(PTSeeds.GetSeedSumSize(seedtype, searchuserid, userstat, seedstat, seedsearchlist, keywordsmode, notinkeywords), false);
                pagecount = (topseedinfocount + seedinfocount) % numperpage == 0 ? (topseedinfocount + seedinfocount) / numperpage : (topseedinfocount + seedinfocount) / numperpage + 1;
                if (pagecount == 0)
                {
                    pagecount = 1;
                }
                if (pageid > pagecount)
                {
                    pageid = pagecount;
                    seedinfolist = PTSeeds.GetSeedInfoList(numperpage, pageid, topseedinfocount, seedtype, searchuserid, userstat_load, seedstat, keywords, keywordsmode, orderby, asc, userid, notinkeywords, ref seedsearchlist, ref seedinfocount);
                }
            }

            pagecount = (topseedinfocount + seedinfocount) % numperpage == 0 ? (topseedinfocount + seedinfocount) / numperpage : (topseedinfocount + seedinfocount) / numperpage + 1;
            if (pagecount == 0)
            {
                pagecount = 1;
            }
            if (pageid > pagecount)
            {
                pageid = pagecount;
            }

            //得到页码链接
            if (seedtype > 0) pageurl = forumurl + "showseeds.aspx?type=" + PrivateBT.Type2Str(seedtype);
            else pageurl = forumurl + "showseeds.aspx?type=all";
            if (orderby != -1) pageurl += "&orderby=" + orderby.ToString();
            if (asc == true) pageurl += "&asc=True";
            if (seedstat != 0) pageurl += "&seedstat=" + seedstat.ToString();
            if (keywords != "") pageurl += "&keywords=" + PTTools.Escape(keywords);
            if (notinkeywords != "") pageurl += "&notin=" + PTTools.Escape(notinkeywords);
            if (keywordsmode != 0) pageurl += "&keywordsmode=" + keywordsmode.ToString();
            if (searchusername != "") pageurl += "&username=" + searchusername;
            if (userstat != 0) pageurl += "&userstat=" + userstat.ToString();
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

            //查找信息
            
            if (keywords != "")
            {
                if (keywordsmode == 1) searchstat += "种子标题中包含 “" + keywords + "” 的 ";
                else if (keywordsmode == 3) searchstat += "种子标题或文件中包含 “" + keywords + "” 的 ";
                else if (keywordsmode == 4) searchstat += "种子文件中包含 “" + keywords + "” 的 ";
            }
            else searchstat = "";
            if (notinkeywords != "") searchstat += searchstat == "" ? "种子标题中不包含 “" + notinkeywords + "” 的 " : "且标题中不包含 “" + notinkeywords + "” 的 ";
            if (searchusername != "")
            {
                searchstat += "用户“" + searchusername + "”";
                if (userstat == 1) searchstat += "上传中的 ";
                else if (userstat == 2) searchstat += "下载中的 ";
                else if (userstat == 3) searchstat += "发布的 ";
                else if (userstat == 4) searchstat += "下载过的 ";
            }

            if (seedtype != 0) searchstat += PrivateBT.Type2Name(seedtype) + " ";

            if (seedstat != 0)
            {
                if (seedstat == 1) searchstat += "在线 ";
                else if (seedstat == 3) searchstat += "IPv6在线 ";
                else if (seedstat == 4) searchstat += "离线 ";
                else if (seedstat == 5) searchstat += "蓝色 ";
                else if (seedstat == 6) searchstat += "在线蓝色 ";
                else if (seedstat == 7) searchstat += "IPv6蓝色 ";
                else if (seedstat == 8) searchstat += "包含死种 ";
                else if (seedstat == 9) searchstat += "死种 ";
            }

            searchstat += "种子";

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

            //修正版主列表
            if (forum.Moderators.Trim() != "")
            {
                string moderHtml = string.Empty;
                foreach (string m in forum.Moderators.Split(','))
                {
                    moderHtml += string.Format("<a href=\"{0}userinfo.aspx?username={1}\">{2}</a>,", forumpath, Utils.UrlEncode(m), m);
                }

                forum.Moderators = moderHtml.TrimEnd(',');
            }

            ForumUtils.UpdateVisitedForumsOptions(forumid);
        }


        private string PreProcessKeyWords(string keywords)
        {
            string t = keywords.Replace("'s", "");
            t = t.Replace("'S", "");
            if (t.ToLower() == "solidwork") t = "solidworks";
            return t;
        }

        /// <summary>
        /// 获取种子列表中所有种子的数量和总大小
        /// </summary>
        /// <param name="seedinfolist"></param>
        /// <param name="scount"></param>
        /// <param name="ssize"></param>
        private void GetSeedlistCountSize(List<PTSeedinfoShort> seedinfolist, ref int scount, ref string ssize)
        {
            scount = seedinfolist.Count;
            decimal tsize = 0M;
            foreach (PTSeedinfoShort seedinfo in seedinfolist)
            {
                tsize += seedinfo.FileSize;
            }
            ssize = PTTools.Upload2Str(tsize, false);
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
        /// 获取主题信息列表
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="startNumber">置顶帖数量</param>
        /// <returns></returns>
        public List<TopicInfo> GetTopicInfoList(int pageSize, int pageIndex, int startNumber)
        {
            TopicListFilterInfo tlf = new TopicListFilterInfo(forumid, 0, -1, topictypeid, Forums.GetTopicSpecialID(filter), interval, orderStr, direct == 1 ? true : false, pageSize, pageIndex, startNumber);
            return Topics.GetTopicList(tlf, 600, config.Hottopic, forum.Autoclose, forum.Topictypeprefix);
        }

        /// <summary>
        /// 设置页码链接
        /// </summary>
        private void SetPageNumber()
        {
            if (DNTRequest.GetString("search") == "")
            {
                if (topictypeid == -1)
                {
                    if (config.Aspxrewrite == 1)
                    {
                        if (Utils.StrIsNullOrEmpty(filter))
                        {
                            if (config.Iisurlrewrite == 0)
                                pagenumbers = Utils.GetStaticPageNumbers(pageid, pagecount, (Utils.StrIsNullOrEmpty(forum.Rewritename) ? "showforum-" + forumid : forumpath + forum.Rewritename), config.Extname, 8, (!Utils.StrIsNullOrEmpty(forum.Rewritename) ? 1 : 0));
                            else
                                pagenumbers = Utils.GetStaticPageNumbers(pageid, pagecount, (Utils.StrIsNullOrEmpty(forum.Rewritename) ? "showforum-" + forumid : forumpath + forum.Rewritename), config.Extname, 8, (!Utils.StrIsNullOrEmpty(forum.Rewritename) ? 2 : 0));

                            if (pageid < pagecount)
                                nextpage = string.Format("<a href=\"{0}{1}\" class=\"next\">下一页</a>", forumpath, Urls.ShowForumAspxRewrite(forumid, pageid + 1, forum.Rewritename));
                        }
                        else
                        {
                            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, string.Format("{0}showforum.aspx?forumid={1}&filter={2}", forumpath, forumid, filter), 8);

                            if (pageid < pagecount)
                                nextpage = string.Format("<a href=\"{0}showforum.aspx?forumid={1}&filter={2}&page={3}\" class=\"next\">下一页</a>", forumpath, forumid, filter, pageid + 1);
                        }
                    }
                    else
                    {
                        pagenumbers = Utils.GetPageNumbers(pageid, pagecount, string.Format("{0}showforum.aspx?forumid={1}{2}", forumpath, forumid, (Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter)), 8);

                        if (pageid < pagecount)
                            nextpage = string.Format("<a href=\"{0}showforum.aspx?forumid={1}{2}&page={3}\" class=\"next\">下一页</a>", forumpath, forumid, (Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter), pageid + 1);
                    }
                }
                else //当有主题类型条件时
                {
                    pagenumbers = Utils.GetPageNumbers(pageid, pagecount, string.Format("{0}showforum.aspx?forumid={1}&typeid={2}{3}",
                        forumpath, forumid, topictypeid, (Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter)), 8);

                    if (pageid < pagecount)
                        nextpage = string.Format("<a href=\"{0}showforum.aspx?forumid={1}&typeid={2}{3}&page={4}\" class=\"next\">下一页</a>", forumpath, forumid, topictypeid, (Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter), pageid + 1);
                }
            }
            else
            {
                pagenumbers = Utils.GetPageNumbers(pageid, pagecount, string.Format("{0}showforum.aspx?search=1&cond={1}&order={2}&direct={3}&forumid={4}&interval={5}&typeid={6}{7}",
                                         forumpath, Utils.HtmlEncode(DNTRequest.GetString("cond").Trim()), order, direct, forumid, interval,
                                         topictypeid, (Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter)), 8);

                if (pageid < pagecount)
                    nextpage = string.Format("<a href=\"{0}showforum.aspx?search=1&cond={1}&order={2}&direct={3}&forumid={4}&interval={5}&typeid={6}{7}&page={8}\" class=\"next\">下一页</a>",
                              forumpath, Utils.HtmlEncode(DNTRequest.GetString("cond").Trim()), order, direct,
                              forumid, interval, topictypeid, (Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter), pageid + 1);
            }
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

        ///// <summary>
        ///// 设置搜索和排序条件
        ///// </summary>
        //private void SetSearchCondition()
        //{
        //    if (topictypeid >= 0)
        //        condition = Forums.ShowForumCondition(1, 0) + topictypeid;
        //    //condition = "" + topictypeid;

        //    if (!Utils.InArray(filter, "poll,reward,rewarded,rewarding,debate,digest"))//过滤参数值以防跨站注入
        //        filter = "";

        //    if (!Utils.StrIsNullOrEmpty(filter))
        //        condition += Topics.GetTopicFilterCondition(filter);

        //    if (DNTRequest.GetString("search").Trim() != "") //进行指定查询
        //    {
        //        //多少时间以来的数据
        //        if (interval < 1)
        //            interval = 0;
        //        else if (topictypeid <= 0) //当有主题分类时，则不加入下面的日期查询条件
        //            condition += Forums.ShowForumCondition(2, interval);

        //        //orderStr = (order == 2) ? Forums.ShowForumCondition(3, 0) : ""; //发布时间
        //        switch (order)
        //        {
        //            case 2:
        //                orderStr = Forums.ShowForumCondition(3, 0);//发布时间
        //                break;
        //            case 3:
        //                orderStr = Forums.ShowForumCondition(4, 0);//浏览数
        //                break;
        //            case 4:
        //                orderStr = Forums.ShowForumCondition(5, 0);//回复数
        //                break;
        //            default:
        //                orderStr = string.Empty;
        //                break;
        //        }
        //    }
        //}

        /// <summary>
        /// 设置编辑器状态
        /// </summary>
        private void SetEditorState()
        {
            //编辑器状态
            StringBuilder sb = new StringBuilder();
            sb.Append("var Allowhtml=1;\r\n");
            smileyoff = 1 - forum.Allowsmilies;
            sb.Append("var Allowsmilies=" + (1 - smileyoff) + ";\r\n");

            bbcodeoff = (forum.Allowbbcode == 1 && usergroupinfo.Allowcusbbcode == 1) ? 0 : 1;
            sb.Append("var Allowbbcode=" + (1 - bbcodeoff) + ";\r\n");
            sb.Append("var Allowimgcode=" + forum.Allowimgcode + ";\r\n");
            AddScript(sb.ToString());
        }

        /// <summary>
        /// 是否显示版块密码提示 1为显示, 0不显示
        /// </summary>
        /// <param name="forum">版块信息</param>
        /// <returns></returns>
        private int IsShowForumLogin(ForumInfo forum)
        {
            // 是否显示版块密码提示 1为显示, 0不显示
            int showForumLogin = 1;
            // 如果版块未设密码
            if (Utils.StrIsNullOrEmpty(forum.Password))
                showForumLogin = 0;
            else
            {
                // 如果检测到相应的cookie正确
                if (Utils.MD5(forum.Password) == ForumUtils.GetCookie("forum" + forumid + "password"))
                    showForumLogin = 0;
                else
                {
                    // 如果用户提交的密码正确则保存cookie
                    if (forum.Password == DNTRequest.GetString("forumpassword"))
                    {
                        ForumUtils.WriteCookie("forum" + forum.Fid + "password", Utils.MD5(forum.Password));
                        showForumLogin = 0;
                    }
                }
            }
            return showForumLogin;
        }

        /// <summary>
        /// 设置访问过版块Cookie
        /// </summary>
        private void SetVisitedForumsCookie()
        {
            if (forum.Layer > 0)
            {
                ForumUtils.SetVisitedForumsCookie(forum.Fid.ToString());
            }
        }

        /// <summary>
        /// 判断是否需要生成游客缓存页面
        /// </summary>
        public void IsGuestCachePage()
        {
            //这里假设最后一页之前的所有页面未修改，均可被缓存
            if (userid < 1 && pageid > 0 && pageid < pagecount && ForumUtils.IsGuestCachePage(pageid, "showforum"))
            {
                isguestcachepage = 1;
            }
        }

    }
}
