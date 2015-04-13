<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.usercp" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>
<%@ Import namespace="Discuz.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2015/1/7 16:52:26.
		本页面代码由Discuz!NT模板引擎生成于 2015/1/7 16:52:26. 
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



	templateBuilder.Append("\r\n<script type=\"text/javascript\">\r\n    var templatepath = \"");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("\";\r\n    var imagedir = \"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("\";\r\nfunction getvalidpic(data)\r\n{\r\nvar pic='';\r\nif(parseInt(data)==1)\r\n{\r\n    pic = '<img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/data_valid.gif\"/>';\r\n}\r\nif(parseInt(data)==0)\r\n{\r\n    pic = '<img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/data_invalid.gif\"/>';\r\n}\r\ndocument.write(pic);\r\n}\r\nfunction tickthreadtype(data)\r\n{\r\nvar str='';\r\n  switch (parseInt(data)){\r\n   case 0:\r\nstr='不允许';\r\n   case 1:\r\nstr='允许本版内置顶';\r\n   case 2:\r\nstr='允许分类内置顶';\r\n    case 3:\r\nstr='执行任意置顶';\r\n}\r\ndocument.write(str);\r\n}\r\n\r\nfunction searchtype(data)\r\n{\r\nswitch (parseInt(data)){\r\n   case 0:\r\nstr='不允许';\r\n   case 1:\r\nstr='允许搜索标题或全文';\r\n   case 2:\r\nstr='仅允许搜索标题';\r\n\r\n}\r\ndocument.write(str);\r\n}\r\n</");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_showtopic.js\"></");
	templateBuilder.Append("script>\r\n<div class=\"wrap cl pageinfo\">\r\n	<div id=\"nav\">\r\n		");
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
	templateBuilder.Append("\" class=\"title\">");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("</a> &raquo; <a href=\"usercpprofile.aspx\">用户中心</a> &raquo; <strong>用户组&权限</strong>\r\n	</div>\r\n</div>\r\n<div class=\"wrap uc cl\">\r\n	");

	if (userid>0)
	{

	templateBuilder.Append("\r\n<div class=\"uc_app\">\r\n	<a href=\"usercp.aspx\"><h2>用户中心</h2></a>\r\n	<ul>\r\n	");
	if (pagename=="usercpprofile.aspx?action=avatar")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercpprofile.aspx?action=avatar\">设置头像</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercpprofile.aspx?action=avatar\">设置头像</a></li>\r\n	");
	}	//end if


	if (pagename=="usercptopic.aspx"||pagename=="usercppost.aspx"||pagename=="usercpdigest.aspx"||pagename=="usercpprofile.aspx"||pagename=="usercppreference.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercpprofile.aspx\">个人资料</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercpprofile.aspx\">个人资料</a></li>\r\n	");
	}	//end if


	if (pagename=="usercpinbox.aspx"||pagename=="usercpsentbox.aspx"||pagename=="usercpdraftbox.aspx"||pagename=="usercppostpm.aspx"||pagename=="usercpshowpm.aspx"||pagename=="usercpannouncepm.aspx"||pagename=="usercpignorelist.aspx"||pagename=="usercpnotice.aspx"||pagename=="usercppmset.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercpinbox.aspx\">短消息</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercpinbox.aspx\">短消息</a></li>\r\n	");
	}	//end if


	if (pagename=="mytopics.aspx"||pagename=="myposts.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"mytopics.aspx\">我的帖子</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"mytopics.aspx\">我的帖子</a></li>\r\n	");
	}	//end if


	if (pagename=="myattachment.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"myattachment.aspx\">附件</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"myattachment.aspx\">附件</a></li>\r\n	");
	}	//end if


	if (pagename=="usercpsubscribe.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercpsubscribe.aspx\">收藏夹</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercpsubscribe.aspx\">收藏夹</a></li>\r\n	");
	}	//end if


	if (config.Enablespace==1 && user.Spaceid>0)
	{


	if (pagename=="usercpspacepostblog.aspx"||pagename=="usercpspacemanageblog.aspx"||pagename=="usercpspaceeditblog.aspx"||pagename=="usercpspacelinklist.aspx"||pagename=="usercpspacelinkedit.aspx"||pagename=="usercpspacelinkadd.aspx"||pagename=="usercpspacecomment.aspx"||pagename=="usercpspacemanagecategory.aspx"||pagename=="usercpspacecategoryadd.aspx"||pagename=="usercpspacecategoryedit.aspx"||pagename=="usercpspacemanageattachment.aspx"||pagename=="usercpspaceset.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercpspacemanageblog.aspx\">");
	templateBuilder.Append(config.Spacename.ToString().Trim());
	templateBuilder.Append("管理</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercpspacemanageblog.aspx\">");
	templateBuilder.Append(config.Spacename.ToString().Trim());
	templateBuilder.Append("管理</a></li>\r\n	");
	}	//end if


	}	//end if


	if (config.Enablealbum==1)
	{


	if (pagename=="usercpspacemanagealbum.aspx"||pagename=="usercpspacemanagephoto.aspx"||pagename=="usercpspacephotoadd.aspx"||pagename=="usercpeditphoto.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercpspacemanagealbum.aspx\">");
	templateBuilder.Append(config.Albumname.ToString().Trim());
	templateBuilder.Append("管理</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercpspacemanagealbum.aspx\">");
	templateBuilder.Append(config.Albumname.ToString().Trim());
	templateBuilder.Append("管理</a></li>\r\n	");
	}	//end if


	}	//end if


	if (pagename=="usercpcreditspay.aspx"||pagename=="usercpcreditstransfer.aspx"||pagename=="usercpcreditspayoutlog.aspx"||pagename=="usercpcreditspayinlog.aspx"   ||pagename=="usercpcreaditstransferlog.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercpcreaditstransferlog.aspx\">积分</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercpcreaditstransferlog.aspx\">积分</a></li>\r\n	");
	}	//end if


	if (pagename=="usercp.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercp.aspx\">用户组</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercp.aspx\">用户组</a></li>\r\n	");
	}	//end if


	if (pagename=="usercpnewpassword.aspx")
	{

	templateBuilder.Append("\r\n	<li class=\"current\"><a href=\"usercpnewpassword.aspx\">更改密码</a></li>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<li><a href=\"usercpnewpassword.aspx\">更改密码</a></li>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	</ul>\r\n</div>\r\n");
	}	//end if



	templateBuilder.Append("\r\n	<div class=\"uc_main\">\r\n	<div class=\"uc_content cl\">\r\n		<h1 class=\"u_t\">用户组&权限</h1>\r\n	");
	if (page_err==0)
	{


	if (ispost)
	{


	templateBuilder.Append("	<div class=\"msgbox\">\r\n		<h1>");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("　提示信息</h1>\r\n		<p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n		");
	if (msgbox_url!="")
	{

	templateBuilder.Append("\r\n		<p><a href=\"");
	templateBuilder.Append(msgbox_url.ToString());
	templateBuilder.Append("\">如果浏览器没有转向, 请点击这里.</a></p>\r\n		");
	}	//end if

	templateBuilder.Append("\r\n	</div>");


	}
	else
	{

	templateBuilder.Append("\r\n		<div class=\"cpuser cl\">\r\n		");	string avatarurl = Avatars.GetAvatarUrl(userid);
	
	templateBuilder.Append("\r\n			<img src=\"");
	templateBuilder.Append(avatarurl.ToString());
	templateBuilder.Append("\" onerror=\"this.onerror=null;this.src='");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("images/common/noavatar_medium.gif';\"/>\r\n			<ul class=\"cprate\">\r\n				<li><strong>");
	templateBuilder.Append(user.Username.ToString().Trim());
	templateBuilder.Append("</strong></li>\r\n				<li><label>积分:</label> ");
	templateBuilder.Append(user.Credits.ToString().Trim());
	templateBuilder.Append("</li>\r\n				");
	if (score[1].ToString().Trim()!="")
	{

	templateBuilder.Append("\r\n				<li><label>" + score[1].ToString().Trim() + ":</label>");
	templateBuilder.Append(score1.ToString());
	templateBuilder.Append("</li>\r\n				");
	}	//end if


	if (score[2].ToString().Trim()!="")
	{

	templateBuilder.Append("\r\n				<li><label>" + score[2].ToString().Trim() + ":</label>");
	templateBuilder.Append(score2.ToString());
	templateBuilder.Append("</li>\r\n				");
	}	//end if


	if (score[3].ToString().Trim()!="")
	{

	templateBuilder.Append("\r\n				<li><label>" + score[3].ToString().Trim() + ":</label>");
	templateBuilder.Append(score3.ToString());
	templateBuilder.Append("</li>\r\n				");
	}	//end if


	if (score[4].ToString().Trim()!="")
	{

	templateBuilder.Append("\r\n				<li><label>" + score[4].ToString().Trim() + ":</label>");
	templateBuilder.Append(score4.ToString());
	templateBuilder.Append("</li>\r\n				");
	}	//end if


	if (score[5].ToString().Trim()!="")
	{

	templateBuilder.Append("\r\n				<li><label>" + score[5].ToString().Trim() + ":</label>");
	templateBuilder.Append(score5.ToString());
	templateBuilder.Append("</li>\r\n				");
	}	//end if


	if (score[6].ToString().Trim()!="")
	{

	templateBuilder.Append("\r\n				<li><label>" + score[6].ToString().Trim() + ":</label>");
	templateBuilder.Append(score6.ToString());
	templateBuilder.Append("</li>\r\n				");
	}	//end if


	if (score[7].ToString().Trim()!="")
	{

	templateBuilder.Append("\r\n				<li><label>" + score[7].ToString().Trim() + ":</label>");
	templateBuilder.Append(score7.ToString());
	templateBuilder.Append("</li>\r\n				");
	}	//end if


	if (score[8].ToString().Trim()!="")
	{

	templateBuilder.Append("\r\n				<li><label>" + score[8].ToString().Trim() + ":</label>");
	templateBuilder.Append(score8.ToString());
	templateBuilder.Append("</li>\r\n				");
	}	//end if

	templateBuilder.Append("										\r\n			</ul>				\r\n			<ul class=\"cpinfo\">\r\n				<li><label>总发帖数:</label>");
	if (user.Posts>0)
	{

	templateBuilder.Append("<A href=\"search.aspx?posterid=");
	templateBuilder.Append(user.Uid.ToString().Trim());
	templateBuilder.Append("\">");
	templateBuilder.Append(user.Posts.ToString().Trim());
	templateBuilder.Append("</A>");
	}
	else
	{
	templateBuilder.Append(user.Posts.ToString().Trim());
	}	//end if

	templateBuilder.Append("</li>\r\n				<li><label>精华帖数:</label>");
	if (user.Digestposts>0)
	{

	templateBuilder.Append("<A href=\"search.aspx?posterid=");
	templateBuilder.Append(user.Uid.ToString().Trim());
	templateBuilder.Append("&type=digest\">");
	templateBuilder.Append(user.Digestposts.ToString().Trim());
	templateBuilder.Append("</a>\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n				");
	templateBuilder.Append(user.Digestposts.ToString().Trim());
	templateBuilder.Append("\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				</li>\r\n				<li><label>新短消息数:</label>");
	if (oluserinfo.Newpms>0)
	{

	templateBuilder.Append("<A href=\"usercpinbox.aspx\">");
	templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
	templateBuilder.Append("</A>\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n				<script>\r\n				document.write(");
	templateBuilder.Append(user.Newpmcount.ToString().Trim());
	templateBuilder.Append("*-1);\r\n				</");
	templateBuilder.Append("script>\r\n				");
	}	//end if

	templateBuilder.Append("</li>\r\n				<li><label>新通知数:</label>");
	if (oluserinfo.Newnotices>0)
	{

	templateBuilder.Append("<A href=\"\">");
	templateBuilder.Append(oluserinfo.Newnotices.ToString().Trim());
	templateBuilder.Append("</A>");
	}
	else
	{
	templateBuilder.Append(oluserinfo.Newnotices.ToString().Trim());
	}	//end if

	templateBuilder.Append("</li>								\r\n			</ul>\r\n		</div>\r\n		<div class=\"cpsignature cl\">\r\n			<span>签名</span>:");
	if (user.Signature=="")
	{

	templateBuilder.Append("\r\n			暂无签名\r\n			");
	}
	else
	{

	templateBuilder.Append("\r\n			");
	templateBuilder.Append(user.Sightml.ToString().Trim());
	templateBuilder.Append("\r\n			");
	}	//end if

	templateBuilder.Append("\r\n		</div>\r\n		<div class=\"limits_box datalist cl\">\r\n			<div id=\"list_memcp_main_c\" style=\"float:left;font-size:14px;padding-right:15px;\">\r\n				<h3>您的主用户组</h3>				\r\n			</div>\r\n			<div id=\"list_memcp_main\">\r\n			<div class=\"channelinfo\">主用户组决定了您在本论坛拥有哪些权限，您可以通过查看权限详细了解它</div>\r\n			<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" class=\"datatable\">\r\n				<thead class=\"colplural\">\r\n				<tr>\r\n					<td width=\"13%\"/>\r\n					<td width=\"11%\">用户级别</td>\r\n					<td width=\"15%\">类型</td>\r\n					<td width=\"20%\">积分起点</td>\r\n					<td width=\"10%\">阅读权限</td>\r\n					<td width=\"10%\">到期时间</td>\r\n					<td>操作</td>\r\n				</tr>\r\n				</thead>\r\n				<tbody>\r\n					<tr>\r\n					<td><strong><u>");
	templateBuilder.Append(usergroupinfo.Grouptitle.ToString().Trim());
	templateBuilder.Append("</u><strong/></strong></td>\r\n					<td>					\r\n					<script type=\"text/javascript\">\r\n					ShowStars(");
	templateBuilder.Append(usergroupinfo.Stars.ToString().Trim());
	templateBuilder.Append(", ");
	templateBuilder.Append(config.Starthreshold.ToString().Trim());
	templateBuilder.Append(");\r\n					</");
	templateBuilder.Append("script>\r\n					</td>\r\n					<td>（会员用户组）</td>\r\n					<td>");
	templateBuilder.Append(usergroupinfo.Creditshigher.ToString().Trim());
	templateBuilder.Append("</td>\r\n					<td>");
	templateBuilder.Append(usergroupinfo.Readaccess.ToString().Trim());
	templateBuilder.Append("</td>\r\n					<td> - </td>\r\n					<td>\r\n					<a class=\"xg2\" href=\"###\" onclick=\"javascript:if ($('usergroupbox').style.display=='none')\r\n					{\r\n					$('usergroupbox').style.display='';\r\n					}\r\n					else\r\n					{\r\n					$('usergroupbox').style.display='none';\r\n					}\">查看权限</a>\r\n					</td>\r\n					</tr>\r\n				</tbody>\r\n			</table>\r\n			</div>\r\n		</div>\r\n    	<div class=\"limits_box cl\" id=\"usergroupbox\" style=\"display:none;\">\r\n			");
	if (useradminid>0)
	{

	templateBuilder.Append("\r\n			<div class=\"c_header cl\" id=\"list_admin_c\">\r\n				<h3 onclick=\"toggle_collapse('list_admin', 1, 1);\">管理权限</h3>\r\n				<div class=\"y\"><p onclick=\"toggle_collapse('list_admin', 1, 1);\" class=\"c_header_ctrlbtn\">[ 展开 ]</p></div>\r\n			</div>\r\n			<div id=\"list_admin\">\r\n			<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" class=\"datatable\" >\r\n			<tbody>\r\n			<tr class=\"colplural\">\r\n				<th>允许编辑帖子</th>\r\n				<th>允许编辑投票</th>\r\n				<th>允许删除帖子</th>\r\n				<th>允许批量删除</th>\r\n				<th>允许编辑用户</th>\r\n				<th>允许查看论坛运行记录</th>\r\n			</tr>\r\n			<tr>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Alloweditpost.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Alloweditpoll.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Allowdelpost.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Allowmassprune.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Allowedituser.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Allowviewlog.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n			</tr>\r\n			</tbody>\r\n			<tbody>\r\n			<tr class=\"colplural\">\r\n				<th>允许查看用户实名</th>\r\n				<th>允许禁止用户</th>\r\n				<th>允许禁止IP</th>\r\n				<th>是否允许置顶</th>\r\n				<th>允许查看IP</th>\r\n				<th>发帖不受审核、过滤、灌水等限制</th>\r\n			</tr>\r\n			<tr>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Allowviewrealname.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Allowbanuser.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Allowbanip.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">tickthreadtype(");
	templateBuilder.Append(admingroupinfo.Allowstickthread.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Allowviewip.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(admingroupinfo.Disablepostctrl.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n			</tr>\r\n			</tbody>\r\n			</table>\r\n			</div>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			<div class=\"c_header cl\" id=\"list_basic_c\">\r\n				<h3 onclick=\"toggle_collapse('list_basic', 1, 1);\">基本权限</h3>\r\n				<div class=\"y\"><p onclick=\"toggle_collapse('list_basic', 1, 1);\" class=\"c_header_ctrlbtn\">[ 展开 ]</p></div>\r\n			</div>\r\n			<table cellspacing=\"0\" cellpadding=\"0\" id=\"list_basic\" width=\"100%\" class=\"datatable\" >\r\n			<tbody>\r\n			<tr class=\"colplural\">\r\n				<th>访问论坛</th>\r\n				<th>阅读权限</th>\r\n				<th>查看用户资料</th>							\r\n				<th>使用搜索</th>								\r\n				<th>短信收件箱容量</th>\r\n			</tr>\r\n			<tr>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowvisit.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td>");
	templateBuilder.Append(usergroupinfo.Readaccess.ToString().Trim());
	templateBuilder.Append("</td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowviewpro.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">searchtype(");
	templateBuilder.Append(usergroupinfo.Allowsearch.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td>");
	templateBuilder.Append(usergroupinfo.Maxpmnum.ToString().Trim());
	templateBuilder.Append("</td>\r\n			</tr>\r\n			</tbody>\r\n			</table>\r\n			<div class=\"c_header cl\" id=\"list_post_c\">\r\n				<h3 onclick=\"toggle_collapse('list_post', 1, 1);\">帖子相关</h3>\r\n				<div class=\"y\"><p onclick=\"toggle_collapse('list_post', 1, 1);\" class=\"c_header_ctrlbtn\">[ 展开 ]</p></div>\r\n			</div>\r\n			<div id=\"list_post\">\r\n			<table cellspacing=\"0\" cellpadding=\"0\"  width=\"100%\" class=\"datatable\" >\r\n			<tbody>\r\n			<tr class=\"colplural\">\r\n				<th>发新话题</th>\r\n				<th>发表回复</th>\r\n				<th>发起投票</th>\r\n				<th>参与投票</th>\r\n				<th>发表悬赏</th>\r\n				<th>发表辩论</th>\r\n				<th>发表交易</th>								\r\n			</tr>\r\n			<tr>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowpost.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowreply.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowpostpoll.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowvote.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowbonus.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowdebate.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowtrade.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>								\r\n			</tr>\r\n			</tbody>\r\n			</table>\r\n			<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" class=\"datatable\" >\r\n			  <tbody>\r\n				<tr class=\"colplural\">\r\n				  <th>最大签名长度</th>\r\n				  <th>签名中使用 Discuz! 代码</th>\r\n				  <th>签名中使用 [img] 代码</th>\r\n				  <th>是否允许HTML帖</th>\r\n				  <th>是否允许使用hide代码</th>\r\n				  <th>主题最高售价</th>\r\n				</tr>\r\n				<tr>\r\n				  <td>");
	templateBuilder.Append(usergroupinfo.Maxsigsize.ToString().Trim());
	templateBuilder.Append("</td>\r\n				  <td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowsigbbcode.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				  <td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowsigimgcode.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				  <td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowhtml.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				  <td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowhidecode.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				  <td>");
	templateBuilder.Append(usergroupinfo.Maxprice.ToString().Trim());
	templateBuilder.Append("</td>\r\n				</tr>\r\n			  </tbody>\r\n			</table>\r\n			</div>\r\n			<div class=\"c_header cl\" id=\"list_attachment_c\">\r\n				<h3 onclick=\"toggle_collapse('list_attachment', 1, 1);\">附件相关</h3>\r\n				<div class=\"y\"><p onclick=\"toggle_collapse('list_attachment', 1, 1);\" class=\"c_header_ctrlbtn\">[ 展开 ]</p></div>\r\n			</div>\r\n			<div id=\"list_post cl\">\r\n			<table cellspacing=\"0\" cellpadding=\"0\" id=\"list_attachment\" width=\"100%\" class=\"datatable\" >\r\n			<tbody>\r\n			<tr class=\"colplural\">\r\n				<th>下载/查看附件</th>\r\n				<th>发布附件</th>\r\n				<th>设置附件权限</th>\r\n				<th>单个最大附件尺寸</th>\r\n				<th>每天最大附件总尺寸</th>\r\n				<th>附件类型</th>\r\n			</tr>\r\n			<tr>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowgetattach.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowpostattach.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td><script type=\"text/javascript\">getvalidpic(");
	templateBuilder.Append(usergroupinfo.Allowsetattachperm.ToString().Trim());
	templateBuilder.Append(")</");
	templateBuilder.Append("script></td>\r\n				<td>");
	templateBuilder.Append(usergroupinfo.Maxattachsize.ToString().Trim());
	templateBuilder.Append("</td>\r\n				<td>");
	templateBuilder.Append(usergroupinfo.Maxsizeperday.ToString().Trim());
	templateBuilder.Append("</td>\r\n				<td>");
	templateBuilder.Append(usergroupattachtype.ToString());
	templateBuilder.Append("</td>\r\n			</tr>\r\n			</tbody>\r\n			</table>\r\n			</div>\r\n		</div>\r\n		");
	}	//end if


	}
	else
	{


	templateBuilder.Append("<div class=\"msgbox error_msg\">\r\n	<h3>错误提示</h3>\r\n	<p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n	<p class=\"errorback\">\r\n		<script type=\"text/javascript\">\r\n			if(");
	templateBuilder.Append(msgbox_showbacklink.ToString());
	templateBuilder.Append(")\r\n			{\r\n				document.write(\"<a href=\\\"");
	templateBuilder.Append(msgbox_backlink.ToString());
	templateBuilder.Append("\\\">返回上一步</a> &nbsp; &nbsp;|  \");\r\n			}\r\n		</");
	templateBuilder.Append("script>\r\n		&nbsp; &nbsp; <a href=\"forumindex.aspx\">论坛首页</a>\r\n		");
	if (usergroupid==7)
	{

	templateBuilder.Append("\r\n		 &nbsp; &nbsp;|&nbsp; &nbsp; <a href=\"login.aspx\">登录</a>&nbsp; &nbsp;|&nbsp; &nbsp; <a href=\"register.aspx\">注册</a>\r\n		");
	}	//end if

	templateBuilder.Append("\r\n	</p>\r\n</div>");


	}	//end if

	templateBuilder.Append("\r\n	</div>\r\n</div>\r\n</div>\r\n");

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
