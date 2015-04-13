using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Common.Generic;

namespace Discuz.Forum
{
    /// <summary>
    /// 在线用户操作类
    /// </summary>
    public class OnlineUsers
    {
        private static object SynObject = new object();
        /// <summary>
        /// 全局静态变量，记录游客用的userid，始终获取最小的（为负数）
        /// </summary>
        private static int minonlineuserid = 0;

        /// <summary>
        /// 获得在线用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        public static int GetOnlineAllUserCount()
        {
            int onlineUserCountCacheMinute = GeneralConfigs.GetConfig().OnlineUserCountCacheMinute;
            if (onlineUserCountCacheMinute == 0)
                return Discuz.Data.OnlineUsers.GetOnlineAllUserCount();

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            int onlineAllUserCount = TypeConverter.ObjectToInt(cache.RetrieveObject("/Forum/OnlineUserCount"));
            if (onlineAllUserCount != 0)
                return onlineAllUserCount;

            onlineAllUserCount = Discuz.Data.OnlineUsers.GetOnlineAllUserCount();
            //Discuz.Cache.ICacheStrategy ics = new RssCacheStrategy();
            //ics.TimeOut = onlineUserCountCacheMinute * 60;
            //cache.LoadCacheStrategy(ics);
            cache.AddObject("/Forum/OnlineUserCount", onlineAllUserCount, onlineUserCountCacheMinute * 60);
            //cache.LoadDefaultCacheStrategy();
            return onlineAllUserCount;
        }

        /// <summary>
        /// 返回缓存中在线用户总数
        /// </summary>
        /// <returns>缓存中在线用户总数</returns>
        public static int GetCacheOnlineAllUserCount()
        {
            int count = TypeConverter.StrToInt(Utils.GetCookie("onlineusercount"), 0);
            if (count == 0)
            {
                count = OnlineUsers.GetOnlineAllUserCount();
                Utils.WriteCookie("onlineusercount", count.ToString(), 3);
            }
            return count;
        }

        /// <summary>
        /// 清理之前的在线表记录(本方法在应用程序初始化时被调用)
        /// </summary>
        /// <returns></returns>
        public static int InitOnlineList()
        {
            return Discuz.Data.OnlineUsers.CreateOnlineTable();
        }

