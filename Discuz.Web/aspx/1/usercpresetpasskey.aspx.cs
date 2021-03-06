﻿using System;
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
    /// BT设置-重置peer
    /// </summary>
    public class usercpresetpasskey : UserCpPage
    {
        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (!IsLogin()) return;

            //获取用户Passkey
            btuserinfo.Passkey = PTUsers.GetBtUserInfo(userid).Passkey;

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


                //重置Passkey
                PTUsers.ResetPasskey(userid);

                SetUrl("usercpresetpasskey.aspx");
                SetMetaRefresh();
                SetShowBackLink(false);
                AddMsgLine(string.Format("您的Passkey已被重置，请更新所有正在上传下载的种子中的Passkey"));
            }
        }
    }
}