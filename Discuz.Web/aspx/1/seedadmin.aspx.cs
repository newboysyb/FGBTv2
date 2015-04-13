using System;
using System.Web;
using System.Data;

using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Common.Generic;
using System.Text.RegularExpressions;

namespace Discuz.Web
{
    /// <summary>
    /// 帖子管理页面
    /// </summary>
    public class seedadmin : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 操作标题
        /// </summary>
        public string operationtitle = "操作提示";
        /// <summary>
        /// 操作类型
        /// </summary>
        public string operation = DNTRequest.GetQueryString("operation").ToLower();
        /// <summary>
        /// 操作类型参数
        /// </summary>
        public string action = DNTRequest.GetQueryString("action");
        /// <summary>
        /// 主题列表
        /// </summary>
        public string topiclist = DNTRequest.GetString("topicid");
        /// <summary>
        /// 帖子Id列表
        /// </summary>
        public string postidlist = DNTRequest.GetString("postid");
        /// <summary>
        /// 版块名称
        /// </summary>
        public string forumname = "";
        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav = "";
        /// <summary>
        /// 帖子标题
        /// </summary>
        public string title = "";
        /// <summary>
        /// 帖子作者用户名
        /// </summary>
        public string poster = "";
        /// <summary>
        /// 版块Id
        /// </summary>
        public int forumid = DNTRequest.GetInt("forumid", -1);
        /// <summary>
        /// 版块列表
        /// </summary>
        public string forumlist = Caches.GetForumListBoxOptionsCache(true);
        /// <summary>
        /// 主题置顶状态
        /// </summary>
        public int displayorder = 0;
        /// <summary>
        /// 主题精华状态
        /// </summary>
        public int digest = DNTRequest.GetFormInt("level", -1);
        /// <summary>
        /// 高亮颜色
        /// </summary>
        public string highlight_color = DNTRequest.GetFormString("highlight_color");
        /// <summary>
        /// 是否加粗
        /// </summary>
        public string highlight_style_b = DNTRequest.GetFormString("highlight_style_b");
        /// <summary>
        /// 是否斜体
        /// </summary>
        public string highlight_style_i = DNTRequest.GetFormString("highlight_style_i");
        /// <summary>
        /// 是否带下划线
        /// </summary>
        public string highlight_style_u = DNTRequest.GetFormString("highlight_style_u");
        /// <summary>
        /// 关闭主题, 0=打开,1=关闭 
        /// </summary>
        public int close = 0;
        /// <summary>
        /// 移动主题时的目标版块Id
        /// </summary>
        public int moveto = DNTRequest.GetFormInt("moveto", 0);
        /// <summary>
        /// 移动方式
        /// </summary>
        public string type = DNTRequest.GetFormString("type"); //移动方式
        /// <summary>
        /// 帖子列表
        /// </summary>
        public DataTable postlist;
        /// <summary>
        /// 可用积分列表
        /// </summary>
        public DataTable scorelist;
        /// <summary>
        /// 主题鉴定类型列表
        /// </summary>
        public List<TopicIdentify> identifylist = Caches.GetTopicIdentifyCollection();
        /// <summary>
        /// 主题鉴定js数组
        /// </summary>
        public string identifyjsarray = Caches.GetTopicIdentifyFileNameJsArray();
        /// <summary>
        /// 主题分类选项
        /// </summary>
        public string topictypeselectoptions; //当前版块的主题类型选项
        /// <summary>
        /// 当前帖子评分日志列表
        /// </summary>
        public DataTable ratelog = new DataTable();
        /// <summary>
        /// 当前帖子评分日志记录数
        /// </summary>
        public int ratelogcount = 0;
        /// <summary>
        /// 当前的主题
        /// </summary>
        public TopicInfo topicinfo;
        /// <summary>
        /// opinion
        /// </summary>
        public int opinion = DNTRequest.GetInt("opinion", -1);
        /// <summary>
        /// 是否允许管理主题, 初始false为不允许
        /// </summary>
        protected bool ismoder = false;
        protected bool issubmit = false;
        /// <summary>
        /// 信息是否充满整个弹出窗
        /// </summary>
        public bool titlemessage = false;

        #endregion


        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
        //【BT修改】

        // 是否允许管理主题, 初始false为不允许
        public bool isseedforum = false;
        public string reason = DNTRequest.GetEncodedString("reason");
        public string operationName = "";
        private int sendmsg = 0;
        public bool isblueable = false;
        public bool isblue = false;
        public bool istop = false;
        public int maxaward = 0;

        public string alltopictitle = "";
        public int alltopiccount = 0;

        private string operatnotice = "";

        public DataTable seedinfolist = new DataTable();
        /// <summary>
        /// 操作说明列表
        /// </summary>
        public List<string> operationlist = new List<string>();
        public int lastseedid = 0;


        //流量系数对应表
        private static float[] downloadratioArray = { 0.0f, 0.3f, 0.6f, 1.0f, 2.0f, 3.0f };
        private static float[] uploadratioArray = { 0.0f, 0.3f, 0.6f, 1.0f, 1.2f, 1.6f };
        private static int[] downloadratioexpireArray = { 60, 30, 14, 7, 3, 1 };
        private static int[] uploadratioexpireArray = { 60, 30, 14, 7, 3, 1 };
        private static int[] deleteseedpunishArray = { 0, 2, 5, 10, 20, 50, 100, 200, 500, 1024 };
        private static int[] awardseedArray = { 20, 10, 5, 2, -2, -5, -10, -20 };

        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        protected int RateIsReady = 0;
        private ForumInfo forum;
        //private int highlight = 0;
        public bool issendmessage = false;
        public bool isreason = false;

        protected override void ShowPage()
        {
            ValidatePermission();

            if (!BindTitle())
                return;
        }
 
