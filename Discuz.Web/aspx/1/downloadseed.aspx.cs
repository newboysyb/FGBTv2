using System;
using System.Web;
using System.Data;
using System.IO;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Plugin.Mall;


namespace Discuz.Web
{
    /// <summary>
    /// attachment页的类派生于BasePage类
    /// </summary>
    public class downloadseed : PageBase
    {
        #region 变量声明
        /// <summary>
        /// 附件所属主题信息
        /// </summary>
        public TopicInfo topic;
        /// <summary>
        /// 附件所属版块Id
        /// </summary>
        public int forumid;
        /// <summary>
        /// 附件所属版块名称
        /// </summary>
        public string forumname;
        /// <summary>
        /// 附件所属主题Id
        /// </summary>
        public int topicid;
        /// <summary>
        /// 附件所属主题标题
        /// </summary>
        public string topictitle;
        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav;
        /// <summary>
        /// 是否需要登录后进行下载
        /// </summary>
        public bool needlogin = true;


        #endregion 变量声明

        /// <summary>
        /// 种子信息
        /// </summary>
        public PTSeedinfo seedinfo;
        /// <summary>
        /// 种子Id
        /// </summary>
        public int seedid;

        protected override void ShowPage()
        {
            pagetitle = "种子下载";

            //下载种子必须登录
            if (userid < 1)
            {
                AddErrLine("你尚未登录");
                return;
            }
            //　如果当前用户非管理员并且论坛设定了禁止下载附件时间段，当前时间如果在其中的一个时间段内，则不允许用户下载附件
            if (useradminid != 1 && usergroupinfo.Disableperiodctrl != 1 && userid < 1)
            {
                string visittime = "";
                if (Scoresets.BetweenTime(config.Attachbanperiods, out visittime))
                {
                    AddErrLine("在此时间段( " + visittime + " )内用户不可以下载种子");
                    return;
                }
            }

            PTUserInfo downuserinfo = PTUsers.GetBtUserInfo(userid);

            // 获取种子ID
            seedid = DNTRequest.GetInt("seedid", -1);
            // 如果种子ID非数字
            if (seedid == -1)
            {
                AddErrLine("无效的种子ID");
                return;
            }

            //文件交换种子处理
            if (DNTRequest.GetInt("abtseed", -1) == 1) //&& DNTRequest.IsPost()
            {
                SendAbtSeed(DNTRequest.GetInt("seedid", -1));
                return;
            }

            // 获取该种子的信息
            seedinfo = PTSeeds.GetSeedInfo(seedid);

            #region 机器人的种子是否存在校验请求

            if (DNTRequest.GetInt("isexist", -1) == 9)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "text/html";
                if (seedinfo.SeedId < 1)
                {
                    HttpContext.Current.Response.Write("DELETED!");
                }
                else
                {
                    HttpContext.Current.Response.Write("OK!");
                }
                PageBase_ResponseEnd();
                return;
            }

            #endregion

            // 如果种子不存在
            if (seedinfo.SeedId < 1)
            {
                AddErrLine("不存在的种子ID");
                return;
            }

            #region 自动加速RSSACC检测和种子添加

            // 自动加速服务器RSS判断，peer和download检测  检测地点：下载种子、设置蓝种、设置置顶
            bool needadd2rss = false;
            if (seedinfo.Rss_Acc == 0 && seedinfo.PostDateTime > DateTime.Now.AddDays(-1))
            {
                if (seedinfo.PostDateTime > DateTime.Now.AddHours(-2))
                {
                    if (seedinfo.FileSize < 20 * 1024 * 1024 * 1024M)
                    {
                        if (seedinfo.Download > 3) needadd2rss = true;
                        if (seedinfo.Upload + seedinfo.Download > 6) needadd2rss = true;
                    }
                    else if (seedinfo.FileSize < 40 * 1024 * 1024 * 1024M)
                    {
                        if (seedinfo.Download > 6) needadd2rss = true;
                        if (seedinfo.Upload + seedinfo.Download > 12) needadd2rss = true;
                    }
                    else if (seedinfo.FileSize < 100 * 1024 * 1024 * 1024M)
                    {
                        if (seedinfo.Download > 9) needadd2rss = true;
                        if (seedinfo.Upload + seedinfo.Download > 18) needadd2rss = true;
                    }
                }
                else
                {
                    if (seedinfo.FileSize < 20 * 1024 * 1024 * 1024M)
                    {
                        if (seedinfo.Download > 5) needadd2rss = true;
                        if (seedinfo.Upload + seedinfo.Download > 10) needadd2rss = true;
                    }
                    else if (seedinfo.FileSize < 40 * 1024 * 1024 * 1024M)
                    {
                        if (seedinfo.Download > 10) needadd2rss = true;
                        if (seedinfo.Upload + seedinfo.Download > 20) needadd2rss = true;
                    }
                    else if (seedinfo.FileSize < 100 * 1024 * 1024 * 1024M)
                    {
                        if (seedinfo.Download > 15) needadd2rss = true;
                        if (seedinfo.Upload + seedinfo.Download > 30) needadd2rss = true;
                    }
                }
            }
            if (needadd2rss)
            {
                PTRss.AddSeedRss(seedinfo, 1, 1);
            }
            
