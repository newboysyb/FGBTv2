using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum.PT_Forum
{
    class PTCatalogue
    {
        /// <summary>
        /// 更新电影tt信息进UniqueNameLink表，每次更新
        /// </summary>
        public void ScheduleTaskUpdate()
        {
            //获取当前最大种子id
            int currid = DatabaseProvider.GetInstance().GetMaxSeedidInUniqueNameLink();
            int addcount = 0, errcount = 0;
            for (int i = 0; i < 5000; i++)
            {
                currid++;
                
                //获取种子信息，包括所有状态的种子
                PTSeedinfo seedinfo = PTSeeds.GetSeedInfo(currid );
                
                //已经到末尾
                if (seedinfo.SeedId < 1) errcount++;
                if (errcount > 10) return;

                //只处理状态为2/3/4的种子
                if (seedinfo.Status < 2 || seedinfo.Status > 4) continue;

                if (seedinfo.Type == 1)
                {
                    //获取imdb编号

                    //获取英文名和年份

                    //检查imdb编号是否已经存在，不存在则插入新记录

                    //插入link

                    addcount++;

                    if (addcount > 100) break;
                }
                
            }
            
        }
    }
}
