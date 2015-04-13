using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discuz.Entity
{
    public class LotteryInfo
    {
        public int Id = 0;
        public int Tid = 0;
        public int BaseWager = 0;
        public int WagerCount = 0;
        public string Poster = "";
        public int PosterId = 0;
        public DateTime StartTime = new DateTime(1990, 1, 1);
        public DateTime EndTime = new DateTime(1990, 1, 1);
        /// <summary>
        /// 0填写结果，1已结贴，2已删帖
        /// </summary>
        public int Ended = 0;
    }
    public class LotteryOption
    {
        public int Tid = 0;
        public int OptionId = 0;
        public string OptionName = "";
        public int WagerCount = 0;
        public int WagerUserCount = 0;
        /// <summary>
        /// 0,未填写，1胜出，2失败
        /// </summary>
        public int Win = 0;
    }
    public class LotteryWager
    {
        public int Id = 0;
        public int Tid = 0;
        public int OptionId = 0;
        public int Userid = 0;
        public string Username = "";
        public string Avatarurl = "";
        public DateTime WagerTime = new DateTime(1990, 1, 1);
        public int WagerCount = 0;
        /// <summary>
        /// 盈利值，负数为未结贴
        /// </summary>
        public decimal Win = -1M;
    }
}
