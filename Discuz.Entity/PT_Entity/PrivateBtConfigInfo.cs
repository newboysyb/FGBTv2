using System;
using System.Collections.Generic;
using System.Text;

namespace Discuz.Entity
{
    

    /// <summary>
    /// BT配置信息类
    /// </summary>
    public class PrivateBTConfigInfo
    {
        //指令更新
        private bool m_allowfreeregister;       //是否允许公开注册 true是
        private bool m_allowinviteregister;     //是否允许邀请注册 true是
        private int m_freereglimit;             //开放注册人数上限 int32
        private int m_totaluserlimit;           //邀请注册人数上限(即总人数上限) int32
        private DateTime m_freeregbegintime;    //开放注册开始时间 
        private DateTime m_freeregendtime;      //开放注册截止时间
        private float m_downloadmulti;         //全站下载因子
        private DateTime m_downmultibegintime;  //全站下载因子开始时间 
        private DateTime m_downmultiendtime;    //全站下载因子截止时间
        private float m_uploadmulti;           //全站上传因子
        private DateTime m_upmultibegintime;    //全站上传因子开始时间 
        private DateTime m_upmultiendtime;      //全站上传因子截止时间
        private float m_maxuploadmulti;        //最高上传倍率
        private decimal m_inviteprice;          //邀请码价格

        //不更新
        private int m_totalusercount;           //用户总数

        //定时更新 5min
        private int m_limitedusercount;         //受限用户数
        private decimal m_totalupload;          //全站总上传
        private decimal m_totaldownload;        //全站总下载
        private decimal m_realtraffic;          //全站真实总流量
        private int m_seedcount;                //种子总数
        private decimal m_seedcapacity;         //种子总容量
        private int m_onlineseedcount;          //在线种子数
        private decimal m_onlineseedcapacity;   //在线种子容量
        private int m_seedercount;              //做种人数
        private int m_downloadcount;            //下载人数


        public DateTime LastConfigReadTime =  new DateTime(1990, 1, 1);
        /// <summary>
        /// 测试用户
        /// </summary>
        public string DebugUser = "";

        /// <summary>
        /// 全站上传因子开始时间
        /// </summary>
        public DateTime UpMultiBeginTime
        {
            get { return m_upmultibegintime; }
            set { m_upmultibegintime = value; }
        }
        /// <summary>
        /// 全站上传因子截止时间
        /// </summary>
        public DateTime UpMultiEndTime
        {
            get { return m_upmultiendtime; }
            set { m_upmultiendtime = value; }
        }
        /// <summary>
        /// 全站下载因子开始时间
        /// </summary>
        public DateTime DownMultiBeginTime
        {
            get { return m_downmultibegintime; }
            set { m_downmultibegintime = value; }
        }
        /// <summary>
        /// 全站下载因子截止时间
        /// </summary>
        public DateTime DownMultiEndTime
        {
            get { return m_downmultiendtime; }
            set { m_downmultiendtime = value; }
        }
        /// <summary>
        /// 是否允许公开注册
        /// </summary>
        public bool AllowFreeRegister
        {
            get { return m_allowfreeregister; }
            set { m_allowfreeregister = value; }
        }
        /// <summary>
        /// 是否允许邀请注册
        /// </summary>
        public bool AllowInviteRegister
        {
            get { return m_allowinviteregister; }
            set { m_allowinviteregister = value; }
        }
        /// <summary>
        /// 开放注册人数上限
        /// </summary>
        public int FreeRegLimit
        {
            get { return m_freereglimit; }
            set { m_freereglimit = value; }
        }
        /// <summary>
        /// 邀请注册人数上限即总人数上限
        /// </summary>
        public int TotalUserLimit
        {
            get { return m_totaluserlimit; }
            set { m_totaluserlimit = value; }
        }
        /// <summary>
        /// 开放注册开始时间
        /// </summary>
        public DateTime FreeRegBeginTime
        {
            get { return m_freeregbegintime; }
            set { m_freeregbegintime = value; }
        }
        /// <summary>
        /// 开放注册截止时间
        /// </summary>
        public DateTime FreeRegEndTime
        {
            get { return m_freeregendtime; }
            set { m_freeregendtime = value; }
        }
        /// <summary>
        /// 全站下载因子
        /// </summary>
        public float DownloadMulti
        {
            get { return m_downloadmulti >= 0 ? m_downloadmulti : 0 ; }
            set { m_downloadmulti = value; }
        }

        /// <summary>
        /// 全站上传因子
        /// </summary>
        public float UploadMulti
        {
            get { return m_uploadmulti >= 0 ? m_uploadmulti : 0; }
            set { m_uploadmulti = value; }
        }
        /// <summary>
        /// 最高上传倍率
        /// </summary>
        public float MaxUploadMulti
        {
            get { return m_maxuploadmulti; }
            set { m_maxuploadmulti = value; }
        }
        /// <summary>
        /// 用户总数
        /// </summary>
        public int TotalUserCount
        {
            get { return m_totalusercount; }
            set { m_totalusercount = value; }
        }
        /// <summary>
        /// 受限用户数
        /// </summary>
        public int LimitedUserCount
        {
            get { return m_limitedusercount; }
            set { m_limitedusercount = value; }
        }
        /// <summary>
        /// 全站总上传
        /// </summary>
        public decimal TotalUpload
        {
            get { return m_totalupload; }
            set { m_totalupload = value; }
        }
        /// <summary>
        /// 全站总下载
        /// </summary>
        public decimal TotalDownload
        {
            get { return m_totaldownload; }
            set { m_totaldownload = value; }
        }
        /// <summary>
        /// 全站真实总流量
        /// </summary>
        public decimal RealTraffic
        {
            get { return m_realtraffic; }
            set { m_realtraffic = value; }
        }
        /// <summary>
        /// 种子总数
        /// </summary>
        public int SeedCount
        {
            get { return m_seedcount; }
            set { m_seedcount = value; }
        }
        /// <summary>
        /// 种子总容量
        /// </summary>
        public decimal SeedCapacity
        {
            get { return m_seedcapacity; }
            set { m_seedcapacity = value; }
        }
        /// <summary>
        /// 在线种子数
        /// </summary>
        public int OnlineSeedCount
        {
            get { return m_onlineseedcount; }
            set { m_onlineseedcount = value; }
        }
        /// <summary>
        /// 在线种子容量
        /// </summary>
        public decimal OnlineSeedCapacity
        {
            get { return m_onlineseedcapacity; }
            set { m_onlineseedcapacity = value; }
        }
        /// <summary>
        /// 做种人数
        /// </summary>
        public int SeederCount
        {
            get { return m_seedercount; }
            set { m_seedercount = value; }
        }
        /// <summary>
        /// 下载人数
        /// </summary>
        public int DownloadCount
        {
            get { return m_downloadcount; }
            set { m_downloadcount = value; }
        }
        /// <summary>
        /// 邀请码价格
        /// </summary>
        public decimal InvitePrice
        {
            get { return m_inviteprice; }
            set { m_inviteprice = value; }
        }

        
    }
}
