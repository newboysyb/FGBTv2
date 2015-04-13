using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

using System.Text.RegularExpressions;

namespace Discuz.Web
{
    /// <summary>
    /// 用户通知
    /// </summary>
    public class usercpnotice : UserCpPage
    {
        /// <summary>
        /// 通知列表
        /// </summary>
        public NoticeinfoCollection noticeinfolist = new NoticeinfoCollection();
        /// <summary>
        /// 消息过滤参数
        /// </summary>
        public string filter = DNTRequest.GetString("filter", true).ToLower();
        /// <summary>
        /// 记录总数
        /// </summary>
        public int reccount = 0;
        /// <summary>
        /// 是否自动标记为已读
        /// </summary>
        public int automarkread = 0;
        /// <summary>
        /// 当前要进行的操作，#空# 为删除，mark为标记已读，unmark为标记未读，now全部标记已读，on开启自动标记已读，off关闭自动标记已读
        /// </summary>
        protected string opreate = DNTRequest.GetFormString("markauto");

        protected override void ShowPage()
        {
            //此页面立即过期。。不起作用
            System.Web.HttpContext.Current.Response.CacheControl = "no-cache";

            pagetitle = "用户控制面板";

            if (!IsLogin()) return;

            automarkread = (btuserinfo.RatioProtection) & (1 << 17);

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】标记已读等操作

            if (DNTRequest.IsPost())
            {
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                if (opreate != "")
                {
                    if (opreate == "markmotice")
                    {
                        int marknid = DNTRequest.GetInt("noticeid", -1);
                        if (marknid > 0)
                        {
                            PrivateBT.UpdateNoticeNewByUidNid(userid, marknid, 0);
                            //newnoticecount = Notices.GetNewNoticeCountByUid(userid);
                            AddMsgLine("标记已读完毕");
                        }
                        else
                        {
                            AddErrLine("参数信息错误！");
                        }
                    }
                    else if (opreate == "now")
                    {
                        PrivateBT.UpdateNoticeNewByUidType(userid, filter, 0);
                        //newnoticecount = Notices.GetNewNoticeCountByUid(userid);
                        AddMsgLine("标记已读完毕");
                    }
                    else if (opreate == "on")
                    {
                        //设置为自动标记全部已读
                        btuserinfo.RatioProtection = btuserinfo.RatioProtection | (1 << 17);
                        PTUsers.SetUserRatioProtection(btuserinfo.Uid, btuserinfo.RatioProtection);
                        AddMsgLine("已经设置为 在访问通知页面时自动标记所有未读通知为已读");
                    }
                    else if (opreate == "off")
                    {
                        //取消自动标记全部已读
                        btuserinfo.RatioProtection = btuserinfo.RatioProtection & ((1 << 30) - (1 << 17) - 1);
                        PTUsers.SetUserRatioProtection(btuserinfo.Uid, btuserinfo.RatioProtection);
                        AddMsgLine("已经取消 在访问通知页面时自动标记所有未读通知为已读");
                    }
                    else
                    {
                        AddErrLine("错误的页面访问");
                    }
                }
                else
                {
                    AddErrLine("错误的页面访问");
                }
                OnlineUsers.UpdateNewNotices(olid);

                SetUrl("usercpnotice.aspx");
                SetMetaRefresh();
                SetShowBackLink(true);

            }
            else if (DNTRequest.GetInt("nid", -1) > 0)
            {
                int nid = DNTRequest.GetInt("nid", -1);
                string url = DNTRequest.GetString("url");

                if (url != "")
                {
                    NoticeInfo ninfo = Notices.GetNoticeInfo(nid);

                    //正常的跳转
                    if (ninfo.Uid == userid && ninfo.Note.IndexOf(url) > -1)
                    {
                        try
                        {
                            PrivateBT.UpdateNoticeNewByUidNid(userid, nid, 0);
                            OnlineUsers.UpdateNewNotices(olid);

                            AddMsgLine("正在为您跳转");
                            SetUrl(url);
                            SetMetaRefresh();
                            SetShowBackLink(true);

                            System.Web.HttpContext.Current.Response.Redirect(url);


                        }
                        catch (System.Exception ex)
                        {
                            PTLog.InsertSystemLog(PTLog.LogType.NoticeTransfer, PTLog.LogStatus.Exception, "Redirect", ex.ToString());
                        }
                    }
                    else
                    {
                        //nid和用户不匹配的情况
                        PTLog.InsertSystemLog(PTLog.LogType.NoticeTransfer, PTLog.LogStatus.Error, "NoticeTran Unmatch",
                            string.Format("UID:{0} --NID:{1} --URL:{2} --NOTICE:{3}", userid, nid, url, ninfo.Note));

                        AddErrLine("错误的链接地址");
                        return;
                    }
                    
                }

                
            }
            else
            {
                if (filter == "spacecomment" && config.Enablespace == 0)
                {
                    AddErrLine("系统未开启" + config.Spacename + "服务, 当前页面暂时无法访问!");
                    return;
                }
                if (filter == "albumcomment" && config.Enablealbum == 0)
                {
                    AddErrLine("系统未开启" + config.Albumname + "服务, 当前页面暂时无法访问!");
                    return;
                }
                if ((filter == "goodstrade" || filter == "goodsleaveword") && config.Enablemall == 0)
                {
                    AddErrLine("系统未开启交易服务, 当前页面暂时无法访问!");
                    return;
                }

                NoticeType noticetype = Notices.GetNoticetype(filter);
                reccount = Notices.GetNoticeCountByUid(userid, noticetype);

                BindItems(reccount, "usercpnotice.aspx?filter=" + filter);

                noticeinfolist = Notices.GetNoticeinfoCollectionByUid(userid, noticetype, pageid, 100);
                foreach (NoticeInfo ninfo in noticeinfolist)
                {
                    if (ninfo.New == 1)
                    {
                        Regex reg = new Regex(@"(?<allurl><a\s+(?<style1>.*?)href\s*?=\s*?\""(?<url>.*?)\""(?<style1>.*?)>)");
                        foreach (Match match in reg.Matches(ninfo.Note))
                        {
                            string orignialstr = match.Groups["allurl"].Value;
                            string style = match.Groups["style1"].Value.Trim();
                            if (style == "" && match.Groups["style2"].Value.Trim() != "") style = match.Groups["style2"].Value.Trim();

                            string newstr = string.Format("<a href=\"usercpnotice.aspx?nid={0}&url={1}\"{2}>", ninfo.Nid, Uri.EscapeDataString(match.Groups["url"].Value), style);
                            ninfo.Note = ninfo.Note.Replace(orignialstr, newstr);
                        }
                    }
                }

                newnoticecount = Notices.GetNewNoticeCountByUid(userid);

                if (automarkread > 0)
                {
                    PrivateBT.UpdateNoticeNewByUidType(userid, "", 0);
                }
                //【BT修改】Notices.UpdateNoticeNewByUid(userid, 0);
                OnlineUsers.UpdateNewNotices(olid);
            }
           
        }
    }
}
