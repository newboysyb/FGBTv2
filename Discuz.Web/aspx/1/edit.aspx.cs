using System;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Web.UI;
using Discuz.Common.Generic;
using Discuz.Plugin.Album;

namespace Discuz.Web
{
    /// <summary>
    /// 编辑帖子页面，修改自编辑帖子页面
    /// </summary>
    public class edit : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 帖子所属版块Id
        /// </summary>
        public int forumid;
        /// <summary>
        /// 帖子信息
        /// </summary>
        public PostInfo postinfo;
        /// <summary>
        /// 帖子所属主题信息
        /// </summary>
        public TopicInfo topic;
        /// <summary>
        /// 是否为主题帖
        /// </summary>
        public bool isfirstpost = false;
        /// <summary>
        /// 投票选项列表
        /// </summary>
        public DataTable polloptionlist;
        /// <summary>
        /// 投票帖类型
        /// </summary>
        public PollInfo pollinfo;
        /// <summary>
        /// 辩论贴类型
        /// </summary>
        public DebateInfo debateinfo;
        /// <summary>
        /// 附件列表
        /// </summary>
        public DataTable attachmentlist;
        /// <summary>
        /// 附件数
        /// </summary>
        public int attachmentcount;
        /// <summary>
        /// 帖子内容
        /// </summary>
        public string message;
        /// <summary>
        /// 是否进行URL解析
        /// </summary>
        public int parseurloff;
        /// <summary>
        /// 是否进行表情解析
        /// </summary>
        public int smileyoff;
        /// <summary>
        /// 是否进行Discuz!NT代码解析
        /// </summary>
        public int bbcodeoff;
        /// <summary>
        /// 是否使用签名
        /// </summary>
        public int usesig;
        /// <summary>
        /// 是否允许[img]代码
        /// </summary>
        public int allowimg;
        /// <summary>
        /// 是否受发帖控制限制
        /// </summary>
        public int disablepostctrl;
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
        /// 积分策略信息
        /// </summary>
        public UserExtcreditsInfo userextcreditsinfo;
        /// <summary>
        /// 悬赏积分信息
        /// </summary>
        public UserExtcreditsInfo bonusextcreditsinfo = new UserExtcreditsInfo();
        /// <summary>
        /// 所属版块信息
        /// </summary>
        public ForumInfo forum = new ForumInfo();
        /// <summary>
        /// 当前版块的主题类型选项
        /// </summary>
        public string topictypeselectoptions;
        /// <summary>
        /// 表情分类
        /// </summary>
        public DataTable smilietypes;
        /// <summary>
        /// 相册列表
        /// </summary>
        public DataTable albumlist;
        /// <summary>
        /// 是否允许上传附件
        /// </summary>
        public bool canpostattach;
        /// <summary>
        /// 是否允许将图片放入相册
        /// </summary>
        public bool caninsertalbum;
        /// <summary>
        /// 是否显示下载链接
        /// </summary>		
        public bool allowviewattach = false;
        /// <summary>
        /// 是否有Html标题的权限
        /// </summary>
        public bool canhtmltitle = false;
        /// <summary>
        /// 当前帖的Html标题
        /// </summary>
        public string htmltitle = string.Empty;
        /// <summary>
        /// 主题所用标签
        /// </summary>
        public string topictags = string.Empty;
        /// <summary>
        /// 本版是否启用了Tag
        /// </summary>
        public bool enabletag = false;
        /// <summary>
        /// 当前版块的分页id
        /// </summary>
        public int forumpageid = DNTRequest.GetInt("forumpage", 1);
        /// <summary>
        /// 开启html功能
        /// </summary>
        public int htmlon = 0;
        /// <summary>
        /// 权限校验提示信息
        /// </summary>
        string msg = "";
        /// <summary>
        /// 是否允许编辑帖子, 初始false为不允许
        /// </summary>
        bool alloweditpost = false;
        /// <summary>
        /// 当前登录用户的交易积分值, 仅悬赏帖时有效
        /// </summary>
        public float mybonustranscredits;
        /// <summary>
        /// 悬赏使用积分字段
        /// </summary>
        public int bonusCreditsTrans = Scoresets.GetBonusCreditsTrans();
        /// <summary>
        /// 
        /// </summary>
        public ShortUserInfo userinfo;
        /// <summary>
        /// 用户组列表
        /// </summary>
        public Discuz.Common.Generic.List<UserGroupInfo> userGroupInfoList = UserGroups.GetUserGroupList();
        public int topicid = DNTRequest.GetInt("topicid", -1);
        public int postid = DNTRequest.GetInt("postid", -1);
        public int pageid = DNTRequest.GetInt("pageid", 1);
        public bool needaudit = false;
        string postMessage = DNTRequest.GetString(GeneralConfigs.GetConfig().Antispampostmessage);
        string postTitle = DNTRequest.GetString(GeneralConfigs.GetConfig().Antispamposttitle);
        /// <summary>
        /// 编辑器自定义按钮
        /// </summary>
        public string customeditbuttons;

        #endregion

        AlbumPluginBase apb = AlbumPluginProvider.GetInstance();

        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
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
        /// 种子ID
        /// </summary>
        public int seedid;
        /// <summary>
        /// 所发布资源的类型
        /// </summary>
        public int type;
        /// <summary>
        /// 是否受发帖灌水限制
        /// </summary>
        public int disablepost = 0;
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool needlogin = true;
        /// <summary>
        /// 特殊主题
        /// </summary>
        public string special = DNTRequest.GetString("type").ToLower();
        /// <summary>
        /// 用户是否可以更改种子类别
        /// </summary>
        public bool canchangetype = false;
        /// <summary>
        /// 此次编辑是否更改种子类别
        /// </summary>
        public bool changetype = false;
        /// <summary>
        /// 编辑前种子的类别
        /// </summary>
        public int oldtype = 0;

        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 


