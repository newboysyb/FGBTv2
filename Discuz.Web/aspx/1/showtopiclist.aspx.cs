using System;
using System.Data;
using System.Text;
using Discuz.Common;

using Discuz.Common.Generic;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Web.UI;
using Discuz.Entity;

namespace Discuz.Web
{
    /// <summary>
    /// 查看新帖、精华帖
    /// </summary>
    public class showtopiclist : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 在线用户列表
        /// </summary>
        public DataTable onlineuserlist;
        /// <summary>
        /// 在线用户图例
        /// </summary>
        public string onlineiconlist;
        /// <summary>
        /// 主题列表
        /// </summary>
        public Discuz.Common.Generic.List<TopicInfo> topiclist;
        /// <summary>
        /// 子版块列表
        /// </summary>
        public List<IndexPageForumInfo> subforumlist;
        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist;
        /// <summary>
        /// 版块信息
        /// </summary>
        public ForumInfo forum = new ForumInfo();
        /// <summary>
        /// 当前用户管理组信息
        /// </summary>
        public AdminGroupInfo admingroupinfo = new AdminGroupInfo();
        /// <summary>
        /// 论坛在线总数
        /// </summary>
        public int forumtotalonline;
        /// <summary>
        /// 论坛在线注册用户总数
        /// </summary>
        public int forumtotalonlineuser;
        /// <summary>
        /// 论坛在线游客数
        /// </summary>
        public int forumtotalonlineguest;
        /// <summary>
        /// 论坛在线隐身用户数
        /// </summary>
        public int forumtotalonlineinvisibleuser;
        /// <summary>
        /// 版块Id
        /// </summary>
        public int forumid = DNTRequest.GetInt("forumid", -1);
        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav = "";
        /// <summary>
        /// 是否显示版块密码提示 1为显示, 0不显示
        /// </summary>
        public int showforumlogin;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = DNTRequest.GetInt("page", 1);
        /// <summary>
        /// 主题总数
        /// </summary>
        public int topiccount = 0;
        /// <summary>
        /// 分页总数
        /// </summary>
        public int pagecount = 0;
        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers = "";
        /// <summary>
        /// 置顶主题数
        /// </summary>
        public int toptopiccount = 0;
        /// <summary>
        /// 得到Tpp设置
        /// </summary>
        public int tpp = Utils.StrToInt(ForumUtils.GetCookie("tpp"), GeneralConfigs.GetConfig().Tpp);
        /// <summary>
        /// 排序方式
        /// </summary>
        public int order = DNTRequest.GetInt("order", 2); //排序字段
        /// <summary>
        /// //排序方向[默认：降序]
        /// </summary>
        public int direct = DNTRequest.GetInt("direct", 1);
        /// <summary>
        /// 查看方式,digest=精华帖,其他值=新帖
        /// </summary>
        public string type = "";
        /// <summary>
        /// 新帖时限
        /// </summary>
        public int newtopic = DNTRequest.GetInt("newtopic", 1200);
        /// <summary>
        /// 用户选择的版块
        /// </summary>
        public string forums = "";
        /// <summary>
        /// 论坛选择多选框列表
        /// </summary>
        public string forumcheckboxlist = "";
        /// <summary>
        /// 获取绑定相关版块的商品分类信息
        /// </summary>
        public string goodscategoryfid = GeneralConfigs.GetConfig().Enablemall > 0 ? Discuz.Plugin.Mall.MallPluginProvider.GetInstance().GetGoodsCategoryWithFid() : "{}";
        /// <summary>
        /// 权限校验提示信息
        /// </summary>
        string msg = "";
        /// <summary>
        /// 弹出导航菜单的HTML代码
        /// </summary>
        public string navhomemenu = "";
        #endregion

        //后台指定的最大查询帖数
        private int maxseachnumber = 10000;

        /// <summary>
        /// 板块列表
        /// </summary>
        private string forumlist = ""; 

        protected override void ShowPage()
        {
            if (userid > 0 && useradminid > 0)
                admingroupinfo = AdminGroups.GetAdminGroupInfo(usergroupid);

            if (config.Rssstatus == 1)
                AddLinkRss("tools/rss.aspx", "最新主题");

            #region 版块信息设置及板块访问权限校验
            
            //当所选论坛为多个时或全部时
            if (forumid == -1)
            {
                //用户点选相应的论坛
                forums = (!Utils.StrIsNullOrEmpty(DNTRequest.GetString("fidlist")) ? DNTRequest.GetString("fidlist") : DNTRequest.GetString("forums")).ToLower();
                //如果是选择全部版块

                forums = (forums == string.Empty || forums == "all") ? GetForums() : forums;
                forums = GetAllowviewForums(forums, ipaddress);
            }

            navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);
            
