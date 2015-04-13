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
        /// 添加RSA记录
        /// </summary>
        /// <returns>数据库更改行数</returns>
        int InsertRsaRecord(int uid, string rsaxml, string rkey);
        /// <summary>
        /// 更新RSA条目
        /// </summary>
        /// <returns></returns>
        int UpdateRsaRecord(int uid, string rsaxml, string rkey);
        /// <summary>
        /// 获取RSA记录
        /// </summary>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        IDataReader GetRsaRecord(int uid);
    }
}