        protected override void ShowPage()
        {
            //pagetitle = "编辑帖子";
            #region 判断是否是灌水
            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(usergroupid);
            this.disablepostctrl = 0;
            if (admininfo != null)
                disablepostctrl = admininfo.Disablepostctrl;
            #endregion

            if (userid < 1)
            {
                forum = new ForumInfo();
                topic = new TopicInfo();
                postinfo = new PostInfo();
                AddErrLine("您尚未登录");
                return;
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】从种子id获取帖子信息

            seedid = DNTRequest.GetInt("seedid", -1);
            if (seedid < 1)
            {
                AddErrLine("无效的种子ID");
                return;
            }
            seedinfo = PTSeeds.GetSeedInfoFull(seedid);           //获得种子信息
            if (seedid < 1)
            {
                AddErrLine("种子不存在");
                return;
            }

            //获得对应的帖子和主题信息
            topicid = seedinfo.TopicId;
            postid = Posts.GetFirstPostId(topicid);
            if (topicid < 1 || postid < 1)
            {
                AddErrLine("帖子ID无效");
                return;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 

            #region 获取帖子和主题相关信息
            // 如果帖子ID非数字
            if (postid == -1)
            {
                AddErrLine("无效的帖子ID");
                return;
            }

            postinfo = Posts.GetPostInfo(topicid, postid);
            // 如果帖子不存在
            if (postinfo == null)
            {
                AddErrLine("不存在的帖子ID");
                return;
            }
            pagetitle = (postinfo.Title == "") ? "编辑帖子" : postinfo.Title;
            htmlon = postinfo.Htmlon;
            message = postinfo.Message;
            isfirstpost = postinfo.Layer == 0;

            // 获取主题ID
            if (topicid != postinfo.Tid || postinfo.Tid == -1)
            {
                AddErrLine("无效的主题ID");
                return;
            }

            // 获取该主题的信息
            topic = Topics.GetTopicInfo(postinfo.Tid);
            // 如果该主题不存在
            if (topic == null)
            {
                AddErrLine("不存在的主题ID");
                return;
            }

            if (topic.Special == 1 && postinfo.Layer == 0)
            {
                pollinfo = Polls.GetPollInfo(topic.Tid);
                polloptionlist = Polls.GetPollOptionList(topic.Tid);
            }

            if (topic.Special == 4 && postinfo.Layer == 0)
            {
                debateinfo = Debates.GetDebateTopic(topic.Tid);
            }

            #endregion

            #region 获取并检查版块信息
            ///得到所在版块信息
            forumid = topic.Fid;
            forum = Forums.GetForumInfo(forumid);
            needaudit = UserAuthority.NeedAudit(forum, useradminid, topic, userid, disablepostctrl, usergroupinfo);
            // 如果该版块不存在
            if (forum == null || forum.Layer == 0)
            {
                AddErrLine("版块已不存在");
                forum = new ForumInfo();
                return;
            }

            if (!Utils.StrIsNullOrEmpty(forum.Password) && Utils.MD5(forum.Password) != ForumUtils.GetCookie("forum" + forumid + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                SetBackLink(base.ShowForumAspxRewrite(forumid, 0));
                return;
            }

            if (forum.Applytopictype == 1)  //启用主题分类
                topictypeselectoptions = Forums.GetCurrentTopicTypesOption(forum.Fid, forum.Topictypes);
            customeditbuttons = Caches.GetCustomEditButtonList();
            #endregion

            //是否有编辑帖子的权限
            if (!UserAuthority.CanEditPost(postinfo, userid, useradminid, ref msg))
            {
                AddErrLine(msg);
                return;
            }

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

            userinfo = Users.GetShortUserInfo(userid);
            if (canpostattach && (config.Enablealbum == 1) && apb != null &&
                (UserGroups.GetUserGroupInfo(userinfo.Groupid).Maxspacephotosize - apb.GetPhotoSizeByUserid(userid) > 0))
            {
                caninsertalbum = true;
                albumlist = apb.GetSpaceAlbumByUserId(userid);
            }
            else
                caninsertalbum = false;

            attachmentlist = Attachments.GetAttachmentListByPid(postinfo.Pid);
            attachmentcount = attachmentlist.Rows.Count;
            //当前用户是否有允许下载附件权限
            allowviewattach = UserAuthority.DownloadAttachment(forum, userid, usergroupinfo);

            #endregion

            smileyoff = (!DNTRequest.IsPost()) ? postinfo.Smileyoff : 1 - forum.Allowsmilies;
            allowimg = forum.Allowimgcode;
            parseurloff = postinfo.Parseurloff;
            bbcodeoff = (usergroupinfo.Allowcusbbcode == 1) ? postinfo.Bbcodeoff : 1;
            usesig = postinfo.Usesig;
            userextcreditsinfo = Scoresets.GetScoreSet(Scoresets.GetTopicAttachCreditsTrans());
            if (bonusCreditsTrans > 0 && bonusCreditsTrans < 9)
            {
                bonusextcreditsinfo = Scoresets.GetScoreSet(bonusCreditsTrans);
                mybonustranscredits = Users.GetUserExtCredits(userid, bonusCreditsTrans);
            }

            //是否有访问当前版块的权限
            if (!UserAuthority.VisitAuthority(forum, usergroupinfo, userid, ref msg))
            {
                AddErrLine(msg);
                return;
            }

            // 判断当前用户是否有修改权限, 检查是否具有版主的身份
            if (!Moderators.IsModer(useradminid, userid, forumid))
            {
                if (postinfo.Posterid != userid)
                {
                    AddErrLine("你并非作者, 且你当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有修改该帖的权限");
                    return;
                }
                //被禁言用户不能编辑种子【不知道其他地方有没有校验。。】
                else if (userinfo.Groupid > 3 && userinfo.Groupid < 9)
                {
                    AddErrLine("你当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有修改帖子的权限");
                    return;
                }
                else if (config.Edittimelimit > 0 && Utils.StrDateDiffMinutes(postinfo.Postdatetime, config.Edittimelimit) > 0)
                {
                    AddErrLine("抱歉, 系统规定只能在帖子发表" + config.Edittimelimit + "分钟内才可以修改");
                    return;
                }
                else if (config.Edittimelimit == -1)
                {
                    AddErrLine("抱歉，系统不允许修改帖子");
                    return;
                }
            }


            #region htmltitle标题
            if (postinfo.Layer == 0)
                canhtmltitle = usergroupinfo.Allowhtmltitle == 1;

            if (Topics.GetMagicValue(topic.Magic, MagicType.HtmlTitle) == 1)
                htmltitle = Topics.GetHtmlTitle(topic.Tid).Replace("\"", "\\\"").Replace("'", "\\'");
            #endregion

            #region tag信息
            enabletag = (config.Enabletag & forum.Allowtag) == 1;
            if (enabletag && Topics.GetMagicValue(topic.Magic, MagicType.TopicTag) == 1)
            {
                foreach (TagInfo tag in ForumTags.GetTagsListByTopic(topic.Tid))
                {
                    if (tag.Orderid > -1)
                        topictags += string.Format(" {0}", tag.Tagname);
                }
                topictags = topictags.Trim();
            }
            #endregion
            userGroupInfoList.Sort(delegate(UserGroupInfo x, UserGroupInfo y) { return (x.Readaccess - y.Readaccess) + (y.Groupid - x.Groupid); });

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】


            //判断是否可以更换类别，普通用户发种，24小时内可以更改类别，版主不限时
            //判断当前用户是否有修改权限, 检查是否具有版主的身份
            if (!Moderators.IsModer(useradminid, userid, forumid))
            {
                if ((DateTime.Now -  DateTime.Parse(postinfo.Postdatetime)).TotalHours < 24)
                {
                    canchangetype = true;
                }
                else
                {
                    canchangetype = false;
                }
            }
            else
            {
                canchangetype = true;
            }


            //保种号规则，禁止其他人编辑
            //if (postinfo.Posterid != userid && userid != 1)
            //{
            //    if (PrivateBT.IsServerUserSeedReadOnly(seedinfo.Uid))
            //    {
            //        AddErrLine("您无权编辑该用户发布的种子，该种子发布者为服务器保种号");
            //        return;
            //    }
            //}

            //获取资源类型，是否要更改资源类型，此之后，如果要更改种子类别，则forumid表示目的板块的fid。
            publishtype = DNTRequest.GetString("type");
            if (publishtype != "" && publishtype != PrivateBT.Type2Str(seedinfo.Type) && canchangetype)
            {
                //更改资源类型
                oldtype = seedinfo.Type;
                type = PrivateBT.Str2Type(publishtype);
                if (type < 1)
                {
                    AddErrLine("错误的种子类型选择！");
                    return;
                }

                PTSeeds.ChangeSeedType(ref seedinfo, type);
                changetype = true;
            }
            else
            {
                type = seedinfo.Type;
            }
            publishtype = PrivateBT.Type2Str(type);
            typedescription = PrivateBT.Type2Name(type);
            forumid = PrivateBT.Type2Forum(type);
            special = publishtype;

            if (forumid == 6)
            {
                AddErrLine("错误的种子类别!");
                return;
            }

            //当种子类别和帖子分区不符时
            if (forumid != topic.Fid && !changetype)
            {
                TopicAdmins.MoveTopics(topic.Tid.ToString(), forumid, topic.Fid, 0);
                topic.Fid = forumid;

                AddErrLine(string.Format("读取种子类别发生错误! 系统已经尝试修正，请在此刷新页面<br/><br/>如果问题无法解决请联系管理员修正<br/>并包含以下信息：ERR-{0}-{1}-{2}-{3}", forumid, topic.Fid, seedinfo.Type, seedinfo.TopicId));
                return;
            }

            message = postinfo.Message;


            ForumInfo finfo = Forums.GetForumInfo(forumid);
            publishnote = finfo.Description;

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 


            //如果是提交...
            if (ispost)
            {
                //默认的返回页面和跳转等候时间，用于发生错误时的信息显示
                SetBackLink("edit.aspx?seedid=" + seedid.ToString());
                SetUrl("edit.aspx?seedid=" + seedid.ToString());
                SetMetaRefresh(9999);
                SetShowBackLink(true);

                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                //生成新的文件信息
                string old_title = "";
                seedinfo = CreateSeedInfo(seedid, ref old_title);
                if (seedinfo == null)
                {
                    AddErrLine("编辑种子信息失败！");
                    return;
                }

                //在保存帖子和主题的时候，系统会进行HtmlDecode工作，而seedinfo.TopicTitle之前在组成时已经htmlencode过了，防止二次编码
                postTitle = Utils.HtmlDecode(seedinfo.TopicTitle);
                postinfo.Title = postTitle;
                topic.Title = postTitle;

                //设置相关帖子信息
                SetPostInfo(admininfo, userinfo, Utils.StrToInt(DNTRequest.GetString("htmlon"), 0) == 1);

                if (IsErr()) return;

                //通过验证的用户可以编辑帖子
                if (Posts.UpdatePost(postinfo, userid, ipaddress) < 1) 
                { 
                    AddErrLine("更新帖子信息失败！"); 
                    return; 
                }

                //设置附件相关信息
                System.Text.StringBuilder sb = SetAttachmentInfo();

                if (IsErr()) return;

                UserCredits.UpdateUserCredits(userid);

                //如果需要移动分区
                if (changetype)
                {
                    if (seedinfo.TopSeed > 0) //更新置顶种子列表
                    {
                        PTSeeds.UpdateTopSeedList(oldtype);
                        PTSeeds.UpdateSeedTop(seedinfo.SeedId, false); //取消置顶（安全设定，不能给其他板块置顶）
                    }

                    TopicAdmins.MoveTopics(topic.Tid.ToString(), forumid, topic.Fid, 0);

                    //操作记录
                    if (Moderators.IsModer(useradminid, userid, topic.Fid))
                    {
                        PrivateBT.InsertSeedModLog(seedinfo.SeedId, string.Format("该种子被版主 {0} 执行 移动种子 ({1}->{2}) 操作", username, PrivateBT.Forum2Nmae(topic.Fid), PrivateBT.Forum2Nmae(forumid)), username, "", userid, 10);
                    }
                    else
                    {
                        PrivateBT.InsertSeedModLog(seedinfo.SeedId, string.Format("该种子被作者 {0} 执行 自移动种子 ({1}->{2}) 操作", username, PrivateBT.Forum2Nmae(topic.Fid), PrivateBT.Forum2Nmae(forumid)), username, "", userid, 11);
                    }
                    topic.Fid = forumid;
                }

                //执行种子信息更新
                if (DoUpdateSeedInfo(seedinfo, seedid, old_title) == null || IsErr())
                {
                    AddErrLine("编辑种子信息已失败！");
                    return;
                }

                #region 设置提示信息和跳转链接

                if (config.Aspxrewrite == 1)
                    SetUrl(string.Format("showtopic-{0}{1}", topic.Tid, config.Extname));
                else
                    SetUrl(string.Format("showtopic.aspx?topicid={0}&forumpage={1}", topic.Tid, forumpageid));
                
                //是否有图片上传附加信息显示
                if (sb.Length > 0)
                {
                    SetMetaRefresh(5);
                    SetShowBackLink(true);
                    if (infloat == 1)
                    {
                        AddErrLine(sb.ToString());
                        return;
                    }
                    else
                    {
                        sb.Insert(0, "<table cellspacing=\"0\" cellpadding=\"4\" border=\"0\"><tr><td colspan=2 align=\"left\"><span class=\"bold\"><nobr>编辑帖子成功,但图片/附件上传出现问题:</nobr></span><br /></td></tr>");
                        sb.Append("</table>");
                        AddMsgLine(sb.ToString());
                    }
                }
                else
                {
                    if (useradminid != 1 && (needaudit || topic.Displayorder == -2 || postinfo.Invisible == 1))
                    {
                        //可能需要审核
                        if (postinfo.Layer == 0)
                            SetUrl(base.ShowForumAspxRewrite(forumid, forumpageid));
                        else
                            SetUrl(base.ShowTopicAspxRewrite(topic.Tid, forumpageid));
                        AddMsgLine("编辑成功, 但需要经过版主审核才可以显示，您可以短消息曾经处理的版主加快审核速度");

                        SetMetaRefresh(5, base.ShowForumAspxRewrite(forumid, forumpageid));
                        SetShowBackLink(true);
                    }
                    else
                    {
                        //正常编辑，快速跳转
                        //MsgForward("editpost_succeed");
                        AddMsgLine("编辑帖子成功, 返回该主题");
                        SetMetaRefresh(1);
                        SetShowBackLink(true);
                    }
                }
                #endregion

                // 删除主题游客缓存
                if (postinfo.Layer == 0)
                    ForumUtils.DeleteTopicCacheFile(topic.Tid);
            }
            else
                AddLinkCss(BaseConfigs.GetForumPath + "templates/" + templatepath + "/editor.css", "css");
        }

        private int GetAttachmentUpdatedIndex(string attachmentId, string[] updatedAttArray)
        {
            for (int i = 0; i < updatedAttArray.Length; i++)
            {
                if (updatedAttArray[i] == attachmentId)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 验证帖子信息
        /// </summary>
        /// <param name="admininfo"></param>
        /// <param name="user"></param>
        /// <param name="ishtmlon"></param>
        private void SetPostInfo(AdminGroupInfo admininfo, ShortUserInfo user, bool ishtmlon)
        {
            if (postinfo.Layer == 0 && forum.Applytopictype == 1 && forum.Postbytopictype == 1 && topictypeselectoptions != string.Empty)
            {
                if (Utils.StrIsNullOrEmpty(DNTRequest.GetString("typeid")) || DNTRequest.GetString("typeid").Trim() == "0")
                {
                    AddErrLine("主题类型不能为空");
                    return;
                }

                if (!Forums.IsCurrentForumTopicType(DNTRequest.GetString("typeid").Trim(), forum.Topictypes))
                {
                    AddErrLine("错误的主题类型");
                    return;
                }
            }

            //这段代码有什么作用，和下面的SetAttachmentInfo方法做的事情是否有重复？能否拿掉？
            ///删除附件
            if (DNTRequest.GetInt("isdeleteatt", 0) == 1)
            {
                if (DNTRequest.GetFormInt("aid", 0) > 0 && Attachments.DeleteAttachment(DNTRequest.GetFormInt("aid", 0)) > 0)
                {
                    attachmentlist = Attachments.GetAttachmentListByPid(postinfo.Pid);
                    attachmentcount = Attachments.GetAttachmentCountByPid(postinfo.Pid);
                }
                AddLinkCss(BaseConfigs.GetForumPath + "templates/" + templatepath + "/editor.css", "css");
                // 帖子内容
                message = postinfo.Message;
                ispost = false;

                return;
            }
            
            #region 检查标题和内容信息
            if (string.IsNullOrEmpty(postTitle.Trim().Replace("　", "")) && postinfo.Layer == 0)
                AddErrLine("标题不能为空");
           
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】标题最大长度修改为255
            else if (postTitle.Length > 255)
                AddErrLine("标题最大长度为255个字符,当前为 " + postTitle.Length.ToString() + " 个字符");
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            
            //string postmessage = DNTRequest.GetString("message");
            if (postMessage.Equals("") || postMessage.Replace("　", "").Equals(""))
                AddErrLine("内容不能为空");

            if (admininfo != null && disablepostctrl != 1)
            {
                if (postMessage.Length < config.Minpostsize)
                    AddErrLine("您发表的内容过少, 系统设置要求帖子内容不得少于 " + config.Minpostsize.ToString() + " 字多于 " + config.Maxpostsize.ToString() + " 字");
                else if (postMessage.Length > config.Maxpostsize)
                    AddErrLine("您发表的内容过多, 系统设置要求帖子内容不得少于 " + config.Minpostsize.ToString() + " 字多于 " + config.Maxpostsize.ToString() + " 字");
            }

            //新用户广告强力屏蔽检查
            if ((config.Disablepostad == 1) && useradminid < 1)  //如果开启新用户广告强力屏蔽检查或是游客
            {
                if ((config.Disablepostadpostcount != 0 && user.Posts <= config.Disablepostadpostcount) ||
                    (config.Disablepostadregminute != 0 && DateTime.Now.AddMinutes(-config.Disablepostadregminute) <= Convert.ToDateTime(user.Joindate)))
                {
                    foreach (string regular in config.Disablepostadregular.Replace("\r", "").Split('\n'))
                    {
                        if (Posts.IsAD(regular, postTitle, postMessage))
                        {
                            AddErrLine("发帖失败，内容中有不符合新用户强力广告屏蔽规则的字符，请检查标题和内容，如有疑问请与管理员联系");
                            return;
                        }
                    }
                }
            }

            #endregion
            
            string[] pollitem = Utils.SplitString(DNTRequest.GetString("PollItemname"), "\r\n");
            int topicprice = 0;
            string tmpprice = DNTRequest.GetString("topicprice");

            if (postinfo.Layer == 0)
            {

                #region 投票信息
                //string[] pollitem = Utils.SplitString(DNTRequest.GetString("PollItemname"), "\r\n");

                if (!Utils.StrIsNullOrEmpty(DNTRequest.GetString("updatepoll")) && topic.Special == 1)
                {
                    pollinfo.Multiple = DNTRequest.GetInt("multiple", 0);

                    // 验证用户是否有发布投票的权限
                    if (usergroupinfo.Allowpostpoll != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有发布投票的权限");
                        return;
                    }

                    if (pollitem.Length < 2)
                        AddErrLine("投票项不得少于2个");
                    else if (pollitem.Length > config.Maxpolloptions)
                        AddErrLine("系统设置为投票项不得多于" + config.Maxpolloptions + "个");
                    else
                    {
                        for (int i = 0; i < pollitem.Length; i++)
                            if (Utils.StrIsNullOrEmpty(pollitem[i]))
                                AddErrLine("投票项不能为空");
                    }
                }
                #endregion

                #region 悬赏信息
                //int topicprice = 0;
                //string tmpprice = DNTRequest.GetString("topicprice");

                if (Regex.IsMatch(tmpprice, "^[0-9]*[0-9][0-9]*$") || tmpprice == string.Empty)
                {
                    topicprice = Utils.StrToInt(tmpprice, 0) > 32767 ? 32767 : Utils.StrToInt(tmpprice, 0);
                    //当不是正在进行的悬赏...
                    if (topic.Special != 2)
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
                        if (usergroupinfo.Radminid != 1)
                        {
                            if (usergroupinfo.Allowbonus == 0)
                                AddErrLine(string.Format("您当前的身份 \"{0}\" 未被允许进行悬赏", usergroupinfo.Grouptitle));

                            if (topicprice < usergroupinfo.Minbonusprice || topicprice > usergroupinfo.Maxbonusprice)
                                AddErrLine(string.Format("悬赏价格超出范围, 您应在 {0} - {1} {2}{3} 范围内进行悬赏", usergroupinfo.Minbonusprice, usergroupinfo.Maxbonusprice,
                                    userextcreditsinfo.Unit, userextcreditsinfo.Name));
                        }
                    }
                }
                else
                {
                    if (topic.Special != 2)
                        AddErrLine("主题售价只能为整数");
                    else
                        AddErrLine("悬赏价格只能为整数");
                }
                #endregion

                #region 辩论信息
                if (!Utils.StrIsNullOrEmpty(DNTRequest.GetString("updatedebate")) && topic.Special == 4)
                {
                    if (usergroupinfo.Allowdebate != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有发布辩论的权限");
                        return;
                    }
                    if (Utils.StrIsNullOrEmpty(DNTRequest.GetString("positiveopinion")))
                    {
                        AddErrLine("正方观点不能为空");
                        return;
                    }
                    if (Utils.StrIsNullOrEmpty(DNTRequest.GetString("negativeopinion")))
                    {
                        AddErrLine("反方观点不能为空");
                        return;
                    }
                    if (Utils.StrIsNullOrEmpty(DNTRequest.GetString("terminaltime")))
                    {
                        AddErrLine("辩论的结束日期不能为空");
                        return;
                    }
                    if (!Utils.IsDateString(DNTRequest.GetString("terminaltime")))
                    {
                        AddErrLine("结束日期格式不正确");
                        return;
                    }
                }
                #endregion

            }

            #region 绑定并检查主题和帖子信息
            if (useradminid == 1)
            {
                postinfo.Title = Utils.HtmlEncode(postTitle);

                if (usergroupinfo.Allowhtml == 0)
                    postinfo.Message = Utils.HtmlEncode(postMessage);
                else
                    postinfo.Message = ishtmlon ? postMessage :
                            Utils.HtmlEncode(postMessage);
            }
            else
            {
                postinfo.Title = Utils.HtmlEncode(ForumUtils.BanWordFilter(postTitle));

                if (usergroupinfo.Allowhtml == 0)
                    postinfo.Message = Utils.HtmlEncode(ForumUtils.BanWordFilter(postMessage));
                else
                    postinfo.Message = ishtmlon ? ForumUtils.BanWordFilter(postMessage) :
                            Utils.HtmlEncode(ForumUtils.BanWordFilter(postMessage));
            }


            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】60->255
            postinfo.Title = postinfo.Title.Length > 255 ? postinfo.Title.Substring(0, 255) : postinfo.Title;
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            if (useradminid != 1 && (ForumUtils.HasBannedWord(postTitle) || ForumUtils.HasBannedWord(postMessage)))
            {
                string bannedWord = ForumUtils.GetBannedWord(postTitle) == string.Empty ? ForumUtils.GetBannedWord(postMessage) : ForumUtils.GetBannedWord(postTitle);
                AddErrLine(string.Format("对不起, 您提交的内容包含不良信息  <font color=\"red\">{0}</font>, 请返回修改!", bannedWord));
                return;
            }

            //if (useradminid != 1 && (ForumUtils.HasAuditWord(postinfo.Title) || ForumUtils.HasAuditWord(postinfo.Message)))
            //{
            //    AddErrLine("对不起, 管理员设置了需要对发帖进行审核, 您没有权力编辑已通过审核的帖子, 请返回修改!");
            //    return;
            //}

            topic.Displayorder = Topics.GetTitleDisplayOrder(usergroupinfo, useradminid, forum, topic, message, disablepostctrl);

            #endregion
            
            // 检察上面验证是否有错误
            if (IsErr())
                return;
            //如果是不是管理员组,或者编辑间隔超过60秒,则附加编辑信息
            if (Utils.StrDateDiffSeconds(postinfo.Postdatetime, 60) > 0 && config.Editedby == 1 && useradminid != 1)
                postinfo.Lastedit = username + " 最后编辑于 " + Utils.GetDateTime();

            postinfo.Usesig = Utils.StrToInt(DNTRequest.GetString("usesig"), 0);
            postinfo.Htmlon = (usergroupinfo.Allowhtml == 1 && ishtmlon ? 1 : 0);
            postinfo.Smileyoff = smileyoff == 0 ? TypeConverter.StrToInt(DNTRequest.GetString("smileyoff")) : smileyoff;
            postinfo.Bbcodeoff = (usergroupinfo.Allowcusbbcode == 1 ? TypeConverter.StrToInt(DNTRequest.GetString("bbcodeoff")) : 1);
            postinfo.Parseurloff = TypeConverter.StrToInt(DNTRequest.GetString("parseurloff"));
            
            //【BT修改】只有正常显示的帖子，才根据是否审核决定编辑后是否显示，否则一律不更改此数值。
            if(postinfo.Invisible == 0) postinfo.Invisible = needaudit ? 1 : 0;

            //如果当前用户就是作者或所在管理组有编辑的权限
            if (userid == postinfo.Posterid || (admininfo != null && admininfo.Alloweditpost == 1 && Moderators.IsModer(useradminid, userid, forumid)))
                alloweditpost = true;
            else
            {
                AddErrLine("您当前的身份不是作者");
                return;
            }

            if (!alloweditpost)
            {
                AddErrLine("您当前的身份没有编辑帖子的权限");
                return;
            }

            if (alloweditpost)
                SetTopicInfo(pollitem, topicprice, postMessage);
        }

        /// <summary>
        /// 设置相关主题信息
        /// </summary>
        /// <param name="pollitem"></param>
        /// <param name="topicprice"></param>
        /// <param name="postmessage"></param>
        private void SetTopicInfo(string[] pollitem, int topicprice, string postmessage)
        {
            if (postinfo.Layer == 0)
            {
                ///修改投票信息
                StringBuilder itemvaluelist = new StringBuilder("");
                if (topic.Special == 1)
                {
                    string pollItemname = Utils.HtmlEncode(DNTRequest.GetFormString("PollItemname").Trim());

                    if (!Utils.StrIsNullOrEmpty(pollItemname))
                    {
                        int multiple = DNTRequest.GetString("multiple") == "on" ? 1 : 0;
                        int maxchoices = DNTRequest.GetInt("maxchoices", 0);

                        if (multiple == 1 && maxchoices > pollitem.Length)
                            maxchoices = pollitem.Length;

                        if (!Polls.UpdatePoll(topic.Tid, multiple, pollitem.Length, DNTRequest.GetFormString("PollOptionID").Trim(), pollItemname, DNTRequest.GetFormString("PollOptionDisplayOrder").Trim(), DNTRequest.GetString("enddatetime"), maxchoices, DNTRequest.GetString("visiblepoll") == "on" ? 1 : 0, DNTRequest.GetString("allowview") == "on" ? 1 : 0))
                        {
                            AddErrLine("投票错误,请检查显示顺序");
                            return;
                        }
                    }
                    else
                    {
                        AddErrLine("投票项为空");
                        return;
                    }
                }

                //修改辩论信息
                if (topic.Special == 4)
                {
                    debateinfo.Positiveopinion = DNTRequest.GetString("positiveopinion");
                    debateinfo.Negativeopinion = DNTRequest.GetString("negativeopinion");
                    debateinfo.Terminaltime = TypeConverter.StrToDateTime(DNTRequest.GetString("terminaltime"));
                    if (!Debates.UpdateDebateTopic(debateinfo))
                    {
                        AddErrLine("辩论修改选择了无效的主题");
                        return;
                    }
                }

                int iconid = DNTRequest.GetInt("iconid", 0);
                topic.Iconid = (iconid > 15 || iconid < 0) ? 0 : iconid;
                topic.Title = postinfo.Title;

                //悬赏差价处理
                if (topic.Special == 2)
                {
                    int pricediff = topicprice - topic.Price;
                    if (pricediff > 0)
                    {
                        if (bonusCreditsTrans < 1 || bonusCreditsTrans > 8)
                        {
                            AddErrLine("系统未设置\"交易积分设置\", 无法判断当前要使用的(扩展)积分字段, 暂时无法发布悬赏"); return;
                        }
                        //扣分
                        if (usergroupinfo.Radminid != 1 && Users.GetUserExtCredits(topic.Posterid, bonusCreditsTrans) < pricediff)
                        {
                            AddErrLine("主题作者 " + Scoresets.GetValidScoreName()[bonusCreditsTrans] + " 不足, 无法追加悬赏");
                            return;
                        }
                        else
                        {
                            topic.Price = topicprice;
                            Users.UpdateUserExtCredits(topic.Posterid, bonusCreditsTrans,
                                                      -pricediff * (Scoresets.GetCreditsTax() + 1)); //计算税后的实际支付
                        }
                    }
                    else if (pricediff < 0 && usergroupinfo.Radminid != 1)
                    {
                        AddErrLine("不能降低悬赏价格");
                        return;
                    }
                }
                else if (topic.Special == 0)//普通主题,出售
                {
                    topic.Price = topicprice;
                }
                if (usergroupinfo.Allowsetreadperm == 1)
                    topic.Readperm = DNTRequest.GetInt("topicreadperm", 0) > 255 ? 255 : DNTRequest.GetInt("topicreadperm", 0);

                if (ForumUtils.IsHidePost(postmessage) && usergroupinfo.Allowhidecode == 1)
                    topic.Hide = 1;

                topic.Typeid = DNTRequest.GetFormInt("typeid", 0);

                htmltitle = DNTRequest.GetString("htmltitle").Trim();
                if (!Utils.StrIsNullOrEmpty(htmltitle) && Utils.HtmlDecode(htmltitle).Trim() != topic.Title)
                {
                    //按照  附加位/htmltitle(1位)/magic(3位)/以后扩展（未知位数） 的方式来存储,  11001
                    topic.Magic = 11000;
                }
                else
                    topic.Magic = 0;

                topic.Displayorder = Topics.GetTitleDisplayOrder(usergroupinfo, useradminid, forum, topic, message, disablepostctrl);

                ForumTags.DeleteTopicTags(topic.Tid);
                Topics.DeleteRelatedTopics(topic.Tid);
                string tags = DNTRequest.GetString("tags").Trim();
                if (enabletag && !Utils.StrIsNullOrEmpty(tags))
                {
                    if (ForumUtils.InBanWordArray(tags))
                    {
                        AddErrLine("标签中含有系统禁止词语,请修改");
                        return;
                    }

                    string[] tagArray = Utils.SplitString(tags, " ", true, 2, 10);
                    if (tagArray.Length > 0 && tagArray.Length <= 5)
                    {
                        topic.Magic = Topics.SetMagicValue(topic.Magic, MagicType.TopicTag, 1);
                        ForumTags.CreateTopicTags(tagArray, topic.Tid, userid, Utils.GetDateTime());
                    }
                    else
                    {
                        AddErrLine("超过标签数的最大限制或单个标签长度没有介于2-10之间，最多可填写 5 个标签");
                        return;
                    }
                }

                Topics.UpdateTopic(topic);

                //保存htmltitle
                if (canhtmltitle && !Utils.StrIsNullOrEmpty(htmltitle) && htmltitle != topic.Title)
                    Topics.WriteHtmlTitleFile(Utils.RemoveUnsafeHtml(htmltitle), topic.Tid);
            }
            else
            {
                if (ForumUtils.IsHidePost(postmessage) && usergroupinfo.Allowhidecode == 1)
                {
                    topic.Hide = 1;
                    Topics.UpdateTopic(topic);
                }
            }
        }

        /// <summary>
        /// 设置相关附件信息
        /// </summary>
        /// <returns></returns>
        private StringBuilder SetAttachmentInfo()
        {
            string attachId = DNTRequest.GetFormString("attachid");
            StringBuilder sb = new StringBuilder();
            AttachmentInfo[] editPostAttachArray = Attachments.GetEditPostAttachArray(0, attachId);
            List<AttachmentInfo> newUploadAttachList = new List<AttachmentInfo>();

            //获取本次编辑操作上传的附件
            foreach (AttachmentInfo info in editPostAttachArray)
            {
                if (info.Pid == 0)
                    newUploadAttachList.Add(info);
            }

            if (!string.IsNullOrEmpty(attachId))
                Attachments.UpdateAttachment(editPostAttachArray, topic.Tid, postinfo.Pid, postinfo, ref sb, userid, config, usergroupinfo);

            //加入相册
            if (config.Enablealbum == 1 && apb != null && newUploadAttachList != null)
                sb.Append(apb.CreateAttachment(newUploadAttachList.ToArray(), usergroupid, userid, username));

            return sb;
        }

        protected string FormatDateTimeString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        protected string AttachmentList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (DataRow dr in attachmentlist.Rows)
            {
                if (dr["isimage"].ToString().Trim() == "0")
                    continue;

                sb.AppendFormat("{{\\'aid\\':{0},\\'attachment\\':\\'{1}\\',\\'attachkey\\':\\'{2}\\',\\'description\\':\\'{3}\\'}},",
                    dr["aid"].ToString(), dr["attachment"].ToString().Trim().Replace("'", "\\'"), GetKey(dr["aid"].ToString()), dr["description"].ToString().Trim().Replace("'", "\\'"));
            }
            return sb.ToString().TrimEnd(',') + "]";

            //StringBuilder html = new StringBuilder(5120);
            //bool find = false;
            //html.Append("<table id=\"uploadattachlist\" cellspacing=\"2\" cellpadding=\"2\" class=\"imgl\">");
            //int count = 1;
            //foreach (DataRow dr in attachmentlist.Rows)
            //{
            //    if (dr["isimage"].ToString().Trim() == "0")
            //        continue;
            //    find = true;
            //    if (count % 4 == 1)
            //        html.Append("<tr>");
            //    html.AppendFormat("<td valign=\"bottom\" id=\"image_td_{0}\" width=\"25%\">", dr["aid"].ToString());
            //    html.AppendFormat("<input type=\"hidden\" value=\"{1}\" name=\"attachid\"><input type=\"hidden\" value=\"0\" name=\"attachprice_{1}\"><input type=\"hidden\" value=\"0\" name=\"readperm_{1}\"><a href=\"javascript:;\" title=\"{0}\"><img src=\"tools/ajax.aspx?t=image&aid={1}&size=300x300&key={2}&nocache=yes&type=fixnone\" id=\"image_{1}\" onclick=\"insertAttachimgTag('{1}')\" width=\"110\" cwidth=\"300\" /></a>",
            //        dr["attachment"].ToString().Trim(), dr["aid"].ToString().Trim(), GetKey(dr["aid"].ToString().Trim()));
            //    html.Append("<p class=\"imgf\">");
            //    html.AppendFormat("<a href=\"javascript:;\" onclick=\"delImgAttach({0},0)\" class=\"del y\">删除</a>", dr["aid"].ToString().Trim());
            //    html.AppendFormat("<input type=\"text\" class=\"px xg2\" value=\"{0}\" onclick=\"this.style.display='none';$('image_desc_{1}').style.display='';$('image_desc_{1}').focus();\" />", dr["description"].ToString().Trim(), dr["aid"].ToString().Trim());
            //    html.AppendFormat("<input type=\"text\" name=\"attachdesc_{0}\" class=\"px\" style=\"display: none\" id=\"image_desc_{0}\" value=\"{1}\" />", dr["aid"].ToString().Trim(), dr["description"].ToString().Trim());
            //    html.Append("</p>");
            //    html.Append("</td>");
            //    if (count % 4 == 0)
            //        html.Append("</tr>");
            //    count++;
            //}
            //string imageNumHidden = string.Format("<input id=\"imagenumhidden\" type=\"hidden\" value=\"{0}\"/>", count - 1);

            //if (--count % 4 != 0)
            //{
            //    for (int i = 0; i < 4 - count % 4; i++)
            //        html.Append("<td width=\"25%\"></td>");
            //    html.Append("</tr>");
            //}
            //html.Append("</table>");
            //html.Append(imageNumHidden);

            //if (find)
            //    return html.ToString();
            //else
            //    return "<input id=\"imagenumhidden\" type=\"hidden\" value=\"0\"/>";
        }

        private string GetKey(string aid)
        {
            return Discuz.Common.DES.Encode(aid.ToString() + ",300,300", Utils.MD5(aid)).Replace("+", "[");
        }



        /// <summary>
        /// 生成上传种子的seedinfo
        /// </summary>
        /// <returns></returns>
        private PTSeedinfo CreateSeedInfo(int edit_seedid, ref string old_title)
        {
            //创建种子信息类，获得分类信息
            string curdatetime = Utils.GetDateTime();

            PTSeedinfo seedinfo = PTSeeds.GetSeedInfoFull(edit_seedid);

            old_title = seedinfo.TopicTitle;

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

            return seedinfo;
        }

        /// <summary>
        /// 执行更新种子信息
        /// </summary>
        /// <returns></returns>
        private PTSeedinfo DoUpdateSeedInfo(PTSeedinfo seedinfo, int old_seedid, string old_title)
        {
           
            //读取提交的种子文件
            string curdatetime = DateTime.Now.ToString();
            HttpFileCollection files = HttpContext.Current.Request.Files;
            HttpPostedFile postedFile = files[0];   //得到提交一个的文件
            PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
            bool torrentnochange = false;
            string editreason = DNTRequest.GetEncodedString("editreason");

            if (postedFile.FileName != "")          //文件不存在时
            {
                if ((DateTime.Now - DateTime.Parse(topic.Postdatetime)).TotalDays > 7) //发布7天之后不能再编辑种子
                {
                    if (seedinfo.Uid == userid && !(PrivateBT.IsServerUser(userid)) && !Moderators.IsModer(useradminid, userid, forumid))
                    {
                        AddErrLine("发布7天之后，不允许再替换种子文件，您可以继续编辑种子介绍");
                        return null;
                    }
                }
                if (seedinfo.DownloadRatio == 0 || seedinfo.TopSeed > 0)//设置蓝种之后不能再编辑种子
                {
                    if (seedinfo.Uid == userid && !(PrivateBT.IsServerUser(userid)) && !Moderators.IsModer(useradminid, userid, forumid))
                    {
                        AddErrLine("置顶或者蓝种不能替换种子编辑，请短消息版主取消置顶和蓝种，您可以继续编辑种子介绍");
                        return null;
                    }
                }


                Stream fileDataStream = postedFile.InputStream;

                //得到文件大小
                int fileLength = postedFile.ContentLength;

                //创建数组
                byte[] fileData = new byte[fileLength];

                //把文件流填充到数组
                fileDataStream.Read(fileData, 0, fileLength);

                //得到文件名字
                string fileTitle = postedFile.FileName;
                fileTitle = fileTitle.Substring(fileTitle.LastIndexOf('\\') + 1);

                seedinfo.FileName = postedFile.FileName;
                //保存种子，如果种子存在重复，会自动将seedinfo中的seedid替换为已存在种子的seedid
                seedid = PTTorrent.SaveSeed(fileData, ref seedinfo, seedinfo.Path);

                if (seedid == 0)
                {
                    if (old_seedid != seedinfo.SeedId)
                    {
                        if (seedinfo.Status == 2 || seedinfo.Status == 3)
                        {
                            AddErrLine("您编辑上传的种子与之前已经发布的种子重复，请耐心等待10秒，即将为您自动跳转到之前已经发布的种子，请直接续种！");
                            SetUrl(base.ShowTopicAspxRewrite(seedinfo.TopicId, 0));
                            SetMetaRefresh(10);
                            SetShowBackLink(true);
                            return null;
                        }
                        else if (seedinfo.Status == 6)
                        {
                            AddErrLine("您编辑上传的种子已被禁止发布，且勿再次尝试发布，请再次仔细阅读相关板块规则！");
                            return null;
                        }
                        else
                        {
                            AddErrLine("您编辑上传的种子存在重复，但是重复种子的状态不正常，无法显示。 请到意见投诉版说明情况，等待处理，感谢您的反馈！");
                            return null;
                        }

                    } 
                    else
                    {
                        AddMsgLine("种子信息已更新，但是您编辑上传的种子与之前的完全相同，因此种子文件没有被修改！不修改种子时种子栏请留空即可！");
                        seedid = seedinfo.SeedId;
                        torrentnochange = true;
                    }
                }
                else if (seedid < 0)
                {
                    AddErrLine(string.Format("发生错误! 种子上传没有成功，错误代码{0}，请检查种子文件是否完好，系统只接受uTorrent制作的UTF-8编码的种子", seedid));
                    return null;
                }
                else
                {
                    //种子文件已经更新
                    //编辑后正常的种子
                    PTSeeds.UpdateSeedStatus(seedid, 2);
                    //删除当前的peer
                    PTPeers.DeletePeerBySeedid(seedid);

                    string userlist_sent = ",";

                    #region 种子操作记录，短消息通知发种人
                    
                    if (seedinfo.Uid == userid) PrivateBT.InsertSeedModLog(seedid, "该种子被用户 " + username + " 执行 自替换种子编辑 操作", username, "", userid, 9);
                    else
                    {
                        PrivateBT.InsertSeedModLog(seedid, "该种子被版主 " + username + " 执行 替换种子编辑 操作" + (editreason != "" ? "，理由为：" + editreason : ""), username, editreason, userid, 8);

                        if (DNTRequest.GetFormInt("sendmessage", 0) > 0 && !PrivateBT.IsServerUserNoPM(seedinfo.Uid))
                        {
                            //如果选中通知作者
                            
                            privatemessageinfo.Message  = string.Format("\n您发布的种子\n<a{0}><span{1}>{2}</span></a>", QHtml.HR_ShowTopic(seedinfo.TopicId), QHtml.TS_BlueBold, seedinfo.TopicTitle);
                            privatemessageinfo.Message += string.Format("\n被 版主 <a{0}>{1}</a> (<a{2}>点击发送短消息</a>) ", QHtml.HR_ShowUser(userid), username, QHtml.HR_SendPM(userid));
                            privatemessageinfo.Message += string.Format("执行了 [ 替换种子编辑 ] 操作。 请仔细查看被编辑后的种子，熟悉发种规则，并在发种时时注意发种界面的最新通知，感谢您的分享！");
                            if (editreason != "") 
                                privatemessageinfo.Message += string.Format("\n\n编辑理由为：\n<span{1}>{0}</span>", editreason, QHtml.TS_RedBold);
                            if (old_title != seedinfo.TopicTitle)
                                privatemessageinfo.Message += string.Format("\n\n编辑前标题：\n<span{2}>{0}</span>\n编辑后标题：\n<span{2}>{1}</span>", old_title, seedinfo.TopicTitle, QHtml.TS_GrayBold);
                            privatemessageinfo.Message += "\n\n您可以点击上面的种子链接查看编辑后的种子信息";
                            privatemessageinfo.Subject = "您发布的种子被替换编辑：请仔细查看被编辑后的种子并重新下载种子";
                            privatemessageinfo.Msgto = Users.GetUserName(seedinfo.Uid);
                            privatemessageinfo.Msgtoid = seedinfo.Uid;
                            privatemessageinfo.Msgfrom = "系统";
                            privatemessageinfo.Msgfromid = 0;
                            privatemessageinfo.New = 1;
                            privatemessageinfo.Postdatetime = curdatetime;
                            privatemessageinfo.Folder = 0;
                            PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                            userlist_sent += privatemessageinfo.Msgtoid.ToString() + ",";
                        }
                    }

                    #endregion

                    #region 短消息通知活动用户（正在上传下载）
                    //短信通知所有正在下载上传和曾经完成的人
                    if (seedinfo.Upload > 0 || seedinfo.Download > 0)
                    {
                        privatemessageinfo.Message = string.Format("\n您正在上传/下载的种子\n<a{0}><span{1}>{2}</span></a>", QHtml.HR_ShowTopic(seedinfo.TopicId), QHtml.TS_BlueBold, seedinfo.TopicTitle);
                        privatemessageinfo.Message += string.Format("\n已经被替换编辑，请重新下载种子以便继续上传/下载，带来不便敬请谅解");
                        privatemessageinfo.Message += string.Format("\n\n您可以点击下面的链接查看替换后的种子页面\n<a {0}'>http://buaabt.cn/showtopic-{1}.aspx</a>", QHtml.HR_ShowTopic(seedinfo.TopicId), seedinfo.TopicId);
                        privatemessageinfo.Message += string.Format("\n您也可以点击下面的链接直接下载新种子\n<a {0}>http://buaabt.cn/downloadseed.aspx?seedid={1}</a>", QHtml.HR_DownloadSeed(seedinfo.SeedId), seedinfo.SeedId);
                        privatemessageinfo.Subject = "您正在上传/下载的种子被替换通知，请重新下载种子";
                        privatemessageinfo.Msgfrom = "系统";
                        privatemessageinfo.Msgfromid = 0;
                        privatemessageinfo.New = 1;
                        privatemessageinfo.Postdatetime = curdatetime;
                        privatemessageinfo.Folder = 0;

                        DataTable peerlist = PrivateBT.GetPeerList(seedid);
                        if (peerlist.Rows.Count > 0) foreach (DataRow dr in peerlist.Rows)
                            {
                                privatemessageinfo.Msgtoid = Utils.StrToInt(dr["uid"].ToString(), 0);
                                privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);

                                //发送短消息
                                if (privatemessageinfo.Msgtoid != 0 && !PrivateBT.IsServerUserNoPM(privatemessageinfo.Msgtoid) && userlist_sent.IndexOf("," + privatemessageinfo.Msgtoid + ",") < 0)
                                {
                                    PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                                    userlist_sent += privatemessageinfo.Msgtoid + ",";
                                }
                            }
                        peerlist.Dispose();
                    }
                    #endregion

                    #region 短消息通知最近30天有活动的曾经下载过的用户
                    privatemessageinfo.Message = string.Format("\n您曾经下载过的种子\n<a{0}><span{1}>{2}</span></a>", QHtml.HR_ShowTopic(seedinfo.TopicId), QHtml.TS_BlueBold, seedinfo.TopicTitle);
                    privatemessageinfo.Message += string.Format("\n已经被替换编辑，请重新下载种子以便继续上传/下载，带来不便敬请谅解");
                    privatemessageinfo.Message += string.Format("\n\n您可以点击下面的链接查看替换后的种子页面\n<a {0}'>http://buaabt.cn/showtopic-{1}.aspx</a>", QHtml.HR_ShowTopic(seedinfo.TopicId), seedinfo.TopicId);
                    privatemessageinfo.Message += string.Format("\n您也可以点击下面的链接直接下载新种子\n<a {0}>http://buaabt.cn/downloadseed.aspx?seedid={1}</a>", QHtml.HR_DownloadSeed(seedinfo.SeedId), seedinfo.SeedId);
                    privatemessageinfo.Subject = "您曾经下载过的种子被替换通知，请重新下载种子";
                    privatemessageinfo.Msgfrom = "系统";
                    privatemessageinfo.Msgfromid = 0;
                    privatemessageinfo.New = 1;
                    privatemessageinfo.Postdatetime = curdatetime;
                    privatemessageinfo.Folder = 0;
                    DataTable uidlist = PrivateBT.GetUserIdListActiveInSeed(seedid, DateTime.Now.AddDays(-30));
                    if (uidlist.Rows.Count > 0) foreach (DataRow dr in uidlist.Rows)
                        {
                            privatemessageinfo.Msgtoid = Utils.StrToInt(dr["uid"].ToString(), 0);
                            privatemessageinfo.Msgto = Users.GetUserName(privatemessageinfo.Msgtoid);
                            if (privatemessageinfo.Msgtoid != 0 && !PrivateBT.IsServerUserNoPM(privatemessageinfo.Msgtoid) && userlist_sent.IndexOf("," + privatemessageinfo.Msgtoid + ",") < 0)
                            {
                                PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                            }
                        }
                    uidlist.Dispose();

                    #endregion           
                }
            }
            if (postedFile.FileName == "" || torrentnochange) 
            {
                PTSeeds.UpdateSeedEditWithOutSeed(seedinfo);       //仅更新种子信息，不更新种子

                //种子操作记录，不替换种子编辑
                if (seedinfo.Uid == userid) PrivateBT.InsertSeedModLog(seedid, "该种子被用户 " + username + " 执行 自编辑 操作", username, "", userid, 7);
                else
                {
                    PrivateBT.InsertSeedModLog(seedid, "该种子被版主 " + username + " 执行 编辑 操作" + (editreason != "" ? "，理由为：" + editreason : ""), username, editreason, userid, 6);

                    if (DNTRequest.GetFormInt("sendmessage", 0) > 0) //只有选择短消息通知才发送
                    {
                        #region 短消息通知作者种子被编辑

                        if (!PrivateBT.IsServerUserNoPM(seedinfo.Uid))
                        {
                            privatemessageinfo.Message = string.Format("\n您发布的种子\n<a{0}><span{1}>{2}</span></a>", QHtml.HR_ShowTopic(seedinfo.TopicId), QHtml.TS_BlueBold, seedinfo.TopicTitle);
                            privatemessageinfo.Message += string.Format("\n被 版主 <a{0}>{1}</a> (<a{2}>点击发送短消息</a>) ", QHtml.HR_ShowUser(userid), username, QHtml.HR_SendPM(userid));
                            privatemessageinfo.Message += string.Format("执行了 [ 编辑 ] 操作。 请仔细查看被编辑后的种子，熟悉发种规则，并在发种时时注意发种界面的最新通知，感谢您的分享！");
                            if (editreason != "")
                                privatemessageinfo.Message += string.Format("\n\n编辑理由为：\n<span{1}>{0}</span>", editreason, QHtml.TS_RedBold);
                            if (old_title != seedinfo.TopicTitle)
                                privatemessageinfo.Message += string.Format("\n\n编辑前标题：\n<span{2}>{0}</span>\n编辑后标题：\n<span{2}>{1}</span>", old_title, seedinfo.TopicTitle, QHtml.TS_GrayBold);
                            privatemessageinfo.Message += "\n\n您可以点击上面的种子链接查看编辑后的种子信息";
                            privatemessageinfo.Subject = "您发布的种子被编辑：请仔细查看被编辑后的种子";
                            privatemessageinfo.Msgto = Users.GetUserName(seedinfo.Uid);
                            privatemessageinfo.Msgtoid = seedinfo.Uid;
                            privatemessageinfo.Msgfrom = "系统";
                            privatemessageinfo.Msgfromid = 0;
                            privatemessageinfo.New = 1;
                            privatemessageinfo.Postdatetime = curdatetime;
                            privatemessageinfo.Folder = 0;
                            PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
                        }

                        #endregion
                    }
                }
            }

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

    }
}
