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
        /// 更新ssouid，条件ssoname和uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfossoUidbyssoName(int uid, string ssoname, int ssouid);

        /// <summary>
        /// 更新ssoname，条件ssouid和uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfossoNamebyssoUid(int uid, int ssouid, string ssoname);
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IDataReader GetBuaaSSOInfobyUid(int uid);
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IDataReader GetBuaaSSOInfobyssoUid(int ssoUid);
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IDataReader GetBuaaSSOInfobyssoNameOldData(string ssoname);
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IDataReader GetBuaaSSOInfobyToken(string token);
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IDataReader GetBuaaSSOInfobyToken1(string token1);
        /// <summary>
        /// 插入sso记录
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        int InsertBuaaSSOInfo(PTBuaaSSOinfo ssoinfo);
        /// <summary>
        /// 删除指定uid对应的sso记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        int DeleteBuaaSSOInfobyUid(int uid);
        /// <summary>
        /// 删除指定ssoname对应的sso记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        int DeleteBuaaSSOInfobyssoName(string ssoname);
        /// <summary>
        /// 更新ssoname，条件uid和ssostatus=0，更新后ssostatus=2
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfossoNamebyUid(int uid, string ssoname);
        ///// <summary>
        ///// 更新uid，条件ssoname和ssostatus=1，更新后ssostatus=3
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="ssoname"></param>
        ///// <returns></returns>
        //int UpdateBuaaSSOInfoUidbyssoName(int uid, string ssoname);
        /// <summary>
        /// 更新uid，条件ssoname和ssostatus=1，更新后ssostatus=3
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfoUidbyssoUid(int uid, int ssouid);
        /// <summary>
        /// 更新ssostatus，条件uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfossoStatusbyUid(int uid, int ssostatus);
        /// <summary>
        /// 更新token和tokendate，条件uid，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfoTokenbyUid(int uid, string token);

        /// <summary>
        /// 更新token1，token和tokendate，条件uid，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfoToken1byUid(int uid, string token, string token1);
        ///// <summary>
        ///// 更新token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        ///// </summary>
        ///// <param name="ssoname"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //int UpdateBuaaSSOInfoTokenbyssoName(string ssoname, string token);
        /// <summary>
        /// 更新token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="ssoname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfoTokenbyssoUid(int ssouid, string token);
        ///// <summary>
        ///// 更新token1，token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        ///// </summary>
        ///// <param name="ssoname"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //int UpdateBuaaSSOInfoToken1byssoName(string ssoname, string token, string token1);
        /// <summary>
        /// 更新token1，token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="ssoname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfoToken1byssoUid(int ssouid, string token, string token1);
        /// <summary>
        /// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tokenstatus"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfoTokenStatusbyUid(int uid, int tokenstatus);
        ///// <summary>
        ///// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="tokenstatus"></param>
        ///// <returns></returns>
        //int UpdateBuaaSSOInfoTokenStatusbyssoName(string ssoname, int tokenstatus);
        /// <summary>
        /// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tokenstatus"></param>
        /// <returns></returns>
        int UpdateBuaaSSOInfoTokenStatusbyssoUid(int ssouid, int tokenstatus);
    }
}