using System;
using System.Web;
using System.Data;
using System.Text;
using System.Threading;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Forum;
using Discuz.Entity;
using Discuz.Data;
using Discuz.Config;



namespace Discuz.Web
{
    /// <summary>
    /// Rss种子更新页面，支持直接下载，访问格式 http://buaabt.cn/rssd.aspx?uid=123&passkey=xxxxx&type=movie&page=1
    /// page不写，默认最新种子，type不写，默认全部种子
    /// </summary>
    public partial class rssd : System.Web.UI.Page, System.Web.SessionState.IRequiresSessionState
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //////////////////////////////////////////////////////////////////////////
            //各项参数
            
            string passkey = DNTRequest.GetString("passkey").Trim().ToUpper();
            int rssuid = DNTRequest.GetInt("uid", -1);
            int seedtype = PrivateBT.Str2Type(DNTRequest.GetString("type").Trim().ToLower());
            int rsspage = DNTRequest.GetInt("page", 1);
            string clientip = DNTRequest.GetIP();
            //rss类别，特殊rss使用
            int rsstype = DNTRequest.GetInt("rsstype", -1);
            bool rssError = false;


            #region 用户Passkey和UID 校验

            if (rsstype != 2)
            {
                if (passkey.Length != 32) rssError = true;
                if (rssuid < 1) rssError = true;

                if (!rssError)
                {
                    //防止暴力猜测uid
                    int errcount = Discuz.Forum.LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), false);
                    if (errcount > 5) { SendEmptyRSS("IP_TEMPORARYLY_BANNED"); return; }

