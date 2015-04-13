<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.showtopiclist" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>
<%@ Import namespace="Discuz.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2015/1/7 16:52:17.
		本页面代码由Discuz!NT模板引擎生成于 2015/1/7 16:52:17. 
	*/

	base.OnInit(e);

	templateBuilder.Capacity = 220000;



	if (infloat!=1)
	{

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    ");
	if (pagetitle=="首页")
	{

	templateBuilder.Append("\r\n        <title>");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append(" ");
	templateBuilder.Append(config.Seotitle.ToString().Trim());
	templateBuilder.Append(" - Powered by Discuz!NT</title>\r\n    ");
	}
	else
	{

	templateBuilder.Append("\r\n        <title>");
	templateBuilder.Append(pagetitle.ToString());
	templateBuilder.Append(" - ");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append(" ");
	templateBuilder.Append(config.Seotitle.ToString().Trim());
	templateBuilder.Append(" - Powered by Discuz!NT</title>\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n    ");
	templateBuilder.Append(meta.ToString());
	templateBuilder.Append("\r\n    <meta name=\"generator\" content=\"Discuz!NT 3.6.711\" />\r\n    <meta name=\"author\" content=\"Discuz!NT Team and Comsenz UI Team\" />\r\n    <meta name=\"copyright\" content=\"2001-2011 Comsenz Inc.\" />\r\n    <meta http-equiv=\"x-ua-compatible\" content=\"edge\" />\r\n    <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(cssdir.ToString());
	templateBuilder.Append("/privatebt.css?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\" type=\"text/css\" media=\"all\"  />\r\n    <link rel=\"icon\" href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("favicon.ico\" type=\"image/x-icon\" />\r\n    <link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("favicon.ico\" type=\"image/x-icon\" />\r\n    ");
	if (pagename!="website.aspx")
	{

	templateBuilder.Append("\r\n        <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(cssdir.ToString());
	templateBuilder.Append("/dnt.css?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\" type=\"text/css\" media=\"all\" />\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n    <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(cssdir.ToString());
	templateBuilder.Append("/float.css?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\" type=\"text/css\" />\r\n    ");
	if (isnarrowpage)
	{

	templateBuilder.Append("\r\n        <link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(cssdir.ToString());
	templateBuilder.Append("/widthauto.css?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\" id=\"css_widthauto\" />\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n    ");
	templateBuilder.Append(link.ToString());
	templateBuilder.Append("\r\n    <script type=\"text/javascript\">\r\n        var creditnotice='");
	templateBuilder.Append(Scoresets.GetValidScoreNameAndId().ToString().Trim());
	templateBuilder.Append("';	\r\n        var forumpath = \"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("\";\r\n    </");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/futuregarden.js?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(config.Jqueryurl.ToString().Trim());
	templateBuilder.Append("?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\">jQuery.noConflict();</");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/common.js?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_report.js?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_utils.js?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/ajax.js?");
	templateBuilder.Append(CSSJSVersion.ToString());
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\">\r\n	    var aspxrewrite = ");
	templateBuilder.Append(config.Aspxrewrite.ToString().Trim());
	templateBuilder.Append(";\r\n	    var IMGDIR = '");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("';\r\n        var disallowfloat = '");
	templateBuilder.Append(config.Disallowfloatwin.ToString().Trim());
	templateBuilder.Append("';\r\n	    var rooturl=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("\";\r\n	    var imagemaxwidth='");
	templateBuilder.Append(Templates.GetTemplateWidth(templatepath).ToString().Trim());
	templateBuilder.Append("';\r\n	    var cssdir='");
	templateBuilder.Append(cssdir.ToString());
	templateBuilder.Append("';\r\n    </");
	templateBuilder.Append("script>\r\n    ");
	templateBuilder.Append(script.ToString());
	templateBuilder.Append("\r\n</head>");

	templateBuilder.Append("\r\n<body onkeydown=\"if(event.keyCode==27) return false;\">\r\n<div id=\"append_parent\"></div><div id=\"ajaxwaitid\"></div>\r\n<div id=\"submenu\">\r\n  ");
	if (userid>0&&pagename!="login.aspx"&&pagename!="logout.aspx"&&pagename!="register.aspx"&&pagename!="cngilogin.aspx")
	{

	templateBuilder.Append("\r\n	<div class=\"wrap s_clear\">\r\n		  <span class=\"submenuright\">\r\n        ");
	templateBuilder.Append(ipaddress_note.ToString());
	templateBuilder.Append("\r\n    </span>\r\n		欢迎 <a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("userinfo.aspx?userid=");
	templateBuilder.Append(userid.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(username.ToString());
	templateBuilder.Append("</a>\r\n  </div>\r\n  ");
	}	//end if

	templateBuilder.Append("\r\n</div>\r\n\r\n");
	if (headerad!="")
	{

	templateBuilder.Append("\r\n	<div id=\"ptnotice_headerbanner\">");
	templateBuilder.Append(headerad.ToString());
	templateBuilder.Append("</div>\r\n");
	}	//end if

	templateBuilder.Append("\r\n<div id=\"hd\">\r\n	<div class=\"wrap\">\r\n		<div class=\"head cl\">\r\n			<h2><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("index.aspx\" title=\"");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/logo.png\" alt=\"");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("\"/></a></h2>\r\n			");
	if (userid>0)
	{

	templateBuilder.Append("\r\n			<div id=\"um\">\r\n				<div class=\"avt y\"><a alt=\"用户名称\" target=\"_blank\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercp.aspx\"><img src=\"");
	templateBuilder.Append(useravatar.ToString());
	templateBuilder.Append("\" onerror=\"this.onerror=null;this.src='");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("images/common/noavatar_small.gif';\" /></a></div>\r\n				<p>\r\n                    ");	string linktitle = "";
	
	string showoverflow = "";
	

	if (oluserinfo.Newpms>0)
	{


	if (oluserinfo.Newpms>=1000)
	{

	 showoverflow = "大于";
	

	}	//end if

	 linktitle = "您有"+showoverflow+oluserinfo.Newpms+"条新短消息";
	

	}
	else
	{

	 linktitle = "您没有新短消息";
	

	}	//end if

	templateBuilder.Append("\r\n					<a id=\"pm_ntc\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpinbox.aspx\" title=\"");
	templateBuilder.Append(linktitle.ToString());
	templateBuilder.Append("\">短消息\r\n                    ");
	if (oluserinfo.Newpms>0 && oluserinfo.Newpms<=1000)
	{

	templateBuilder.Append("\r\n                                (");
	templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
	if (oluserinfo.Newpms>=1000)
	{

	templateBuilder.Append("1000+");
	}	//end if

	templateBuilder.Append(")\r\n                    ");
	}	//end if

	templateBuilder.Append("</a>\r\n                    <span class=\"pipe\">|</span>\r\n                    ");	 showoverflow = "";
	

	if (oluserinfo.Newnotices>0)
	{


	if (oluserinfo.Newnotices>=1000)
	{

	 showoverflow = "大于";
	

	}	//end if

	 linktitle = "您有"+showoverflow+oluserinfo.Newnotices+"条新通知";
	

	}
	else
	{

	 linktitle = "您没有新通知";
	

	}	//end if

	templateBuilder.Append("\r\n					<a id=\"notice_ntc\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpnotice.aspx?filter=all\" title=\"");
	templateBuilder.Append(linktitle.ToString());
	templateBuilder.Append("\">\r\n                        通知");
	if (oluserinfo.Newnotices>0)
	{

	templateBuilder.Append("\r\n                                (");
	templateBuilder.Append(oluserinfo.Newnotices.ToString().Trim());
	if (oluserinfo.Newnotices>=1000)
	{

	templateBuilder.Append("+");
	}	//end if

	templateBuilder.Append(")\r\n                            ");
	}	//end if

	templateBuilder.Append("\r\n                    </a>\r\n                    <span class=\"pipe\">|</span>\r\n					<a id=\"usercenter\" class=\"drop\" onmouseover=\"showMenu(this.id);\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercp.aspx\">用户中心</a>\r\n					");
	if (cngi_login==true)
	{

	templateBuilder.Append("\r\n              <span class=\"pipe\">|</span><a href=\"javascript:alert('CNGI登陆用户只有关闭浏览器才能退出登录，如果需要退出登录，请关闭浏览器')\">退出</a>\r\n					");
	}
	else
	{

	templateBuilder.Append("\r\n              <span class=\"pipe\">|</span><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("logout.aspx?userkey=");
	templateBuilder.Append(userkey.ToString());
	templateBuilder.Append("\">退出</a>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n				</p>\r\n				");
	templateBuilder.Append(userinfotips.ToString());
	templateBuilder.Append("\r\n			</div> \r\n			<div id=\"pm_ntc_menu\" class=\"g_up\" style=\"display:none;\">\r\n				<div class=\"mncr\"></div>\r\n				<div class=\"crly\">\r\n					<div style=\"clear:both;font-size:0;\"></div>\r\n					<span class=\"y\"><a onclick=\"javascript:$('pm_ntc_menu').style.display='none';closenotice(");
	templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
	templateBuilder.Append(");\" href=\"javascript:;\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/delete.gif\" alt=\"关闭\"/></a></span>\r\n					<a id=\"pmcountshow\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpinbox.aspx\">您有");
	if (oluserinfo.Newpms>=1000)
	{

	templateBuilder.Append("大于");
	}	//end if
	templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
	templateBuilder.Append("条新消息</a>\r\n				</div>\r\n			</div>\r\n			<div id=\"notice_ntc_menu\" class=\"g_up\" style=\"display:none;\">\r\n				<div class=\"mncr\"></div>\r\n				<div class=\"crly\">\r\n					<div style=\"clear:both;font-size:0;\"></div>\r\n					<span class=\"y\"><a onclick=\"javascript:$('notice_ntc_menu').style.display='none';closenotice(");
	templateBuilder.Append(oluserinfo.Newnotices.ToString().Trim());
	templateBuilder.Append(");\" href=\"javascript:;\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/delete.gif\" alt=\"关闭\"/></a></span>\r\n					<a id=\"noticecountshow\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpnotice.aspx?filter=all\">您有");
	if (oluserinfo.Newnotices>=1000)
	{

	templateBuilder.Append("大于");
	}	//end if
	templateBuilder.Append(oluserinfo.Newnotices.ToString().Trim());
	templateBuilder.Append("条新通知</a>\r\n				</div>\r\n			</div>\r\n\r\n            <span id=\"msgsoundplayerspanheader\" style=\"display:hidden\"></span>\r\n            <script type=\"text/javascript\">\r\n                var pmcount = ");
	templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
	templateBuilder.Append(";\r\n                var noticecount = ");
	templateBuilder.Append(oluserinfo.Newnotices.ToString().Trim());
	templateBuilder.Append(";\r\n                var originalTitle =  document.title;\r\n                var playpmsound = ");
	if (oluserinfo.PMSound>0)
	{

	templateBuilder.Append("true");
	}
	else
	{

	templateBuilder.Append("false");
	}	//end if

	templateBuilder.Append(";\r\n                setnewpmnoticeposion(");
	templateBuilder.Append(oluserinfo.Newnotices.ToString().Trim());
	templateBuilder.Append(", ");
	templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
	templateBuilder.Append(");\r\n                initpmnoticeupdate();\r\n\r\n            </");
	templateBuilder.Append("script>\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n		</div>\r\n		<div id=\"menubar\">\r\n			<a onMouseOver=\"showMenu(this.id, false);\" href=\"javascript:void(0);\" id=\"mymenu\">我的中心</a>\r\n            <div class=\"popupmenu_popup headermenu_popup\" id=\"mymenu_menu\" style=\"display: none\">\r\n                 ");
	if (userid>0)
	{

	templateBuilder.Append("\r\n                    <ul class=\"sel_my\">\r\n                      <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("mytopics.aspx\">我的主题</a></li>\r\n                      <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("myposts.aspx\">我的帖子</a></li>\r\n                      <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("search.aspx?posterid=current&type=digest&searchsubmit=1\">我的精华</a></li>\r\n                      <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("myattachment.aspx\">我的附件</a></li>\r\n                      <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpsubscribe.aspx\">我的收藏</a></li>\r\n                    ");
	if (config.Enablespace==1)
	{

	templateBuilder.Append("\r\n                          <li class=\"myspace\"><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("space/\">我的空间</a></li>\r\n                    ");
	}	//end if


	if (config.Enablealbum==1)
	{

	templateBuilder.Append("\r\n                          <li class=\"myalbum\"><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showalbumlist.aspx?uid=");
	templateBuilder.Append(userid.ToString());
	templateBuilder.Append("\">我的相册</a></li>\r\n                    ");
	}	//end if

	templateBuilder.Append("\r\n                      </ul>\r\n                 ");
	}
	else
	{


	}	//end if


	if (config.Allowchangewidth==1&&pagename!="website.aspx")
	{

	templateBuilder.Append("\r\n                         <ul class=\"sel_mb\">\r\n                      <li><a href=\"javascript:;\" onclick=\"widthauto(this,'");
	templateBuilder.Append(cssdir.ToString());
	templateBuilder.Append("')\">");
	if (isnarrowpage)
	{

	templateBuilder.Append("切换到宽版");
	}
	else
	{

	templateBuilder.Append("切换到窄版");
	}	//end if

	templateBuilder.Append("</a></li>\r\n                    </ul>\r\n                  ");
	}	//end if

	templateBuilder.Append("\r\n            </div>\r\n            <ul id=\"usercenter_menu\" class=\"p_pop\" style=\"display:none;\">\r\n                <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpprofile.aspx?action=avatar\"><span class=\"PrivateBTNavList\">设置头像</span></a></li>\r\n                <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpprofile.aspx\"><span class=\"PrivateBTNavList\">个人资料</span></a></li>\r\n                <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpnewpassword.aspx\"><span class=\"PrivateBTNavList\">更改密码</span></a></li>\r\n                <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercp.aspx\"><span class=\"PrivateBTNavList\">用户组</span></a></li>\r\n                <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpcreaditstransferlog.aspx\"><span class=\"PrivateBTNavList\">积分</span></a></li>\r\n            </ul>\r\n			<ul id=\"menu\" class=\"cl\">\r\n				");
	templateBuilder.Append(mainnavigation.ToString());
	templateBuilder.Append("\r\n			</ul>\r\n		</div>\r\n	</div>\r\n</div>\r\n");
	}
	else
	{


	Response.Clear();
	Response.ContentType = "Text/XML";
	Response.Expires = 0;
	Response.Cache.SetNoStore();
	
	templateBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><![CDATA[\r\n");
	}	//end if



	templateBuilder.Append("\r\n<script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/ajax.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\nvar templatepath = \"");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("\";\r\nvar imagedir = \"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("\";\r\nvar fid = parseInt(");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append(");\r\nvar fidlist=\"");
	templateBuilder.Append(forums.ToString());
	templateBuilder.Append("\";\r\n</");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_showforum.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_showtopiclist.js\"></");
	templateBuilder.Append("script>\r\n<div class=\"wrap cl pageinfo\">\r\n	<div id=\"nav\">	\r\n		<span class=\"y\">\r\n      ");
	if (type=="newimage")
	{

	templateBuilder.Append("\r\n			    <a href=\"showtopiclist.aspx?type=newimage\">");
	if (forums!="2"&&forums!="37")
	{

	templateBuilder.Append("<b>默认列表</b>");
	}
	else
	{

	templateBuilder.Append("默认列表");
	}	//end if

	templateBuilder.Append("</a> | \r\n			    <a href=\"showtopiclist.aspx?type=newimage&fidlist=2\">");
	if (forums=="2")
	{

	templateBuilder.Append("<b>贴图秀</b>");
	}
	else
	{

	templateBuilder.Append("贴图秀");
	}	//end if

	templateBuilder.Append("</a> | \r\n			    <a href=\"showtopiclist.aspx?type=newimage&fidlist=37\">");
	if (forums=="37")
	{

	templateBuilder.Append("<b>跳蚤市场</b>");
	}
	else
	{

	templateBuilder.Append("跳蚤市场");
	}	//end if

	templateBuilder.Append("</a>\r\n      ");
	}
	else
	{


	if (forumid==-1)
	{

	templateBuilder.Append("\r\n			    <a href=\"forumindex.aspx\">全部</a>\r\n			    <a href=\"showtopiclist.aspx?type=digest&amp;forums=");
	templateBuilder.Append(forums.ToString());
	templateBuilder.Append("\">精华帖区</a>\r\n		    ");
	}
	else
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(forumid,0);
	
	templateBuilder.Append("\r\n			    <a href=\"");
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">全部</a>\r\n			    <a href=\"showtopiclist.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&type=digest\">精华帖区</a>\r\n		    ");
	}	//end if


	if (config.Rssstatus!=0)
	{

	templateBuilder.Append("\r\n			    <a href=\"tools/rss.aspx\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/icon_feed.gif\" alt=\"Rss\" class=\"absmiddle\"/></a>\r\n		    ");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n		</span>\r\n		<a id=\"forumlist\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("\" ");
	if (config.Forumjump==1)
	{

	templateBuilder.Append("onmouseover=\"showMenu(this.id);\" onmouseout=\"showMenu(this.id);\"");
	}	//end if

	templateBuilder.Append(" class=\"title\">");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("</a> &raquo; \r\n		<strong>\r\n		    ");
	if (forumid>0&&forum!=null)
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(forum.Fid,0);
	
	templateBuilder.Append("\r\n		        <a href=\"");
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(forum.Name.ToString().Trim());
	templateBuilder.Append("</a>\r\n		    ");
	}
	else if (type=="newimage")
	{

	templateBuilder.Append("\r\n          新图\r\n		    ");
	}
	else if (type=="digest")
	{

	templateBuilder.Append("\r\n			    精华帖\r\n		    ");
	}
	else
	{

	templateBuilder.Append("\r\n			    新帖\r\n		    ");
	}	//end if

	templateBuilder.Append("\r\n		</strong>\r\n	</div>\r\n</div>\r\n");
	if (page_err==0)
	{


	if (type=="newimage")
	{

	templateBuilder.Append("\r\n<style type=\"text/css\">\r\nbody { overflow-y:scroll; }\r\n.fgbtf_roundimg\r\n{\r\nborder-radius: 10px;\r\nwidth:100%;\r\nmax-height:450px;\r\nheight:expression(this.height > 450 ? 450: true);\r\nborder: 1px solid #666;\r\n}\r\n.wf-main{\r\n    position: relative;\r\n    margin: 0 auto;\r\n    width: 100%;\r\n    overflow: hidden;\r\n}\r\n.wf-main .wf-cld\r\n{\r\n    position: absolute;\r\n    margin-bottom: 10px;\r\n    padding:10px;\r\n    width: 214px;\r\n    left: -9999px;\r\n    top: -9999px;\r\n    line-height:18px;\r\n    border: 0px solid #999;\r\n    border-radius: 10px;\r\n    overflow: hidden;\r\n}\r\n.wf-cld .inner{\r\n    position: absolute;\r\n    left: -9999px;\r\n    top: -9999px;\r\n    margin-bottom: 5px;\r\n    width: 73px;\r\n    overflow: hidden;\r\n    border: 1px solid #f00;\r\n    border-radius: 3px;\r\n}\r\n.wf-cld .title{\r\n    margin: 0 0 5px;\r\n    padding: 5px;\r\n    width: 63px;\r\n    color: #f00;\r\n    font-size: 14px;\r\n}\r\n</style>\r\n<div class=\"wrap cl\">\r\n<div class=\"pages_btns cl\">\r\n  <div class=\"topicslistsearch\" style=\"float:left;vertical-align:bottom;padding-top:7px;\">\r\n    <form id=\"forum_quicksearch_form\" name=\"forum_quicksearch_form\" method=\"post\" action=\"search.aspx?advsearch=1\">\r\n        <input type=\"text\" id=\"forum_quicksearch_textinput\" name=\"keyword\" value=\"主题搜索\" onblur=\"javascript:if(this.value=='') this.value='主题搜索';\" onclick=\"javascript:if(this.value=='主题搜索') this.value='';\" class=\"txt\"/>\r\n        <input type=\"text\" name=\"searchsubmit\" value=\"1\" style=\"display:none;\"/>\r\n        <input type=\"submit\" name=\"submit\" value=\"\" class=\"btnsearch\"/>\r\n        <input type=\"text\" id=\"searchforumid\" name=\"searchforumid\" value=\"\" style=\"display:none;\">\r\n    </form>\r\n  </div>\r\n</div>\r\n<div class=\"main thread hslice\">\r\n<div class=\"wf-main\" id=\"wf-main\">\r\n</div>\r\n	<div class=\"threadlist\">\r\n    <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tbody><tr><td colspan=\"6\"><a href=\"javascript:loadmoreimage();\"><div class=\"zerothreads\" id=\"addnewbar\">请稍候</div></a></td></tr></tbody></table>\r\n	</div>\r\n</div>\r\n<script type=\"text/javascript\">loadmoreimage();</");
	templateBuilder.Append("script>\r\n");
	}
	else if (showforumlogin==1)
	{

	templateBuilder.Append("\r\n<div class=\"wrap cl\" id=\"wrap\">\r\n	<div class=\"main\">\r\n		<h3>本版块已经被管理员设置了密码</h3>\r\n		<form id=\"forumlogin\" name=\"forumlogin\" method=\"post\" action=\"\">\r\n		<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n		<tbody>\r\n		<tr>\r\n			<th><label for=\"forumpassword\">请输入密码</label></th>\r\n			<td><input name=\"forumpassword\" type=\"password\" id=\"forumpassword\" size=\"20\" class=\"txt\"/></td>\r\n		</tr>\r\n		</tbody>\r\n		");
	if (isseccode)
	{

	templateBuilder.Append("	\r\n		<tbody>\r\n		<tr>\r\n			<th><label for=\"vcode\">输入验证码</label></th>\r\n			<td>\r\n				<div style=\"position: relative;\">\r\n				");
	templateBuilder.Append("<div id=\"vcode_temp\"></div>\r\n<script type=\"text/javascript\" reload=\"1\">\r\n	var infloat = ");
	templateBuilder.Append(infloat.ToString());
	templateBuilder.Append(";\r\n	if (typeof vcodeimgid == 'undefined'){\r\n		var vcodeimgid = 1;\r\n	}\r\n	else\r\n	    vcodeimgid++;\r\n\r\n    $('vcode_temp').parentNode.innerHTML = '<input name=\"vcodetext\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"4\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"7\"");
	}	//end if

	templateBuilder.Append(" size=\"20\" onkeyup=\"changevcode(this.form, this.value);\" class=\"txt\" style=\"width:90px;\" id=\"vcodetext' + vcodeimgid + '\"  onblur=\"if(!seccodefocus) {display(this.id + \\'_menu\\')};\"  onfocus=\"opensecwin('+vcodeimgid+',1)\"   value=\"验证码\" autocomplete=\"off\"/>' +\r\n	                                       '<div class=\"seccodecontent\"  style=\"display:none;cursor: pointer;width: 124px; height: 44px;top:256px;z-index:10009;padding:0;\" id=\"vcodetext' + vcodeimgid + '_menu\" onmouseout=\"seccodefocus = 0\" onmouseover=\"seccodefocus = 1\"><img src=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("tools/VerifyImagePage.aspx?time=");
	templateBuilder.Append(Processtime.ToString());
	templateBuilder.Append("\" class=\"cursor\" id=\"vcodeimg' + vcodeimgid + '\" onclick=\"this.src=\\'");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("tools/VerifyImagePage.aspx?id=");
	templateBuilder.Append(olid.ToString());
	templateBuilder.Append("&time=\\' + Math.random();\"/></div>';\r\n	\r\n	function changevcode(form, value){\r\n		if (!$('vcode')){\r\n			var vcode = document.createElement('input');\r\n			vcode.id = 'vcode';\r\n			vcode.name = 'vcode';\r\n			vcode.type = 'hidden';\r\n			vcode.value = value;\r\n			form.appendChild(vcode);\r\n		}else{\r\n			$('vcode').value = value;\r\n		}\r\n	}\r\n</");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\nvar secclick = new Array();\r\nvar seccodefocus = 0;\r\nfunction opensecwin(id,type) {\r\n	if($('vcode')){\r\n	$('vcode').parentNode.removeChild($('vcode'));}\r\n\r\n	if (!secclick['vcodetext' + id]) {\r\n	    $('vcodetext' + id).value = '';\r\n	    secclick['vcodetext' + id] = 1;\r\n	    if(type)\r\n	        $('vcodetext' + id + '_menu').style.top = parseInt($('vcodetext' + id + '_menu').style.top) - parseInt($('vcodetext' + id + '_menu').style.height) + 'px';\r\n	}\r\n\r\n	$('vcodetext' + id + '_menu').style.position = 'absolute';\r\n	$('vcodetext' + id + '_menu').style.top = (-parseInt($('vcodetext' + id + '_menu').style.height) - 2) + 'px';\r\n	$('vcodetext' + id + '_menu').style.left = '0px';\r\n	$('vcodetext' + id + '_menu').style.display = '';\r\n	$('vcodetext' + id).focus();\r\n	$('vcodetext' + id).unselectable = 'off';\r\n	$('vcodeimg' + id).src = '");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("tools/VerifyImagePage.aspx?id=");
	templateBuilder.Append(olid.ToString());
	templateBuilder.Append("&time=' + Math.random();\r\n}\r\n</");
	templateBuilder.Append("script>");

	templateBuilder.Append("\r\n				</div>\r\n		    </td>\r\n		</tr>\r\n		</tbody>\r\n		");
	}	//end if

	templateBuilder.Append("\r\n		<tbody>\r\n		<tr>\r\n			<th></th>\r\n			<td><input type=\"submit\"  value=\"确定\"/></td>\r\n		</tr>\r\n		</tbody>\r\n		</table>\r\n		</form>\r\n	</div>\r\n</div>\r\n");
	}
	else
	{

	templateBuilder.Append("\r\n<div class=\"wrap cl\">\r\n<div class=\"pages_btns cl\">\r\n  <div class=\"topicslistsearch\" style=\"float:left;vertical-align:bottom;padding-top:7px;\">\r\n    <form id=\"forum_quicksearch_form\" name=\"forum_quicksearch_form\" method=\"post\" action=\"search.aspx?advsearch=1\">\r\n        <input type=\"text\" id=\"forum_quicksearch_textinput\" name=\"keyword\" value=\"主题搜索\" onblur=\"javascript:if(this.value=='') this.value='主题搜索';\" onclick=\"javascript:if(this.value=='主题搜索') this.value='';\" class=\"txt\"/>\r\n        <input type=\"text\" name=\"searchsubmit\" value=\"1\" style=\"display:none;\"/>\r\n        <input type=\"submit\" name=\"submit\" value=\"\" class=\"btnsearch\"/>\r\n        <input type=\"text\" id=\"searchforumid\" name=\"searchforumid\" value=\"\" style=\"display:none;\">\r\n    </form>\r\n  </div>\r\n	<div class=\"pages\">\r\n		");
	templateBuilder.Append(pagenumbers.ToString());
	templateBuilder.Append("<em>");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append(" / ");
	templateBuilder.Append(pagecount.ToString());
	templateBuilder.Append("</em>\r\n	</div>\r\n</div>\r\n<div class=\"main thread hslice\">\r\n	<div class=\"category\">\r\n		<table cellspacing=\"0\" cellpadding=\"0\" summary=\"19\">\r\n		<tbody>\r\n		<tr>\r\n			<th>标题</th>\r\n			<td class=\"by\">作者</td>\r\n			<td class=\"num\">回复/查看</td>\r\n			<td class=\"by\">最后发表</td>\r\n		</tr>\r\n		</tbody>\r\n		</table>\r\n	</div>\r\n	<div class=\"threadlist\">\r\n	<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n	");
	int announcement__loop__id=0;
	foreach(DataRow announcement in Announcements.GetSimplifiedAnnouncementList(nowdatetime).Rows)
	{
		announcement__loop__id++;

	templateBuilder.Append("\r\n	<tbody>\r\n		<tr>\r\n			<td class=\"folder\"><img alt=\"announcement\" src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/icon_announcement.gif\"></td>\r\n			<td class=\"icon\">&nbsp;</td>\r\n			<th class=\"subject f_bold\">\r\n				<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("announcement.aspx#" + announcement["id"].ToString().Trim() + "\">" + announcement["title"].ToString().Trim() + "</a>\r\n			</th>\r\n			<td class=\"by\">\r\n			");
	if (Utils.StrToInt(announcement["posterid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("\r\n				游客\r\n			");
	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(announcement["posterid"].ToString().Trim());
	
	templateBuilder.Append("\r\n				<a href=\"");
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">" + announcement["poster"].ToString().Trim() + "</a>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			</td>\r\n			<td class=\"num\">&nbsp;</td>\r\n			<td class=\"by\">&nbsp;</td>\r\n		</tr>\r\n	</tbody>\r\n	");
	}	//end loop


	if (topiclist.Count>0)
	{


	int topic__loop__id=0;
	foreach(TopicInfo topic in topiclist)
	{
		topic__loop__id++;

	templateBuilder.Append("\r\n	<tbody id=\"normalthread_");
	templateBuilder.Append(topic.Tid.ToString().Trim());
	templateBuilder.Append("\" >\r\n	<tr>\r\n		<td class=\"folder\">\r\n		");
	if (topic.Folder!="")
	{

	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	
	templateBuilder.Append("\r\n			<a href=\"");
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/folder_");
	templateBuilder.Append(topic.Folder.ToString().Trim());
	templateBuilder.Append(".gif\" alt=\"主题图标\"/></a>\r\n		");
	}
	else
	{

	templateBuilder.Append("\r\n			&nbsp;\r\n		");
	}	//end if

	templateBuilder.Append("\r\n		</td>\r\n		<td class=\"icon\">\r\n		");
	if (topic.Iconid!=0)
	{

	templateBuilder.Append("\r\n				<img src=\"");
	templateBuilder.Append(posticondir.ToString());
	templateBuilder.Append("/");
	templateBuilder.Append(topic.Iconid.ToString().Trim());
	templateBuilder.Append(".gif\" alt=\"示图\"/>\r\n			");
	}
	else
	{

	templateBuilder.Append("\r\n				&nbsp;\r\n			");
	}	//end if

	templateBuilder.Append("\r\n		</td>\r\n		<th class=\"subject\">	\r\n			<label class=\"y\">\r\n			");
	if (topic.Digest>0)
	{

	templateBuilder.Append("\r\n				<img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/digest");
	templateBuilder.Append(topic.Digest.ToString().Trim());
	templateBuilder.Append(".gif\" alt=\"digtest\"/>\r\n			");
	}	//end if


	if (topic.Special==1)
	{

	templateBuilder.Append("\r\n				<img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/pollsmall.gif\" alt=\"投票\" />\r\n			");
	}	//end if


	if (topic.Special==2 || topic.Special==3)
	{

	templateBuilder.Append("\r\n				<img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/bonus.gif\" alt=\"悬赏\"/>\r\n			");
	}	//end if


	if (topic.Special==4)
	{

	templateBuilder.Append("\r\n				<img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/debatesmall.gif\" alt=\"辩论\"/>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			</label>\r\n			");	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	

	if (topic.Forumname!="")
	{

	templateBuilder.Append("\r\n      <span class=\"PTGrayFont\">[<a href=\"showforum.aspx?forumid=");
	templateBuilder.Append(topic.Fid.ToString().Trim());
	templateBuilder.Append("\" >");
	templateBuilder.Append(topic.Forumname.ToString().Trim());
	templateBuilder.Append("</a>]</span>\r\n      ");
	}	//end if


	if (topic.Forumname!="" && topic.Topictypename!="")
	{

	templateBuilder.Append("\r\n      <em>[<a href=\"showforum.aspx?forumid=");
	templateBuilder.Append(topic.Fid.ToString().Trim());
	templateBuilder.Append("&typeid=");
	templateBuilder.Append(topic.Typeid.ToString().Trim());
	templateBuilder.Append("\" >");
	templateBuilder.Append(topic.Topictypename.ToString().Trim());
	templateBuilder.Append("</a>]</em>\r\n      ");
	}
	else if (topic.Topictypename!="")
	{

	templateBuilder.Append("\r\n      <em>[");
	templateBuilder.Append(topic.Topictypename.ToString().Trim());
	templateBuilder.Append("]</em>\r\n      ");
	}	//end if

	templateBuilder.Append("\r\n			<a href=\"");
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(topic.Title.ToString().Trim());
	templateBuilder.Append("</a>\r\n			");
	if (topic.Attachment>0)
	{

	templateBuilder.Append("\r\n				<img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/attachment.gif\" alt=\"附件\"/>\r\n			");
	}	//end if


	if (topic.Replies/config.Ppp>0)
	{

	templateBuilder.Append("\r\n				<script type=\"text/javascript\">getpagenumbers(\"");
	templateBuilder.Append(config.Extname.ToString().Trim());
	templateBuilder.Append("\", ");
	templateBuilder.Append(topic.Replies.ToString().Trim());
	templateBuilder.Append(",");
	templateBuilder.Append(config.Ppp.ToString().Trim());
	templateBuilder.Append(",0,\"\",");
	templateBuilder.Append(topic.Tid.ToString().Trim());
	templateBuilder.Append(",\"\",\"\",");
	templateBuilder.Append(config.Aspxrewrite.ToString().Trim());
	templateBuilder.Append(");</");
	templateBuilder.Append("script>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n		</th>\r\n		<td class=\"by\">\r\n			<cite>\r\n			");
	if (topic.Posterid==-1)
	{

	templateBuilder.Append("\r\n				游客\r\n			");
	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(topic.Posterid);
	
	templateBuilder.Append("\r\n				<a href=\"");
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(topic.Poster.ToString().Trim());
	templateBuilder.Append("</a>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			</cite>\r\n			");	string ttpdtime = ForumUtils.ConvertDateTimeWithColor(topic.Postdatetime);
	
	templateBuilder.Append("\r\n			<em>");
	templateBuilder.Append(ttpdtime.ToString());
	templateBuilder.Append("</em>\r\n		</td>\r\n		<td class=\"num\"><a href=\"#\">");
	templateBuilder.Append(topic.Replies.ToString().Trim());
	templateBuilder.Append("</a><em>");
	templateBuilder.Append(topic.Views.ToString().Trim());
	templateBuilder.Append("</em></td>\r\n		<td class=\"by\">\r\n			<cite>\r\n			");
	if (topic.Lastposterid==-1)
	{

	templateBuilder.Append("\r\n				游客\r\n			");
	}
	else
	{

	templateBuilder.Append("\r\n				<a href=\"");
	templateBuilder.Append(UserInfoAspxRewrite(topic.Lastposterid).ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\">");
	templateBuilder.Append(topic.Lastposter.ToString().Trim());
	templateBuilder.Append("</a>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			</cite>\r\n			");	string lptime = ForumUtils.ConvertDateTime(topic.Lastpost);
	
	templateBuilder.Append("\r\n			<em><a href=\"showtopic.aspx?topicid=");
	templateBuilder.Append(topic.Tid.ToString().Trim());
	templateBuilder.Append("&page=end#lastpost\">");
	templateBuilder.Append(lptime.ToString());
	templateBuilder.Append("</a></em>\r\n		</td>\r\n	</tr>\r\n	</tbody>\r\n    ");
	}	//end loop


	}
	else
	{

	templateBuilder.Append("\r\n		<tbody><tr><td colspan=\"6\"><div class=\"zerothreads\">暂无帖子</div></td></tr></tbody>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	</table>\r\n	</div>\r\n</div>\r\n<div class=\"pages_btns cl\">\r\n	<div class=\"pages\">\r\n		");
	templateBuilder.Append(pagenumbers.ToString());
	templateBuilder.Append("<em>");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append(" / ");
	templateBuilder.Append(pagecount.ToString());
	templateBuilder.Append("</em>		\r\n	</div>\r\n</div>\r\n<script type=\"text/javascript\">\r\nfunction CheckAll(form)\r\n{\r\n	for (var i = 0; i < form.elements.length; i++)\r\n	{\r\n		var e = form.elements[i];\r\n		if (e.id == \"fidlist\"){\r\n		   e.checked = form.chkall.checked;\r\n		}\r\n	}\r\n}\r\n\r\nfunction SH_SelectOne(obj)\r\n{\r\n	for (var i = 0; i < document.getElementById(\"LookBySearch\").elements.length; i++)\r\n	{\r\n		var e = document.getElementById(\"LookBySearch\").elements[i];\r\n		if (e.id == \"fidlist\"){\r\n		   if (!e.checked){\r\n			document.getElementById(\"chkall\").checked = false;\r\n			return;\r\n		   }\r\n		}\r\n	}\r\n	document.getElementById(\"chkall\").checked = true;\r\n\r\n}\r\n\r\nfunction ShowDetailGrid(tid)\r\n{\r\n   if(");
	templateBuilder.Append(config.Aspxrewrite.ToString().Trim());
	templateBuilder.Append(")\r\n   {\r\n       window.location.href = \"showforum-\" + tid + \"");
	templateBuilder.Append(config.Extname.ToString().Trim());
	templateBuilder.Append("\";\r\n   }\r\n   else\r\n   {\r\n       window.location.href = \"showforum.aspx?forumid=\" + tid ;\r\n   }\r\n}\r\n</");
	templateBuilder.Append("script>\r\n");
	if (forumid==-1)
	{

	templateBuilder.Append("\r\n<div class=\"main\">\r\n	<h1>以下论坛版块:</h1>\r\n	<form name=\"LookBySearch\" method=\"post\" action=\"showtopiclist.aspx?search=1&forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&type=");
	templateBuilder.Append(type.ToString());
	templateBuilder.Append("&newtopic=");
	templateBuilder.Append(newtopic.ToString());
	templateBuilder.Append("\" ID=\"LookBySearch\">\r\n	    <table width=\"100%\" border=\"0\" cellspacing=\"3\" cellpadding=\"0\">\r\n		    <tr>\r\n			     ");
	templateBuilder.Append(GetForumCheckBoxListCache().ToString());
	templateBuilder.Append("\r\n		    </tr>\r\n	    </table>\r\n	    <div id=\"footfilter\">\r\n            <span class=\"right\">\r\n			    排序方式\r\n			    <select name=\"order\" id=\"order\">\r\n			      <option value=\"1\" ");
	if (order==1)
	{

	templateBuilder.Append("selected");
	}	//end if

	templateBuilder.Append(">最后回复时间</option>\r\n			      <option value=\"2\" ");
	if (order==2)
	{

	templateBuilder.Append("selected");
	}	//end if

	templateBuilder.Append(">发布时间</option>\r\n			    </select>	\r\n			    <select name=\"direct\" id=\"direct\">\r\n			      <option value=\"0\" ");
	if (direct==0)
	{

	templateBuilder.Append("selected");
	}	//end if

	templateBuilder.Append(">按升序排列</option>\r\n			      <option value=\"1\" ");
	if (direct==1)
	{

	templateBuilder.Append("selected");
	}	//end if

	templateBuilder.Append(">按降序排列</option>\r\n			    </select>\r\n			    <button type=\"submit\" onclick=\"document.LookBySearch.submit();\">提交</button>\r\n		    </span>\r\n		    <input title=\"选中/取消选中 本页所有Case\" onclick=\"CheckAll(this.form)\" type=\"checkbox\" name=\"chkall\" id=\"chkall\"/>全选/取消全选\r\n	    </div>\r\n	</form>\r\n	</div>\r\n");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n</div>\r\n");
	}
	else
	{


	templateBuilder.Append("<div class=\"wrap cl\">\r\n<div class=\"main\">\r\n	<div class=\"msgbox\">\r\n		<h1>出现了");
	templateBuilder.Append(page_err.ToString());
	templateBuilder.Append("个错误</h1>\r\n		<hr class=\"solidline\"/>\r\n		<div class=\"msg_inner error_msg\">\r\n			<p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n			<p class=\"errorback\">\r\n				<script type=\"text/javascript\">\r\n					if(");
	templateBuilder.Append(msgbox_showbacklink.ToString());
	templateBuilder.Append(")\r\n					{\r\n						document.write(\"<a href=\\\"");
	templateBuilder.Append(msgbox_backlink.ToString());
	templateBuilder.Append("\\\">返回上一步</a> &nbsp; &nbsp;|&nbsp; &nbsp  \");\r\n					}\r\n				</");
	templateBuilder.Append("script>\r\n				<a href=\"forumindex.aspx\">论坛首页</a>\r\n				");
	if (usergroupid==7)
	{

	templateBuilder.Append("\r\n				 &nbsp; &nbsp;|&nbsp; &nbsp; <a href=\"login.aspx\">登录</a>&nbsp; &nbsp;|&nbsp; &nbsp; <a href=\"register.aspx\">注册</a>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n			</p>\r\n		</div>\r\n	</div>\r\n</div>\r\n</div>");


	}	//end if


	if (config.Forumjump==1)
	{

	templateBuilder.Append("\r\n	");
	templateBuilder.Append(navhomemenu.ToString());
	templateBuilder.Append("\r\n");
	}	//end if



	if (infloat!=1)
	{


	if (pagename=="website.aspx")
	{

	templateBuilder.Append("    \r\n       <div id=\"websitebottomad\"></div>\r\n");
	}
	else if (footerad!="")
	{

	templateBuilder.Append(" \r\n     <div id=\"ptnotice_footerbanner\">");
	templateBuilder.Append(footerad.ToString());
	templateBuilder.Append("</div>   \r\n");
	}	//end if

	templateBuilder.Append("\r\n<div id=\"footer\">\r\n	<div class=\"wrap\"  id=\"wp\">\r\n  <div id=\"footlinks\">\r\n        <p>推荐屏幕分辨率1366或以上&nbsp;&nbsp;&nbsp;<a href=\"http://bt.buaa6.edu.cn\" target=\"_blank\">北京航空航天大学 未来花园</a>&nbsp; \r\n        </p>\r\n        <a href=\"http://bt.buaa6.edu.cn\" target=\"_blank\">Discuz!NT 3.6</a>&nbsp; \r\n  </div>\r\n		<a title=\"Powered by Discuz!NT\" target=\"_blank\" href=\"http://nt.discuz.net\"><img border=\"0\" alt=\"Discuz!NT\" src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/discuznt_logo.gif\"/></a>\r\n		<p id=\"copyright\">\r\n			Powered by <strong><a href=\"http://nt.discuz.net\" target=\"_blank\" title=\"Discuz!NT\">Discuz!NT</a></strong> <em class=\"f_bold\">3.6.711</em>\r\n			");
	if (config.Licensed==1)
	{

	templateBuilder.Append("\r\n				(<a href=\"\" onclick=\"this.href='http://nt.discuz.net/certificate/?host='+location.href.substring(0, location.href.lastIndexOf('/'))\" target=\"_blank\">Licensed</a>)\r\n			");
	}	//end if

	templateBuilder.Append("\r\n				");
	templateBuilder.Append(config.Forumcopyright.ToString().Trim());
	templateBuilder.Append("\r\n		</p>\r\n		<p id=\"debuginfo\" class=\"grayfont\">\r\n		");
	if (config.Debug!=0)
	{

	templateBuilder.Append("\r\n			Processed in ");
	templateBuilder.Append(this.Processtime.ToString().Trim());
	templateBuilder.Append(" second(s)\r\n			");
	if (isguestcachepage==1)
	{

	templateBuilder.Append("\r\n				(Cached).\r\n			");
	}
	else if (querycount>1)
	{

	templateBuilder.Append("\r\n				 , ");
	templateBuilder.Append(querycount.ToString());
	templateBuilder.Append(" queries.\r\n			");
	}
	else
	{

	templateBuilder.Append("\r\n				 , ");
	templateBuilder.Append(querycount.ToString());
	templateBuilder.Append(" query.\r\n			");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n		</p>\r\n	</div>\r\n</div>\r\n<a id=\"scrolltop\" href=\"javascript:;\" style=\"display:none;\" class=\"scrolltop\" onclick=\"setScrollToTop(this.id);\">TOP</a>\r\n\r\n");
	int prentid__loop__id=0;
	foreach(string prentid in mainnavigationhassub)
	{
		prentid__loop__id++;

	templateBuilder.Append("\r\n<ul class=\"p_pop\" id=\"menu_");
	templateBuilder.Append(prentid.ToString());
	templateBuilder.Append("_menu\" style=\"display: none\">\r\n");
	int subnav__loop__id=0;
	foreach(DataRow subnav in subnavigation.Rows)
	{
		subnav__loop__id++;

	bool isoutput = false;
	

	if (subnav["parentid"].ToString().Trim()==prentid)
	{


	if (subnav["level"].ToString().Trim()=="0")
	{

	 isoutput = true;
	

	}
	else
	{


	if (subnav["level"].ToString().Trim()=="1" && userid>0)
	{

	 isoutput = true;
	

	}
	else
	{

	bool leveluseradmindi = true;
	
	 leveluseradmindi = (useradminid==3 || useradminid==1 || useradminid==2);
	

	if (subnav["level"].ToString().Trim()=="2" &&  leveluseradmindi)
	{

	 isoutput = true;
	

	}	//end if


	if (subnav["level"].ToString().Trim()=="3" && useradminid==1)
	{

	 isoutput = true;
	

	}	//end if


	}	//end if


	}	//end if


	}	//end if


	if (isoutput)
	{


	if (subnav["id"].ToString().Trim()=="11" || subnav["id"].ToString().Trim()=="12")
	{


	if (config.Statstatus==1)
	{

	templateBuilder.Append("\r\n	" + subnav["nav"].ToString().Trim() + "\r\n        ");	continue;


	}
	else
	{

	continue;


	}	//end if


	}	//end if


	if (subnav["id"].ToString().Trim()=="18")
	{


	if (config.Oltimespan>0)
	{

	templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n	");	continue;


	}
	else
	{

	continue;


	}	//end if


	}	//end if


	if (subnav["id"].ToString().Trim()=="24")
	{


	if (config.Enablespace==1)
	{

	templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n 	");	continue;


	}
	else
	{

	continue;


	}	//end if


	}	//end if


	if (subnav["id"].ToString().Trim()=="25")
	{


	if (config.Enablealbum==1)
	{

	templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n 	");	continue;


	}
	else
	{

	continue;


	}	//end if


	}	//end if


	if (subnav["id"].ToString().Trim()=="26")
	{


	if (config.Enablemall>=1)
	{

	templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n   	");	continue;


	}
	else
	{

	continue;


	}	//end if


	}	//end if

	templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n</ul>\r\n");
	}	//end loop


	if (config.Stylejump==1)
	{


	if (userid>0 || config.Guestcachepagetimeout<=0)
	{

	templateBuilder.Append("\r\n	<ul id=\"styleswitcher_menu\" class=\"popupmenu_popup s_clear\" style=\"display: none;\">\r\n	");
	templateBuilder.Append(templatelistboxoptions.ToString());
	templateBuilder.Append("\r\n	</ul>\r\n	");
	}	//end if


	}	//end if




	templateBuilder.Append("</body>\r\n</html>\r\n");
	}
	else
	{

	templateBuilder.Append("\r\n]]></root>\r\n");
	}	//end if




	Response.Write(templateBuilder.ToString());
}
</script>
