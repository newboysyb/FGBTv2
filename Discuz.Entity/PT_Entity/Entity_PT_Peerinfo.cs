using System;
using System.Collections.Generic;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// 位置信息共用体
    /// </summary>
    public enum PTsIpRegionInBuaa
    {
        INIT = 0,
        /// <summary>
        /// 沙河校区节点 101
        /// </summary>
        SHAHE_DISTRICT = 101,
        /// <summary>
        /// 大运村宿舍节点 1
        /// </summary>
        DAYUNCUN_VILLAGE = 1,
        /// <summary>
        /// 本部南区宿舍节点 2
        /// </summary>
        DORMITORY_SOUTH = 2,
        /// <summary>
        /// 本部北区宿舍节点 3
        /// </summary>
        DORMITORY_NORTH = 3,
        /// <summary>
        /// 教学区节点 4
        /// </summary>
        TEACHING_AERA = 4,
        /// <summary>
        /// 新主楼节点 5
        /// </summary>
        NEWMAIN_BUILDING = 5,
        /// <summary>
        /// 图书馆节点 6
        /// </summary>
        LIBRARY = 6,
        /// <summary>
        /// 核心服务器区 7
        /// </summary>
        SERVER_CORE = 7,
        /// <summary>
        /// 服务器区 8
        /// </summary>
        SERVER_AERA = 8,
        /// <summary>
        /// 家属区 9
        /// </summary>
        LIVING_AREA = 9,

        /// <summary>
        /// 未知区域 253 
        /// </summary>
        UNKNOWN_AREA = 253,
        /// <summary>
        /// 校外节点 254
        /// </summary>
        NOT_IN_BUAA = 254,
        /// <summary>
        /// 错误 255
        /// </summary>
        ERROR = 255,

    }

    /// <summary>
    /// Peer信息类
    /// </summary>
    public class PrivateBTPeerInfo
    {
        /// <summary>
        /// Peer自动编号
        /// </summary>
        public int Pid = 0;
        /// <summary>
        /// 锁定标志
        /// </summary>
        public int PeerLock = -1;

        //////////////////////////////////////////////////////////////////////////
        //原始信息：由客户端提交
        
        /// <summary>
        /// 原始信息：种子HASH
        /// </summary>
        public string InfoHash = "";
        /// <summary>
        /// 原始信息：用户Passkey
        /// </summary>
        public string Passkey = "";
        /// <summary>
        /// 原始信息：客户端Peerid
        /// </summary>
        public string PeerId = "";
        /// <summary>
        /// 原始信息：客户端端口
        /// </summary>
        public int Port = 0;
        /// <summary>
        /// 原始信息：上传流量；从peer表读取时，仅为本次数据，不包括历史数据
        /// </summary>
        public decimal Upload = 0M;
        /// <summary>
        /// 原始信息：下载流量；从peer表读取时，仅为本次数据，不包括历史数据
        /// </summary>
        public decimal Download = 0M;
        /// <summary>
        /// 原始信息：剩余流量
        /// </summary>
        public decimal Left = 0M;
        /// <summary>
        /// 原始信息：错误流量
        /// </summary>
        public decimal Corrupt = 0M;
        /// <summary>
        /// 原始信息：客户端IPv4地址
        /// </summary>
        public string IPv4IP = "";
        /// <summary>
        /// 原始信息：客户端IPv6地址
        /// </summary>
        public string IPv6IP = "";
        /// <summary>
        /// 原始信息：客户提交的附加IPv6地址
        /// </summary>
        public string IPv6IPAdd = "";
        /// <summary>
        /// 原始信息：客户端名称
        /// </summary>
        public string Client = "";
        /// <summary>
        /// 原始信息：紧凑模式发送用户列表
        /// </summary>
        public int Compact = 0;
        /// <summary>
        /// 原始信息：发送用户列表时省略Peerid
        /// </summary>
        public int No_Peer_Id = 0;
        /// <summary>
        /// 原始信息：上次更新时服务器发送的TrackerId
        /// </summary>
        public string TrackerId = "";
        /// <summary>
        /// 原始信息：请求内容. 此次操作内容，客户端发送来的started，completed，stopped，空（中间更新）。 
        /// </summary>
        public string Event = "";
        /// <summary>
        /// 原始信息：客户端提交的原始信息
        /// </summary>
        public string RawRequestString = "";

        //////////////////////////////////////////////////////////////////////////
        //计算信息：由客户端提交数据计算或查找获得

        /// <summary>
        /// 计算信息：种子ID
        /// </summary>
        public int SeedId = 0;
        /// <summary>
        /// 计算信息：用户ID
        /// </summary>
        public int Uid = 0;
        /// <summary>
        /// 计算信息：当前完成百分比
        /// </summary>
        public double Percentage = 0;
        /// <summary>
        /// 计算信息：当前上传速度 B/s
        /// </summary>
        public double UploadSpeed = 0;
        /// <summary>
        /// 计算信息：当前下载速度 B/s
        /// </summary>
        public double DownloadSpeed = 0;
        /// <summary>
        /// 计算信息：是否正在做种
        /// </summary>
        public bool IsSeed = false;
        /// <summary>
        /// 计算信息：IP状态信息
        /// 0.校内纯IPv4，1.校内IPv4+ISATAP，2.校内IPv4+异常v6，3.校内IPv4+原生IPv6，4.校内纯ISATAP，5.校内纯原生IPv6，6.校外IPv6，7.IPv6首次更新，-1.校外IPv4
        /// </summary>
        public int IPStatus = 0;
        /// <summary>
        /// 计算信息：IP位置信息
        /// </summary>
        public PTsIpRegionInBuaa IPRegionInBuaa = 0;


        //////////////////////////////////////////////////////////////////////////
        //历史信息：当前客户端活动的历史信息
        
        /// <summary>
        /// 历史信息：本次活动的首次更新时间
        /// </summary>
        public DateTime FirstTime = DateTime.Now;
        /// <summary>
        /// 历史信息：本次活动的最后更新时间
        /// </summary>
        public DateTime LastTime = DateTime.Now;
        /// <summary>
        /// 历史信息：种子的历史保种时间（秒）
        /// </summary>
        public int KeepTime = 0;
        /// <summary>
        /// 历史信息：本次活动的最后IPv4更新时间
        /// </summary>
        public DateTime v4Last = DateTime.Now;
        /// <summary>
        /// 历史信息：本次活动的最后IPv6更新时间
        /// </summary>
        public DateTime v6Last = DateTime.Now;
        /// <summary>
        /// 历史信息：该用户该种子历史总计上传量
        /// </summary>
        public decimal TotalUpload = 0M;
        /// <summary>
        /// 历史信息：该用户该种子历史总计下载量
        /// </summary>
        public decimal TotalDownload = 0M;
        /// <summary>
        /// 历史信息：下载节点地址发送情况，-1学院路，-2沙河，0 不限制， >0 限定区域
        /// </summary>
        public int LastSend = 0;


        public PrivateBTPeerInfo(PrivateBTPeerInfo peerinfo)
        {
            InfoHash = peerinfo.InfoHash;
            Passkey = peerinfo.Passkey;
            PeerId = peerinfo.PeerId;
            Port = peerinfo.Port;
            Upload = peerinfo.Upload;
            Download = peerinfo.Download;
            Left = peerinfo.Left;
            Corrupt = peerinfo.Corrupt;
            IPv4IP = peerinfo.IPv4IP;
            IPv6IP = peerinfo.IPv6IP;
            IPv6IPAdd = peerinfo.IPv6IPAdd;
            Client = peerinfo.Client;
            RawRequestString = peerinfo.RawRequestString;


            SeedId = peerinfo.SeedId;
            Uid = peerinfo.Uid;
            Percentage = peerinfo.Percentage;
            UploadSpeed = peerinfo.UploadSpeed;
            DownloadSpeed = peerinfo.DownloadSpeed;
            IsSeed = peerinfo.IsSeed;
            IPStatus = peerinfo.IPStatus;
            
            
            FirstTime = peerinfo.FirstTime;
            LastTime = peerinfo.LastTime;
            v4Last = peerinfo.v4Last;
            v6Last = peerinfo.v6Last;
            TotalUpload = peerinfo.TotalUpload;
            TotalDownload = peerinfo.TotalDownload;
        }
        public PrivateBTPeerInfo()
        {
        }
    }

    /// <summary>
    /// 用户保种信息统计信息类
    /// </summary>
    public class PTKeepRewardStatic
    {
        public int TotalUpCount = 0;
        public decimal TotalRewardPerHour = 0M;

        public int BigBig_UpCount = 0;
        public decimal BigBig_RewardPerHour = 0M;
        public decimal BigBig_RewardPerHour_Limit = 600 * 1024 * 1024M;

        public int Big_UpCount = 0;
        public decimal Big_RewardPerHour = 0M;
        public decimal Big_RewardPerHour_Limit = 600 * 1024 * 1024M;

        public int Mid_UpCount = 0;
        public decimal Mid_RewardPerHour = 0M;
        public decimal Mid_RewardPerHour_Limit = 600 * 1024 * 1024M;

        public int Small_UpCount = 0;
        public decimal Small_RewardPerHour = 0M;
        public decimal Small_RewardPerHour_Limit = 400 * 1024 * 1024M;

        public int Tiny_UpCount = 0;
        public decimal Tiny_RewardPerHour = 0M;
        public decimal Tiny_RewardPerHour_Limit = 200 * 1024 * 1024M;

        public decimal Old_RewardPerHour = 0M;
        public decimal Old_RewardPerHour_Real = 0M;
    }
}