                    PTUserInfo btuserinfo = PTUsers.GetBtUserInfo(rssuid);
                    if (btuserinfo.Passkey != passkey)
                    {
                        rssError = true;

                        //重试校验，此处应该增加重试校验，防止暴力猜测uid
                        errcount = Discuz.Forum.LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), true);
                        if (errcount > 5) { SendEmptyRSS("IP_TEMPORARYLY_BANNED"); return; }
                    }
                }
            }
            else
            {
                passkey = "";
                rssuid = -1;
            }

            #endregion

            //////////////////////////////////////////////////////////////////////////
            //发送列表



            // Amphetamine:RSS_ACC，Core2使用普通RSS
            // RSSTYPE 分类
            // -1. 普通RSS
            // 1. RSS_ACC 新种加速
            // 2. RSS_RE 经典种子列表
            // 3. RSS_ACC_SH 热门种子加速


            //用户校验未通过
            if (rssError) 
            {
                if (rsstype == 2) { SendEmptyPublicRss("FGBTRSS_END_OF_LIST"); return; }
                else { SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }
            }
            
            else  
            {
                //通过安全校验，发送RSS数据

                
                if (rsstype < 1)
                {
                    #region 普通RSS，面向普通用户

                    int seedinfocount = PTSeeds.GetSeedInfoCount(seedtype, 0, 0, 0, "", 0, "");
                    int pagecount = seedinfocount % 100 == 0 ? seedinfocount / 100 : seedinfocount / 100 + 1;
                    if (rsspage > pagecount) { SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }

                    DataTable seedinfolist = null;
                    if (rssuid == 7442){seedinfolist = PTSeeds.GetSeedRssList(100, rsspage, seedtype, 1);}
                    else {seedinfolist = PTSeeds.GetSeedRssList(100, rsspage, seedtype, 0);}

                    SendRssHead();
                    foreach (DataRow dr in seedinfolist.Rows)
                    {
                        if (dr["type"].ToString() == "1")
                        {
                            if (rssuid == 7442)
                            {
                                SendSeedInfoWithLastStarted(passkey, dr["topictitle"].ToString(), int.Parse(dr["seedid"].ToString()), dr["topictitle"].ToString(), PrivateBT.Type2Str(int.Parse(dr["type"].ToString())), dr["info3"].ToString(), rssuid, TypeConverter.ObjectToDateTime(dr["firstupdate"], new DateTime(1990,1,1)), TypeConverter.ObjectToInt(dr["accrss"], -1));
                            }
                            else
                                SendSeedInfo(passkey, dr["topictitle"].ToString(), int.Parse(dr["seedid"].ToString()), dr["topictitle"].ToString(), PrivateBT.Type2Str(int.Parse(dr["type"].ToString())), "", rssuid);
                        }
                        else
                        {
                            SendSeedInfo(passkey, dr["topictitle"].ToString(), int.Parse(dr["seedid"].ToString()), dr["topictitle"].ToString(), PrivateBT.Type2Str(int.Parse(dr["type"].ToString())), "", rssuid);
                        }
                    }
                    seedinfolist.Dispose();
                    SendRssEnd();
                    return;

                    #endregion
                }
                else
                {
                    #region 特殊RSS权限校验，面向服务器用户，自动加速、保种、外链等

                    //特殊RSS权限校验，RSS_ACC
                    if (rsstype == 1)
                    {
                        //UID校验，Amphetamine，22854
                        if (rssuid != 1 && rssuid != 22854) rssError = true;

                        //IP校验
                        if (clientip != "127.0.0.1" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx"
                            && clientip != "xxx.xxx.xxx.xxx") rssError = true;

                    }
                    //特殊RSS权限校验，推荐种子
                    else if (rsstype == 2)
                    {
                        //rssError = true;
                    }
                    //特殊RSS权限校验，RSS_ACC_SH
                    else if (rsstype == 3)
                    {
                        //UID校验，H1N1，2268
                        if (rssuid != 1 && rssuid != 2268) rssError = true;

                        //IP校验
                        if (clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx") rssError = true;
                    }
                    //特殊RSS权限校验，STATUS_CHECK
                    else if (rsstype == 4)
                    {
                        //UID校验，H1N1，2268
                        if (rssuid != 1 && rssuid != 2268 && rssuid != 22854) rssError = true;

                        //IP校验
                        if (clientip != "127.0.0.1" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" &&
                            clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && 
                            clientip != "xxx.xxx.xxx.xxx") rssError = true;
                    }
                    //特殊RSS权限校验，RSS_OLDHOT
                    else if (rsstype == 5)
                    {
                        //UID校验，Challenger，5695
                        if (rssuid != 1 && rssuid != 5695 && rssuid != 10597) rssError = true;

                        //IP校验
                        if (clientip != "127.0.0.1" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx"
                            && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx") rssError = true;
                    }
                    //特殊RSS权限校验，RSS_KEEPHOT
                    else if (rsstype == 6)
                    {
                        //UID校验，lxzylllsl，10597
                        if (rssuid != 1 && rssuid != 10597) rssError = true;

                        //IP校验
                        if (clientip != "127.0.0.1" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx") rssError = true;
                    }
                    //特殊RSS权限校验，RSS_OLDHOT
                    else if (rsstype == 7)
                    {
                        //UID校验，Challenger，5695
                        if (rssuid != 1 && rssuid != 5695 && rssuid != 10597) rssError = true;

                        //IP校验
                        if (clientip != "127.0.0.1" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx"
                            && clientip != "xxx.xxx.xxx.xxx" && clientip != "xxx.xxx.xxx.xxx") rssError = true;
                    }
                    else
                    {
                        rssError = true;
                    }

                    #endregion


                    //发送RSS
                    if (rssError)
                    {
                        if (rsstype == 2) { SendEmptyPublicRss("FGBTRSS_END_OF_LIST"); return; }
                        else { SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }
                    }
                    else
                    {
                        
                        if (rsstype == 2)
                        {
                            #region 推荐种子
                            int seedinfocount = PTRss.GetSeedRssCountbyType(seedtype, rsstype);
                            int pagecount = seedinfocount % 100 == 0 ? seedinfocount / 100 : seedinfocount / 100 + 1;
                            if (rsspage > pagecount) { SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }

                            System.Collections.Generic.List<PTSeedRssinfo> seedrsslist = PTRss.GetSeedRssListbyType(100, rsspage, seedtype, rsstype);

                            GeneralConfigInfo config = GeneralConfigs.GetConfig();
                            ForumInfo forum = Forum.Forums.GetForumInfo(23);
                            SendRssHead();
                            foreach (PTSeedRssinfo rssinfo in seedrsslist)
                            {
                                SendPublicSeedInfo(rssinfo.SeedTitle, rssinfo.Seedid, rssinfo.SeedType.ToString(), rssinfo.Rssid.ToString(), rssuid, rssinfo.Rssid, rssinfo.RssType, rssinfo.Topicid, rssinfo.RssStatus, config, forum);
                            }
                            SendRssEnd();
                            return;
                            #endregion
                        }
                        //
                        else if (rsstype == 3)
                        {
                            #region RSS_ACC_SH
                            DataTable seedinfolist = seedinfolist = PTRss.GetSeedRssListHotDownload(2000, 5);

                            if (seedinfolist.Rows.Count < 1) { seedinfolist.Dispose(); SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }

                            SendRssHead();
                            foreach (DataRow dr in seedinfolist.Rows)
                            {
                                SendSeedInfo(passkey, dr["topictitle"].ToString(), int.Parse(dr["seedid"].ToString()), dr["topictitle"].ToString(), "RSS_ACC_SH", dr["downcount"].ToString(), rssuid);
                            }
                            seedinfolist.Dispose();
                            SendRssEnd();
                            return;
                            #endregion
                        }
                        else if (rsstype == 4)
                        {
                            #region STATUS_CHECK 种子状态检查RSS

                            string checkseed = DNTRequest.GetString("check");
                            if(checkseed.Length == 40)
                            {
                                int seedid = PTSeeds.GetSeedInfoTracker(checkseed).SeedId;
                                if (seedid > 0)
                                {
                                    //TEST URL: http://shaoyunbin.com/rssd.aspx?passkey=AUQNGUNFYFNNKQPZCADZ3A8OTJEJ7HWS&uid=1&rsstype=4&check=77A95E4493233A3785D03B7F783B40E76BCE6296&downcount=1
                                    //TEST REPLY: SEEDOK_77A95E4493233A3785D03B7F783B40E76BCE6296|DOWNLOAD24H:0|DOWNLOAD48H:0
                                    string appendstr24 = "";
                                    if (DNTRequest.GetString("downcount") == "1")
                                    {
                                        appendstr24 = string.Format("|DOWNLOAD24H:{0}|DOWNLOAD48H:{1}",
                                            PrivateBT.GetNewTrafficRecordCount(seedid, DateTime.Now.AddDays(-1)), PrivateBT.GetNewTrafficRecordCount(seedid, DateTime.Now.AddDays(-2)));
                                    }
                                    SendText("SEEDOK_" + checkseed + appendstr24);
                                    return;
                                }
                                else { SendText("SEEDFAIL_" + checkseed); return; }
                                
                            }
                            else { SendText("MALFORMAT_" + checkseed); return; }

                            #endregion
                        }
                        else if (rsstype == 5)
                        {
                            #region RSS_OLDHOT 发送热门老种RSS

                            DataTable seedinfolist = seedinfolist = PTRss.GetSeedRssListOldHot(8, 30, 240, 2000);

                            if (seedinfolist.Rows.Count < 1) { seedinfolist.Dispose(); SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }

                            SendRssHead();
                            foreach (DataRow dr in seedinfolist.Rows)
                            {
                                SendSeedInfo(passkey, dr["topictitle"].ToString(), int.Parse(dr["seedid"].ToString()), dr["topictitle"].ToString(), "RSS_OLDHOT", dr["downcount"].ToString(), rssuid);
                            }
                            seedinfolist.Dispose();
                            SendRssEnd();
                            
                            #endregion
                        }
                        else if (rsstype == 6)
                        {
                            #region RSS_NEWHOT 发送热门新种RSS

                            DataTable seedinfolist = seedinfolist = PTRss.GetSeedRssListKeepHot(5, 1, 2, 10, 300);

                            if (seedinfolist.Rows.Count < 1) { seedinfolist.Dispose(); SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }

                            SendRssHead();
                            foreach (DataRow dr in seedinfolist.Rows)
                            {
                                SendSeedInfo(passkey, dr["topictitle"].ToString(), int.Parse(dr["seedid"].ToString()), dr["topictitle"].ToString(), "RSS_KEEPHOT", dr["downcount"].ToString(), rssuid);
                            }
                            seedinfolist.Dispose();
                            SendRssEnd();

                            #endregion
                        }
                        else if (rsstype == 7)
                        {
                            #region RSS_OLDHOT_NMB 发送热门老种RSS

                            DataTable seedinfolist = seedinfolist = PTRss.GetSeedRssListOldHotNMB(8, 30, 120, 2000);

                            if (seedinfolist.Rows.Count < 1) { seedinfolist.Dispose(); SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }

                            SendRssHead();
                            foreach (DataRow dr in seedinfolist.Rows)
                            {
                                SendSeedInfo(passkey, dr["topictitle"].ToString(), int.Parse(dr["seedid"].ToString()), dr["topictitle"].ToString(), "RSS_OLDHOT", dr["downcount"].ToString(), rssuid);
                            }
                            seedinfolist.Dispose();
                            SendRssEnd();

                            #endregion
                        }
                        else
                        {
                            #region 其他？？？

                            int seedinfocount = PTRss.GetSeedRssCountbyType(seedtype, rsstype);
                            int pagecount = seedinfocount % 100 == 0 ? seedinfocount / 100 : seedinfocount / 100 + 1;
                            if (rsspage > pagecount) { SendEmptyRSS("FGBTRSS_END_OF_LIST"); return; }


                            System.Collections.Generic.List<PTSeedRssinfo> seedrsslist = PTRss.GetSeedRssListbyType(100, rsspage, seedtype, rsstype);

                            SendRssHead();
                            foreach (PTSeedRssinfo rssinfo in seedrsslist)
                            {
                                SendSeedInfo(passkey, rssinfo.SeedTitle, rssinfo.Seedid, rssinfo.SeedTitle, rssinfo.SeedType.ToString(), rssinfo.Rssid.ToString(), rssuid, rssinfo.Rssid, rssinfo.RssType, rssinfo.Topicid, rssinfo.RssStatus);
                            }
                            SendRssEnd();

                            #endregion
                        }
                    }
                }
                

                return;
            }



        }

        /// <summary>
        /// 发送纯文本
        /// </summary>
        /// <param name="message"></param>
        protected void SendText(string message)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write(message);
                Response.Flush();
                Response.End();
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.RssSendText, PTLog.LogStatus.Detail, "SendText", string.Format("{IP:{0} -MESSAGE:{1} -URL:{2} -EX:{3}}", Request.UserHostAddress, message, Request.RawUrl, ex));
            }
        }

        /// <summary>
        /// 发送空的RSS文档
        /// </summary>
        /// <param name="message"></param>
        protected void SendEmptyPublicRss(string message)
        {
            SendRssHead();
            SendPublicSeedInfo(message, 0, message, message, 0, 0, 0, 0, 0, null, null);
            SendRssEnd();
        }

        /// <summary>
        /// 发送空的RSS文档
        /// </summary>
        /// <param name="message"></param>
        protected void SendEmptyRSS(string message)
        {
            SendRssHead();
            SendSeedInfo(message, message, 0, message, message, message, 0);
            SendRssEnd();
        }

        protected void SendRssHead()
        {
            Response.ContentType = "text/xml";
            Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n");
            Response.Write("<rss version=\"0.91\">\n");
            Response.Write("<channel>\n");
            Response.Write("<title>FGBT Torrents</title>\n");
            Response.Write("<description>FGBT(buaabt.cn)</description>\n");
            Response.Write("<link>http://buaabt.cn/</link>\n");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
        }

        protected void SendRssEnd()
        {
            Response.Write("");
            Response.Write("</channel>\n");
            Response.Write("</rss>\n");

            try
            {
                Response.Flush();
                Response.End();
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.RssSendRss, PTLog.LogStatus.Detail, "SendRss", string.Format("{IP:{0} -URL:{2} -EX:{3}}", Request.UserHostAddress, Request.RawUrl, ex));
            }
        }

        protected void SendSeedInfo(string passkey, string title, int seedid, string description, string type, string guid, int rssuid)
        {
            SendSeedInfo(passkey, title, seedid, description, type, guid, rssuid, 0, 0, 0, 0);
        }
        protected void SendSeedInfo(string passkey, string title, int seedid, string description, string type, string guid, int rssuid, int rssid, int rsstype, int topicid, int rssstatus)
        {
            Response.Write("<item>\n");
            Response.Write(string.Format("<title>{0}</title>\n", title.Trim()));
            Response.Write(string.Format("<seedid>{0}</seedid>\n", seedid));

            if (rssid > 0) Response.Write(string.Format("<rssid>{0}</rssid>\n", rssid));
            if (rsstype > 0) Response.Write(string.Format("<rsstype>{0}</rsstype>\n", rsstype));
            if (topicid > 0) Response.Write(string.Format("<topicid>{0}</topicid>\n", topicid));
            if (rssstatus != 0) Response.Write(string.Format("<rssstatus>{0}</rssstatus>\n", rssstatus));

            Response.Write(string.Format("<seedtype>{0}</seedtype>\n", type.Trim()));
            Response.Write(string.Format("<guid>{0}</guid>\n", guid.Trim()));
            Response.Write(string.Format("<description>{0}</description>\n", description.Trim()));
            Response.Write(string.Format("<link>http://{0}/downloadseedrssd.aspx?seedid={1}&amp;uid={2}&amp;passkey={3}</link>\n", DNTRequest.GetHost(), seedid, rssuid, passkey.Trim()));
            Response.Write("</item>\n");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
        }
        protected void SendSeedInfoWithLastStarted(string passkey, string title, int seedid, string description, string type, string guid, int rssuid, DateTime laststarted, int rssstatus)
        {
            SendSeedInfoWithLastStarted(passkey, title, seedid, description, type, guid, rssuid, 0, 0, 0, rssstatus, laststarted);
        }
        protected void SendSeedInfoWithLastStarted(string passkey, string title, int seedid, string description, string type, string guid, int rssuid, int rssid, int rsstype, int topicid, int rssstatus, DateTime laststarted)
        {
            Response.Write("<item>\n");
            Response.Write(string.Format("<title>{0}</title>\n", title.Trim()));
            Response.Write(string.Format("<seedid>{0}</seedid>\n", seedid));

            if (rssid > 0) Response.Write(string.Format("<rssid>{0}</rssid>\n", rssid));
            if (rsstype > 0) Response.Write(string.Format("<rsstype>{0}</rsstype>\n", rsstype));
            if (topicid > 0) Response.Write(string.Format("<topicid>{0}</topicid>\n", topicid));
            
            Response.Write(string.Format("<rssstatus>{0}</rssstatus>\n", rssstatus));

            Response.Write(string.Format("<seedtype>{0}</seedtype>\n", type.Trim()));
            Response.Write(string.Format("<guid>{0}</guid>\n", guid.Trim()));
            Response.Write(string.Format("<last>{0}</last>\n", laststarted.ToString()));
            Response.Write(string.Format("<description>{0}</description>\n", description.Trim()));
            Response.Write(string.Format("<link>http://{0}/downloadseedrssd.aspx?seedid={1}&amp;uid={2}&amp;passkey={3}</link>\n", DNTRequest.GetHost(), seedid, rssuid, passkey.Trim()));
            Response.Write("</item>\n");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
        }

        /// <summary>
        /// 公共RSS？
        /// </summary>
        /// <param name="title"></param>
        /// <param name="seedid"></param>
        /// <param name="type"></param>
        /// <param name="guid"></param>
        /// <param name="rssuid"></param>
        /// <param name="rssid"></param>
        /// <param name="rsstype"></param>
        /// <param name="topicid"></param>
        /// <param name="rssstatus"></param>
        /// <param name="config"></param>
        /// <param name="forum"></param>
        protected void SendPublicSeedInfo(string title, int seedid, string type, string guid, int rssuid, int rssid, int rsstype, int topicid, int rssstatus, GeneralConfigInfo config, ForumInfo forum)
        {
            List<ShowtopicPageAttachmentInfo> attachList = new List<ShowtopicPageAttachmentInfo>();
            string message = "";
            

            if (topicid > 0)
            {

                TopicInfo topic = Forum.Topics.GetTopicInfo(topicid);
                UserGroupInfo usergroupinfo = Forum.UserGroups.GetUserGroupInfo(10);
                int userid = 0;
                int ismoder = 0;

                //PostInfo seedpostinfo = Forum.Posts.GetTopicPostInfo(topicid);
                //if (seedpostinfo != null && seedpostinfo.Tid > 0)
                //{
                //    message = seedpostinfo.Message;
                //}


                //获取当前页主题列表
                PostpramsInfo postpramsInfo = new PostpramsInfo();
                postpramsInfo.Fid = forum.Fid;
                postpramsInfo.Tid = topicid;
                postpramsInfo.Jammer = forum.Jammer;
                postpramsInfo.Pagesize = 1;     // 得到Ppp设置
                postpramsInfo.Pageindex = 1;
                postpramsInfo.Getattachperm = forum.Getattachperm;
                postpramsInfo.Usergroupid = usergroupinfo.Groupid; //初学咋练的gourpid
                postpramsInfo.Attachimgpost = config.Attachimgpost;
                postpramsInfo.Showattachmentpath = config.Showattachmentpath;
                postpramsInfo.Price = 0;
                postpramsInfo.Usergroupreadaccess = usergroupinfo.Readaccess;
                postpramsInfo.CurrentUserid = userid;
                postpramsInfo.Showimages = forum.Allowimgcode;
                postpramsInfo.Smiliesinfo = Forum.Smilies.GetSmiliesListWithInfo();
                postpramsInfo.Customeditorbuttoninfo = Forum.Editors.GetCustomEditButtonListWithInfo();
                postpramsInfo.Smiliesmax = config.Smiliesmax;
                postpramsInfo.Bbcodemode = config.Bbcodemode;
                postpramsInfo.CurrentUserGroup = usergroupinfo;
                postpramsInfo.Topicinfo = topic;
                postpramsInfo.Condition = Forum.Posts.GetPostPramsInfoCondition("", topicid, topic.Posterid);

                //判断是否为回复可见帖, hide=0为不解析[hide]标签, hide>0解析为回复可见字样, hide=-1解析为以下内容回复可见字样显示真实内容
                //将逻辑判断放入取列表的循环中处理,此处只做是否为回复人的判断，主题作者也该可见
                postpramsInfo.Hide = (topic.Hide == 1 && (Forum.Posts.IsReplier(topicid, userid) || ismoder == 1)) ? -1 : 1;
                postpramsInfo.Pid = Forum.Posts.GetFirstPostId(topic.Tid);
                UserInfo userInfo = Forum.Users.GetUserInfo(userid);
                postpramsInfo.Usercredits = userInfo == null ? 0 : userInfo.Credits;
                List<ShowtopicPageAttachmentInfo> attachmentlist;
                ShowtopicPagePostInfo showtopicpostinfo = Forum.Posts.GetSinglePostForRSS(postpramsInfo, out attachmentlist, ismoder == 1);
                postpramsInfo.Pid = 0;

                message = showtopicpostinfo.Message.Replace("&nbsp;", " ");
            }
            

            Response.Write("<item>\n");
            Response.Write(string.Format("<title>{0}</title>\n", title.Trim()));
            Response.Write(string.Format("<seedid>{0}</seedid>\n", seedid));

            if (rssid > 0) Response.Write(string.Format("<rssid>{0}</rssid>\n", rssid));
            if (rsstype > 0) Response.Write(string.Format("<rsstype>{0}</rsstype>\n", rsstype));
            if (topicid > 0) Response.Write(string.Format("<topicid>{0}</topicid>\n", topicid));
            if (rssstatus != 0) Response.Write(string.Format("<rssstatus>{0}</rssstatus>\n", rssstatus));

            Response.Write(string.Format("<seedtype>{0}</seedtype>\n", type.Trim()));
            Response.Write(string.Format("<guid>{0}</guid>\n", guid.Trim()));
            Response.Write(string.Format("<description>{0}</description>\n", message));
            Response.Write(string.Format("<link>http://{0}/downloadseed.aspx?seedid={1}</link>\n", DNTRequest.GetHost(), seedid));
            Response.Write("</item>\n");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
            Response.Write("");
        }

    }
}
