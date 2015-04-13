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
    public class usercpmyinvitation : UserCpPage
    {

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //【BT修改】相关变量

        /// <summary>
        /// 每页显示的数量（此数量已经固化到sql语句中，需一起更改）
        /// </summary>
        private int pagesize = 50;

        /// <summary>
        /// 邀请码总数
        /// </summary>
        public int invitationcount;

        /// <summary>
        /// 邀请码列表
        /// </summary>
        public DataTable invitationlist = new DataTable();

        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (!IsLogin()) return;

            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //【BT修改】

            user = Users.GetUserInfo(userid);

            //得到当前用户请求的页数
            pageid = DNTRequest.GetInt("page", 1);
            //获取主题总数
            invitationcount = PrivateBTInvitation.GetInviteCodeCount(userid, false);
            //获取总页数
            pagecount = invitationcount % pagesize == 0 ? invitationcount / pagesize : invitationcount / pagesize + 1;
            if (pagecount == 0)
            {
                pagecount = 1;
            }
            //修正请求页数中可能的错误
            if (pageid < 1)
            {
                pageid = 1;
            }
            if (pageid > pagecount)
            {
                pageid = pagecount;
            }

            //获取收入记录并分页显示

            invitationlist = PrivateBTInvitation.GetInviteCodeList(userid, pagesize, pageid, false);
            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "usercpmyinvitation.aspx", 8);

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

        }
    }
}