            if (forumid > 0)
            {
                forum = Forums.GetForumInfo(forumid);
                if (forum == null)
                {
                    AddErrLine("不存在的版块ID");
                    return;
                }

                pagetitle = Utils.RemoveHtml(forum.Name);
                forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);
                showforumlogin = ShowForumLogin();

                //校验IP允许范围
                if ((forum.Allowpostspecial & 64) == 64 && !PTAbt.IsIPAllowed(ipaddress))
                {
                    AddErrLine("不存在的版块ID");
                    return;
                }

                //校验板块访问权限
                if (!UserAuthority.VisitAuthority(forum, usergroupinfo, userid, ref msg))
                {
                    AddErrLine(msg);
                    return;
                }
                // 得到子版块列表
                subforumlist = Forums.GetSubForumCollection(forumid, forum.Colcount, config.Hideprivate, usergroupid, config.Moddisplay, ipaddress);
            }
            #endregion

            //设置查询条件
            SetCondition();

            if (IsErr()) return;

            pagetitle = (type == "digest" ? "查看精华" : "查看新帖");

            SetPageIdAndNumber();

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】
            
            forum.Topictypeprefix = 1;
            
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            topiclist = Topics.GetTopicListGlobalNewOrDigest(tpp, pageid, 0, 10, config.Hottopic, forum.Autoclose, forum.Topictypeprefix, forumlist, digest, lastpost, GetOrder(), direct);

            OnlineUsers.UpdateAction(olid, UserAction.ShowForum.ActionID, forumid, config.Onlinetimeout);
            ForumUtils.UpdateVisitedForumsOptions(forumid);
        }

        /// <summary>
        /// 获取排序方式
        /// </summary>
        /// <returns></returns>
        private string GetOrder()
        {
            //排序的字段
            if (!Utils.StrIsNullOrEmpty(DNTRequest.GetString("search"))) //进行指定查询
                return order == 1 ? "lastpostid" : "tid";
            else
                return "tid";
        }

        private int digest = -1;
        private DateTime lastpost = DateTime.Now;
        /// <summary>
        /// 设置分页信息
        /// </summary>
        private void SetPageIdAndNumber()
        {
            //设置查询条件
            //SetCondition();
            //获取主题总数
            if (type == "digest")
            {
                digest = 0;
                topiccount = Topics.GetTopicCount(forumlist, digest);
            }
            else if (type == "newtopic")
            {
                lastpost = DateTime.Now.AddMinutes(-newtopic);
                topiccount = Topics.GetTopicCount(forumlist, lastpost);
            }
            else if (type == "newimage")
            {
                return;
            }
            else { AddErrLine("发生错误"); return; }

            //防止查询数超过系统规定的最大值
            topiccount = maxseachnumber > topiccount ? topiccount : maxseachnumber;

            if (tpp <= 0)
                tpp = config.Tpp;

            //得到用户设置的每页显示主题数
            if (userid > 0)
            {
                ShortUserInfo userinfo = Users.GetShortUserInfo(userid);
                if (userinfo != null)
                {
                    if (userinfo.Tpp > 0)
                        tpp = userinfo.Tpp;

                    if (userinfo.Newpm == 0)
                        newpmcount = 0;
                }
            }

            //获取总页数
            pagecount = topiccount % tpp == 0 ? topiccount / tpp : topiccount / tpp + 1;
            if (pagecount == 0)
                pagecount = 1;

            //修正请求页数中可能的错误
            pageid = pageid < 1 ? 1 : pageid;
            pageid = pageid > pagecount ? pagecount : pageid;

            //如果当前页面的返回结果超过系统规定的的范围时，则进行相应删剪 //没必要搞这个呀。。搞了导致最后一页显示不正常，因为修改了每页显示数量，算出来的数就不对了
            //if ((pageid * tpp) > topiccount)
            //    tpp = tpp - (pageid * tpp - topiccount);

            //得到页码链接
            pagenumbers = Utils.StrIsNullOrEmpty(DNTRequest.GetString("search")) ?
                    Utils.GetPageNumbers(pageid, pagecount, string.Format("showtopiclist.aspx?type={0}&newtopic={1}&forumid={2}&forums={3}", type, newtopic, forumid, forums), 8) :
                    Utils.GetPageNumbers(pageid, pagecount, string.Format("showtopiclist.aspx?search=1&type={0}&newtopic={1}&order={2}&direct={3}&forumid={4}&forums={5}",
                                             type, newtopic, DNTRequest.GetString("order"), DNTRequest.GetString("direct"), forumid, forums), 8);
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        private void SetCondition()
        {
            #region 对搜索条件进行检索

            string intype = DNTRequest.GetString("type", true);
            if(intype == "digest")
            {
                type = "digest";
            }
            else if(intype == "newtopic")
            {
                type = "newtopic";
            }
            else if (intype == "newimage")
            {
                type = "newimage";
            }


            if (forumid > 0)
                forumlist = forumid.ToString();
            else
            {
                //验证重新生成的版块id列表是否合法(需要放入sql语句查询)                
                if (!Utils.IsNumericList(forums))
                    AddErrLine("版块ID不合法或没有该版块访问权限");

                forumlist = forums;
            }
            #endregion
        }

        /// <summary>
        /// 显示登陆窗体
        /// </summary>
        /// <returns></returns>
        private int ShowForumLogin()
        {
            // 是否显示版块密码提示 1为显示, 0不显示
            int showforumlogin = 1;
            // 如果版块未设密码
            if (forum.Password == "")
                showforumlogin = 0;
            else
            {
                // 如果检测到相应的cookie正确
                if (Utils.MD5(forum.Password) == ForumUtils.GetCookie("forum" + forumid + "password"))
                    showforumlogin = 0;
                else
                {
                    // 如果用户提交的密码正确则保存cookie
                    if (forum.Password == DNTRequest.GetString("forumpassword"))
                    {
                        ForumUtils.WriteCookie("forum" + forumid + "password", Utils.MD5(forum.Password));
                        showforumlogin = 0;
                    }
                }
            }
            return showforumlogin;
        }

        /// <summary>
        /// 取得当前用户有权访问的版块列表//【此函数用于IP过滤时需要进行改进】
        /// </summary>
        /// <param name="forums">原始版块列表(用逗号分隔的fid)</param>
        /// <returns>有权访问的版块列表(用逗号分隔的fid)</returns>
        private string GetAllowviewForums(string forums, string ipaddress)
        {
            //验证版块id列表是否合法的数字列表                
            if (!Utils.IsNumericList(forums))
                return "";

            string allowviewforums = "";

            foreach (string strfid in forums.Split(','))
            {
                int fid = Utils.StrToInt(strfid, 0);
                ForumInfo forumInfo = Forums.GetForumInfo(fid);

                if (forumInfo == null||forumInfo.Layer == 0||forumInfo.Status == 0)
                    continue;

                if (!Forums.AllowView(forumInfo, usergroupid, ipaddress))
                    continue;

                //金币数较少时，不允许访问分享版
                if (forumInfo.Fid == 58 && btuserinfo.Extcredits2 < 10)
                {
                    continue;
                }
                //根据ip地址不显示的特定板块
                if ((forumInfo.Allowpostspecial & 64) == 64 && !PTAbt.IsIPAllowed(ipaddress)) continue;

                if ((Utils.StrIsNullOrEmpty(forumInfo.Password) || Utils.MD5(forumInfo.Password.Trim()) == ForumUtils.GetCookie("forum" + strfid.Trim() + "password")))
                {
                    allowviewforums += string.Format(",{0}", fid);
                }
            }
            return allowviewforums.Trim(',');
        }

        /// <summary>
        /// 获取所有版块列表的fid字符串
        /// </summary>
        /// <returns></returns>
        private string GetForums()
        {
            string forums = string.Empty;
            foreach (ForumInfo forumInfo in Forums.GetForumList())
                forums += string.Format(",{0}", forumInfo.Fid);

            return forums.Trim(',');
        }


        /// <summary>
        /// 获得已选取的论坛列表
        /// </summary>
        /// <returns>列表内容的html</returns>
        public string GetForumCheckBoxListCache()
        {
            StringBuilder sb = new StringBuilder();

            forums = "," + forums + ",";

            List<ForumInfo> forumList = Forums.GetForumList(GetAllowviewForums(GetForums(), ipaddress));

            int count = 1;
            foreach (ForumInfo forumInfo in forumList)
            {
                if (forums == ",all," || forums.IndexOf("," + forumInfo.Fid + ",") >= 0)
                    sb.AppendFormat("<td><input id=\"fidlist\" onclick=\"javascript:SH_SelectOne(this)\" type=\"checkbox\" value=\"{0}\"	name=\"fidlist\"  checked/> {1}</td>\r\n",
                        forumInfo.Fid, forumInfo.Name);
                else
                    sb.AppendFormat("<td><input id=\"fidlist\" onclick=\"javascript:SH_SelectOne(this)\" type=\"checkbox\" value=\"{0}\"	name=\"fidlist\"  /> {1}</td>\r\n",
                        forumInfo.Fid, forumInfo.Name);

                if (count > 3)
                {
                    sb.Append("			  </tr>\r\n");
                    sb.Append("			  <tr>\r\n");
                    count = 0;
                }
                count++;
            }
            return sb.ToString();
        }
    }
}
