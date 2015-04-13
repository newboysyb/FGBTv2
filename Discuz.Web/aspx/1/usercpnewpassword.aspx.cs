using System;
using System.Data;

using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Plugin.PasswordMode;

namespace Discuz.Web
{
    /// <summary>
    /// 重置密码页面
    /// </summary>
    public class usercpnewpassword : UserCpPage
    {
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
        //【BT修改】
        
        /// <summary>
        /// 是否需要原密码
        /// </summary>
        public bool needoldpassword = true;

        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (!IsLogin()) return;

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】ihome不校验原密码

            UserInfo userInfo = Users.GetUserInfo(userid);
            //if (userInfo.Password.Length == 32 && userInfo.Password.Substring(0, 16).ToLower() == "0123456789abcdef")
            if (PTUsers.IsEmptyPasswordSHA512(userid))
            {
                needoldpassword = false;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            if (DNTRequest.IsPost())
            {
                
                string newpassword = DNTRequest.GetString("newpassword");

                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 
                //【BT修改】ihome不校验原密码
                
                if (needoldpassword)
                {
                    ////第三方加密验证模式
                    //if (config.Passwordmode > 1 && PasswordModeProvider.GetInstance() != null)
                    //{
                    //    if (!PasswordModeProvider.GetInstance().CheckPassword(userInfo, DNTRequest.GetString("oldpassword")))
                    //    {
                    //        AddErrLine("您的原密码错误");
                    //        return;
                    //    }
                    //}
                    //else if (Users.CheckPassword(userid, DNTRequest.GetString("oldpassword"), true) == -1)
                    //{
                    //    AddErrLine("您的原密码错误");
                    //    return;
                    //}

                    //新密码体系验证用户密码，暂时为原始密码
                    int uid = PTUsers.CheckPasswordSHA512(userid, DNTRequest.GetString("oldpassword"), true, false, "", false);
                    if (uid < 1)
                    {
                        AddErrLine("您的原密码错误");
                        return;
                    }
                }
                
                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////

                if (newpassword != DNTRequest.GetString("newpassword2"))
                {
                    AddErrLine("新密码两次输入不一致");
                    return;
                }
                if (Utils.StrIsNullOrEmpty(newpassword))
                {
                    newpassword = DNTRequest.GetString("oldpassword");
                }
                if (newpassword.Length < 6)
                {
                    AddErrLine("密码不得少于6个字符");
                    return;
                }

                userInfo.Password = newpassword;
                PTUsers.ResetPassword(userInfo);

                //同步其他应用密码
                Sync.UpdatePassword(userInfo.Username, userInfo.Password, "");

                if (!Utils.StrIsNullOrEmpty(DNTRequest.GetString("changesecques")))
                    PTUsers.UpdateUserSecques(userid, DNTRequest.GetInt("question", 0), DNTRequest.GetString("answer"));

                //更新Cookie密码
                userInfo = Users.GetUserInfo(userInfo.Uid);
                ForumUtils.WriteCookie("password", ForumUtils.SetCookiePassword(userInfo.Password, config.Passwordkey));
                OnlineUsers.UpdatePassword(olid, userInfo.Password);

                SetUrl("usercpnewpassword.aspx");
                SetMetaRefresh();
                SetShowBackLink(true);
                AddMsgLine("修改密码完毕, 同时已经更新了您的登录信息");
            }
        }
    }
}