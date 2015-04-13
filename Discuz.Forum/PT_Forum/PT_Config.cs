using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;
using Discuz.Cache;

namespace Discuz.Forum
{
    public class PrivateBTConfig
    {
        /// <summary>
        /// 读取BT配置信息
        /// </summary>
        /// <returns></returns>
        public static PrivateBTConfigInfo GetPrivateBTConfig()
        {
           
            bool cacheneedupdate = false;
            PrivateBTConfigInfo btconfig = new PrivateBTConfigInfo();

            btconfig = DNTCache.GetCacheService().RetrieveObject("/PTConfig/ConfigInfo") as PrivateBTConfigInfo;

            if (btconfig != null)
            {
                if((DateTime.Now - btconfig.LastConfigReadTime).TotalMinutes > 70) cacheneedupdate = true;
                else
                {
                    return btconfig;
                }
            }
            if (btconfig == null || cacheneedupdate)
            {
                btconfig = new PrivateBTConfigInfo();
                IDataReader reader = DatabaseProvider.GetInstance().GetPrivateBTConfigToReader();

                if (reader.Read())
                {
                    btconfig.AllowFreeRegister = bool.Parse(reader["allowfreeregister"].ToString());
                    btconfig.AllowInviteRegister = bool.Parse(reader["allowinviteregister"].ToString());
                    btconfig.DownloadCount = int.Parse(reader["downloadcount"].ToString());
                    btconfig.DownloadMulti = float.Parse(reader["downloadmulti"].ToString());
                    btconfig.DownMultiBeginTime = DateTime.Parse(reader["downmultibegintime"].ToString());
                    btconfig.DownMultiEndTime = DateTime.Parse(reader["downmultiendtime"].ToString());
                    btconfig.FreeRegBeginTime = DateTime.Parse(reader["freeregbegintime"].ToString());
                    btconfig.FreeRegEndTime = DateTime.Parse(reader["freeregendtime"].ToString());
                    btconfig.FreeRegLimit = int.Parse(reader["freereglimit"].ToString());
                    btconfig.InvitePrice = decimal.Parse(reader["inviteprice"].ToString());
                    btconfig.LimitedUserCount = int.Parse(reader["limitedusercount"].ToString());
                    btconfig.MaxUploadMulti = float.Parse(reader["maxuploadmulti"].ToString());
                    btconfig.OnlineSeedCapacity = decimal.Parse(reader["onlineseedcapacity"].ToString());
                    btconfig.OnlineSeedCount = int.Parse(reader["onlineseedcount"].ToString());
                    btconfig.RealTraffic = decimal.Parse(reader["realtraffic"].ToString());
                    btconfig.SeedCapacity = decimal.Parse(reader["seedcapacity"].ToString());
                    btconfig.SeedCount = int.Parse(reader["seedcount"].ToString());
                    btconfig.SeederCount = int.Parse(reader["seedercount"].ToString());
                    btconfig.TotalDownload = decimal.Parse(reader["totaldownload"].ToString());
                    btconfig.TotalUpload = decimal.Parse(reader["totalupload"].ToString());
                    btconfig.TotalUserCount = int.Parse(reader["totalusers"].ToString());
                    btconfig.TotalUserLimit = int.Parse(reader["totaluserlimit"].ToString());
                    btconfig.UploadMulti = float.Parse(reader["uploadmulti"].ToString());
                    btconfig.UpMultiBeginTime = DateTime.Parse(reader["upmultibegintime"].ToString());
                    btconfig.UpMultiEndTime = DateTime.Parse(reader["upmultiendtime"].ToString());
                    btconfig.DebugUser = reader["debuguser"].ToString().Trim();
                    reader.Close();

                    btconfig.LastConfigReadTime = DateTime.Now;
                    DNTCache.GetCacheService().RemoveObject("/PTConfig/ConfigInfo");
                    DNTCache.GetCacheService().AddObject("/PTConfig/ConfigInfo", btconfig, 3600);

                    return btconfig;
                }
                reader.Close();
                return null;
            }
            else return null;
        }
    }
}
