using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Plugin.PasswordMode;

namespace Discuz.Web
{
    /// <summary>
    /// 积分转账
    /// </summary>
    public class usercpcreditstransfer : UserCpPage
    {
        #region 页面变量
        /// <summary>
        /// 交易税
        /// </summary>
        public float creditstax = Scoresets.GetCreditsTax();
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (!IsLogin()) return;

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】
            
            creditstax = (float)0;

            if (usergroupid != 1) //!PrivateBT.IsServerUser(userid) && 
            {
                AddErrLine("当前系统禁止转账");
                return;
            }

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            
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

                int paynum = DNTRequest.GetInt("paynum", 0);
                if (paynum <= 0)
                {
                    AddErrLine("数量必须是大于等于0的整数");
                    return;
                }
                int fromto = Users.GetUserId(DNTRequest.GetString("fromto").Trim());
                if (fromto == -1)
                {
                    AddErrLine("指定的转帐接受人不存在");
                    return;
                }
                int extcredits = DNTRequest.GetInt("extcredits", 0);
                if (extcredits < 1 || extcredits > 8)
                {
                    AddErrLine("请正确选择要转帐的积分类型!");
                    return;
                }

                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 
                //【BT修改】转账部分

                if (paynum >= 300)
                {
                    AddErrLine("转账上限不得超过300G");
                    return;
                }


                string fromtonamelist = DNTRequest.GetString("fromto").Trim();

                //当转账人为管理员时的处理：从IMAX账户转账
                if (user.Groupid == 1)
                {
                    
                    fromto = -1;

                    //转账为上传流量
                    extcredits = 3;

                    //转账从imax的账户支出
                    int fromid = 13;

                    //对转帐后的积分增减进行修改设置
                    UserExtcreditsInfo extcreditsinfo = Scoresets.GetScoreSet(extcredits);
                    if (extcreditsinfo.Name.Trim() == "")
                    {
                        AddErrLine("错误的输入!");
                        return;
                    }

                    //管理转账验证，确保：所有目的用户都存在，支付完成后Imax剩余流量允许
                    int totalcount = 0;
                    foreach (string fromtoname in fromtonamelist.Split(','))
                    {
                        //循环转账部分
                        fromto = Users.GetUserId(fromtoname.Trim());
                        if (fromto < 1)
                        {
                            AddErrLine("发生错误：指定的转帐接受人不存在，请检查：" + fromtoname);
                            return;
                        }
                        else
                        {
                            totalcount++;
                        }
                    }
                    if ((Users.GetUserExtCredits(fromid, extcredits) - totalcount * paynum * 1024f * 1024 * 1024) < Scoresets.GetTransferMinCredits())
                    {
                        AddErrLine(string.Format("抱歉,  \"{0}\" 不足.系统当前规定转帐余额不得小于{1}", extcreditsinfo.Name, Scoresets.GetTransferMinCredits().ToString()));
                        return;
                    }

                    if (extcredits == 3 && Users.GetUserExtCredits(fromid, 3) - totalcount * paynum * 1024f * 1024 * 1024 < Users.GetUserExtCredits(fromid, 4)) //上传不能小于下载
                    {
                        AddErrLine("抱歉, 上传不足. 转帐后共享率不能低于1");
                        return;
                    }



                    foreach (string fromtoname in fromtonamelist.Split(','))
                    {
                        //循环转账部分
                        fromto = Users.GetUserId(fromtoname.Trim());
                        if (fromto < 1)
                        {
                            AddErrLine("发生内部错误：指定的转帐接受人不存在，请检查：" + fromtoname);
                            continue;
                        }


                        //UserInfo __userinfo = Users.GetUserInfo(userid);
                        if ((Users.GetUserExtCredits(fromid, extcredits) - paynum * 1024f * 1024 * 1024) < Scoresets.GetTransferMinCredits())
                        {
                            AddErrLine(string.Format("抱歉, 您的 \"{0}\" 不足.系统当前规定转帐余额不得小于{1}", extcreditsinfo.Name, Scoresets.GetTransferMinCredits().ToString()));
                            return;
                        }

                        if (extcredits == 3 && Users.GetUserExtCredits(fromid, 3) - paynum * 1024f * 1024 * 1024 < Users.GetUserExtCredits(fromid, 4)) //上传不能小于下载
                        {
                            AddErrLine("抱歉, 您的上传不足. 转帐后共享率不能低于1");
                            return;
                        }

                        //计算并更新2个扩展积分的新值
                        //float topaynum = (float)Math.Round(paynum * (1 - creditstax), 2);
                        float topaynum = paynum;
                        Users.UpdateUserExtCredits(fromid, extcredits, paynum * -1 * 1024f * 1024 * 1024);
                        Users.UpdateUserExtCredits(fromto, extcredits, topaynum * 1024f * 1024 * 1024);
                        CreditsLogs.AddCreditsLog(fromid, fromto, extcredits, extcredits, paynum * 1024f * 1024 * 1024, 0, Utils.GetDateTime(),
                                                  8);
                        //管理转账记录
                        CreditsLogs.AddCreditsLog(userid, fromto, extcredits, extcredits, paynum * 1024f * 1024 * 1024, topaynum * 1024f * 1024 * 1024, Utils.GetDateTime(),
                                                 8);
                        UserCredits.UpdateUserCredits(fromid);
                        UserCredits.UpdateUserCredits(fromto);
                        SetUrl("usercpcreaditstransferlog.aspx");
                        SetMetaRefresh();
                        SetShowBackLink(false);

                        PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                        // 收件箱
                        privatemessageinfo.Message =
                                string.Format(
                                    "这是由论坛系统自动发送的通知短消息。\n\n转账通知：收到管理员 " + username + " 转账来上传" + topaynum.ToString() +
                                    "GB \n\n<a href='usercpcreaditstransferlog.aspx' target='_blank'>点击此处查看历史转账记录</a>"
                                    );
                        privatemessageinfo.Subject = Utils.HtmlEncode(string.Format("[转账通知]：收到管理员 " + username + " 转账来上传 " + topaynum.ToString() + " GB"));
                        privatemessageinfo.Msgto = Users.GetUserName(fromto);
                        privatemessageinfo.Msgtoid = fromto;
                        privatemessageinfo.Msgfrom = "系统";
                        privatemessageinfo.Msgfromid = userid;
                        privatemessageinfo.New = 1;
                        privatemessageinfo.Postdatetime = Utils.GetDateTime();
                        privatemessageinfo.Folder = 0;
                        PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);


                    }

