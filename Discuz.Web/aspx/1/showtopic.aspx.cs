using System;
using System.Data;
using System.Text;
using System.Web;
using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Forum;

namespace Discuz.Web
{
    /// <summary>
    /// 查看主题页面
    /// </summary>
    public class showtopic : Discuz.Web.UI.TopicPage
    {
        #region 页面变量
        /// <summary>
        /// 帖子列表
        /// </summary>
        public List<ShowtopicPagePostInfo> postlist = new List<ShowtopicPagePostInfo>();
        /// <summary>
        /// 投票帖类型
        /// </summary>
        public PollInfo pollinfo = new PollInfo();
        /// <summary>
        /// 是否显示投票结果
        /// </summary>
        public bool showpollresult = true;
        /// <summary>
        /// 通过TID得到帖子观点
        /// </summary>
        public Dictionary<int, int> debateList = new Dictionary<int, int>();
        /// <summary>
        /// 作为辩论主题的扩展属性
        /// </summary>
        public DebateInfo debateexpand = new DebateInfo();
        /// <summary>
        /// 获取绑定相关版块的商品分类信息
        /// </summary>
        public string goodscategoryfid = GeneralConfigs.GetConfig().Enablemall <= 0 ? "{}" : Discuz.Plugin.Mall.MallPluginProvider.GetInstance().GetGoodsCategoryWithFid();
        /// <summary>
        /// 积分策略信息
        /// </summary>
        public UserExtcreditsInfo userextcreditsinfo = new UserExtcreditsInfo();
        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist = new List<PrivateMessageInfo>();
        /// <summary>
        /// 悬赏给分日志
        /// </summary>
        public List<BonusLogInfo> bonuslogs = new List<BonusLogInfo>();
        /// <summary>
        /// 用户ID(在只看该用户功能中使用)
        /// </summary>
        public int posterid = DNTRequest.GetInt("posterid", 0);
        /// <summary>
        /// 下一页
        /// </summary>
        public string nextpage = "";
        public string nextpageurl = "";
        /// <summary>
        /// 返回帖子列表
        /// </summary>
        public string listlink = "";
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
        public bool showvisitedforumsmenu = false;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        private ShortUserInfo userInfo = null;
        /// <summary>
        /// 当前主题中是否有隐藏内容
        /// </summary>
        public int hide;
        /// <summary>
        /// 获取url中的主题id
        /// </summary>
        public int typeid = DNTRequest.GetInt("typeid", -1);

        /// <summary>
        /// 是否允许自行删除主题
        /// </summary>
        public bool allowselfdel = false;
        /// <summary>
        /// 是否允许自行关闭主题
        /// </summary>
        public bool allowselfclose = false;

        public string[] postleftshow;
        public string[] userfaceshow;
        #endregion

        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
        //【BT修改】

        public AbtSeedInfo abtseedinfo = new AbtSeedInfo();
        public PTSeedinfo seedinfo = new PTSeedinfo();
        public string autodownloadseed = "";
        public double uploadratio = 1;
        public double downloadratio = 1;
        public string imdblink = "";
        public string blueseednote = "";
        public double blueseedleft = 0;
        public string trafficcheck = "";

        public LotteryInfo lotteryinfo = null;
        /// <summary>
        /// 当前用户已经投注的选项，大于-1则存在
        /// </summary>
        public int lotterycuroption = -1;