        /// <summary>
        /// 复位在线表, 如果系统未重启, 仅是应用程序重新启动, 则不会重新创建
        /// </summary>
        /// <returns></returns>
        public static int ResetOnlineList()
        {
            try
            {

                // 如果距离现在系统运行时间小于10分钟
                if (System.Environment.TickCount < 600000 && System.Environment.TickCount > 0)
                    return Discuz.Data.OnlineUsers.CreateOnlineTable();

                return -1;
            }
            catch
            {
                try
                {
                    return Discuz.Data.OnlineUsers.CreateOnlineTable();
                }
                catch
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 获得在线注册用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        public static int GetOnlineUserCount()
        {
            return Discuz.Data.OnlineUsers.GetOnlineUserCount();
        }

        #region 根据不同条件查询在线用户信息


        /// <summary>
        /// 返回在线用户列表
        /// </summary>
        /// <param name="totaluser">全部用户数</param>
        /// <param name="guest">游客数</param>
        /// <param name="user">登录用户数</param>
        /// <param name="invisibleuser">隐身会员数</param>
        /// <returns>线用户列表</returns>
        public static DataTable GetOnlineUserList(int totaluser, out int guest, out int user, out int invisibleuser)
        {
            DataTable dt = Discuz.Data.OnlineUsers.GetOnlineUserListTable();
            int highestonlineusercount = TypeConverter.StrToInt(Statistics.GetStatisticsRowItem("highestonlineusercount"), 1);

            if (totaluser > highestonlineusercount)
            {
                if (Statistics.UpdateStatistics("highestonlineusercount", totaluser.ToString()) > 0)
                {
                    Statistics.UpdateStatistics("highestonlineusertime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Statistics.ReSetStatisticsCache();
                }
            }
            // 统计用户
            //DataRow[] dr = dt.Select("userid>0");
            user = Discuz.Data.OnlineUsers.GetOnlineUserCount();// dr == null ? 0 : dr.Length;

            //统计隐身用户
            if (EntLibConfigs.GetConfig() != null && EntLibConfigs.GetConfig().Cacheonlineuser.Enable)
                invisibleuser = Discuz.Data.OnlineUsers.GetInvisibleOnlineUserCount();
            else
            {
                DataRow[] dr = dt.Select("invisible=1");
                invisibleuser = dr == null ? 0 : dr.Length;
            }
            //统计游客
            guest = totaluser > user ? totaluser - user : 0;

            //返回当前版块的在线用户表
            return dt;
        }
        #endregion


        /// <summary>
        /// 返回在线用户图例
        /// </summary>
        /// <returns>在线用户图例</returns>
        private static DataTable GetOnlineGroupIconTable()
        {
            lock (SynObject)
            {
                Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
                DataTable dt = cache.RetrieveObject("/Forum/OnlineIconTable") as DataTable;

                if (dt == null)
                {
                    dt = Discuz.Data.OnlineUsers.GetOnlineGroupIconTable();
                    cache.AddObject("/Forum/OnlineIconTable", dt);
                }
                return dt;
            }
        }

        /// <summary>
        /// 返回用户组图标
        /// </summary>
        /// <param name="groupid">用户组</param>
        /// <returns>用户组图标</returns>
        public static string GetGroupImg(int groupid)
        {
            string img = "";
            DataTable dt = GetOnlineGroupIconTable();
            // 如果没有要显示的图例类型则返回""
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    // 图例类型初始为:普通用户
                    // 如果有匹配的则更新为匹配的图例
                    if ((int.Parse(dr["groupid"].ToString()) == 0 && img == "") || (int.Parse(dr["groupid"].ToString()) == groupid))
                    {
                        img = "<img src=\"" + BaseConfigs.GetForumPath + "images/groupicons/" + dr["img"].ToString() + "\" />";
                    }
                }
            }
            return img;
        }

        #region 查看指定的某一用户的详细信息
        public static OnlineUserInfo GetOnlineUser(int olid)
        {
            return Discuz.Data.OnlineUsers.GetOnlineUser(olid);
        }

        /// <summary>
        /// 获得指定用户的详细信息
        /// </summary>
        /// <param name="userid">在线用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>用户的详细信息</returns>
        private static OnlineUserInfo GetOnlineUser(int userid, string password)
        {
            //【BT修改】增加password null判断
            if (Utils.StrIsNullOrEmpty(password)) return null;
            return Discuz.Data.OnlineUsers.GetOnlineUser(userid, password);
        }

        /// <summary>
        /// 获得指定用户的详细信息（此函数中userid无效，一律查找userid小于零的用户，即游客）
        /// </summary>
        /// <returns>用户的详细信息</returns>
        private static OnlineUserInfo GetOnlineUserByIP(int userid, string ip)
        {
            return Discuz.Data.OnlineUsers.GetOnlineUserByIP(userid, ip);
        }
        /// <summary>
        /// 获得指定用户的详细信息（此函数中userid无效，一律查找userid小于零的用户，即游客）
        /// </summary>
        /// <returns>用户的详细信息</returns>
        public static OnlineUserInfo GetOnlineUserByRkey(int userid, string rkey)
        {
            return Discuz.Data.OnlineUsers.GetOnlineUserByRkey(userid, rkey);
        }

        /// <summary>
        /// 检查在线用户验证码是否有效
        /// </summary>
        /// <param name="olid">在组用户ID</param>
        /// <param name="verifycode">验证码</param>
        /// <returns>在组用户ID</returns>
        public static bool CheckUserVerifyCode(int olid, string verifycode)
        {
            return Discuz.Data.OnlineUsers.CheckUserVerifyCode(olid, verifycode, ForumUtils.CreateAuthStr(5, false));
        }

        #endregion

        #region 添加新的在线用户

        /// <summary>
        /// Cookie中没有用户ID或则存的的用户ID无效时在在线表中增加一个游客.
        /// </summary>
        public static OnlineUserInfo CreateGuestUser(int timeout, string ip)
        {
            OnlineUserInfo onlineuserinfo = new OnlineUserInfo();

            

            onlineuserinfo.Username = "游客";
            onlineuserinfo.Nickname = "游客";
            onlineuserinfo.Password = "";
            onlineuserinfo.Groupid = 7;
            onlineuserinfo.Olimg = GetGroupImg(7);
            onlineuserinfo.Adminid = 0;
            onlineuserinfo.Invisible = 0;
            onlineuserinfo.Lastposttime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastpostpmtime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastsearchtime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastupdatetime = Utils.GetDateTime();
            onlineuserinfo.Action = 0;
            onlineuserinfo.Lastactivity = 0;
            onlineuserinfo.Verifycode = ForumUtils.CreateAuthStr(5, false);

            onlineuserinfo.RKey = PTTools.GetRandomString(10, true);

            if (Utils.StrIsNullOrEmpty(ip))
            {
                onlineuserinfo.Ip = DNTRequest.GetIP();
            }
            else
            {
                onlineuserinfo.Ip = ip;
            }

            //插入游客的锁定，更改到自减处
            //lock (SynObject)
            {
                //此处需要保证userid不重复，已登录用户使用自己的userid，未登录游客使用递减负数
                //如何防止这个递减的负数重复？
                //①【数据库中设置userid为唯一键】，即可防止重复，在发生问题时重复获取
                //   由于插入游客的频率不会太高，这个异常处理是否是可以接受的？
                //② 对【minonlineuserid自减】进行lock？
                if (minonlineuserid > -1)
                {
                    minonlineuserid = DatabaseProvider.GetInstance().GetMinOnlneUserid();
                    if (minonlineuserid > -1) minonlineuserid = -1;
                }

                //赋值后自减
                lock (SynObject)
                {
                    onlineuserinfo.Userid = --minonlineuserid;
                }
                try
                {
                    onlineuserinfo.Olid = Discuz.Data.OnlineUsers.CreateOnlineUserInfo(onlineuserinfo, timeout);
                }
                catch
                {
                    //onlineuserinfo.Olid = OnlineUsers.GetOnlineUserByIP()


                	//插入游客异常，可能原因为minonlineuserid数值不对？
                    minonlineuserid = DatabaseProvider.GetInstance().GetMinOnlneUserid();
                    lock (SynObject)
                    {
                        onlineuserinfo.Userid = --minonlineuserid;
                    }
                    try
                    {
                        onlineuserinfo.Olid = Discuz.Data.OnlineUsers.CreateOnlineUserInfo(onlineuserinfo, timeout);
                    }
                    catch
                    {
                        onlineuserinfo.Olid = -1;
                    }
                } 
            }

            //下发访客Rkey
            System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = onlineuserinfo.RKey;
            System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;
            System.Web.HttpContext.Current.Response.Cookies["rkey"].HttpOnly = true;

            return onlineuserinfo;
        }


        /// <summary>
        /// 增加一个会员信息到在线列表中。用户login.aspx或在线用户信息超时,但用户仍在线的情况下重新生成用户在线列表
        /// Rkey将被重置
        /// </summary>
        /// <param name="uid"></param>
        private static OnlineUserInfo CreateUser(int uid, int timeout, string ip, string rkeyexpire, int mode)
        {
            OnlineUserInfo onlineuserinfo = new OnlineUserInfo();
            if (uid > 0)
            {
                ShortUserInfo ui = Users.GetShortUserInfo(uid);
                if (ui != null)
                {
                    onlineuserinfo.Userid = uid;
                    onlineuserinfo.Username = ui.Username.Trim();
                    onlineuserinfo.Nickname = ui.Nickname.Trim();
                    onlineuserinfo.Password = ui.Password.Trim();
                    
                    //onlineuserinfo.RKey = PTUsers.GetUserRKey(uid);
                    //Rkey调整为每次在线均被重置
                    if (mode == 0)
                    {
                        onlineuserinfo.RKey = PTTools.GetRandomString(10, true);
                    }
                    else
                    {
                        onlineuserinfo.RKey = "CN_" + PTTools.GetRandomString(7, true);
                    }
                    
                    onlineuserinfo.RkeyExpire = rkeyexpire;
                    onlineuserinfo.RkeyExpireTime = DateTime.Now.AddMinutes(5);
                    
                    onlineuserinfo.Groupid = short.Parse(ui.Groupid.ToString());
                    onlineuserinfo.Olimg = GetGroupImg(short.Parse(ui.Groupid.ToString()));
                    onlineuserinfo.Adminid = short.Parse(ui.Adminid.ToString());
                    onlineuserinfo.Invisible = short.Parse(ui.Invisible.ToString());
                    if (Utils.StrIsNullOrEmpty(ip))
                    {
                        onlineuserinfo.Ip = DNTRequest.GetIP();
                    }
                    else
                    {
                        onlineuserinfo.Ip = ip;
                    }
                    onlineuserinfo.Lastposttime = "1900-1-1 00:00:00";
                    onlineuserinfo.Lastpostpmtime = "1900-1-1 00:00:00";
                    onlineuserinfo.Lastsearchtime = "1900-1-1 00:00:00";
                    onlineuserinfo.Lastupdatetime = Utils.GetDateTime();
                    onlineuserinfo.LastCheckInTime = ui.LastCheckInTime;
                    onlineuserinfo.LastCheckInCount = ui.LastCheckInCount;
                    onlineuserinfo.Action = 0;
                    onlineuserinfo.Lastactivity = 0;
                    onlineuserinfo.Verifycode = ForumUtils.CreateAuthStr(5, false);

                    int newPms = PrivateMessages.GetPrivateMessageCount(uid, 0, 1);
                    int newNotices = Notices.GetNewNoticeCountByUid(uid);

                    onlineuserinfo.Newpms = short.Parse(newPms > 1000 ? "1000" : newPms.ToString());
                    onlineuserinfo.Newnotices = short.Parse(newNotices > 1000 ? "1000" : newNotices.ToString());
                    onlineuserinfo.PMSound = ui.Pmsound;
                    //onlineuserinfo.Newfriendrequest = short.Parse(Friendship.GetUserFriendRequestCount(uid).ToString());
                    //onlineuserinfo.Newapprequest = short.Parse(ManyouApplications.GetApplicationInviteCount(uid).ToString());

                    if (DNTRequest.GetPageName() == "sessionajax.aspx")
                    {
                        onlineuserinfo.Olid = -1001;
                        return onlineuserinfo;
                    }


                    try
                    {
                        //插入记录
                        int curolid = OnlineUsers.GetOlidByUid(uid);
                        if (curolid > 0)
                        {
                            PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "DelOnLineUser_New", string.Format("用户{0}再次登陆，删除当前在线记录：{1}", uid, curolid));
                            Discuz.Data.OnlineUsers.DeleteRows(curolid);
                        }
                        onlineuserinfo.Olid = Discuz.Data.OnlineUsers.CreateOnlineUserInfo(onlineuserinfo, timeout);
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(200);
                        PTLog.InsertSystemLog(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "InsOnLineUser_Fail", string.Format("插入用户在线记录失败：{0} -IP:{1}", uid, onlineuserinfo.Ip));

                        try
                        {
                            //尝试再次插入记录
                            int curolid = OnlineUsers.GetOlidByUid(uid);
                            if (curolid > 0)
                            {
                                PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "DelOnLineUser_New", string.Format("用户{0}再次登陆，删除当前在线记录2：{1}", uid, curolid));
                                Discuz.Data.OnlineUsers.DeleteRows(curolid);
                            }
                            onlineuserinfo.Olid = Discuz.Data.OnlineUsers.CreateOnlineUserInfo(onlineuserinfo, timeout);
                        }
                        catch
                        {
                            PTLog.InsertSystemLog(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "InsOnLineUser_Fail", string.Format("插入用户在线记录失败2：{0} -IP:{1}", uid, onlineuserinfo.Ip));
                            onlineuserinfo.Olid = -1;
                        } 
                    }


                    //下发Rkey
                    System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = onlineuserinfo.RKey;
                    System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;
                    System.Web.HttpContext.Current.Response.Cookies["rkey"].HttpOnly = true;

                    //////////////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////
                    //【BT修改】取消此功能
                    
                    //给管理人员发送关注通知
                    //if (ui.Adminid > 0 && ui.Adminid < 4)
                    //{
                    //    if (Discuz.Data.Notices.ReNewNotice((int)NoticeType.AttentionNotice, ui.Uid) == 0)
                    //    {
                    //        NoticeInfo ni = new NoticeInfo();
                    //        ni.New = 1;
                    //        ni.Note = "请及时查看<a href=\"modcp.aspx?operation=attention&forumid=0\">需要关注的主题</a>";
                    //        ni.Postdatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //        ni.Type = NoticeType.AttentionNotice;
                    //        ni.Poster = "";
                    //        ni.Posterid = 0;
                    //        ni.Uid = ui.Uid;
                    //        Notices.CreateNoticeInfo(ni);
                    //    }
                    //}
                    
                    //【END BT修改】
                    //////////////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////

                    Discuz.Data.OnlineUsers.SetUserOnlineState(uid, 1);

                    HttpCookie cookie = HttpContext.Current.Request.Cookies["dnt"];
                    if (cookie != null)
                    {
                        cookie.Values["tpp"] = ui.Tpp.ToString();
                        cookie.Values["ppp"] = ui.Ppp.ToString();
                        if (HttpContext.Current.Request.Cookies["dnt"]["expires"] != null)
                        {
                            int expires = TypeConverter.StrToInt(HttpContext.Current.Request.Cookies["dnt"]["expires"].ToString(), 0);
                            if (expires > 0)
                            {
                                cookie.Expires = DateTime.Now.AddMinutes(TypeConverter.StrToInt(HttpContext.Current.Request.Cookies["dnt"]["expires"].ToString(), 0));
                            }
                        }
                    }

                    string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
                    if (!Utils.StrIsNullOrEmpty(cookieDomain) && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && ForumUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
                        cookie.Domain = cookieDomain;
                    HttpContext.Current.Response.AppendCookie(cookie);
                }
                else
                {
                    onlineuserinfo = CreateGuestUser(timeout, ip);
                }
            }
            else
            {
                onlineuserinfo = CreateGuestUser(timeout, ip);
            }




            return onlineuserinfo;
        }



        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
        //【BT修改】添加 cngi_login
        
