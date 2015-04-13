using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Data;

namespace Discuz.Common
{
    /// <summary>
    /// BT工具类：此工具类不能依赖于数据库、专有变量类型等存在
    /// </summary>
    public class PTTools
    {
        /// <summary>
        ///  过滤标签中不应该出现的字符、字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TagStringFilter(string input)
        {
            input = input.Replace(":", "").Replace("@", "").Replace("\\", "").Replace("：", "").Replace("【", "").Replace("】", "");
            input = input.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("&#160;", "").Replace(".", "").Trim();
            input = input.Replace("/color", "").Replace("&#183;", "");

            return input;
        }
        /// <summary>
        /// 判断input中是否包含value中的某一个,value可用","或"，"分割多个值，可以和其他词组相连，“.”可以自动被忽略为空格
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool StringContainsAny(string input, string value)
        {
            //将判断条件处理成正则表达式
            value = value.Trim();
            if (value.Substring(value.Length - 1) == ",") value = value.Substring(0, value.Length - 1).Trim();
            value = value.Replace("，", ",");
            value = value.Replace(".", @"[\.\s]");
            value = value.Replace(" ", @"[\.\s]");
            value = value.Replace(",", @"|");

            //正则判断
            Regex r1 = new Regex(value);
            if (r1.IsMatch(input)) return true;
            else return false;
        }

        /// <summary>
        /// 返回以浮点形式表示的当前时间，如2:10分，返回2.1667
        /// </summary>
        /// <returns></returns>
        public static double GetDateTimeNow()
        {
            double currenthour = DateTime.Now.Hour + (DateTime.Now.Minute / 60.0);
            return currenthour;
        }
        /// <summary>
        /// 返回以字符串表示的当前时间，例如 20130414144269
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeNowString()
        {
            DateTime ndt = DateTime.Now;
            return string.Format("{0}{1}{2}{3}{4}{5}",
                ndt.Year.ToString("0000").Substring(2), ndt.Month.ToString("00"), ndt.Day.ToString("00"), ndt.Hour.ToString("00"), ndt.Minute.ToString("00"), ndt.Second.ToString("00"));
        }
        /// <summary>
        /// 返回以字符串表示的当前时间，例如 20130414144269
        /// </summary>
        /// <returns></returns>
        public static string GetDateNowString()
        {
            DateTime ndt = DateTime.Now;
            return string.Format("{0}{1}{2}",
                ndt.Year.ToString("0000").Substring(2), ndt.Month.ToString("00"), ndt.Day.ToString("00"));
        }


        /// <summary>
        /// 检查用户提交的举报链接是否合法
        /// </summary>
        /// <param name="inputurl"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static int CheckReportUrlFormat(string inputurl, ref int pid)
        {
            try
            {
                Regex re1 = new Regex(@"^showtopic.aspx\?topicid=(?<tid>\d+)&postid=(?<pid>\d+)#(?<pid2>\d+)$");
                Match match1 = re1.Match(inputurl);
                if (match1.Success)
                {
                    string tidstring = match1.Groups["tid"].Value;
                    string pidstring = match1.Groups["pid"].Value;
                    string pid2string = match1.Groups["pid2"].Value;
                    if (pidstring != pid2string) return -2;

                    pid = TypeConverter.StrToInt(pidstring, -3);
                    return TypeConverter.StrToInt(tidstring, -4);
                }
                else
                {
                    return -1;
                }
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return -101;
            }
        }

