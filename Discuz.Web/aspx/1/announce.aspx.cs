using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Forum;
using Discuz.Entity;
using Discuz.Data;
using Discuz.Config;
using Discuz.Forum.ScheduledEvents;

namespace Discuz.Web
{
    /// <summary>
    /// 向后台线程传递参数的参数类
    /// </summary>
    public class AnnounceNewThreadParam
    {
        public PTUserInfo btuserinfo = null;
        public PrivateBTPeerInfo peerinfo = null;
        public PrivateBTPeerInfo oldpeerinfo = null;
        public PTSeedinfoShort seedinfo = null;
        public string peer_event = null;
        public decimal peer_left = 0M;
        public decimal peer_uploaded = 0M;
        public decimal peer_downloaded = 0M;
        public bool peer_ipv6tracker = false;

        //public DataTable uploadlist = null;
        //public DataTable downloadlist = null;
    }


    /// <summary>
    /// Tracker实现类
    /// </summary>
    public partial class announce : System.Web.UI.Page, System.Web.SessionState.IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                #region 初始化、负载状态检测和计划任务

#if RELEASETRACKER

                //RELEASETRACKER模式执行负载状态优化
                //负载状态优化，当系统负载高时，跳过执行Tracker页面直接返回
                if (!LoadFilter()) return;

#endif

#if DEBUG

                //RELEASETRACKER模式执行负载状态优化
                //负载状态优化，当系统负载高时，跳过执行Tracker页面直接返回
                //if (!LoadFilter()) return;

#endif

#if RELEASEWEB

            //Web站不执行Tracker，返回提示信息

            SendErrorMessage("错误的Tracker地址！请按 用户中心 页面内容更新Tracker地址");
            return;

#endif // ReleaseWeb

                //Tracker站和DEBUG模式，正常执行Tracker
                //执行计划任务
                ScheduledEventTracker();


                #endregion

                #region peer信息初始化

                Discuz.Data.DbHelper.QueryCount = 0;

                //获取客户端发送来的GET数据
                string peer_passkey = DNTRequest.GetString("passkey").Trim().ToUpper();
                string peer_info_hash = PTTools.RawUrl2INFO_HASH_HEX(Request.RawUrl);// DNTRequest.GetString("info_hash").Trim().ToUpper();
                string peer_id = DNTRequest.GetString("peer_id").Trim();
                string peer_id_HEX = PTTools.RawUrl2PEERID_HEX(Request.RawUrl);//16进制格式的Peerid
                string peer_event = DNTRequest.GetString("event").Trim().ToLower();
                string peer_client_type = Request.Browser.Browser;
                int peer_port = DNTRequest.GetInt("port", 0);
                int peer_numwant = DNTRequest.GetInt("numwant", 50);
                string peer_uploadedstr = DNTRequest.GetString("uploaded").Trim();
                string peer_downloadedstr = DNTRequest.GetString("downloaded").Trim();
                string peer_leftstr = DNTRequest.GetString("left").Trim();
                decimal peer_uploaded = decimal.Parse(Utils.IsInt(peer_uploadedstr) && peer_uploadedstr.Length > 0 ? peer_uploadedstr : "0");
                decimal peer_downloaded = decimal.Parse(Utils.IsInt(peer_downloadedstr) && peer_downloadedstr.Length > 0 ? peer_downloadedstr : "0");
                decimal peer_left = decimal.Parse(Utils.IsInt(peer_leftstr) && peer_leftstr.Length > 0 ? peer_leftstr : "0");
                string peer_ip = Request.UserHostAddress;
                string peer_ipv6addip = DNTRequest.GetString("ipv6");
                string ClientString = DNTRequest.GetRawUrl();
                bool peer_no_peer_id = DNTRequest.GetInt("no_peer_id", 1) > 0 ? true : false;
                bool peer_compact = DNTRequest.GetInt("compact", 1) > 0 ? true : false;

                if (peer_compact) peer_no_peer_id = true;
                bool ipv6tracker = PTTools.IsIPv6(peer_ip);     //当前tracker是否IPv6访问的

                //bool ipv6added = false;                         //本次tracker更新中，新添加了ipv6地址                          
                //bool ipv6name = false;                          //当前tracker是否通过bt.buaa6.edu.cn这个域名访问
                //bool nullpeer = false;                          //是否不发送peer信息                

                #endregion

                #region 线程优先级

                //if (Thread.CurrentThread.Priority != ThreadPriority.Normal)
                //{
                //    PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "Priority", "优先级错误！ " + Thread.CurrentThread.Priority);
                //}
                //if (peer_event == "stopped")
                //{
                //    Thread.CurrentThread.Priority = ThreadPriority.Lowest;
                //    Thread.Sleep(0);
                //}

                #endregion

                #region 客户端agent验证

                //客户端字符串
                HttpBrowserCapabilities bc = Request.Browser;
                //ClientString += bc.Platform + "|" + bc.Browser + "|" + bc.Id + "|" + bc.Version + "|" + Request.UserAgent;

                ////校验客户端，只允许UT1830或以上或者Transmission1.5以上
                if (peer_ip != "xxx.xxx.xxx.xxx" && peer_ip != "xxx.xxx.xxx.xxx") //DEBUG
                {
                    if (peer_client_type.IndexOf("FireFox") > -1 || peer_client_type.IndexOf("IE") > -1 || peer_client_type.IndexOf("Safari") > -1 || peer_client_type.IndexOf("Optera") > -1)
                    {
                        SendErrorMessage("错误的下载软件 ERROR:0x101");
                        return;
                    }
                    if (Request.UserAgent.Length < 13)
                    {
                        SendErrorMessage("错误的下载软件 ERROR:0x102");
                        return;
                    }
                    else
                    {
                        if (Request.UserAgent.Substring(0, 2) == "uT")        //客户端为uTorrent
                        {
                            if (Request.UserAgent.Substring(0, 9) != "uTorrent/")
                            {
                                SendErrorMessage("错误的下载软件，本站仅支持uTorrent和Transmission ERROR:0x103");
                                return;
                            }
                            if (Utils.StrToInt(Request.UserAgent.Substring(9, 3), 0) < 200)
                            {
                                SendErrorMessage("请将uTorrent更新为UT200以上版本，本站右上角有下载 ERROR:0x110");
                                return;
                            }
                            if (Request.UserAgent.IndexOf("B") > 0)
                            {
                                SendErrorMessage("请勿使用测试版UT，本站右上角有正式版UT下载 ERROR:0x104");
                                return;
                            }
                            if (peer_id.Substring(1, 2) != "UT" || Utils.StrToInt(peer_id.Substring(3, 3), 0) < 183 || peer_id_HEX.Length != 40)
                            {
                                SendErrorMessage("错误的下载软件，本站仅支持uTorrent和Transmission ERROR:0x105");
                                return;
                            }
                        }
                        else if (Request.UserAgent.Length > 16)              //客户端为Transmission
                        {
                            if (Request.UserAgent.Substring(0, 13) != "Transmission/")
                            {
                                SendErrorMessage("错误的下载软件，本站仅支持uTorrent和Transmission ERROR:0x106");
                                return;
                            }
                            if (Utils.StrToFloat(Request.UserAgent.Substring(13, 3), 0) < 1.5)
                            {
                                SendErrorMessage("请将Transmission更新为1.5以上版本 ERROR:0x107");
                                return;
                            }
                            if (peer_id.Substring(1, 2) != "TR" || Utils.StrToInt(peer_id.Substring(3, 3), 0) < 150 || peer_id_HEX.Length != 40)
                            {
                                SendErrorMessage("错误的下载软件，本站仅支持uTorrent和Transmission ERROR:0x108");
                                return;
                            }
                        }
                        else
                        {
                            SendErrorMessage("错误的下载软件，本站仅支持uTorrent和Transmission ERROR:0x109");
                            return;
                        }
                    }
                }

                //D//临时措施TR客户端加入记录
                //D//if (peerinfo.Client.IndexOf("TR") > -1)
                //D//{
                //D//    PTPeerErrorLog.InsertPeerLog(0, ip, ClientString, peerinfo);
                //D//}

                #endregion

                #region 客户IP验证(暂时无代码)

                //校内用户必须使用域名访问tracker，校外ipv6用户可以使用IP地址
                //if(!ipv6tracker || ip.IndexOf("2001:da8:203") > -1 || ip.IndexOf("2001:da8:ae") > -1 || ipv6addip.IndexOf("2001:da8:203") > -1 || ipv6addip.IndexOf("2001:da8:ae") > -1) //是校内用户
                //{
                //    //buaaip = true;  
                //    if (Request.Url.Host.ToLower().IndexOf("bt.buaa.edu.cn") < 0 && Request.Url.Host.ToLower().IndexOf("bt.buaa6.edu.cn") < 0 && Request.Url.Host.ToLower().IndexOf("buaabt.cn") < 0) //没有使用域名作为Tracker
                //    {
                //        //SendErrorMessage("0x12 校内用户请使用域名作为Tracker地址");
                //        //return;
                //    }
                //}
                //if (Request.Url.Host.ToLower().IndexOf("bt.buaa6.edu.cn") > -1) ipv6name = true;



                //if (ip.IndexOf("2001:da8:203") > -1 || ip.IndexOf("2001:da8:ae") > -1) //通过IPv6访问Tracker的用户，阻止重复共享
                //{
                //    if (PrivateBT.GetPeerCountV4V6(ip, seedinfo.SeedId, peer_id) > 0)
                //    {
                //        SendErrorMessage("0x13 同一IP或者同一用户不能同时上传下载同一个种子");
                //        return;
                //    }
                //}
                //////////////////////////////////////////////////////////////////////////

                #endregion

                #region 获取用户信息，权限验证 [读取dnt_users表]

                //账户验证

                //验证passkey
                if (peer_passkey.Length != 32)
                {
                    SendErrorMessage("错误的Passkey格式 ERROR:0x201");
                    return;
                }

                //【数据库查询-SELECT】
                //ShortUserInfo btuserinfo = PrivateBT.GetBtUserInfo(passkey);
                PTUserInfo btuserinfo = new PTUserInfo();
                //验证passkey格式，必须由32位大写字母或数字构成
                Regex reg = new Regex(@"^[0-9a-zA-Z]{32}$");
                if (!reg.IsMatch(peer_passkey))
                {
                    SendErrorMessage("错误的Passkey格式 ERROR:0x301");
                    return;
                }

                if (DNTRequest.GetInt("abt", -1) > 0)
                {
                    AbtAnnounce(DNTRequest.GetString("passkey").Trim(), peer_ip, peer_port, peer_id_HEX, peer_uploaded, peer_downloaded, peer_left, peer_event);
                    return;
                }

                //获取用户信息【数据库查询-SELECT】
                btuserinfo = PTUsers.GetBtUserInfoForTracker(peer_passkey);
                if (btuserinfo.Uid < 1)
                {
                    SendErrorMessage("不存在的Passkey ERROR:0x302");
                    return;
                }

                //账户是否被禁封
                if (btuserinfo.Groupid == 5 || btuserinfo.Vip < 0) //用||，BT操作也可以禁封账户
                {
                    SendErrorMessage("账户被禁封 ERROR:0x303");
                    btuserinfo.Uid = -1;
                    return;
                }

                #endregion

                #region 获取种子信息 [读取bt_seed表] 完整表，非tracker表，暂时解决

                //种子验证

                //info_hash = PrivateBTTools.RawUrl2INFO_HASH_HEX(Request.RawUrl);//上面已经有了
                PTSeedinfoShort seedinfo = new PTSeedinfoShort();

                //验证info_hash格式，必须由40位16进制数字构成
                Regex reg2 = new Regex(@"^[0-9A-F]{40}$");
                if (!reg2.IsMatch(peer_info_hash))
                {
                    SendErrorMessage("错误的种子info_hash格式 ERROR:0x401");
                    return;
                }

                //【数据库查询-SELCET】
                seedinfo = PTSeeds.GetSeedInfoShort(peer_info_hash);

                if (seedinfo.SeedId < 1)
                {
                    SendErrorMessage("种子不存在 ERROR:0x204");
                    return;
                }

                #endregion

                #region 生成PeerInfo

                //生成PeerInfo
                //peer_id_HEX和ipv6addip的特殊处理
                if (peer_id_HEX.Length > 16) peer_id_HEX = peer_id_HEX.Substring(16);
                if (peer_id_HEX.Length > 24) peer_id_HEX = peer_id_HEX.Substring(0, 24);
                if (ipv6tracker && peer_ipv6addip == peer_ip) peer_ipv6addip = "";

                //生成本次的PeerInfo//仍然需要继续补充的信息：Fristtime,uploadspeed,downloadspeed
                PrivateBTPeerInfo peerinfo = new PrivateBTPeerInfo();

                //三要素，PeerId，Uid，Seedid
                peerinfo.PeerId = peer_id_HEX;//
                peerinfo.Uid = btuserinfo.Uid;//
                peerinfo.SeedId = seedinfo.SeedId;//



                //IP和IPv6判定，IP仅用于存放IPv4地址，没有IPv4地址的时候为"IP_NULL"，IPv6IP存放IPv6 Tracker获得的IP，
                // 没有时为"IP_NULL"，IPv6AddIP存放ut附加信息里的IPv6地址

                //本次访问的IP地址
                peerinfo.IPStatus = ipv6tracker ? 1 : 0;
                peerinfo.IPv4IP = ipv6tracker ? "IP_NULL" : peer_ip;
                peerinfo.IPv6IP = ipv6tracker ? peer_ip : "IP_NULL";

                //校内用户ipv4访问时，如果附加地址带有正确的ipv6地址则判断为ipv6用户
                if (peer_ipv6addip.IndexOf("2001:") > -1) peerinfo.IPStatus = 1;
                else peer_ipv6addip = "";
                peerinfo.IPv6IPAdd = (peer_ipv6addip == "") ? "IP_NULL" : peer_ipv6addip;

                //IP地址过滤
                IPAddressFilter(ref peerinfo);

                //最后更新时间
                if (ipv6tracker)
                {
                    peerinfo.v6Last = DateTime.Now;
                    peerinfo.v4Last = DateTime.Parse("2009-01-01");
                }
                else
                {
                    peerinfo.v6Last = DateTime.Parse("2009-01-01");
                    peerinfo.v4Last = DateTime.Now;
                }

                //其他信息
                peerinfo.Client = peer_id.Substring(1, 6);//peerinfo.Client = Request.UserAgent;
                peerinfo.IsSeed = (peer_left == 0 ? true : false);//
                peerinfo.Port = peer_port;//
                peerinfo.LastTime = DateTime.Now;
                peerinfo.Percentage = ((double)seedinfo.FileSize - (double)peer_left) / (double)seedinfo.FileSize;
                if (peerinfo.Percentage > 0.9999 && peer_left > 0) peerinfo.Percentage = 0.9999; //防止未完成时显示100%
                peerinfo.Upload = peer_uploaded;
                peerinfo.Download = peer_downloaded;
                peerinfo.RawRequestString = ClientString;
                peerinfo.Left = peer_left;

                //FGBTACC 特殊处理
                if (peerinfo.Uid == 2268 || peerinfo.Uid == 22854)
                {
                    string ipappend = DNTRequest.GetString("ip");
                    if (ipappend == "172.30.102.1")
                    {
                        peerinfo.IPv6IPAdd = "FGBTACC_RSSUPLOAD";
                        peer_ipv6addip = "FGBTACC_RSSUPLOAD";
                    }
                    else
                    {
                        peerinfo.IPv6IPAdd = "";
                    }
                }

                #endregion

                #region 同用户同种子单线程执行限制

                //同用户、同一个种子，不能同时执行
                bool passoperate = (peer_event == "stopped" || peer_event == "completed");
                if (!WaitForConCurrentUserSeed(btuserinfo.Uid, seedinfo.SeedId, passoperate))
                {
                    if (!passoperate)
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("处理等候超时 UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        SendWaitMessage(60);
                        return;
                    }
                    else
                    {
                        if (peer_event == "stopped")
                        {
                            //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Detail, "Stop Pass", string.Format("跳过处理Stop UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                            return;
                        }
                        else
                        {
                            //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Detail, "Stop Pass", string.Format("跳过处理Complete UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                            SendWaitMessage(60);
                            return;
                        }
                    }
                }

                #endregion

                #region 获取旧信息 OldPeerInfo [读取bt_peer表]

                PrivateBTPeerInfo oldpeerinfo = new PrivateBTPeerInfo();
                oldpeerinfo = PrivateBT.GetPeerInfo(peerinfo);

                #endregion

                #region 检查事件逻辑性

                //Tracker规则：
                //同Peerid，同Seedid，不同Uid，不允许
                //同Uid，同Seedid，不能同时上传下载
                //不发送同IP，同IPv6者的Tracker信息
                //分类更新信息，此范围内需要更新的1数值：peer：uploadspeed,downloadspeed          user：finished+     seed：live+/finished+/lastlive/lastseederid/lastseedername