        ///// <summary>
        ///// 用户在线信息维护。判断当前用户的身份(会员还是游客),是否在在线列表中存在,如果存在则更新会员的当前动,不存在则建立.
        ///// </summary>
        ///// <param name="passwordkey">用户密码</param
        ///// <param name="timeout">在线超时时间</param>
        //public static OnlineUserInfo UpdateInfo(string passwordkey, int timeout, bool cngi_login, string cngi_ip)
        //{
        //    return UpdateInfo(passwordkey, timeout, -1, "", cngi_login, cngi_ip);
        //}


        //原始，保留
        /// <summary>
        /// 用户在线信息维护。判断当前用户的身份(会员还是游客),是否在在线列表中存在,如果存在则更新会员的当前动,不存在则建立.
        /// 使用此函数，将从cookie中获取uid、password信息。
        /// </summary>
        /// <param name="passwordkey">用户密码</param
        /// <param name="timeout">在线超时时间</param>
        public static OnlineUserInfo UpdateInfo(string passwordkey, int timeout)
        {
            string ip = DNTRequest.GetIP();
            int userid = TypeConverter.StrToInt(ForumUtils.GetCookie("userid"), -1);
            string password = ForumUtils.GetCookiePassword(passwordkey);
            return UpdateInfo(passwordkey, timeout, ip, userid, password, false);
        }

        /// <summary>
        /// 用户在线信息维护。判断当前用户的身份(会员还是游客),是否在在线列表中存在,如果存在则更新会员的当前动,不存在则建立.
        /// 传递uid和passwd，此函数在login和cngilogin页面调用，调用时cookie信息可能还未更新，因此需要传递uid和passwd
        /// 此处的passwd是MD5密码，非DES加密的
        /// </summary>
        /// <param name="passwordkey">论坛passwordkey</param>
        /// <param name="timeout">在线超时时间</param>
        /// <param name="passwd">MD5密码，非DES加密的</param>
        public static OnlineUserInfo UpdateInfo(string passwordkey, int timeout, int uid, string passwd)
        {
            string ip = DNTRequest.GetIP();
            int userid = TypeConverter.StrToInt(ForumUtils.GetCookie("userid"), uid);

            //原来DNT的密码更新逻辑：密码为空，则从cookie获取MD5密码，否则，认为密码是原始的DES加密的MD5密码
            //string password = (Utils.StrIsNullOrEmpty(passwd) ? ForumUtils.GetCookiePassword(passwordkey) : ForumUtils.GetCookiePassword(passwd, passwordkey));
            
            //新的函数：密码为空，从cookie获取，否则直接使用passwd作为MD5密码
            string password = (Utils.StrIsNullOrEmpty(passwd) ? ForumUtils.GetCookiePassword(passwordkey) : passwd);
            return UpdateInfo(passwordkey, timeout, ip, userid, password, false);
        }



