using System;
using System.Data;
using System.Text;

#if NET1
#else
using Discuz.Common.Generic;
#endif

using Discuz.Entity;
using System.Data.Common;


namespace Discuz.Data
{
    public partial interface IDataProvider
    {
        /// <summary>
        /// 插入Abt种子
        /// </summary>
        /// <returns></returns>
        int AbtInsertSeed(string infohash, int filecount, decimal filesize, string filename, int uid);
        /// <summary>
        /// 插入Abt节点信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int AbtInsertPeer(AbtPeerInfo peerinfo);
        /// <summary>
        /// 插入Abt下载信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int AbtInsertDownload(int aid, int uid, string passkey, string infohash, int status);
        /// <summary>
        /// 插入Abt日志信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int AbtInsertLog(int aid, int uid, int type, string msg);



        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 获取Abt种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        DataTable AbtGetSeedInfoList(DateTime lasttime);
        /// <summary>
        /// 获取Abt种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        IDataReader AbtGetSeedInfo(int aid);
        /// <summary>
        /// 获取Abt种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        DataTable AbtGetSeedInfoList(DataTable aid);
        /// <summary>
        /// 获取Abt节点信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        IDataReader AbtGetPeerInfo(int aid, string peerid);
        /// <summary>
        /// 获取Abt节点数
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        int AbtGetPeerCount(int aid, bool isupload);
        /// <summary>
        /// 获取Abt下载记录
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        IDataReader AbtGetDownload(int aid, int uid, string passkey);
        /// <summary>
        /// 获取Abt下载记录
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        IDataReader AbtGetDownload(int aid, int uid);
        /// <summary>
        /// 获取Abt节点列表（IPv4、IPv6、Port列表）
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        DataTable AbtGetPeerList(int aid, bool isupload);

        /// <summary>
        /// 获取Abt节点列表（aid列表）
        /// </summary>
        /// <returns></returns>
        DataTable AbtGetPeerList(DateTime lasttime);


        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 删除Abt节点
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        int AbtDeletePeer(DateTime lasttime);
        /// <summary>
        /// 删除Abt节点
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        int AbtDeletePeer(int aid, string peerid);
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        int AbtDeleteDownload(DateTime lasttime);
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        int AbtDeleteDownload(int aid);
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        int AbtDeleteDownload(int aid, int uid);
        /// <summary>
        /// 删除Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        int AbtDeleteSeed(DateTime lastlive);
        /// <summary>
        /// 删除Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        int AbtDeleteSeed(int aid);


        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 更新Abt节点信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int AbtUpdatePeer(AbtPeerInfo peerinfo);
        /// <summary>
        /// 更新Abt下载信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int AbtUpdateDownload(int aid, int uid, string passkey, int status, string peerid, float percentage);
        /// <summary>
        /// 更新种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="updatelive"></param>
        /// <returns></returns>
        int AbtUpdateSeed(int aid, int upload, int download, bool updatelive, bool addfinished);
    }
}
