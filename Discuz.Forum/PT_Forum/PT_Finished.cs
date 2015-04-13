using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
    public partial class PrivateBT
    {
        /// <summary>
        /// 获得对应SeedId的种子的完成节点信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetUserListFinished(int seedid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetUserListFinished(seedid);
            dt.Dispose();
            return dt;
        }
        /// <summary>
        /// 判断用户是否曾经完成过此种子
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static bool IsUserHaveFinished(int uid, int seedid)
        {
            return DatabaseProvider.GetInstance().IsUserHaveFinished(uid, seedid);
        }
        /// <summary>
        /// 插入下载完成记录
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <returns></returns>
        public static int InsertFinished(int seedid, int userid, decimal upload, decimal download)
        {
            DatabaseProvider.GetInstance().AddSeedFinishedToday(seedid, userid);
            return DatabaseProvider.GetInstance().InsertFinished(seedid, userid, upload, download);
        }
    }
}