using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Common.Generic;

namespace Discuz.Web
{
    /// <summary>
    /// 查看用户信息页
    /// </summary>
    public class userinfo : PageBase
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user;
        /// <summary>
        /// 当前用户用户组信息
        /// </summary>
        public UserGroupInfo group;
        /// <summary>
        /// 当前用户管理组信息
        /// </summary>
        public AdminGroupInfo admininfo;
        /// <summary>
        /// 可用的扩展积分名称列表
        /// </summary>
        public string[] score;
        /// <summary>
        /// 是否需要快速登录
        /// </summary>
        public bool needlogin = false;

        public string score1, score2, score3, score4, score5, score6, score7, score8;

        public int id = DNTRequest.GetInt("userid", -1);

        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
        //【BT修改】增加realup，realdown

        public string realup, realdown;

        //【END BT修改】
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 

        protected override void ShowPage()
        {
            pagetitle = "查看用户信息";

            if (usergroupinfo.Allowviewpro != 1 && userid != id)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有查看用户资料的权限", usergroupinfo.Grouptitle));
                if (userid < 1)
                    needlogin = true;

                return;
            }

            if (Utils.StrIsNullOrEmpty(DNTRequest.GetString("username")) && Utils.StrIsNullOrEmpty(DNTRequest.GetString("userid")))
            {
                AddErrLine("错误的URL链接");
                return;
            }



            if (id == -1)
                id = Users.GetUserId(Utils.UrlDecode(DNTRequest.GetString("username")));

            if (id == -1)
            {
                AddErrLine("该用户不存在");
                return;
            }

            user = Users.GetUserInfo(id);
            if (user == null)
            {
                AddErrLine("该用户不存在");
                return;
            }

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】更新共享率、积分数据，所有更新必须使用user变量，因为更新的是他人的数据！
            
            user.Ratio = PTTools.GetRatio(user.Extcredits3, user.Extcredits4);
            user.UpCount = PTSeeds.GetSeedInfoCount(0, user.Uid, 1, 0, "", 0, "");
            user.DownCount = PTSeeds.GetSeedInfoCount(0, user.Uid, 2, 0, "", 0, "");
            UserCredits.UpdateUserCredits(id); //更新总积分
            PTUsers.UpdateUserInfo_Ratio(id, user.Ratio, user.UpCount, user.DownCount);
            user = Users.GetUserInfo(id);

            user.Ratio = PTTools.GetFloorDot2(user.Ratio);
            btuserinfo.Ratio = (float)user.Ratio;
            
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 


            //用户设定Email保密时，清空用户的Email属性以避免被显示
            if (user.Showemail != 1 && id != userid)
                user.Email = "";

            //获取积分机制和用户组信息，底层有缓存
            score = Scoresets.GetValidScoreName();
            group = UserGroups.GetUserGroupInfo(user.Groupid);
            admininfo = AdminUserGroups.AdminGetAdminGroupInfo(usergroupid);
            score1 = ((decimal)user.Extcredits1).ToString();
            score2 = ((decimal)user.Extcredits2).ToString();

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】流量

            score3 = PTTools.Upload2Str((decimal)user.Extcredits3);
            score4 = PTTools.Upload2Str((decimal)user.Extcredits4);
            score5 = PTTools.Upload2Str((decimal)user.Extcredits5);
            score6 = PTTools.Upload2Str((decimal)user.Extcredits6);

            //原始
            //score3 = ((decimal)user.Extcredits3).ToString();
            //score4 = ((decimal)user.Extcredits4).ToString();
            //score5 = ((decimal)user.Extcredits5).ToString();
            //score6 = ((decimal)user.Extcredits6).ToString();
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 

            score7 = PTTools.Upload2Str((decimal)user.Extcredits9, false);
            score8 = PTTools.Upload2Str((decimal)user.Extcredits10, false);

            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】增加realup，realdown

            realup = PTTools.Upload2Str(user.Extcredits7, false);
            realdown = PTTools.Upload2Str(user.Extcredits8, false);

            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
        }
    }
}