        /// <summary>
        /// 上传系数过期时间字符串
        /// </summary>
        public string uploadratioNote = "";
        /// <summary>
        /// 下载系数过期时间字符串
        /// </summary>
        public string downloadratioNote = "";
        /// <summary>
        /// 共享率保护信息
        /// </summary>
        public string ratioprotectionNote = "";
        /// <summary>
        /// 当前主题售价
        /// </summary>
        public int price;

        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        protected override void ShowPage()
        {
            //获取主题信息
            topic = GetTopicInfo();
            if (topic == null || topic.Displayorder < 0)
            {
                AddErrLine("不存在主题ID");
                return;
            } 
            topicid = topic.Tid;
            forumid = topic.Fid;
            forum = Forums.GetForumInfo(forumid);
            if (forum == null)
            {
                AddErrLine("不存在的版块ID"); return;
            }

            //增加IP限制
            if (!Forums.AllowView(forum, usergroupid, ipaddress) || (forumid == 58 && btuserinfo.Extcredits2 < 10))
            {
                AddErrLine("您无权访问此板块");
                return;
            }

            //验证不通过则返回
            if (!ValidateInfo() || IsErr())
                return;

            if (userid > 0)
            {
                userInfo = Users.GetShortUserInfo(userid);
            }

            //判断是否是版主应该在售价判断之前
            IsModer();

            price = GetTopicPrice(topic);
            if (topic.Special == 0 && price > 0 && ismoder != 1)
            {
                HttpContext.Current.Response.Redirect(forumpath + "buytopic.aspx?topicid=" + topic.Tid);
                return;
            }

            if (postid > 0 && Posts.GetPostInfo(topicid, postid) == null)
            {
                AddErrLine("该帖可能已被删除 " + string.Format("<a href=\"{0}\">[返回主题]</a>", ShowTopicAspxRewrite(topicid, 1))); 
                return;
            }

            //将版块加入到已访问版块列表中
            ForumUtils.SetVisitedForumsCookie(forumid.ToString());


            if (topic.Identify > 0)
                topicidentify = Caches.GetTopicIdentify(topic.Identify);

            pagetitle = string.Format("{0} - {1}", topic.Title, Utils.RemoveHtml(forum.Name));

            ///得到广告列表              
            GetForumAds(forum.Fid);

            
            //获取主题类型
            Caches.GetTopicTypeArray().TryGetValue(topic.Typeid, out topictypes);
            topictypes = Utils.StrIsNullOrEmpty(topictypes) ? "" : "[" + topictypes + "]";

            userextcreditsinfo = Scoresets.GetScoreSet(Scoresets.GetTopicAttachCreditsTrans());
            score = Scoresets.GetValidScoreName();
            scoreunit = Scoresets.GetValidScoreUnit();
            navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);
            //编辑器状态
            EditorState();

            string[] customauthorinfo = config.Customauthorinfo.Split('|');
            postleftshow = customauthorinfo[0].Split(',');//帖子左边要显示的用户信息项目
            userfaceshow = customauthorinfo[1].Split(',');//头像上方要显示的项目
            //if (newpmcount > 0)
            //    pmlist = PrivateMessages.GetPrivateMessageListForIndex(userid, 5, 1, 1);

            onlyauthor = (onlyauthor == "1" || onlyauthor == "2") ? onlyauthor : "0";
            // 获取分页相关信息
            BindPageCountAndId();

            GetPostAds(GetPostPramsInfo(price), postlist.Count);

            #region 获取特殊主题相关信息
            bonuslogs = Bonus.GetLogs(topic);

            if (topic.Special == 1)//获取投票信息
                GetPollInfo();

            if (topic.Special == 4) //获取辩论信息
                GetDebateInfo();

            if (topic.Special == 128)
            {
                lotteryinfo = PTLottery.GetLotteryInfo(topicid);
                List<LotteryWager> lotterylist = PTLottery.GetLotteryWagerListbyUid(topicid, userid);
                int totalcount = 0;
                foreach (LotteryWager lotteryw in lotterylist)
                {
                    lotterycuroption = lotteryw.OptionId;
                    totalcount += lotteryw.WagerCount;
                }
                //投注超过100
                if (totalcount >= 100) lotterycuroption = -2;
                
                //投注尚未开始
                if (lotteryinfo.StartTime > DateTime.Now) lotterycuroption = -3;

                //投注已过期
                if (DateTime.Now > lotteryinfo.EndTime) lotterycuroption = -4;

                //已结贴
                if (lotteryinfo.Ended > 0) lotterycuroption = -5;
            }

