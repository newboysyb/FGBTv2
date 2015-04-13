using System;
using System.Text;
using System.Data;

using Discuz.Cache;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;
using System.IO;
using System.Text.RegularExpressions;
using Discuz.Common.Generic;

namespace Discuz.Forum
{
    /// <summary>
    /// 热点管理类
    /// </summary>
    public class ForumHots
    {
        /// <summary>
        /// 移出对应的帖子列表缓存（即强制重新加载）
        /// </summary>
        /// <param name="objectname"></param>
        public static void RemoveForumHotTopicsCache()
        {
            DNTCache.GetCacheService().RemoveObject("/Forum");
        }


        /// <summary>
        /// 获得帖子列表
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="views">最小浏览量</param>
        /// <param name="fid">板块ID</param>
        /// <param name="timetype">期限类型,一天、一周、一月、不限制</param>
        /// <param name="ordertype">排序类型,时间倒序、浏览量倒序、最后回复倒序</param>
        /// <param name="isdigest">是否精华</param>
        /// <param name="cachetime">缓存的有效期(单位:分钟)</param>
        /// <returns></returns>
        public static DataTable GetTopicList(ForumHotItemInfo forumHotItemInfo)
        {
            //防止恶意行为
            forumHotItemInfo.Cachetimeout = forumHotItemInfo.Cachetimeout == 0 ? 1 : forumHotItemInfo.Cachetimeout;
            forumHotItemInfo.Dataitemcount = forumHotItemInfo.Dataitemcount > 50 ? 50 : (forumHotItemInfo.Dataitemcount < 1 ? 1 : forumHotItemInfo.Dataitemcount);

            DataTable dt = new DataTable();

            if (forumHotItemInfo.Cachetimeout > 0)
                dt = DNTCache.GetCacheService().RetrieveObject("/Forum/ForumHostList-" + forumHotItemInfo.Id) as DataTable;

            if (dt == null)
            {
                //如果版块idlist设置为空，则默认读取所有可见板块的idlist
                string forumList = string.IsNullOrEmpty(forumHotItemInfo.Forumlist) ? Forums.GetVisibleForum("::1") : forumHotItemInfo.Forumlist;
                string orderFieldName = Focuses.GetFieldName((TopicOrderType)Enum.Parse(typeof(TopicOrderType), forumHotItemInfo.Sorttype));

                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////
                //【BT修改】不显示已经过期公告，增加分类标签，【显示关闭的主题（在PostManage.cs），此处无法修改~~~~】

                if (forumHotItemInfo.Forumlist == "4")
                {
                    dt = Discuz.Data.Topics.GetTopicList(forumHotItemInfo.Dataitemcount, -1, 0, "0",
                         Focuses.GetStartDate((TopicTimeType)Enum.Parse(typeof(TopicTimeType), forumHotItemInfo.Datatimetype)),
                         orderFieldName, forumList, orderFieldName == "digest", false);
                }
                else if (forumHotItemInfo.Forumlist == "37")
                {
                    dt = Discuz.Data.Topics.GetTopicList(forumHotItemInfo.Dataitemcount, -1, 0, "6,8",
                         Focuses.GetStartDate((TopicTimeType)Enum.Parse(typeof(TopicTimeType), forumHotItemInfo.Datatimetype)),
                         orderFieldName, forumList, orderFieldName == "digest", false);
                }
                else
                {
                    dt = Discuz.Data.Topics.GetTopicList(forumHotItemInfo.Dataitemcount, -1, 0, "",
                        Focuses.GetStartDate((TopicTimeType)Enum.Parse(typeof(TopicTimeType), forumHotItemInfo.Datatimetype)),
                        orderFieldName, forumList, orderFieldName == "digest", false);
                }


                //【END BT修改】
                //////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////

                if (forumHotItemInfo.Cachetimeout > 0)
                    DNTCache.GetCacheService().AddObject("/Forum/ForumHostList-" + forumHotItemInfo.Id, dt, forumHotItemInfo.Cachetimeout);
            }
            return dt;
        }

