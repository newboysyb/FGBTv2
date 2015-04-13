using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
    public class PTPeerLog
    {


        public static int InsertPeerHistoryLog(int pid, int seedid, int uid, int loglevel, string logtype, string message)
        {
            return DatabaseProvider.GetInstance().InsertPeerHistoryLog(pid, seedid, uid, loglevel, logtype, message);
        }


        public static int InsertPeerErrorInfo(string traffictype, decimal addtraffic, PrivateBTPeerInfo curPeerinfo, PrivateBTPeerInfo prePeerinfo)
        {
            int errortype = 0;
            int errorlevel = 0;
            int loglevel = 0;
            string message = "";
            if (traffictype == "UPLOAD")
            {
                errortype = 1;

                //20M一下的回滚不记录
                if (addtraffic < 20 * 1024 * 1024M) return -1; 
                else if (addtraffic < 100 * 1024 * 1024M)
                {
                    errorlevel = 1;
                    loglevel = 101;
                    message = "上传回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 500 * 1024 * 1024M)
                {
                    errorlevel = 2;
                    loglevel = 102;
                    message = "上传回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 1024 * 1024 * 1024M)
                {
                    errorlevel = 3;
                    loglevel = 103;
                    message = "上传回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 2 * 1024 * 1024 * 1024M)
                {
                    errorlevel = 4;
                    loglevel = 104;
                    message = "上传回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 5 * 1024 * 1024 * 1024M)
                {
                    errorlevel = 5;
                    loglevel = 105;
                    message = "上传回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 10 * 1024 * 1024 * 1024M)
                {
                    errorlevel = 6;
                    loglevel = 106;
                    message = "上传回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else
                {
                    errorlevel = 7;
                    loglevel = 107;
                    message = "上传回滚 " + PTTools.Upload2Str(addtraffic);
                }

            }
            else if (traffictype == "DOWNLOAD")
            {
                errortype = 2;
                //20M一下的回滚不记录
                if (addtraffic < 20 * 1024 * 1024M) return -1;
                else if (addtraffic < 100 * 1024 * 1024M)
                {
                    errorlevel = 1;
                    loglevel = 111;
                    message = "下载回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 500 * 1024 * 1024M)
                {
                    errorlevel = 2;
                    loglevel = 112;
                    message = "下载回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 1024 * 1024 * 1024M)
                {
                    errorlevel = 3;
                    loglevel = 113;
                    message = "下载回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 2 * 1024 * 1024 * 1024M)
                {
                    errorlevel = 4;
                    loglevel = 114;
                    message = "下载回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 5 * 1024 * 1024 * 1024M)
                {
                    errorlevel = 5;
                    loglevel = 115;
                    message = "下载回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else if (addtraffic < 10 * 1024 * 1024 * 1024M)
                {
                    errorlevel = 6;
                    loglevel = 116;
                    message = "下载回滚 " + PTTools.Upload2Str(addtraffic);
                }
                else
                {
                    errorlevel = 7;
                    loglevel = 117;
                    message = "下载回滚 " + PTTools.Upload2Str(addtraffic);
                }

            }

            else if (traffictype == "UPSPEED")
            { 
                errortype = 3;
                if (curPeerinfo.UploadSpeed < 1024 * 1024 * 10.0) return -1;
                else if (curPeerinfo.UploadSpeed < 1024 * 1024 * 20.0)
                {
                    errorlevel = 2;
                    loglevel = 122;
                    message = "超速上传 " + (curPeerinfo.UploadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " + 
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.UploadSpeed < 1024 * 1024 * 30.0)
                {
                    errorlevel = 3;
                    loglevel = 123;
                    message = "超速上传 " + (curPeerinfo.UploadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.UploadSpeed < 1024 * 1024 * 40.0)
                {
                    errorlevel = 4;
                    loglevel = 124;
                    message = "超速上传 " + (curPeerinfo.UploadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.UploadSpeed < 1024 * 1024 * 50.0)
                {
                    errorlevel = 5;
                    loglevel = 125;
                    message = "超速上传 " + (curPeerinfo.UploadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.UploadSpeed < 1024 * 1024 * 60.0)
                {
                    errorlevel = 6;
                    loglevel = 126;
                    message = "超速上传 " + (curPeerinfo.UploadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.UploadSpeed >= 1024 * 1024 * 60.0)
                {
                    errorlevel = 7;
                    loglevel = 127;
                    message = "超速上传 " + (curPeerinfo.UploadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
            }

            else if (traffictype == "DOWNSPEED")
            {
                //if ((DateTime.Now - prePeerinfo.LastTime).TotalSeconds < 5 && addtraffic < 200)

                errortype = 4;
                if (curPeerinfo.DownloadSpeed < 1024 * 1024 * 10.0) return -1;
                else if (curPeerinfo.DownloadSpeed < 1024 * 1024 * 20.0)
                {
                    errorlevel = 2;
                    loglevel = 132;
                    message = "超速下载 " + (curPeerinfo.DownloadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.DownloadSpeed < 1024 * 1024 * 30.0)
                {
                    errorlevel = 3;
                    loglevel = 133;
                    message = "超速下载 " + (curPeerinfo.DownloadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.DownloadSpeed < 1024 * 1024 * 40.0)
                {
                    errorlevel = 4;
                    loglevel = 134;
                    message = "超速下载 " + (curPeerinfo.DownloadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.DownloadSpeed < 1024 * 1024 * 50.0)
                {
                    errorlevel = 5;
                    loglevel = 135;
                    message = "超速下载 " + (curPeerinfo.DownloadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.DownloadSpeed < 1024 * 1024 * 60.0)
                {
                    errorlevel = 6;
                    loglevel = 136;
                    message = "超速下载 " + (curPeerinfo.DownloadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
                else if (curPeerinfo.DownloadSpeed >= 1024 * 1024 * 60.0)
                {
                    errorlevel = 7;
                    loglevel = 137;
                    message = "超速下载 " + (curPeerinfo.DownloadSpeed / 1024 / 1024.0).ToString("0.00") + "MB/s | " +
                        PTTools.Upload2Str(addtraffic) + "/" + (DateTime.Now - prePeerinfo.LastTime).TotalSeconds.ToString("0.0") + "s";
                }
            }
            else
            {
                message = "NULLINFO";
            }

            message = message + "|" + curPeerinfo.Event;

            int eid = InsertPeerErrorInfo(errortype, errorlevel, message, curPeerinfo, prePeerinfo);

            if (curPeerinfo.IPStatus == 1 && curPeerinfo.IPv6IP != "IP_NULL")
            {
                InsertPeerLog(eid, loglevel, "IP_FROMPEER", "", prePeerinfo);
                InsertPeerLog(eid, loglevel, curPeerinfo.IPv6IP, curPeerinfo.RawRequestString, curPeerinfo);
            }
            else
            {
                InsertPeerLog(eid, loglevel, "IP_FROMPEER", "", prePeerinfo);
                InsertPeerLog(eid, loglevel, curPeerinfo.IPv4IP, curPeerinfo.RawRequestString, curPeerinfo);
            }

            return eid;
        }


        /// <summary>
        /// 插入peer error记录信息
        /// </summary>
        /// <param name="errortype">1.上传回滚，2.下载回滚，3上传超速，4.下载超速</param>
        /// <param name="errorlevel">
        /// 1.轻微 回滚100M以内，超速10.0M，，
        /// 2.轻度 回滚500M以内，超速20.0M，，
        /// 3.中度 回滚1G以内，超速30.0M，，
        /// 4.重度 回滚2G以内，超速40.0M，，
        /// 5.严重 回滚5G以内，超速50.0M，，
        /// 6.极度 回滚10G以内，超速60.0M，，
        /// 7.恶劣 回滚10G以上，超速60.0M以上
        /// </param>
        /// <param name="curPeerinfo"></param>
        /// <param name="prePeerinfo"></param>
        /// <returns></returns>
        public static int InsertPeerErrorInfo(int errortype, int errorlevel, string message, PrivateBTPeerInfo curPeerinfo, PrivateBTPeerInfo prePeerinfo)
        {
            string curip = "";
            int ipregion = 0;
            string iptail = "";
            string ipheader = "";
            int preipregion = 0;
            string preiptail = "";
            int preipv6region = 0;
            string preipv6tail = "";


            //获取当前IP信息
            if (curPeerinfo.IPStatus == 1 && curPeerinfo.IPv6IP != "IP_NULL")
                curip = curPeerinfo.IPv6IP;
            else
                curip = curPeerinfo.IPv4IP;

            if (PTTools.SplitIP(curip, out ipheader, out iptail) > -1)
            {
                ipregion = PrivateBT.GetUserIPRegionId(ipheader);
            }
            else
            {
                iptail = curip;
                ipregion = -100;
            }

            //获取之前的IPv4信息
            if (prePeerinfo.IPv4IP != "IP_NULL")
            {
                if (prePeerinfo.IPv4IP == curip)
                {
                    preipregion = ipregion;
                    preiptail = iptail;
                }
                else
                {
                    if (PTTools.SplitIP(prePeerinfo.IPv4IP, out ipheader, out preiptail) > -1)
                    {
                        preipregion = PrivateBT.GetUserIPRegionId(ipheader);
                    }
                    else
                    {
                        preiptail = prePeerinfo.IPv4IP;
                        preipregion = -100;
                    }
                }
            }

            //获取之前的IPv6信息
            if (prePeerinfo.IPv6IP != "IP_NULL")
            {
                if (prePeerinfo.IPv6IP == curip)
                {
                    preipv6region = ipregion;
                    preipv6tail = iptail;
                }
                else
                {
                    if (PTTools.SplitIP(prePeerinfo.IPv6IP, out ipheader, out preipv6tail) > -1)
                    {
                        preipv6region = PrivateBT.GetUserIPRegionId(ipheader);
                    }
                    else
                    {
                        preipv6tail = prePeerinfo.IPv4IP;
                        preipv6region = -100;
                    }
                }
            }

            return DatabaseProvider.GetInstance().InsertPeerErrorInfo(curPeerinfo.Uid, curPeerinfo.SeedId, errortype, errorlevel, ipregion, iptail, preipregion, preiptail, preipv6region,
                preipv6tail, curPeerinfo.Upload, prePeerinfo.Upload, curPeerinfo.Download, prePeerinfo.Download, curPeerinfo.Client, curPeerinfo.Port, message);
        }


        /// <summary>
        /// 插入peer日志信息
        /// </summary>
        /// <param name="loglevel">
        /// 0.插入Peer，1.完成事件，2.删除Peer;
        /// 51.异常结束（超时删除）
        /// 
        /// 101.轻微 回滚100M以内,111下载，121.超速10.0M，131下载
        /// 102.轻度 回滚500M以内，122.超速20.0M，，
        /// 103.中度 回滚1G以内，123.超速30.0M，，
        /// 104.重度 回滚2G以内，124.超速40.0M，，
        /// 105.严重 回滚5G以内，125.超速50.0M，，
        /// 106.极度 回滚10G以内，126.超速60.0M
        /// 106.恶劣 回滚10G以上，126.超速60.0M以上
        /// </param>
        /// <param name="fromip"></param>
        /// <param name="rawstring"></param>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int InsertPeerLog(int loglevel, string fromip, string rawstring, PrivateBTPeerInfo peerinfo)
        {
            return InsertPeerLog(-1, loglevel, fromip, rawstring, peerinfo);
        }
        /// <summary>
        /// 插入peer日志信息
        /// </summary>
        /// <param name="loglevel">
        /// 0.插入Peer，1.完成事件，2.删除Peer;
        /// 51.异常结束（超时删除）
        /// 
        /// 101.轻微 回滚100M以内,111下载，121.超速10.0M，131下载
        /// 102.轻度 回滚500M以内，122.超速20.0M，，
        /// 103.中度 回滚1G以内，123.超速30.0M，，
        /// 104.重度 回滚2G以内，124.超速40.0M，，
        /// 105.严重 回滚5G以内，125.超速50.0M，，
        /// 106.极度 回滚10G以内，126.超速60.0M
        /// 106.恶劣 回滚10G以上，126.超速60.0M以上
        /// </param>
        /// <param name="fromip"></param>
        /// <param name="rawstring"></param>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int InsertPeerLog(int eid, int loglevel, string fromip, string rawstring, PrivateBTPeerInfo peerinfo)
        {
            int fromipregion = 0;
            string fromiptail = "";
            string fromipheader = "";

            if (PTTools.SplitIP(fromip, out fromipheader, out fromiptail) > -1)
            {
                fromipregion = PrivateBT.GetUserIPRegionId(fromipheader);
            }
            else
            {
                fromiptail = fromip;
                fromipregion = -100;
            }

            return DatabaseProvider.GetInstance().InsertPeerLog(eid, loglevel, fromipregion, fromiptail, rawstring, peerinfo);
            
        }


        public static int InsertPeerHistory(PrivateBTPeerInfo peerinfo)
        {
            return DatabaseProvider.GetInstance().InsertPeerHistory(peerinfo);
        }

        public static int UpdatePeerHistory(PrivateBTPeerInfo peerinfo, int end_type)
        {
            return DatabaseProvider.GetInstance().UpdatePeerHistory(peerinfo, end_type);
        }

    }
}