            #endregion

            topicid = seedinfo.TopicId;
            // 获取该主题的信息
            topic = Topics.GetTopicInfo(topicid);
            // 如果该主题不存在
            if (topic == null)
            {
                AddErrLine("不存在的主题ID");
                return;
            }

            //Passkey格式校验
            if (downuserinfo.Passkey.Length != 32)
            {
                PTUsers.ResetPasskey(downuserinfo.Uid);
                downuserinfo = PTUsers.GetBtUserInfo(userid);
                if (downuserinfo.Passkey.Length != 32)
                {
                    AddErrLine("您的Passkey格式不正确，请先进行Passkey重置再进行下载");
                    return;
                }
            }



            topictitle = topic.Title;
            forumid = topic.Fid;
            ForumInfo forum = Forums.GetForumInfo(forumid);
            forumname = forum.Name;

            pagetitle = Utils.RemoveHtml(forum.Name);
            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);


            //判断帖子是否被屏蔽
            int postid = Posts.GetFirstPostId(topic.Tid);
            PostInfo seedpost = Posts.GetPostInfo(topic.Tid, postid);
            if (seedpost.Invisible != 0)
            {
                if (!Moderators.IsModer(useradminid, userid, forumid) && seedpost.Posterid != userid)
                {
                    AddErrLine("该种子已经被暂时屏蔽，等待作者修改，请稍后再试");
                    return;
                }
            }


            //【数据库查询-SELECT】
            PrivateBTConfigInfo btconfig = PrivateBTConfig.GetPrivateBTConfig();


            //获取上传下载因子（只需要对比config中的数值，不用进行seedinfo中的ratioexpiredate判断，此工作由计划任务完成，从bt_seed_tracker表中获取的seedinfo也没有系数过期时间）
            //自己发布的种子，在没有设置上传系数小于1的情况下，上传系数 = 2
            if (seedinfo.Uid == btuserinfo.Uid && seedinfo.UploadRatio >= 1.0f && seedinfo.UploadRatio < 2.0) seedinfo.UploadRatio = 2.0f;

            //全局上传下载系数是否生效，如果生效，则取代种子中的上传下载系数
            if (btconfig.UploadMulti >= 1.0f && DateTime.Now > btconfig.UpMultiBeginTime && DateTime.Now < btconfig.UpMultiEndTime)
            {
                if (btconfig.UploadMulti > seedinfo.UploadRatio)
                {
                    seedinfo.UploadRatio = btconfig.UploadMulti;
                }
            }
            if (btconfig.DownloadMulti <= 1.0f && DateTime.Now > btconfig.DownMultiBeginTime && DateTime.Now < btconfig.DownMultiEndTime)
            {

                if (btconfig.DownloadMulti < seedinfo.DownloadRatio)
                {
                    seedinfo.DownloadRatio = btconfig.DownloadMulti;
                }
            }


