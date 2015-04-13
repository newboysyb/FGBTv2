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
        int InsertPeerHistoryLog(int pid, int seedid, int uid, int loglevel, string logtype, string message);


        /// <summary>
        /// 插入peer error记录信息
        /// </summary>
        /// <returns></returns>
        int InsertPeerErrorInfo(int uid, int seedid, int errortype, int errorlevel, int ipregion, string iptail, int preipregion, string preiptail, int preipv6region, string preipv6tail,
            decimal upload, decimal preupload, decimal download, decimal predownload, string client, int port, string message);
        

        /// <summary>
        /// 插入peer日志信息
        /// </summary>
        /// <param name="fromipregion"></param>
        /// <param name="fromiptail"></param>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int InsertPeerLog(int eid, int loglevel, int fromipregion, string fromiptail, string rawstring, PrivateBTPeerInfo peerinfo);




        int InsertPeerHistory(PrivateBTPeerInfo peerinfo);
        int UpdatePeerHistory(PrivateBTPeerInfo peerinfo, int end_type);
    }
}