                bool LogicCheckOk = false;
                //complete
                if (peer_event == "completed")
                {
                    if (peer_left == 0) //做种
                    {
                        if (oldpeerinfo != null && oldpeerinfo.SeedId > 0 && oldpeerinfo.Uid > 0) LogicCheckOk = true;
                        else
                        {
                            PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Normal, "TDEBUG", string.Format("0x18 COMPLETE 获取节点不存在 W30 UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                            SendWaitMessage(30); return; 
                        }
                    }
                    else //下载，异常
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("0x17 COMPLETE 错误的种子事件 不完整 UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        PTPeerLog.InsertPeerErrorInfo(6, 0, "0x17 错误的种子事件complete2 不完整", peerinfo, peerinfo);
                        SendErrorMessage("错误的种子状态 ERROR:0x017");
                        return;
                    }
                }
                //paused
                if (peer_event == "paused") peer_event = "stopped";
                //
                if (peer_event == "")
                {
                    if (oldpeerinfo != null && oldpeerinfo.SeedId > 0 && oldpeerinfo.Uid > 0) LogicCheckOk = true;
                    else if(oldpeerinfo == null)
                    {
                        //中间更新，但是节点不存在
                        peer_event = "mid2started";
                        PTPeerLog.InsertPeerErrorInfo(5, 0, "update_2start2", peerinfo, peerinfo);
                        //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Normal, "TDEBUG", string.Format("0x21 MID 锁定节点不存在 2START UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        LogicCheckOk = true;
                    }
                    else PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Normal, "TDEBUG", string.Format("0x22 MID 节点异常 2START UID:{0} SEEDID:{1} OLDPEER:{2}", btuserinfo.Uid, seedinfo.SeedId, oldpeerinfo.Pid));

                }
                //started
                if (peer_event == "started" || peer_event == "mid2started")
                {
                    //之前存在当前ut的上传下载记录
                    if (oldpeerinfo != null && oldpeerinfo.SeedId > 0 && oldpeerinfo.Uid > 0)
                    {
                        //存在记录，转中间更新 //双Tracker时会遇到的正常情况
                        //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("0x312 START 有记录时开始 UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        peer_event = "started2mid";
                        LogicCheckOk = true;
                    }
                    else
                    {
                        //检查是否存在同一UT的其他人账户
                        if (PrivateBT.IsExistsOtherPeer(btuserinfo.Uid, peer_id_HEX, seedinfo.SeedId) != 0)
                        {
                            SendErrorMessage("禁止单种子双人做种 ERROR:0x401");
                            //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("0x401 挂多人TRACKER UID:{0} SEEDID:{1} PEER:{2} IP:{3}-{4}", btuserinfo.Uid, seedinfo.SeedId, peerinfo.PeerId, peerinfo.IPv4IP, peerinfo.IPv6IP));
                            return;
                        }

                        peerinfo.FirstTime = DateTime.Now;

                        if (peer_left == 0) //做种
                        {
                            //当前用户之前是否在下载（可能不是同一个ut软件在上传下载），如果有下载，删除正在下载的记录 //是否应该改成报错等待下载节点结束？？？
                            if (PrivateBT.IsExistsPeer(btuserinfo.Uid, seedinfo.SeedId, true) > 0)
                            {
                                PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("0x313 START 有下载节点时开始上传 DEL下载 UID:{0} SEEDID:{1} PEER:{2} IP:{3}-{4}", btuserinfo.Uid, seedinfo.SeedId, peerinfo.PeerId, peerinfo.IPv4IP, peerinfo.IPv6IP));
                                //PrivateBT.DeletePeerInfo(btuserinfo.Uid, seedinfo.SeedId);
                                SendErrorMessage("不能同时下载和上传同一个种子 ERROR:0x313");
                                return;
                            }
                            else LogicCheckOk = true;
                        }
                        else //下载
                        {
                            //当前用户之前是否在上传（可能不是同一个ut软件在上传），如果有上传，禁止本次tracker
                            if (PrivateBT.IsExistsPeer(btuserinfo.Uid, seedinfo.SeedId, false) > 0)
                            {
                                PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("0x314 START 有上传节点时开始下载 ERR UID:{0} SEEDID:{1} PEER:{2} IP:{3}-{4}", btuserinfo.Uid, seedinfo.SeedId, peerinfo.PeerId, peerinfo.IPv4IP, peerinfo.IPv6IP));
                                SendErrorMessage("不能同时上传和下载同一个种子 ERROR:0x314");
                                return;
                            }

                            //当前用户之前是否在下载（不是同一个ut软件在下载），一般为死机造成
                            if (PrivateBT.IsExistsPeer(btuserinfo.Uid, seedinfo.SeedId, true) > 0)
                            {
                                //需要考虑UT崩溃后再次启动的情况，尽量减少用户疑惑？？？
                                //只更新peerid，使得记录一致，之后的流量更新由下一次tracker访问完成
                                //PrivateBTPeerInfo olddown = PrivateBT.GetPeerInfo_OldDownload(peerinfo);
                                //if ((DateTime.Now - olddown.LastTime).TotalSeconds > 90)
                                //{

                                //}
                                PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("0x315 START 有下载节点时开始下载 ERR UID:{0} SEEDID:{1} PEER:{2} IP:{3}-{4}", btuserinfo.Uid, seedinfo.SeedId, peerinfo.PeerId, peerinfo.IPv4IP, peerinfo.IPv6IP));
                                //if(oldpeerinfo)
                                SendErrorMessage("不能同时下载同一个种子 ERROR:0x315");
                                //SendWaitMessage(60);
                                return;
                            }
                            else
                            {
                                #region 计算共享率，检查用户共享率情况，是否允许下载

                                //只在新增下载peer时，校验用户共享率。如果已经开始下载，下载过程中低于限值的情况不应该阻止更新。
                                if (btuserinfo.Extcredits4 != 0) btuserinfo.Ratio = ((float)btuserinfo.Extcredits3 / (float)btuserinfo.Extcredits4);
                                else btuserinfo.Ratio = 1.00001f;
                                if (peer_left > 0)
                                {
                                    if (
                                        (btuserinfo.Extcredits4 > 20 * 1024M * 1048576 && btuserinfo.Ratio < 0.3f) ||
                                        (btuserinfo.Extcredits4 > 100 * 1024M * 1048576 && btuserinfo.Ratio < 0.4f) ||
                                        (btuserinfo.Extcredits4 > 200 * 1024M * 1048576 && btuserinfo.Ratio < 0.5f) ||
                                        (btuserinfo.Extcredits4 > 300 * 1024M * 1048576 && btuserinfo.Ratio < 0.6f) ||
                                        (btuserinfo.Extcredits4 > 400 * 1024M * 1048576 && btuserinfo.Ratio < 0.7f) ||
                                        (btuserinfo.Extcredits4 > 500 * 1024M * 1048576 && btuserinfo.Ratio < 0.8f) ||
                                        (btuserinfo.Extcredits4 > 750 * 1024M * 1048576 && btuserinfo.Ratio < 0.9f) ||
                                        (btuserinfo.Extcredits4 > 1024 * 1024M * 1048576 && btuserinfo.Ratio < 1.0f)
                                        )
                                    {
                                        SendErrorMessage("共享率过低 ERROR:0x304");
                                        btuserinfo.Uid = -1;
                                        return;
                                    }
                                }

                                #endregion
                                LogicCheckOk = true;
                            }
                            
                        }
                    }
                }
                //stopped
                if (peer_event == "stopped")
                {
                    //如果是停止操作，则排队等候运行
                    int delay = GetStopDelay();
                    Thread.Sleep(delay);
                    //PTLog.InsertSystemLog(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Detail, "Stop Delay", string.Format("UID:{0} SEEDID:{1} -DELAY:{2}", peerinfo.Uid, peerinfo.SeedId, delay));
                    
                    if (oldpeerinfo != null && oldpeerinfo.SeedId > 0 && oldpeerinfo.Uid > 0)  LogicCheckOk = true;
                    else
                    {
                        //双Tracker删除种子事件很容易导致PEER已经被删除
                        //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("0x42 停止PEER不存在 ERR UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        SendWaitMessage(60);
                        return;
                    }
                }
                //others
                if (peer_event != "completed" && peer_event != "" && peer_event != "started2mid" && peer_event != "mid2started" && peer_event != "started" && peer_event != "stopped")
                {
                    PTPeerLog.InsertPeerErrorInfo(7, 0, "不存在的种子事件1" + peer_event, peerinfo, new PrivateBTPeerInfo(peerinfo));
                    PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", "0x33 种子状态异常 ERROR:0x519" + peer_event);
                    SendErrorMessage("种子状态异常 ERROR:0x519");
                    return;
                }

                if (!LogicCheckOk)
                {
                    PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", "逻辑检查FALSE");
                    SendErrorMessage("种子状态异常 ERROR:0x520");
                    return;
                }

                #endregion

                #region 获取PeerList并发送Tracker回应[读取bt_peer表]

                //获取PeerList 
                DataTable uploadlist = new DataTable();
                DataTable downloadlist = new DataTable();
                if (peer_event == "started" || peer_event == "completed" || peer_event == "" || peer_event == "started2mid" || peer_event == "mid2started")
                {
                    bool initialseeder = false;

                    //获取Peer节点列表
                    if (peer_numwant > 200) peer_numwant = 200;
                    if (peer_left > 0) uploadlist = PrivateBT.GetPeerListTracker(seedinfo.SeedId, true, peer_numwant, !peer_no_peer_id);
                    if (uploadlist.Rows.Count < peer_numwant) downloadlist = PrivateBT.GetPeerListTracker(seedinfo.SeedId, false, peer_numwant - uploadlist.Rows.Count, !peer_no_peer_id);
                    if ((DateTime.Now - seedinfo.PostDateTime).TotalHours < 12) initialseeder = true;

                    //发送限制模式，获取当前节点的校内IP地理位置
                    if (!ipv6tracker) peerinfo.IPRegionInBuaa = PrivateBT.GetIPRegion(peerinfo.IPv4IP);
                    else if (oldpeerinfo != null && oldpeerinfo.IPRegionInBuaa != PTsIpRegionInBuaa.INIT) peerinfo.IPRegionInBuaa = oldpeerinfo.IPRegionInBuaa;
                    else peerinfo.IPRegionInBuaa = PrivateBT.GetIPRegion(peerinfo.IPv6IP);
                    if (peerinfo.IPRegionInBuaa == PTsIpRegionInBuaa.INIT || peerinfo.IPRegionInBuaa == PTsIpRegionInBuaa.ERROR)
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "IPRegion", string.Format("0x901 位置判断失败 UID:{0} SEEDID:{1} IPREGION:{2}", btuserinfo.Uid, seedinfo.SeedId, peerinfo.IPRegionInBuaa));
                        SendErrorMessage("异常访问 ERROR:0x901");
                        return;
                    }

                    //获取IP状态和发送模式
                    if (peer_event == "started" || peer_event == "mid2started")
                    {
                        //首次更新时，可能需要暂缓处理的情况
                        int curipstatus = 0;
                        if (ipv6tracker) curipstatus = (int)PTTools.GetIPType(peerinfo.IPv6IP);
                        else curipstatus = (int)PTTools.GetIPType(peerinfo.IPv4IP);
                        if (curipstatus < 0) PTLog.InsertSystemLog(PTLog.LogType.PeerInvidIP, PTLog.LogStatus.Error, "INSERT_PEER", "未能判断来访者的IP地址--" + peerinfo.IPv4IP + "--" + peerinfo.IPv6IP);
                        if (curipstatus == 4) PTLog.InsertSystemLog(PTLog.LogType.PeerOutIPv4, PTLog.LogStatus.Error, "INSERT_PEER", "校外IPv4地址访问" + peerinfo.IPv4IP + "--" + peerinfo.IPv6IP);

                        if (curipstatus == 1 || curipstatus == 2)  //首次更新为 校内v6，暂不发送
                        {
                            peerinfo.LastSend = 255;
                            peerinfo.IPStatus = 7;
                        }
                        else if (curipstatus == 0) peerinfo.IPStatus = 0; //首次更新为 校内v4
                        else if (curipstatus == 3) peerinfo.IPStatus = 6; //校外v6
                        else peerinfo.IPStatus = -2;
                    }
                    if (peerinfo.LastSend != 255) peerinfo.LastSend = GetSendingMode(peerinfo.Percentage == 1.0 ? false : true, peerinfo.IPRegionInBuaa, uploadlist, downloadlist, seedinfo.SeedId, false);

                    //更新间隔（默认种子数小于5）注意：修改下次更新时间，必须同是修改超时删除Peer的期限
                    int nextupdate_second = 300;
                    if (btuserinfo.Uid == 22854 || btuserinfo.Uid == 2268) //FGBTACC RSS和RSSUPLOAD快速更新
                    {
                        if (peer_ip == "xxx.xxx.xxx.xxx" || peer_ip == "xxx.xxx.xxx.xxx" || peer_ipv6addip == "FGBTACC_RSSUPLOAD") nextupdate_second = 40;
                        else nextupdate_second = 600;
                    }
                    else if (peerinfo.Percentage < 1.0 || initialseeder) nextupdate_second = 40;
                    else if (PrivateBT.IsServerUser(btuserinfo.Uid) || btuserinfo.Uid == 13 || btuserinfo.Uid == 10597) nextupdate_second = 1800;
                    else if (seedinfo.Upload > 5) nextupdate_second = 600;
                    nextupdate_second = nextupdate_second + PTTools.GetRandomInt(0, nextupdate_second / 2);

                    //发送正常反馈信息
                    SendPeerList(peerinfo.IPStatus, peer_numwant, seedinfo.Upload, seedinfo.Download, uploadlist, downloadlist,
                        btuserinfo.Uid, peer_ip, peer_ipv6addip, peerinfo.Percentage == 1.0 ? false : true, peer_no_peer_id, initialseeder,
                        peerinfo.LastSend, (int)peerinfo.IPRegionInBuaa, nextupdate_second);

                    //D//if (peerinfo.IPStatus == 6 && peerinfo.Percentage < 1) PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Detail, "IPRegion", string.Format("LastSend:{0}, IPRegion:{1}, IP:{2}, ", peerinfo.LastSend, (int)peerinfo.IPRegionInBuaa, peerinfo.IPv6IP));
                }

                #endregion

                #region 开启后台处理线程

                //D//ParameterizedThreadStart bgThreadStart = new ParameterizedThreadStart(BackgroundProcessThread);
                //D//Thread bgThread = new Thread(bgThreadStart);
                AnnounceNewThreadParam par = new AnnounceNewThreadParam();
                par.btuserinfo = btuserinfo;
                //par.downloadlist = downloadlist;
                par.peer_downloaded = peer_downloaded;
                par.peer_event = peer_event;
                par.peer_ipv6tracker = ipv6tracker;
                par.peer_left = peer_left;
                par.peer_uploaded = peer_uploaded;
                par.peerinfo = peerinfo;
                par.oldpeerinfo = oldpeerinfo;
                par.seedinfo = seedinfo;
                //par.uploadlist = uploadlist;
                //D//bgThread.Start((object)par);
                ThreadPool.QueueUserWorkItem(BackgroundProcessThread, (object)par);

                #endregion

                #region 内存回收及执行统计

                //System.GC.Collect();

                //////////////////////////////////////////////////////////////////////////
                //内存回收
                //peerinfo = null;
                //btuserinfo = null;
                uploadlist.Dispose();
                uploadlist = null;
                downloadlist.Dispose();
                downloadlist = null;
                //seedinfo = null;
                //////////////////////////////////////////////////////////////////////////

                Discuz.Data.DbHelper.QueryCount = 0;

                #endregion

                #region 新版测试

                //Response.Write("d8:completei1e10:incompletei0e8:intervali10e5:peersld2:ip18:240c:2:100:46c1::17:peer id20:S588-----gqQ8TqDeqaY4:porti6882eeee");



                //Discuz.Data.DbHelper.QueryCount = 0;

                ///////////////////////////////////////////////////////////////////////////
                ////获取客户端发送来的GET数据

                ////用户信息
                //string peer_passkey = DNTRequest.GetString("passkey").Trim().ToUpper();
                //string peer_info_hash = PTTools.RawUrl2INFO_HASH_HEX(Request.RawUrl).ToUpper();// DNTRequest.GetString("info_hash").Trim().ToUpper();
                //string peer_id = PTTools.RawUrl2PEERID_HEX(Request.RawUrl).ToUpper();           //16进制格式的Peerid
                //string peer_key = DNTRequest.GetString("key").Trim().ToUpper();                 //附加key
                ////请求内容
                //string peer_event = DNTRequest.GetString(("EVENT")).Trim().ToLower();;
                //int peer_numwant = DNTRequest.GetInt("numwant", 50);
                //int peer_compact = DNTRequest.GetInt("compact", 0);
                //int peer_nopeerid = DNTRequest.GetInt("no_peer_id", 0);
                ////上传下载数据
                //decimal peer_uploaded = TypeConverter.StrToDecimal(DNTRequest.GetString("uploaded").Trim());
                //decimal peer_downloaded = TypeConverter.StrToDecimal(DNTRequest.GetString("downloaded").Trim());
                //decimal peer_left = TypeConverter.StrToDecimal(DNTRequest.GetString("left").Trim());
                //decimal peer_corrupt = TypeConverter.StrToDecimal(DNTRequest.GetString("corrupt").Trim());
                ////IP和端口
                //string peer_ipv4ip = PTTools.IsIPv6(Request.UserHostAddress) ? "" : Request.UserHostAddress;
                //string peer_ipv6ip = PTTools.IsIPv6(Request.UserHostAddress) ? Request.UserHostAddress : "";
                //string peer_ipv6ipadd =  PTTools.IsIPv6(DNTRequest.GetString("ipv6")) ? DNTRequest.GetString("ipv6") : "";
                //int peer_port = DNTRequest.GetInt("port", 0);


                /////////////////////////////////////////////////////////////////////////////////
                ////所需数据验证和生成

                //// ① 验证下载客户端
                //if (!ValidateClientSoftware(Request.Browser.Browser, Request.UserAgent, peer_id)) return;

                //// ② 验证客户端IP地址，返回的IP地址均为合法
                //if (!ValidateClientIP(ref peer_ipv4ip, ref peer_ipv6ip, ref peer_ipv6ipadd)) return;

                //// ③ 用户验证
                //PTUserInfo userinfo = ValidateUser(peer_passkey, peer_left);
                //if (userinfo.Uid < 1) return;

                //// 获取BT配置信息【数据库查询-SELECT】未来替换为文件保存，不再读取数据库
                //PrivateBTConfigInfo config = PrivateBTConfig.GetPrivateBTConfig();

                //// ④ 验证种子，并计算最终流量系数
                //PTSeedinfoShort seedinfo = ValidateSeed(peer_info_hash, config, userinfo.Uid);
                //if (seedinfo.SeedId < 1) return;

                //// ⑤ 生成PeerInfo  新Peerid = peerid后24位+key（8位） = 32位，Peerid为客户端唯一识别码，认为不会重复，校验peerinfo合理性
                //PTPeerInfo peerinfo = CreatePeerInfo(peer_id, peer_key, userinfo.Uid, seedinfo.SeedId, peer_port, peer_ipv4ip, peer_ipv6ip, peer_ipv6ipadd, peer_uploaded, 
                //    peer_downloaded, peer_left, peer_corrupt, seedinfo.FileSize, peer_event, peer_numwant, peer_compact, peer_nopeerid);
                //if (peerinfo.SeedId < 1) return;

                ////========================================================================
                //// 所有数据均已经整合进类变量，以下部分不允许再使用单独的 peer_xx 变量进行操作

                ////////////////////////////////////////////////////////////////////////////
                //// Peer操作类型验证和流量统计

                //// ⑥ 校验是否合乎规则，获取oldpeerinfo并上锁
                //PTPeerInfo oldpeerinfo = CheckPeerStatus(ref peerinfo);
                //if (oldpeerinfo.Event == "error") return;

                //// ⑦ 计算流量并进行速度反作弊检查
                //if (CalculateTraffic(oldpeerinfo, ref peerinfo, seedinfo)) return;

                //// ⑧ 更新Peer，更新完成即解锁
                //if (!UpdatePeer(oldpeerinfo, peerinfo)) return;

                //// ⑨ 更新Seed
                //if (!UpdateSeed(oldpeerinfo, peerinfo, seedinfo)) return;

                //// ⑩ 更新User
                //if (!UpdateUser(oldpeerinfo, peerinfo, userinfo)) return;

                //// ⑾ 向客户端发送信息
                //SendPeerList(peerinfo);

                //Discuz.Data.DbHelper.QueryCount = 0;

                #endregion
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TrackerBaseEx", ex.ToString());
            }
        }

        //D//private int testseedid = -1;
        //D//private int testuid = -1;
        /// <summary>
        /// 后台处理线程
        /// </summary>
        /// <param name="ParObject"></param>
        protected void BackgroundProcessThread(object ParObject)
        {
            try
            {
                #region 变量

                AnnounceNewThreadParam Par = (AnnounceNewThreadParam)ParObject;
                PTUserInfo btuserinfo = Par.btuserinfo;
                PrivateBTPeerInfo peerinfo = Par.peerinfo;
                PrivateBTPeerInfo oldpeerinfo = Par.oldpeerinfo;
                PTSeedinfoShort seedinfo = Par.seedinfo;
                string peer_event = Par.peer_event;
                decimal peer_left = Par.peer_left;
                decimal peer_uploaded = Par.peer_uploaded;
                decimal peer_downloaded = Par.peer_downloaded;
                bool peer_ipv6tracker = Par.peer_ipv6tracker;
                //DataTable uploadlist = Par.uploadlist;
                //DataTable downloadlist = Par.downloadlist;

                int addcount_upload = 0;   //种子上传数变化量
                int addcount_download = 0; //种子下载数变化量

                bool ipv6added = false;
                bool gettrafficinfo_ok = false;

                //正在上传下载的数目
                //保存之前的存活时间
                int oldlive = seedinfo.Live;
                peerinfo.Event = peer_event;
                if (oldpeerinfo != null) peerinfo.Pid = oldpeerinfo.Pid;

                //之后作为增量使用，清零
                btuserinfo.FinishCount = 0;
                seedinfo.Finished = 0;
                seedinfo.Live = 0;
                //本次更新，增加的上传量
                decimal addupload = 0M;
                //本次更新，增加的下载量
                decimal adddownload = 0M;
                //本次更新，增加的种子剩余未完成量（应为负数）
                decimal addleft = 0M;
                //本次更新，增加的保种时间
                int addkeeptime = 0;

                //D//testseedid = seedinfo.SeedId;
                //D//testuid = btuserinfo.Uid;

                #endregion

                #region 线程优先级

                //if (Thread.CurrentThread.Priority != ThreadPriority.Normal)
                //{
                //    PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "Priority", "优先级错误！BackgroundThread " + Thread.CurrentThread.Priority);
                //}
                //if (peer_event == "stopped")
                //{
                //    Thread.CurrentThread.Priority = ThreadPriority.Lowest;
                //    Thread.Sleep(0);
                //}

                #endregion

                #region 获得流量系数设置

                //获得BT配置信息/////
                //【数据库查询-SELECT】
                PrivateBTConfigInfo btconfig = PrivateBTConfig.GetPrivateBTConfig();
                if (btuserinfo == null)
                {
                    PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", "0x000 服务器内部错误");
                    return;
                }
                //获取上传下载因子（只需要对比config中的数值，不用进行seedinfo中的ratioexpiredate判断，此工作由计划任务完成，从bt_seed_tracker表中获取的seedinfo也没有系数过期时间）
                
                //自己发布的种子，在没有设置上传系数小于1的情况下，上传系数 = 2
                if (seedinfo.Uid == btuserinfo.Uid && (DateTime.Now - seedinfo.PostDateTime).TotalHours < 12 && seedinfo.UploadRatio >= 1.0f && seedinfo.UploadRatio < 2.0) seedinfo.UploadRatio = 2.0f;

                //全局上传下载系数是否生效，如果生效，则取代种子中的上传下载系数
                bool globalblue = false;
                if (btconfig.UploadMulti >= 1.0f && DateTime.Now > btconfig.UpMultiBeginTime && DateTime.Now < btconfig.UpMultiEndTime)
                {
                    if (btconfig.UploadMulti > seedinfo.UploadRatio)
                    {
                        seedinfo.UploadRatio = btconfig.UploadMulti;
                    }
                }
                if (btconfig.DownloadMulti <= 1.0f && DateTime.Now > btconfig.DownMultiBeginTime && DateTime.Now < btconfig.DownMultiEndTime)
                {
                    if (btconfig.DownloadMulti <= 0.2f) globalblue = true;

                    if (btconfig.DownloadMulti < seedinfo.DownloadRatio)
                    {
                        seedinfo.DownloadRatio = btconfig.DownloadMulti;
                    }
                }

                #endregion

                #region 事件处理：完成 completed
                //int peerupdatelevel = 1; //UpdatePeerInfo更新级别，1 notraffic，2 up， 3 updown， 4完整
                //**************************************************************
                //下载完成了
                if (peer_event == "completed")
                {
                    if (oldpeerinfo != null)  
                    {
                        //计算流量
                        CalcPeerTraffic(peerinfo, oldpeerinfo, ref addupload, ref adddownload, ref addleft, ref addkeeptime);
                        //做种时间和最后做种
                        GetSeedLastLive(seedinfo, peerinfo);
                        //peerinfo信息更新 //因为完成是同时发起的Tracker请求，所以不能用时间先后来判断
                        //updatepeer函数更新的数值：download, downloadspeed,seed,lasttime,percentage,upload,uploadspeed
                        if (UpdatePeerInfo(peerinfo, oldpeerinfo, peer_ipv6tracker, seedinfo.SeedId, globalblue, ref ipv6added) < 1)
                        {
                            PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("失败 BGTASK COMPLETE OLDPEERINFO FAIL UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                            return;                
                        }
                        //种子上传下载数变化
                        else if (!oldpeerinfo.IsSeed && peerinfo.IsSeed) { addcount_download = -1; addcount_upload = 1; }

                        #region 确认完成，如果完成流量大于种子体积的90%，且之前无完成记录，则更新种子最后完成时间
                        if (oldpeerinfo.Download + oldpeerinfo.TotalDownload + adddownload >= seedinfo.FileSize * 0.9M)
                        {
                            if (!PrivateBT.IsUserHaveFinished(btuserinfo.Uid, seedinfo.SeedId))
                            {
                                int coincount = 0;
                                if ((DateTime.Now - seedinfo.PostDateTime).TotalDays > 15 && seedinfo.DownloadRatio > 0)
                                {
                                    coincount = (int)(seedinfo.FileSize / 1024 / 1024 / 1024M);
                                    if (seedinfo.FileSize - (decimal)coincount * 1024 * 1024 * 1024M < 0) coincount -= 1;
                                    if (coincount < 1) coincount = 1;
                                }
                                PTSeeds.UpdateSeedLastFinish(seedinfo.SeedId, coincount);

                                //发放种子完成奖励
                                Discuz.Forum.Users.UpdateUserExtCredits(seedinfo.Uid, 2, (float)coincount);
                            }
                        }
                        #endregion

                        //完成记录
                        PrivateBT.InsertFinished(seedinfo.SeedId, btuserinfo.Uid, 0M, 0M);
                        btuserinfo.FinishCount = 1;
                        seedinfo.Finished = 1;
                    }
                    else //错误，不应该存在的状态
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("错误 BGTASK COMPLETE OLDPEERINFO NULL UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        return;

                        //相当于Start事件
                        //peer_event = "started";
                        //btuserinfo.FinishCount = 1;
                        //seedinfo.Finished = 1;
                        //PrivateBT.InsertFinished(seedinfo.SeedId, btuserinfo.Uid, 0M, 0M);

                        ////处理完成，解锁
                        //PrivateBT.UnLockPeer(peerinfo);
                    }

                }

                #endregion

                #region 事件处理：中间更新 mid

                //*************************************************************
                //中间的更新
                else if (peer_event == "" || peer_event == "started2mid")
                {
                    if (oldpeerinfo != null)                           
                    {
                        //计算流量
                        CalcPeerTraffic(peerinfo, oldpeerinfo, ref addupload, ref adddownload, ref addleft, ref addkeeptime);
                        //做种时间和最后做种
                        GetSeedLastLive(seedinfo, peerinfo);
                        //updatepeer函数更新的数值：download, downloadspeed,seed,lasttime,percentage,upload,uploadspeed
                        if (UpdatePeerInfo(peerinfo, oldpeerinfo, peer_ipv6tracker, seedinfo.SeedId, globalblue, ref ipv6added) < 1)
                        {
                            PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("失败 BGTASK UPDATE OLDPEERINFO FAIL UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                            return;
                        }
                    }
                    else
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("错误 BGTASK UPDATE OLDPEERINFO NULL UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        return;
                        ////之前没有下载，做为开始//可能是服务器故障导致
                        ////相当于Start事件
                        //peer_event = "started";
                        //peerinfo.Event = "update_2start";
                        //PTPeerLog.InsertPeerErrorInfo(5, 0, "update_2start1", peerinfo, peerinfo);

                        ////更新完毕，解锁
                        //PrivateBT.UnLockPeer(peerinfo);
                    }
                }

                #endregion

                #region 事件处理：开始 started
                
                else if (peer_event == "started" || peer_event == "mid2started")
                {
                    //创建新peer
                    if (CreatePeerInfo(peerinfo, ref gettrafficinfo_ok) > 0)
                    {
                        if (peerinfo.Left == 0)
                        {
                            addcount_download = 0;
                            addcount_upload = 1;
                            GetSeedLastLive(seedinfo, peerinfo);
                        }
                        else
                        {
                            addcount_download = 1;
                            addcount_upload = 0;
                        }
                    }
                    else
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("失败 BGTASK INSERT PEERINFO UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        return;
                    }
                }

                #endregion

                #region 事件处理：stopped

                //*************************************************************
                //正常退出
                else if (peer_event == "stopped")
                {
                    if (oldpeerinfo != null)
                    {
                        //计算流量
                        CalcPeerTraffic(peerinfo, oldpeerinfo, ref addupload, ref adddownload, ref addleft, ref addkeeptime);
                        //做种时间和最后做种
                        GetSeedLastLive(seedinfo, peerinfo);
                        //删除peer
                        if (PrivateBT.DeletePeerInfo(peerinfo) > 0)
                        {
                            if (peerinfo.IsSeed) { addcount_download = 0; addcount_upload = -1; }
                            else { addcount_download = -1; addcount_upload = 0; }
                        }
                        else
                        {
                            PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("失败 BGTASK DELETE PEERINFO FAIL UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                            return;
                        }
                    }
                    else
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("错误 BGTASK DELETE PEERINFO NULL UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        return;
                    }
                }

                #endregion

                #region 事件处理：不可处理事件
                else //if (peer_event != "completed" && peer_event != "" && peer_event != "started2mid" && peer_event != "mid2started" && peer_event != "started" && peer_event != "stopped")
                {
                    PTPeerLog.InsertPeerErrorInfo(7, 0, "不存在的种子事件1" + peer_event, peerinfo, new PrivateBTPeerInfo(peerinfo));
                    PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", "0x519 不存在的种子事件");
                    return;
                }

                #endregion

                #region 解除同用户同种子单线程执行限制

                EndConCurrentUserSeed();

                #endregion

                #region 更新Seed表，种子信息

                //增量不可小于零
                if (addupload < 0M) addupload = 0M;
                if (adddownload < 0M) adddownload = 0M;

                //更新种子信息//总计需要更新的信息：upload, download,finished+, live+, lastlive, traffic+, ipv6, lastseederid, lastseedername ";
                if (seedinfo.Live > 7200) seedinfo.Live = 7200;
                seedinfo.Traffic = adddownload;
                seedinfo.UpTraffic = addupload;

                if (peer_event == "" && ipv6added == false)
                {
                    //if (peerinfo.IsSeed) PTSeeds.UpdateSeedAnnounceUpTrafficOnly(peerinfo.SeedId, seedinfo.UpTraffic);
                    //else 
                    PTSeeds.UpdateSeedAnnounceTrafficOnly(peerinfo.SeedId, seedinfo.UpTraffic, seedinfo.Traffic);
                }
                //else if (peer_event == "" && ipv6added == true)
                //{
                //    //seedinfo.IPv6 = PrivateBT.GetPeerSeedIPv6UploadCount(seedinfo.SeedId);
                //    PTSeeds.UpdateSeedAnnounceTrafficOnly(peerinfo.SeedId, seedinfo.UpTraffic, seedinfo.Traffic);
                //}
                else
                {
                    bool renew = false;
                    int newipv6 = seedinfo.IPv6;
                    if (ipv6added) newipv6 = PrivateBT.GetPeerSeedIPv6UploadCount(seedinfo.SeedId);

                    if (peerinfo.Event != "started2mid" && (DateTime.Now - seedinfo.LastPeerCountUpdate).TotalMinutes > 1440)
                    {
                        //24个小时，重新统计种子的上传下载数
                        int newupload = PrivateBT.GetPeerSeedUploadCount(seedinfo.SeedId);
                        int newdownload = PrivateBT.GetPeerSeedDownloadCount(seedinfo.SeedId);

                        //PTSeedinfoShort ninfo = PTSeeds.GetSeedInfoShort(seedinfo.SeedId);
                        //获取新Peer节点数前后，当前种子记录的上传下载数没有变化，则可以进行重新统计更新
                        //if (ninfo.Upload == seedinfo.Upload && ninfo.Download == seedinfo.Download)
                        //{
                        if (seedinfo.Upload + addcount_upload != newupload || seedinfo.Download + addcount_download != newdownload)
                        {
                            if (PTSeeds.UpdateSeedAnnounce(seedinfo.SeedId, newupload, newdownload, seedinfo.Finished, newipv6, seedinfo.UpTraffic, seedinfo.Traffic, false, seedinfo.Upload, seedinfo.Download) > 0)
                            {
                                renew = true;
                                PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "PeerCountError", string.Format("修正种子节点数 SEEDID:{0} UP:{1} ADDUP:{2} NEWUP:{3} DOWN:{4} ADDDOWN:{5} NEWDOWN:{6}", seedinfo.SeedId, seedinfo.Upload, addcount_upload, newupload, seedinfo.Download, addcount_download, newdownload)
                                + string.Format(" PEERUID:{0} EVENT:{1}", peerinfo.Uid, peer_event));
                            }
                            else
                            {
                                PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "PeerCountErrorF", string.Format("修正种子节点数 FAIL! SEEDID:{0} UP:{1} ADDUP:{2} NEWUP:{3} DOWN:{4} ADDDOWN:{5} NEWDOWN:{6}", seedinfo.SeedId, seedinfo.Upload, addcount_upload, newupload, seedinfo.Download, addcount_download, newdownload)
                                + string.Format(" PEERUID:{0} EVENT:{1}", peerinfo.Uid, peer_event));
                            }
                        }
                        //}
                        //else
                        //{
                        //    PTSeeds.UpdateSeedAnnounce(seedinfo.SeedId, addcount_upload, addcount_download, seedinfo.Finished, newipv6, seedinfo.UpTraffic, seedinfo.Traffic, true);
                        //    PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "PeerCountErrorC", string.Format("CHANGED! SEEDID:{0} UP:{1} ADDUP:{2} NEWUP:{3} DOWN:{4} ADDDOWN:{5} NEWDOWN:{6}", seedinfo.SeedId, seedinfo.Upload, addcount_upload, newupload, seedinfo.Download, addcount_download, newdownload)
                        //    + string.Format(" PEERUID:{0} EVENT:{1} NUP:{2} NDOWNLOAD:{3}", peerinfo.Uid, peer_event, ninfo.Upload, ninfo.Download));
                        //}

                    }

                    if(!renew) PTSeeds.UpdateSeedAnnounce(seedinfo.SeedId, addcount_upload, addcount_download, seedinfo.Finished, newipv6, seedinfo.UpTraffic, seedinfo.Traffic, true,0,0); 
                    

                }

                //加上本次存活时间
                oldlive += seedinfo.Live;
                //存活时间、最后做种的临时解决方法
                if (peerinfo.IsSeed)
                {
                    PTSeeds.UpdateSeedLive(peerinfo.SeedId, oldlive, peerinfo.Uid);
                    //如果为死亡种子，有人重新做种，则复活
                    if (seedinfo.Status == 3)
                    {
                        PTSeeds.UpdateSeedStatus(seedinfo.SeedId, 2);
                    }
                }

                #endregion

                #region 更新User表，用户信息，计算保种奖励

                //更新用户信息//需要更新的信息：ratio,upcount,downcount,lastactivity    ++realupload,realdownload,extcredits3,extcredits4,finished,extcredits5,extcredits6

                //这些数据必须清零或重写，否则将被累加
                btuserinfo.Extcredits5 = addupload * (decimal)seedinfo.UploadRatio;        //今天上传信息（含流量系数）
                btuserinfo.Extcredits6 = adddownload * (decimal)seedinfo.DownloadRatio;    //今天下载信息（含流量系数）
                btuserinfo.Extcredits7 = addupload;       //总计真实上传
                btuserinfo.Extcredits8 = adddownload;     //总计真实下载
                btuserinfo.Extcredits9 = addupload;      //今天真实上传
                btuserinfo.Extcredits10 = adddownload;   //今天真实下载
                btuserinfo.Extcredits11 = 0M;
                btuserinfo.Extcredits12 = 0M;

                //只有完整做种才有奖励
                if (peerinfo.IsSeed)
                {
                    //超过5分钟，计算并添加保种奖励数值
                    if ((DateTime.Now - btuserinfo.LastKeepRewardUpdateTime).TotalSeconds > 300)
                    {
                        //单线程操作
                        #region 同用户同种子单线程执行限制

                        decimal rewardcredits = 0M;
                        int totalsecond = 0;
                        //同用户、同一个种子，不能同时执行
                        if (!WaitForConCurrentUserSeed(btuserinfo.Uid, -1, true))
                        {
                            //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "TDEBUG", string.Format("[U{0}S{1}] 计算保种奖励处理等候超时或跳过 UID:{0} SEEDID:{1}", btuserinfo.Uid, seedinfo.SeedId));
                        }
                        else
                        {
                            PTUserInfo keepuserinfo = PTUsers.GetBtUserInfoForTracker(btuserinfo.Passkey);
                            DateTime updatetime = DateTime.Now;
                            if ((updatetime - keepuserinfo.LastKeepRewardUpdateTime).TotalSeconds > 900) totalsecond = 900;
                            else totalsecond = (int)(updatetime - keepuserinfo.LastKeepRewardUpdateTime).TotalSeconds;
                            PTKeepRewardStatic kr = new PTKeepRewardStatic();

                            int updateok = 0;

                            //300秒即执行，如果设置为大于300秒就不能执行了。。。
                            if (totalsecond > 280) 
                            {
                                updateok = PTUsers.UpdateUserInfo_LastKeepReward(keepuserinfo.Uid, updatetime);
                                if (updateok > 0)
                                {
                                    kr = PrivateBT.GetKeepReward(keepuserinfo);
                                    rewardcredits = kr.TotalRewardPerHour * (decimal)totalsecond / 3600M;
                                    btuserinfo.Extcredits5 += rewardcredits;
                                    btuserinfo.Extcredits11 = rewardcredits;
                                    btuserinfo.Extcredits12 = rewardcredits;

                                    if (btuserinfo.Extcredits11 > 1024 * 1024 * 1024M)
                                    {
                                        PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Error, "Reward", string.Format(
                                            "[U{0}S{6}] 保种奖励测试 OK:{5} -Last:{7} -Second:{1} -Reward:{2} -Total:{3} -BEFORE:{4} UID:{0}" +
                                        " -DETAIL: BIGBIG:{8}-{9} BIG:{10}-{11} MID:{12}-{13} SM:{14}-{15} TY:{16}-{17} ALL:{18}-{19}",
                                        btuserinfo.Uid, totalsecond, rewardcredits, btuserinfo.Extcredits5, btuserinfo.Extcredits3, updateok,
                                        seedinfo.SeedId, keepuserinfo.LastKeepRewardUpdateTime,
                                        kr.BigBig_UpCount, kr.BigBig_RewardPerHour, kr.Big_UpCount, kr.Big_RewardPerHour, kr.Mid_UpCount, kr.Mid_RewardPerHour,
                                        kr.Small_UpCount, kr.Small_RewardPerHour, kr.Tiny_UpCount, kr.Tiny_RewardPerHour, kr.TotalUpCount, kr.TotalRewardPerHour));
                                    }
                                }
                            }
                            EndConCurrentUserSeed();

                            //PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Normal, "Reward", string.Format(
                            //    "[U{0}S{6}] 保种奖励测试 OK:{5} -Last:{7} -Second:{1} -Reward:{2} -Total:{3} -BEFORE:{4} UID:{0}" + 
                            //" -DETAIL: BIGBIG:{8}-{9} BIG:{10}-{11} MID:{12}-{13} SM:{14}-{15} TY:{16}-{17} ALL:{18}-{19}", 
                            //btuserinfo.Uid, totalsecond, rewardcredits, btuserinfo.Extcredits5, btuserinfo.Extcredits3, updateok, 
                            //seedinfo.SeedId, keepuserinfo.LastKeepRewardUpdateTime,
                            //kr.BigBig_UpCount, kr.BigBig_RewardPerHour, kr.Big_UpCount, kr.Big_RewardPerHour, kr.Mid_UpCount, kr.Mid_RewardPerHour, 
                            //kr.Small_UpCount, kr.Small_RewardPerHour, kr.Tiny_UpCount, kr.Tiny_RewardPerHour, kr.TotalUpCount, kr.TotalRewardPerHour));
                        }



                        #endregion
                    }
                    
                }

                //if ((double)(btuserinfo.Extcredits4 + btuserinfo.Extcredits6) > 0)
                //{
                //    btuserinfo.Ratio = (float)(btuserinfo.Extcredits3 + btuserinfo.Extcredits5) / (float)(btuserinfo.Extcredits4 + btuserinfo.Extcredits6);
                //}
                //else btuserinfo.Ratio = 1.00001F;
                ////共享率限制规则
                ////btuserinfo.Download += btuserinfo.Extcredits6;
                //float RatioLimit = 1.00001F;
                //if (btuserinfo.Extcredits4 < 1024M * 1024 * 1024) RatioLimit = 1.00001F;
                //else if (btuserinfo.Extcredits4 < 10 * 1024M * 1024 * 1024) RatioLimit = 10;
                //else if (btuserinfo.Extcredits4 < 20 * 1024M * 1024 * 1024) RatioLimit = 50;
                //else if (btuserinfo.Extcredits4 < 50 * 1024M * 1024 * 1024) RatioLimit = 100;
                //else if (btuserinfo.Extcredits4 < 100 * 1024M * 1024 * 1024) RatioLimit = 200;
                //else if (btuserinfo.Extcredits4 < 500 * 1024M * 1024 * 1024) RatioLimit = 500;
                //else RatioLimit = 100000F;
                //if (btuserinfo.Ratio > RatioLimit) btuserinfo.Ratio = RatioLimit;
                ////if (btuserinfo.Download == 0M) btuserinfo.Ratio = 1.00001;

                btuserinfo.Ratio = (float)PTTools.GetRatio(btuserinfo.Extcredits3 + btuserinfo.Extcredits5, btuserinfo.Extcredits4 + btuserinfo.Extcredits6);

                btuserinfo.Extcredits3 = btuserinfo.Extcredits5;   //总上传信息（含流量系数及保种奖励流量）
                btuserinfo.Extcredits4 = btuserinfo.Extcredits6;

                //疑似删除peer导致死锁问题，延时2秒
                //if (peer_event == "stopped") System.Threading.Thread.Sleep(2000);


                //btuserinfo.UpCount = PrivateBT.GetSeedInfoCount(0, btuserinfo.Uid, 1, 0, "");
                //btuserinfo.DownCount = PrivateBT.GetSeedInfoCount(0, btuserinfo.Uid, 2, 0, "");
                if (peer_event == "")
                {

                }
                else
                {
                    //替换为单纯由peer表统计上传下载数
                    //btuserinfo.UploadCount = PTSeeds.GetSeedInfoCount(0, btuserinfo.Uid, 1, 0, "",0);
                    //btuserinfo.DownloadCount = PTSeeds.GetSeedInfoCount(0, btuserinfo.Uid, 2, 0, "",0);

                    btuserinfo.UploadCount = PTPeers.GetPeerUserUploadCount(btuserinfo.Uid);
                    btuserinfo.DownloadCount = PTPeers.GetPeerUserDownloadCount(btuserinfo.Uid);

                }

                //C//btuserinfo.Lastactivity = DateTime.Now.ToString();

                //更新用户信息，每30分钟更新一次积分值和用户组
                if ((DateTime.Now - btuserinfo.LastCreditsUpdateTime).TotalMinutes > 30)
                {
                    PTUsers.UpdateUserInfo_Tracker(btuserinfo, true);
                    Forum.UserCredits.UpdateUserCredits(btuserinfo.Uid); //更新总积分
                }
                else
                {
                    PTUsers.UpdateUserInfo_Tracker(btuserinfo, false);
                }

                #endregion

                #region 更新Traffic表，详细单种流量、保种时间记录

                string t_ipv4 = peerinfo.IPv4IP + "@" + peerinfo.Port;
                string t_ipv6 = peerinfo.IPv6IP + "@" + peerinfo.Port;
                string t_peerid = peerinfo.PeerId.Length > 10 ? peerinfo.PeerId.Substring(peerinfo.PeerId.Length - 10, 10) : peerinfo.PeerId;
                
                if (peer_event == "started")
                {
                    //已经有记录，session更新
                    if (gettrafficinfo_ok) PrivateBT.UpdatePerUserSeedTraffic_SessionFirst(peerinfo.Pid, seedinfo.SeedId, btuserinfo.Uid, addupload, adddownload, t_ipv4, t_ipv6, peer_left, peer_downloaded, t_peerid);
                    //没有记录，seed更新
                    else
                    {
                        PrivateBT.UpdatePerUserSeedTraffic_SeedFirst(peerinfo.Pid, seedinfo.SeedId, btuserinfo.Uid, addupload, adddownload, t_ipv4, t_ipv6, peer_left, peer_downloaded, t_peerid, (float)peerinfo.Percentage);
                        PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, seedinfo.SeedId, btuserinfo.Uid, 101, "SEED FIRST", string.Format("LEFT:{0} -PEERID:{1} -RATIO:{2} -TUP:{3} -TDOWN:{4} -LLEFT:{5}", peer_left, t_peerid, (float)peerinfo.Percentage, peerinfo.TotalUpload, peerinfo.TotalDownload, peer_left));
                    }
                }
                else if (adddownload > 0)
                {
                    //需要更新下载量
                    if (ipaddressupdated) PrivateBT.UpdatePerUserSeedTraffic_DownloadWithIP(peerinfo.Pid, seedinfo.SeedId, btuserinfo.Uid, addupload, adddownload, peer_left, peer_downloaded, t_ipv4, t_ipv6);
                    else PrivateBT.UpdatePerUserSeedTraffic_Download(peerinfo.Pid, seedinfo.SeedId, btuserinfo.Uid, addupload, adddownload, peer_left, peer_downloaded);
                }
                else
                {
                    //只更新上传量
                    if (ipaddressupdated) PrivateBT.UpdatePerUserSeedTraffic_WithIP(peerinfo.Pid, seedinfo.SeedId, btuserinfo.Uid, addupload, addkeeptime, t_ipv4, t_ipv6);
                    else PrivateBT.UpdatePerUserSeedTraffic(peerinfo.Pid, seedinfo.SeedId, btuserinfo.Uid, addupload, addkeeptime);
                }

                //是否存在异常
                if (adddownload != addleft)
                {
                    if (adddownload < addleft)
                    {
                        if (addleft - adddownload > 1 * 300 * 1024 * 1024M)
                            PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, peerinfo.SeedId, peerinfo.Uid, 163, "DOWN MISS", string.Format("ADDDOWN:{0} -ADDLEFT:{1} -CURLEFT:{2} -MARGIN:{3} -SIZE:{4} -RAW:{5}", adddownload, addleft, peer_left, addleft - adddownload, PTTools.Upload2Str(addleft - adddownload), peerinfo.RawRequestString));
                        else
                            PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, peerinfo.SeedId, peerinfo.Uid, 153, "DOWN MISS", string.Format("ADDDOWN:{0} -ADDLEFT:{1} -CURLEFT:{2} -MARGIN:{3} -SIZE:{4}", adddownload, addleft, peer_left, addleft - adddownload, PTTools.Upload2Str(addleft - adddownload)));
                    }
                    else
                    {
                        if (adddownload - addleft > 1 * 300 * 1024 * 1024M)
                            PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, peerinfo.SeedId, peerinfo.Uid, 164, "Down over", string.Format("ADDDOWN:{0} -ADDLEFT:{1} -CURLEFT:{2} -MARGIN:{3} -SIZE:{4} -RAW:{5}", adddownload, addleft, peer_left, adddownload - addleft, PTTools.Upload2Str(adddownload - addleft), peerinfo.RawRequestString));
                        else
                            PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, peerinfo.SeedId, peerinfo.Uid, 154, "Down over", string.Format("ADDDOWN:{0} -ADDLEFT:{1} -CURLEFT:{2} -MARGIN:{3} -SIZE:{4}", adddownload, addleft, peer_left, adddownload - addleft, PTTools.Upload2Str(adddownload - addleft)));
                    }
                }

                #endregion

                #region 内存回收

                peerinfo = null;
                btuserinfo = null;
                //uploadlist.Dispose();
                //uploadlist = null;
                //downloadlist.Dispose();
                //downloadlist = null;
                seedinfo = null;

                #endregion
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TrackerServEx", ex.ToString());
            }

        }

        /// <summary>
        /// 更新种子最后做种、生存时间
        /// </summary>
        /// <param name="seedinfo"></param>
        private static void GetSeedLastLive(PTSeedinfoShort seedinfo, PrivateBTPeerInfo peerinfo)
        {
            if (peerinfo.IsSeed && (peerinfo.Event == "" || peerinfo.Event == "started2mid" || peerinfo.Event == "stopped"))
            {
                //种子生存时间
                seedinfo.Live = (int)(DateTime.Now - seedinfo.LastLive).TotalSeconds; //即两次Tracker访问的时间间隔
                //种子最后做种时间
                seedinfo.LastLive = DateTime.Now;
                //种子最后做种者
                //seedinfo.LastSeederId = btuserinfo.Uid;
                //seedinfo.LastSeederName = btuserinfo.Username;
            }
            else if (peerinfo.IsSeed)
            {
                //种子最后做种时间
                seedinfo.LastLive = DateTime.Now;
                //种子最后做种者
                //seedinfo.LastSeederId = btuserinfo.Uid;
                //seedinfo.LastSeederName = btuserinfo.Username;
            }

        }
        /// <summary>
        /// 计算流量信息
        /// </summary>
        private static void CalcPeerTraffic(PrivateBTPeerInfo peerinfo, PrivateBTPeerInfo oldpeerinfo, ref decimal addupload, ref decimal adddownload, ref decimal addleft, ref int addkeeptime)
        {
            addupload = peerinfo.Upload - oldpeerinfo.Upload;      //计算上传下载增量
            adddownload = peerinfo.Download - oldpeerinfo.Download;
            addleft = oldpeerinfo.Left - peerinfo.Left;
            

            if (addupload < 0M)
            {
                peerinfo.Event = peerinfo.Event + "_e1";
                PTPeerLog.InsertPeerErrorInfo("UPLOAD", addupload, peerinfo, oldpeerinfo);
                peerinfo.Upload = oldpeerinfo.Upload;
                addupload = 0M;
            }
            if (adddownload < 0M)
            {
                peerinfo.Event = peerinfo.Event + "_e2";
                PTPeerLog.InsertPeerErrorInfo("DOWNLOAD", addupload, peerinfo, oldpeerinfo);
                peerinfo.Download = oldpeerinfo.Download;
                adddownload = 0M;
            }

            double timediv = (DateTime.Now - oldpeerinfo.LastTime).TotalSeconds;
            if (timediv > 0) peerinfo.UploadSpeed = (double)addupload / timediv;
            else timediv = 1;
            addkeeptime = (int)timediv;
           
            if (peerinfo.UploadSpeed > 1024 * 1024 * 12.5)
            {
                peerinfo.Event = peerinfo.Event + "_e3";
                PTPeerLog.InsertPeerErrorInfo("UPSPEED", addupload, peerinfo, oldpeerinfo);
                //上传速度大于80M/s的异常情况，上传清零
                if (peerinfo.UploadSpeed > 1024 * 1024 * 80)
                {
                    peerinfo.UploadSpeed = 1024 * 1024 * 80;
                    addupload = 0M;
                }
            }
            if (timediv > 0) peerinfo.DownloadSpeed = (double)adddownload / timediv;
            if (peerinfo.DownloadSpeed > 1024 * 1024 * 12.5)
            {
                peerinfo.Event = peerinfo.Event + "_e4";
                PTPeerLog.InsertPeerErrorInfo("DOWNSPEED", addupload, peerinfo, oldpeerinfo);
                if (peerinfo.DownloadSpeed > 1024 * 1024 * 80)
                {
                    peerinfo.DownloadSpeed = 1024 * 1024 * 80;
                    adddownload = 0M;
                }
            }
        }

        private static DateTime LastStopTime = DateTime.Now;
        private static object LastStopSynObject = new object();
        /// <summary>
        /// 排队执行停止操作，返回需要等候的时间
        /// </summary>
        /// <returns></returns>
        private static int GetStopDelay()
        {
            double wait = 0;
            DateTime cur = DateTime.Now;
            lock (LastStopSynObject)
            {
                if (LastStopTime < cur.AddMilliseconds(-30)) LastStopTime = cur;
                else if ((LastStopTime - cur).TotalSeconds > 20) LastStopTime = LastStopTime.AddMilliseconds(10);
                else if ((LastStopTime - cur).TotalSeconds > 10) LastStopTime = LastStopTime.AddMilliseconds(20);
                else LastStopTime = LastStopTime.AddMilliseconds(30);

                wait = (LastStopTime - cur).TotalMilliseconds;
            }
            if (wait > 30000) return 30000;
            else return (int)wait;
        }

        /// <summary>
        /// 获取IP状态
        /// 0.校内纯IPv4，1.校内IPv4+ISATAP，2.校内IPv4+异常v6，3.校内IPv4+原生IPv6，4.校内纯ISATAP，5.校内纯原生IPv6，6.校外IPv6，7.IPv6首次更新，-1.校外IPv4，-2.无法判断
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <param name="oldpeerinfo"></param>
        private int GetIPStatus(PrivateBTPeerInfo peerinfo, PrivateBTPeerInfo oldpeerinfo)
        {
            int ipv4status = (int)PTTools.GetIPType(peerinfo.IPv4IP);
            int ipv6status = (int)PTTools.GetIPType(peerinfo.IPv6IP);

            //IPType：0.校内v4，1.校内ISATAP v6，2.校内原生v6，3.校外v6，4.校外v4，5.teredo v6，-100.解析错误
            //v4可能值： 0, 4, -100
            //v6可能值： 1, 2, 3, 5, -100

            //0.校内纯IPv4
            if(ipv4status == 0 && ipv6status == -100) return 0;
            //1.校内IPv4+ISATAP
            if(ipv4status == 0 && ipv6status == 1) return 1;
            //2.校内IPv4+异常v6
            if(ipv4status == 0 && ipv6status == 3) return 2;
            //3.校内IPv4+原生IPv6
            if(ipv4status == 0 && ipv6status == 2) return 3;

            //4.校内纯ISATAP
            if(ipv4status == -100 && ipv6status == 1) return 4;
            //5.校内纯原生IPv6
            if(ipv4status == -100 && ipv6status == 2) return 5;
            //6.校外IPv6
            if(ipv4status == -100 && ipv6status == 3) return 6;
            //-2.无法判断
            if(ipv4status == -100 && ipv6status == -100) return -2;
            //-1.校外IPv4
            if(ipv4status == 4) return -1;
                
            return -2;
        }

        private int CreatePeerInfo(PrivateBTPeerInfo peerinfo, ref bool gettrafficinfo_ok)
        {
            decimal totalupload = 0M;
            decimal totaldownload = 0M;
            decimal lastleft = 0M;
            int keeptime = 0;

            peerinfo.FirstTime = DateTime.Now;

            //获取流量历史记录
            if (PrivateBT.GetPerUserSeedTraffic(peerinfo.SeedId, peerinfo.Uid, ref totalupload, ref totaldownload, ref lastleft, ref keeptime) > 0)
            {
                //lastleft默认-1，首次种子更新时更新
                if (lastleft >= 0)
                {
                    gettrafficinfo_ok = true;
                    peerinfo.TotalUpload = totalupload;
                    peerinfo.TotalDownload = totaldownload;
                    peerinfo.KeepTime = keeptime;
                }
            }

            //如果上次left值与本次不同
            if (lastleft != peerinfo.Left && lastleft > -1)
            {
                if (peerinfo.Left < lastleft)
                {
                    if (lastleft - peerinfo.Left > 1 * 300 * 1024 * 1024M)
                        PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, peerinfo.SeedId, peerinfo.Uid, 161, "LEFT DECREASE", string.Format("U CUR:{0} -LAST:{1} -MARGIN:{2} -SIZE:{3} -RAW:{4}", peerinfo.Left, lastleft, lastleft - peerinfo.Left, PTTools.Upload2Str(lastleft - peerinfo.Left), peerinfo.RawRequestString));
                    else
                        PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, peerinfo.SeedId, peerinfo.Uid, 151, "LEFT DECREASE", string.Format("U CUR:{0} -LAST:{1} -MARGIN:{2} -SIZE:{3}", peerinfo.Left, lastleft, lastleft - peerinfo.Left, PTTools.Upload2Str(lastleft - peerinfo.Left)));
                }
                else
                {
                    if (peerinfo.Left - lastleft > 1 * 300 * 1024 * 1024M)
                        PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, peerinfo.SeedId, peerinfo.Uid, 162, "Left Increase", string.Format("U CUR:{0} -LAST:{1} -MARGIN:{2} -SIZE:{3} -RAW:{4}", peerinfo.Left, lastleft, peerinfo.Left - lastleft, PTTools.Upload2Str(peerinfo.Left - lastleft), peerinfo.RawRequestString));
                    else
                        PTPeerLog.InsertPeerHistoryLog(peerinfo.Pid, peerinfo.SeedId, peerinfo.Uid, 152, "Left Increase", string.Format("U CUR:{0} -LAST:{1} -MARGIN:{2} -SIZE:{3}", peerinfo.Left, lastleft, peerinfo.Left - lastleft, PTTools.Upload2Str(peerinfo.Left - lastleft)));
                }
            }

            int rtv = PrivateBT.InsertPeerInfo(peerinfo);
            if (rtv < 1) PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "TDEBUG", string.Format("错误 INSERT PEERINFO UID:{0} SEEDID:{1} EVENT:{2}", peerinfo.Uid, peerinfo.SeedId, peerinfo.Event));
            return rtv;
        }

        private bool ipaddressupdated = false;
        /// <summary>
        /// 更新peerinfo  DataTable uploadlist, DataTable downloadlist, 
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <param name="oldpeerinfo"></param>
        private int UpdatePeerInfo(PrivateBTPeerInfo peerinfo, PrivateBTPeerInfo oldpeerinfo, bool IsIPv6Request, int seedid, bool globalblue, ref bool ipv6added)
        {
            //注意：引用类型参数传递会直接修改原值！

            #region IP地址变化情况
            
            //原peerinfo为纯IPv6
            if (oldpeerinfo.IPv6IP != "IP_NULL" && oldpeerinfo.IPv4IP == "IP_NULL")
            {
                //当前为IPv6 Tracker
                if (IsIPv6Request)
                {
                    if (peerinfo.Event == "mid2started")
                    {
                        if (peerinfo.Upload != oldpeerinfo.Upload)
                        {
                            PTPeerLog.InsertPeerErrorInfo(6, 0, "异常Tracker启动1A", peerinfo, oldpeerinfo);
                            if (peerinfo.Upload < oldpeerinfo.Upload) peerinfo.Upload = oldpeerinfo.Upload;
                            if (peerinfo.Download < oldpeerinfo.Download) peerinfo.Download = oldpeerinfo.Download;
                        }
                    }
                    //PrivateBT.InsertFinished(seedinfo.SeedId, btuserinfo.Uid, 0M, 0M);
                    //btuserinfo.Finished = 1;
                    //seedinfo.Finished = 1;
                }
                //当前为IPv4 Tracker//不大可能出现的情况//校内一直存在IPv4，校外不可能有IPv4
                else
                {
                    peerinfo.IPv6IP = oldpeerinfo.IPv6IP;
                    if (peerinfo.Event == "mid2started")
                    {
                        if (peerinfo.Upload != oldpeerinfo.Upload)
                        {
                            PTPeerLog.InsertPeerErrorInfo(6, 0, "异常Tracker启动1A", peerinfo, oldpeerinfo);
                            if (peerinfo.Upload < oldpeerinfo.Upload) peerinfo.Upload = oldpeerinfo.Upload;
                            if (peerinfo.Download < oldpeerinfo.Download) peerinfo.Download = oldpeerinfo.Download;
                        }
                    }
                }
            }
            //原peerinfo为纯IPv4
            else if (oldpeerinfo.IPv6IP == "IP_NULL" && oldpeerinfo.IPv4IP != "IP_NULL")
            {
                //当前为IPv6 Tracker//可能出现，校内隧道联通
                if (IsIPv6Request)
                {
                    peerinfo.IPv4IP = oldpeerinfo.IPv4IP;
                    if (peerinfo.IsSeed) ipv6added = true;

                    if (peerinfo.Event == "mid2started")
                    {
                        if (peerinfo.Upload != oldpeerinfo.Upload)
                        {
                            PTPeerLog.InsertPeerErrorInfo(6, 0, "异常Tracker启动2A", peerinfo, oldpeerinfo);
                            if (peerinfo.Upload < oldpeerinfo.Upload) peerinfo.Upload = oldpeerinfo.Upload;
                            if (peerinfo.Download < oldpeerinfo.Download) peerinfo.Download = oldpeerinfo.Download;
                        }
                    }
                }
                //当前为IPv4 Tracker
                else
                {
                    //PrivateBT.InsertFinished(seedinfo.SeedId, btuserinfo.Uid, 0M, 0M);
                    //btuserinfo.Finished = 1;
                    //seedinfo.Finished = 1;
                    if (peerinfo.Event == "mid2started")
                    {
                        if (peerinfo.Upload != oldpeerinfo.Upload)
                        {
                            PTPeerLog.InsertPeerErrorInfo(6, 0, "异常Tracker启动2B", peerinfo, oldpeerinfo);
                            if (peerinfo.Upload < oldpeerinfo.Upload) peerinfo.Upload = oldpeerinfo.Upload;
                            if (peerinfo.Download < oldpeerinfo.Download) peerinfo.Download = oldpeerinfo.Download;
                        }
                    }
                }
            }
            //原来为双栈tracker，v6的为确定信息，先走上面造成双tracker的可能性很小
            else
            {
                if (IsIPv6Request)
                { 
                    peerinfo.IPv4IP = oldpeerinfo.IPv4IP;
                    //PrivateBT.InsertFinished(seedinfo.SeedId, btuserinfo.Uid, 0M, 0M);
                    //btuserinfo.Finished = 1;
                    //seedinfo.Finished = 1;
                    if (peerinfo.Event == "mid2started")
                    {
                        if (peerinfo.Upload != oldpeerinfo.Upload)
                        {
                            PTPeerLog.InsertPeerErrorInfo(6, 0, "异常Tracker启动0A", peerinfo, oldpeerinfo);
                            if (peerinfo.Upload < oldpeerinfo.Upload) peerinfo.Upload = oldpeerinfo.Upload;
                            if (peerinfo.Download < oldpeerinfo.Download) peerinfo.Download = oldpeerinfo.Download;
                        }
                    }
                }
                else 
                {
                    peerinfo.IPv6IP = oldpeerinfo.IPv6IP;
                                        if (peerinfo.Event == "mid2started")
                    {
                        if (peerinfo.Upload != oldpeerinfo.Upload)
                        {
                            PTPeerLog.InsertPeerErrorInfo(6, 0, "异常Tracker启动1A", peerinfo, oldpeerinfo);
                            if (peerinfo.Upload < oldpeerinfo.Upload) peerinfo.Upload = oldpeerinfo.Upload;
                            if (peerinfo.Download < oldpeerinfo.Download) peerinfo.Download = oldpeerinfo.Download;
                        }
                    }
                }
            }

            #endregion

            //区域信息读取（因为v6暂时无法判断区域，如果是v6访问，则从旧数据中读取）
            //if (peerinfo.IPRegionInBuaa == PTsIpRegionInBuaa.INIT && oldpeerinfo.IPRegionInBuaa != PTsIpRegionInBuaa.INIT)
            //    peerinfo.IPRegionInBuaa = oldpeerinfo.IPRegionInBuaa;

            peerinfo.PeerLock = oldpeerinfo.PeerLock;

            //????????
            //peerinfo.LastSend = GetSendingMode(peerinfo.Percentage == 1.0 ? false : true, peerinfo.IPRegionInBuaa, uploadlist, downloadlist, seedid, globalblue);

            peerinfo.Pid = oldpeerinfo.Pid;

            //处理v6首次在insertpeerinfo。。。。校内IPv6首次更新的情况，有可能还没有v4地址信息，此时只发送校外地址
            //if (peerinfo.IPStatus == 7)
            //{
            //    PTLog.InsertSystemLog(PTLog.LogType.PeerIPLimit, PTLog.LogStatus.Normal, "校内v6首次", string.Format("SEED:{0} -IP:{1}", peerinfo.SeedId, peerinfo.IPv6IP));
            //    peerinfo.LastSend = 255;
            //}
            

            //IPv6IPAdd，不与IPv6IP重复，如果重复则设置为IP_NULL
            if (peerinfo.IPv6IPAdd == peerinfo.IPv6IP) peerinfo.IPv6IPAdd = "IP_NULL";
            if (peerinfo.IPv6IPAdd == oldpeerinfo.IPv6IP) peerinfo.IPv6IPAdd = "IP_NULL";

            //if (peerinfo.IPv6IPAdd == "IP_NULL" && oldpeerinfo.IPv6IPAdd != "IP_NULL") peerinfo.IPv6IPAdd = oldpeerinfo.IPv6IPAdd;
            if (peerinfo.IPv6IP == "IP_NULL" && oldpeerinfo.IPv6IP != "IP_NULL") peerinfo.IPv6IP = oldpeerinfo.IPv6IP;
            if (peerinfo.IPv4IP == "IP_NULL" && oldpeerinfo.IPv4IP != "IP_NULL") peerinfo.IPv4IP = oldpeerinfo.IPv4IP;
            
            
            
            
            //临时更新措施：每一个都判断
            //peerinfo.IPStatus = GetIPStatus(peerinfo, oldpeerinfo);
            //if (peerinfo.IPStatus == 0 && oldpeerinfo.IPStatus != 0) peerinfo.IPStatus = oldpeerinfo.IPStatus;
            // peerinfo.IPStatus != oldpeerinfo.IPStatus ||

            peerinfo.Pid = oldpeerinfo.Pid;
            if (peerinfo.Pid < 1) return -1;

            #region 执行更新

            //地址有任何变化，则全部更新
            if ( peerinfo.IPv4IP != oldpeerinfo.IPv4IP || peerinfo.IPv6IP != oldpeerinfo.IPv6IP || peerinfo.IPv6IPAdd != oldpeerinfo.IPv6IPAdd
                || peerinfo.IsSeed != oldpeerinfo.IsSeed || peerinfo.Port != oldpeerinfo.Port || oldpeerinfo.IPStatus == 7 || oldpeerinfo.IPRegionInBuaa == PTsIpRegionInBuaa.INIT
                || peerinfo.LastSend != oldpeerinfo.LastSend)
            {
                ipaddressupdated = true;
                //更新IP状态
                peerinfo.IPStatus = GetIPStatus(peerinfo, oldpeerinfo);
                if (peerinfo.IPStatus == 4 || peerinfo.IPStatus == 5)
                {
                    //校内纯IPv6节点

                }

                return PrivateBT.UpdatePeerInfo(peerinfo, IsIPv6Request);
            }

            else if (peerinfo.Download != oldpeerinfo.Download || peerinfo.DownloadSpeed != oldpeerinfo.DownloadSpeed || peerinfo.Percentage != oldpeerinfo.Percentage || peerinfo.Left != oldpeerinfo.Left)
            {
                return PrivateBT.UpdatePeerInfo_UpDownTrafficOnly(peerinfo, IsIPv6Request);
            }
            else if (peerinfo.Upload != oldpeerinfo.Upload || peerinfo.UploadSpeed != oldpeerinfo.UploadSpeed)
            {
                return PrivateBT.UpdatePeerInfo_UpTrafficOnly(peerinfo, IsIPv6Request);
            }
            else
            {
                return PrivateBT.UpdatePeerInfo_NoTraffic(peerinfo, IsIPv6Request);
            }
            
            #endregion
        }

        /// <summary>
        /// 发送出错信息
        /// </summary>
        /// <param name="errormessage"></param>
        protected void SendErrorMessage(string errormessage)
        {
            SendErrorMessage(errormessage, true);
        }
        /// <summary>
        /// 发送出错信息
        /// </summary>
        /// <param name="errormessage"></param>
        protected void SendErrorMessage(string errormessage, bool endconcurrent)
        {
            if(endconcurrent) EndConCurrentUserSeed();
            try
            {
                Response.Clear();
                //C//Response.ClearHeaders();
                Response.ContentType = "text/plain";
                Response.Write(string.Format("d14:failure reason{0}:{1}e", Encoding.UTF8.GetByteCount(errormessage), errormessage));
                Response.Flush();
                //C//Response.Close();
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TDEBUG", "SENDERRMESSAGE:" + ex.ToString());
            }
        }


        /// <summary>
        /// 发送等候信息
        /// </summary>
        /// <param name="seconds"></param>
        protected void SendWaitMessage(int seconds)
        {
            EndConCurrentUserSeed();
            try
            {
                Response.Clear();
                //C//Response.ClearHeaders();
                Response.ContentType = "text/plain";
                Response.Write(string.Format("d8:completei0e10:incompletei0e8:intervali{0}e5:peerslee", seconds));
                Response.Flush();
                //C//Response.Close();
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TDEBUG", "SENDWAITMESSAGE:" + ex.ToString());
            }
        }



        /// <summary>
        /// 判断应该使用的发送模式，-1 只发送学院路校区，-2 只发送沙河校区， 0不限制，>0 只发送对应区域
        /// 校外用户不限制
        /// </summary>
        /// <returns></returns>
        private int GetSendingMode(bool isdownload, PTsIpRegionInBuaa currentregion, DataTable uploadlist, DataTable downloadlist, int seedid, bool globalblue)
        {
            if (!isdownload) return 0;

            //限制模式：7:30-11:30 低等级，11:30-14:30高等级，14:30-17:30低等级，17:30-0:30高等级，0:30-1:30低等级，1:30-7:30不限制
            double currenthour = PTTools.GetDateTimeNow();
            if (globalblue)
            {
                if (currenthour > 1.5 && currenthour < 7.5) return 0;
            }
            else
            {
                if (currenthour > 1.5 && currenthour < 7.5) return 0;
            }
            
            
            if(currentregion == PTsIpRegionInBuaa.INIT  || currentregion == PTsIpRegionInBuaa.UNKNOWN_AREA || currentregion == PTsIpRegionInBuaa.NOT_IN_BUAA) return 0;
            if(currentregion == PTsIpRegionInBuaa.ERROR ) return 0;
            if(uploadlist == null || uploadlist.Rows.Count < 5) return 0;


            //当前区域
            int currentregionid = (int)currentregion;

            //当前校区：-1学院路，-2沙河
            int currentdistrict = 0;
            if(currentregionid > 100 && currentregionid <= 200) currentdistrict = -2;
            if(currentregionid > 0 && currentregionid <= 100) currentdistrict = -1;
            
            //统计各分区数量
            int current_region_count = 0;
            int shahe_region_count = 0;
            int xueyuan_region_count = 0;
            int total_count = 0;
            for (int i = 0; i < uploadlist.Rows.Count; i++ )
            {
                int rid = TypeConverter.ObjectToInt(uploadlist.Rows[i]["ipregioninbuaa"], 0);
                if (rid == currentregionid) current_region_count++;
                if (rid > 100 && rid <= 200) shahe_region_count++;
                else if(rid <= 100 && rid > 0) xueyuan_region_count++;
                total_count++;
            }


            //正常模式
            int districtlimit = 8; //本校区种子数大于8，不发送跨区
            int regionlimit = 10; //本区域种子数大于10，不发送跨区

            //全站蓝种时，采取更严格控制措施
            if (globalblue) 
            {
                districtlimit = 3;
                regionlimit = 3;
            }
            //低等级限制，0.5-1.5，7.5-11.5，14.5-17.5
            else if ((currenthour >= 0.5 && currenthour <= 1.5) || (currenthour >= 7.5 && currenthour <= 11.5) || (currenthour >= 14.5 && currenthour <= 17.5))
            {
                districtlimit = 16;
                regionlimit = 20;
            }

            ////高等级限制，0-0.5，11.5-14.5，17.5-24
            //else if ((currenthour < 0.5) || (currenthour > 11.5 && currenthour < 14.5) || (currenthour > 17.5 && currenthour < 24))
            //{
            //    districtlimit = 8;
            //    regionlimit = 10;
            //}

            //寒暑假模式
            //int districtlimit = -1;
            //int regionlimit = 20;


            if (current_region_count > regionlimit)
            {
                //if(lastmode != currentregionid ) 
                //PTLog.InsertSystemLogDebug(PTLog.LogType.PeerIPLimit, PTLog.LogStatus.Normal, "IP优化：限本区", string.Format("SEED:{0} -CUR:{1} -CURCOUNT:{2} -TOTAL:{3}", seedid, currentregion, current_region_count, total_count));
                return currentregionid;
            }
            if (districtlimit > 0)
            {
                //当前为沙河校区，并且沙河校区节点数大于8
                if ((currentdistrict == -2) && shahe_region_count > districtlimit)
                {
                    //if(lastmode != -2)
                    //PTLog.InsertSystemLogDebug(PTLog.LogType.PeerIPLimit, PTLog.LogStatus.Normal, "IP优化：限沙河", string.Format("SEED:{0} -CUR:{1} -CURCOUNT:{2} -SHAHE:{3} -TOTAL{4}", seedid, currentregion, current_region_count, shahe_region_count, total_count));
                    return -2;
                }
                //当前为学院路校区，并且学院路校区节点数大于8
                if ((currentdistrict == -1) && xueyuan_region_count > districtlimit)
                {
                    //if (lastmode != -1)
                    //PTLog.InsertSystemLogDebug(PTLog.LogType.PeerIPLimit, PTLog.LogStatus.Normal, "IP优化：限学院路", string.Format("SEED:{0} -CUR:{1} -CURCOUNT:{2} -XUEYUANLU:{3} -TOTAL{4}", seedid, currentregion, current_region_count, xueyuan_region_count, total_count));
                    return -1;
                }
            }
            

            return 0;
        }
        /// <summary>
        /// 发送正常信息
        /// </summary>
        /// <param name="uploadcount"></param>
        /// <param name="downloadcount"></param>
        /// <param name="uploadlist"></param>
        /// <param name="downloadlist"></param>
        protected void SendPeerList(int ipstatus, int numwant, int uploadcount, int downloadcount, DataTable uploadlist, DataTable downloadlist, int currentuid, string currentip, string currentipv6add, bool isdownload, bool nopeerid, bool initialseeder, int sendmode, int currentregion, int nextupdate_second)
        {
            #region 变量定义

            //C//EndConCurrentUserSeed();
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(28000);
            string rsspub_delay = "";

            //不能是NULL，数据库中NULL表示不存在
            if (currentip == "IP_NULL") currentip = "";
            if (currentipv6add == "IP_NULL") currentipv6add = "";
            string peerip = "", peerport = "", peerid = ""; ; string peeripv6add = ""; string peeripv6 = "";
            int peeruid = -1;
            int uplistcount = (uploadlist == null ? 0: uploadlist.Rows.Count);
            int downlistcount = (downloadlist == null ? 0: downloadlist.Rows.Count);
            int peeripstatus = -3;
            int peeripregion = 0;
            int peerlastsend = 0;

            #endregion

            #region  信息头:种子数，下载数，下次更新时间
            
            strBuilder.AppendFormat("d8:completei{0}e10:incompletei{1}e8:intervali{2}e5:peersl", uploadcount, downloadcount, nextupdate_second);
            
            #endregion

            for (int i = 0; i < numwant; i++)
            {
                #region 获取PEER信息

                peeripv6add = "";
                if (i < uplistcount)
                {
                    //对于做种用户，无需发送正在做种的peer
                    if (!isdownload) { numwant++; continue; }
                    peeripstatus = TypeConverter.ObjectToInt(uploadlist.Rows[i]["isipv6"], -1);
                    peeruid = TypeConverter.ObjectToInt(uploadlist.Rows[i]["uid"], -1);
                    if (!nopeerid) peerid = uploadlist.Rows[i]["peerid"].ToString();
                    peerip = (peeripstatus < 4) ? uploadlist.Rows[i]["ip"].ToString().Trim() : "";
                    peeripv6 = (peeripstatus > 0) ? uploadlist.Rows[i]["ipv6ip"].ToString().Trim() : "";
                    peerport = uploadlist.Rows[i]["port"].ToString().Trim();
                    peeripregion = TypeConverter.ObjectToInt(uploadlist.Rows[i]["ipregioninbuaa"], 0);
                    peerlastsend = TypeConverter.ObjectToInt(uploadlist.Rows[i]["lastsend"], 0);
                    if (peeripstatus > 0 && uploadlist.Rows[i]["ipv6addip"] != null) peeripv6add = uploadlist.Rows[i]["ipv6addip"].ToString().Trim();
                }
                else if (i < uplistcount + downlistcount)
                {
                    peeripstatus = TypeConverter.ObjectToInt(downloadlist.Rows[i - uplistcount]["isipv6"], -1);
                    peeruid = TypeConverter.ObjectToInt(downloadlist.Rows[i - uplistcount]["uid"], -1);
                    if (!nopeerid) peerid = downloadlist.Rows[i - uplistcount]["peerid"].ToString();
                    peerip = (peeripstatus < 4) ? downloadlist.Rows[i - uplistcount]["ip"].ToString().Trim() : "";
                    peeripv6 = (peeripstatus > 0) ? downloadlist.Rows[i - uplistcount]["ipv6ip"].ToString().Trim() : "";
                    peerport = downloadlist.Rows[i - uplistcount]["port"].ToString().Trim();
                    peeripregion = TypeConverter.ObjectToInt(downloadlist.Rows[i - uplistcount]["ipregioninbuaa"], 0);
                    peerlastsend = TypeConverter.ObjectToInt(downloadlist.Rows[i - uplistcount]["lastsend"], 0);
                    if (peeripstatus > 0 && downloadlist.Rows[i - uplistcount]["ipv6addip"] != null) 
                        peeripv6add = downloadlist.Rows[i - uplistcount]["ipv6addip"].ToString().Trim();
                }
                else break;

                if (!nopeerid)
                {
                    string o_peerid = "";
                    for (int l = 0; l < peerid.Length - 1; l += 2)
                    {
                        o_peerid += Char.ConvertFromUtf32(Convert.ToInt32("0x" + peerid.Substring(l, 2), 16)).ToString();
                    }
                    peerid = o_peerid;
                    peerid = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(o_peerid));
                }
                #endregion

                #region 跳过相同uid的用户和相同IP的用户

                if (peeruid == currentuid) continue;
                if (currentip != "")
                {
                    if (peerip == currentip) continue;
                    if (peeripv6 == currentip) continue;
                    if (peeripv6add == currentip) continue;
                }
                if (currentipv6add != "")
                {
                    if (peeripv6 == currentipv6add) continue;
                    if (peeripv6add == currentipv6add) continue;
                }

                #endregion

                #region IPv4和IPv6区分发送策略，跳过无用地址

                //////////////////////////////////////////////////////////////////////////
                //IP区分发送策略
                    
                //待发送数据预处理
                //校外IPv4地址或其他异常地址总是清空
                if (peeripstatus < 0) 
                {
                    peerip = "";
                    peeripv6 = "";
                    peeripv6add = "";
                }

                //根据来访者IP判断和处理

                //-1，校外IPv4，不发送任何地址
                if (ipstatus < 0) break;
                //0，校内纯IPv4，只发送IPv4地址
                else if (ipstatus == 0)
                {
                    peeripv6 = "";
                    peeripv6add = "";
                }
                //1，校内IPv4 + ISATAP，只发送校内IPv4地址和（纯v6地址）
                else if (ipstatus == 1)
                {
                    if (peeripstatus != 4 && peeripstatus != 5 && peeripstatus != 6)
                    {
                        peeripv6 = "";
                        peeripv6add = "";
                    }
                }
                //2，校内IPv4 + 异常v6，只发送IPv4地址
                else if (ipstatus == 2)
                {
                    peeripv6 = "";
                    peeripv6add = "";
                }
                //3，校内IPv4 + 原生IPv6，比isatap的增加发送原生IPv6地址
                else if (ipstatus == 3)
                {
                    if (peeripstatus == 1 || peeripstatus == 2 || peeripstatus == 7)
                    {
                        peeripv6 = "";
                        peeripv6add = "";
                    }
                }
                //4，校内纯ISATAP 5，校内原生v6  6，校外v6   不发送v4信息 
                else if (ipstatus == 4 || ipstatus == 5 || ipstatus == 6)
                {
                    //if (peeripstatus == 0 || peeripstatus == 1 || peeripstatus == 2 || peeripstatus == 3)
                    //{
                        peerip = "";
                    //}
                }
                //7，校内IPv6首次更新，按纯IPv4处理，不发送v6信息
                else if (ipstatus == 7)
                {
                    peeripv6 = "";
                    peeripv6add = "";
                }
                else
                {
                    //未知情况
                    break;
                }


                //IP区分发送策略结束
                //////////////////////////////////////////////////////////////////////////
                #endregion

                #region FGBTACC加速机的SAS加速盘，不受地理位置发送影响

                bool peer_fgbtacc_rssupload = false;
                if ((peeruid == 2268 || peeruid == 22854) && (peeripv6add == "FGBTACC_RSSUPLOAD" || peerip == "xxx.xxx.xxx.xxx")) peer_fgbtacc_rssupload = true;
                bool current_fgbtacc_rssupload = false;
                if ((currentuid == 2268 || currentuid == 22854) && (currentipv6add == "FGBTACC_RSSUPLOAD" || currentip == "xxx.xxx.xxx.xxx")) current_fgbtacc_rssupload = true;

                #endregion

                #region 地理位置区分发送策略，跳过非本区的地址

                if (sendmode == 255) break;
                //当前节点为下载
                if (isdownload)
                {
                    //校外用户除外：正常发送列表中的所有校外地址
                    if (peeripstatus != 6 && !peer_fgbtacc_rssupload)
                    {
                        if (peeripregion == 255) continue;
                        //只发送沙河校区的情况
                        if (sendmode == -2) if (peeripregion <= 100 || peeripregion > 200) continue;
                        //只发送学院路校区的情况
                        if (sendmode == -1) if (peeripregion <= 0 || peeripregion > 100) continue;
                        //只发送本区的情况
                        if (sendmode > 0) if (peeripregion != sendmode) continue;
                    }
                }
                //当前节点为上传，同样不向其发送其他受限下载者的IP 
                else 
                {
                    //校外用户除外：对于校外用户，正常发送列表中的所有地址
                    if (ipstatus != 6 && !current_fgbtacc_rssupload)
                    {
                        if (peerlastsend == 255) continue;
                        //只发送沙河校区的情况
                        if (peerlastsend == -2) if (currentregion <= 100 || currentregion > 200) continue;
                        //只发送学院路校区的情况
                        if (peerlastsend == -1) if (currentregion <= 0 || currentregion > 100) continue;
                        //只发送本区的情况
                        if (peerlastsend > 0) if (currentregion != peerlastsend) continue;
                    }
                }

                #endregion

                #region 当前用户是IMAX和Core2的特殊处理，种子数较多时，不给IMAX和Core2发送下载节点

                if ((currentuid == 13) && (uplistcount > 10 || uplistcount > 1 && downlistcount > 15) && !isdownload)
                {
                    //IMAX和CORE2，当上传者大于20时，不再向IMAX和CORE2发送记录
                    break;
                }
                else if ((currentuid == 7442) && (uplistcount > 10 || uplistcount > 1 && downlistcount > 15) && !isdownload)
                {
                    //IMAX和CORE2，当上传者大于20时，不再向IMAX和CORE2发送记录
                    break;
                }

                #endregion

                #region 发送列表中的RSSACC、IMAX、Core2特殊处理

                if (peeruid == 22854 || peeruid == 2268)
                {
                    if (peerip != "xxx.xxx.xxx.xxx" && peerip != "xxx.xxx.xxx.xxx")
                    {
                        //RSSUPLOAD和PUB，如果RSSUPLOAD存在则发送RSSUPLOAD，否则发送PUB，除此之外为RSS 正常发送
                        if (peeripv6add != "FGBTACC_RSSUPLOAD")
                        {
                            //PUB，如果没有发送过，则记录，等待发送
                            if (rsspub_delay != "SEND")
                            {
                                rsspub_delay = string.Format("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peerip), peerip, peerport);
                                if(peeripv6 != "" && peeripv6 != "IP_NULL")
                                    rsspub_delay += string.Format("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerport);
                            }
                            //PUB的内容暂时不发送
                            continue;
                        }
                        else
                        {
                            //RSSUPLOAD，正常发送，记录状态
                            rsspub_delay = "SEND";
                            peeripv6add = "";
                        }
                    }
                }
                else if (sendmode == 0 && (peeruid == 13) && (uplistcount > 10 || uplistcount > 1 && downlistcount > 15))
                {
                    //IMAX和CORE2，当上传者大于10时，不再发送IMAX和CORE2的记录
                    continue;
                }
                else if (sendmode == 0 && (peeruid == 7442) && (uplistcount > 10 || uplistcount > 1 && downlistcount > 15))
                {
                    //IMAX和CORE2，当上传者大于10时，不再发送IMAX和CORE2的记录
                    continue;
                }

                #endregion

                #region 地址发送

                //v4地址发送
                if (peerip != "" && peerip != "IP_NULL")
                {
                    if(nopeerid) strBuilder.AppendFormat("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peerip), peerip, peerport);
                    else strBuilder.AppendFormat("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peerip), peerip, peerid, peerport, Encoding.ASCII.GetByteCount(peerid));
                    if (ipstatus == 6)
                    {
                        PTLog.InsertSystemLogDebug_WithPageIP(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Error, "ErrSendIPv4", "SEND:" + peerip);
                    }
                }
                //v6地址发送
                if (peeripv6 != "" && peeripv6 != "IP_NULL")
                {
                    //首先发送IPv6地址
                    if (nopeerid) strBuilder.AppendFormat("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerport);
                    else strBuilder.AppendFormat("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerid, peerport, Encoding.ASCII.GetByteCount(peerid));
                }
                //v6附加地址发送
                if (peeripv6add != "" && peeripv6add != "IP_NULL" )
                {
                    //首先发送IPv6地址
                    if (nopeerid) strBuilder.AppendFormat("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peeripv6add), peeripv6add, peerport);
                    else strBuilder.AppendFormat("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6add), peeripv6add, peerid, peerport, Encoding.ASCII.GetByteCount(peerid));
                }

                #endregion
            }

            #region 发送RSSACC PUB内容

            if (rsspub_delay != "SEND" && rsspub_delay != "") strBuilder.Append(rsspub_delay);
            strBuilder.Append("ee");

            #endregion

            #region 发送

            try
            {
                Response.Clear();
                //C//Response.ClearHeaders();
                Response.ContentType = "text/plain";
                Response.Write(strBuilder.ToString());
                Response.Flush();
                //C//Response.Close();
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                //D//PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TDEBUG", "SENDPEERLIST:" + ex.ToString());
            }

            #endregion
        }

        #region 放弃的修改版程序

        ///// <summary>
        ///// ⑾ 发送出错信息
        ///// </summary>
        ///// <param name="errormessage"></param>
        //protected void SendErrorMessage1(string errormessage)
        //{
        //    Response.ContentType = "text/plain";
        //    Response.Write(string.Format("d14:failure reason{0}:{1}e", Encoding.UTF8.GetByteCount(errormessage), errormessage));
        //    //Response.Write("d14:failure reason56:Please check if the torrent be blocked or it is unpostede");
        //}

        ///// <summary>
        ///// ⑾ 发送正常peerlist信息
        ///// </summary>
        ///// <param name="uploadcount"></param>
        ///// <param name="downloadcount"></param>
        ///// <param name="uploadlist"></param>
        ///// <param name="downloadlist"></param>
        //protected void SendPeerList1()//PTPeerInfo peerinfo)
        //{

            ////获取PeerList
            //DataTable uploadlist = new DataTable();
            //if (peer_left > 0) uploadlist = PrivateBT.GetPeerListTracker(seedinfo.SeedId, true, peer_numwant);
            //DataTable downloadlist = new DataTable();
            //if (uploadlist.Rows.Count < peer_numwant) downloadlist = PrivateBT.GetPeerListTracker(seedinfo.SeedId, false, peer_numwant - uploadlist.Rows.Count);

            ////发送正常反馈信息
            //if (peer_event == "started" || peer_event == "completed" || peer_event == "") SendPeerList(peer_numwant, seedinfo.Upload, seedinfo.Download, uploadlist, downloadlist, userinfo.Uid, peer_ipv4ip, peer_ipv6ipadd);


            //Response.ContentType = "text/plain";
            //Random ra = new Random();

            //if (PrivateBT.IsServerUser(currentuid) || currentuid == 13)
            //    Response.Write(string.Format("d8:completei{0}e10:incompletei{1}e8:intervali{2}e5:peersl", uploadcount, downloadcount * 3, (3600 + ra.Next(1800))));//(300 + ra.Next(600))
            //else
            //    Response.Write(string.Format("d8:completei{0}e10:incompletei{1}e8:intervali{2}e5:peersl", uploadcount, downloadcount * 3, (300 + ra.Next(600))));//(300 + ra.Next(600))


            ////不能是NULL，数据库中NULL表示不存在
            //if (currentip == "IP_NULL") currentip = "";
            //if (currentipv6 == "IP_NULL") currentipv6 = "";

            //string peerip = "", peerport = "", peerid = ""; string peeripv6add = ""; string peeripv6 = "";
            //int uplistcount = uploadlist.Rows.Count;
            //int downlistcount = downloadlist.Rows.Count;
            //for (int i = 0; i < numwant; i++)
            //{
            //    peeripv6add = "";
            //    if (i < uplistcount)
            //    {
            //        if (uploadlist.Rows[i]["uid"].ToString().Trim() == currentuid.ToString()) continue;         //不发送相同uid的用户
            //        if (uploadlist.Rows[i]["ip"].ToString().Trim() == currentip) continue;                      //不发送相同IP的用户
            //        if (uploadlist.Rows[i]["ipv6ip"].ToString().Trim() == currentip) continue;                  //不发送相同IP的用户
            //        if (uploadlist.Rows[i]["ipv6ip"].ToString().Trim() == currentipv6) continue;                //不发送相同IP的用户

            //        peerip = uploadlist.Rows[i]["ip"].ToString().Trim();
            //        peeripv6 = uploadlist.Rows[i]["ipv6ip"].ToString().Trim();
            //        peerport = uploadlist.Rows[i]["port"].ToString().Trim();
            //        peerid = uploadlist.Rows[i]["peerid"].ToString();
            //        //peerid = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(peerid));
            //        if (uploadlist.Rows[i]["ipv6addip"] != null) peeripv6add = uploadlist.Rows[i]["ipv6addip"].ToString().Trim();
            //    }
            //    else if (i < uplistcount + downlistcount)
            //    {
            //        if (downloadlist.Rows[i - uplistcount]["uid"].ToString().Trim() == currentuid.ToString()) continue;     //不发送相同uid的用户
            //        if (downloadlist.Rows[i - uplistcount]["ip"].ToString().Trim() == currentip) continue;                      //不发送相同IP的用户
            //        if (downloadlist.Rows[i - uplistcount]["ipv6ip"].ToString().Trim() == currentip) continue;                    //不发送相同IP的用户
            //        if (downloadlist.Rows[i - uplistcount]["ipv6ip"].ToString().Trim() == currentipv6) continue;                    //不发送相同IP的用户

            //        peerip = downloadlist.Rows[i - uplistcount]["ip"].ToString().Trim();
            //        peeripv6 = downloadlist.Rows[i - uplistcount]["ipv6ip"].ToString().Trim();
            //        peerport = downloadlist.Rows[i - uplistcount]["port"].ToString().Trim();
            //        peerid = downloadlist.Rows[i - uplistcount]["peerid"].ToString();
            //        //peerid = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(peerid));
            //        if (downloadlist.Rows[i - uplistcount]["ipv6addip"] != null) peeripv6add = downloadlist.Rows[i - uplistcount]["ipv6addip"].ToString().Trim();
            //    }
            //    else break;


            //    if (Request.UserHostAddress == "xxx.xxx.xxx.xxx") //DEBUG
            //    {

            //        string o_peerid = "";
            //        for (int l = 0; l < peerid.Length - 1; l += 2)
            //        {
            //            o_peerid += Char.ConvertFromUtf32(Convert.ToInt32("0x" + peerid.Substring(l, 2), 16)).ToString();
            //        }
            //        peerid = o_peerid;
            //        peerid = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(o_peerid));


            //        //v4地址发送
            //        if (peerip != "IP_NULL")
            //        {
            //            Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peerip), peerip, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //        }
            //        //v6地址发送
            //        if (peeripv6 != "" && peeripv6 != "IP_NULL")
            //        {
            //            //首先发送IPv6地址
            //            Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //            //可能的宿舍区IPv4内网地址1
            //            if (peeripv6.IndexOf("2001:da8:203:888:0:5efe:") > -1 || peeripv6.IndexOf("2001:da8:203:666:0:5efe:") > -1)
            //            {
            //                if (peeripv6.Length > 24)
            //                {
            //                    peeripv6 = peeripv6.Substring(24, peeripv6.Length - 24);
            //                    Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));
            //                }
            //            }
            //        }

            //        //v6附加地址发送
            //        if (peeripv6add != "" && peeripv6add != "IP_NULL")
            //        {
            //            //首先发送IPv6地址
            //            Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6add), peeripv6add, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //            //可能的宿舍区IPv4内网地址1
            //            if (peeripv6add.IndexOf("2001:da8:203:888:0:5efe:") > -1 || peeripv6add.IndexOf("2001:da8:203:666:0:5efe:") > -1)
            //            {
            //                if (peeripv6add.Length > 24)
            //                {
            //                    peeripv6add = peeripv6add.Substring(24, peeripv6add.Length - 24);
            //                    Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6add), peeripv6add, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //                }
            //            }
            //            //可能的宿舍区IPv4内网地址2
            //            if (peerip.IndexOf("211.71") > -1 && peeripv6add.IndexOf("fe80::5efe:") > -1)
            //            {
            //                if (peeripv6add.Length > 11)
            //                {
            //                    peeripv6add = peeripv6add.Substring(11, peeripv6add.Length - 11);
            //                    Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6add), peeripv6add, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        string o_peerid = "";
            //        for (int l = 0; l < peerid.Length - 1; l += 2)
            //        {
            //            o_peerid += Char.ConvertFromUtf32(Convert.ToInt32("0x" + peerid.Substring(l, 2), 16)).ToString();
            //        }
            //        peerid = o_peerid;
            //        peerid = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(o_peerid));

            //        //v4地址发送
            //        if (peerip != "IP_NULL")
            //        {
            //            Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peerip), peerip, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //        }
            //        //v6地址发送
            //        if (peeripv6 != "" && peeripv6 != "IP_NULL")
            //        {
            //            //首先发送IPv6地址
            //            Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //            //可能的宿舍区IPv4内网地址1
            //            if (peeripv6.IndexOf("2001:da8:203:888:0:5efe:") > -1 || peeripv6.IndexOf("2001:da8:203:666:0:5efe:") > -1)
            //            {
            //                if (peeripv6.Length > 24)
            //                {
            //                    peeripv6 = peeripv6.Substring(24, peeripv6.Length - 24);
            //                    Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));
            //                }
            //            }
            //        }

            //        //v6附加地址发送
            //        if (peeripv6add != "" && peeripv6add != "IP_NULL")
            //        {
            //            //首先发送IPv6地址
            //            Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6add), peeripv6add, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //            //可能的宿舍区IPv4内网地址1
            //            if (peeripv6add.IndexOf("2001:da8:203:888:0:5efe:") > -1 || peeripv6add.IndexOf("2001:da8:203:666:0:5efe:") > -1)
            //            {
            //                if (peeripv6add.Length > 24)
            //                {
            //                    peeripv6add = peeripv6add.Substring(24, peeripv6add.Length - 24);
            //                    Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6add), peeripv6add, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //                }
            //            }
            //            //可能的宿舍区IPv4内网地址2
            //            if (peerip.IndexOf("211.71") > -1 && peeripv6add.IndexOf("fe80::5efe:") > -1)
            //            {
            //                if (peeripv6add.Length > 11)
            //                {
            //                    peeripv6add = peeripv6add.Substring(11, peeripv6add.Length - 11);
            //                    Response.Write(string.Format("d2:ip{0}:{1}7:peer id{4}:{2}4:porti{3}ee", Encoding.UTF8.GetByteCount(peeripv6add), peeripv6add, peerid, peerport, Encoding.ASCII.GetByteCount(peerid)));

            //                }
            //            }
            //        }
            //    }
            //}
            //Response.Write("ee");

            ////Response.Write("d8:completei1e10:incompletei0e8:intervali10e5:peersld2:ip18:240c:2:100:46c1::17:peer id20:S588-----gqQ8TqDeqaY4:porti6882eeee");


            //uploadlist.Dispose();
            //uploadlist = null;
            //downloadlist.Dispose();
            //downloadlist = null;

        //}

//        /// <summary>
//        /// ⑩ 更新用户信息
//        /// </summary>
//        /// <param name="oldpeerinfo"></param>
//        /// <param name="peerinfo"></param>
//        /// <param name="userinfo"></param>
//        /// <returns></returns>
//        protected bool UpdateUser(PrivateBTPeerInfo oldpeerinfo, PrivateBTPeerInfo peerinfo, PTUserInfo userinfo)
//        {
//            //更新用户信息//需要更新的信息：ratio,upcount,downcount,lastactivity    ++realupload,realdownload,extcredits3,extcredits4,finished,extcredits5,extcredits6
//            userinfo.Extcredits7 = addupload;
//            userinfo.Extcredits8 = adddownload;
//            userinfo.Extcredits9 = addupload;     //当天真实上传
//            userinfo.Extcredits10 = adddownload;
//            userinfo.Extcredits5 = addupload * (decimal)uploadratio; //当天上传信息
//            userinfo.Extcredits6 = adddownload * (decimal)downloadratio;
//            if (peerinfo.IsSeed)//只有完整做种才有奖励
//            {
//                userinfo.Extcredits5 += PTTools.GetSeedAward(seedinfo.FileSize, oldlive, seedinfo.Upload) * (decimal)(seedinfo.Live) / 3600m; //当天上传信息
//            }
//            if ((userinfo.Extcredits4 + userinfo.Extcredits6) > 0m) userinfo.Ratio = (float)(userinfo.Extcredits3 + userinfo.Extcredits5) / (float)(userinfo.Extcredits4 + userinfo.Extcredits6);
//            else userinfo.Ratio = 1.00001f;
//            userinfo.Extcredits3 = userinfo.Extcredits5;//总上传
//            userinfo.Extcredits4 = userinfo.Extcredits6;

//            //疑似删除peer导致死锁问题，延时2秒
//            //if (peer_event == "stopped") System.Threading.Thread.Sleep(2000);


//            //btuserinfo.UpCount = PrivateBT.GetSeedInfoCount(0, btuserinfo.Uid, 1, 0, "");
//            //btuserinfo.DownCount = PrivateBT.GetSeedInfoCount(0, btuserinfo.Uid, 2, 0, "");
//            if (peer_event == "")
//            {

//            }
//            else
//            {
//                userinfo.UploadCount = PTSeeds.GetSeedInfoCount(0, userinfo.Uid, 1, 0, "");
//                userinfo.DownloadCount = PTSeeds.GetSeedInfoCount(0, userinfo.Uid, 2, 0, "");
//            }


//            //共享率限制规则
//            //btuserinfo.Download += btuserinfo.Extcredits6;
//            float RatioLimit = 1.00001f;
//            if (userinfo.Extcredits4 < 1024M) RatioLimit = 1.00001f;
//            else if (userinfo.Extcredits4 < 10 * 1024M) RatioLimit = 10f;
//            else if (userinfo.Extcredits4 < 20 * 1024M) RatioLimit = 50f;
//            else if (userinfo.Extcredits4 < 50 * 1024M) RatioLimit = 100f;
//            else if (userinfo.Extcredits4 < 100 * 1024M) RatioLimit = 200f;
//            else if (userinfo.Extcredits4 < 500 * 1024M) RatioLimit = 500f;
//            else RatioLimit = 100000;
//            if (userinfo.Ratio > RatioLimit) userinfo.Ratio = RatioLimit;
//            //if (btuserinfo.Download == 0M) btuserinfo.Ratio = 1.00001;


//            PrivateBT.UpdatePerUserSeedTraffic(seedinfo.SeedId, userinfo.Uid, addupload, adddownload, peerinfo.IPv4IP + "@" + peerinfo.Port, peerinfo.IPv6IP + "@" + peerinfo.Port);
//            PTUsers.UpdateUserInfo_Tracker(userinfo);
//            Forum.UserCredits.UpdateUserCredits(userinfo.Uid); //更新总积分
//        }
//        /// <summary>
//        /// ⑨ 更新种子信息
//        /// </summary>
//        /// <param name="oldpeerinfo"></param>
//        /// <param name="peerinfo"></param>
//        /// <param name="seedinfo"></param>
//        /// <returns></returns>
//        protected bool UpdateSeed(PTPeerInfo oldpeerinfo, PTPeerInfo peerinfo, PTSeedinfoShort seedinfo)
//        {
//            // 种子信息中可以更新的内容包括：
//            // 1.上传uptraffic下载量traffic（种子的流量统计均为真实流量，增量），2.做种数upload下载数download（绝对值）完成数finished（增量），
//            // 3.种子做种状态ipv6（IPv4/6），4.种子存活时间live、最后做种时间lastlive，5最后做种者lastseederid和用户名lastseedername
//            // 
//            // 其中1~4为Seed表，5为seed_detail表
//            // 几种流量更新模式：notraffic,uptraffic,(traffic,iptraffic)，
//            // 事件更新模式started,completed,stopped（前三个均需要更新做种下载完成数）,""（只更新流量）



//            if (peerinfo.Event == "")
//            {
//                //中间更新
//                if (peerinfo.Percentage == 1.0f)
//                {
//                    //做种状态，需要更新live和lastlive，不需要更新节点数量
//                    if (oldpeerinfo.Event == "notraffic")
//                    {
//                        //更新live和lastlive
//                    }
//                    else if (oldpeerinfo.Event == "uptraffic")
//                    {
//                        //更新live和lastlive，更新上传流量
//                    }
//                    else if (oldpeerinfo.Event == "traffic")
//                    {
//                        //不可能出现的情况
//                    }
//                    else //if(oldpeerinfo.Event == "iptraffic")
//                    {
//                        //更新live和lastlive，更新上传流量【IP变更造成】
//                    }
//                }
//                else
//                {
//                    //下载状态，不需要更新live和lastlive
//                    if (oldpeerinfo.Event == "notraffic")
//                    {
//                        //不需要更新任何内容
//                    }
//                    else if (oldpeerinfo.Event == "uptraffic")
//                    {
//                        //更新上传流量
//                    }
//                    else if (oldpeerinfo.Event == "traffic")
//                    {
//                        //更新上传下载流量
//                    }
//                    else//if(oldpeerinfo.Event == "iptraffic")
//                    {
//                        //更新上传下载流量
//                    }
//                }
//            }
//            if (peerinfo.Event == "started")
//            {
//                //开始新节点，不需要更新流量，需要更新节点数
//                if (peerinfo.Percentage == 1.0f)
//                {
//                    //做种开始，需要更新live和lastlive
//                    if (seedinfo.Upload > 0)
//                    {
//                        //更新上传数，更新live和lastlive【之前有人在做种】

//                    }
//                    else
//                    {
//                        //更新上传数，更新lastlive【之前无人做种，只更新最后做种时间】

//                    }
//                }
//                else
//                {
//                    //下载开始
//                    //更新下载数
//                }

//            }
//            else if (peerinfo.Event == "stopped")
//            {
//                //停止节点
//                if (peerinfo.Percentage == 1.0f)
//                {
//                    //做种中的节点停止，需要更新上传数、live和lastlive
//                    if (oldpeerinfo.Event == "notraffic")
//                    {
//                        //更新上传数，更新live和lastlive
//                    }
//                    else if (oldpeerinfo.Event == "uptraffic")
//                    {
//                        //更新上传数，更新live和lastlive，更新上传流量
//                    }
//                    else if (oldpeerinfo.Event == "traffic")
//                    {
//                        //不可能出现的情况
//                    }
//                    else //if(oldpeerinfo.Event == "iptraffic")
//                    {
//                        //更新上传数，更新live和lastlive，更新上传流量【ip地址更改造成】
//                    }


//                    if (seedinfo.Upload == 0)
//                    {
//                        //已经没有做种者【upload数值为更新后的数值】
//                        //更新最后做种者id和用户名

//                    }
//                }
//                else
//                {
//                    //下载中的节点停止，需要更新下载数
//                    if (oldpeerinfo.Event == "notraffic")
//                    {
//                        //更新下载数
//                    }
//                    else if (oldpeerinfo.Event == "uptraffic")
//                    {
//                        //更新下载数，更新上传流量
//                    }
//                    else if (oldpeerinfo.Event == "traffic")
//                    {
//                        //更新下载数，更新上传下载流量
//                    }
//                    else //if(oldpeerinfo.Event == "iptraffic")
//                    {
//                        //更新下载数，更新上传下载流量
//                    }
//                }
//            }
//            else if (peerinfo.Event == "completed")
//            {
//                //完成的节点
//                if (seedinfo.Upload > 0)
//                {
//                    //存在有人做种的情况【多数都是这样】，更新上传下载完成数，更新live和lastlive
//                    if (oldpeerinfo.Event == "notraffic")
//                    {
//                        //更新上传下载完成数，更新live和lastlive
//                    }
//                    else if (oldpeerinfo.Event == "uptraffic")
//                    {
//                        //【不可能出现的情况】更新上传下载完成数，更新live和lastlive，更新上传流量
//                    }
//                    else if (oldpeerinfo.Event == "traffic")
//                    {
//                        //更新上传下载完成数，更新live和lastlive，更新上传下载流量
//                    }
//                    else //if(oldpeerinfo.Event == "iptraffic")
//                    {
//                        //更新上传下载完成数，更新live和lastlive，更新上传下载流量
//                    }
//                }
//                else
//                {
//                    //更新上传下载完成数，更新lastlive，更新上传下载流量

//                }

//            }

//            return true;

//            //if (addupload < 0M) addupload = 0M;
//            //if (adddownload < 0M) adddownload = 0M;


//            ////更新种子信息//总计需要更新的信息：upload, download,finished+, live+, lastlive, traffic+, ipv6, lastseederid, lastseedername ";
//            //if (seedinfo.Live > 7200) seedinfo.Live = 7200;
//            //seedinfo.Traffic = adddownload;
//            //seedinfo.UpTraffic = addupload;

//            //if (peer_event == "" && ipv6added == false)
//            //{
//            //    if (peerinfo.IsSeed) PTSeeds.UpdateSeedAnnounceUpTrafficOnly(peerinfo.SeedId, seedinfo.UpTraffic);
//            //    else PTSeeds.UpdateSeedAnnounceTrafficOnly(peerinfo.SeedId, seedinfo.UpTraffic, seedinfo.Traffic);
//            //}
//            //else if (peer_event == "" && ipv6added == true)
//            //{
//            //    seedinfo.IPv6 = PrivateBT.GetPeerIPv6UploadCount(seedinfo.SeedId);
//            //    if (peerinfo.IsSeed) PTSeeds.UpdateSeedAnnounceUpTrafficOnly(peerinfo.SeedId, seedinfo.UpTraffic);
//            //    else PTSeeds.UpdateSeedAnnounceTrafficOnly(peerinfo.SeedId, seedinfo.UpTraffic, seedinfo.Traffic);
//            //}
//            //else
//            //{
//            //    seedinfo.Upload = PrivateBT.GetPeerUploadCount(seedinfo.SeedId);
//            //    seedinfo.Download = PrivateBT.GetPeerDownloadCount(seedinfo.SeedId);
//            //    seedinfo.IPv6 = PrivateBT.GetPeerIPv6UploadCount(seedinfo.SeedId);
//            //    PTSeeds.UpdateSeedAnnounce(seedinfo.SeedId, seedinfo.Upload, seedinfo.Download, seedinfo.Finished, seedinfo.IPv6, seedinfo.UpTraffic, seedinfo.Traffic);
//            //}

//            ////加上本次存活时间
//            //oldlive += seedinfo.Live;
//            ////存活时间、最后做种的临时解决方法
//            //if (peerinfo.IsSeed)
//            //{
//            //    PTSeeds.UpdateSeedLive(peerinfo.SeedId, oldlive, peerinfo.Uid);
//            //}
//        }

//        /// <summary>
//        /// ⑧ 更新Peer，对应四种操作，completed,"",started,stopped
//        /// </summary>
//        /// <param name="oldpeerinfo"></param>
//        /// <param name="peerinfo"></param>
//        /// <returns></returns>
//        protected bool UpdatePeer(PTPeerInfo oldpeerinfo, PTPeerInfo peerinfo)
//        {
//            // Peer表的6种更新模式：删除，插入，4种更新：无流量notraffic，仅上传uptraffic，上传和下载traffic，IP地址和上传下载iptraffic
//            // 可更新范围为除了seedid,uid,peerid,client,sessionid之外的所有值

//            switch (peerinfo.Event)
//            {
//                // 中间流量更新
//                case "":
//                case "completed":
//                    {
//                        switch (oldpeerinfo.Event)
//                        {
//                            case "notraffic":
//                                {
//                                    break;
//                                }
//                            case "uptraffic":
//                                {
//                                    break;
//                                }
//                            case "traffic":
//                                {
//                                    break;
//                                }
//                            case "iptraffic":
//                                {
//                                    break;
//                                }
//                            default:
//                                {
//                                    break;
//                                }
//                        }
//                        break;
//                    }
//                case "stopped":
//                    {
//                        break;
//                    }
//                case "started":
//                    {
//                        break;
//                    }
//                default:
//                    {
//                        break;
//                    }
//            }
//            return false;
//        }

//        /// <summary>
//        /// ⑦ 计算流量，并进行速度反作弊检查
//        /// </summary>
//        /// <param name="oldpeerinfo"></param>
//        /// <param name="peerinfo"></param>
//        /// <returns></returns>
//        protected bool CalculateTraffic(PrivateBTPeerInfo oldpeerinfo, ref PrivateBTPeerInfo peerinfo, PTSeedinfoShort seedinfo)
//        {
//            // 存放结果的变量
//            decimal addupload = 0M;
//            decimal adddownload = 0M;
//            float uploadspeed = 0f;
//            float downloadspeed = 0f;

//            // 需要进行流量统计的操作
//            if (peerinfo.Event == "" || peerinfo.Event == "completed" || peerinfo.Event == "stopped")
//            {
//                // 更新时间差，秒
//                int timediv = (int)(DateTime.Now - oldpeerinfo.LastTime).TotalSeconds;

//                // 计算流量和速度
//                if (timediv > 0)
//                {
//                    // 所有数据已经经过校验，不会出现负数、零等错误
//                    // 上传流量、速度计算
//                    if (oldpeerinfo.Event == "uptraffic" || oldpeerinfo.Event == "traffic" || oldpeerinfo.Event == "iptraffic")
//                    {
//                        addupload = peerinfo.Uploaded - oldpeerinfo.Uploaded;
//                        uploadspeed = (float)addupload / timediv;
//                    }
//                    // 下载流量、速度计算
//                    if (oldpeerinfo.Event == "traffic" || oldpeerinfo.Event == "iptraffic")
//                    {
//                        adddownload = peerinfo.Downloaded - oldpeerinfo.Downloaded;
//                        downloadspeed = (float)adddownload / timediv;
//                    }
//                }
//                else
//                {
//                    SendErrorMessage("0x701 您的Tracker更新过于频繁，请稍后再试");
//                    return false;
//                }

//                // 速度异常检测
//                if (uploadspeed > 40.0 * 1024 * 1024) // 速度 > 40.0MB/s，不可能出现的情况，限速，并记录
//                {
//                    addupload = (decimal)(40.0 * 1024 * 1024 * timediv);
//                    // *记录异常*

//                }
//                else if (uploadspeed > 20.0 * 1024 * 1024) // 速度 > 20.0MB/s，不可能出现的情况，记录
//                {
//                    // *记录异常*

//                }
//                else if (uploadspeed > 12.5 * 1024 * 1024) // 速度 > 12.5MB/s，可疑，记录
//                {
//                    // *记录异常*

//                }

//                //添加到返回数值中，上传，下载，奖励
//                if (addupload > 0M)
//                {
//                    peerinfo.TotalUploaded = addupload;
//                    peerinfo.StatsSessionUploaded = addupload * (decimal)seedinfo.UploadRatio;
//                }
//                if (adddownload > 0M)
//                {
//                    peerinfo.TotalDownloaded = adddownload;
//                    peerinfo.StatsSessionDownloaded = adddownload * (decimal)seedinfo.DownloadRatio;
//                }
//                if (oldpeerinfo.Percentage == 1.0f && peerinfo.Percentage == 1.0f)
//                {
//                    peerinfo.StatsSessionAward = PTSeeds.GetSeedAward(seedinfo.FileSize, seedinfo.Live, seedinfo.Upload, timediv);
//                }

//            }

//            return true;
//        }


//        /// <summary>
//        /// ⑥ Peer操作类型确认，确认没有各种不允许的情况出现。 检测完成之后，将可以进行流量统计，返回值中event为判断依据，error为错误
//        ///    如果是更新peer，则锁定peer（所有之前有peer的，都尝试锁定）
//        /// </summary>
//        /// <param name="peerinfo"></param>
//        /// <returns></returns>
//        protected PrivateBTPeerInfo CheckPeerStatus(ref PTPeerInfo peerinfo)
//        {
//            //////////////////////////////////////////////////////////////////////////
//            // Tracker更新逻辑

//            // Tracker规则：
//            // 同Peerid，同Seedid，不同Uid，不允许(Peerid和Seedid设置为唯一索引，不允许出现第二个)
//            // 同Uid，同Seedid，不能同时上传下载
//            // 不发送同IP，同IPv6者的Tracker信息
//            // 
//            // 之前oldpeerinfo可能存在的状态：
//            // 1.不存在，2.存在且uid或client更改，3.存在且IP地址更改，4.存在且基本和流量信息未更改，5.存在且基本信息未更改流量更改


//            //////////////////////////////////////////////////////////////////////////
//            // 获取旧Peerinfo，按照Peerid和seedid

//            //尝试锁定该Peer
//            if (PrivateBT.LockPeer(peerinfo) < 1)
//            {
//                //无法锁定，等待2秒之后再次尝试。可能出现此情况的：completed、stopped的并发操作，中间更新部分可能性较小
//                System.Threading.Thread.Sleep(2000);
//                if (PrivateBT.LockPeer(peerinfo) < 1)
//                {
//                    if (peerinfo.Event != "stopped") SendErrorMessage("0x609 请稍后重试");
//                    peerinfo.Event = "error";
//                    return peerinfo;
//                }
//            }

//            PTPeerInfo oldpeerinfo = PrivateBT.GetPeerInfo(peerinfo);
//            //默认为"error"，错误状态
//            oldpeerinfo.Event = "error";


//            //////////////////////////////////////////////////////////////////////////
//            // 校验

//            // peer列表中已经存在该peer，
//            if (oldpeerinfo.Uid > 0 && oldpeerinfo.SessionId > 0)
//            {
//                // 检测不可更改的部分 seedid,uid,peerid,client（其中seedid和peerid是读取peerinfo的项目，不用检测）
//                // 同一UT同一种子，是否有其他人的uid
//                if (oldpeerinfo.Uid != peerinfo.Uid)
//                {
//                    SendErrorMessage("0x601 禁止挂双人Tracker");
//                    oldpeerinfo.Uid = -1;//替换为记录异常语句，下同
//                    return oldpeerinfo;
//                }
//                // Client字符串更改
//                if (oldpeerinfo.Client != peerinfo.Client)
//                {
//                    SendErrorMessage("0x602 不应存在的Tracker行为");
//                    oldpeerinfo.Uid = -1;
//                    return oldpeerinfo;
//                }

//                // 检测可以更改的部分，ip和端口，流量等
//                // 流量数据减少是不允许的
//                if (oldpeerinfo.Uploaded > peerinfo.Uploaded)
//                {
//                    SendErrorMessage("0x603 不应存在的Tracker行为");
//                    oldpeerinfo.Uid = -1;
//                    return oldpeerinfo;
//                }
//                if (oldpeerinfo.Downloaded > peerinfo.Downloaded)
//                {
//                    SendErrorMessage("0x604 不应存在的Tracker行为");
//                    oldpeerinfo.Uid = -1;
//                    return oldpeerinfo;
//                }
//                // 流量增加或ip地址更改，设置Event级别，notraffic，uptraffic，traffic，iptraffic
//                oldpeerinfo.Event = "notraffic";
//                if (oldpeerinfo.Uploaded < peerinfo.Uploaded) oldpeerinfo.Event = "uptraffic";
//                if (oldpeerinfo.Downloaded < peerinfo.Downloaded || oldpeerinfo.Left != peerinfo.Left) oldpeerinfo.Event = "traffic";
//                if (oldpeerinfo.IPv4IP != peerinfo.IPv4IP || oldpeerinfo.IPv6IP != peerinfo.IPv6IP || oldpeerinfo.IPv6IPAdd != peerinfo.IPv6IPAdd || oldpeerinfo.Port != peerinfo.Port)
//                {
//                    oldpeerinfo.Event = "iptraffic";
//                    //更新IP地址

//                }
//                // 事件校验 completed，""，started，stopped
//                // 前后更新事件间隔
//                if (peerinfo.Event == "" && (DateTime.Now - oldpeerinfo.LastTime).TotalSeconds < 10)
//                {
//                    SendErrorMessage("0x605 不应存在的Tracker行为");
//                    oldpeerinfo.Event = "error";
//                    return oldpeerinfo;
//                }
//                // 如果是开始，则为第二个tracker访问，作为中间更新处理
//                if (peerinfo.Event == "started")
//                {
//                    peerinfo.Event = "";
//                }

//                // 需要检测：如果为上传，则同UID是否还存在其他Peerid的下载记录，如果为下载，同UID是否还存在其他Peerid的上传下载记录

//                // 需要检测：如果为上传，同IP是否还存在其他下载记录，如果为下载，同IP是否还存在其他上传下载记录（不违规，仅记录）





//            }
//            // peer列表中不存在这个Peerid
//            else
//            {
//                // 默认oldpeerinfo.Event是error，如果为正常，则置为notraffic，其余无需更改
//                // 事件校验 completed，""，started，stopped
//                if (peerinfo.Event == "stopped")
//                {
//                    return oldpeerinfo;
//                }
//                // 不存在，则只能为started或者stopped
//                if (peerinfo.Event == "completed" || peerinfo.Event == "")
//                {
//                    peerinfo.Event = "started";
//                    oldpeerinfo.Event = "notraffic";
//                    // 记录异常的开始

//                }
//                // 只能为started
//                if (peerinfo.Event == "started")
//                {
//                    // 需要检测：如果为上传，则同UID是否还存在其他Peerid的下载记录，如果为下载，同UID是否还存在其他Peerid的上传下载记录

//                    // 需要检测：如果为上传，同IP是否还存在其他下载记录，如果为下载，同IP是否还存在其他上传下载记录（不违规，仅记录）

//                    oldpeerinfo.Event = "notraffic";
//                }
//                else
//                {
//                    SendErrorMessage("0x611 不应存在的Tracker行为");
//                    return oldpeerinfo;
//                }
//            }

//            // 校验完成
//            return oldpeerinfo;
//        }



//        /// <summary>
//        /// ⑤ 生成本次Tracker访问的peerinfo，校验peerinfo的合理性(seedid小于-1，则为出错)
//        /// </summary>
//        /// <param name="peerid"></param>
//        /// <param name="userid"></param>
//        /// <param name="seedid"></param>
//        /// <param name="peerport"></param>
//        /// <param name="ipv4ip"></param>
//        /// <param name="ipv6ip"></param>
//        /// <param name="ipv6ipadd"></param>
//        /// <param name="peeruploaded"></param>
//        /// <param name="peerdownloaded"></param>
//        /// <param name="peerleft"></param>
//        /// <param name="seedsize"></param>
//        /// <returns></returns>
//        protected PTPeerInfo CreatePeerInfo(string peer_id, string peer_key, int userid, int peer_seedid, int peer_port, string peer_ipv4ip, string peer_ipv6ip,
//            string peer_ipv6ipadd, decimal peer_uploaded, decimal peer_downloaded, decimal peer_left, decimal peer_corrupt, decimal seedsize,
//            string peer_event, int peer_numwant, int peer_compact, int peer_nopeerid)
//        {
//            // 生成本次的PeerInfo//仍然需要继续补充的信息：Fristtime,uploadspeed,downloadspeed
//            PTPeerInfo peerinfo = new PTPeerInfo();

//            // 基本信息，PeerId，Uid，Seedid
//            peerinfo.SeedId = peer_seedid;
//            peerinfo.Uid = userid;
//            peerinfo.PeerId = peer_id.Substring(16) + peer_key;
//            peerinfo.Client = PTTools.HEX2Str(peer_id.Substring(2, 12));
//            peerinfo.SessionId = 0;

//            // IP和端口，本次访问的IP地址，v4，v6判断
//            if (peer_ipv6ip != "")
//            {
//                peerinfo.IPType = 6;
//                peerinfo.IPv6IP = peer_ipv6ip;
//                peerinfo.IPv6IPAdd = peer_ipv6ipadd;
//            }
//            else
//            {
//                peerinfo.IPType = 4;
//                peerinfo.IPv4IP = peer_ipv4ip;
//                peerinfo.IPv6IPAdd = peer_ipv6ipadd;
//            }
//            peerinfo.Port = peer_port;

//            // 请求内容
//            peerinfo.Event = peer_event;
//            peerinfo.NumWant = peer_numwant;
//            peerinfo.Compact = peer_compact;
//            peerinfo.NoPeerid = peer_nopeerid;
//            peerinfo.LastTime = DateTime.Now;

//            // 流量
//            peerinfo.Uploaded = peer_uploaded;
//            peerinfo.Downloaded = peer_downloaded;
//            peerinfo.Left = peer_left;
//            peerinfo.Corrupt = peer_corrupt;
//            peerinfo.Percentage = ((float)seedsize - (float)peer_left) / (float)seedsize;
//            if (peerinfo.Percentage > 0.9999 && peer_left > 0m) peerinfo.Percentage = 0.9999f; //防止未完成时显示100%

//            //////////////////////////////////////////////////////////////////////////
//            // 校验peerinfo合理性
//            // 流量不能小于零
//            if (peerinfo.Uploaded < 0M || peerinfo.Downloaded < 0M || peerinfo.Left < 0M || peerinfo.Corrupt < 0M)
//            {
//                SendErrorMessage("0x501 非法的Tracker信息");
//                peerinfo.SeedId = -1; peerinfo.Uid = -1;
//                return peerinfo;
//            }
//            // 汇报完成时，剩余流量必须为0
//            if (peerinfo.Event == "completed" && peerinfo.Left != 0M)
//            {
//                SendErrorMessage("0x502 非法的Tracker信息");
//                peerinfo.SeedId = -1; peerinfo.Uid = -1;
//                return peerinfo;
//            }
//            // 汇报动作只能为completed,"",started,stopped
//            if (peerinfo.Event != "completed" && peerinfo.Event != "" && peerinfo.Event != "started" && peerinfo.Event != "stopped")
//            {
//                SendErrorMessage("0x503 非法的Tracker信息");
//                peerinfo.SeedId = -1; peerinfo.Uid = -1;
//                return peerinfo;
//            }
//            // 端口号必须 > 1
//            if (peerinfo.Port < 1)
//            {
//                SendErrorMessage("0x504 非法的Tracker信息");
//                peerinfo.SeedId = -1; peerinfo.Uid = -1;
//                return peerinfo;
//            }
//            // 限制每次Session的最终流量不能超过5TB
//            if (peerinfo.Uploaded > 5M * 1024 * 1024 * 1024 * 1024 || peerinfo.Downloaded > 5M * 1024 * 1024 * 1024 * 1024)
//            {
//                peerinfo.Uploaded = 5M * 1024 * 1024 * 1024 * 1024;
//                peerinfo.Downloaded = 5M * 1024 * 1024 * 1024 * 1024;
//                // *记录异常*

//            }
//            // 限制每次Session的下载流量不能超过种子大小的5倍
//            if (peerinfo.Downloaded > 5 * seedsize)
//            {
//                peerinfo.Downloaded = 5 * seedsize;
//                // *记录异常*

//            }
//            return peerinfo;
//        }


//        /// <summary>
//        /// ④ 验证种子信息，计算最终上传下载系数
//        /// </summary>
//        /// <param name="InfoHash"></param>
//        /// <param name="btconfig"></param>
//        /// <param name="userid"></param>
//        /// <returns></returns>
//        protected PTSeedinfoShort ValidateSeed(string info_hash, PrivateBTConfigInfo btconfig, int userid)
//        {
//            PTSeedinfoShort seedinfo = new PTSeedinfoShort();

//            //验证info_hash格式，必须由40位16进制数字构成
//            Regex reg = new Regex(@"^[0-9A-F]{40}$");
//            if (!reg.IsMatch(info_hash))
//            {
//                SendErrorMessage("0x401 种子info_hash格式错误");
//                return seedinfo;
//            }

//            //获取seedinfo【数据库查询-SELCET】
//            seedinfo = PTSeeds.GetSeedInfoShort(info_hash);

//            if (seedinfo.SeedId < 1)
//            {
//                SendErrorMessage("0x402 种子不存在");
//                return seedinfo;
//            }

//            //获取上传下载因子（只需要对比config中的数值，不用进行seedinfo中的ratioexpiredate判断，此工作由计划任务完成，从bt_seed_tracker表中获取的seedinfo也没有系数过期时间）
//            //自己发布的种子，在没有设置上传系数小于1的情况下，上传系数 = 2
//            if (seedinfo.Uid == userid && seedinfo.UploadRatio >= 1.0f && seedinfo.UploadRatio < 2.0) seedinfo.UploadRatio = 2.0f;

//            //全局上传下载系数是否生效，如果生效，则取代种子中的上传下载系数
//            if (btconfig.UploadMulti >= 1.0f && DateTime.Now > btconfig.UpMultiBeginTime && DateTime.Now < btconfig.UpMultiEndTime)
//            {
//                if (btconfig.UploadMulti > seedinfo.UploadRatio)
//                {
//                    seedinfo.UploadRatio = btconfig.UploadMulti;
//                }
//            }
//            if (btconfig.DownloadMulti <= 1.0f && DateTime.Now > btconfig.DownMultiBeginTime && DateTime.Now < btconfig.DownMultiEndTime)
//            {

//                if (btconfig.DownloadMulti < seedinfo.UploadRatio)
//                {
//                    seedinfo.UploadRatio = btconfig.UploadMulti;
//                }
//            }

//            return seedinfo;
//        }


//        /// <summary>
//        /// ③ 用户信息验证，包含是否存在、共享率、用户组等用户相关内容验证，返回PTUserinfo.uid小于等于0时，验证失败
//        /// </summary>
//        /// <param name="Passkey"></param>
//        /// <returns></returns>
//        protected PTUserInfo ValidateUser(string Passkey, decimal peer_left)
//        {
//            PTUserInfo btuserinfo = new PTUserInfo();

//            //验证passkey格式，必须由32位大写字母或数字构成
//            Regex reg = new Regex(@"^[0-9A-Z]{32}$");
//            if (!reg.IsMatch(Passkey))
//            {
//                SendErrorMessage("0x301 Passkey格式错误");
//                return btuserinfo;
//            }

//            //获取用户信息【数据库查询-SELECT】
//            btuserinfo = PTUsers.GetBtUserInfoForTracker(Passkey);
//            if (btuserinfo.Uid < 1)
//            {
//                SendErrorMessage("0x302 Passkey不存在");
//                return btuserinfo;
//            }

//            //账户是否被禁封
//            if (btuserinfo.Groupid == 5 || btuserinfo.Vip < 0) //用||，BT操作也可以禁封账户
//            {
//                SendErrorMessage("0x303 账户被禁封，不能下载！");
//                btuserinfo.Uid = -1;
//                return btuserinfo;
//            }

//            //计算共享率，检查用户共享率情况，是否允许下载
//            if (btuserinfo.Extcredits4 != 0) btuserinfo.Ratio = ((float)btuserinfo.Extcredits3 / (float)btuserinfo.Extcredits4);
//            else btuserinfo.Ratio = 1.00001f;
//            if (peer_left > 0)
//            {
//                if (
//                    (btuserinfo.Extcredits4 > 20 * 1024M * 1048576 && btuserinfo.Ratio < 0.3f) ||
//                    (btuserinfo.Extcredits4 > 100 * 1024M * 1048576 && btuserinfo.Ratio < 0.4f) ||
//                    (btuserinfo.Extcredits4 > 200 * 1024M * 1048576 && btuserinfo.Ratio < 0.5f) ||
//                    (btuserinfo.Extcredits4 > 300 * 1024M * 1048576 && btuserinfo.Ratio < 0.6f) ||
//                    (btuserinfo.Extcredits4 > 400 * 1024M * 1048576 && btuserinfo.Ratio < 0.7f) ||
//                    (btuserinfo.Extcredits4 > 500 * 1024M * 1048576 && btuserinfo.Ratio < 0.8f) ||
//                    (btuserinfo.Extcredits4 > 750 * 1024M * 1048576 && btuserinfo.Ratio < 0.9f) ||
//                    (btuserinfo.Extcredits4 > 1024 * 1024M * 1048576 && btuserinfo.Ratio < 1.0f)
//                    )
//                {
//                    SendErrorMessage("0x304 共享率过低，不能下载！");
//                    btuserinfo.Uid = -1;
//                    return btuserinfo;
//                }

//            }

//            //无问题，返回
//            return btuserinfo;
//        }


//        /// <summary>
//        /// ② 客户端IP地址验证，验证地址合法性以及进行IP区域限制
//        /// </summary>
//        /// <param name="IPv4"></param>
//        /// <param name="IPv6"></param>
//        /// <param name="IPv6Add"></param>
//        /// <returns>4.IPv4，6.IPv6，-1.失败，不包含对IPv6Add的判断，这个地址仅仅检测合法性</returns>
//        protected bool ValidateClientIP(ref string IPv4IP, ref string IPv6IP, ref string IPv6IPAdd)
//        {
//            //客户IP地址合法性验证
//            if (IPv4IP != "" && !PTTools.IsIPv4(IPv4IP)) IPv4IP = "";
//            if (IPv6IP != "" && !PTTools.IsIPV6(IPv6IP)) IPv6IP = "";
//            else
//            {
//                //只接受开头为2001的地址
//                if (IPv6IP.IndexOf("2001:") != 0) IPv6IP = "";
//            }
//            if (IPv6IPAdd != "" && !PTTools.IsIPV6(IPv6IPAdd)) IPv6IPAdd = "";
//            else
//            {
//                //只接受开头为2001的地址
//                if (IPv6IPAdd.IndexOf("2001:") != 0) IPv6IPAdd = "";
//            }

//            //是否存在合法IP地址
//            if (IPv4IP == "" && IPv6IP == "")
//            {
//                SendErrorMessage("0x201 IP地址错误");
//                return false;
//            }

//            //IP地址区域限制
//            //校内用户必须使用域名访问tracker，校外ipv6用户可以使用IP地址
//            //if(!ipv6tracker || ip.IndexOf("2001:da8:203") > -1 || ip.IndexOf("2001:da8:ae") > -1 || ipv6addip.IndexOf("2001:da8:203") > -1 || ipv6addip.IndexOf("2001:da8:ae") > -1) //是校内用户
//            //{
//            //    //buaaip = true;  
//            //    if (Request.Url.Host.ToLower().IndexOf("bt.buaa.edu.cn") < 0 && Request.Url.Host.ToLower().IndexOf("bt.buaa6.edu.cn") < 0 && Request.Url.Host.ToLower().IndexOf("buaabt.cn") < 0) //没有使用域名作为Tracker
//            //    {
//            //        //SendErrorMessage("0x12 校内用户请使用域名作为Tracker地址");
//            //        //return;
//            //    }
//            //}
//            //if (Request.Url.Host.ToLower().IndexOf("bt.buaa6.edu.cn") > -1) ipv6name = true;



//            //if (ip.IndexOf("2001:da8:203") > -1 || ip.IndexOf("2001:da8:ae") > -1) //通过IPv6访问Tracker的用户，阻止重复共享
//            //{
//            //    if (PrivateBT.GetPeerCountV4V6(ip, seedinfo.SeedId, peer_id) > 0)
//            //    {
//            //        SendErrorMessage("0x13 同一IP或者同一用户不能同时上传下载同一个种子");
//            //        return;
//            //    }
//            //}
//            //////////////////////////////////////////////////////////////////////////

//            return true;
//        }


//        /// <summary>
//        /// ① 客户端软件验证，只允许uTorrent2.0或以上或者Transmission1.5以上
//        /// </summary>
//        /// <param name="BrowserString">浏览器标记</param>
//        /// <param name="UserAgent">客户端软件标记</param>
//        /// <param name="PeerID">BT客户端的PeerID，HEX字符串格式，40位</param>
//        /// <returns></returns>
//        protected bool ValidateClientSoftware(string BrowserString, string UserAgent, string PeerID)
//        {
//            //客户端字符串
//            //HttpBrowserCapabilities bc = Request.Browser;
//            //string ClientString = bc.Platform + "|" + bc.Browser + "|" + bc.Id + "|" + bc.Version + "|" + Request.UserAgent;

//            //验证Peerid是否合乎要求，必须由40位16进制数字构成
//            //验证info_hash格式，
//            Regex reg = new Regex(@"^[0-9A-F]{40}$");
//            if (!reg.IsMatch(PeerID))
//            {
//                SendErrorMessage("0x101 种子info_hash格式错误");
//                return false;
//            }


//#if DEBUG
//            //DEBUG 测试地址，不检测客户端软件
//            string uncheckip = Request.UserHostAddress;
        //            if (uncheckip != "xxx.xxx.xxx.xxx" && uncheckip != "127.0.0.1")
//#endif
//            {
//                if (BrowserString.IndexOf("FireFox") > -1 || BrowserString.IndexOf("IE") > -1 || BrowserString.IndexOf("Safari") > -1 || BrowserString.IndexOf("Optera") > -1)
//                {
//                    SendErrorMessage("0x102 错误的下载软件");
//                    return false;
//                }
//                if (UserAgent.Length < 13)
//                {
//                    SendErrorMessage("0x103 错误的下载软件");
//                    return false;
//                }
//                else
//                {
//                    if (UserAgent.Substring(0, 2) == "uT")        //客户端为uTorrent
//                    {
//                        if (UserAgent.Substring(0, 9) != "uTorrent/")
//                        {
//                            SendErrorMessage("0x104 错误的下载软件，本站仅支持uTorrent和Transmission");
//                            return false;
//                        }
//                        if (Utils.StrToInt(UserAgent.Substring(9, 3), 0) < 200)
//                        {
//                            SendErrorMessage("0x105 请将uTorrent更新为UT200以上版本，本站右上角有下载");
//                            return false;
//                        }
//                        if (UserAgent.IndexOf("B") > 0)
//                        {
//                            SendErrorMessage("0x106 请勿使用测试版UT，本站右上角有正式版UT下载");
//                            return false;
//                        }
//                        if (PeerID.Substring(0, 6) != "2D5554" || TypeConverter.StrToInt(PTTools.HEX2Str(PeerID.Substring(6, 8)), 0) < 2000)
//                        {
//                            SendErrorMessage("0x107 错误的下载软件，本站仅支持uTorrent2.0以上和Transmission1.5以上版本");
//                            return false;
//                        }
//                    }
//                    else if (UserAgent.Length > 16)              //客户端为Transmission
//                    {
//                        if (UserAgent.Substring(0, 13) != "Transmission/")
//                        {
//                            SendErrorMessage("0x108 错误的下载软件，本站仅支持uTorrent和Transmission");
//                            return false;
//                        }
//                        if (Utils.StrToFloat(UserAgent.Substring(13, 3), 0) < 1.5)
//                        {
//                            SendErrorMessage("0x109 请将Transmission更新为1.5以上版本");
//                            return false;
//                        }
//                        if (PeerID.Substring(0, 6) != "2D5452" || TypeConverter.StrToInt(PTTools.HEX2Str(PeerID.Substring(6, 8)), 0) < 1500)
//                        {
//                            SendErrorMessage("0x110 错误的下载软件，本站仅支持uTorrent2.0以上和Transmission1.5以上版本");
//                            return false;
//                        }
//                    }
//                    else
//                    {
//                        SendErrorMessage("0x111 错误的下载软件，本站仅支持uTorrent和Transmission");
//                        return false;
//                    }
//                }
//            }

//            //检测通过
//            return true;
        //        }

        #endregion
    
    static Timer eventTimer;
        /// <summary>
        /// 计划任务初始化（Tracker）
        /// Web的在Forum/HttpModule.cs
        /// </summary>
        public void ScheduledEventTracker()
        {
            try
            {
                if (eventTimer == null && ScheduleConfigs.GetConfig().Enabled)
                {
                    EventLogs.LogFileName = Utils.GetMapPath(string.Format("{0}cache/scheduleeventfaildlog.config", BaseConfigs.GetForumPath));
                    EventManager.RootPath = Utils.GetMapPath(BaseConfigs.GetForumPath);
                    if (eventTimer == null) eventTimer = new Timer(new TimerCallback(ScheduledEventWorkCallbackTracker), null, 30000, EventManager.TimerMinutesInterval * 30000);
                }
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.ScheduledEventTracker, PTLog.LogStatus.Exception, "ScheduledEventT", "EX:" + ex.ToString());   
                EventLogs.WriteFailedLog("Failed ScheduledEventCallBack Start Sch Tracker");
            }
        }

        /// <summary>
        /// 计划任务执行（Tracker）
        /// Web的在Forum/HttpModule.cs
        /// </summary>
        /// <param name="sender"></param>
        private void ScheduledEventWorkCallbackTracker(object sender)
        {
            try
            {
                if (ScheduleConfigs.GetConfig().Enabled)
                {
                    EventManager.Execute("Tracker");
                }
            }
            catch
            {
                EventLogs.WriteFailedLog("Failed ScheduledEventCallBack Tracker");
            }

        }


        private void AbtAnnounce(string passkey, string ip, int port, string peerid, decimal upload, decimal download, decimal left, string peerevent)
        {
            //检测IP是否允许访问
            if (!PTAbt.IsIPAllowed(ip)) return;

            AbtPeerInfo abtpeer = new AbtPeerInfo();
            AbtSeedInfo abtseed = new AbtSeedInfo();
            AbtDownloadInfo abtdownload = new AbtDownloadInfo();

            int aid = DNTRequest.GetInt("aid", -1);
            int uid = DNTRequest.GetInt("uid", -1);
            string peerid_20 = peerid.Length > 20 ? peerid.Substring(peerid.Length - 20, 20) : peerid;


            //检测种子下载状态是否存在
            if(aid < 1 || uid < 1)
            {
                SendErrorMessage("种子不存在");
                return;
            }
            abtdownload = PTAbt.AbtGetDownload(aid,  uid, passkey);
            if (abtdownload.Aid < 1)
            {
                SendErrorMessage("种子不存在");
                return;
            }
            if (peerevent == "stopped")
            {
                PTAbt.AbtDeletePeer(aid, peerid_20);
                if (left == 0) PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), true, false);
                else PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), false, false);
                return;
            }

            //获取种子信息
            abtseed = PTAbt.AbtGetSeedInfo(aid);
            if (abtseed.Aid < 1)
            {
                SendErrorMessage("种子不存在");
                return;
            }


            //获取并更新PEER
            bool isipv6 = PTTools.IsIPv6(ip);
            string ipv4 = isipv6 ? "IP_NULL" : ip;
            string ipv6 = isipv6 ? ip : "IP_NULL";

            if (!WaitForConCurrentUserSeed(uid, aid, false))
            {
                SendErrorMessage("请稍后重试");
                return;
            }

            abtpeer = PTAbt.AbtGetPeerInfo(aid, peerid_20);
            if (abtpeer.Aid > 0)
            {
                if (ipv4 != "IP_NULL") abtpeer.IPv4 = ipv4;
                if (ipv6 != "IP_NULL") abtpeer.IPv6 = ipv6;
                abtpeer.Port = port;
                abtpeer.Percentage = (float)(abtseed.FileSize - left) / (float)abtseed.FileSize;
                PTAbt.AbtUpdatePeer(abtpeer);
            }
            else
            {
                abtpeer.Aid = abtseed.Aid;
                abtpeer.Peerid = peerid_20;
                abtpeer.Uid = uid;
                abtpeer.IPv4 = ipv4;
                abtpeer.IPv6 = ipv6;
                abtpeer.Port = port;
                abtpeer.Percentage = (float)(abtseed.FileSize - left) / (float)abtseed.FileSize;
                PTAbt.AbtInsertPeer(abtpeer);
            }

            EndConCurrentUserSeed();


            //下载状态校验，并更新下载状态信息
            if (abtdownload.Status == 0)
            {
                //状态0，允许下载，下载即变为状态1
                if (peerevent == "completed" && abtpeer.Percentage == 1)
                {
                    //完成
                    PTAbt.AbtUpdateDownload(aid, uid, passkey, 2, peerid_20, abtpeer.Percentage);
                    if (!isipv6) //只有v4连接增加完成计数
                        PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), true, true);
                    else
                        PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), true, false);
                }
                if (peerevent == "started" && abtpeer.Percentage < 1)
                {
                    //下载，开始
                    PTAbt.AbtUpdateDownload(aid, uid, passkey, 1, peerid_20, abtpeer.Percentage);
                    PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), false, false);
                }
                else if (peerevent == "started" && abtpeer.Percentage == 1)
                {
                    //保种，开始
                    PTAbt.AbtUpdateDownload(aid, uid, passkey, 2, peerid_20, abtpeer.Percentage);
                    PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), true, false);
                }
                else if (abtpeer.Percentage < 1)
                {
                    //下载，中间更新
                    PTAbt.AbtUpdateDownload(aid, uid, passkey, 1, peerid_20, abtpeer.Percentage);
                    PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), false, false);
                    PTAbt.AbtInsertLog(aid, uid, 0, "错误的下载开始1");
                }
                else
                {
                    //保种，中间更新
                    PTAbt.AbtUpdateDownload(aid, uid, passkey, 2, peerid_20, abtpeer.Percentage);
                    PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), true, false);
                    PTAbt.AbtInsertLog(aid, uid, 0, "错误的下载开始2");
                }

                
            }
            else if (abtdownload.Status == 1)
            {
                //状态1，只允许特定PEERID的客户端下载，当开始做种，即变为状态2
                if (peerevent == "started" || abtpeer.Percentage < 1)
                {
                    if (abtdownload.Peerid != peerid_20)
                    {
                        SendErrorMessage("种子失效，如需继续下载，请重新下载种子");
                        PTAbt.AbtInsertLog(aid, uid, 0, "错误的下载开始3");
                        return;
                    }
                }
                if (abtpeer.Percentage == 1)
                {
                    PTAbt.AbtUpdateDownload(aid, uid, passkey, 2, peerid_20, abtpeer.Percentage);
                }

                if (peerevent == "completed" && abtpeer.Percentage == 1)
                {
                    if (!isipv6)
                        PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), true, true);
                    else
                        PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), false, false);
                }
                else if (peerevent == "started")
                {
                    PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), false, false);
                }
            }
            else if (abtdownload.Status == 2)
            {
                //状态2，只允许做种
                if (abtpeer.Percentage < 1)
                {
                    SendErrorMessage("当前种子只能用于做种");
                    PTAbt.AbtInsertLog(aid, uid, 0, "当前种子只能用于做种");
                    return;
                }

                if (peerevent == "started")
                {
                    PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), true, false);
                }
                else
                {
                    //如果种子信息 最后活动时间距今超过5分，则更新一次最后活动时间
                    if ((DateTime.Now - abtseed.LastLive).TotalSeconds > 300)
                    {
                        PTAbt.AbtUpdateSeed(aid, PTAbt.AbtGetPeerCount(aid, true), PTAbt.AbtGetPeerCount(aid, false), true, false);
                    }
                }
            }

            //获取PeerList 
            DataTable uploadlist = new DataTable();
            if (left > 0) uploadlist = PTAbt.AbtGetPeerList(aid, true);
            DataTable downloadlist = new DataTable();
            downloadlist = PTAbt.AbtGetPeerList(aid, false);
            int uplistcount = uploadlist.Rows.Count;
            int downlistcount = downloadlist.Rows.Count;

            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(28000);
            
            strBuilder.AppendFormat("d8:completei{0}e10:incompletei{1}e8:intervali{2}e5:peersl", uplistcount, downlistcount * 3, (300 + PTTools.GetRandomInt(0, 900)));//(300 + ra.Next(600))
            string peerip = "", peerport = ""; string peeripv6 = "";
            
            //发送正在下载的客户列表
            for (int i = 0; i < 200; i++)
            {
                if (i < downlistcount)
                {
                    peerip = downloadlist.Rows[i]["ipv4"].ToString().Trim();
                    peeripv6 = downloadlist.Rows[i]["ipv6"].ToString().Trim();
                    peerport = downloadlist.Rows[i]["port"].ToString().Trim();
                }
                else break;

                //v4地址发送
                if (peerip != "IP_NULL")
                {
                    strBuilder.AppendFormat("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peerip), peerip, peerport);

                }
                //v6地址发送
                if (peeripv6 != "" && peeripv6 != "IP_NULL")
                {
                    //首先发送IPv6地址
                    strBuilder.AppendFormat("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerport);
                }
            }
            
            //发送正在上传的客户列表
            if (left > 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (i < uplistcount)
                    {
                        peerip = uploadlist.Rows[i]["ipv4"].ToString().Trim();
                        peeripv6 = uploadlist.Rows[i]["ipv6"].ToString().Trim();
                        peerport = uploadlist.Rows[i]["port"].ToString().Trim();
                    }
                    else break;

                    //v4地址发送
                    if (peerip != "IP_NULL")
                    {
                        strBuilder.AppendFormat("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peerip), peerip, peerport);

                    }
                    //v6地址发送
                    if (peeripv6 != "" && peeripv6 != "IP_NULL")
                    {
                        //首先发送IPv6地址
                        strBuilder.AppendFormat("d2:ip{0}:{1}4:porti{2}ee", Encoding.UTF8.GetByteCount(peeripv6), peeripv6, peerport);
                    }
                }
            }
            strBuilder.Append("ee");


            try
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write(strBuilder.ToString());
                Response.Flush();
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TDEBUG", "ABT SENDPEERLIST:" + ex.ToString());
            }

        }

        /// <summary>
        /// CPU性能计数器
        /// </summary>
        public static System.Diagnostics.PerformanceCounter CpuWatch = null;
        /// <summary>
        /// 可用内存 性能计数器
        /// </summary>
        public static System.Diagnostics.PerformanceCounter MemWatch = null;
        private static DateTime lastcputime = DateTime.Now;
        private static int cpuratioupdatecount = 0;
        private static double lastcpuratie0 = 90.0;
        private static double lastcpuratie1 = 90.0;
        private static double lastcpuratie2 = 90.0;
        private static double lastcpuratie3 = 90.0;
        private static double lastcpuratie4 = 90.0;
        private static object SynObjectCPUWatch = new object();
        /// <summary>
        /// 负载状态优化，True可执行，False停止
        /// </summary>
        /// <returns></returns>
        private bool LoadFilter()
        {
            //启动优化、高负载优化
            try
            {
                if (CpuWatch == null)
                {
                    if (Monitor.TryEnter(SynObjectCPUWatch, 0))
                    {
                        #region 初始化CPU和内存 性能监视器
                        
                        try
                        {
                            if (CpuWatch == null)
                            {
                                CpuWatch = new System.Diagnostics.PerformanceCounter();//新建一个性能计数器
                                //设置属性
                                CpuWatch.CategoryName = "Processor";
                                CpuWatch.CounterName = "% Processor Time";
                                CpuWatch.InstanceName = "_Total";
                                CpuWatch.NextValue();
                                lastcputime = DateTime.Now;
                                lastcpuratie0 = 90.0;
                            }
                            if (MemWatch == null)
                            {
                                MemWatch = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");//新建一个性能计数器
                                MemWatch.NextValue();
                                lastcputime = DateTime.Now;
                                lastcpuratie0 = 90.0;
                            }
                        }
                        catch (System.Exception ex)
                        {
                            PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TDEBUG", "LOADFILTER1:" + ex.ToString());
                        }
                        Monitor.Exit(SynObjectCPUWatch);
                        
                        #endregion
                    }
                }
                else
                {
                    #region 更新CPU内存使用率

                    if ((DateTime.Now - lastcputime).TotalMilliseconds > 1000)
                    {
                        bool needupdate = false;
                        if (Monitor.TryEnter(SynObjectCPUWatch, 0))
                        {
                            try
                            {
                                if ((DateTime.Now - lastcputime).TotalMilliseconds > 1000)
                                {
                                    lastcputime = DateTime.Now;
                                    needupdate = true;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TDEBUG", "LOADFILTER2:" + ex.ToString());
                            }
                            Monitor.Exit(SynObjectCPUWatch);
                        }
                        if (needupdate)
                        {
                            lastcpuratie4 = lastcpuratie3;
                            lastcpuratie3 = lastcpuratie2;
                            lastcpuratie2 = lastcpuratie1;
                            lastcpuratie1 = lastcpuratie0;
                            lastcpuratie0 = CpuWatch.NextValue();


                            float memrat = MemWatch.NextValue();

                            if (memrat < 500) { if (lastcpuratie0 < 100) lastcpuratie0 = 100; }
                            else if (memrat < 1000) { if (lastcpuratie0 < 95) lastcpuratie0 = 95; }
                            else if (memrat < 1500) { if (lastcpuratie0 < 90) lastcpuratie0 = 90; }

                            lastcputime = DateTime.Now;
                            cpuratioupdatecount++;
                        }
                    }

                    #endregion
                }

                #region 繁忙时发送回应

                if (cpuratioupdatecount < 30)
                {
                    //程序初始化时，延后执行
                    SendWaitMessage(180 + PTTools.GetRandomInt(0,300));
                    return false;
                }
                else if (lastcpuratie0 > 70.0 && lastcpuratie1 > 70.0 && lastcpuratie2 > 70.0 && lastcpuratie3 > 70.0 && lastcpuratie4 > 70.0)
                {
                    //连续70以上时，报服务器忙
                    SendErrorMessage("服务器忙，请稍后更新 0x000");
                    return false;
                }
                else if (lastcpuratie0 > 90 || (cpuratioupdatecount < 2 && lastcpuratie0 > 60))
                {
                    SendWaitMessage(180 + PTTools.GetRandomInt(0, 300));
                    return false;
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerDebug, PTLog.LogStatus.Exception, "TDEBUG", "LOADFILTER3:" + ex.ToString());
                SendWaitMessage(300 + PTTools.GetRandomInt(0, 300));
                return false;
            }
            return true;
        }

        /// <summary>
        /// IP地址检查和过滤
        /// </summary>
        /// <param name="peerinfo"></param>
        private void IPAddressFilter(ref PrivateBTPeerInfo peerinfo)
        {
            string ipv4ip = peerinfo.IPv4IP;
            string ipv6ip = peerinfo.IPv6IP;
            string ipv6ipadd = peerinfo.IPv6IPAdd;

            //过滤非北航校内的IPv4地址//由于服务器不能通过校外IPv4地址访问，可以忽略此条
            if (ipv4ip != "IP_NULL" && ipv4ip != "")
            {

            }
            //过滤微软Terendo地址 2001:0::
            if (ipv6ip != "IP_NULL" && ipv6ip != "")
            {
                if (ipv6ip.Length > 7 && ipv6ip.Substring(0, 7) == "2001:0:")
                {
                    peerinfo.IPv6IP = "IP_NULL";
                }
            }
            if (ipv6ipadd != "IP_NULL" && ipv6ipadd != "")
            {
                if (ipv6ipadd.Length > 7 && ipv6ipadd.Substring(0, 7) == "2001:0:")
                {
                    peerinfo.IPv6IPAdd = "IP_NULL";
                }
            }
        }



        /// <summary>
        /// 静态变量访问独占标志
        /// </summary>
        private static object SynObject = new object();
        /// <summary>
        /// 静态变量：当前所有正在执行的用户种子表，访问此变量必须经过lock！
        /// </summary>
        private static string SynCurrentUserSeedList = "";

        /// <summary>
        /// 当前线程正在执行的用户种子
        /// </summary>
        private string currentuserseed = "";
        private bool SynCurrentUserSeedListTimeOut = false;
        private DateTime reqstarttime = new DateTime(1970,1,1);
        private static DateTime lastovertime = new DateTime(1970,1,1);

        /// <summary>
        /// 获取当前UserSeed独占
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="seedid"></param>
        /// <param name="pass">一次锁定不成功就立即跳过返回false</param>
        private bool WaitForConCurrentUserSeed(int uid, int seedid, bool pass)
        {
            currentuserseed = string.Format(pass ? "[U{0}S{1}P]" : "[U{0}S{1}]", uid, seedid);
            //D//if (pass) PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Normal, "Reward_C", string.Format("获取当前Reward独占A：{0} -A:{1}", currentuserseed, SynCurrentUserSeedList));

            bool ok = false;
            try
            {
                for (int tw = 0; tw < 110; tw++)
                {
                    lock (SynObject)
                    {
                        if (SynCurrentUserSeedList.IndexOf(currentuserseed) < 0)
                        {
                            SynCurrentUserSeedList = SynCurrentUserSeedList + currentuserseed;
                            ok = true;
                        }
                    }

                    //D//if (pass) PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Normal, "Reward_C", string.Format("[U{3}S{4}] 获取当前Reward独占：OK:{1} -CS:{0} -A:{2}", currentuserseed, ok, SynCurrentUserSeedList, uid, testseedid));
                    
                    if (ok) break;
                    else if (pass) return false;
                    else
                    {
                        //最长等候20秒
                        Thread.Sleep(200);
                        if (tw > 100)
                        {
                            PTLog.InsertSystemLog(PTLog.LogType.TrackerWaitUS, PTLog.LogStatus.Error, "WaitUS Timeout", string.Format("Tracker等候处理超时：{0} -ALL:{1} -T:{2} -TS:{3}", currentuserseed, tw, (DateTime.Now - reqstarttime).TotalSeconds, reqstarttime));
                            SynCurrentUserSeedListTimeOut = true;
                            //解除当前并返回，后面发送消息时会解除
                            //EndConCurrentUserSeed();
                            return false;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.TrackerWaitUS, PTLog.LogStatus.Error, "WaitUS GetEx", string.Format("获取当前UserSeed独占 异常：{0} -EX:{1}", currentuserseed, ex));
                SynCurrentUserSeedListTimeOut = true;
                return false;
            }

            //默认状态为成功，强制执行后续函数
            reqstarttime = DateTime.Now;
            return true;
            
        }
        /// <summary>
        /// 解除当前UserSeed独占
        /// </summary>
        private void EndConCurrentUserSeed()
        {
            try
            {
                bool endok = false;

                //此函数可能被多线程访问，变量独占访问
                //D//if (currentuserseed.IndexOf("-1") > 0) PTLog.InsertSystemLogDebug(PTLog.LogType.TrackerUpdateTraffic, PTLog.LogStatus.Normal, "Reward_C", string.Format("[U{2}S{3}] 结束当前Reward独占：{0} -A:{1}", currentuserseed, SynCurrentUserSeedList, testuid, testseedid));

                if (currentuserseed == "") return;

                lock (SynObject)
                {
                    if (currentuserseed != "")
                    {
                        SynCurrentUserSeedList = SynCurrentUserSeedList.Replace(currentuserseed, "").Trim();
                        currentuserseed = "";
                        endok = true;
                    }
                }

                DateTime reqendtime = DateTime.Now;
                if (endok)
                {
                    if (SynCurrentUserSeedListTimeOut)
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.TrackerWaitUS, PTLog.LogStatus.Normal, "WaitUS Clear", string.Format("超时：清除标记：{0}", currentuserseed));
                    }
                    else if ((reqendtime - reqstarttime).TotalSeconds > 15)
                    {
                        bool dolog = false;
                        lock (SynObject)
                        {
                            if((DateTime.Now - lastovertime).Seconds > 300)
                            {
                                dolog = true;
                                lastovertime = DateTime.Now;
                            }
                            
                        }
                        if(dolog) PTLog.InsertSystemLog(PTLog.LogType.TrackerWaitUS, PTLog.LogStatus.Normal, "WaitUS RunOver", string.Format("超时：执行时间超过30秒：{0} -T:{1} -ST:{2}", currentuserseed, (reqendtime - reqstarttime).TotalSeconds, reqstarttime));
                    }
                }
                else
                {
                    if (reqstarttime > new DateTime(2010, 1, 1))
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.TrackerWaitUS, PTLog.LogStatus.Normal, "WaitUS RunOver", string.Format("错误：已被清除：{0} -T:{1} -ST:{2}", currentuserseed, (reqendtime - reqstarttime).TotalSeconds, reqstarttime));
                    }
                }      
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.TrackerWaitUS, PTLog.LogStatus.Error, "WaitUS DelEx", string.Format("异常：解除当前UserSeed独占异常：{0} -EX:{1}", currentuserseed, ex));
            }

        }
    }
}