            #endregion


            //是否有权限自行删除或关闭主题
            if (topic.Posterid == userid)
            {
                if (forumid == 37 && topic.Closed != 1) allowselfclose = true;
                else if (topic.Replies == 0)
                {
                    if ((config.Deletetimelimit != 0 && Utils.StrDateDiffMinutes(topic.Postdatetime, config.Deletetimelimit) < 0) )
                    {
                        allowselfdel = true;
                    }
                }
            }


            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】

            if (topic.SeedId > 0)        //读取种子信息
            {
                AddErrLine("错误：该主题不存在");
                return;

                seedinfo = PTSeeds.GetSeedInfo(topic.SeedId);

                //仅在种子上传系数>=1.0的时候，发布者享受2倍上传
                if (seedinfo.Uid == userid && seedinfo.UploadRatio >= 1.0)
                {
                    seedinfo.UploadRatio = 2.0f;

                }

                if (seedinfo.SeedId < 1)
                {
                    AddErrLine("错误：该主题不存在");
                    Title = "页面";
                    topic.Title = "页面";
                    pagetitle = "页面";
                    return;
                }
                if (DNTRequest.GetString("downloadseed") == "yes")
                {
                    autodownloadseed = "downloadseed.aspx?seedid=" + seedinfo.SeedId.ToString();
                    //SetUrl("http://bt.buaa6.edu.cn/downloadseed.aspx?seedid=" + seedinfo.SeedId.ToString());
                    //SetMetaRefresh();
                }
                

                //流量系数过期信息
                uploadratioNote = PrivateBT.GetRatioNote(seedinfo, 1);
                downloadratioNote = PrivateBT.GetRatioNote(seedinfo, 2);
                blueseednote = PrivateBT.GetRatioNote(seedinfo, 3);

                //电影类 imdb链接
                //if (seedinfo.Type == 1)
                //{
                //    if (Utils.IsInt(seedinfo.Info3))
                //    {
                //        imdblink = "http://www.imdb.com/title/tt" + Utils.StrToInt(seedinfo.Info3, 0).ToString("0000000");
                //    }
                //    else if (seedinfo.Info3.IndexOf("tt") > -1 && seedinfo.Info3.IndexOf("tt") + 2 < seedinfo.Info3.Length)
                //    {
                //        imdblink = imdblink = "http://www.imdb.com/title/tt" + Utils.StrToInt(seedinfo.Info3.Substring(seedinfo.Info3.IndexOf("tt") + 2), 0).ToString("0000000");
                //    }
                //}

                if (seedinfo.Traffic - seedinfo.UpTraffic > 1024 * 1024 * 1024M) trafficcheck = "blue";
                else if (seedinfo.UpTraffic - seedinfo.Traffic > 1024 * 1024 * 1024 * 50M) trafficcheck = "red";
                else if (seedinfo.UpTraffic - seedinfo.Traffic > 1024 * 1024 * 1024M) trafficcheck = "yellow";
                else trafficcheck = "green";

                //下载警告，自己发的种子没有下载警告，ALERT时显示允许下载警告，“警告”时不允许下载
                if (PTTools.GetRatioAlert(userInfo.Extcredits3, userInfo.Extcredits4 + seedinfo.FileSize) != "" && seedinfo.Uid != userid && seedinfo.DownloadRatio != 0) 
                {
                    if ((userInfo.RatioProtection & 1) > 0) ratioprotectionNote = "警告";
                    else ratioprotectionNote = "ALERT";
                }
                //如果种子内记录和帖子内记录不相等，则更新
                if (seedinfo.Views != topic.Views)
                {
                    PTSeeds.UpdateSeedTopicViewCountAccurate(topic.Tid, seedinfo.SeedId);
                }
            }
            else if (topic.Special == 64 && topic.SeedId < 0)
            {
                abtseedinfo = PTAbt.AbtGetSeedInfo(-topic.SeedId);
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////


            enabletag = (config.Enabletag & forum.Allowtag) == 1;
            //if (enabletag)
            //    relatedtopics = Topics.GetRelatedTopicList(topicid, 5);

            //更新页面Meta信息
            if (postlist != null && postlist.Count > 0)
                UpdateMetaInfo(Utils.RemoveHtml(postlist[0].Message));

            //判断是否需要生成游客缓存页面
            IsGuestCachePage();

            
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】修复查看次数bug，topicviews计算放到前面，否则当需要更新时，显示会出错
            
            //更新主题查看次数和在线用户信息
            topicviews = topic.Views + 1 + (config.TopicQueueStats == 1 ? TopicStats.GetStoredTopicViewCount(topic.Tid) : 0);
            TopicStats.Track(topicid, 1);
            Topics.MarkOldTopic(topic);
            
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            OnlineUsers.UpdateAction(olid, UserAction.ShowTopic.ActionID, forumid, forum.Name, topicid, topic.Title);

            //如果是从
            if (DNTRequest.GetInt("fromfav", 0) > 0)
                Favorites.UpdateUserFavoriteViewTime(userid, topicid);
            //UserCredits.UpdateUserCredits(userInfo);此方法与后台积分设置中的条目不匹配，故注释

            //清空postid，防止当链接地址中存在postid（通知中点击等情况）时，造成获取到异常的postid，回复通知异常
            //此处应该也可以删除showtopic中相应条目？？？只删除showtopic中的条目不会改变ajax提交的数值。。。
            if(infloat != 1) postid = 0;
        }