            //共享率保护
            ShortUserInfo userInfo = Users.GetShortUserInfo(userid);
            if (PTTools.GetRatioAlert(userInfo.Extcredits3, userInfo.Extcredits4 + seedinfo.FileSize) != "" && (userInfo.RatioProtection & 1) > 0 && seedinfo.Uid != userid && seedinfo.DownloadRatio != 0)
            {
                AddErrLine("共享率保护：您的上传量不足，现在无法下载此种子。请先下载蓝种，保种上传获取足够上传量之后再下载非蓝种");
                return;
            }

            //共享率低跳转提示
            //if (DNTRequest.GetString("force") != "true" && btuserinfo.VIP < 1 && PTTools.Ratio2Level(userid, btuserinfo.Ratio, btuserinfo.Extcredits3, btuserinfo.Extcredits4) < 0)
            //{
            //    HttpContext.Current.Response.Redirect("ratioalert.aspx?seedid=" + seedid.ToString());
            //    return;
            //}

            //判断当前用户是否可以下载种子（下载量>50G且分享率小于0.1）
            //if (btuserinfo.VIP < 1 && btuserinfo.Extcredits4 > 51200 && btuserinfo.Ratio < 0.1)
            //{
            //    AddErrLine("对不起，您的分享率过低，已经不能下载种子");
            //    return;
            //}

            //添加判断特殊用户的代码
            if (!Forums.AllowViewByUserId(forum.Permuserlist, userid))
            {
                if (!Forums.AllowView(forum, usergroupid, ipaddress))
                {
                    AddErrLine("您没有下载该种子的权限");
                    if (userid < 1)
                    {
                        needlogin = true;
                    }
                    return;
                }
            }

            //添加判断特殊用户的代码
            if (!Forums.AllowGetAttachByUserID(forum.Permuserlist, userid))
            {
                if (string.IsNullOrEmpty(forum.Getattachperm))
                {
                    // 验证用户是否有下载附件的权限
                    if (usergroupinfo.Allowgetattach != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有下载种子的权限");
                        if (userid < 1)
                        {
                            needlogin = true;
                        }
                        return;
                    }
                }
                else
                {
                    if (!Forums.AllowGetAttach(forum.Getattachperm, usergroupid))
                    {
                        AddErrLine("您没有在该版块下载种子的权限");
                        if (userid < 1)
                        {
                            needlogin = true;
                        }
                        return;
                    }
                }
            }
            
