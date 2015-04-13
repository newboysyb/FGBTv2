using System;
using System.Collections.Generic;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// BT种子信息类
    /// </summary>
    public class PrivateBTSeedInfo
    {
        private int m_seedid;//种子ID
        private bool m_ok = false;//种子信息是否完备
        private string m_path = "";//种子存放的目录（完整目录，包含保存的文件名）
        private int m_uid = 0;//种子发布者uid
        private int m_upload = 0;//正在做种的数量
        private int m_download = 0;//正在下载的数量
        private int m_finished = 0;//下载完成的数量
        private bool m_deleted = false;//种子是否被删除
        private string m_createdby = "";//创建种子的软件
        private DateTime m_createddate = new DateTime(1970,1,1);//创建种子的时间
        private double m_uploadratio = 1;//上传因子
        private double m_downloadratio = 1;//下载因子
        private int m_filecount = 0;//种子包含的文件计数
        private decimal m_filesize = 0;//种子总容量
        private int m_live = 0;//种子存活时间（秒）
        private DateTime m_lastlive = new DateTime(1970, 1, 1);//最后存活时间
        private string m_info1 = "";//种子分类信息
        private string m_info2 = "";//种子分类信息
        private string m_info3 = "";//种子分类信息
        private string m_info4 = "";//种子分类信息
        private string m_info5 = "";//种子分类信息
        private string m_info6 = "";//种子分类信息
        private string m_info7 = "";//种子分类信息
        private string m_info8 = "";//种子分类信息
        private string m_info9 = "";//种子分类信息
        private string m_info10 = "";//种子分类信息
        private string m_info11 = "";//种子分类信息
        private string m_info12 = "";//种子分类信息
        private string m_info13 = "";//种子分类信息
        private string m_info14 = "";//种子分类信息
        private int m_topicid = 0;//种子对应主题帖的id
        private string m_filename = "";//种子文件名称
        private int m_type = 0;//种子类别
        private int m_topseed = 0;//是否为置顶种子
        private string m_topictitle = "";//主题帖的标题（冗余信息）
        private string m_username = "";//发布者（冗余信息）
        private int m_post = 0;//回复数（冗余信息）
        private decimal m_traffic = 0;//种子下载流量
        private decimal m_uptraffic = 0;//种子上传流量
        private int m_ipv6 = 0;//是否来自ipv6，0为ipv4，1为ipv6，2为都有
        private bool m_singlefile = true;//是否为单文件
        private string m_foldername = "";//文件夹的名字
        private string m_infohash = "";//Info hash
        private string m_lastseedername = "";//最后做种人
        private int m_lastseederid = 0;//最后做种id
        private DateTime m_downloadratioexpiredate = new DateTime(2099, 1, 1);  //下载流量系数过期时间
        private DateTime m_uploadratioexpiredate = new DateTime(2099, 1, 1);  //下载流量系数过期时间
        private DateTime m_postdatetime = new DateTime(1970, 1, 1);   //种子发布时间


        public int Award = 0;
        public int PieceLength = 0;//种子内的piece length



        private int m_bluehour = 720;//蓝种时限

        /// <summary>
        /// 种子发布时间
        /// </summary>
        public System.DateTime PostDatetime
        {
            get { return m_postdatetime; }
            set { m_postdatetime = value; }
        }
        /// <summary>
        /// 上传流量系数过期时间
        /// </summary>
        public DateTime UploadRatioExpireDate
        {
            get { return m_uploadratioexpiredate; }
            set { m_uploadratioexpiredate = value; }
        }
        /// <summary>
        /// 下载流量系数过期时间
        /// </summary>
        public DateTime DownloadRatioExpireDate
        {
            get { return m_downloadratioexpiredate; }
            set { m_downloadratioexpiredate = value; }
        }
        /// <summary>
        /// 蓝种时限
        /// </summary>
        public int Bluehour
        {
            get { return m_bluehour; }
            set { m_bluehour = value; }
        }

        //public PrivateBTSeedInfo()
        //{
        //    m_ok = false;
        //    m_singlefile = true;
        //    m_seedid = 0;
        //    m_createddate = DateTime.Now;
        //    m_lastlive = DateTime.Now;
        //    m_path = "";

        //}

        /// <summary>
        /// 种子ID
        /// </summary>
        public int SeedId
        {
            get { return m_seedid; }
            set { m_seedid = value; }
        }
        /// <summary>
        /// 种子信息是否完备
        /// </summary>
        public bool OK
        {
            get { return m_ok; }
            set { m_ok = value; }
        }
        /// <summary>
        /// 种子存放的目录（完整目录，包含保存的文件名）
        /// </summary>
        public string Path
        {
            get { return m_path; }
            set { m_path = value; }
        }
        /// <summary>
        /// 种子发布者uid
        /// </summary>
        public int Uid
        {
            get { return m_uid; }
            set { m_uid = value; }
        }
        /// <summary>
        /// 正在做种的数量
        /// </summary>
        public int Upload
        {
            get { return m_upload; }
            set { m_upload = value; }
        }
        /// <summary>
        /// 正在下载的数量
        /// </summary>
        public int Download
        {
            get { return m_download; }
            set { m_download = value; }
        }
        /// <summary>
        /// 下载完成的数量
        /// </summary>
        public int Finished
        {
            get { return m_finished; }
            set { m_finished = value; }
        }
        /// <summary>
        /// 种子是否被删除
        /// </summary>
        public bool Deleted
        {
            get { return m_deleted; }
            set { m_deleted = value; }
        }
        /// <summary>
        /// 创建种子的软件
        /// </summary>
        public string CreatedBy
        {
            get { return m_createdby; }
            set { m_createdby = value; }
        }
        /// <summary>
        /// 创建种子的时间
        /// </summary>
        public DateTime CreatedDate
        {
            get { return m_createddate; }
            set { m_createddate = value; }
        }
        /// <summary>
        /// 上传因子
        /// </summary>
        public double UploadRatio
        {
            get { return m_uploadratio; }
            set { m_uploadratio = value; }
        }
        /// <summary>
        /// 下载因子
        /// </summary>
        public double DownloadRatio
        {
            get { return m_downloadratio; }
            set { m_downloadratio = value; }
        }
        /// <summary>
        /// 种子包含的文件计数
        /// </summary>
        public int FileCount
        {
            get { return m_filecount; }
            set { m_filecount = value; }
        }
        /// <summary>
        /// 种子总容量
        /// </summary>
        public decimal FileSize
        {
            get { return m_filesize; }
            set { m_filesize = value; }
        }
        /// <summary>
        /// 种子存活时间（秒）
        /// </summary>
        public int Live
        {
            get { return m_live; }
            set { m_live = value; }
        }
        /// <summary>
        /// 最后存活时间
        /// </summary>
        public DateTime LastLive
        {
            get { return m_lastlive; }
            set { m_lastlive = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info1
        {
            get { return m_info1; }
            set { m_info1 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info2
        {
            get { return m_info2; }
            set { m_info2 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info3
        {
            get { return m_info3; }
            set { m_info3 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info4
        {
            get { return m_info4; }
            set { m_info4 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info5
        {
            get { return m_info5; }
            set { m_info5 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info6
        {
            get { return m_info6; }
            set { m_info6 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info7
        {
            get { return m_info7; }
            set { m_info7 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info8
        {
            get { return m_info8; }
            set { m_info8 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info9
        {
            get { return m_info9; }
            set { m_info9 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info10
        {
            get { return m_info10; }
            set { m_info10 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info11
        {
            get { return m_info11; }
            set { m_info11 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info12
        {
            get { return m_info12; }
            set { m_info12 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info13
        {
            get { return m_info13; }
            set { m_info13 = value; }
        }
        /// <summary>
        /// 种子分类信息
        /// </summary>
        public string Info14
        {
            get { return m_info14; }
            set { m_info14 = value; }
        }
        /// <summary>
        /// 种子对应主题帖的id
        /// </summary>
        public int TopicId
        {
            get { return m_topicid; }
            set { m_topicid = value; }
        }
        /// <summary>
        /// 种子文件名称
        /// </summary>
        public string FileName
        {
            get { return m_filename; }
            set { m_filename = value; }
        }
        /// <summary>
        /// 种子类别,1电影，2剧集，3动漫，4音乐，5游戏，6纪录，7体育，8综艺，9软件，10学习，11视频，12其他
        /// </summary>
        public int Type
        {
            get { return m_type; }
            set { m_type = value; }
        }
        /// <summary>
        /// 是否为置顶种子
        /// </summary>
        public int TopSeed
        {
            get { return m_topseed; }
            set { m_topseed = value; }
        }
        /// <summary>
        /// 主题帖的标题（冗余信息）
        /// </summary>
        public string TopicTitle
        {
            get { return m_topictitle; }
            set { m_topictitle = value; }
        }
        /// <summary>
        /// 发布者（冗余信息）
        /// </summary>
        public string UserName
        {
            get { return m_username; }
            set { m_username = value; }
        }
        /// <summary>
        /// 回复数（冗余信息）
        /// </summary>
        public int Post
        {
            get { return m_post; }
            set { m_post = value; }
        }
        /// <summary>
        /// 种子流量
        /// </summary>
        public decimal Traffic
        {
            get { return m_traffic; }
            set { m_traffic = value; }
        }
        /// <summary>
        /// 种子上传流量
        /// </summary>
        public decimal UpTraffic
        {
            get { return m_uptraffic; }
            set { m_uptraffic = value; }
        }
        /// <summary>
        /// 是否来自ipv6，0为ipv4，1为ipv6，2为都有
        /// </summary>
        public int IPv6
        {
            get { return m_ipv6; }
            set { m_ipv6 = value; }
        }
        /// <summary>
        /// 是否为单文件
        /// </summary>
        public bool SingleFile
        {
            get { return m_singlefile; }
            set { m_singlefile = value; }
        }
        /// <summary>
        /// 文件夹的名字
        /// </summary>
        public string FolderName
        {
            get { return m_foldername; }
            set { m_foldername = value; }
        }
        /// <summary>
        /// INFO HASH
        /// </summary>
        public string InfoHash
        {
            get { return m_infohash; }
            set { m_infohash = value; }
        }
        /// <summary>
        /// 最后做种人
        /// </summary>
        public string LastSeederName
        {
            get { return m_lastseedername; }
            set { m_lastseedername = value; }
        }
        /// <summary>
        /// 最后做种id
        /// </summary>
        public int LastSeederId
        {
            get { return m_lastseederid; }
            set { m_lastseederid = value; }
        }
    }

    /// <summary>
    /// Tracker种子信息类
    /// </summary>
    public class PTSeedinfoTracker
    {
        public int SeedId = -1;          //种子ID
        public int Status = 0;          //0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        public int Uid = 0;             //种子发布者uid
        public float UploadRatio = 1;  //上传因子
        public float DownloadRatio = 1;//下载因子
        public decimal FileSize = 0;    //种子总容量
        public string InfoHash = "";    //Info hash
    }

    /// <summary>
    /// Tracker种子信息类，简短，列表用
    /// </summary>
    public class PTSeedinfoShort
    {
        public int SeedId = -1;          //种子ID
        /// <summary>
        /// 0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        /// </summary>
        public int Status = 0;          
        public int Uid = 0;             //种子发布者uid
        public float UploadRatio = 1;  //上传因子
        public DateTime UploadRatioExpireDate = new DateTime(9999, 1, 1);
        public float DownloadRatio = 1;//下载因子
        public DateTime DownloadRatioExpireDate = new DateTime(9999, 1, 1);
        public decimal FileSize = 0;    //种子总容量

        public int Upload = 0;          //正在做种的数量
        public int Download = 0;        //正在下载的数量
        public int Finished = 0;        //下载完成的数量
        
        public int FileCount = 0;       //种子包含的文件计数
        public int Live = 0;            //种子存活时间（秒）
        public DateTime LastLive = new DateTime(1970, 1, 1);//最后存活时间
        
        public int TopicId = 0;         //种子对应主题帖的id

        public int Type = 0;            //种子类别
        public int TopSeed = 0;         //是否为置顶种子
        public string TopicTitle = "";  //主题帖的标题（冗余信息）
        public string UserName = "";    //发布者（冗余信息）
        public string InfoHash_c = "";  //种子文件列表部分的INFO HASH

        public int Replies = 0;          //回复数（冗余信息，外部更新）
        public int Views = 0;            //查看数（冗余信息，外部更新）
        public DateTime PostDateTime = new DateTime(1970, 1, 1); //发布时间

        public decimal Traffic = 0;     //种子下载流量
        public decimal UpTraffic = 0;   //种子上传流量
        public int IPv6 = 0;            //是否来自ipv6，0为ipv4，1为ipv6，2为都有

        public int Rss_Acc = 0;
        public int Rss_Keep = 0;
        public int Rss_Pub = 0;

        public decimal User_UpTraffic = 0M;  //当前用户该种子上传量，保种信息额外加载项
        public int User_Keeptime = 0;           //当前用户该种子保种时间，保种信息额外加载项
        public DateTime LastFinish = new DateTime(1970, 1, 1); //最后完成时间，保种信息额外加载项
        public DateTime LastPeerCountUpdate = new DateTime(1970, 1, 1); //最后种子数更新时间

        //文字信息显示部分
        public string Dis_ChnTypeName = "";     //种子类别中文字符串
        public string Dis_Size = "";            //种子大小字符串
        public string Dis_UploadCount = "";     //做种人数描述html
        public string Dis_DownloadTraffic = ""; //流量描述html
        public int    Dis_UserDisplayStyle = 0;//区分用户正在上传下载的种子
        public string Dis_TopicTitleFilter = "";//过滤单引号之后的种子名
        public string Dis_EngTypeName = "";     //种类英文描述例如movie
        public string Dis_TrafficCheck = "";    //流量异常信息
        public string Dis_TimetoLive = "";      //存活时间字符串
        public string Dis_DownloadRatioNote = "";//下载系数提醒字符串
        public string Dis_UploadRatioNote = ""; //下载系数提醒字符串
        public string Dis_RatioNoteAdd = "";    //附加下载系数提醒字符串
        public string Dis_PostDateTime = "";    //发布时间
        public string Dis_KeepReward_All = "";    //当前用户的保种总系数和总保种流量
        public string Dis_UserKeepTime = "";      //当前用户的保种时间
        public string Dis_UserUpTraffic = "";     //当前用户的上传流量
        public string Dis_KeepRewardFactor = ""; //保种系数
    }


    /// <summary>
    /// Tracker种子信息类，完整类
    /// </summary>
    public class PTSeedinfo : PTSeedinfoShort
    {

        public string Path = "";//种子存放的目录（完整目录，包含保存的文件名）
        
        public string CreatedBy = "";//创建种子的软件
        public DateTime CreatedDate = new DateTime(1970, 1, 1);//创建种子的时间
        
        public string Info1 = "";//种子分类信息
        public string Info2 = "";//种子分类信息
        public string Info3 = "";//种子分类信息
        public string Info4 = "";//种子分类信息
        public string Info5 = "";//种子分类信息
        public string Info6 = "";//种子分类信息
        public string Info7 = "";//种子分类信息
        public string Info8 = "";//种子分类信息
        public string Info9 = "";//种子分类信息
        public string Info10 = "";//种子分类信息
        public string Info11 = "";//种子分类信息
        public string Info12 = "";//种子分类信息
        public string Info13 = "";//种子分类信息
        public string Info14 = "";//种子分类信息

        
        public string FileName = "";//种子文件名称
        public string InfoHash = "";//Info hash

        public bool SingleFile = true;//是否为单文件
        public string FolderName = "";//文件夹的名字
        public string LastSeederName = "";//最后做种人
        public int LastSeederId = 0;//最后做种id
        public decimal Award = 0;//该种子获得的奖励


        //以下数值并不保存在数据库中
        public int PieceLength = 0;//种子内的piece length
    }

    /// <summary>
    /// 种子操作列表
    /// </summary>
    public class PTSeedOPinfo : PTSeedinfoShort
    {
        public int OperatorId = -1;     //操作者id
        public string Operator = "";    //操作者用户名
        public DateTime OpDateTime = new DateTime(1970, 1, 1); //操作时间
        public int OpType = -1;         //操作类别
        public string Operation = "";   //种子操作内容（用于操作显示）
        public string OpReason = "";    //种子操作原因

        public string Dis_OpDateTime = "";  //显示的操作时间
    }
    
}
