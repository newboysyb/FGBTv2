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
    public class topicadmin : PageBase
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

        public bool isseedforum = false;
        public string intopic = "false";
        public string topictitle = "";
        private bool ismoderrate = false;
        private string moderstr = "";
        
        //将Dooperation函数中的局部变量全局，移动种子后强制发送
        private int sendmsg = 0;

        public int optionid = -1;
        
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
            #region 用户登录状态及提交状态检查，判断是否需要发送短消息通知
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
            #endregion

            #region 需要版主权限的操作进行权限校验
            // 检查是否具有版主的身份
            ismoder = Moderators.IsModer(useradminid, userid, forumid);
            // 如果拥有管理组身份
            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(usergroupid);

            //不需要版主权限的操作 博彩，评分，悬赏，结束悬赏
            // banpost??????? !operation.Equals("banpost") &&  && !DNTRequest.GetString("operat").Equals("banpost")
            // 
            //if (!operation.Equals("rate") && !operation.Equals("rate") && !operation.Equals("bonus") && !operation.Equals("closebonus") &&
            //!operation.Equals("banpost") && !DNTRequest.GetString("operat").Equals("rate") && !DNTRequest.GetString("operat").Equals("bonus") &&
            //!DNTRequest.GetString("operat").Equals("closebonus") && !DNTRequest.GetString("operat").Equals("banpost"))
            if (!operation.Equals("lottery") && !DNTRequest.GetString("operat").Equals("lottery") &&
                !operation.Equals("rate") && !DNTRequest.GetString("operat").Equals("rate") &&
                !operation.Equals("bonus") && !DNTRequest.GetString("operat").Equals("bonus") &&
                !operation.Equals("closebonus") && !DNTRequest.GetString("operat").Equals("closebonus"))
            {

                // 如果所属管理组不存在
                if (admininfo == null)
                {
                    AddErrLine("你没有管理权限");
                    return;
                }
            }
            #endregion

            #region 种子区识别及部分操作特殊处理
            if (PrivateBT.Forum2Type(forumid) > 0)
            {
                isseedforum = true;
                string operation1 = DNTRequest.GetString("operat").ToLower(); //此处为第一次提交时转入操作页面的值
                if ("delete,rate,banpost,repair,delposts,close,identify".IndexOf(operation1) < 0)
                {
                    operation = operation1;
                    BindTitle();
                    AddErrLine("种子发布区不能执行该操作");
                    return;
                }
                //如果是执行操作的那次提交（正常情况下不会出现）
                if ("delete,rate,banpost,repair,delposts,close,identify".IndexOf(operation) < 0)
                {
                    BindTitle();
                    AddErrLine("种子发布区不能执行该操作");
                    return;
                }
                //可以执行的操作包括 删除（不包括含种子的主题），评分，屏蔽，修复，批量删除（不包括含种子的主题），关闭主题
                //                  delete                        rate    banpost repair delposts     close

                //实际操作中，种子发布区不允许出现普通主题，所以delete和delposts操作也不会被执行
                //rate，banpost，repair，close可以正常执行
            }
            #endregion

            #region 基本信息获取及校验
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
                AddErrLine("无法对非本版块主题进行管理操作.");
                return;
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
            #endregion

            #region 提交实际执行的部分

            if (operation.CompareTo("") != 0)  //operation 值不为空
            {
                if (!DoOperations(forum, admininfo, config.Reasonpm))
                    return;

                ForumUtils.DeleteTopicCacheFile(topiclist); // 删除主题游客缓存
                issubmit = true;
            }
            #endregion

            #region 首次请求页面显示
            //
            if (action.CompareTo("moderate") != 0) //action不是moderate
            {
                //需要管理员权限的操作
                if ("delete,move,type,highlight,close,displayorder,digest,copy,split,merge,bump,repair,delposts,banpost".IndexOf(operation) == -1)
                {
                    AddErrLine("你无权操作此功能"); 
                    return;
                }
                operation = action;
            }
            else if (operation.CompareTo("") == 0) //operation为空（不为空的话，是实际执行部分）
            {
                operation = DNTRequest.GetString("operat"); //重新读取operat值

                if (operation.CompareTo("") == 0)
                {
                    AddErrLine("您没有选择主题或相应的管理操作.");
                    return;
                }
            }
            #endregion
        }

        /// <summary>
        /// 绑定操作的标题
        /// </summary>
        /// <returns></returns>
        private bool BindTitle()
        {
            switch (operation)
            {
                case "split":
                    {
                        #region 分割主题
                        operationtitle = "分割主题";
                        if (Utils.StrToInt(topiclist, 0) <= 0)
                        {
                            AddErrLine(string.Format("您的身份 \"{0}\" 没有分割主题的权限.", usergroupinfo.Grouptitle));
                            return false;
                        }
                        postlist = Posts.GetPostListTitle(Utils.StrToInt(topiclist, 0));
                        if (postlist != null && postlist.Rows.Count > 0)
                        {
                            postlist.Rows[0].Delete();
                            postlist.AcceptChanges();
                        }
                        break;
                        #endregion
                    }
                case "rate":
                    {
                        #region 评分
                        operationtitle = "参与评分";
                        if (!CheckRatePermission()) return false;

                        string repost = TopicAdmins.CheckRateState(postidlist, userid);
                        if (config.Dupkarmarate != 1 && !repost.Equals("") && RateIsReady != 1)
                        {
                            AddErrLine("对不起,您不能对同一个帖子重复评分.");
                            return false;
                        }
                        scorelist = UserGroups.GroupParticipateScore(userid, usergroupid);
                        if (scorelist.Rows.Count < 1)
                        {
                            AddErrLine(string.Format("您的身份 \"{0}\" 没有设置评分范围或者今日可用评分已经用完", usergroupinfo.Grouptitle));
                            return false;
                        }
                        PostInfo postinfo = Posts.GetPostInfo(TypeConverter.StrToInt(topiclist), TypeConverter.StrToInt(postidlist));
                        if (postinfo == null)
                        {
                            AddErrLine("您没有选择要评分的帖子.");
                            return false;
                        }
                        poster = postinfo.Poster;
                        if (postinfo.Posterid == userid)
                        {
                            AddErrLine("您不能对自已的帖子评分.");
                            return false;
                        }
                        title = postinfo.Title;
                        topiclist = postinfo.Tid.ToString();
                        break;
                        #endregion
                    }
                case "cancelrate":
                    {
                        #region 取消评分
                        operationtitle = "撤销评分";
                        PostInfo postinfo = Posts.GetPostInfo(Utils.StrToInt(topiclist, 0), Utils.StrToInt(postidlist, 0));
                        if (postinfo == null)
                        {
                            AddErrLine("您没有选择要撤消评分的帖子");
                            return false;
                        }
                        if (!ismoder)
                        {
                            AddErrLine("您的身份 \"" + usergroupinfo.Grouptitle + "\" 没有撤消评分的权限.");
                            return false;
                        }

                        poster = postinfo.Poster;
                        title = postinfo.Title;
                        topiclist = postinfo.Tid.ToString();

                        ratelogcount = AdminRateLogs.RecordCount("pid = " + postidlist);
                        ratelog = AdminRateLogs.LogList(ratelogcount, 1, "pid = " + postidlist);
                        ratelog.Columns.Add("extcreditname", Type.GetType("System.String"));
                        DataTable scorePaySet = Scoresets.GetScoreSet();

                        //绑定积分名称属性
                        foreach (DataRow dr in ratelog.Rows)
                        {
                            int extcredits = Utils.StrToInt(dr["extcredits"].ToString(), 0);
                            if ((extcredits > 0) && (extcredits < 9) || scorePaySet.Columns.Count > extcredits + 1)
                                dr["extcreditname"] = scorePaySet.Rows[0][extcredits + 1].ToString();
                            else
                                dr["extcreditname"] = "";
                        }
                        break;
                        #endregion
                    }
                case "bonus":
                    {
                        #region 悬赏
                        operationtitle = "派发悬赏";
                        int tid = Utils.StrToInt(topiclist, 0);
                        postlist = Posts.GetPostListTitle(tid);
                        if (postlist != null)
                        {
                            if (postlist.Rows.Count > 0)
                            {
                                postlist.Rows[0].Delete();
                                postlist.AcceptChanges();
                            }
                        }

                        if (postlist.Rows.Count == 0)
                        {
                            AddErrLine("无法对没有回复的悬赏进行结帖.");
                            return false;
                        }

                        topicinfo = Topics.GetTopicInfo(tid);
                        if (topicinfo.Special == 3)
                        {
                            AddErrLine("本主题的悬赏已经结束.");
                            return false;
                        }
                        break;
                        #endregion
                    }
                case "closebonus":
                    {
                        #region 无答案结束悬赏
                        operationtitle = "无答案结束悬赏";
                        int tid = Utils.StrToInt(topiclist, 0);
                        topicinfo = Topics.GetTopicInfo(tid);
                        if (topicinfo.Special == 3)
                        {
                            AddErrLine("本主题的悬赏已经结束.");
                            return false;
                        }
                        break;
                        #endregion
                    }
                case "delete": operationtitle = "删除主题"; break;
                case "move": operationtitle = "移动主题"; break;
                case "type": operationtitle = "主题分类"; break;
                case "highlight": operationtitle = "高亮显示"; break;
                case "close": operationtitle = "关闭/打开主题"; break;
                case "displayorder": operationtitle = "置顶/解除置顶"; break;
                case "digest": operationtitle = "加入/解除精华 "; break;
                case "copy": operationtitle = "复制主题"; break;
                case "merge": operationtitle = "合并主题"; break;
                case "bump": operationtitle = "提升/下沉主题"; break;
                case "repair": operationtitle = "修复主题"; break;
                case "delposts": operationtitle = "批量删帖"; break;
                case "banpost": operationtitle = "单帖屏蔽"; break;
                case "identify": operationtitle = "鉴定主题"; break;
                case "lottery":
                    {
                        // 投注条件检测
                        // 0.投注博彩存在，项目存在
                        // 1.如果有投注，则只能投注相同选项，且总数不超过100注
                        // 2.用户剩余流量大于10G
                        int tid = DNTRequest.GetInt("topicid", -1);
                        topicinfo = Topics.GetTopicInfo(tid);
                        if (tid < 0 || topicinfo == null || topicinfo.Tid < 1)
                        {
                            AddErrLine("主题不存在");
                            return false;
                        }
                        if (topicinfo.Posterid == userid)
                        {
                            AddErrLine("不能投注自己发布的博彩");
                            return false;
                        }

                        LotteryInfo lottery = PTLottery.GetLotteryInfo(topicinfo.Tid);
                        if(lottery == null || lottery.Id < 1) 
                        {
                            AddErrLine("博彩不存在");
                            return false;
                        }
                        if (lottery.StartTime > DateTime.Now || DateTime.Now > lottery.EndTime)
                        {
                            AddErrLine("博彩尚未开始或已过期");
                            return false;
                        }
                        
                        bool lotteryexist = false;
                        string opname = "";
                        int opid = DNTRequest.GetInt("optionid", -1);
                        List<LotteryOption> lotteryoptionlist = PTLottery.GetLotteryOptionList(topicinfo.Tid);
                        foreach (LotteryOption op in lotteryoptionlist)
                        {
                            if (opid == op.OptionId)
                            {
                                lotteryexist = true;
                                opname = op.OptionName;
                            }
                        }
                        if (opid < 0 || !lotteryexist)
                        {                       
                            AddErrLine("博彩选项不存在");
                            return false;
                        }

                        if (btuserinfo.Extcredits3 - btuserinfo.Extcredits4 < 10M * 1024 * 1024 * 1024)
                        {
                            AddErrLine("您的流量不足，不能参与博彩");
                            return false;
                        }

                        optionid = opid;
                        topiclist = topicinfo.Tid.ToString();
                        operationtitle = "投注 [ " + opname + " ] @"; break;
                    }

                default: operationtitle = "未知操作"; break;
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】添加操作时主题提醒
            

            topicinfo = Topics.GetTopicInfo(Utils.StrToInt(topiclist, 0));
            if (topicinfo != null) topictitle = topicinfo.Title;
            else topictitle = "";
            //标题过长的处理，防止出现操作窗体异常
            if (topictitle.Length > 60) topictitle = topictitle.Substring(0, 57) + "...";
            operationtitle += "&nbsp;&nbsp;&nbsp;&nbsp;" + topictitle; 


            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            return true;
        }

        /// <summary>
        /// 检查评分权限
        /// </summary>
        /// <returns></returns>
        private bool CheckRatePermission()
        {
            if (usergroupinfo.Raterange.Equals(""))
            {
                AddErrLine(string.Format("您的身份 \"{0}\" 没有评分的权限.", usergroupinfo.Grouptitle));
                return false;
            }
            else
            {
                bool hasExtcreditsCanRate = false;
                foreach (string roleByScoreType in usergroupinfo.Raterange.Split('|'))
                {
                    //数组结构:  扩展积分编号,参与评分,积分代号,积分名称,评分最小值,评分最大值,24小时最大评分数
                    //				0			1			2		3		4			5			6
                    if (Utils.StrToBool(roleByScoreType.Split(',')[1], false))
                        hasExtcreditsCanRate = true;
                }
                if (!hasExtcreditsCanRate)
                {
                    AddErrLine(string.Format("您的身份 \"{0}\" 没有评分的权限.", usergroupinfo.Grouptitle));
                    return false;
                }
            }
            return true;
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
            string referer = Utils.InArray(operation, "delete,move") ? forumpath + Urls.ShowForumAspxRewrite(forumid, 1, forum.Rewritename) : DNTRequest.GetUrlReferrer();

            DataTable dt = null;

            #region DoOperation

            string reason = DNTRequest.GetString("reason");
            sendmsg = DNTRequest.GetFormInt("sendmessage", 0);
            if (issendmessage && sendmsg == 0)
            {
                titlemessage = true;
                AddErrLine("操作必须发送短消息通知用户");
                return false;
            }

            if (!Utils.InArray(operation, "identify,bonus,lottery") && isreason)
            {
                if (Utils.StrIsNullOrEmpty(reason))
                {
                    titlemessage = true;
                    AddErrLine("操作原因不能为空");
                    return false;
                }
                else if (reason.Length > 200)
                {
                    titlemessage = true;
                    AddErrLine("操作原因不能多于200个字符");
                    return false;
                }
            }
            if (!Utils.InArray(operation, "delete,move,type,highlight,close,displayorder,digest,copy,split,merge,bump,repair,rate,cancelrate,delposts,identify,bonus,closebonus,banpost,lottery"))
            {
                titlemessage = true;
                AddErrLine("未知的操作参数");
                return false;
            }

            //执行提交操作
            if (!Utils.StrIsNullOrEmpty(next.Trim()))
                referer = string.Format("topicadmin.aspx?action={0}&forumid={1}&topicid={2}", next, forumid, topiclist);

            int operationid = 0;
            bool istopic = false;
            string subjecttype;

            Dictionary<int, string> titleList = new Dictionary<int, string>();
            if (Utils.InArray(operation, "rate,delposts,banpost,cancelrate"))
            {
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
                }
            }

            #region switch operation
            switch (operation)
            {
                case "delete":
                    #region delete
                    operationName = "删除主题";
                    if (!DoDeleteOperation(forum))
                        return false;
                    operationid = 1;
                    break;
                    #endregion
                case "move":
                    #region move
                    operationName = "移动主题";
                    if (!DoMoveOperation())
                        return false;
                    operationid = 2;
                    break;
                    #endregion
                case "type":
                    #region type
                    operationName = "主题分类";
                    if (!DoTypeOperation())
                        return false;
                    operationid = 3;
                    break;
                    #endregion
                case "highlight":
                    #region highlight
                    operationName = "设置高亮";
                    if (!DoHighlightOperation())
                        return false;
                    operationid = 4;
                    break;
                    #endregion
                case "close":
                    #region close
                    operationName = "关闭主题/取消";
                    if (!DoCloseOperation())
                        return false;
                    operationid = 5;
                    break;
                    #endregion
                case "displayorder":
                    #region displayorder
                    operationName = "主题置顶/取消";
                    if (!DoDisplayOrderOperation(admininfo))
                        return false;
                    operationid = 6;
                    break;
                    #endregion
                case "digest": //设置精华
                    #region digest
                    operationName = "设置精华/取消";
                    if (!DoDigestOperation())
                        return false;
                    operationid = 7;
                    break;
                    #endregion
                case "copy": //复制主题";
                    #region copy
                    operationName = "复制主题";
                    if (!DoCopyOperation())
                        return false;
                    operationid = 8;
                    break;
                    #endregion
                case "split":
                    #region split
                    operationName = "分割主题";
                    if (!DoSplitOperation())
                        return false;
                    operationid = 9;
                    break;
                    #endregion
                case "merge":
                    #region merge
                    operationName = "合并主题";
                    if (!DoMergeOperation())
                        return false;
                    operationid = 10;
                    break;
                    #endregion
                case "bump": //提升主题
                    #region bump
                    operationName = "提升/下沉主题";
                    if (!DoBumpTopicsOperation())
                        return false;
                    operationid = 11;
                    break;
                    #endregion
                case "repair": //修复主题
                    #region repair
                    operationName = "修复主题";
                    if (!ismoder)
                    {
                        titlemessage = true;
                        AddErrLine("您没有修复主题的权限");
                        return false;
                    }
                    TopicAdmins.RepairTopicList(topiclist);
                    operationid = 12;
                    break;
                    #endregion
                case "rate":
                    #region rate
                    operationName = "帖子评分";
                    if (!DoRateOperation(reason))
                        return false;
                    operationid = 13;
                    break;
                    #endregion
                case "delposts":
                    #region delposts
                    operationName = "批量删帖";
                    int layer = 1;
                    bool flag = DoDelpostsOperation(reason, forum, ref layer);
                    if (layer == 0)
                        return true;
                    if (!flag)
                        return false;
                    operationid = 14;
                    break;
                    #endregion
                case "identify":
                    #region identify
                    operationName = "鉴定主题";
                    if (!DoIndentifyOperation())
                        return false;
                    operationid = 15;
                    break;
                    #endregion
                case "cancelrate":
                    #region cancelrate
                    operationName = "撤销评分";
                    if (!DoCancelRateOperation(reason))
                        return false;
                    operationid = 16;
                    break;
                    #endregion
                case "bonus":
                    #region bonus
                    operationName = "派发悬赏";

                    if (!DoBonusOperation())
                        return false;
                    operationid = 16;
                    break;
                    #endregion
                case "banpost":
                    #region banpost
                    operationName = "屏蔽帖子";
                    if (!DoBanPostOperatopn())
                        return false;
                    operationid = 17;
                    break;
                    #endregion
                case "closebonus":
                    #region bonus
                    operationName = "无答案结束悬赏";

                    if (!DoCloseBonusOperation())
                        return false;
                    operationid = 18;
                    break;
                    #endregion
                case "lottery":
                    #region lottery
                    operationName = "参与博彩";
                    if (!DoLotteryOperation())
                        return false;
                    operationid = 1;
                    break;
                    #endregion

                default: operationName = "未知操作"; break;
            }

            #endregion

            AddMsgLine(next.CompareTo("") == 0 ? "管理操作成功,现在将转入主题列表" : "管理操作成功,现在将转入后续操作");

            #region 主题操作附加记录(显示于帖子下方)
            if ((!operation.Equals("rate") && !operation.Equals("lottery") && !operation.Equals("split")) && config.Modworkstatus == 1)
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
            #endregion

            SendMessage(operationid, dt, istopic, operationName, reason, sendmsg, subjecttype);

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





        private static object SynObjectLottery = new object();
        private static string SynObjectLotteryCurrent = "";
        private bool DoLotteryOperation()
        {
            #region 投注条件检测


            if (DNTRequest.GetFormInt("sendmessage", 0) == 0)
            {
                titlemessage = true;
                AddErrLine("请勾选确认复选框");
                return false;
            }

            // 投注条件检测
            // 0.投注博彩存在，项目存在
            // 1.如果有投注，则只能投注相同选项，且总数不超过100注
            // 2.用户剩余流量大于10G
            int tid = DNTRequest.GetFormInt("topicid", -1);
            topicinfo = Topics.GetTopicInfo(tid);
            if (tid < 0 || topicinfo == null || topicinfo.Tid < 1)
            {
                titlemessage = true;
                AddErrLine("主题不存在");
                return false;
            }
            if (topicinfo.Posterid == userid)
            {
                titlemessage = true;
                AddErrLine("不能投注自己发布的博彩");
                return false;
            }

            LotteryInfo lottery = PTLottery.GetLotteryInfo(topicinfo.Tid);
            if (lottery == null || lottery.Id < 1)
            {
                titlemessage = true;
                AddErrLine("博彩不存在");
                return false;
            }
            if (lottery.StartTime > DateTime.Now || DateTime.Now > lottery.EndTime)
            {
                AddErrLine("博彩尚未开始或已过期");
                return false;
            }

            bool lotteryexist = false;
            string opname = "";
            int opid = DNTRequest.GetFormInt("optionid", -1);
            List<LotteryOption> lotteryoptionlist = PTLottery.GetLotteryOptionList(topicinfo.Tid);
            foreach (LotteryOption op in lotteryoptionlist)
            {
                if (opid == op.OptionId)
                {
                    lotteryexist = true;
                    opname = op.OptionName;
                }
            }
            if (opid < 0 || !lotteryexist)
            {
                titlemessage = true;
                AddErrLine("博彩选项不存在");
                return false;
            }

            int wagercount = DNTRequest.GetFormInt("wagercount", -1);
            if (wagercount < 1)
            {
                titlemessage = true;
                AddErrLine("投注数不能小于1");
                return false;
            }

            #region 确保同一用户同时只能执行一个:检测流量和下注数
            
            bool curok = false;
            string curstr = "[" + userid.ToString() + "]";
            int curcount = 0;
            bool optionmatch = true;

            lock(SynObjectLottery)
            {
                if(SynObjectLotteryCurrent.IndexOf(curstr) < 0) 
                {
                    curok = true;
                    SynObjectLotteryCurrent = SynObjectLotteryCurrent + curstr;
                }
            }


            if (!curok)
            {
                AddErrLine("您已提交，正在处理，若不成功请稍后再次提交！");
                PTLottery.InsertLotteryLog(topicinfo.Tid, userid, "CONCURRENT ADD", string.Format("等候返回：{0}-{1}", userid, SynObjectLotteryCurrent));
                return false;
            }
            else
            {
                try
                {
                    //用户剩余流量监测

                    //重新获取用户信息，避免并发问题
                    btuserinfo = PTUsers.GetBtUserInfoForPagebase(userid);
                    if (btuserinfo.Extcredits3 - btuserinfo.Extcredits4 - 200M * 1024 * 1024 * 1024 < wagercount * 10M * 1024 * 1024 * 1024)
                    {
                        titlemessage = true;
                        AddErrLine("您的流量不足，投注后 上传流量-下载流量 不得小于200GB");
                        lock (SynObjectLottery) { SynObjectLotteryCurrent = SynObjectLotteryCurrent.Replace(curstr, ""); }
                        return false;
                    }
                    //投注总数检测
                    List<LotteryWager> wagerlist = PTLottery.GetLotteryWagerListbyUid(topicinfo.Tid, userid);

                    foreach (LotteryWager wg in wagerlist)
                    {
                        curcount += wg.WagerCount;
                        if (wg.OptionId != opid) optionmatch = false;
                    }
                    if (!optionmatch)
                    {
                        titlemessage = true;
                        AddErrLine("您已投注过其他选项");
                        lock (SynObjectLottery) { SynObjectLotteryCurrent = SynObjectLotteryCurrent.Replace(curstr, ""); }
                        return false;
                    }
                    if (curcount + wagercount > 100)
                    {
                        titlemessage = true;
                        AddErrLine("总计投注数不能超过100");
                        lock (SynObjectLottery) { SynObjectLotteryCurrent = SynObjectLotteryCurrent.Replace(curstr, ""); }
                        return false;
                    }

                    //插入投注记录
                    PTLottery.InsertLotteryWager(topicinfo.Tid, userid, opid, wagercount);

                    lock (SynObjectLottery) { SynObjectLotteryCurrent = SynObjectLotteryCurrent.Replace(curstr, ""); }
                }
                catch (System.Exception ex)
                {
                    PTLottery.InsertLotteryLog(topicinfo.Tid, userid, "Ex ADD", string.Format("异常：WGCOUNT:{0}-EX:{1}", wagercount, ex));
                    lock (SynObjectLottery) { SynObjectLotteryCurrent = SynObjectLotteryCurrent.Replace(curstr, ""); }
                }
            }   
            

            #endregion

            #endregion

            //更新投注信息，投注日志
            PTLottery.UpdateLotteryInfoSumCount(topicinfo.Tid, opid);
            PTLottery.InsertLotteryLog(topicinfo.Tid, userid, "ADD WG", string.Format("{0},{1}", wagercount, curcount));


            //计算并更新金币值
            float extcredit3paynum;
            extcredit3paynum = wagercount * 10 * 1024 * 1024 * 1024f;
            if (extcredit3paynum > 0)
            {
                Users.UpdateUserExtCredits(userid, 3, -extcredit3paynum);
                CreditsLogs.AddCreditsLog(userid, userid, 3, 3, extcredit3paynum, 0, Utils.GetDateTime(), 15);
            }

            //短信通知
            PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
            privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您已投注 [ " + wagercount + " ] 注： [ " + opname + " ] ，来自博彩：" + topicinfo.Title + " ，支付上传流量：" + PTTools.Upload2Str((decimal)extcredit3paynum);
            privatemessageinfo.Subject = "您已投注 [ " + wagercount + " ] 注： [ " + opname + " ] ，支付上传流量：" + PTTools.Upload2Str((decimal)extcredit3paynum);
            privatemessageinfo.Msgfrom = "系统";
            privatemessageinfo.Msgfromid = 0;
            privatemessageinfo.New = 1;
            privatemessageinfo.Postdatetime = Utils.GetDateTime();
            privatemessageinfo.Folder = 0;
            privatemessageinfo.Msgto = username;
            privatemessageinfo.Msgtoid = userid;
            PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);

            return true;
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
            return string.Format("<a href=\"showtopic-{0}.aspx\">{1}</a>", tid, title);
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
        }
        /// <summary>
        /// 返回被操作的帖子链接
        /// </summary>
        /// <param name="tid">帖子ID</param>
        /// <returns></returns>
        private string GetOperatePostUrl(int tid, int pid, string title)
        {
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】
            //return string.Format("[url={0}{1}]{2}[/url]", Utils.GetRootUrl(BaseConfigs.GetForumPath), Urls.ShowTopicAspxRewrite(tid, 1), title);
            return string.Format("<a href=\"showtopic.aspx?topicid={0}&postid={2}#{2}\">{1}</a>", tid, title, pid);
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
        }
        /// <summary>
        /// 返回被操作的帖子链接
        /// </summary>
        /// <param name="tid">帖子ID</param>
        /// <returns></returns>
        private string GetOperatePostUrlOnly(int tid, int pid, string title)
        {
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】
            //return string.Format("[url={0}{1}]{2}[/url]", Utils.GetRootUrl(BaseConfigs.GetForumPath), Urls.ShowTopicAspxRewrite(tid, 1), title);
            return string.Format("<a href=\"showtopic.aspx?topicid={0}&postid={2}#{2}\">", tid, title, pid);
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
        }


        private void SendMessage(int operationid, DataTable dt, bool istopic, string operationName, string reason, int sendmsg, string subjecttype)
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
                        MessagePost(dr, operationName, subjecttype, reason);
                }
                dt.Dispose();
            }
        }

        #region Operations

        private void MessagePost(DataRow dr, string operationName, string subjecttype, string reason)
        {
            int posterid = Utils.StrToInt(dr["posterid"], -1);
            if (posterid == -1) //是游客，管理操作就不发短消息了
                return;
            string titlemsg = "";
            NoticeInfo ni = new NoticeInfo();
            ni.New = 1;
            ni.Postdatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】 帖子评分：橙黄色，管理操作：蓝色
            
            if (operationName == "帖子评分") ni.Type = NoticeType.TopicAdmin;
            else ni.Type = NoticeType.UserValue;
            
            ni.Poster = username;
            ni.Posterid = userid;
            ni.Uid = posterid;

            
            

            
            if (subjecttype == "主题" || Utils.StrToInt(dr["layer"], -1) == 0)
            {
                if (operationName == "帖子评分")
                {
                    titlemsg = operation != "delete" ? GetOperatePostUrl(int.Parse(dr["tid"].ToString()), int.Parse(dr["pid"].ToString()), dr["title"].ToString().Trim()) : dr["title"].ToString().Trim();
                    titlemsg = titlemsg.Replace("<a ", "<a style=\"color:#C60\" ");
                    if(ismoderrate) ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#C00\">您发表的主题 < {0} > 被版主 {1} 执行了 <b>管理评分</b>：{2}，理由为：{3}</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#C60\" >" + username + "</a>", moderstr, reason));
                    else ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#C60\">您发表的主题 < {0} > 被用户 {1} 执行了 <b>用户评分</b>：{2}，留言为：{3}</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#C60\" >" + username + "</a>", moderstr, reason));
                }
                else if (operationName == "参与博彩")
                {
                    titlemsg = operation != "delete" ? GetOperatePostUrl(int.Parse(dr["tid"].ToString()), dr["title"].ToString().Trim()) : dr["title"].ToString().Trim();
                    titlemsg = titlemsg.Replace("<a ", "<a style=\"color:#00A\" ");
                    ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#00A\">{1} 参与了您发布的<  {0} > 博彩</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#00A\" >" + username + "</a>"));
                }
                else
                {
                    reason = string.IsNullOrEmpty(reason) ? reason : "理由为:" + reason;
                    titlemsg = operation != "delete" ? GetOperatePostUrl(int.Parse(dr["tid"].ToString()), dr["title"].ToString().Trim()) : dr["title"].ToString().Trim();
                    titlemsg = titlemsg.Replace("<a ", "<a style=\"color:#00A\" ");
                    ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#00A\">您发表的主题 <  {0} > 被 {1} 执行了 <b>{2}</b> 操作 {3}</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#00A\" >" + username + "</a>", operationName, reason));

                }
            }
            else
            {
                if (operationName == "帖子评分")
                {
                    titlemsg = GetOperatePostUrl(int.Parse(dr["tid"].ToString()), int.Parse(dr["pid"].ToString()), Topics.GetTopicInfo(Utils.StrToInt(dr["tid"], 0)).Title);
                    titlemsg = titlemsg.Replace("<a ", "<a style=\"color:#C60\" ");
                    //ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#C60\">您在 < {0} > 回复的帖子被 {1} 执行了{2}操作 {3}</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#C60\" >" + username + "</a>", operationName, reason));
                    if (ismoderrate) ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#C00\">您在 < {0} > 回复的帖子 被版主 {1} 执行了 <b>管理评分</b>：{2}，理由为：{3}</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#C60\" >" + username + "</a>", moderstr, reason));
                    else ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#C60\">您您在 < {0} > 回复的帖子 被用户 {1} 执行了 <b>用户评分</b>：{2}，留言为：{3}</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#C60\" >" + username + "</a>", moderstr, reason));

                }
                else
                {
                    reason = string.IsNullOrEmpty(reason) ? reason : "理由为:" + reason;
                    titlemsg = GetOperatePostUrl(int.Parse(dr["tid"].ToString()), Topics.GetTopicInfo(Utils.StrToInt(dr["tid"], 0)).Title);
                    titlemsg = titlemsg.Replace("<a ", "<a style=\"color:#00A\" ");
                    ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#00A\">您在 < {0} > 回复的帖子被 {1} 执行了 <b>{2}</b> 操作 {3}</span>", titlemsg, "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#00A\" >" + username + "</a>", operationName, reason));

                }
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            if (operationName != "参与博彩") //参与博彩不发送短消息
            {
                Notices.CreateNoticeInfo(ni);
            }
            
        }

        /// <summary>
        /// 评分
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        private bool DoRateOperation(string reason)
        {
            if (!CheckRatePermission())
                return false;

            if (Utils.StrIsNullOrEmpty(postidlist))
            {
                titlemessage = true;
                AddErrLine("您没有选择要评分的帖子");
                return false;
            }

            if (config.Dupkarmarate != 1 && AdminRateLogs.RecordCount(AdminRateLogs.GetRateLogCountCondition(userid, postidlist)) > 0)
            {
                titlemessage = true;
                AddErrLine("您不能对本帖重复评分");
                return false;
            }

            scorelist = UserGroups.GroupParticipateScore(userid, usergroupid);
            string[] scoreArr = Utils.SplitString(DNTRequest.GetFormString("score").Replace("+", ""), ",");
            string[] extcreditsArr = Utils.SplitString(DNTRequest.GetFormString("extcredits"), ",");
            string cscoreArr = "", cextcreditsArr = "";
            int arr = 0;
            for (int i = 0; i < scoreArr.Length; i++)
            {
                if (Utils.IsNumeric(scoreArr[i].ToString()) && scoreArr[i].ToString() != "0" && !scoreArr[i].ToString().Contains("."))
                {
                    cscoreArr = cscoreArr + scoreArr[i] + ",";
                    cextcreditsArr = cextcreditsArr + extcreditsArr[i] + ",";
                }
            }

            if (cscoreArr.Length == 0)
            {
                titlemessage = true;
                AddErrLine("分值超过限制.");
                return false;
            }

            foreach (DataRow scoredr in scorelist.Rows)
            {
                if (scoredr["ScoreCode"].ToString().Equals(extcreditsArr[arr]))
                {
                    if (Utils.StrToInt(scoredr["MaxInDay"], 0) < Math.Abs(Utils.StrToInt(scoreArr[arr], 0)) ||
                        Utils.StrToInt(scoredr["Max"], 0) < Utils.StrToInt(scoreArr[arr], 0) ||
                        Utils.StrToInt(scoreArr[arr], 0) != 0 && (Utils.StrToInt(scoredr["Min"], 0) > Utils.StrToInt(scoreArr[arr], 0)))
                    {
                        titlemessage = true;
                        AddErrLine("分值超过限制.");
                        return false;
                    }
                    if (extcreditsArr[arr] == "2" && TypeConverter.StrToInt(scoreArr[arr]) != 0 || (extcreditsArr[arr] == "3" && (TypeConverter.StrToInt(scoreArr[arr]) >= 100 || TypeConverter.StrToInt(scoreArr[arr]) <= -100)))
                    {
                        if (ismoder) ismoderrate = true;
                    }
                    if (TypeConverter.StrToInt(scoreArr[arr]) != 0)
                    {
                        moderstr += (moderstr == "" ? "" : ", ") + scoredr["ScoreName"] + (scoreArr[arr].IndexOf("-") < 0 ? "+" : "") + scoreArr[arr];
                    }
                    
                }
                arr++;
            }

            TopicAdmins.RatePosts(Utils.StrToInt(topiclist, 0), postidlist, cscoreArr, cextcreditsArr, userid, username, reason);
            Posts.UpdatePostRateTimes(Utils.StrToInt(topiclist, 0), postidlist);
            RateIsReady = 1;
            return true;
        }

        /// <summary>
        /// 撤消评分
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        private bool DoCancelRateOperation(string reason)
        {
            if (!CheckRatePermission())
                return false;

            if (postidlist.Equals(""))
            {
                titlemessage = true;
                AddErrLine("您未选择要撤销评分的帖子");
                return false;
            }
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有撤消评分的权限");
                return false;
            }
            if (DNTRequest.GetFormString("ratelogid").Equals(""))
            {
                titlemessage = true;
                AddErrLine("您未选择要撤销评分的记录");
                return false;
            }

            TopicAdmins.CancelRatePosts(DNTRequest.GetFormString("ratelogid"), Utils.StrToInt(topiclist, 0), postidlist, userid, username, usergroupinfo.Groupid, usergroupinfo.Grouptitle, forumid, forumname, reason);
            Posts.UpdatePostRateTimes(Utils.StrToInt(topiclist, 0), postidlist);
            return true;
        }

        /// <summary>
        /// 合并主题
        /// </summary>
        /// <returns></returns>
        private bool DoMergeOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有合并主题的权限");
                return false;
            }
            if (DNTRequest.GetFormInt("othertid", 0) == 0)
            {
                titlemessage = true;
                AddErrLine("您没有输入要合并的主题ID");
                return false;
            }
            //同一主题,不能合并
            if (DNTRequest.GetFormInt("othertid", 0) == Utils.StrToInt(topiclist, 0))
            {
                titlemessage = true;
                AddErrLine("不能对同一主题进行合并操作");
                return false;
            }

            if (Topics.GetTopicInfo(DNTRequest.GetFormInt("othertid", 0)) == null)
            {
                titlemessage = true;
                AddErrLine("目标主题不存在");
                return false;
            }
            //如果目标主题和当前主题的帖子不在同一个分表当中，则暂时设定不允许合并，看以后的解决方案
            if (Posts.GetPostTableId(DNTRequest.GetFormInt("othertid", 0)) != Posts.GetPostTableId(Utils.StrToInt(topiclist, 0)))
            {
                titlemessage = true;
                AddErrLine("不允许跨分表合并主题");
                return false;
            }

            TopicAdmins.MerrgeTopics(topiclist, DNTRequest.GetFormInt("othertid", 0));
            return true;
        }

        /// <summary>
        /// 分割主题
        /// </summary>
        /// <returns></returns>
        private bool DoSplitOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有分割主题的权限");
                return false;
            }
            if (DNTRequest.GetString("subject").Equals(""))
            {
                titlemessage = true;
                AddErrLine("您没有输入标题");
                return false;
            }
            if (DNTRequest.GetString("subject").Length > 60)
            {
                titlemessage = true;
                AddErrLine("标题长为60字以内");
                return false;
            }
            if (postidlist.Equals(""))
            {
                titlemessage = true;
                AddErrLine("请选择要分割入新主题的帖子");
                return false;
            }
            //如果分割的主题使用的帖子表不是当前最新分表,则暂时无法分割主题，待以后解决方案
            if (Posts.GetPostTableId(TypeConverter.StrToInt(topiclist)) != Posts.GetPostTableId())
            {
                titlemessage = true;
                AddErrLine("主题过旧,无法分割");
                return false;
            }
            TopicAdmins.SplitTopics(postidlist, DNTRequest.GetString("subject"), topiclist);
            return true;
        }

        /// <summary>
        /// 复制主题
        /// </summary>
        /// <returns></returns>
        private bool DoCopyOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有复制主题的权限");
                return false;
            }
            if (DNTRequest.GetFormInt("copyto", 0) == 0)
            {
                titlemessage = true;
                AddErrLine("您没有选择目标论坛/分类");
                return false;
            }

            TopicAdmins.CopyTopics(topiclist, DNTRequest.GetFormInt("copyto", 0));
            return true;
        }

        /// <summary>
        /// 精华操作
        /// </summary>
        /// <returns></returns>
        private bool DoDigestOperation()
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
        /// 置顶操作
        /// </summary>
        /// <param name="admininfo"></param>
        /// <returns></returns>
        private bool DoDisplayOrderOperation(AdminGroupInfo admininfo)
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有置顶的管理权限");
                return false;
            }

            displayorder = DNTRequest.GetFormInt("level", -1);
            if (displayorder < 0 || displayorder > 3)
            {
                titlemessage = true;
                AddErrLine("置顶参数超出范围");
                return false;
            }
            // 检查用户所在管理组是否具有置顶的管理权限
            if (admininfo.Admingid != 1 && admininfo.Allowstickthread < displayorder)
            {
                titlemessage = true;
                AddErrLine(string.Format("您没有{0}级置顶的管理权限", displayorder));
                return false;
            }

            TopicAdmins.SetTopTopicList(forumid, topiclist, short.Parse(displayorder.ToString()));
            return true;
        }

        /// <summary>
        /// 关闭主题
        /// </summary>
        /// <returns></returns>
        private bool DoCloseOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有关闭主题的权限");
                return false;
            }

            int op = DNTRequest.GetFormInt("close", -1);
            if (op == -1)
            {
                titlemessage = true;
                AddErrLine("您没选择打开还是关闭");
                return false;
            }

            int rst = 0;
            if(op == 0) rst = TopicAdmins.SetClose(topiclist, 0);
            else 
            {
                rst = TopicAdmins.SetClose(topiclist, 1);
                if(op == 2) TopicAdmins.BumpTopics(topiclist, -1);
                //更新板块最后发帖，被关闭的主题不上最后发帖
                Forums.UpdateLastPost(forum);
            }

            if (rst < 1)
            {
                titlemessage = true;
                AddErrLine("要操作的主题未找到");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 高亮主题
        /// </summary>
        /// <returns></returns>
        private bool DoHighlightOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有设置高亮的权限");
                return false;
            }

            string highlightStyle = "";

            //加粗
            if (!highlight_style_b.Equals(""))
                highlightStyle = highlightStyle + "font-weight:bold;";

            //加斜
            if (!highlight_style_i.Equals(""))
                highlightStyle = highlightStyle + "font-style:italic;";

            //加下划线
            if (!highlight_style_u.Equals(""))
                highlightStyle = highlightStyle + "text-decoration:underline;";

            //设置颜色
            if (!highlight_color.Equals(""))
                highlightStyle = highlightStyle + "color:" + highlight_color + ";";

            if (highlightStyle == "")
            {
                titlemessage = true;
                AddErrLine("您没有选择字体样式及颜色");
                return false;
            }

            TopicAdmins.SetHighlight(topiclist, highlightStyle);
            return true;
        }

        /// <summary>
        /// 修改主题分类
        /// </summary>
        /// <returns></returns>
        private bool DoTypeOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有修改主题分类的权限");
                return false;
            }
            if (DNTRequest.GetFormInt("typeid", 0) == 0)
            {
                titlemessage = true;
                AddErrLine("你没有选择相应的主题分类");
                return false;
            }

            TopicAdmins.ResetTopicTypes(DNTRequest.GetFormInt("typeid", 0), topiclist);
            return true;
        }

        /// <summary>
        /// 移动主题
        /// </summary>
        /// <returns></returns>
        private bool DoMoveOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有移动主题权限");
                return false;
            }
            if (moveto == 0 || type.CompareTo("") == 0 || ",normal,redirect,".IndexOf("," + type.Trim() + ",") == -1)
            {
                titlemessage = true;
                AddErrLine("您没选择分类或移动方式");
                return false;
            }
            if (moveto == forumid)
            {
                titlemessage = true;
                AddErrLine("主题不能在相同分类内移动");
                return false;
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】种子的移动

            if (PrivateBT.Forum2Type(moveto) > 0 && PrivateBT.Forum2Type(forumid) <= 0)
            {
                AddErrLine("不能移动到种子发布区");
                return false;
            }
            if (PrivateBT.Forum2Type(moveto) <= 0 && PrivateBT.Forum2Type(forumid) > 0)
            {
                AddErrLine("不能将种子移出种子发布区");
                return false;
            }

            //不允许向 “校园资讯”移动主题
            if (moveto == 50)
            {
                AddErrLine("由于校园资讯版主题需要单独审核，不能向校园资讯版移动主题");
                return false;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


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

            int topicType = DNTRequest.GetInt("movetopictype", 0);
            bool isDefinedType = false;

            //检测当前获取的主题分类ID是否是目标版块定义的分类
            foreach (string t in movetoinfo.Topictypes.Split('|'))
            {
                if (topicType == TypeConverter.StrToInt(t.Split(',')[0]))
                {
                    isDefinedType = true;
                    break;
                }
            }

            topicType = isDefinedType ? topicType : 0;

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】

            if (PrivateBT.Forum2Type(moveto) > 0 && PrivateBT.Forum2Type(forumid) > 0)
            {
                type = "move";
                sendmsg = 1;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            TopicAdmins.MoveTopics(topiclist, moveto, forumid, type.CompareTo("redirect") == 0, topicType);

            return true;
        }

        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="forum"></param>
        /// <returns></returns>
        private bool DoDeleteOperation(ForumInfo forum)
        {
            if (!ismoder || AdminGroups.GetAdminGroupInfo(useradminid).Allowdelpost != 1)
            {
                titlemessage = true;
                AddErrLine("您没有删除权限");
                return false;
            }
            if (Utils.SplitString(topiclist, ",", true).Length > 1 && AdminGroups.GetAdminGroupInfo(useradminid).Allowmassprune != 1)
            {
                titlemessage = true;
                AddErrLine("您没有批量删除权限");
                return false;
            }


            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】验证是否要删除的帖子中包含种子
            
            foreach (string str in Utils.SplitString(topiclist, ",", true))
            {
                if (PrivateBT.GetSeedIdByTopicId(Utils.StrToInt(str, -1)) > 0)
                {
                    AddErrLine("该帖子中包含种子，不能通过此方式删除");
                    return false;
                }
                TopicInfo dotopicinfo = Topics.GetTopicInfo(Utils.StrToInt(str, -1));
                if (dotopicinfo.Special == 2) //|| dotopicinfo.Special == 3
                {
                    AddErrLine("该帖子中包含未结贴悬赏，不能删除，请先结贴");
                    return false;
                }
                if (dotopicinfo.SeedId > 0)
                {
                    AddErrLine("错误：该帖子中包含种子，不能通过此方式删除");
                    return false;
                }
                if (dotopicinfo.Special == 64)
                {
                    //删除文件交换种子
                    PTAbt.AbtDeleteSeed(-dotopicinfo.SeedId);
                    PTAbt.AbtDeleteDownload(-dotopicinfo.SeedId);
                }
                //如果是博彩
                if (dotopicinfo.Special == 128) DoDelLotteryOperation(dotopicinfo.Tid, dotopicinfo.Title);
            }



            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            TopicAdmins.DeleteTopics(topiclist, byte.Parse(forum.Recyclebin.ToString()), DNTRequest.GetInt("reserveattach", 0) == 1);
            Forums.SetRealCurrentTopics(forum.Fid);
            //更新指定版块的最新发帖数信息
            Forums.UpdateLastPost(forum);
            return true;
        }

        /// <summary>
        /// 提升/下沉主题
        /// </summary>
        /// <returns></returns>
        private bool DoBumpTopicsOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有提升/下沉主题的权限");
                return false;
            }
            if (!Utils.IsNumericList(topiclist))
            {
                titlemessage = true;
                AddErrLine("非法的主题ID");
                return false;
            }
            if (Math.Abs(DNTRequest.GetFormInt("bumptype", 0)) != 1)
            {
                titlemessage = true;
                AddErrLine("错误的参数");
                return false;
            }

            TopicAdmins.BumpTopics(topiclist, DNTRequest.GetFormInt("bumptype", 0));
            return true;
        }

        /// <summary>
        /// 单帖屏蔽
        /// </summary>
        /// <returns></returns>
        private bool DoBanPostOperatopn()
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
            if (!Utils.IsNumericList(postidlist))
            {
                titlemessage = true;
                AddErrLine("非法的帖子ID");
                return false;
            }
            return Posts.BanPosts(topic.Tid, postidlist, DNTRequest.GetFormInt("banpost", -1));
            //int banposttype = DNTRequest.GetFormInt("banpost", -1);
            //if (banposttype != -1 && (banposttype == 0 || banposttype == -2))
            //{
            //    Posts.BanPosts(topic.Tid, postidlist, banposttype);
            //    return true;
            //}

            //return false;
        }

        /// <summary>
        /// 删除博彩：返还已投注者流量
        /// </summary>
        /// <param name="topicid"></param>
        private void DoDelLotteryOperation(int topicid, string topictitle)
        {

            List<LotteryOption> lotteryoptionlist = PTLottery.GetLotteryOptionList(topicid);

            LotteryInfo lottery = PTLottery.GetLotteryInfo(topicid);

            if (lottery.Ended == 0)
            {
                if (PTLottery.UpdateLotteryInfo(topicid, lottery.BaseWager, lottery.EndTime, 2) > 0)
                {
                    //返还流量
                    foreach (LotteryOption ops in lotteryoptionlist)
                    {
                        //获取投注者列表
                        List<LotteryWager> wagerlist = PTLottery.GetLotteryWagerList(topicid, ops.OptionId);

                        float extcredit3paynum;
                        foreach (LotteryWager wa in wagerlist)
                        {
                            if (wa.Win < 0)
                            {
                                extcredit3paynum = wa.WagerCount * 10 * 1024 * 1024 * 1024f;
                                if (extcredit3paynum > 0)
                                {
                                    PTLottery.UpdateLotteryWager(wa.Id, (decimal)extcredit3paynum);
                                    PTLottery.InsertLotteryLog(topicid, userid, "DEL LOTTERY", string.Format("归还：ID:{0} -UID:{1} -USERNAME:{2} -UP:{3}", wa.Id, wa.Userid, wa.Username, extcredit3paynum));


                                    Users.UpdateUserExtCredits(wa.Userid, 3, extcredit3paynum);
                                    CreditsLogs.AddCreditsLog(wa.Userid, wa.Userid, 3, 3, 0, extcredit3paynum, Utils.GetDateTime(), 15);

                                    //短信通知
                                    PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                                    privatemessageinfo.Message = "这是由论坛系统自动发送的通知短消息。\n\n您投注的 [ " + wa.WagerCount + " ] 注： [ " + ops.OptionName + " ] 因博彩被删除，已返还流量： " + PTTools.Upload2Str((decimal)extcredit3paynum);
                                    privatemessageinfo.Message += "，来自博彩：" + topictitle;
                                    privatemessageinfo.Subject = "您投注的 [ " + wa.WagerCount + " ] 注： [ " + ops.OptionName + " ] 因博彩被删除，已返还流量";
                                    privatemessageinfo.Msgfrom = "系统";
                                    privatemessageinfo.Msgfromid = 0;
                                    privatemessageinfo.New = 1;
                                    privatemessageinfo.Postdatetime = Utils.GetDateTime();
                                    privatemessageinfo.Folder = 0;
                                    privatemessageinfo.Msgto = wa.Username;
                                    privatemessageinfo.Msgtoid = wa.Userid;
                                    PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                                }
                                else
                                {
                                    PTLottery.InsertLotteryLog(topicid, userid, "DEL LOTTERY", string.Format("未能归还：ID:{0} -UID:{1} -USERNAME:{2} -UP:{3}", wa.Id, wa.Userid, wa.Username, extcredit3paynum));
                                }
                            }
                            else
                            {
                                PTLottery.InsertLotteryLog(topicid, userid, "DEL LOTTERY", string.Format("重复归还：ID:{0} -UID:{1} -USERNAME:{2} -WIN:{3}", wa.Id, wa.Userid, wa.Username, wa.Win));
                            }

                        }


                    }
                }
                
            }
        }


        /// <summary>
        /// 批量删帖
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="forum"></param>
        /// <returns></returns>
        private bool DoDelpostsOperation(string reason, ForumInfo forum, ref int layer)
        {
            if (!ismoder || AdminGroups.GetAdminGroupInfo(useradminid).Allowdelpost != 1)
            {
                titlemessage = true;
                AddErrLine("您没有批量删帖的权限");
                return false;
            }
            if (Utils.SplitString(postidlist, ",", true).Length > 1 && AdminGroups.GetAdminGroupInfo(useradminid).Allowmassprune != 1)
            {
                titlemessage = true;
                AddErrLine("您没有批量删除的权限");
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
            if (!Utils.IsNumericList(postidlist))
            {
                titlemessage = true;
                AddErrLine("非法的帖子ID");
                return false;
            }

            bool flag = false;
            foreach (string postid in postidlist.Split(','))
            {
                PostInfo post = Posts.GetPostInfo(topic.Tid, Utils.StrToInt(postid, 0));

                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 
                //【BT修改】验证是否要删除的帖子中包含种子

                if(post.Layer <= 0)
                {
                    //if (PrivateBT.GetSeedIdByTopicId(topic.Tid) > 0 && post.Layer <= 0)
                    if (topic.SeedId > 0)
                    {
                        AddErrLine("帖子中包含种子，不能通过此方式删除");
                        return false;
                    }
                    if (topic.Special == 2 || topic.Special == 3)
                    {
                        AddErrLine("该帖子中包含悬赏，不能删除");
                        return false;
                    }
                    if (topic.Special == 64)
                    {
                        //删除文件交换种子
                        PTAbt.AbtDeleteSeed(-topic.SeedId);
                        PTAbt.AbtDeleteDownload(-topic.SeedId);
                    }
                    //如果是博彩
                    if (topic.Special == 128)
                    {
                        DoDelLotteryOperation(topic.Tid, topic.Title);
                    }
                }

                

                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////


                if (post == null || (post.Layer <= 0 && topic.Replies > 0) || topic.Tid != post.Tid)
                {
                    titlemessage = true;
                    AddErrLine("主题无效或者已被回复");
                    return false;
                }
                // 通过验证的用户可以删除帖子
                if (post.Layer == 0)
                {
                    TopicAdmins.DeleteTopics(topic.Tid.ToString(), byte.Parse(forum.Recyclebin.ToString()), DNTRequest.GetInt("reserveattach", 0) == 1);
                    layer = 0;
                    break;
                }
                else
                {
                    int reval = Posts.DeletePost(Posts.GetPostTableId(topic.Tid), post.Pid, DNTRequest.GetInt("reserveattach", 0) == 1, true);
                    if (topic.Special == 4)
                    {
                        if (opinion != 1 && opinion != 2)
                        {
                            titlemessage = true;
                            AddErrLine("参数错误");
                            return false;
                        }

                        string opiniontext = "";
                        switch (opinion)
                        {
                            case 1: opiniontext = "positivediggs"; break;
                            case 2: opiniontext = "negativediggs"; break;
                        }
                        Discuz.Data.DatabaseProvider.GetInstance().DeleteDebatePost(topic.Tid, opiniontext, Utils.StrToInt(postid, -1));
                    }
                    //if (reval > 0 && (config.Losslessdel == 0 || Utils.StrDateDiffHours(post.Postdatetime, config.Losslessdel * 24) < 0))
                    //    UserCredits.UpdateUserCreditsByDeletePosts(post.Posterid);
                }
                flag = true;
            }
            //确保回复数精确

            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //【BT修改】函数修改
            
            if(topic.SeedId > 0)
                Topics.UpdateTopicReplyCount(topic.Tid, 1);
            else
                Topics.UpdateTopicReplyCount(topic.Tid, -1);

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            
            //更新指定版块的最新发帖数信息
            Forums.UpdateLastPost(forum);
            return flag;
        }

        /// <summary>
        /// 鉴定主题
        /// </summary>
        /// <returns></returns>
        private bool DoIndentifyOperation()
        {
            if (!ismoder)
            {
                titlemessage = true;
                AddErrLine("您没有鉴定主题的权限");
                return false;
            }

            int identify = DNTRequest.GetInt("selectidentify", 0);
            if (identify > 0 || identify == -1)
            {
                TopicAdmins.IdentifyTopic(topiclist, identify);
                return true;
            }
            else
            {
                titlemessage = true;
                AddErrLine("请选择签定类型");
                return false;
            }
        }

        /// <summary>
        /// 悬赏结帖
        /// </summary>
        /// <returns></returns>
        private bool DoCloseBonusOperation()
        {
            //身份验证
            topicinfo = Topics.GetTopicInfo(DNTRequest.GetInt("topicid", 0));

            if (topicinfo.Special == 3)
            {
                titlemessage = true;
                AddErrLine("本主题的悬赏已经结束");
                return false;
            }
            if (topicinfo.Posterid <= 0)
            {
                titlemessage = true;
                AddErrLine("无法结束游客发布的悬赏");
                return false;
            }
            if (topicinfo.Posterid != userid && !ismoder)//不是作者或管理者
            {
                titlemessage = true;
                AddErrLine("您没有权限结束此悬赏");
                return false;
            }

            int bonusCreditsTrans = Scoresets.GetBonusCreditsTrans();
            if (DateTime.Parse(topicinfo.Postdatetime) > DateTime.Parse("2012-05-28 15:00"))
            {
                Users.UpdateUserExtCredits(topicinfo.Posterid, bonusCreditsTrans, topicinfo.Price * 1024f * 1024 * 1024 * 2.0f); //返还悬赏
                CreditsLogs.AddCreditsLog(topicinfo.Posterid, userid, 3, 3, 0, topicinfo.Price * 1024f * 1024 * 1024 * 2.0f, Utils.GetDateTime(), 13);

                topicinfo.Special = 3;//标示为悬赏主题
                Topics.UpdateTopic(topicinfo);//更新标志位为已结帖状态
                Discuz.Data.Bonus.AddLog(topicinfo.Tid, topicinfo.Posterid, topicinfo.Posterid, "无答案结束悬赏", 0, topicinfo.Price, Scoresets.GetBonusCreditsTrans(), 0);
            }
            else
            {
                //系统改版前发布的悬赏
                Users.UpdateUserExtCredits(topicinfo.Posterid, bonusCreditsTrans, topicinfo.Price * 1024f * 1024 * 1024 * 1.0f); //返还悬赏
                CreditsLogs.AddCreditsLog(topicinfo.Posterid, userid, 3, 3, 0, topicinfo.Price * 1024f * 1024 * 1024 * 1.0f, Utils.GetDateTime(), 13);
                topicinfo.Special = 3;//标示为悬赏主题
                Topics.UpdateTopic(topicinfo);//更新标志位为已结帖状态
                Discuz.Data.Bonus.AddLog(topicinfo.Tid, topicinfo.Posterid, topicinfo.Posterid, "无答案结束悬赏", 0, topicinfo.Price, Scoresets.GetBonusCreditsTrans(), 0);
            }
            return true;
        }


        /// <summary>
        /// 悬赏结帖
        /// </summary>
        /// <returns></returns>
        private bool DoBonusOperation()
        {
            //身份验证
            topicinfo = Topics.GetTopicInfo(DNTRequest.GetInt("topicid", 0));

            if (topicinfo.Special == 3)
            {
                titlemessage = true;
                AddErrLine("本主题的悬赏已经结束");
                return false;
            }
            if (topicinfo.Posterid <= 0)
            {
                titlemessage = true;
                AddErrLine("无法结束游客发布的悬赏");
                return false;
            }
            if (topicinfo.Posterid != userid && !ismoder)//不是作者或管理者
            {
                titlemessage = true;
                AddErrLine("您没有权限结束此悬赏");
                return false;
            }

            int costBonus = 0;
            string[] costBonusArray = DNTRequest.GetString("postbonus").Split(',');

            foreach (string s in costBonusArray)
            {
                costBonus += Utils.StrToInt(s, 0);
            }

            if (costBonus != topicinfo.Price)
            {
                titlemessage = true;
                AddErrLine("获奖分数与悬赏分数不一致");
                return false;
            }

            string[] addonsArray = DNTRequest.GetFormString("addons").Split(',');
            int[] winneridArray = new int[addonsArray.Length];
            int[] postidArray = new int[addonsArray.Length];
            string[] winnernameArray = new string[addonsArray.Length];

            foreach (string addon in addonsArray)
            {
                if (Utils.StrToInt(addon.Split('|')[0], 0) == topicinfo.Posterid)
                {
                    titlemessage = true;
                    AddErrLine("不能向悬赏者发放积分奖励");
                    return false;
                }
            }

            if (costBonusArray.Length != addonsArray.Length)
            {
                titlemessage = true;
                AddErrLine("获奖者数量与积分奖励数量不一致");
                return false;
            }

            if (IsErr()) return false;

            for (int i = 0; i < addonsArray.Length; i++)
            {
                winneridArray[i] = Utils.StrToInt(addonsArray[i].Split('|')[0], 0);
                postidArray[i] = Utils.StrToInt(addonsArray[i].Split('|')[1], 0);
                winnernameArray[i] = addonsArray[i].Split('|')[2];
            }
            Bonus.CloseBonus(topicinfo, userid, postidArray, winneridArray, winnernameArray, costBonusArray, DNTRequest.GetFormString("valuableAnswers").Split(','), DNTRequest.GetFormInt("bestAnswer", 0));
            //发通知给得分用户
            if (DNTRequest.GetFormInt("sendmessage", 0) == 0)
                return true;
            for (int i = 0; i < winneridArray.Length; i++)
            {
                int bonus = TypeConverter.StrToInt(costBonusArray[i]);
                if (bonus != 0)
                {
                    BonusPostMessage(topicinfo, postidArray[i], winneridArray[i], postidArray[i] == DNTRequest.GetFormInt("bestAnswer", 0), bonus);
                }
            }
            return true;
        }

        private void BonusPostMessage(TopicInfo topicInfo, int pid, int answerUid, bool isBeta, int num)
        {
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】更改通知类型和颜色
            NoticeInfo ni = new NoticeInfo();
            ni.New = 1;
            ni.Postdatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ni.Type = NoticeType.Bouns;
            ni.Poster = username;
            ni.Posterid = userid;
            ni.Uid = answerUid;
            ni.Fromid = pid;
            ni.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#0AA\">您发表的 {0} 被 {1} 评为 {2} ,给予 {3}GB{4} 奖励</span>",
                "<a href=\"" + Urls.ShowTopicAspxRewrite(topicInfo.Tid, 0, topicInfo.Typeid) + "#" + pid + "\" target=\"_blank\" style=\"color:#0AA\">悬赏回复</a>",
                "<a href=\"" + UserInfoAspxRewrite(userid) + "\" target=\"_blank\" style=\"color:#0AA\">" + username + "</a>",
                isBeta ? "最佳种子" : "有价值的种子", num, Scoresets.GetScoreSet(Scoresets.GetTopicAttachCreditsTrans()).Name));
            Notices.CreateNoticeInfo(ni);
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
        }

        protected string TransAttachImgUbb(string message)
        {
            return Regex.Replace(message, @"\[attachimg\](\d+)\[/attachimg\]", "<a href='javascript:void(0)' aid='$1' class='floatimg'>[图片]</a><img id='img$1' src='attachment.aspx?attachmentid=$1' width='150' style='display:none;' />",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
        #endregion
    }
}
