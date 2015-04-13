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
        /// 读取BT配置信息
        /// </summary>
        /// <returns></returns>
        public IDataReader GetPrivateBTConfigToReader()
        {
            //string sqlstring = string.Format("SELECT TOP 1 [bt_config].[allmsg],[bt_config].[allmsgtitle],[bt_config].[allowfreeregister],[bt_config].[allowinviteregister],[bt_config].[freereglimit]" +
            //                                ",[bt_config].[totaluserlimit],[bt_config].[freeregbegintime],[bt_config].[freeregendtime],[bt_config].[downloadmulti],[bt_config].[downmultibegintime]" +
            //                                ",[bt_config].[downmultiendtime],[bt_config].[uploadmulti],[bt_config].[upmultibegintime],[bt_config].[upmultiendtime],[bt_config].[maxuploadmulti]" +
            //                                ",[bt_config].[totalusercount],[bt_config].[limitedusercount],[bt_config].[totalupload],[bt_config].[totaldownload],[bt_config].[realtraffic],[bt_config].[seedcount]" +
            //                                ",[bt_config].[seedcapacity],[bt_config].[onlineseedcount],[bt_config].[onlineseedcapacity],[bt_config].[seedercount],[bt_config].[inviteprice],[bt_config].[downloadcount]" +
            //                                ",[bt_config].[uttest], [{0}statistics].[totalusers] FROM [bt_config], [{0}statistics]", BaseConfigs.GetTablePrefix);
            //return DbHelper.ExecuteReader(CommandType.Text, sqlstring);

            return DbHelper.ExecuteReader(CommandType.StoredProcedure, "bt_config_getptconfig");
        }
        public int uttest(string qustring)
        {
            DbParameter parm = DbHelper.MakeInParam("@uttest", (DbType)SqlDbType.NText, qustring.Length, qustring);
            return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [bt_config] SET [uttest] = @uttest", parm);

        }
    }
}