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
        /// 插入种子文件列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int InsertSeedFileList(DataTable dt)
        {
            if (dt.Rows.Count > 0)
                return DatabaseProvider.GetInstance().InsertSeedFileList(dt);
            else
                return -1;
        }

        /// <summary>
        /// 清除对应种子id的所有文件列表信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static bool DeleteSeedFileInfo(int seedid)
        {
            if (DatabaseProvider.GetInstance().DeleteSeedFileInfo(seedid) > 0) return true;
            else return false;
        }
        /// <summary>
        /// 获得对应SeedId的种子所包含的文件列表
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetSeedFileList(int seedid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetSeedFileList(seedid);
            dt.Columns.Add("size");
            dt.Columns.Add("filetype");
            foreach (DataRow dr in dt.Rows)
            {
                dr["size"] = PTTools.Upload2Str(decimal.Parse(dr["filesize"].ToString()), false);
                string filename = dr["filename"].ToString().Trim();
                int lastdot = filename.LastIndexOf(".");
                if (lastdot > 0 && lastdot < filename.Length - 1 && lastdot > (filename.Length - 8))
                {
                    //dr["filetype"] = filename.Length.ToString() + "--" + lastdot.ToString();
                    dr["filetype"] = filename.Substring(lastdot + 1, filename.Length - lastdot - 1).ToUpper();
                }
                else dr["filetype"] = "";
            }
            dt.Dispose();
            return dt;
        }
        /// <summary>
        /// 保存种子中的列表信息到数据库bt_seedfile表
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="filename"></param>
        /// <param name="filesize"></param>
        /// <returns></returns>
        public static bool SaveSeedFileInfo(int seedid, string filename, decimal filesize)
        {
            if (seedid < 1 || filename.Length > 260) return false; //种子ID和文件名长度校验（Windows下不超过260）
            else if (DatabaseProvider.GetInstance().SaveSeedFileInfo(seedid, filename, filesize) > 0) return true;
            else return false;
        }
    }
}