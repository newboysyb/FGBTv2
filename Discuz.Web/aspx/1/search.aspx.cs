using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;

namespace Discuz.Web
{
    /// <summary>
    /// 搜索页面
    /// </summary>
    public class search : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 搜索缓存Id
        /// </summary>
        public int searchid = DNTRequest.GetInt("searchid", -1);
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = DNTRequest.GetInt("page", 1);
        /// <summary>
        /// 主题数量
        /// </summary>
        public int topiccount;
        /// <summary>
        /// 相册数量
        /// </summary>
        public int albumcount;
        /// <summary>
        /// 日志数量
        /// </summary>
        public int blogcount;
        /// <summary>
        /// 分页数量
        /// </summary>
        public int pagecount;
        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers = "";
        /// <summary>
        /// 搜索结果数量
        /// </summary>
        public int searchresultcount = 0;
        /// <summary>
        /// 搜索出的主题列表
        /// </summary>
        public DataTable topiclist = new DataTable();
        /// <summary>
        /// 帖子分表列表
        /// </summary>
        public DataTable tablelist;
        /// <summary>
        /// 搜索出的日志列表
        /// </summary>
        public DataTable spacepostlist = new DataTable();
        /// <summary>
        /// 搜索出的相册列表
        /// </summary>
        public DataTable albumlist = new DataTable();
        /// <summary>
        /// 当此值为true时,显示搜索结果提示
        /// </summary>
        public bool searchpost = false;
        /// <summary>
        /// 搜索类型
        /// </summary>
        public string type = Utils.HtmlEncode(DNTRequest.GetString("type").ToLower());
        /// <summary>
        /// 当前主题页码
        /// </summary>
        public int topicpageid = DNTRequest.GetInt("topicpage", 1);
        /// <summary>
        /// 主题分页总数
        /// </summary>
        public int topicpagecount;
        /// <summary>
        /// 主题分页页码链接
        /// </summary>
        public string topicpagenumbers = "";
        /// <summary>
        /// 当前日志分页页码
        /// </summary>
        public int blogpageid = DNTRequest.GetInt("blogpage", 1);
        /// <summary>
        /// 日志分页总数
        /// </summary>
        public int blogpagecount;
        /// <summary>
        /// 日志分页页码链接
        /// </summary>
        public string blogpagenumbers = "";
        /// <summary>
        /// 当前相册页码
        /// </summary>
        public int albumpageid = DNTRequest.GetInt("albumpage", 1);
        /// <summary>
        /// 相册分页总数
        /// </summary>
        public int albumpagecount;
        /// <summary>
        /// 相册分页页码链接
        /// </summary>
        public string albumpagenumbers = "";
        /// <summary>
        /// 提示信息
        /// </summary>
        string msg = "";
        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string keyword = Utils.HtmlEncode(DNTRequest.GetString("keyword").Trim());
        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string forumids = Utils.HtmlEncode(DNTRequest.GetString("forumids").Trim());
        /// <summary>
        /// 搜索作者用户名
        /// </summary>
        public string poster = Utils.HtmlEncode(DNTRequest.GetString("poster").Trim());
        /// <summary>
        /// 是否显示高级搜索
        /// </summary>
        public int advsearch = DNTRequest.GetInt("advsearch", 0);
        /// <summary>
        /// 是否是get提交的查询任务
        /// </summary>
        public int searchsubmit = DNTRequest.GetInt("searchsubmit", 0);
        /// <summary>
        /// 帖子分表ID
        /// </summary>
        public int posttableid = DNTRequest.GetInt("posttableid", 0);
        /// <summary>
        /// 查询类别枚举
        /// </summary>
        public SearchType searchType;
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "搜索";
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】

            if (type == "" && advsearch == 0 && searchid == -1 && poster == "" && posttableid ==0 && DNTRequest.GetString("posterid") == "") type = "seed";

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            
            GetSearchType();

            int dotrans = DNTRequest.GetInt("trans", -1);