        /// <summary>
        /// 验证权限
        /// </summary>
        private void ValidatePermission()
        {
            if (userid < 1)
            {
                AddErrLine("请先登录.");
                return;
            }
            if (ForumUtils.IsCrossSitePost(DNTRequest.GetUrlReferrer(), DNTRequest.GetHost()) || action == "")
            {
                AddErrLine("非法提交.");
                return;
            }

            UserGroupInfo usergroupinfo = UserGroups.GetUserGroupInfo(Users.GetUserInfo(userid).Groupid);
            switch (usergroupinfo.Reasonpm)
            {
                case 1: isreason = true; break;
                case 2: issendmessage = true; break;
                case 3:
                    isreason = true;
                    issendmessage = true;
                    break;
                default: break;
            }

            // 检查是否具有版主的身份
            ismoder = Moderators.IsModer(useradminid, userid, forumid);
            // 如果拥有管理组身份
            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(usergroupid);
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】操作权限，种子区识别

            if (DNTRequest.GetString("operat").Equals("seeddeleteself") || DNTRequest.GetString("operation").Equals("seeddeleteself"))
            {
                //自行删除不需要管理权限
            }
            else
            {
                // 如果所属管理组不存在
                if (admininfo == null)
                {
                    AddErrLine("你没有管理权限");
                    return;
                }
            }



            if (PrivateBT.Forum2Type(forumid) < 0)
            {
                BindTitle();
                AddErrLine("非种子发布区不能执行该操作");
                return;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            if (action == "")
            {
                AddErrLine("操作类型参数为空.");
                return;
            }
            if (forumid == -1)
            {
                AddErrLine("版块ID必须为数字.");
                return;
            }
            if (DNTRequest.GetFormString("topicid") != "" && !Topics.InSameForum(topiclist, forumid))
            {
                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 
                //【BT修改】允许对fid 6进行操作（全部种子列表）
                if (forumid != 6)
                {
                    AddErrLine("无法对非本版块主题进行管理操作或主题不存在无法操作！请将此问题汇报给管理员");
                    return;
                }
                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////
            }

            displayorder = TopicAdmins.GetDisplayorder(topiclist);
            digest = TopicAdmins.GetDigest(topiclist);
            forum = Forums.GetForumInfo(forumid);
            forumname = forum.Name;
            topictypeselectoptions = Forums.GetCurrentTopicTypesOption(forum.Fid, forum.Topictypes);
            pagetitle = Utils.RemoveHtml(forumname);
            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);
            if (operation == "delposts")
                SetUrl(base.ShowForumAspxRewrite(forumid, 0));
            else
                SetUrl(DNTRequest.GetUrlReferrer());

            if (!Forums.AllowView(forum.Viewperm, usergroupid))
            {
                AddErrLine("您没有浏览该版块的权限.");
                return;
            }
            if (topiclist.CompareTo("") == 0)
            {
                AddErrLine("您没有选择主题或相应的管理操作.");
                return;
            }

            if (operation.CompareTo("") != 0)
            {
                if (!DoOperations(forum, admininfo, config.Reasonpm)) // DoOperations执行管理操作
                    return;

                ForumUtils.DeleteTopicCacheFile(topiclist); // 删除主题游客缓存
                issubmit = true;
            }

            if (action.CompareTo("moderate") != 0)
            {
                if ("delete,move,type,highlight,close,displayorder,digest,copy,split,merge,bump,repair,delposts,banpost".IndexOf(operation) == -1)
                {
                    AddErrLine("你无权操作此功能");
                    return;
                }
                operation = action;
            }
            else if (operation.CompareTo("") == 0)
            {
                operation = DNTRequest.GetString("operat");

                if (operation.CompareTo("") == 0)
                {
                    AddErrLine("您没有选择主题或相应的管理操作.");
                    return;
                }
            }


            //特殊提醒
            if (operation == "seedratio")
            {
                int remain = GetSeedOpRemain();

                operatnotice = string.Format("设置流量系数 剩余次数 [下载 0%: {0}] [下载 30%: {1}] [下载 60%: {2}] [上传 1.6: {3}] [上传 1.2: {4}]",
                    (int)Math.Floor(remain / 8.0), (int)Math.Floor(remain / 4.0), (int)Math.Floor(remain / 2.0), (int)Math.Floor(remain / 2.0), (int)Math.Floor(remain / 1.0));
            }
        }

        private int GetSeedOpRemain()
        {
            int remain100 = 64 - PTSeeds.GetSeedOpValueSUM(PrivateBT.Forum2Type(forumid), 100);
            int remain200 = 96 - PTSeeds.GetSeedOpValueSUM(PrivateBT.Forum2Type(forumid), 200);

            int remain = remain100 < remain200 ? remain100 : remain200;
            if (remain < 0) remain = 0;

            return remain;
        }
        private int GetSeedOpRemain(int seedtype)
        {
            int remain100 = 64 - PTSeeds.GetSeedOpValueSUM(seedtype, 100);
            int remain200 = 96 - PTSeeds.GetSeedOpValueSUM(seedtype, 200);

            int remain = remain100 < remain200 ? remain100 : remain200;
            if (remain < 0) remain = 0;

            return remain;
        }

        /// <summary>
        /// 绑定操作的标题
        /// </summary>
        /// <returns></returns>
        private bool BindTitle()
        {  
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】删除无关操作,null问题修正

            switch (operation)
            {

                case "seeddelete": operationtitle = "删除种子"; break;
                case "seeddeleteself": operationtitle = "自行删除种子"; break;
                case "seedratio": operationtitle = operatnotice; break;
                case "seedaward": operationtitle = "种子奖励/处罚"; break;
                case "seedunblue": operationtitle = "取消蓝种"; break;
                case "seedtop": operationtitle = "置顶种子"; break;
                case "seeduntop": operationtitle = "取消置顶"; break;
                case "seedmove": operationtitle = "移动种子"; break;
                case "seedclose": operationtitle = "关闭/打开主题"; break;
                case "seeddigest": operationtitle = "设置/取消精华"; break;
                case "seededit": operationtitle = "编辑种子"; break;
                case "seedban": operationtitle = "屏蔽/取消屏蔽种子"; break;
                default: operationtitle = "未知操作"; break;

            }

            //获取要操作的帖子标题列表
            DataTable dt = Topics.GetTopicList(topiclist, -1);
            if(dt == null)
            {
                AddErrLine("错误的管理操作！");
                return false;
            }
            foreach (DataRow dr in dt.Rows)
            {
                alltopictitle += dr["title"].ToString() + "<br/>";
                alltopiccount++;
                lastseedid = TypeConverter.ObjectToInt(dr["seedid"]);
            }
            dt.Clear();
            dt.Dispose();
            dt = null;

            return true;

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
        }

        