//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Discuz.Entity
//{
//    /// <summary>
//    /// Peer信息类，默认值seedid,uid,sessionid均为-1，其余值为空或0
//    /// </summary>
//    public class PTPeerInfo
//    {
//        //////////////////////////////////////////////////////////////////////////
//        // 基本信息
//        /// <summary>
//        /// 基本信息. 种子ID
//        /// </summary>
//        public int SeedId = -1;
//        /// <summary>
//        /// 基本信息. 用户ID
//        /// </summary>
//        public int Uid = -1;
//        /// <summary>
//        /// 基本信息. 客户端ID，包含peer_id的后24位随机数和8位的Key随机数，认为全局唯一
//        /// </summary>
//        public string PeerId = "";
//        /// <summary>
//        /// 客户端
//        /// </summary>
//        public string Client = "";
//        /// <summary>
//        /// Tracker会话ID，bt_peer表自增量
//        /// </summary>
//        public int SessionId = -1;

//        //////////////////////////////////////////////////////////////////////////
//        // IP和端口
//        /// <summary>
//        /// IP和端口. IPv4地址
//        /// </summary>
//        public string IPv4IP = "";          
//        /// <summary>
//        /// IP和端口. 从服务器获得的ipv6地址
//        /// </summary>
//        public string IPv6IP = "";          
//        /// <summary>
//        /// IP和端口. ut的tracker信息里附加的ipv6地址
//        /// </summary>
//        public string IPv6IPAdd = "";       
//        /// <summary>
//        /// IP和端口. IP地址类型，是否为IPv6，4-IPv4，5-IPv4_IPv6Add，6-IPv6，7-IPv6_IPv6Add，10-IPv4_IPv6，11-IPv4_IPv6_IPv6Add，0错误
//        /// </summary>
//        public int IPType = 0;              
//        /// <summary>
//        /// IP和端口. 端口
//        /// </summary>
//        public int Port = 0;

