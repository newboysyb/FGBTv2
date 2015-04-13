<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.invite" %>
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




	if (infloat==1)
	{

	templateBuilder.Append("\r\n	<!--弹窗start-->\r\n	");
	if (ispost)
	{


	if (page_err==0)
	{

	templateBuilder.Append("\r\n		<script type=\"text/javascript\">\r\n		    $('returnmessage').className = '';\r\n	        setTimeout('location.reload()', 2000);\r\n		</");
	templateBuilder.Append("script>		\r\n	  ");
	}	//end if

	templateBuilder.Append("\r\n	  <p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<h3 class=\"flb\">\r\n		<em id=\"returnmessage\">发送邀请码</em>\r\n		<span class=\"y\"><a href=\"javascript:;\" class=\"flbc\" onclick=\"hideWindow('emailtoinvite')\" title=\"关闭\">关闭</a></span>\r\n	</h3>\r\n	<div id=\"floatwinemailbox\" style=\" text-align:left; margin:20px\">\r\n	<form id=\"sendcodeonfloat\" method=\"post\" onsubmit=\"javascript:$('sendcodeonfloat').action='invite.aspx?action=floatwinemailsend&invitecode=");
	templateBuilder.Append(invitecode.ToString());
	templateBuilder.Append("&usersaid=");
	templateBuilder.Append(usersaid.ToString());
	templateBuilder.Append("&infloat=1&';ajaxpost('sendcodeonfloat', 'returnmessage', 'returnmessage', 'onerror');return false;\" action=\"\">\r\n        <ul>\r\n        ");
	if (config.Regstatus==3)
	{

	templateBuilder.Append("\r\n           <li style=\"padding-bottom:8px\">邀请码:<a href=\"javascript:;\" onclick=\"copycodeinfloat('");
	templateBuilder.Append(invitecode.ToString());
	templateBuilder.Append("');\">");
	templateBuilder.Append(invitecode.ToString());
	templateBuilder.Append("</a> 点击复制</li>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n           <li style=\"padding-bottom:8px\">Email: <input id=\"floatwinemail\" name=\"email\" type=\"text\" style=\"width:300px;margin-right:10px;\" class=\"txt\"/><br/>(多个地址用\",\"分割)</li>\r\n           <li><button name=\"emailtoinvite\" title=\"通过Email将邀请链接发送给好友\" type=\"submit\" id=\"floatwinemailtoinvite\" onclick=\"return checkStrNullOrEmptyInFloat('floatwinemail','Email地址不能为空')\">发送给好友</button></li>\r\n        </ul>\r\n	</form>\r\n	</div>\r\n	<script type=\"text/javascript\" reload=\"1\">\r\n	    function checkStrNullOrEmptyInFloat(str, message) {\r\n	        var s = $(str).value;\r\n	        if (s == '') {\r\n	            $('returnmessage').innerHTML = '<p>' + message + '</p>';\r\n	            return false;\r\n	        }\r\n	        return true;\r\n	    }\r\n	    \r\n        function copycodeinfloat(obj) {\r\n            obj = '");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("invite.aspx?invitecode=' + obj;\r\n//            if(is_moz)\r\n//               floatwin('close_emailtoinvite');\r\n            setcopy(is_ie ? obj.replace(/\\r\\n\\r\\n/g, '\\r\\n') : obj, '复制完成');\r\n            }\r\n    </");
	templateBuilder.Append("script>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	<!--弹窗over-->\r\n");
	}
	else
	{

	templateBuilder.Append("\r\n	<!--非弹窗start-->\r\n<script type=\"text/javascript\">\r\n    function copycode(obj) {\r\n        setcopy(is_ie ? obj.value.replace(/\\r\\n\\r\\n/g, '\\r\\n') : obj.value, '复制完成');\r\n    }\r\n\r\n    function checkStrNullOrEmpty(str, message) {\r\n        var s = $(str).value;\r\n        if (s == '') {\r\n            alert(message);\r\n            return false;\r\n        }\r\n        return true;\r\n    }\r\n\r\n    function usersaidPriview() {\r\n        $('usersaidinemail').innerHTML = $('usersaid').value;\r\n    }\r\n</");
	templateBuilder.Append("script>\r\n<div class=\"wrap s_clear pageinfo\">\r\n	<div id=\"nav\"><a href=\"");
	templateBuilder.Append(config.Forumurl.ToString().Trim());
	templateBuilder.Append("\">");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("</a> &raquo; <strong>邀请注册</strong></div>\r\n</div>\r\n");
	if (page_err!=0)
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


	}
	else
	{


	if (ispost)
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

	templateBuilder.Append("\r\n	<div class=\"wrap s_clear\" id=\"wrap\">\r\n	<div class=\"main\" style=\"padding:18px 28px\">\r\n	<h1>邀请注册</h1>\r\n    ");
	if (userid>0)
	{


	if (config.Regstatus==2)
	{


	if (invitecodeinfo==null)
	{

	templateBuilder.Append("\r\n				<form id=\"createinvitecode\" method=\"post\" action=\"invite.aspx?action=createcode\">\r\n					<table summary=\"邀请注册\" cellspacing=\"0\" cellpadding=\"0\" class=\"formtable\"  id=\"invitecodemsg\">\r\n					<tr id=\"promotiondescription\">\r\n						<td>");
	templateBuilder.Append(invitationconfiginfo.InvitationLoginUserDescription.ToString().Trim());
	templateBuilder.Append("</td>\r\n					</tr>\r\n					<tr>\r\n						<td><button name=\"createcode\" type=\"submit\" id=\"createcode\">我要加入</button></td></td>\r\n					</tr>\r\n					</table>\r\n				</form>\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n				<div style=\"margin-top:20px\">\r\n				 您可以通过QQ、MSN等IM工具，或者发送邮件，把下面的链接告诉你的好友，邀请他们加入进来。<br />\r\n				 ");
	if (invitationconfiginfo.InviteCodePayCount>0)
	{

	templateBuilder.Append("当成功邀请");
	templateBuilder.Append(invitationconfiginfo.InviteCodePayCount.ToString().Trim());
	templateBuilder.Append("个用户后可以兑换到积分。<br />");
	}	//end if


	if (invitationconfiginfo.InviteCodeMaxCount>0)
	{

	templateBuilder.Append("您的邀请链接最多可以被成功使用");
	templateBuilder.Append(invitationconfiginfo.InviteCodeMaxCount.ToString().Trim());
	templateBuilder.Append("次。<br />");
	}	//end if


	if (invitecodeinfo.CreateTime!=invitecodeinfo.ExpireTime)
	{
	templateBuilder.Append(InviteCodeExpireTip(invitecodeinfo.ExpireTime).ToString().Trim());
	}	//end if

	templateBuilder.Append("\r\n				</div>\r\n				<hr class=\"solidline\"/>\r\n				<form id=\"convertinvitecode\" method=\"post\" action=\"invite.aspx?action=convertcode\">\r\n				<table summary=\"邀请注册\" cellspacing=\"0\" cellpadding=\"0\" class=\"formtable\"  id=\"invitecodemsg\">\r\n				<tr>\r\n					<th><label for=\"invitecode\">推广链接:</label></th>\r\n					<td><input id=\"invitecode\" title=\"双击文本框复制推广链接\" type=\"text\" value=\"");
	templateBuilder.Append(inviteurl.ToString());
	templateBuilder.Append("\" style=\"width:350px;margin-right:10px;\" readonly=\"readonly\" ondblclick=\"copycode($('invitecode'))\" />&nbsp;&nbsp;<button id=\"copylink\" type=\"button\" onclick=\"copycode($('invitecode'))\">复制链接</button>\r\n					</td>\r\n				</tr>\r\n				<tr>\r\n					<th><label for=\"successcount\">成功邀请用户:</label></th>\r\n					<td>");
	templateBuilder.Append(invitecodeinfo.SuccessCount.ToString().Trim());
	templateBuilder.Append("人 </td>\r\n				</tr>\r\n				<tr>\r\n					<th><label for=\"userscore\">可兑换到的积分:</label></th>\r\n					<td>");
	templateBuilder.Append(userscore.ToString());
	if (invitecodeinfo.SuccessCount-invitationconfiginfo.InviteCodePayCount>0)
	{

	templateBuilder.Append("<button name=\"convertcode\" type=\"submit\" id=\"convertcode\" style=\"margin-left:10px;\" onclick=\"return confirm('是否要将邀请码兑换成积分')\">我要兑换</button>\r\n                    ");
	}
	else if (invitecodeinfo.SuccessCount-invitationconfiginfo.InviteCodePayCount<0&&datediff>0)
	{

	templateBuilder.Append("<button name=\"convertcode\" type=\"submit\" id=\"Button1\" style=\"margin-left:10px;\" onclick=\"return confirm('是否要删除已过期的邀请链接?')\">删除过期邀请链接</button>");
	}	//end if

	templateBuilder.Append("</td>\r\n				</tr>\r\n				</table>\r\n				</form>\r\n				<hr class=\"solidline\"/>\r\n				<h1>邮件邀请朋友</h1>\r\n				<div style=\"padding-top:20px\">\r\n				<table summary=\"用户自定义邮件内容\" cellspacing=\"0\" cellpadding=\"0\" class=\"formtable\"  id=\"userscript\">\r\n				");
	if (isuseusersaid)
	{

	templateBuilder.Append("\r\n				<tr>\r\n				   <th><label for=\"usersaid\">想对好友说的话:</label></th>\r\n				   <td><input id=\"usersaid\" name=\"usersaid\" type=\"text\" style=\"width:350px;margin-right:10px;\" onkeyup=\"usersaidPriview();\" /></td>\r\n				</tr>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				<tr>\r\n				   <th colspan=\"2\"><label for=\"emailpreview\">邮件内容预览:</label></th>\r\n				</tr>\r\n				<tr>\r\n				   <td colspan=\"2\">\r\n				     <div style=\"overflow:hidden;zoom:1;border:1px dashed #CCC;margin-top:10px;background:#FBFBFB; padding:20px\">");
	templateBuilder.Append(emailpreview.ToString());
	templateBuilder.Append("</div>\r\n				   </td>				   \r\n				</tr>\r\n				<tr>\r\n				   <td colspan=\"2\"><button onclick=\"showWindow('emailtoinvite', '");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("invite.aspx?action=floatwinemail&invitecode=");
	templateBuilder.Append(invitecodeinfo.Code.ToString().Trim());
	templateBuilder.Append("&usersaid='");
	if (isuseusersaid)
	{

	templateBuilder.Append("+escape($('usersaid').value)");
	}	//end if

	templateBuilder.Append(",'get',0);return false;\">通过Email邀请好友</button></td>\r\n				</tr>\r\n				</table>\r\n				</div>\r\n				");
	}	//end if


	}
	else
	{

	templateBuilder.Append("\r\n		   <div id=\"userinvitecodelist\" class=\"datalist\" style=\" margin-top:20px\">\r\n		   <div class=\"hintinfo\" style=\"margin-top:0\">您拥有的扩展积分:");
	templateBuilder.Append(CreateUserExtCreditsStr().ToString());
	templateBuilder.Append("</div>\r\n		   <div class=\"hintinfo\" style=\"margin-top:0\">您购买的邀请码会在失效之后由系统定时清理，不会长期驻留在列表中</div>\r\n		      ");
	if (invitecodecount>0)
	{

	templateBuilder.Append("\r\n				<table summary=\"邀请码\" cellspacing=\"0\" cellpadding=\"0\" class=\"datatable\" id=\"invitecodelist\">\r\n				<thead class=\"colplural\">\r\n				<tr>\r\n					<th>邀请码</th>\r\n					<th>购买时间</th>\r\n					<th>过期时间</th>\r\n					<th>可使用次数</th>\r\n					<th>已使用次数</th>\r\n				</tr>\r\n				</thead>\r\n				<tbody>\r\n				");
	int code__loop__id=0;
	foreach(InviteCodeInfo code in invitecodelist)
	{
		code__loop__id++;

	templateBuilder.Append("\r\n				<tr>\r\n					<td><a href=\"javascript:;\" onclick=\"showWindow('emailtoinvite', '");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("invite.aspx?action=floatwinemail&invitecode=");
	templateBuilder.Append(code.Code.ToString().Trim());
	templateBuilder.Append("','get',0);return false;\">");
	templateBuilder.Append(code.Code.ToString().Trim());
	templateBuilder.Append("</a></td><td>");
	templateBuilder.Append(code.CreateTime.ToString().Trim());
	templateBuilder.Append("</td><td>");
	if (code.CreateTime!=code.ExpireTime)
	{
	templateBuilder.Append(code.ExpireTime.ToString().Trim());
	}
	else
	{

	templateBuilder.Append("永不过期");
	}	//end if

	templateBuilder.Append("</td><td>");
	templateBuilder.Append(code.MaxCount.ToString().Trim());
	templateBuilder.Append("</td><td>");
	templateBuilder.Append(code.SuccessCount.ToString().Trim());
	templateBuilder.Append("</td>\r\n				</tr>\r\n				");
	}	//end loop

	templateBuilder.Append("\r\n				</tbody>\r\n				</table>\r\n			  ");
	}
	else
	{

	templateBuilder.Append("\r\n				<div class=\"zerothreads\">您还没有购买邀请码</div>\r\n			  ");
	}	//end if

	templateBuilder.Append("\r\n				<div class=\"pages_btns\">\r\n					<div class=\"pages\">\r\n						");
	if (invitecodecount>0)
	{

	templateBuilder.Append("<kbd>现有");
	templateBuilder.Append(invitecodecount.ToString());
	templateBuilder.Append("个邀请码</kbd>");
	templateBuilder.Append(pagenumber.ToString());
	templateBuilder.Append("<kbd>共");
	templateBuilder.Append(pagecount.ToString());
	templateBuilder.Append("页</kbd>");
	}	//end if

	templateBuilder.Append("\r\n					</div>\r\n				   <div style=\"overflow:hidden; zoom:1; margin-bottom:10px;float:left;\">\r\n					   <form id=\"buyinvitecode\" method=\"post\" action=\"invite.aspx?action=buycode\" style=\"float:left; margin-right:10px\">\r\n						  <div id=\"buycode\">\r\n							  邀请码售价: ");
	templateBuilder.Append(invitecodeprice.ToString());
	if (invitecodecount<invitationconfiginfo.InviteCodeMaxCountToBuy)
	{

	int couldbuy = invitationconfiginfo.InviteCodeMaxCountToBuy-invitecodecount;
	
	templateBuilder.Append("您还能购买");
	templateBuilder.Append(couldbuy.ToString());
	templateBuilder.Append("个 <button id=\"buycode\" type=\"submit\" onclick=\"return confirm('确定购买？')\">我要购买</button>");
	}	//end if

	templateBuilder.Append("\r\n						  </div>\r\n					   </form>\r\n				   </div>\r\n				</div>\r\n		   </div>\r\n		");
	}	//end if


	}
	else
	{

	templateBuilder.Append("\r\n		<table summary=\"邀请注册\" cellspacing=\"0\" cellpadding=\"0\" class=\"formtable\"  id=\"invitecodemsg\">\r\n		<tr id=\"visitordescription\">\r\n			<td>");
	templateBuilder.Append(invitationconfiginfo.InvitationVisitorDescription.ToString().Trim());
	templateBuilder.Append("</td>\r\n		</tr>\r\n		<tr>\r\n		    <td>\r\n		    <div style=\"overflow:hidden;zoom:1;border:1px dashed #CCC;margin-top:20px;background:#FBFBFB; padding:20px\">\r\n		    ");
	if (invitecodeinfo!=null)
	{

	string avatarurl = Avatars.GetAvatarUrl(invitecodeinfo.CreatorId);
	
	templateBuilder.Append("\r\n				<img src=\"");
	templateBuilder.Append(avatarurl.ToString());
	templateBuilder.Append("\" onerror=\"this.onerror=null;this.src='");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/noavatar_medium.gif';\"  alt=\"头像\" id=\"memberinfo\" style=\"float:left; margin-right:40px; height:80px\"/>\r\n			    <div id=\"creatormessage\" style=\"vertical-align:top\">您好:<br/>您的朋友 ");
	templateBuilder.Append(invitecodeinfo.Creator.ToString().Trim());
	templateBuilder.Append(" 邀请您加入到我们的行列当中<br />");
	}	//end if

	string regurl = "register.aspx";
	

	if (invitecodeinfo!=null)
	{

	 regurl = "register.aspx?invitecode="+invitecode;
	

	}	//end if

	templateBuilder.Append("\r\n			    <a href=\"");
	templateBuilder.Append(regurl.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/add_invite.gif\" style=\"margin-top:14px\" /></a></div>\r\n		    </div>\r\n		    </td>\r\n		</tr>\r\n		</table>\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n	</div>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n		<!--非弹窗over-->\r\n	</div>\r\n  ");
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