        /// <summary>
        /// 进行相关操作
        /// </summary>
        /// <param name="forum"></param>
        /// <param name="admininfo"></param>
        /// <param name="reasonpm"></param>
        /// <returns></returns>
        private bool DoOperations(ForumInfo forum, AdminGroupInfo admininfo, int reasonpm)
        {
            string operationName = "";
            string next = DNTRequest.GetFormString("next");
            string referer = Utils.InArray(operation, "seeddelete,seeddeleteself") ? forumpath + Urls.ShowForumAspxRewrite(forumid, 1, forum.Rewritename) : DNTRequest.GetUrlReferrer();

            DataTable dt = null;

            #region DoOperation

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】

            string reason = DNTRequest.GetString("reason");
            sendmsg = 1;
            issendmessage = true;

            // 原始
            //string reason = DNTRequest.GetString("reason");
            //sendmsg = DNTRequest.GetFormInt("sendmessage", 0);
            //if (issendmessage && sendmsg == 0)
            //{
            //    titlemessage = true;
            //    AddErrLine("操作必须发送短消息通知用户");
            //    return false;
            //}
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            if (!Utils.InArray(operation, "identify,bonus") && isreason)
            {
                if (Utils.StrIsNullOrEmpty(reason))
                {
                    titlemessage = true;
                    AddErrLine("操作原因不能为空");
                    return false;
                }
                else if (reason.Length > 500)
                {
                    titlemessage = true;
                    AddErrLine("操作原因不能多于500个字符");
                    return false;
                }
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】修改为bt相关操作验证和提交

            if (!Utils.InArray(operation, "seeddelete,seeddeleteself,seedmove,seededit,seedratio,seedtop,seeduntop,seeddigest,seedclose,seedban,seedaward"))
            {
                titlemessage = true;
                AddErrLine("未知的操作参数");
                return false;
            }


            //执行提交操作
            if (!Utils.StrIsNullOrEmpty(next.Trim()))
                referer = string.Format("seedadmin.aspx?action={0}&forumid={1}&topicid={2}", next, forumid, topiclist);

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            int operationid = 0;
            bool istopic = false;
            string subjecttype;

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】seedban操作，增加读取列表
            
            Dictionary<int, string> titleList = new Dictionary<int, string>();
            if (Utils.InArray(operation, "rate,delposts,banpost,cancelrate,seedban"))
            {
                if (operation == "seedban")
                {
                    TopicInfo topic = Topics.GetTopicInfo(Utils.StrToInt(topiclist, 0));
                    postidlist = Posts.GetFirstPostId(topic.Tid).ToString();
                }

                dt = Posts.GetPostList(postidlist, topiclist);
                subjecttype = "帖子";
                foreach (DataRow dr in dt.Rows)
                {
                    titleList.Add(TypeConverter.ObjectToInt(dr["pid"]), dr["message"].ToString());
                }
            }
            else
            {
                dt = Topics.GetTopicList(topiclist, -1);
                istopic = true;
                subjecttype = "主题";
                foreach (DataRow dr in dt.Rows)
                {
                    titleList.Add(TypeConverter.ObjectToInt(dr["tid"]), dr["title"].ToString());
                    alltopictitle += dr["title"].ToString() + "<br/>";
                }
            }
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            #region switch operation
            switch (operation)
            {
                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 
                //【BT修改】修改为BT相关操作
                case "seedratio":
                    operationName = "设置流量系数";
                    if (!DoSeedRatioOperation())
                        return false;
                    operationid = 101;
                    break;

                case "seeddelete":
                    operationName = "删除种子";
                    if (!DoSeedDeleteOperation(forum))
                        return false;
                    operationid = 101;
                    break;

                case "seedmove":
                    operationName = "移动种子";
                    if (!DoSeedMoveOperation())
                        return false;
                    operationid = 102;
                    break;

                case "seededit":
                    operationName = "错误操作";
                        return false;

                case "seeddeleteself":
                        operationName = "自行删除种子";
                        if (!DoSeedDeleteSelfOperation())
                            return false;
                        operationid = 103;
                        break;

                case "seedtop":
                    operationName = "置顶种子";
                    if (!DoSeedTopOperation(true))
                        return false;
                    operationid = 106;
                    break;

                case "seeduntop":
                    operationName = "取消置顶";
                    if (!DoSeedTopOperation(false))
                        return false;
                    operationid = 107;
                    break;

                case "seeddigest":
                    operationName = "移动种子";
                    if (!DoSeedDigestOperation())
                        return false;
                    operationid = 108;
                    break;

                case "seedclose":
                    operationName = "关闭帖子";
                    if (!DoSeedCloseOperation())
                        return false;
                    operationid = 109;
                    break;
                case "seedban":
                    operationName = "屏蔽帖子";
                    if (!DoSeedBanOperation())
                        return false;
                    operationid = 110;
                    break;
                case "seedaward":
                    operationName = "奖励/处罚";
                    if (!DoSeedAwardOperation())
                        return false;
                    operationid = 111;
                    break;
                default:
                    operationName = "未知操作";
                    break;
                
                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////
            }

            #endregion

            AddMsgLine(next.CompareTo("") == 0 ? "管理操作成功,现在将转入主题列表" : "管理操作成功,现在将转入后续操作");

            if ((!operation.Equals("rate") && !operation.Equals("split")) && config.Modworkstatus == 1)
            {
                if (postidlist.Equals(""))
                {
                    foreach (string tid in topiclist.Split(','))
                    {
                        string title = "";
                        titleList.TryGetValue(TypeConverter.StrToInt(tid), out title);
                        if (string.IsNullOrEmpty(title))
                        {
                            TopicInfo topicinfo = Topics.GetTopicInfo(Utils.StrToInt(tid, -1));
                            title = topicinfo == null ? title : topicinfo.Title;
                        }
                        AdminModeratorLogs.InsertLog(userid.ToString(), username, usergroupid.ToString(),
                                                       usergroupinfo.Grouptitle, Utils.GetRealIP(),
                                                       Utils.GetDateTime(), forumid.ToString(), forumname,
                                                       tid, title, operationName, reason);
                    }
                }
                else
                {
                    int topicId = Utils.StrToInt(topiclist, -1);
                    TopicInfo topicInfo = Topics.GetTopicInfo(topicId);

                    foreach (string postid in postidlist.Split(','))
                    {
                        PostInfo postinfo = new PostInfo();
                        string postMessage = titleList[Utils.StrToInt(postid, 0)];

                        subjecttype = "回复的主题";
                        string postTitle = postMessage.Replace(" ", "").Replace("|", "");
                        if (postTitle.Length > 100)
                            postTitle = postTitle.Substring(0, 20) + "...";
                        postTitle = "(pid:" + postid + ")" + postTitle;

                        if (operation != "delposts")
                        {
                            postinfo = Posts.GetPostInfo(topicId, Utils.StrToInt(postid, 0));
                            postTitle = postinfo == null ? postTitle : postinfo.Title;
                        }

                        AdminModeratorLogs.InsertLog(userid.ToString(), username, usergroupid.ToString(),
                                                     usergroupinfo.Grouptitle, Utils.GetRealIP(),
                                                     Utils.GetDateTime(), forumid.ToString(), forumname,
                                                     topicInfo.Tid.ToString(), postTitle, operationName, reason);
                    }
                }
            }
            SendNotice(operationid, dt, istopic, operationName, reason, sendmsg, subjecttype);

            //执行完某一操作后转到后续操作
            SetUrl(referer);
            if (next != string.Empty)
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + referer, false);
            else
                AddScript("window.setTimeout('redirectURL()', 2000);function redirectURL() {window.location='" + referer + "';}");

            SetShowBackLink(false);

            #endregion

            return true;
        }


        private void SendNotice(int operationid, DataTable dt, bool istopic, string operationName, string reason, int sendmsg, string subjecttype)
        {
            if (istopic)
                Topics.UpdateTopicModerated(topiclist, operationid);

            if (dt != null)
            {
                if (useradminid != 1 && ForumUtils.HasBannedWord(reason))
                {
                    AddErrLine(string.Format("您提交的内容包含不良信息 <font color=\"red\">{0}</font>", ForumUtils.GetBannedWord(reason)));
                    return;
                }
                else
                    reason = ForumUtils.BanWordFilter(reason);

                foreach (DataRow dr in dt.Rows)
                {
                    if (sendmsg == 1)
                        NoticePost(dr, operationName, subjecttype, reason);
                }
                dt.Dispose();
            }
        }

        #region Operations


