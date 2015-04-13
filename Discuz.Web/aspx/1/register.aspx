<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.register" %>
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




	if (infloat!=1)
	{

	templateBuilder.Append("\r\n<div class=\"wrap cl pageinfo\">\r\n	<div id=\"nav\">\r\n		");
	if (usergroupinfo.Allowsearch>0)
	{


	templateBuilder.Append("<form method=\"post\" action=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("search.aspx\" target=\"_blank\" onsubmit=\"bind_keyword(this);\" class=\"y\">\r\n	<input type=\"hidden\" name=\"poster\" />\r\n	<input type=\"hidden\" name=\"keyword\" />\r\n	<input type=\"hidden\" name=\"type\" value=\"\" />\r\n	<input id=\"keywordtype\" type=\"hidden\" name=\"keywordtype\" value=\"0\" />\r\n	<a href=\"javascript:void(0);\" class=\"drop s_type\" id=\"quicksearch\" onclick=\"showMenu(this.id, false);\" onmouseover=\"MouseCursor(this);\">快速搜索</a>\r\n	<input type=\"text\" id=\"quicksearch_textinput\" name=\"keywordf\" value=\"输入搜索关键字:种子标题\" onblur=\"bind_inputmessage(false);\" onclick=\"bind_inputmessage(true);\" onkeydown=\"bind_inputmessage(true);\" class=\"txt\"/>\r\n	<input name=\"searchsubmit\" type=\"submit\" value=\"\" class=\"btnsearch\"/>\r\n</form>\r\n<ul id=\"quicksearch_menu\" class=\"p_pop\" style=\"display: none;\">\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='0';$('quicksearch').innerHTML='帖子标题';$('quicksearch_menu').style.display='none';bind_inputmessage(false);\" onmouseover=\"MouseCursor(this);\">帖子标题</a></li>\r\n	");
	if (config.Enablespace==1)
	{

	templateBuilder.Append("\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='2';$('quicksearch').innerHTML='空间日志';$('quicksearch_menu').style.display='none';bind_inputmessage(false);\" onmouseover=\"MouseCursor(this);\">空间日志</a></li>\r\n	");
	}	//end if


	if (config.Enablealbum==1)
	{

	templateBuilder.Append("\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='3';$('quicksearch').innerHTML='相册标题';$('quicksearch_menu').style.display='none';bind_inputmessage(false);\" onmouseover=\"MouseCursor(this);\">相册标题</a></li>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='8';$('quicksearch').innerHTML='作者';$('quicksearch_menu').style.display='none';bind_inputmessage(false);\" onmouseover=\"MouseCursor(this);\">作者</a></li>\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='9';$('quicksearch').innerHTML='版块';$('quicksearch_menu').style.display='none';bind_inputmessage(false);\" onmouseover=\"MouseCursor(this);\">版块</a></li>\r\n</ul>\r\n<script type=\"text/javascript\">\r\n    function bind_inputmessage(click)\r\n    {\r\n\r\n        if(!click)\r\n        {\r\n            var isdefaultvalue = false;\r\n            if($('quicksearch_textinput').value == \"\" ||$('quicksearch_textinput').value == \"输入搜索关键字:种子标题\" ||$('quicksearch_textinput').value == \"输入搜索关键字:帖子标题\" ||$('quicksearch_textinput').value == \"输入搜索关键字:空间日志\" ||$('quicksearch_textinput').value == \"输入搜索关键字:相册标题\" ||$('quicksearch_textinput').value == \"输入搜索关键字:作者名称\" ||$('quicksearch_textinput').value == \"输入搜索关键字:版块名称\" )\r\n            {\r\n                isdefaultvalue = true;\r\n            }\r\n            else if($('quicksearch').innerHTML=='帖子标题' || $('quicksearch').innerHTML=='快速搜索' )\r\n            {\r\n                if(isdefaultvalue) $('quicksearch_textinput').value = \"输入搜索关键字:帖子标题\";\r\n            }\r\n            else if($('quicksearch').innerHTML=='空间日志')\r\n            {\r\n                if(isdefaultvalue) $('quicksearch_textinput').value = \"输入搜索关键字:空间日志\";\r\n            }\r\n            else if($('quicksearch').innerHTML=='相册标题')\r\n            {\r\n                if(isdefaultvalue) $('quicksearch_textinput').value = \"输入搜索关键字:相册标题\";\r\n            }\r\n            else if($('quicksearch').innerHTML=='作者')\r\n            {\r\n                if(isdefaultvalue) $('quicksearch_textinput').value = \"输入搜索关键字:作者名称\";\r\n            }\r\n            else if($('quicksearch').innerHTML=='版块')\r\n            {\r\n                if(isdefaultvalue) $('quicksearch_textinput').value = \"输入搜索关键字:版块名称\";\r\n            }\r\n         }\r\n         else\r\n         {\r\n\r\n            if($('quicksearch').innerHTML=='帖子标题' || $('quicksearch').innerHTML=='快速搜索' )\r\n            {\r\n                if($('quicksearch_textinput').value == \"输入搜索关键字:帖子标题\") $('quicksearch_textinput').value = \"\";\r\n            }\r\n            else if($('quicksearch').innerHTML=='空间日志')\r\n            {\r\n                if($('quicksearch_textinput').value == \"输入搜索关键字:空间日志\") $('quicksearch_textinput').value = \"\";\r\n            }\r\n            else if($('quicksearch').innerHTML=='相册标题')\r\n            {\r\n                if($('quicksearch_textinput').value == \"输入搜索关键字:相册标题\") $('quicksearch_textinput').value = \"\";\r\n            }\r\n            else if($('quicksearch').innerHTML=='作者')\r\n            {\r\n                if($('quicksearch_textinput').value == \"输入搜索关键字:作者名称\") $('quicksearch_textinput').value = \"\";\r\n            }\r\n            else if($('quicksearch').innerHTML=='版块')\r\n            {\r\n                if($('quicksearch_textinput').value == \"输入搜索关键字:版块名称\") $('quicksearch_textinput').value = \"\";\r\n            }\r\n         }\r\n    }\r\n    function bind_keyword(form) \r\n    {\r\n        if (form.keywordtype.value == '20') \r\n        {\r\n            form.action = '");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?keywords=' + escape(form.keywordf.value).replace(/\\+/g, '%2B').replace(/\\\"/g,'%22').replace(/\\'/g, '%27').replace(/\\//g,'%2F');\r\n        } \r\n        else if (form.keywordtype.value == '9')\r\n        {\r\n            form.action = '");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("forumsearch.aspx?q=' + escape(form.keywordf.value).replace(/\\+/g, '%2B').replace(/\\\"/g,'%22').replace(/\\'/g, '%27').replace(/\\//g,'%2F');\r\n        } \r\n        else if (form.keywordtype.value == '8') \r\n        {\r\n            form.action = '");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("search.aspx?advsearch=1';\r\n            form.keyword.value = '';\r\n            form.poster.value = form.keywordf.value != form.keywordf.defaultValue ? form.keywordf.value.replace(/\\+/g, '%2B').replace(/\\\"/g,'%22').replace(/\\'/g, '%27').replace(/\\//g,'%2F') : '';\r\n        } \r\n        else \r\n        {\r\n            form.action = '");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("search.aspx';\r\n            form.poster.value = '';\r\n            form.keyword.value = form.keywordf.value != form.keywordf.defaultValue ? form.keywordf.value.replace(/\\+/g, '%2B').replace(/\\\"/g,'%22').replace(/\\'/g, '%27').replace(/\\//g,'%2F') : '';\r\n            if (form.keywordtype.value == '2')\r\n                form.type.value = 'spacepost';\r\n            else if (form.keywordtype.value == '3')\r\n                form.type.value = 'album';\r\n            else \r\n                form.type.value = 'topic';\r\n        }\r\n    }\r\n</");
	templateBuilder.Append("script>");


	}	//end if

	templateBuilder.Append("\r\n		<a href=\"");
	templateBuilder.Append(config.Forumurl.ToString().Trim());
	templateBuilder.Append("\">");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("</a> &raquo; \r\n		 <strong>\r\n      ");
	if (cngi_login)
	{


	if (cngi_school=="buaa")
	{

	templateBuilder.Append("\r\n              <strong>通过CNGI08 自助注册 将自动绑定所注册的未来花园BT账号到 CNGI08[ 用户 ");
	templateBuilder.Append(cngi_name.ToString());
	templateBuilder.Append(" 来自 ");
	templateBuilder.Append(cngi_school.ToString());
	templateBuilder.Append("] （账号名可以任意取，不是必须使用学号）</strong>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n              <strong>通过CNGI08 用户注册 将自动绑定所注册的未来花园BT账号到 CNGI08[ 用户 ");
	templateBuilder.Append(cngi_name.ToString());
	templateBuilder.Append(" 来自 ");
	templateBuilder.Append(cngi_school.ToString());
	templateBuilder.Append(" ] （账号名可以任意取，不是必须使用CNGI认证编号）</strong>\r\n          ");
	}	//end if


	}
	else if (ssoCreateToken!="")
	{

	templateBuilder.Append("\r\n          <strong>北航校内用户 通过 i北航 i.buaa.edu.cn [ 用户 ");
	templateBuilder.Append(ssoUid.ToString());
	templateBuilder.Append(" ] 自助注册未来花园BT账号</strong>\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n          用户注册\r\n      ");
	}	//end if

	templateBuilder.Append("\r\n		</strong>\r\n	</div>\r\n</div>\r\n");
	}	//end if


	if (agree=="" && infloat!=1)
	{


	if (page_err==0)
	{


	if (config.Rules==1)
	{

	templateBuilder.Append("\r\n        <div class=\"wrap cl\">\r\n	        <div class=\"blr\">\r\n		        <h3 class=\"flb\"><em>用户注册协议</em></h3>\r\n		        <form id=\"form1\" name=\"form1\" method=\"post\" action=\"\">\r\n		        <div class=\"c cl floatwrap\">\r\n			        ");
	templateBuilder.Append(config.Rulestxt.ToString().Trim());
	templateBuilder.Append("\r\n		        </div>\r\n		        <p class=\"fsb pns cl\">\r\n			        <input name=\"agree\" type=\"hidden\" value=\"true\" />\r\n			        <button disabled=\"disabled\" type=\"submit\" id=\"btnagree\" class=\"pn pnc\"><span>同意</span></button>\r\n			        <button name=\"cancel\" id=\"cancel\" type=\"button\" onClick=\"javascript:location.replace('index.aspx')\" class=\"pn\"><span>不同意</span></button>	  \r\n			        <script type=\"text/javascript\">\r\n			        var secs = 5;\r\n			        var wait = secs * 1000;\r\n			        $(\"btnagree\").innerHTML = \"<span>同 意(\" + secs + \")</span>\";\r\n			        $(\"btnagree\").disabled = true;\r\n			        for(i = 1; i <= secs; i++) {\r\n				        window.setTimeout(\"update(\" + i + \")\", i * 1000);\r\n			        }\r\n			        window.setTimeout(\"timer()\", wait);\r\n			        function update(num, value) {\r\n				        if(num == (wait/1000)) {\r\n						        $(\"btnagree\").innerHTML = \"<span>同 意</span>\";\r\n				        } else {\r\n						        printnr = (wait / 1000) - num;\r\n						        $(\"btnagree\").innerHTML = \"<span>同 意(\" + printnr + \")</span>\";\r\n				        }\r\n			        }\r\n			        function timer() {\r\n				        $(\"btnagree\").disabled = false;\r\n				        $(\"btnagree\").innerHTML = \"<span>同 意</span>\";\r\n			        }\r\n			        </");
	templateBuilder.Append("script>\r\n		        </p>\r\n		        </form>\r\n	        </div>\r\n        </div>\r\n		");
			/*
			<script type="text/javascript">
			location.replace('register.aspx?agree=yes')
			</");
	templateBuilder.Append("script>
			*/
			

	}	//end if


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


	}
	else
	{


	if (createuser=="")
	{


	if (page_err==0)
	{


	if (infloat!=1)
	{

	templateBuilder.Append("\r\n	    <div class=\"wrap cl\">\r\n		    <div class=\"blr\" id=\"floatlayout_register\">\r\n	    ");
	}	//end if


	if (config.Rules==1)
	{

	templateBuilder.Append("\r\n		    <div id=\"bbrule\" style=\"display:none\">\r\n			    ");
	if (infloat==1)
	{

	templateBuilder.Append("\r\n					<h3 class=\"flb\" id=\"fctrl_register\" style=\"cursor: move;\"><em id=\"returnregmessage\" fwin=\"register\">网站服务条款</em><span><a title=\"关闭\" onclick=\"hideWindow('register')\" class=\"flbc\" onclick=\"hideWindow('register')\" title=\"关闭\">关闭</a></span></h3>\r\n			    ");
	}	//end if

	templateBuilder.Append("\r\n			    <div class=\"c cl floatwrap\">\r\n				    ");
	templateBuilder.Append(config.Rulestxt.ToString().Trim());
	templateBuilder.Append("\r\n			    </div>\r\n			    <p class=\"fsb pns cl\">\r\n				    <button type=\"submit\" id=\"btnagree\" class=\"pn pnc\"  onclick=\"javascript:$('agree').checked=true;$('bbrule').style.display='none';$('bbreg').style.display=''\"><span>同意</span></button>\r\n				    <button name=\"cancel\" id=\"cancel\"  onClick=\"hideWindow('register')\" class=\"pn\"><span>不同意</span></button>\r\n			    </p>\r\n		    </div>\r\n	    ");
	}	//end if


	if (infloat==1)
	{

	templateBuilder.Append("\r\n	    <div id=\"bbreg\">\r\n	        <h3 class=\"flb\"><em id=\"returnregmessage\">注册</em><span><a href=\"javascript:;\" class=\"flbc\" onclick=\"hideWindow('register')\" title=\"关闭\">关闭</a></span></h3>\r\n	        <div id=\"succeedmessage\" class=\"c cl\" style=\"display:none\"></div>\r\n	            <form id=\"form2\" name=\"form2\" method=\"post\" onsubmit=\" ");
	if (config.Rules==1)
	{

	templateBuilder.Append("if(!checkagreed()) return false;");
	}	//end if

	templateBuilder.Append("$('form2').action='");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("register.aspx?infloat=1&createuser=1");
	if (ssoCreateToken!="")
	{

	templateBuilder.Append("&ctoken=");
	templateBuilder.Append(ssoCreateToken.ToString());
	}	//end if

	templateBuilder.Append("&';ajaxpost('form2', 'returnregmessage', 'returnregmessage', 'onerror');return false;\" action=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("register.aspx?infloat=1&createuser=1");
	if (ssoCreateToken!="")
	{

	templateBuilder.Append("&ctoken=");
	templateBuilder.Append(ssoCreateToken.ToString());
	}	//end if

	templateBuilder.Append("&\">\r\n	    ");
	}
	else
	{

	templateBuilder.Append("\r\n            <h3 class=\"flb\"><em id=\"returnregmessage\"></em></h3>\r\n            <div id=\"succeedmessage\" class=\"c cl\" style=\"display:none\"></div>\r\n            <form id=\"form1\" name=\"form1\" method=\"post\" action=\"?createuser=1");
	if (ssoCreateToken!="")
	{

	templateBuilder.Append("&ctoken=");
	templateBuilder.Append(ssoCreateToken.ToString());
	}	//end if

	templateBuilder.Append("\">\r\n	    ");
	}	//end if

	templateBuilder.Append("\r\n	        <div class=\"c cl\">\r\n		        <div style=\"overflow:hidden;overflow-y:auto\" class=\"lgfm\" id=\"reginfo_a\">\r\n		          ");
	if (ssoCreateToken!="")
	{

	templateBuilder.Append("\r\n		          <span id=\"ihomeregisterspan\" >\r\n                  <button tabindex=\"6\" value=\"true\" name=\"regsubmit\" type=\"submit\" id=\"registerformsubmitihome\" class=\"pn\" ");
	if (config.Rules==1)
	{

	templateBuilder.Append("onclick=\"return checkagreed();\" ");
	}	//end if

	templateBuilder.Append("><span>立即自动创建未来花园BT账号</span></button>\r\n                  <br/><br/>\r\n                  <label><em>提示：</em><span class=\"ihome_reg_alert\">非常重要：如果您现在还不了解未来花园BT，请勿直接创建账号，先到 <a href=\"http://i.buaa.edu.cn/space.php?uid=8555\">未来花园BT的i北航主页</a> 了解之后再做决定</span></label>\r\n                  <label><em>用户名：</em>");
	templateBuilder.Append(DEFAULT_username.ToString());
	templateBuilder.Append("</label>\r\n                  <label><em>密码：</em>通过 i北航 登陆后可设置</label>\r\n                  <label><em>邮箱：</em>通过 i北航 登陆后可设置</label>\r\n                  <label><em>注意：</em><span class=\"ihome_reg_alert\">自动生成的用户名（格式为：数字_iHome）注册后#不可更改#，必须设置独立密码后方可脱离 i北航 独立登陆</span></label>\r\n                  <br/>\r\n                  <button tabindex=\"7\" value=\"false\" name=\"registerformsubmitihomeself\" type=\"button\" id=\"registerformsubmitihomeself\" class=\"pn\" onclick=\"javascript:switchtoselfregister();\" ><span>自定义用户名密码创建[推荐！]</span></button>\r\n                  <br/><br/>\r\n                  <script type=\"text/javascript\" reload=\"1\">\r\n                      function switchtoselfregister() {\r\n                          $('username').value='';\r\n                          $('password').value='';\r\n                          $('password2').value='';\r\n                          $('email').value='';\r\n                          $('email2').value='';\r\n                          $('activation_hidden').style.display='';\r\n                          $('ihomeregisterspan').style.display='none';\r\n                          $('registerformsubmit').style.display='';\r\n                    }\r\n                  </");
	templateBuilder.Append("script>\r\n		          </span>\r\n		          ");
	}	//end if

	templateBuilder.Append("\r\n			        <span id=\"activation_hidden\" ");
	if (ssoCreateToken!="")
	{

	templateBuilder.Append("style=\"display:none;\"");
	}	//end if

	templateBuilder.Append(">\r\n			        ");
	if (allowregister==true||ssoCreateToken!=""||cngi_login==true)
	{


	if (needinvitecode && cngi_login==false)
	{

	templateBuilder.Append("\r\n                      本站（bt.buaa6.edu.cn）目前采用邀请注册机制，你可以通过已经获得账号的同学朋友获得邀请码。邀请码不是免费获得的，需要消耗上传流量来购买。请珍惜邀请，珍惜账号。<br/><br/>\r\n                    <label><em>邀请码:</em><input name=\"registercode\" type=\"text\" id=\"registercode\" size=\"40\" value=\"");
	templateBuilder.Append(DEFAULT_invitecode.ToString());
	templateBuilder.Append("\"  maxlength=\"32\" class=\"txt\"/> *</label>\r\n                    ");
	}	//end if

	templateBuilder.Append("\r\n                    <label><em>用户名:</em><input type=\"text\" class=\"txt\" tabindex=\"1\"  value=\"");
	templateBuilder.Append(DEFAULT_username.ToString());
	templateBuilder.Append("\" maxlength=\"20\" size=\"25\" autocomplete=\"off\" name=\"");
	templateBuilder.Append(config.Antispamregisterusername.ToString().Trim());
	templateBuilder.Append("\" id=\"username\" onkeyup=\"checkusername(this.value);\"/> *</label>\r\n                    <label><em>密码:</em><input type=\"password\" class=\"txt\" tabindex=\"1\" value=\"");
	templateBuilder.Append(DEFAULT_password.ToString());
	templateBuilder.Append("\"  id=\"password\" size=\"25\" name=\"password\" onblur=\"return checkpasswd(this);\"/> *</label>	\r\n                    <label id=\"passwdpower\" style=\"display: none;\"><em>密码强度</em><strong id=\"showmsg\"></strong></label>\r\n                    <label><em>确认密码:</em><input type=\"password\" class=\"txt\" tabindex=\"1\" value=\"");
	templateBuilder.Append(DEFAULT_password.ToString());
	templateBuilder.Append("\" id=\"password2\" size=\"25\" name=\"password2\" onblur=\"checkdoublepassword(this.form)\"/> *</label>\r\n                    邮箱将用于重置密码等，填写后不能更改\r\n                    <label><em>Email:</em><input type=\"text\" class=\"txt\" value=\"");
	templateBuilder.Append(DEFAULT_email.ToString());
	templateBuilder.Append("\" tabindex=\"1\"  id=\"email\" size=\"25\" autocomplete=\"off\" name=\"");
	templateBuilder.Append(config.Antispamregisteremail.ToString().Trim());
	templateBuilder.Append("\" onblur=\"checkemail(this.value)\"/> *</label>\r\n                    <label><em>重复Email:</em><input name=\"email2\" type=\"text\" class=\"txt\" value=\"");
	templateBuilder.Append(DEFAULT_email.ToString());
	templateBuilder.Append("\" tabindex=\"1\"  id=\"email2\" size=\"25\" autocomplete=\"off\" name=\"");
	templateBuilder.Append(config.Antispamregisteremail.ToString().Trim());
	templateBuilder.Append("\" onblur=\"checkemail(this.value)\"/> *</label>\r\n                    ");
	if (config.Realnamesystem==1)
	{

	templateBuilder.Append("\r\n                    <label><em>真实姓名:</em><input name=\"realname\" type=\"text\" id=\"realname\" size=\"10\" class=\"txt\" /> *</label>\r\n                    <label><em>身份证:</em><input name=\"idcard\" type=\"text\" id=\"idcard\" size=\"20\" class=\"txt\" /> *</label>\r\n                    <label><em>移动电话:</em><input name=\"mobile\" type=\"text\" id=\"mobile\" size=\"20\" class=\"txt\" /> *</label>\r\n                    <label><em>固定电话:</em><input name=\"phone\" type=\"text\" id=\"phone\" size=\"20\" class=\"txt\" /> *</label>\r\n                    ");
	}	//end if

	templateBuilder.Append("\r\n                  \r\n                  </span>\r\n                  ");
	if (isseccode)
	{

	templateBuilder.Append("\r\n                      <div class=\"regsec\">\r\n                        <label style=\"display: inline;\"><em>验证: </em><span style=\"position: relative;\">\r\n                            ");
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

	templateBuilder.Append("</span>\r\n                        </label>\r\n                      </div>\r\n                  ");
	}	//end if


	}
	else
	{

	templateBuilder.Append("\r\n                  本站当前不允许普通注册<br/>请使用iHome或CNGI08通道注册<br/><br/><br/><br/>\r\n			       ");
	}	//end if


	if (cngi_login)
	{

	templateBuilder.Append("\r\n                      <h4>注册成功之后，CNGI08教育科研网统一身份认证账号和未来花园BT账号后将不能解除绑定</h4>\r\n                ");
	}
	else if (ssoCreateToken!="")
	{

	templateBuilder.Append("\r\n                      <h4>注册成功之后，航行iHome.buaa.edu.cn账号和未来花园BT账号后将不能解除绑定</h4>\r\n                ");
	}
	else
	{


	}	//end if

	templateBuilder.Append("\r\n                <br/>\r\n                ");
	if (!cngi_login&&ssoCreateToken=="")
	{

	templateBuilder.Append("\r\n                    <h4><a href=\"http://sp-bbs.buaa6.edu.cn/Shibboleth.sso/DS?target=http%3A%2F%2Fsp-bbs.buaa6.edu.cn%2Fregister.aspx\">通过 CNGI08教育科研网统一<br/>身份认证免邀请注册（暂不可用）</a></h4>\r\n<br/><h4><a href=\"http://i.buaa.edu.cn/buaasso.php?m=sso&to=buaabt\">通过 ihome i北航社区免邀请注册</a></h4>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n		        </div>\r\n		        <div class=\"lgf\">\r\n			        <h4>已有帐号？\r\n				        ");
	if (infloat==1)
	{

	templateBuilder.Append("\r\n				        \r\n				        <a onclick=\"hideWindow('register');showWindow('login', this.href);\" href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("login.aspx\" class=\"xg2\">现在登录</a>\r\n				        \r\n				        ");
	}
	else
	{


	if (cngi_login)
	{

	templateBuilder.Append("\r\n                          <a href=\"cngilogin.aspx\" title=\"现在登录\" class=\"xg2\">现在绑定</a>\r\n                    ");
	}
	else if (ssoCreateToken!="")
	{

	templateBuilder.Append("\r\n                          <a href=\"login.aspx?ctoken=");
	templateBuilder.Append(ssoCreateToken.ToString());
	templateBuilder.Append("\" title=\"现在登录\" class=\"xg2\">现在绑定</a>\r\n                    ");
	}
	else
	{

	templateBuilder.Append("\r\n                          <a href=\"login.aspx\" title=\"现在登录\" class=\"xg2\">现在登录</a>\r\n                    ");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n			        </h4>\r\n			        \r\n		        </div>\r\n	        </div>\r\n	        <p class=\"fsb pns cl\">\r\n		        <span id=\"reginfo_b_btn\">\r\n		        <button tabindex=\"5\" value=\"true\" name=\"regsubmit\" type=\"submit\" id=\"registerformsubmit\" \r\n		        ");
	if (ssoCreateToken!=""||(allowregister==false&&cngi_login==false))
	{

	templateBuilder.Append("style=\"display:none;\"");
	}	//end if

	templateBuilder.Append(" class=\"pn\" ");
	if (config.Rules==1)
	{

	templateBuilder.Append("onclick=\"return checkagreed();\" ");
	}	//end if

	templateBuilder.Append("><span>创建用户</span></button>\r\n		        ");
	if (config.Rules==1)
	{

	templateBuilder.Append("\r\n		        <input type=\"checkbox\" id=\"agree\" value=\"true\" name=\"agree\" class=\"checkbox\" style=\"margin-left:5px;\"/><label for=\"agreebbrule\">同意<a onclick=\"javascript:$('bbrule').style.display='';$('bbreg').style.display='none'\" href=\"javascript:;\">网站服务条款</a></label>\r\n			        <script type=\"text/javascript\" reload=\"1\">\r\n			            function checkagreed() {\r\n			                $('returnregmessage').className = ''; \r\n						        if ($('agree').checked == true) {\r\n							        return true;\r\n						        }\r\n						        else {\r\n						            $('returnregmessage').innerHTML = \"请确认《网络服务条款》\";\r\n						            $('returnregmessage').className = 'onerror';\r\n							        return false;\r\n						        }\r\n				        }\r\n			        </");
	templateBuilder.Append("script>\r\n		        ");
	}	//end if

	templateBuilder.Append("\r\n		        </span>\r\n	        </p>\r\n	    </form>\r\n        </div>\r\n        </div>\r\n        </div>\r\n        <script type=\"text/javascript\">\r\n	        var PasswordStrength ={\r\n		        Level : [\"极佳\",\"一般\",\"较弱\",\"太弱\"],\r\n		        LevelValue : [35,25,15,0],//强度值\r\n		        Factor : [2,1,4],//字符加数,分别为字母，数字，其它\r\n		        KindFactor : [0,0,5,10],//密码含几种组成的加数 \r\n		        Regex : [/[a-zA-Z]/g,/\\d/g,/[^a-zA-Z0-9]/g] //字符正则数字正则其它正则\r\n		        }\r\n		\r\n	        PasswordStrength.StrengthValue = function(pwd)\r\n	        {\r\n		        var strengthValue = 0;\r\n		        var ComposedKind = 0;\r\n		        for(var i = 0 ; i < this.Regex.length;i++)\r\n		        {\r\n			        var chars = pwd.match(this.Regex[i]);\r\n			        if(chars != null)\r\n			        {\r\n				        strengthValue += chars.length * this.Factor[i];\r\n				        ComposedKind ++;\r\n			        }\r\n		        }\r\n		        strengthValue += this.KindFactor[ComposedKind];\r\n		        return strengthValue;\r\n	        } \r\n\r\n	        PasswordStrength.StrengthLevel = function(pwd)\r\n	        {\r\n		        var value = this.StrengthValue(pwd);\r\n		        for(var i = 0 ; i < this.LevelValue.length ; i ++)\r\n		        {\r\n			        if(value >= this.LevelValue[i] )\r\n				        return this.Level[i];\r\n		        }\r\n	        }\r\n\r\n	        function checkpasswd(o)\r\n	        {\r\n		        var pshowmsg = '密码不得少于6个字符';\r\n		        if(o.value.length<6)  {\r\n		            $(\"returnregmessage\").innerHTML = pshowmsg;\r\n		            $(\"returnregmessage\").className = 'onerror';\r\n		        } \r\n		        else\r\n		        {\r\n		 \r\n		           var showmsg=PasswordStrength.StrengthLevel(o.value);\r\n		           switch(showmsg) {\r\n		               case \"太弱\": showmsg += \" <img src='\" + forumpath + \"images/level/1.gif' width='88' height='11' />\"; break;\r\n		               case \"较弱\": showmsg += \" <img src='\" + forumpath + \"images/level/2.gif' width='88' height='11' />\"; break;\r\n		               case \"一般\": showmsg += \" <img src='\" + forumpath + \"images/level/3.gif' width='88' height='11' />\"; break;\r\n		               case \"极佳\": showmsg += \" <img src='\" + forumpath + \"images/level/4.gif' width='88' height='11' />\"; break;\r\n		           }\r\n		           $('passwdpower').style.display='';\r\n		           $('showmsg').innerHTML = showmsg;\r\n		           $('returnregmessage').className = '';\r\n		           $('returnregmessage').innerHTML = '注册';		   \r\n		        }\r\n		 \r\n//		        if(pshowmsg!='' &&  pshowmsg!=null && pshowmsg!=undefined)\r\n//		        {\r\n//		        $('returnregmessage').innerHTML=pshowmsg;\r\n//		        $('returnregmessage').className='onerror';\r\n//		        }\r\n//		        else\r\n//		        {\r\n//		        $('returnregmessage').className='';\r\n//		        $('returnregmessage').innerHTML='注册';\r\n//		        }\r\n        \r\n \r\n	        }\r\n	        function checkemail(strMail)\r\n	        {\r\n		        var str;\r\n		        if(strMail.length==0) return false; \r\n		        var objReg = new RegExp(\"[A-Za-z0-9-_]+@[A-Za-z0-9-_]+[\\.][A-Za-z0-9-_]\") \r\n		        var IsRightFmt = objReg.test(strMail) \r\n		        var objRegErrChar = new RegExp(\"[^a-z0-9-._@]\",\"ig\") \r\n		        var IsRightChar = (strMail.search(objRegErrChar)==-1) \r\n		        var IsRightLength = strMail.length <= 60 \r\n		        var IsRightPos = (strMail.indexOf(\"@\",0) != 0 && strMail.indexOf(\".\",0) != 0 && strMail.lastIndexOf(\"@\")+1 != strMail.length && strMail.lastIndexOf(\".\")+1 != strMail.length) \r\n		        var IsNoDupChar = (strMail.indexOf(\"@\",0) == strMail.lastIndexOf(\"@\"))\r\n		        if (!(IsRightFmt && IsRightChar && IsRightLength && IsRightPos && IsNoDupChar))\r\n                {\r\n		         str=\"E-mail 地址无效，请提供真实Email，以便找回密码和论坛通知所用。\";\r\n		         }\r\n	            if(str!='' &&  str!=null && str!=undefined)\r\n		        {\r\n		        $('returnregmessage').innerHTML=str;\r\n		        $('returnregmessage').className='onerror';\r\n		        }\r\n		        else\r\n		        {\r\n		        $('returnregmessage').className='';\r\n		        $('returnregmessage').innerHTML='注册';\r\n		        }\r\n	        }\r\n	        function htmlEncode(source, display, tabs)\r\n	        {\r\n		        function special(source)\r\n		        {\r\n			        var result = '';\r\n			        for (var i = 0; i < source.length; i++)\r\n			        {\r\n				        var c = source.charAt(i);\r\n				        if (c < ' ' || c > '~')\r\n				        {\r\n					        c = '&#' + c.charCodeAt() + ';';\r\n				        }\r\n				        result += c;\r\n			        }\r\n			        return result;\r\n		        }\r\n		\r\n		        function format(source)\r\n		        {\r\n			        // Use only integer part of tabs, and default to 4\r\n			        tabs = (tabs >= 0) ? Math.floor(tabs) : 4;\r\n			\r\n			        // split along line breaks\r\n			        var lines = source.split(/\\r\\n|\\r|\\n/);\r\n			\r\n			        // expand tabs\r\n			        for (var i = 0; i < lines.length; i++)\r\n			        {\r\n				        var line = lines[i];\r\n				        var newLine = '';\r\n				        for (var p = 0; p < line.length; p++)\r\n				        {\r\n					        var c = line.charAt(p);\r\n					        if (c === '\\t')\r\n					        {\r\n						        var spaces = tabs - (newLine.length % tabs);\r\n						        for (var s = 0; s < spaces; s++)\r\n						        {\r\n							        newLine += ' ';\r\n						        }\r\n					        }\r\n					        else\r\n					        {\r\n						        newLine += c;\r\n					        }\r\n				        }\r\n				        // If a line starts or ends with a space, it evaporates in html\r\n				        // unless it's an nbsp.\r\n				        newLine = newLine.replace(/(^ )|( $)/g, '&nbsp;');\r\n				        lines[i] = newLine;\r\n			        }\r\n			\r\n			        // re-join lines\r\n			        var result = lines.join('<br />');\r\n			\r\n			        // break up contiguous blocks of spaces with non-breaking spaces\r\n			        result = result.replace(/  /g, ' &nbsp;');\r\n			\r\n			        // tada!\r\n			        return result;\r\n		        }\r\n\r\n		        var result = source;\r\n		\r\n		        // ampersands (&)\r\n		        result = result.replace(/\\&/g,'&amp;');\r\n\r\n		        // less-thans (<)\r\n		        result = result.replace(/\\</g,'&lt;');\r\n\r\n		        // greater-thans (>)\r\n		        result = result.replace(/\\>/g,'&gt;');\r\n		\r\n		        if (display)\r\n		        {\r\n			        // format for display\r\n			        result = format(result);\r\n		        }\r\n		        else\r\n		        {\r\n			        // Replace quotes if it isn't for display,\r\n			        // since it's probably going in an html attribute.\r\n			        result = result.replace(new RegExp('\"','g'), '&quot;');\r\n		        }\r\n\r\n		        // special characters\r\n		        result = special(result);\r\n		\r\n		        // tada!\r\n		        return result;\r\n	        }\r\n\r\n	        var profile_username_toolong = '您的用户名超过 20 个字符，请输入一个较短的用户名。';\r\n	        var profile_username_tooshort = '您输入的用户名小于3个字符, 请输入一个较长的用户名。';\r\n	        var profile_username_pass = \"<img src='");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/check_right.gif'/>\";\r\n\r\n	        function checkusername(username)\r\n	        {\r\n		        var unlen = username.replace(/[^\\x00-\\xff]/g, \"**\").length;\r\n\r\n		        if(unlen < 3 || unlen > 20) {\r\n			        $(\"returnregmessage\").innerHTML =(unlen < 3 ? profile_username_tooshort : profile_username_toolong);\r\n			        $('returnregmessage').className = 'onerror';\r\n			        return;\r\n		        }\r\n		        ajaxRead(\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("tools/ajax.aspx?t=checkusername&username=\" + escape(username), \"showcheckresult(obj,'\" + escape(username) + \"');\");\r\n	        }\r\n\r\n	        function showcheckresult(obj, username)\r\n	        {\r\n		        var res = obj.getElementsByTagName('result');\r\n		        var result = \"\";\r\n		        if (res[0] != null && res[0] != undefined)\r\n		        {\r\n			        if (res[0].childNodes.length > 1) {\r\n				        result = res[0].childNodes[1].nodeValue;\r\n			        } else {\r\n				        result = res[0].firstChild.nodeValue;    		\r\n			        }\r\n		        }\r\n		        if (result == \"1\")\r\n		        {\r\n		            var tips=\"对不起，您输入的用户名 \\\"\" + htmlEncode(unescape(username), true, 4) + \"\\\" 已经被他人使用或被禁用。\";\r\n			        $('returnregmessage').innerHTML=tips;\r\n			        $('returnregmessage').className='onerror';\r\n		        }\r\n		        else\r\n		        {\r\n			        $('returnregmessage').className='';\r\n			         $('returnregmessage').innerHTML='注册';\r\n		        }\r\n	        }\r\n\r\n	        function checkdoublepassword(theform)\r\n	        {\r\n	          var pw1 = theform.password.value;\r\n	          var pw2 = theform.password2.value;\r\n	          if(pw1=='' &&  pw2=='')\r\n	          {\r\n	          return;\r\n	          }\r\n	          var str;\r\n	  \r\n		         if(pw1!=pw2)\r\n		         {\r\n		          str =\"两次输入的密码不一致\";\r\n		         }\r\n		          if(str!='' &&  str!=null && str!=undefined)\r\n		          {\r\n		          $('returnregmessage').innerHTML=str;\r\n		          $('returnregmessage').className='onerror';\r\n		          }\r\n		          else\r\n		          {\r\n		          $('returnregmessage').className='';\r\n		          $('returnregmessage').innerHTML='注册';\r\n		          }\r\n	        }\r\n	    </");
	templateBuilder.Append("script>\r\n	    <script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/ajax.js\"></");
	templateBuilder.Append("script>\r\n    ");
	}
	else
	{


	if (infloat==1)
	{

	templateBuilder.Append("\r\n        	<h3 class=\"flb\"><em>出现了");
	templateBuilder.Append(page_err.ToString());
	templateBuilder.Append("个错误</em><span><a href=\"javascript:;\" class=\"flbc\" onclick=\"hideWindow('register')\" title=\"关闭\">关闭</a></span></h3>\r\n            <div class=\"c cl\" id=\"errormsg\">\r\n		        <div class=\"msg_inner error_msg\">\r\n		            <p style=\"margin-bottom:16px;line-height:60px;\">");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n		        </div>\r\n	        </div>\r\n        ");
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


	}
	else
	{


	if (createuser!="")
	{


	if (infloat==1)
	{


	if (page_err==0)
	{

	templateBuilder.Append("	\r\n	            <script type=\"text/javascript\">\r\n	                $('form2').style.display='none';\r\n	                $('returnregmessage').className='';\r\n	            </");
	templateBuilder.Append("script>\r\n	            <div class=\"msgbox cl\" id=\"succeemessage\">\r\n		            <div class=\"msg_inner\">\r\n		            <p style=\"margin-bottom:16px;\">");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n		            ");
	if (msgbox_url!="")
	{

	templateBuilder.Append("\r\n		            <p><a href=\"javascript:;\" onclick=\"location.reload()\" class=\"xg2\">如果长时间没有响应请点击此处</a></p>\r\n		            <script type=\"text/javascript\">setTimeout('location.reload()', 3000);</");
	templateBuilder.Append("script>\r\n		            ");
	}	//end if

	templateBuilder.Append("\r\n		            </div>\r\n	            </div>\r\n	            <script>\r\n	                $('succeedmessage').style.display='';\r\n	                $('succeedmessage').innerHTML=$('succeemessage').innerHTML;\r\n	                $('returnregmessage').innerHTML='注册';\r\n	            </");
	templateBuilder.Append("script>	\r\n	        ");
	}
	else
	{

	templateBuilder.Append("\r\n	            <p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n	        ");
	}	//end if


	}
	else
	{


	if (page_err==0)
	{


	templateBuilder.Append("<div class=\"wrap s_clear\" id=\"wrap\">\r\n<div class=\"main\">\r\n	<div class=\"msgbox\">\r\n		<h1>");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("　提示信息</h1>\r\n		<hr class=\"solidline\"/>\r\n		<div class=\"msg_inner\">\r\n			<p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n			");
	if (msgbox_url!="")
	{

	templateBuilder.Append("\r\n			<p><a href=\"");
	templateBuilder.Append(msgbox_url.ToString());
	templateBuilder.Append("\">如果浏览器没有转向, 请点击这里.</a></p>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n		</div>\r\n	</div>\r\n</div>\r\n</div>");


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


	}	//end if


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
