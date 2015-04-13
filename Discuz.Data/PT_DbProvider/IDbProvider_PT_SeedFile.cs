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
        /// 插入种子文件列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        int InsertSeedFileList(DataTable dt);
        /// <summary>
        /// 保存种子中的列表信息到数据库bt_seedfile表
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="filename"></param>
        /// <param name="filesize"></param>
        /// <returns></returns>
        int SaveSeedFileInfo(int seedid, string filename, decimal filesize);
        /// <summary>
        /// 清除对应种子id的所有文件列表信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        int DeleteSeedFileInfo(int seedid);
        /// <summary>
        /// 获得对应SeedId的种子所包含的文件列表
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetSeedFileList(int seedid);
    }
}