                    AddMsgLine("积分转帐完毕, 正在返回积分兑换与转帐记录");
                }
                else
                {
                    //以下代码为非管理员转账，暂不使用


                    //fromto = -1;

                    ////转账为上传流量
                    //extcredits = 3;

                    ////转账从imax的账户支出
                    //int fromid = userid;

                    ////对转帐后的积分增减进行修改设置
                    //UserExtcreditsInfo extcreditsinfo = Scoresets.GetScoreSet(extcredits);
                    //if (extcreditsinfo.Name.Trim() == "")
                    //{
                    //    AddErrLine("错误的输入!");
                    //    return;
                    //}

                    //foreach (string fromtoname in fromtonamelist.Split(','))
                    //{
                    //    //循环转账部分
                    //    fromto = Users.GetUserId(fromtoname.Trim());
                    //    if (fromto == -1)
                    //    {
                    //        AddErrLine("发生错误：指定的转帐接受人不存在，请检查：" + fromtoname);
                    //        continue;
                    //    }


                    //    //UserInfo __userinfo = Users.GetUserInfo(userid);
                    //    if ((Users.GetUserExtCredits(fromid, extcredits) - paynum) < Scoresets.GetTransferMinCredits())
                    //    {
                    //        AddErrLine(string.Format("抱歉, 您的 \"{0}\" 不足.系统当前规定转帐余额不得小于{1}", extcreditsinfo.Name, Scoresets.GetTransferMinCredits().ToString()));
                    //        return;
                    //    }

                    //    if (extcredits == 3 && Users.GetUserExtCredits(fromid, 3) - paynum < Users.GetUserExtCredits(fromid, 4)) //上传不能小于下载
                    //    {
                    //        AddErrLine("抱歉, 您的上传不足. 转帐后共享率不能低于1");
                    //        return;
                    //    }

                    //    //计算并更新2个扩展积分的新值
                    //    float topaynum = (float)Math.Round(paynum * (1 - creditstax), 2);
                    //    Users.UpdateUserExtCredits(fromid, extcredits, paynum * -1);
                    //    Users.UpdateUserExtCredits(fromto, extcredits, topaynum);
                    //    CreditsLogs.AddCreditsLog(fromid, fromto, extcredits, extcredits, paynum, 0, Utils.GetDateTime(),
                    //                              8);
                    //    //管理转账记录
                    //    CreditsLogs.AddCreditsLog(userid, fromto, extcredits, extcredits, paynum, topaynum, Utils.GetDateTime(),
                    //                             8);
                    //    UserCredits.UpdateUserCredits(fromid);
                    //    UserCredits.UpdateUserCredits(fromto);
                    //    SetUrl("usercpcreaditstransferlog.aspx");
                    //    SetMetaRefresh();
                    //    SetShowBackLink(false);

                    //    PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();
                    //    // 收件箱
                    //    string paytype = "";
                    //    if (extcredits == 2) paytype = "金币";
                    //    else paytype = "流量(MB)";
                    //    privatemessageinfo.Message =
                    //            string.Format(
                    //                "这是由论坛系统自动发送的通知短消息。\n\n转账通知：收到用户 " + username + " 转账来 " + paytype.ToString() + " " + topaynum.ToString() +
                    //                "\n\n<a href='usercpcreaditstransferlog.aspx' target='_blank'>点击此处查看历史转账记录</a>"
                    //                );
                    //    privatemessageinfo.Subject = Utils.HtmlEncode(string.Format("[转账通知]：收到用户 " + username + " 转账来 " + paytype.ToString() + " " + topaynum.ToString()));
                    //    privatemessageinfo.Msgto = Users.GetUserName(fromto);
                    //    privatemessageinfo.Msgtoid = fromto;
                    //    privatemessageinfo.Msgfrom = "系统";
                    //    privatemessageinfo.Msgfromid = userid;
                    //    privatemessageinfo.New = 1;
                    //    privatemessageinfo.Postdatetime = Utils.GetDateTime();
                    //    privatemessageinfo.Folder = 0;
                    //    PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);


                    //}

                    //AddMsgLine("积分转帐完毕, 正在返回积分兑换与转帐记录");
                }

