using System.Data;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{

	/// <summary>
	/// 登录日志操作类
	/// </summary>
	public class LoginLogs
	{
        private static object lockHelper = new object();

		/// <summary>
		/// 增加错误次数并返回错误次数, 如不存在登录错误日志则建立
		/// </summary>
		/// <param name="ip">ip地址</param>
        /// <returns>int</returns>
		public static int UpdateLoginLog(string ip, bool update)
		{
            lock (lockHelper)
            {
                DataTable dt = Discuz.Data.LoginLogs.GetErrLoginRecordByIP(ip);           
                if (dt.Rows.Count > 0)
                {
                    //存在记录
                    int errcount = Utils.StrToInt(dt.Rows[0][0].ToString(), 0);
                    if ((Utils.StrDateDiffMinutes(dt.Rows[0][1].ToString(), 0) <= 15 && errcount > 0) || update)
                    {
                        //（最后错误时间为15分钟之内，且错误计数大于0），或者（需要增加计数）
                        if ((errcount >= 5) || (!update))
                        {
                            //错误数大于5，不再增加，直接返回。或者 不新增错误记录，直接返回
                            return errcount;
                        }
                        else
                        {
                            //增加错误记录，返回
                            Discuz.Data.LoginLogs.AddErrLoginCount(ip);
                            return errcount + 1;
                        }
                    }
                    else
                    {
                        //错误计数等于0，或者时间超出

                        //原程序为：大于15分钟？置1？
                        //修改后：大于15分钟，置0，但是仍然返回1，即必须输入验证码
                        //删除清空记录的函数修改为必修errcount为0才删除，否则不删除    
                        if(errcount > 0) Discuz.Data.LoginLogs.ResetErrLoginCount(ip);
                        return 1;
                    } 
                }
                else
                {
                    if (update)
                    {
                        //新增一个错误记录，原记录为0
                        try
                        {
                            Discuz.Data.LoginLogs.AddErrLoginRecord(ip);
                        }
                        catch (System.Exception ex) { ex.ToString(); }
                        return 1;
                    }
                    else
                    {
                        //仅查询，原记录为0
                        return 0;
                    }
                    
                }
            }
		}

		/// <summary>
		/// 删除指定ip地址的登录错误日志
        /// 此处需要进行安全增强：不因为正确用户名密码就删除记录，必须等到15分钟后才删除，即距离上次错误15分钟
		/// </summary>
		/// <param name="ip">ip地址</param>
        /// <returns>int</returns>
		public static int DeleteLoginLog(string ip)
		{
            return Utils.IsIP(ip.Trim()) ? Discuz.Data.LoginLogs.DeleteErrLoginRecord(ip.Trim()) : 0;	
        }
	}
}
