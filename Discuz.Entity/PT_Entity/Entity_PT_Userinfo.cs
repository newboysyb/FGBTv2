using System;
using System.Collections.Generic;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// BT用户信息类，Tracker访问
    /// </summary>
    public class PTUserInfo
    {
        /// <summary>
        /// uid
        /// </summary>
        public int Uid = -1;                 //uid
        /// <summary>
        /// passkey,40个字母
        /// </summary>
        public string Passkey = "";         //passkey
        /// <summary>
        /// 积分
        /// </summary>
        public decimal Credits = 0M;             //积分
        /// <summary>
        /// 威望
        /// </summary>
        public decimal Extcredits1 = 0M;    //威望
        /// <summary>
        /// 金币
        /// </summary>
        public decimal Extcredits2 = 0M;    //金币
        /// <summary>
        /// 扩展积分3，上传
        /// </summary>
        public decimal Extcredits3 = 0M;    //上传
        /// <summary>
        /// 扩展积分4，下载
        /// </summary>
        public decimal Extcredits4 = 0M;    //下载
        /// <summary>
        /// 扩展积分5，今天上传，含做种奖励
        /// </summary>
        public decimal Extcredits5 = 0M;    //今天上传，含做种奖励
        /// <summary>
        /// 扩展积分6，今天下载
        /// </summary>
        public decimal Extcredits6 = 0M;    //今天下载
        /// <summary>
        /// 扩展积分7，真实上传
        /// </summary>
        public decimal Extcredits7 = 0M;    //真实上传
        /// <summary>
        /// 扩展积分8，真实下载
        /// </summary>
        public decimal Extcredits8 = 0M;    //真实下载
        /// <summary>
        /// 扩展积分9，今天真实上传
        /// </summary>
        public decimal Extcredits9 = 0M;    //今天真实上传
        /// <summary>
        /// 扩展积分10，今天真实下载
        /// </summary>
        public decimal Extcredits10 = 0M;   //今天真实下载
        /// <summary>
        /// 扩展积分11，保种奖励
        /// </summary>
        public decimal Extcredits11 = 0M;
        /// <summary>
        /// 扩展积分12，今天保种奖励
        /// </summary>
        public decimal Extcredits12 = 0M;
        /// <summary>
        /// 正在上传计数
        /// </summary>
        public int UploadCount = 0;         //正在上传计数
        /// <summary>
        /// 正在下载计数
        /// </summary>
        public int DownloadCount = 0;       //正在下载计数
        /// <summary>
        /// 完成计数
        /// </summary>
        public int FinishCount = 0;         //完成计数
        /// <summary>
        /// 发布计数
        /// </summary>
        public int PublishCount = 0;        //发布计数
        /// <summary>
        /// 共享率
        /// </summary>
        public float Ratio = 0;            //共享率
        /// <summary>
        /// 共享率保护
        /// </summary>
        public int RatioProtection = 0;     //共享率保护
        /// <summary>
        /// 用户组
        /// </summary>
        public int Groupid = 0;             //用户组
        /// <summary>
        /// VIP
        /// </summary>
        public int Vip = 0;                 //VIP
        /// <summary>
        /// 
        /// </summary>
        public string Joindate = "";
        /// <summary>
        /// 最后Tracker访问时间
        /// </summary>
        public DateTime LastTrackerUpdateTime = new DateTime(1990, 1, 1);
        /// <summary>
        /// 最后积分更新时间
        /// </summary>
        public DateTime LastCreditsUpdateTime = new DateTime(1990, 1, 1);
        /// <summary>
        /// 最后保种奖励更新时间
        /// </summary>
        public DateTime LastKeepRewardUpdateTime = new DateTime(1980, 1, 1);
    }
}