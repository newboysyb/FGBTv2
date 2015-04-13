<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.topicadmin" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>
<%@ Import namespace="Discuz.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2015/1/7 16:52:34.
		本页面代码由Discuz!NT模板引擎生成于 2015/1/7 16:52:34. 
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




	if (!issubmit)
	{


	if (page_err==0)
	{

	templateBuilder.Append("\r\n<div id=\"floatlayout_mods\">\r\n	<h3 class=\"flb\"> \r\n	<em id=\"return_mods\">");
	templateBuilder.Append(operationtitle.ToString());
	templateBuilder.Append("</em>\r\n	");
	if (infloat==1)
	{

	templateBuilder.Append("\r\n		<span class=\"y\">\r\n			<a title=\"关闭\" onclick=\"hideWindow('mods');if(navigator.userAgent.indexOf('Firefox')>=0) window.location.reload(true);\" class=\"flbc\" href=\"javascript:;\">关闭</a>\r\n		</span>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	</h3>\r\n	<div class=\"c cl\">\r\n	");
	if (infloat==1)
	{

	templateBuilder.Append("\r\n	<form id=\"moderate_admin\" name=\"moderate_admin\" method=\"post\" onsubmit=\"ajaxpost('moderate_admin', 'return_mods', 'return_mods', 'onerror');return false;\" action=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("topicadmin.aspx?action=moderate&operation=");
	templateBuilder.Append(operation.ToString());
	templateBuilder.Append("&infloat=1\">\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<form id=\"moderate_admin\" name=\"moderate_admin\" method=\"post\" action=\"topicadmin.aspx?action=moderate&operation=");
	templateBuilder.Append(operation.ToString());
	templateBuilder.Append("\">\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	<input type=\"hidden\" name=\"topicid\" value=\"");
	templateBuilder.Append(topiclist.ToString());
	templateBuilder.Append("\" />\r\n	<input type=\"hidden\" name=\"forumid\" value=\"");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\" />\r\n    ");
	if (config.Aspxrewrite==1)
	{

	templateBuilder.Append("\r\n	<input type=\"hidden\" id=\"referer\" name=\"referer\" value=\"showforum-");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append(config.Extname.ToString().Trim());
	templateBuilder.Append("\" />\r\n    ");
	}
	else
	{

	templateBuilder.Append("	\r\n	<input type=\"hidden\" id=\"referer\" name=\"referer\" value=\"showforum.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\">\r\n    ");
	}	//end if

	templateBuilder.Append("	\r\n    <script type=\"text/javascript\">\r\n	    var re = getQueryString(\"referer\");\r\n	    if (re != \"\")\r\n	    {\r\n		    $(\"referer\").value = unescape(re);\r\n	    }\r\n    </");
	templateBuilder.Append("script>\r\n	<!--操作面板开始-->	\r\n		");
	if (operation=="highlight")
	{

	templateBuilder.Append("\r\n		<!--高亮开始-->\r\n	    <div class=\"topicadminlow detailopt\">\r\n	        <span class=\"hasdropdownbtn\">\r\n			    <input type=\"hidden\" id=\"highlight_color\" name=\"highlight_color\" value=\"\" />\r\n			    <span style=\"display:none\">\r\n			    <input type=\"checkbox\" id=\"highlight_style_b\" name=\"highlight_style_b\" value=\"B\" />\r\n			    <input type=\"checkbox\" id=\"highlight_style_i\" name=\"highlight_style_i\" value=\"I\" />\r\n			    <input type=\"checkbox\" id=\"highlight_style_u\" name=\"highlight_style_u\" value=\"U\" />\r\n			    </span>\r\n			    <input id=\"color_bg\" type=\"text\" class=\"txt\" readonly=\"readonly\" />\r\n			    <a href=\"javascript:;\" onclick=\"display('color_menu')\" class=\"dropdownbtn\">^</a>\r\n		    </span>\r\n		    <div id=\"color_menu\" class=\"color_menu\" style=\"display: none\">\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" title=\"取消高亮\" style=\"background:#000;text-indent:0;color:#F00;text-decoration:none;\">X</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#EE1B2E;color:#EE1B2E;\">#EE1B2E</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#EE5023;color:#EE5023;\">#EE5023</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#996600;color:#996600;\">#996600</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#3C9D40;color:#3C9D40;\">#3C9D40</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#2897C5;color:#2897C5;\">#2897C5</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#2B65B7;color:#2B65B7;\">#2B65B7</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#8F2A90;color:#8F2A90;\">#8F2A90</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#EC1282;color:#EC1282;\">#EC1282</a>\r\n\r\n                <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#FFFF00;color:#FFFF00;\">#FFFF00</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#00FFFF;color:#00FFFF;\">#00FFFF</a>\r\n			    <a href=\"javascript:;\" onclick=\"switchhl(1,this,0)\" style=\"background:#808080;color:#808080;\">#808080</a>\r\n		    </div>\r\n		    <a title=\"粗体\" style=\"text-indent: 0pt; text-decoration: none; font-weight: 700;\" class=\"detailopt_bold\" onclick=\"switchhl(2, this, 'b')\" id=\"highlight_op_1\" href=\"javascript:;\">B</a>\r\n            <a title=\"斜体\" style=\"text-indent: 0pt; text-decoration: none; font-style: italic;\" class=\"detailopt_italic\" onclick=\"switchhl(2, this, 'i')\" id=\"highlight_op_2\" href=\"javascript:;\">I</a>\r\n            <a title=\"下划线\" style=\"text-indent: 0pt; text-decoration: underline;\" class=\"detailopt_underline\" onclick=\"switchhl(2, this, 'u')\" id=\"highlight_op_3\" href=\"javascript:;\">U</a>\r\n		    <script type=\"text/javascript\">\r\n		        function switchhl(op, obj, v)\r\n		        {\r\n		            if (op == 1)\r\n		            {\r\n		                $('highlight_color').value = obj.style.backgroundColor;\r\n		                $('color_bg').style.backgroundColor = obj.style.backgroundColor;\r\n		                $('color_menu').style.display = 'none';\r\n		            } else if (op == 2)\r\n		            {\r\n		                if ($('highlight_style_' + v).checked)\r\n		                {\r\n		                    $('highlight_style_' + v).checked = false;\r\n		                    obj.className = obj.className.replace(/ current/, '');\r\n		                } else\r\n		                {\r\n		                    $('highlight_style_' + v).checked = true;\r\n		                    obj.className += ' current';\r\n		                }\r\n		            }\r\n		        }\r\n            </");
	templateBuilder.Append("script>\r\n        </div>\r\n		<!--高亮结束-->\r\n		");
	}	//end if


	if (operation=="displayorder")
	{

	templateBuilder.Append("\r\n		<!--置顶开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <ul class=\"inlinelist\">\r\n                ");
	if (displayorder>0)
	{

	templateBuilder.Append("<li class=\"wide\"><label><input type=\"radio\" value=\"0\" name=\"level\" class=\"radio\"/> 解除置顶</label></li>");
	}	//end if

	templateBuilder.Append("\r\n                <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"level\" class=\"radio\"");
	if (displayorder<=1)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 本版置顶</label></li>\r\n                <li class=\"wide\"><label><input type=\"radio\" value=\"2\" name=\"level\" class=\"radio\"");
	if (displayorder==2)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 分类置顶</label></li>\r\n                <li class=\"wide\"><label><input type=\"radio\" value=\"3\" name=\"level\" class=\"radio\"");
	if (displayorder==3)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 全局置顶</label></li>\r\n            </ul>\r\n		</div>\r\n		<!--置顶结束-->\r\n		");
	}	//end if


	if (operation=="digest")
	{

	templateBuilder.Append("\r\n		<!--精华开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <ul class=\"inlinelist\">\r\n                ");
	if (digest>0)
	{

	templateBuilder.Append("<li class=\"wide\"><label><input type=\"radio\" value=\"0\" name=\"level\" class=\"radio\"/> 解除精华</label></li>");
	}	//end if

	templateBuilder.Append("\r\n                <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"level\" class=\"radio\"");
	if (digest<=1)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 一级精华</label></li>\r\n                <li class=\"wide\"><label><input type=\"radio\" value=\"2\" name=\"level\" class=\"radio\"");
	if (digest==2)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 二级精华</label></li>\r\n                <li class=\"wide\"><label><input type=\"radio\" value=\"3\" name=\"level\" class=\"radio\"");
	if (digest==3)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 三级精华</label></li>\r\n            </ul>\r\n		</div>\r\n		<!--精华结束-->\r\n		");
	}	//end if


	if (operation=="move")
	{

	templateBuilder.Append("\r\n		<!--移动开始-->\r\n		<div class=\"topicadminlow cl\">\r\n		    <p class=\"tah_body tah_fixiesel\">\r\n                <label for=\"moveto\">目标版块:</label> <select onchange=\"movetoOnchange(this);\" name=\"moveto\" id=\"moveto\">\r\n                ");
	templateBuilder.Append(forumlist.ToString());
	templateBuilder.Append("\r\n                </select>\r\n            </p>\r\n            <p id=\"movetopictypelist\" class=\"tah_body tah_fixiesel cl\">\r\n                 <label for=\"movettype\">主题分类:</label> <select id=\"movettype\" name=\"movetopictype\">\r\n                 </select>\r\n            </p>\r\n            <p class=\"tah_body\"></p>\r\n            <ul style=\"margin: 5px 0pt;display: none;\" id=\"moveext\" class=\"inlinelist cl\">\r\n                <li class=\"wide\"><label><input type=\"radio\" checked=\"checked\" value=\"normal\" name=\"type\" class=\"radio\"/> 移动主题</label></li>\r\n                <li class=\"wide\"><label><input type=\"radio\" value=\"redirect\" name=\"type\" class=\"radio\"/> 保留转向</label></li>\r\n            </ul>\r\n        </div>\r\n        <script type=\"text/javascript\">\r\n            function movetoOnchange(obj){\r\n                if (obj.value)\r\n                    $('moveext').style.display = '';\r\n                else\r\n                    $('moveext').style.display = 'none';\r\n\r\n                var movselect = $('movettype');\r\n                while (movselect.length > 0) {\r\n                    movselect.options[movselect.length - 1] = null;\r\n                }\r\n                var forumid = obj.value;\r\n                _sendRequest('");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("tools/ajax.aspx?t=getforumtopictypelist&fid=' + forumid + '&r=1' + Math.random(), function (responseText) {\r\n                    if (responseText) {\r\n                        var topictypes = eval('(' + responseText + ')');\r\n                        if (topictypes.length > 0) {\r\n                            for (var i in topictypes) {\r\n                                var option = document.createElement('option');\r\n                                option.appendChild(document.createTextNode(topictypes[i].typename));\r\n                                option.setAttribute('value', topictypes[i].typeid);\r\n                                movselect.appendChild(option);\r\n                            }\r\n                            $('movetopictypelist').style.display = '';\r\n                        }\r\n                        else {\r\n                            $('movetopictypelist').style.display = 'none';\r\n                        }\r\n                    }\r\n                });\r\n            }\r\n            movetoOnchange($('moveto'));\r\n        </");
	templateBuilder.Append("script>\r\n		<!--移动结束-->\r\n		");
	}	//end if


	if (operation=="close")
	{

	templateBuilder.Append("\r\n		<!--关闭开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <ul style=\"margin: 5px 0pt;\" class=\"inlinelist\">\r\n		        <li class=\"wide\"><label><input type=\"radio\" value=\"0\" name=\"close\" class=\"radio\"/> 打开主题</label></li>\r\n            <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"close\" class=\"radio\"/> 关闭主题</label></li>\r\n            <li class=\"wide\"><label><input type=\"radio\" checked=\"checked\" value=\"2\" name=\"close\" class=\"radio\"/> 关闭并下沉主题</label></li>\r\n		    </ul>\r\n		</div>\r\n		<!--关闭结束-->\r\n		");
	}	//end if


	if (operation=="closebonus")
	{

	templateBuilder.Append("\r\n		<!--关闭悬赏开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <ul style=\"margin: 5px 0pt;\" class=\"inlinelist\">\r\n                <li class=\"wide\">无满意答案关闭悬赏，悬赏本金和保证金（为本金的100%）将返还悬赏者，税金（为本金的100%）不返还</li>\r\n		    </ul>\r\n		</div>\r\n		<!--关闭悬赏结束-->\r\n		");
	}	//end if


	if (operation=="banpost")
	{

	templateBuilder.Append("\r\n		<!--屏蔽开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <ul style=\"margin: 5px 0pt;\" class=\"inlinelist\">\r\n		    	<input type=\"hidden\" size=\"10\" name=\"postid\" id=\"postid\" value=\"");
	templateBuilder.Append(postidlist.ToString());
	templateBuilder.Append("\" />\r\n		        <li class=\"wide\"><label><input id=\"banpost1\" type=\"radio\" value=\"0\" name=\"banpost\" class=\"radio\" checked=\"checked\"/> 取消屏蔽</label></li>\r\n                <li class=\"wide\"><label><input id=\"banpost2\" type=\"radio\" value=\"-2\" name=\"banpost\" class=\"radio\"/> 屏蔽帖子</label></li>\r\n		    </ul>\r\n			<script type=\"text/javascript\">\r\n				var status = getQueryString(\"banstatus\");\r\n				if (status == \"0\") {\r\n					$(\"banpost1\").checked = true;\r\n					$(\"banpost2\").checked = false;\r\n				}\r\n				else {\r\n					$(\"banpost2\").checked = true;\r\n					$(\"banpost1\").checked = false;\r\n				}			\r\n			</");
	templateBuilder.Append("script>\r\n		</div>\r\n		<!--屏蔽结束-->\r\n		");
	}	//end if


	if (operation=="bump")
	{

	templateBuilder.Append("\r\n		<!--提升开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <ul style=\"margin: 5px 0pt;\" class=\"inlinelist\">\r\n		        <li class=\"wide\"><label><input id=\"bumptype1\" type=\"radio\" checked=\"checked\" value=\"1\" name=\"bumptype\" class=\"radio\"/>提升</label></li>\r\n                <li class=\"wide\"><label><input id=\"bumptype2\" type=\"radio\" value=\"-1\" name=\"bumptype\" class=\"radio\"/>下沉</label></li>\r\n		    </ul>\r\n		</div>\r\n		<!--提升结束-->\r\n		");
	}	//end if


	if (operation=="copy")
	{

	templateBuilder.Append("\r\n		<!--拷贝开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <p class=\"tah_body tah_fixiesel\">\r\n                <label for=\"copyto\">目标论坛/分类: </label><br />\r\n                <select name=\"copyto\">");
	templateBuilder.Append(forumlist.ToString());
	templateBuilder.Append("</select>\r\n            </p>\r\n        </div>\r\n		<!--拷贝结束-->\r\n		");
	}	//end if


	if (operation=="split")
	{

	templateBuilder.Append("\r\n		<!--分割开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <p class=\"tah_body tah_fixiesel\">\r\n                <label for=\"subject\">新主题的标题:</label> <br />\r\n                <input type=\"text\" name=\"subject\" size=\"32\" value=\"\" class=\"txt\"/>\r\n            </p>\r\n        </div>\r\n        <p>选择内容:        \r\n		    <div style=\"width:218px;height:60px;overflow:hidden;overflow-y:auto;\">\r\n			");
	int post__loop__id=0;
	foreach(DataRow post in postlist.Rows)
	{
		post__loop__id++;

	templateBuilder.Append("<input name=\"postid\" type=\"checkbox\" value=\"" + post["pid"].ToString().Trim() + "\" /><strong>" + post["poster"].ToString().Trim() + "</strong><br />\r\n				" + post["message"].ToString().Trim() + "<br />\r\n			");
	}	//end loop

	templateBuilder.Append("\r\n			</div>\r\n        </p>\r\n		<!--分割结束-->\r\n		");
	}	//end if


	if (operation=="merge")
	{

	templateBuilder.Append("\r\n		<!--合并开始-->\r\n		<div class=\"topicadminlow\">\r\n		    <table cellspacing=\"0\" cellpadding=\"0\" style=\"width:100%\">\r\n		        <tr>\r\n		            <td><label for=\"othertid\">合并 →</label></td>\r\n		            <td>填写目标主题 ID (tid)</td>\r\n		        </tr>\r\n		        <tr>\r\n		            <td></td>\r\n		            <td><input size=\"10\" name=\"othertid\" id=\"othertid\"  class=\"txt\" title=\"");
	if (config.Aspxrewrite==1)
	{

	templateBuilder.Append("即将与这个主题合并的主题id,如showtopic-22.aspx,tid 为 22");
	}
	else
	{

	templateBuilder.Append(">即将与这个主题合并的主题id,<br />如showtopic.aspx?topicid=22,tid 为 22");
	}	//end if

	templateBuilder.Append("\" /></td>\r\n		        </tr>\r\n		    </table>		    \r\n        </div>\r\n		<!--合并结束-->\r\n		");
	}	//end if


	if (operation=="lottery")
	{

	templateBuilder.Append("\r\n		<!--合并开始-->\r\n		<br/>\r\n		<input type=\"hidden\" name=\"optionid\" id=\"optionid\" value=\"");
	templateBuilder.Append(optionid.ToString());
	templateBuilder.Append("\" />\r\n		<div class=\"topicadminlow\">\r\n		    <table cellspacing=\"0\" cellpadding=\"0\" style=\"width:100%\">\r\n		        <tr>\r\n		            <td><label for=\"othertid\">投注数：</label></td>\r\n		            <td>每注上传流量10GB，请填写正整数</td>\r\n		        </tr>\r\n		        <tr>\r\n		            <td></td>\r\n		            <td><input size=\"10\" name=\"wagercount\" id=\"wagercount\" value=\"1\"  class=\"txt\" title=\"每注上传流量10GB，请填写正整数\" /></td>\r\n		        </tr>\r\n		    </table>		    \r\n        </div>\r\n		<!--合并结束-->\r\n		");
	}	//end if


	if (operation=="type")
	{

	templateBuilder.Append("\r\n		<!--分类开始-->\r\n		<div class=\"topicadminlow\">\r\n            <p><label for=\"typeid\">分类:</label> <select id=\"typeid\" name=\"typeid\">");
	templateBuilder.Append(topictypeselectoptions.ToString());
	templateBuilder.Append("</select></p>\r\n        </div>\r\n		<!--分类结束-->\r\n		");
	}	//end if


	if (operation=="rate")
	{

	templateBuilder.Append("\r\n		<!--评分开始-->\r\n		<input type=\"hidden\" name=\"postid\" id=\"postid\" value=\"");
	templateBuilder.Append(postidlist.ToString());
	templateBuilder.Append("\" class=\"txt\" />\r\n		<div class=\"rateopt\">\r\n		");
	if (ismoder==true)
	{

	templateBuilder.Append("流量评分大于100M时发送管理评分通知，否则发送用户评分通知<br/><br/>");
	}	//end if


	int score__loop__id=0;
	foreach(DataRow score in scorelist.Rows)
	{
		score__loop__id++;

	int defaultratevalue = 0;
	
	templateBuilder.Append("\r\n                <div class=\"hasdropdownbtn ratelist s_clear\">\r\n                    <label for=\"score" + score__loop__id.ToString() + "\"> " + score["ScoreName"].ToString().Trim() + "</label>\r\n                    <input type=\"text\" class=\"txt\" value=\"");
	templateBuilder.Append(defaultratevalue.ToString());
	templateBuilder.Append("\" name=\"score\" id=\"score" + score__loop__id.ToString() + "\"/>\r\n                    <input type=\"hidden\" value=\"" + score["ScoreCode"].ToString().Trim() + "\" name=\"extcredits\" />\r\n                    <a onclick=\"InFloat='floatlayout_rate';showselect(this, 'score" + score__loop__id.ToString() + "', 'scoreoption" + score__loop__id.ToString() + "')\" class=\"dropdownbtn\" href=\"javascript:;\">^</a>\r\n                    <ul style=\"display: none;\" id=\"scoreoption" + score__loop__id.ToString() + "\">" + score["options"].ToString().Trim() + "</ul>&nbsp;");
	if (score__loop__id>2)
	{

	templateBuilder.Append("MB");
	}	//end if

	templateBuilder.Append("&nbsp;&nbsp;&nbsp;<span style=\"padding-left:4px;\">(今日剩余 " + score["MaxInDay"].ToString().Trim() + "");
	if (score__loop__id>2)
	{

	templateBuilder.Append("MB");
	}	//end if

	templateBuilder.Append(" " + score["ScoreName"].ToString().Trim() + ")</span>\r\n                </div>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n        </div>\r\n		<!--评分结束-->\r\n		");
	}	//end if


	if (operation=="cancelrate")
	{

	templateBuilder.Append("\r\n		<!--取消评分开始-->\r\n		<input type=\"hidden\" name=\"postid\" id=\"postid\" value=\"");
	templateBuilder.Append(postidlist.ToString());
	templateBuilder.Append("\" class=\"txt\" />\r\n		<div class=\"floatwrap\" style=\"height:280px; margin-bottom:10px;overflow-y:auto;\">\r\n		    <table cellspacing=\"0\" cellpadding=\"0\" class=\"list\" width=\"570\" style=\"table-layout:fixed;\">\r\n		        <thead>\r\n		            <tr>\r\n		                <td style=\"width:30px;\"> </td>\r\n		                <td style=\"width:100px;\">用户名</td>\r\n		                <td style=\"width:120px;\">时间</td>\r\n		                <td>积分</td>\r\n		                <td style=\"width:190px;\">理由</td>\r\n		            </tr>\r\n		        </thead>\r\n		        <tbody>\r\n                    ");
	int rateloginfo__loop__id=0;
	foreach(DataRow rateloginfo in ratelog.Rows)
	{
		rateloginfo__loop__id++;

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td><input name=\"ratelogid\" type=\"checkbox\"  value=\"" + rateloginfo["id"].ToString().Trim() + "\" /></td>\r\n                        <td><a href=\"");
	if (config.Aspxrewrite!=1)
	{

	templateBuilder.Append("userinfo.aspx?userid=" + rateloginfo["uid"].ToString().Trim() + "");
	}
	else
	{

	templateBuilder.Append("userinfo-" + rateloginfo["uid"].ToString().Trim() + ".aspx");
	}	//end if

	templateBuilder.Append("\">" + rateloginfo["username"].ToString().Trim() + "</a></td>\r\n                        <td><span class=\"time\">" + rateloginfo["postdatetime"].ToString().Trim() + "</span></td>\r\n                        <td>" + rateloginfo["extcreditname"].ToString().Trim() + " <span class=\"bold\">");
	if (!rateloginfo["score"].ToString().Trim().StartsWith("-"))
	{

	templateBuilder.Append("+" + rateloginfo["score"].ToString().Trim() + "");
	}
	else
	{

	templateBuilder.Append("" + rateloginfo["score"].ToString().Trim() + "");
	}	//end if

	templateBuilder.Append("</span></td>\r\n                        <td>" + rateloginfo["reason"].ToString().Trim() + "</td>\r\n                    </tr>            \r\n				    ");
	}	//end loop

	templateBuilder.Append("\r\n                </tbody>\r\n            </table>\r\n        </div>\r\n		<!--取消评分结束-->\r\n		");
	}	//end if


	if (operation=="identify")
	{

	templateBuilder.Append("\r\n		<!--鉴定开始-->\r\n		");
	templateBuilder.Append(identifyjsarray.ToString());
	templateBuilder.Append("	\r\n		<div>\r\n            <p>鉴定: \r\n                <select name=\"selectidentify\" id=\"selectidentify\" onchange=\"changeindentify(this.value)\">\r\n				    <option value=\"0\" selected=\"selected\">请选择</option>\r\n				    <option value=\"-1\">* 取消鉴定 *</option>\r\n			    ");
	int identify__loop__id=0;
	foreach(TopicIdentify identify in identifylist)
	{
		identify__loop__id++;

	templateBuilder.Append("  \r\n				    <option value=\"");
	templateBuilder.Append(identify.Identifyid.ToString().Trim());
	templateBuilder.Append("\"");
	if (identify__loop__id==1)
	{

	templateBuilder.Append(" selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">");
	templateBuilder.Append(identify.Name.ToString().Trim());
	templateBuilder.Append("</option>						  \r\n			    ");
	}	//end loop

	templateBuilder.Append("\r\n			    </select>	\r\n            </p>\r\n        </div>\r\n        <p style=\"text-align:center;padding:2px;\"><img id=\"identify_preview\" src=\"");
	templateBuilder.Append(topicidentifydir.ToString());
	templateBuilder.Append("/zc.gif\" onerror=\"changeindentify($('selectidentify').options[2].value)\" /></p>\r\n		<script type=\"text/javascript\">\r\n			function changeindentify(imgid)\r\n			{\r\n				if (imgid != \"0\" && imgid != \"-1\")\r\n				{\r\n				    $(\"identify_preview\").src = \"");
	templateBuilder.Append(topicidentifydir.ToString());
	templateBuilder.Append("/\" + topicidentify[imgid];\r\n					$(\"identify_preview\").style.display = \"\";\r\n				}\r\n				else\r\n				{\r\n					$(\"identify_preview\").style.display = \"none\";\r\n				}\r\n            }\r\n            changeindentify($('selectidentify').options[2].value);\r\n		</");
	templateBuilder.Append("script>	\r\n		<!--鉴定结束-->\r\n		");
	}	//end if


	if (operation=="delete" || operation=="delposts")
	{

	templateBuilder.Append("\r\n		<!--删除帖子开始-->\r\n		<div class=\"topicadminlow\">\r\n             <ul class=\"inlinelist\">\r\n			     <p>您确认要 <strong>删除</strong> 选择的\r\n                  ");
	if (operation=="delposts")
	{

	templateBuilder.Append("\r\n                         <input type=\"hidden\" size=\"10\" name=\"postid\" id=\"Hidden1\" value=\"");
	templateBuilder.Append(postidlist.ToString());
	templateBuilder.Append("\" />\r\n	                     <input type=\"hidden\" size=\"10\" name=\"opinion\" id=\"opinion\" value=\"");
	templateBuilder.Append(opinion.ToString());
	templateBuilder.Append("\" />\r\n                             帖子\r\n                   ");
	}
	else
	{

	templateBuilder.Append("\r\n                            主题\r\n                   ");
	}	//end if

	templateBuilder.Append("么?\r\n                     </p>\r\n		    </ul>\r\n	    </div>\r\n		<!--删除帖子结束-->\r\n		");
	}	//end if


	if (operation!="identify"&&operation!="bonus"&&operation!="cancelrate"&&operation!="lottery")
	{

	templateBuilder.Append("\r\n		<!--操作说明开始-->\r\n		<div class=\"topicadminlog\">\r\n		    <h4>\r\n		        <span class=\"hasdropdownbtn y\"><a href=\"javascript:;\" class=\"dropdownbtn\" onclick=\"showselect(this, 'reason', 'reasonselect')\">^</a></span>\r\n		        操作说明：<label>最多50个字符，还可输入<strong><span id=\"chLeft\">50</span></strong></label>\r\n		    </h4>\r\n		    <p>\r\n			    <textarea onkeyup=\"seditor_ctlent(event, '$(\\'moderateform\\').submit()')\" class=\"txtarea\" name=\"reason\" id=\"reason\" ");
	if (operation=="rate")
	{

	templateBuilder.Append("style=\"width:322px;\"");
	}	//end if

	templateBuilder.Append(">");
	if (operation=="close")
	{


	if (forumid==37)
	{

	templateBuilder.Append("结贴请自行选择主题贴下方“结贴”操作，勿编辑标题");
	}
	else
	{

	templateBuilder.Append("结贴");
	}	//end if


	}	//end if

	templateBuilder.Append("</textarea>\r\n          <script type=\"text/javascript\">checkLength($('reason'), 50); //检查评分内容长度</");
	templateBuilder.Append("script>\r\n		    </p>\r\n		    <ul style=\"display: none;\" id=\"reasonselect\">\r\n			    <li>广告/SPAM</li>\r\n			    <li>恶意灌水</li>\r\n			    <li>违规内容</li>\r\n			    <li>文不对题</li>\r\n			    <li>重复发帖</li>\r\n			    <li></li>\r\n			    <li>我很赞同</li>\r\n			    <li>精品文章</li>\r\n			    <li>原创内容</li>\r\n		    </ul>\r\n	    </div>\r\n		<!--操作说明结束-->\r\n		");
	}	//end if


	if (operation=="bonus")
	{

	templateBuilder.Append("\r\n		<!--结帖开始-->\r\n		<div class=\"bonus\">\r\n			<script type=\"text/javascript\" reload=\"1\">\r\n                jQuery(function ($){\r\n                    $(\".bonuspostmessage\").each(function(){\r\n                        var  message = $(this);\r\n                        if(message.children().length == 0){\r\n                            if(message.html().length > 120){\r\n                                var html = message.html();\r\n                                message.html(html.substr(0,120));\r\n                                var span = $(\"<span />\").html(html.substr(120)).hide();\r\n                                span.appendTo(message);\r\n                                var a = $(\"<a href='javascript:void(0)' title='展开/收起内容' class='xi1'>&gt;&gt;&gt;展开内容</a>\").click(function(){\r\n                                    var a = $(this);\r\n                                    var aprev = a.prev().first();\r\n                                    aprev.css(\"display\",aprev.css(\"display\") == 'none' ? '' : 'none');\r\n                                    a.html(a.html() == \"&gt;&gt;&gt;展开内容\" ? \"&lt;&lt;&lt;收起内容\" : \"&gt;&gt;&gt;展开内容\");\r\n                                });\r\n                                a.appendTo(message);\r\n                            }\r\n                        }\r\n                    });\r\n                    $(\".bonustoolsbar li\").click(function(){\r\n                        var obj = $(this);\r\n                        var pid = obj.attr(\"pid\");\r\n                        if(obj.is(\".valuable\")){\r\n                            if(obj.siblings().first().is(\".checkedbest\")){\r\n                                obj.siblings().first().removeClass(\"checkedbest\");\r\n                                setValuableOrBestAnswer(pid,\"\");\r\n                            }\r\n                            obj.toggleClass(\"checkedvaluable\");\r\n                            setValuableOrBestAnswer(pid,obj.is(\".checkedvaluable\") ? \"valuable\" : \"\");\r\n                            if(obj.is(\".checkedvaluable\")){\r\n                                $(\"#input\" + pid + \" > span\").html(\"有价值种子\");\r\n                                $(\"#input\" + pid).show();\r\n                            }\r\n                        }\r\n                        else{\r\n                            if($('#bestAnswer').val() != \"\" && $('#bestAnswer').val() != obj.attr(\"pid\")){\r\n                                alert(\"已经设置了最佳种子！\");\r\n                                return;\r\n                            }\r\n                            if(obj.siblings().first().is(\".checkedvaluable\")){\r\n                                obj.siblings().first().removeClass(\"checkedvaluable\");\r\n                                setValuableOrBestAnswer(pid,\"\");\r\n                            }\r\n                            obj.toggleClass(\"checkedbest\");\r\n                            setValuableOrBestAnswer(pid,obj.is(\".checkedbest\") ? \"best\" : \"\");\r\n                            if(obj.is(\".checkedbest\")){\r\n                                $(\"#input\" + pid + \" > span\").html(\"最佳种子\");\r\n                                $(\"#input\" + pid).show();\r\n                            }\r\n                        }\r\n\r\n                        var count = $(this).parent().find(\".checkedvaluable,.checkedbest\").length;\r\n                        if(count == 0){\r\n                            $(\"#bonus_\" + pid).val(0);\r\n                            $(\"#input\" + pid).hide();\r\n                        }\r\n\r\n                    });\r\n\r\n                    $(\".floatimg\").each(function(){\r\n                        $(this).mousemove(function(){\r\n                            var position = $(this).position();\r\n                            $(\"#img\" + $(this).attr(\"aid\")).css({position:'absolute',left:position.left,top:position.top + 25}).show();\r\n                        });\r\n                        $(this).mouseout(function(){\r\n                             $(\"#img\" + $(this).attr(\"aid\")).hide();\r\n                        });\r\n                    });\r\n                });\r\n\r\n				var reg = /^\\d+$/i;\r\n				$('moderate').onsubmit = function (){\r\n					if (getCostBonus() != ");
	templateBuilder.Append(topicinfo.Price.ToString().Trim());
	templateBuilder.Append(")\r\n					{\r\n						alert('分配流量总和和与悬赏流量不相符');\r\n						return false;\r\n					}\r\n					return true;\r\n				}\r\n				\r\n				function getCostBonus()\r\n				{\r\n					var bonusboxs = document.getElementsByName('postbonus');\r\n					var costbonus = 0;\r\n					for (var i = 0; i < bonusboxs.length ; i ++ )\r\n					{\r\n						var bonus = isNaN(parseInt(bonusboxs[i].value)) ? 0 : parseInt(bonusboxs[i].value);\r\n						costbonus += bonus;\r\n					}\r\n\r\n					return costbonus;\r\n				}\r\n				function checkInt(obj)\r\n				{				\r\n					if (!reg.test(obj.value))\r\n					{\r\n						obj.value = 0;\r\n					}\r\n				}\r\n				function bonushint(obj)\r\n				{							\r\n					var costbonus = getCostBonus();\r\n					var leftbonus = ");
	templateBuilder.Append(topicinfo.Price.ToString().Trim());
	templateBuilder.Append(" - costbonus;\r\n					$('bonus_menu').innerHTML = '总悬赏流量: ' + ");
	templateBuilder.Append(topicinfo.Price.ToString().Trim());
	templateBuilder.Append(" + 'GB<br />当前可用: ' + leftbonus + 'GB';\r\n                    var position = jQuery(obj).position();\r\n                    $('bonus_menu').style.left = position.left + 'px';\r\n                    $('bonus_menu').style.top = position.top + 25 + 'px';\r\n					$('bonus_menu').style.display = '';\r\n					obj.focus();\r\n				}\r\n\r\n				function closebonushint(obj)\r\n				{\r\n					$('bonus_menu').style.display = 'none';\r\n				}\r\n\r\n				function str_pad(text, length, padstring) {\r\n					text += '';\r\n					padstring += '';\r\n\r\n					if(text.length < length) {\r\n						padtext = padstring;\r\n\r\n						while(padtext.length < (length - text.length)) {\r\n							padtext += padstring;\r\n						}\r\n\r\n						text = padtext.substr(0, (length - text.length)) + text;\r\n					}\r\n\r\n					return text;\r\n				}\r\n				function setValuableOrBestAnswer(pid,choice)\r\n				{\r\n					switch (choice)\r\n					{\r\n						case \"valuable\":\r\n                            if($('valuableAnswers').value == \"\"){\r\n                                $('valuableAnswers').value = pid;\r\n                                return;\r\n                            }\r\n                            var valuableAnswers = $('valuableAnswers').value.split(',');\r\n                            if(in_array(pid,valuableAnswers))\r\n                                break;\r\n                            valuableAnswers[valuableAnswers.length] = pid;\r\n                            $('valuableAnswers').value = valuableAnswers.join(',');\r\n							break;\r\n						case \"best\":\r\n                            if($('bestAnswer').value == \"\"){\r\n                                $('bestAnswer').value = pid;\r\n                                return;\r\n                            }\r\n                            var bestAnswers = $('bestAnswer').value.split(',');\r\n                            if(in_array(pid,bestAnswers))\r\n                                break;\r\n                            bestAnswers[bestAnswers.length] = pid;\r\n                            $('bestAnswer').value = bestAnswers.join(',');\r\n							break;\r\n						default:\r\n                            if(in_array(pid,$('valuableAnswers').value.split(','))){\r\n                                var valuableAnswers = $('valuableAnswers').value.split(',');\r\n                                for(var i = 0; i < valuableAnswers.length; i++){\r\n                                    if(valuableAnswers[i] == pid){\r\n                                        valuableAnswers.splice(i,1);\r\n                                        break;\r\n                                    }\r\n                                }\r\n                                $('valuableAnswers').value = valuableAnswers.join(',');\r\n                            }\r\n                            if(in_array(pid,$('bestAnswer').value.split(','))){\r\n                                var bestAnswers = $('bestAnswer').value.split(',');\r\n                                for(var i = 0; i < bestAnswers.length; i++){\r\n                                    if(bestAnswers[i] == pid){\r\n                                        bestAnswers.splice(i,1);\r\n                                        break;\r\n                                    }\r\n                                }\r\n                                $('bestAnswer').value = bestAnswers.join(',');\r\n                                break;\r\n                            }\r\n					}\r\n				}\r\n			</");
	templateBuilder.Append("script>\r\n			<input type=\"hidden\" id=\"bestAnswer\" name=\"bestAnswer\" value=\"\" />\r\n			<input type=\"hidden\" id=\"valuableAnswers\" name=\"valuableAnswers\" value=\"\" />\r\n			<div class=\"topicadminhigh\">\r\n			    ");
	int post__loop__id=0;
	foreach(DataRow post in postlist.Rows)
	{
		post__loop__id++;

	templateBuilder.Append("\r\n			    <dl>\r\n					<dt>\r\n                    ");
	if (Utils.StrToInt(post["posterid"].ToString().Trim(), 0)!=topicinfo.Posterid)
	{

	templateBuilder.Append("\r\n                    <ul class=\"bonustoolsbar\">\r\n						<li title=\"最佳种子\" class=\"bonusli\" pid=\"" + post["pid"].ToString().Trim() + "\">最佳种子</li>\r\n						<li title=\"有价值种子\" class=\"bonusli valuable\" pid=\"" + post["pid"].ToString().Trim() + "\">有价值种子</li>\r\n                    </ul>\r\n                    ");
	}	//end if


	if (Utils.StrToInt(post["posterid"].ToString().Trim(), 0)!=topicinfo.Posterid)
	{

	templateBuilder.Append(" \r\n			        <cite id=\"input" + post["pid"].ToString().Trim() + "\"><span></span>获得流量GB:\r\n                        <input name=\"postbonus\" id=\"bonus_" + post["pid"].ToString().Trim() + "\" type=\"text\" class=\"txt\" value=\"0\" size=\"1\" maxlength=\"9\" onblur=\"checkInt(this);\" \r\n			            onmouseover=\"bonushint(this);\" onmouseout=\"closebonushint(this);\" />\r\n			            <input name=\"addons\" type=\"hidden\" value=\"" + post["posterid"].ToString().Trim() + "|" + post["pid"].ToString().Trim() + "|" + post["poster"].ToString().Trim() + "\" />\r\n                    </cite>\r\n                    ");
	}	//end if


	if (Utils.StrToInt(post["posterid"].ToString().Trim(), 0)==topicinfo.Posterid)
	{

	templateBuilder.Append("<span class='bonusinfo'>不能给自己加流量</span>");
	}	//end if

	templateBuilder.Append("<strong>" + post["poster"].ToString().Trim() + "</strong>\r\n					</dt>\r\n					<dd>\r\n					 ");	string message = TransAttachImgUbb(post["message"].ToString().Trim());
	
	templateBuilder.Append("\r\n						<span class=\"bonuspostmessage\">");
	templateBuilder.Append(message.ToString());
	templateBuilder.Append("</span>\r\n					</dd>\r\n			    </dl>\r\n			    ");
	}	//end loop

	templateBuilder.Append("\r\n			</div>\r\n			<div id=\"bonus_menu\" class=\"p_pop\" style=\"position:absolute;display:none;padding:5px;\"></div>\r\n		</div>\r\n		<!--结帖结束-->\r\n		");
	}	//end if

	templateBuilder.Append("\r\n		\r\n		<!--短消息通知开始-->\r\n		");
	if (operation=="lottery")
	{

	templateBuilder.Append("\r\n      <p>\r\n        <button type=\"submit\" class=\"pn\" name=\"modsubmit\"><span>确定</span></button>\r\n        <input name=\"sendmessage\" type=\"checkbox\" id=\"sendmessage\" value=\"1\"/>\r\n        <label for=\"sendmessage\">确认选项选择正确</label>\r\n      </p>\r\n		");
	}
	else if (operation!="cancelrate")
	{

	templateBuilder.Append("\r\n      <p>\r\n        <button type=\"submit\" class=\"pn\" name=\"modsubmit\"><span>确定</span></button>\r\n        ");
	if (operation=="delete" || operation=="delposts")
	{

	templateBuilder.Append("\r\n            <!--保留附件开始-->\r\n              <input name=\"reserveattach\" type=\"checkbox\" value=\"1\" /> <label for=\"reserveattach\">保留附件</label>\r\n            <!--保留附件结束-->\r\n        ");
	}	//end if


	if (issendmessage)
	{

	templateBuilder.Append("\r\n            <input type=\"checkbox\" disabled checked=\"checked\"/>\r\n            <input name=\"sendmessage\" type=\"hidden\" id=\"sendmessage\" value=\"1\"/>\r\n		    ");
	}
	else
	{

	templateBuilder.Append("\r\n		        <input name=\"sendmessage\" type=\"checkbox\" id=\"sendmessage\"  checked=\"checked\"  value=\"1\"/>\r\n		    ");
	}	//end if

	templateBuilder.Append(" \r\n		    <label for=\"sendmessage\">通知作者</label>\r\n      </p>\r\n    ");
	}
	else
	{

	templateBuilder.Append("\r\n      <div class=\"topic_msg\">\r\n          <p style=\"float: right;\">\r\n              <input type=\"checkbox\" class=\"checkbox\" id=\"sendmessage\" name=\"sendmessage\"  checked=\"checked\"/> <label for=\"sendreasonpm\">通知作者</label>  \r\n              操作说明: <input class=\"txt\" name=\"reason\"/>\r\n              <button name=\"ratesubmit\" value=\"true\" type=\"submit\" class=\"submit\">提交</button>\r\n          </p>\r\n          <label><input name=\"chkall\" type=\"checkbox\"  onclick=\"checkall(this.form, 'ratelogid')\" /> 全选</label>\r\n      </div>\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n		<!--短消息通知结束-->\r\n	\r\n	</div>\r\n	<!--操作面板结束-->\r\n	\r\n    </form>\r\n	</div>\r\n");
	}
	else
	{


	if (infloat==1)
	{


	if (titlemessage)
	{

	templateBuilder.Append("\r\n            ");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n            <div id=\"floatlayout_mods\">\r\n	            <h3 class=\"flb\">\r\n	                <em id=\"em1\">");
	templateBuilder.Append(operationtitle.ToString());
	templateBuilder.Append("</em>\r\n					<span><a href=\"javascript:;\" class=\"flbc\" onclick=\"hideWindow('mods');if(navigator.userAgent.indexOf('Firefox')>=0) window.location.reload(true);\" title=\"关闭\">关闭</a></span>\r\n	            </h3>\r\n		        <div class=\"c cl\">");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</div>\r\n		    </div>\r\n        ");
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


	}	//end if


	}
	else
	{


	if (infloat==1)
	{

	templateBuilder.Append("\r\n	\r\n        <script type=\"text/javascript\"  reload=\"1\">\r\n            location.href = '");
	templateBuilder.Append(msgbox_url.ToString());
	templateBuilder.Append("';	\r\n			$('return_mods').className='';\r\n        </");
	templateBuilder.Append("script>\r\n	");
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
