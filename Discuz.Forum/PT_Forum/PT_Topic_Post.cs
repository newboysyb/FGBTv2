using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
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
        public static bool CheckTitle(int fid, string title)
        {
            if (IsInfoPublishForum(fid))
            {
                if (title.IndexOf("】") > -1 || title.IndexOf("【") > -1 || title.IndexOf("。。。") > -1 || title.IndexOf("!!!") > -1 || title.IndexOf("！！！") > -1 || title.IndexOf("...") > -1 || title.IndexOf("~~~") > -1)
                    return false;
                else return true;
            }
            else return true;
        }

        /// <summary>
        /// 是否是信息发布类板块，这些板块禁止使用特殊字符、禁止设置帖子售价，禁止回复可见
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public static bool IsInfoPublishForum(int fid)
        {
            return (",2,4,35,37,38,46,47,48,49,50,54,56,58,".IndexOf("," + fid.ToString() + ",") > -1);
        }

        /// <summary>
        /// 获得指定TopicId对应的seedid
        /// </summary>
        /// <param name="topicid"></param>
        /// <returns></returns>
        public static int GetSeedIdByTopicId(int topicid)
        {
            if (topicid > 0) return DatabaseProvider.GetInstance().GetSeedIdByTopicId(topicid);
            else return -1;
        }
        public static void ReSetAllFourmTopicStatic()
        {
            List<ForumInfo> forumlist = Forums.GetForumList();
            foreach (ForumInfo forum in forumlist)
            {
                ReSetFourmTopicAPost(forum.Fid);
            }
            forumlist = null;
        }
        /// <summary>
        /// 重建指定论坛帖数
        /// </summary>
        public static void ReSetFourmTopicAPost(int fid)
        {
            int topiccount = 0;
            int postcount = 0;
            //int lasttid = 0;
            //string lasttitle = "";
            //string lastpost = "";
            //int lastposterid = 0;
            //string lastposter = "";
            int todaypostcount = 0;
            if (fid < 1)
            {
                return;
            }

            topiccount = Topics.GetTopicCount(fid);
            postcount = AdminForumStats.GetPostsCountByFid(fid, out todaypostcount);

            //下面这段代码毫无用处啊。。。没有引用。。。
            //IDataReader postreader = DatabaseProvider.GetInstance().GetLastPostByFid(fid, Posts.GetPostTableName());
            //if (postreader.Read())
            //{
            //    lasttid = Utils.StrToInt(postreader["tid"], 0);
            //    lasttitle = Topics.GetTopicInfo(lasttid).Title;//postreader["title"].ToString();
            //    lastpost = postreader["postdatetime"].ToString();
            //    lastposterid = Utils.StrToInt(postreader["posterid"], 0);
            //    lastposter = postreader["poster"].ToString();
            //}
            //postreader.Close();
            //postreader.Dispose();

            DatabaseProvider.GetInstance().UpdateForumStatic(fid, topiccount, postcount, todaypostcount);

        }
        /// <summary>
        /// 获取指定条件的帖子pid在帖子中的排序，此函数需要读取已经保存的帖子内容！！！
        /// </summary>
        /// <param name="_postpramsinfo">参数列表</param>
        /// <returns></returns>
        public static int GetPostCountInTopic(int Tid, int Pid)
        {
            return DatabaseProvider.GetInstance().GetPostCountInTopic(Tid, Pid, Posts.GetPostTableIdArray(Tid, Tid)[0]);
        }

        /// <summary>
        /// 帖子中” @ “功能，发送提醒
        // 使用系统的自定义Discuz代码功能，增加[atuser]自定义代码，替换为<a class="atuser" href="userinfo.aspx?username={1}" target="_blank">@{1}</a>，
        // 同时修改javascript文件件中的bbcode.js，添加显示此自定义代码的功能
        // bbcode.js,bbcode2html,str = str.replace(/\[atuser\](.*?)\[\/atuser\]/ig, '  <a class="atuser" href="userinfo.aspx?username=$1 target="_blank">@$1</a>  ');
        // bbcode.js,html2bbcode/html2bbcode1,str = str.replace(/<a\s+?class="atuser"\s+?href="userinfo.aspx\?username=.*?">@(.*?)<\/a>/ig, "[atuser]$1[/atuser]");
        /// </summary>
        /// <param name="postInfo"></param>
        /// <returns></returns>
        public static int GetPostContentAtUser(int pid, int tid)
        {
            PostInfo postInfo = Posts.GetPostInfo(tid, pid);
            TopicInfo topic = Topics.GetTopicInfo(postInfo.Tid);
            postInfo.Message = PTTools.BlankFilter(postInfo.Message);

            //标记全部Email地址，防止错误识别
            Regex r0 = new Regex(@"\w{1,}@\w{1,}\.\w{1,}(\.\w{1,})?(\.\w{1,})?");
            int emailcount = 0;
            if (r0.IsMatch(postInfo.Message))
            {
                //获取Email匹配的字符串
                MatchCollection gp = r0.Matches(postInfo.Message);
                for (int i = 0; i < gp.Count; i++)
                {
                    if (gp[i].Success)
                    {
                        string tmp = gp[i].Value;
                        postInfo.Message = postInfo.Message.Replace(tmp, tmp.Replace("@", "<EMAILATREPLACEPART>"));
                        emailcount++;
                    }
                }
            }

            string modMessage = postInfo.Message;


            //标记[code]标签中的@，此部分不识别
            Regex r0a = new Regex(@"\[code\]([\s\S]*?)@([\s\S]*?)\[\/code\]");
            int codecount = 0;
            if (r0a.IsMatch(modMessage))
            {
                //获取[code]xx[/code]匹配的字符串
                MatchCollection gp = r0a.Matches(modMessage);
                for (int i = 0; i < gp.Count; i++)
                {
                    if (gp[i].Success)
                    {
                        string tmp = gp[i].Value;
                        modMessage = modMessage.Replace(tmp, tmp.Replace("@", "<CODEATREPLACEPART>"));
                        codecount++;
                    }
                }
            }

            //new Regex(@"\[code\]([\s\S]+?)@([\s\S]+?)\[\/code\]")
            //用户名中不允许存在的字符都不检测，只允许普通字符@[^\r\n\s,,，:\-;\\\/\(\)\[\]\{\}%@\*!\'\<\>*$\&]{1,20}
            //测试字符串 
            // www@asee.buaa.edu.cn
            // @zzz.n
            // @buaa
            //@dddd,a
            //@,aa
            //@dfsdfsf.zzzz
            //@sdfsdf-dfdf
            //@abce.efgh@zz 

            //Regex r1 = new Regex(@"@.[^\r\n\s,，:-;\\\/\(\)\[\]\{\}%@\*!\'\<\>*$\&]{1,20}?([\s,，]|\b|$)", RegexOptions.Multiline);
            //Regex r1a = new Regex(@"\[url=atuser:([^\s\[\""']+?)\]@([\s\S]+?)\[\/url\]");

            //所有直接输入的[atuser]标签，替换为@
            int directcount = 0;
            Regex r1c = new Regex(@"\[atuser\](?<t>[^\s\[\""']+?)\[/atuser\]");
            if (r1c.IsMatch(modMessage))
            {
                modMessage = r1c.Replace(modMessage, "@$1");
                directcount++;
            }


            int atcount = 0;
            //替换编辑时已经识别的url=atuser:xxxx
            Regex r1a = new Regex(@"\[url=atuser:(?<t>[^\s\[\""']+?)\]@\k<t>\[\/url\]");
            if (r1a.IsMatch(modMessage))
            {

                modMessage = r1a.Replace(modMessage, "[atuser]$1[/atuser]");
                atcount++;
            }


            //标记[url]标签中的@，此部分不识别（正常的at已经在上面替换）
            Regex r0b = new Regex(@"\[url([^\]]+?)\]([^\]]*@[^\]]*)\[\/url\]");
            int urlcount = 0;
            if (r0b.IsMatch(modMessage))
            {
                //获取[code]xx[/code]匹配的字符串
                MatchCollection gp = r0b.Matches(modMessage);
                for (int i = 0; i < gp.Count; i++)
                {
                    if (gp[i].Success)
                    {
                        string tmp = gp[i].Value;
                        modMessage = modMessage.Replace(tmp, tmp.Replace("@", "<URLATREPLACEPART>"));
                        urlcount++;
                    }
                }
            }

            //Prométhée
            if (modMessage.IndexOf("@") > -1)
            {
                string htmlencodemessage = Utils.HtmlDecode(modMessage); 
                Regex r1 = new Regex(@"@[^\r\n\s,,，:\-;\\\/\(\)\[\]\{\}%@\*!\'\<\>*$\&]{1,20}", RegexOptions.Multiline);
                if (r1.IsMatch(htmlencodemessage))
                {
                    //获取AT匹配的字符串
                    MatchCollection gp = r1.Matches(htmlencodemessage);
                    int indexadd = 0;
                    //string oldMessage = modMessage;
                    string atstr = "";
                    for (int i = 0; i < gp.Count && i < 50; i++)
                    {
                        if (gp[i].Success)
                        {
                            string tmp = gp[i].Value.Replace("@", "").Replace(",", "").Replace(" ", "").Replace("，", "").Replace("\n", "").Replace("\r", "");
                            if (("@" + tmp) == gp[i].Value && Utils.IsSafeSqlString(tmp) && Utils.IsSafeUserInfoString(tmp))
                            {
                                int atuserid = Users.GetUserId(tmp);
                                if (atuserid > 0)
                                {
                                    SendPostAtNotice(postInfo, topic, atuserid);
                                    atcount++;
                                    //modMessage = modMessage.Replace("@" + tmp, string.Format(" [atuser]{0}[/atuser] ", tmp));
                                    atstr = string.Format(" [atuser]{0}[/atuser] ", tmp);
                                    htmlencodemessage = htmlencodemessage.Substring(0, gp[i].Index + indexadd) + atstr + htmlencodemessage.Substring(gp[i].Index + indexadd + gp[i].Length);
                                    indexadd += atstr.Length - gp[i].Length;
                                }
                            }
                        }
                    }
                }
                if (directcount > 0 || atcount > 0)
                {
                    modMessage = htmlencodemessage;

                    if (emailcount > 0)
                    {
                        modMessage = modMessage.Replace("<EMAILATREPLACEPART>", "@");
                    }
                    if (codecount > 0)
                    {
                        modMessage = modMessage.Replace("<CODEATREPLACEPART>", "@");
                    }
                    if (urlcount > 0)
                    {
                        modMessage = modMessage.Replace("<URLATREPLACEPART>", "@");
                    }

                    //replace后在编码
                    postInfo.Message = Utils.HtmlEncode(modMessage);
                    Data.Posts.UpdatePost(postInfo, Posts.GetPostTableId(postInfo.Tid));

                    return atcount;
                } 
            }

            //存在直接输入[atuser]标签，但是转换为@后没有成功的情况，更新帖子信息，避免存储直接输入的[atuser]标签
            //编辑时原文中存在已经识别的at标签，需要更新
            if (directcount > 0 || atcount > 0)
            {
                if (emailcount > 0)
                {
                    modMessage = modMessage.Replace("<EMAILATREPLACEPART>", "@");
                }
                if (codecount > 0)
                {
                    modMessage = modMessage.Replace("<CODEATREPLACEPART>", "@");
                }
                if (urlcount > 0)
                {
                    modMessage = modMessage.Replace("<URLATREPLACEPART>", "@");
                }
                postInfo.Message = modMessage;
                Data.Posts.UpdatePost(postInfo, Posts.GetPostTableId(postInfo.Tid));
            }

            return atcount;
        }
        /// <summary>
        /// 发送回复通知
        /// </summary>
        /// <param name="postinfo">回复信息</param>
        /// <param name="topicinfo">所属主题信息</param>
        /// <param name="replyuserid">回复的某一楼的作者</param>
        public static void SendPostAtNotice(PostInfo postinfo, TopicInfo topicinfo, int atuserid)
        {
            NoticeInfo noticeinfo = new NoticeInfo();

            noticeinfo.Note = Utils.HtmlEncode(string.Format("<span style=\"color:#90C;\"><a href=\"userinfo.aspx?userid={0}\" style=\"color:#90C;\">{1}</a> 在以下帖子中提到了你： </span><a href =\"showtopic.aspx?topicid={2}&postid={3}#{3}\" style=\"color:#90C;\">{4}</a>.", postinfo.Posterid, postinfo.Poster, topicinfo.Tid, postinfo.Pid, topicinfo.Title));
            noticeinfo.Type = NoticeType.UserAt;
            noticeinfo.New = 1;
            noticeinfo.Posterid = postinfo.Posterid;
            noticeinfo.Poster = postinfo.Poster;
            noticeinfo.Postdatetime = Utils.GetDateTime();
            noticeinfo.Fromid = topicinfo.Tid;
            noticeinfo.Uid = atuserid;

            Notices.CreateNoticeInfo(noticeinfo);
        }

        /// <summary>
        /// 踩整奖励功能
        /// </summary>
        /// <param name="postinfo"></param>
        public static void SendZeroAward(PostInfo postinfo)
        {

        }

        /// <summary>
        ///  更新指定uid和nid的通知的状态
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newType"></param>
        public static void UpdateNoticeNewByUidNid(int uid, int nid, int newType)
        {
            DatabaseProvider.GetInstance().UpdateNoticeNewByUidNid(uid, nid, newType);
        }
        /// <summary>
        ///  更新指定uid和type的通知的状态，参考NoticeType
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newType"></param>
        public static void UpdateNoticeNewByUidType(int uid, string type, int newType)
        {
            if (type == "all" || type == "")
            {
                DatabaseProvider.GetInstance().UpdateNoticeNewByUid(uid, newType);
            }
            else if (type == "spacecomment")
            {
                DatabaseProvider.GetInstance().UpdateNoticeNewByUidType(uid, 3, newType);
            }
            else if (type == "albumcomment")
            {
                DatabaseProvider.GetInstance().UpdateNoticeNewByUidType(uid, 2, newType);
            }
            else if (type == "postreply")
            {
                DatabaseProvider.GetInstance().UpdateNoticeNewByUidType(uid, 1, newType);
            }
            else if (type == "topicadmin")
            {
                DatabaseProvider.GetInstance().UpdateNoticeNewByUidType(uid, 9, newType);
            }
        }
    }
}