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
        
        public static string MsgOut = "";

        //分类信息选择项，添加删除项目，必须同时修改下方输出函数、publish和edit页面及对应的模板文件
        private static string[] blank = {};
        private static string[] movie_rank = { "G", "PG", "PG-13", "R", "NC-17" };
        private static string[] movie_type = { "喜剧", "动作", "爱情", "文艺", "剧情", "科幻", "魔幻", "悬疑", "惊悚", "恐怖", "罪案", "战争", "纪录", "动画", "音乐", "歌舞", "冒险", "历史" };
        private static string[] movie_resolution = { "1080P", "1080i", "720P", "BDRIP", "HDDVDRIP", "DVDRIP", "DVDSCR", "R5", "MiniSD", "HalfCD"};
        private static string[] movie_video = { "H264", "VC1", "WMV", "MOV", "DivX", "XviD", "MPEG4", "MPEG2", "MPEG" };
        private static string[] movie_audio = { "LPCM", "DTS-HDMA", "TrueHD", "DTS", "DD51", "AC3", "MP3"};
        private static string[] movie_region = { "美国", "中国大陆", "中国香港", "中国台湾", "日本", "韩国", "泰国", "法国", "印度", "英国", "德国", "澳大利亚" };
        private static string[] movie_language = { "英语", "国语", "粤语", "日语", "韩语", "法语", "德语", "西班牙语", "俄语"};
        private static string[] movie_subtitle = { "中文字幕", "英文字幕", "中英字幕", "暂无字幕", "无需字幕" };
        
        private static string[] tv_region = { "美剧","英剧", "大陆", "港台", "日剧", "韩剧", "其他" };
        private static string[] tv_language = { "英语", "国语", "粤语", "日语", "韩语", "法语", "德语", "西班牙语", "俄语" };
        private static string[] tv_resolution = { "1080P", "1080i", "720P", "BDRIP", "HDDVDRIP", "DVDRIP", "DVDSCR", "RMVB", "MiniSD", "HalfCD"};
        private static string[] tv_subtitle = { "中文字幕", "英文字幕", "中英字幕", "暂无字幕", "无需字幕" };

        private static string[] comic_region = { "日漫", "美国", "大陆", "港台", "其他" };
        private static string[] comic_language = { "日语", "英语", "国语" };
        private static string[] comic_source = { "BDISO", "DVDISO", "BDRip", "HDTVRip", "DVDRip", "TVRip", "WebRip", "LDRip", "BDMV", "无损音频", "有损音频", "n/a"};
        private static string[] comic_format = { "H264", "X264 10Bit", "X264", "VC1", "WMV", "MOV", "DivX", "XviD", "MPEG4", "MPEG2", "MPEG", "RMVB", "RM", "n/a"};
        private static string[] comic_subtitle = { "中文字幕", "英文字幕", "日文字幕", "中英字幕", "中日字幕", "暂无字幕", "无需字幕" };
        private static string[] comic_type = { "连载", "TV", "剧场", "OVA", "OAD", "漫画", "音乐","周边" };

        private static string[] game_language = { "英文", "日文", "官方繁体", "官方简体", "简体汉化", "繁体汉化", "其他" };
        private static string[] game_platform = { "PC", "PSP", "NDS", "GBA", "NGC", "Wii", "PS", "PS2", "XBOX", "XBOX360", "周边" };
        private static string[] game_type = { "ACT", "AVG", "FPS", "FTG", "MUG", "PUZ", "TCG", "SIM", "TAB", "SPG", "RAG", "STG", "SLG", "RTS", "RPG", "SRPG", "ARPG", "其他" };
        private static string[] game_format = { "光盘镜像", "压缩包", "安装包", "其他" };

        private static string[] music_language = { "国语", "粤语", "闽南", "日语", "欧美", "韩语"};
        private static string[] music_region = { "华语", "欧美", "日韩" };
        private static string[] music_format = { "MP3", "OGG", "MPC", "APE", "FLAC", "WV", "TTA", "DTS", "其他" };
        private static string[] music_company = { "同人", "环球", "百代", "索尼", "华纳", "滚石", "福茂", "海蝶", "天娱", "英皇"};
        private static string[] music_bps = { "无损","320Kbps","256Kbps","192Kbps" };
        private static string[] music_type = { "流行", "摇滚", "民谣", "电子", "爵士", "原声", "嘻哈", "古典", "轻音乐" };

        private static string[] discovery_language = { "英语", "中文", "德语", "日语"};
        private static string[] discovery_type = { "探索频道", "国家地理", "BBC", "NHK", "CCTV", "PBS"};
        private static string[] discovery_format = { "MKV", "AVI", "TS", "RMVB", "WMV" };
        private static string[] discovery_resolution = { "1080P", "1080i", "720P", "BDRIP", "HDDVDRIP", "DVDRIP", "RMVB" };
        private static string[] discovery_subtitle = { "中文字幕", "英文字幕", "中英字幕", "暂无字幕", "无需字幕" };

        private static string[] sport_language = { "英语", "中文", "德语", "日语" };
        private static string[] sport_type = { "足球", "篮球", "网球", "排球", "赛车", "羽毛球", "斯诺克" };
        private static string[] sport_format = { "MKV", "AVI", "TS", "RMVB", "WMV" };
        private static string[] sport_resolution = { "TVRIP", "1080P", "1080i", "720P", "BDRIP", "HDDVDRIP", "DVDRIP" };
        private static string[] sport_subtitle = { "中文字幕", "英文字幕", "中英字幕", "暂无字幕", "无需字幕" };

        private static string[] entertainment_language = { "国语", "粤语", "闽南", "日语", "英语", "韩语" };
        private static string[] entertainment_region = { "台湾", "香港", "大陆", "日本", "美国", "韩国" };
        private static string[] entertainment_format = { "MKV", "AVI", "TS", "RMVB", "WMV" };
        private static string[] entertainment_resolution = { "1080P", "1080i", "720P", "BDRIP", "HDDVDRIP", "DVDRIP", "RMVB" };
        private static string[] entertainment_subtitle = { "中文字幕", "英文字幕", "中英字幕", "暂无字幕", "无需字幕" };

        private static string[] software_language = { "英文", "中文", "多语言", "汉化", "日文" };
        private static string[] software_type = { "操作系统", "应用软件", "网络软件", "系统工具", "多媒体类", "行业软件", "编程开发", "安全相关" };
        private static string[] software_format = { "RAR", "ZIP", "ISO", "BIN", "EXE" };

        private static string[] staff_language = { "英文", "中文", "多语言", "日文" };
        private static string[] staff_format = { "RAR", "ZIP", "ISO", "BIN", "EXE", "SWF" };
        private static string[] staff_type = { "外语", "计算机", "考研", "史哲", "文学", "经管", "体育", "娱乐", "其他" };

        private static string[] video_type = { "游戏", "北航", "新闻", "军事", "搞笑", "宣传", "科技", "教育", "生活", "测评", "写真", "娱乐", "文化"};
        private static string[] video_language = { "国语", "粤语", "闽南", "日语", "英语", "韩语" };
        private static string[] video_region = { "台湾", "香港", "大陆", "日本", "美国", "韩国" };
        private static string[] video_format = { "MKV", "AVI", "TS", "RMVB", "WMV", "MP4", "3GP", "FLV", "AVI", "RMVB" };
        //private static string[] video_resolution = { "1080P", "1080i", "720P", "BDRIP", "HDDVDRIP", "DVDRIP", "SVGA", "VGA", "QVGA", "CIF", "QCIF"};
        private static string[] video_subtitle = { "中文字幕", "英文字幕", "中英字幕", "暂无字幕", "无需字幕" };

        private static string[] other_format = { "RAR", "ZIP", "ISO", "EXE", "FLV" };




        /// <summary>
        /// 生成包含指定类别选择信息的HTML，用户发布种子时方便填表
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string InfoSelectionList(string category)
        {
            string[] list;
            bool replace;
            //string html = "<a onclick=\"replaceField('','" + category + "');\" style=\"cursor:pointer\">重置</a>&nbsp;&nbsp;&nbsp;&nbsp;";
            string html = "";
            switch (category)
            {
                case "movie_type": list = movie_type; replace = false; break;
                case "movie_rank": list = movie_rank; replace = true; break;
                case "movie_resolution": list = movie_resolution; replace = true; break;
                case "movie_video": list = movie_video; replace = true; break;
                case "movie_region": list = movie_region; replace = true; break;
                case "movie_language": list = movie_language; replace = true; break;
                case "movie_subtitle": list = movie_subtitle; replace = true; break;
                case "movie_audio": list = movie_audio; replace = true; break;
                case "tv_region": list = tv_region; replace = true; break;
                case "tv_language": list = tv_language; replace = true; break;
                case "tv_resolution": list = tv_resolution; replace = true; break;
                case "tv_subtitle": list = tv_subtitle; replace = true; break;
                case "comic_region": list = comic_region; replace = true; break;
                case "comic_language": list = comic_language; replace = true; break;
                case "comic_source": list = comic_source; replace = true; break;
                case "comic_format": list = comic_format; replace = true; break;
                case "comic_subtitle": list = comic_subtitle; replace = true; break;
                case "comic_type": list = comic_type; replace = true; break;
                case "game_language": list = game_language; replace = true; break;
                case "game_platform": list = game_platform; replace = true; break;
                case "game_type": list = game_type; replace = true; break;
                case "game_format": list = game_format; replace = true; break;
                case "music_language": list = music_language; replace = true; break;
                case "music_region": list = music_region; replace = true; break;
                case "music_format": list = music_format; replace = true; break;
                case "music_company": list = music_company; replace = true; break;
                case "music_bps": list = music_bps; replace = true; break;
                case "music_type": list = music_type; replace = true; break;
                case "discovery_language": list = discovery_language; replace = true; break;
                case "discovery_type": list = discovery_type; replace = true; break;
                case "discovery_format": list = discovery_format; replace = true; break;
                case "discovery_resolution": list = discovery_resolution; replace = true; break;
                case "discovery_subtitle": list = discovery_subtitle; replace = true; break;
                case "sport_language": list = sport_language; replace = true; break;
                case "sport_type": list = sport_type; replace = true; break;
                case "sport_format": list = sport_format; replace = true; break;
                case "sport_resolution": list = sport_resolution; replace = true; break;
                case "sport_subtitle": list = sport_subtitle; replace = true; break;
                case "entertainment_language": list = entertainment_language; replace = true; break;
                case "entertainment_region": list = entertainment_region; replace = true; break;
                case "entertainment_format": list = entertainment_format; replace = true; break;
                case "entertainment_resolution": list = entertainment_resolution; replace = true; break;
                case "entertainment_subtitle": list = entertainment_subtitle; replace = true; break;
                case "software_language": list = software_language; replace = true; break;
                case "software_type": list = software_type; replace = true; break;
                case "software_format": list = software_format; replace = true; break;
                case "staff_language": list = staff_language; replace = true; break;
                case "staff_format": list = staff_format; replace = true; break;
                case "staff_type": list = staff_type; replace = true; break;
                case "video_language": list = video_language; replace = true; break;
                case "video_region": list = video_region; replace = true; break;
                case "video_format": list = video_format; replace = true; break;
                //case "video_resolution": list = video_resolution; replace = true; break;
                case "video_type": list = video_type; replace = true; break;
                case "video_subtitle": list = video_subtitle; replace = true; break;
                case "other_format": list = other_format; replace = true; break;
                default: list = blank; replace = true; break;
            }
            foreach (string s in list)
            {
                if (replace) html += "<a onclick=\"replaceField('" + s + "','" + category + "');\" class=\"PublishTag\">" + s + "</a>&nbsp;&nbsp;";
                else html += "<a onclick=\"addField('" + s + "','" + category + "');\" class=\"PublishTag\">" + s + "</a>&nbsp;&nbsp;";
            }
            return html;
        }

        /// <summary>
        /// 是否是测试用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool DEBUG_IsTestUser(int uid)
        {
            if (Utils.InArray(uid.ToString(), PrivateBTConfig.GetPrivateBTConfig().DebugUser)) return true;
            else return false;
        }

        /// <summary>
        /// 获取IP地址在北航校内的位置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static PTsIpRegionInBuaa GetIPRegion(string input)
        {
            if (input == "IP_NULL") return PTsIpRegionInBuaa.UNKNOWN_AREA;

            if (input.IndexOf(":") < 0 && input.IndexOf(".") > 0)
            {
                //IPv4地址处理
                string[] inputstr = input.Split('.');
                if (inputstr.Length != 4)
                {
                    PTLog.InsertSystemLog(PTLog.LogType.IPFormat, PTLog.LogStatus.Error, "IP错误", string.Format("IP:{0} -LEN:{1}", input, inputstr.Length));
                    return PTsIpRegionInBuaa.ERROR;
                }
                long ipint = (long)TypeConverter.StrToInt(inputstr[0]) * (long)16777216 + TypeConverter.StrToInt(inputstr[1]) * 65536 + TypeConverter.StrToInt(inputstr[2]) * 256 + TypeConverter.StrToInt(inputstr[3]);


                //判断是否为校内IP

                //沙河校区
                if (ipint > PTTools.GetIPv4Int(172, 16, 0, 0) && ipint < PTTools.GetIPv4Int(172, 16, 255, 255)) return PTsIpRegionInBuaa.SHAHE_DISTRICT;
                if (ipint > PTTools.GetIPv4Int(115, 25, 128, 0) && ipint < PTTools.GetIPv4Int(115, 25, 191, 255)) return PTsIpRegionInBuaa.SHAHE_DISTRICT;

                //大运村
                if (ipint > PTTools.GetIPv4Int(172, 31, 0, 0) && ipint < PTTools.GetIPv4Int(172, 31, 255, 255)) return PTsIpRegionInBuaa.DAYUNCUN_VILLAGE;

                //宿舍南区 192.168.0-95
                if (ipint > PTTools.GetIPv4Int(192, 168, 0, 0) && ipint < PTTools.GetIPv4Int(192, 168, 95, 255)) return PTsIpRegionInBuaa.DORMITORY_SOUTH;

                //宿舍北区 192.168.160-191
                if (ipint > PTTools.GetIPv4Int(192, 168, 160, 0) && ipint < PTTools.GetIPv4Int(192, 168, 191, 255)) return PTsIpRegionInBuaa.DORMITORY_NORTH;

                //教学区 192.168.96-159，192-255
                if (ipint > PTTools.GetIPv4Int(192, 168, 96, 0) && ipint < PTTools.GetIPv4Int(192, 168, 159, 255)) return PTsIpRegionInBuaa.TEACHING_AERA;
                if (ipint > PTTools.GetIPv4Int(192, 168, 192, 0) && ipint < PTTools.GetIPv4Int(192, 168, 255, 255)) return PTsIpRegionInBuaa.TEACHING_AERA;

                //新主楼
                if (ipint > PTTools.GetIPv4Int(219, 224, 128, 0) && ipint < PTTools.GetIPv4Int(219, 224, 192, 255)) return PTsIpRegionInBuaa.NEWMAIN_BUILDING;

                //服务器区
                if (ipint > PTTools.GetIPv4Int(202, 112, 136, 0) && ipint < PTTools.GetIPv4Int(202, 112, 137, 255)) return PTsIpRegionInBuaa.SERVER_CORE;
                if (ipint > PTTools.GetIPv4Int(10, 0, 0, 0) && ipint < PTTools.GetIPv4Int(10, 255, 255, 255)) return PTsIpRegionInBuaa.SERVER_CORE;

                //服务器区
                if (ipint > PTTools.GetIPv4Int(202, 112, 128, 0) && ipint < PTTools.GetIPv4Int(202, 112, 128, 255)) return PTsIpRegionInBuaa.SERVER_AERA;

                //家属区
                if (ipint > PTTools.GetIPv4Int(58, 195, 0, 0) && ipint < PTTools.GetIPv4Int(58, 195, 16, 255)) return PTsIpRegionInBuaa.LIVING_AREA;

                //图书馆
                if (ipint > PTTools.GetIPv4Int(202, 112, 128, 0) && ipint < PTTools.GetIPv4Int(202, 112, 143, 255)) return PTsIpRegionInBuaa.LIBRARY;
                if (ipint > PTTools.GetIPv4Int(211, 71, 0, 0) && ipint < PTTools.GetIPv4Int(211, 71, 15, 255)) return PTsIpRegionInBuaa.LIBRARY;
                if (ipint > PTTools.GetIPv4Int(58, 194, 224, 0) && ipint < PTTools.GetIPv4Int(58, 194, 231, 255)) return PTsIpRegionInBuaa.LIBRARY;

                //未知区域
                PTLog.InsertSystemLog(PTLog.LogType.IPFormat, PTLog.LogStatus.Error, "IP未知", string.Format("IP:{0}", input));
                return PTsIpRegionInBuaa.UNKNOWN_AREA;

            }
            else if (input.IndexOf(":") > 0)
            {
                //IPv6地址处理
                if (input.IndexOf("2001:0:") == 0) return PTsIpRegionInBuaa.NOT_IN_BUAA;
                else if (input.IndexOf("2001:da8:203") == 0)
                {
                    if (input.IndexOf("2001:da8:203:888:") == 0 || input.IndexOf("2001:da8:203:666:") == 0) return PTsIpRegionInBuaa.UNKNOWN_AREA;
                    else if (input.IndexOf(":0:5efe:") > 0) return PTsIpRegionInBuaa.UNKNOWN_AREA;
                    else return PTsIpRegionInBuaa.UNKNOWN_AREA;
                }
                else if (input.IndexOf("2001:da8:ae") == 0)
                {
                    return PTsIpRegionInBuaa.UNKNOWN_AREA;
                }
                else
                {
                    return PTsIpRegionInBuaa.NOT_IN_BUAA;
                }
            }
            else
            {
                PTLog.InsertSystemLog(PTLog.LogType.IPFormat, PTLog.LogStatus.Error, "IP错误", string.Format("IP:{0}", input));
                return PTsIpRegionInBuaa.ERROR;
            }
        }

       

        /// <summary>
        /// 由种子板块，返回其英文描述
        /// </summary>
        /// <param name="forumid"></param>
        /// <returns></returns>
        public static string Forum2Str(int forumid)
        {
            return Type2Str(Forum2Type(forumid));
        }
        /// <summary>
        /// 由种子板块，返回其中文描述
        /// </summary>
        /// <param name="forumid"></param>
        /// <returns></returns>
        public static string Forum2Nmae(int forumid)
        {
            return Type2Name(Forum2Type(forumid));
        }

        /// <summary>
        /// 检测是否该forum是种子发布区，返去其种子类别
        /// </summary>
        /// <param name="forumid"></param>
        /// <returns>大于零，则为具体发布区，0则为发布分区，小于零，非种子发布区</returns>
        public static int Forum2Type(int forumid)
        {
            switch (forumid)
            {
                case 16:
                    return 1;
                case 17:
                    return 2;
                case 19:
                    return 3;
                case 18:
                    return 4;
                case 21:
                    return 5;
                case 29:
                    return 6;
                case 22:
                    return 7;
                case 20:
                    return 8;
                case 32:
                    return 9;
                case 23:
                    return 10;
                case 30:
                    return 11;
                case 24:
                    return 12;
                case 6://全部种子发布区
                    return 0;
                default:
                    return -1;
            }
        }
        /// <summary>
        /// 由种子类别获得板块id
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int Type2Forum(int type)
        {
            switch (type)
            {
                case 1:
                    return 16;
                case 2:
                    return 17;
                case 3:
                    return 19;
                case 4:
                    return 18;
                case 5:
                    return 21;
                case 6:
                    return 29;
                case 7:
                    return 22;
                case 8:
                    return 20;
                case 9:
                    return 32;
                case 10:
                    return 23;
                case 11:
                    return 30;
                case 12:
                    return 24;
                case 0:
                    return 6;  
                default:
                    return -1;
            }
        }
        /// <summary>
        /// 由种子类别获得其中文描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Type2Name(int type)
        {
            switch (type)
            {
                case 1:
                    return "电影";
                case 2:
                    return "剧集";
                case 3:
                    return "动漫";
                case 4:
                    return "音乐";
                case 5:
                    return "游戏";
                case 6:
                    return "纪录";
                case 7:
                    return "体育";
                case 8:
                    return "综艺";
                case 9:
                    return "软件";
                case 10:
                    return "学习";
                case 11:
                    return "视频";
                case 12:
                    return "其他";
                case 0:
                    return "全部";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 由种子类别获得其英文描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Type2Str(int type)
        {
            switch (type)
            {
                case 1:
                    return "movie";
                case 2:
                    return "tv";
                case 3:
                    return "comic";
                case 4:
                    return "music";
                case 5:
                    return "game";
                case 6:
                    return "discovery";
                case 7:
                    return "sport";
                case 8:
                    return "entertainment";
                case 9:
                    return "software";
                case 10:
                    return "staff";
                case 11:
                    return "video";
                case 12:
                    return "other";
                case 0:
                    return "all";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 由种子英文描述获得其类别
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int Str2Type(string str)
        {
            switch (str)
            {
                case "movie":
                    return 1;
                case "tv":
                    return 2;
                case "comic":
                    return 3;
                case "music":
                    return 4;
                case "game":
                    return 5;
                case "discovery":
                    return 6;
                case "sport":
                    return 7;
                case "entertainment":
                    return 8;
                case "software":
                    return 9;
                case "staff":
                    return 10;
                case "video":
                    return 11;
                case "other":
                    return 12;
                case "all":
                    return 0;
                default:
                    return -1;
            }
        }


        public static void MessagePost(int sendtoid, string seedtoname, string seedtitle, string op, string opdetail, string reason, string opdate)
        {
            if (sendtoid == -1) //是游客，管理操作就不发短消息了
            {
                return;
            }

            PrivateMessageInfo privatemessageinfo = new PrivateMessageInfo();

            // 收件箱
            privatemessageinfo.Message =
                Utils.HtmlEncode(
                    string.Format(
                        "这是由论坛系统自动发送的通知短消息。\r\n以下您所发表的\r\n{0}\r\n被系统自动执行 {3} 操作。\r\n\r\n\r\n操作内容: {4}\r\n操作理由: {5}\r\n操作时间:{2}\r\n\r\n本操作由系统自动执行，请勿回复。",
                        seedtitle, "系统自动", opdate, op, opdetail, reason));
            privatemessageinfo.Subject = Utils.HtmlEncode(string.Format("系统自动删种通知：{0}被系统自动删除", seedtitle));
            privatemessageinfo.Msgto = seedtoname;
            privatemessageinfo.Msgtoid = sendtoid;
            privatemessageinfo.Msgfrom = "系统";
            privatemessageinfo.Msgfromid = 0;
            privatemessageinfo.New = 1;
            privatemessageinfo.Postdatetime = Utils.GetDateTime();
            privatemessageinfo.Folder = 0;
            PrivateMessages.CreatePrivateMessage(privatemessageinfo, 0);
        }

        /// <summary>
        /// 判断是否为保种号
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool IsServerUser(int Uid)
        {
            if (Uid == 5753 || Uid == 5695 || Uid == 5741 || Uid == 7442 || Uid == 2268 || Uid == 5744)
                return true;
            else return false;
        }

        /// <summary>
        /// 判断是否为无需发送短消息的用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool IsServerUserNoPM(int Uid)
        {
            // Core2 7742   IMAX 13   lxzylllsl 10597   Amphetamine 22854    Challenger 5695    TVReady 5741    H1N1 2268   Burninhell 5753
            if (Uid == 7742 || Uid == 13 || Uid == 10597 || Uid == 22854 || Uid == 5695 || Uid == 5741 || Uid == 2268 || Uid == 5753)
                return true;
            else return false;
        }

        /// <summary>
        /// 判断是否为保种号，种子禁止编辑
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public static bool IsServerUserSeedReadOnly(int Uid)
        {
            if (Uid == 2268 || Uid == 1 || Uid == 2 || Uid == 5753 || Uid == 5695 || Uid == 7442 || Uid == 5741) return true;
            else return false;
        }
     
        /// <summary>
        /// 数字转汉字，星期x
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetWeekDay(int i)
        {
            switch (i)
            {
                case 0: return "星期一";
                case 1: return "星期二";
                case 2: return "星期三";
                case 3: return "星期四";
                case 4: return "星期五";
                case 5: return "星期六";
                case 6: return "星期日";
                default: return "错误";
            }
        }
        /// <summary>
        /// 数字转汉字，星期x，字符串数字，无检测！
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetWeekDay(string i)
        {
            return GetWeekDay(int.Parse(i));
        }
        /// <summary>
        /// 从一个用“,”分隔的字符串中取值
        /// </summary>
        /// <param name="stringlist"></param>
        /// <returns></returns>
        public static string GetFirstString(ref string stringlist)
        {
            string returnvalue = "";
            int dotpos = stringlist.IndexOf(",");
            if(dotpos < 0)
            {
                returnvalue = stringlist;
                stringlist = "";
            }
            else if(dotpos == 0)
            {
                returnvalue = "";
                stringlist = stringlist.Substring(1);
            }
            else if(dotpos == stringlist.Length - 1)
            {
                returnvalue = stringlist.Substring(0, dotpos);
                stringlist = "";
            }
            else
            {
                returnvalue = stringlist.Substring(0, dotpos);
                stringlist = stringlist.Substring(dotpos + 1);
            }
            return returnvalue;
        }
        /// <summary>
        /// 获得蓝种或其他优惠截止提示字符串
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public static string GetRatioNote(PTSeedinfoShort seedinfo, double blueseedleft)
        {
            //蓝种剩余
            string rationote = "";
            if (seedinfo.DownloadRatio == 0)
                rationote = "蓝种 [剩余" + (PTTools.Minutes2String(blueseedleft)).Replace("时", "小时").Replace("分", "分钟") + "]";
            else if (seedinfo.DownloadRatio == 0.3)
                rationote = "30%下载 [剩余" + (PTTools.Minutes2String(blueseedleft)).Replace("时", "小时").Replace("分", "分钟") + "]";
            else if (seedinfo.DownloadRatio == 0.6)
                rationote = "60%下载";
            else if (blueseedleft < 4320)
                rationote = (seedinfo.DownloadRatio * 100).ToString("00") + "%下载 [剩余" + (PTTools.Minutes2String(blueseedleft)).Replace("时", "小时").Replace("分", "分钟") + "]";
            return rationote;
        }
        /// <summary>
        /// 获取流量系数提醒字符串，此函数会修改seedinfo数据！！！
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <param name="rtvaluetyep">0=流量系数+上传+下载提醒，1=(上传提醒)，2=(下载提醒)，3=蓝种提醒</param>
        /// <returns></returns>
        public static string GetRatioNote(PTSeedinfoShort seedinfo, int rtvaluetyep)
        {
            //问题函数，需要修改，暂时应用
            string uploadratioNote = "";
            string downloadratioNote = "";
            string blueseednote = "";

            
            //兼容历史数据：蓝种、30%下载最长有效期限制，发现老种子则更新数据
            if(rtvaluetyep == 0 || rtvaluetyep == 1)
            if (seedinfo.DownloadRatio <= 0.3 && seedinfo.DownloadRatioExpireDate > DateTime.Parse("2098-1-1"))
            {
                PTSeeds.UpdateSeedRatio(seedinfo.SeedId, seedinfo.DownloadRatio, seedinfo.PostDateTime.AddDays(60), seedinfo.UploadRatio, seedinfo.UploadRatioExpireDate);
            }


            //获取全站上传下载因子
            PrivateBTConfigInfo btconfig = PrivateBTConfig.GetPrivateBTConfig();
            if (btconfig == null) return "";
            //float globaluploadratio = 1.0f;
            //float globaldownloadratio = 1.0f;

            //全站流量系数优惠设置
            if (rtvaluetyep == 0 || rtvaluetyep == 1)
            if (btconfig.UploadMulti > seedinfo.UploadRatio && DateTime.Now > btconfig.UpMultiBeginTime && DateTime.Now < btconfig.UpMultiEndTime)
            {
                seedinfo.UploadRatio = btconfig.UploadMulti;
                seedinfo.UploadRatioExpireDate = btconfig.UpMultiEndTime;
                uploadratioNote = string.Format("全站优惠, 剩余{0}", PTTools.Minutes2String((btconfig.UpMultiEndTime - DateTime.Now).TotalMinutes));
            }
            if (rtvaluetyep == 0 || rtvaluetyep == 2 || rtvaluetyep == 3)
            if (btconfig.DownloadMulti < seedinfo.DownloadRatio && DateTime.Now > btconfig.DownMultiBeginTime && DateTime.Now < btconfig.DownMultiEndTime)
            {
                seedinfo.DownloadRatio = btconfig.DownloadMulti;
                seedinfo.DownloadRatioExpireDate = btconfig.DownMultiEndTime;
                downloadratioNote = string.Format("全站优惠, 剩余{0}", PTTools.Minutes2String((btconfig.DownMultiEndTime - DateTime.Now).TotalMinutes));
            }

            //流量系数过期信息
            if (rtvaluetyep == 0 || rtvaluetyep == 1)
            if (seedinfo.UploadRatioExpireDate < DateTime.Parse("2099-1-1") && uploadratioNote == "")
                uploadratioNote = string.Format("剩余{0}", PTTools.Minutes2String((seedinfo.UploadRatioExpireDate - DateTime.Now).TotalMinutes));
            
            if (rtvaluetyep == 0 || rtvaluetyep == 2)
            if (seedinfo.DownloadRatioExpireDate < DateTime.Parse("2099-1-1") && downloadratioNote == "")
                downloadratioNote = string.Format("剩余{0}", PTTools.Minutes2String((seedinfo.DownloadRatioExpireDate - DateTime.Now).TotalMinutes));

            if (rtvaluetyep == 2 || rtvaluetyep == 3)
            if (seedinfo.DownloadRatio == 0 && (seedinfo.DownloadRatioExpireDate - DateTime.Now).TotalMinutes < 10080)
                blueseednote = string.Format("请注意：系统将在{0}后取消该种子的蓝种状态", PTTools.Minutes2String((seedinfo.DownloadRatioExpireDate - DateTime.Now).TotalMinutes));

            //
            //if (seedinfo.TopicTitle.IndexOf("REMUX") > -1 || seedinfo.TopicTitle.IndexOf("1080P") > -1 || seedinfo.TopicTitle.IndexOf("remux") > -1 || seedinfo.TopicTitle.IndexOf("1080p") > -1 || seedinfo.TopicTitle.IndexOf("Remux") > -1 || seedinfo.TopicTitle.IndexOf("1080i") > -1)
            //    blueseednote += "<div class=\"threadstampseed2\" onclick=\"this.style.display='none';\"><img onload=\"setIdentify(this.parentNode);\" src=\"templates/default/images/bt/hdmovie.png\" alt=\"点击关闭播放器推荐\" title=\"点击关闭播放器推荐\" /></div>";

            //自己上传的种子两倍上传提醒
            if (seedinfo.UploadRatio == 2.0f && uploadratioNote == "")
            {
                uploadratioNote = "自己的种子，双倍上传";
            }


            if (rtvaluetyep == 0)
            {
                //获取图标链接【此处使用的绝对地址~~~~~~~~】
                string UpImgLink = (seedinfo.UploadRatio == 1.0f) ? "" : string.Format("<img class=\"PrivateBTInlineIMG\" src=\"templates/default/images/bt/U{0}.png\" title=\"按{0}0%计算上传流量 {1}\" />", (int)(seedinfo.UploadRatio * 10), seedinfo.Dis_UploadRatioNote);
                string DownImgLink = (seedinfo.DownloadRatio == 1.0f) ? "" : string.Format("<img class=\"PrivateBTInlineIMG\" src=\"templates/default/images/bt/D{0}.png\" title=\"按{0}0%计算下载流量 {1}\" />", (int)(seedinfo.DownloadRatio * 10), seedinfo.Dis_DownloadRatioNote);

                if (seedinfo.DownloadRatio != 1 && seedinfo.UploadRatio != 1)
                    return string.Format("{4} 按{0}%计算下载流量{1},  {5} 按{2}%计算上传流量{3}", (seedinfo.DownloadRatio * 100).ToString("0"), " " + downloadratioNote, (seedinfo.UploadRatio * 100).ToString("0"), " " + uploadratioNote, DownImgLink, UpImgLink);
                else if (seedinfo.DownloadRatio != 1 && seedinfo.UploadRatio == 1)
                    return string.Format("{2} 按{0}%计算下载流量{1}", (seedinfo.DownloadRatio * 100).ToString("0"), " " + downloadratioNote, DownImgLink);
                else if (seedinfo.DownloadRatio == 1 && seedinfo.UploadRatio != 1)
                    return string.Format("{2} 按{0}%计算上传流量{1}", (seedinfo.UploadRatio * 100).ToString("0"), " " + uploadratioNote, UpImgLink);
                else
                    return "";
            }
            else if (rtvaluetyep == 1 && uploadratioNote.Trim() != "") return string.Format("({0})", uploadratioNote);
            else if (rtvaluetyep == 2 && downloadratioNote.Trim() != "") return string.Format("({0})", downloadratioNote);
            else if (rtvaluetyep == 3 && blueseednote.Trim() != "") return string.Format("({0})", blueseednote);
            else return "";
        }
        ///// <summary>
        ///// 蓝种或其他优惠剩余时间，返回5000为当前无优惠
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <param name="topic"></param>
        ///// <returns></returns>
        //public static double GetRatioLeft(PTSeedinfoShort seedinfo, TopicInfo topic)
        //{
        //    //蓝种或其他优惠剩余时间
        //    double blueseedleft = 0;
        //    if (seedinfo.DownloadRatio == 0) blueseedleft = seedinfo.Bluehour * 60 - (DateTime.Now - DateTime.Parse(topic.Postdatetime)).TotalMinutes;
        //    else if (seedinfo.DownloadRatio == 0.3) blueseedleft = 720 * 60 - (DateTime.Now - DateTime.Parse(topic.Postdatetime)).TotalMinutes;
        //    //else if (seedinfo.DownloadRatio == 0.3 && (DateTime.Now - DateTime.Parse(topic.Postdatetime)).TotalHours < 72)
        //    //    blueseedleft = 24 * 60 - (DateTime.Now - DateTime.Parse(topic.Postdatetime)).TotalMinutes;
        //    else blueseedleft = 5000;

        //    if (blueseedleft < 0) blueseedleft = 0;

        //    return blueseedleft;
        //}
        ///// <summary>
        ///// 获得蓝种或其他优惠截止提示字符串
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <param name="topic"></param>
        ///// <returns></returns>
        //public static string GetRatioNote(PrivateBTSeedInfo seedinfo, TopicInfo topic)
        //{
        //    return GetRatioNote(seedinfo, GetRatioLeft(seedinfo, topic));
        //}
    }
}