        /// <summary>
        /// 蓝种操作
        /// </summary>
        /// <param name="admininfo"></param>
        /// <returns></returns>
        private bool DoSeedRatioOperation()
        {
            if (!ismoder)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有设置流量系数的管理权限", usergroupinfo.Grouptitle));
                return false;
            }
            if (Utils.SplitString(topiclist, ",", true).Length > 1 && AdminGroups.GetAdminGroupInfo(useradminid).Allowmassprune != 1)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有批量设置流量系数的权限", usergroupinfo.Grouptitle));
                return false;
            }

            //验证是否都包含种子
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                if (seedid < 0)
                {
                    AddErrLine("包含错误/无效的种子ID或者主题ID，不能设置流量系数，如果操作没有问题，请联系管理员修正此问题");
                    return false;
                }
            }

            //流量系数设置值
            int downratio = DNTRequest.GetInt("downratio", -1);
            int upratio = DNTRequest.GetInt("upratio", -1);
            int downratioexpire = DNTRequest.GetInt("downratioexpire", -1);
            int upratioexpire = DNTRequest.GetInt("upratioexpire", -1);
            if (downratio < 0 || upratio < 0 || downratioexpire < 0 || upratioexpire < 0 || downratio > 5 || upratio > 5 || downratioexpire > 5 || upratioexpire > 5)
            {
                AddErrLine("包含错误/无效的流量系数选择，不能设置流量系数，如果操作没有问题，请联系管理员修正此问题");
                return false;
            }

            float downloadratio = downloadratioArray[downratio];
            float uploadratio = uploadratioArray[upratio];
            DateTime downloadratioexpiredate = downloadratioexpireArray[downratioexpire] > 30 ? DateTime.Parse("9999-12-31") : DateTime.Now.AddDays(downloadratioexpireArray[downratioexpire]);
            DateTime uploadratioexpiredate = uploadratioexpireArray[upratioexpire] > 30 ? DateTime.Parse("9999-12-31") : DateTime.Now.AddDays(uploadratioexpireArray[upratioexpire]);

            //0.3下载系数，最大设置之日起60天
            if (downloadratio <= 0.3 && (downloadratioexpiredate - DateTime.Now).TotalDays > 60)
            {
                downloadratioexpiredate = DateTime.Now.AddDays(60);
            }

            //设置流量系数操作
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));

                PTSeedinfoShort seedinfo = PTSeeds.GetSeedInfoShort(seedid);

               

                //0下载系数，最大设置为发布之日起60天
                if (downloadratio == 0 && (downloadratioexpiredate - seedinfo.PostDateTime).TotalDays > 60)
                {
                    downloadratioexpiredate = seedinfo.PostDateTime.AddDays(60);
                    //已经过期，则设置为0.3，指定天数
                    if (downloadratioexpiredate < DateTime.Now)
                    {
                        downloadratio = 0.3f;
                        downloadratioexpiredate = DateTime.Now.AddDays(downloadratioexpireArray[downratioexpire]);
                    }
                }

                //操作消耗的点数，下载0% 8点，30% 4点， 60% 2点   上传 1.2 1点， 1.6 2点
                int operatvalue = 0;
                if (downloadratio == 0f) operatvalue = 8;
                else if (downloadratio == 0.3f) operatvalue = 4;
                else if (downloadratio == 0.6f) operatvalue = 2;
                if (uploadratio == 1.6f) operatvalue += 2;
                else if (uploadratio == 1.2f) operatvalue += 1;

                if (seedinfo.DownloadRatio > downloadratio)
                {
                    if (seedinfo.DownloadRatio == 0.6f) operatvalue -= 2;
                    else if (seedinfo.DownloadRatio == 0.3f) operatvalue -= 4;
                }
                else
                {
                    if (downloadratio == 0f) operatvalue -= 8;
                    else if (downloadratio == 0.3f) operatvalue -= 4;
                    else if (downloadratio == 0.6f) operatvalue -= 2;
                }
                if (seedinfo.UploadRatio < uploadratio)
                {
                    if (seedinfo.UploadRatio == 1.2f) operatvalue -= 1;
                }
                else
                {
                    if (uploadratio == 1.6f) operatvalue -= 2;
                    else if (uploadratio == 1.2f) operatvalue -= 1;
                }

                int remain = GetSeedOpRemain(seedinfo.Type);
                if (operatvalue > remain)
                {
                    AddErrLine("剩余流量操作次数不足");
                    return false;
                }


                PTSeeds.UpdateSeedRatio(seedinfo.SeedId, downloadratio, downloadratioexpiredate, uploadratio, uploadratioexpiredate);

                string seedopmessage = string.Format("该种子被版主 {0} 执行 设置下载系数为{1}, 有效期{2}; 设置上传系数为{3}, 有效期{4} 操作",
                    username, downloadratio, downloadratioexpiredate > DateTime.Parse("2099-1-1") ? "永久" : downloadratioexpiredate.ToString(), uploadratio, uploadratioexpiredate > DateTime.Parse("2099-1-1") ? "永久" : uploadratioexpiredate.ToString());

                PrivateBT.InsertSeedModLog(seedid, seedopmessage, username, reason, userid, (downloadratio == 1.0f && uploadratio == 1.0f) ? 3 : 2, operatvalue);

                UserInfo __userinfo = Users.GetUserInfo(seedinfo.Uid);
                MessagePost(__userinfo.Uid, __userinfo.Username, seedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToString());


                #region 自动加速RSS检测
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////
                // 自动加速服务器RSS判断，peer和download检测  检测地点：下载种子、设置蓝种、设置置顶

                bool needadd2rss = false;
                if (downloadratio == 0 && seedinfo.Rss_Acc == 0 && seedinfo.PostDateTime > DateTime.Now.AddDays(-7))
                {
                    if (seedinfo.FileSize < 20 * 1024 * 1024 * 1024M)
                    {
                        needadd2rss = true;
                    }
                    else if (seedinfo.FileSize < 40 * 1024 * 1024 * 1024M)
                    {
                        needadd2rss = true;
                    }
                    else if (seedinfo.FileSize < 100 * 1024 * 1024 * 1024M)
                    {
                        
                    }
                }
                if (needadd2rss)
                {
                    PTRss.AddSeedRss(seedinfo.SeedId, 1, 1);
                }

                ////////////////////////////////////////////////////////////////////////// 
                ////////////////////////////////////////////////////////////////////////// 
                #endregion
            }

            return true;
        }



        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="operationName"></param>
        /// <param name="subjecttype"></param>
        /// <param name="reason"></param>
        private void NoticePost(DataRow dr, string operationName, string subjecttype, string reason)
        {
            int posterid = Utils.StrToInt(dr["posterid"], -1);
            if (posterid == -1) //是游客，管理操作就不发短消息了
                return;
            string titlemsg = "";
            NoticeInfo ni = new NoticeInfo();
            ni.New = 1;
            ni.Postdatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ni.Type = NoticeType.SeedAdmin;
            ni.Poster = username;
            ni.Posterid = userid;
            ni.Uid = posterid;

            reason = string.IsNullOrEmpty(reason) ? reason : "理由:" + reason;

            if (subjecttype == "主题" || Utils.StrToInt(dr["layer"], -1) == 0)
            {
                titlemsg = operation != "delete" ? GetOperatePostUrl(int.Parse(dr["tid"].ToString()), dr["title"].ToString().Trim()) : dr["title"].ToString().Trim();
                ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#0A0\">您发布的种子 {0} 被 {1} 执行了 {2} 操作 理由为：{3}</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#0A0\" >" + username + "</a>", operationName, reason));
            }
            else
            {
                titlemsg = GetOperatePostUrl(int.Parse(dr["tid"].ToString()), Topics.GetTopicInfo(Utils.StrToInt(dr["tid"], 0)).Title);
                ni.Note = Utils.HtmlEncode(string.Format("您在 {0} 回复的帖子被 {1} 执行了{2}操作 {3}", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" >" + username + "</a>", operationName, reason));
            }
            Notices.CreateNoticeInfo(ni);
        }
        /// <summary>
        /// 返回被操作的帖子链接
        /// </summary>
        /// <param name="tid">帖子ID</param>
        /// <returns></returns>
        private string GetOperatePostUrl(int tid, string title)
        {
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】
            //return string.Format("[url={0}{1}]{2}[/url]", Utils.GetRootUrl(BaseConfigs.GetForumPath), Urls.ShowTopicAspxRewrite(tid, 1), title);
            return string.Format("<a href=\"showtopic-{0}.aspx\" style=\"color:#0A0\">{1}</a>", tid, title);
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
        }
       
        /// <summary>
        /// 精华操作
        /// </summary>
        /// <returns></returns>
        private bool DoSeedDigestOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有设置精华的权限");
                return false;
            }
            digest = DNTRequest.GetFormInt("level", -1);
            if (digest > 3 || digest < 0)
            {
                digest = -1;
            }
            if (digest == -1)
            {
                titlemessage = true;
                AddErrLine("您没有选择精华级别");
                return false;
            }

            TopicAdmins.SetDigest(topiclist, short.Parse(digest.ToString()));
            return true;
        }

        

        /// <summary>
        /// 关闭主题
        /// </summary>
        /// <returns></returns>
        private bool DoSeedCloseOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有关闭主题的权限");
                return false;
            }
            if (DNTRequest.GetFormInt("close", -1) == -1)
            {
                titlemessage = true;
                AddErrLine("您没选择打开还是关闭");
                return false;
            }
            if (TopicAdmins.SetClose(topiclist, short.Parse(DNTRequest.GetFormInt("close", -1).ToString())) < 1)
            {
                titlemessage = true;
                AddErrLine("要操作的主题未找到");
                return false;
            }
            return true;
        }

        

        /// <summary>
        /// 移动主题
        /// </summary>
        /// <returns></returns>
        private bool DoSeedMoveOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有移动主题权限");
                return false;
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】种子的移动


            int movetotype = moveto + 1;
            if (movetotype < 1 || movetotype > 12)
            {
                AddErrLine("错误的分区选择，目标板块不是种子发布区！");
                return false;
            }
            moveto = PrivateBT.Type2Forum(movetotype);

            //删掉type相关...
            if (moveto == 0)
            {
                titlemessage = true;
                AddErrLine("您没选择分类或移动方式");
                return false;
            }


            //验证是否都包含种子，并进行移动操作
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                if (seedid < 0)
                {
                    AddErrLine("包含错误的种子ID或者主题ID，不能删除种子");
                    return false;
                }

                PTSeedinfo seedinfo = PTSeeds.GetSeedInfoFull(seedid);
                int oldtype = seedinfo.Type;

                if (movetotype == seedinfo.Type)
                {
                    titlemessage = true;
                    AddErrLine("主题不能在相同分类内移动");
                    return false;
                }

                ForumInfo movetoinfo = Forums.GetForumInfo(moveto);
                if (movetoinfo == null)
                {
                    titlemessage = true;
                    AddErrLine("目标版块不存在");
                    return false;
                }
                if (movetoinfo.Layer == 0)
                {
                    titlemessage = true;
                    AddErrLine("主题不能在分类间移动");
                    return false;
                }

                //移动种子
                PTSeeds.ChangeSeedType(ref seedinfo, movetotype);
                PTSeeds.UpdateSeedEditWithOutSeed(seedinfo);
                if (seedinfo.TopSeed > 0) //更新置顶种子列表
                {
                    PTSeeds.UpdateTopSeedList(oldtype);
                    PTSeeds.UpdateSeedTop(seedinfo.SeedId, false); //取消置顶（安全设定，不能给其他板块置顶）
                }
                
                //移动帖子
                TopicAdmins.MoveTopics(str, moveto, forumid, 0);
                TopicInfo topic = Topics.GetTopicInfo(Utils.StrToInt(str, -1));
                topic.Title = seedinfo.TopicTitle;
                Topics.UpdateTopic(topic);

                //操作记录
                PrivateBT.InsertSeedModLog(seedinfo.SeedId, string.Format("该种子被版主 {0} 执行 移动种子 ({1}->{2}) 操作", username, PrivateBT.Type2Name(oldtype), PrivateBT.Type2Name(movetotype)), username, "", userid, 10);
            }     

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            return true;
        }

        
        /// <summary>
        /// 是否为自删种子
        /// </summary>
        private bool ValidatePermissionSelf()
        {
            operation = DNTRequest.GetQueryString("operation").ToLower();
            if (operation.CompareTo("") == 0)
            {
                operation = DNTRequest.GetString("operat");
            }
            if (operation.CompareTo("") == 0)
            {
                AddErrLine("您没有选择主题或相应的管理操作,请返回修改");
                return false;
            }
            if (operation != "seeddeleteself") return false;
            if (userid < 1)
            {
                AddErrLine("请先登录");
                return false;
            }
            forumid = DNTRequest.GetInt("forumid", -1);
            topiclist = DNTRequest.GetString("topicid");
            forum = Forums.GetForumInfo(forumid);
            if (!Utils.IsInt(topiclist) || topiclist.IndexOf(",") > -1)
            {
                AddErrLine("错误的帖子ID");
                return false;
            }
            topicinfo = Topics.GetTopicInfo(Utils.StrToInt(topiclist, 0));
            if (topicinfo.Fid != forumid)
            {
                AddErrLine("错误的帖子ID或者论坛ID");
                return false;
            }

            // 检查是否具有版主的身份
            ismoder = Moderators.IsModer(useradminid, userid, forumid);
            // 如果拥有管理组身份
            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(useradminid);
            // 是管理成员
            if (ismoder)
            {
                HttpContext.Current.Response.Redirect(string.Format("{0}seedadmin.aspx?action=moderate&operat=seeddelete&forumid={1}&topicid={2}", forumpath, topicinfo.Fid, topicinfo.Tid));
                return false;
            }

            if (topicinfo != null)
            {
                if (topicinfo.SeedId > 0 && topicinfo.Posterid == userid)//确认当前用户为种子发布者
                {
                    if (DNTRequest.GetString("operation").ToLower() == "seeddeleteself") //是最终提交的结果，执行删除
                    {
                        operationName = "删除种子";
                        if (DoSeedDeleteSelfOperation())
                        {
                            ForumUtils.DeleteTopicCacheFile(topiclist);

                            AddMsgLine("种子删除操作成功,现在将转入种子列表");

                            //执行完某一操作后转到后续操作
                            string referer = Urls.ShowForumAspxRewrite(forumid, 1);
                            SetUrl(referer);
                            AddScript("window.setTimeout('redirectURL()', 2000);function redirectURL() {window.location='" + referer + "';}");
                            SetShowBackLink(false);
                        }

                    }
                    return true;
                }
                else
                {
                    //AddErrLine("您不是该帖子的作者，无法删除");
                    HttpContext.Current.Response.Redirect(string.Format("{0}seedadmin.aspx?action=moderate&operat=seeddelete&forumid={1}&topicid={2}", forumpath, topicinfo.Fid, topicinfo.Tid));
                    return false;
                }
            }
            else
            {
                AddErrLine("错误的帖子ID");
                return false;
            }
        }
        
        private void MessagePost(int sendtoid, string seedtoname, string seedtitle, string op, string opdetail, string reason, string opdate)
        {
            //if (sendtoid == -1) //是游客，管理操作就不发短消息了
            //{
            //    return;
            //}

            //PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();

            //// 收件箱
            //privatemessageinfo.Message =
            //        string.Format(
            //            "这是由论坛系统自动发送的通知短消息。<br/><br/>以下您所发表的<br/><span style=\"font-weight:bold\">{0}</span><br/>被 {1} 执行<span style=\"font-weight:bold; color:#A00\"> {3} </span>操作。<br/><br/>操作内容: {4}<br/>操作理由:<span style=\"font-weight:bold; color:#A00\"> {5}</span><br/>操作时间:{2}<br/><br/>如果您对本管理操作有异议，请与我取得联系。",
            //            Utils.HtmlEncode(seedtitle), username, opdate, op, opdetail, reason);

            //if (userid < 3)
            //    privatemessageinfo.Message =
            //            string.Format(
            //                "这是由论坛系统自动发送的通知短消息。<br/><br/>以下您所发表的<br/><span style=\"font-weight:bold\">{0}</span><br/>被 {1} 执行<span style=\"font-weight:bold; color:#A00\"> {3} </span>操作。<br/><br/>操作内容: {4}<br/>操作理由:<span style=\"font-weight:bold; color:#A00\"> {5}</span><br/>操作时间:{2}<br/><br/>请勿回复此短消息，此操作不接受投诉，感谢您的支持与配合。",
            //                Utils.HtmlEncode(seedtitle), username, opdate, op, opdetail, reason);
            //privatemessageinfo.Subject = Utils.HtmlEncode(string.Format("您发表的{0}被执行管理操作", seedtitle));
            //privatemessageinfo.Msgto = seedtoname;
            //privatemessageinfo.Msgtoid = sendtoid;
            //privatemessageinfo.Msgfrom = username;
            //privatemessageinfo.Msgfromid = userid;
            //privatemessageinfo.New = 1;
            //privatemessageinfo.Postdatetime = Utils.GetDateTime();
            //privatemessageinfo.Folder = 0;
            //PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
        }
        /// <summary>
        /// 置顶操作
        /// </summary>
        /// <param name="admininfo"></param>
        /// <returns></returns>
        private bool DoSeedTopOperation(bool settop)
        {
            if (!ismoder)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有置顶的管理权限", usergroupinfo.Grouptitle));
                return false;
            }
            if (Utils.SplitString(topiclist, ",", true).Length > 1 && AdminGroups.GetAdminGroupInfo(useradminid).Allowmassprune != 1)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有批量置顶的权限", usergroupinfo.Grouptitle));
                return false;
            }

            int oplevel = DNTRequest.GetInt("level", -1);
            if (oplevel < 0 && oplevel > 4 && settop || oplevel < 0 && oplevel > 5 && !settop)
            {
                AddErrLine("包含错误的操作类型");
                return false;
            }

            //验证是否都包含种子
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                if (seedid < 0)
                {
                    AddErrLine("包含错误的种子ID或者主题ID，不能置顶种子");
                    return false;
                }
            }

            //置顶操作
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                if (PTSeeds.UpdateSeedTop(seedid, settop) > 0)
                {
                    PTSeedinfoShort seedinfo = PTSeeds.GetSeedInfoShort(seedid);
                    UserInfo __userinfo = Users.GetUserInfo(seedinfo.Uid);
                    
                    string seedopmessage = string.Format("该种子被版主 {0} 执行 {1} 操作", username, settop ? "置顶" : "取消置顶");
                    PrivateBT.InsertSeedModLog(seedid, seedopmessage, username, reason, userid, settop ? 0 : 1);
                    MessagePost(__userinfo.Uid, __userinfo.Username, seedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToLongDateString());

                    #region 自动加速RSS检测
                    //////////////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////
                    // 自动加速服务器RSS判断，peer和download检测  检测地点：下载种子、设置蓝种、设置置顶

                    bool needadd2rss = false;
                    if (settop && (seedinfo.Rss_Acc == 0 || (seedinfo.Rss_Acc > 0 && seedinfo.PostDateTime < DateTime.Now.AddDays(-30))))
                    {
                        if (seedinfo.FileSize < 20 * 1024 * 1024 * 1024M)
                        {
                            needadd2rss = true;
                        }
                        else if (seedinfo.FileSize < 40 * 1024 * 1024 * 1024M)
                        {
                            needadd2rss = true;
                        }
                        else if (seedinfo.FileSize < 100 * 1024 * 1024 * 1024M)
                        {

                        }
                    }
                    if (needadd2rss)
                    {
                        PTRss.AddSeedRss(seedinfo.SeedId, 1, 1);
                    }

                    ////////////////////////////////////////////////////////////////////////// 
                    ////////////////////////////////////////////////////////////////////////// 
                    #endregion
                }
            }
            return true;
        }

        /// <summary>
        /// 奖励/处罚操作
        /// </summary>
        /// <param name="admininfo"></param>
        /// <returns></returns>
        private bool DoSeedAwardOperation()
        {
            if (!ismoder)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有置顶的管理权限", usergroupinfo.Grouptitle));
                return false;
            }
            if (Utils.SplitString(topiclist, ",", true).Length > 1 && AdminGroups.GetAdminGroupInfo(useradminid).Allowmassprune != 1)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有批量置顶的权限", usergroupinfo.Grouptitle));
                return false;
            }

            int awardlevel = DNTRequest.GetInt("award", -1);
            if (awardlevel < 0 || awardlevel > awardseedArray.Length - 1)
            {
                AddErrLine("包含错误的操作类型");
                return false;
            }

            //验证是否都包含种子
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                if (seedid < 0)
                {
                    AddErrLine("包含错误的种子ID或者主题ID，不能置顶种子");
                    return false;
                }
            }

            //奖励/处罚操作
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                PTSeedinfoShort seedinfo = PTSeeds.GetSeedInfoShort(seedid);

                UserInfo __userinfo = Users.GetUserInfo(seedinfo.Uid);

                //计算并更新金币值
                float extcredit3paynum;

                extcredit3paynum = awardseedArray[awardlevel] * 1024 * 1024 * 1024f;
                Users.UpdateUserExtCredits(__userinfo.Uid, 3, extcredit3paynum);
                if (extcredit3paynum > 0)
                {
                    CreditsLogs.AddCreditsLog(__userinfo.Uid, userid, 3, 3, 0, extcredit3paynum, Utils.GetDateTime(), 4);
                }
                else
                {
                    CreditsLogs.AddCreditsLog(__userinfo.Uid, userid, 3, 3, -extcredit3paynum, 0, Utils.GetDateTime(), 4);
                }

                //操作记录，给作者发送通知短消息
                string seedopmessage = "";
                if (extcredit3paynum > 0)
                {
                    seedopmessage = string.Format("该种子被版主 {0} 执行 奖励 操作, 奖励发种者 {1} 上传流量 {2}", username, seedinfo.UserName, PTTools.Download2Str((decimal)(extcredit3paynum), true));
                }
                else
                {
                    seedopmessage = string.Format("该种子被版主 {0} 执行 处罚 操作, 扣除发种者 {1} 上传流量 {2}", username, seedinfo.UserName, PTTools.Download2Str((decimal)(-extcredit3paynum), true));
                }
                PrivateBT.InsertSeedModLog(seedid, seedopmessage, username, reason, userid, 14);
                MessagePost(__userinfo.Uid, __userinfo.Username, seedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToString());

            }
            return true;
        }

        /// <summary>
        /// 删除种子
        /// </summary>
        /// <param name="forum"></param>
        /// <returns></returns>
        private bool DoSeedDeleteOperation(ForumInfo forum)
        {
            if (!ismoder)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有删除的权限", usergroupinfo.Grouptitle));
                return false;
            }

            if (Utils.SplitString(topiclist, ",", true).Length > 1 && AdminGroups.GetAdminGroupInfo(useradminid).Allowmassprune != 1)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有批量删除的权限", usergroupinfo.Grouptitle));
                return false;
            }

            int awardlevel = DNTRequest.GetInt("award", -1);
            if (awardlevel < 0 || awardlevel > 9)
            {
                AddErrLine("包含错误的操作类型，请联系管理员修正！");
                return false;
            }

            //验证是否都包含种子
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                if (seedid < 0)
                {
                    AddErrLine("包含错误的种子ID或者主题ID，不能删除种子");
                    return false;
                }
            }

            string forumlist = "";

            //删除种子记录
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                PTSeedinfoShort seedinfo = PTSeeds.GetSeedInfoShort(seedid);
                //if (PrivateBT.IsServerUserSeedReadOnly(seedinfo.Uid))
                //{
                //    if (userid > 2)
                //    {
                //        AddErrLine("您无权删除该用户发布的种子");
                //        return false;
                //    }

                //}

                //记录种子所属的fid
                if (!Utils.InArray(PrivateBT.Type2Forum(seedinfo.Type).ToString(), forumlist))
                {
                    forumlist += PrivateBT.Type2Forum(seedinfo.Type).ToString() + ",";
                }

                if (PTSeeds.UpdateSeedStatus(seedid, 4) > 0)
                {
                    UserInfo __userinfo = Users.GetUserInfo(seedinfo.Uid);
                    
                    //计算并更新金币值
                    float extcredit3paynum;

                    extcredit3paynum = deleteseedpunishArray[awardlevel] * 1024 * 1024 * 1024f;

                    if (extcredit3paynum > 0)
                    {
                        Users.UpdateUserExtCredits(__userinfo.Uid, 3, -extcredit3paynum);
                        CreditsLogs.AddCreditsLog(__userinfo.Uid, userid, 3, 3, extcredit3paynum, 0, Utils.GetDateTime(), 4);
                    }

                    //操作记录，给作者发送通知短消息
                    string seedopmessage = "";
                    if (extcredit3paynum > 0)
                        seedopmessage = string.Format("该种子被版主 {0} 执行 删除 操作, 扣除发种者 {1} 上传流量 {2}", username, seedinfo.UserName, PTTools.Download2Str((decimal)(extcredit3paynum), true));
                    else 
                        seedopmessage = string.Format("该种子被版主 {0} 执行 删除 操作", username, seedinfo.UserName);
                    PrivateBT.InsertSeedModLog(seedid, seedopmessage, username, reason, userid, 4);
                    MessagePost(__userinfo.Uid, __userinfo.Username, seedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToString());

                    //更新用户发种数目
                    PTUsers.UpdateUserInfoPublishedCount(__userinfo.Uid);


                    //短信通知所有正在下载上传和曾经完成的人
                    PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                    privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您发布的种子\n" + seedinfo.TopicTitle + "\n已经被删除，带来不便敬请谅解";
                    privatemessageinfo.Message += "\n\n操作理由为：\n\n<span style=\"color:red;font-weight:bold\">" + reason + "</span>";
                    if (extcredit3paynum > 0)
                        privatemessageinfo.Message += "\n\n 作为警告，扣除您上传流量 " + PTTools.Download2Str((decimal)(extcredit3paynum), true);
                    privatemessageinfo.Message += "\n\n<span style=\"color:black;font-weight:bold\">请仔细核对版主给出的操作理由，同时仔细阅读版规，下次发种避免出错</span>";
                    privatemessageinfo.Message += "\n\n<span style=\"color:blue;font-weight:bold\">如果您对上述操作理由存在异议（如：版规中不存在关于此删种理由的描述或表述不清），请务必到《意见投诉》板块发帖说明，我们会非常感谢您的反馈！</span>";
                    privatemessageinfo.Subject = "您发布的种子被删除，理由为：" + reason;
                    privatemessageinfo.Msgfrom = username;
                    privatemessageinfo.Msgfromid = userid;
                    privatemessageinfo.New = 1;
                    privatemessageinfo.Postdatetime = Utils.GetDateTime();
                    privatemessageinfo.Folder = 0;
                    privatemessageinfo.Msgto = seedinfo.UserName;
                    privatemessageinfo.Msgtoid = seedinfo.Uid;
                    PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

                    //短信通知所有正在下载上传和曾经完成的人
                    privatemessageinfo = new PrivateMessageInfo();
                    privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您正在下载/上传的种子\n" + seedinfo.TopicTitle + "\n已经被删除，带来不便敬请谅解";
                    privatemessageinfo.Message += "\n\n操作理由为：\n\n<span style=\"color:red;font-weight:bold\">" + reason + "</span>";
                    if (extcredit3paynum > 0)
                        privatemessageinfo.Message += "\n\n 作为警告，扣除发布者上传流量 " + PTTools.Download2Str((decimal)(extcredit3paynum), true);
                    privatemessageinfo.Message += "\n\n<span style=\"color:blue;font-weight:bold\">如果您对上述操作理由存在异议（如：版规中不存在关于此删种理由的描述或表述不清），请务必到《意见投诉》板块发帖说明，我们会非常感谢您的反馈！</span>";
                    privatemessageinfo.Subject = "您正在下载/上传的种子被删除，理由为：" + reason;
                    privatemessageinfo.Msgfrom = "系统";
                    privatemessageinfo.Msgfromid = 0;
                    privatemessageinfo.New = 1;
                    privatemessageinfo.Postdatetime = Utils.GetDateTime();
                    privatemessageinfo.Folder = 0;

                    DataTable peerlist = new DataTable();
                    peerlist = PrivateBT.GetPeerList(seedid);
                    if (peerlist.Rows.Count > 0) foreach (DataRow dr in peerlist.Rows)
                        {
                            privatemessageinfo.Msgtoid = Utils.StrToInt(dr["uid"].ToString(), 0);
                            if (privatemessageinfo.Msgtoid == seedinfo.Uid) continue;
                            privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                            if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                        }
                    peerlist.Dispose();

                    privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您曾经下载完成的种子\n" + seedinfo.TopicTitle + "\n已经被删除，带来不便敬请谅解";
                    privatemessageinfo.Message += "\n\n操作理由为：\n\n<span style=\"color:red;font-weight:bold\">" + reason + "</span>";
                    if (extcredit3paynum > 0)
                        privatemessageinfo.Message += "\n\n 作为警告，扣除发布者上传流量 " + PTTools.Download2Str((decimal)(extcredit3paynum), true);
                    privatemessageinfo.Message += "\n\n<span style=\"color:blue;font-weight:bold\">如果您对上述操作理由存在异议（如：版规中不存在关于此删种理由的描述或表述不清），请务必到《意见投诉》板块发帖说明，我们会非常感谢您的反馈！</span>";

                    privatemessageinfo.Subject = "您曾经下载完成的种子被删除，理由为：" + reason;

                    peerlist = PrivateBT.GetUserListFinished(seedid);
                    if (peerlist.Rows.Count > 0) foreach (DataRow dr in peerlist.Rows)
                        {
                            privatemessageinfo.Msgtoid = Utils.StrToInt(dr["uid"].ToString(), 0);
                            if (privatemessageinfo.Msgtoid == seedinfo.Uid) continue;
                            privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                            if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                        }
                    peerlist.Dispose();
                }

            }



            //删除对应的帖子，放进回收站，保留附件 //此处调用为(byte)否则将从数据库中删除帖子内容！！！！务必注意
            TopicAdmins.DeleteTopics(topiclist, (byte)1, true);

            //更新指定版块的最新发帖数信息
            foreach (string str in Utils.SplitString(forumlist, ",", true))
            {
                int fid = Utils.StrToInt(str, -1);
                if (fid > 0)
                {
                    Forums.SetRealCurrentTopics(fid);
                    Forums.UpdateLastPost(Forums.GetForumInfo(fid));
                }
            }

            return true;
        }
        /// <summary>
        /// 删除种子
        /// </summary>
        /// <param name="forum"></param>
        /// <returns></returns>
        private bool DoSeedDeleteSelfOperation()
        {
            //验证是否都包含种子
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                if (seedid < 0)
                {
                    AddErrLine("包含错误的种子ID或者主题ID，不能删除种子");
                    return false;
                }
            }

            string forumlist = "";

            //删除种子记录
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                int seedid = PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1));
                PTSeedinfoShort seedinfo = PTSeeds.GetSeedInfoShort(seedid);

                //最后一步的验证，是否种子为当前登录者所发
                if (seedinfo.Uid != userid)     
                {
                    AddErrLine("您不是帖子的作者，无法删除");
                    return false;
                }
                //7天之前的种子不能删除
                if ((DateTime.Now - seedinfo.PostDateTime).TotalDays > 7)
                {
                    AddErrLine("种子发布已经超过7天，无法删除");
                    return false;
                }

                //记录种子所属的fid
                if (!Utils.InArray(PrivateBT.Type2Forum(seedinfo.Type).ToString(), forumlist))
                {
                    forumlist += PrivateBT.Type2Forum(seedinfo.Type).ToString() + ",";
                }

                if (PTSeeds.UpdateSeedStatus(seedid, 5) > 0)
                {
                    UserInfo __userinfo = Users.GetUserInfo(seedinfo.Uid);

                    //计算并更新金币值
                    float extcredit3paynum;

                    extcredit3paynum = 10 * 1024 * 1024 * 1024f;

                    if (extcredit3paynum > 0)
                    {
                        Users.UpdateUserExtCredits(__userinfo.Uid, 3, -extcredit3paynum);
                        CreditsLogs.AddCreditsLog(__userinfo.Uid, userid, 3, 3, extcredit3paynum, 0, Utils.GetDateTime(), 4);
                    }

                    //操作记录，给作者发送通知短消息
                    string seedopmessage = "";
                    if (extcredit3paynum > 0)
                        seedopmessage = string.Format("该种子被发布者 {0} 执行 自行删除 操作, 扣发布者 {1} 上传流量 {2}", username, seedinfo.UserName, PTTools.Download2Str((decimal)(extcredit3paynum), true));
                    else
                        seedopmessage = string.Format("该种子被发布者 {0} 执行 自行删除 操作", username, seedinfo.UserName);
                    PrivateBT.InsertSeedModLog(seedid, seedopmessage, username, reason, userid, 4);
                    MessagePost(__userinfo.Uid, __userinfo.Username, seedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToString());

                    //更新用户发种数目
                    PTUsers.UpdateUserInfoPublishedCount(__userinfo.Uid);


                    //短信通知所有正在下载上传和曾经完成的人
                    PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                    string curdatetime = Utils.GetDateTime();
                    privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您正在下载/上传的种子\n" + seedinfo.TopicTitle + "\n已经被发布者自行删除，带来不便敬请谅解";
                    privatemessageinfo.Subject = "您正在下载/上传的种子被删除通知：发布者自行删除";
                    privatemessageinfo.Msgfrom = "系统";
                    privatemessageinfo.Msgfromid = 0;
                    privatemessageinfo.New = 1;
                    privatemessageinfo.Postdatetime = curdatetime;
                    privatemessageinfo.Folder = 0;

                    DataTable peerlist = new DataTable();
                    peerlist = PrivateBT.GetPeerList(seedid);
                    if (peerlist.Rows.Count > 0) foreach (DataRow dr in peerlist.Rows)
                        {
                            privatemessageinfo.Msgtoid = Utils.StrToInt(dr["uid"].ToString(), 0);
                            privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                            if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                        }
                    peerlist.Dispose();

                    privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您曾经下载完成的种子\n" + seedinfo.TopicTitle + "\n已经被发布者自行删除，带来不便敬请谅解";
                    privatemessageinfo.Subject = "您曾经下载完成的种子被删除通知：发布者自行删除";

                    peerlist = PrivateBT.GetUserListFinished(seedid);
                    if (peerlist.Rows.Count > 0) foreach (DataRow dr in peerlist.Rows)
                        {
                            privatemessageinfo.Msgtoid = Utils.StrToInt(dr["uid"].ToString(), 0);
                            privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                            if (privatemessageinfo.Msgtoid != 0) PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                        }
                    peerlist.Dispose();
                }

            }



            //删除对应的帖子，放进回收站，保留附件
            TopicAdmins.DeleteTopics(topiclist, (byte)1, true);

            //更新指定版块的最新发帖数信息
            foreach (string str in Utils.SplitString(forumlist, ",", true))
            {
                int fid = Utils.StrToInt(str, -1);
                if (fid > 0)
                {
                    Forums.SetRealCurrentTopics(fid);
                    Forums.UpdateLastPost(Forums.GetForumInfo(fid));
                }
            }

            return true;
        }
        /// <summary>
        /// 屏蔽种子
        /// </summary>
        /// <returns></returns>
        private bool DoSeedBanOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有单帖屏蔽的权限");
                return false;
            }
            if (!Utils.IsNumeric(topiclist))
            {
                titlemessage = true;
                AddErrLine("无效的主题ID");
                return false;
            }

            TopicInfo topic = Topics.GetTopicInfo(Utils.StrToInt(topiclist, 0));
            if (topic == null)
            {
                titlemessage = true;
                AddErrLine("不存在的主题");
                return false;
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】只屏蔽主帖
            
            postidlist = Posts.GetFirstPostId(topic.Tid).ToString();
            
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            
            if (!Utils.IsNumericList(postidlist))
            {
                titlemessage = true;
                AddErrLine("非法的帖子ID");
                return false;
            }

            //操作记录和发送短消息
            string seedopmessage = string.Format("该种子被版主 {0} 执行 {1} 操作", username, DNTRequest.GetFormInt("banpost", -1) == -2 ? "屏蔽种子" : "取消屏蔽种子");
            PrivateBT.InsertSeedModLog(topic.SeedId, seedopmessage, username, reason, userid, DNTRequest.GetFormInt("banpost", -1) == -2 ? 12 : 13);
            UserInfo __userinfo = Users.GetUserInfo(topic.Posterid);
            MessagePost(__userinfo.Uid, __userinfo.Username, topic.Title, operationName, seedopmessage, reason, DateTime.Now.ToString());

            return Posts.BanPosts(topic.Tid, postidlist, DNTRequest.GetFormInt("banpost", -1));
        }
        #endregion
    }
}