        private static object SynObjectCreateOnlineUser = new object();
        private static object SynObjectUpdateOnlineRkey = new object();
        /// <summary>
        /// 获取用户olid和olineinfo
        /// 用户在线信息维护。判断当前用户的身份(会员还是游客),是否在在线列表中存在,如果存在则更新会员的当前动,不存在则建立.
        /// </summary>
        /// <param name="passwordkey">论坛passwordkey</param>
        /// <param name="timeout">在线超时时间</param>
        /// <param name="passwd">用户密码（MD5）</param>
        /// <param name="ip"></param>
        /// <param name="userid"></param>
        /// <param name="cngi_login"></param>
        /// <param name="cngi_ip"></param>
        /// <returns></returns>
        public static OnlineUserInfo UpdateInfo(string passwordkey, int timeout, string ip, int userid, string password, bool cngi_login)
        {
            //关于此处之前版本需要lock的原因：
            //为保证online表用户的唯一性，防止同时修改数据,lock为全局锁，保证此段代码全局不会同时执行，防止插入重复的游客记录或者在线用户记录
            // lock ----> 从online表读取数据 ---->   NULL  ----> 检测cookie密码   ---->  正确 ----> 用户加入online
            //      |                        |                                    |
            //      |                        |                                    ---->  错误 ----> 创建游客
            //      |                        ---->   存在userid记录  --> 检测cookie密码  ---> 正确 --->加入online
            //      |                                                                    |
            //      |                                                                    --->  错误 ---> 创建游客
            //      ----> cookie中不存在用户记录 ->  创建游客                                                                    
            //                                                                           
            //原有系统，游客的userid均为-1，使用ip区分不同游客，相同ip的用户共用一个游客olid，造成了部分用户验证码刷新不正常
            //尤其是使用ipv6访问的用户，因为ipv6地址被截断，大量重复。
            //原有系统允许同一个账号多地点同时登陆，共用一个olid（同样会造成验证码问题），只是在不停的更新用户ip
            //
            //不在依照ip查找游客，这会导致游客的验证码问题？？？ 
            //
 
            

            //lock (SynObject)
            //{
                OnlineUserInfo onlineuser = new OnlineUserInfo();
                password = (Utils.StrIsNullOrEmpty(password) ? ForumUtils.GetCookiePassword(passwordkey) : password);
                int cookie_uid = userid;
                          
                // 如果密码非Base64编码字符串则怀疑被非法篡改, 直接置身份为游客。CNGI登陆不检测此处password
                if (!cngi_login && userid > 0 && (password.Length == 0 || !Utils.IsBase64String(password)))
                    userid = -1;

                if (userid > 0) //Cookie中存在用户id或者用户已经CNGI登录
                {
                    onlineuser = GetOnlineUser(userid, password);
                    //更新流量统计
                    if (!DNTRequest.GetPageName().EndsWith("ajax.aspx") && GeneralConfigs.GetConfig().Statstatus == 1)
                        Stats.UpdateStatCount(false, onlineuser != null);



                    //当前用户未在线
                    if (onlineuser == null) 
                    {
                        //判断Cookie中的密码是否正确
                        int ckuserid = PTUsers.CheckCookiePassword(userid, password);
                        string rkeyold = PTUsers.GetUserRKey(userid);

                        if (ckuserid > 0 && Utils.GetCookie("rkey") == rkeyold)
                        {
                            #region 用户当前不在线，Password正确，Rkey正确，生成在线用户信息

                            //密码正确，返回在线信息 //Discuz.Data.OnlineUsers.DeleteRowsByIP(ip); //????????为什么    
                            
                            int createuserok = 0;

                            //存在并发问题的更新
                            lock (SynObjectCreateOnlineUser)
                            {
                                onlineuser = GetOnlineUser(userid, password);
                                if (onlineuser == null)
                                {
                                    createuserok = 1;
                                    onlineuser = CreateUser(ckuserid, timeout, ip, rkeyold, 0);
                                    
                                    //sessionajax页面，访问成功时，直接返回（并没有加入online表）
                                    if (onlineuser.Olid == -1001) return onlineuser;
                                }
                            }

                            if (createuserok == 1)
                            {
                                if (onlineuser == null)
                                {
                                    PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "CreateFail",
                                        string.Format("UID:{0} -IP:{1} -ORKEY:{2} -NRKEY:{3}", userid, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid)));

                                    AddUserLoginRecord(userid, "[CREATE_FAIL]", onlineuser.Groupid, ip, 13, 2, password, Utils.GetCookie("rkey"));

                                    //返回游客信息
                                    return GetGuestOlinfo(timeout, ip);
                                }
                                else
                                {
                                    //更新user表的Rkey记录
                                    PTUsers.UpdateUserRKey(ckuserid, onlineuser.RKey);
                                }

                                #region CNGI RKey的额外校验处理

                                //CNGI登陆的额外校验
                                //若RKey开头为“CN_”，则必须为CNGI登陆（==10判断为保证substring不出错）
                                if (Utils.GetCookie("rkey").Length != 10 || Utils.GetCookie("rkey").Substring(0, 3) == "CN_")
                                {
                                    if (!cngi_login)
                                    {
                                        AddUserLoginRecord(userid, onlineuser.Username, onlineuser.Groupid, ip, 13, 2, password, Utils.GetCookie("rkey"));

                                        ForumUtils.ClearUserCookie();
                                        Utils.WriteCookie(Utils.GetTemplateCookieName(), "", -999999);
                                        System.Web.HttpCookie cookie = new System.Web.HttpCookie("dntadmin");
                                        cookie.HttpOnly = true;
                                        System.Web.HttpContext.Current.Response.AppendCookie(cookie);

                                        //返回游客
                                        onlineuser = GetOnlineUserByRkey(-1, Utils.GetCookie("rkey"));

                                        if (onlineuser == null)
                                        {
                                            onlineuser = CreateGuestUser(timeout, ip);
                                            return onlineuser;
                                        }
                                        else
                                            return onlineuser;
                                    }
                                    else
                                    {
                                        AddUserLoginRecord(userid, onlineuser.Username, onlineuser.Groupid, ip, 13, 1, password, Utils.GetCookie("rkey"));
                                    }
                                }

                                #endregion

                                //密码正确，Cookie/CNGI登陆成功//之前要先创建onlineuser，不然是null啊
                                AddUserLoginRecord(userid, onlineuser.Username, onlineuser.Groupid, ip, 11, 1, password, Utils.GetCookie("rkey"));

                                return onlineuser;
                            }
                            else if(createuserok == 0)
                            {
                                PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "ConCurent_Create",
                                        string.Format("UID:{0} -IP:{1} -ORKEY:{2} -NRKEY:{3} --PAGE:{4}", userid, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid), DNTRequest.GetPageName()));


                            }
                            