            //检查附件是否存在
            if (System.IO.File.Exists(seedinfo.Path))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(seedinfo.Path, FileMode.Open);
                }
                catch
                {
                    System.Threading.Thread.Sleep(666);
                    try
                    {
                        fs = new FileStream(seedinfo.Path, FileMode.Open);
                    }
                    catch
                    {
                        HttpContext.Current.Response.Write("服务器内部错误，请重试");
                        if (fs != null)
                        {
                            // 关闭文件
                            fs.Close();
                            fs.Dispose();
                        }
                        return;
                    }
                }
                
                try
                {
                    if (fs.Length > 2147483648)
                    {
                        AddErrLine("服务器内部错误：种子文件过大，无法下载");
                        return;
                    }
                    byte[] buffer = new Byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    fs.Close();
                    string seedhead = "";

                    string tracker1 = "";
                    string tracker2 = "";

                    //IPv4校内访问，使用
                    if (!isipv6)
                    {
                        //测试系统
                        if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf("dnt3") > -1)
                        {
                            tracker1 = "http://dnt3.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                            tracker2 = "http://dnt3v6.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                        }
                        //测试系统
                        else if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf("localhost") > -1)
                        {
                            tracker1 = "http://localhost/announce.aspx?passkey=" + downuserinfo.Passkey;
                        }
                        //实际运行系统
                        else
                        {
                            tracker1 = "http://tracker4.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                            tracker2 = "http://tracker6.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                        }
                    }
                    //IPv6访问
                    else
                    {
                        //测试系统
                        if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf("dnt3") > -1)
                        {
                            tracker1 = "http://dnt3.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                            tracker2 = "http://dnt3v6.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                        }
                        //测试系统
                        else if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf("localhost") > -1)
                        {
                            tracker1 = "http://localhost/announce.aspx?passkey=" + downuserinfo.Passkey;
                        }
                        //校内IPv6地址
                        else if (ipaddress.Length > 12 && (ipaddress.Substring(0, 12) == "2001:da8:203" || ipaddress.Substring(0, 12) == "2001:da8:ae"))
                        {
                            tracker1 = "http://tracker4.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                            tracker2 = "http://tracker6.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                        }
                        //校外IPv6地址
                        else
                        {
                            tracker1 = "http://tracker6.buaabt.cn/announce.aspx?passkey=" + downuserinfo.Passkey;
                            //tracker2 = string.Format("http://{0}/announce.aspx?passkey={1}", HttpContext.Current.Request.Url.Host, downuserinfo.Passkey);
                        }
                    }

                    //添加主Tracker
                    seedhead = string.Format("d8:announce{0}:{1}", tracker1.Length, tracker1);

                    //包括两个Tracker的时候
                    if (tracker2 != "")
                    {
                        seedhead += string.Format("13:announce-listll{0}:{1}el{2}:{3}ee", tracker1.Length, tracker1, tracker2.Length, tracker2);
                    }

                    byte[] output = new byte[buffer.Length + seedhead.Length];
                    Array.Copy(System.Text.Encoding.UTF8.GetBytes(seedhead), 0, output, 0, seedhead.Length);
                    Array.Copy(buffer, 0, output, seedhead.Length, buffer.Length);

                    HttpContext.Current.Response.Charset = "utf-8";
                    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    HttpContext.Current.Response.ContentType = "application/x-bittorrent";

                    if (seedinfo.FileName.Trim() == "")
                    {
                        if (HttpContext.Current.Request.Browser.Browser.IndexOf("IE") > -1)
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + HttpUtility.UrlEncode(seedinfo.TopicTitle.Trim()) + ".torrent\"");//HttpUtility.UrlEncode
                        else
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + seedinfo.TopicTitle.Trim() + ".torrent\"");//HttpUtility.UrlEncode
                    }
                    else
                    {
                        if(HttpContext.Current.Request.Browser.Browser.IndexOf("IE") > -1)
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + HttpUtility.UrlEncode(seedinfo.FileName.Trim()) + ".torrent\"");//HttpUtility.UrlEncode
                        else
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + seedinfo.FileName.Trim() + ".torrent\"");//HttpUtility.UrlEncode
                    }
                    
                    
                    HttpContext.Current.Response.OutputStream.Write(output, 0, output.Length);


                    //【BT】试图解决The remote host closed the connection. The error code is 0x800704CD.
                    PageBase_ResponseFlush();
                    //try
                    //{
                    //    HttpContext.Current.Response.Flush();
                    //}
                    //catch (System.Exception ex)
                    //{
                    //    string errormsg = ex.ToString();
                    //    //PageBase_ResponseEnd();
                    //}
                    buffer = null;
                    output = null;

                    //下载种子的时候就插入流量记录
                    PrivateBT.InsertPerUserSeedTraffic(seedinfo.SeedId, btuserinfo.Uid, 0M, 0M, isipv6 ? "" : ipaddress, isipv6 ? ipaddress : "");
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("服务器内部错误，请重试" + ex.Message);
                }
                finally
                {
                    if (fs != null)
                    {
                        // 关闭文件
                        fs.Close();
                        fs.Dispose();
                    }
                    
                }

                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                
                //添加流量记录
                //PrivateBT.InsertPerUserSeedTraffic(seedid, downuserinfo.Uid, 0M, 0M, "", "");

                PageBase_ResponseEnd();
            }
            else
            {
                AddErrLine("服务器内部错误：该种子文件不存在或已被删除");
                return;
            }
        }


        private void SendAbtSeed(int aid)
        {
            if (aid < 0)
            {
                AddErrLine("不存在的种子ID");
                return;
            }

            if (!PTAbt.IsIPAllowed(ipaddress))
            {
                AddErrLine("您无权访问此板块");
                return;
            }
            // 获取该种子的信息
            AbtSeedInfo abtseed = PTAbt.AbtGetSeedInfo(aid);
            // 如果该附件不存在
            if (abtseed.Aid < 1)
            {
                AddErrLine("不存在的种子ID");
                return;
            }
            
            ShortUserInfo userInfo = Users.GetShortUserInfo(userid);
            
            #region 流量检查，用于下载种子扣流量，已取消
            
            //if (userid != abtseed.Uid && (userInfo.Extcredits3 - (abtseed.FileSize > 1024 * 1024 * 1024M ? (abtseed.FileSize) : (1024 * 1024 * 1024M)) < userInfo.Extcredits4))
            //{
            //    AddErrLine("您的上传量不足，无法支付下载种子所需上传。 请先下载蓝种，保种上传获取足够上传量之后再下载");
            //    return;
            //}

            #endregion

            //检查权限
            topicid = DNTRequest.GetInt("topicid", -1);
            TopicInfo topic = Topics.GetTopicInfo(topicid);
            if (topicid < 0 || topic == null || topic.SeedId != -abtseed.Aid || topic.Displayorder < 0)
            {
                AddErrLine("无效的主题ID");
                return;
            }
            forumid = topic.Fid;
            ForumInfo forum = Forums.GetForumInfo(forumid);
            if (forum == null)
            {
                AddErrLine("不存在的版块ID"); 
                return;
            }

            //增加IP限制
            if (!Forums.AllowView(forum, usergroupid, ipaddress))
            {
                AddErrLine("您无权访问此板块");
                return;
            }

            if (topic.Readperm > usergroupinfo.Readaccess && topic.Posterid != userid && useradminid != 1 && !Moderators.IsModer(useradminid, userid, forum.Fid))
            {
                AddErrLine(string.Format("本主题阅读权限为: {0}, 您当前的身份 \"{1}\" 阅读权限不够", topic.Readperm, usergroupinfo.Grouptitle));
                return;
            }





            string passkey = PTTools.GetRandomString(32, true);
            int status = (userid == abtseed.Uid) ? 2 : 0;


            if (abtseed.Upload < 1 && userid != abtseed.Uid)
            {
                if (DNTRequest.GetString("confirm") != "1")
                {
                    AddErrLine("这个种子现在没有人做种！你确定要下载这个文件分享种子并向发布者支付1金币吗？<br />如果确定需要下载，请点击下方链接！<br /><br /><a href=\"downloadseed.aspx?abtseed=1&seedid=" + seedid + "&topicid=" + topicid + "&confirm=1\">点此下载文件分享种子:  " + topic.Title + "</a>");
                    return;
                }
            }

            //如果之前下载过
            AbtDownloadInfo olddowninfo = PTAbt.AbtGetDownload(aid, userid);
            if (olddowninfo.Aid > 0)
            {
                if (DNTRequest.GetString("redownload") != "1" && userid != abtseed.Uid)
                {
                    if (olddowninfo.Status == 0)
                    {
                        AddErrLine("您之前已经下载过这个文件分享种子但并未开始下载！<br/>重新下载文件分享种子将使得之前下载的种子失效！<br />如果确定需要重新下载，请点击下方链接！<br /><br /><a href=\"downloadseed.aspx?abtseed=1&seedid=" + seedid + "&topicid=" + topicid + (DNTRequest.GetString("confirm") == "1" ? "&confirm=1" : "") + "&redownload=1\">点此重新下载文件分享种子:  " + topic.Title + "</a>");
                        return;
                    }
                    else
                    {
                        AddErrLine("您之前已经下载过这个文件分享种子并开始下载！<br />重新下载文件分享种子将重复支付1金币，并使得之前下载的种子失效！<br />如果确定需要重新下载，请点击下方链接！<br /><br /><a href=\"downloadseed.aspx?abtseed=1&seedid=" + seedid + "&topicid=" + topicid + (DNTRequest.GetString("confirm") == "1" ? "&confirm=1" : "") + "&redownload=1\">点此重新下载文件分享种子:  " + topic.Title + "</a>");
                        return;
                    }

                }
                else
                {
                    //删除之前的下载记录
                    if (PTAbt.AbtDeleteDownload(aid, userid) > 0)
                    {
                        PTAbt.AbtInsertLog(aid, userid, 101, "下载种子：删除原有下载记录");
                    }
                    //扣除1金币
                    //向发布者支付1金币
                    if (userid != abtseed.Uid && olddowninfo.Status != 0)
                    {
                        if (userInfo.Extcredits2 > 0)
                        {
                            Users.UpdateUserExtCredits(userInfo.Uid, 2, -1);
                            CreditsLogs.AddCreditsLog(userInfo.Uid, userInfo.Uid, 2, 2, 1, 0, Utils.GetDateTime(), 18);
                        }
                        else
                        {
                            PTAbt.AbtInsertLog(aid, userid, 111, "金币不足:" + userInfo.Extcredits2.ToString());
                            AddErrLine("您的金币不足： 需要支付 1 金币，您仅有 " + userInfo.Extcredits2.ToString() + " 金币。 <br/>如何获取金币，请查看 <a href=\"showtopic-83062.aspx\">关于金币项目</a>");
                            return;
                        }
                    }
                }
            }
            else
            {
                //向发布者支付1金币
                if (userid != abtseed.Uid)
                {
                    if (userInfo.Extcredits2 > 0)
                    {
                        Users.UpdateUserExtCredits(userInfo.Uid, 2, -1);
                        Users.UpdateUserExtCredits(abtseed.Uid, 2, 1);
                        //CreditsLogs.AddCreditsLog(userInfo.Uid, abtseed.Uid, 2, 2, 1, 0, Utils.GetDateTime(), 18);
                        //CreditsLogs.AddCreditsLog(abtseed.Uid, userInfo.Uid, 2, 2, 0, 1, Utils.GetDateTime(), 18);
                    }
                    else
                    {
                        PTAbt.AbtInsertLog(aid, userid, 111, "金币不足:" + userInfo.Extcredits2.ToString());
                        AddErrLine("您的金币不足： 需要支付 1 金币，您仅有 " + userInfo.Extcredits2.ToString() + " 金币。 <br/>如何获取金币，请查看 <a href=\"showtopic-83062.aspx\">关于金币项目</a>");
                        return;
                    }
                }
            }

            int rtv = PTAbt.AbtInsertDownload(aid, userid, passkey, abtseed.InfoHash, status);
            if (rtv < 0)
            {
                PTAbt.AbtInsertLog(aid, userid, 102, "下载种子：添加记录失败-1");
                AddErrLine("请勿重复下载种子");
                return;
            }
            else if(rtv == 0)
            {
                PTAbt.AbtInsertLog(aid, userid, 102, "下载种子：添加记录失败");
                AddErrLine("获取种子信息出错");
                return;
            }

            string path = "D:\\torrentabt\\" + (abtseed.Aid / 10000).ToString("000") + "\\" + ((abtseed.Aid % 10000) / 100).ToString("00") + "\\" + abtseed.Aid.ToString() + ".torrent";

            //检查种子是否存在
            if (System.IO.File.Exists(path))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(path, FileMode.Open);
                }
                catch
                {
                    System.Threading.Thread.Sleep(666);
                    try
                    {
                        fs = new FileStream(path, FileMode.Open);
                    }
                    catch
                    {
                        HttpContext.Current.Response.Write("服务器内部错误，请重试");
                        PTAbt.AbtInsertLog(aid, userid, 103, "下载种子：文件读取出错1");
                        if (fs != null)
                        {
                            // 关闭文件
                            fs.Close();
                            fs.Dispose();
                        }
                        return;
                    }
                }

                try
                {
                    if (fs.Length > 2147483648)
                    {
                        PTAbt.AbtInsertLog(aid, userid, 104, "下载种子：文件读取出错2");
                        AddErrLine("服务器内部错误：种子文件过大，无法下载");
                        return;
                    }
                    byte[] buffer = new Byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    fs.Close();
                    string seedhead = "";

                    string tracker1 = "";
                    string tracker2 = "";

                    //IPv4校内访问，使用
                    if (!isipv6)
                    {
                        tracker1 = "http://tracker4.buaabt.cn/announce.aspx?abt=1&passkey=" + passkey + "&uid=" + userid + "&aid=" + aid;
                        tracker2 = "http://tracker6.buaabt.cn/announce.aspx?abt=2&passkey=" + passkey + "&uid=" + userid + "&aid=" + aid;
                    }
                    //IPv6访问
                    else
                    {
                        if (ipaddress.Length > 12 && (ipaddress.Substring(0, 12) == "2001:da8:203" || ipaddress.Substring(0, 12) == "2001:da8:ae"))
                        {
                            tracker1 = "http://tracker4.buaabt.cn/announce.aspx?abt=1&passkey=" + passkey + "&uid=" + userid + "&aid=" + aid;
                            tracker2 = "http://tracker6.buaabt.cn/announce.aspx?abt=2&passkey=" + passkey + "&uid=" + userid + "&aid=" + aid;
                        }
                        //校外IPv6地址
                        else
                        {
                            //AddErrLine("文件不存在！");
                            tracker1 = "http://tracker4.buaabt.cn/announce.aspx?abt=1&passkey=" + passkey + "&uid=" + userid + "&aid=" + aid;
                            tracker2 = "http://tracker6.buaabt.cn/announce.aspx?abt=2&passkey=" + passkey + "&uid=" + userid + "&aid=" + aid;
                        }
                    }

                    //添加主Tracker
                    seedhead = string.Format("d8:announce{0}:{1}", tracker1.Length, tracker1);

                    //包括两个Tracker的时候
                    if (tracker2 != "")
                    {
                        seedhead += string.Format("13:announce-listll{0}:{1}el{2}:{3}ee", tracker1.Length, tracker1, tracker2.Length, tracker2);
                    }

                    byte[] output = new byte[buffer.Length + seedhead.Length];
                    Array.Copy(System.Text.Encoding.UTF8.GetBytes(seedhead), 0, output, 0, seedhead.Length);
                    Array.Copy(buffer, 0, output, seedhead.Length, buffer.Length);

                    HttpContext.Current.Response.Charset = "utf-8";
                    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    HttpContext.Current.Response.ContentType = "application/x-bittorrent";

                    if (abtseed.FileName.Trim() == "")
                    {
                        if (HttpContext.Current.Request.Browser.Browser.IndexOf("IE") > -1)
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + HttpUtility.UrlEncode(abtseed.Aid.ToString()) + ".torrent\"");//HttpUtility.UrlEncode
                        else
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + abtseed.Aid.ToString() + ".torrent\"");//HttpUtility.UrlEncode
                    }
                    else
                    {
                        if (HttpContext.Current.Request.Browser.Browser.IndexOf("IE") > -1)
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + HttpUtility.UrlEncode(abtseed.Aid.ToString()) + ".torrent\"");//HttpUtility.UrlEncode
                        else
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + abtseed.Aid.ToString() + ".torrent\"");//HttpUtility.UrlEncode
                    }


                    HttpContext.Current.Response.OutputStream.Write(output, 0, output.Length);


                    //【BT】试图解决The remote host closed the connection. The error code is 0x800704CD.
                    PageBase_ResponseFlush();
                    //try
                    //{
                    //    HttpContext.Current.Response.Flush();
                    //}
                    //catch (System.Exception ex)
                    //{
                    //    string errormsg = ex.ToString();
                    //    //PageBase_ResponseEnd();
                    //}
                    buffer = null;
                    output = null;

                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("服务器内部错误，请重试" + ex.Message);
                    PTAbt.AbtInsertLog(aid, userid, 105, "下载种子：文件读取出错3:" + ex.ToString());
                    return;
                }
                finally
                {
                    if (fs != null)
                    {
                        // 关闭文件
                        fs.Close();
                        fs.Dispose();
                    }

                }

                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

                PageBase_ResponseEnd();
            }
            else
            {
                AddErrLine("服务器内部错误：该种子文件不存在或已被删除");
                PTAbt.AbtInsertLog(aid, userid, 106, "下载种子：文件读取出错4");
                return;
            }


            //扣除上传，添加记录
            //if (abtseed.Uid != userid)
            //{
            //    float extcredit3paynum = -(float)Math.Round(abtseed.FileSize > 1024 * 1024 * 1024M ? (abtseed.FileSize) : (1024 * 1024 * 1024M));
            //    Users.UpdateUserExtCredits(userid, 3, extcredit3paynum);
            //    UserCredits.UpdateUserCredits(userid);
            //    CreditsLogs.AddCreditsLog(userid, userid, 3, 3, -extcredit3paynum, 0, Utils.GetDateTime(), 15);
            //}
            PTAbt.AbtInsertLog(aid, userid, 107, "下载种子：OK");
        }
    }
}
