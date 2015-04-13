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
        /// 获得对应SeedId的种子的完成节点信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetUserListFinished(int seedid);
        /// <summary>
        /// 判断用户是否曾经完成过此种子
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        bool IsUserHaveFinished(int uid, int seedid);
        /// <summary>
        /// 插入下载完成记录
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <returns></returns>
        int InsertFinished(int seedid, int userid, decimal upload, decimal download);
    }
}