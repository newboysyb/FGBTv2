using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;

namespace Discuz.Web
{
    /// <summary>
    /// 找回密码页
    /// </summary>
    public class getpassword : PageBase
    {
        protected override void ShowPage()
        {
            pagetitle = "密码找回";
            username = Utils.RemoveHtml(DNTRequest.GetString("username"));

            //如果提交...
            if (DNTRequest.IsPost())
            {
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                base.SetBackLink("getpassword.aspx?username=" + username);

                if (Users.GetUserId(username) == 0)
                {
                    AddErrLine("用户不存在");
                    return;
                }
                if (Utils.StrIsNullOrEmpty(DNTRequest.GetString("email")))
                {
                    AddErrLine("邮箱不能为空");
                    return;
                }

                if (IsErr()) return;
  
                if (Users.CheckEmailAndSecques(username, DNTRequest.GetString("email"), DNTRequest.GetInt("question", 0), DNTRequest.GetString("answer"), GetForumPath()))
                {
                    SetUrl(forumpath);
                    SetMetaRefresh(5);
                    SetShowBackLink(false);
                    MsgForward("getpassword_succeed");
                    AddMsgLine("邮箱验证通过！<br />重置密码的方法已经通过 Email 发送到您的邮箱中, 请在 3 天之内到论坛修改您的密码。<br />如 2 天以上无法收到密码重置邮件，请先检查邮箱垃圾邮件夹！ 仍无法收到时，请使用注册邮箱发送邮件到system@buaabt.cn，申请人工密码重置");
                }
                else
                    AddErrLine("用户名 与 Email 地址 或 安全提问 不匹配, 请返回重试。 如果没有设置安全提问，安全提问一项不能选择任何问题，否则将不能通过验证");
            }
        }

        private string GetForumPath()
        {
            return this.Context.Request.Url.ToString().ToLower().Substring(0, this.Context.Request.Url.ToString().ToLower().IndexOf("/aspx/"));
        }
    }
}