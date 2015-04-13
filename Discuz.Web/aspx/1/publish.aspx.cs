using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using Discuz.Common;
using Discuz.Forum;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Plugin.Album;
using Discuz.Plugin.Space;

//【BT修改】
using System.Web;
using System.IO;
//【END BT修改】

namespace Discuz.Web
{
    /// <summary>
    /// 发表主题页面
    /// </summary>
    public class publish : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 主题信息
        /// </summary>
        public TopicInfo topic = new TopicInfo();
        /// <summary>
        /// 帖子信息
        /// </summary>
        public PostInfo postinfo = new PostInfo();
        /// <summary>
        /// 是否为主题帖
        /// </summary>
        public bool isfirstpost = true;
        /// <summary>
        /// 所属板块Id
        /// </summary>
        public int forumid = DNTRequest.GetInt("forumid", -1);
        /// <summary>
        /// 主题内容
        /// </summary>
        public string message = "";
        /// <summary>
        /// 是否允许发表主题
        /// </summary>
        public bool allowposttopic = true;
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
        public int bbcodeoff = 1;
        /// <summary>
        /// 是否使用签名
        /// </summary>
        public int usesig = ForumUtils.GetCookie("sigstatus") == "0" ? 0 : 1;
        /// <summary>
        /// 是否允许 [img] 标签
        /// </summary>
        public int allowimg;
        /// <summary>
        /// 是否受发帖灌水限制
        /// </summary>
        public int disablepost = 0;
        /// <summary>
        /// 允许的附件类型和大小数组
        /// </summary>
        public string attachextensions;
        /// <summary>
        /// 允许的附件类型
        /// </summary>
        public string attachextensionsnosize;
        /// <summary>
        /// 今天可上传附件大小
        /// </summary>
        public int attachsize;
        /// <summary>
        /// 主题附件购买积分策略信息
        /// </summary>
        public UserExtcreditsInfo userextcreditsinfo;
        /// <summary>
        /// 悬赏积分信息
        /// </summary>
        public UserExtcreditsInfo bonusextcreditsinfo;
        /// <summary>
        /// 所属版块信息
        /// </summary>
        public ForumInfo forum = new ForumInfo();
        /// <summary>
        /// 主题分类选项字串
        /// </summary>
        public string topictypeselectoptions;
        /// <summary>
        /// 相册列表
        /// </summary>
        public DataTable albumlist;
        /// <summary>
        /// 是否允许上传附件
        /// </summary>
        public bool canpostattach;
        /// <summary>
        /// 是否允许同时发布到相册
        /// </summary>
        public bool caninsertalbum = false;
        /// <summary>
        /// 交易积分
        /// </summary>
        public int creditstrans;
        /// <summary>
        /// 投票截止时间
        /// </summary>
        public string enddatetime = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
        /// <summary>
        /// 是否允许Html标题
        /// </summary>
        public bool canhtmltitle = false;
        /// <summary>
        /// 发帖人的个人空间Id
        /// </summary>
        public int spaceid = 0;
        /// <summary>
        /// 本版是否可用Tag
        /// </summary>
        public bool enabletag = false;
        /// <summary>
        /// 发帖的类型，如普通帖、悬赏帖等。。
        /// </summary>
        public string type = DNTRequest.GetString("type").ToLower();
        /// <summary>
        /// 当前登录用户的交易积分值, 仅悬赏帖时有效
        /// </summary>
        public float mybonustranscredits;
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool needlogin = false;
        /// <summary>
        /// 当前版块的分页id
        /// </summary>
        public int forumpageid = DNTRequest.GetInt("forumpage", 1);
        /// <summary>
        /// 开启html功能
        /// </summary>
        public int htmlon = 0;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public ShortUserInfo userinfo = new ShortUserInfo();
        /// <summary>
        /// 投票帖
        /// </summary>
        bool createpoll = DNTRequest.GetString("createpoll") == "1";
        /// <summary>
        /// 投票项
        /// </summary>
        string[] pollitem = { };
        /// <summary>
        /// 当前时间
        /// </summary>
        string curdatetime = Utils.GetDateTime();
        /// <summary>
        /// 权限校验提示信息
        /// </summary>
        string msg = "";
        /// <summary>
        /// 帖子标题信息
        /// </summary>
        string posttitle = DNTRequest.GetString(GeneralConfigs.GetConfig().Antispamposttitle);
        /// <summary>
        /// 帖子内容信息
        /// </summary>
        string postmessage = DNTRequest.GetString(GeneralConfigs.GetConfig().Antispampostmessage);
        /// <summary>
        /// 标签
        /// </summary>
        public string topictags = "";
        /// <summary>
        /// 当前帖的Html标题
        /// </summary>
        public string htmltitle = "";
        /// <summary>
        /// 附件列表
        /// </summary>
        public DataTable attachmentlist = new DataTable();
        /// <summary>
        /// 用户组列表
        /// </summary>
        public Discuz.Common.Generic.List<UserGroupInfo> userGroupInfoList = UserGroups.GetUserGroupList();
        /// <summary>
        /// 编辑器自定义按钮
        /// </summary>
        public string customeditbuttons;
        #endregion

        public int topicid = 0;
        public bool needaudit = false;
        AlbumPluginBase apb = AlbumPluginProvider.GetInstance();
        public int fromindex = DNTRequest.GetInt("fromindex", 0);

        //【BT修改】发布页面所需变量

        /// <summary>
        /// 所发布资源的类型
        /// </summary>
        public string publishtype = "movie";
        /// <summary>
        /// 种子类别的中文描述，如movie->电影
        /// </summary>
        public string typedescription = "电影";
        /// <summary>
        /// 为了从aspx文件向cs传递参数而设置的一个变量
        /// </summary>
        public string infoselectionstring = "";
        /// <summary>
        /// 每个版块单独的发布提醒
        /// </summary>
        public string publishnote = "";
        /// <summary>
        /// 上传种子的seedinfo
        /// </summary>
        public PTSeedinfo seedinfo = new PTSeedinfo();
        /// <summary>
        /// 特殊主题
        /// </summary>
        public string special = DNTRequest.GetString("type").ToLower();
        /// <summary>
        /// 用户是否可以更改种子类别(对于publish无用)
        /// </summary>
        public bool canchangetype = true;


