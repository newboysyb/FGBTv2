using System;
using System.Collections.Generic;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// BUAA-SSO信息类
    /// </summary>
    public class PTBuaaSSOinfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Uid = -1;
        /// <summary>
        /// 绑定的sso用户名
        /// </summary>
        public string ssoName = "";
        /// <summary>
        /// 绑定的sso UID
        /// </summary>
        public int ssoUid = -1;
        /// <summary>
        /// sso绑定状态，0，本地->SSO尚未绑定；1，SSO->本地尚未绑定；2，本地->SSO绑定成功；3，SSO->本地绑定成功；4，此绑定已经禁用，将禁止通过此方式登陆
        /// </summary>
        public int ssoStatus = -1;
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token = "";
        /// <summary>
        /// 令牌状态：1可用，2登陆标记，-1不可用
        /// </summary>
        public int TokenStatus = -1;
        /// <summary>
        /// 令牌创建时间
        /// </summary>
        public DateTime TokenDate = DateTime.Parse("2011-1-1");
        /// <summary>
        /// 令牌1
        /// </summary>
        public string Token1 = "";
    }
}
