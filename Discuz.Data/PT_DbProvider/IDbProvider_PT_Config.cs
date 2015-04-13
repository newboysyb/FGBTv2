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
        /// 读取BT配置信息
        /// </summary>
        /// <returns></returns>
        IDataReader GetPrivateBTConfigToReader();
        /// <summary>
        /// UT测试
        /// </summary>
        /// <param name="qustring"></param>
        /// <returns></returns>
        int uttest(string qustring);//UT测试
    }
}