        /// <summary>
        /// 判断是否需要生成游客缓存页面
        /// </summary>
        public void IsGuestCachePage()
        {
            //这里假设最后一页之前的所有页面未修改，均可被缓存
            if (userid < 1 && pageid > 0 && pageid < pagecount && ForumUtils.IsGuestCachePage(pageid, "showtopic"))
            {
                //int topiccachemark = 100 - (int)
                //                     (topic.Displayorder * 15 + topic.Digest * 10 + Math.Min(topic.Views / 20, 50) +
                //                      Math.Min(topic.Replies / config.Ppp * 1.5, 15));
                //if (topiccachemark < config.Topiccachemark)
                //{
                isguestcachepage = 1;
                //}
            }
        }

        /// <summary>
        /// 获取投票信息
        /// </summary>
        public void GetPollInfo()
        {
            pollinfo = Polls.GetPollInfo(topicid);
            voters = Polls.GetVoters(topicid, userid, username, out allowvote);

            if (pollinfo.Uid != userid && useradminid != 1) //当前用户不是投票发起人或不是管理组成员
            {
                if (pollinfo.Visible == 1 && //当为投票才可见时
                (allowvote || (userid < 1 && !Utils.InArray(topicid.ToString(), ForumUtils.GetCookie("dnt_polled")))))//当允许投票或为游客(且并未投过票时)时
                {
                    showpollresult = false;
                }
            }

            if (Utils.StrIsNullOrEmpty(pollinfo.Expiration))
                pollinfo.Expiration = DateTime.Now.ToString();

            if (DateTime.Parse(pollinfo.Expiration) < DateTime.Now)
                allowvote = false;
        }

        /// <summary>
        /// 获取辩论信息
        /// </summary>
        public void GetDebateInfo()
        {
            debateexpand = Debates.GetDebateTopic(topicid);
            debateList = Debates.GetPostDebateList(topicid);//通过TID得到帖子观点
            if (debateexpand.Terminaltime < DateTime.Now)
                isenddebate = true;

            foreach (ShowtopicPagePostInfo postlistinfo in postlist)
            {
                //设置POST的观点属性
                if (debateList != null && debateList.ContainsKey(postlistinfo.Pid))
                    postlistinfo.Debateopinion = debateList[postlistinfo.Pid];
            }
        }