            //判断当前操作是否是用户打开的页面
            if (searchsubmit == 0 && !ispost)
            {
                //用户权限校验
                if (!UserAuthority.Search(usergroupinfo, ref msg))
                {
                    AddErrLine(msg);
                    return;
                }

                //读取分表信息

                //if (searchid == -2)
                //{
                //    AddErrLine("查询的关键词过多（大于8个），请减少关键词数量在进行查询！使用空格分隔关键词，关键词之间为 “或” 关系");
                //    return;
                //}

                if (searchid <= 0)
                {
                    tablelist = Posts.GetAllPostTableName();
                }
                else
                {
                    if (searchType == SearchType.Error)
                    {
                        AddErrLine("未知的参数信息，请尝试重新提交");
                        return;
                    }

                    //此处存在安全漏洞，遍历searchid即可获取其他用户查询的内容，包括管理员用户。
                    //需要先校验对应的searchid是否符合当前用户的权限[较复杂，不现实]，以及keyword是否匹配、forumids是否匹配【现在的实现方案】
                    //仍没有校验用户是否有权看到这些帖子，只是阻止了遍历
                    
                    int posterid = CheckSearchInfo(forumids);

                    switch (searchType)
                    {
                        case SearchType.SpacePostTitle:
                            spacepostlist = Searches.GetSearchCacheList(posttableid, searchid, 16, pageid, out topiccount, searchType, keyword.ToLower(), forumids, posterid.ToString());
                            break;
                        case SearchType.AlbumTitle:
                            albumlist = Searches.GetSearchCacheList(posttableid, searchid, 16, pageid, out topiccount, searchType, keyword.ToLower(), forumids, posterid.ToString());
                            break;
                        case SearchType.ByPoster:
                            topiclist = Searches.GetSearchCacheList(posttableid, searchid, 16, topicpageid, out topiccount, SearchType.TopicTitle, keyword.ToLower(), forumids, posterid.ToString());
                            topicpageid = CalculateCurrentPage(topiccount, topicpageid, out topicpagecount);
                            topicpagenumbers = topicpagecount > 1 ? Utils.GetPageNumbers(topicpageid, topicpagecount, "search.aspx?type=" + type + "&searchid=" + searchid.ToString() + "&keyword=" + keyword + "&posterid=" + posterid + "&forumids=" + forumids, 8, "topicpage", "#1") : "";
                            return;

                        case SearchType.PostContent:
                        default:
                            topiclist = Searches.GetSearchCacheList(posttableid, searchid, 16, pageid, out topiccount, searchType, keyword.ToLower(), forumids, posterid.ToString());
                            break;
                    }

                    //根据主题数量处理结果
                    if (topiccount == -1)
                    {
                        dotrans += 1;
                        //不向用户发送无用的提示信息，直接跳转到新的搜索地址，下同
                        //AddErrLine("此查询结果链接已经过期，请重新提交查询请求");
                        //return;
                    }
                    else if (topiccount == -2)
                    {
                        dotrans += 1;
                        //AddErrLine("非法的参数信息：不存在的searchid");
                        //return;
                    }
                    else if (topiccount == 0)
                    {
                        AddErrLine("抱歉, 没有搜索到符合要求的记录，请尝试缩短或更换关键词，如搜索 “未来花园” 若没有结果，请搜索 “未来” 或 “花园”");
                        //AddErrLine("不存在的searchid");
                        return;
                    }
                    else
                    {
                        CalculateCurrentPage();
                        //得到页码链接 "search.aspx?type=" + type + "&searchid=" + searchid.ToString() + "&keyword=" + keyword + "&posterid=" + posterid + "&forumids=" + forumids
                        pagenumbers = pagecount > 1 ? Utils.GetPageNumbers(pageid, pagecount, string.Format("{0}search.aspx?type={1}&searchid={2}&keyword={3}&posterid={4}&posttableid={5}&forumids={6}&trans={7}", forumpath, type, searchid, keyword, posterid, posttableid, forumids, dotrans), 8) : "";
                        return;
                    }
                }
            }

