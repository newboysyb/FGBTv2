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
    public class downloadseedrssd : System.Web.UI.Page, System.Web.SessionState.IRequiresSessionState
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
        /// 种子Id
        /// </summary>
        public int seedid;
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
        /// <summary>
        /// 种子信息
        /// </summary>
        public PTSeedinfo seedinfo;


        #endregion 变量声明

        protected void Page_Load(object sender, EventArgs e)
        {

            bool rssError = false;
            string passkey = DNTRequest.GetString("passkey").Trim().ToUpper();
            int rssuid = DNTRequest.GetInt("uid", -1);
            if (passkey.Length != 32) rssError = true;



            //检验用户
            if (rssuid < 1) rssError = true;

            //防止暴力猜测uid
            int errcount = Discuz.Forum.LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), false);
            if (errcount > 5) { SendText("IP_TEMPORARYLY_BANNED"); return; }
            

            PTUserInfo downuserinfo = PTUsers.GetBtUserInfo(rssuid);
            if (downuserinfo == null) rssError = true;
            else
            {
                if (downuserinfo.Passkey != passkey)
                {
                    rssError = true;

                    errcount = Discuz.Forum.LoginLogs.UpdateLoginLog(DNTRequest.GetIP(), true);
                    if (errcount > 5) { SendText("IP_TEMPORARYLY_BANNED"); return; }
                }
            }


            // 获取种子ID
            seedid = DNTRequest.GetInt("seedid", -1);
            // 如果种子ID非数字
            if (seedid == -1) rssError = true;

            if (rssError) { SendText("FGBTRSS_ERROR"); return; }

            // 获取该种子的信息
            seedinfo = PTSeeds.GetSeedInfo(seedid);
            if (DNTRequest.GetInt("isexist", -1) == 9)
            {
                if (seedinfo.SeedId < 1) { SendText("DELETED!"); return; }
                else  { SendText("OK!"); return; }
            }
            // 如果该附件不存在
            if (seedinfo.SeedId < 1) rssError = true;
            if (rssError) { SendText("FGBTRSS_ERROR"); return; }

            int userid = downuserinfo.Uid;
            int usergroupid = downuserinfo.Groupid;
            UserGroupInfo usergroupinfo = UserGroups.GetUserGroupInfo(usergroupid);

            topicid = seedinfo.TopicId;
            // 获取该主题的信息
            topic = Topics.GetTopicInfo(topicid);
            // 如果该主题不存在
            if (topic == null) rssError = true;


            topictitle = topic.Title;
            forumid = topic.Fid;
            ForumInfo forum = Forums.GetForumInfo(forumid);
            forumname = forum.Name;

            //判断帖子是否被屏蔽
            int postid = Posts.GetFirstPostId(topic.Tid);
            PostInfo seedpost = Posts.GetPostInfo(topic.Tid, postid);
            if (seedpost.Invisible != 0) { SendText("FGBTRSS_ERROR：SEED NOT AVAIABLE"); return; }

            //共享率保护
            ShortUserInfo userInfo = Users.GetShortUserInfo(userid);
            if (PTTools.GetRatioAlert(userInfo.Extcredits3, userInfo.Extcredits4 + seedinfo.FileSize) != "" && (userInfo.RatioProtection & 1) > 0 && seedinfo.Uid != userid && seedinfo.DownloadRatio != 0)
            { SendText("FGBTRSS_ERROR：RATIO PROTECTION LIMITED"); return; }

            //共享率低跳转提示
            //if (DNTRequest.GetString("force") != "true" && btuserinfo.VIP < 1 && PTTools.Ratio2Level(userid, btuserinfo.Ratio, btuserinfo.Extcredits3, btuserinfo.Extcredits4) < 0)
            //{
            //    HttpContext.Current.Response.Redirect("ratioalert.aspx?seedid=" + seedid.ToString());
            //    return;
            //}
            
            //判断当前用户是否可以下载种子（下载量>50G且分享率小于0.1）
            //if (downuserinfo.Vip < 1 && downuserinfo.Extcredits4 > 51200 && downuserinfo.Ratio < 0.1)
            //{
            //    rssError = true;
            //}



            //添加判断特殊用户的代码
            if (!Forums.AllowViewByUserId(forum.Permuserlist, userid))
            {
                if (!Forums.AllowView(forum.Viewperm, usergroupid))
                {
                    rssError = true;
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
                        rssError = true;
                    }
                }
                else
                {
                    if (!Forums.AllowGetAttach(forum.Getattachperm, usergroupid))
                    {
                        rssError = true;
                    }
                }
            }

            if (rssError) { SendText("FGBTRSS_ERROR"); return; }


            string ipaddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            bool isipv6 = PTTools.IsIPv6(ipaddress);

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
                        if (fs != null)
                        {
                            // 关闭文件
                            fs.Close();
                            fs.Dispose();
                        }
                        SendText("错误，请重试");
                        return;
                    }
                }
                try
                {
                    if (fs.Length > 2147483648) { SendText("FGBTRSS_ERROR"); return; }

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
                        if (HttpContext.Current.Request.Browser.Browser.IndexOf("IE") > -1)
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + HttpUtility.UrlEncode(seedinfo.FileName.Trim()) + ".torrent\"");//HttpUtility.UrlEncode
                        else
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "seedfile;filename=\"[FGBT]." + seedinfo.FileName.Trim() + ".torrent\"");//HttpUtility.UrlEncode
                    }


                    HttpContext.Current.Response.OutputStream.Write(output, 0, output.Length);

                    buffer = null;
                    output = null;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("错误，请重试");
                    PTLog.InsertSystemLog(PTLog.LogType.RssDSeedSendTorrent, PTLog.LogStatus.Exception, "SendTorrent", string.Format("{IP:{0} -URL:{1} -EX:{2}}", Request.UserHostAddress, Request.RawUrl, ex));
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

                try
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }

                    //下载种子的时候就插入流量记录
                    PrivateBT.InsertPerUserSeedTraffic(seedinfo.SeedId, downuserinfo.Uid, 0M, 0M, isipv6 ? "" : ipaddress, isipv6 ? ipaddress : "");

                    Response.Flush();
                    Response.End();
                }
                catch (System.Exception ex)
                {
                    PTLog.InsertSystemLog(PTLog.LogType.RssDSeedSendTorrent, PTLog.LogStatus.Detail, "SendTorrentEnd", string.Format("{IP:{0} -URL:{1} -EX:{2}}", Request.UserHostAddress, Request.RawUrl, ex));
                }
            }
            else { SendText("FGBTRSS_ERROR"); return; }
        }
        protected void SendText(string message)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(message);
                Response.Flush();
                Response.End();
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.RssDSeedSendText, PTLog.LogStatus.Detail, "SendText", string.Format("{IP:{0} -MESSAGE:{1} -URL:{2} -EX:{3}}", Request.UserHostAddress, message, Request.RawUrl, ex));
            }
        }
    }
}