        /// <summary>
        /// 获取主题价格
        /// </summary>
        /// <param name="topicInfo"></param>
        /// <returns></returns>
        public int GetTopicPrice(TopicInfo topicInfo)
        {
            int price = 0;
            if (topicInfo.Special == 0)//普通主题
            {
                //购买帖子操作
                //判断是否为购买可见帖, price=0为非购买可见(正常), price>0 为购买可见, price=-1为购买可见但当前用户已购买                
                if (topicInfo.Price > 0 && userid != topicInfo.Posterid)
                {
                    price = topicInfo.Price;
                    //时间乘以-1是因为当Configs.GetMaxChargeSpan()==0时,帖子始终为购买帖
                    if (PaymentLogs.IsBuyer(topicInfo.Tid, userid) ||
                        (Utils.StrDateDiffHours(topicInfo.Postdatetime, Scoresets.GetMaxChargeSpan()) > 0 &&
                         Scoresets.GetMaxChargeSpan() != 0)) //判断当前用户是否已经购买
                    {
                        price = -1;
                    }
                }
            }
            return price;
        }

        /// <summary>
        /// 获取帖子参数信息(PostPramsInfo)
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public PostpramsInfo GetPostPramsInfo(int price)
        {
            //获取当前页主题列表
            PostpramsInfo postpramsInfo = new PostpramsInfo();
            postpramsInfo.Fid = forum.Fid;
            postpramsInfo.Tid = topicid;
            postpramsInfo.Jammer = forum.Jammer;
            postpramsInfo.Pagesize = ppp;     // 得到Ppp设置
            postpramsInfo.Pageindex = pageid;
            postpramsInfo.Getattachperm = forum.Getattachperm;
            postpramsInfo.Usergroupid = usergroupid;
            postpramsInfo.Attachimgpost = config.Attachimgpost;
            postpramsInfo.Showattachmentpath = config.Showattachmentpath;
            postpramsInfo.Price = price;
            postpramsInfo.Usergroupreadaccess = (ismoder == 1) ? int.MaxValue : usergroupinfo.Readaccess;
            postpramsInfo.CurrentUserid = userid;
            
            //【BT修改】，选择模板5，手机版时不解析图片
            if (templateid == 5)
                postpramsInfo.Showimages = 0;
            else
                postpramsInfo.Showimages = forum.Allowimgcode;

            postpramsInfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
            postpramsInfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
            postpramsInfo.Smiliesmax = config.Smiliesmax;
            postpramsInfo.Bbcodemode = config.Bbcodemode;
            postpramsInfo.CurrentUserGroup = usergroupinfo;
            postpramsInfo.Topicinfo = topic;
            //判断是否为回复可见帖, hide=0为不解析[hide]标签, hide>0解析为回复可见字样, hide=-1解析为以下内容回复可见字样显示真实内容
            //将逻辑判断放入取列表的循环中处理,此处只做是否为回复人的判断，主题作者也该可见
            postpramsInfo.Hide = topic.Hide >= 1 ? (ismoder == 1 || Posts.IsReplier(topicid, userid) ? -1 : topic.Hide) : topic.Hide;
            postpramsInfo.Hide = topic.Posterid == userid ? -2 : postpramsInfo.Hide;
            hide = postpramsInfo.Hide;
            postpramsInfo.Condition = Posts.GetPostPramsInfoCondition(onlyauthor, topicid, posterid);
            postpramsInfo.TemplateWidth = Templates.GetTemplateWidth(templatepath);//获取当前模版的宽度
            postpramsInfo.Usercredits = userInfo == null ? 0 : userInfo.Credits;
            postlist = Posts.GetPostList(postpramsInfo, out attachmentlist, ismoder == 1);
            if (postlist.Count == 0)
            {
                TopicAdmins.RepairTopicList(topicid.ToString());
                topic = GetTopicInfo();
                BindPageCountAndId();
                postpramsInfo.Pageindex = pagecount;
                postlist = Posts.GetPostList(postpramsInfo, out attachmentlist, ismoder == 1);
            }

            foreach (ShowtopicPageAttachmentInfo showtopicpageattachinfo in attachmentlist)
            {
                if (Forums.AllowGetAttachByUserID(forum.Permuserlist, userid))
                {
                    showtopicpageattachinfo.Getattachperm = 1;
                    showtopicpageattachinfo.Allowread = 1;
                }
            }
            BindDownloadAttachmentTip();

            return postpramsInfo;
        }

