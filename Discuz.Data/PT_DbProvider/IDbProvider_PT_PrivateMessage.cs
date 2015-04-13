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
        /// 标记指定用户的短信息为已读
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要标记已读的短信息列表(数组)</param>
        /// <returns>标记已读记录数</returns>
        int SetPrivateMessagesState(int userId, DataTable pmIdList, int status);
        ///// <summary>
        ///// 标记指定用户的短信息为未读
        ///// </summary>
        ///// <param name="userId">用户ID</param>
        ///// <param name="pmitemid">要标记未读的短信息列表(数组)</param>
        ///// <returns>标记未读记录数</returns>
        //int MarkPrivateMessagesUnRead(int userId, DataTable pmIdList);
        /// <summary>
        /// 标记全部用户的短信息为已读/指定状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>标记已读/指定状态记录数</returns>
        int SetPrivateMessagesStateAll(int userId, int state);
    }
}
