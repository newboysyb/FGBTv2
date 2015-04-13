using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
    public class PTPrivateMessage
    {
        /// <summary>
        /// 标记指定用户的短信息状态，0已读，1未读，2草稿
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要标记已读的短信息列表(数组)</param>
        /// <returns>标记已读记录数</returns>
        public static int SetPrivateMessagesState(int userId, string[] pmIdList, int state)
        {
            if (userId < 1) return -2;
            if (!Utils.IsNumericArray(pmIdList))
                return -1;

            //将字符串数组转换为整数datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("IntValue", typeof(int), "");
            foreach (string str in pmIdList)
            {
                DataRow dr = dt.NewRow();
                dr["IntValue"] = TypeConverter.StrToInt(str, -1);
                dt.Rows.Add(dr);
            }

            int reval = DatabaseProvider.GetInstance().SetPrivateMessagesState(userId, dt, state);
            if (reval > 0)
                Discuz.Data.Users.SetUserNewPMCount(userId, Discuz.Data.PrivateMessages.GetNewPMCount(userId));

            return reval;
        }
        ///// <summary>
        ///// 标记指定用户的短信息为未读
        ///// </summary>
        ///// <param name="userId">用户ID</param>
        ///// <param name="pmitemid">要标记未读的短信息列表(数组)</param>
        ///// <returns>标记未读记录数</returns>
        //public static int MarkPrivateMessagesUnRead(int userId, string[] pmIdList)
        //{
        //    if (!Utils.IsNumericArray(pmIdList))
        //        return -1;

        //    //将字符串数组转换为整数datatable
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("IntValue", typeof(int), "");
        //    foreach (string str in pmIdList)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr["IntValue"] = TypeConverter.StrToInt(str, -1);
        //        dt.Rows.Add(dr);
        //    }

        //    int reval = DatabaseProvider.GetInstance().SetPrivateMessagesState(userId, dt, 1);
        //    if (reval > 0)
        //        Discuz.Data.Users.SetUserNewPMCount(userId, Discuz.Data.PrivateMessages.GetNewPMCount(userId));

        //    return reval;
        //}
        /// <summary>
        /// 标记全部用户的短信息为已读/指定状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>标记已读/指定状态记录数</returns>
        public static int SetPrivateMessagesStateAll(int userId, int state)
        {
            if (userId < 1) return -2;

            int reval = DatabaseProvider.GetInstance().SetPrivateMessagesStateAll(userId, state);
            if (reval > 0)
                Discuz.Data.Users.SetUserNewPMCount(userId, Discuz.Data.PrivateMessages.GetNewPMCount(userId));

            return reval;
        }
    }
}