        protected override void ShowPage()
        {
            if (oluserinfo.Groupid == 4)
            {
                AddErrLine("你所在的用户组，为禁止发言"); return;
            }

            #region 临时帐号发帖
            //int realuserid = -1;
            //bool tempaccountspost = false;
            //string tempusername = DNTRequest.GetString("tempusername");
            //if (!Utils.StrIsNullOrEmpty(tempusername) && tempusername != username)
            //{
            //    realuserid = Users.CheckTempUserInfo(tempusername, DNTRequest.GetString("temppassword"), DNTRequest.GetInt("question", 0), DNTRequest.GetString("answer"));
            //    if (realuserid == -1)
            //    {
            //        AddErrLine("临时帐号登录失败，无法继续发帖。"); return;
            //    }
            //    else
            //    {
            //        userid = realuserid;
            //        username = tempusername;
            //        tempaccountspost = true;
            //    }
            //}


            #endregion

            if (userid > 0)
            {
                userinfo = Users.GetShortUserInfo(userid);
                //    if (userinfo != null)
                //    {
                //        usergroupinfo = UserGroups.GetUserGroupInfo(userinfo.Groupid);
                //        usergroupid = usergroupinfo.Groupid;
                //        useradminid = userinfo.Adminid;
                //    }
            }

            //【BT修改】检查权限，判断应该发到哪个版面

            //内容设置为空;  
            message = "请在此处填写详细的种子介绍,信息太少可能会被删种并扣罚上传流量。勿直接粘贴百度知道的内容，最好将图片上传到本地。";
            
            needlogin = false;
            allowposttopic = true;

            //检查用户是否达到发种要求20GB上传
            if ((btuserinfo.Extcredits4 < 20480 * 1048576M || btuserinfo.Extcredits3 < 20480 * 1048576M || btuserinfo.Extcredits7 < 10240 * 1048576M) && (usergroupid > 3 && usergroupid != 34) && HttpContext.Current.Request.Url.Host.ToLower().IndexOf("dnt3") < 0) //管理员和播种员除外
            {
                allowposttopic = false;
                AddErrLine("您的上传量尚未达到发布种子所需的\"最低统计上传20GB、并且最低统计下载20G、并且最低真实上传流量大于10GB\"，请先下载种子，续种上传，直至统计上传下载流量均大于20G并且真实上传流量大于10G，才能发布种子");
                //forumnav = "";
                return;
            }

            type = "";
            publishtype = DNTRequest.GetString("type").ToLower();
            forumid = PrivateBT.Type2Forum(PrivateBT.Str2Type(publishtype));
            if (forumid == 6)
            {
                forumid = 16;
                publishtype = "movie";
            }
            typedescription = PrivateBT.Type2Name(PrivateBT.Str2Type(publishtype));
            seedinfo.Type = PrivateBT.Forum2Type(forumid);
            special = publishtype;

            if (typedescription == "")
            {
                AddErrLine("错误发布类型");
                return;
            }

            ForumInfo finfo = Forums.GetForumInfo(forumid);
            publishnote = finfo.Description;
 

            #region 获取并检查版块信息
            forum = Forums.GetForumInfo(forumid);
            if (forum == null || forum.Layer == 0)
            {
                forum = new ForumInfo();//如果不初始化对象，则会报错
                allowposttopic = false;
                AddErrLine("错误的论坛ID"); return;
            }

            pagetitle = Utils.RemoveHtml(forum.Name);
            enabletag = (config.Enabletag & forum.Allowtag) == 1;

            if (forum.Applytopictype == 1)  //启用主题分类
                topictypeselectoptions = Forums.GetCurrentTopicTypesOption(forum.Fid, forum.Topictypes);

            if (forum.Password != "" && Utils.MD5(forum.Password) != ForumUtils.GetCookie("forum" + forumid + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                SetBackLink(base.ShowForumAspxRewrite(forumid, 0)); return;
            }
            needaudit = UserAuthority.NeedAudit(forum, useradminid, userid, usergroupinfo);
            smileyoff = 1 - forum.Allowsmilies;
            bbcodeoff = (forum.Allowbbcode == 1 && usergroupinfo.Allowcusbbcode == 1) ? 0 : 1;
            allowimg = forum.Allowimgcode;
            customeditbuttons = Caches.GetCustomEditButtonList();
            #endregion

            #region 访问和发帖权限校验
            if (!UserAuthority.VisitAuthority(forum, usergroupinfo, userid, ref msg))
            {
                AddErrLine(msg);
                needlogin = true; return;
            }

            if (!UserAuthority.PostAuthority(forum, usergroupinfo, userid, ref msg))
            {
                AddErrLine(msg);
                needlogin = true; return;
            }
            #endregion

            #region  附件信息绑定
            //得到用户可以上传的文件类型
            string attachmentTypeSelect = Attachments.GetAllowAttachmentType(usergroupinfo, forum);
            attachextensions = Attachments.GetAttachmentTypeArray(attachmentTypeSelect);
            attachextensionsnosize = Attachments.GetAttachmentTypeString(attachmentTypeSelect);
            //得到今天允许用户上传的附件总大小(字节)
            int MaxTodaySize = (userid > 0 ? MaxTodaySize = Attachments.GetUploadFileSizeByuserid(userid) : 0);
            attachsize = usergroupinfo.Maxsizeperday - MaxTodaySize;//今天可上传得大小
            //是否有上传附件的权限
            canpostattach = UserAuthority.PostAttachAuthority(forum, usergroupinfo, userid, ref msg);

            if (canpostattach && (userinfo != null && userinfo.Uid > 0) && apb != null && config.Enablealbum == 1 &&
            (UserGroups.GetUserGroupInfo(userinfo.Groupid).Maxspacephotosize - apb.GetPhotoSizeByUserid(userid) > 0))
            {
                caninsertalbum = true;
                albumlist = apb.GetSpaceAlbumByUserId(userid);
            }
            #endregion

            canhtmltitle = usergroupinfo.Allowhtmltitle == 1;

            #region 积分信息
            creditstrans = Scoresets.GetTopicAttachCreditsTrans();
            userextcreditsinfo = Scoresets.GetScoreSet(creditstrans);
            bonusextcreditsinfo = Scoresets.GetScoreSet(Scoresets.GetBonusCreditsTrans());
            #endregion

            #region 特殊主题权限判断
            if (forum.Allowspecialonly > 0 && !Utils.InArray(type, "poll,bonus,debate"))
            {
                AddErrLine(string.Format("当前版块 \"{0}\" 不允许发表普通主题", forum.Name)); return;
            }
            if (!UserAuthority.PostSpecialAuthority(forum, type, ref msg))
            {
                AddErrLine(msg); return;
            }
            if (!UserAuthority.PostSpecialAuthority(usergroupinfo, type, ref msg))
            {
                AddErrLine(msg);
                needlogin = true; return;
            }
            if (type == "bonus")
            {
                int creditTrans = Scoresets.GetBonusCreditsTrans();
                //当“交易积分设置”有效时(1-8的整数):
                if (creditTrans <= 0)
                {
                    //AddErrLine(string.Format("系统未设置\"交易积分设置\", 无法判断当前要使用的(扩展)积分字段, 暂时无法发布悬赏", usergroupinfo.Grouptitle)); return;
                    AddErrLine("系统未设置\"交易积分设置\", 无法判断当前要使用的(扩展)积分字段, 暂时无法发布悬赏"); return;
                }
                mybonustranscredits = Users.GetUserExtCredits(userid, creditTrans);
            }
            userGroupInfoList.Sort(delegate(UserGroupInfo x, UserGroupInfo y) { return (x.Readaccess - y.Readaccess) + (y.Groupid - x.Groupid); });
            #endregion

            //发帖不受审核、过滤、灌水等限制权限
            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(usergroupid);
            disablepost = admininfo != null ? admininfo.Disablepostctrl : usergroupinfo.Disableperiodctrl;
            //如果是提交...
            if (ispost)
            {
                #region 判断是否是灌水
                if (!UserAuthority.CheckPostTimeSpan(usergroupinfo, admininfo, oluserinfo, userinfo, ref msg))
                {
                    AddErrLine(msg); return;
                }
                #endregion

                //默认的返回页面和跳转等候时间，用于发生错误时的信息显示
                SetBackLink(string.Format("publish.aspx?restore=1&type={0}", publishtype));
                SetUrl(string.Format("publish.aspx?restore=1&type={0}", publishtype));
                SetMetaRefresh(9999);
                SetShowBackLink(true);

                #region 生成种子信息，解析种子文件，保存种子信息

                seedinfo = CreateSeedInfo();

                if (IsErr())
                    return;

                if (seedinfo == null)
                {
                    AddErrLine("生成种子信息失败，请检查分类信息输入");
                    return;
                }
                else if (seedinfo.SeedId <= 0)
                    return;

                #endregion

                ForumUtils.WriteCookie("postmessage", postmessage);

                #region 验证提交信息
                
                //常规项验证
                NormalValidate(admininfo, postmessage, userinfo);
                if (IsErr()) return;

                // 如果用户上传了附件,则检测用户是否有上传附件的权限
                if (ForumUtils.IsPostFile())
                {
                    if (Utils.StrIsNullOrEmpty(Attachments.GetAttachmentTypeArray(attachmentTypeSelect)))
                        AddErrLine("系统不允许上传附件");

                    if (!UserAuthority.PostAttachAuthority(forum, usergroupinfo, userid, ref msg))
                        AddErrLine(msg);
                }

                //发悬赏校验
                int topicprice = 0;
                bool isbonus = type == "bonus";
                ValidateBonus(ref topicprice, ref isbonus);

                //发特殊主题校验
                ValidatePollAndDebate();

                if (IsErr())
                {
                    PTLog.InsertSystemLog(PTLog.LogType.SeedPublish, PTLog.LogStatus.Error, "CreateTopic", string.Format("ERR -1: SEEDID:{0} -UID:{1} -IP:{2} -TITLE:{3}", seedinfo.SeedId, userid, ipaddress, seedinfo.TopicTitle));
                    AddErrLine(string.Format("发生错误! 种子上传成功但发布主题没有成功，错误代码 -1，请按照错误说明修改后尝试重新发布。 若多次发布失败，请向管理员反馈此问题并附上错误代码"));
                    return;
                }

                #endregion

                #region 创建主题和帖子，当失败时，回滚操作

                //是否存在隐藏内容
                int hide = (ForumUtils.IsHidePost(postmessage) && usergroupinfo.Allowhidecode == 1) ? 1 : 0;
                TopicInfo topicinfo = CreateTopic(admininfo, postmessage, isbonus, topicprice);

                if (IsErr() || topicinfo.Tid == -909)
                {
                    PTSeeds.UpdateSeedStatus(seedinfo.SeedId, -11);
                    PTLog.InsertSystemLog(PTLog.LogType.SeedPublish, PTLog.LogStatus.Error, "CreateTopic", string.Format("ERR -909: SEEDID:{0} -UID:{1} -IP:{2} -TITLE:{3}", seedinfo.SeedId, userid, ipaddress, seedinfo.TopicTitle));
                    AddErrLine(string.Format("<br/>发生错误! 发布主题没有成功，错误代码 -9，请仔细查看错误原因后尝试重新发布。 若多次发布失败，请向管理员反馈此问题并附上错误代码"));
                    return;
                }
                else if (IsErr() || topicinfo.Tid < 1)
                {
                    PTSeeds.UpdateSeedStatus(seedinfo.SeedId, -12);
                    PTLog.InsertSystemLog(PTLog.LogType.SeedPublish, PTLog.LogStatus.Error, "CreateTopic", string.Format("ERR -2: SEEDID:{0} -UID:{1} -IP:{2} -TITLE:{3}", seedinfo.SeedId, userid, ipaddress, seedinfo.TopicTitle));
                    AddErrLine(string.Format("<br/>发生错误! 发布主题没有成功，错误代码 -2，请仔细查看错误原因后尝试重新发布。 若多次发布失败，请向管理员反馈此问题并附上错误代码"));
                    return;
                }

                PostInfo postinfo = CreatePost(topicinfo);
                if (IsErr() || postinfo.Pid < 1)
                {
                    
                    PTSeeds.UpdateSeedStatus(seedinfo.SeedId, -13);
                    TopicAdmins.DeleteTopics(topicinfo.Tid.ToString(), (byte)1, false);
                    PTLog.InsertSystemLog(PTLog.LogType.SeedPublish, PTLog.LogStatus.Error, "CreateTopic", string.Format("ERR -3: SEEDID:{0} -UID:{1} -IP:{2} -TITLE:{3}", seedinfo.SeedId, userid, ipaddress, seedinfo.TopicTitle));
                    AddErrLine(string.Format("<br/>发生错误! 发布帖子没有成功，错误代码 -3，请仔细查看错误原因后尝试重新发布。 若多次发布失败，请向管理员反馈此问题并附上错误代码"));
                    return;
                }

                #endregion

                #region 处理附件
                //处理附件
                StringBuilder sb = new StringBuilder();
                AttachmentInfo[] attachmentinfo = null;
                string attachId = DNTRequest.GetFormString("attachid");
                if (!string.IsNullOrEmpty(attachId))
                {
                    attachmentinfo = Attachments.GetNoUsedAttachmentArray(userid, attachId);
                    Attachments.UpdateAttachment(attachmentinfo, topicinfo.Tid, postinfo.Pid, postinfo, ref sb, userid, config, usergroupinfo);
                }
                //加入相册
                if (config.Enablealbum == 1 && apb != null)
                    sb.Append(apb.CreateAttachment(attachmentinfo, usergroupid, userid, username));
                #endregion

                #region 添加日志的操作
                SpacePluginBase spb = SpacePluginProvider.GetInstance();
                if (DNTRequest.GetFormString("addtoblog") == "on" && spb != null)
                {
                    if (userid > 0 && userinfo.Spaceid > 0)
                        spb.CreateTopic(topicinfo, postinfo, attachmentinfo);
                    else
                        AddMsgLine("您的个人空间尚未开通, 无法同时添加为日志");
                }
                #endregion

                OnlineUsers.UpdateAction(olid, UserAction.PostTopic.ActionID, forumid, forum.Name, -1, "");

                #region 发帖成功，更新种子信息

                if (topicinfo.Tid > 0 && postinfo.Pid > 0)
                {
                    seedinfo.TopicId = topicinfo.Tid;
                    seedinfo.Status = 2;
                    PTSeeds.UpdateSeedEditWithSeed(seedinfo);//更新种子信息中的Topicid，完成发布
                    PTUsers.UpdateUserInfoPublishedCount(userid);
                    
                    //调试记录，检测主题信息消失的问题
                    TopicInfo newtest = Topics.GetTopicInfo(seedinfo.TopicId);
                    if(newtest.Tid > 0) PTLog.InsertSystemLogDebug(PTLog.LogType.SeedPublish, PTLog.LogStatus.Normal, "CreateTopicPost", string.Format("Seedid:{0}, Topicid:{1}, Postid:{2}", seedinfo.SeedId, topic.Tid, postinfo.Pid));
                    else PTLog.InsertSystemLogDebug(PTLog.LogType.SeedPublish, PTLog.LogStatus.Normal, "CreateTopicPost", string.Format("ERRR: Seedid:{0}, Topicid:{1}, Postid:{2}", seedinfo.SeedId, topic.Tid, postinfo.Pid));
                }
                else
                {                    
                    PTSeeds.UpdateSeedStatus(seedinfo.SeedId, -4);
                    PTLog.InsertSystemLog(PTLog.LogType.SeedPublish, PTLog.LogStatus.Error, "CreateTopicPost", string.Format("ERR:TID:{0} -PID:{1} -SEEDID:{2} -UID:{3} -IP:{4} -TITLE:{5}",
                        topicinfo.Tid, postinfo.Pid, seedinfo.SeedId, userid, ipaddress, seedinfo.TopicTitle));
                    AddErrLine(string.Format("发生错误! 种子上传没有成功，错误代码 -4，请尝试重新发布。 若多次发布失败，请向管理员反馈此问题"));
                    return;
                }

                #endregion

                #region 设置提示信息和跳转链接
                if (sb.Length > 0)
                {
                    //附件上传出现问题时的跳转显示
                    SetUrl(base.ShowTopicAspxRewrite(topicinfo.Tid, 0));
                    SetMetaRefresh(5);
                    SetShowBackLink(true);
                    if (infloat == 1)
                    {
                        AddErrLine(sb.ToString());
                        return;
                    }
                    else
                    {
                        sb.Insert(0, "<table cellspacing=\"0\" cellpadding=\"4\" border=\"0\"><tr><td colspan=2 align=\"left\"><span class=\"bold\"><nobr>发表主题成功,但图片/附件上传出现问题:</nobr></span><br /></td></tr>");
                        AddMsgLine(sb.Append("</table>").ToString());
                    }
                }
                else
                {
                    SetShowBackLink(false);
                    if (useradminid != 1)
                    {
                        //是否需要审核
                        if (UserAuthority.NeedAudit(forum, useradminid, userid, usergroupinfo) || topicinfo.Displayorder == -2)
                        {
                            ForumUtils.WriteCookie("postmessage", "");
                            SetLastPostedForumCookie();
                            SetUrl(base.ShowForumAspxRewrite(forumid, forumpageid));
                            SetMetaRefresh();
                            AddMsgLine("发表主题成功, 但需要经过审核才可以显示. 返回该版块");
                        }
                        else
                            PostTopicSucceed(Forums.GetValues(forum.Postcredits), topicinfo, topicinfo.Tid); //此函数会设置跳转时间为1，跳转链接为新帖子
                    }
                    else
                        PostTopicSucceed(Forums.GetValues(forum.Postcredits), topicinfo, topicinfo.Tid); //此函数会设置跳转时间为1，跳转链接为新帖子
                }
                #endregion

            }
            else //非提交操作
                AddLinkCss(BaseConfigs.GetForumPath + "templates/" + templatepath + "/editor.css", "css");
        }

        /// <summary>
        /// 创建主题信息
        /// </summary>
        /// <param name="admininfo"></param>
        /// <param name="postmessage"></param>
        /// <param name="isbonus"></param>
        /// <param name="topicprice"></param>
        /// <returns></returns>
        public TopicInfo CreateTopic(AdminGroupInfo admininfo, string postmessage, bool isbonus, int topicprice)
        {
            TopicInfo topicinfo = new TopicInfo();
            topicinfo.Fid = forumid;
            topicinfo.Iconid = (DNTRequest.GetInt("iconid", 0) < 0 || DNTRequest.GetInt("iconid", 0) > 15) ? 0 :
                                DNTRequest.GetInt("iconid", 0);
            message = Posts.GetPostMessage(usergroupinfo, admininfo, postmessage,
                (TypeConverter.StrToInt(DNTRequest.GetString("htmlon")) == 1));


            //【BT修改】标题由种子信息获得，添加种子id

            //组成seedinfo.TopicTitle以及该HtmlEncode过了，不用二次编码
            topicinfo.Title = seedinfo.TopicTitle;
            topicinfo.SeedId = seedinfo.SeedId;

            //topicinfo.Title = (useradminid == 1) ? Utils.HtmlEncode(DNTRequest.GetString(config.Antispamposttitle)) :
            //                   Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString(config.Antispamposttitle)));

            if (useradminid != 1 && usergroupid != 34 && (ForumUtils.HasBannedWord(topicinfo.Title) || ForumUtils.HasBannedWord(message)))
            {
                AddErrLine("对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!"); 
                return topicinfo;
            }


            //topicinfo.Title = (useradminid == 1) ? Utils.HtmlEncode(posttitle) :
            //       Utils.HtmlEncode(ForumUtils.BanWordFilter(posttitle));
            
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 



            if (useradminid != 1 && usergroupid != 34 && (ForumUtils.HasBannedWord(posttitle) || ForumUtils.HasBannedWord(postmessage)))
            {
                string bannedWord = ForumUtils.GetBannedWord(posttitle) == string.Empty ? ForumUtils.GetBannedWord(postmessage) : ForumUtils.GetBannedWord(posttitle);
                AddErrLine(string.Format("对不起, 您提交的内容包含不良信息  <font color=\"red\">{0}</font>, 请返回修改!", bannedWord));
                return topicinfo;
            }

            if (Utils.GetCookie("lasttopictitle") == Utils.MD5(topicinfo.Title) || Utils.GetCookie("lasttopicmessage") == Utils.MD5(message))
            {
                AddErrLine("请勿重复发帖：您本次提交的 帖子标题 或 帖子内容 与上次提交的相同：\"" + topicinfo.Title + "\"，请返回修改!");
                topicinfo.Tid = -909;
                return topicinfo;
            }

            topicinfo.Typeid = DNTRequest.GetInt("typeid", 0);
            if (usergroupinfo.Allowsetreadperm == 1)
                topicinfo.Readperm = DNTRequest.GetInt("topicreadperm", 0) > 255 ? 255 : DNTRequest.GetInt("topicreadperm", 0);

            topicinfo.Price = topicprice;
            topicinfo.Poster = username;
            topicinfo.Posterid = userid;
            topicinfo.Postdatetime = curdatetime;
            topicinfo.Lastpost = curdatetime;
            topicinfo.Lastposter = username;
            topicinfo.Displayorder = Topics.GetTitleDisplayOrder(usergroupinfo, useradminid, forum, topicinfo, message, disablepost);


            string htmltitle = DNTRequest.GetString("htmltitle").Trim();
            if (!Utils.StrIsNullOrEmpty(htmltitle) && Utils.HtmlDecode(htmltitle).Trim() != topicinfo.Title)
            {
                //按照  附加位/htmltitle(1位)/magic(3位)/以后扩展（未知位数） 的方式来存储  例： 11001
                topicinfo.Magic = 11000;
            }

            //标签(Tag)操作                
            string tags = DNTRequest.GetString("tags").Trim();
            string[] tagArray = null;
            if (enabletag && !Utils.StrIsNullOrEmpty(tags))
            {
                if (ForumUtils.InBanWordArray(tags))
                {
                    AddErrLine("标签中含有系统禁止词语，请返回修改!");
                    return topicinfo;
                }

                tagArray = Utils.SplitString(tags, " ", true, 2, 10);
                if (tagArray.Length > 0 && tagArray.Length <= 5)
                {
                    if (topicinfo.Magic == 0)
                        topicinfo.Magic = 10000;

                    topicinfo.Magic = Utils.StrToInt(topicinfo.Magic.ToString() + "1", 0);
                }
                else
                {
                    AddErrLine("超过标签数的最大限制或单个标签长度没有介于2-10之间，最多可填写 5 个标签");
                    return topicinfo;
                }
            }

            if (isbonus)
            {
                topicinfo.Special = 2;

                //检查积分是否足够
                if (mybonustranscredits < topicprice && usergroupinfo.Radminid != 1)
                {
                    AddErrLine(string.Format("无法进行悬赏<br /><br />您当前的{0}为 {1} {3}<br/>悬赏需要{0} {2} {3}，请返回修改!", bonusextcreditsinfo.Name, mybonustranscredits, topicprice, bonusextcreditsinfo.Unit));
                    return topicinfo;
                }
                else
                    Users.UpdateUserExtCredits(topicinfo.Posterid, Scoresets.GetBonusCreditsTrans(),
                                       -topicprice * (Scoresets.GetCreditsTax() + 1)); //计算税后的实际支付
            }

            if (type == "poll")
                topicinfo.Special = 1;

            if (type == "debate") //辩论帖
                topicinfo.Special = 4;

            if (!Moderators.IsModer(useradminid, userid, forumid))
                topicinfo.Attention = 1;

            if (ForumUtils.IsHidePost(postmessage) && usergroupinfo.Allowhidecode == 1)
                topicinfo.Hide = 1;

            topicinfo.Tid = Topics.CreateTopic(topicinfo);

            //canhtmltitle = config.Htmltitle == 1 && Utils.InArray(usergroupid.ToString(), config.Htmltitleusergroup);
            //canhtmltitle = config.Htmltitle == 1 && usergroupinfo.Allowhtml == 1;
            //保存htmltitle

            if (canhtmltitle && !Utils.StrIsNullOrEmpty(htmltitle) && htmltitle != topicinfo.Title)
                Topics.WriteHtmlTitleFile(Utils.RemoveUnsafeHtml(htmltitle), topicinfo.Tid);

            if (enabletag && tagArray != null && tagArray.Length > 0)
            {
                if (useradminid != 1 && ForumUtils.HasBannedWord(tags))
                {
                    string bannedWord = ForumUtils.GetBannedWord(tags);
                    AddErrLine(string.Format("标签中含有系统禁止词语 <font color=\"red\">{0}</font>，请返回修改!", bannedWord));
                    return topicinfo;
                }
                ForumTags.CreateTopicTags(tagArray, topicinfo.Tid, userid, curdatetime);
            }

            if (type == "debate")
            {
                DebateInfo debatetopic = new DebateInfo();
                debatetopic.Tid = topicinfo.Tid;
                debatetopic.Positiveopinion = DNTRequest.GetString("positiveopinion");
                debatetopic.Negativeopinion = DNTRequest.GetString("negativeopinion");
                debatetopic.Terminaltime = Convert.ToDateTime(DNTRequest.GetString("terminaltime"));
                Topics.CreateDebateTopic(debatetopic);
            }

            Topics.AddParentForumTopics(forum.Parentidlist.Trim(), 1);
            return topicinfo;
        }

        /// <summary>
        /// 创建主题帖信息
        /// </summary>
        /// <param name="topicinfo"></param>
        /// <returns></returns>
        public PostInfo CreatePost(TopicInfo topicinfo)
        {
            PostInfo postinfo = new PostInfo();
            postinfo.Fid = forumid;
            postinfo.Tid = topicinfo.Tid;
            postinfo.Poster = username;
            postinfo.Posterid = userid;
            
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】组成seedinfo.TopicTitle以及该HtmlEncode过了，不用二次编码
            postinfo.Title = seedinfo.TopicTitle;

            //原始
            //postinfo.Title = useradminid == 1 ? Utils.HtmlEncode(posttitle) :
            //     postinfo.Title = Utils.HtmlEncode(ForumUtils.BanWordFilter(posttitle));
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            postinfo.Postdatetime = curdatetime;
            postinfo.Message = message;
            postinfo.Ip = ipaddress;
            postinfo.Invisible = UserAuthority.GetTopicPostInvisible(forum, useradminid, userid, usergroupinfo, postinfo);
            postinfo.Usesig = TypeConverter.StrToInt(DNTRequest.GetString("usesig"));
            postinfo.Htmlon = (usergroupinfo.Allowhtml == 1 && (TypeConverter.StrToInt(DNTRequest.GetString("htmlon")) == 1)) ? 1 : 0;
            postinfo.Smileyoff = (smileyoff == 0 && forum.Allowsmilies == 1) ? TypeConverter.StrToInt(DNTRequest.GetString("smileyoff")) : smileyoff;
            postinfo.Bbcodeoff = (usergroupinfo.Allowcusbbcode == 1 && forum.Allowbbcode == 1) ? postinfo.Bbcodeoff = TypeConverter.StrToInt(DNTRequest.GetString("bbcodeoff")) : 1;
            postinfo.Parseurloff = TypeConverter.StrToInt(DNTRequest.GetString("parseurloff"));
            postinfo.Topictitle = topicinfo.Title;

            //if (Utils.GetCookie("lasttopictitle") == Utils.MD5(postinfo.Title) || Utils.GetCookie("lasttopicmessage") == Utils.MD5(postinfo.Message))
            //{
            //    AddErrLine("请勿重复发帖");
            //    return postinfo;
            //}

            try { postinfo.Pid = Posts.CreatePost(postinfo); }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.SeedPublish, PTLog.LogStatus.Exception, "CreatePost", ex.ToString());
                TopicAdmins.DeleteTopics(topicinfo.Tid.ToString(), false);
                AddErrLine("帖子保存出现异常");
            }
            try
            {
                Utils.WriteCookie("lasttopictitle", Utils.MD5(postinfo.Title));
                Utils.WriteCookie("lasttopicmessage", Utils.MD5(postinfo.Message));
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.SeedPublish, PTLog.LogStatus.Exception, "AppendCookie", ex.ToString());
            }

            //创建投票
            if (createpoll)
            {
                msg = Polls.CreatePoll(DNTRequest.GetFormString("PollItemname"), DNTRequest.GetString("multiple") == "on" ? 1 : 0,
                    DNTRequest.GetInt("maxchoices", 1), DNTRequest.GetString("visiblepoll") == "on" ? 1 : 0, DNTRequest.GetString("allowview") == "on" ? 1 : 0,
                    enddatetime, topicinfo.Tid, pollitem, userid);
            }
            return postinfo;
        }

        /// <summary>
        /// 发帖成功
        /// </summary>
        /// <param name="values">版块积分设置</param>
        /// <param name="topicinfo">主题信息</param>
        /// <param name="topicid">主题ID</param>
        private void PostTopicSucceed(float[] values, TopicInfo topicinfo, int topicid)
        {
            if (values != null) ///使用版块内积分
            {
                UserCredits.UpdateUserExtCredits(userid, values, false);
                if (userid > 0)
                    UserCredits.WriteUpdateUserExtCreditsCookies(values);
            }
            else ///使用默认积分                
            {
                UserCredits.UpdateUserCreditsByPostTopic(userid);
                if (userid > 0)
                    UserCredits.WriteUpdateUserExtCreditsCookies(Scoresets.GetUserExtCredits(CreditsOperationType.PostTopic));
            }

            //当使用伪aspx
            if (config.Aspxrewrite == 1)
                SetUrl(topicinfo.Special == 4 ? ShowDebateAspxRewrite(topicid) : ShowTopicAspxRewrite(topicid, 0));
            else
                SetUrl((topicinfo.Special == 4 ? ShowDebateAspxRewrite(topicid) : ShowTopicAspxRewrite(topicid, 0)) + "&forumpage=" + forumpageid);

            ForumUtils.WriteCookie("postmessage", "");
            ForumUtils.WriteCookie("clearUserdata", "forum");
            SetLastPostedForumCookie();

            SetMetaRefresh(1);
            MsgForward("posttopic_succeed");
            AddMsgLine("发布种子成功, 即将返回发布的种子<br />(<a href=\"" + base.ShowForumAspxRewrite(forumid, forumpageid) + "\">点击这里返回 " + forum.Name + "</a>)<br />");

            //通知应用有新主题
            //Sync.NewTopic(topicid.ToString(), topicinfo.Title, topicinfo.Poster, topicinfo.Posterid.ToString(), topicinfo.Fid.ToString(), "");
        }

        /// <summary>
        /// 常规项验证
        /// </summary>
        /// <param name="admininfo"></param>
        /// <param name="postmessage"></param>
        private void NormalValidate(AdminGroupInfo admininfo, string postmessage, ShortUserInfo user)
        {
            if (ForumUtils.IsCrossSitePost())
            {
                AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                return;
            }

            if (forum.Applytopictype == 1 && forum.Postbytopictype == 1 && !Utils.StrIsNullOrEmpty(topictypeselectoptions))
            {
                if (DNTRequest.GetString("typeid").Trim().Equals(""))
                    AddErrLine("主题类型不能为空");
                //检测所选主题分类是否有效
                if (!Forums.IsCurrentForumTopicType(DNTRequest.GetString("typeid").Trim(), forum.Topictypes))
                    AddErrLine("错误的主题类型");
            }
            if (Utils.StrIsNullOrEmpty(DNTRequest.GetString(config.Antispamposttitle).Trim().Replace("　", "")))
                AddErrLine("标题不能为空");
            
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】标题最大长度修改为255

            else if (DNTRequest.GetString(config.Antispamposttitle).Length > 255)
                AddErrLine("标题最大长度为255个字符,当前为 " + DNTRequest.GetString(config.Antispamposttitle).Length + " 个字符");

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 

            if (Utils.StrIsNullOrEmpty(postmessage.Replace("　", "")))
                AddErrLine("内容不能为空");

            if (admininfo != null && admininfo.Disablepostctrl != 1)
            {
                if (postmessage.Length < config.Minpostsize)
                    AddErrLine("您发表的内容过少, 系统设置要求帖子内容不得少于 " + config.Minpostsize + " 字多于 " + config.Maxpostsize + " 字");
                else if (postmessage.Length > config.Maxpostsize)
                    AddErrLine("您发表的内容过多, 系统设置要求帖子内容不得少于 " + config.Minpostsize + " 字多于 " + config.Maxpostsize + " 字");
            }

            //新用户广告强力屏蔽检查
            if ((config.Disablepostad == 1) && useradminid < 1)  //如果开启新用户广告强力屏蔽检查或是游客
            {
                if ((config.Disablepostadpostcount != 0 && user.Posts <= config.Disablepostadpostcount) ||
                    (config.Disablepostadregminute != 0 && DateTime.Now.AddMinutes(-config.Disablepostadregminute) <= Convert.ToDateTime(user.Joindate)))
                {
                    foreach (string regular in config.Disablepostadregular.Replace("\r", "").Split('\n'))
                    {
                        if (Posts.IsAD(regular, DNTRequest.GetString(config.Antispamposttitle), postmessage))
                            AddErrLine("发帖失败，内容中有不符合新用户强力广告屏蔽规则的字符，请检查标题和内容，如有疑问请与管理员联系");
                    }
                }
            }
        }

        /// <summary>
        /// 悬赏校验
        /// </summary>
        /// <param name="topicprice"></param>
        /// <param name="isbonus"></param>
        private void ValidateBonus(ref int topicprice, ref bool isbonus)
        {
            #region 悬赏/售价验证
            isbonus = type == "bonus";
            topicprice = 0;
            string tmpprice = DNTRequest.GetString("topicprice");

            if (Regex.IsMatch(tmpprice, "^[0-9]*[0-9][0-9]*$") || tmpprice == "")
            {
                topicprice = Utils.StrToInt(tmpprice, 0) > 32767 ? 32767 : Utils.StrToInt(tmpprice, 0);
                if (!isbonus)
                {
                    if (topicprice > usergroupinfo.Maxprice && usergroupinfo.Maxprice > 0)
                    {
                        if (userextcreditsinfo.Unit.Equals(""))
                            AddErrLine(string.Format("主题售价不能高于 {0} {1}", usergroupinfo.Maxprice, userextcreditsinfo.Name));
                        else
                            AddErrLine(string.Format("主题售价不能高于 {0} {1}({2})", usergroupinfo.Maxprice, userextcreditsinfo.Name, userextcreditsinfo.Unit));
                    }
                    else if (topicprice > 0 && usergroupinfo.Maxprice <= 0)
                        AddErrLine(string.Format("您当前的身份 \"{0}\" 未被允许出售主题", usergroupinfo.Grouptitle));
                    else if (topicprice < 0)
                        AddErrLine("主题售价不能为负数");
                }
                else
                {

                    if (usergroupinfo.Allowbonus == 0)
                        AddErrLine(string.Format("您当前的身份 \"{0}\" 未被允许进行悬赏", usergroupinfo.Grouptitle));
                    if (topicprice > mybonustranscredits)
                        AddErrLine(string.Format("您悬赏的{0},已超过您所能支付的范围", bonusextcreditsinfo.Name));
                    if (topicprice < usergroupinfo.Minbonusprice || topicprice > usergroupinfo.Maxbonusprice)
                        AddErrLine(string.Format("悬赏价格超出范围, 您应在 {0} - {1} {2}{3} 范围内进行悬赏", usergroupinfo.Minbonusprice, usergroupinfo.Maxbonusprice,
                            bonusextcreditsinfo.Unit, bonusextcreditsinfo.Name));
                }
            }
            else
            {
                if (!isbonus)
                    AddErrLine("主题售价只能为整数");
                else
                    AddErrLine("悬赏价格只能为整数");
            }
            #endregion
        }

        /// <summary>
        /// 投票和辩论校验
        /// </summary>
        /// <param name="createpoll"></param>
        /// <param name="pollitem"></param>
        private void ValidatePollAndDebate()
        {
            #region 投票验证
            if (DNTRequest.GetString("createpoll") == "1")
            {
                // 验证用户是否有发布投票的权限
                if (usergroupinfo.Allowpostpoll != 1)
                    AddErrLine(string.Format("您当前的身份 \"{0}\" 没有发布投票的权限", usergroupinfo.Grouptitle));

                pollitem = Utils.SplitString(DNTRequest.GetString("PollItemname"), "\r\n");
                if (pollitem.Length < 2)
                    AddErrLine("投票项不得少于2个");
                else if (pollitem.Length > config.Maxpolloptions)
                    AddErrLine(string.Format("系统设置为投票项不得多于{0}个", config.Maxpolloptions));
                else
                {
                    for (int i = 0; i < pollitem.Length; i++)
                    {
                        if (pollitem[i].Trim().Equals(""))
                            AddErrLine("投票项不能为空");
                    }
                }

                enddatetime = DNTRequest.GetString("enddatetime");
                if (!Utils.IsDateString(enddatetime))
                    AddErrLine("投票结束日期格式错误");
            }
            #endregion

            #region 辩论验证
            if (type == "debate")
            {
                if (usergroupinfo.Allowdebate != 1)
                    AddErrLine(string.Format("您当前的身份 \"{0}\" 没有发起辩论的权限", usergroupinfo.Grouptitle)); ;
                if (DNTRequest.GetString("positiveopinion") == "")
                    AddErrLine("正方观点不能为空");
                if (DNTRequest.GetString("negativeopinion") == "")
                    AddErrLine("反方观点不能为空");
                if (!Utils.IsDateString(DNTRequest.GetString("terminaltime")))
                    AddErrLine("结束日期格式不正确");
            }
            #endregion
        }

        /// <summary>
        /// 设置最后发帖版块Cookie 
        /// </summary>
        private void SetLastPostedForumCookie()
        {
            Utils.WriteCookie("lastpostedforum", forum.Fid.ToString(), 525600);
        }

        protected string AttachmentList()
        {
            return "";
        }


        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
        //【BT修改】新增函数

        /// <summary>
        /// 生成上传种子的seedinfo
        /// </summary>
        /// <returns></returns>
        private PTSeedinfo CreateSeedInfo()
        {
            //创建种子信息类，获得分类信息
            string curdatetime = Utils.GetDateTime();

            PTSeedinfo seedinfo = new PTSeedinfo();
            seedinfo.Uid = userid;
            seedinfo.DownloadRatio = 1;//刚发布的种子，下载系数均为0.3

            if (publishtype == "movie")
            {
                seedinfo.Type = 1;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("movie_cname"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("movie_ename"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("movie_imdb"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("movie_type"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("movie_resolution"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("movie_video"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("movie_audio"));
                seedinfo.Info8 = Utils.HtmlEncode(DNTRequest.GetString("movie_director"));
                seedinfo.Info9 = Utils.HtmlEncode(DNTRequest.GetString("movie_actor"));
                seedinfo.Info10 = Utils.HtmlEncode(DNTRequest.GetString("movie_year"));
                seedinfo.Info11 = Utils.HtmlEncode(DNTRequest.GetString("movie_region"));
                seedinfo.Info12 = Utils.HtmlEncode(DNTRequest.GetString("movie_language"));
                seedinfo.Info13 = Utils.HtmlEncode(DNTRequest.GetString("movie_subtitle"));
                seedinfo.Info14 = Utils.HtmlEncode(DNTRequest.GetString("movie_rank"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info2 == "" || seedinfo.Info3 == "" || seedinfo.Info4 == "" || seedinfo.Info14 == "" || seedinfo.Info5 == "" || seedinfo.Info10 == "" || seedinfo.Info11 == "" || seedinfo.Info12 == "" || seedinfo.Info13 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info10, seedinfo.Info11, seedinfo.Info1, seedinfo.Info2, seedinfo.Info4, seedinfo.Info13, seedinfo.Info5, seedinfo.Info14);
            }
            else if (publishtype == "tv")
            {
                seedinfo.Type = 2;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("tv_region"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("tv_cname"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("tv_ename"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("tv_season"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("tv_language"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("tv_resolution"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("tv_subtitle"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info2 == "" || seedinfo.Info3 == "" || seedinfo.Info5 == "" || seedinfo.Info6 == "" || seedinfo.Info7 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info8, seedinfo.Info7, seedinfo.Info6);
            }
            else if (publishtype == "comic")
            {
                seedinfo.Type = 3;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("comic_region"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("comic_cname"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("comic_ename"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("comic_season"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("comic_type"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("comic_language"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("comic_format"));
                seedinfo.Info8 = Utils.HtmlEncode(DNTRequest.GetString("comic_source"));
                seedinfo.Info9 = Utils.HtmlEncode(DNTRequest.GetString("comic_subtitle"));
                seedinfo.Info10 = Utils.HtmlEncode(DNTRequest.GetString("comic_subtitlegroup"));
                seedinfo.Info11 = Utils.HtmlEncode(DNTRequest.GetString("comic_year"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info2 == "" || seedinfo.Info3 == "" || seedinfo.Info5 == "" || seedinfo.Info6 == "" || seedinfo.Info7 == "" || seedinfo.Info8 == "" || seedinfo.Info9 == "" || seedinfo.Info10 == "" || seedinfo.Info11 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info1, seedinfo.Info5, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info9, seedinfo.Info10, seedinfo.Info8, seedinfo.Info7);
            }
            else if (publishtype == "music")
            {
                seedinfo.Type = 4;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("music_region"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("music_type"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("music_artist"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("music_name"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("music_year"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("music_language"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("music_format"));
                seedinfo.Info8 = Utils.HtmlEncode(DNTRequest.GetString("music_bps"));
                seedinfo.Info9 = Utils.HtmlEncode(DNTRequest.GetString("music_company"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info2 == "" || seedinfo.Info3 == "" || seedinfo.Info4 == "" || seedinfo.Info5 == "" || seedinfo.Info6 == "" || seedinfo.Info7 == "" || seedinfo.Info8 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info1, seedinfo.Info2, seedinfo.Info5, seedinfo.Info3, seedinfo.Info4, seedinfo.Info7, seedinfo.Info8, seedinfo.Info9);
            }
            else if (publishtype == "game")
            {
                seedinfo.Type = 5;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("game_platform"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("game_cname"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("game_ename"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("game_type"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("game_language"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("game_format"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("game_company"));
                seedinfo.Info8 = Utils.HtmlEncode(DNTRequest.GetString("game_region"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info2 == "" || seedinfo.Info3 == "" || seedinfo.Info4 == "" || seedinfo.Info5 == "" || seedinfo.Info6 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info5, seedinfo.Info6);
            }
            else if (publishtype == "discovery")
            {
                seedinfo.Type = 6;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("discovery_cname"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("discovery_ename"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("discovery_type"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("discovery_language"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("discovery_format"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("discovery_resolution"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("discovery_subtitle"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info2 == "" || seedinfo.Info3 == "" || seedinfo.Info4 == "" || seedinfo.Info5 == "" || seedinfo.Info6 == "" || seedinfo.Info7 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info3, seedinfo.Info1, seedinfo.Info2, seedinfo.Info7, seedinfo.Info6, seedinfo.Info5);
            }
            else if (publishtype == "sport")
            {
                seedinfo.Type = 7;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("sport_year"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("sport_cname"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("sport_ename"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("sport_type"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("sport_language"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("sport_format"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("sport_resolution"));
                seedinfo.Info8 = Utils.HtmlEncode(DNTRequest.GetString("sport_subtitle"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info2 == "" || seedinfo.Info4 == "" || seedinfo.Info5 == "" || seedinfo.Info6 == "" || seedinfo.Info7 == "" || seedinfo.Info8 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info4, seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info5, seedinfo.Info7, seedinfo.Info6);
            }
            else if (publishtype == "entertainment")
            {
                seedinfo.Type = 8;//
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_ename"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_region"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_cname"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_year"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_brief"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_language"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_format"));
                seedinfo.Info8 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_resolution"));
                seedinfo.Info9 = Utils.HtmlEncode(DNTRequest.GetString("entertainment_subtitle"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info2 == "" || seedinfo.Info3 == "" || seedinfo.Info4 == "" || seedinfo.Info5 == "" || seedinfo.Info6 == "" || seedinfo.Info7 == "" || seedinfo.Info8 == "" || seedinfo.Info9 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info5, seedinfo.Info9, seedinfo.Info8, seedinfo.Info7);
            }
            else if (publishtype == "software")
            {
                seedinfo.Type = 9;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("software_cname"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("software_ename"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("software_language"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("software_type"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("software_format"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("software_year"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info3 == "" || seedinfo.Info4 == "" || seedinfo.Info5 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info4, seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info5);
            }
            else if (publishtype == "staff")
            {
                seedinfo.Type = 10;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("staff_cname"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("staff_ename"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("staff_type"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("staff_language"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("staff_format"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("staff_year"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info3 == "" || seedinfo.Info4 == "" || seedinfo.Info5 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info3, seedinfo.Info1, seedinfo.Info2, seedinfo.Info4, seedinfo.Info5);
            }
            else if (publishtype == "video")
            {
                seedinfo.Type = 11;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("video_year"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("video_region"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("video_cname"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("video_ename"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("video_language"));
                seedinfo.Info6 = Utils.HtmlEncode(DNTRequest.GetString("video_format"));
                seedinfo.Info7 = Utils.HtmlEncode(DNTRequest.GetString("video_resolution"));
                seedinfo.Info8 = Utils.HtmlEncode(DNTRequest.GetString("video_subtitle"));
                seedinfo.Info9 = Utils.HtmlEncode(DNTRequest.GetString("video_type"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info2 == "" || seedinfo.Info3 == "" || seedinfo.Info5 == "" || seedinfo.Info6 == "" || seedinfo.Info7 == "" || seedinfo.Info8 == "" || seedinfo.Info9 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info9, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info8, seedinfo.Info7, seedinfo.Info6);
            }
            else if (publishtype == "other")
            {
                seedinfo.Type = 12;
                seedinfo.Info1 = Utils.HtmlEncode(DNTRequest.GetString("other_cname"));
                seedinfo.Info2 = Utils.HtmlEncode(DNTRequest.GetString("other_ename"));
                seedinfo.Info3 = Utils.HtmlEncode(DNTRequest.GetString("other_brief"));
                seedinfo.Info4 = Utils.HtmlEncode(DNTRequest.GetString("other_format"));
                seedinfo.Info5 = Utils.HtmlEncode(DNTRequest.GetString("other_year"));
                TitleFilter(ref seedinfo);
                if (seedinfo.Info1 == "" || seedinfo.Info4 == "")
                {
                    AddErrLine("请完善种子信息!");
                    return null;
                }
                seedinfo.TopicTitle = FillTitle(seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4);
            }
            else
            {
                AddErrLine("错误的种子分类!请确认您是否从正确途径提交种子");
                return null;
            }
            if (seedinfo.TopicTitle.Length > 255)
            {
                AddErrLine("标题最大长度为255个字符,当前为 " + seedinfo.TopicTitle.Length.ToString() + " 个字符");
                return null;
            }


            //BT种子文件相关的操作
            //////////////////////////////////////////////////////////////////////////
            HttpFileCollection files = HttpContext.Current.Request.Files;
            HttpPostedFile postedFile = files[0];   //得到提交一个的文件
            if (postedFile.FileName == "")          //文件不存在时
            {
                AddErrLine("请选择种子文件！");
                return null;
            }

            Stream fileDataStream = postedFile.InputStream;

            //得到文件大小
            int fileLength = postedFile.ContentLength;

            //创建数组
            byte[] fileData = new byte[fileLength];

            //把文件流填充到数组
            fileDataStream.Read(fileData, 0, fileLength);

            seedinfo.FileName = postedFile.FileName;

            //
            seedinfo.LastLive = DateTime.Now.AddMinutes(-1);
            seedinfo.PostDateTime = DateTime.Now;
            seedinfo.UserName = username;

            int seedid = PTTorrent.SaveSeed(fileData, ref seedinfo);
            if (seedid == 0)
            {
                //IMAX等帐号特殊处理，与正常发布流程一致，但是直接跳转到种子页面
                if (PrivateBT.IsServerUser(userid) || userid == 13)
                {
                    SetShowBackLink(false);
                    SetUrl(base.ShowTopicAspxRewrite(seedinfo.TopicId, 0));
                    MsgForward("posttopic_succeed");
                    SetMetaRefresh();
                    seedinfo.SeedId = 0;
                    return seedinfo;
                }
                else if(seedinfo.Status == 2 || seedinfo.Status == 3)
                {
                    AddErrLine("您上传的种子是重复种子，请稍等5秒，即将为您自动跳转到之前已经发布的种子，请直接续种即可");
                    SetShowBackLink(true);
                    SetUrl(base.ShowTopicAspxRewrite(seedinfo.TopicId, 0));
                    SetMetaRefresh(5);
                    return null;
                }
                else if (seedinfo.Status == 6)
                {
                    AddErrLine("您上传的种子已被禁止发布，且勿再次尝试发布，请再次仔细阅读相关板块规则，感谢您的分享！");
                    return null;
                }
                else
                {
                    AddErrLine("您上传的种子存在重复，但是重复种子的状态不正常，无法显示。 请到意见投诉版说明情况，等待处理，感谢您的反馈");
                    return null;
                }
            }
            if (seedid < 0)
            {
                AddErrLine(string.Format("发生错误! 种子上传没有成功，错误代码{0}，请检查种子文件是否完好，系统只接受uTorrent制作的UTF-8编码的种子", seedid));
                return null;
            }
            //////////////////////////////////////////////////////////////////////////

            //注意：此时seedinfo.status为1，即为上传成功，若后续发帖出错，应置status为负数
            return seedinfo;
        }

        /// <summary>
        /// 生成标题，由参数列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string FillTitle(params string[] list)
        {
            string title = "";
            foreach (string str in list)
            {
                if (str != "") title += "[" + str.Trim() + "]";
            }
            return title;
        }

        /// <summary>
        /// 过滤标题信息
        /// </summary>
        /// <param name="seedinfo"></param>
        private void TitleFilter(ref PTSeedinfo seedinfo)
        {
            seedinfo.Info1 = seedinfo.Info1.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info2 = seedinfo.Info2.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info3 = seedinfo.Info3.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info4 = seedinfo.Info4.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info5 = seedinfo.Info5.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info6 = seedinfo.Info6.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info7 = seedinfo.Info7.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info8 = seedinfo.Info8.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info9 = seedinfo.Info9.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info10 = seedinfo.Info10.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info11 = seedinfo.Info11.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info12 = seedinfo.Info12.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info13 = seedinfo.Info13.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info14 = seedinfo.Info14.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
        }

        //【END BT修改】新增函数
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
    }
}
