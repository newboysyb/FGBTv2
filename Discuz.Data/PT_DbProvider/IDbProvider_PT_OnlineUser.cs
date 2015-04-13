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
        /// 获取在线用户表中最小的userid（负数）
        /// </summary>
        /// <returns></returns>
        int GetMinOnlneUserid();
    }
}