//        //////////////////////////////////////////////////////////////////////////
//        // 请求内容
//        /// <summary>
//        /// 请求内容. 此次操作内容，客户端发送来的started，completed，stopped，空（中间更新）。 
//        /// 或者处理后的自定义类型error（出错），notraffic（没有流量变化），uptraffic（只更新上传流量）,
//        /// traffic（更新上传下载流量），iptraffic（更新IP地址和上传下载流量）
//        /// </summary>
//        public string Event = "";
//        /// <summary>
//        /// 请求内容. 需要的节点数量
//        /// </summary>
//        public int NumWant = 0;
//        /// <summary>
//        /// 请求内容. 返回peer列表紧凑模式
//        /// </summary>
//        public int Compact = 0;
//        /// <summary>
//        /// 请求内容. peer列表不需要peerid
//        /// </summary>
//        public int NoPeerid = 0;
//        /// <summary>
//        /// 下次更新时间
//        /// </summary>
//        public DateTime NextTime = DateTime.Parse("1970-01-01");
//        /// <summary>
//        /// 最后更新时间
//        /// </summary>
//        public DateTime LastTime = DateTime.Parse("1970-01-01");
//        /// <summary>
//        /// 首次更新时间
//        /// </summary>
//        public DateTime FirstTime = DateTime.Parse("1970-01-01");
//        /// <summary>
//        /// 更新次数
//        /// </summary>
//        public int UpdateTimes = 0;
        
