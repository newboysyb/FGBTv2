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
        /// 保存种子中的列表信息到数据库bt_seedfile表
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="filename"></param>
        /// <param name="filesize"></param>
        /// <returns></returns>
        public int SaveSeedFileInfo(int seedid, string filename, decimal filesize)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NChar, 260, filename),
                                        DbHelper.MakeInParam("@filesize",(DbType)SqlDbType.Decimal, 16, filesize)
                                  };
            string sqlstring = "INSERT INTO [bt_seedfile] ([seedid],[filename],[filesize]) VALUES(@seedid,@filename,@filesize)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

        }

        /// <summary>
        /// 插入种子文件列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int InsertSeedFileList(DataTable dt)
        {
            DbParameter[] parms = { 
                                      new SqlParameter("@TVP", dt),
                                  };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtSeedListTable";

            string sqlstring = "INSERT INTO [bt_seedfile] ([seedid],[filename],[filesize]) SELECT seedid, filename, filesize FROM @TVP";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 清除对应种子id的所有文件列表信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public int DeleteSeedFileInfo(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                  };
            string sqlstring = "DELETE FROM [bt_seedfile] WHERE [seedid] = @seedid ";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获得对应SeedId的种子所包含的文件列表
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetSeedFileList(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                  };
            string sqlstring = "SELECT * FROM [bt_seedfile] WHERE [seedid] = @seedid ORDER BY [filename] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
    }
}