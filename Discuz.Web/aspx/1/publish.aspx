<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.publish" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>
<%@ Import namespace="Discuz.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2015/1/7 16:52:11.
		本页面代码由Discuz!NT模板引擎生成于 2015/1/7 16:52:11. 
	*/

	base.OnInit(e);

	templateBuilder.Capacity = 236000;



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



	templateBuilder.Append("\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/bbcode.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/editor.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/futuregarden.js\"></");
	templateBuilder.Append("script>\r\n");
	if (infloat!=1)
	{

	templateBuilder.Append("\r\n<div class=\"wrap cl pageinfo\">\r\n	<div id=\"nav\">\r\n	");
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

	templateBuilder.Append("\r\n	<a href=\"");
	templateBuilder.Append(config.Forumurl.ToString().Trim());
	templateBuilder.Append("\" class=\"title\">");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("</a> &raquo; ");
	if (forum.Pathlist!="")
	{
	templateBuilder.Append(ShowForumAspxRewrite(forum.Pathlist.Trim(),forumid,forumpageid).ToString().Trim());
	templateBuilder.Append("  &raquo; ");
	}	//end if

	templateBuilder.Append("<strong>发布新种子</strong>\r\n	</div>\r\n</div>\r\n ");
	}	//end if

	templateBuilder.Append("\r\n<script type=\"text/javascript\" reload=\"1\">\r\nvar postminchars = parseInt('");
	templateBuilder.Append(config.Minpostsize.ToString().Trim());
	templateBuilder.Append("');\r\nvar postmaxchars = parseInt('");
	templateBuilder.Append(config.Maxpostsize.ToString().Trim());
	templateBuilder.Append("');\r\nvar disablepostctrl = parseInt('");
	templateBuilder.Append(disablepost.ToString());
	templateBuilder.Append("');\r\nvar forumpath = \"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("\";\r\nvar posturl=forumpath+'publish.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&forumpage=");
	templateBuilder.Append(forumpageid.ToString());
	templateBuilder.Append("';\r\nvar postaction='");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("publish.aspx?infloat=1&forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&'\r\n</");
	templateBuilder.Append("script>\r\n");
	if (page_err==0)
	{


	if (ispost)
	{


	if (infloat==1)
	{

	templateBuilder.Append("\r\n        ");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("\r\n    ");
	}
	else
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


	}	//end if


	}
	else
	{

	templateBuilder.Append("\r\n<div class=\"wrap cl post\">\r\n<script type=\"text/javascript\">\r\nfunction geteditormessage(theform)\r\n{\r\n	var message = wysiwyg ? html2bbcode(getEditorContents()) : (!theform.parseurloff.checked ? parseurl(theform.message.value) : theform.message.value);\r\n	theform.message.value = message;\r\n}\r\n</");
	templateBuilder.Append("script>\r\n<form method=\"post\" name=\"postform\" id=\"postform\" action=\"\" enctype=\"multipart/form-data\" onsubmit=\"return checkinput(this);\">\r\n    ");
	    string formatNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
	    
	int postid = 0;
	
	templateBuilder.Append("\r\n    <input type=\"hidden\" name=\"posttime\" id=\"posttime\" value=\"");
	templateBuilder.Append(formatNow.ToString());
	templateBuilder.Append("\" />\r\n	");
	templateBuilder.Append("<div id=\"editorbox\">\r\n	<div class=\"edt_main\">\r\n	<div class=\"edt_content cl\">\r\n		");
	if (special=="" && topic.Special>0)
	{


	if (topic.Special==1)
	{

	 special = "poll";
	

	}	//end if


	if (topic.Special==2 || topic.Special==3)
	{

	 special = "bonus";
	

	}	//end if


	if (topic.Special==4)
	{

	 special = "debate";
	

	}	//end if


	}	//end if

	bool adveditor = (special!="" || topic.Special>0)&&isfirstpost;
	
	string action = pagename.Replace("post","").Replace(".aspx","").Replace("topic","newthread");
	
	string actiontitle = "";
	
	templateBuilder.Append("\r\n		<ul class=\"f_tab cl mbw\">\r\n			");
	if (pagename=="publish.aspx")
	{

	 actiontitle = "发布" + typedescription + "种子";
	
	templateBuilder.Append("\r\n\r\n              <li");
	if (publishtype=="movie")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=movie')\" href=\"javascript:;\">&nbsp;&nbsp;电影&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="tv")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=tv')\" href=\"javascript:;\">&nbsp;&nbsp;剧集&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="comic")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=comic')\" href=\"javascript:;\">&nbsp;&nbsp;动漫&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="music")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=music')\" href=\"javascript:;\">&nbsp;&nbsp;音乐&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="game")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=game')\" href=\"javascript:;\">&nbsp;&nbsp;游戏&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="discovery")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=discovery')\" href=\"javascript:;\">&nbsp;&nbsp;纪录&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="sport")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=sport')\" href=\"javascript:;\">&nbsp;&nbsp;体育&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="entertainment")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=entertainment')\" href=\"javascript:;\">&nbsp;&nbsp;综艺&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="software")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=software')\" href=\"javascript:;\">&nbsp;&nbsp;软件&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="staff")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=staff')\" href=\"javascript:;\">&nbsp;&nbsp;学习&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="video")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=video')\" href=\"javascript:;\">&nbsp;&nbsp;视频&nbsp;&nbsp;</a></li>\r\n              <li");
	if (publishtype=="other")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('publish.aspx?type=other')\" href=\"javascript:;\">&nbsp;&nbsp;其他&nbsp;&nbsp;</a></li>\r\n\r\n			");
	}
	else if (pagename=="edit.aspx")
	{

	 actiontitle = "编辑" + typedescription + "种子";
	
	 isfirstpost = true;
	

	if (canchangetype)
	{

	templateBuilder.Append("\r\n                  <li");
	if (publishtype=="movie")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=movie&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;电影&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="tv")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=tv&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;剧集&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="comic")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=comic&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;动漫&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="music")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=music&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;音乐&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="game")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=game&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;游戏&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="discovery")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=discovery&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;纪录&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="sport")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=sport&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;体育&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="entertainment")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=entertainment&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;综艺&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="software")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=software&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;软件&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="staff")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=staff&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;学习&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="video")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=video&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;视频&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="other")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a onclick=\"switchpost('edit.aspx?type=other&seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\" href=\"javascript:;\" title=\"\">&nbsp;&nbsp;其他&nbsp;&nbsp;</a></li>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n                  <li");
	if (publishtype=="movie")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;电影&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="tv")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;剧集&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="comic")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;动漫&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="music")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;音乐&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="game")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;游戏&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="discovery")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;纪录&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="sport")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;体育&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="entertainment")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;综艺&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="software")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;软件&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="staff")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;学习&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="video")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;视频&nbsp;&nbsp;</a></li>\r\n                  <li");
	if (publishtype=="other")
	{

	templateBuilder.Append(" class=\"cur_tab\"");
	}	//end if

	templateBuilder.Append("><a title=\"\" style=\"cursor:default;\">&nbsp;&nbsp;其他&nbsp;&nbsp;</a></li>\r\n          ");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n		</ul>\r\n		");	char comma = ',';
	
	string editorid = "e";
	
	int thumbwidth = 400;
	
	int thumbheight = 300;
	
	templateBuilder.Append("\r\n		<div id=\"postbox\">\r\n		<div class=\"pbt cl\">\r\n			<input type=\"hidden\" name=\"iconid\" id=\"iconid\" value=\"");
	templateBuilder.Append(topic.Iconid.ToString().Trim());
	templateBuilder.Append("\" />\r\n		");
	if (special=="" && isfirstpost)
	{

	templateBuilder.Append("\r\n		<div class=\"ftid\">\r\n			<a id=\"icon\" class=\"z\" onmouseover=\"InFloat='floatlayout_");
	templateBuilder.Append(action.ToString());
	templateBuilder.Append("';showMenu(this.id)\"><img id=\"icon_img\" src=\"");
	templateBuilder.Append(posticondir.ToString());
	templateBuilder.Append("/");
	templateBuilder.Append(topic.Iconid.ToString().Trim());
	templateBuilder.Append(".gif\" style=\"margin-top:4px;\"/></a>\r\n		</div>\r\n		<ul id=\"icon_menu\" class=\"sltm\" style=\"display:none\">\r\n		");	string icons = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15";
	

	int id__loop__id=0;
	foreach(string id in icons.Split(comma))
	{
		id__loop__id++;

	templateBuilder.Append("\r\n			<li><a href=\"javascript:;\"><img onclick=\"switchicon(");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(", this)\" src=\"");
	templateBuilder.Append(posticondir.ToString());
	templateBuilder.Append("/");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(".gif\" alt=\"\" /></a></li>\r\n		");
	}	//end loop

	templateBuilder.Append("\r\n		</ul>\r\n		");
	}	//end if


	if (forum.Applytopictype==1 && topictypeselectoptions!=""&&isfirstpost)
	{

	templateBuilder.Append("\r\n			<div class=\"ftid\">\r\n				<select name=\"typeid\" id=\"typeid\">");
	templateBuilder.Append(topictypeselectoptions.ToString());
	templateBuilder.Append("</select>\r\n			</div>\r\n            <script type=\"text/javascript\" reload=\"1\">$('typeid').value = '");
	templateBuilder.Append(topic.Typeid.ToString().Trim());
	templateBuilder.Append("';</");
	templateBuilder.Append("script>\r\n			<script type=\"text/javascript\">simulateSelect(\"typeid\");</");
	templateBuilder.Append("script>\r\n		");
	}	//end if


	if (!isfirstpost && (topic.Special==4||special=="debate"))
	{

	templateBuilder.Append("\r\n			<div class=\"ftid\">\r\n				<select id=\"debateopinion\" name=\"debateopinion\">\r\n					<option value=\"0\">观点</option>\r\n					<option value=\"1\">正方</option>\r\n					<option value=\"2\">反方</option>\r\n				</select>\r\n			</div>\r\n			<script type=\"text/javascript\">simulateSelect(\"debateopinion\");</");
	templateBuilder.Append("script>\r\n			<script type=\"text/javascript\" reload=\"1\">$('debateopinion').selectedIndex = parseInt(getQueryString(\"debate\"));</");
	templateBuilder.Append("script>\r\n		");
	}	//end if

	templateBuilder.Append("\r\n			<span class=\"z\">\r\n            <input name=\"");
	templateBuilder.Append(config.Antispamposttitle.ToString().Trim());
	templateBuilder.Append("\" type=\"text\" id=\"title\" value=\"种子标题将由分类信息自动生成，请直接填写分类信息表，粗体项目为必填内容\" class=\"txt postpx\" style=\"width:770px;\"/>\r\n		");
	if (action=="reply" || postinfo.Layer>0)
	{

	templateBuilder.Append("\r\n			<cite class=\"tips\">(可选)</cite>\r\n		");
	}	//end if


	if (canhtmltitle)
	{

	templateBuilder.Append("<a href=\"###\" id=\"titleEditorButton\" onclick=\"RootTitleEditor();\" class=\"xg2\" style=\"margin-left:10px;\">高级编辑</a>");
	}	//end if

	templateBuilder.Append("\r\n            </span>\r\n		");
	if (needaudit)
	{

	templateBuilder.Append("<em class=\"needverify\">需审核</em>");
	}	//end if

	templateBuilder.Append("\r\n		</div>\r\n		");
	if (canhtmltitle)
	{

	templateBuilder.Append("\r\n		<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/simplyeditor.js\" reload=\"1\"></");
	templateBuilder.Append("script>\r\n		<div class=\"pbt cl\" id=\"editorDiv\" style=\"display: none;\">\r\n			<div class=\"title_editor\" id=\"titleEditorDiv\">\r\n                <script type=\"text/javascript\">\r\n                    var titleEditor;\r\n                    function RootTitleEditor(){\r\n                        if($('editorDiv').style.display == 'none')\r\n                        AdvancedTitleEditor();\r\n                        else\r\n                        TextTitleBox();\r\n                    }\r\n                    function AdvancedTitleEditor() {\r\n                        $('editorDiv').style.display = '';\r\n                        $('title').style.display = 'none';\r\n                        if(titleEditor==null){\r\n                            titleEditor = new SimplyEditor('htmltitle', 'titleEditorDiv', '");
	templateBuilder.Append(cssdir.ToString());
	templateBuilder.Append("', '");
	templateBuilder.Append(htmltitle.ToString());
	templateBuilder.Append("');\r\n                        }\r\n                        $('titleEditorButton').innerHTML='普通编辑';\r\n                    }\r\n                    function TextTitleBox(){\r\n                        $('editorDiv').style.display = 'none';\r\n                        $('title').style.display = '';\r\n                        $('titleEditorButton').innerHTML='高级编辑';\r\n                    }\r\n//                    $('titleEditorButton').onclick = function () {\r\n//                        AdvancedTitleEditor();\r\n//                    };\r\n                    ");
	if (htmltitle!="")
	{

	templateBuilder.Append("\r\n			            AdvancedTitleEditor();\r\n			        ");
	}	//end if

	templateBuilder.Append("\r\n                </");
	templateBuilder.Append("script>\r\n			</div>\r\n		</div>\r\n		");
	}	//end if


	if (adveditor)
	{

	templateBuilder.Append("\r\n		<div id=\"specialpost\" class=\"pbt cl\"></div>\r\n		<script type=\"text/javascript\" reload=\"1\">\r\n			_attachEvent(window, \"load\", function(){ \r\n			if($('specialposttable')) {\r\n				$('specialpost').innerHTML = $('specialposttable').innerHTML;\r\n				$('specialposttable').innerHTML = '';\r\n			}\r\n			});\r\n		</");
	templateBuilder.Append("script>\r\n		");
	}	//end if


	templateBuilder.Append("<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/post.js\" reload=\"1\" ></");
	templateBuilder.Append("script>\r\n");
	    /*下方代码会在_postattachment中的大量程序中使用*/
	
	templateBuilder.Append("\r\n<script type=\"text/javascript\">\r\n    var TABLEBG = '#FFF';//'{  WRAPBG  }';\r\n    var uid = parseInt('");
	templateBuilder.Append(userid.ToString());
	templateBuilder.Append("');\r\n\r\n    var special = parseInt('0');\r\n    var charset = 'utf-8';\r\n    var thumbwidth = parseInt(400);\r\n    var thumbheight = parseInt(300);\r\n    var extensions = '");
	templateBuilder.Append(attachextensions.ToString());
	templateBuilder.Append("';\r\n    var ATTACHNUM = {'imageused':0,'imageunused':0,'attachused':0,'attachunused':0};\r\n    var pid = parseInt('");
	templateBuilder.Append(topicid.ToString());
	templateBuilder.Append("');\r\n    var fid = ");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append(";\r\n    var custombbcodes = { ");
	templateBuilder.Append(customeditbuttons.ToString());
	templateBuilder.Append(" };\r\n</");
	templateBuilder.Append("script>\r\n<div class=\"edt cl\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_body\">\r\n	<div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_controls\" class=\"bar\">\r\n		<div class=\"y\">\r\n			<div class=\"b2r nbl nbr\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_5\">\r\n				<p><a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_undo\" title=\"撤销\">Undo</a></p>\r\n				<p><a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_redo\" title=\"重做\">Redo</a></p>\r\n			</div>\r\n			<div class=\"z\">\r\n				<span class=\"mbn\"><a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_simple\"></a><a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_fullswitcher\"></a></span>\r\n				<label id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_switcher\" class=\"bar_swch ptn\"><input type=\"checkbox\" class=\"pc\" name=\"checkbox\" value=\"0\" ");
	if (config.Defaulteditormode==0)
	{

	templateBuilder.Append("checked=\"checked\"");
	}	//end if

	templateBuilder.Append(" onclick=\"switchEditor(this.checked?0:1)\" />代码模式</label>\r\n			</div>\r\n		</div>\r\n		<div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_button\" class=\"cl\">\r\n			<div class=\"b1r\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_s0\">\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_paste\" title=\"粘贴\">粘贴</a>\r\n			</div>\r\n			<div class=\"b2r nbr\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_s2\">\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_fontname\" class=\"dp\" title=\"设置字体\"><span id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_font\">字体</span></a>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_fontsize\" class=\"dp\" title=\"设置文字大小\"><span id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_size\">大小</span></a>\r\n				<br id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_1\" />\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_bold\" title=\"粗体\">B</a>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_italic\" title=\"文字斜体\">I</a>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_underline\" title=\"文字加下划线\">U</a>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_forecolor\" title=\"设置文字颜色\">Color</a>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_url\" title=\"添加链接\">Url</a>\r\n				<span id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_8\">\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_unlink\" title=\"移除链接\">Unlink</a>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_inserthorizontalrule\" title=\"分隔线\">Hr</a>\r\n				</span>\r\n			</div>\r\n			<div class=\"b2r nbl\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_2\">\r\n				<p id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_3\"><a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_tbl\" title=\"添加表格\">Table</a></p>\r\n				<p>	<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_removeformat\" title=\"清除文本格式\">Removeformat</a></p>\r\n			</div>\r\n			<div class=\"b2r\">\r\n				<p>\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_justifyleft\" title=\"居左\">Left</a>\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_justifycenter\" title=\"居中\">Center</a>\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_justifyright\" title=\"居右\">Right</a>\r\n				</p>\r\n				<p id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_4\">\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_autotypeset\" title=\"自动排版\">Autotypeset</a>\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_insertorderedlist\" title=\"排序的列表\">Orderedlist</a>\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_insertunorderedlist\" title=\"未排序列表\">Unorderedlist</a>\r\n				</p>\r\n			</div>\r\n			<div class=\"b1r\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_s1\">\r\n                ");
	if (config.Smileyinsert==1 && forum.Allowsmilies==1)
	{

	templateBuilder.Append("\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_sml\" title=\"添加表情\">表情</a>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n				<div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_imagen\" style=\"display:none\">!</div>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image\" title=\"添加图片\">图片</a>\r\n				");
	if (canpostattach)
	{

	templateBuilder.Append("\r\n				<div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_attachn\" style=\"display:none\">!</div>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_attach\" title=\"添加附件\">附件</a>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_aud\" title=\"添加音乐\">音乐</a>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_vid\" title=\"添加视频\">视频</a>\r\n				<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_fls\" title=\"添加 Flash\">Flash</a>\r\n			</div>\r\n			<div class=\"b2r nbr\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_6\">\r\n				<p>\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_code\" title=\"添加代码文字\">代码</a>	\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_hide\" title=\"隐藏内容\">隐藏内容</a>				\r\n				</p>\r\n				<p>\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_quote\" title=\"添加引用文字\">引用</a>\r\n					<a id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_free\" title=\"免费信息\">免费信息</a>\r\n				</p>\r\n			</div>\r\n			<div class=\"b2r nbl\">\r\n			<script type=\"text/javascript\">\r\n			    //自定义按扭显示\r\n			    if (typeof (custombbcodes) != 'undefined') {\r\n			        var i = 0;\r\n			        var firstlayer = \"\";\r\n			        var secondlayer = \"\";\r\n			        for (var id in custombbcodes) {\r\n			            if (custombbcodes[id][1] == '') {\r\n			                continue;\r\n			            }\r\n			            if (i % 2 == 0)\r\n			                firstlayer += '<a class=\"cst\" id=\"e_cst' + custombbcodes[id][5] + '_' + custombbcodes[id][0] + '\"><img title=\"' + custombbcodes[id][2] + '\" alt=\"' + custombbcodes[id][2] + '\" src = \"editor/images/' + custombbcodes[id][1] + '\" /></a>';\r\n			            else\r\n			                secondlayer += '<a class=\"cst\" id=\"e_cst' + custombbcodes[id][5] + '_' + custombbcodes[id][0] + '\"><img title=\"' + custombbcodes[id][2] + '\" alt=\"' + custombbcodes[id][2] + '\" src = \"editor/images/' + custombbcodes[id][1] + '\" /></a>';\r\n			            i++;\r\n			            //document.writeln('<a class=\"cst\" id=\"e_cst' + custombbcodes[id][5] + '_' + custombbcodes[id][0] + '\"><img title=\"' + custombbcodes[id][2] + '\" alt=\"' + custombbcodes[id][2] + '\" src = \"editor/images/' + custombbcodes[id][1] + '\" /></a>');\r\n			        }\r\n\r\n			        document.writeln(firstlayer + \"<br id=\\\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_adv_7\\\" />\" + secondlayer);\r\n			    }\r\n			</");
	templateBuilder.Append("script>\r\n			</div>			\r\n		</div>\r\n	</div>\r\n	\r\n	<div id=\"rstnotice\" class=\"ntc_l bbs\" style=\"display:none\">\r\n        <a href=\"javascript:;\" title=\"清除内容\" class=\"d y\" onclick=\"userdataoption(0)\">close</a>\r\n        您有上次未提交成功的数据 <a class=\"xi2\" href=\"javascript:;\" onclick=\"userdataoption(1,'");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("')\"><strong>恢复数据</strong></a>\r\n    </div>\r\n	\r\n	<div class=\"area cl\">\r\n		<textarea name=\"");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_textarea\" class=\"pt\" tabindex=\"1\" rows=\"15\">");
	templateBuilder.Append(message.ToString());
	templateBuilder.Append("</textarea>\r\n	</div>\r\n	");
	templateBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(cssdir.ToString());
	templateBuilder.Append("/editor.css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/post_editor.js\" ></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n	var infloat = ");
	templateBuilder.Append(infloat.ToString());
	templateBuilder.Append(";\r\n	var InFloat_Editor = 'floatlayout_");
	templateBuilder.Append(action.ToString());
	templateBuilder.Append("';\r\n	var editoraction = '");
	templateBuilder.Append(action.ToString());
	templateBuilder.Append("';\r\n	var lang	= new Array();\r\n	lang['post_discuzcode_code'] = '请输入要插入的代码';\r\n	lang['post_discuzcode_quote'] = '请输入要插入的引用';\r\n	lang['post_discuzcode_free'] = '请输入要插入的免费信息';\r\n	lang['post_discuzcode_hide'] = '请输入要插入的隐藏内容';\r\n	lang['board_allowed'] = '系统限制';\r\n	lang['lento'] = '到';\r\n	lang['bytes'] = '字节';\r\n	lang['post_curlength'] = '当前长度';\r\n	lang['post_title_and_message_isnull'] = '请完成标题或内容栏。';\r\n	lang['post_title_toolong'] = '您的标题超过 60 个字符的限制。';\r\n	lang['post_message_length_invalid'] = '您的帖子长度不符合要求。';\r\n	lang['post_type_isnull'] = '请选择主题对应的分类。';\r\n	lang['post_reward_credits_null'] = '对不起，您输入悬赏积分。';\r\n	lang['post_attachment_ext_notallowed']	= '对不起，不支持上传此类扩展名的附件。';\r\n	lang['post_attachment_img_invalid']		= '无效的图片文件。';\r\n	lang['post_attachment_deletelink']		= '删除';\r\n	lang['post_attachment_insert']			= '点击这里将本附件插入帖子内容中当前光标的位置';\r\n	lang['post_attachment_insertlink']		= '插入';\r\n\r\n	lang['enter_list_item']			= \"输入一个列表项目.\\r\\n留空或者点击取消完成此列表.\";\r\n	lang['enter_link_url']			= \"请输入链接的地址:\";\r\n	lang['enter_image_url']			= \"请输入图片链接地址:\";\r\n	lang['enter_email_link']		= \"请输入此链接的邮箱地址:\";\r\n	lang['fontname']				= \"字体\";\r\n	lang['fontsize']				= \"大小\";\r\n	lang['post_advanceeditor']		= \"全部功能\";\r\n	lang['post_simpleeditor']		= \"简单功能\";\r\n	lang['submit']					= \"提交\";\r\n	lang['cancel']					= \"取消\";\r\n	lang['post_autosave_none'] = \"没有可以恢复的数据\";\r\n	lang['post_autosave_confirm'] = \"本操作将覆盖当前帖子内容，确定要恢复数据吗？\";\r\n	lang['enter_tag_option']		= \"请输入 %1 标签的选项:\";\r\n	lang['enter_table_rows']		= \"请输入行数，最多 30 行:\";\r\n	lang['enter_table_columns']		= \"请输入列数，最多 30 列:\";\r\nvar editorid = '");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("';\r\n	var editorcss = 'templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/editor.css';\r\n	var textobj = $(editorid + '_textarea');\r\n	var typerequired = parseInt('0');\r\n	var seccodecheck = parseInt('0');\r\n	var secqaacheck = parseInt('0');\r\n	var special = 1;\r\n	");
	if (special=="")
	{

	templateBuilder.Append("\r\n	special = 0;\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	var isfirstpost = 0;\r\n	");
	if (isfirstpost)
	{

	templateBuilder.Append("\r\n	isfirstpost = 1;\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	var allowposttrade = parseInt('1');\r\n	var allowpostreward = parseInt('1');\r\n	var allowpostactivity = parseInt('1');\r\n\r\n	var bbinsert = parseInt('1');\r\n	\r\n	var allowhtml = parseInt('");
	templateBuilder.Append(htmlon.ToString());
	templateBuilder.Append("');\r\n	var forumallowhtml = parseInt('1');\r\n	var allowsmilies = 1 - parseInt('");
	templateBuilder.Append(smileyoff.ToString());
	templateBuilder.Append("');\r\n	var allowbbcode = parseInt('");
	templateBuilder.Append(usergroupinfo.Allowcusbbcode.ToString().Trim());
	templateBuilder.Append("') == 1 && parseInt('");
	templateBuilder.Append(forum.Allowbbcode.ToString().Trim());
	templateBuilder.Append("') == 1;\r\n	var allowimgcode = parseInt('");
	templateBuilder.Append(forum.Allowimgcode.ToString().Trim());
	templateBuilder.Append("');\r\n\r\n	//var wysiwyg = (is_ie || is_moz || (is_opera && opera.version() >= 9)) && parseInt('");
	templateBuilder.Append(config.Defaulteditormode.ToString().Trim());
	templateBuilder.Append("') && allowbbcode == 1 ? 1 : 0;//bbinsert == 1 ? 1 : 0;\r\n	var wysiwyg = (BROWSER.ie || BROWSER.firefox || (BROWSER.opera >= 9)) && parseInt('");
	templateBuilder.Append(config.Defaulteditormode.ToString().Trim());
	templateBuilder.Append("') && allowbbcode == 1 ? 1 : 0;//bbinsert == 1 ? 1 : 0;\r\n	var allowswitcheditor = parseInt('");
	templateBuilder.Append(config.Allowswitcheditor.ToString().Trim());
	templateBuilder.Append("') && allowbbcode == 1 ;\r\n\r\n	var custombbcodes = { ");
	templateBuilder.Append(Caches.GetCustomEditButtonList().ToString().Trim());
	templateBuilder.Append(" };\r\n\r\n	var smileyinsert = parseInt('1');\r\n	var smiliesCount = 32;//显示表情总数\r\n	var colCount = 8; //每行显示表情个数\r\n	var title = \"\";				   //标题\r\n	var showsmiliestitle = 1;        //是否显示标题（0不显示 1显示）\r\n	var smiliesIsCreate = 0;		   //编辑器是否已被创建(0否，1是）\r\n	\r\n	var maxpolloptions = parseInt('");
	templateBuilder.Append(config.Maxpolloptions.ToString().Trim());
	templateBuilder.Append("');\r\n	function alloweditorhtml() {\r\n		if($('htmlon').checked) {\r\n			allowhtml = 1;\r\n			forumallowhtml = 1;\r\n		} else {\r\n			allowhtml = 0;\r\n			forumallowhtml = 0;\r\n		}\r\n	}\r\n	var simplodemode = parseInt('1');\r\n		editorsimple();\r\n</");
	templateBuilder.Append("script>\r\n<div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_bbar\" class=\"bbar\">\r\n	<em id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_tip\"></em>\r\n	<span id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_svdsecond\"></span>\r\n	<a href=\"javascript:;\" onclick=\"discuzcode('svd',{'titlename':'");
	templateBuilder.Append(config.Antispamposttitle.ToString().Trim());
	templateBuilder.Append("','contentname':'");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("'});return false;\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_svd\">保存数据</a> |\r\n	<a href=\"javascript:;\" onclick=\"discuzcode('rst','");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("');return false;\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_rst\">恢复数据</a> &nbsp;&nbsp;\r\n	<a href=\"javascript:;\" onclick=\"discuzcode('chck');return false;\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_chck\">字数检查</a> |\r\n	<a href=\"javascript:;\" onclick=\"discuzcode('tpr');return false;\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_tpr\">清空内容</a> &nbsp;&nbsp;\r\n	<span id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_resize\"><a href=\"javascript:;\" onclick=\"editorsize('+')\">加大编辑框</a> | <a href=\"javascript:;\" onclick=\"editorsize('-')\">缩小编辑器</a><img src=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("editor/images/resize.gif\" onmousedown=\"editorresize(event)\" /></span>\r\n</div>");

	templateBuilder.Append("\r\n</div>\r\n<div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_menus\" class=\"editorrow\" style=\"overflow: hidden; margin-top: -5px; height: 0; border: none; background: transparent;\">\r\n	");
	templateBuilder.Append("<div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_editortoolbar\" class=\"editortoolbar\">\r\n	<div class=\"p_pop fnm\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_fontname_menu\" style=\"display: none\">\r\n	<ul unselectable=\"on\">\r\n	");	string fontoptions = "仿宋_GB2312,黑体,楷体_GB2312,宋体,新宋体,微软雅黑,TrebuchetMS,Tahoma,Arial,Impact,Verdana,TimesNewRoman";
	

	int fontname__loop__id=0;
	foreach(string fontname in fontoptions.Split(comma))
	{
		fontname__loop__id++;

	templateBuilder.Append("\r\n	    <li onclick=\"discuzcode('fontname', '");
	templateBuilder.Append(fontname.ToString());
	templateBuilder.Append("')\" style=\"font-family: ");
	templateBuilder.Append(fontname.ToString());
	templateBuilder.Append("\" unselectable=\"on\"><a href=\"javascript:;\" title=\"");
	templateBuilder.Append(fontname.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(fontname.ToString());
	templateBuilder.Append("</a></li>\r\n		");
	}	//end loop

	templateBuilder.Append("\r\n	</ul>\r\n	</div>\r\n	");	string sizeoptions = "1,2,3,4,5,6,7";
	
	templateBuilder.Append("\r\n	<div class=\"p_pop fszm\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_fontsize_menu\" style=\"display: none\">\r\n	<ul unselectable=\"on\">\r\n		");
	int size__loop__id=0;
	foreach(string size in sizeoptions.Split(comma))
	{
		size__loop__id++;

	templateBuilder.Append("\r\n			<li onclick=\"discuzcode('fontsize', ");
	templateBuilder.Append(size.ToString());
	templateBuilder.Append(")\" unselectable=\"on\"><a href=\"javascript:;\" title=\"");
	templateBuilder.Append(size.ToString());
	templateBuilder.Append("\"><font size=\"");
	templateBuilder.Append(size.ToString());
	templateBuilder.Append("\" unselectable=\"on\">");
	templateBuilder.Append(size.ToString());
	templateBuilder.Append("</font></a></li>\r\n		");
	}	//end loop

	templateBuilder.Append("\r\n	</ul>\r\n	</div>\r\n</div>\r\n");
	if (config.Smileyinsert==1)
	{

	templateBuilder.Append("\r\n	<div class=\"p_pof upf\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_sml_menu\" style=\"display: none;width:320px;\">\r\n		");
	templateBuilder.Append("<div class=\"smilieslist\">\r\n	");	string defaulttypname = string.Empty;
	
	templateBuilder.Append("\r\n	<div id=\"smiliesdiv\">\r\n		<div class=\"smiliesgroup\" style=\"margin-right: 0pt;\">\r\n			<ul>\r\n			");
	int stype__loop__id=0;
	foreach(DataRow stype in Caches.GetSmilieTypesCache().Rows)
	{
		stype__loop__id++;


	if (stype__loop__id==1)
	{

	 defaulttypname = stype["code"].ToString().Trim();
	

	}	//end if


	if (stype__loop__id==1)
	{

	templateBuilder.Append("\r\n				<li id=\"t_s_" + stype__loop__id.ToString() + "\"><a id=\"s_" + stype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"showsmiles1(" + stype__loop__id.ToString() + ", '" + stype["code"].ToString().Trim() + "');\" class=\"current\">" + stype["code"].ToString().Trim() + "</a></li>\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n				<li id=\"t_s_" + stype__loop__id.ToString() + "\"><a id=\"s_" + stype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"showsmiles1(" + stype__loop__id.ToString() + ", '" + stype["code"].ToString().Trim() + "');\">" + stype["code"].ToString().Trim() + "</a></li>\r\n				");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n			</ul>\r\n		 </div>\r\n		 <div style=\"clear: both;\" id=\"showsmilie\"></div>\r\n		 <table class=\"smilieslist_table\" id=\"s_preview_table\" style=\"display: none\"><tr><td class=\"smilieslist_preview\" id=\"s_preview\"></td></tr></table>\r\n		 <div id=\"showsmilie_pagenum\" class=\"smilieslist_page\">&nbsp;</div>\r\n	</div>\r\n</div>\r\n<script type=\"text/javascript\" reload=\"1\">\r\n	function getSmilies(func){\r\n		if($('showsmilie').innerHTML !='' && $('showsmilie').innerHTML != '正在加载表情...')\r\n			return;\r\n		var c = \"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("tools/ajax.aspx?t=smilies\";\r\n		_sendRequest(c,function(d){var e={};try{e=eval(\"(\"+d+\")\")}catch(f){e={}}var h=e?e:null;func(h);e=null;func=null},false,true);\r\n		setTimeout(\"if($('showsmilie').innerHTML=='')$('showsmilie').innerHTML = '正在加载表情...'\", 2000);\r\n	}\r\n	\r\n	function getSmilies_callback(obj) {\r\n		smilies_HASH = obj; \r\n		showsmiles1(1, '");
	templateBuilder.Append(defaulttypname.ToString());
	templateBuilder.Append("');\r\n	}\r\n	//_attachEvent($('");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_popup_smilies'), 'click', function(){\r\n		getSmilies(getSmilies_callback);\r\n	//});\r\n</");
	templateBuilder.Append("script>");

	templateBuilder.Append("\r\n	</div>\r\n");
	}	//end if

	templateBuilder.Append("\r\n<!-- <script type=\"text/javascript\">smilies_show('smiliesdiv', 8, editorid + '_');</");
	templateBuilder.Append("script> -->");

	templateBuilder.Append("\r\n</div>\r\n<!--====================================================================================================================-->\r\n\r\n\r\n\r\n\r\n<!--====================================================================================================================-->\r\n<div class=\"p_pof uploadfile\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image_menu\" style=\"display: none\" unselectable=\"on\">\r\n	<span class=\"y\"><a href=\"javascript:;\" class=\"flbc\" onclick=\"hideMenu()\">关闭</a></span>\r\n	<ul id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image_ctrl\" class=\"imguptype\" style=\"cursor: move;\">\r\n        <li><a onclick=\"switchImagebutton('www');\" id=\"e_btn_www\" class=\"\" hidefocus=\"true\" href=\"javascript:;\">网络图片</a></li>\r\n        <li><a onclick=\"switchImagebutton('imgattachlist');\" id=\"e_btn_imgattachlist\" hidefocus=\"true\" href=\"javascript:;\" class=\"\">图片列表</a></li>\r\n        <li><a onclick=\"switchImagebutton('multi');\" id=\"e_btn_multi\" hidefocus=\"true\" href=\"javascript:;\" class=\"current\">批量上传</a></li>\r\n    </ul>\r\n    \r\n    <div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_www\" unselectable=\"on\" class=\"p_opt popupfix\" style=\"display:none;\">\r\n        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n			<tr>\r\n				<th width=\"74%\">请输入图片地址<span id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image_status\" class=\"xi1\"></span></th>\r\n				<th width=\"13%\">宽(可选)</th>\r\n				<th width=\"13%\">高(可选)</th>\r\n			</tr>\r\n			<tr>\r\n				<td><input type=\"text\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image_param_1\" onchange=\"loadimgsize(this.value)\" style=\"width: 95%;\" value=\"\" class=\"px\" autocomplete=\"off\" /></td>\r\n				<td><input id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image_param_2\" size=\"1\" value=\"\" class=\"px p_fre\" autocomplete=\"off\" /></td>\r\n				<td><input id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image_param_3\" size=\"1\" value=\"\" class=\"px p_fre\" autocomplete=\"off\" /></td>\r\n			</tr>\r\n			<tr>\r\n				<td colspan=\"3\" class=\"pns mtn\">\r\n					<button type=\"button\" class=\"pn pnc\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image_submit\"><strong>提交</strong></button>\r\n					<button type=\"button\" class=\"pn\" onclick=\"hideMenu();\"><em>取消</em></button>\r\n				</td>\r\n			</tr>\r\n		</table>\r\n	</div>\r\n  \r\n    <div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_multi\" unselectable=\"on\" class=\"p_opt\" style=\"display:none;\">\r\n        <div id=\"e_multiimg\" class=\"fswf\">\r\n            <embed width=\"470\" height=\"268\" type=\"application/x-shockwave-flash\" wmode=\"transparent\" allowscriptaccess=\"always\" menu=\"false\" quality=\"high\" \r\n            src=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("images/common/upload.swf?site=");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("tools/attachupload.aspx%3fmod=swfupload%26type=image%26forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&amp;type=image&amp;random=");
	templateBuilder.Append(DateTime.Now.Ticks.ToString().Trim());
	templateBuilder.Append("\" />\r\n        </div>\r\n        <div class=\"notice uploadinfo\">\r\n      <span id=\"todayfilesizelimit\">当天上传限制: <strong>");
	templateBuilder.Append(FormatBytes(usergroupinfo.Maxsizeperday).ToString().Trim());
	templateBuilder.Append("</strong></span>&nbsp;\r\n			单帖上传限制: <strong>");
	templateBuilder.Append(config.Maxattachments.ToString().Trim());
	templateBuilder.Append("</strong> 个文件&nbsp;&nbsp;单个文件限制: <strong>");
	templateBuilder.Append(FormatBytes(usergroupinfo.Maxattachsize).ToString().Trim());
	templateBuilder.Append("</strong>\r\n			<br />可用扩展名: <strong>");
	templateBuilder.Append(Attachments.GetImageAttachmentTypeString(attachextensionsnosize).ToString().Trim());
	templateBuilder.Append("</strong>&nbsp;\r\n        </div>\r\n    </div>\r\n	\r\n	<div id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_imgattachlist\" unselectable=\"on\" class=\"p_opt\" style=\"display:none;\">\r\n        <div class=\"upfilelist\">\r\n            <div id=\"usedimgattachlist\"></div>\r\n            <div id=\"imgattachlist\">\r\n                <p class=\"notice\">本帖还没有图片附件, <a onclick=\"switchImagebutton('multi');\" href=\"javascript:;\">点击这里</a>上传</p>\r\n            </div>\r\n            <div id=\"unusedimgattachlist\"></div>\r\n            ");
	if (action=="edit")
	{

	templateBuilder.Append("\r\n            <script type=\"text/javascript\">\r\n                var uploadedimagelist = eval('");
	templateBuilder.Append(AttachmentList().ToString());
	templateBuilder.Append("');\r\n                updateimagelistHTML(uploadedimagelist, 3); //加载已使用图片列表\r\n            </");
	templateBuilder.Append("script>\r\n		    ");
	}	//end if

	templateBuilder.Append("\r\n        </div>\r\n        <p style=\"display: none;\" id=\"imgattach_notice\" class=\"noticetip\">\r\n            <span class=\"xi1 xw1\">点击图片添加到帖子内容中</span>\r\n        </p>\r\n    </div>\r\n</div>\r\n<script type=\"text/javascript\">\r\n\r\n//FGBT20141110\r\nfunction updatefilesizeleft(monitorobj, monitoradd, textobj){\r\n    if(!BROWSER.webkit){\r\n        monitorobj.addEventListener(\"DOMAttrModified\", function (e){ \r\n          if (e.attrName == 'style') { if(monitorobj.style.display!='none' && monitoradd.style.display!='none'){\r\n                //textobj.innerHTML = textobj.innerHTML + \" 剩余更新中，请稍后\";\r\n                _sendRequest(\"tools/ajax.aspx?t=todayfilesizelimit\",function(obj){textobj.innerHTML = \"当天上传剩余: <strong>\" + obj + \"</strong>\";},false,null);\r\n               }\r\n          }\r\n        }, false);\r\n    }\r\n    else{\r\n        var MutationObserver = window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver;\r\n        var observer = new MutationObserver(observercallback);\r\n        var observeroptions = {'subtree': true,'attributes':true};\r\n        observer.observe(monitorobj, observeroptions);\r\n        function observercallback(){\r\n          if(monitorobj.style.display!='none' && monitoradd.style.display!='none'){\r\n                    //textobj.innerHTML = textobj.innerHTML + \" 剩余更新中，请稍后\";\r\n                    _sendRequest(\"tools/ajax.aspx?t=todayfilesizelimit\",function(obj){textobj.innerHTML = \"当天上传剩余: <strong>\" + obj + \"</strong>\";},false,null);\r\n                   }\r\n        }\r\n    }\r\n}\r\n\r\nupdatefilesizeleft($(\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_image_menu\"), $(\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_multi\"), $(\"todayfilesizelimit\"));\r\n</");
	templateBuilder.Append("script>\r\n<!--====================================================================================================================-->\r\n\r\n\r\n\r\n\r\n\r\n\r\n<!--====================================================================================================================-->\r\n<input type=\"hidden\" name=\"wysiwyg\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_mode\" value=\"1\" />\r\n<input type=\"hidden\" id=\"testsubmit\" />\r\n");


	if (pagename=="publish.aspx" || (pagename=="edit.aspx"&&isfirstpost))
	{


	if (enabletag)
	{

	templateBuilder.Append("\r\n			<div class=\"pbt cl margint\">\r\n				<p><strong>标签(Tags):</strong>(用空格隔开多个标签，最多可填写 5 个)</p>\r\n				<p><input type=\"text\" name=\"tags\" id=\"tags\" class=\"txt\" value=\"");
	templateBuilder.Append(topictags.ToString());
	templateBuilder.Append("\" tabindex=\"1\" /><button name=\"addtags\" type=\"button\" onclick=\"relatekw();return false\">+可用标签</button> <span id=\"tagselect\"></span></p>\r\n			</div>\r\n			");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n		<div id=\"moreinfo\"></div>\r\n		<div style=\"clear:both;\"></div>\r\n		<div class=\"pbt cl margint\">\r\n			<div class=\"custominfoarea\" id=\"custominfoarea\" style=\"display: none;\"></div>\r\n			");
	if (postinfo.Layer==0 && forum.Applytopictype==1)
	{

	templateBuilder.Append("\r\n			<input type=\"hidden\" id=\"postbytopictype\" name=\"postbytopictype\" value=\"");
	templateBuilder.Append(forum.Postbytopictype.ToString().Trim());
	templateBuilder.Append("\" tabindex=\"3\">\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			<script type=\"text/javascript\">\r\n				function RunMutiUpload() {\r\n				if ($('MultiUploadFile').content != null)\r\n					$('MultiUploadFile').content.MultiFileUpload.GetAttachmentList();	\r\n				}\r\n				/*checkLength($('title'), 255);//检查标题长度*/\r\n				function switchpost(href) {\r\n				    editchange = false;\r\n				    saveData(undefined,'postform','");
	templateBuilder.Append(config.Antispamposttitle.ToString().Trim());
	templateBuilder.Append("', '");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("');\r\n				    location.href = href;\r\n				    doane();\r\n				}\r\n				\r\n                if (getQueryString('cedit') == '' && loadUserdata('forum')){\r\n                    $('rstnotice').style.display = '';\r\n                }\r\n			</");
	templateBuilder.Append("script>\r\n			<button type=\"submit\" id=\"postsubmit\" value=\"true\"");
	if (pagename=="publish.aspx")
	{

	templateBuilder.Append(" name=\"topicsubmit\"");
	}
	else if (pagename=="postreply.aspx")
	{

	templateBuilder.Append(" name=\"replysubmit\"");
	}
	else if (pagename=="edit.aspx")
	{

	templateBuilder.Append(" name=\"editsubmit\"");
	}	//end if

	templateBuilder.Append(" tabindex=\"1\" class=\"pn\"><span>");
	templateBuilder.Append(actiontitle.ToString());
	templateBuilder.Append("</span></button>\r\n      <span id=\"more_2\">\r\n          ");
	if (pagename=="edit.aspx"&&seedinfo.Uid!=userid)
	{


	if ((DateTime.Now-seedinfo.PostDateTime).TotalDays<14)
	{

	templateBuilder.Append("\r\n                  <input name=\"sendmessage\" type=\"checkbox\" id=\"sendmessage\" checked=\"checked\" value=\"1\"/> 发送短消息通知，通知将自动包含标题变动信息，若有其他要说明的信息可填写于右侧理由框内\r\n              ");
	}
	else
	{

	templateBuilder.Append("\r\n                  <input name=\"sendmessage\" type=\"checkbox\" id=\"sendmessage\" value=\"1\"/> 发送短消息通知，通知将自动包含标题变动信息，若有其他要说明的信息可填写于右侧理由框内\r\n              ");
	}	//end if


	}	//end if


	if (userinfo.Spaceid>0 && special==""&&action=="newthread"&&config.Enablespace==1)
	{

	templateBuilder.Append("<input type=\"checkbox\" name=\"addtoblog\" /> 添加到个人空间");
	}	//end if

	templateBuilder.Append("\r\n			</span>\r\n			");
	if (isseccode)
	{

	templateBuilder.Append("<span style=\"position:relative\">");
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

	templateBuilder.Append("</span>");
	}	//end if

	templateBuilder.Append("\r\n		</div>\r\n		</div>\r\n	</div>\r\n	</div>\r\n    <script type=\"text/javascript\">\r\n        var topicreadperm = [\r\n            {readaccess:'',grouptitle:'不限'},\r\n            ");
	int userGroupInfo__loop__id=0;
	foreach(UserGroupInfo userGroupInfo in userGroupInfoList)
	{
		userGroupInfo__loop__id++;


	if (userGroupInfo.Readaccess!=0)
	{

	templateBuilder.Append("\r\n            {readaccess:'");
	templateBuilder.Append(userGroupInfo.Readaccess.ToString().Trim());
	templateBuilder.Append("',grouptitle:'");
	templateBuilder.Append(Utils.RemoveHtml(userGroupInfo.Grouptitle).ToString().Trim());
	templateBuilder.Append("'},\r\n            ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n            {readaccess:'255',grouptitle:'最高权限'}\r\n        ];\r\n    </");
	templateBuilder.Append("script>\r\n	<div class=\"edt_app\">\r\n		");
	if (pagename=="publish.aspx" || (pagename=="edit.aspx"&&isfirstpost))
	{


	if (userid!=-1 && usergroupinfo.Allowsetreadperm==1)
	{

	templateBuilder.Append("\r\n			<p><strong>阅读权限:</strong></p>\r\n			<p class=\"mbn\">\r\n                <em class=\"ftid\">\r\n                    <select name=\"topicreadperm\" id=\"topicreadperm\" class=\"ps\" style=\"width:90px\"></select>\r\n                </em>\r\n                <script type=\"text/javascript\">\r\n                    for (var i = 0 ; i < topicreadperm.length ; i++) {\r\n                        var option = new Option(topicreadperm[i].grouptitle, topicreadperm[i].readaccess);\r\n                        option.title = \"阅读权限:\" + topicreadperm[i].readaccess;\r\n                        $('topicreadperm').options.add(option);\r\n                        if(topicreadperm[i].readaccess == ");
	templateBuilder.Append(topic.Readperm.ToString().Trim());
	templateBuilder.Append(")\r\n                            $('topicreadperm').options.selectedIndex = i;\r\n                    }\r\n                    simulateSelect(\"topicreadperm\");\r\n                </");
	templateBuilder.Append("script>\r\n                <img src=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("images/common/faq.gif\" alt=\"Tip\" class=\"mtn vm\" style=\"margin: 0;\" onmouseover=\"showTip(this)\" tip=\"阅读权限按由高到低排列，高于或等于选中组的用户才可以阅读。\" />\r\n            </p>\r\n		");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n		<h4 style=\"clear:both;\">发帖选项:</h4>\r\n		<p class=\"mbn\">\r\n            <input type=\"checkbox\" value=\"1\" name=\"htmlon\" id=\"htmlon\"  onclick=\"alloweditorhtml()\" ");
	if (usergroupinfo.Allowhtml!=1)
	{

	templateBuilder.Append("disabled");
	}	//end if


	if (htmlon==1)
	{

	templateBuilder.Append("checked");
	}	//end if

	templateBuilder.Append(" /><label for=\"htmlon\">html 代码</label>\r\n            <img src=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("images/common/warning.gif\" alt=\"Tip\" class=\"mtn vm\" style=\"margin: 0;vertical-align: top;\" onmouseover=\"showTip(this)\" tip=\"使用html代码可能会与表情符冲突，如“:D”等表情符。建议在使用html代码时勾选“禁用表情”。\" />\r\n        </p>\r\n		<p class=\"mbn\"><input type=\"checkbox\" id=\"allowimgcode\" disabled");
	if (allowimg==1)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append(" /><label for=\"allowimgcode\">[img] 代码</label></p>\r\n		<p class=\"mbn\"><input type=\"checkbox\" value=\"1\" name=\"parseurloff\" id=\"parseurloff\" ");
	if (parseurloff==1)
	{

	templateBuilder.Append("checked");
	}	//end if

	templateBuilder.Append(" /><label for=\"parseurloff\">禁用 网址自动识别</label></p>\r\n		<p class=\"mbn\"><input type=\"checkbox\" value=\"1\" name=\"smileyoff\" id=\"smileyoff\" ");
	if (smileyoff==1)
	{

	templateBuilder.Append("checked");
	}	//end if


	if (forum.Allowsmilies!=1)
	{

	templateBuilder.Append("disabled");
	}	//end if

	templateBuilder.Append(" /><label for=\"smileyoff\">禁用 表情</label></p>\r\n		<p class=\"mbn\"><input type=\"checkbox\" value=\"1\" name=\"bbcodeoff\" id=\"bbcodeoff\" ");
	if (bbcodeoff==1)
	{

	templateBuilder.Append(" checked");
	}	//end if


	if (usergroupinfo.Allowcusbbcode!=1)
	{

	templateBuilder.Append(" disabled");
	}
	else if (forum.Allowbbcode!=1)
	{

	templateBuilder.Append(" disabled");
	}	//end if

	templateBuilder.Append(" /><label for=\"bbcodeoff\">禁用 论坛代码</label></p>\r\n		<p class=\"mbn\"><input type=\"checkbox\" value=\"1\" name=\"usesig\" id=\"usesig\" ");
	if (usesig==1)
	{

	templateBuilder.Append("checked");
	}	//end if

	templateBuilder.Append(" /><label for=\"usesig\">使用个人签名</label></p>\r\n		");
	if (pagename=="postreply.aspx")
	{

	templateBuilder.Append("\r\n		<p class=\"mbn\"><input type=\"checkbox\" name=\"emailnotify\" id=\"emailnotify\" ");
	if (config.Replyemailstatus==1)
	{

	templateBuilder.Append(" checked");
	}	//end if

	templateBuilder.Append(" /><label for=\"emailnotify\">发送邮件告知楼主</label></p>\r\n		<p class=\"mbn\"><input type=\"checkbox\" name=\"postreplynotice\" id=\"postreplynotice\" ");
	if (config.Replynotificationstatus==1)
	{

	templateBuilder.Append(" checked ");
	}	//end if

	templateBuilder.Append("/><label for=\"emailnotify\">发送论坛通知给楼主</label></p>\r\n		");
	}	//end if


	if (pagename=="publish.aspx" || (pagename=="edit.aspx"&&isfirstpost))
	{


	if (special==""&&Scoresets.GetCreditsTrans()!=0 && usergroupinfo.Maxprice>0)
	{

	templateBuilder.Append("\r\n			<p style=\"clear:both;\"><strong>售价</strong>(");
	templateBuilder.Append(userextcreditsinfo.Name.ToString().Trim());
	templateBuilder.Append("):</p>\r\n			<p><input type=\"text\" name=\"topicprice\" value=\"");
	templateBuilder.Append(topic.Price.ToString().Trim());
	templateBuilder.Append("\" class=\"txt\"  size=\"6\"/> ");
	templateBuilder.Append(userextcreditsinfo.Unit.ToString().Trim());
	templateBuilder.Append(" <br/>最高 ");
	templateBuilder.Append(usergroupinfo.Maxprice.ToString().Trim());
	templateBuilder.Append(" ");
	templateBuilder.Append(userextcreditsinfo.Unit.ToString().Trim());
	templateBuilder.Append("售价只允许非负整数, 单个主题最大收入 ");
	templateBuilder.Append(Scoresets.GetMaxIncPerTopic().ToString().Trim());
	templateBuilder.Append(userextcreditsinfo.Unit.ToString().Trim());
	templateBuilder.Append("</p>\r\n		");
	}	//end if


	}	//end if


	if (pagename=="edit.aspx"&&seedinfo.Uid!=userid)
	{

	templateBuilder.Append("\r\n        编辑理由：<label>最多500个字符，还可输入<strong><span id=\"chLeft\">500</span></strong></label><br/>\r\n        <textarea class=\"txtarea\" name=\"editreason\" id=\"editreason\" style=\"width:150px;height:300px;\"></textarea>\r\n        <script type=\"text/javascript\">checkLength($('editreason'), 500); //检查内容长度</");
	templateBuilder.Append("script>\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n	</div>\r\n</div>");


	templateBuilder.Append("       <div class=\"p_opt\" unselectable=\"on\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_local\" style=\"display: none;\">\r\n				<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">\r\n					<tbody id=\"imgattachbodyhidden\" style=\"display:none\"><tr>\r\n						<td class=\"atnu\"><span id=\"imglocalno[]\"><img src=\"images/attachicons/common_new.gif\" /></span></td>\r\n						<td class=\"atna\">\r\n							<span id=\"imgdeschidden[]\" style=\"display:none\">\r\n								<span id=\"imglocalfile[]\"></span>\r\n							</span>\r\n							<input type=\"hidden\" name=\"imglocalid[]\" />\r\n						</td>\r\n						<td class=\"attc delete_msg\"><span id=\"imgcpdel[]\"></span></td>\r\n					</tr></tbody>\r\n				</table>\r\n				<div class=\"p_tbl\"><table cellpadding=\"0\" cellspacing=\"0\" summary=\"post_attachbody\" border=\"0\" width=\"100%\"><tbody id=\"imgattachbody\"></tbody></table></div>\r\n				<div class=\"upbk\">\r\n					<div id=\"imgattachbtnhidden\" style=\"display:none\"><span><form name=\"imgattachform\" id=\"imgattachform\" method=\"post\" autocomplete=\"off\" action=\"tools/attachupload.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\" target=\"attachframe\" enctype=\"multipart/form-data\"><input type=\"file\" name=\"Filedata\" size=\"45\" class=\"filedata\" /></form></span></div>\r\n					<div id=\"imgattachbtn\"></div>\r\n					<p id=\"imguploadbtn\">						\r\n						<button class=\"pn\" type=\"button\" onclick=\"hideMenu();\"><span>取消</span></button>\r\n						<button class=\"pn pnc\" type=\"button\" onclick=\"uploadAttach(0, 0, 'img')\"><span>上传</span></button>\r\n					</p>\r\n					<p id=\"imguploading\" style=\"display: none;\">上传中</p>\r\n				</div>\r\n				<div class=\"notice upnf\">\r\n					<span id=\"todayfilesizelimitatt\">当天上传限制: <strong>");
	templateBuilder.Append(FormatBytes(usergroupinfo.Maxsizeperday).ToString().Trim());
	templateBuilder.Append("</strong></span>&nbsp;\r\n					单帖上传限制: <strong>");
	templateBuilder.Append(config.Maxattachments.ToString().Trim());
	templateBuilder.Append("</strong> 个文件&nbsp;&nbsp;单个文件限制: <strong>");
	templateBuilder.Append(FormatBytes(usergroupinfo.Maxattachsize).ToString().Trim());
	templateBuilder.Append("</strong>\r\n					<br />可用扩展名: <strong>");
	templateBuilder.Append(attachextensionsnosize.ToString());
	templateBuilder.Append("</strong>&nbsp;\r\n				</div>\r\n			</div>\r\n      <script type=\"text/javascript\">\r\n        updatefilesizeleft($(\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_local\"), $(\"imgattachbtnhidden\"), $(\"todayfilesizelimitatt\"));\r\n      </");
	templateBuilder.Append("script>		\r\n			\r\n			\r\n<!--====================================================================================================================-->\r\n	<iframe name=\"attachframe\" id=\"attachframe\" style=\"display: none;\" onload=\"uploadNextAttach();\"></iframe>\r\n<!--====================================================================================================================-->\r\n\r\n\r\n\r\n<!--====================================================================================================================-->\r\n<script type=\"text/javascript\"  reload=\"1\">\r\n     //获取silverlight插件已经上传的附件列表  //sl上传完返回\r\n    function getAttachmentList(sender, args) {\r\n        var attachment = args.AttchmentList;\r\n        if (isUndefined(attachment) || attachment == '[]') {\r\n            if (infloat == 1) {\r\n                pagescrolls('swfreturn'); return false;\r\n            }\r\n            else { swfuploadwin(); return; }\r\n        }\r\n        var attachmentList = eval(\"(\" + attachment + \")\");\r\n        switchAttachbutton('attachlist');\r\n        updateAttachList();\r\n       \r\n    }\r\n\r\n    function onLoad(plugin, userContext, sender) {\r\n        //只读属性,标识 Silverlight 插件是否已经加载。\r\n        //if (sender.getHost().IsLoaded) {\r\n        $(\"MultiUploadFile\").content.JavaScriptObject.UploadAttchmentList = getAttachmentList;\r\n        // }\r\n    }\r\n\r\n</");
	templateBuilder.Append("script>\r\n<!--====================================================================================================================-->\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n<!--====================================================================================================================-->\r\n<div class=\"p_pof upf\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_attach_menu\" style=\"display: none;width:600px;\" unselectable=\"on\">\r\n		<span class=\"y\"><a href=\"javascript:;\" class=\"flbc\" onclick=\"hideMenu()\">关闭</a></span>\r\n		<ul class=\"imguptype\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_attach_ctrl\">\r\n			<li><a href=\"javascript:;\" hidefocus=\"true\" class=\"current\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_btn_attachlist\" onclick=\"switchAttachbutton('attachlist');\">附件列表</a></li>\r\n			<li><a href=\"javascript:;\" hidefocus=\"true\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_btn_upload\" onclick=\"switchAttachbutton('upload');\">普通上传</a></li>\r\n			<li><a href=\"javascript:;\" hidefocus=\"true\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_btn_swfupload\" onclick=\"switchAttachbutton('swfupload');\">批量上传</a></li>\r\n		</ul>\r\n			<div class=\"p_opt\" unselectable=\"on\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_upload\" style=\"display: none;\">\r\n				<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">\r\n					<tbody id=\"attachbodyhidden\" style=\"display:none\"><tr>\r\n						<td class=\"atnu\"><span id=\"localno[]\"><img src=\"images/attachicons/common_new.gif\" /></span></td>\r\n						<td class=\"atna\">\r\n							<span id=\"deschidden[]\" style=\"display:none\">\r\n								<span id=\"localfile[]\"></span>\r\n							</span>\r\n							<input type=\"hidden\" name=\"localid\" />\r\n						</td>\r\n						<td class=\"attc delete_msg\"><span id=\"cpdel[]\"></span></td>\r\n					</tr></tbody>\r\n				</table>\r\n				<div class=\"p_tbl\"><table cellpadding=\"0\" cellspacing=\"0\" summary=\"post_attachbody\" border=\"0\" width=\"100%\"><tbody id=\"attachbody\"></tbody></table></div>\r\n				<div class=\"upbk\">\r\n					<div id=\"attachbtnhidden\" style=\"display:none\"><span><form name=\"attachform\" id=\"attachform\" method=\"post\" autocomplete=\"off\" action=\"tools/attachupload.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\" target=\"attachframe\" enctype=\"multipart/form-data\"><input type=\"hidden\" name=\"uid\" value=\"$_G['uid']\"><input type=\"hidden\" name=\"hash\" value=\"{echo md5(substr(md5($_G['config']['security']['authkey']), 8).$_G['uid'])}\"><input type=\"file\" name=\"Filedata\" size=\"45\" class=\"fldt\" /></form></span></div>\r\n					<div id=\"attachbtn\"></div>\r\n					<p id=\"uploadbtn\">\r\n						<button type=\"button\" class=\"pn\" onclick=\"hideMenu();\"><span>取消</span></button>\r\n						<button type=\"button\" class=\"pn pnc\" onclick=\"uploadAttach(0, 0)\"><span>上传</span></button>\r\n					</p>\r\n					<p id=\"uploading\" style=\"display: none;\"><img src=\"images/common/uploading.gif\" style=\"vertical-align: middle;\" /> 上传中，请稍候，您可以<a href=\"javascript:;\" onclick=\"hideMenu()\">暂时关闭这个小窗口</a>，上传完成后您会收到通知。</p>\r\n				</div>\r\n				<div class=\"notice upnf\">\r\n					<span id=\"todayfilesizelimitf1\">当天上传限制: <strong>");
	templateBuilder.Append(FormatBytes(usergroupinfo.Maxsizeperday).ToString().Trim());
	templateBuilder.Append("</strong></span>&nbsp;\r\n					单帖上传限制: <strong>");
	templateBuilder.Append(config.Maxattachments.ToString().Trim());
	templateBuilder.Append("</strong> 个文件&nbsp;&nbsp;单个文件限制: <strong>");
	templateBuilder.Append(FormatBytes(usergroupinfo.Maxattachsize).ToString().Trim());
	templateBuilder.Append("</strong>\r\n					<br />可用扩展名: <strong>");
	templateBuilder.Append(attachextensionsnosize.ToString());
	templateBuilder.Append("</strong>&nbsp;\r\n				</div>				\r\n			</div>\r\n      <script type=\"text/javascript\">\r\n          updatefilesizeleft($(\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_attach_menu\"), $(\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_upload\"), $(\"todayfilesizelimitf1\"));\r\n      </");
	templateBuilder.Append("script>	\r\n			<div class=\"p_opt\" unselectable=\"on\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_swfupload\" style=\"display: none;\">\r\n				<div class=\"floatboxswf\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_multiattach\">\r\n			    ");
	if (config.Swfupload==1)
	{

	templateBuilder.Append("\r\n			    <embed width=\"470\" height=\"268\" type=\"application/x-shockwave-flash\" wmode=\"transparent\" allowscriptaccess=\"always\" menu=\"false\" quality=\"high\" src=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("images/common/upload.swf?site=");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("tools/attachupload.aspx%3fmod=swfupload%26forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&amp;random=");
	templateBuilder.Append(DateTime.Now.Ticks.ToString().Trim());
	templateBuilder.Append("\">\r\n			    ");
	}
	else
	{


						string authToken=Discuz.Common.DES.Encode(oluserinfo.Olid.ToString() + "," + oluserinfo.Username.ToString(), oluserinfo.Password.Substring(0, 10)).Replace("+", "[");
						

	if (pagename.IndexOf("goods")<0 && config.Silverlight==1)
	{

	templateBuilder.Append("\r\n					<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("silverlight/uploadfile/silverlight.js\" reload=\"1\"></");
	templateBuilder.Append("script> \r\n					<div id=\"swfbox\"> \r\n					<object  id=\"MultiUploadFile\" data=\"data:application/x-silverlight-2,\" type=\"application/x-silverlight-2\" Width=\"100%\" Height=\"340\">\r\n					<param name=\"source\" value=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("silverlight/UploadFile/ClientBin/MultiFileUpload.xap\"/>\r\n					<param name=\"onError\" value=\"onSilverlightError\" />\r\n					<param name=\"onLoad\" value=\"onLoad\" />\r\n					<param name=\"background\" value=\"aliceblue\" />\r\n					<param name=\"minRuntimeVersion\" value=\"4.0.50401.0\" />\r\n					<param name=\"autoUpgrade\" value=\"true\" />\r\n					<param name=\"initParams\" value=\"forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append(",authToken=");
	templateBuilder.Append(authToken.ToString());
	templateBuilder.Append(",max=");
	templateBuilder.Append(config.Maxattachments.ToString().Trim());
	templateBuilder.Append("\" />		  \r\n					<a href=\"http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50401.0\" style=\"text-decoration:none\" target=\"_blank\">\r\n					<img src=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("silverlight/uploadfile/uploadfile.jpg\" alt=\"安装微软Silverlight控件,即刻使用批量上传附件\" style=\"border-style:none\"/>\r\n					</a>\r\n					</object></div>\r\n					");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n				</div>\r\n				<div class=\"notice upnf\">\r\n					<span id=\"todayfilesizelimitf2\">当天上传限制: <strong>");
	templateBuilder.Append(FormatBytes(usergroupinfo.Maxsizeperday).ToString().Trim());
	templateBuilder.Append("</strong></span>&nbsp;\r\n					单帖上传限制: <strong>");
	templateBuilder.Append(config.Maxattachments.ToString().Trim());
	templateBuilder.Append("</strong> 个文件&nbsp;&nbsp;单个文件限制: <strong>");
	templateBuilder.Append(FormatBytes(usergroupinfo.Maxattachsize).ToString().Trim());
	templateBuilder.Append("</strong>\r\n					<br />可用扩展名: <strong>");
	templateBuilder.Append(attachextensionsnosize.ToString());
	templateBuilder.Append("</strong>&nbsp;\r\n				</div>\r\n        <script type=\"text/javascript\">\r\n            updatefilesizeleft($(\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_attach_menu\"), $(\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_swfupload\"), $(\"todayfilesizelimitf2\"));\r\n        </");
	templateBuilder.Append("script>	\r\n			</div>\r\n		<div class=\"p_opt post_tablelist\" unselectable=\"on\" id=\"");
	templateBuilder.Append(editorid.ToString());
	templateBuilder.Append("_attachlist\">\r\n				<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" id=\"attachlist_edittablist\">\r\n					<tbody>\r\n					    <tr>\r\n						<td class=\"atnu\">&nbsp;</td>\r\n						<td class=\"atna\">文件名&nbsp;(<a class=\"xg2\" href=\"javascript:;\" onclick=\"insertAllAttachTag();return false;\" style=\"margin:0 4px;\">插入全部附件</a>)</td>\r\n						<td class=\"atds\">描述</td>\r\n						");
	if (userid!=-1 && usergroupinfo.Allowsetattachperm==1)
	{

	templateBuilder.Append("<td class=\"attv\">阅读权限</td>");
	}	//end if


	if (topicattachscorefield>0 && usergroupinfo.Maxprice>0)
	{

	templateBuilder.Append("<td class=\"attp\">");
	templateBuilder.Append(Scoresets.GetTopicAttachCreditsTransName().ToString().Trim());
	templateBuilder.Append("</td>");
	}	//end if

	templateBuilder.Append("\r\n						<td class=\"attc delete_msg\"></td>\r\n					   </tr>\r\n					</tbody>\r\n					");
	if (action=="edit")
	{


	int attachment__loop__id=0;
	foreach(DataRow attachment in attachmentlist.Rows)
	{
		attachment__loop__id++;


	if (Utils.StrToInt(attachment["pid"].ToString().Trim(), 0)==postinfo.Pid && attachment["isimage"].ToString().Trim()=="0")
	{

	string filetypeimage = "";
	
	int isimage = 0;
	
	string inserttype = "";
	

	if (attachment["filetype"].ToString().Trim().IndexOf("image")>-1)
	{

	 filetypeimage = "image.gif";
	
	 inserttype = "insertAttachimgTag";
	
	 isimage = 1;
	

	}
	else
	{

	 inserttype = "insertAttachTag";
	

	if (Utils.GetFileExtName(attachment["attachment"].ToString().Trim())=="rar" || Utils.GetFileExtName(attachment["attachment"].ToString().Trim())=="zip")
	{

	 filetypeimage = "rar.gif";
	

	}
	else
	{

	 filetypeimage = "attachment.gif";
	

	}	//end if


	}	//end if

	templateBuilder.Append("		\r\n					        <tbody id=\"attach_" + attachment["aid"].ToString().Trim() + "\">\r\n					        <tr>\r\n					        <td class=\"atnu\">\r\n					        <img id=\"attach" + attachment["aid"].ToString().Trim() + "_type\" border=\"0\" src=\"images/attachicons/");
	templateBuilder.Append(filetypeimage.ToString());
	templateBuilder.Append("\" class=\"vm\" alt=\"\"/>\r\n					        </td>\r\n					        <td class=\"atna\">\r\n					        <span id=\"attach" + attachment["aid"].ToString().Trim() + "\">\r\n					        <a id=\"attachname" + attachment["aid"].ToString().Trim() + "\" onclick=\"");
	templateBuilder.Append(inserttype.ToString());
	templateBuilder.Append("(" + attachment["aid"].ToString().Trim() + ")\" href=\"javascript:;\" isimage=\"");
	templateBuilder.Append(isimage.ToString());
	templateBuilder.Append("\" title=\"" + attachment["attachment"].ToString().Trim() + "\">");	templateBuilder.Append(Utils.GetUnicodeSubString(attachment["attachment"].ToString().Trim(),25,"..."));
	templateBuilder.Append("</a> \r\n 					        <a href=\"javascript:;\" class=\"atturl\" title=\"添加附件地址\" onclick=\"insertText('attach://')\">\r\n					        <img alt=\"\" src=\"images/attachicons/attachurl.gif\"/>\r\n					        </a>\r\n					        </span>\r\n					        <span id=\"attachupdate" + attachment["aid"].ToString().Trim() + "\" style=\"display:none;\">\r\n					        <form enctype=\"multipart/form-data\" target=\"attachframe\" action=\"tools/attachupload.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&aid=" + attachment["aid"].ToString().Trim() + "\" method=\"post\" id=\"attachform_" + attachment["aid"].ToString().Trim() + "\" name=\"attachform_" + attachment["aid"].ToString().Trim() + "\" style=\"float:left;\">\r\n					            <input type=\"file\" name=\"Filedata\" size=\"8\" />\r\n					            <input type=\"hidden\" value=\"" + attachment["aid"].ToString().Trim() + "\" name=\"attachupdatedid\" />\r\n					            <input type=\"submit\" value=\"上传\" />\r\n					        </form>\r\n					        </span>\r\n					        <a id=\"attach" + attachment["aid"].ToString().Trim() + "_opt\" href=\"javascript:;\" class=\"right\" onclick=\"attachupdate('" + attachment["aid"].ToString().Trim() + "', this)\">更新</a>\r\n					        <input type=\"hidden\" value=\"" + attachment["aid"].ToString().Trim() + "\" name=\"attachid\" />\r\n                            ");
	if (isimage==1)
	{

	string attachkey = Thumbnail.GetKey(TypeConverter.StrToInt(attachment["aid"].ToString().Trim()));
	
	templateBuilder.Append("\r\n					            <img src=\"tools/ajax.aspx?t=image&aid=" + attachment["aid"].ToString().Trim() + "&size=300x300&key=");
	templateBuilder.Append(attachkey.ToString());
	templateBuilder.Append("&nocache=yes&type=fixnone\" id=\"image_" + attachment["aid"].ToString().Trim() + "\" cwidth=\"" + attachment["width"].ToString().Trim() + "\" style=\"position: absolute; top: -10000px;\"/>\r\n                            ");
	}	//end if

	templateBuilder.Append("\r\n                            <script type=\"text/javascript\">ATTACHNUM['attachused']++;</");
	templateBuilder.Append("script>\r\n					        </td>\r\n					        <td class=\"atds\"><input type=\"text\" name=\"attachdesc_" + attachment["aid"].ToString().Trim() + "\" size=\"18\" class=\"txt\" value=\"" + attachment["description"].ToString().Trim() + "\"/></td>\r\n					        <td class=\"attv\">\r\n                                <select id=\"readperm_" + attachment["aid"].ToString().Trim() + "\" onchange=\"$('readperm_hidden_" + attachment["aid"].ToString().Trim() + "').value = this.value;\" size=\"1\">\r\n                                    <option value=\"\">不限</option>\r\n                                </select>\r\n                                <script type=\"text/javascript\">getreadpermoption($('readperm_" + attachment["aid"].ToString().Trim() + "'), " + attachment["readperm"].ToString().Trim() + ");</");
	templateBuilder.Append("script>\r\n                                <input type=\"hidden\" id=\"readperm_hidden_" + attachment["aid"].ToString().Trim() + "\" value=\"" + attachment["readperm"].ToString().Trim() + "\" name=\"readperm_" + attachment["aid"].ToString().Trim() + "\"/>\r\n                            </td>\r\n					        <td class=\"attp\"><input type=\"text\" size=\"1\" value=\"" + attachment["attachprice"].ToString().Trim() + "\" name=\"attachprice_" + attachment["aid"].ToString().Trim() + "\"/></td>\r\n					        <td class=\"attp\"><input type=\"text\" size=\"1\" value=\"" + attachment["attachprice"].ToString().Trim() + "\" name=\"attachprice_" + attachment["aid"].ToString().Trim() + "\"/></td>\r\n					        <td class=\"attc delete_msg\"><a href=\"javascript:;\" class=\"d\" onclick=\"delAttach('" + attachment["aid"].ToString().Trim() + ",");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("',0)\">删除</a></td>\r\n					        </tr></tbody>\r\n						   ");
	}	//end if


	}	//end loop


	}	//end if

	templateBuilder.Append("\r\n\r\n				</table>\r\n				<div id=\"attachlist_tablist_current\"></div>\r\n				<div id=\"attachlist_tablist\"></div>\r\n				<p class=\"ptm\" id=\"attach_notice\" style=\"display: none\" >点击文件名插入到帖子内容中</p>\r\n\r\n				");
	if (infloat==0)
	{

	templateBuilder.Append("\r\n				<div id=\"uploadlist\" class=\"upfilelist\" style=\"height:auto\">\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n				<div id=\"uploadlist\" class=\"upfilelist\">\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">\r\n					");
	if (pagename.IndexOf("goods")<0 && config.Silverlight==1)
	{

	templateBuilder.Append("\r\n					<tbody id=\"attachuploadedhidden\" style=\"display:none\"><tr>\r\n						<td class=\"attachnum\"><span id=\"sl_localno[]\"><img src=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("images/attachicons/common_new.gif\" /></span></td>\r\n						<td class=\"attachctrl\"><span id=\"sl_cpadd[]\"></span></td>\r\n						<td class=\"attachname\">\r\n							<span id=\"sl_deschidden[]\" style=\"display:none\">\r\n								<a href=\"javascript:;\" onclick='parentNode.innerHTML=\"<input type=\\\"text\\\" name=\\\"attachdesc\\\" size=\\\"25\\\" class=\\\"txt\\\" />\"'>描述</a>\r\n								<span id=\"attachfile[]\"></span>\r\n								<input type=\"text\" name=\"sl_attachdesc\" style=\"display:none\" />\r\n							</span>\r\n						</td>\r\n						");
	if (userid!=-1 && usergroupinfo.Allowsetattachperm==1)
	{

	templateBuilder.Append("<td class=\"attachview\"><input type=\"text\" name=\"sl_readperm\" value=\"0\" size=\"1\" class=\"txt\" /></td>");
	}	//end if


	if (topicattachscorefield>0 && usergroupinfo.Maxprice>0)
	{

	templateBuilder.Append("<td class=\"attachpr\"><input type=\"text\" name=\"sl_attachprice\" value=\"0\" size=\"1\" class=\"txt\" /></td>");
	}	//end if


	if (config.Enablealbum==1 && caninsertalbum)
	{

	templateBuilder.Append("\r\n							<td  style=\"vertical-align:top;\">\r\n								<select name=\"sl_albums\" style=\"display:none\">\r\n								<option value=\"0\"></option>\r\n								");
	int album__loop__id=0;
	foreach(DataRow album in albumlist.Rows)
	{
		album__loop__id++;

	templateBuilder.Append("\r\n								<option value=\"" + album["albumid"].ToString().Trim() + "\">" + album["title"].ToString().Trim() + "</option>\r\n								");
	}	//end loop

	templateBuilder.Append("\r\n								</select>\r\n							</td>\r\n						");
	}	//end if

	templateBuilder.Append("\r\n						<td class=\"attachdel\"><span id=\"sl_cpdel[]\"></span></td>\r\n					</tr></tbody>\r\n\r\n					");
	}	//end if

	templateBuilder.Append("\r\n					<tbody id=\"attachbodyhidden\" style=\"display:none\"><tr>\r\n						<td class=\"attachnum\"><span id=\"localno[]\"><img src=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("images/attachicons/common_new.gif\" /></span></td>\r\n						<td class=\"attachctrl\"><span id=\"cpadd[]\"></span></td>\r\n						<td class=\"attachname\">\r\n							<span id=\"deschidden[]\" style=\"display:none\">\r\n								<a href=\"javascript:;\" onclick='parentNode.innerHTML=\"<input type=\\\"text\\\" name=\\\"attachdesc\\\" size=\\\"25\\\" class=\\\"txt\\\" />\"'>描述</a>\r\n								<span id=\"localfile[]\"></span>\r\n							</span>\r\n							<input type=\"hidden\" name=\"localid\" />\r\n						</td>\r\n\r\n						");
	if (config.Enablealbum==1 && caninsertalbum)
	{

	templateBuilder.Append("\r\n							<td  style=\"vertical-align:top;\">\r\n							");
	if (albumlist.Rows.Count!=0)
	{

	templateBuilder.Append("\r\n								<select name=\"albums\"  style=\"display:none\">\r\n								<option value=\"0\"></option>\r\n								");
	int album__loop__id=0;
	foreach(DataRow album in albumlist.Rows)
	{
		album__loop__id++;

	templateBuilder.Append("\r\n								<option value=\"" + album["albumid"].ToString().Trim() + "\">" + album["title"].ToString().Trim() + "</option>\r\n								");
	}	//end loop

	templateBuilder.Append("\r\n								</select>\r\n							");
	}	//end if

	templateBuilder.Append("\r\n							</td>\r\n						");
	}	//end if

	templateBuilder.Append("\r\n						<td class=\"attachdel\"><span id=\"cpdel[]\"></span></td>\r\n					</tr></tbody>\r\n				</table>\r\n				");
	if (pagename.IndexOf("goods")<0 && config.Silverlight==1)
	{

	templateBuilder.Append("\r\n				<div id=\"swfattachlist\">\r\n					<table cellspacing=\"0\" cellpadding=\"0\" id=\"attachuploadednote\" style=\"display:none;\">\r\n						<tbody>\r\n							<tr>\r\n								<td class=\"attachnum\"></td>\r\n								<td>您有 <span id=\"attachuploadednotenum\"></span> 个已经上传的附件<span id=\"maxattachnote\" style=\"display: none;\">, 只能使用前<span id=\"num2upload2\"><strong>");
	templateBuilder.Append(config.Maxattachments.ToString().Trim());
	templateBuilder.Append("</strong></span>个</span>  \r\n								<a onclick=\"addAttachUploaded(attaches);\" href=\"javascript:;\">使用</a>   <a onclick=\"attachlist()\" href=\"javascript:;\">忽略</a>\r\n								</td>\r\n							</tr>\r\n						</tbody>\r\n					</table>\r\n				</div>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				<table cellpadding=\"0\" cellspacing=\"0\" summary=\"post_attachbody\" border=\"0\" width=\"100%\"><tbody id=\"attachuploaded\"></tbody><tbody id=\"attachbody\"></tbody></table>\r\n			</div>\r\n		</div>\r\n<div id=\"img_hidden\" alt=\"1\" style=\"position:absolute;top:-100000px;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='image');width:");
	templateBuilder.Append(thumbwidth.ToString());
	templateBuilder.Append("px;height:");
	templateBuilder.Append(thumbheight.ToString());
	templateBuilder.Append("px\"></div>		</div>\r\n	</div>\r\n\r\n<!--====================================================================================================================-->\r\n	\r\n	\r\n	\r\n	\r\n	\r\n	\r\n	\r\n	\r\n<!--====================================================================================================================-->\r\n	\r\n<script type=\"text/javascript\">\r\n	var editorform = $('testform');\r\n	var editorsubmit = $('testsubmit');\r\n	if (wysiwyg) {\r\n	    newEditor(1, bbcode2html(textobj.value));\r\n	} else {\r\n	    newEditor(0, textobj.value);\r\n	}\r\n	if (getQueryString('cedit') == 'yes') {\r\n	    loadData(true, '");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("');\r\n	}\r\n</");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\nfunction switchImagebutton(btn) {\r\n        var btns = ['www', 'imgattachlist'];\r\n        btns.push('multi'); switchButton(btn, btns);\r\n        $(editorid + '_image_menu').style.height = '';\r\n    }\r\n\r\n\r\n/*function switchImagebutton(btn) {\r\nvar btns = ['www', 'albumlist'];\r\nswitchButton(btn, btns);\r\n$(editorid + '_image_menu').style.height = '';\r\n}*/\r\n\r\nfunction switchAttachbutton(btn) {\r\nvar btns = ['attachlist'];\r\nbtns.push('upload');btns.push('swfupload');switchButton(btn, btns);\r\n}\r\n\r\n");
	if (action=="edit")
	{

	templateBuilder.Append("\r\n    ATTACHNUM['imageused'] = uploadedimagelist?uploadedimagelist.length:ATTACHNUM['imageused'];//更新已使用图片的数量\r\n");
	}	//end if

	templateBuilder.Append("\r\nupdateattachnum('attach');\r\nupdateattachnum('image');\r\n\r\n");
	if (userid!=-1)
	{

	templateBuilder.Append("\r\nupdateAttachList();\r\nupdateImageList();\r\n");
	}	//end if

	templateBuilder.Append("\r\n\r\nif(ATTACHNUM['attachused'] + ATTACHNUM['attachunused']<=0)\r\n    switchAttachbutton('swfupload');\r\nelse\r\n    switchAttachbutton('attachlist');\r\nsetCaretAtEnd();\r\n\r\nif(!$('usedimgattachlist').childNodes.length && !$('unusedimgattachlist').childNodes.length)\r\n    switchImagebutton('multi');\r\nif(BROWSER.ie >= 5 || BROWSER.firefox >= '2') {\r\n    _attachEvent(window, 'beforeunload',function on(){ saveData(undefined, 'postform','");
	templateBuilder.Append(config.Antispamposttitle.ToString().Trim());
	templateBuilder.Append("', '");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("')});\r\n}\r\n");
	if (userid!=-1)
	{

	templateBuilder.Append("\r\ngetunusedattachlist_dialog();\r\n");
	}	//end if

	templateBuilder.Append("\r\naddAttach();\r\n</");
	templateBuilder.Append("script>");


	if (adveditor)
	{

	templateBuilder.Append("\r\n	<div id=\"specialposttable\"  style=\"display: none;\">	\r\n		<div class=\"PrivateBTexfm cl\">\r\n			");
	templateBuilder.Append("<table class=\"PublishTable\" summary=\"post\" cellspacing=\"0\" cellpadding=\"0\" id=\"newpost\">\r\n			");
	if (publishnote!="")
	{

	templateBuilder.Append("<tbody><tr class=\"PublishBar\"><th></th><td></td></tr><tr class=\"PublishWarning\"><td colspan = \"2\" class=\"PublishWarning\">");
	templateBuilder.Append(publishnote.ToString());
	templateBuilder.Append("</td></tr></tbody>");
	}	//end if

	templateBuilder.Append("\r\n			<tbody>\r\n        <tr class=\"PublishBar\"><th></th><td></td></tr>\r\n				<tr class=\"PublishWarning\"><td colspan=\"2\" class=\"PublishWarning\"><span class=\"Warning\">请勿发布重复种子");
	if (pagename=="edit.aspx")
	{

	templateBuilder.Append("<b>&nbsp;&nbsp;&nbsp;如果不需要更换种子，“种子文件”一栏请留空</b>");
	}	//end if

	templateBuilder.Append("<br/>\r\n          种子信息中不要出现任何全角字符，主观描述，以及其他用于引起别人注意的字符，包括但不限于【】，〖〗，『』，“非常好看”，“强烈推荐” 等<br/>\r\n          发种者有义务尽可能长时间连续做种，发种后不做种者会被删除种子并扣除一定的上传流量作为惩罚，发种前请熟悉发种教程。</span>\r\n        </td></tr><tr class=\"PublishBar\"><th></th><td> \r\n			");
	if (publishtype=="movie")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"movie_file\" type=\"file\" id=\"movie_file\" size=\"1\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n          <input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('movie_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('movie_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB&nbsp;&nbsp;&nbsp;<input class=\"file_btn\" id=\"autofillbtn\" type=\"button\" onclick=\"fgbtshowwindow('seedautofillw','tools/ajax.aspx?t=seedautofill&reason=movie');\" value=\"自动标签\" style=\"z-index:999;display:none;\" />\r\n					\r\n					</td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"movie_cname\" type=\"text\" id=\"movie_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\" value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">英文名</span></th><td>\r\n					<input name=\"movie_ename\" type=\"text\" id=\"movie_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;请填写附带有发布组名的完整名称&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">IMDb编号</span></th><td>\r\n					<input name=\"movie_imdb\" type=\"text\" id=\"movie_imdb\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;不是评分！形如tt012345格式");	 infoselectionstring = "movie_imdb";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">电影类别</span></th><td>\r\n					<input name=\"movie_type\" type=\"text\" id=\"movie_type\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">分辨率</span></th><td>\r\n					<input name=\"movie_resolution\" type=\"text\" id=\"movie_resolution\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_resolution";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>视频格式</th><td>\r\n					<input name=\"movie_video\" type=\"text\" id=\"movie_video\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_video";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>音频格式</th><td>\r\n					<input name=\"movie_audio\" type=\"text\" id=\"movie_audio\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_audio";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>导演</th><td>\r\n					<input name=\"movie_director\" type=\"text\" id=\"movie_director\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info8.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_director";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>演员</th><td>\r\n					<input name=\"movie_actor\" type=\"text\" id=\"movie_actor\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info9.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_actor";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">发行年份</span></th><td>\r\n					<input name=\"movie_year\" type=\"text\" id=\"movie_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info10.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;只填写数字年份，以IMDb数据为准，下同，有多个数据时，以\"/\"分隔");	 infoselectionstring = "movie_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">制片国家/地区</span></th><td>\r\n					<input name=\"movie_region\" type=\"text\" id=\"movie_region\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info11.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_region";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"movie_language\" type=\"text\" id=\"movie_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info12.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">字幕</span></th><td>\r\n					<input name=\"movie_subtitle\" type=\"text\" id=\"movie_subtitle\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info13.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_subtitle";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">分级</span></th><td>\r\n					<input name=\"movie_rank\" type=\"text\" id=\"movie_rank\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info14.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "movie_rank";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="tv")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"tv_file\" type=\"file\" id=\"tv_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n					<input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('tv_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('tv_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n					\r\n					</td></tr><tr><th><span class=\"b\">国家/地区</span></th><td>\r\n					<input name=\"tv_region\" type=\"text\" id=\"tv_region\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "tv_region";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"tv_cname\" type=\"text\" id=\"tv_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "tv_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">英文名</span></th><td>\r\n					<input name=\"tv_ename\" type=\"text\" id=\"tv_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "tv_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>季度信息</th><td>\r\n					<input name=\"tv_season\" type=\"text\" id=\"tv_season\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;填写格式S01E01，S01，S01~S04，S01E02~05&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "tv_season";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"tv_language\" type=\"text\" id=\"tv_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "tv_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">分辨率</span></th><td>\r\n					<input name=\"tv_resolution\" type=\"text\" id=\"tv_resolution\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "tv_resolution";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">字幕</span></th><td>\r\n					<input name=\"tv_subtitle\" type=\"text\" id=\"tv_subtitle\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "tv_subtitle";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="comic")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"comic_file\" type=\"file\" id=\"comic_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />					\r\n					<input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('comic_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('comic_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n					\r\n					</td></tr><tr><th><span class=\"b\">国家/地区</span></th><td>\r\n					<input name=\"comic_region\" type=\"text\" id=\"comic_region\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_region";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"comic_cname\" type=\"text\" id=\"comic_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">英文名</span></th><td>\r\n					<input name=\"comic_ename\" type=\"text\" id=\"comic_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>季度信息</th><td>\r\n					<input name=\"comic_season\" type=\"text\" id=\"comic_season\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_season";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">类别</span></th><td>\r\n					<input name=\"comic_type\" type=\"text\" id=\"comic_type\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"comic_language\" type=\"text\" id=\"comic_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">视频格式</span></th><td>\r\n					<input name=\"comic_format\" type=\"text\" id=\"comic_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">片源</span></th><td>\r\n					<input name=\"comic_source\" type=\"text\" id=\"comic_source\" size=\"10\" onkeyup=\"filltitle()\" title=\"zzz\"  value=\"");
	templateBuilder.Append(seedinfo.Info8.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_source";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">字幕</span></th><td>\r\n					<input name=\"comic_subtitle\" type=\"text\" id=\"comic_subtitle\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info9.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_subtitle";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">字幕组</span></th><td>\r\n					<input name=\"comic_subtitlegroup\" type=\"text\" id=\"comic_subtitlegroup\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info10.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_subtitlegroup";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">发行时间</span></th><td>\r\n					<input name=\"comic_year\" type=\"text\" id=\"comic_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info11.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "comic_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="music")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"music_file\" type=\"file\" id=\"music_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n					<input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('music_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('music_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n					\r\n					</td></tr><tr><th><span class=\"b\">国家/地区</span></th><td>\r\n					<input name=\"music_region\" type=\"text\" id=\"music_region\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_region";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">类别</span></th><td>\r\n					<input name=\"music_type\" type=\"text\" id=\"music_type\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">艺术家</span></th><td>\r\n					<input name=\"music_artist\" type=\"text\" id=\"music_artist\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_artist";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">专辑名</span></th><td>\r\n					<input name=\"music_name\" type=\"text\" id=\"music_name\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_name";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">发行时间</span></th><td>\r\n					<input name=\"music_year\" type=\"text\" id=\"music_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"music_language\" type=\"text\" id=\"music_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"music_format\" type=\"text\" id=\"music_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">码率</span></th><td>\r\n					<input name=\"music_bps\" type=\"text\" id=\"music_bps\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info8.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_bps";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>发行</th><td>\r\n					<input name=\"music_company\" type=\"text\" id=\"music_company\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info9.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "music_company";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="game")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"game_file\" type=\"file\" id=\"game_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n					<input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('game_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('game_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n					\r\n					</td></tr><tr><th><span class=\"b\">平台</span></th><td>\r\n					<input name=\"game_platform\" type=\"text\" id=\"game_platform\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "game_platform";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"game_cname\" type=\"text\" id=\"game_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "game_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">英文名</span></th><td>\r\n					<input name=\"game_ename\" type=\"text\" id=\"game_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "game_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">类别</span></th><td>\r\n					<input name=\"game_type\" type=\"text\" id=\"game_type\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "game_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"game_language\" type=\"text\" id=\"game_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "game_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"game_format\" type=\"text\" id=\"game_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "game_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>制作公司</th><td>\r\n					<input name=\"game_company\" type=\"text\" id=\"game_company\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "game_company";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>国家/地区</th><td>\r\n					<input name=\"game_region\" type=\"text\" id=\"game_region\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info8.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "game_region";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="discovery")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"discovery_file\" type=\"file\" id=\"discovery_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n					<input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('discovery_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('discovery_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n					\r\n					</td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"discovery_cname\" type=\"text\" id=\"discovery_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "discovery_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">英文名</span></th><td>\r\n					<input name=\"discovery_ename\" type=\"text\" id=\"discovery_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "discovery_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">类别</span></th><td>\r\n					<input name=\"discovery_type\" type=\"text\" id=\"discovery_type\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "discovery_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"discovery_language\" type=\"text\" id=\"discovery_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "discovery_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"discovery_format\" type=\"text\" id=\"discovery_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "discovery_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">分辨率</span></th><td>\r\n					<input name=\"discovery_resolution\" type=\"text\" id=\"discovery_resolution\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "discovery_resolution";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">字幕</span></th><td>\r\n					<input name=\"discovery_subtitle\" type=\"text\" id=\"discovery_subtitle\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "discovery_subtitle";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="sport")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"sport_file\" type=\"file\" id=\"sport_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n					<input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('sport_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('sport_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB，电子竞技相关请发“其他”类\r\n					\r\n					</td></tr><tr><th><span class=\"b\">时间</span></th><td>\r\n					<input name=\"sport_year\" type=\"text\" id=\"sport_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "sport_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"sport_cname\" type=\"text\" id=\"sport_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "sport_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th>英文名</th><td>\r\n					<input name=\"sport_ename\" type=\"text\" id=\"sport_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "sport_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">类别</span></th><td>\r\n					<input name=\"sport_type\" type=\"text\" id=\"sport_type\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "sport_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言/解说人</span></th><td>\r\n					<input name=\"sport_language\" type=\"text\" id=\"sport_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "sport_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"sport_format\" type=\"text\" id=\"sport_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "sport_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">分辨率</span></th><td>\r\n					<input name=\"sport_resolution\" type=\"text\" id=\"sport_resolution\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "sport_resolution";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">字幕</span></th><td>\r\n					<input name=\"sport_subtitle\" type=\"text\" id=\"sport_subtitle\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info8.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "sport_subtitle";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="entertainment")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"entertainment_file\" type=\"file\" id=\"entertainment_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n					<input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('entertainment_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('entertainment_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n					\r\n					</td></tr><tr><th><span class=\"b\">国家/地区</span></th><td>\r\n					<input name=\"entertainment_region\" type=\"text\" id=\"entertainment_region\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_region";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"entertainment_cname\" type=\"text\" id=\"entertainment_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th>英文名</th><td>\r\n					<input name=\"entertainment_ename\" type=\"text\" id=\"entertainment_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">发行时间</span></th><td>\r\n					<input name=\"entertainment_year\" type=\"text\" id=\"entertainment_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append(" 形如20100101格式\r\n					</td></tr><tr><th><span class=\"b\">简介</span></th><td>\r\n					<input name=\"entertainment_brief\" type=\"text\" id=\"entertainment_brief\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_brief";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"entertainment_language\" type=\"text\" id=\"entertainment_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"entertainment_format\" type=\"text\" id=\"entertainment_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">分辨率</span></th><td>\r\n					<input name=\"entertainment_resolution\" type=\"text\" id=\"entertainment_resolution\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info8.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_resolution";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">字幕</span></th><td>\r\n					<input name=\"entertainment_subtitle\" type=\"text\" id=\"entertainment_subtitle\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info9.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "entertainment_subtitle";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="software")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"software_file\" type=\"file\" id=\"software_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n          <input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('software_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('software_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n          \r\n          </td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"software_cname\" type=\"text\" id=\"software_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;包括版本号&nbsp;&nbsp;");	 infoselectionstring = "software_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th>英文名</th><td>\r\n					<input name=\"software_ename\" type=\"text\" id=\"software_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "software_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"software_language\" type=\"text\" id=\"software_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "software_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">软件类型</span></th><td>\r\n					<input name=\"software_type\" type=\"text\" id=\"software_type\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "software_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"software_format\" type=\"text\" id=\"software_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "software_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>发行时间</th><td>\r\n					<input name=\"software_year\" type=\"text\" id=\"software_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "software_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="staff")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"staff_file\" type=\"file\" id=\"staff_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n          <input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('staff_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('staff_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n          \r\n          </td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"staff_cname\" type=\"text\" id=\"staff_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "staff_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th>英文名</th><td>\r\n					<input name=\"staff_ename\" type=\"text\" id=\"staff_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "staff_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">类别</span></th><td>\r\n					<input name=\"staff_type\" type=\"text\" id=\"staff_type\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "staff_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"staff_language\" type=\"text\" id=\"staff_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "staff_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"staff_format\" type=\"text\" id=\"staff_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "staff_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>发行时间</th><td>\r\n					<input name=\"staff_year\" type=\"text\" id=\"staff_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "staff_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="video")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"video_file\" type=\"file\" id=\"video_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n					<input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('video_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('video_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n					\r\n					</td></tr><tr><th>时间</th><td>\r\n					<input name=\"video_year\" type=\"text\" id=\"video_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">类别</span></th><td>\r\n					<input name=\"video_type\" type=\"text\" id=\"video_type\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info9.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_type";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">国家/地区</span></th><td>\r\n					<input name=\"video_region\" type=\"text\" id=\"video_region\" size=\"30\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_region";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"video_cname\" type=\"text\" id=\"video_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th>英文名</th><td>\r\n					<input name=\"video_ename\" type=\"text\" id=\"video_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">语言</span></th><td>\r\n					<input name=\"video_language\" type=\"text\" id=\"video_language\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_language";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"video_format\" type=\"text\" id=\"video_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info6.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">分辨率</span></th><td>\r\n					<input name=\"video_resolution\" type=\"text\" id=\"video_resolution\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info7.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_resolution";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">字幕</span></th><td>\r\n					<input name=\"video_subtitle\" type=\"text\" id=\"video_subtitle\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info8.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "video_subtitle";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else if (publishtype=="other")
	{

	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">种子文件</span></th><td>\r\n					<input class=\"hideinputfile\" name=\"other_file\" type=\"file\" id=\"other_file\" size=\"60\" onchange=\"checkfiletype(this)\" onblur=\"checkfiletypeempty(this)\" title=\"\" />\r\n          <input class=\"file_show\" name=\"file_show\" type=\"text\" id=\"file_show\" size=\"60\" onclick=\"$('other_file').click();\"   />\r\n          <input class=\"file_btn\" type=\"button\" onclick=\"$('other_file').click();\" value=\"选择种子\" style=\"z-index:999;\" />&nbsp;&nbsp;&nbsp;种子文件文件最大为4MB\r\n          \r\n          </td></tr><tr><th><span class=\"b\">中文名</span></th><td>\r\n					<input name=\"other_cname\" type=\"text\" id=\"other_cname\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info1.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "other_cname";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n          </td></tr><tr><th>英文名</th><td>\r\n					<input name=\"other_ename\" type=\"text\" id=\"other_ename\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info2.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "other_ename";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>简介</th><td>\r\n					<input name=\"other_brief\" type=\"text\" id=\"other_brief\" size=\"60\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info3.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "other_brief";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th><span class=\"b\">文件格式</span></th><td>\r\n					<input name=\"other_format\" type=\"text\" id=\"other_format\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info4.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "other_format";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n					</td></tr><tr><th>发行时间</th><td>\r\n					<input name=\"other_year\" type=\"text\" id=\"other_year\" size=\"10\" onkeyup=\"filltitle()\" title=\"\"  value=\"");
	templateBuilder.Append(seedinfo.Info5.ToString().Trim());
	templateBuilder.Append("\" />&nbsp;&nbsp;&nbsp;");	 infoselectionstring = "other_year";
	templateBuilder.Append(PrivateBT.InfoSelectionList(infoselectionstring).ToString().Trim());
	templateBuilder.Append("\r\n			");
	}
	else
	{


	}	//end if

	templateBuilder.Append("\r\n			\r\n</td></tr></tbody></table>\r\n<script type=\"text/javascript\">\r\n\r\nvar inputvar = null;\r\ncheckfileempty();\r\nfunction checkfileempty()\r\n{\r\n    if(inputvar != null)\r\n    {\r\n        if(!inputvar.value) { $(\"file_show\").value=\"\";} \r\n        else\r\n        { \r\n            var filename = input.value.split(\"\\\\\");\r\n            var len = filename.length;\r\n            $(\"file_show\").value = filename[len-1];\r\n        } \r\n        setTimeout(checkfileempty,2000);\r\n    }\r\n    else $(\"file_show\").value=\"\";\r\n}\r\nfunction checkfiletypeempty(input)\r\n{\r\n    if(!input.value) $(\"file_show\").value=\"\";\r\n}\r\n\r\nfunction checkfiletype(input)\r\n{\r\n  inputvar = input;\r\n  setTimeout(checkfileempty,2000);\r\n  if(!input.value) return;\r\n  if(input.value.length > 8)\r\n  {\r\n    if(input.value.slice(input.value.length - 8) == \".torrent\") \r\n    {\r\n        var filename = input.value.split(\"\\\\\");\r\n        var len = filename.length;\r\n        $(\"file_show\").value = filename[len-1];\r\n        posted = 0;\r\n        if(input.id == \"movie_file\") $(\"autofillbtn\").style.display = \"\";\r\n        return;\r\n\r\n    }\r\n  }\r\n  if(input.id == \"movie_file\") $(\"autofillbtn\").style.display = \"none\";\r\n  alert(\"只能选择.torrent类型的文件\");\r\n}\r\n\r\nfunction do_seedautofill()\r\n{\r\n   var seedname = escape($(\"movie_file\").value);\r\n   var message = escape($(\"seedautofill_area\").value);\r\n   _sendRequest('/tools/sessionajax.aspx?t=seedautofill', do_seedautofill_callback, true, 'seedname='+ seedname + \"&message=\" + escape(message));\r\n   hideWindow('seedautofillw');\r\n}\r\nfunction do_seedautofill_callback(doc)\r\n{\r\n  var obj =  eval('(' + doc + ')');\r\n  $(\"movie_cname\").value = obj.cname;\r\n  $(\"movie_ename\").value = obj.ename;\r\n  $(\"movie_imdb\").value = obj.imdb;\r\n  $(\"movie_type\").value = obj.type;\r\n  $(\"movie_resolution\").value = obj.resolution;\r\n  $(\"movie_year\").value = obj.year;\r\n  $(\"movie_region\").value = obj.region;\r\n  $(\"movie_language\").value = obj.language;\r\n  $(\"movie_subtitle\").value = obj.subtitle;\r\n  $(\"movie_rank\").value = obj.rank;\r\n  editdoc.body.innerHTML = unescape(obj.message);\r\n}\r\n//检查输入\r\nvar posted = 0;\r\nfunction checkinput(input)\r\n{\r\n    if(posted > 0){alert(\"您已提交，请勿重复提交。如填写信息不全，请修改后再次尝试提交\");return false;}\r\n    else {posted = 1;}\r\n    \r\n      //alert(\"check begin\");\r\n      ");
	if (publishtype=="movie")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"movie_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"movie_file\").value.slice($(\"movie_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"movie_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"movie_ename\").value == \"\") {alert(\"请填写英文名称！\");return false;}\r\n          if ($(\"movie_imdb\").value == \"\") {alert(\"请填写IMDb编号！\");return false;}\r\n          if ($(\"movie_type\").value == \"\") {alert(\"请填写电影类型！\");return false;}\r\n          if ($(\"movie_resolution\").value == \"\") {alert(\"请填写分辨率！\");return false;}\r\n          if ($(\"movie_year\").value == \"\") {alert(\"请填写年份！\");return false;}\r\n          if ($(\"movie_region\").value == \"\") {alert(\"请填写国家地区！\");return false;}\r\n          if ($(\"movie_language\").value == \"\") {alert(\"请填写电影语言！\");return false;}\r\n          if ($(\"movie_subtitle\").value == \"\") {alert(\"请填写字幕情况！\");return false;}\r\n          if ($(\"movie_rank\").value == \"\") {alert(\"请填写电影分级！\");return false;}\r\n  		");
	}
	else if (publishtype=="tv")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"tv_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"tv_file\").value.slice($(\"tv_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"tv_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"tv_ename\").value == \"\") {alert(\"请填写英文名称！\");return false;}\r\n          if ($(\"tv_resolution\").value == \"\") {alert(\"请填写分辨率！\");return false;}\r\n          if ($(\"tv_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n          if ($(\"tv_subtitle\").value == \"\") {alert(\"请填写字幕情况！\");return false;}\r\n          if ($(\"tv_region\").value == \"\") {alert(\"请填写国家地区！\");return false;}\r\n			");
	}
	else if (publishtype=="comic")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"comic_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"comic_file\").value.slice($(\"comic_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"comic_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"comic_ename\").value == \"\") {alert(\"请填写英文名称！\");return false;}\r\n          if ($(\"comic_type\").value == \"\") {alert(\"请填写类别！\");return false;}\r\n          if ($(\"comic_format\").value == \"\") {alert(\"请填写视频格式！\");return false;}\r\n          if ($(\"comic_year\").value == \"\") {alert(\"请填写年份！\");return false;}\r\n          if ($(\"comic_region\").value == \"\") {alert(\"请填写国家地区！\");return false;}\r\n          if ($(\"comic_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n          if ($(\"comic_subtitle\").value == \"\") {alert(\"请填写字幕情况！\");return false;}\r\n          if ($(\"comic_subtitlegroup\").value == \"\") {alert(\"请填写字幕组！\");return false;}\r\n          if ($(\"comic_source\").value == \"\") {alert(\"请填写片源！\");return false;}\r\n			");
	}
	else if (publishtype=="music")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"music_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"music_file\").value.slice($(\"music_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"music_name\").value == \"\") {alert(\"请填写专辑名称！\");return false;}\r\n          if ($(\"music_type\").value == \"\") {alert(\"请填写类别！\");return false;}\r\n          if ($(\"music_artist\").value == \"\") {alert(\"请填写艺术家！\");return false;}\r\n          if ($(\"music_year\").value == \"\") {alert(\"请填写年份！\");return false;}\r\n          if ($(\"music_region\").value == \"\") {alert(\"请填写国家地区！\");return false;}\r\n          if ($(\"music_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n          if ($(\"music_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n          if ($(\"music_bps\").value == \"\") {alert(\"请填写码率！\");return false;}\r\n			");
	}
	else if (publishtype=="game")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"game_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"game_file\").value.slice($(\"game_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"game_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"game_ename\").value == \"\") {alert(\"请填写英文名称！\");return false;}\r\n          if ($(\"game_type\").value == \"\") {alert(\"请填写类别！\");return false;}\r\n          if ($(\"game_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n          if ($(\"game_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n          if ($(\"game_platform\").value == \"\") {alert(\"请填写平台！\");return false;}\r\n			");
	}
	else if (publishtype=="discovery")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"discovery_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"discovery_file\").value.slice($(\"discovery_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"discovery_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"discovery_ename\").value == \"\") {alert(\"请填写英文名称！\");return false;}\r\n          if ($(\"discovery_type\").value == \"\") {alert(\"请填写类别！\");return false;}\r\n          if ($(\"discovery_resolution\").value == \"\") {alert(\"请填写分辨率！\");return false;}\r\n          if ($(\"discovery_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n          if ($(\"discovery_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n          if ($(\"discovery_subtitle\").value == \"\") {alert(\"请填写字幕情况！\");return false;}\r\n			");
	}
	else if (publishtype=="sport")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"sport_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"sport_file\").value.slice($(\"sport_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"sport_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"sport_type\").value == \"\") {alert(\"请填写类别！\");return false;}\r\n          if ($(\"sport_resolution\").value == \"\") {alert(\"请填写分辨率！\");return false;}\r\n          if ($(\"sport_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n          if ($(\"sport_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n          if ($(\"sport_subtitle\").value == \"\") {alert(\"请填写字幕情况！\");return false;}\r\n          if ($(\"sport_year\").value == \"\") {alert(\"请填写时间！\");return false;}\r\n			");
	}
	else if (publishtype=="entertainment")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"entertainment_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"entertainment_file\").value.slice($(\"entertainment_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"entertainment_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"entertainment_resolution\").value == \"\") {alert(\"请填写分辨率！\");return false;}\r\n          if ($(\"entertainment_year\").value == \"\") {alert(\"请填写年份！\");return false;}\r\n          if ($(\"entertainment_region\").value == \"\") {alert(\"请填写国家地区！\");return false;}\r\n          if ($(\"entertainment_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n          if ($(\"entertainment_subtitle\").value == \"\") {alert(\"请填写字幕情况！\");return false;}\r\n          if ($(\"entertainment_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n			");
	}
	else if (publishtype=="software")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"software_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"software_file\").value.slice($(\"software_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"software_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"software_type\").value == \"\") {alert(\"请填写类别！\");return false;}\r\n          if ($(\"software_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n          if ($(\"software_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n			");
	}
	else if (publishtype=="staff")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"staff_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"staff_file\").value.slice($(\"staff_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"staff_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"staff_type\").value == \"\") {alert(\"请填写类别！\");return false;}\r\n          if ($(\"staff_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n          if ($(\"staff_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n\r\n			");
	}
	else if (publishtype=="video")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"video_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"video_file\").value.slice($(\"video_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"video_type\").value == \"\") {alert(\"请填写类别！\");return false;}\r\n          if ($(\"video_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"video_resolution\").value == \"\") {alert(\"请填写分辨率！\");return false;}\r\n          if ($(\"video_region\").value == \"\") {alert(\"请填写国家地区！\");return false;}\r\n          if ($(\"video_language\").value == \"\") {alert(\"请填写语言！\");return false;}\r\n          if ($(\"video_subtitle\").value == \"\") {alert(\"请填写字幕情况！\");return false;}\r\n          if ($(\"video_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n			");
	}
	else if (publishtype=="other")
	{


	if (pagename=="publish.aspx")
	{

	templateBuilder.Append("\r\n              if ($(\"other_file\").value == \"\") {alert(\"请选择种子！\");return false;}\r\n              if ($(\"other_file\").value.slice($(\"other_file\").value.length - 8) != \".torrent\") {alert(\"种子文件只能选择.torrent类型的文件！\");return false;}\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          if ($(\"other_cname\").value == \"\") {alert(\"请填写中文名称！\");return false;}\r\n          if ($(\"other_format\").value == \"\") {alert(\"请填写文件格式！\");return false;}\r\n			");
	}	//end if

	templateBuilder.Append("\r\n  \r\n  //alert(\"check finished\");\r\n  return validate(input);\r\n}\r\n//生成标题\r\nfunction filltitle()\r\n{\r\n//alert(\"test\");\r\n  posted = 0;\r\n  var titlestring = \"\";\r\n      ");
	if (publishtype=="movie")
	{

	templateBuilder.Append("\r\n          if ($(\"movie_year\").value != \"\") titlestring += \"[\" + $(\"movie_year\").value + \"]\";\r\n          if ($(\"movie_region\").value != \"\") titlestring += \"[\" + $(\"movie_region\").value + \"]\";\r\n          if ($(\"movie_cname\").value != \"\") titlestring += \"[\" + $(\"movie_cname\").value + \"]\";\r\n          if ($(\"movie_ename\").value != \"\") titlestring += \"[\" + $(\"movie_ename\").value + \"]\";\r\n          if ($(\"movie_type\").value != \"\") titlestring += \"[\" + $(\"movie_type\").value + \"]\";\r\n          if ($(\"movie_subtitle\").value != \"\") titlestring += \"[\" + $(\"movie_subtitle\").value + \"]\";\r\n          if ($(\"movie_resolution\").value != \"\") titlestring += \"[\" + $(\"movie_resolution\").value + \"]\";\r\n          if ($(\"movie_rank\").value != \"\") titlestring += \"[\" + $(\"movie_rank\").value + \"]\";\r\n  		");
	}
	else if (publishtype=="tv")
	{

	templateBuilder.Append("\r\n  		    if ($(\"tv_region\").value != \"\") titlestring += \"[\" + $(\"tv_region\").value + \"]\";\r\n          if ($(\"tv_cname\").value != \"\") titlestring += \"[\" + $(\"tv_cname\").value + \"]\";\r\n          if ($(\"tv_ename\").value != \"\") titlestring += \"[\" + $(\"tv_ename\").value + \"]\";\r\n          if ($(\"tv_season\").value != \"\") titlestring += \"[\" + $(\"tv_season\").value + \"]\";\r\n          if ($(\"tv_subtitle\").value != \"\") titlestring += \"[\" + $(\"tv_subtitle\").value + \"]\";\r\n          if ($(\"tv_resolution\").value != \"\") titlestring += \"[\" + $(\"tv_resolution\").value + \"]\";\r\n			");
	}
	else if (publishtype=="comic")
	{

	templateBuilder.Append("\r\n			    if ($(\"comic_region\").value != \"\") titlestring += \"[\" + $(\"comic_region\").value + \"]\";\r\n			    if ($(\"comic_type\").value != \"\") titlestring += \"[\" + $(\"comic_type\").value + \"]\";\r\n          if ($(\"comic_cname\").value != \"\") titlestring += \"[\" + $(\"comic_cname\").value + \"]\";\r\n          if ($(\"comic_ename\").value != \"\") titlestring += \"[\" + $(\"comic_ename\").value + \"]\";\r\n          if ($(\"comic_season\").value != \"\") titlestring += \"[\" + $(\"comic_season\").value + \"]\";       \r\n          if ($(\"comic_subtitle\").value != \"\") titlestring += \"[\" + $(\"comic_subtitle\").value + \"]\";\r\n          if ($(\"comic_subtitlegroup\").value != \"\") titlestring += \"[\" + $(\"comic_subtitlegroup\").value + \"]\";\r\n          if ($(\"comic_source\").value != \"\") titlestring += \"[\" + $(\"comic_source\").value + \"]\";\r\n          if ($(\"comic_format\").value != \"\") titlestring += \"[\" + $(\"comic_format\").value + \"]\";  \r\n			");
	}
	else if (publishtype=="music")
	{

	templateBuilder.Append("\r\n					if ($(\"music_region\").value != \"\") titlestring += \"[\" + $(\"music_region\").value + \"]\";\r\n          if ($(\"music_type\").value != \"\") titlestring += \"[\" + $(\"music_type\").value + \"]\";\r\n          if ($(\"music_year\").value != \"\") titlestring += \"[\" + $(\"music_year\").value + \"]\";\r\n          if ($(\"music_artist\").value != \"\") titlestring += \"[\" + $(\"music_artist\").value + \"]\";\r\n          if ($(\"music_name\").value != \"\") titlestring += \"[\" + $(\"music_name\").value + \"]\";       \r\n          if ($(\"music_format\").value != \"\") titlestring += \"[\" + $(\"music_format\").value + \"]\";\r\n          if ($(\"music_bps\").value != \"\") titlestring += \"[\" + $(\"music_bps\").value + \"]\";\r\n          if ($(\"music_company\").value != \"\") titlestring += \"[\" + $(\"music_company\").value + \"]\";\r\n			");
	}
	else if (publishtype=="game")
	{

	templateBuilder.Append("\r\n					if ($(\"game_platform\").value != \"\") titlestring += \"[\" + $(\"game_platform\").value + \"]\";\r\n          if ($(\"game_cname\").value != \"\") titlestring += \"[\" + $(\"game_cname\").value + \"]\";\r\n          if ($(\"game_ename\").value != \"\") titlestring += \"[\" + $(\"game_ename\").value + \"]\";\r\n          if ($(\"game_type\").value != \"\") titlestring += \"[\" + $(\"game_type\").value + \"]\";\r\n          if ($(\"game_language\").value != \"\") titlestring += \"[\" + $(\"game_language\").value + \"]\";       \r\n          if ($(\"game_format\").value != \"\") titlestring += \"[\" + $(\"game_format\").value + \"]\";\r\n			");
	}
	else if (publishtype=="discovery")
	{

	templateBuilder.Append("\r\n					if ($(\"discovery_type\").value != \"\") titlestring += \"[\" + $(\"discovery_type\").value + \"]\";\r\n          if ($(\"discovery_cname\").value != \"\") titlestring += \"[\" + $(\"discovery_cname\").value + \"]\";\r\n          if ($(\"discovery_ename\").value != \"\") titlestring += \"[\" + $(\"discovery_ename\").value + \"]\";\r\n          if ($(\"discovery_subtitle\").value != \"\") titlestring += \"[\" + $(\"discovery_subtitle\").value + \"]\";       \r\n          if ($(\"discovery_resolution\").value != \"\") titlestring += \"[\" + $(\"discovery_resolution\").value + \"]\";\r\n          if ($(\"discovery_format\").value != \"\") titlestring += \"[\" + $(\"discovery_format\").value + \"]\";\r\n			");
	}
	else if (publishtype=="sport")
	{

	templateBuilder.Append("\r\n					if ($(\"sport_type\").value != \"\") titlestring += \"[\" + $(\"sport_type\").value + \"]\";\r\n          if ($(\"sport_year\").value != \"\") titlestring += \"[\" + $(\"sport_year\").value + \"]\";\r\n          if ($(\"sport_cname\").value != \"\") titlestring += \"[\" + $(\"sport_cname\").value + \"]\";\r\n          if ($(\"sport_ename\").value != \"\") titlestring += \"[\" + $(\"sport_ename\").value + \"]\";\r\n          if ($(\"sport_language\").value != \"\") titlestring += \"[\" + $(\"sport_language\").value + \"]\";\r\n          if ($(\"sport_resolution\").value != \"\") titlestring += \"[\" + $(\"sport_resolution\").value + \"]\";       \r\n          if ($(\"sport_format\").value != \"\") titlestring += \"[\" + $(\"sport_format\").value + \"]\";\r\n			");
	}
	else if (publishtype=="entertainment")
	{

	templateBuilder.Append("\r\n				  if ($(\"entertainment_region\").value != \"\") titlestring += \"[\" + $(\"entertainment_region\").value + \"]\";\r\n          if ($(\"entertainment_cname\").value != \"\") titlestring += \"[\" + $(\"entertainment_cname\").value + \"]\";\r\n          if ($(\"entertainment_ename\").value != \"\") titlestring += \"[\" + $(\"entertainment_ename\").value + \"]\";\r\n          if ($(\"entertainment_brief\").value != \"\") titlestring += \"[\" + $(\"entertainment_brief\").value + \"]\";\r\n          if ($(\"entertainment_subtitle\").value != \"\") titlestring += \"[\" + $(\"entertainment_subtitle\").value + \"]\";\r\n			    if ($(\"entertainment_resolution\").value != \"\") titlestring += \"[\" + $(\"entertainment_resolution\").value + \"]\";       \r\n          if ($(\"entertainment_format\").value != \"\") titlestring += \"[\" + $(\"entertainment_format\").value + \"]\";\r\n			");
	}
	else if (publishtype=="software")
	{

	templateBuilder.Append("\r\n					if ($(\"software_type\").value != \"\") titlestring += \"[\" + $(\"software_type\").value + \"]\";\r\n          if ($(\"software_cname\").value != \"\") titlestring += \"[\" + $(\"software_cname\").value + \"]\";\r\n          if ($(\"software_ename\").value != \"\") titlestring += \"[\" + $(\"software_ename\").value + \"]\";\r\n          if ($(\"software_language\").value != \"\") titlestring += \"[\" + $(\"software_language\").value + \"]\";\r\n          if ($(\"software_format\").value != \"\") titlestring += \"[\" + $(\"software_format\").value + \"]\";       \r\n			");
	}
	else if (publishtype=="staff")
	{

	templateBuilder.Append("\r\n					if ($(\"staff_type\").value != \"\") titlestring += \"[\" + $(\"staff_type\").value + \"]\";\r\n          if ($(\"staff_cname\").value != \"\") titlestring += \"[\" + $(\"staff_cname\").value + \"]\";\r\n          if ($(\"staff_ename\").value != \"\") titlestring += \"[\" + $(\"staff_ename\").value + \"]\";\r\n          if ($(\"staff_language\").value != \"\") titlestring += \"[\" + $(\"staff_language\").value + \"]\";\r\n          if ($(\"staff_format\").value != \"\") titlestring += \"[\" + $(\"staff_format\").value + \"]\";\r\n			");
	}
	else if (publishtype=="video")
	{

	templateBuilder.Append("\r\n					if ($(\"video_region\").value != \"\") titlestring += \"[\" + $(\"video_region\").value + \"]\";\r\n          if ($(\"video_cname\").value != \"\") titlestring += \"[\" + $(\"video_cname\").value + \"]\";\r\n          if ($(\"video_ename\").value != \"\") titlestring += \"[\" + $(\"video_ename\").value + \"]\";\r\n          if ($(\"video_subtitle\").value != \"\") titlestring += \"[\" + $(\"video_subtitle\").value + \"]\";\r\n          if ($(\"video_resolution\").value != \"\") titlestring += \"[\" + $(\"video_resolution\").value + \"]\";\r\n          if ($(\"video_format\").value != \"\") titlestring += \"[\" + $(\"video_format\").value + \"]\";       \r\n			");
	}
	else if (publishtype=="other")
	{

	templateBuilder.Append("\r\n					if ($(\"other_cname\").value != \"\") titlestring += \"[\" + $(\"other_cname\").value + \"]\";\r\n					if ($(\"other_ename\").value != \"\") titlestring += \"[\" + $(\"other_ename\").value + \"]\";\r\n					if ($(\"other_brief\").value != \"\") titlestring += \"[\" + $(\"other_brief\").value + \"]\";\r\n          if ($(\"other_format\").value != \"\") titlestring += \"[\" + $(\"other_format\").value + \"]\";\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			if(titlestring.replace(/(^\\s*)|(\\s*$)/g, \"\") == \"\")\r\n			{\r\n          $(\"title\").value = \"种子标题将由分类信息自动生成，请直接填写分类信息表，粗体项目为必填内容\";\r\n          $(\"seedpostHideTitle\") = \"\";\r\n			}\r\n			else \r\n			{\r\n          $(\"title\").value = \"标题预览：\" + titlestring;\r\n      }\r\n			if(titlestring.length > 255) alert(\"标题长度过长，请酌情删减分类信息!\");\r\n}\r\n//便捷填空\r\nfunction addField(selection,category) \r\n{\r\n	var target=$(category);\r\n			if(target.value==''){target.value = selection;}\r\n			else target.value+='/'+selection;\r\n	filltitle();\r\n}\r\nfunction replaceField(selection,category) \r\n{\r\n	var target=$(category);\r\n  target.value = selection;\r\n  filltitle();\r\n}\r\nfunction autofill_showinput()\r\n{\r\n  \r\n}\r\n</");
	templateBuilder.Append("script>");

	templateBuilder.Append("\r\n		</div>\r\n  </div>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n</form>\r\n</div>\r\n	");
	}	//end if


	}
	else
	{


	if (ispost)
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


	if (infloat==1)
	{

	templateBuilder.Append("\r\n			 <p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n			 ");
	}
	else
	{


	                    string backLink = HttpContext.Current.Request.RawUrl.ToString();
	                    SetBackLink(backLink.Contains("&cedit=yes") ? backLink : backLink + "&cedit=yes");
	                

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


	}	//end if

	templateBuilder.Append("\r\n<script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_calendar.js\"></");
	templateBuilder.Append("script>\r\n");

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
