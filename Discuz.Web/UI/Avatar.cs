using System;
using System.Data;
using System.Data.Common;
using System.Web;

using Discuz.Forum;
using Discuz.Common;
using System.IO;

namespace Discuz.Web.UI
{
	/// <summary>
	/// 头像页面类
	/// </summary>
	public class Avatar : PageBase
	{
        public Avatar()
		{
            AvatarSize avatarSize;
            switch (DNTRequest.GetString("size").ToLower())
            {
                case "large":
                    {
                        avatarSize = AvatarSize.Large;
                        break;
                    }
                case "medium":
                    {
                        avatarSize = AvatarSize.Medium;
                        break;
                    }
                case "small":
                    {
                        avatarSize = AvatarSize.Small;
                        break;
                    }
                default:
                    {
                        avatarSize = AvatarSize.Medium;
                        break;
                    }
            }
            string avatarUrl = Avatars.GetAvatarUrl(DNTRequest.GetString("uid"), avatarSize);
            
            try
            {
                //【BT修改】不能redirect的临时解决方案，redirect失败后用transfer，再失败则返回
                HttpContext.Current.Response.Redirect(avatarUrl);
            }
            catch
            {
                System.Threading.Thread.Sleep(500);
                try
                {
                    Server.Transfer(avatarUrl);
                }
                catch
                {
                    return;
                }
                
            }
           
		}
	}
}