                            #endregion
                        }
                        else
                        {
                            #region 用户当前不在线，Password或Rkey错误，则置为游客
                            //密码错误或Rkey失效

                            #region CNGI登陆的情况

                            if (cngi_login)
                            {
                                //CNGI登陆，创建用户前需要删除online表中原有此uid的记录，因为CNGI登陆password值有可能为空
                                Discuz.Data.OnlineUsers.DeleteRows(Discuz.Data.OnlineUsers.GetOlidByUid(cookie_uid));

                                //更新Cookie和RKey
                                ForumUtils.ClearUserCookie();
                                ForumUtils.WriteUserCookie(cookie_uid, 0, passwordkey, 0, -1);

                                onlineuser = CreateUser(cookie_uid, timeout, ip, rkeyold, 1);

                                if (onlineuser == null)
                                {
                                    AddUserLoginRecord(cookie_uid, "[CNGI_NULL]", -1, ip, 13, 3, password, Utils.GetCookie("rkey"));
                                    onlineuser = CreateGuestUser(timeout, ip);
                                    return onlineuser;
                                }

                                //CNGI正常登陆，第一次时的情况，cookie中没有password
                                //CNGI已经登录，若此时用户在其他域名再次登录，并等待20分钟系统删除在线用户后访问，将造成此情况，记为CNGI重新登陆
                                AddUserLoginRecord(cookie_uid, onlineuser.Username, onlineuser.Groupid, ip, 13, 3, password, Utils.GetCookie("rkey"));

                                return onlineuser;
                            }

                            #endregion

                            //sessionajax页面特殊处理，不清空用户cookie
                            //因为有很多浏览器访问sessionajax页面不带cookie？？？？
                            if (DNTRequest.GetPageName() == "sessionajax.aspx")
                            {
                                if (ckuserid > 0)
                                {
                                    //Rkey错误
                                    AddUserLoginRecord(cookie_uid, "[RKEY_EXPIRE]", 0, ip, 11, 2, password, Utils.GetCookie("rkey"));

                                    PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Detail, "Offline_PassRkey_Fail",string.Format("UID:{0} -IP:{1} -ORKEY:{2} -NRKEY:{3}", userid, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid)));
                                }
                                else
                                {
                                    //Pass错误
                                    AddUserLoginRecord(cookie_uid, "[PASS_EXPIRE]", 0, ip, 11, 2, password, Utils.GetCookie("rkey"));

                                    PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Detail, "Offline_Password_Fail", string.Format("UID:{0} -IP:{1} -ORKEY:{2} -NRKEY:{3}", userid, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid)));
                                }

                                //返回空白信息
                                return EmptyOlinfo(2048, userid, ip);
                            }
                            else
                            {
                                if (ckuserid > 0)
                                {
                                    //Rkey错误
                                    AddUserLoginRecord(cookie_uid, "[RKEY_EXPIRE_C]", 0, ip, 11, 2, password, Utils.GetCookie("rkey"));

                                    PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Detail, "Offline_RkeyE_CCook", string.Format("UID:{0} -IP:{1} -ORKEY:{2} -NRKEY:{3}", userid, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid)));
                                }
                                else
                                {
                                    //Pass错误
                                    AddUserLoginRecord(cookie_uid, "[PASS_EXPIRE_C]", 0, ip, 11, 2, password, Utils.GetCookie("rkey"));

                                    PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Detail, "Offline_PassE_CCook", string.Format("UID:{0} -IP:{1} -ORKEY:{2} -NRKEY:{3}", userid, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid)));
                                }

                                //清除用户当前的Cookie信息
                                ForumUtils.ClearUserCookie();

                                //返回游客信息
                                return GetGuestOlinfo(timeout, ip);
                            }
                                
                            

                            #endregion
                        }
                    }



                    //当前用户在线
                    if(onlineuser != null)
                    {
                        if (Utils.GetCookie("rkey") == onlineuser.RKey || (Utils.GetCookie("rkey") == onlineuser.RkeyExpire && DateTime.Now < onlineuser.RkeyExpireTime))
                        {
                            #region 在线用户，Rkey正确，或Rkeyexpire正确，获取当前用户信息

                            #region CNGI Cookie确认

                            //若RKey开头为“CN_”，则必须为CNGI登陆（==10判断为保证substring不出错）
                            if (Utils.GetCookie("rkey").Length != 10 || Utils.GetCookie("rkey").Substring(0, 3) == "CN_")
                            {
                                if (!cngi_login)
                                {
                                    //RKey状态为CNGI登陆，但是CNGI登陆失败（如未能获取cngi_name）
                                    AddUserLoginRecord(userid, onlineuser.Username, onlineuser.Groupid, ip, 3, 2, password, Utils.GetCookie("rkey"));

                                    //此处不更新Cookie，因为CNGI登陆经常出现此情况，推测客户访问CNGI的IP更改时，会导致此现象，cngi_name会丢失，需要重新跳转登陆
                                    
                                    //ForumUtils.ClearUserCookie();
                                    //Utils.WriteCookie(Utils.GetTemplateCookieName(), "", -999999);
                                    //System.Web.HttpCookie cookie = new System.Web.HttpCookie("dntadmin");
                                    //cookie.HttpOnly = true;
                                    //System.Web.HttpContext.Current.Response.AppendCookie(cookie);

                                    //返回游客
                                    onlineuser = GetOnlineUserByRkey(-1, Utils.GetCookie("rkey"));
                                    if (onlineuser == null)
                                    {
                                        onlineuser = CreateGuestUser(timeout, ip);
                                        return onlineuser;
                                    }
                                    else
                                        return onlineuser;
                                }
                            }

                            #endregion

                            if (Utils.GetCookie("rkey") == onlineuser.RKey)
                            {
                                //普通登陆方式，使用RKey确认登陆
                                if (cngi_login) AddUserLoginRecord(userid, onlineuser.Username, onlineuser.Groupid, ip, 3, 1, password, Utils.GetCookie("rkey"));
                                else AddUserLoginRecord(userid, onlineuser.Username, onlineuser.Groupid, ip, 1, 1, password, Utils.GetCookie("rkey"));


                                //IP地址变更，重新下发Rkey
                                if (onlineuser.Ip != ip)
                                {
                                    if ((PTTools.IsIPv4(ip) && PTTools.IsIPv6(onlineuser.Ip)) || (PTTools.IsIPv4(onlineuser.Ip) && PTTools.IsIPv6(ip)))
                                    {
                                        //如果IP是在Ipv4和IPv6间变化，暂时不做处理，只更新IP
                                        PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Detail, "IPChange_Keep", string.Format("UID:{0} -OIP:{1} - NIP:{2} -OldRKEY:{3} --PAGE:{4}", userid, onlineuser.Ip, ip, Utils.GetCookie("rkey"), DNTRequest.GetPageName()));
                                    }
                                    else
                                    {
                                        string newreky = PTTools.GetRandomString(10, true);
                                        
                                        //存在并发问题的更新
                                        bool updaterkey = false;
                                        lock (SynObjectUpdateOnlineRkey)
                                        {
                                            onlineuser = onlineuser = GetOnlineUser(userid, password);
                                            if (Utils.GetCookie("rkey") == onlineuser.RKey)
                                            {
                                                updaterkey = true;
                                                PTUsers.SetOnlineRKey(userid, newreky);
                                            } 
                                        }   
                                        
                                        //IP地址更改，更新Rkey信息
                                        if (updaterkey)
                                        {
                                            PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "IPChange_NewReky", string.Format("UID:{0} -OIP:{1} - NIP:{2} -OldRKEY:{3} -NewRKEY:{4}", userid, onlineuser.Ip, ip, Utils.GetCookie("rkey"), newreky));

                                            onlineuser.RKey = newreky;
                                        }
                                        else
                                        {
                                            PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "ConCurent_RkeyU", string.Format("UID:{0} -IP:{1} -ORKEY:{2} -NRKEY:{3}", userid, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid)));
                                        }
                                    }

                                    onlineuser.Ip = ip;
                                    UpdateIP(onlineuser.Olid, ip);
                                }
                            }
                            else
                            {
                                AddUserLoginRecord(userid, "[ExpThru]", onlineuser.Groupid, ip, 1, 1, password, Utils.GetCookie("rkey"));

                                //Rkey过期5分钟内，不检查ip更新
                                PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "RKey_ExpireThru",
                                            string.Format("UID:{0} -OIP:{1} - NIP:{2} -CookieRKEY:{3} -RKEY:{4} -RKEYEX:{5} --PAGE:{6}", userid, onlineuser.Ip, ip, Utils.GetCookie("rkey"), onlineuser.RKey, onlineuser.RkeyExpire, DNTRequest.GetPageName()));

                                if (onlineuser.Ip == ip)
                                {
                                    //如果IP相同、Rkey不同，则继续下发Rkey
                                    System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = onlineuser.RKey;
                                    System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;
                                    System.Web.HttpContext.Current.Response.Cookies["rkey"].HttpOnly = true;
                                }
                            }
                            
                            //返回当前用户信息
                            return onlineuser;

                            #endregion
                        }
                        else
                        {
                            #region 在线用户，Rkey不正确，要求重新登陆

                            //Rkey不同，需要重新登录

                            #region CNGI情况处理

                            if (cngi_login)
                            {
                                // CNGI登录，IP改变，CNGI优先，重新添加随机RKey，用户在CNGI登陆后又在其他域名登陆可导致此情况
                                AddUserLoginRecord(userid, onlineuser.Username, onlineuser.Groupid, ip, 13, 1, password, Utils.GetCookie("rkey"));

                                //更新RKey//CNGI登陆，RKey以“CN_”开头
                                string rkey = "CN_" + PTTools.GetRandomString(7);
                                PTUsers.SetUserRKey(userid, rkey);

                                if (onlineuser.Ip != ip)
                                {
                                    UpdateIP(onlineuser.Olid, ip);
                                    onlineuser.Ip = ip; 
                                }
                                return onlineuser;
                            }

                            #endregion 

                            //由于Rkey错误导致的登陆失败，返回特殊用户信息，供提醒用
                            if (DNTRequest.GetPageName() == "sessionajax.aspx")
                            {
                                //在线过程中RKey错误
                                AddUserLoginRecord(userid, "[RKEY_ERR]", onlineuser.Groupid, ip, 1, 2, password, Utils.GetCookie("rkey"));

                                PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "Online_RkeyFail",string.Format("UID:{0} -OIP:{1} - NIP:{2} -CookieRKEY:{3} -RKEY:{4}", userid, onlineuser.Ip, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid)));

                                //返回空白用户信息
                                return EmptyOlinfo(2048, userid, ip);
                            }
                            else
                            {
                                AddUserLoginRecord(userid, "[RKEY_ERR_C]", onlineuser.Groupid, ip, 1, 2, password, Utils.GetCookie("rkey"));

                                PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "Online_RKeyECCookies",string.Format("UID:{0} -IP:{1} -ORKEY:{2} -NRKEY:{3}", userid, ip, Utils.GetCookie("rkey"), PTUsers.GetUserRKey(userid)));

                                //RKey错误，置为游客
                                ForumUtils.ClearUserCookie();

                                //返回游客信息
                                return GetGuestOlinfo(timeout, ip);
                            }

                            #endregion
                        }

                    }
                    else
                    {
                        //不可能出现的情况
                        //返回空白用户信息
                        AddUserLoginRecord(userid, "[ERR]", -1, ip, 220, 220, password, Utils.GetCookie("rkey"));

                        PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Error, "ERR",  string.Format("UID:{0} -IP:{1} -ORKEY:{2}", userid, ip, Utils.GetCookie("rkey")));

                        return EmptyOlinfo(2048, userid, ip);
                    }
                }
                else
                {
                    #region Cookies中无用户记录，游客

                    //Cookie中不存在用户信息，游客访问
                    string userrkey = System.Web.HttpContext.Current.Request.Cookies["rkey"] !=null ? System.Web.HttpContext.Current.Request.Cookies["rkey"].Value : "";
                    
                    if (userrkey != null && userrkey.Trim().Length == 10)
                        onlineuser = GetOnlineUserByRkey(-1, userrkey.Trim());
                    else onlineuser = null;

                    //更新流量统计
                    if (!DNTRequest.GetPageName().EndsWith("ajax.aspx") && GeneralConfigs.GetConfig().Statstatus == 1)
                        Stats.UpdateStatCount(true, onlineuser != null);

                    //游客访问记录
                    AddUserLoginRecord(0, "NULL", 0, ip, 1, 2, password, Utils.GetCookie("rkey"));

                    //返回游客信息
                    return GetGuestOlinfo(timeout, ip);

                    #endregion
                }

                //onlineuser.Lastupdatetime = Utils.GetDateTime();  为了客户端能够登录注释此句，如有问题再修改。
                //return onlineuser;
            //}
        }

        private static OnlineUserInfo GetGuestOlinfo(int timeout, string ip)
        {

            OnlineUserInfo onlineuser = GetOnlineUserByRkey(-1, Utils.GetCookie("rkey"));
            if (onlineuser == null)
            {
                onlineuser = CreateGuestUser(timeout, ip);
                return onlineuser;
            }
            else
                return onlineuser;

        }

        /// <summary>
        /// 空onlineuserinfo，用于传递错误信息
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        private static OnlineUserInfo EmptyOlinfo(int eid, int uid, string ip)
        {
            OnlineUserInfo onlineuserinfo = new OnlineUserInfo();
            onlineuserinfo.Olid = -eid;
            onlineuserinfo.Userid = uid;
            onlineuserinfo.Username = "[" + eid.ToString() + "]";

            //onlineuserinfo.Username = "游客";
            onlineuserinfo.Nickname = "游客";
            onlineuserinfo.Password = "";
            onlineuserinfo.Groupid = 7;
            onlineuserinfo.Olimg = GetGroupImg(7);
            onlineuserinfo.Adminid = 0;
            onlineuserinfo.Invisible = 0;
            onlineuserinfo.Lastposttime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastpostpmtime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastsearchtime = "1900-1-1 00:00:00";
            onlineuserinfo.Lastupdatetime = Utils.GetDateTime();
            onlineuserinfo.Action = 0;
            onlineuserinfo.Lastactivity = 0;
            onlineuserinfo.Verifycode = ForumUtils.CreateAuthStr(5, false);

            onlineuserinfo.RKey = PTTools.GetRandomString(10, true);

            if (Utils.StrIsNullOrEmpty(ip))
            {
                onlineuserinfo.Ip = DNTRequest.GetIP();
            }
            else
            {
                onlineuserinfo.Ip = ip;
            }

            return onlineuserinfo;
        }


        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
        //【BT修改】添加

        /// <summary>
        /// 添加登陆日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ip"></param>
        /// <param name="type">1.cookie；2.password；3.CNGI + cookie；4. CNGI验证；5.cookie + rkey； 6.CNGI，更新RKey</param>
        /// <param name="ok">1.pass；2.fail</param>
        /// <returns></returns>
        public static int AddUserLoginRecord(int uid, string ip, int type, int ok, string msg)
        {
            string url = "http://" + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.RawUrl;
            string agent = System.Web.HttpContext.Current.Request.UserAgent;
            return PrivateBT.AddUserLoginRecord(uid, ip, type, ok, DateTime.Now, url, agent, msg);
        }
        /// <summary>
        /// 添加登陆日志，type偶数次为https访问....访问方式，Cookie维持1，CNGI维持3，Cookie登陆11，CNGI登陆13，SSO登陆15，密码登陆21，CNGI绑定登陆23，SSO绑定登陆25，后台登陆31，邀请注册登陆41，CNGI注册43，SSO注册45
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ip"></param>
        /// <param name="type">访问方式，Cookie维持1，CNGI维持3，Cookie登陆11，CNGI登陆13，SSO登陆15，密码登陆21，CNGI绑定登陆23，SSO绑定登陆25，后台登陆31，邀请注册登陆41，CNGI注册43，SSO注册45</param>
        /// <param name="result">1.PASS通过；2.FAIL失败</param>
        /// <returns></returns>
        public static int AddUserLoginRecord(int uid, string username, int usergroup, string ip, int type, int result, string md5, string rkey)
        {
            if (type < 0) type = 255;
            if (result < 0) result = 255;
            string domain = System.Web.HttpContext.Current.Request.Url.Host;
            string url = System.Web.HttpContext.Current.Request.RawUrl;
            string agent = System.Web.HttpContext.Current.Request.UserAgent;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTPS"] == "on")
            {
                type += 1;
                domain = "https://" + domain;
            }
            else
            {
                domain = "http://" + domain;
            }

            if (md5.Length > 5) md5 = md5.Substring(0, 5);
            if (rkey.Length > 5) rkey = rkey.Substring(0, 5);

            //由于访问过于频繁，不记录成功的/tools/sessionajax.aspx?t=newpmcount和/tools/sessionajax.aspx?t=newnoticecount || url == "/tools/sessionajax.aspx?t=newnoticecount"
            if (result == 1 && url == "/tools/sessionajax.aspx?t=newpmnoticecount")
                return -2;

            return PrivateBT.AddUserAccessLog(uid, username, usergroup, type, result, DateTime.Now, ip, agent, domain, url, md5, rkey, DNTRequest.IsPost());
        }
        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 检查ip地址是否合法
        /// </summary>
        /// <param name="ip"></param>
        private static void CheckIp(string ip)
        {
            string errmsg = "";
            //判断IP地址是否合法,需要重构
            Discuz.Common.Generic.List<IpInfo> list = Caches.GetBannedIpList();

            foreach (IpInfo ipinfo in list)
            {
                if (ip == (string.Format("{0}.{1}.{2}.{3}", ipinfo.Ip1, ipinfo.Ip2, ipinfo.Ip3, ipinfo.Ip4)))
                {
                    errmsg = "您的ip被封,于" + ipinfo.Expiration + "后解禁";
                    break;
                }

                if (ipinfo.Ip4.ToString() == "*")
                {
                    if ((TypeConverter.StrToInt(ip.Split('.')[0], -1) == ipinfo.Ip1) && (TypeConverter.StrToInt(ip.Split('.')[1], -1) == ipinfo.Ip2) && (TypeConverter.StrToInt(ip.Split('.')[2], -1) == ipinfo.Ip3))
                    {
                        errmsg = "您所在的ip段被封,于" + ipinfo.Expiration + "后解禁";
                        break;
                    }
                }
            }

            if (errmsg != string.Empty)
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + "tools/error.htm?forumpath=" + BaseConfigs.GetForumPath + "&templatepath=default&msg=" + Utils.UrlEncode(errmsg));
        }

        #endregion

        #region 在组用户信息更新

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作</param>
        /// <param name="inid">所在位置代码</param>
        /// <param name="timeout">过期时间</param>
        public static void UpdateAction(int olid, int action, int inid, int timeout)
        {
            // 如果上次刷新cookie间隔小于5分钟, 则不刷新数据库最后活动时间
            if ((timeout < 0) && (Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastolupdate"), Environment.TickCount) < 300000))
                Utils.WriteCookie("lastolupdate", Environment.TickCount.ToString());
            else
                UpdateAction(olid, action, inid);
        }

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作</param>
        /// <param name="inid">所在位置代码</param>
        public static void UpdateAction(int olid, int action, int inid)
        {
            if (GeneralConfigs.GetConfig().Onlineoptimization != 1)
            {
                Discuz.Data.OnlineUsers.UpdateAction(olid, action, inid);
            }
        }


        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作id</param>
        /// <param name="fid">版块id</param>
        /// <param name="forumname">版块名</param>
        /// <param name="tid">主题id</param>
        /// <param name="topictitle">主题名</param>
        /// 
        public static void UpdateAction(int olid, int action, int fid, string forumname, int tid, string topictitle)
        {
            bool isupdate = false;
            forumname = forumname.Length > 40 ? forumname.Substring(0, 37) + "..." : forumname;
            topictitle = topictitle.Length > 40 ? topictitle.Substring(0, 37) + "..." : topictitle;
            if (action == UserAction.PostReply.ActionID || action == UserAction.PostTopic.ActionID)
            {
                if (GeneralConfigs.GetConfig().PostTimeStorageMedia == 0 || Utils.GetCookie("lastposttime") == "")//如果检测到用户的该cookie值为空(即用户禁用cookie)，则需要通过更新数据库在线列表来确保该值的准确性，否则就只更新用户cookie来保证该值的正确性
                    isupdate = true;
                else
                    Utils.WriteCookie("lastposttime", Utils.GetDateTime());
            }
            else if (GeneralConfigs.GetConfig().Onlineoptimization != 1)
            {
                if (System.Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastolupdate"), System.Environment.TickCount) >= 300000) // 如果上次刷新cookie间隔小于5分钟, 则不刷新数据库最后活动时间
                {
                    if (action == UserAction.ShowForum.ActionID || action == UserAction.ShowTopic.ActionID || action == UserAction.ShowTopic.ActionID || action == UserAction.PostReply.ActionID)
                        isupdate = true;
                }
            }
            if (isupdate)
            {
                Discuz.Data.OnlineUsers.UpdateAction(olid, action, fid, forumname, tid, topictitle);
                Utils.WriteCookie("lastolupdate", System.Environment.TickCount.ToString());
                Utils.WriteCookie("lastposttime", Utils.GetDateTime());
            }
        }

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="timeout">超时时间</param>
        private static void UpdateLastTime(int olid, int timeout)
        {
            // 如果上次刷新cookie间隔小于5分钟, 则不刷新数据库最后活动时间
            if ((timeout < 0) && (System.Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastolupdate"), System.Environment.TickCount) < 300000))
                Utils.WriteCookie("lastolupdate", System.Environment.TickCount.ToString());
            else
                Discuz.Data.OnlineUsers.UpdateLastTime(olid);
        }


        /// <summary>
        /// 更新用户最后发短消息时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public static void UpdatePostPMTime(int olid)
        {
            if (GeneralConfigs.GetConfig().Onlineoptimization != 1)
            {
                Discuz.Data.OnlineUsers.UpdatePostPMTime(olid);
            }
        }

        /// <summary>
        /// 更新在线表中指定用户是否隐身
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="invisible">是否隐身</param>
        public static void UpdateInvisible(int olid, int invisible)
        {
            if (GeneralConfigs.GetConfig().Onlineoptimization != 1)
            {
                Discuz.Data.OnlineUsers.UpdateInvisible(olid, invisible);
            }
        }

        /// <summary>
        /// 更新在线表中指定用户的用户密码
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="password">用户密码</param>
        public static void UpdatePassword(int olid, string password)
        {
            Discuz.Data.OnlineUsers.UpdatePassword(olid, password);
        }


        /// <summary>
        /// 更新用户IP地址
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="ip">ip地址</param>
        public static void UpdateIP(int olid, string ip)
        {
            Discuz.Data.OnlineUsers.UpdateIP(olid, ip);
        }

        /// <summary>
        /// 更新用户最后搜索时间
        /// </summary>
        /// <param name="olid">在线id</param>
        //public static void UpdateSearchTime(int olid)
        //{
        //    if (GeneralConfigs.GetConfig().Onlineoptimization != 1)
        //    {
        //        Discuz.Data.OnlineUsers.UpdateSearchTime(olid);
        //    }
        //}

        #endregion

        /// <summary>
        /// 删除在线表中指定在线id的行
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <returns></returns>
        public static int DeleteRows(int olid)
        {
            return Discuz.Data.OnlineUsers.DeleteRows(olid);
        }

        #region 条件编译的方法

        /// <summary>
        /// 返回在线用户列表
        /// </summary>
        /// <param name="totaluser">全部用户数</param>
        /// <param name="guest">游客数</param>
        /// <param name="user">登录用户数</param>
        /// <param name="invisibleuser">隐身会员数</param>
        /// <returns></returns>
        public static Discuz.Common.Generic.List<OnlineUserInfo> GetForumOnlineUserCollection(int forumid, out int totaluser, out int guest, out int user, out int invisibleuser)
        {
            Discuz.Common.Generic.List<OnlineUserInfo> coll = Discuz.Data.OnlineUsers.GetForumOnlineUserCollection(forumid);

            //在线游客
            guest = 0;
            //在线隐身用户
            invisibleuser = 0;
            //当前版块在线总用户数
            totaluser = coll.Count;

            foreach (OnlineUserInfo onlineUserInfo in coll)
            {
                if (onlineUserInfo.Userid < 1)
                    guest++;

                if (onlineUserInfo.Invisible == 1)
                    invisibleuser++;
            }

            //统计用户
            user = totaluser - guest;
            //返回当前版块的在线用户表
            return coll;
        }


        /// <summary>
        /// 返回在线用户列表
        /// </summary>
        /// <param name="totaluser">全部用户数</param>
        /// <param name="guest">游客数</param>
        /// <param name="user">登录用户数</param>
        /// <param name="invisibleuser">隐身会员数</param>
        /// <returns></returns>
        public static Discuz.Common.Generic.List<OnlineUserInfo> GetOnlineUserCollection(out int totaluser, out int guest, out int user, out int invisibleuser)
        {
            Discuz.Common.Generic.List<OnlineUserInfo> coll = Discuz.Data.OnlineUsers.GetOnlineUserCollection();

            //在线注册用户数
            user = 0;
            //在线隐身用户数
            invisibleuser = 0;

            //当在线列表不隐藏游客时,意味'GetOnlineUserCollection()'方法返回了在线表中所有记录
            if (GeneralConfigs.GetConfig().Whosonlinecontract == 0)
                totaluser = coll.Count;
            else
                totaluser = OnlineUsers.GetOnlineAllUserCount();//否则需要重新获取全部用户数

            foreach (OnlineUserInfo onlineUserInfo in coll)
            {
                if (onlineUserInfo.Userid > 0)
                    user++;

                if (onlineUserInfo.Invisible == 1)
                    invisibleuser++;
            }

            if (totaluser > TypeConverter.StrToInt(Statistics.GetStatisticsRowItem("highestonlineusercount"), 1))
            {
                if (Statistics.UpdateStatistics("highestonlineusercount", totaluser.ToString()) > 0)
                {
                    Statistics.UpdateStatistics("highestonlineusertime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Statistics.ReSetStatisticsCache();
                }
            }

            //统计游客
            guest = totaluser > user ? totaluser - user : 0;

            //返回当前版块的在线用户集合
            return coll;
        }

        /// <summary>
        /// 更新在线时间
        /// </summary>
        /// <param name="oltimespan">在线时间间隔</param>
        /// <param name="uid">当前用户id</param>
        public static void UpdateOnlineTime(int oltimespan, int uid)
        {
            //为0代表关闭统计功能
            if (oltimespan != 0)
            {
                if (Utils.StrIsNullOrEmpty(Utils.GetCookie("lastactivity", "onlinetime")))
                    Utils.WriteCookie("lastactivity", "onlinetime", System.Environment.TickCount.ToString());

                //自上次更新数据库中用户在线时间后到当前的时间间隔
                int oltime = System.Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastactivity", "onlinetime"), System.Environment.TickCount);
                if (oltime <= 0 /*TickCount 49天后系统会清零，这会造成该值为负*/ 
                    || oltime >= oltimespan * 60 * 1000)
                {
                    Discuz.Data.OnlineUsers.UpdateOnlineTime(oltimespan, uid);
                    Utils.WriteCookie("lastactivity", "onlinetime", System.Environment.TickCount.ToString());

                    oltime = System.Environment.TickCount - TypeConverter.StrToInt(Utils.GetCookie("lastactivity", "oltime"), System.Environment.TickCount);
                    //判断是否同步oltime (登录后的第一次onlinetime更新的时候或者在线超过oltimespan2倍时间间隔)
                    if (Utils.StrIsNullOrEmpty(Utils.GetCookie("lastactivity", "oltime")) ||
                        oltime <= 0 /*TickCount 49天系统会清零，这会造成该值为负*/ 
                        || oltime >= (2 * oltimespan * 60 * 1000))
                    {
                        Discuz.Data.OnlineUsers.SynchronizeOnlineTime(uid);
                        Utils.WriteCookie("lastactivity", "oltime", System.Environment.TickCount.ToString());
                    }
                }
            }
        }

        #endregion



        /// <summary>
        /// 根据Uid获得Olid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static int GetOlidByUid(int uid)
        {
            return Discuz.Data.OnlineUsers.GetOlidByUid(uid);
        }

        /// <summary>
        /// 删除在线表中Uid的用户
        /// </summary>
        /// <param name="uid">要删除用户的Uid</param>
        /// <returns></returns>
        public static int DeleteUserByUid(int uid)
        {
            return DeleteRows(GetOlidByUid(uid));
        }

        /// <summary>
        /// 更新用户新短消息数
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="count">更新数</param>
        /// <returns></returns>
        public static int UpdateNewPms(int olid, int count)
        {
            return Discuz.Data.OnlineUsers.UpdateNewPms(olid, count);
        }

        /// <summary>
        /// 更新用户新通知数
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="pluscount">增加量</param>
        /// <returns></returns>
        public static int UpdateNewNotices(int olid, int pluscount)
        {
            return Discuz.Data.OnlineUsers.UpdateNewNotices(olid, pluscount);
        }

        /// <summary>
        /// 重新获取用户新通知数，从表中重新查询
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <returns></returns>
        public static int UpdateNewNotices(int olid)
        {
            return Discuz.Data.OnlineUsers.UpdateNewNotices(olid, 0);
        }

        ///// <summary>
        ///// 更新在线表中好友关系请求计数
        ///// </summary>
        ///// <param name="olId">在线id</param>
        ///// <param name="count">增加量</param>
        ///// <returns></returns>
        //public static int UpdateNewFriendsRequest(int olId, int count)
        //{
        //    return Data.OnlineUsers.UpdateNewFriendsRequest(olId, count);
        //}

        ///// <summary>
        ///// 更新在线表中应用请求计数
        ///// </summary>
        ///// <param name="olId">在线id</param>
        ///// <param name="count">更新数</param>
        ///// <returns></returns>
        //public static int UpdateNewApplicationRequest(int olId, int count)
        //{
        //    return Data.OnlineUsers.UpdateNewApplicationRequest(olId, count);
        //}

    }//class end
}
