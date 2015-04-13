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
        /// 获取CNGI联盟的用户ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        int GetCNGIUserID(string cngi_name, string cngi_school);
        /// <summary>
        /// 绑定CNGI联盟用户id
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="cngi_name"></param>
        /// <param name="cngi_school"></param>
        /// <returns></returns>
        int SetCNGIUserID(int userid, string cngi_name, string cngi_school);
        /// <summary>
        /// 判断未来花园账号是否已经被绑定
        /// </summary>
        /// <param name="username"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        int IsCNGIUser(int userid);
    }
}