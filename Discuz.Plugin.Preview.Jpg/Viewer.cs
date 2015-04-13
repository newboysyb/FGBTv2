using System;
#if NET1
#else
using System.Collections.Generic;
#endif

using System.Text;

using Discuz.Entity;
using Discuz.Plugin.Preview;

namespace Discuz.Plugin.Preview.Jpg
{
    public class Viewer: IPreview
    {
        private bool useFTP;
        #region IPreview Members
        public bool UseFTP
        {
            get
            {
                return useFTP;
            }
            set
            {
                useFTP = value;
            }
        }

        /// <summary>
        /// 获得预览信息
        /// </summary>
        /// <param name="fileName">文件物理路径</param>
        /// <param name="attachment">附件对象</param>
        /// <returns>预览信息的HTML</returns>
        public string GetPreview(string fileName, ShowtopicPageAttachmentInfo attachment)
        {
            if (!PreviewHelper.IsFileExist(fileName))
                return "";

            //////////////////////////////////////////////////////////////////////////
            //【BT修改】防止因为部分文件问题，导致程序出错，当不确定文件是否为图片时，使用try
            if (attachment.Isimage == 1)
            {
                string id = Guid.NewGuid().ToString();
                EXIFextractor exif = new EXIFextractor(fileName, string.Empty, string.Empty);
                if (exif.Count < 1)
                {
                    return string.Empty;
                }
                StringBuilder builder = new StringBuilder();
                foreach (System.Web.UI.Pair s in exif)
                {
                    if (s.Second.ToString().Trim() == "-" || s.Second.ToString().Trim() == string.Empty || s.Second.ToString().Trim() == "0")
                        continue;
                    builder.Append("<li>");
                    builder.Append(s.First.ToString().Trim() + " : " + s.Second.ToString().Replace("\0", string.Empty).Trim());
                    builder.Append("</li>");
                }
                if (builder.Length == 0)
                {
                    return string.Empty;
                }
                builder.Append("</ul></div>");

                builder.Insert(0, string.Format("<span><a onclick=\"$('{0}').style.display=$('{0}').style.display=='none'?'':'none';\" style='cursor:pointer;' title='点击显示或隐藏'>EXIF信息</a></span><div class=\"preview{1}\"><ul id=\"{0}\" style='display: none;'>", id, "jpg"));

                return builder.ToString();
            }
            else
            {
                try
                {
                    string id = Guid.NewGuid().ToString();
                    EXIFextractor exif = new EXIFextractor(fileName, string.Empty, string.Empty);
                    if (exif.Count < 1)
                    {
                        return string.Empty;
                    }
                    StringBuilder builder = new StringBuilder();
                    foreach (System.Web.UI.Pair s in exif)
                    {
                        if (s.Second.ToString().Trim() == "-" || s.Second.ToString().Trim() == string.Empty || s.Second.ToString().Trim() == "0")
                            continue;
                        builder.Append("<li>");
                        builder.Append(s.First.ToString().Trim() + " : " + s.Second.ToString().Replace("\0", string.Empty).Trim());
                        builder.Append("</li>");
                    }
                    if (builder.Length == 0)
                    {
                        return string.Empty;
                    }
                    builder.Append("</ul></div>");

                    builder.Insert(0, string.Format("<span><a onclick=\"$('{0}').style.display=$('{0}').style.display=='none'?'':'none';\" style='cursor:pointer;' title='点击显示或隐藏'>EXIF信息</a></span><div class=\"preview{1}\"><ul id=\"{0}\" style='display: none;'>", id, "jpg"));

                    return builder.ToString();
                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                    return "";
                }
                
            }
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////

        }

        public void OnSaved(string fileName)
        {
        }

        #endregion

    }
}
