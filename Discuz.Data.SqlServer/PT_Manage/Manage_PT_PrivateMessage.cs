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
        /// 标记指定用户的短信息为已读/指定状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要标记已读/指定状态的短信息列表(数组)</param>
        /// <returns>标记已读/指定状态记录数</returns>
        public int SetPrivateMessagesState(int userId, DataTable pmIdList, int state)
        {
            DbParameter[] parms = {
                                       new SqlParameter("@TVP", pmIdList),
									   DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int,4, userId),
                                       DbHelper.MakeInParam("@state", (DbType)SqlDbType.Int,4, state)
			                      };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";
            string commandText = string.Format("UPDATE [{0}pms] SET [new] = @state WHERE [pmid] IN (SELECT [IntValue] FROM @TVP) AND (([msgtoid] = @userid AND [folder] = 0) OR ([msgfromid] = @userid AND [folder] = 1) OR ([msgfromid] = @userid AND [folder] = 2))",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        ///// <summary>
        ///// 标记指定用户的短信息为未读
        ///// </summary>
        ///// <param name="userId">用户ID</param>
        ///// <param name="pmitemid">要标记未读的短信息列表(数组)</param>
        ///// <returns>标记未读记录数</returns>
        //public int MarkPrivateMessagesUnRead(int userId, DataTable pmIdList)
        //{
        //    DbParameter[] parms = {
        //                               new SqlParameter("@TVP", pmIdList),
        //                               DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int,4, userId)
        //                          };
        //    ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
        //    ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";
        //    string commandText = string.Format("UPDATE [{0}pms] SET [new] = 1 WHERE [pmid] IN (SELECT [IntValue] FROM @TVP) AND (([msgtoid] = @userid AND [folder] = 0) OR ([msgfromid] = @userid AND [folder] = 1) OR ([msgfromid] = @userid AND [folder] = 2))",
        //                                        BaseConfigs.GetTablePrefix,
        //                                        pmIdList);
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        //}
        /// <summary>
        /// 标记全部用户的短信息为已读/指定状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>标记已读/指定状态记录数</returns>
        public int SetPrivateMessagesStateAll(int userId,  int state)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int,4, userId),
                                       DbHelper.MakeInParam("@state", (DbType)SqlDbType.Int,4, state)
			                      };
            string commandText = "";
            if (state == 0)
            {
                commandText = string.Format("UPDATE [{0}pms] SET [new] = @state WHERE [new] = 1 AND (([msgtoid] = @userid AND [folder] = 0) OR ([msgfromid] = @userid AND [folder] = 1) OR ([msgfromid] = @userid AND [folder] = 2))",
                                    BaseConfigs.GetTablePrefix);
            }
            else
            {
                commandText = string.Format("UPDATE [{0}pms] SET [new] = @state WHERE [new] = 0 AND (([msgtoid] = @userid AND [folder] = 0) OR ([msgfromid] = @userid AND [folder] = 1) OR ([msgfromid] = @userid AND [folder] = 2))",
                    BaseConfigs.GetTablePrefix);
            }
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
    }
}