                //原始
                ////对转帐后的积分增减进行修改设置
                //string extcreditsName = Scoresets.GetScoreSet(extcredits).Name.Trim();
                //if (Utils.StrIsNullOrEmpty(extcreditsName))
                //{
                //    AddErrLine("错误的输入!");
                //    return;
                //}
                //if ((Users.GetUserExtCredits(userid, extcredits) - paynum) < Scoresets.GetTransferMinCredits())
                //{
                //    AddErrLine(string.Format("抱歉, 您的 \"{0}\" 不足.系统当前规定转帐余额不得小于{1}", extcreditsName, Scoresets.GetTransferMinCredits().ToString()));
                //    return;
                //}

                ////计算并更新2个扩展积分的新值
                //float toPayNum = (float)Math.Round(paynum * (1 - creditstax), 2);
                //Users.UpdateUserExtCredits(userid, extcredits, paynum * -1);
                //Users.UpdateUserExtCredits(fromto, extcredits, toPayNum);
                //CreditsLogs.AddCreditsLog(userid, fromto, extcredits, extcredits, paynum, toPayNum, Utils.GetDateTime(), 2);

                //SetUrl("usercpcreaditstransferlog.aspx");
                //SetMetaRefresh();
                //SetShowBackLink(false);
                //AddMsgLine("积分转帐完毕, 正在返回积分兑换与转帐记录");

                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 
                
                
            }
        }
    } //class end
}