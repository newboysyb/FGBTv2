using System;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Web.UI;

namespace Discuz.Web
{
    /// <summary>
    /// 用户列表页面
    /// </summary>
    public class showuser : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 用户列表
        /// </summary>
        public DataTable userlist = new DataTable();
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = DNTRequest.GetInt("page", 1);
        /// <summary>
        /// 总用户数
        /// </summary>
        public int totalusers;
        /// <summary>
        /// 分页页数
        /// </summary>
        public int pagecount;
        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers;

        /// <summary>
        /// 原程序中使用了类似 document.getElementById('ordertype').value="{request[ordertype]}";的方式在页面中展现，因为没有过滤，是不安全的。
        /// </summary>
        public string orderby = DNTRequest.GetString("orderby").Trim();

        public string ordertype = DNTRequest.GetString("ordertype").Trim();

        #endregion

        /// <summary>
        /// 显示级别，0为普通版主，1为管理员（显示注册IP和注册方式），2为详细
        /// </summary>
        public int showlevel = 0;

        protected override void ShowPage()
        {
            pagetitle = "用户";

            if (config.Memliststatus != 1)
            {
                AddErrLine("系统不允许查看用户列表");
                return;
            }

            if (usergroupid > 3 || usergroupid < 1)
            {
                AddErrLine("系统不允许查看用户列表");
                return;
            }
            else
            {
                if (userid == 1) showlevel = 2;
                else if (usergroupid == 1) showlevel = 1;
                else showlevel = 0;
            }

            
            //进行参数过滤
            if (!Utils.StrIsNullOrEmpty(orderby) && !Utils.InArray(orderby, "uid,username,credits,posts,admin,joindate,lastactivity"))
                orderby = "uid";
            
            //进行参数过滤      
            if (!ordertype.Equals("desc") && !ordertype.Equals("asc") )
                ordertype = "desc";

            totalusers = Users.GetUserCountByAdmin(DNTRequest.GetString("orderby"));

            //获取总页数
            pagecount = totalusers%100 == 0 ? totalusers/100 : totalusers/100 + 1;
            pagecount = pagecount == 0 ? 1 : pagecount;

            //修正请求页数中可能的错误
            pageid = pageid < 1 ? 1 : pageid;
            pageid = pageid > pagecount ? pagecount : pageid;
            //获取当前页主题列表
            userlist = Users.GetUserList(100, pageid, orderby, ordertype);
            //得到页码链接
            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, string.Format("showuser.aspx{0}", string.Format("?orderby={0}&ordertype={1}", orderby, ordertype)), 8);
        }
    }
}