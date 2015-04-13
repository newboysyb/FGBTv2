using System;
using System.Collections.Generic;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// Abt种子信息类
    /// </summary>
    public class AbtSeedInfo
    {
        /// <summary>
        /// Abt种子ID
        /// </summary>
        public int Aid = 0;
        public int Uid = 0;
        /// <summary>
        /// Abt种子HASH
        /// </summary>
        public string InfoHash = "";
        /// <summary>
        /// 正在上传数
        /// </summary>
        public int Upload = 0;
        /// <summary>
        /// 正在下载数
        /// </summary>
        public int Download = 0;
        /// <summary>
        /// 完成数
        /// </summary>
        public int Finished = 0;
        /// <summary>
        /// 最后存活
        /// </summary>
        public DateTime LastLive = new DateTime(1990, 1, 1);
        /// <summary>
        /// 文件数
        /// </summary>
        public int FileCount = 0;
        /// <summary>
        /// 文件大小
        /// </summary>
        public decimal FileSize = 0M;
        public string FileName = "";
        public string Dis_Size = "";
        public string Dis_Live = "";
        public string Dis_Upload = "";
    }
    /// <summary>
    /// Abt 节点信息
    /// </summary>
    public class AbtPeerInfo
    {
        /// <summary>
        /// Abt种子ID
        /// </summary>
        public int Aid = 0;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Uid = 0;
        /// <summary>
        /// Abt 节点ID
        /// </summary>
        public string Peerid = "";
        /// <summary>
        /// IPv4地址
        /// </summary>
        public string IPv4 = "";
        /// <summary>
        /// IPv6地址
        /// </summary>
        public string IPv6 = "";
        /// <summary>
        /// 端口
        /// </summary>
        public int Port = 0;
        /// <summary>
        /// 下载比例
        /// </summary>
        public float Percentage = 0;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastTime = new DateTime(1990, 1, 1);
    }

    public class AbtDownloadInfo
    {
        public int Aid = 0;
        public string Passkey = "";
        public int Uid = 0;
        public string InfoHash = "";
        public string Peerid = "";
        public int Status = 0;
        public float Percentage = 0;
        public DateTime RecordTime = new DateTime(1990, 1, 1);
        public DateTime LastTime = new DateTime(1990, 1, 1);
    }
}
