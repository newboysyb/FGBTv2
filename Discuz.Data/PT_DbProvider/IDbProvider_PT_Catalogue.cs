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
        /// 插入唯一标识名称
        /// </summary>
        /// <returns></returns>
        int CatInsertUniqueName(int type, string uniqueidentity, string uniquename, string othername, string description);
        /// <summary>
        /// 插入唯一标识-种子对应表
        /// </summary>
        /// <returns></returns>
        int CatInsertUniqueNameLink(int uniqueid, int seedid, short prefer, string version, string information);
       
        //////////////////////////////////////////////////////////////////////////
        //Update

        /// <summary>
        /// 更新唯一标识名称（以uniqueid和type为依据，实际uniqueid为唯一）
        /// </summary>
        /// <returns></returns>
        int CatUpdateUniqueName(int uniqueid, int type, string uniqueidentity, string uniquename, string othername, string description);
        /// <summary>
        /// 更新唯一标识-种子对应表（以seedid为依据）
        /// </summary>
        /// <returns></returns>
        int CatUpdateUniqueNameLink(int uniqueid, int seedid, short prefer, string version, string information);
        //////////////////////////////////////////////////////////////////////////
        //delete
        /// <summary>
        /// 删除唯一标识名称（以uniqueid/type和uniqueidentity为依据，实际uniqueid为唯一）
        /// </summary>
        /// <returns></returns>
        int CatDeleteUniqueName(int uniqueid, int type, string uniqueidentity);
        /// <summary>
        /// 删除唯一标识-种子对应表（以uniqueid为依据）
        /// </summary>
        /// <returns></returns>
        int CatDeleteUniqueNameLinkbyUniqueid(int uniqueid);
        /// <summary>
        /// 删除唯一标识-种子对应表（以seedid为依据）
        /// </summary>
        /// <returns></returns>
        int CatDeleteUniqueNameLinkbySeedid(int seedid);
        
        //////////////////////////////////////////////////////////////////////////
        //Get
        /// <summary>
        /// 获取当前UniqueNameLink表中最大的seedid
        /// </summary>
        /// <returns></returns>
        int GetMaxSeedidInUniqueNameLink();
    }
}
