using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Discuz.Data;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;
using System.Text.RegularExpressions;
//using SQLDMO;
using System.Collections.Generic;

//////////////////////////////////////////////////////////////////////////
//BT相关的SQL数据库操作

namespace Discuz.Data.SqlServer
{


    public partial class DataProvider : IDataProvider
    {
        /// <summary>
        /// 获取在线用户表中最小的userid（负数）
        /// </summary>
        /// <returns></returns>
        public int GetMinOnlneUserid()
        {
            string sqlstring = "SELECT TOP (1) [userid] FROM [dnt_online] ORDER BY [userid] ASC";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring), -1);
        }


    }
}
