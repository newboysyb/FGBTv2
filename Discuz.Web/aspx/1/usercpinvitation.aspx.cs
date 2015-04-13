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
    /// BT设置-购买邀请
    /// </summary>
    public class usercpinvitation : UserCpPage
    {
        #region 页面变量
        /// <summary>
        /// 积分交易税
        /// </summary>
        public float creditstax = Scoresets.GetCreditsTax();
        /// <summary>
        /// 积分计算器js脚本
        /// </summary>
        public string jscreditsratearray = "<script type=\"text/javascript\">\r\nvar creditsrate = new Array();\r\n{0}\r\n</script>";
        /// <summary>
        /// 交易积分
        /// </summary>
        public int creditstrans = Scoresets.GetCreditsTrans();
        /// <summary>
        /// 交易积分名称
        /// </summary>
        public string creditstransname = Scoresets.GetValidScoreName()[Scoresets.GetCreditsTrans()];
        /// <summary>
        /// 交易积分单位
        /// </summary>
        public string creditstransunit = Scoresets.GetValidScoreUnit()[Scoresets.GetCreditsTrans()];
        /// <summary>
        /// 购买的积分数量
        /// </summary>
        public int creditsamount = DNTRequest.GetInt("amount", 1);
        #endregion

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //【BT修改】相关变量

        /// <summary>
        /// 邀请码当前价格
        /// </summary>
        public decimal inviteprice = 204800;

        /// <summary>
        /// 可以购买的最大数量
        /// </summary>
        public int maxnumber;
        /// <summary>
        /// bt配置信息
        /// </summary>
        public PrivateBTConfigInfo btconfig = new PrivateBTConfigInfo();
        /// <summary>
        /// 警示信息
        /// </summary>
        public string alertmessage = "";

        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            string jsCreditsRateArray = "";
            foreach (DataRow dr in Scoresets.GetScorePaySet(0).Rows)
            {
                jsCreditsRateArray += "creditsrate[" + dr["id"] + "] = " + dr["rate"] + ";\r\n";
            }
            jscreditsratearray = string.Format(jscreditsratearray, jsCreditsRateArray);

            if (!IsLogin()) return;

            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //【BT修改】

            btconfig = PrivateBTConfig.GetPrivateBTConfig();
            inviteprice = btconfig.InvitePrice;
            maxnumber = (int)((float)(user.Extcredits3 - user.Extcredits4) / (float)inviteprice);

            //检测当前是否可以购买邀请
            if (btconfig.AllowInviteRegister == false || DateTime.Now < btconfig.FreeRegBeginTime || DateTime.Now > btconfig.FreeRegEndTime)
            {
                alertmessage = "当前本站禁止邀请注册，邀请码仅能用于复活用户，请务必注意！";
            }
            else
            {
                alertmessage = "注意：邀请注册截止时间：" + btconfig.FreeRegEndTime + "， 现在距离截至还有：" + (btconfig.FreeRegEndTime - DateTime.Now).TotalHours.ToString("0.0") + "小时。 ";
                alertmessage += "邀请注册时间截止后，邀请码将只能用于复活用户！";
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

                ////检测当前是否可以购买邀请
                //if (btconfig.AllowInviteRegister == false || DateTime.Now < btconfig.FreeRegBeginTime || DateTime.Now > btconfig.FreeRegEndTime)
                //{
                //    alertmessage = "当前本站禁止邀请注册，邀请码仅能用于复活帐号，请务必注意！";
                //}
                //else
                //{
                //    alertmessage = "注意：邀请注册截止时间：" + btconfig.FreeRegEndTime + "， 现在距离截至还有：" + (btconfig.FreeRegEndTime - DateTime.Now).TotalHours.ToString("0.0") + "小时。 ";
                //    alertmessage += "邀请注册时间截止后，邀请码将只能用于复活帐号！";
                //}


                if (!ValidateUserPassword(true))
                {
                    AddErrLine("密码错误");
                    return;
                }

                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////
                //【BT修改】

                int paynum = DNTRequest.GetInt("paynum", 0);
                if (paynum <= 0)
                {
                    AddErrLine("数量必须是大于0的整数");
                    return;
                }


                //对购买邀请后的积分增减进行修改设置

                UserInfo __userinfo = Users.GetUserInfo(userid);
                if ((Users.GetUserExtCredits(userid, 3) - Users.GetUserExtCredits(userid, 4) - paynum * (double)inviteprice) < 0)
                {
                    AddErrLine("抱歉, 您的上传不足.");
                    return;
                }

                //邀请用的是金币，不用校验
                //if (Users.GetUserExtCredits(userid, 3) - paynum * inviteprice < Users.GetUserExtCredits(userid, 4)) //上传不能小于下载
                //{
                //    AddErrLine("抱歉, 您的上传不足. 购买邀请后共享率不能低于1");
                //    return;
                //}

                //生成邀请码//默认awardlevel已修改为10，不进行任何奖励
                paynum = PrivateBTInvitation.AddInviteCode(userid, paynum);

                //计算并更新金币值
                float extcredit3paynum = -(float)Math.Round(paynum * inviteprice);
                Users.UpdateUserExtCredits(userid, 3, extcredit3paynum);
                CreditsLogs.AddCreditsLog(userid, userid, 3, 3, paynum * (float)inviteprice, 0, Utils.GetDateTime(), 3);
                UserCredits.UpdateUserCredits(userid);

                SetUrl("usercpmyinvitation.aspx");
                SetMetaRefresh();
                SetShowBackLink(false);
                AddMsgLine(string.Format("成功购买{0}个邀请码，正在转向邀请码列表", paynum));

                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////
            }
        }
    }
}