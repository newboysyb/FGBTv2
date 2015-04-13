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
        /// 获得种子操作记录数
        /// </summary>
        /// <returns></returns>
        int GetSeedOPCount(int OperatorId, int OperatType, int SeedType, int userid);
        /// <summary>
        /// 获得种子操作记录表
        /// </summary>
        /// <returns></returns>
        DataTable GetSeedOPList(int OperatorId, int OperatType, int SeedType, int userid, int numperpage, int pageindex);
        /// <summary>
        /// 插入种子操作记录
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="op"></param>
        /// <param name="opr"></param>
        /// <returns></returns>
        int InsertSeedModLog(int seedid, string op, string opr, string operatreason, int operatorid, int operattype, int operatvalue);
        /// <summary>
        /// 获得种子操作记录
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetSeedModLog(int seedid);
    }
}