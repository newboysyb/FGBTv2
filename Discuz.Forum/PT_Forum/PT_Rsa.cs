using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using System.IO;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;



namespace Discuz.Forum
{
    public partial class PTRsa
    {

        public static string GetUserRsaPublicKey(int uid, ref string UserRsaRkey)
        {
            string RsaPublicKey = "";
            string RsaXML = "";
            DateTime RsaLastUpdate = DateTime.MinValue;
            string RsaRkey = "";
            bool RsaExist = false;

            //尝试从数据库中读取
            IDataReader reader = DatabaseProvider.GetInstance().GetRsaRecord(uid);
            if (reader.Read())
            {
                RsaExist = true;
                RsaXML = reader["rsaxml"].ToString().Trim();
                RsaLastUpdate = TypeConverter.ObjectToDateTime(reader["lastupdate"], DateTime.MinValue);
                RsaRkey = reader["rkey"].ToString().Trim();
                reader.Close();
                reader.Dispose();
            }
            reader.Close();
            reader.Dispose();

            //如果记录不存在，则创建
            if(!RsaExist)
            {
                RSACryptoServiceProvider rsaReceive = new RSACryptoServiceProvider();
                RsaXML = rsaReceive.ToXmlString(true);
                RsaRkey = PTTools.GetRandomHex(10);
                try
                {
                    if(DatabaseProvider.GetInstance().InsertRsaRecord(uid, RsaXML, RsaRkey) < 1)
                    {
                        DatabaseProvider.GetInstance().UpdateRsaRecord(uid, RsaXML, RsaRkey);
                    }
                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                	DatabaseProvider.GetInstance().UpdateRsaRecord(uid, RsaXML, RsaRkey);
                }
            }

            //如果读取的信息已经过期或失效
            if(RsaXML == "" || (DateTime.Now - RsaLastUpdate).TotalMinutes > 60 && RsaRkey.Length != 10)
            {
                RSACryptoServiceProvider rsaReceive = new RSACryptoServiceProvider();
                RsaXML = rsaReceive.ToXmlString(true);
                RsaRkey = PTTools.GetRandomHex(10);
                DatabaseProvider.GetInstance().UpdateRsaRecord(uid, RsaXML, RsaRkey);
            }

            //读取RsaXML中的公钥
            try
            {
                XmlDocument rssDoc = new XmlDocument();
                rssDoc.LoadXml(PTTools.SanitizeXmlString(RsaXML));
                XmlNodeList rssItems = rssDoc.SelectNodes("RSAKeyValue");
                if(rssItems.Count > 0)
                {
                    RsaPublicKey = rssItems[0].SelectSingleNode("Modulus").InnerText;
                    byte[] byteTemp = Convert.FromBase64String(RsaPublicKey);
                    RsaPublicKey = PTTools.Byte2HEX(byteTemp);
                    UserRsaRkey = RsaRkey;
                }
                else return "";
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            	return "";
            }

            return RsaPublicKey;
        }

        public static int ClearUserRsaPublicKey(int uid)
        {
            return DatabaseProvider.GetInstance().UpdateRsaRecord(uid, "", "");
        }

        public static string DecryptUserPassword(int uid, string UserRsaRkey, string password)
        {
            string RsaXML = "";
            string RsaRkey = "";
            DateTime RsaLastUpdate = DateTime.MinValue;

            //尝试从数据库中读取
            IDataReader reader = DatabaseProvider.GetInstance().GetRsaRecord(uid);
            if (reader.Read())
            {
                RsaXML = reader["rsaxml"].ToString().Trim();
                RsaLastUpdate = TypeConverter.ObjectToDateTime(reader["lastupdate"], DateTime.MinValue);
                RsaRkey = reader["rkey"].ToString().Trim();
                reader.Close();
                reader.Dispose();
            }
            reader.Close();
            reader.Dispose();

            //解密密码
            if (RsaXML != "" && (DateTime.Now - RsaLastUpdate).TotalMinutes < 80 && RsaRkey == UserRsaRkey)
            {
                RSACryptoServiceProvider rsaReceive = new RSACryptoServiceProvider();
                try
                {
                    rsaReceive.FromXmlString(RsaXML);
                    byte[] bytePassword;
                    byte[] byteTemp;
                    byteTemp = PTTools.HEX2BYTE(password);
                    bytePassword = rsaReceive.Decrypt(byteTemp, false);
                    return PTTools.Byte2HEX(bytePassword);
                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                    return "";
                }
            }
            else
                return "";
        }



    }
}
