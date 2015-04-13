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
    /// <summary>
    /// BUAA-SSO 单点登录实现
    /// </summary>
    public class PTBuaaSSO
    {
        public static PTBuaaSSOinfo LoadPTBuaaSSOinfo(IDataReader rd)
        {
            PTBuaaSSOinfo ssoinfo = new PTBuaaSSOinfo();

            ssoinfo.Uid = TypeConverter.ObjectToInt(rd["uid"], -1);
            ssoinfo.ssoUid = TypeConverter.ObjectToInt(rd["ssouid"], -1);
            ssoinfo.ssoName = rd["ssoname"].ToString().Trim();
            ssoinfo.ssoStatus = TypeConverter.ObjectToInt(rd["ssostatus"], -1);
            ssoinfo.Token = rd["token"].ToString().Trim();
            ssoinfo.Token1 = rd["token1"].ToString().Trim();
            ssoinfo.TokenDate = TypeConverter.ObjectToDateTime(rd["tokendate"]);
            ssoinfo.TokenStatus = TypeConverter.ObjectToInt(rd["tokenstatus"]);

            return ssoinfo;
        }
        /// <summary>
        /// 获取BUAA-SSO用户名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static PTBuaaSSOinfo GetBuaaSSOInfobyUid(int userid)
        {
            PTBuaaSSOinfo ssoinfo = new PTBuaaSSOinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetBuaaSSOInfobyUid(userid);
            if (rd.Read())
            {
                ssoinfo = LoadPTBuaaSSOinfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return ssoinfo;
        }
        /// <summary>
        /// 获取BUAA-SSO用户名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static PTBuaaSSOinfo GetBuaaSSOInfobyssoUid(int ssouid)
        {
            PTBuaaSSOinfo ssoinfo = new PTBuaaSSOinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetBuaaSSOInfobyssoUid(ssouid);
            if (rd.Read())
            {
                ssoinfo = LoadPTBuaaSSOinfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return ssoinfo;
        }
        /// <summary>
        /// 获取BUAA-SSO用户名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static PTBuaaSSOinfo GetBuaaSSOInfobyToken1(string token1)
        {
            PTBuaaSSOinfo ssoinfo = new PTBuaaSSOinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetBuaaSSOInfobyToken1(token1);
            if (rd.Read())
            {
                ssoinfo = LoadPTBuaaSSOinfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return ssoinfo;
        }
        /// <summary>
        /// 获取BUAA-SSO用户名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static PTBuaaSSOinfo GetBuaaSSOInfobyssoNameOldData(string ssoname)
        {
            PTBuaaSSOinfo ssoinfo = new PTBuaaSSOinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetBuaaSSOInfobyssoNameOldData(ssoname);
            if (rd.Read())
            {
                ssoinfo = LoadPTBuaaSSOinfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return ssoinfo;
        }
        /// <summary>
        /// 获取BUAA-SSO用户名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static PTBuaaSSOinfo GetBuaaSSOInfobyToken(string token)
        {
            PTBuaaSSOinfo ssoinfo = new PTBuaaSSOinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetBuaaSSOInfobyToken(token);
            if (rd.Read())
            {
                ssoinfo = LoadPTBuaaSSOinfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return ssoinfo;
        }
        public static int DeleteBuaaSSOInfobyUid(int uid)
        {
            return DatabaseProvider.GetInstance().DeleteBuaaSSOInfobyUid(uid);
        }
        public static int DeleteBuaaSSOInfobyssoName(string ssoname)
        {
            return DatabaseProvider.GetInstance().DeleteBuaaSSOInfobyssoName(ssoname);
        }
        /// <summary>
        /// 先删除指定uid的sso记录，再插入新的记录
        /// </summary>
        /// <param name="ssoinfo"></param>
        /// <returns></returns>
        public static int CreateBuaaSSOInfobyUid(PTBuaaSSOinfo ssoinfo)
        {
            if (ssoinfo.Uid < 2) return -1;
            if (ssoinfo.ssoName.Trim() == "") ssoinfo.ssoName = "<*>" + ssoinfo.Uid.ToString();

            DeleteBuaaSSOInfobyUid(ssoinfo.Uid);
            
            try
            {
                return DatabaseProvider.GetInstance().InsertBuaaSSOInfo(ssoinfo);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return -2;
            }
            

        }
        ///// <summary>
        ///// 先删除指定ssoname的sso记录，再插入新的记录
        ///// </summary>
        ///// <param name="ssoinfo"></param>
        ///// <returns></returns>
        //public static int CreateBuaaSSOInfobyssoName(PTBuaaSSOinfo ssoinfo)
        //{
        //    if (ssoinfo.ssoName.Trim() == "") return -1;

        //    DeleteBuaaSSOInfobyssoName(ssoinfo.ssoName);
            
        //    try
        //    {
        //        return DatabaseProvider.GetInstance().InsertBuaaSSOInfo(ssoinfo);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ex.ToString();
        //        return -2;
        //    } 
        //}
        /// <summary>
        /// 先删除指定ssouid的sso记录，再插入新的记录
        /// </summary>
        /// <param name="ssoinfo"></param>
        /// <returns></returns>
        public static int CreateBuaaSSOInfobyssoUid(PTBuaaSSOinfo ssoinfo)
        {
            if (ssoinfo.ssoUid < 1) return -1;

            try
            {
                return DatabaseProvider.GetInstance().InsertBuaaSSOInfo(ssoinfo);
            }
            catch 
            {
                return -2;
            }
        }

        ///// <summary>
        ///// 更新ssoname
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="ssoname"></param>
        ///// <returns></returns>
        //public static int UpdateBuaaSSOInfossoNamebyUid(int uid, string ssoname)
        //{
        //    if (ssoname.Trim() == "") return -1;
        //    if (uid < 2) return -1;

        //    ////删掉有相同uid的记录？此操作原因：当初设计为可双向绑定，若用户发起了BT->iHome绑定，但是没有操作完毕，再发起iHome->BT绑定时，会出现重复uid问题，需要删除
        //    ////因为现在不需要进行BT->iHome绑定，此功能可忽略
        //    //PTBuaaSSOinfo ssoinfo = GetBuaaSSOInfobyssoName(ssoname);
        //    //if (ssoinfo.Uid > 0 || (ssoinfo.ssoStatus > 0 && ssoinfo.ssoStatus != 0)) return -1;
        //    //DeleteBuaaSSOInfobyssoName(ssoname);

        //    //ssoname为唯一字段，防止冲突
        //    try
        //    {
        //        return DatabaseProvider.GetInstance().UpdateBuaaSSOInfossoNamebyUid(uid, ssoname);
        //    }
        //    catch
        //    {
        //        return -2;
        //    }
        //}
        ///// <summary>
        ///// 更新uid
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="ssoname"></param>
        ///// <returns></returns>
        //public static int UpdateBuaaSSOInfoUidbyssoName(int uid, string ssoname)
        //{
        //    if (ssoname.Trim() == "") return -1;
        //    if (uid < 2) return -1;

        //    PTBuaaSSOinfo ssoinfo = GetBuaaSSOInfobyUid(uid);
        //    if (ssoinfo.ssoName.Trim() != "" || (ssoinfo.ssoStatus > 0 && ssoinfo.ssoStatus != 1)) return -1;

        //    //删掉有相同uid的记录？
        //    DeleteBuaaSSOInfobyUid(uid);

        //    return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoUidbyssoName(uid, ssoname);
        //}
        /// <summary>
        /// 更新uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfoUidbyssoUid(int uid, int ssouid)
        {
            if (ssouid < 1) return -1;
            if (uid < 3) return -1;

            //PTBuaaSSOinfo ssoinfo = GetBuaaSSOInfobyUid(uid);
            //if (ssoinfo.ssoName.Trim() != "" || (ssoinfo.ssoStatus > 0 && ssoinfo.ssoStatus != 1)) return -1;
            ////删掉有相同uid的记录？此操作原因：当初设计为可双向绑定，若用户发起了BT->iHome绑定，但是没有操作完毕，再发起iHome->BT绑定时，会出现重复uid问题，需要删除
            ////因为现在不需要进行BT->iHome绑定，此功能可忽略
            //DeleteBuaaSSOInfobyUid(uid);
            try
            {
                return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoUidbyssoUid(uid, ssouid);
            }
            catch 
            {
                return -2;
            }
            
        }


        /// <summary>
        /// 更新ssouid，条件ssoname和uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfossoUidbyssoName(int uid, string ssoname, int ssouid)
        {
            if (uid < 3 || ssouid < 1 || Utils.StrIsNullOrEmpty(ssoname)) return -1;
            try
            {
                return DatabaseProvider.GetInstance().UpdateBuaaSSOInfossoUidbyssoName(uid, ssoname, ssouid);
            }
            catch
            {
                return -2;
            }
        }

        /// <summary>
        /// 更新ssoname，条件ssouid和uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfossoNamebyssoUid(int uid, int ssouid, string ssoname)
        {
            if (uid < 3 || ssouid < 1 || Utils.StrIsNullOrEmpty(ssoname)) return -1;
            try
            {
                return DatabaseProvider.GetInstance().UpdateBuaaSSOInfossoNamebyssoUid(uid, ssouid, ssoname);
            }
            catch
            {
                return -2;
            }
        }

        /// <summary>
        /// 更新token和tokendate，条件uid，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfoTokenbyUid(int uid, string token)
        {
            if (token.Trim().Length != 64) return -1;
            if (uid < 2) return -1;

            return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoTokenbyUid(uid, token);
        }
        /// <summary>
        /// 更新token1，token和tokendate，条件uid，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfoToken1byUid(int uid, string token, string token1)
        {
            if (token.Trim().Length != 64) return -1;
            if (token1.Trim().Length != 64) return -1;
            if (uid < 2) return -1;

            return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoToken1byUid(uid, token, token1);
        }

        ///// <summary>
        ///// 更新token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        ///// </summary>
        ///// <param name="ssoname"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public static int UpdateBuaaSSOInfoTokenbyssoName(string ssoname, string token)
        //{
        //    if (token.Trim().Length != 64) return -1;
        //    if (ssoname.Trim() == "") return -1;

        //    return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoTokenbyssoName(ssoname, token);
        //}
        /// <summary>
        /// 更新token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="ssoname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfoTokenbyssoUid(int ssouid, string token)
        {
            if (token.Trim().Length != 64) return -1;
            if (ssouid < 1) return -1;

            return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoTokenbyssoUid(ssouid, token);
        }

        ///// <summary>
        ///// 更新token1，token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        ///// </summary>
        ///// <param name="ssoname"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public static int UpdateBuaaSSOInfoToken1byssoName(string ssoname, string token, string token1)
        //{
        //    if (token.Trim().Length != 64) return -1;
        //    if (token1.Trim().Length != 64) return -1;
        //    if (ssoname.Trim() == "") return -1;

        //    return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoToken1byssoName(ssoname, token, token1);
        //}
        /// <summary>
        /// 更新token1，token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="ssoname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfoToken1byssoUid(int ssouid, string token, string token1)
        {
            if (token.Trim().Length != 64) return -1;
            if (token1.Trim().Length != 64) return -1;
            if (ssouid < 1) return -1;

            return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoToken1byssoUid(ssouid, token, token1);
        }

        /// <summary>
        /// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tokenstatus"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfoTokenStatusbyUid(int uid, int tokenstatus)
        {
            if (uid < 2) return -1;
            return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoTokenStatusbyUid(uid, tokenstatus);
        }
        ///// <summary>
        ///// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="tokenstatus"></param>
        ///// <returns></returns>
        //public static int UpdateBuaaSSOInfoTokenStatusbyssoName(string ssoname, int tokenstatus)
        //{
        //    if (ssoname.Trim() == "") return -1;
        //    return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoTokenStatusbyssoName(ssoname, tokenstatus);
        //}
        /// <summary>
        /// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tokenstatus"></param>
        /// <returns></returns>
        public static int UpdateBuaaSSOInfoTokenStatusbyssoUid(int ssouid, int tokenstatus)
        {
            if (ssouid < 1) return -1;
            return DatabaseProvider.GetInstance().UpdateBuaaSSOInfoTokenStatusbyssoUid(ssouid, tokenstatus);
        }




    }
}