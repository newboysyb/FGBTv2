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

        int InsertLotteryInfo(LotteryInfo lottery);

        IDataReader GetLotteryInfo(int tid);


        int UpdateLotteryInfo(int tid, int basewager, DateTime endtime, int ended);

        int UpdateLotteryInfoSumCount(int tid, int optionid);

        //////////////////////////////////////////////////////////////////////////



        int InsertLotteryOption(LotteryOption lotteryoption);
            

        DataTable GetLotteryOptionList(int tid);


        int UpdateLotteryOption(int tid, int optionid, int win);


        int DeleteLotteryOption(int tid);




        //////////////////////////////////////////////////////////////////////////
        


        int InsertLotteryWager(LotteryWager lotterywager);



        DataTable GetLotteryWagerList(int tid, int optionid);


        DataTable GetLotteryWagerListbyUid(int tid, int userid);


        int UpdateLotteryWager(int id, decimal win);


        //////////////////////////////////////////////////////////////////////////


        int InsertLotteryLog(int tid, int uid, string action, string message);



    }
}
