using System;
using System.Data;
using System.Web;

using Discuz.Common;
using Discuz.Entity;
using Discuz.Forum;
using Discuz.Plugin.Payment;
using Discuz.Plugin.Payment.Alipay;
using Discuz.Web.UI;
using Discuz.Plugin.PasswordMode;

namespace Discuz.Web
{
    /// <summary>
    /// BT设置-共享率保护
    /// </summary>
    public class usercpratioprotect : UserCpPage
    {
        /// <summary>
        /// 共享率保护
        /// </summary>
        public int ratioprotect = 0;
        /// <summary>
        /// Passkey保护
        /// </summary>
        public int passkeyprotect = 0;
        /// <summary>
        /// 重复下载保护
        /// </summary>
        public int downloadprotect = 0;

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (!IsLogin()) return;
          
            ratioprotect = btuserinfo.RatioProtection & 1;
            passkeyprotect = btuserinfo.RatioProtection & 2;
            downloadprotect = btuserinfo.RatioProtection & 4;

            if (DNTRequest.IsPost())
            {
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                if (!ValidateUserPassword(true))
                {
                    AddErrLine("密码错误");
                    return;
                }

                if (!Utils.StrIsNullOrEmpty(DNTRequest.GetString("ratioprotect")))
                {
                    btuserinfo.RatioProtection = btuserinfo.RatioProtection | 1;
                }
                else
                {
                    btuserinfo.RatioProtection = btuserinfo.RatioProtection & ((1 << 30) - (1 << 0) - 1);
                }

                if (!Utils.StrIsNullOrEmpty(DNTRequest.GetString("passkeyprotect")))
                {
                    btuserinfo.RatioProtection = btuserinfo.RatioProtection | 2;
                }
                else
                {
                    btuserinfo.RatioProtection = btuserinfo.RatioProtection & ((1 << 30) - (1 << 1) - 1);
                }

                if (!Utils.StrIsNullOrEmpty(DNTRequest.GetString("downloadprotect")))
                {
                    btuserinfo.RatioProtection = btuserinfo.RatioProtection | 4;
                }
                else
                {
                    btuserinfo.RatioProtection = btuserinfo.RatioProtection & ((1 << 30) - (1 << 2) - 1);
                }

                PTUsers.SetUserRatioProtection(btuserinfo.Uid, btuserinfo.RatioProtection);

                SetUrl("usercpratioprotect.aspx");
                SetMetaRefresh();
                SetShowBackLink(false);
                AddMsgLine("BT共享率保护参数设置成功, 正在返回 BT共享率保护参数设置 页面");
            }
        }
    }
}