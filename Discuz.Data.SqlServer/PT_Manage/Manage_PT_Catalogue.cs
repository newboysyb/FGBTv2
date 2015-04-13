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
        //Insert

        /// <summary>
        /// 插入唯一标识名称
        /// </summary>
        /// <returns></returns>
        public int CatInsertUniqueName(int type, string uniqueidentity, string uniquename, string othername, string description)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, type),
                                        DbHelper.MakeInParam("@uniqueidentity",(DbType)SqlDbType.NChar, 50, uniqueidentity),
                                        DbHelper.MakeInParam("@uniquename",(DbType)SqlDbType.NVarChar, 50, uniquename),
                                        DbHelper.MakeInParam("@othername",(DbType)SqlDbType.NVarChar, 200, othername),
                                        DbHelper.MakeInParam("@description",(DbType)SqlDbType.NVarChar, 200, description),
                                  };
            string sqlstring = "INSERT INTO [bt_cat_uniquename] ([type],[uniqueidentity],[uniquename],[othername],[description]) ";
            sqlstring += "VALUES(@type,@uniqueidentity,@uniquename,@othername,@description);SELECT SCOPE_IDENTITY()";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        /// <summary>
        /// 插入唯一标识-种子对应表
        /// </summary>
        /// <returns></returns>
        public int CatInsertUniqueNameLink(int uniqueid, int seedid, short prefer, string version, string information)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@uniqueid",(DbType)SqlDbType.Int, 4, uniqueid),
                                      DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                      DbHelper.MakeInParam("@prefer",(DbType)SqlDbType.TinyInt, 4, prefer),
                                        DbHelper.MakeInParam("@version",(DbType)SqlDbType.NChar, 20, version),
                                        DbHelper.MakeInParam("@information",(DbType)SqlDbType.NVarChar, 200, information),
                                  };
            string sqlstring = "INSERT INTO [bt_cat_uniquenamelink] ([uniqueid],[seedid],[prefer],[version],[information]) ";
            sqlstring += "VALUES(@uniqueid,@seedid,@prefer,@version,@information)";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        //////////////////////////////////////////////////////////////////////////
        //Update

        /// <summary>
        /// 更新唯一标识名称（以uniqueid和type为依据，实际uniqueid为唯一）
        /// </summary>
        /// <returns></returns>
        public int CatUpdateUniqueName(int uniqueid, int type, string uniqueidentity, string uniquename, string othername, string description)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@uniqueid",(DbType)SqlDbType.Int, 4, uniqueid),
                                      DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, type),
                                      
                                        DbHelper.MakeInParam("@uniqueidentity",(DbType)SqlDbType.NChar, 50, uniqueidentity),
                                        DbHelper.MakeInParam("@uniquename",(DbType)SqlDbType.NVarChar, 50, uniquename),
                                        DbHelper.MakeInParam("@othername",(DbType)SqlDbType.NVarChar, 200, othername),
                                        DbHelper.MakeInParam("@description",(DbType)SqlDbType.NVarChar, 200, description),
                                  };
            string sqlstring = "UPDATE [bt_cat_uniquename] SET  [uniqueidentity] = @uniqueidentity, [uniquename] = @uniquename, [othername] = @othername, [description] = @description";
            sqlstring += " WHERE [uniqueid] = @uniqueid AND [type] = @type";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        /// <summary>
        /// 更新唯一标识-种子对应表（以seedid为依据）
        /// </summary>
        /// <returns></returns>
        public int CatUpdateUniqueNameLink(int uniqueid, int seedid, short prefer, string version, string information)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                      DbHelper.MakeInParam("@uniqueid",(DbType)SqlDbType.Int, 4, uniqueid),
                                      DbHelper.MakeInParam("@prefer",(DbType)SqlDbType.TinyInt, 4, prefer),
                                        DbHelper.MakeInParam("@version",(DbType)SqlDbType.NChar, 20, version),
                                        DbHelper.MakeInParam("@information",(DbType)SqlDbType.NVarChar, 200, information),
                                  };
            string sqlstring = "UPDATE [bt_cat_uniquenamelink] SET [uniqueid] = @uniqueid, [prefer] = @prefer, [version] = @version, [information] = @information";
            sqlstring += " WHERE [seedid] = @seedid";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        //////////////////////////////////////////////////////////////////////////
        //delete
        /// <summary>
        /// 删除唯一标识名称（以uniqueid/type和uniqueidentity为依据，实际uniqueid为唯一）
        /// </summary>
        /// <returns></returns>
        public int CatDeleteUniqueName(int uniqueid, int type, string uniqueidentity)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@uniqueid",(DbType)SqlDbType.Int, 4, uniqueid),
                                      DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, type),
                                      DbHelper.MakeInParam("@uniqueidentity",(DbType)SqlDbType.NChar, 50, uniqueidentity),
                                  };
            string sqlstring = "DELETE FROM [bt_cat_uniquename] WHERE [uniqueid] = @uniqueid AND [type] = @type AND [uniqueidentity] = @uniqueidentity";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        /// <summary>
        /// 删除唯一标识-种子对应表（以uniqueid为依据）
        /// </summary>
        /// <returns></returns>
        public int CatDeleteUniqueNameLinkbyUniqueid(int uniqueid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@uniqueid",(DbType)SqlDbType.Int, 4, uniqueid),
                                  };
            string sqlstring = "DELETE FROM [bt_cat_uniquenamelink] WHERE [uniqueid] = @uniqueid";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }
        /// <summary>
        /// 删除唯一标识-种子对应表（以seedid为依据）
        /// </summary>
        /// <returns></returns>
        public int CatDeleteUniqueNameLinkbySeedid(int seedid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                  };
            string sqlstring = "DELETE FROM [bt_cat_uniquenamelink] WHERE [seedid] = @seedid";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        //////////////////////////////////////////////////////////////////////////
        //Get

        /// <summary>
        /// 获取当前UniqueNameLink表中最大的seedid
        /// </summary>
        /// <returns></returns>
        public int GetMaxSeedidInUniqueNameLink()
        {
            string sqlstring = "SELECT TOP(1) [seedid] FROM [bt_cat_uniquenamelink] ORDER BY [seedid] DESC";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring), -1);
        }


    }
}