            if (ispost || searchsubmit == 1 || (dotrans > -1 && dotrans < 2))
            {
                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 
                //【BT修改】
                if (type == "seed")
                {
                    System.Web.HttpContext.Current.Response.Redirect(forumpath + "showseeds.aspx?&keywords=" + keyword, true);
                    return;
                }
                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////// 

                //检查用户的搜索权限，包括搜索时间间隔的限制
                if (!UserAuthority.Search(userid, lastsearchtime, useradminid, usergroupinfo, ref msg))
                {
                    AddErrLine(msg);
                    return;
                }

                if (searchType == SearchType.Error)
                {
                    AddErrLine("未知的参数信息，请尝试重新提交");
                    return;
                }

                searchpost = true;
                string searchforumid = DNTRequest.GetString("searchforumid").Trim();
                int posterid = CheckSearchInfo(searchforumid);
                if (IsErr()) return;

                //if (Utils.StrIsNullOrEmpty(keyword) && posterid > 0 && Utils.StrIsNullOrEmpty(type))
                //{
                //    type = "author";
                //    searchType = SearchType.ByPoster;
                //}

                searchid = Searches.Search(posttableid, userid, btuserinfo.Extcredits2, usergroupid, keyword, posterid, searchType, ref searchforumid, DNTRequest.GetInt("searchtime", 0), DNTRequest.GetInt("searchtimetype", 0), DNTRequest.GetInt("resultorder", 0), DNTRequest.GetInt("resultordertype", 0), ipaddress);
                
                if (searchid > 0)
                    System.Web.HttpContext.Current.Response.Redirect(string.Format("{0}search.aspx?type={1}&searchid={2}&keyword={3}&posterid={4}&posttableid={5}&forumids={6}&trans={7}", forumpath, type, searchid, keyword, posterid, posttableid, searchforumid, dotrans), false);
                else
                {
                    if (searchid == -2)
                    {
                        AddErrLine("查询的关键词过多，请减少关键词数量到8个或以下再进行查询。 使用空格分隔不同的关键词，关键词之间为 “或” 关系");
                        return;
                    }

                    AddErrLine("抱歉, 没有搜索到符合要求的记录，请尝试缩短或更换关键词，如搜索 “未来花园” 若没有结果，请搜索 “未来” 或 “花园”");
                    return;
                }
            }
        }

        /// <summary>
        /// 分页信息
        /// </summary>
        private void CalculateCurrentPage()
        {
            //获取总页数
            pagecount = topiccount % 16 == 0 ? topiccount / 16 : topiccount / 16 + 1;
            pagecount = (pagecount == 0 ? 1 : pagecount);
            //修正请求页数中可能的错误
            pageid = (pageid < 1 ? 1 : pageid);
            pageid = (pageid > pagecount ? pagecount : pageid);
        }

        /// <summary>
        /// 分页信息
        /// </summary>
        private int CalculateCurrentPage(int listcount, int pageid, out int pagecount)
        {
            //获取总页数
            pagecount = listcount % 16 == 0 ? listcount / 16 : listcount / 16 + 1;
            pagecount = (pagecount == 0 ? 1 : pagecount);
            //修正请求页数中可能的错误
            pageid = (pageid < 1 ? 1 : pageid);
            pageid = (pageid > pagecount ? pagecount : pageid);
            return pageid;
        }

        /// <summary>
        /// 获取提交的查询枚举类型
        /// </summary>
        private void GetSearchType()
        {
            switch (type)
            {
                case "":
                case "topic": searchType = SearchType.TopicTitle; break;
                case "author": searchType = SearchType.ByPoster; break;
                case "post": searchType = SearchType.PostContent; break;
                case "spacepost": searchType = SearchType.SpacePostTitle; break;
                case "album": searchType = SearchType.AlbumTitle; break;
                case "digest": searchType = SearchType.DigestTopic; break;
                case "seed": searchType = SearchType.Seed; break;
                default: searchType = SearchType.Error; break;
            }
        }

        public string LightKeyWord(string str, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return str;
            return str.Replace(keyword, "<font color=\"#ff0000\">" + keyword + "</font>");
        }

        private int CheckSearchInfo(string searchforumid)
        {
            //如果posterid中的值是current，则代表当前登录用户，否则搜索指定的posterid
            int posterid = DNTRequest.GetString("posterid").ToLower().Trim() == "current" ? userid : DNTRequest.GetInt("posterid", -1);

            if (Utils.StrIsNullOrEmpty(keyword) && Utils.StrIsNullOrEmpty(poster) && Utils.StrIsNullOrEmpty(DNTRequest.GetString("posterid")))
            {
                AddErrLine("关键字和用户名不能同时为空");
                return posterid;
            }

            if (posterid > 0 && Users.GetShortUserInfo(posterid) == null)
            {
                AddErrLine("指定的用户ID不存在");
                return posterid;
            }
            else if (!Utils.StrIsNullOrEmpty(poster))
            {
                posterid = Users.GetUserId(poster);
                if (posterid == -1)
                {
                    AddErrLine("搜索用户名不存在");
                    return posterid;
                }
            }
            if (!Utils.StrIsNullOrEmpty(searchforumid))
            {
                foreach (string forumId in Utils.SplitString(searchforumid, ","))
                {
                    if (!Utils.IsNumeric(forumId))
                    {
                        AddErrLine("非法的搜索版块ID");
                        return posterid;
                    }
                }
            }
            return posterid;
        }
    }
}
