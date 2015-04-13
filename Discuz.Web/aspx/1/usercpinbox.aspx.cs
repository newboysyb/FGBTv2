using System;
using System.Data;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
    /// <summary>
    /// 用户收件箱页面
    /// </summary>
    public class usercpinbox : UserCpPage
    {
        /// <summary>
        /// 是否自动标记为已读
        /// </summary>
        public int automarkread = 0;
        /// <summary>
        /// 当前要进行的操作，#空# 为删除，mark为标记已读，unmark为标记未读，now全部标记已读，on开启自动标记已读，off关闭自动标记已读
        /// </summary>
        protected string opreate = DNTRequest.GetFormString("markauto");
        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist = new List<PrivateMessageInfo>();

        protected override void ShowPage()
        {
            //此页面立即过期。。不起作用
            System.Web.HttpContext.Current.Response.CacheControl = "no-cache";

            pagetitle = "短消息收件箱";
            
            if (!IsLogin()) return;

            automarkread = (btuserinfo.RatioProtection) & (1 << 16);

            if (DNTRequest.IsPost())
            {
                string[] pmitemid = null;

                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                if (opreate != "now" && opreate != "on" && opreate != "off")
                {
                    if (Utils.StrIsNullOrEmpty(DNTRequest.GetFormString("pmitemid")))
                    {
                        AddErrLine("您未选中任何短消息，当前操作失败！");
                        return;
                    }
                    if (!Utils.IsNumericList(DNTRequest.GetFormString("pmitemid")))
                    {
                        AddErrLine("参数信息错误！请重新选择短消息");
                        return;
                    }

                    pmitemid = Utils.SplitString(DNTRequest.GetFormString("pmitemid"), ",");

                    if (!Utils.IsNumericArray(pmitemid))
                    {
                        AddErrLine("参数无效，请重新选择短消息");
                        return;
                    }

                }

                //检测是否是标记已读操作
                if (DNTRequest.GetFormString("markauto") == "mark")
                {
                    //标记的短消息设置为已读
                    if (PTPrivateMessage.SetPrivateMessagesState(userid, pmitemid, 0) > 0)
                    {
                        AddMsgLine("标记已读完毕");
                    }
                    else
                    {
                        AddErrLine("参数无效，请重试");
                        return;
                    }
                }
                else if (DNTRequest.GetFormString("markauto") == "unmark")
                {
                    //标记的短消息设置为未读
                    if (PTPrivateMessage.SetPrivateMessagesState(userid, pmitemid, 1) > 0)
                    {
                        AddMsgLine("标记未读完毕");
                    }
                    else
                    {
                        AddErrLine("参数无效，请重试");
                        return;
                    }
                }
                else if (DNTRequest.GetFormString("markauto") == "now")
                {
                    //全部短消息设置为已读
                    PTPrivateMessage.SetPrivateMessagesStateAll(userid, 0);
                }
                else if (DNTRequest.GetFormString("markauto") == "on")
                {
                    //设置为自动标记全部已读
                    btuserinfo.RatioProtection = btuserinfo.RatioProtection | (1 << 16);
                    PTUsers.SetUserRatioProtection(btuserinfo.Uid, btuserinfo.RatioProtection);
                    AddMsgLine("已经设置为 在访问收件箱时自动标记所有未读短消息为已读");
                }
                else if (DNTRequest.GetFormString("markauto") == "off")
                {
                    //取消自动标记全部已读
                    btuserinfo.RatioProtection = btuserinfo.RatioProtection & ((1 << 30) - (1 << 16) - 1);
                    PTUsers.SetUserRatioProtection(btuserinfo.Uid, btuserinfo.RatioProtection);
                    AddMsgLine("已经取消 在访问收件箱时自动标记所有未读短消息为已读");
                }
                else
                {
                    if (PrivateMessages.DeletePrivateMessage(userid, pmitemid, ipaddress) == -1)
                    {
                        AddErrLine("参数无效");
                        return;
                    }
                    AddMsgLine("删除完毕");
                }

                Users.UpdateUserNewPMCount(userid, olid);

                SetUrl("usercpinbox.aspx");
                SetMetaRefresh();
                SetShowBackLink(true);

            }
            else
            {
                BindPrivateMessage(0);
                pmlist = PrivateMessages.GetPrivateMessageCollection(userid, 0, 100, pageid, 2);

                if (automarkread > 0)
                {
                    PTPrivateMessage.SetPrivateMessagesStateAll(userid, 0);
                }

                Users.UpdateUserNewPMCount(userid, olid);
            }

            

        }
    }
}