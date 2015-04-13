using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Data;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
    public partial class PTCommon
    {

        /// <summary>
        /// 获取远程返回值，【【【【【这个函数应该移出tools文件】】】】】
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetRemoteResponse(string url)
        {
            string responseHTML = string.Empty;

            HttpWebRequest BBSRequest = null;
            HttpWebResponse BBSResponse = null;

            BBSRequest = (HttpWebRequest)WebRequest.Create(url);
            //CookieContainer cookieCon = new CookieContainer();
            //BBSRequest.CookieContainer = cookieCon;
            //cookieCon.Add(cookies);
            BBSRequest.Accept = "*/*";
            BBSRequest.AllowAutoRedirect = false;
            BBSRequest.Headers.Add("Accept-Language", "zh-cn");
            BBSRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            BBSRequest.KeepAlive = true;
            BBSRequest.Timeout = 15000;
            BBSRequest.Method = "GET";
            BBSRequest.UserAgent = "User-Agent: Mozilla/4.0 (compatible;FGBT SERVER 2.1; MSIE 7.0; windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322)";

            //返回HTML
            Stream dataStream = null;
            StreamReader reader = null;
            try
            {
                BBSResponse = (HttpWebResponse)BBSRequest.GetResponse();
                dataStream = BBSResponse.GetResponseStream();
                reader = new StreamReader(dataStream, Encoding.GetEncoding("utf-8"));
                responseHTML = reader.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (dataStream != null)
                {
                    dataStream.Close();
                    dataStream.Dispose();
                }

                //Discuz.Forum.PTError.InsertErrorLog("GETURL ERROR 0", string.Format("ERROR-{0}, URL-{1}, ", ex.ToString(), url));
                PTLog.InsertSystemLog(Forum.PTLog.LogType.HttpReq, Forum.PTLog.LogStatus.Exception, "HTTPREQ", url + " ---EX:" + ex.ToString());
                return "ERROR_EXCEPTION";
            }
            reader.Close();
            dataStream.Close();
            reader.Dispose();
            dataStream.Dispose();

            return responseHTML;
        }
    }
}
