using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
    public partial class PTLottery
    {

        /// <summary>
        /// 返还庄家的比例
        /// </summary>
        public static float BANKER_RETURN_RATIO = 0.10f;
        
        /// <summary>
        /// 评分给投注者的比例
        /// </summary>
        public static float WAGER_RETURN_RATIO = 0.65f;


                /// <summary>
        /// 创建博彩
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <param name="options"></param>
        /// <param name="basewager"></param>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <param name="mins"></param>
        /// <returns></returns>
        public static string CreateLottery(int tid, int uid, string username, string optionsitem, int basewager, string date, int hour, int mins)
        {
            return CreateLottery(tid, uid, username, optionsitem, basewager, date, hour, mins, false);
        }
        /// <summary>
        /// 创建博彩
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <param name="options"></param>
        /// <param name="basewager"></param>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <param name="mins"></param>
        /// <returns></returns>
        public static string CreateLottery(int tid, int uid, string username, string optionsitem, int basewager, string date, int hour, int mins, bool optiononly)
        {
            //校验
            if (tid < 0 || uid < 0 || optionsitem.Length < 1 || basewager < 1 || hour < 0 || mins < 0) return "格式错误";
            DateTime endtime = TypeConverter.StrToDateTime(date, new DateTime(1990, 1, 1));
            endtime = endtime.AddHours(hour).AddMinutes(mins);
            if ((endtime - DateTime.Now).TotalHours < 2.9) return "截止时间距离现在太短（必须大于3小时）";
            

            string msg = "";

            string[] itemname = Utils.SplitString(Utils.HtmlEncode(optionsitem), "\r\n");

            int i = 0;
            foreach (string item in itemname)
            {
                if (item.Trim().Length == 0 || InsertLotteryOption(tid, i++, item) < 1)
                {
                    msg = "创建博彩项失败";
                    break;
                }
                else
                {
                    InsertLotteryLog(tid, uid, "INSERT OPTION", "增加项：" + item);
                }
            }

            if (msg != "") return msg;


            if (!optiononly && InsertLotteryInfo(tid, basewager, username, uid, endtime) < 1)
            {
                InsertLotteryLog(tid, uid, "INSERT LOTTERY", "创建博彩失败：" + username + " --BASE:" + basewager + " --END:" + endtime);
                DeleteLotteryOption(tid);
                return "创建博彩失败，请联系管理员";
            }

            if (optiononly) InsertLotteryLog(tid, uid, "EDIT LOTTERY", "更新博彩：" + username + " --BASE:" + basewager + " --END:" + endtime);
            else InsertLotteryLog(tid, uid, "INSERT LOTTERY", "创建博彩：" + username + " --BASE:" + basewager + " --END:" + endtime);

            return msg;
        }


        public static int InsertLotteryInfo(int tid, int basewager, string poster, int posterid, DateTime endtime)
        {
            LotteryInfo lottery = new LotteryInfo();

            lottery.Tid = tid;
            lottery.BaseWager = basewager;
            lottery.Poster = poster;
            lottery.PosterId = posterid;
            lottery.StartTime = DateTime.Now.AddHours(1);
            lottery.EndTime = endtime;
            lottery.Ended = 0;

            //最低3小时投注时间
            if ((lottery.EndTime - lottery.StartTime).TotalHours < 1) return -2;

            return DatabaseProvider.GetInstance().InsertLotteryInfo(lottery);
        }

        public static LotteryInfo GetLotteryInfo(int tid)
        {
            LotteryInfo lottery = new LotteryInfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetLotteryInfo(tid);
            if (rd.Read())
            {
                lottery = LoadSingleLotteryInfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return lottery;
        }

        private static LotteryInfo LoadSingleLotteryInfo(IDataReader rd)
        {
            LotteryInfo lottery = new LotteryInfo();

            lottery.Id = TypeConverter.ObjectToInt(rd["id"]);
            lottery.Tid = TypeConverter.ObjectToInt(rd["tid"]);
            lottery.BaseWager = TypeConverter.ObjectToInt(rd["basewager"]);
            lottery.Poster = rd["poster"].ToString().Trim();
            lottery.PosterId = TypeConverter.ObjectToInt(rd["posterid"]);
            lottery.StartTime = TypeConverter.ObjectToDateTime(rd["starttime"]);
            lottery.EndTime = TypeConverter.ObjectToDateTime(rd["endtime"]);
            lottery.WagerCount = TypeConverter.ObjectToInt(rd["wagercount"]);
            lottery.Ended = TypeConverter.ObjectToInt(rd["ended"]);

            return lottery;
        }

        public static int UpdateLotteryInfo(int tid, int basewager, DateTime endtime, int ended)
        {
            return DatabaseProvider.GetInstance().UpdateLotteryInfo(tid, basewager, endtime, ended);
        }

        public static int UpdateLotteryInfoSumCount(int tid, int optionid)
        {
            return DatabaseProvider.GetInstance().UpdateLotteryInfoSumCount(tid, optionid);
        }



        //////////////////////////////////////////////////////////////////////////



        public static int InsertLotteryOption(int tid, int optionid, string optionname)
        {
            LotteryOption lotteryoption = new LotteryOption();

            lotteryoption.Tid = tid;
            lotteryoption.OptionId = optionid;
            lotteryoption.OptionName = optionname;

            return DatabaseProvider.GetInstance().InsertLotteryOption(lotteryoption);
        }


        public static List<LotteryOption> GetLotteryOptionList(int tid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetLotteryOptionList(tid);
            List<LotteryOption> lotteryoptionlist = new List<LotteryOption>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    LotteryOption lotteryoption = LoadSingleLotteryOption(dr);
                    lotteryoptionlist.Add(lotteryoption);
                }
                
            }

            return lotteryoptionlist;
        }
        private static LotteryOption LoadSingleLotteryOption(DataRow dr)
        {
            LotteryOption lotteryoption = new LotteryOption();

            if (dr != null)
            {
                lotteryoption.Tid = TypeConverter.ObjectToInt(dr["tid"]);
                lotteryoption.OptionId = TypeConverter.ObjectToInt(dr["optionid"]);
                lotteryoption.OptionName = dr["optionname"].ToString().Trim();
                lotteryoption.WagerCount = TypeConverter.ObjectToInt(dr["wagercount"]);
                lotteryoption.WagerUserCount = TypeConverter.ObjectToInt(dr["wagerusercount"]);
                lotteryoption.Win = TypeConverter.ObjectToInt(dr["win"]);
            }

            return lotteryoption;
        }

        public static int UpdateLotteryOption(int tid, int optionid, int win)
        {
            return DatabaseProvider.GetInstance().UpdateLotteryOption(tid, optionid, win);
        }


        public static int DeleteLotteryOption(int tid)
        {
            return DatabaseProvider.GetInstance().DeleteLotteryOption(tid);
        }




        //////////////////////////////////////////////////////////////////////////



        public static int InsertLotteryWager(int tid, int uid, int optionid, int count)
        {
            LotteryWager lotterywager = new LotteryWager();

            lotterywager.Tid = tid;
            lotterywager.Userid = uid;
            lotterywager.OptionId = optionid;
            lotterywager.WagerCount = count;
            lotterywager.WagerTime = DateTime.Now;
            lotterywager.Win = -1M;

            return DatabaseProvider.GetInstance().InsertLotteryWager(lotterywager);
        }

        private static LotteryWager LoadSingleLotteryWager(DataRow dr)
        {
            LotteryWager lotterywager = new LotteryWager();

            lotterywager.Id = TypeConverter.ObjectToInt(dr["id"]);
            lotterywager.Tid = TypeConverter.ObjectToInt(dr["tid"]);
            lotterywager.OptionId = TypeConverter.ObjectToInt(dr["optionid"]);
            lotterywager.Userid = TypeConverter.ObjectToInt(dr["userid"]);
            lotterywager.Username = dr["username"].ToString().Trim();
            lotterywager.WagerTime = TypeConverter.ObjectToDateTime(dr["wagertime"]);
            lotterywager.WagerCount = TypeConverter.ObjectToInt(dr["wagercount"]);
            lotterywager.Win = TypeConverter.ObjectToDecimal(dr["win"]);
            lotterywager.Avatarurl = Avatars.GetAvatarUrl(lotterywager.Userid, AvatarSize.Small);

            return lotterywager;
        }

        public static List<LotteryWager> GetLotteryWagerList(int tid, int optionid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetLotteryWagerList(tid, optionid);
            List<LotteryWager> lotterywagerlist = new List<LotteryWager>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    LotteryWager lotterywager = LoadSingleLotteryWager(dr);
                    lotterywagerlist.Add(lotterywager);
                }

            }

            return lotterywagerlist;
        }


        public static List<LotteryWager> GetLotteryWagerListbyUid(int tid, int userid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetLotteryWagerListbyUid(tid, userid);
            List<LotteryWager> lotterywagerlist = new List<LotteryWager>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    LotteryWager lotterywager = LoadSingleLotteryWager(dr);
                    lotterywagerlist.Add(lotterywager);
                }

            }

            return lotterywagerlist;
        }


        public static int UpdateLotteryWager(int id, decimal win)
        {
            return DatabaseProvider.GetInstance().UpdateLotteryWager(id, win);
        }


        //////////////////////////////////////////////////////////////////////////


        public static int InsertLotteryLog(int tid, int uid, string action, string message)
        {
            return DatabaseProvider.GetInstance().InsertLotteryLog(tid, uid, action, message);
        }




    }
}
