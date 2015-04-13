<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.stats" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>
<%@ Import namespace="Discuz.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2015/1/7 16:52:22.
		本页面代码由Discuz!NT模板引擎生成于 2015/1/7 16:52:22. 
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



	templateBuilder.Append("\r\n<div class=\"wrap s_clear pageinfo\">\r\n    <div id=\"nav\">\r\n        <a href=\"");
	templateBuilder.Append(config.Forumurl.ToString().Trim());
	templateBuilder.Append("\" class=\"title\">");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("</a> &raquo; <a href=\"stats.aspx\">\r\n            统计</a> &raquo; <strong>\r\n                ");
	if (type=="")
	{

	templateBuilder.Append("\r\n                基本概况\r\n                ");
	}
	else if (type=="views")
	{

	templateBuilder.Append("\r\n                流量统计\r\n                ");
	}
	else if (type=="client")
	{

	templateBuilder.Append("\r\n                客户软件\r\n                ");
	}
	else if (type=="posts")
	{

	templateBuilder.Append("\r\n                发帖量记录\r\n                ");
	}
	else if (type=="forumsrank")
	{

	templateBuilder.Append("\r\n                版块排行\r\n                ");
	}
	else if (type=="topicsrank")
	{

	templateBuilder.Append("\r\n                主题排行\r\n                ");
	}
	else if (type=="postsrank")
	{

	templateBuilder.Append("\r\n                发帖排行\r\n                ");
	}
	else if (type=="creditsrank")
	{

	templateBuilder.Append("\r\n                积分排行\r\n                ");
	}
	else if (type=="onlinetime")
	{

	templateBuilder.Append("\r\n                在线时间\r\n                ");
	}
	else if (type=="trade")
	{

	templateBuilder.Append("\r\n                交易排行\r\n                ");
	}
	else if (type=="team")
	{

	templateBuilder.Append("\r\n                管理团队\r\n                ");
	}
	else if (type=="modworks")
	{

	templateBuilder.Append("\r\n                管理统计\r\n                ");
	}
	else if (type=="trend")
	{

	templateBuilder.Append("\r\n                趋势统计\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n            </strong>\r\n    </div>\r\n</div>\r\n\r\n<script type=\"text/javascript\">\r\n    function changeTab(obj) {\r\n        if (obj.className == 'currenttab') {\r\n            obj.className = '';\r\n        }\r\n        else {\r\n            obj.className = 'currenttab';\r\n        }\r\n    }\r\n</");
	templateBuilder.Append("script>\r\n\r\n");
	if (page_err==0)
	{

	templateBuilder.Append("\r\n<div class=\"wrap uc cl\">\r\n    <div class=\"uc_app\">\r\n        <h2>\r\n            统计</h2>\r\n        <ul>\r\n            <li id=\"tab_main\" class=\"current\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"stats.aspx\">基本状况</a></li>\r\n            <li id=\"tab_updown\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=updown\">上传下载排行</a></li>\r\n            <li id=\"tab_ratio\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=ratio\">共享率排行</a></li>\r\n            <li id=\"tab_views\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=views\">流量统计</a></li>\r\n            <li id=\"tab_client\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=client\">客户软件</a></li>\r\n            <li id=\"tab_posts\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=posts\">发帖量记录</a></li>\r\n            <li id=\"tab_forumsrank\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=forumsrank\">版块排行</a></li>\r\n            <li id=\"tab_topicsrank\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=topicsrank\">主题排行</a></li>\r\n            <li id=\"tab_postsrank\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=postsrank\">发帖排行</a></li>\r\n            <li id=\"tab_creditsrank\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=creditsrank\">积分排行</a></li>\r\n            <li id=\"tab_onlinetime\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=onlinetime\">在线时间</a></li>   \r\n        </ul>\r\n    </div>\r\n\r\n    <script type=\"text/javascript\">\r\n        try {\r\n            $(\"tab_main\").className = \"\";\r\n            $(\"tab_\" + '");
	templateBuilder.Append(type.ToString());
	templateBuilder.Append("').className = \"current\";\r\n        } catch (e) {\r\n            $(\"tab_main\").className = \"current\";\r\n        }\r\n    </");
	templateBuilder.Append("script>\r\n\r\n    <div class=\"uc_main\">\r\n        <div class=\"uc_content stats\">\r\n            ");
	if (type=="bt")
	{


	}	//end if


	if (type=="")
	{

	templateBuilder.Append("\r\n            <h1>BT统计数据</h1>\r\n            <div class=\"hintinfo notice\">BT统计数据已被缓存，上次于 ");
	templateBuilder.Append(btstats.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(btstats.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>	\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\" style=\"margin-bottom: 10px;\">\r\n              <tr><!--1-->\r\n                <td class=\"t_th\">发种计数</td>\r\n                <td>");
	templateBuilder.Append(btstats.AllSeedsCount.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.AllSeedsCount.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[统计]总上传流量</td>\r\n                <td>");
	templateBuilder.Append(btstats.SStatsUpload.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.SStatsUpload.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n              </tr>\r\n              <tr><!--2-->\r\n                <td class=\"t_th\">发种总容量</td>\r\n                <td>");
	templateBuilder.Append(btstats.SAllSize.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.SAllSize.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[统计]总上传流量，含金币折算</td>\r\n                <td>");
	templateBuilder.Append(btstats.SStatsUploadAll.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.SStatsUploadAll.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n              </tr>\r\n              <tr><!--3-->\r\n                <td class=\"t_th\">在线种子计数</td>\r\n                <td>");
	templateBuilder.Append(btstats.OnlineSeedsCount.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">|</span>&nbsp;");
	templateBuilder.Append(btstats.SOnlineCountRatio.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.OnlineSeedsCount.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[统计]总下载流量</td>\r\n                <td>");
	templateBuilder.Append(btstats.SStatsDownload.ToString().Trim());
	templateBuilder.Append("</td>\r\n              </tr>\r\n              <tr><!--4-->\r\n                <td class=\"t_th\">在线种子总容量</td>\r\n                <td>");
	templateBuilder.Append(btstats.SOnlineSize.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">|</span>&nbsp;");
	templateBuilder.Append(btstats.SOnlineSizeRatio.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.SOnlineSize.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[统计]今天上传流量</td>\r\n                <td>");
	templateBuilder.Append(btstats.SStatsUploadToday.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.SStatsUploadToday.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n              </tr>\r\n              <tr><!--5-->\r\n                <td class=\"t_th\">客户端在线人数</td>\r\n                <td>");
	templateBuilder.Append(btstats.OnlineUserCount.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.OnlineUserCount.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[统计]今天下载流量</td>\r\n                <td>");
	templateBuilder.Append(btstats.SStatsDownloadToday.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.SStatsDownloadToday.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n              </tr>\r\n              <tr><!--6-->\r\n                <td class=\"t_th\">正在做种人数</td>\r\n                <td>");
	templateBuilder.Append(btstats.SeederCount.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.SeederCount.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[统计]上传/下载</td>\r\n                <td>");
	templateBuilder.Append(btstats.StatsRatio.ToString().Trim());
	templateBuilder.Append("</td>\r\n              </tr>\r\n              <tr><!--7-->\r\n                <td class=\"t_th\">正在下载人数</td>\r\n                <td>");
	templateBuilder.Append(btstats.LeecherCount.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.LeecherCount.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\"></td>\r\n                <td></td>\r\n              </tr>\r\n              <tr><!--8-->\r\n                <td class=\"t_th\">总节点计数</td>\r\n                <td>");
	templateBuilder.Append(btstats.PeerCount.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.PeerCount.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[物理]当前下载总速度</td>\r\n                <td>");
	templateBuilder.Append(btstats.SDownSpeed.ToString().Trim());
	templateBuilder.Append("/s&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.SDownSpeed.ToString().Trim());
	templateBuilder.Append("/s)</span></td>\r\n              </tr>\r\n              <tr><!--9-->\r\n                <td class=\"t_th\">正在做种节点数</td>\r\n                <td>");
	templateBuilder.Append(btstats.UpPeerCount.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.UpPeerCount.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[物理]今天下载总流量</td>\r\n                <td>");
	templateBuilder.Append(btstats.STodayTraffic.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.STodayTraffic.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n              </tr>\r\n              <tr><!--10-->\r\n                <td class=\"t_th\">正在下载节点数</td>\r\n                <td>");
	templateBuilder.Append(btstats.DownPeerCount.ToString().Trim());
	templateBuilder.Append("&nbsp;<span class=\"StatsGray\">(");
	templateBuilder.Append(btstatspk.DownPeerCount.ToString().Trim());
	templateBuilder.Append(")</span></td>\r\n                <td class=\"t_th\">[物理]历史下载总流量</td>\r\n                <td>");
	templateBuilder.Append(btstats.SAllTraffic.ToString().Trim());
	templateBuilder.Append("</td>\r\n              </tr>\r\n            </table>\r\n            \r\n            \r\n            <h1>基本状况</h1>\r\n            <div class=\"hintinfo notice\">基本状况数据已被缓存，上次于 ");
	templateBuilder.Append(Default_Basic.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Default_Basic.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n              ");
	templateBuilder.Append(Default_Basic.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            <h1>论坛统计</h1>\r\n            <div class=\"hintinfo notice\">论坛统计数据已被缓存，上次于 ");
	templateBuilder.Append(Default_Forum.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Default_Forum.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n              ");
	templateBuilder.Append(Default_Forum.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            <h1>流量概况</h1>\r\n            <div class=\"hintinfo notice\">流量概况数据已被缓存，上次于 ");
	templateBuilder.Append(Default_Traffic.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Default_Traffic.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>	\r\n              ");
	templateBuilder.Append(Default_Traffic.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            <h1>月份流量</h1>\r\n            <div class=\"hintinfo notice\">月份流量数据已被缓存，上次于 ");
	templateBuilder.Append(Default_MonthTraffic.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Default_MonthTraffic.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n              ");
	templateBuilder.Append(Default_MonthTraffic.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n\r\n            ");
	}	//end if


	if (type=="views")
	{

	templateBuilder.Append("\r\n                <h1>星期流量</h1>\r\n                <div class=\"hintinfo notice\">星期流量数据已被缓存，上次于 ");
	templateBuilder.Append(Views_Week.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Views_Week.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(Views_Week.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n                <h1>时段流量</h1>\r\n                <div class=\"hintinfo notice\">时段流量数据已被缓存，上次于 ");
	templateBuilder.Append(Views_Hour.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Views_Hour.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(Views_Hour.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="client")
	{

	templateBuilder.Append("\r\n                <h1>操作系统</h1>\r\n                <div class=\"hintinfo notice\">操作系统数据已被缓存，上次于 ");
	templateBuilder.Append(Client_OS.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Client_OS.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(Client_OS.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n                <h1>浏览器</h1>\r\n                <div class=\"hintinfo notice\">浏览器数据已被缓存，上次于 ");
	templateBuilder.Append(Client_Browser.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Client_Browser.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(Client_Browser.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="posts")
	{

	templateBuilder.Append("\r\n                <h1>每月新增帖子记录</h1>\r\n                <div class=\"hintinfo notice\">每月新增帖子记录已被缓存，上次于 ");
	templateBuilder.Append(Posts_Month.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Posts_Month.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(Posts_Month.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n                <h1>每日新增帖子记录</h1>\r\n                <div class=\"hintinfo notice\">每日新增帖子记录已被缓存，上次于 ");
	templateBuilder.Append(Posts_Day.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(Posts_Day.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(Posts_Day.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="forumsrank")
	{

	templateBuilder.Append("\r\n                <h1>版块排行</h1>\r\n                <div class=\"hintinfo notice\">版块排行数据已被缓存，上次于 ");
	templateBuilder.Append(ForumsRank.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(ForumsRank.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(ForumsRank.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="topicsrank")
	{

	templateBuilder.Append("\r\n                <h1>主题排行</h1>\r\n                <div class=\"hintinfo notice\">主题排行数据已被缓存，上次于 ");
	templateBuilder.Append(TopicsRank.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(TopicsRank.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(TopicsRank.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="postsrank")
	{

	templateBuilder.Append("\r\n                <h1>发帖排行</h1>\r\n                <div class=\"hintinfo notice\">发帖排行数据已被缓存，上次于 ");
	templateBuilder.Append(PostsRank.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(PostsRank.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(PostsRank.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="creditsrank")
	{

	templateBuilder.Append("\r\n                <h1>积分排行</h1>\r\n                <div class=\"hintinfo notice\">积分排行数据已被缓存，上次于 ");
	templateBuilder.Append(CreditsRank.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(CreditsRank.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(CreditsRank.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="onlinetime")
	{

	templateBuilder.Append("\r\n                <h1>在线时间排行</h1>\r\n                <div class=\"hintinfo notice\">在线时间排行数据已被缓存，上次于 ");
	templateBuilder.Append(OnlineTimeRank.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(OnlineTimeRank.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                  ");
	templateBuilder.Append(OnlineTimeRank.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="updown")
	{

	templateBuilder.Append("\r\n              <h1>上传下载排行</h1>\r\n              <div class=\"hintinfo notice\">上传下载排行数据已被缓存，上次于 ");
	templateBuilder.Append(UpDownRank.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(UpDownRank.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                ");
	templateBuilder.Append(UpDownRank.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if


	if (type=="ratio")
	{

	templateBuilder.Append("\r\n              <h1>共享率排行</h1>\r\n              <div class=\"hintinfo notice\">共享率排行数据已被缓存，上次于 ");
	templateBuilder.Append(RatioRank.LastUpdate.ToString().Trim());
	templateBuilder.Append(" 被更新，下次将于 ");
	templateBuilder.Append(RatioRank.NextUpdate.ToString().Trim());
	templateBuilder.Append(" 进行更新</div>\r\n                ");
	templateBuilder.Append(RatioRank.StatsValue.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n        </div>\r\n    </div>\r\n</div>\r\n");
	}
	else
	{


	if (needlogin)
	{



	if (infloat!=1)
	{

	templateBuilder.Append("\r\n<div class=\"wrap cl\">\r\n	<div class=\"blr\">\r\n	<div class=\"msgbox\" style=\"margin:4px auto;padding:0 !important;margin-left:0;background:none;\">\r\n		<div class=\"msg_inner error_msg\">\r\n			<p>您无权进行当前操作，这可能因以下原因之一造成</p>\r\n			<p><b>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</b></p>\r\n			<p>您还没有登录，请填写下面的登录表单后再尝试访问。</p>\r\n		</div>\r\n	</div>\r\n	<hr class=\"solidline\"/>\r\n	<form id=\"formlogin\" name=\"formlogin\" method=\"post\" action=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("login.aspx\" onsubmit=\"submitLogin(this);\">\r\n	<div class=\"c cl\">\r\n		<div style=\"overflow:hidden;overflow-y:auto\" class=\"lgfm\">\r\n		<input type=\"hidden\" value=\"2592000\" name=\"cookietime\"/>\r\n			<div class=\"sipt lpsw\">\r\n				<label for=\"username\">用户名　：</label>\r\n				<input type=\"text\" id=\"username\" name=\"username\" size=\"25\" maxlength=\"40\" tabindex=\"2\" class=\"txt\" />\r\n			</div>\r\n			<div class=\"sipt lpsw\">\r\n				<label for=\"password\">密　码　：</label>\r\n				<input type=\"password\" name=\"password\" size=\"25\" tabindex=\"3\" class=\"txt\"/>\r\n			</div>\r\n        ");
	if (isLoginCode)
	{

	templateBuilder.Append("\r\n			<div class=\"lpsw\" style=\"position: relative;margin-bottom:10px;\">\r\n				");
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

	templateBuilder.Append("\r\n			</div>\r\n        ");
	}	//end if


	if (config.Secques==1)
	{

	templateBuilder.Append("\r\n				<div class=\"ftid sltp\" style=\"margin-bottom:10px\">\r\n					<select name=\"question\" id=\"question_login\" change=\"changequestion();\" tabindex=\"5\">\r\n						<option value=\"0\">安全提问（未设置请忽略）</option>\r\n						<option onclick=\"loadselect_liset('question_login', 0, 'question_login','1',this.innerHTML, 1)\" value=\"1\" k_id=\"question_login\">母亲的名字</option>\r\n						<option onclick=\"loadselect_liset('question_login', 0, 'question_login','2',this.innerHTML, 2)\" value=\"2\" k_id=\"question_login\">爷爷的名字</option>\r\n						<option onclick=\"loadselect_liset('question_login', 0, 'question_login','3',this.innerHTML, 3)\" value=\"3\" k_id=\"question_login\">父亲出生的城市</option>\r\n						<option onclick=\"loadselect_liset('question_login', 0, 'question_login','4',this.innerHTML, 4)\" value=\"4\" k_id=\"question_login\">您其中一位老师的名字</option>\r\n						<option onclick=\"loadselect_liset('question_login', 0, 'question_login','5',this.innerHTML, 5)\" value=\"5\" k_id=\"question_login\">您个人计算机的型号</option>\r\n						<option onclick=\"loadselect_liset('question_login', 0, 'question_login','6',this.innerHTML, 6)\" value=\"6\" k_id=\"question_login\">您最喜欢的餐馆名称</option>\r\n						<option onclick=\"loadselect_liset('question_login', 0, 'question_login','7',this.innerHTML, 7)\" value=\"7\" k_id=\"question_login\">驾驶执照的最后四位数字</option>\r\n					</select>\r\n					<script type=\"text/javascript\">simulateSelect('question_login', '214');</");
	templateBuilder.Append("script>\r\n					<script type=\"text/javascript\">\r\n					    window.onload = function(){setselect(");
	templateBuilder.Append(question.ToString());
	templateBuilder.Append(");}\r\n				        function changequestion() {\r\n				            if ($('question_login').getAttribute(\"selecti\") != \"0\") {\r\n				                $('answer_login').style.display = '';\r\n						        $('answer_login').focus();\r\n				            }\r\n				            else {\r\n				                $('answer_login').style.display = 'none';\r\n				            }\r\n				        }\r\n				        function setselect(value) {\r\n				            try {\r\n                                var questionarray = new Array('安全提问','母亲的名字','爷爷的名字','父亲出生的城市','您其中一位老师的名字','您个人计算机的型号','您最喜欢的餐馆名称','驾驶执照的最后四位数字');\r\n                                $('question_login').setAttribute(\"selecti\",value);\r\n                                $('question_login').options[0].value = value;\r\n                                $('question_ctrl').innerHTML = questionarray[value];\r\n                                changequestion();\r\n				            }\r\n				            catch (e) {\r\n				            }\r\n				        }\r\n\r\n					</");
	templateBuilder.Append("script>\r\n				</div>\r\n				<div class=\"sltp\" style=\"clear:both;\"><input type=\"text\" tabindex=\"6\" class=\"txt\" size=\"36\" autocomplete=\"off\" style=\"display: none;\" id=\"answer_login\" name=\"answer\"/></div>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			<div class=\"sltp\"  style=\"display:none\">\r\n				<label for=\"templateid\">界面风格</label>\r\n				<select name=\"templateid\" tabindex=\"7\">\r\n				<option value=\"0\">- 使用默认 -</option>\r\n					");
	templateBuilder.Append(templatelistboxoptions.ToString());
	templateBuilder.Append("\r\n				</select>\r\n			</div>\r\n		</div>\r\n		<div class=\"lgf\">\r\n			<h4>没有帐号？\r\n				");
	if (infloat==1)
	{

	templateBuilder.Append("\r\n				<a href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("register.aspx\"  onclick=\"hideWindow('login');showWindow('register', this.href);\" class=\"xg2\">立即注册</a>\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n				<a href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("register.aspx\" tabindex=\"-1\" accesskey=\"r\" title=\"立即注册 (ALT + R)\" class=\"xg2\">立即注册</a>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n			</h4>\r\n			<p>\r\n				");
	if (infloat==1)
	{

	templateBuilder.Append("\r\n				<a href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("getpassword.aspx\" onclick=\"hideWindow('login');showWindow('getpassword', this.href);\" accesskey=\"g\" title=\"忘记密码 (ALT + G)\" class=\"xg2\">找回密码</a>\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n				<a href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("getpassword.aspx\" accesskey=\"g\" title=\"找回密码\" class=\"xg2\">找回密码</a>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n			</p>\r\n		</div>\r\n	</div>\r\n	<p class=\"fsb pns cl\">\r\n		<input type=\"submit\" style=\"width:0;filter:alpha(opacity=0);-moz-opacity:0;opacity:0;\"/>\r\n		<button name=\"login\" type=\"submit\" id=\"login\" tabindex=\"8\" ");
	if (infloat!=1)
	{

	templateBuilder.Append("onclick=\"javascript:window.location.replace('?agree=yes')\"");
	}	//end if

	templateBuilder.Append(" class=\"pn\"><span>登录</span></button>\r\n		<input type=\"checkbox\" value=\"43200\" tabindex=\"9\" id=\"expires\" name=\"expires\" checked/>\r\n		<label for=\"expires\"><span title=\"下次访问自动登录\">记住我</span></label>\r\n	</p>\r\n	<script type=\"text/javascript\">\r\n		document.getElementById(\"username\").focus();\r\n	</");
	templateBuilder.Append("script>\r\n	</form>\r\n</div>\r\n</div>\r\n");
	}
	else
	{

	templateBuilder.Append("\r\n<div class=\"main\">\r\n	<div class=\"msgbox\">\r\n		<h1>");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append(" 提示信息</h1>\r\n		<hr class=\"solidline\"/>\r\n		<div class=\"msg_inner error_msg\">\r\n			<p>您无权进行当前操作，这可能因以下原因之一造成</p>\r\n			<p><b>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</b></p>\r\n			<p>您还没有登录，请填写下面的登录表单后再尝试访问。</p>\r\n		</div>\r\n	</div>\r\n</div>\r\n<script type=\"text/javascript\" reload=\"1\">\r\nsetTimeout(\"floatwin('close_newthread');floatwin('close_reply');floatwin('close_edit');floatwin('open_login', '");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("login.aspx', 600, 410)\",1000);\r\n</");
	templateBuilder.Append("script>\r\n");
	}	//end if

	templateBuilder.Append("	\r\n<script type=\"text/javascript\">\r\n        ");
	if (infloat!=1)
	{

	templateBuilder.Append("\r\n		document.getElementById(\"username\").focus();\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n		function submitLogin(loginForm)\r\n		{\r\n//		    loginForm.action = '");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("login.aspx?loginsubmit=true&reurl=' + escape(window.location);\r\n            loginForm.action = '");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("login.aspx?reurl=' + escape(window.location);\r\n            \r\n			loginForm.submit();\r\n		}\r\n</");
	templateBuilder.Append("script>");


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
