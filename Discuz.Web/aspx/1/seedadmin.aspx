<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.seedadmin" %>
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

	templateBuilder.Append("\r\n    <div id=\"floatlayout_mods\">\r\n      <h3 class=\"flb\"> \r\n      <em id=\"return_mods\">");
	templateBuilder.Append(operationtitle.ToString());
	templateBuilder.Append(" [ 同时操作：");
	templateBuilder.Append(alltopiccount.ToString());
	templateBuilder.Append(" ]</em>\r\n      ");
	if (infloat==1)
	{

	templateBuilder.Append("\r\n        <span class=\"y\">\r\n          <a title=\"关闭\" onclick=\"hideWindow('mods');if(navigator.userAgent.indexOf('Firefox')>=0) window.location.reload(true);\" class=\"flbc\" href=\"javascript:;\">关闭</a>\r\n        </span>\r\n      ");
	}	//end if

	templateBuilder.Append("\r\n      </h3>\r\n      <div class=\"c cl\">\r\n      ");
	if (infloat==1)
	{

	templateBuilder.Append("\r\n      <form id=\"moderate_admin\" name=\"moderate_admin\" method=\"post\" onsubmit=\"ajaxpost('moderate_admin', 'return_mods', 'return_mods', 'onerror');return false;\" action=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("seedadmin.aspx?action=moderate&operation=");
	templateBuilder.Append(operation.ToString());
	templateBuilder.Append("&infloat=1\">\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <form id=\"moderate_admin\" name=\"moderate_admin\" method=\"post\" action=\"seedadmin.aspx?action=moderate&operation=");
	templateBuilder.Append(operation.ToString());
	templateBuilder.Append("\">\r\n      ");
	}	//end if

	templateBuilder.Append("\r\n      <input type=\"hidden\" name=\"topicid\" value=\"");
	templateBuilder.Append(topiclist.ToString());
	templateBuilder.Append("\" />\r\n      <input type=\"hidden\" name=\"forumid\" value=\"");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\" />\r\n        ");
	if (config.Aspxrewrite==1)
	{

	templateBuilder.Append("\r\n      <input type=\"hidden\" id=\"referer\" name=\"referer\" value=\"showforum-");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append(config.Extname.ToString().Trim());
	templateBuilder.Append("\" />\r\n        ");
	}
	else
	{

	templateBuilder.Append("	\r\n      <input type=\"hidden\" id=\"referer\" name=\"referer\" value=\"showforum.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\">\r\n        ");
	}	//end if

	templateBuilder.Append("	\r\n        <script type=\"text/javascript\">\r\n          var re = getQueryString(\"referer\");\r\n          if (re != \"\")\r\n          {\r\n            $(\"referer\").value = unescape(re);\r\n          }\r\n        </");
	templateBuilder.Append("script>\r\n        \r\n        ");
	templateBuilder.Append(alltopictitle.ToString());
	templateBuilder.Append("\r\n        \r\n      <!--操作面板开始-->	\r\n        \r\n        ");
	if (operation=="seedratio")
	{

	templateBuilder.Append("\r\n          <div class=\"topicadminlow detailopt\">\r\n              <br/>下载流量系数 蓝种（流量系数为0）操作最长有效期为种子发布起60天\r\n              <ul class=\"inlinelist\">\r\n                  <li><label><input type=\"radio\" value=\"0\" name=\"downratio\" class=\"radio\" />0</label></li>\r\n                  <li><label><input type=\"radio\" value=\"1\" name=\"downratio\" class=\"radio\" />0.3</label></li>\r\n                  <li><label><input type=\"radio\" value=\"2\" name=\"downratio\" class=\"radio\" />0.6</label></li>\r\n                  <li><label><input type=\"radio\" value=\"3\" name=\"downratio\" class=\"radio\" checked=\"checked\"/>1.0</label></li>\r\n                  <li><label><input type=\"radio\" value=\"4\" name=\"downratio\" class=\"radio\" />2.0</label></li>\r\n                  <li><label><input type=\"radio\" value=\"5\" name=\"downratio\" class=\"radio\" />3.0</label></li>\r\n             </ul>\r\n         </div>\r\n         <div class=\"topicadminlow detailopt\">\r\n              <br/>下载流量系数有效期  流量系数为0、0.3的种子在过期之后，4G以上种子仍将保持0.6下载，其余过期后恢复为1\r\n              <br/>有效期选择为（永久/60天）时，蓝种为自发布起60天，0.3系数为设置之日起60天，其余为永久\r\n              <ul class=\"inlinelist\">\r\n                  <li><label><input type=\"radio\" value=\"0\" name=\"downratioexpire\" class=\"radio\" checked=\"checked\"/>永久/60天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"1\" name=\"downratioexpire\" class=\"radio\" />30天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"2\" name=\"downratioexpire\" class=\"radio\" />14天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"3\" name=\"downratioexpire\" class=\"radio\" />7天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"4\" name=\"downratioexpire\" class=\"radio\" />3天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"5\" name=\"downratioexpire\" class=\"radio\" />1天</label></li>\r\n             </ul>\r\n         </div>\r\n         <div class=\"topicadminlow detailopt\">\r\n              <br/>上传流量系数 不推荐设置为大于1的值\r\n              <ul class=\"inlinelist\">\r\n                  <li><label><input type=\"radio\" value=\"0\" name=\"upratio\" class=\"radio\" />0</label></li>\r\n                  <li><label><input type=\"radio\" value=\"1\" name=\"upratio\" class=\"radio\" />0.3</label></li>\r\n                  <li><label><input type=\"radio\" value=\"2\" name=\"upratio\" class=\"radio\" />0.6</label></li>\r\n                  <li><label><input type=\"radio\" value=\"3\" name=\"upratio\" class=\"radio\" checked=\"checked\"/>1.0</label></li>\r\n                  <li><label><input type=\"radio\" value=\"4\" name=\"upratio\" class=\"radio\" />1.2</label></li>\r\n                  <li><label><input type=\"radio\" value=\"5\" name=\"upratio\" class=\"radio\" />1.6</label></li>\r\n             </ul>\r\n          </div>\r\n         <div class=\"topicadminlow detailopt\">\r\n              <br/>上传流量系数有效期 过期后上传系数将恢复为1\r\n              <ul class=\"inlinelist\">\r\n                  <li><label><input type=\"radio\" value=\"0\" name=\"upratioexpire\" class=\"radio\" checked=\"checked\"/>永久</label></li>\r\n                  <li><label><input type=\"radio\" value=\"1\" name=\"upratioexpire\" class=\"radio\" />30天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"2\" name=\"upratioexpire\" class=\"radio\" />14天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"3\" name=\"upratioexpire\" class=\"radio\" />7天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"4\" name=\"upratioexpire\" class=\"radio\" />3天</label></li>\r\n                  <li><label><input type=\"radio\" value=\"5\" name=\"upratioexpire\" class=\"radio\" />1天</label></li>\r\n             </ul>\r\n         </div>\r\n          \r\n        ");
	}	//end if


	if (operation=="seedaward")
	{

	templateBuilder.Append("\r\n         <div class=\"topicadminlow detailopt\">\r\n              <br/>奖励/处罚\r\n              <ul class=\"inlinelist\">\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"0\" name=\"award\" class=\"radio\" />奖励20G（需管理员批准）</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"award\" class=\"radio\" />奖励10G（慎重使用）</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"2\" name=\"award\" class=\"radio\" />奖励5G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"3\" name=\"award\" class=\"radio\" checked=\"checked\"/>奖励2G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"4\" name=\"award\" class=\"radio\" />扣罚2G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"5\" name=\"award\" class=\"radio\" />扣罚5G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"6\" name=\"award\" class=\"radio\" />扣罚10G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"7\" name=\"award\" class=\"radio\" />扣罚20G</label></li>\r\n             </ul>\r\n         </div>\r\n        ");
	}	//end if


	if (operation=="seedmove")
	{

	templateBuilder.Append("\r\n          <div class=\"topicadminlow detailopt\">\r\n              <br/>移动种子到\r\n              <ul class=\"inlinelist\">\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"0\" name=\"moveto\" class=\"radio\" checked=\"checked\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/1.png\"  style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/2.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"2\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/3.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"3\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/4.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"4\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/5.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"5\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/6.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"6\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/7.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"7\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/8.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"8\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/9.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"9\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/10.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"10\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/11.png\" style=\"width:50px\" /></label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"11\" name=\"moveto\" class=\"radio\" /><img class=\"PrivateBTInlineIMGPointer\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/12.png\" style=\"width:50px\" /></label></li>\r\n             </ul>\r\n         </div>\r\n        ");
	}	//end if


	if (operation=="seeddelete")
	{

	templateBuilder.Append("\r\n          <div class=\"topicadminlow detailopt\">\r\n              <br/>删除种子（此选项功能暂不可用，等待完善）\r\n              <ul class=\"inlinelist\">\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"0\" name=\"seeddelete\" class=\"radio\" checked=\"checked\" />重复种子</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"seeddelete\" class=\"radio\" />违规种子</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"2\" name=\"seeddelete\" class=\"radio\" />清理过期种子等</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"3\" name=\"seeddelete\" class=\"radio\" />禁止发布的内容，禁止此种子再次发布</label></li>\r\n             </ul>\r\n         </div>\r\n         <div class=\"topicadminlow detailopt\">\r\n              <br/>奖励/处罚\r\n              <ul class=\"inlinelist\">\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"0\" name=\"award\" class=\"radio\"  checked=\"checked\"/>不进行奖励/处罚</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"award\" class=\"radio\" />扣罚流量2G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"2\" name=\"award\" class=\"radio\" />扣罚流量5G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"3\" name=\"award\" class=\"radio\" />扣罚流量10G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"4\" name=\"award\" class=\"radio\" />扣罚流量20G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"5\" name=\"award\" class=\"radio\" />扣罚流量50G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"6\" name=\"award\" class=\"radio\" />扣罚流量100G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"7\" name=\"award\" class=\"radio\" />扣罚流量200G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"8\" name=\"award\" class=\"radio\" />扣罚流量500G</label></li>\r\n                  <li class=\"wide\"><label><input type=\"radio\" value=\"9\" name=\"award\" class=\"radio\" />扣罚流量1T</label></li>\r\n             </ul>\r\n         </div>\r\n         <br/>\r\n         <span style=\"color:red\">\r\n               请务必包含操作理由:如删种所引用的具体理由，而不仅仅写“违规”、“请仔细查看版规”，因为<br/>\r\n               <strong>理由会通过站内消息发送给所有曾经下载和正在下载、上传的人，而不仅仅是发布者</strong>\r\n         </span><br/>\r\n         <span style=\"color:blue\">\r\n               请确保所引用的版规无差错、无歧义，并且它在版规中是<strong>确实存在的，请随时再次细读版规</strong>\r\n         </span>\r\n        ");
	}	//end if


	if (operation=="seedclose")
	{

	templateBuilder.Append("\r\n        <!--关闭开始-->\r\n        <div class=\"topicadminlow\">\r\n            <br/>\r\n            <ul style=\"margin: 5px 0pt;\" class=\"inlinelist\">\r\n                <li class=\"wide\"><label><input type=\"radio\" checked=\"checked\" value=\"0\" name=\"close\" class=\"radio\"/> 打开主题</label></li>\r\n                    <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"close\" class=\"radio\"/> 关闭主题</label></li>\r\n            </ul>\r\n        </div>\r\n        <!--关闭结束-->\r\n        ");
	}	//end if


	if (operation=="seedban")
	{

	templateBuilder.Append("\r\n        <!--关闭开始-->\r\n        <div class=\"topicadminlow\">\r\n            <br/>\r\n            <ul style=\"margin: 5px 0pt;\" class=\"inlinelist\">\r\n                <li class=\"wide\"><label><input id=\"banpost1\" type=\"radio\" value=\"0\" name=\"banpost\" class=\"radio\" checked=\"checked\"/> 解除屏蔽</label></li>\r\n                <li class=\"wide\"><label><input id=\"banpost2\" type=\"radio\" value=\"-2\" name=\"banpost\" class=\"radio\"/> 屏蔽种子</label></li>\r\n            </ul>\r\n        </div>\r\n        <!--关闭结束-->\r\n        ");
	}	//end if


	if (operation=="seeddigest")
	{

	templateBuilder.Append("\r\n        <!--精华开始-->\r\n        <div class=\"topicadminlow\">\r\n            <br/>\r\n            <ul class=\"inlinelist\">\r\n                    ");
	if (digest>0)
	{

	templateBuilder.Append("<li class=\"wide\"><label><input type=\"radio\" value=\"0\" name=\"level\" class=\"radio\"/> 解除精华</label></li>");
	}	//end if

	templateBuilder.Append("\r\n                    <li class=\"wide\"><label><input type=\"radio\" value=\"1\" name=\"level\" class=\"radio\"");
	if (digest<=1)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 一级精华</label></li>\r\n                    <li class=\"wide\"><label><input type=\"radio\" value=\"2\" name=\"level\" class=\"radio\"");
	if (digest==2)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 二级精华</label></li>\r\n                    <li class=\"wide\"><label><input type=\"radio\" value=\"3\" name=\"level\" class=\"radio\"");
	if (digest==3)
	{

	templateBuilder.Append(" checked=\"checked\"");
	}	//end if

	templateBuilder.Append("/> 三级精华</label></li>\r\n                </ul>\r\n        </div>\r\n        <!--精华结束-->\r\n        ");
	}	//end if


	if (operation=="seeddeleteself")
	{

	templateBuilder.Append("\r\n        <!--自删除开始-->\r\n        <div class=\"topicadminlow\">\r\n            <br/>\r\n            种子发布7天之内可以自行删除种子，<span style=\"color:#C00\">自行删除种子将扣除10G上传</span><br/><br/>\r\n            如果种子发布有问题，您可以编辑替换之前的种子，而不是删除重发。\r\n        </div>\r\n        <!--自删除结束-->\r\n        ");
	}	//end if


	if (operation=="seededit")
	{

	templateBuilder.Append("\r\n        <!--精华开始-->\r\n        <div class=\"topicadminlow\">\r\n            <br/>正在为您跳转到种子编辑页面，请稍等...\r\n              <script type=\"text/javascript\"  reload=\"1\">\r\n                  $('return_mods').className='';\r\n                  location.href = 'edit.aspx?seedid=");
	templateBuilder.Append(lastseedid.ToString());
	templateBuilder.Append("';	\r\n              </");
	templateBuilder.Append("script>\r\n        </div>\r\n        <!--精华结束-->\r\n        ");
	}	//end if


	if (operation!="identify"&&operation!="bonus"&&operation!="cancelrate"&&operation!="seededit")
	{

	templateBuilder.Append("\r\n        <!--操作说明开始-->\r\n        <div class=\"topicadminlog\">\r\n            <h4>\r\n                <span class=\"hasdropdownbtn y\"><a href=\"javascript:;\" class=\"dropdownbtn\" onclick=\"showselect(this, 'reason', 'reasonselect')\">^</a></span>\r\n                <br/>操作说明：<label>最多500个字符，还可输入<strong><span id=\"chLeft\">500</span></strong></label>（<a href=\"modcp.aspx?operation=editforum\">预设操作说明请访问板块编辑页面</a>）\r\n            </h4>\r\n            <p>\r\n              <textarea onkeyup=\"seditor_ctlent(event, '$(\\'moderateform\\').submit()')\" class=\"txtarea\" name=\"reason\" id=\"reason\" ");
	if (operation=="rate")
	{

	templateBuilder.Append("style=\"width:322px;\"");
	}	//end if

	templateBuilder.Append("></textarea>\r\n                    <script type=\"text/javascript\">checkLength($('reason'), 500); //检查评分内容长度</");
	templateBuilder.Append("script>\r\n            </p>\r\n            <ul style=\"display: none;\" id=\"reasonselect\">\r\n            ");
	int opname__loop__id=0;
	foreach(string opname in operationlist)
	{
		opname__loop__id++;

	templateBuilder.Append("\r\n              <li>opname</li>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n              <li>广告/SPAM</li>\r\n              <li>恶意灌水</li>\r\n              <li>违规内容</li>\r\n              <li>文不对题</li>\r\n              <li>重复发帖</li>\r\n              <li></li>\r\n              <li>我很赞同</li>\r\n              <li>精品文章</li>\r\n              <li>原创内容</li>\r\n            </ul>\r\n          </div>\r\n        <!--操作说明结束-->\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n        \r\n        <!--短消息通知开始-->\r\n        ");
	if (operation!="seededit")
	{

	templateBuilder.Append("\r\n        <p>\r\n                <button type=\"submit\" class=\"pn\" name=\"modsubmit\"><span>确定</span></button>\r\n                ");
	if (operation=="delete" || operation=="delposts")
	{

	templateBuilder.Append("\r\n            <!--保留附件开始-->\r\n              <input name=\"reserveattach\" type=\"checkbox\" value=\"1\" /> <label for=\"reserveattach\">保留附件</label>\r\n            <!--保留附件结束-->\r\n            ");
	}	//end if


	if (issendmessage)
	{

	templateBuilder.Append("\r\n                <input type=\"checkbox\" disabled checked=\"checked\"/>\r\n                <input name=\"sendmessage\" type=\"hidden\" id=\"sendmessage\" value=\"1\"/>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n                <input name=\"sendmessage\" type=\"checkbox\" id=\"sendmessage\" value=\"1\" checked=\"checked\" disabled=\"disabled\"/>\r\n            ");
	}	//end if

	templateBuilder.Append(" <label for=\"sendmessage\">通知作者</label>\r\n            </p>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <div class=\"topic_msg\">\r\n                <p style=\"float: right;\">\r\n                    <input type=\"checkbox\" class=\"checkbox\" id=\"sendmessage\" name=\"sendmessage\" checked=\"checked\" disabled=\"disabled\"/> <label for=\"sendreasonpm\">通知作者</label>  \r\n                    操作说明: <input class=\"txt\" name=\"reason\"/>\r\n                    <button name=\"ratesubmit\" value=\"true\" type=\"submit\" class=\"submit\">提交</button>\r\n                </p>\r\n                <label><input name=\"chkall\" type=\"checkbox\"  onclick=\"checkall(this.form, 'ratelogid')\" /> 全选</label>\r\n            </div>\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n        <!--短消息通知结束-->\r\n      \r\n      </div>\r\n      <!--操作面板结束-->\r\n      \r\n        </form>\r\n      </div>\r\n    ");
	}
	else
	{


	if (infloat==1)
	{


	if (titlemessage)
	{

	templateBuilder.Append("\r\n                ");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n                 <em id=\"em1\">");
	templateBuilder.Append(operationtitle.ToString());
	templateBuilder.Append("</em>&nbsp;&nbsp;&nbsp;");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("\r\n            ");
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