        /// <summary>
        /// 获取当前页数和页面ID
        /// </summary>
        public new void BindPageCountAndId()
        {
            base.BindPageCountAndId();

            //得到页码链接
            if (Utils.StrIsNullOrEmpty(onlyauthor) || onlyauthor == "0")
            {
                if (config.Aspxrewrite == 1 && typeid<-1)
                    pagenumbers = Utils.GetStaticPageNumbers(pageid, pagecount, "showtopic-" + topicid, config.Extname, 8);
                else
                    pagenumbers = Utils.GetPageNumbers(pageid, pagecount, string.Format("showtopic.aspx?topicid={0}&forumpage={1}&typeid={2}", topicid, forumpageid, typeid), 8);
            }
            else
                pagenumbers = Utils.GetPageNumbers(pageid, pagecount, string.Format("showtopic.aspx?onlyauthor={0}&topicid={1}&forumpage={2}&posterid={3}{4}", onlyauthor, topicid, forumpageid, posterid, (typeid>=0 ? "&typeid=" + typeid : "")), 8);

            if (pageid != pagecount)
            {
                if (string.IsNullOrEmpty(onlyauthor) || onlyauthor == "0")
                {
                    string showtopiclink = Urls.ShowTopicAspxRewrite(topicid, pageid + 1, typeid);
                    nextpage = "<a href=\"" + showtopiclink + (showtopiclink.IndexOf("?") < 0 ? "" :
                        string.Format("&onlyauthor={0}&forumpage={1}&posterid={2}", onlyauthor, forumpageid, posterid)) + "\" class=\"next\">下一页</a>";
                    nextpageurl = showtopiclink + (showtopiclink.IndexOf("?") < 0 ? "" :
                        string.Format("&onlyauthor={0}&forumpage={1}&posterid={2}", onlyauthor, forumpageid, posterid));
                }
            }

            showvisitedforumsmenu = visitedforums != null && ((visitedforums.Length == 1 && visitedforums[0].Fid != forumid) || visitedforums.Length > 1);
            //获取查看列表的页数
            int forumpid = TypeConverter.ObjectToInt(Utils.GetCookie("forumpageid"), 1);

            if (typeid  <0)
                listlink = "<a id=\"visitedforums\" href=\"" + Urls.ShowForumAspxRewrite(forumid, forumpid, forum.Rewritename) + "\"";
            else
                listlink = "<a id='visitedforums' href=showforum.aspx?forumid=" + forumid + "&page=" + forumpid + "&typeid=" + typeid + "";
            if (showvisitedforumsmenu)
                listlink += " onmouseover=\"$('visitedforums').id = 'visitedforumstmp';this.id = 'visitedforums';showMenu({'ctrlid':this.id, 'pos':'34'})\";";
            listlink += ">返回列表</a>";

            //写入页码链接
            if (onlyauthor == "" || onlyauthor == "0")
                ForumUtils.WriteCookie("referer", string.Format("showtopic.aspx?topicid={0}&page={1}&forumpage=" + forumpageid, topicid, pageid));
            else
                ForumUtils.WriteCookie("referer", "showtopic.aspx?onlyauthor=" + onlyauthor + "&topicid=" + topicid + "&forumpage=" + forumpageid + "&page=" + pageid + "&posterid=" + posterid);
        }

        public string GetJsFormat(string str)
        {
            return str.Replace("'", "\\'");
        }
    }
}
