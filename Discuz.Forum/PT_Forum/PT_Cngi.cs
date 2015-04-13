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
    public partial class PrivateBT
    {
        /// <summary>
        /// 获取CNGI联盟的用户ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        public static int GetCNGIUserID(string cngi_name, string cngi_school)
        {
            if (cngi_name == "" || cngi_name == null || cngi_school == "" || cngi_school == null) return -1;
            else
                return DatabaseProvider.GetInstance().GetCNGIUserID(cngi_name, cngi_school);
        }
        /// <summary>
        /// 绑定CNGI联盟用户id
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="cngi_name"></param>
        /// <param name="cngi_school"></param>
        /// <returns></returns>
        public static int SetCNGIUserID(int userid, string cngi_name, string cngi_school)
        {
            if (userid < 1 || cngi_name == "" || cngi_name == null || cngi_school == "" || cngi_school == null) return -2;
            else
            {
                //不能重复绑定
                if (GetCNGIUserID(cngi_name, cngi_school) > 0) return -1;
                else
                {
                    return DatabaseProvider.GetInstance().SetCNGIUserID(userid, cngi_name, cngi_school);
                }
            }
        }
        /// <summary>
        /// 判断用户是否已经绑定CNGI
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int IsCNGIUser(int userid)
        {
            return DatabaseProvider.GetInstance().IsCNGIUser(userid);
        }
    }
}