//        //////////////////////////////////////////////////////////////////////////
//        // 流量信息

//        /// <summary>
//        /// 汇报的上传流量（本次会话累计值）
//        /// </summary>
//        public decimal Uploaded = 0M;
//        /// <summary>
//        /// 汇报的下载流量（本次会话累计值）
//        /// </summary>
//        public decimal Downloaded = 0M;
//        /// <summary>
//        /// 汇报的剩余流量
//        /// </summary>
//        public decimal Left = 0M;
//        /// <summary>
//        /// 汇报的损坏流量
//        /// </summary>
//        public decimal Corrupt = 0M;
//        /// <summary>
//        /// 【真实上传】会话累计上传流量，超过1GB以后，将被更新到Traffic表，并置0
//        /// </summary>
//        public decimal SessionUploaded = 0M;
//        /// <summary>
//        /// 【真实下载】会话累计下载流量，超过1GB以后，将被更新到Traffic表，并置0
//        /// </summary>
//        public decimal SessionDownloaded = 0M;
//        /// <summary>
//        /// 【真实上传/更新数据】总计上传流量，仅供显示用
//        /// </summary>
//        public decimal TotalUploaded = 0M;
//        /// <summary>
//        /// 【真实下载/更新数据】总计下载流量，仅供显示用
//        /// </summary>
//        public decimal TotalDownloaded = 0M;
//        /// <summary>
//        /// 【统计上传/更新数据】包含上传系数，会话累计上传流量，超过1GB以后，将被更新到Traffic表，并置0
//        /// </summary>
//        public decimal StatsSessionUploaded = 0M;
//        /// <summary>
//        /// 【统计下载/更新数据】包含下载系数，会话累计下载流量，超过1GB以后，将被更新到Traffic表，并置0
//        /// </summary>
//        public decimal StatsSessionDownloaded = 0M;
//        /// <summary>
//        /// 【统计上传/更新数据】会话累计奖励上传流量，超过1GB以后，将被更新到Traffic表，并置0
//        /// </summary>
//        public decimal StatsSessionAward = 0M;

//        //////////////////////////////////////////////////////////////////////////
//        //计算结果
        
//        /// <summary>
//        /// 完成率
//        /// </summary>
//        public float Percentage = 0f;
//        /// <summary>
//        /// 上传速度，按MB/s计算
//        /// </summary>
//        public float UploadSpeed = 0f;
//        /// <summary>
//        /// 下载速度，按MB/s计算
//        /// </summary>
//        public float DownloadSpeed = 0f;
      
//        //////////////////////////////////////////////////////////////////////////
//        ////////////////////////////////////////////////////////////////////////// 
//        // 放弃的数值

//        //public PTPeerInfo( PTPeerInfo peerinfo)
//        //{
//        //    SeedId = peerinfo.SeedId;
//        //    Uid = peerinfo.Uid;
//        //    IsSeed = peerinfo.IsSeed;
//        //    IPType = peerinfo.IPType;
//        //    PeerId = peerinfo.PeerId;
//        //    IPv4IP = peerinfo.IPv4IP;
//        //    IPv6IP = peerinfo.IPv6IP;
//        //    Port = peerinfo.Port;
//        //    Percentage = peerinfo.Percentage;
//        //    Uploaded = peerinfo.Uploaded;
//        //    UploadSpeed = peerinfo.UploadSpeed;
//        //    Downloaded = peerinfo.Downloaded;
//        //    DownloadSpeed = peerinfo.DownloadSpeed;
//        //    NextTime = peerinfo.NextTime;
//        //    LastTime = peerinfo.LastTime;
//        //    v4Last = peerinfo.v4Last;
//        //    v6Last = peerinfo.v6Last;
//        //    Client = peerinfo.Client;
//        //    IPv6IPAdd = peerinfo.IPv6IPAdd;
//        //    TotalUploaded = peerinfo.TotalUploaded;
//        //    TotalDownloaded = peerinfo.TotalDownloaded;
//        //}
//        public PTPeerInfo()
//        {
//        }
//    }
//}