        /// <summary>
        /// 获取对应字符串“1,2,3,4,5”一类的数据表，以方便进行表值参数传递
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DataTable GetIntTableFromString(string input)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IntValue", typeof(int), "");
            string[] IdList = input.Split(',');
            foreach (string str in IdList)
            {
                DataRow dr = dt.NewRow();
                dr["IntValue"] = TypeConverter.StrToInt(str, -1);
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 获取对应字符串“1,2,3,4,5”一类的数据表，以方便进行表值参数传递
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DataTable GetChar20TableFromString(string input)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Char20Value", typeof(string), "");
            string[] IdList = input.Split(',');
            foreach (string str in IdList)
            {
                DataRow dr = dt.NewRow();
                dr["Char20Value"] = str.Length > 20 ? str.Substring(0, 20) : str;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        //public static byte[] HEX2BYTE(string str)
        //{
        //    //由16进制字符转字符数组
        //    int i;
        //    string strOK = "";
        //    Regex rx = new Regex(@"[0-9a-fA-F]"); ;

        //    //去掉不是HEX的字符
        //    for (i = 0; i < str.Length; i++) if (rx.IsMatch(str[i].ToString())) strOK += str[i];
        //    //长度必须是偶数
        //    if (strOK.Length % 2 == 1) strOK = strOK.Substring(0, strOK.Length - 1);

        //    byte[] byteReturn = new byte[strOK.Length / 2];
        //    for (i = 0; i < strOK.Length; i += 2)
        //    {

        //        byteReturn[i / 2] = Convert.ToByte(Convert.ToInt16(strOK.Substring(i, 2), 16));
        //    }
        //    return byteReturn;
        //}
        /// <summary>
        /// 将HEX字符串转换为字符串BYTE数组，返回十六进制代表的字符串的BYTE数组
        /// </summary>
        /// <param name="mHex"></param>
        /// <returns></returns>
        public static byte[] HEX2BYTE(string input)
        {
            input = input.Replace(" ", "");
            if (input.Length <= 0) return new byte[input.Length / 2];
            byte[] vBytes = new byte[input.Length / 2];
            for (int i = 0; i < input.Length; i += 2)
                if (!byte.TryParse(input.Substring(i, 2), System.Globalization.NumberStyles.HexNumber, null, out vBytes[i / 2]))
                    vBytes[i / 2] = 0;
            return vBytes;
        }

        /// <summary>
        /// 删除XML文件中的非法字符
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            StringBuilder buffer = new StringBuilder(xml.Length);
            foreach (char c in xml)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }
        /// <summary>   
        /// 判断字符是否符合XML规定 Whether a given character is allowed by XML 1.0.   
        /// </summary>   
        public static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

        /// <summary>
        /// 过滤字符串中的全角空格为英文空格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string BlankFilter(string input)
        {
            return input.Replace("　", " ").Replace("", " ").Replace("", " ").Replace("", " ").Replace("", " ").Replace("", " ").Replace("", " ").Replace("", " ").Replace("", " ").Replace("", " ").Replace("", " ");
        }


        /// <summary>
        /// 将共享率转化为HTML描述
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static int Ratio2Level(int uid, double ratio, decimal upload, decimal download)
        {
            if (ratio < PTTools.GetRatio(upload, download))
            {
                ratio = PTTools.GetRatio(upload, download);
                //PTUsers.UpdateUserInfo_Ratio(uid, ratio);
            }
            if (download == 0M) return 0;
            else if (download < 20 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.4) return -2;
                else if (ratio < 0.8) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
            else if (download < 100 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.3) return -3;
                else if (ratio < 0.4) return -2;
                else if (ratio < 1) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
            else if (download < 200 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.4) return -3;
                else if (ratio < 0.5) return -2;
                else if (ratio < 1) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
            else if (download < 300 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.5) return -3;
                else if (ratio < 0.6) return -2;
                else if (ratio < 1.1) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
            else if (download < 400 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.6) return -3;
                else if (ratio < 0.7) return -2;
                else if (ratio < 1.2) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
            else if (download < 500 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.7) return -3;
                else if (ratio < 0.8) return -2;
                else if (ratio < 1.3) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
            else if (download < 750 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.8) return -3;
                else if (ratio < 0.9) return -2;
                else if (ratio < 1.4) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
            else if (download < 1024 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.9) return -3;
                else if (ratio < 1.0) return -2;
                else if (ratio < 1.5) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
            else
            {
                if (ratio < 1.0) return -3;
                else if (ratio < 1.1) return -2;
                else if (ratio < 1.6) return -1;
                else if (ratio < 3) return 0;
                else if (ratio <= 10) return 1;
                else return 2;
            }
        }

        /// <summary>
        /// 获取共享率警告描述，含新手任务考核
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="registertime"></param>
        /// <returns></returns>
        public static string GetRatioAlert(decimal upload, decimal download, DateTime registertime)
        {
            string rtvlaue = "";//GetRatioAlert(upload, download);

            if ((DateTime.Now - registertime).TotalDays < 65)
            {
                if (download < 20 * 1024M * 1024 * 1024)
                {
                    rtvlaue += string.Format("距离新手考核还有{0}天，下载量差距{1}", (int)(65 - (DateTime.Now - registertime).TotalDays), Upload2Str(20 * 1024M * 1024 * 1024 - download));
                }
                else
                {
                    rtvlaue += "";
                }
            }
            return rtvlaue;
        }


        /// <summary>
        /// 获取共享率警告描述
        /// </summary>
        /// <param name="ratio"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <returns></returns>
        public static string GetRatioAlertTitle(decimal upload, decimal download)
        {
            return GetRatioAlert(upload, download).Replace(">紧急: 请速提高共享率<", ">紧急<").Replace(">警告: 请尽快提高共享率<", ">警告<").Replace(">警告: 请尽快提高上传量<", ">警告<").Replace(">共享率过低，不能下载。请速续种上传，以免账户被封<", ">紧急<");
        }
        /// <summary>
        /// 获取共享率警告描述
        /// </summary>
        /// <param name="ratio"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <returns></returns>
        public static string GetRatioAlert(decimal upload, decimal download)
        {
            double ratio = 1;
            if (download != 0)
            {
                //ratio = (double)(upload / download);
                ratio = GetFloorDot2(GetRatio(upload, download));
            }

            //上传量小于10G
            if (upload < 10 * 1024M * 1024 * 1024)
            {
                if (ratio < 1) return "<span style=\"font-weight:bold; color:#F00\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else return "<span style=\"font-weight:bold; color:#C80\" title=\"\">警告: 请尽快提高上传量</span>";
            }

            //上传减去下载大于100G
            if (upload - download > 100 * 1024M * 1024 * 1024)
            {
                return "";
            }

            if (download == 0M) return "";
            else if (download < 20 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.4) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
            else if (download < 100 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.3) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"共享率过低，不能下载。请速续种上传，以免账户被封\">共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.4) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 1) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
            else if (download < 200 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.4) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"共享率过低，不能下载。请速续种上传，以免账户被封\">共享率过低，不能下载。请速续种上传，以免账户被封率</span>";
                else if (ratio < 0.5) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 1) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
            else if (download < 300 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.5) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"共享率过低，不能下载。请速续种上传，以免账户被封\">共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.6) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 1.1) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
            else if (download < 400 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.6) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"共享率过低，不能下载。请速续种上传，以免账户被封\">共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.7) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 1.2) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
            else if (download < 500 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.7) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"共享率过低，不能下载。请速续种上传，以免账户被封\">共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 1.3) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
            else if (download < 750 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"共享率过低，不能下载。请速续种上传，以免账户被封\">共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.9) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 1.4) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
            else if (download < 1024 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.9) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"共享率过低，不能下载。请速续种上传，以免账户被封\">共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 1.0) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 1.5) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
            else
            {
                if (ratio < 1.0) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"共享率过低，不能下载。请速续种上传，以免账户被封\">共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 1.1 && upload - download < 200 * 1024M * 1024 * 1024) return "<span style=\"font-weight:bold; color:#FF0000\" title=\"紧急: 请速提高共享率\">紧急: 请速提高共享率</span>";
                else if (ratio < 1.6) return "<span style=\"font-weight:bold; color:#C80\" title=\"警告: 请尽快提高共享率\">警告: 请尽快提高共享率</span>";
                else return "";
            }
        }
        /// <summary>
        /// 将共享率转化为HTML描述
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static string Ratio2Str(decimal upload, decimal download)
        {
            double ratio = 1;
            if (download != 0)
            {
                //ratio = (double)(upload / download);
                ratio = GetFloorDot2(GetRatio(upload, download));
            }

            //上传量小于10G
            if (upload < 10 * 1024M * 1024 * 1024)
            {
                if (ratio < 1) return "<span style=\"font-weight:bold; color:#F00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
            }

            if (download == 0M) return "<span style=\"font-weight:bold; color:#000000\">1.00</span>";
            else if (download < 20 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.3) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 100 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.3) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 0.4) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 200 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.4) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 0.5) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 300 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.5) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 0.6) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1.1) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 400 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.6) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 0.7) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1.2) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 500 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.7) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1.3) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 750 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 0.9) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1.4) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 1024 * 1024M * 1024 * 1024)
            {
                if (ratio < 0.9) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1.0) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1.5) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else
            {
                if (ratio < 1.0) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1.1 && upload - download < 200 * 1024M * 1024 * 1024) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 1.6) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
        }
        /// <summary>
        /// 将共享率转化为HTML描述
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static string Ratio2Str(decimal upload, decimal download, bool showalert)
        {
            if (!showalert) return Ratio2Str(upload, download);

            double ratio = 1;
            if (download != 0) ratio = (double)(upload / download);

            //上传量小于10G
            if (upload < 10 * 1024M * 1048576)
            {
                if (ratio < 1) return "<span style=\"font-weight:bold; color:#F00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + "</span>";
            }

            if (download == 0M) return "<span style=\"font-weight:bold; color:#000000\">1.00</span>";
            else if (download < 20 * 1024M * 1048576)
            {
                if (ratio < 0.4) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 100 * 1024M * 1048576)
            {
                if (ratio < 0.3) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.4) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 1) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 200 * 1024M * 1048576)
            {
                if (ratio < 0.4) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 共享率过低，不能下载。请速续种上传，以免账户被封率</span>";
                else if (ratio < 0.5) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 1) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 300 * 1024M * 1048576)
            {
                if (ratio < 0.5) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.6) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 1.1) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 400 * 1024M * 1048576)
            {
                if (ratio < 0.6) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.7) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 1.2) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 500 * 1024M * 1048576)
            {
                if (ratio < 0.7) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 1.3) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 750 * 1024M * 1048576)
            {
                if (ratio < 0.8) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 0.9) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 1.4) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else if (download < 1024 * 1024M * 1048576)
            {
                if (ratio < 0.9) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 1.0) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 1.5) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
            else
            {
                if (ratio < 1.0) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 共享率过低，不能下载。请速续种上传，以免账户被封</span>";
                else if (ratio < 1.1 && upload - download < 200 * 1024M * 1024 * 1024) return "<span style=\"font-weight:bold; color:#FF0000\">" + ratio.ToString("F02") + " 紧急: 请速提高共享率</span>";
                else if (ratio < 1.6) return "<span style=\"font-weight:bold; color:#C80\">" + ratio.ToString("F02") + " 警告: 请尽快提高共享率</span>";
                else if (ratio < 3) return "<span style=\"font-weight:bold; color:#000000\">" + ratio.ToString("F02") + "</span>";
                else if (ratio <= 10) return "<span style=\"font-weight:bold; color:#00AA00\">" + ratio.ToString("F02") + "</span>";
                else return "<span style=\"font-weight:bold; color:#0000AA\">" + ratio.ToString("F02") + "</span>";
            }
        }

        /// <summary>
        /// 转换上传值为带单位的描述，原始数据为字节
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public static string Upload2Str(decimal upload)
        {
            return Upload2Str(upload, false);
        }
        /// <summary>
        /// 转换上传值为带单位的描述，原始数据为字节
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public static string Upload2Str(float upload)
        {
            return Upload2Str((decimal)upload, false);
        }
        /// <summary>
        /// 转换上传值为带单位的描述，单位为字节
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="bM">单位是否为M（不起作用）</param>
        /// <returns></returns>
        public static string Upload2Str(decimal upload, bool bM)
        {
            if (upload >= 0M)
            {
                //if (bM)
                //{
                //    if (upload < 0.001m) return (upload * 1048576m).ToString("F00") + " B";
                //    else if (upload < 1m) return (upload * 1024m).ToString("F02") + " KB";
                //    else if (upload < 1000m) return (upload).ToString("F02") + " MB";
                //    else if (upload < 1024000m) return (upload / 1024m).ToString("F02") + " GB";
                //    else if (upload < 1048576000m) return (upload / 1048576m).ToString("F02") + " TB";
                //    else if (upload < 1073741824000m) return (upload / 1073741824m).ToString("F02") + " PB";
                //    else if (upload < 1099511627776000m) return (upload / 1099511627776m).ToString("F02") + " EB";
                //    else if (upload < 1125899906842624000m) return (upload / 1125899906842624m).ToString("F02") + " ZB";
                //    else if (upload < 1152921504606846976000m) return (upload / 1152921504606846976m).ToString("F02") + " YB";
                //    else if (upload < 1180591620717411303424000m) return (upload / 1180591620717411303424m).ToString("F02") + " NB";
                //    else if (upload < 1208925819614629174706176000m) return (upload / 1208925819614629174706176m).ToString("F02") + " DB";
                //    else return "超出范围";
                //}
                //else
                {
                    if (upload < 1000m) return (upload).ToString("F00") + " B";
                    else if (upload < 1024000m) return (upload / 1024m).ToString("F02") + " KB";
                    else if (upload < 1048576000m) return (upload / 1048576m).ToString("F02") + " MB";
                    else if (upload < 1073741824000m) return (upload / 1073741824m).ToString("F02") + " GB";
                    else if (upload < 1099511627776000m) return (upload / 1099511627776m).ToString("F02") + " TB";
                    else if (upload < 1125899906842624000m) return (upload / 1125899906842624m).ToString("F02") + " PB";
                    else if (upload < 1152921504606846976000m) return (upload / 1152921504606846976m).ToString("F02") + " EB";
                    else if (upload < 1180591620717411303424000m) return (upload / 1180591620717411303424m).ToString("F02") + " ZB";
                    else if (upload < 1208925819614629174706176000m) return (upload / 1208925819614629174706176m).ToString("F02") + " YB";
                    else return "超出范围";
                }
            }
            //if (bM)
            //{
            //    if (upload > -0.0009765625m) return (upload * 1048576m).ToString("F00") + " B";
            //    else if (upload > -0.9765625m) return (upload * 1024m).ToString("F02") + " KB";
            //    else if (upload > -1000m) return (upload).ToString("F02") + " MB";
            //    else if (upload > -1024000m) return (upload / 1024m).ToString("F02") + " GB";
            //    else if (upload > -1048576000m) return (upload / 1048576m).ToString("F02") + " TB";
            //    else if (upload > -1073741824000m) return (upload / 1073741824m).ToString("F02") + " PB";
            //    else if (upload > -1099511627776000m) return (upload / 1099511627776m).ToString("F02") + " EB";
            //    else if (upload > -1125899906842624000m) return (upload / 1125899906842624m).ToString("F02") + " ZB";
            //    else if (upload > -1152921504606846976000m) return (upload / 1152921504606846976m).ToString("F02") + " YB";
            //    else if (upload > -1180591620717411303424000m) return (upload / 1180591620717411303424m).ToString("F02") + " NB";
            //    else if (upload > -1208925819614629174706176000m) return (upload / 1208925819614629174706176m).ToString("F02") + " DB";
            //    else return "超出范围";
            //}
            //else
            {
                if (upload > -1000m) return (upload).ToString("F00") + " B";
                else if (upload > -1024000m) return (upload / 1024m).ToString("F02") + " KB";
                else if (upload > -1048576000m) return (upload / 1048576m).ToString("F02") + " MB";
                else if (upload > -1073741824000m) return (upload / 1073741824m).ToString("F02") + " GB";
                else if (upload > -1099511627776000m) return (upload / 1099511627776m).ToString("F02") + " TB";
                else if (upload > -1125899906842624000m) return (upload / 1125899906842624m).ToString("F02") + " PB";
                else if (upload > -1152921504606846976000m) return (upload / 1152921504606846976m).ToString("F02") + " EB";
                else if (upload > -1180591620717411303424000m) return (upload / 1180591620717411303424m).ToString("F02") + " ZB";
                else if (upload > -1208925819614629174706176000m) return (upload / 1208925819614629174706176m).ToString("F02") + " YB";
                else return "超出范围";
            }
        }
        /// <summary>
        /// 转换下载值为带单位的描述
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="bM">单位是否为M</param>
        /// <returns></returns>
        public static string Download2Str(decimal download, bool bM)
        {
            return Upload2Str(download, bM);
        }

        /// <summary>
        /// SHA512函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA512结果</returns>
        public static string SHA512(string str)
        {
            byte[] SHA512Data = Encoding.UTF8.GetBytes(str);
            SHA512Managed sha512 = new SHA512Managed();
            byte[] Result = sha512.ComputeHash(SHA512Data);
            string ret = "";
            for (int i = 0; i < Result.Length; i++)
                ret += Result[i].ToString("x").PadLeft(2, '0');

            return ret;
        }
        /// <summary>
        /// SHA512函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA512结果</returns>
        public static byte[] SHA512BYTE(string str)
        {
            byte[] SHA512Data = Encoding.UTF8.GetBytes(str);
            SHA512Managed sha512 = new SHA512Managed();
            byte[] Result = sha512.ComputeHash(SHA512Data);
            return Result;
        }

        /// <summary>
        /// 生成由指定字符组成的随机字符串
        /// </summary>
        /// <param name="source">指定字符</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GetRandomString(string source, int length)
        {
            byte[] rndBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(rndBytes);
            System.Random RandomObj = new System.Random(BitConverter.ToInt32(rndBytes, 0));
            string str = null;
            for (int i = 0; i < length; i++)
            {
                str += source.Substring(RandomObj.Next(0, source.Length - 1), 1);
            }
            return str;
        }
        private static System.Random RandomObj = null;
        /// <summary>
        /// 生成指定范围内的随机整数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomInt(int min, int max)
        {
            if (RandomObj == null)
            {
                byte[] rndBytes = new byte[4];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(rndBytes);
                RandomObj = new System.Random(BitConverter.ToInt32(rndBytes, 0));
            }
            return RandomObj.Next(min, max);
        }
        /// <summary>
        /// 生成16进制随机字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomHex(int length)
        {
            string source = "0123456789ABCDEF";
            return GetRandomString(source, length);
        }
        /// <summary>
        /// 生成大写的随机字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            return GetRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length);
        }
        /// <summary>
        /// 生成大小写敏感的随机字符串，cap=true，则大小写敏感，否则一律为大写
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length, bool cap)
        {
            if (cap) return GetRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length);
            else return GetRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length);
        }

        /// <summary>
        /// 将IP地址拆分为头和尾
        /// </summary>
        /// <param name="input"></param>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static int SplitIP(string input, out string head, out string tail)
        {
            head = "";
            tail = "";

            if (IsIPv6(input))
            {
                int sectionid = 0;
                string[] ipsection = input.Split(':');
                foreach (string section in ipsection)
                {
                    if (section == "") sectionid = 5;       //遇到::，则之后的全部算到tail
                    if (sectionid < 4) head += section + ":";
                    else tail += section + ":";
                    sectionid++;
                }
                if (tail.Length > 0 && tail.Substring(tail.Length - 1, 1) == ":") tail = tail.Substring(0, tail.Length - 1); //末尾的“：”
                return sectionid;
            }
            else if (IsIPv4(input))
            {
                int sectionid = 0;
                string[] ipsection = input.Split('.');
                foreach (string section in ipsection)
                {
                    if (sectionid < 3) head += section + ".";
                    else tail = section;
                    sectionid++;
                }
                return sectionid;
            }
            else
            {
                return -1;
            }
        }
        
        /// <summary>
        /// IP地址分类
        /// </summary>
        public enum IPType
        {
            InBuaa_IPv4 = 0,
            InBuaaISATAP_IPv6 = 1,
            InBuaaNative_IPv6 = 2,
            Out_IPv6 = 3,
            Out_IPv4 = 4,
            Teredo_IPv6 = 5,

            ParseError = -100
        }

        /// <summary>
        /// 获取IPv4地址的整数表示方式
        /// </summary>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <param name="part3"></param>
        /// <param name="part4"></param>
        /// <returns></returns>
        public static long GetIPv4Int(int part1, int part2, int part3, int part4)
        {
            return (long)part1 * 16777216 + part2 * 65536 + part3 * 256 + part4;
        }
        /// <summary>
        /// 获取IPv4地址的整数表示方式
        /// </summary>
        /// <param name="ipv4"></param>
        /// <returns></returns>
        public static long GetIPv4Int(string ipv4)
        {
            string[] inputstr = ipv4.Split('.');
            if (inputstr.Length != 4) return -1;
            long ipint = (long)TypeConverter.StrToInt(inputstr[0]) * (long)16777216 + TypeConverter.StrToInt(inputstr[1]) * 65536 + TypeConverter.StrToInt(inputstr[2]) * 256 + TypeConverter.StrToInt(inputstr[3]);
            return ipint;
        }

        /// <summary>
        /// 获取IP地址类型，
        /// 返回值：0.校内v4，1.校内ISATAP v6，2.校内原生v6，3.校外v6，4.校外v4，5.teredo v6，-100.解析错误
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IPType GetIPType(string input)
        {
            if (input.IndexOf(":") < 0 && input.IndexOf(".") > 0)
            {
                //IPv4地址处理
                string[] inputstr = input.Split('.');
                if (inputstr.Length != 4) return IPType.ParseError;
                long ipint = (long)TypeConverter.StrToInt(inputstr[0]) * (long)16777216 + TypeConverter.StrToInt(inputstr[1]) * 65536 + TypeConverter.StrToInt(inputstr[2]) * 256 + TypeConverter.StrToInt(inputstr[3]);


                //判断是否为校内IP

                //【沙河校区和大运村】地址范围 172.16.0.0/16 mask 255.240.0.0
                //[172.16.0.0 - 172.31.255.255]
                if (ipint > (long)172 * 16777216 + 16 * 65536 + 0 * 256 && ipint < (long)172 * 16777216 + 31 * 65536 + 255 * 256 + 255) return IPType.InBuaa_IPv4;

                //【本部宿舍区】地址范围 192.168.0.0/21 mask 255.255.0.0
                //[192.168.0.0 - 192.168.255.255]
                if (ipint > (long)192 * 16777216 + 168 * 65536 + 0 * 256 && ipint < (long)192 * 16777216 + 168 * 65536 + 255 * 256 + 255) return IPType.InBuaa_IPv4;

                //【沙河校区】地址范围 115.25.128.1/18 mask 255.255.192.0
                //[115.25.128.0 - 115.25.191.255]
                if (ipint > (long)115 * 16777216 + 25 * 65536 + 128 * 256 && ipint < (long)115 * 16777216 + 25 * 65536 + 191 * 256 + 255) return IPType.InBuaa_IPv4;

                //【新主楼】地址范围 219.224.128.0/20 mask 255.255.192.0    219.224.192.0/24 mask 255.255.255.0
                //[219.224.128.0 - 219.224.192.255]
                if (ipint > (long)219 * 16777216 + 224 * 65536 + 128 * 256 && ipint < (long)219 * 16777216 + 224 * 65536 + 192 * 256 + 255) return IPType.InBuaa_IPv4;

                //【其他地址】
                //地址范围 211.71.0.0/20 MASK 255.255.240.0 
                //[211.71.0.0 - 211.71.15.255]
                if (ipint > (long)211 * 16777216 + 71 * 65536 + 0 * 256 && ipint < (long)211 * 16777216 + 71 * 65536 + 15 * 256 + 255) return IPType.InBuaa_IPv4;
                
                //地址范围 202.112.128.0/20 mask 255.255.240.0 
                //[202.112.128.0 - 202.112.143.255]
                if (ipint > (long)202 * 16777216 + 112 * 65536 + 128 * 256 && ipint < (long)202 * 16777216 + 112 * 65536 + 143 * 256 + 255) return IPType.InBuaa_IPv4;

                //地址范围 219.239.227.0/24 mask 255.255.255.0 
                //[219.239.227.0 - 219.239.227.255]
                if (ipint > (long)219 * 16777216 + 239 * 65536 + 227 * 256 && ipint < (long)219 * 16777216 + 239 * 65536 + 227 * 256 + 255) return IPType.InBuaa_IPv4;

                //地址范围 58.194.224.0/21 mask 255.255.248.0
                //[58.194.224.0 - 58.194.231.255]
                if (ipint > (long)58 * 16777216 + 194 * 65536 + 224 * 256 && ipint < (long)58 * 16777216 + 194 * 65536 + 231 * 256 + 255) return IPType.InBuaa_IPv4;

                //地址范围 58.195.8.1/21 mask 255.255.248.0    58.195.16.1/24 mask 255.255.255.0
                //[58.195.8.0 - 58.195.16.255]
                if (ipint > (long)58 * 16777216 + 195 * 65536 + 8 * 256 && ipint < (long)58 * 16777216 + 195 * 65536 + 16 * 256 + 255) return IPType.InBuaa_IPv4;

                //地址范围 10.0.0.0/8 mask 255.0.0.0
                //[10.0.0.0 - 10.255.255.255]
                if (ipint > (long)10 * 16777216 + 0 * 65536 + 0 * 256 && ipint < (long)10 * 16777216 + 255 * 65536 + 255 * 256 + 255) return IPType.InBuaa_IPv4;

                return IPType.Out_IPv4;

            }
            else if (input.IndexOf(":") > 0)
            {
                //IPv6地址处理
                if (input.IndexOf("2001:0:") == 0) return IPType.Teredo_IPv6;
                else if (input.IndexOf("2001:da8:203") == 0) 
                {
                    if (input.IndexOf("2001:da8:203:888:") == 0 || input.IndexOf("2001:da8:203:666:") == 0) return IPType.InBuaaISATAP_IPv6;
                    else if (input.IndexOf(":0:5efe:") > 0) return IPType.InBuaaISATAP_IPv6;
                    else return IPType.InBuaaNative_IPv6;
                }
                else if (input.IndexOf("2001:da8:ae") == 0)
                {
                    return IPType.InBuaaNative_IPv6;
                }
                else
                {
                    return IPType.Out_IPv6;
                }
            }
            else
            {
                return IPType.ParseError;
            }
        }
        

        /// <summary>
        /// 检测是否为IPv6地址，不检测是否合法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPv6(string input)
        {
            if (input.IndexOf(":") < 0) return false;
            else return true;
        }
        ///// <summary>
        ///// 判断输入的字符串是否是合法的IPv6地址 //此函数有问题。。。需要修改
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public static bool IsIPV6(string input)
        //{
        //    /* *******************************************************************
        // * 1、通过“:”来分割字符串看得到的字符串数组长度是否小于等于8
        // * 2、判断输入的IPV6字符串中是否有“::”。
        // * 3、如果没有“::”采用 ^([\da-f]{1,4}:){7}[\da-f]{1,4}$ 来判断
        // * 4、如果有“::” ，判断"::"是否止出现一次
        // * 5、如果出现一次以上 返回false
        // * 6、^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$
        // * ******************************************************************/
        //    string pattern = "";
        //    string temp = input;
        //    string[] strs = temp.Split(':');
        //    if (strs.Length > 8)
        //    {
        //        return false;
        //    }
        //    int count = GetStringCount(input, "::");
        //    if (count > 1)
        //    {
        //        return false;
        //    }
        //    else if (count == 0)
        //    {
        //        pattern = @"^([\da-f]{1,4}:){7}[\da-f]{1,4}$";

        //        Regex regex = new Regex(pattern);
        //        return regex.IsMatch(input);
        //    }
        //    else
        //    {
        //        pattern = @"^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$";
        //        Regex regex1 = new Regex(pattern);
        //        return regex1.IsMatch(input);
        //    }
        //}
        /// <summary>
        /// 判断输入的字符串是否是合法的IPv4地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPv4(string input)
        {
            string pattern = @"^((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 判断字符串compare 在 input字符串中出现的次数
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="compare">用于比较的字符串</param>
        /// <returns>字符串compare 在 input字符串中出现的次数</returns>
        public static int GetStringCount(string input, string compare)
        {
            int index = input.IndexOf(compare);
            if (index != -1)
            {
                return 1 + GetStringCount(input.Substring(index + compare.Length), compare);
            }
            else
            {
                return 0;
            }

        }
        /// <summary>
        /// 将字符串转化为对应的HEX字符串：info_hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RawUrl2INFO_HASH_HEX(string input)
        {
            //return input;
            if (input.IndexOf("info_hash=") > 0) input = input.Substring(input.IndexOf("info_hash=") + 10);
            else return "";
            if (input.IndexOf("&") > 0) input = input.Substring(0, input.IndexOf("&"));


            int len = input.Length;
            string output = "";
            for (int i = 0; i < len; i++)
            {
                //if (input.Substring(i, 1) == "&") break;
                if (input.Substring(i, 1) != "%") output += String.Format("{0:X}", Convert.ToInt32(input.Substring(i, 1).ToCharArray()[0]));
                else if (i + 2 < len)
                {
                    output += input.Substring(i + 1, 2);
                    i += 2;
                }
            }
            return output.ToUpper();
        }
        /// <summary>
        /// 将字符串转化为对应的HEX字符串：peer_id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RawUrl2PEERID_HEX(string input)
        {
            //return input;
            if (input.IndexOf("peer_id=") > 0) input = input.Substring(input.IndexOf("peer_id=") + 8);
            else return "";
            if (input.IndexOf("&") > 0) input = input.Substring(0, input.IndexOf("&"));


            int len = input.Length;
            string output = "";
            for (int i = 0; i < len; i++)
            {
                //if (input.Substring(i, 1) == "&") break;
                if (input.Substring(i, 1) != "%") output += String.Format("{0:X}", Convert.ToInt32(input.Substring(i, 1).ToCharArray()[0]));
                else if (i + 2 < len)
                {
                    output += input.Substring(i + 1, 2);
                    i += 2;
                }
            }
            return output;
        }
        /// <summary>
        /// 将HEX字符串转换为字符串，返回十六进制代表的字符串
        /// </summary>
        /// <param name="mHex"></param>
        /// <returns></returns>
        public static string HEX2Str(string input)
        {
            input = input.Replace(" ", "");
            if (input.Length <= 0) return "";
            byte[] vBytes = new byte[input.Length / 2];
            for (int i = 0; i < input.Length; i += 2)
                if (!byte.TryParse(input.Substring(i, 2), System.Globalization.NumberStyles.HexNumber, null, out vBytes[i / 2]))
                    vBytes[i / 2] = 0;
            return ASCIIEncoding.Default.GetString(vBytes);
        }
        /// <summary>
        /// 将Char数组转换为HEX字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Char2HEX(char[] input)
        {
            int len = input.Length;
            string output = "";
            for (int i = 0; i < len; i++)
            {
                output += String.Format("{0:X}", Convert.ToInt32(input[i]));
            }
            return output;
        }
        ///                                                                                                                                                                                                    <summary>
        /// 将byte数组转换为HEX字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Byte2HEX(byte[] input)
        {
            int len = input.Length;
            string output = "";
            string tmp = "";
            for (int i = 0; i < len; i++)
            {
                tmp = String.Format("{0:X}", Convert.ToInt32(input[i]));
                if (tmp.Length < 2) tmp = "0" + tmp;
                output += tmp;
            }
            return output;
        }
        ///                                                                                                                                                                                                    <summary>
        /// 将byte数组转换为HEX字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Byte2HEXLower(byte[] input)
        {
            int len = input.Length;
            string output = "";
            string tmp = "";
            for (int i = 0; i < len; i++)
            {
                tmp = String.Format("{0:x}", Convert.ToInt32(input[i]));
                if (tmp.Length < 2) tmp = "0" + tmp;
                output += tmp;
            }
            return output;
        }
        /// <summary>
        /// 将byte数组转换为HEX字符串，f为格式化（按组排列）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static string Byte2HEX(byte[] input, bool f)
        {
            int len = input.Length;
            string output = "";
            string tmp = "";
            for (int i = 1; i <= len; i++)
            {
                tmp = String.Format("{0:X}", Convert.ToInt32(input[i]));
                if (tmp.Length < 2) tmp = "0" + tmp;
                output += tmp;
                output += " ";
                if (i % 16 == 0) output += "&nbsp;&nbsp;";
                if (i % 64 == 0) output += "<br/>";
            }
            return output;
        }
        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime Int2Time(int d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds((double)d);
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static int Time2Int(System.DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (int)(time - startTime).TotalSeconds;
            return intResult;
        }
        /// <summary>
        /// 将秒数转化为字符串描述 如5天3小时32分
        /// </summary>
        /// <param name="min"></param>
        /// <returns></returns>
        public static string Second2String(double second)
        {
            return Second2String(second, false);
        }
        /// <summary>
        /// 将秒数转化为字符串描述 如5天3小时32分
        /// </summary>
        /// <param name="min"></param>
        /// <returns></returns>
        public static string Second2String(double second, bool shortformat)
        {
            string output = "";
            if (second / 60 / 60 / 24.0 > 1)
            {
                output += ((int)(second / 60 / 60 / 24)).ToString("0") + "天";
                second -= ((int)(second / 60 / 60 / 24)) * 60  * 60 * 24;
                if (shortformat) return output;
            }
            if (second / 60 / 60 > 1)
            {
                output += ((int)(second / 60 / 60)).ToString("0") + "时";
                second -= ((int)(second / 60 / 60)) * 60 * 60;
                if (shortformat) return output;
            }
            if (second / 60 > 1)
            {
                output += ((int)(second / 60)).ToString("0") + "分";
                second -= ((int)(second / 60)) * 60;
                if (shortformat) return output;
            }
            if (second < 0) second = 0;
            output += second.ToString("0") + "秒";
            return output;
        }
        /// <summary>
        /// 将分钟数转化为字符串描述 如5天3小时32分
        /// </summary>
        /// <param name="min"></param>
        /// <returns></returns>
        public static string Minutes2String(double min)
        {
            return Minutes2String(min, false);
        }
        /// <summary>
        /// 将分钟数转化为字符串描述 如5天3小时32分
        /// </summary>
        /// <param name="min"></param>
        /// <returns></returns>
        public static string Minutes2String(double min, bool shortformat)
        {
            string output = "";
            if (min / 60 / 24.0 > 1)
            {
                output += ((int)(min / 60 / 24)).ToString() + "天";
                min -= ((int)(min / 60 / 24)) * 60 * 24;
                if (shortformat) return output;
            }
            if (min / 60 > 1)
            {
                output += ((int)(min / 60)).ToString() + "时";
                min -= ((int)(min / 60)) * 60;
                if (shortformat) return output;
            }
            if (min < 0) min = 0;
            output += min.ToString("0") + "分";
            return output;
        }

        /// <summary>
        /// 按照容量大小获得上传系数(上传种子时计算)
        /// </summary>
        /// <param name="filesize"></param>
        /// <returns></returns>
        public static float GetUploadRatio(decimal filesize)
        {
            return 1.0f;

            //if (filesize < 1073741824m) return 1.0;             //1G
            //else if (filesize < 2147483648m) return 1.0;        //2G
            //else if (filesize < 4294967296m) return 1.0;        //4G
            //else if (filesize < 8589934592m) return 1.2;        //8G
            //else if (filesize < 17179869184m) return 1.6;       //16G
            //else return 2.0;
        }
        /// <summary>
        /// 按照容量大小获得下载系数(上传种子时计算)
        /// </summary>
        /// <param name="filesize"></param>
        /// <returns></returns>
        public static double GetDownloadRatio(decimal filesize)
        {
            //1G一下为1，1G以上为1/(1+((容量-1G)/16G))
            if (filesize < 1073741824m) return 1.0;
            else return double.Parse((1.0 / (1 + ((double)filesize - 1073741824) / 17179869184)).ToString("0.00"));
        }
        /// <summary>
        /// 两位小数向下取整
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double GetFloorDot2(double input)
        {
            return Math.Floor(input * 100) / 100.0;
        }
        /// <summary>
        /// 共享率计算
        /// </summary>
        /// <param name="uploadvalue"></param>
        /// <param name="downloadvalue"></param>
        /// <returns></returns>
        public static double GetRatio(decimal uploadvalue, decimal downloadvalue)
        {
            double ratio = 0;

            if ((double)(downloadvalue) > 0)
            {
                ratio = (double)(uploadvalue) / (double)(downloadvalue);
                //ratio = (float)(Math.Floor(ratio * 100.0) / 100.0);  //精确到小数点后两位，向下取整
                //由于此函数被保存函数调用，此处做此修改会影响共享率排序，应该在读取时做修改，直接此函数的显示页面做调整
            }
            else ratio = 1.00001;

            //共享率限制规则
            //btuserinfo.Download += btuserinfo.Extcredits6;
            double RatioLimit = 1.00001;
            if (downloadvalue < 1024M * 1024 * 1024) RatioLimit = 1.00001;
            else if (downloadvalue < 10 * 1024M * 1024 * 1024) RatioLimit = 10.00001;
            else if (downloadvalue < 20 * 1024M * 1024 * 1024) RatioLimit = 50.00001;
            else if (downloadvalue < 50 * 1024M * 1024 * 1024) RatioLimit = 100.00001;
            else if (downloadvalue < 100 * 1024M * 1024 * 1024) RatioLimit = 200.00001;
            else if (downloadvalue < 500 * 1024M * 1024 * 1024) RatioLimit = 500.00001;
            else RatioLimit = 1000000;

            //超出共享率限制的情况
            if (ratio > RatioLimit) ratio = RatioLimit;

            return ratio;
        }

        /// <summary>
        /// 共享率计算
        /// </summary>
        /// <param name="uploadvalue"></param>
        /// <param name="downloadvalue"></param>
        /// <param name="uploadadd"></param>
        /// <param name="downloadadd"></param>
        /// <returns></returns>
        public static double GetRatio(decimal uploadvalue, decimal downloadvalue, decimal uploadadd, decimal downloadadd)
        {
            return GetRatio(uploadvalue + uploadadd, downloadvalue + downloadadd);
        }


        /// <summary>
        /// 获得imdb链接
        /// </summary>
        /// <param name="infoinput"></param>
        /// <returns></returns>
        public static string GetImdbLink(string infoinput)
        {
            if (Utils.IsInt(infoinput))
            {
                return "http://www.imdb.com/title/tt" + Utils.StrToInt(infoinput, 0).ToString("0000000");
            }
            else if (infoinput.IndexOf("tt") > -1 && infoinput.IndexOf("tt") + 2 < infoinput.Length)
            {
                return "http://www.imdb.com/title/tt" + Utils.StrToInt(infoinput.Substring(infoinput.IndexOf("tt") + 2), 0).ToString("0000000");
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// javascript的escape函数，url编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EscapeDefault(string str)
        {
            str = Microsoft.JScript.GlobalObject.escape(str);
            return str;
        }
        /// <summary>
        /// javascript的escape函数，url编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Escape(string str)
        {
            str = Microsoft.JScript.GlobalObject.escape(str);
            str = str.Replace("+", "%2B").Replace("\"", "%22").Replace("'", "%27").Replace("/", "%2F");
            return str;
        }
        /// <summary>
        /// javascript的unescape函数，url解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnEscape(string str)
        {
            return Microsoft.JScript.GlobalObject.unescape(str).Replace("%2B", "+").Replace("%22", "\"").Replace("%27", "'").Replace("%2F", "/");
        }
        /// <summary>
        /// javascript的unescape函数，url解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnEscapeDefault(string str)
        {
            return Microsoft.JScript.GlobalObject.unescape(str);
        }

        /// <summary>
        /// OR查询方式：对输入的搜索字符串进行预处理，去除全文搜索干扰词
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static string SQLFullContentIndexKeywordsProcessOR(string keywords)
        {
            string okstring = "", tmpstring = "";
            string[] badwords = {
                                    "?","about","$","1","2","3","4","5","6","7","8","9","0","_",
                                    "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o",
                                    "p","q","r","s","t","u","v","w","x","y","z","after","all","also",
                                    "an","and","another","any","are","as","at","be","because","been",
                                    "before","being","between","both","but","by","came","can","come",
                                    "could","did","do","each","for","from","get","got","had","has",
                                    "have","he","her","here","him","himself","his","how","if","in","into",
                                    "is","it","like","make","many","me","might","more","most","much","must",
                                    "my","never","now","of","on","only","or","other","our","out","over","said",
                                    "same","see","should","since","some","still","such","take","than","that",
                                    "the","their","them","then","there","these","they","this","those","through",
                                    "to","too","under","up","very","was","way","we","well","were","what","where",
                                    "which","while","who","with","would","you","your",
                                    "的","一","不","在","人","有","是","为","以","于","上","他","而","后","之","来",
                                    "及","了","因","下","可","到","由","这","与","也","此","但","并","个","其","已",
                                     "无","小","我","们","起","最","再","今","去","好","只","又","或","很","亦","某",
                                    "把","那","你","乃","它"
                                };
            keywords = keywords.Replace(".", " ");
            keywords = keywords.Replace("[", " ");
            keywords = keywords.Replace("]", " ");
            keywords = keywords.Replace("'", " ");
            keywords = keywords.Replace("\"", " ");
            keywords = keywords.Replace("*", " ");
            keywords = keywords.Replace(",", " ");
            keywords = keywords.Replace("，", " ");
            int i = 0;
            for (; keywords.IndexOf(" ") > -1 && i < 100; i++)
            {
                if (keywords.IndexOf(" ") == 0)
                {
                    keywords = keywords.Substring(1);
                    continue;
                }

                tmpstring = keywords.Substring(0, keywords.IndexOf(" ")).ToLower();
                if (Array.IndexOf(badwords, tmpstring) < 0)
                {
                    okstring += (okstring == "") ? "" : ".AND.";
                    okstring += tmpstring + "*";
                    //if (tmpstring != "and" && tmpstring != "or") okstring += tmpstring;
                }
                keywords = keywords.Substring(keywords.IndexOf(" ") + 1);
            }
            if (i > 99) return "";
            if (keywords.Length > 1)
            {
                okstring += (okstring == "") ? "" : ".AND.";
                okstring += keywords + "*";
            }
            return okstring.Replace(".AND.", " OR ");
        }


















        /// <summary>
        /// 支持模式配置的全文检索字符串处理
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static string SQLFullContentIndexKeywordsProcess(string keywords)
        {
            return SQLFullContentIndexKeywordsProcess(keywords, 0);
        }

        /// <summary>
        /// 对输入的搜索字符串进行预处理，不去除全文搜索干扰词
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="mode">0默认精确AND模式，1添加通配符的模糊AND模式</param>
        /// <returns></returns>
        public static string SQLFullContentIndexKeywordsProcess(string keywords, int mode)
        {
            #region 前置过滤

            if (Utils.StrIsNullOrEmpty(keywords)) return "";

            string okstring = "";
            string tmpstring = "";
            string bkstring = "";
            //string[] badwords = {
            //                        "?","about","$","1","2","3","4","5","6","7","8","9","0","_",
            //                        "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o",
            //                        "p","q","r","s","t","u","v","w","x","y","z","after","all","also",
            //                        "an","and","another","any","are","as","at","be","because","been",
            //                        "before","being","between","both","but","by","came","can","come",
            //                        "could","did","do","each","for","from","get","got","had","has",
            //                        "have","he","her","here","him","himself","his","how","if","in","into",
            //                        "is","it","like","make","many","me","might","more","most","much","must",
            //                        "my","never","now","of","on","only","or","other","our","out","over","said",
            //                        "same","see","should","since","some","still","such","take","than","that",
            //                        "the","their","them","then","there","these","they","this","those","through",
            //                        "to","too","under","up","very","was","way","we","well","were","what","where",
            //                        "which","while","who","with","would","you","your",
            //                        "的","一","不","在","人","有","是","为","以","于","上","他","而","后","之","来",
            //                        "及","了","因","下","可","到","由","这","与","也","此","但","并","个","其","已",
            //                         "无","小","我","们","起","最","再","今","去","好","只","又","或","很","亦","某",
            //                        "把","那","你","乃","它"
            //                    };
            keywords = keywords.Replace(".", " ");
            keywords = keywords.Replace("[", " ");
            keywords = keywords.Replace("]", " ");
            keywords = keywords.Replace("'", " ");
            keywords = keywords.Replace("\"", " ");
            keywords = keywords.Replace("*", " ");
            keywords = keywords.Replace(",", " ");
            keywords = keywords.Replace("，", " ");
            keywords = keywords.Replace("<", " ");
            keywords = keywords.Replace(">", " ");
            keywords = keywords.Replace("《", " ");
            keywords = keywords.Replace("》", " ");
            keywords = keywords.Trim();

            #endregion

            #region 单个关键词，直接返回

            if (keywords.IndexOf(" ") < 0)
            {
                if (mode == 0) return "\"" + keywords + "\"";
                else if (mode == 1) return "\"" + keywords + "*\"";
                else if (mode == 2) return "%" + keywords + "%";
                else if (mode == 3 || mode == 4 || mode == -2) return "\"" + keywords + "\"";
                else return "";
            }

            #endregion

            int i = 0;
            for (; keywords.IndexOf(" ") > -1 && i < 100; i++)
            {
                if (keywords.IndexOf(" ") == 0)
                {
                    keywords = keywords.Substring(1);
                    continue;
                }

                tmpstring = keywords.Substring(0, keywords.IndexOf(" ")).ToLower();

                //if (Array.IndexOf(badwords, tmpstring) < 0)
                {
                    bkstring = okstring;
                    okstring += (okstring == "") ? "" : ".AND.";
                    if (mode == 0 || mode == 2 || mode == 3 || mode == 4 || mode == -2)   //在此处添加使用全文索引的关键词
                    {
                        okstring += "\"" + tmpstring + "\"";
                    }
                    else if (mode == 1)                                      //在此处添加使用like搜索的关键词
                    {
                        okstring += "\"" + tmpstring + "*" + "\"";          
                    }

                    if (okstring.Length > 499)
                    {
                        return bkstring.Replace(".AND.", " AND ");
                    }
                    //if (tmpstring != "and" && tmpstring != "or") okstring += tmpstring;
                }
                keywords = keywords.Substring(keywords.IndexOf(" ") + 1);
            }

            //收尾的处理
            //if (i > 99) return "";
            keywords = keywords.Trim();

            bkstring = okstring;
            if (keywords.Length > 0)
            {
                okstring += (okstring == "") ? "" : ".AND.";
                if (mode == 0 || mode == 2 || mode == 3 || mode == 4 || mode == -2)       //在此处添加使用全文索引的关键词
                {
                    okstring += "\"" + keywords + "\"";
                }
                //contains
                else if (mode == 1)                                           //在此处添加使用like搜索的关键词
                {
                    okstring += "\"" + keywords + "*" + "\"";
                }
            }
            if (okstring.Length > 499)
            {
                okstring = bkstring.Replace(".AND.", " AND ");
            }
            okstring = okstring.Replace(".AND.", " AND ");


            //返回关键词
            if (mode == 2)
            {
                return "%" + okstring.Replace(" AND ", "%").Replace("\"", "") + "%";
            }
            else if (mode == -2)
            {
                //排除，将 AND 替换为 OR 
                return okstring.Replace(" AND ", " OR ");
            }
            else
            {
                return okstring;
            }
        }
    }
}