        /// <summary>
        /// 获取一个帖子的缓存
        /// </summary>
        /// <param name="tid">帖子ID</param>
        /// <param name="cachetime">缓存的有效期</param>
        /// <returns></returns>
        public static DataTable GetFirstPostInfo(int tid, int cachetime)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/Forum/HotForumFirst_" + tid) as DataTable;
            if (dt == null)
            {
                dt = Posts.GetPostList(tid.ToString());
                cache.AddObject("/Forum/HotForumFirst_" + tid, dt, cachetime);
            }
            return dt;
        }


        /// <summary>
        /// 获取热门板块
        /// </summary>
        /// <param name="topNumber">获取的数量</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="fid">板块ID</param>
        /// <param name="cachetime">缓存时间</param>
        /// <returns></returns>
        public static List<ForumInfo> GetHotForumList(int topNumber, string orderby, int cachetime, int tabid)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            List<ForumInfo> forumList = cache.RetrieveObject("/Aggregation/HotForumList_" + tabid) as List<ForumInfo>;
            if (forumList == null)
            {
                forumList = Stats.GetForumArray(orderby);
                if (forumList.Count > topNumber)
                {
                    List<ForumInfo> list = new List<ForumInfo>();
                    for (int i = 0; i < topNumber; i++)
                        list.Add(forumList[i]);

                    forumList = list;
                }
                cache.AddObject("/Aggregation/HotForumList" + tabid, forumList, cachetime);
            }
            return forumList;
        }

        /// <summary>
        /// 获取热门用户
        /// </summary>
        /// <param name="topNumber">获取的数量</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="cachetime">缓存时间</param>
        /// <returns></returns>
        public static ShortUserInfo[] GetUserList(int topNumber, string orderBy, int cachetime, int tabid)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();

            ShortUserInfo[] userList = cache.RetrieveObject("/Aggregation/Users_" + tabid + "List") as ShortUserInfo[];
            if (userList == null)
            {
                if (Utils.InArray(orderBy, "lastactivity,joindate"))
                {
                    List<ShortUserInfo> list = new List<ShortUserInfo>();
                    DataTable dt = Users.GetUserList(topNumber, 1, orderBy, "desc");
                    foreach (DataRow dr in dt.Rows)
                    {
                        ShortUserInfo info = new ShortUserInfo();
                        info.Uid = TypeConverter.ObjectToInt(dr["uid"]);
                        info.Username = dr["username"].ToString();
                        info.Lastactivity = dr["lastactivity"].ToString();
                        info.Joindate = dr["joindate"].ToString();
                        list.Add(info);
                    }
                    userList = list.ToArray();
                }
                else
                {
                    userList = Stats.GetUserArray(orderBy);
                    if (userList.Length > topNumber)
                    {
                        List<ShortUserInfo> list = new List<ShortUserInfo>();
                        for (int i = 0; i < topNumber; i++)
                            list.Add(userList[i]);

                        userList = list.ToArray();
                    }
                }
                cache.AddObject("/Aggregation/Users_" + tabid + "List", userList, cachetime);
            }
            return userList;
        }

        /// <summary>
        /// 静态变量访问独占标志
        /// </summary>
        private static object SynObject = new object();
        /// <summary>
        /// 获取热门图片
        /// </summary>
        /// <param name="topNumber">获取的数量</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="cachetime">缓存时间</param>
        /// <returns></returns>
        private static DataTable HotImages(int count, int cachetime, string orderby, int tabid, string fidlist, int continuous)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();

            string fidname = fidlist.Replace(",", "_");

            DataTable imagelist = cache.RetrieveObject("/Aggregation/HotImages_" + tabid + "List_" + fidname) as DataTable;
            if (imagelist == null)
            {
                lock (SynObject)
                {
                    imagelist = cache.RetrieveObject("/Aggregation/HotImages_" + tabid + "List_" + fidname) as DataTable;
                    if (imagelist == null)
                    {
                        imagelist = Discuz.Data.DatabaseProvider.GetInstance().GetWebSiteAggHotImages(count, orderby, fidlist, continuous);
                        cache.AddObject("/Aggregation/HotImages_" + tabid + "List_" + fidname, imagelist, cachetime);
                        PTLog.InsertSystemLog(PTLog.LogType.Aggregation, PTLog.LogStatus.Detail, "HotImage", "更新 HotImage 缓存 COUNT:" + imagelist.Rows.Count);
                    }
                }
            }
            return imagelist;
        }
        /// <summary>
        /// 获取热门图片瀑布流
        /// </summary>
        /// <param name="topNumber">获取的数量</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="cachetime">缓存时间</param>
        /// <returns></returns>
        private static DataTable HotImagesWaterFall(int section, int cachetime, string orderby, int tabid, string fidlist, int continuous)
        {
            if (section < 1 || section > 10) return null;

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();

            string fidname = fidlist.Replace(",", "_");

            DataTable imagelist = cache.RetrieveObject("/Aggregation/HotImagesWF_" + section.ToString() + "_" + fidname) as DataTable;
            
            if (imagelist == null && section == 1)
            {
                DataTable newimagelist = Discuz.Data.DatabaseProvider.GetInstance().GetWebSiteAggHotImages(300, orderby, fidlist, continuous);

                for (int i = 0; i < 10; i++)
                {
                    DataTable splitlist = newimagelist.Clone();
                    for (int j = 0; j < 30; j++)
                    {
                        DataRow dr = splitlist.NewRow();
                        dr.ItemArray = newimagelist.Rows[i * 30 + j].ItemArray;
                        splitlist.Rows.Add(dr);
                        if (i * 30 + j >= newimagelist.Rows.Count - 1) break;
                    }

                    if (i == section - 1)
                        imagelist = splitlist;

                    cache.AddObject("/Aggregation/HotImagesWF_" + (i + 1).ToString() + "_" + fidname, splitlist, cachetime);
                    if (i * 30 + 30 >= newimagelist.Rows.Count - 1) break;
                }

                PTLog.InsertSystemLog(PTLog.LogType.Aggregation, PTLog.LogStatus.Detail, "HotImage", "更新 HotImageWaterFall 缓存 COUNT:" + newimagelist.Rows.Count);
                 
            }
            return imagelist;
        }



        /// <summary>
        /// 转换热门图片为数组
        /// </summary>
        /// <param name="topNumber">获取的数量</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="cachetime">缓存时间</param>
        /// <returns></returns>
        public static string HotImagesArray(ForumHotItemInfo forumHotItemInfo, int groupid, string ipaddress)
        {
            return HotImagesArray(forumHotItemInfo, groupid, ipaddress, -1, "", 0);
        }
        /// <summary>
        /// 转换热门图片为数组
        /// </summary>
        /// <param name="topNumber">获取的数量</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="cachetime">缓存时间</param>
        /// <returns></returns>
        public static string HotImagesArray(ForumHotItemInfo forumHotItemInfo, int groupid, string ipaddress, decimal ext2)
        {
            return HotImagesArray(forumHotItemInfo, groupid, ipaddress, -1, "", ext2);
        }
                /// <summary>
        /// 转换热门图片为数组
        /// </summary>
        /// <param name="topNumber">获取的数量</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="cachetime">缓存时间</param>
        /// <param name="section">-1为首页轮显，1~10为warterfall</param>
        /// <returns></returns>
        public static string HotImagesArray(ForumHotItemInfo forumHotItemInfo, int groupid, string ipaddress, int section, string fidlist)
        {
            return HotImagesArray(forumHotItemInfo, groupid, ipaddress, section, fidlist, 0);
        }
        /// <summary>
        /// 转换热门图片为数组
        /// </summary>
        /// <param name="topNumber">获取的数量</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="cachetime">缓存时间</param>
        /// <param name="section">-1为首页轮显，1~10为warterfall</param>
        /// <returns></returns>
        public static string HotImagesArray(ForumHotItemInfo forumHotItemInfo, int groupid, string ipaddress, int section, string fidlist, decimal ext2)
        {
            string imagesItemTemplate = "title:\"{0}\",img:\"{1}\",url:\"{2}\"";
            string waterfallTemplate = "<div class=\"wf-cld\"><a href=\"{2}\"><img class=\"fgbtf_roundimg\" src=\"{1}\" />{0}</a></div>";
            StringBuilder hotImagesArray = new StringBuilder();

            //如果没有缩略图目录，则去生成
            if (!Directory.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/rotatethumbnail/")))
                Utils.CreateDir(Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/rotatethumbnail/"));

            //如果版块idlist设置为空，则默认读取所有可见板块的idlist
            string allforums = Forums.GetVisibleForum(ext2, groupid, ipaddress);
            string forumList = string.IsNullOrEmpty(forumHotItemInfo.Forumlist) ? allforums : forumHotItemInfo.Forumlist;

            //对于最新图片瀑布流页面，只允许默认或者 2贴图区，37跳蚤区
            if (section > 0)
            {
                if (fidlist != "")
                {
                    if (fidlist == "2" || fidlist == "37")
                    {
                        forumList = fidlist;
                    }
                }
            }

            forumList = "2,37";

            //最新图片分享区侧重推荐
            //if (("," + allforums + ",").IndexOf(",58,") > -1)
            //{
            //    //如果分享区可见，则25%概率只显示分享区(58)内容，其余显示正常数据
            //    int r = PTTools.GetRandomInt(0, 100);
            //    if (r <= 10 && section == -1)
            //    {
            //        forumList = "2";
            //    }
            //    else if (r <= 40 && section == -1 && ext2 > 50)
            //    {
            //        //D//PTLog.InsertSystemLogDebug(PTLog.LogType.DebugTEST, PTLog.LogStatus.Normal, "DEBUG", "58 ONLY **** " + groupid + "--" + allforums);
            //        forumList = "58";
            //    }
            //    else if (ext2 < 51)
            //    {
            //        //不显示分享区内容
            //        forumList = ("," + forumList + ",").Replace(",58,", ",");
            //        forumList = forumList.Substring(1, forumList.Length - 2);
            //    }
            //    //D//PTLog.InsertSystemLogDebug(PTLog.LogType.DebugTEST, PTLog.LogStatus.Normal, "DEBUG", "NORMAL ---- " + groupid + "--" + allforums);
            //}
            //else
            //{
            //    //分享区不可见，不显示所有分享区的图片
            //    if (forumList.Length > 2)
            //    {
            //        forumList = ("," + forumList + ",").Replace(",58,", ",");
            //        forumList = forumList.Substring(1, forumList.Length - 2);
            //    }
            //    //D//PTLog.InsertSystemLogDebug(PTLog.LogType.DebugTEST, PTLog.LogStatus.Normal, "DEBUG", "HIDDEN #### " + groupid + "--" + allforums);
            //}


            DataTable dt;
            if(section == -1)
                dt = HotImages(forumHotItemInfo.Dataitemcount, forumHotItemInfo.Cachetimeout, forumHotItemInfo.Sorttype, forumHotItemInfo.Id, forumList, forumHotItemInfo.Enabled);
            else
                dt = HotImagesWaterFall(section, forumHotItemInfo.Cachetimeout, forumHotItemInfo.Sorttype, forumHotItemInfo.Id, forumList, forumHotItemInfo.Enabled);

            if (dt == null) return "";

            foreach (DataRow dr in dt.Rows)
            {
                int tid = TypeConverter.ObjectToInt(dr["tid"]);
                string fileName = dr["filename"].ToString().Trim();
                string title = dr["title"].ToString().Trim();

                title = Utils.JsonCharFilter(title).Replace("'", "\\'");

                if (fileName.StartsWith("http://"))
                {
                    DeleteCacheImageFile();
                    Thumbnail.MakeRemoteThumbnailImage(fileName, Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/rotatethumbnail/r_" + Utils.GetFilename(fileName)), 360, 240);
                    hotImagesArray.Append("{");
                    hotImagesArray.AppendFormat(imagesItemTemplate, title, "cache/rotatethumbnail/r_" + Utils.GetFilename(fileName), Urls.ShowTopicAspxRewrite(tid, 0));
                    hotImagesArray.Append("},");
                    continue;
                }
                //图片文件名称
                string fullFileName = BaseConfigs.GetForumPath + "upload/" + fileName.Replace('\\', '/').Trim();
                //图片缩略后的名称
                string thumbnailFileName = "cache/rotatethumbnail/r_" + Utils.GetFilename(fullFileName);

                if (!File.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath + thumbnailFileName)) && File.Exists(Utils.GetMapPath(fullFileName)))
                {
                    DeleteCacheImageFile();
                    Thumbnail.MakeThumbnailImage(Utils.GetMapPath(fullFileName), Utils.GetMapPath(BaseConfigs.GetForumPath + thumbnailFileName), 360, 240);
                }
                if (section == -1)
                {
                    hotImagesArray.Append("{");
                    hotImagesArray.AppendFormat(imagesItemTemplate, title, "cache/rotatethumbnail/r_" + Utils.GetFilename(fullFileName), Urls.ShowTopicAspxRewrite(tid, 0));
                    hotImagesArray.Append("},");
                }
                else
                {
                    hotImagesArray.AppendFormat(waterfallTemplate, title, "cache/rotatethumbnail/r_" + Utils.GetFilename(fullFileName), Urls.ShowTopicAspxRewrite(tid, 0));
                }

            }

            dt.Dispose();

            if (section == -1) return "[" + hotImagesArray.ToString().TrimEnd(',') + "]";
            else return hotImagesArray.ToString();
        }

        /// <summary>
        /// 返回删除了图片附件
        /// </summary>
        /// <param name="message">帖子内容</param>
        /// <param name="length">截取内容的长度</param>
        /// <returns></returns>
        public static string RemoveUbb(string message, int length)
        {
            message = Regex.Replace(message, @"\[attachimg\](\d+)(\[/attachimg\])*", "{图片}", RegexOptions.IgnoreCase);
            message = Regex.Replace(message, @"\[img\]\s*([^\[\<\r\n]+?)\s*\[\/img\]", "{图片}", RegexOptions.IgnoreCase);
            message = Regex.Replace(message, @"\[img=(\d{1,4})[x|\,](\d{1,4})\]\s*([^\[\<\r\n]+?)\s*\[\/img\]", "{图片}", RegexOptions.IgnoreCase);
            message = Regex.Replace(message, @"\[attach\](\d+)(\[/attach\])*", "{附件}", RegexOptions.IgnoreCase);
            //隐藏内容有两种UBB格式，正则需要区分，两种正则不会冲突
            message = Regex.Replace(message, @"\s*\[hide\][\n\r]*([\s\S]+?)[\n\r]*\[\/hide\]\s*", "{隐藏内容}", RegexOptions.IgnoreCase);
            message = Regex.Replace(message, @"\s*\[hide=(\d+?)\][\n\r]*([\s\S]+?)[\n\r]*\[\/hide\]\s*", "{隐藏内容}", RegexOptions.IgnoreCase);

            if (message.IndexOf("[free]") > -1)
            {
                Match match = Regex.Match(message, @"\s*\[free\][\n\r]*([\s\S]+?)[\n\r]*\[\/free\]\s*", RegexOptions.IgnoreCase);
                message = match.Groups[0] != null && match.Groups[0].Value != "" ? match.Groups[0].Value : message;
            }
            return Utils.GetSubString(Utils.ClearUBB(Utils.RemoveHtml(message)).Replace("{", "[").Replace("}", "]"), length, "......");
        }
        private static void DeleteCacheImageFile()
        {
            FileInfo[] files = new DirectoryInfo(Utils.GetMapPath(BaseConfigs.GetForumPath + "cache/rotatethumbnail/")).GetFiles();
            //如果缓存文件夹cache/rotatethumbnail 下的文件大于100个，则删除最后100个
            if (files.Length > 1200)
            {
                Attachments.QuickSort(files, 0, files.Length - 1);

                for (int i = files.Length - 1; i >= 1100; i--)
                {
                    try
                    {
                        files[i].Delete();
                    }
                    catch
                    { }
                }
            }

        }
    }
}