<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.showseeds" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>
<%@ Import namespace="Discuz.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2015/1/7 16:52:16.
		本页面代码由Discuz!NT模板引擎生成于 2015/1/7 16:52:16. 
	*/

	base.OnInit(e);

	templateBuilder.Capacity = 253370;



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




	templateBuilder.Append("<!--弹出信息窗口代码 Newboysyb Studio 2010 Futrue Garden BT System v2.0 Copyright(c)-->\r\n<!--2010.06.16 第二版 -->\r\n              \r\n              <div class=\"PrivateBTInfoFloat\" id=\"PrivateBTInfoFloat\"><div class=\"PrivateBTInfoFloatMid\"><div class=\"PrivateBTInfoFloatMid2\"  id=\"PrivateBTInfoFloatMid2\"></div></div></div>\r\n              <div class=\"PrivateBTInfoFloatMask\" id=\"PrivateBTInfoFloatMask\"></div>\r\n				      <script type=\"text/javascript\">\r\n                  var buaabtoHttpReq;\r\n				          function privatebtshowfloatinfo(showtype, seedid)\r\n                  {\r\n                      $('PrivateBTInfoFloat').style.width = document.body.clientWidth * 0.9 + \"px\";\r\n                      $('PrivateBTInfoFloat').style.marginLeft = \"-\" + document.body.clientWidth * 0.45 + \"px\";  \r\n                      //$('PrivateBTInfoFloat').style.visibility = \"visible\";\r\n                      $('PrivateBTInfoFloatMid2').innerHTML=\"<div class=\\\"PrivateBTListBorder\\\"><div  class=\\\"PrivateBTList\\\"><div class=\\\"float\\\" id=\\\"floatlayout_handlekey\\\"><h3 class=\\\"float_ctrl\\\"><em><img src=\\\"/templates/default/images/loading.gif\\\"> 加载中...</em></h3><br/><br/><br/><br/></div></div></div>\";\r\n                      adjustinfodiv1();\r\n\r\n                      if(showtype == \"publish\") setTimeout(\"privatebtshowfloatinfoloading('\" + showtype + \"', '\" + seedid + \"')\",0);\r\n                      else setTimeout(\"privatebtshowfloatinfoloading('\" + showtype + \"', \" + seedid + \")\",0);\r\n                   }\r\n                   function privatebtshowfloatinfoloading(showtype, seedid)\r\n                   {\r\n                      if(window.ActiveXObject)\r\n                      {\r\n                          buaabtoHttpReq=new ActiveXObject(\"Microsoft.XMLHTTP\");\r\n                      }\r\n                      else if(window.XMLHttpRequest)\r\n                      {\r\n                          buaabtoHttpReq=new XMLHttpRequest();\r\n                      }\r\n                          \r\n                      if(showtype=='peerlist') buaabtoHttpReq.open(\"GET\",\"showpeer.aspx?seedid=\" + seedid,false);\r\n                      else if(showtype=='filelist') buaabtoHttpReq.open(\"GET\",\"showseedfile.aspx?seedid=\" + seedid,true);\r\n                      else if(showtype=='peerhistorylist') buaabtoHttpReq.open(\"GET\",\"showpeerhistory.aspx?seedid=\" + seedid,true);\r\n                      else if(showtype=='seedoplist') buaabtoHttpReq.open(\"GET\",\"showseedop.aspx?seedid=\" + seedid,true);\r\n                      else if(showtype=='seedinfolist') buaabtoHttpReq.open(\"GET\",\"showseedinfo.aspx?seedid=\" + seedid,true);\r\n                      else if(showtype=='publish') buaabtoHttpReq.open(\"GET\",\"publish.aspx?infloat=1&type=\" + seedid,true);\r\n                      else if(showtype=='edit') buaabtoHttpReq.open(\"GET\",\"edit.aspx?infloat=1&seedid=\" + seedid,true);\r\n                      else buaabtoHttpReq.open(\"GET\",\"showseedinfo.aspx?seedid=\" + seedid,true);\r\n                      \r\n                      buaabtoHttpReq.send(null); \r\n                      \r\n                      setTimeout(\"privatebtshowfloatinfodelay()\",500);\r\n                   }\r\n                   \r\n                  function privatebtshowfloatinfodelay()\r\n                  {\r\n                      //alert(\"checking status!\");   \r\n                      if (buaabtoHttpReq.readyState == 4)\r\n                      {\r\n                           //alert(\"Server is done!\");\r\n                           if (buaabtoHttpReq.status == 200)\r\n                           {\r\n                              //alert(\"Server is ok!\");\r\n                              $('PrivateBTInfoFloatMid2').innerHTML = buaabtoHttpReq.responseText;  \r\n                              buaabtoHttpReq = null;\r\n                              window.onresize = adjustinfodiv;\r\n                              document.onkeydown = esckeydown;\r\n                              //window.onscroll = adjustinfodiv;\r\n                              adjustinfodiv();\r\n                              return;\r\n                           }\r\n                      }\r\n                      setTimeout(\"privatebtshowfloatinfodelay()\",500);  \r\n                  }\r\n                  function esckeydown(event)\r\n                  {\r\n                      event = event || window.event; \r\n                      if(event.keyCode==27)\r\n                      {\r\n                          event.returnValue = null;\r\n                          window.returnValue = null;\r\n                          hidedetailinfo()\r\n                          event.returnvalue = false;\r\n                          return;\r\n                      }\r\n                      event.returnvalue = true;\r\n                  }\r\n                  function adjustinfodiv1()\r\n                  {\r\n                      var clientWidth = document.body.clientWidth;\r\n                      var clientHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight;\r\n                      var scrollTop = document.body.scrollTop ? document.body.scrollTop : document.documentElement.scrollTop;\r\n                      \r\n                      $('PrivateBTInfoFloat').style.width = (document.body.clientWidth * 0.9) + \"px\";                    \r\n                      $('PrivateBTInfoFloat').style.marginLeft = \"-\" + (document.body.clientWidth * 0.45) + \"px\"; \r\n                      $('PrivateBTInfoFloatMask').style.marginLeft = \"-\" + (document.body.clientWidth * 0.45 + 7) + \"px\";\r\n\r\n                      if($('PrivateBTInfoFloat').offsetHeight / 2 - document.documentElement.scrollTop < 0)\r\n                      {\r\n                          $('PrivateBTInfoFloat').style.top = ((clientHeight - $('PrivateBTInfoFloat').offsetHeight) / 2 + scrollTop) + 'px';\r\n                          $('PrivateBTInfoFloatMask').style.top = ((clientHeight - $('PrivateBTInfoFloat').offsetHeight) / 2 + scrollTop - 6) + 'px';\r\n                      }\r\n                      else\r\n                      {\r\n                          $('PrivateBTInfoFloat').style.top = ((clientHeight - $('PrivateBTInfoFloat').offsetHeight) / 2 + scrollTop) + 'px';\r\n                          $('PrivateBTInfoFloatMask').style.top = ((clientHeight - $('PrivateBTInfoFloat').offsetHeight) / 2 + scrollTop - 6) + 'px';                      }\r\n                      //$('PrivateBTInfoFloatMask').style.marginTop = ($('PrivateBTInfoFloat').style.marginTop -6) + \"px\"\r\n                      $('PrivateBTInfoFloatMask').style.width = ($('PrivateBTInfoFloat').clientWidth + 14) + \"px\";\r\n                      $('PrivateBTInfoFloatMask').style.height = ($('PrivateBTInfoFloat').clientHeight + 12) + \"px\";\r\n                      $('PrivateBTInfoFloat').style.visibility = \"visible\";\r\n                      $('PrivateBTInfoFloatMask').style.visibility = \"visible\";\r\n                      \r\n                      //$('PrivateBTInfoFloat').style.height = \"200px\";\r\n                        \r\n                  }\r\n                  function adjustinfodiv()\r\n                  {\r\n                      var clientWidth = document.body.clientWidth;\r\n                      var clientHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight;\r\n                      var scrollTop = document.body.scrollTop ? document.body.scrollTop : document.documentElement.scrollTop;\r\n                    \r\n                      $('PrivateBTInfoFloat').style.width = (clientWidth * 0.9) + \"px\";                    \r\n                      $('PrivateBTInfoFloat').style.marginLeft = \"-\" + (clientWidth * 0.45) + \"px\"; \r\n                      $('PrivateBTInfoFloatMask').style.marginLeft = \"-\" + (clientWidth * 0.45 + 7) + \"px\";\r\n                      \r\n                      if($('PrivateBTInfoListBorder2').offsetHeight  > document.body.parentNode.clientHeight * 0.8 - 80)\r\n                      {\r\n                          if((document.body.parentNode.clientHeight * 0.8 - 90) > 50)\r\n                          {\r\n                              $('PrivateBTInfoListBorder').style.height = (document.body.parentNode.clientHeight * 0.8 - 80) + \"px\";\r\n                              //$('PrivateBTInfoFloatMask').style.height = (document.body.parentNode.clientHeight * 0.8 - 80 + 12) + \"px\";\r\n                          }\r\n                          else\r\n                          {\r\n                              $('PrivateBTInfoListBorder').style.height = \"50px\";\r\n                              //$('PrivateBTInfoFloatMask').style.height = \"62px\";\r\n                          }\r\n                      }\r\n                      else\r\n                      {\r\n                          //if($('PrivateBTInfoListBorder').style.height != \"\")\r\n                          $('PrivateBTInfoListBorder').style.height = \"auto\";\r\n                          \r\n                      }\r\n                      if($('PrivateBTInfoFloat').offsetHeight / 2 - scrollTop < 0)\r\n                      {\r\n                          //$('PrivateBTInfoFloat').style.top = (scrollTop - $('PrivateBTInfoFloat').offsetHeight / 2) + \"px\"\r\n                          //$('PrivateBTInfoFloatMask').style.top = (scrollTop - $('PrivateBTInfoFloat').offsetHeight / 2 - 6) + \"px\"\r\n                          $('PrivateBTInfoFloat').style.top = ((clientHeight - $('PrivateBTInfoFloat').offsetHeight) / 2 + scrollTop) + 'px';\r\n                          $('PrivateBTInfoFloatMask').style.top = ((clientHeight - $('PrivateBTInfoFloat').offsetHeight) / 2 + scrollTop - 6) + 'px';\r\n                      }\r\n                      else\r\n                      {\r\n                          //$('PrivateBTInfoFloat').style.top = \"-\" + ($('PrivateBTInfoFloat').offsetHeight / 2 - scrollTop) + \"px\";\r\n                          //$('PrivateBTInfoFloatMask').style.top = \"-\" + ($('PrivateBTInfoFloat').offsetHeight / 2 - scrollTop + 6) + \"px\";\r\n                          $('PrivateBTInfoFloat').style.top = ((clientHeight - $('PrivateBTInfoFloat').offsetHeight) / 2 + scrollTop) + 'px';\r\n                          $('PrivateBTInfoFloatMask').style.top = ((clientHeight - $('PrivateBTInfoFloat').offsetHeight) / 2 + scrollTop - 6) + 'px';\r\n                      }\r\n                      //$('PrivateBTInfoFloatMask').style.marginTop = ($('PrivateBTInfoFloat').style.marginTop -6) + \"px\"\r\n                      $('PrivateBTInfoFloatMask').style.width = ($('PrivateBTInfoFloat').clientWidth + 14) + \"px\";\r\n                      $('PrivateBTInfoFloatMask').style.height = ($('PrivateBTInfoFloat').clientHeight + 12) + \"px\";\r\n                      $('PrivateBTInfoFloat').style.visibility = \"visible\";\r\n                      $('PrivateBTInfoFloatMask').style.visibility = \"visible\";\r\n                      \r\n                      $('PrivateBTInfoListBorder2').style.height = ($('PrivateBTInfoFloat').clientHeight - 157) + \"px\";\r\n                      \r\n                  }\r\n                  function hidedetailinfo()\r\n                  {\r\n                    $('PrivateBTInfoFloat').style.visibility = \"hidden\";\r\n                    $('PrivateBTInfoFloatMask').style.visibility = \"hidden\";\r\n                    $('PrivateBTInfoFloatMid2').innerHTML = \"\";\r\n                    //window.onresize=\"\";\r\n                    //document.onkeydown=\"\";\r\n                    window.onresize=null;\r\n                    document.onkeydown=null;\r\n                    window.onscroll=null;\r\n                  }\r\n\r\n              </");
	templateBuilder.Append("script>\r\n<!--End弹出信息窗口代码 Newboysyb Studio 2009 FGBT v1.0-->");


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

	templateBuilder.Append("\r\n		<a id=\"forumlist\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("\" ");
	if (config.Forumjump==1)
	{

	templateBuilder.Append("onmouseover=\"showMenu(this.id);\" onmouseout=\"showMenu(this.id);\"");
	}	//end if

	templateBuilder.Append(" class=\"title\">");
	templateBuilder.Append(config.Forumtitle.ToString().Trim());
	templateBuilder.Append("</a> &raquo; ");
	templateBuilder.Append(forumnav.ToString());
	templateBuilder.Append("\r\n	</div>\r\n</div>\r\n<script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/ajax.js\"></");
	templateBuilder.Append("script>\r\n");
	if (page_err==0)
	{

	templateBuilder.Append("\r\n	<script type=\"text/javascript\">\r\n	var templatepath = \"");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("\";\r\n    var imagedir = \"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("\";\r\n	var fid = parseInt(");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append(");\r\n	var postminchars = parseInt(");
	templateBuilder.Append(config.Minpostsize.ToString().Trim());
	templateBuilder.Append(");\r\n	var postmaxchars = parseInt(");
	templateBuilder.Append(config.Maxpostsize.ToString().Trim());
	templateBuilder.Append(");\r\n	var disablepostctrl = parseInt(");
	templateBuilder.Append(disablepostctrl.ToString());
	templateBuilder.Append(");\r\n	var forumurl = forumpath = \"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("\";\r\n	</");
	templateBuilder.Append("script>\r\n");
	}	//end if

	templateBuilder.Append("\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_showforum.js\"></");
	templateBuilder.Append("script>\r\n");
	if (page_err==0)
	{

	templateBuilder.Append("\r\n<div class=\"wrap cl\">\r\n");

	if (pagewordad.Length>0)
	{

	templateBuilder.Append("\r\n<div id=\"ptnotice_text\" class=\"ptnotice_text sclear\">\r\n	<table cellspacing=\"1\" cellpadding=\"0\" width=\"100%\" summary=\"text ptnotice\">\r\n	<tbody>\r\n		<tr>\r\n		");	int adindex = 0;
	

	int pageword__loop__id=0;
	foreach(string pageword in pagewordad)
	{
		pageword__loop__id++;


	if (adindex<4)
	{

	templateBuilder.Append("\r\n			<td>");
	templateBuilder.Append(pageword.ToString());
	templateBuilder.Append("</td>\r\n				");	 adindex = adindex+1;
	

	}
	else
	{

	templateBuilder.Append("\r\n		</tr><tr>\r\n			<td>");
	templateBuilder.Append(pageword.ToString());
	templateBuilder.Append("</td>\r\n				");	 adindex = 1;
	

	}	//end if


	}	//end loop


	if (pagewordad.Length%4>0)
	{


					for (int j = 0; j < (4 - pagewordad.Length % 4); j++)
					{
				
	templateBuilder.Append("\r\n			<td>&nbsp;</td>\r\n			");
					}
				

	}	//end if

	templateBuilder.Append("\r\n		</tr>\r\n	</tbody>\r\n	</table>\r\n</div>\r\n");
	}	//end if


	if (pagead.Count>0)
	{


	int pageadtext__loop__id=0;
	foreach(string pageadtext in pagead)
	{
		pageadtext__loop__id++;

	templateBuilder.Append("\r\n        <div class=\"ptnotice_text sclear\">\r\n            ");
	templateBuilder.Append(pageadtext.ToString());
	templateBuilder.Append("\r\n        </div>\r\n    ");
	}	//end loop


	}	//end if




	if (showforumlogin==1)
	{

	templateBuilder.Append("\r\n	<div class=\"main\">\r\n		<h3>本版块已经被管理员设置了密码</h3>\r\n		<form id=\"forumlogin\" name=\"forumlogin\" method=\"post\" action=\"\">\r\n		<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"formtable\">\r\n		<tbody>\r\n		<tr>\r\n			<th><label for=\"forumpassword\">请输入密码</label></th>\r\n			<td><input name=\"forumpassword\" type=\"password\" id=\"forumpassword\" size=\"20\" class=\"txt\"/></td>\r\n		</tr>\r\n		</tbody>\r\n		");
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

	templateBuilder.Append("\r\n<div id=\"forumheader\" class=\"main cl\">\r\n	<span class=\"y o\">\r\n        ");
	if (forum.Rules!="")
	{

	templateBuilder.Append("\r\n        <img id=\"rules_img\"  src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/collapsed_no.gif\" alt=\"展开/收起\" onclick=\"toggle_collapse('rules');\"/>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n    </span>\r\n	");
	if (page_err==0)
	{

	templateBuilder.Append("\r\n	<span class=\"y\">\r\n	");
	if (ismoder)
	{

	templateBuilder.Append("<a href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("modcp.aspx?operation=attention&forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\" target=\"_blank\" class=\"f_bold\">管理面板</a>");
	}	//end if

	templateBuilder.Append("\r\n	</span>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	<div class=\"forumaction y\">\r\n		<!--<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showforum.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&filter=digest\" class=\"digest\">精华</a>-->\r\n		");
	if (config.Rssstatus!=0&&forum.Allowrss!=0)
	{

	 aspxrewriteurl = this.RssAspxRewrite(forum.Fid);
	
	templateBuilder.Append("	\r\n		<a class=\"feed\" target=\"_blank\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("tools/");
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">RSS</a>	\r\n		");
	}	//end if

	templateBuilder.Append("\r\n	</div>\r\n	<h1>");	 aspxrewriteurl = this.ShowForumAspxRewrite(forum.Fid,0);
	templateBuilder.Append(forum.Name.ToString().Trim());
	templateBuilder.Append("</h1>\r\n	<span class=\"forumstats\">今日: <strong class=\"xi1\">");
	templateBuilder.Append(forum.Todayposts.ToString().Trim());
	templateBuilder.Append("</strong><span class=\"pipe\">|</span>主题: <strong class=\"xi1\">");
	templateBuilder.Append(topiccount.ToString());
	templateBuilder.Append("</strong><span class=\"pipe\">|</span>帖子: <strong class=\"xi1\">");
	templateBuilder.Append(forum.Posts.ToString().Trim());
	templateBuilder.Append("</strong></span>\r\n	<p id=\"modedby\">\r\n");
	if (page_err==0)
	{

	templateBuilder.Append("版主: <span class=\"f_c\">\r\n	");
	if (forum.Moderators!="")
	{

	templateBuilder.Append("\r\n		");
	templateBuilder.Append(forum.Moderators.ToString().Trim());
	templateBuilder.Append("\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n		*空缺中*\r\n	");
	}	//end if

	templateBuilder.Append("</span>\r\n");
	}	//end if

	templateBuilder.Append("\r\n	</p>\r\n");
	if (forum.Rules!="")
	{

	templateBuilder.Append("\r\n	<div id=\"rules\">");
	templateBuilder.Append(forum.Rules.ToString().Trim());
	templateBuilder.Append("</div>\r\n");
	}	//end if

	templateBuilder.Append("\r\n</div>\r\n");
	if (forum.Subforumcount>0&&subforumlist.Count>0)
	{


	templateBuilder.Append("<div id=\"subforum\" class=\"main cl list\">\r\n	<div class=\"titlebar xg2\">\r\n		<span class=\"y\">\r\n		");
	if (forum.Moderators!="")
	{

	templateBuilder.Append("\r\n			分类版主: ");
	templateBuilder.Append(forum.Moderators.ToString().Trim());
	templateBuilder.Append("\r\n		");
	}	//end if

	templateBuilder.Append("\r\n		<img id=\"category_");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("_img\"  src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/collapsed_no.gif\" alt=\"展开/收起\" onclick=\"toggle_collapse('category_");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("');\" class=\"cursor\"/>\r\n		</span>\r\n		<h2>子版块</h2>\r\n	</div>\r\n	<div id=\"category_");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("\"  class=\"fi\" summary=\"category_");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("\">\r\n	<table cellspacing=\"0\" cellpadding=\"0\">\r\n	<tbody>	\r\n	");
	if (forum.Colcount==1)
	{


	int subforum__loop__id=0;
	foreach(IndexPageForumInfo subforum in subforumlist)
	{
		subforum__loop__id++;

	templateBuilder.Append("\r\n			<tr>\r\n				");	 aspxrewriteurl = this.ShowForumAspxRewrite(subforum.Fid,0);
	
	templateBuilder.Append("\r\n				<th ");
	if (config.Shownewposticon==1)
	{

	templateBuilder.Append("class=\"notopic ");
	if (subforum.Havenew=="new")
	{

	templateBuilder.Append("new");
	}	//end if

	templateBuilder.Append("\"");
	}	//end if

	templateBuilder.Append(">\r\n					<h2>						\r\n						");
	if (subforum.Redirect=="")
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(subforum.Fid,0,subforum.Rewritename);
	
	templateBuilder.Append("\r\n							<a href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">\r\n						");
	}
	else
	{

	templateBuilder.Append("\r\n							<a href=\"");
	templateBuilder.Append(subforum.Redirect.ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\">\r\n						");
	}	//end if


	if (subforum.Icon!="")
	{

	templateBuilder.Append("\r\n							<img src=\"");
	templateBuilder.Append(subforum.Icon.ToString().Trim());
	templateBuilder.Append("\" border=\"0\" align=\"left\" hspace=\"5\" alt=\"");
	templateBuilder.Append(subforum.Name.ToString().Trim());
	templateBuilder.Append("\"/>\r\n						");
	}	//end if

	templateBuilder.Append("\r\n						");
	templateBuilder.Append(subforum.Name.ToString().Trim());
	templateBuilder.Append("</a>");
	if (subforum.Todayposts>0)
	{

	templateBuilder.Append("<span class=\"today\">(今日:<strong>");
	templateBuilder.Append(subforum.Todayposts.ToString().Trim());
	templateBuilder.Append("</strong>)</span>");
	}	//end if

	templateBuilder.Append("\r\n					</h2>\r\n					");
	if (subforum.Description!="")
	{

	templateBuilder.Append("<p>");
	templateBuilder.Append(subforum.Description.ToString().Trim());
	templateBuilder.Append("</p>");
	}	//end if


	if (subforum.Moderators!="")
	{

	templateBuilder.Append("<p class=\"moderators\">版主:");
	templateBuilder.Append(subforum.Moderators.ToString().Trim());
	templateBuilder.Append("</p>");
	}	//end if

	templateBuilder.Append("\r\n				</th>\r\n				<td class=\"nums\"><em>");
	templateBuilder.Append(subforum.Topics.ToString().Trim());
	templateBuilder.Append("</em> / ");
	templateBuilder.Append(subforum.Posts.ToString().Trim());
	templateBuilder.Append("</td>\r\n				<td class=\"lastpost\">\r\n					");
	if (subforum.Status==-1)
	{

	templateBuilder.Append("\r\n						<p>私密论坛</p>\r\n					");
	}
	else
	{


	if (subforum.Lasttid!=0)
	{

	templateBuilder.Append("\r\n						<p>\r\n							");	 aspxrewriteurl = this.ShowTopicAspxRewrite(subforum.Lasttid,0);
	
	templateBuilder.Append("\r\n							<a href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">");	templateBuilder.Append(Utils.GetUnicodeSubString(subforum.Lasttitle,35,"..."));
	templateBuilder.Append("</a>\r\n						</p>\r\n						<div class=\"topicbackwriter\">by\r\n							");
	if (subforum.Lastposter!="")
	{


	if (subforum.Lastposterid==-1)
	{

	templateBuilder.Append("\r\n									游客\r\n								");
	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(subforum.Lastposterid);
	
	templateBuilder.Append("\r\n									<a href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\" target=\"_blank\">");
	templateBuilder.Append(subforum.Lastposter.ToString().Trim());
	templateBuilder.Append("</a>\r\n								");
	}	//end if


	}
	else
	{

	templateBuilder.Append("\r\n								匿名\r\n							");
	}	//end if

	 aspxrewriteurl = this.ShowTopicAspxRewrite(subforum.Lasttid,0);
	
	string sublastdatetime = ForumUtils.ConvertDateTime(subforum.Lastpost);
	
	templateBuilder.Append("\r\n						- 	<a href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("showtopic.aspx?topicid=");
	templateBuilder.Append(subforum.Lasttid.ToString().Trim());
	templateBuilder.Append("&page=end#lastpost\" title=\"");
	templateBuilder.Append(subforum.Lasttitle.ToString().Trim());
	templateBuilder.Append("\"><span>");
	templateBuilder.Append(sublastdatetime.ToString());
	templateBuilder.Append("</span></a>\r\n						</div>\r\n						");
	}
	else
	{

	templateBuilder.Append("\r\n							从未\r\n						");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n				</td>\r\n			  </tr>\r\n		");
	}	//end loop


	}
	else
	{

	int subforumindex = 0;
	
	double colwidth = 99.6 / forum.Colcount;
	

	int subforum__loop__id=0;
	foreach(IndexPageForumInfo subforum in subforumlist)
	{
		subforum__loop__id++;

	 subforumindex = subforumindex+1;
	

	if (subforumindex==1)
	{

	templateBuilder.Append("\r\n			<tr>\r\n		");
	}	//end if

	templateBuilder.Append("\r\n				<th style=\"width:");
	templateBuilder.Append(colwidth.ToString());
	templateBuilder.Append("%;\" ");
	if (config.Shownewposticon==1)
	{

	templateBuilder.Append("class=\"notopic ");
	if (subforum.Havenew=="new")
	{

	templateBuilder.Append("new");
	}	//end if

	templateBuilder.Append("\"");
	}	//end if

	templateBuilder.Append(">\r\n				<h2>				\r\n				");
	if (forum.Redirect=="")
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(subforum.Fid,0,subforum.Rewritename);
	
	templateBuilder.Append("\r\n					<a href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\">\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n					<a href=\"");
	templateBuilder.Append(subforum.Redirect.ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\">\r\n				");
	}	//end if


	if (subforum.Icon!="")
	{

	templateBuilder.Append("\r\n					<img src=\"");
	templateBuilder.Append(subforum.Icon.ToString().Trim());
	templateBuilder.Append("\" alt=\"");
	templateBuilder.Append(subforum.Name.ToString().Trim());
	templateBuilder.Append("\" hspace=\"5\" />\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				");
	templateBuilder.Append(subforum.Name.ToString().Trim());
	templateBuilder.Append("</a>\r\n				");
	if (subforum.Todayposts>0)
	{

	templateBuilder.Append("\r\n				<span class=\"time\">(今日:<strong>");
	templateBuilder.Append(subforum.Todayposts.ToString().Trim());
	templateBuilder.Append("</strong>)</span>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				</h2>\r\n				<p>主题:");
	templateBuilder.Append(subforum.Topics.ToString().Trim());
	templateBuilder.Append(", 帖数:");
	templateBuilder.Append(subforum.Posts.ToString().Trim());
	templateBuilder.Append("</p>\r\n				");
	if (subforum.Status==-1)
	{

	templateBuilder.Append("\r\n				<p>私密版块</p>\r\n				");
	}
	else
	{


	if (subforum.Lasttid!=0)
	{

	string sublastdatetime = ForumUtils.ConvertDateTime(subforum.Lastpost);
	
	templateBuilder.Append("\r\n						<p>最后: <a href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("showtopic.aspx?topicid=");
	templateBuilder.Append(subforum.Lasttid.ToString().Trim());
	templateBuilder.Append("&page=end#lastpost\" title=\"");
	templateBuilder.Append(subforum.Lasttitle.ToString().Trim());
	templateBuilder.Append("\"><span>");
	templateBuilder.Append(sublastdatetime.ToString());
	templateBuilder.Append("</span></a> by \r\n							");
	if (subforum.Lastposter!="")
	{


	if (subforum.Lastposterid==-1)
	{

	templateBuilder.Append("\r\n									游客\r\n								");
	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(subforum.Lastposterid);
	
	templateBuilder.Append("\r\n									<a href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\" target=\"_blank\">");
	templateBuilder.Append(subforum.Lastposter.ToString().Trim());
	templateBuilder.Append("</a>\r\n								");
	}	//end if


	}
	else
	{

	templateBuilder.Append("\r\n								匿名\r\n							");
	}	//end if

	templateBuilder.Append("\r\n						</p>\r\n					");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n				 </th>\r\n		");
	if (subforumindex==forum.Colcount)
	{

	templateBuilder.Append("\r\n			</tr>\r\n			");	 subforumindex = 0;
	

	}	//end if


	}	//end loop


	if (subforumindex!=0)
	{

	for (int i = 0; i < forum.Colcount-subforumindex; i++)
	{
		templateBuilder.Append("<td>&nbsp;</td>");
	}

	templateBuilder.Append("\r\n			</tr>\r\n		");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n	</tbody>\r\n	</table>\r\n	</div>\r\n</div>");


	}	//end if


	templateBuilder.Append("<form method=\"post\" action=\"javascript:PrivateBTSearchSubmit()\">\r\n<div class=\"PrivateBTSeedSearchForm\" id = \"PrivateBTSeedSearchForm\"><span>\r\n		<input id=\"PrivateBTSeedSearchOrderBy\" type=\"hidden\" name=\"orderby\" value=\"");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("\"/>\r\n		<input id=\"PrivateBTSeedSearchAsc\" type=\"hidden\" name=\"asc\" value=\"");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("\"/>\r\n		<div class=\"PrivateBTseedsearchdiv\" id=\"PrivateBTseedsearchdiv\">	\r\n						<div class=\"PrivateBTseedsearchdiv1\">\r\n						<span class=\"PrivateBTseedsearchText\">按名称搜索种子</span>\r\n						<input id=\"PrivateBTSeedSearchhidekeywords\" name=\"asc\" value=\"");
	templateBuilder.Append(keywords.ToString());
	templateBuilder.Append(" \" onkeypress=\"if(event.keyCode==13) {PrivateBTSearchSubmit();return false;}\"/>\r\n						<div class=\"select_typeid\">\r\n              <select name=\"PrivateBTSeedSearchSelectType\" class=\"PrivateBTSeedSearchSelect\" id=\"PrivateBTSeedSearchSelectType\">\r\n                  <option value=\"0\" ");
	if (seedtype==0)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">种子分类</option>\r\n                  <option value=\"1\" ");
	if (seedtype==1)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">电影</option>\r\n                  <option value=\"2\" ");
	if (seedtype==2)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">剧集</option>\r\n                  <option value=\"3\" ");
	if (seedtype==3)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">动漫</option>\r\n                  <option value=\"4\" ");
	if (seedtype==4)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">音乐</option>\r\n                  <option value=\"5\" ");
	if (seedtype==5)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">游戏</option>\r\n                  <option value=\"6\" ");
	if (seedtype==6)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">纪录</option>\r\n                  <option value=\"7\" ");
	if (seedtype==7)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">体育</option>\r\n                  <option value=\"8\" ");
	if (seedtype==8)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">综艺</option>\r\n                  <option value=\"9\" ");
	if (seedtype==9)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">软件</option>\r\n                  <option value=\"10\" ");
	if (seedtype==10)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">学习</option>\r\n                  <option value=\"11\" ");
	if (seedtype==11)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">视频</option>\r\n                  <option value=\"12\" ");
	if (seedtype==12)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">其他</option>\r\n              </select>\r\n              <script type=\"text/javascript\">loadselect(\"PrivateBTSeedSearchSelectType\");</");
	templateBuilder.Append("script>\r\n            </div>\r\n            <div class=\"select_typeid\">\r\n              <select name=\"PrivateBTSeedSearchSelectStat\" class=\"PrivateBTSeedSearchSelect\" id=\"PrivateBTSeedSearchSelectStat\">\r\n                  <option value=\"0\" ");
	if (seedstat==0)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">正常种子</option>\r\n                  <option value=\"1\" ");
	if (seedstat==1)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">在线种子</option>\r\n                  <option value=\"3\" ");
	if (seedstat==3)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">IPv6在线</option>\r\n                  <option value=\"4\" ");
	if (seedstat==4)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">离线种子</option>\r\n                  <option value=\"5\" ");
	if (seedstat==5)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">蓝色种子</option>\r\n                  <option value=\"6\" ");
	if (seedstat==6)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">在线蓝种</option>\r\n                  <option value=\"7\" ");
	if (seedstat==7)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">IPv6蓝种</option>\r\n                  <option value=\"8\" ");
	if (seedstat==8)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">包含死种</option>\r\n                  <option value=\"9\" ");
	if (seedstat==9)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">只看死种</option>\r\n              </select>\r\n              <script type=\"text/javascript\">loadselect(\"PrivateBTSeedSearchSelectStat\");</");
	templateBuilder.Append("script>\r\n            </div>\r\n            </div>\r\n            <br/>\r\n            \r\n            \r\n            <div class=\"PrivateBTseedsearchdiv2\" id=\"divnotinkeyword\">\r\n              <span class=\"PrivateBTseedsearchText\">种子标题中排除</span>\r\n              <input id=\"PrivateBTSeedSearchhidenotinkeywords\" name=\"asc\" value=\"");
	templateBuilder.Append(notinkeywords.ToString());
	templateBuilder.Append(" \" onkeypress=\"if(event.keyCode==13) {PrivateBTSearchSubmit();return false;}\"/>\r\n              <br/>\r\n            </div>\r\n            \r\n            \r\n            <div class=\"PrivateBTseedsearchdiv2\">\r\n						<span class=\"PrivateBTseedsearchText\">按用户搜索种子</span><input id=\"PrivateBTSeedSearchhideusername\" onblur=\"PrivateBTSearchUserStatChange()\" name=\"asc\" value=\"");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("\"  onkeypress=\"if(event.keyCode==13) {PrivateBTSearchSubmit();return false;}\"/>\r\n            <span id=\"userstatreplace\">\r\n              <div class=\"select_typeid\">\r\n                <select name=\"PrivateBTSeedSearchSelectuser\" class=\"PrivateBTSeedSearchSelect\" id=\"PrivateBTSeedSearchSelectuser\">\r\n                    <option value=\"1\" ");
	if (userstat==1)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">上传中</option>\r\n                    <option value=\"2\" ");
	if (userstat==2)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">下载中</option>\r\n                    <option value=\"3\" ");
	if (userstat==3)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">发布的</option>\r\n                    ");
	if (searchusername==username)
	{

	templateBuilder.Append("<option value=\"4\" ");
	if (userstat==4)
	{

	templateBuilder.Append("selected=\"selected\"");
	}	//end if

	templateBuilder.Append(">完成的</option>");
	}	//end if

	templateBuilder.Append("\r\n                </select>\r\n                <script type=\"text/javascript\">loadselect(\"PrivateBTSeedSearchSelectuser\");</");
	templateBuilder.Append("script>\r\n              </div>\r\n            </span>\r\n            </div>\r\n            <div id=\"divnotinkeywordhit\" style=\"display:none;\"><a onclick=\"shownotin();\">排除</a></div>\r\n            <div class=\"PrivateBTseedsearchdiv3\">\r\n						<button class=\"PrivateBTSeedSearchButton\" onclick=\"PrivateBTSearchSubmit()\"/> 搜&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;索 </button>\r\n						<button class=\"PrivateBTSeedSearchButton\" onclick=\"PrivateBTSearchReset()\"/> 重&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;置 </button>\r\n		        </div>\r\n		        \r\n          \r\n        \r\n		</div>\r\n</div>\r\n<div id=\"PrivateBTSearchTips\" class=\"PrivateBTSearchTips\"></div>\r\n<div id=\"PrivateBTSearchMenu\" class=\"PrivateBTSearchMenu\" onmouseover=\"clearTimeout(privatebtclock)\"; onmouseout=\"privatebtclock=setTimeout('PrivateBTSeedSearchKeywordTypeHideMenu()',500);\">\r\n    <a style=\"cursor:pointer;\" onclick=\"PrivateBTSearchMenuSet('seedname')\">种子名</a>\r\n    <br/><a style=\"cursor:pointer;\" onclick=\"PrivateBTSearchMenuSet('username')\">用户名</a>\r\n</div>\r\n\r\n<script language=\"javascript\">\r\nfunction shownotin()\r\n{\r\n  $(\"divnotinkeyword\").style.display=\"block\";\r\n  $(\"divnotinkeywordhit\").style.display=\"none\";\r\n}\r\nvar privatebtclock;\r\n    var currentuser = '");
	templateBuilder.Append(username.ToString());
	templateBuilder.Append("';\r\n    var allstat = '<div class=\"select_typeid\"><select name=\\\"PrivateBTSeedSearchSelectuser\\\" class=\\\"PrivateBTSeedSearchSelect\\\" id=\\\"PrivateBTSeedSearchSelectuser\\\">';\r\n    allstat += '<option value=\\\"1\\\"';");
	if (userstat==1)
	{

	templateBuilder.Append("allstat += 'selected=\\\"selected\\\"';");
	}	//end if

	templateBuilder.Append("allstat += '>上传中</option>';\r\n    allstat += '<option value=\\\"2\\\"';");
	if (userstat==2)
	{

	templateBuilder.Append("allstat += 'selected=\\\"selected\\\"';");
	}	//end if

	templateBuilder.Append("allstat += '>下载中</option>';\r\n    allstat += '<option value=\\\"3\\\"';");
	if (userstat==3)
	{

	templateBuilder.Append("allstat += 'selected=\\\"selected\\\"';");
	}	//end if

	templateBuilder.Append("allstat += '>发布的</option>';\r\n    allstat += '<option value=\\\"4\\\"';");
	if (userstat==4)
	{

	templateBuilder.Append("allstat += 'selected=\\\"selected\\\"';");
	}	//end if

	templateBuilder.Append("allstat += '>完成的</option></select></div>';\r\n    var partstat = '<div class=\"select_typeid\"><select name=\\\"PrivateBTSeedSearchSelectuser\\\" class=\\\"PrivateBTSeedSearchSelect\\\" id=\\\"PrivateBTSeedSearchSelectuser\\\">';\r\n    partstat += '<option value=\\\"1\\\"';");
	if (userstat==1)
	{

	templateBuilder.Append("partstat += 'selected=\\\"selected\\\"';");
	}	//end if

	templateBuilder.Append("partstat += '>上传中</option>';\r\n    partstat += '<option value=\\\"2\\\"';");
	if (userstat==2)
	{

	templateBuilder.Append("partstat += 'selected=\\\"selected\\\"';");
	}	//end if

	templateBuilder.Append("partstat += '>下载中</option>';\r\n    partstat += '<option value=\\\"3\\\"';");
	if (userstat==3)
	{

	templateBuilder.Append("partstat += 'selected=\\\"selected\\\"';");
	}	//end if

	templateBuilder.Append("partstat += '>发布的</option></select></div>';\r\nfunction PrivateBTSearchInputKeyDown(event)\r\n{\r\n    event = event ? event : (window.event ? window.event : null); \r\n    if(event.keyCode==13)\r\n    { \r\n        PrivateBTSearchSubmit();\r\n        event.returnvalue = false;\r\n        return;\r\n    }\r\n    else\r\n    { \r\n        setTimeout('PrivateBTSearchUserStatChange()',50);\r\n        event.returnvalue = false;\r\n        return false;\r\n    }\r\n    event.returnvalue = false;\r\n    return false;\r\n}\r\nfunction PrivateBTSearchUserStatChange()\r\n{\r\n\r\n    if(document.getElementById(\"PrivateBTSeedSearchhideusername\").value == currentuser)\r\n    {\r\n        document.getElementById('userstatreplace').innerHTML = allstat;\r\n    }\r\n    else document.getElementById('userstatreplace').innerHTML = partstat;\r\n    loadselect(\"PrivateBTSeedSearchSelectuser\");\r\n}\r\n\r\nfunction PrivateBTSearchReset()\r\n{\r\n    document.getElementById(\"PrivateBTSeedSearchhideusername\").value = \"\";\r\n    document.getElementById(\"PrivateBTSeedSearchhidekeywords\").value = \"\";\r\n}\r\nfunction PrivateBTSeedSearchKeywordTypeHideMenu()\r\n{\r\n    document.getElementById(\"PrivateBTSearchMenu\").className='PrivateBTSearchMenu';\r\n}\r\nfunction PrivateBTSeedSearchKeywordTypeshowMenu()\r\n{\r\n    var target = document.getElementById(\"PrivateBTseedsearchdiv\");\r\n    var pos = new CPos(target.offsetLeft, target.offsetTop);\r\n    var target = target.offsetParent;\r\n    while (target)\r\n    {\r\n        pos.x += target.offsetLeft;\r\n        pos.y += target.offsetTop;\r\n        target = target.offsetParent\r\n    }\r\n    document.getElementById(\"PrivateBTSearchMenu\").className='PrivateBTSearchMenuShow';\r\n    if(navigator.userAgent.indexOf(\"Firefox\")!=-1)\r\n    {\r\n        document.getElementById(\"PrivateBTSearchMenu\").style.left=(pos.x - 0) + 'px';\r\n        document.getElementById(\"PrivateBTSearchMenu\").style.top=(pos.y + 28) + 'px';\r\n    }\r\n    else\r\n    {\r\n        document.getElementById(\"PrivateBTSearchMenu\").style.left=(pos.x - 0) + 'px';\r\n        document.getElementById(\"PrivateBTSearchMenu\").style.top=(pos.y + 28) + 'px';\r\n    }\r\n}\r\n\r\nfunction PrivateBTSearchSubmit()\r\n{\r\n    var nexturl = \"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("showseeds.aspx?\";\r\n    var type = \"\";\r\n    var orderby = \"\";\r\n    var asc = \"False\";\r\n    var keywords = \"\";\r\n    var notinkeywords = \"\";\r\n    var seedstat = \"\";\r\n    var userstat = \"\";\r\n    var username = \"\";\r\n\r\n    if(document.getElementById('PrivateBTSeedSearchSelectType').value=='0') type =\"type=all\";\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='1') type = 'type=movie';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='2') type = 'type=tv';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='3') type = 'type=comic';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='4') type = 'type=music';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='5') type = 'type=game';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='6') type = 'type=discovery';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='7') type = 'type=sport';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='8') type = 'type=entertainment';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='9') type = 'type=software';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='10') type = 'type=staff';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='11') type = 'type=video';\r\n    else if(document.getElementById('PrivateBTSeedSearchSelectType').value=='12') type = 'type=other';\r\n    else  type='type=all';\r\n    \r\n    orderby = document.getElementById('PrivateBTSeedSearchOrderBy').value;\r\n    asc = document.getElementById('PrivateBTSeedSearchAsc').value;\r\n    keywords = document.getElementById('PrivateBTSeedSearchhidekeywords').value;\r\n    notinkeywords = document.getElementById('PrivateBTSeedSearchhidenotinkeywords').value;\r\n    seedstat = document.getElementById('PrivateBTSeedSearchSelectStat').value;\r\n    userstat = document.getElementById('PrivateBTSeedSearchSelectuser').value;\r\n    username = document.getElementById('PrivateBTSeedSearchhideusername').value;\r\n    \r\n    nexturl += type;\r\n    if(orderby != '') nexturl += '&orderby=' + orderby;\r\n    if(asc != '') nexturl +=  '&asc=' + asc;\r\n    if(keywords != '') nexturl += '&keywords=' + escape(keywords).replace(/\\+/g, '%2B').replace(/\\\"/g,'%22').replace(/\\'/g, '%27').replace(/\\//g,'%2F');\r\n    if(notinkeywords != '') nexturl += '&notin=' + escape(notinkeywords).replace(/\\+/g, '%2B').replace(/\\\"/g,'%22').replace(/\\'/g, '%27').replace(/\\//g,'%2F');\r\n    if(seedstat != '') nexturl += '&seedstat=' + seedstat;\r\n    if(username != '') nexturl += '&username=' + username;\r\n    if(userstat != '') nexturl += '&userstat=' + userstat;\r\n    \r\n    window.location.href = nexturl;\r\n\r\n}\r\nfunction PrivateBTSearchSubmitPage(url)\r\n{\r\n    document.getElementById('seedsearchform').action = url;\r\n    document.getElementById('seedsearchform').submit()\r\n}\r\nfunction CPos(x, y)\r\n{\r\n    this.x = x;\r\n    this.y = y;\r\n}\r\nfunction tips()\r\n{\r\n    document.onkeydown = \"\";\r\n    var target = document.getElementById(\"PrivateBTseedsearchdiv\");\r\n    var pos = new CPos(target.offsetLeft, target.offsetTop);\r\n    var target = target.offsetParent;\r\n    while (target)\r\n    {\r\n        pos.x += target.offsetLeft;\r\n        pos.y += target.offsetTop;\r\n        target = target.offsetParent\r\n    }\r\n    document.getElementById(\"PrivateBTSearchTips\").className='PrivateBTSearchTipsShow';\r\n    document.getElementById(\"PrivateBTSearchTips\").innerHTML='请使用空格分隔关键词，种子名和用户名的搜索条件会同时起作用，不用时请清除';\r\n    if(navigator.userAgent.indexOf(\"Firefox\")!=-1)\r\n    {\r\n        document.getElementById(\"PrivateBTSearchTips\").style.left=(pos.x - 0) + 'px';\r\n        document.getElementById(\"PrivateBTSearchTips\").style.top=(pos.y + 28) + 'px';\r\n    }\r\n    else\r\n    {\r\n        document.getElementById(\"PrivateBTSearchTips\").style.left=(pos.x - 0) + 'px';\r\n        document.getElementById(\"PrivateBTSearchTips\").style.top=(pos.y + 28) + 'px';\r\n    }\r\n}\r\nfunction outtips(){\r\n    document.getElementById(\"PrivateBTSearchTips\").className='PrivateBTSearchTips';\r\n    document.onkeydown = function(event) \r\n    {\r\n        event = event ? event : (window.event ? window.event : null);   \r\n        //alert((event.currentTarget || document.activeElement).tagName);\r\n        if((event.currentTarget || document.activeElement).tagName == \"INPUT\")\r\n        {\r\n            event.returnvalue = true;\r\n            return;\r\n        }\r\n        if (event.keyCode==39) \r\n        {\r\n            location=nextpage;\r\n            event.returnvalue = false;\r\n            return;\r\n        }\r\n        if (event.keyCode==37) \r\n        {\r\n            location=prevpage;\r\n            event.returnvalue = false;\r\n            return;\r\n        }\r\n        event.returnvalue = true;\r\n        return;\r\n        //event.returnvalue = false;\r\n    }\r\n}\r\n</");
	templateBuilder.Append("script>\r\n</form>\r\n");


	bool showkeepreward = false;
	

	if (userstat==1&&((searchuserid==userid)||(userid==1&&searchuserid>1)))
	{

	 showkeepreward = true;
	
	PTKeepRewardStatic kr = PrivateBT.GetKeepReward(searchuserid, DateTime.Now.AddMinutes(-0), searchuserinfo.Joindate);
	
	double kratio = 0;
	
	string kratiofomart = "0.0";
	
	templateBuilder.Append("\r\n<br/><br/>\r\n<div class=\"PTSeedRewardShow\" id = \"PTSeedRewardShow\" style=\"width:100%;text-align:center;\">\r\n<div style=\"width:920px;margin-right:auto;margin-left:auto;border:#AAA 1px solid;\">\r\n   <span></span>\r\n   <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n   <tr>\r\n      <th width=\"200\" colspan=\"4\" style=\"text-align:center;\"><b>有效保种 ");
	templateBuilder.Append(kr.TotalUpCount.ToString().Trim());
	templateBuilder.Append(" 个，总计保种奖励 ");
	templateBuilder.Append(PTTools.Upload2Str(kr.TotalRewardPerHour).ToString().Trim());
	templateBuilder.Append("/h</b> ");
	if (PrivateBT.DEBUG_IsTestUser(userid)==true)
	{

	templateBuilder.Append("<br/>（旧算法理论保种奖励：");
	templateBuilder.Append(PTTools.Upload2Str(kr.Old_RewardPerHour).ToString().Trim());
	templateBuilder.Append("/h）（旧算法实际执行保种奖励大约：");
	templateBuilder.Append(PTTools.Upload2Str(kr.Old_RewardPerHour_Real).ToString().Trim());
	templateBuilder.Append("/h）");
	}	//end if

	templateBuilder.Append("</th>\r\n   </tr>\r\n   <tr>\r\n      <th width=\"150\">超大体积（&gt;100GB)</th>\r\n      <th width=\"50\">");
	templateBuilder.Append(kr.BigBig_UpCount.ToString().Trim());
	templateBuilder.Append(" 个</th>\r\n      <th width=\"90\">");
	templateBuilder.Append(PTTools.Upload2Str(kr.BigBig_RewardPerHour).ToString().Trim());
	templateBuilder.Append("/h</th>\r\n      ");	 kratio = (double)(kr.BigBig_RewardPerHour/kr.BigBig_RewardPerHour_Limit);
	
	templateBuilder.Append("\r\n      <td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:");
	templateBuilder.Append(((int)(kratio*420)).ToString());
	templateBuilder.Append("px'>&nbsp;</div></div>&nbsp;上限");
	templateBuilder.Append(PTTools.Upload2Str(kr.BigBig_RewardPerHour_Limit).ToString().Trim());
	templateBuilder.Append("/h （");
	templateBuilder.Append((kratio*100).ToString(kratiofomart).ToString().Trim());
	templateBuilder.Append("%）</td>\r\n   </tr>\r\n   <tr>\r\n      <th width=\"150\">大体积（25GB~100GB）</th>\r\n      <th width=\"50\">");
	templateBuilder.Append(kr.Big_UpCount.ToString().Trim());
	templateBuilder.Append(" 个</th>\r\n      <th width=\"90\">");
	templateBuilder.Append(PTTools.Upload2Str(kr.Big_RewardPerHour).ToString().Trim());
	templateBuilder.Append("/h</th>\r\n      ");	 kratio = (double)(kr.Big_RewardPerHour/kr.Big_RewardPerHour_Limit);
	
	templateBuilder.Append("\r\n      <td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:");
	templateBuilder.Append(((int)(kratio*420)).ToString());
	templateBuilder.Append("px'>&nbsp;</div></div>&nbsp;上限");
	templateBuilder.Append(PTTools.Upload2Str(kr.Big_RewardPerHour_Limit).ToString().Trim());
	templateBuilder.Append("/h （");
	templateBuilder.Append((kratio*100).ToString(kratiofomart).ToString().Trim());
	templateBuilder.Append("%）</td>\r\n   </tr>\r\n   <tr>\r\n      <th width=\"150\">中等体积（5GB~25GB）</th>\r\n      <th width=\"50\">");
	templateBuilder.Append(kr.Mid_UpCount.ToString().Trim());
	templateBuilder.Append(" 个</th>\r\n      <th width=\"90\">");
	templateBuilder.Append(PTTools.Upload2Str(kr.Mid_RewardPerHour).ToString().Trim());
	templateBuilder.Append("/h</th>\r\n      ");	 kratio = (double)(kr.Mid_RewardPerHour/kr.Mid_RewardPerHour_Limit);
	
	templateBuilder.Append("\r\n      <td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:");
	templateBuilder.Append(((int)(kratio*420)).ToString());
	templateBuilder.Append("px'>&nbsp;</div></div>&nbsp;上限");
	templateBuilder.Append(PTTools.Upload2Str(kr.Mid_RewardPerHour_Limit).ToString().Trim());
	templateBuilder.Append("/h （");
	templateBuilder.Append((kratio*100).ToString(kratiofomart).ToString().Trim());
	templateBuilder.Append("%）</td>\r\n   </tr>\r\n   <tr>\r\n      <th width=\"150\">小体积（1GB~5GB）</th>\r\n      <th width=\"50\">");
	templateBuilder.Append(kr.Small_UpCount.ToString().Trim());
	templateBuilder.Append(" 个</th>\r\n      <th width=\"90\">");
	templateBuilder.Append(PTTools.Upload2Str(kr.Small_RewardPerHour).ToString().Trim());
	templateBuilder.Append("/h</th>\r\n      ");	 kratio = (double)(kr.Small_RewardPerHour/kr.Small_RewardPerHour_Limit);
	
	templateBuilder.Append("\r\n      <td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:");
	templateBuilder.Append(((int)(kratio*420)).ToString());
	templateBuilder.Append("px'>&nbsp;</div></div>&nbsp;上限");
	templateBuilder.Append(PTTools.Upload2Str(kr.Small_RewardPerHour_Limit).ToString().Trim());
	templateBuilder.Append("/h （");
	templateBuilder.Append((kratio*100).ToString(kratiofomart).ToString().Trim());
	templateBuilder.Append("%）</td>\r\n   </tr>\r\n   <tr>\r\n      <th width=\"150\">微小体积（&lt;1GB）</th>\r\n      <th width=\"50\">");
	templateBuilder.Append(kr.Tiny_UpCount.ToString().Trim());
	templateBuilder.Append(" 个</th>\r\n      <th width=\"90\">");
	templateBuilder.Append(PTTools.Upload2Str(kr.Tiny_RewardPerHour).ToString().Trim());
	templateBuilder.Append("/h</th>\r\n      ");	 kratio = (double)(kr.Tiny_RewardPerHour/kr.Tiny_RewardPerHour_Limit);
	
	templateBuilder.Append("\r\n      <td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:");
	templateBuilder.Append(((int)(kratio*420)).ToString());
	templateBuilder.Append("px'>&nbsp;</div></div>&nbsp;上限");
	templateBuilder.Append(PTTools.Upload2Str(kr.Tiny_RewardPerHour_Limit).ToString().Trim());
	templateBuilder.Append("/h （");
	templateBuilder.Append((kratio*100).ToString(kratiofomart).ToString().Trim());
	templateBuilder.Append("%）</td>\r\n   </tr>\r\n   </table>\r\n</div>\r\n</div>\r\n");
	}	//end if




	if (forum.Layer!=0)
	{

	templateBuilder.Append("\r\n<div class=\"pages_btns cl\">\r\n	<div class=\"pages\">\r\n    ");
	if (keywords!=""||searchusername!=""||notinkeywords!="")
	{

	templateBuilder.Append("\r\n        <script type=\"text/javascript\">\r\n            $('PrivateBTSeedSearchForm').style.display='';\r\n        </");
	templateBuilder.Append("script>\r\n    ");
	}
	else
	{

	templateBuilder.Append("\r\n        <script type=\"text/javascript\">\r\n            $('PrivateBTSeedSearchForm').style.display='none';\r\n        </");
	templateBuilder.Append("script>\r\n        <cite class=\"z\" id=\"PrivateBTSeedSearchFormShowButton\"><a onclick=\"javascript:$('PrivateBTSeedSearchForm').style.display='';$('PrivateBTSeedSearchFormShowButton').style.display='none';\"> 搜索种子 </a> &nbsp;&nbsp; </cite>\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n		<cite class=\"pageback z\" id=\"visitedforums\"");
	if (showvisitedforumsmenu)
	{

	templateBuilder.Append(" onmouseover=\"$('visitedforums').id = 'visitedforumstmp';this.id = 'visitedforums';showMenu({'ctrlid':this.id, 'pos':'34'});\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("\">返回</a></cite>\r\n		");
	if (pagecount!=1)
	{

	templateBuilder.Append("\r\n			");
	templateBuilder.Append(pagenumbers.ToString());
	templateBuilder.Append("\r\n			");
	if (pagecount>8)
	{

	templateBuilder.Append("\r\n			<kbd>\r\n			<input name=\"gopage\" type=\"text\" class=\"txt\" id=\"pageidinput1\" title=\"可以输入页码按回车键自动跳转\" value=\"");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append("\" style=\"text-align:center;\" onfocus=\"this.value=this.defaultValue;this.select();\" onKeyDown=\"pageinputOnKeyDown(this,event);\" size=\"2\" maxlength=\"9\" />/ ");
	templateBuilder.Append(pagecount.ToString());
	templateBuilder.Append("</kbd>\r\n            <script type=\"text/javascript\">\r\n                function pageinputOnKeyDown(obj, event) {\r\n                    if (event.keyCode == 13) \r\n                    {\r\n                         window.location = 'showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&seedstat=0&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("&page=' + (parseInt(obj.value) > 0 ? parseInt(obj.value) : 1);\r\n                    }\r\n                    return (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 97 && event.keyCode <= 105) || event.keyCode == 8;\r\n                }\r\n            </");
	templateBuilder.Append("script>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			");
	templateBuilder.Append(nextpage.ToString());
	templateBuilder.Append("\r\n		");
	}	//end if

	templateBuilder.Append("\r\n	</div>\r\n\r\n<span class=\"PrivateBTFontLargeLow\"><span class=\"SeedGray\">种子数：");
	templateBuilder.Append(seedinfocount.ToString());
	templateBuilder.Append("&nbsp;&nbsp;&nbsp;&nbsp;总容量：");
	templateBuilder.Append(totalsize.ToString());
	templateBuilder.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
	templateBuilder.Append(addmessage.ToString());
	templateBuilder.Append("</span></span>\r\n</div>\r\n\r\n<!-- 分类选择区预留 -->\r\n\r\n<!-- END分类选择区预留 -->\r\n\r\n<ul id=\"rewardmenu_menu\" class=\"p_pop\"  style=\"display: none\">\r\n	<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showforum.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&filter=rewarding\">进行中的悬赏</a></li>\r\n	<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showforum.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&filter=rewarded\">已结束的悬赏</a></li>\r\n</ul>\r\n<div class=\"main thread\">\r\n	<form id=\"seedmoderate\" name=\"seedmoderate\" method=\"post\" action=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("seedadmin.aspx?action=moderate&forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&infloat=1\">\r\n		<div class=\"category\">\r\n		<table summary=\"");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("\" cellspacing=\"0\" cellpadding=\"0\">\r\n			<tr>\r\n			<th><span title=\"在新窗口中打开帖子\" id=\"atarget\" style=\"float:right;\">新窗</span>\r\n			");
	if (keywords!="")
	{

	templateBuilder.Append("\r\n			搜索字段: \r\n        <a id=\"keywordselectmenu\" onclick=\"showMenu(this.id);\" href=\"javascript:;\" class=\"drop xg2\">\r\n            ");
	if (keywordsmode==0||keywordsmode==1||keywordsmode==2)
	{

	templateBuilder.Append("仅种子标题");
	}
	else if (keywordsmode==3)
	{

	templateBuilder.Append("标题和文件");
	}
	else if (keywordsmode==4)
	{

	templateBuilder.Append("仅种子文件");
	}	//end if

	templateBuilder.Append("\r\n        </a>\r\n          <ul id=\"keywordselectmenu_menu\" class=\"p_pop\" style=\"display: none\">\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=1\" ");
	if (keywordsmode==0||keywordsmode==1||keywordsmode==2)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">仅种子标题</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=3\" ");
	if (keywordsmode==3)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">标题和文件</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=4\" ");
	if (keywordsmode==4)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">仅种子文件</a></li>\r\n          </ul>\r\n        <span class=\"pipe\">|</span>\r\n      ");
	}	//end if

	templateBuilder.Append("\r\n        筛选: \r\n        <a id=\"seedselectmenu\" onclick=\"showMenu(this.id);\" href=\"javascript:;\" class=\"drop xg2\">\r\n            ");
	if (seedstat==0)
	{

	templateBuilder.Append("正常种子");
	}
	else if (seedstat==1)
	{

	templateBuilder.Append("在线种子");
	}
	else if (seedstat==3)
	{

	templateBuilder.Append("IPv6在线");
	}
	else if (seedstat==4)
	{

	templateBuilder.Append("离线种子");
	}
	else if (seedstat==5)
	{

	templateBuilder.Append("蓝色种子");
	}
	else if (seedstat==6)
	{

	templateBuilder.Append("在线蓝种");
	}
	else if (seedstat==7)
	{

	templateBuilder.Append("IPv6蓝种");
	}
	else if (seedstat==8)
	{

	templateBuilder.Append("包含死种");
	}
	else if (seedstat==9)
	{

	templateBuilder.Append("只看死种");
	}	//end if

	templateBuilder.Append("\r\n        </a>\r\n          <ul id=\"seedselectmenu_menu\" class=\"p_pop\" style=\"display: none\">\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=0&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==0)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">正常种子</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=1&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==1)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">在线种子</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=3&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==3)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">IPv6在线</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=4&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==4)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">离线种子</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=5&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==5)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">蓝色种子</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=6&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==6)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">在线蓝种</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=7&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==7)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">IPv6蓝种</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=8&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==8)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">包含死种</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=9&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (seedstat==9)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">只看死种</a></li>\r\n          </ul>\r\n        <span class=\"pipe\">|</span>排序方式: \r\n        <a id=\"seedordermenu\" onclick=\"showMenu(this.id);\" href=\"javascript:;\" class=\"drop xg2\">\r\n           ");
	if (orderby==-1)
	{

	templateBuilder.Append("默认排序");
	}
	else if (orderby==0)
	{

	templateBuilder.Append("发布时间");
	}
	else if (orderby==1)
	{

	templateBuilder.Append("文件数量");
	}
	else if (orderby==2)
	{

	templateBuilder.Append("文件大小");
	}
	else if (orderby==3)
	{

	templateBuilder.Append("种子数量");
	}
	else if (orderby==4)
	{

	templateBuilder.Append("正在下载");
	}
	else if (orderby==5)
	{

	templateBuilder.Append("完成人数");
	}
	else if (orderby==6)
	{

	templateBuilder.Append("总计流量");
	}
	else if (orderby==7)
	{

	templateBuilder.Append("存活时间");
	}	//end if

	templateBuilder.Append("\r\n        </a>\r\n          <ul id=\"seedordermenu_menu\" class=\"p_pop\" style=\"display: none\">\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=-1&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==-1)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">默认排序</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=0&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==0)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">发布时间</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=1&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==1)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">文件数量</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=2&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==2)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">文件大小</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=3&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==3)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">种子数量</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=4&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==4)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">正在下载</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=5&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==5)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">完成人数</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=6&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==6)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">总计流量</a></li>\r\n          <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=7&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\" ");
	if (orderby==7)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">存活时间</a></li>\r\n          </ul>\r\n				<span class=\"pipe\">|</span>排序:\r\n				<a id=\"directmenu\" onclick=\"showMenu(this.id);\" href=\"javascript:;\" class=\"drop xg2\">");
	if (asc==false)
	{

	templateBuilder.Append("按降序排列");
	}
	else
	{

	templateBuilder.Append("按升序排列");
	}	//end if

	templateBuilder.Append("</a>\r\n				<ul id=\"directmenu_menu\" class=\"p_pop\" style=\"display: none\">\r\n				    <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=False&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\"");
	if (asc==false)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">按降序排列</a></li>\r\n				    <li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=True&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("\"");
	if (asc==true)
	{

	templateBuilder.Append("class=\"PrivateBTBold\"");
	}	//end if

	templateBuilder.Append(">按升序排列</a></li>\r\n				</ul>\r\n				");
	if (showmode2link>0)
	{

	templateBuilder.Append("\r\n				<span class=\"pipe\">|</span>\r\n				<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&notin=");
	templateBuilder.Append(PTTools.Escape(notinkeywords).ToString().Trim());
	templateBuilder.Append("&seedstat=");
	templateBuilder.Append(seedstat.ToString());
	templateBuilder.Append("&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=2\" class=\"PrivateBTBold\" >使用模糊查询更多结果：");
	templateBuilder.Append(showmode2link.ToString());
	templateBuilder.Append("个种子</a>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n			</th>\r\n			</tr>\r\n		</table>\r\n		</div>\r\n\r\n\r\n		<div class=\"threadlist\">\r\n		<table summary=\"");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("\" id=\"threadlist\" cellspacing=\"0\" cellpadding=\"0\" class=\"ShowseedsMainTable\">\r\n      \r\n      ");
	templateBuilder.Append("      <tbody><tr align=\"right\">\r\n          <td class=\"PrivateBTSeedListGrayLS\"><span>类别</span></td>\r\n          <td class=\"PrivateBTSeedListGrayR\"><a onclick=\"PrivateBTChangeOrderby('size')\" title=\"点击按照种子包含的文件大小排序\">大小</a>/<a onclick=\"PrivateBTChangeOrderby('file')\" title=\"点击按照种子包含的文件数排序\">文件</a></td>\r\n          <td class=\"PrivateBTSeedListGrayRS\"><a onclick=\"PrivateBTChangeOrderby('seed')\" title=\"点击按照正在做种的人数排序\">种子</a>/<a onclick=\"PrivateBTChangeOrderby('completed')\" title=\"点击按照正在下载的人数排序\">下载</a></td>\r\n          <td class=\"PrivateBTSeedListGrayRS\"><a onclick=\"PrivateBTChangeOrderby('finished')\" title=\"点击按照种子的完成数排序\">完成</a>/<a onclick=\"PrivateBTChangeOrderby('live')\" title=\"点击按照种子的生存时间排序\">存活</a></td>\r\n          ");
	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("\r\n            <td width=\"70px\"></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n            <td width=\"50px\"></td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListGrayL\">种子名称</td>\r\n          ");
	if (showkeepreward==false)
	{

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListGrayR\">回复/查看</td>\r\n          <td class=\"PrivateBTSeedListGrayREnd\"><a onclick=\"PrivateBTChangeOrderby('time')\" title=\"点击按照种子种子发布的时间排序\">发布者/发布时间</a></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <td width=\"120px\" style=\"text-align: right;color:#666\">真实上传/保种时间</td>\r\n          <td class=\"PrivateBTSeedListGrayREnd\">保种奖励/保种系数</td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n        </tr>\r\n      </tbody>");

	templateBuilder.Append("\r\n      \r\n      <!--Top Seed Begin-->\r\n      ");
	if (keywords==""&&pageid==1&&topseedinfolist.Count>0&&searchusername=="")
	{


	int topseedinfo__loop__id=0;
	foreach(PTSeedinfoShort topseedinfo in topseedinfolist)
	{
		topseedinfo__loop__id++;

	templateBuilder.Append("			\r\n      <tbody>\r\n        ");
	if (topseedinfo.Dis_UserDisplayStyle>10000)
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListUpload\">\r\n        ");
	}
	else if (topseedinfo.Dis_UserDisplayStyle>1000)
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListDownload\">\r\n        ");
	}
	else if (topseedinfo.Dis_UserDisplayStyle>100)
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListFinished\">\r\n        ");
	}
	else if (topseedinfo.Dis_UserDisplayStyle>10)
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListPublish\">\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListTop\">\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListType\">\r\n              <a href=\"?type=");
	templateBuilder.Append(topseedinfo.Dis_EngTypeName.ToString().Trim());
	templateBuilder.Append("\"  style=\"text-decoration:none\">\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/");
	templateBuilder.Append(topseedinfo.Type.ToString().Trim());
	templateBuilder.Append(".png\" title=\"");
	templateBuilder.Append(topseedinfo.Dis_ChnTypeName.ToString().Trim());
	templateBuilder.Append("\" />\r\n              </a></td>\r\n          <td class=\"PrivateBTSeedListFile\" title=\"文件总大小 ");
	templateBuilder.Append(topseedinfo.Dis_Size.ToString().Trim());
	templateBuilder.Append("，种子包含的文件数 ");
	templateBuilder.Append(topseedinfo.FileCount.ToString().Trim());
	templateBuilder.Append("，点击显示文件列表\"  onclick=\"fgbtshowwindow('showseedfile");
	templateBuilder.Append(topseedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("','showseedfile.aspx?seedid=");
	templateBuilder.Append(topseedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\">\r\n             <span class=\"PrivateBTSeedListSize\" align=\"right\" title=\"\">");
	templateBuilder.Append(topseedinfo.Dis_Size.ToString().Trim());
	templateBuilder.Append("</span><br/>\r\n             ");
	templateBuilder.Append(topseedinfo.FileCount.ToString().Trim());
	templateBuilder.Append("</td>\r\n          <td class=\"PrivateBTSeedListSeeds\" align=\"right\" title=\"在线种子数 ");
	templateBuilder.Append(topseedinfo.Upload.ToString().Trim());
	templateBuilder.Append("，正在下载 ");
	templateBuilder.Append(topseedinfo.Download.ToString().Trim());
	templateBuilder.Append("，点击显示节点列表\" onclick=\"fgbtshowwindow('showseedpeer");
	templateBuilder.Append(topseedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("','showseedpeer.aspx?seedid=");
	templateBuilder.Append(topseedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\">\r\n             ");
	templateBuilder.Append(topseedinfo.Dis_UploadCount.ToString().Trim());
	templateBuilder.Append("<br/>\r\n             <span class=\"PrivateBTSeedListDown\" align=\"right\">");
	templateBuilder.Append(topseedinfo.Download.ToString().Trim());
	templateBuilder.Append("</span></td>\r\n          <td class=\"PrivateBTSeedListFinished\" align=\"right\" title=\"完成数 ");
	templateBuilder.Append(topseedinfo.Finished.ToString().Trim());
	templateBuilder.Append("，存活时间 ");
	templateBuilder.Append(topseedinfo.Dis_TimetoLive.ToString().Trim());
	templateBuilder.Append("，累计流量 ");
	templateBuilder.Append(topseedinfo.Dis_DownloadTraffic.ToString().Trim());
	templateBuilder.Append("\">\r\n            ");
	templateBuilder.Append(topseedinfo.Finished.ToString().Trim());
	templateBuilder.Append("</br>\r\n            <span class=\"PrivateBTSeedListLive\" align=\"right\">");
	templateBuilder.Append(topseedinfo.Dis_TimetoLive.ToString().Trim());
	templateBuilder.Append("</span></td>\r\n          <td class=\"PrivateBTSeedListOp\" align=\"center\" style=\"color:#000\">\r\n            ");
	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("\r\n              <input type=\"checkbox\" name=\"topicid\" value=\"");
	templateBuilder.Append(topseedinfo.TopicId.ToString().Trim());
	templateBuilder.Append("\"  onclick=\"modclick(this);\" />\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n            \r\n            <a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("downloadseed.aspx?seedid=");
	templateBuilder.Append(topseedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("\">\r\n            <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/download.gif\" title=\"下载种子\"/></a></td>\r\n          <td class=\"PrivateBTSeedListTitle\">\r\n            <a  onclick=\"atarget(this)\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showtopic-");
	templateBuilder.Append(topseedinfo.TopicId.ToString().Trim());
	templateBuilder.Append(".aspx\">\r\n            ");
	if (topseedinfo.DownloadRatio==0f)
	{

	templateBuilder.Append("\r\n              <span style=\"color:#00F\">");
	templateBuilder.Append(topseedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}
	else if (topseedinfo.DownloadRatio==0.3f)
	{

	templateBuilder.Append("\r\n              <span style=\"color:#04A\">");
	templateBuilder.Append(topseedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}
	else if (topseedinfo.DownloadRatio==0.6f)
	{

	templateBuilder.Append("\r\n              <span style=\"color:#088\">");
	templateBuilder.Append(topseedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}
	else if (topseedinfo.DownloadRatio>1.0f||topseedinfo.UploadRatio<1.0f)
	{

	templateBuilder.Append("\r\n              <span style=\"color:#888\">");
	templateBuilder.Append(topseedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n              ");
	templateBuilder.Append(topseedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n            </a>\r\n            ");
	if (downloadratio<topseedinfo.DownloadRatio)
	{


	if (downloadratio==0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D0.png\" title=\"全站优惠，蓝种，不计下载");
	templateBuilder.Append(topseedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\"/>\r\n                ");
	}
	else if (downloadratio==0.3f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D3.png\" title=\"全站优惠，按30%计算下载流量");
	templateBuilder.Append(topseedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (downloadratio==0.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D6.png\" title=\"全站优惠，按60%计算下载流量");
	templateBuilder.Append(topseedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}	//end if


	}
	else
	{


	if (topseedinfo.DownloadRatio==1.0f)
	{


	}
	else if (topseedinfo.DownloadRatio==0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D0.png\" title=\"蓝种，不计下载");
	templateBuilder.Append(topseedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\"/>\r\n                ");
	}
	else if (topseedinfo.DownloadRatio==0.3f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D3.png\" title=\"按30%计算下载流量");
	templateBuilder.Append(topseedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (topseedinfo.DownloadRatio==0.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D6.png\" title=\"按60%计算下载流量");
	templateBuilder.Append(topseedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (topseedinfo.DownloadRatio==2.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D20.png\" title=\"按200%计算下载流量");
	templateBuilder.Append(topseedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (topseedinfo.DownloadRatio==3.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D30.png\" title=\"按300%计算下载流量");
	templateBuilder.Append(topseedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}	//end if


	}	//end if


	if (uploadratio>topseedinfo.UploadRatio)
	{


	if (uploadratio==3.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U30.png\" title=\"按300%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (uploadratio==2.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U20.png\" title=\"按200%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (uploadratio==1.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U16.png\" title=\"按160%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (uploadratio==1.2f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U12.png\" title=\"按120%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}	//end if


	}
	else
	{


	if (topseedinfo.UploadRatio==1.0f)
	{


	}
	else if (topseedinfo.UploadRatio==2.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U20.png\" title=\"按200%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (topseedinfo.UploadRatio==1.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U16.png\" title=\"按160%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (topseedinfo.UploadRatio==1.2f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U12.png\" title=\"按120%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (topseedinfo.UploadRatio==0.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U6.png\" title=\"按60%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (topseedinfo.UploadRatio==0.3f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U3.png\" title=\"按30%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (topseedinfo.UploadRatio==0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U0.png\" title=\"按0%计算上传流量");
	templateBuilder.Append(topseedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}	//end if


	}	//end if


	if (topseedinfo.Upload!=0)
	{


	if (topseedinfo.IPv6==1)
	{

	templateBuilder.Append("\r\n                  <span class=\"PrivateBTFontRed\" title=\"该种子只能通过IPv6下载\">IPv6</span>\r\n              ");
	}
	else if (topseedinfo.IPv6==0)
	{

	templateBuilder.Append("\r\n                  <span class=\"PrivateBTFontRed\" title=\"该种子只能通过IPv4下载\">IPv4</span>\r\n              ");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n            \r\n            <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/TopSeed.png\" title=\"置顶种子\" />\r\n            \r\n            ");
	if (orderby==6)
	{

	templateBuilder.Append("\r\n                  <span class=\"PrivateBTFontGray\" title=\"累计流量\">");
	templateBuilder.Append(topseedinfo.Dis_DownloadTraffic.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}	//end if


	if (orderby==7)
	{

	templateBuilder.Append("\r\n                  <span class=\"PrivateBTFontGray\" title=\"存活时间\">");
	templateBuilder.Append(topseedinfo.Dis_TimetoLive.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}	//end if


	if (userid<=2)
	{


	if (topseedinfo.Dis_TrafficCheck=="blue")
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/check_blue.png\" title=\"上传比下载偏低\"/>\r\n              ");
	}
	else if (topseedinfo.Dis_TrafficCheck=="green")
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/check_green.png\" title=\"上传与下载相当\"/>\r\n              ");
	}
	else if (topseedinfo.Dis_TrafficCheck=="yellow")
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/check_yellow.png\" title=\"上传过多\"/>\r\n              ");
	}
	else
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/check_red.png\" title=\"上传严重超过下载\"/>\r\n              ");
	}	//end if


	if (topseedinfo.Rss_Acc>0)
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/speedup.png\" title=\"早期加速RSS\"/>\r\n              ");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n          ");
	templateBuilder.Append(topseedinfo.Dis_RatioNoteAdd.ToString().Trim());
	templateBuilder.Append("</td> \r\n          <td class=\"PrivateBTSeedListReplies\" align=\"right\"><em>");
	templateBuilder.Append(topseedinfo.Replies.ToString().Trim());
	templateBuilder.Append("</em><br/>");
	templateBuilder.Append(topseedinfo.Views.ToString().Trim());
	templateBuilder.Append("</td>\r\n          <td class=\"PrivateBTSeedListAuthor\" align=\"right\">\r\n              <cite><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("userinfo-");
	templateBuilder.Append(topseedinfo.Uid.ToString().Trim());
	templateBuilder.Append(".aspx\" title=\"");
	templateBuilder.Append(topseedinfo.UserName.ToString().Trim());
	templateBuilder.Append("\">");
	templateBuilder.Append(topseedinfo.UserName.ToString().Trim());
	templateBuilder.Append("</a></cite>\r\n              <em>");
	templateBuilder.Append(topseedinfo.Dis_PostDateTime.ToString().Trim());
	templateBuilder.Append("</em></td>\r\n        </tr>\r\n      </tbody>\r\n\r\n      ");
	}	//end loop


	templateBuilder.Append("      <tbody><tr align=\"right\">\r\n          <td class=\"PrivateBTSeedListGrayLS\"><span>类别</span></td>\r\n          <td class=\"PrivateBTSeedListGrayR\"><a onclick=\"PrivateBTChangeOrderby('size')\" title=\"点击按照种子包含的文件大小排序\">大小</a>/<a onclick=\"PrivateBTChangeOrderby('file')\" title=\"点击按照种子包含的文件数排序\">文件</a></td>\r\n          <td class=\"PrivateBTSeedListGrayRS\"><a onclick=\"PrivateBTChangeOrderby('seed')\" title=\"点击按照正在做种的人数排序\">种子</a>/<a onclick=\"PrivateBTChangeOrderby('completed')\" title=\"点击按照正在下载的人数排序\">下载</a></td>\r\n          <td class=\"PrivateBTSeedListGrayRS\"><a onclick=\"PrivateBTChangeOrderby('finished')\" title=\"点击按照种子的完成数排序\">完成</a>/<a onclick=\"PrivateBTChangeOrderby('live')\" title=\"点击按照种子的生存时间排序\">存活</a></td>\r\n          ");
	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("\r\n            <td width=\"70px\"></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n            <td width=\"50px\"></td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListGrayL\">种子名称</td>\r\n          ");
	if (showkeepreward==false)
	{

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListGrayR\">回复/查看</td>\r\n          <td class=\"PrivateBTSeedListGrayREnd\"><a onclick=\"PrivateBTChangeOrderby('time')\" title=\"点击按照种子种子发布的时间排序\">发布者/发布时间</a></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <td width=\"120px\" style=\"text-align: right;color:#666\">真实上传/保种时间</td>\r\n          <td class=\"PrivateBTSeedListGrayREnd\">保种奖励/保种系数</td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n        </tr>\r\n      </tbody>");


	}	//end if

	templateBuilder.Append("\r\n      <!--Top Seed End-->\r\n      \r\n      \r\n      \r\n      \r\n      \r\n      \r\n      <!--SeedList Begin-->\r\n      ");
	int seedinfo__loop__id=0;
	foreach(PTSeedinfoShort seedinfo in seedinfolist)
	{
		seedinfo__loop__id++;


	if (seedinfo.SeedId<0)
	{


	templateBuilder.Append("      <tbody><tr align=\"right\">\r\n          <td class=\"PrivateBTSeedListGrayLS\"><span>类别</span></td>\r\n          <td class=\"PrivateBTSeedListGrayR\"><a onclick=\"PrivateBTChangeOrderby('size')\" title=\"点击按照种子包含的文件大小排序\">大小</a>/<a onclick=\"PrivateBTChangeOrderby('file')\" title=\"点击按照种子包含的文件数排序\">文件</a></td>\r\n          <td class=\"PrivateBTSeedListGrayRS\"><a onclick=\"PrivateBTChangeOrderby('seed')\" title=\"点击按照正在做种的人数排序\">种子</a>/<a onclick=\"PrivateBTChangeOrderby('completed')\" title=\"点击按照正在下载的人数排序\">下载</a></td>\r\n          <td class=\"PrivateBTSeedListGrayRS\"><a onclick=\"PrivateBTChangeOrderby('finished')\" title=\"点击按照种子的完成数排序\">完成</a>/<a onclick=\"PrivateBTChangeOrderby('live')\" title=\"点击按照种子的生存时间排序\">存活</a></td>\r\n          ");
	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("\r\n            <td width=\"70px\"></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n            <td width=\"50px\"></td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListGrayL\">种子名称</td>\r\n          ");
	if (showkeepreward==false)
	{

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListGrayR\">回复/查看</td>\r\n          <td class=\"PrivateBTSeedListGrayREnd\"><a onclick=\"PrivateBTChangeOrderby('time')\" title=\"点击按照种子种子发布的时间排序\">发布者/发布时间</a></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <td width=\"120px\" style=\"text-align: right;color:#666\">真实上传/保种时间</td>\r\n          <td class=\"PrivateBTSeedListGrayREnd\">保种奖励/保种系数</td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n        </tr>\r\n      </tbody>");


	}
	else
	{

	templateBuilder.Append("\r\n      <tbody>\r\n        ");
	if (seedinfo.Dis_UserDisplayStyle>10000)
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListUpload\">\r\n        ");
	}
	else if (seedinfo.Dis_UserDisplayStyle>1000)
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListDownload\">\r\n        ");
	}
	else if (seedinfo.Dis_UserDisplayStyle>100)
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListFinished\">\r\n        ");
	}
	else if (seedinfo.Dis_UserDisplayStyle>10)
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedListPublish\">\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n        <tr class=\"PrivateBTSeedList\">\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n          <td  class=\"PrivateBTSeedListType\">\r\n              <a href=\"?type=");
	templateBuilder.Append(seedinfo.Dis_EngTypeName.ToString().Trim());
	templateBuilder.Append("\"  style=\"text-decoration:none\">\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/");
	templateBuilder.Append(seedinfo.Type.ToString().Trim());
	templateBuilder.Append(".png\" title=\"");
	templateBuilder.Append(seedinfo.Dis_ChnTypeName.ToString().Trim());
	templateBuilder.Append("\" /></td>\r\n              </a>\r\n          <td class=\"PrivateBTSeedListFile\" title=\"文件总大小 ");
	templateBuilder.Append(seedinfo.Dis_Size.ToString().Trim());
	templateBuilder.Append("，种子包含的文件数 ");
	templateBuilder.Append(seedinfo.FileCount.ToString().Trim());
	templateBuilder.Append("，点击显示文件列表\" onclick=\"fgbtshowwindow('showseedfile");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("','showseedfile.aspx?seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\">\r\n             <span class=\"PrivateBTSeedListSize\">");
	templateBuilder.Append(seedinfo.Dis_Size.ToString().Trim());
	templateBuilder.Append("</span><br/>\r\n             ");
	templateBuilder.Append(seedinfo.FileCount.ToString().Trim());
	templateBuilder.Append("</td>\r\n          <td class=\"PrivateBTSeedListSeeds\" title=\"在线种子数 ");
	templateBuilder.Append(seedinfo.Upload.ToString().Trim());
	templateBuilder.Append("，正在下载数 ");
	templateBuilder.Append(seedinfo.Download.ToString().Trim());
	templateBuilder.Append("，点击显示节点列表\" onclick=\"fgbtshowwindow('showseedpeer");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("','showseedpeer.aspx?seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("')\">\r\n            ");
	templateBuilder.Append(seedinfo.Dis_UploadCount.ToString().Trim());
	templateBuilder.Append("<br/>\r\n            <span class=\"PrivateBTSeedListDown\">");
	templateBuilder.Append(seedinfo.Download.ToString().Trim());
	templateBuilder.Append("</span></td>\r\n          <td class=\"PrivateBTSeedListFinished\" title=\"完成数 ");
	templateBuilder.Append(seedinfo.Finished.ToString().Trim());
	templateBuilder.Append("，存活时间 ");
	templateBuilder.Append(seedinfo.Dis_TimetoLive.ToString().Trim());
	templateBuilder.Append("，累计流量 ");
	templateBuilder.Append(seedinfo.Dis_DownloadTraffic.ToString().Trim());
	templateBuilder.Append("\">\r\n            ");
	templateBuilder.Append(seedinfo.Finished.ToString().Trim());
	templateBuilder.Append("</br>\r\n            <span class=\"PrivateBTSeedListLive\" align=\"right\">");
	templateBuilder.Append(seedinfo.Dis_TimetoLive.ToString().Trim());
	templateBuilder.Append("</span></td>\r\n          <td class=\"PrivateBTSeedListOp\">\r\n            ");
	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("\r\n              <input type=\"checkbox\" name=\"topicid\" value=\"");
	templateBuilder.Append(seedinfo.TopicId.ToString().Trim());
	templateBuilder.Append("\"  onclick=\"modclick(this);\" />\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n            \r\n            <a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("downloadseed.aspx?seedid=");
	templateBuilder.Append(seedinfo.SeedId.ToString().Trim());
	templateBuilder.Append("\">\r\n            <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/download.gif\" title=\"下载种子\"/></a></td>\r\n          <td class=\"PrivateBTSeedListTitle\">\r\n            <a onclick=\"atarget(this)\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("showtopic-");
	templateBuilder.Append(seedinfo.TopicId.ToString().Trim());
	templateBuilder.Append(".aspx\">\r\n            ");
	if (seedinfo.DownloadRatio==0f)
	{

	templateBuilder.Append("\r\n              <span style=\"color:#00F\">");
	templateBuilder.Append(seedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}
	else if (seedinfo.DownloadRatio==0.3f)
	{

	templateBuilder.Append("\r\n              <span style=\"color:#04A\">");
	templateBuilder.Append(seedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}
	else if (seedinfo.DownloadRatio==0.6f)
	{

	templateBuilder.Append("\r\n              <span style=\"color:#088\">");
	templateBuilder.Append(seedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}
	else if (seedinfo.DownloadRatio>1.0f||seedinfo.UploadRatio<1.0f)
	{

	templateBuilder.Append("\r\n              <span style=\"color:#888\">");
	templateBuilder.Append(seedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n              ");
	templateBuilder.Append(seedinfo.TopicTitle.ToString().Trim());
	templateBuilder.Append("\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n            </a>\r\n            ");
	if (downloadratio<seedinfo.DownloadRatio)
	{


	if (downloadratio==0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D0.png\" title=\"全站优惠，蓝种，不计下载");
	templateBuilder.Append(seedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\"/>\r\n                ");
	}
	else if (downloadratio==0.3f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D3.png\" title=\"全站优惠，按30%计算下载流量");
	templateBuilder.Append(seedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (downloadratio==0.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D6.png\" title=\"全站优惠，按60%计算下载流量");
	templateBuilder.Append(seedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}	//end if


	}
	else
	{


	if (seedinfo.DownloadRatio==1.0f)
	{


	}
	else if (seedinfo.DownloadRatio==0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D0.png\" title=\"蓝种，不计下载");
	templateBuilder.Append(seedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\"/>\r\n                ");
	}
	else if (seedinfo.DownloadRatio==0.3f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D3.png\" title=\"按30%计算下载流量");
	templateBuilder.Append(seedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (seedinfo.DownloadRatio==0.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D6.png\" title=\"按60%计算下载流量");
	templateBuilder.Append(seedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (seedinfo.DownloadRatio==2.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D20.png\" title=\"按200%计算下载流量");
	templateBuilder.Append(seedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (seedinfo.DownloadRatio==3.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/D30.png\" title=\"按300%计算下载流量");
	templateBuilder.Append(seedinfo.Dis_DownloadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}	//end if


	}	//end if


	if (uploadratio>seedinfo.UploadRatio)
	{


	if (uploadratio==3.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U30.png\" title=\"按300%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (uploadratio==2.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U20.png\" title=\"按200%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (uploadratio==1.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U16.png\" title=\"按160%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (uploadratio==1.2f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U12.png\" title=\"按120%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}	//end if


	}
	else
	{


	if (seedinfo.UploadRatio==1.0f)
	{


	}
	else if (seedinfo.UploadRatio==2.0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U20.png\" title=\"按200%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (seedinfo.UploadRatio==1.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U16.png\" title=\"按160%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (seedinfo.UploadRatio==1.2f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U12.png\" title=\"按120%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (seedinfo.UploadRatio==0.6f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U6.png\" title=\"按60%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (seedinfo.UploadRatio==0.3f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U3.png\" title=\"按30%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}
	else if (seedinfo.UploadRatio==0f)
	{

	templateBuilder.Append("\r\n                    <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/U0.png\" title=\"按0%计算上传流量");
	templateBuilder.Append(seedinfo.Dis_UploadRatioNote.ToString().Trim());
	templateBuilder.Append("\" />\r\n                ");
	}	//end if


	}	//end if


	if (seedinfo.Upload!=0)
	{


	if (seedinfo.IPv6==1)
	{

	templateBuilder.Append("\r\n                  <span class=\"PrivateBTFontRed\" title=\"该种子只能通过IPv6下载\">IPv6</span>\r\n              ");
	}
	else if (seedinfo.IPv6==0)
	{

	templateBuilder.Append("\r\n                  <span class=\"PrivateBTFontRed\" title=\"该种子只能通过IPv4下载\">IPv4</span>\r\n              ");
	}	//end if


	}	//end if


	if (orderby==6)
	{

	templateBuilder.Append("\r\n                  <span class=\"PrivateBTFontGray\" title=\"累计流量\">");
	templateBuilder.Append(seedinfo.Dis_DownloadTraffic.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}	//end if


	if (orderby==7)
	{

	templateBuilder.Append("\r\n                  <span class=\"PrivateBTFontGray\" title=\"存活时间\">");
	templateBuilder.Append(seedinfo.Dis_TimetoLive.ToString().Trim());
	templateBuilder.Append("</span>\r\n            ");
	}	//end if


	if (userid<=2)
	{


	if (seedinfo.Dis_TrafficCheck=="blue")
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/check_blue.png\" title=\"上传比下载偏低\"/>\r\n              ");
	}
	else if (seedinfo.Dis_TrafficCheck=="green")
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/check_green.png\" title=\"上传与下载相当\"/>\r\n              ");
	}
	else if (seedinfo.Dis_TrafficCheck=="yellow")
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/check_yellow.png\" title=\"上传过多，注意作弊\"/>\r\n              ");
	}
	else
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/check_red.png\" title=\"上传严重超过下载，核实作弊\"/>\r\n              ");
	}	//end if


	if (seedinfo.Rss_Acc>0)
	{

	templateBuilder.Append("\r\n                  <img class=\"PrivateBTInlineIMG\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images/bt/speedup.png\" title=\"早期加速RSS\"/>\r\n              ");
	}	//end if


	}	//end if

	templateBuilder.Append("       \r\n           ");
	templateBuilder.Append(seedinfo.Dis_RatioNoteAdd.ToString().Trim());
	templateBuilder.Append("</td>    \r\n          ");
	if (showkeepreward==false)
	{

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListReplies\" align=\"right\"><em>");
	templateBuilder.Append(seedinfo.Replies.ToString().Trim());
	templateBuilder.Append("</em><br/>");
	templateBuilder.Append(seedinfo.Views.ToString().Trim());
	templateBuilder.Append("</td>\r\n          <td class=\"PrivateBTSeedListAuthor\" align=\"right\">\r\n              <cite><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("userinfo-");
	templateBuilder.Append(seedinfo.Uid.ToString().Trim());
	templateBuilder.Append(".aspx\" title=\"");
	templateBuilder.Append(seedinfo.UserName.ToString().Trim());
	templateBuilder.Append("\">");
	templateBuilder.Append(seedinfo.UserName.ToString().Trim());
	templateBuilder.Append("</a></cite>\r\n              <em>");
	templateBuilder.Append(seedinfo.Dis_PostDateTime.ToString().Trim());
	templateBuilder.Append("</em></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListReplies\" align=\"right\"><em>");
	templateBuilder.Append(seedinfo.Dis_UserUpTraffic.ToString().Trim());
	templateBuilder.Append("</em><br/>");
	templateBuilder.Append(seedinfo.Dis_UserKeepTime.ToString().Trim());
	templateBuilder.Append("</td>\r\n          <td class=\"PrivateBTSeedListAuthor\" align=\"right\">\r\n              <cite><a href=\"showtopic-83062.aspx\">");
	templateBuilder.Append(seedinfo.Dis_KeepReward_All.ToString().Trim());
	templateBuilder.Append("</a></cite>\r\n              <em>");
	templateBuilder.Append(seedinfo.Dis_KeepRewardFactor.ToString().Trim());
	templateBuilder.Append("</em></td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n        </tr>\r\n      </tbody>\r\n      ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n      <!--SeedList End-->\r\n\r\n       \r\n       \r\n       \r\n       \r\n       \r\n    ");
	if (seedinfolist.Count<=0)
	{

	templateBuilder.Append("\r\n    <tbody><tr><td colspan=\"8\"><div class=\"zerothreads\">没有查找到符合要求<span class=\"StatsGray\">&nbsp;[&nbsp;<b>");
	templateBuilder.Append(searchstat.ToString());
	templateBuilder.Append("</b>&nbsp;]&nbsp;</span>的种子</div></td></tr></tbody>\r\n    ");
	}	//end if


	templateBuilder.Append("      <tbody><tr align=\"right\">\r\n          <td class=\"PrivateBTSeedListGrayLS\"><span>类别</span></td>\r\n          <td class=\"PrivateBTSeedListGrayR\"><a onclick=\"PrivateBTChangeOrderby('size')\" title=\"点击按照种子包含的文件大小排序\">大小</a>/<a onclick=\"PrivateBTChangeOrderby('file')\" title=\"点击按照种子包含的文件数排序\">文件</a></td>\r\n          <td class=\"PrivateBTSeedListGrayRS\"><a onclick=\"PrivateBTChangeOrderby('seed')\" title=\"点击按照正在做种的人数排序\">种子</a>/<a onclick=\"PrivateBTChangeOrderby('completed')\" title=\"点击按照正在下载的人数排序\">下载</a></td>\r\n          <td class=\"PrivateBTSeedListGrayRS\"><a onclick=\"PrivateBTChangeOrderby('finished')\" title=\"点击按照种子的完成数排序\">完成</a>/<a onclick=\"PrivateBTChangeOrderby('live')\" title=\"点击按照种子的生存时间排序\">存活</a></td>\r\n          ");
	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("\r\n            <td width=\"70px\"></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n            <td width=\"50px\"></td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListGrayL\">种子名称</td>\r\n          ");
	if (showkeepreward==false)
	{

	templateBuilder.Append("\r\n          <td class=\"PrivateBTSeedListGrayR\">回复/查看</td>\r\n          <td class=\"PrivateBTSeedListGrayREnd\"><a onclick=\"PrivateBTChangeOrderby('time')\" title=\"点击按照种子种子发布的时间排序\">发布者/发布时间</a></td>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <td width=\"120px\" style=\"text-align: right;color:#666\">真实上传/保种时间</td>\r\n          <td class=\"PrivateBTSeedListGrayREnd\">保种奖励/保种系数</td>\r\n          ");
	}	//end if

	templateBuilder.Append("\r\n        </tr>\r\n      </tbody>");

	templateBuilder.Append("\r\n    \r\n    </table>\r\n    \r\n        ");
	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("\r\n            <div id=\"modlayer\" style=\"display: none;\">\r\n                <input type=\"hidden\" name=\"optgroup\" />\r\n                <input type=\"hidden\" name=\"operat\" />\r\n                <input type=\"hidden\" name=\"winheight\" />\r\n                <a class=\"collapse\" href=\"javascript:;\" onclick=\"$('modlayer').className='collapsed'\">最小化</a>\r\n                <label><input class=\"checkbox\" type=\"checkbox\" name=\"chkall\" onclick=\"if(!($('modcount').innerHTML = modclickcount = checkall(this.form, 'topicid'))) {$('modlayer').style.display = 'none';}\" /> 全选</label>\r\n                <h4><span>选中</span><strong onmouseover=\"$('moremodoption').style.display='block'\" onclick=\"$('modlayer').className=''\" id=\"modcount\"></strong><span>篇: </span></h4>\r\n                <p>\r\n                    <strong><a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seeddelete');return false;\">删除</a></strong>&nbsp;&nbsp;\r\n                    <strong><a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seedban');return false;\">屏蔽</a></strong>\r\n                    <span class=\"pipe\">|</span>\r\n                    <strong><a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seedtop');return false;\">置顶</a></strong>&nbsp;&nbsp;\r\n                    <strong><a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seedratio');return false;\">流量系数</a></strong>\r\n                    <span class=\"pipe\">|</span>\r\n                    <strong><a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seedmove');return false;\">移动</a></strong>&nbsp;&nbsp;\r\n                    <strong><a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seededit');return false;\">编辑</a></strong>\r\n\r\n                </p>\r\n                <div id=\"moremodoption\">\r\n                    <a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seeduntop');return false;\">取消置顶</a>\r\n                    <span class=\"pipe\">|</span>\r\n                    <a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seedaward');return false;\">奖励/处罚</a>\r\n                    <span class=\"pipe\">|</span>\r\n                    <a href=\"javascript:;\" onclick=\"seedmodthreads(1, 'seeddigest');return false;\">精华</a>\r\n                    <span class=\"pipe\">|</span>\r\n                    <a href=\"javascript:;\" onclick=\"seedmodthreads(4, 'seedclose');return false;\">关闭打开</a>\r\n                </div>\r\n            </div>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n  </form>\r\n			\r\n\r\n        \r\n	</div>\r\n</div>\r\n\r\n");
	if (nexturl!="")
	{

	templateBuilder.Append("\r\n<div class=\"big_nextbtn\" title=\"点击 或 按键盘“右方向键” 显示下一页\" onclick=\"location.href='");
	templateBuilder.Append(nexturl.ToString());
	templateBuilder.Append("'\">\r\n  <a href=\"");
	templateBuilder.Append(nexturl.ToString());
	templateBuilder.Append("\"><span style=\"font-size:large;\">下&nbsp;&nbsp;一&nbsp;&nbsp;页&nbsp;&nbsp;▷ </span></a>\r\n</div>\r\n");
	}	//end if

	templateBuilder.Append("\r\n\r\n<div class=\"pages_btns cl\">\r\n	<div class=\"pages\">\r\n		<cite class=\"pageback z\" id=\"visitedforums\"");
	if (showvisitedforumsmenu)
	{

	templateBuilder.Append(" onmouseover=\"$('visitedforums').id = 'visitedforumstmp';this.id = 'visitedforums';showMenu({'ctrlid':this.id, 'pos':'34'});\"");
	}	//end if

	templateBuilder.Append("><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("\">返回</a></cite>\r\n		");
	if (pagecount!=1)
	{

	templateBuilder.Append("\r\n			");
	templateBuilder.Append(pagenumbers.ToString());
	templateBuilder.Append("\r\n			");
	if (pagecount>8)
	{

	templateBuilder.Append("\r\n			<kbd>\r\n			<input name=\"gopage\" type=\"text\" class=\"txt\" id=\"pageidinput2\" title=\"可以输入页码按回车键自动跳转\" value=\"");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append("\" style=\"text-align:center;\" onfocus=\"this.value=this.defaultValue;this.select();\" onKeyDown=\"pageinputOnKeyDown(this,event);\" size=\"2\" maxlength=\"9\" />/ ");
	templateBuilder.Append(pagecount.ToString());
	templateBuilder.Append("</kbd>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			");
	templateBuilder.Append(nextpage.ToString());
	templateBuilder.Append("\r\n		");
	}	//end if

	templateBuilder.Append("\r\n	</div>\r\n");
	if (userid<0||canposttopic)
	{

	string newtopicurl = "";
	

	if (forum.Allowspecialonly<=0)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&forumpage=" + pageid;
	

	}
	else if (1==(forum.Allowpostspecial&1)&&usergroupinfo.Allowpostpoll==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=poll&forumpage=" + pageid;
	

	}
	else if (4==(forum.Allowpostspecial&4)&&usergroupinfo.Allowbonus==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=bonus&forumpage=" + pageid;
	

	}
	else if (16==(forum.Allowpostspecial&16)&&usergroupinfo.Allowdebate==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=debate&forumpage=" + pageid;
	

	}	//end if

	string newtopiconclick = "";
	

	if (forum.Allowspecialonly<=0&&canposttopic)
	{

	 newtopiconclick = "showWindow('newthread', '" + forumpath + "showforum.aspx?forumid=" + forum.Fid + "')";
	

	}	//end if

	bool allowpost = userid<=0&&(forum.Postperm==""?usergroupinfo.Allowpost==0:!Utils.InArray(usergroupid.ToString(),forum.Postperm));
	

	if (allowpost)
	{

	 newtopiconclick = "showWindow('login', '" + forumpath + "login.aspx');hideWindow('register');";
	

	}
	else
	{

	 newtopiconclick = "showWindow('newthread', '" + forumpath + "showforum.aspx?forumid=" + forum.Fid + "')";
	

	}	//end if

	templateBuilder.Append("\r\n	<span ");
	if (userid>0)
	{

	templateBuilder.Append(" onmouseover=\"if($('newspecial2_menu')!=null&&$('newspecial_menu').childNodes.length>0)  showMenu(this.id);\"");
	}	//end if

	templateBuilder.Append(" id=\"newspecial2\">\r\n        <a title=\"发新话题\" id=\"newtopic2\" href=\"");
	templateBuilder.Append(newtopicurl.ToString());
	templateBuilder.Append("\" onclick=\"");
	templateBuilder.Append(newtopiconclick.ToString());
	templateBuilder.Append("\">\r\n            <img alt=\"发新话题\" src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/newtopic.png\"  style=\"display:inline\"/></a>\r\n    </span>\r\n");
	}	//end if

	templateBuilder.Append("\r\n</div>\r\n\r\n");
	if (canquickpost)
	{


	templateBuilder.Append("<script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/post.js\"></");
	templateBuilder.Append("script>\r\n");	string seditorid = "infloatquickposttopic";
	

	if (infloat!=1)
	{

	 seditorid = "quickposttopic";
	

	}	//end if

	string poster = "";
	
	int postid = 0;
	
	int postlayer = 0;
	
	templateBuilder.Append("\r\n<form method=\"post\" name=\"postform\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("form\" action=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("posttopic.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\" enctype=\"multipart/form-data\" onsubmit=\"return fastvalidate(this,'");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\">\r\n<div id=\"quickpost\" class=\"quickpost cl ");
	if (infloat!=1)
	{

	templateBuilder.Append("main");
	}	//end if

	templateBuilder.Append("\">\r\n	");
	if (infloat!=1)
	{

	templateBuilder.Append("\r\n	<h4 class=\"bm_h\">\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<h4 class=\"flb\">\r\n	");
	}	//end if


	if (infloat==1)
	{

	templateBuilder.Append("\r\n	<span class=\"y\">\r\n		<a title=\"关闭\" onclick=\"clearTimeout(showsmiletimeout);hideWindow('newthread');\" class=\"flbc\" href=\"javascript:;\">关闭</a>\r\n	</span>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	<em>快速发帖</em>\r\n    ");
	if (infloat==1 && needaudit)
	{

	templateBuilder.Append("\r\n    <span class=\"needverify\">需审核</span>\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n	</h4>\r\n	<div class=\"bm_inner c cl\">\r\n		");
	if (infloat!=1)
	{


	if (quickeditorad!="")
	{

	templateBuilder.Append("\r\n		<div class=\"leaderboard\">");
	templateBuilder.Append(quickeditorad.ToString());
	templateBuilder.Append("</div>\r\n		");
	}	//end if


	}
	else
	{


	if (quickeditorad!="")
	{

	templateBuilder.Append("\r\n		<div class=\"leaderboard\">");
	templateBuilder.Append(quickeditorad.ToString());
	templateBuilder.Append("</div>\r\n		");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n		<div class=\"pbt cl\">\r\n			");
	if (forum.Applytopictype==1 && topictypeselectoptions!="")
	{

	templateBuilder.Append("\r\n			<div class=\"ftid\">\r\n				<select name=\"typeid\" id=\"typeid\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"1\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"5\"");
	}	//end if

	templateBuilder.Append(">");
	templateBuilder.Append(topictypeselectoptions.ToString());
	templateBuilder.Append("</select>\r\n				<script type=\"text/javascript\">simulateSelect('typeid');</");
	templateBuilder.Append("script>\r\n			</div>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			<input type=\"text\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("title\" name=\"");
	templateBuilder.Append(config.Antispamposttitle.ToString().Trim());
	templateBuilder.Append("\" size=\"60\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"2\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"6\"");
	}	//end if

	templateBuilder.Append(" value=\"\" class=\"txt postpx\"/>\r\n            标题最多为60个字符，还可输入<b><span id=\"chLeft\">60</span></b>\r\n            <script type=\"text/javascript\">checkLength($('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("title'), 60); //检查标题长度</");
	templateBuilder.Append("script>\r\n			<em id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("validatemessage\"></em>\r\n		</div>\r\n		<div class=\"pbt cl\">\r\n			<span>\r\n			<input type=\"hidden\" value=\"usergroupinfo.allowhtml}\" name=\"htmlon\" id=\"htmlon\"/>\r\n			<input type=\"hidden\" value=\"");
	templateBuilder.Append(parseurloff.ToString());
	templateBuilder.Append("\" name=\"parseurloff\" id=\"parseurloff\"/>\r\n			<input type=\"hidden\" value=\"");
	templateBuilder.Append(smileyoff.ToString());
	templateBuilder.Append("\" name=\"smileyoff\" id=\"smileyoff\" />\r\n			<input type=\"hidden\" value=\"");
	templateBuilder.Append(bbcodeoff.ToString());
	templateBuilder.Append("\" name=\"bbcodeoff\" id=\"bbcodeoff\"/>\r\n			<input type=\"hidden\" value=\"");
	templateBuilder.Append(usesig.ToString());
	templateBuilder.Append("\" name=\"usesig\" id=\"usesig\"/>\r\n			</span>\r\n			<script type=\"text/javascript\">\r\n				var bbinsert = parseInt('1');\r\n				var smiliesCount = 24;\r\n				var colCount = 8;\r\n			</");
	templateBuilder.Append("script>\r\n			<div ");
	if (infloat!=1)
	{

	templateBuilder.Append("style=\"margin-right:170px;\" ");
	}
	else
	{

	templateBuilder.Append("style=\"width:600px;\"");
	}	//end if

	templateBuilder.Append(">\r\n			");	char comma = ',';
	

	templateBuilder.Append("<link href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/seditor.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<div class=\"editor_tb\">\r\n	<span class=\"y\">\r\n		");
	if (topicid>0)
	{

	string replyurl = rooturl+"postreply.aspx?topicid="+topicid+"&forumpage="+forumpageid;
	

	if (postid>0)
	{

	 replyurl = replyurl+"&postid="+postid+"&postlayer="+postlayer+"&poster="+Utils.UrlEncode(poster);
	

	}	//end if

	templateBuilder.Append("\r\n		    <a onclick=\"switchAdvanceMode(this.href, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');doane(event);\" href=\"");
	templateBuilder.Append(replyurl.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/external2.png\" alt=\"高级编辑器\" class=\"vm\"/>高级编辑器</a>\r\n		");
	}
	else
	{

	templateBuilder.Append("\r\n		    <a onclick=\"switchAdvanceMode(this.href, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');doane(event);\" href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("posttopic.aspx?forumid=");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("&forumpage=");
	templateBuilder.Append(forumpageid.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/external2.png\" alt=\"高级编辑器\" class=\"vm\"/>高级编辑器</a>\r\n		");
	}	//end if


	if (infloat!=1)
	{


	if (userid<0||canposttopic)
	{

	string newtopicurl = "";
	

	if (forum.Allowspecialonly<=0)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&forumpage=" + pageid;
	

	}
	else if (1==(forum.Allowpostspecial&1)&&usergroupinfo.Allowpostpoll==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=poll&forumpage=" + pageid;
	

	}
	else if (4==(forum.Allowpostspecial&4)&&usergroupinfo.Allowbonus==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=reward&forumpage=" + pageid;
	

	}
	else if (16==(forum.Allowpostspecial&16)&&usergroupinfo.Allowdebate==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=debate&forumpage=" + pageid;
	

	}	//end if

	string newtopiconclick = "";
	

	if (forum.Allowspecialonly<=0&&canposttopic)
	{

	 newtopiconclick = "showWindow('newthread', '" + forumpath + "showforum.aspx?forumid=" + forum.Fid + "')";
	

	}	//end if


	if (userid<=0)
	{

	 newtopiconclick = "showWindow('login', '" + forumpath + "login.aspx');hideWindow('register');";
	

	}	//end if


	}	//end if


	}	//end if

	templateBuilder.Append("\r\n	</span>\r\n	<div>\r\n		<a href=\"javascript:;\" title=\"粗体\" class=\"tb_bold\" onclick=\"seditor_insertunit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', '[b]', '[/b]')\">B</a>\r\n		<a href=\"javascript:;\" title=\"颜色\" class=\"tb_color\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("forecolor\" onclick=\"showMenu(this.id, true, 0, 2)\">Color</a>\r\n		");	string coloroptions = "Black,Sienna,DarkOliveGreen,DarkGreen,DarkSlateBlue,Navy,Indigo,DarkSlateGray,DarkRed,DarkOrange,Olive,Green,Teal,Blue,SlateGray,DimGray,Red,SandyBrown,YellowGreen,SeaGreen,MediumTurquoise,RoyalBlue,Purple,Gray,Magenta,Orange,Yellow,Lime,Cyan,DeepSkyBlue,DarkOrchid,Silver,Pink,Wheat,LemonChiffon,PaleGreen,PaleTurquoise,LightBlue,Plum,White";
	
	templateBuilder.Append("\r\n		<div class=\"popupmenu_popup tb_color\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("forecolor_menu\" style=\"display: none\">\r\n			");
	int colorname__loop__id=0;
	foreach(string colorname in coloroptions.Split(comma))
	{
		colorname__loop__id++;

	templateBuilder.Append("\r\n				<input type=\"button\" style=\"background-color: ");
	templateBuilder.Append(colorname.ToString());
	templateBuilder.Append("\" onclick=\"seditor_insertunit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', '[color=");
	templateBuilder.Append(colorname.ToString());
	templateBuilder.Append("]', '[/color]')\" />");
	if (colorname__loop__id%8==0)
	{

	templateBuilder.Append("<br />");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n		</div>\r\n		<a href=\"javascript:;\" title=\"图片\" class=\"tb_img\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("img\" onclick=\"seditor_menu('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', 'img')\">Image</a>\r\n		<a href=\"javascript:;\" title=\"链接\" class=\"tb_link\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("url\" onclick=\"seditor_menu('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', 'url')\">Link</a>\r\n		<a href=\"javascript:;\" title=\"引用\" class=\"tb_quote\" onclick=\"seditor_insertunit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', '[quote]', '[/quote]')\">Quote</a>\r\n		<a href=\"javascript:;\" title=\"代码\" class=\"tb_code\" onclick=\"seditor_insertunit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', '[code]', '[/code]')\">Code</a>\r\n	");
	if (config.Smileyinsert==1 && forum.Allowsmilies==1)
	{

	templateBuilder.Append("\r\n		<a href=\"javascript:;\" class=\"tb_smilies\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("smilies\" onclick=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("getSmilies(");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("getSmilies_callback);showMenu({'ctrlid':this.id, 'evt':'click', 'layer':2})\">Smilies</a>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	</div>\r\n</div>\r\n");
	if (config.Smileyinsert==1 && forum.Allowsmilies==1)
	{

	templateBuilder.Append("\r\n	<div class=\"smilies\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("smilies_menu\" style=\"display:none;width:315px;\">\r\n		<div class=\"smilieslist\">\r\n			");	string defaulttypname = string.Empty;
	
	templateBuilder.Append("\r\n			<div id=\"smiliesdiv\">\r\n				<div class=\"smiliesgroup\" style=\"margin-right: 0pt;\">\r\n					<ul>\r\n					");
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

	templateBuilder.Append("\r\n						<li id=\"t_s_" + stype__loop__id.ToString() + "\"><a id=\"s_" + stype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"showsmiles(" + stype__loop__id.ToString() + ", '" + stype["code"].ToString().Trim() + "', 1, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\" class=\"current\">" + stype["code"].ToString().Trim() + "</a></li>\r\n						");
	}
	else
	{

	templateBuilder.Append("\r\n						<li id=\"t_s_" + stype__loop__id.ToString() + "\"><a id=\"s_" + stype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"showsmiles(" + stype__loop__id.ToString() + ", '" + stype["code"].ToString().Trim() + "', 1, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\">" + stype["code"].ToString().Trim() + "</a></li>\r\n						");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n					</ul>\r\n				 </div>\r\n				 <div style=\"clear: both;\" class=\"float_typeid\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie\"></div>\r\n				 <table class=\"smilieslist_table\" id=\"s_preview_table\" style=\"display: none\"><tr><td class=\"smilieslist_preview\" id=\"s_preview\"></td></tr></table>\r\n				 <div id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie_pagenum\" class=\"smilieslist_page\">&nbsp;</div>\r\n			</div>\r\n		</div>\r\n		<script type=\"text/javascript\" reload=\"1\">\r\n			function ");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("getSmilies(func){\r\n				if($('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie').innerHTML !='' && $('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie').innerHTML != '正在加载表情...')\r\n					return;\r\n				var c = \"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("tools/ajax.aspx?t=smilies\";\r\n				_sendRequest(c,function(d){var e={};try{e=eval(\"(\"+d+\")\")}catch(f){e={}}var h=e?e:null;func(h);e=null;func=null},false,true);\r\n				setTimeout(\"if($('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie').innerHTML=='')$('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie').innerHTML = '正在加载表情...'\", 2000);\r\n			}\r\n			function ");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("getSmilies_callback(obj) {\r\n				smilies_HASH = obj; \r\n				showsmiles(1, '");
	templateBuilder.Append(defaulttypname.ToString());
	templateBuilder.Append("', 1, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\r\n			}\r\n		</");
	templateBuilder.Append("script>\r\n	</div>\r\n");
	}	//end if



	templateBuilder.Append("\r\n			<div class=\"postarea cl\">\r\n				<div class=\"postinner\">\r\n				");
	if (canposttopic)
	{

	templateBuilder.Append("\r\n				<textarea ");
	if (infloat!=1)
	{

	templateBuilder.Append("rows=\"5\"");
	}
	else
	{

	templateBuilder.Append("rows=\"7\"");
	}	//end if

	templateBuilder.Append(" cols=\"80\" name=\"");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("message\" onKeyDown=\"seditor_ctlent(event, 'fastvalidate($(\\'");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("form\\'),\\'");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("\\')','");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"3\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"6\"");
	}	//end if

	templateBuilder.Append("  style=\"background-image:url(" + quickbgad[1].ToString().Trim() + ");background-repeat:no-repeat;background-position:50% 50%;\" ");
	if (quickbgad[0].ToString().Trim()!="")
	{

	templateBuilder.Append(" onfocus=\"$('adlinkbtn').style.display='';$('closebtn').style.display='';this.onfocus=null;\"");
	}	//end if

	templateBuilder.Append("></textarea>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n				<div class=\"hm p_login cl\">你需要登录后才可以发帖 <a class=\"xg2\" onclick=\"hideWindow('register');showWindow('login', this.href);\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("login.aspx\">登录</a> | <a class=\"xg2\" onclick=\"hideWindow('login');showWindow('register', this.href);\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("register.aspx\">注册</a></div>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				</div>\r\n			</div>\r\n			</div>\r\n		</div>\r\n		");
	if (canposttopic)
	{

	templateBuilder.Append("\r\n        <div id=\"PrivateBTQuickQuickTopicSmilesShowDiv\" class=\"PrivateBTQuickSmilesShowDiv\">\r\n            <div class=\"smiliesgroup\" style=\"margin-right: 0pt;\">\r\n            <ul>\r\n                ");	string BTQuickQuickTopicdefaulttypname = string.Empty;
	

	int BTQuickQuickTopicstype__loop__id=0;
	foreach(DataRow BTQuickQuickTopicstype in Caches.GetSmilieTypesCache().Rows)
	{
		BTQuickQuickTopicstype__loop__id++;


	if (BTQuickQuickTopicstype__loop__id==1)
	{

	 BTQuickQuickTopicdefaulttypname = BTQuickQuickTopicstype["code"].ToString().Trim();
	

	}	//end if


	if (BTQuickQuickTopicstype__loop__id==1)
	{

	templateBuilder.Append("\r\n                  <li id=\"BTQuickQuickTopict_s_" + BTQuickQuickTopicstype__loop__id.ToString() + "\"><a id=\"BTQuickQuickTopics_" + BTQuickQuickTopicstype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"BTQuickshowsmiles(" + BTQuickQuickTopicstype__loop__id.ToString() + ", '" + BTQuickQuickTopicstype["code"].ToString().Trim() + "', 1, 'BTQuickQuickTopic', '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\" class=\"current\">" + BTQuickQuickTopicstype["code"].ToString().Trim() + "</a></li>\r\n                  ");
	}
	else
	{

	templateBuilder.Append("\r\n                  <li id=\"BTQuickQuickTopict_s_" + BTQuickQuickTopicstype__loop__id.ToString() + "\"><a id=\"BTQuickQuickTopics_" + BTQuickQuickTopicstype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"BTQuickshowsmiles(" + BTQuickQuickTopicstype__loop__id.ToString() + ", '" + BTQuickQuickTopicstype["code"].ToString().Trim() + "', 1, 'BTQuickQuickTopic', '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\">" + BTQuickQuickTopicstype["code"].ToString().Trim() + "</a></li>\r\n                  ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n            </ul>\r\n           </div>\r\n           <div style=\"clear: both;\" class=\"float_typeid\" id=\"BTQuickQuickTopicshowsmilie\"></div>\r\n           <div id=\"BTQuickQuickTopicshowsmilie_pagenum\" class=\"smilieslist_page\">&nbsp;</div>\r\n           <script type=\"text/javascript\" reload=\"1\">\r\n                var BTQuickQuickTopicsmilies_HASH = {};\r\n                var showsmiletimeout;\r\n                function BTQuickQuickTopicgetSmilies(func){\r\n\r\n                  if($('BTQuickQuickTopicshowsmilie').innerHTML !='' && $('BTQuickQuickTopicshowsmilie').innerHTML != '正在加载表情...')\r\n                    return;\r\n                  var c = \"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("tools/ajax.aspx?t=smilies\";\r\n                  _sendRequest(c,function(d){var e={};try{e=eval(\"(\"+d+\")\")}catch(f){e={}}var h=e?e:null;func(h);e=null;func=null},false,true);\r\n                  showsmiletimeout = setTimeout(\"if($('BTQuickQuickTopicshowsmilie').innerHTML=='')$('BTQuickQuickTopicshowsmilie').innerHTML = '正在加载表情...'\", 2000);\r\n                  \r\n                }\r\n                function BTQuickQuickTopicgetSmilies_callback(obj) {\r\n\r\n                  BTQuickQuickTopicsmilies_HASH = obj; \r\n                  BTQuickshowsmiles(1, '");
	templateBuilder.Append(BTQuickQuickTopicdefaulttypname.ToString());
	templateBuilder.Append("', 1, 'BTQuickQuickTopic', '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\r\n                  clearTimeout(showsmiletimeout);\r\n                }\r\n                \r\n                BTQuickQuickTopicgetSmilies(BTQuickQuickTopicgetSmilies_callback);\r\n          </");
	templateBuilder.Append("script>\r\n        </div>\r\n    ");
	}	//end if


	if (isseccode)
	{

	templateBuilder.Append("\r\n		<div class=\"pbt\" style=\"position: relative;\">\r\n		");
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

	templateBuilder.Append("\r\n		</div>\r\n		");
	}	//end if

	templateBuilder.Append("\r\n		<div class=\"pbt\">\r\n		    ");
	if (canposttopic)
	{

	templateBuilder.Append("\r\n			<button type=\"button\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("submit\" onclick=\"fastsubmit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\" name=\"topicsubmit\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"5\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"8\"");
	}	//end if

	templateBuilder.Append(" class=\"pn\"><span>发表帖子</span></button> <span class=\"grayfont\">[Ctrl+Enter快速发布]</span>\r\n			");
	}
	else
	{

	templateBuilder.Append("\r\n			<button type=\"button\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("submit\" name=\"topicsubmit\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"5\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"8\"");
	}	//end if

	templateBuilder.Append(" onclick=\"hideWindow('register');showWindow('login', '");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("login.aspx');\" class=\"pn\"><span>发表帖子</span></button>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			<a href=\"###\" id=\"adlinkbtn\" style=\"display:none;\" onclick=\"window.open('" + quickbgad[0].ToString().Trim() + "','_blank');\">查看背景广告</a>\r\n			<a href=\"###\" id=\"closebtn\" style=\"display:none;\" onclick=\"$('quickpostmessage').style.background='';this.style.display='none';$('adlinkbtn').style.display='none';\">隐藏</a>\r\n			<input type=\"hidden\" id=\"postbytopictype\" name=\"postbytopictype\" value=\"");
	templateBuilder.Append(forum.Postbytopictype.ToString().Trim());
	templateBuilder.Append("\" tabindex=\"3\" />\r\n		</div>\r\n	</div>\r\n</div>\r\n</form>");


	}	//end if


	if (config.Whosonlinestatus!=0 && config.Whosonlinestatus!=1)
	{

	templateBuilder.Append("\r\n	<div class=\"bm cl\" id=\"online\">\r\n		<div class=\"bm_h\">		\r\n			<span class=\"l_action\" style=\"display:none\">\r\n				");
	if (DNTRequest.GetString("showonline")=="no")
	{

	templateBuilder.Append("\r\n					<a href=\"showforum.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&showonline=yes#online\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/collapsed_no.gif\" alt=\"收起\" />\r\n				");
	}
	else
	{

	templateBuilder.Append("\r\n					<a href=\"showforum.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("&showonline=no#online\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/collapsed_yes.gif\" alt=\"展开\" />\r\n				");
	}	//end if

	templateBuilder.Append("</a>\r\n			</span>\r\n			<h3>\r\n				<strong>在线用户</strong> - <em id=\"forumtotalonline\">");
	templateBuilder.Append(forumtotalonline.ToString());
	templateBuilder.Append("</em> 人在线<span id=\"invisible\"></span>\r\n			</h3>\r\n		</div>\r\n		<dl id=\"onlinelist\">\r\n			<dt style=\"display:none\">");
	templateBuilder.Append(onlineiconlist.ToString());
	templateBuilder.Append("</dt>\r\n			");
	if (showforumonline)
	{

	templateBuilder.Append("\r\n			<dd>\r\n			<ul class=\"userlist cl\">\r\n				");	int invisiblecount = 0;
	

	if (forumtotalonline!=0)
	{


	int onlineuser__loop__id=0;
	foreach(OnlineUserInfo onlineuser in onlineuserlist)
	{
		onlineuser__loop__id++;


	if (onlineuser.Invisible==1)
	{

	 invisiblecount = invisiblecount + 1;
	
	templateBuilder.Append("\r\n						<li style=\"overflow:hidden;text-align:center;height:70px;width:80px;line-height:60px\">(隐身会员)</li>\r\n					");
	}
	else
	{

	templateBuilder.Append("\r\n						<li style=\"overflow:hidden;text-align:center;height:70px;width:80px\">\r\n							");	string avatarurl = Avatars.GetAvatarUrl(onlineuser.Userid, AvatarSize.Small);
	
	templateBuilder.Append("\r\n								<img src=\"");
	templateBuilder.Append(avatarurl.ToString());
	templateBuilder.Append("\" onerror=\"this.onerror=null;this.src='");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/noavatar_small.gif';\"  alt=\"头像\" id=\"memberinfo_" + onlineuser__loop__id.ToString() + "\" style=\"border:1px solid #E8E8E8;padding:1px;width:48px;height:48px;\" />\r\n							");
	if (onlineuser.Userid==-1)
	{

	templateBuilder.Append("\r\n								<p>");
	templateBuilder.Append(onlineuser.Username.ToString().Trim());
	templateBuilder.Append("</p>\r\n							");
	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(onlineuser.Userid);
	
	templateBuilder.Append("\r\n								<p><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\" target=\"_blank\">");
	templateBuilder.Append(onlineuser.Username.ToString().Trim());
	templateBuilder.Append("</a></p>\r\n							");
	}	//end if

	templateBuilder.Append("\r\n						</li>\r\n					");
	}	//end if


	}	//end loop


	if (invisiblecount>0)
	{

	templateBuilder.Append("\r\n					<script type=\"text/javascript\">$('invisible').innerHTML = '(");
	templateBuilder.Append(invisiblecount.ToString());
	templateBuilder.Append("' + \" 隐身)\";</");
	templateBuilder.Append("script>\r\n				");
	}	//end if


	}
	else
	{

	templateBuilder.Append("\r\n                  <script type=\"text/javascript\">$('forumtotalonline').innerHTML = parseInt($('forumtotalonline').innerHTML)+1;</");
	templateBuilder.Append("script>\r\n					<li style=\"overflow:hidden;text-align:center;height:70px;width:80px\">\r\n						");	string avatarurl = Avatars.GetAvatarUrl(userid, AvatarSize.Small);
	
	templateBuilder.Append("\r\n							<img src=\"");
	templateBuilder.Append(avatarurl.ToString());
	templateBuilder.Append("\" onerror=\"this.onerror=null;this.src='");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/noavatar_small.gif';\"  alt=\"头像\" style=\"border:1px solid #E8E8E8;padding:1px;width:48px;height:48px;\" />\r\n						");
	if (userid==-1)
	{

	templateBuilder.Append("\r\n							<p>");
	templateBuilder.Append(username.ToString());
	templateBuilder.Append("</p>\r\n						");
	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(userid);
	
	templateBuilder.Append("\r\n							<p><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append(aspxrewriteurl.ToString());
	templateBuilder.Append("\" target=\"_blank\">");
	templateBuilder.Append(username.ToString());
	templateBuilder.Append("</a></p>\r\n						");
	}	//end if

	templateBuilder.Append("\r\n					</li>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n			</ul>\r\n			</dd>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n		</dl>\r\n	</div>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n</div>\r\n");
	if (userid<0||canposttopic)
	{

	templateBuilder.Append("\r\n	<ul class=\"popupmenu_popup newspecialmenu\" id=\"newspecial_menu\" style=\"display: none\">\r\n	");
	if (forum.Allowspecialonly<=0)
	{

	templateBuilder.Append("\r\n	<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("posttopic.aspx?forumid=");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("&forumpage=");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append("\" >发新主题</a></li>\r\n	");
	}	//end if


	if ((forum.Allowpostspecial&1)==1 && usergroupinfo.Allowpostpoll==1)
	{

	templateBuilder.Append("\r\n		<li class=\"poll\"><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("posttopic.aspx?forumid=");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("&type=poll&forumpage=");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append("\">发布投票</a></li>\r\n	");
	}	//end if


	if ((forum.Allowpostspecial&4)==4 && usergroupinfo.Allowbonus==1)
	{

	templateBuilder.Append("\r\n		<li class=\"reward\"><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("posttopic.aspx?forumid=");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("&type=bonus&forumpage=");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append("\">发布悬赏</a></li>\r\n	");
	}	//end if


	if ((forum.Allowpostspecial&16)==16 && usergroupinfo.Allowdebate==1)
	{

	templateBuilder.Append("\r\n		<li class=\"debate\"><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("posttopic.aspx?forumid=");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("&type=debate&forumpage=");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append("\" >发起辩论</a></li>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	</ul>\r\n	<ul class=\"popupmenu_popup newspecialmenu\" id=\"newspecial2_menu\" style=\"display: none\">\r\n	</ul>\r\n    <ul class=\"popupmenu_popup newspecialmenu\" id=\"seditor_newspecial_menu\" style=\"display: none\">\r\n	</ul>\r\n	<script type=\"text/javascript\">\r\n	    $('newspecial2_menu').innerHTML = $('newspecial_menu').innerHTML;\r\n	    $('seditor_newspecial_menu').innerHTML = $('newspecial_menu').innerHTML;\r\n	</");
	templateBuilder.Append("script>\r\n");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n	<script type=\"text/javascript\">\r\n		var maxpage = parseInt('");
	templateBuilder.Append(pagecount.ToString());
	templateBuilder.Append("');\r\n		var pageid = parseInt('");
	templateBuilder.Append(pageid.ToString());
	templateBuilder.Append("');\r\n		if(maxpage > 1) {\r\n			document.onkeyup = function(e){\r\n				e = e ? e : window.event;\r\n				var tagname = is_ie ? e.srcElement.tagName : e.target.tagName;\r\n				if(tagname == 'INPUT' || tagname == 'TEXTAREA') return;\r\n				actualCode = e.keyCode ? e.keyCode : e.charCode;\r\n				if(pageid < maxpage && actualCode == 39) {\r\n					window.location = 'showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&seedstat=0&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("&page=");
	templateBuilder.Append((pageid+1).ToString());
	templateBuilder.Append("';\r\n				}\r\n				if(pageid > 1 && actualCode == 37) {\r\n					window.location = 'showseeds.aspx?type=");
	templateBuilder.Append(typestr.ToString());
	templateBuilder.Append("&orderby=");
	templateBuilder.Append(orderby.ToString());
	templateBuilder.Append("&asc=");
	templateBuilder.Append(asc.ToString());
	templateBuilder.Append("&keywords=");
	templateBuilder.Append(PTTools.Escape(keywords).ToString().Trim());
	templateBuilder.Append("&seedstat=0&userstat=");
	templateBuilder.Append(userstat.ToString());
	templateBuilder.Append("&username=");
	templateBuilder.Append(searchusername.ToString());
	templateBuilder.Append("&keywordsmode=");
	templateBuilder.Append(keywordsmode.ToString());
	templateBuilder.Append("&page=");
	templateBuilder.Append((pageid-1).ToString());
	templateBuilder.Append("';\r\n				}\r\n			}\r\n		}\r\n	</");
	templateBuilder.Append("script>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n</div>\r\n");
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


	if (config.Forumjump==1)
	{

	templateBuilder.Append("\r\n	");
	templateBuilder.Append(navhomemenu.ToString());
	templateBuilder.Append("\r\n");
	}	//end if


	if (showvisitedforumsmenu)
	{

	templateBuilder.Append("\r\n<div class=\"p_pop\" id=\"visitedforums_menu\" style=\"display: none\">\r\n	<h3 class=\"xi1\">浏览过的版块</h3>\r\n	<ul>\r\n	");
	int simpforuminfo__loop__id=0;
	foreach(SimpleForumInfo simpforuminfo in visitedforums)
	{
		simpforuminfo__loop__id++;


	if (simpforuminfo.Fid!=forumid)
	{

	templateBuilder.Append("\r\n		<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append(simpforuminfo.Url.ToString().Trim());
	templateBuilder.Append("\">");
	templateBuilder.Append(simpforuminfo.Name.ToString().Trim());
	templateBuilder.Append("</a></li>\r\n		");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n	</ul>\r\n</div>\r\n");
	}	//end if



	if (infloat!=1)
	{


	if (floatad!="")
	{

	templateBuilder.Append("\r\n	<script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_floatadv.js\"></");
	templateBuilder.Append("script>\r\n	");
	templateBuilder.Append(floatad.ToString());
	templateBuilder.Append("\r\n	<script type=\"text/javascript\">theFloaters.play();</");
	templateBuilder.Append("script>\r\n");
	}
	else if (doublead!="")
	{

	templateBuilder.Append("\r\n	<script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/template_floatadv.js\"></");
	templateBuilder.Append("script>\r\n	");
	templateBuilder.Append(doublead.ToString());
	templateBuilder.Append("\r\n	<script type=\"text/javascript\">theFloaters.play();</");
	templateBuilder.Append("script>\r\n");
	}	//end if


	}	//end if



	templateBuilder.Append("\r\n");
	templateBuilder.Append(mediaad.ToString());
	templateBuilder.Append("\r\n");
	}
	else
	{


	templateBuilder.Append("<script type=\"text/javascript\"  src=\"");
	templateBuilder.Append(jsdir.ToString());
	templateBuilder.Append("/post.js\"></");
	templateBuilder.Append("script>\r\n");	string seditorid = "infloatquickposttopic";
	

	if (infloat!=1)
	{

	 seditorid = "quickposttopic";
	

	}	//end if

	string poster = "";
	
	int postid = 0;
	
	int postlayer = 0;
	
	templateBuilder.Append("\r\n<form method=\"post\" name=\"postform\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("form\" action=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("posttopic.aspx?forumid=");
	templateBuilder.Append(forumid.ToString());
	templateBuilder.Append("\" enctype=\"multipart/form-data\" onsubmit=\"return fastvalidate(this,'");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\">\r\n<div id=\"quickpost\" class=\"quickpost cl ");
	if (infloat!=1)
	{

	templateBuilder.Append("main");
	}	//end if

	templateBuilder.Append("\">\r\n	");
	if (infloat!=1)
	{

	templateBuilder.Append("\r\n	<h4 class=\"bm_h\">\r\n	");
	}
	else
	{

	templateBuilder.Append("\r\n	<h4 class=\"flb\">\r\n	");
	}	//end if


	if (infloat==1)
	{

	templateBuilder.Append("\r\n	<span class=\"y\">\r\n		<a title=\"关闭\" onclick=\"clearTimeout(showsmiletimeout);hideWindow('newthread');\" class=\"flbc\" href=\"javascript:;\">关闭</a>\r\n	</span>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	<em>快速发帖</em>\r\n    ");
	if (infloat==1 && needaudit)
	{

	templateBuilder.Append("\r\n    <span class=\"needverify\">需审核</span>\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n	</h4>\r\n	<div class=\"bm_inner c cl\">\r\n		");
	if (infloat!=1)
	{


	if (quickeditorad!="")
	{

	templateBuilder.Append("\r\n		<div class=\"leaderboard\">");
	templateBuilder.Append(quickeditorad.ToString());
	templateBuilder.Append("</div>\r\n		");
	}	//end if


	}
	else
	{


	if (quickeditorad!="")
	{

	templateBuilder.Append("\r\n		<div class=\"leaderboard\">");
	templateBuilder.Append(quickeditorad.ToString());
	templateBuilder.Append("</div>\r\n		");
	}	//end if


	}	//end if

	templateBuilder.Append("\r\n		<div class=\"pbt cl\">\r\n			");
	if (forum.Applytopictype==1 && topictypeselectoptions!="")
	{

	templateBuilder.Append("\r\n			<div class=\"ftid\">\r\n				<select name=\"typeid\" id=\"typeid\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"1\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"5\"");
	}	//end if

	templateBuilder.Append(">");
	templateBuilder.Append(topictypeselectoptions.ToString());
	templateBuilder.Append("</select>\r\n				<script type=\"text/javascript\">simulateSelect('typeid');</");
	templateBuilder.Append("script>\r\n			</div>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			<input type=\"text\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("title\" name=\"");
	templateBuilder.Append(config.Antispamposttitle.ToString().Trim());
	templateBuilder.Append("\" size=\"60\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"2\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"6\"");
	}	//end if

	templateBuilder.Append(" value=\"\" class=\"txt postpx\"/>\r\n            标题最多为60个字符，还可输入<b><span id=\"chLeft\">60</span></b>\r\n            <script type=\"text/javascript\">checkLength($('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("title'), 60); //检查标题长度</");
	templateBuilder.Append("script>\r\n			<em id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("validatemessage\"></em>\r\n		</div>\r\n		<div class=\"pbt cl\">\r\n			<span>\r\n			<input type=\"hidden\" value=\"usergroupinfo.allowhtml}\" name=\"htmlon\" id=\"htmlon\"/>\r\n			<input type=\"hidden\" value=\"");
	templateBuilder.Append(parseurloff.ToString());
	templateBuilder.Append("\" name=\"parseurloff\" id=\"parseurloff\"/>\r\n			<input type=\"hidden\" value=\"");
	templateBuilder.Append(smileyoff.ToString());
	templateBuilder.Append("\" name=\"smileyoff\" id=\"smileyoff\" />\r\n			<input type=\"hidden\" value=\"");
	templateBuilder.Append(bbcodeoff.ToString());
	templateBuilder.Append("\" name=\"bbcodeoff\" id=\"bbcodeoff\"/>\r\n			<input type=\"hidden\" value=\"");
	templateBuilder.Append(usesig.ToString());
	templateBuilder.Append("\" name=\"usesig\" id=\"usesig\"/>\r\n			</span>\r\n			<script type=\"text/javascript\">\r\n				var bbinsert = parseInt('1');\r\n				var smiliesCount = 24;\r\n				var colCount = 8;\r\n			</");
	templateBuilder.Append("script>\r\n			<div ");
	if (infloat!=1)
	{

	templateBuilder.Append("style=\"margin-right:170px;\" ");
	}
	else
	{

	templateBuilder.Append("style=\"width:600px;\"");
	}	//end if

	templateBuilder.Append(">\r\n			");	char comma = ',';
	

	templateBuilder.Append("<link href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/seditor.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<div class=\"editor_tb\">\r\n	<span class=\"y\">\r\n		");
	if (topicid>0)
	{

	string replyurl = rooturl+"postreply.aspx?topicid="+topicid+"&forumpage="+forumpageid;
	

	if (postid>0)
	{

	 replyurl = replyurl+"&postid="+postid+"&postlayer="+postlayer+"&poster="+Utils.UrlEncode(poster);
	

	}	//end if

	templateBuilder.Append("\r\n		    <a onclick=\"switchAdvanceMode(this.href, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');doane(event);\" href=\"");
	templateBuilder.Append(replyurl.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/external2.png\" alt=\"高级编辑器\" class=\"vm\"/>高级编辑器</a>\r\n		");
	}
	else
	{

	templateBuilder.Append("\r\n		    <a onclick=\"switchAdvanceMode(this.href, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');doane(event);\" href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("posttopic.aspx?forumid=");
	templateBuilder.Append(forum.Fid.ToString().Trim());
	templateBuilder.Append("&forumpage=");
	templateBuilder.Append(forumpageid.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(imagedir.ToString());
	templateBuilder.Append("/external2.png\" alt=\"高级编辑器\" class=\"vm\"/>高级编辑器</a>\r\n		");
	}	//end if


	if (infloat!=1)
	{


	if (userid<0||canposttopic)
	{

	string newtopicurl = "";
	

	if (forum.Allowspecialonly<=0)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&forumpage=" + pageid;
	

	}
	else if (1==(forum.Allowpostspecial&1)&&usergroupinfo.Allowpostpoll==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=poll&forumpage=" + pageid;
	

	}
	else if (4==(forum.Allowpostspecial&4)&&usergroupinfo.Allowbonus==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=reward&forumpage=" + pageid;
	

	}
	else if (16==(forum.Allowpostspecial&16)&&usergroupinfo.Allowdebate==1)
	{

	 newtopicurl = forumpath + "posttopic.aspx?forumid=" + forum.Fid + "&type=debate&forumpage=" + pageid;
	

	}	//end if

	string newtopiconclick = "";
	

	if (forum.Allowspecialonly<=0&&canposttopic)
	{

	 newtopiconclick = "showWindow('newthread', '" + forumpath + "showforum.aspx?forumid=" + forum.Fid + "')";
	

	}	//end if


	if (userid<=0)
	{

	 newtopiconclick = "showWindow('login', '" + forumpath + "login.aspx');hideWindow('register');";
	

	}	//end if


	}	//end if


	}	//end if

	templateBuilder.Append("\r\n	</span>\r\n	<div>\r\n		<a href=\"javascript:;\" title=\"粗体\" class=\"tb_bold\" onclick=\"seditor_insertunit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', '[b]', '[/b]')\">B</a>\r\n		<a href=\"javascript:;\" title=\"颜色\" class=\"tb_color\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("forecolor\" onclick=\"showMenu(this.id, true, 0, 2)\">Color</a>\r\n		");	string coloroptions = "Black,Sienna,DarkOliveGreen,DarkGreen,DarkSlateBlue,Navy,Indigo,DarkSlateGray,DarkRed,DarkOrange,Olive,Green,Teal,Blue,SlateGray,DimGray,Red,SandyBrown,YellowGreen,SeaGreen,MediumTurquoise,RoyalBlue,Purple,Gray,Magenta,Orange,Yellow,Lime,Cyan,DeepSkyBlue,DarkOrchid,Silver,Pink,Wheat,LemonChiffon,PaleGreen,PaleTurquoise,LightBlue,Plum,White";
	
	templateBuilder.Append("\r\n		<div class=\"popupmenu_popup tb_color\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("forecolor_menu\" style=\"display: none\">\r\n			");
	int colorname__loop__id=0;
	foreach(string colorname in coloroptions.Split(comma))
	{
		colorname__loop__id++;

	templateBuilder.Append("\r\n				<input type=\"button\" style=\"background-color: ");
	templateBuilder.Append(colorname.ToString());
	templateBuilder.Append("\" onclick=\"seditor_insertunit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', '[color=");
	templateBuilder.Append(colorname.ToString());
	templateBuilder.Append("]', '[/color]')\" />");
	if (colorname__loop__id%8==0)
	{

	templateBuilder.Append("<br />");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n		</div>\r\n		<a href=\"javascript:;\" title=\"图片\" class=\"tb_img\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("img\" onclick=\"seditor_menu('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', 'img')\">Image</a>\r\n		<a href=\"javascript:;\" title=\"链接\" class=\"tb_link\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("url\" onclick=\"seditor_menu('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', 'url')\">Link</a>\r\n		<a href=\"javascript:;\" title=\"引用\" class=\"tb_quote\" onclick=\"seditor_insertunit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', '[quote]', '[/quote]')\">Quote</a>\r\n		<a href=\"javascript:;\" title=\"代码\" class=\"tb_code\" onclick=\"seditor_insertunit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("', '[code]', '[/code]')\">Code</a>\r\n	");
	if (config.Smileyinsert==1 && forum.Allowsmilies==1)
	{

	templateBuilder.Append("\r\n		<a href=\"javascript:;\" class=\"tb_smilies\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("smilies\" onclick=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("getSmilies(");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("getSmilies_callback);showMenu({'ctrlid':this.id, 'evt':'click', 'layer':2})\">Smilies</a>\r\n	");
	}	//end if

	templateBuilder.Append("\r\n	</div>\r\n</div>\r\n");
	if (config.Smileyinsert==1 && forum.Allowsmilies==1)
	{

	templateBuilder.Append("\r\n	<div class=\"smilies\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("smilies_menu\" style=\"display:none;width:315px;\">\r\n		<div class=\"smilieslist\">\r\n			");	string defaulttypname = string.Empty;
	
	templateBuilder.Append("\r\n			<div id=\"smiliesdiv\">\r\n				<div class=\"smiliesgroup\" style=\"margin-right: 0pt;\">\r\n					<ul>\r\n					");
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

	templateBuilder.Append("\r\n						<li id=\"t_s_" + stype__loop__id.ToString() + "\"><a id=\"s_" + stype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"showsmiles(" + stype__loop__id.ToString() + ", '" + stype["code"].ToString().Trim() + "', 1, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\" class=\"current\">" + stype["code"].ToString().Trim() + "</a></li>\r\n						");
	}
	else
	{

	templateBuilder.Append("\r\n						<li id=\"t_s_" + stype__loop__id.ToString() + "\"><a id=\"s_" + stype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"showsmiles(" + stype__loop__id.ToString() + ", '" + stype["code"].ToString().Trim() + "', 1, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\">" + stype["code"].ToString().Trim() + "</a></li>\r\n						");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n					</ul>\r\n				 </div>\r\n				 <div style=\"clear: both;\" class=\"float_typeid\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie\"></div>\r\n				 <table class=\"smilieslist_table\" id=\"s_preview_table\" style=\"display: none\"><tr><td class=\"smilieslist_preview\" id=\"s_preview\"></td></tr></table>\r\n				 <div id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie_pagenum\" class=\"smilieslist_page\">&nbsp;</div>\r\n			</div>\r\n		</div>\r\n		<script type=\"text/javascript\" reload=\"1\">\r\n			function ");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("getSmilies(func){\r\n				if($('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie').innerHTML !='' && $('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie').innerHTML != '正在加载表情...')\r\n					return;\r\n				var c = \"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("tools/ajax.aspx?t=smilies\";\r\n				_sendRequest(c,function(d){var e={};try{e=eval(\"(\"+d+\")\")}catch(f){e={}}var h=e?e:null;func(h);e=null;func=null},false,true);\r\n				setTimeout(\"if($('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie').innerHTML=='')$('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("showsmilie').innerHTML = '正在加载表情...'\", 2000);\r\n			}\r\n			function ");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("getSmilies_callback(obj) {\r\n				smilies_HASH = obj; \r\n				showsmiles(1, '");
	templateBuilder.Append(defaulttypname.ToString());
	templateBuilder.Append("', 1, '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\r\n			}\r\n		</");
	templateBuilder.Append("script>\r\n	</div>\r\n");
	}	//end if



	templateBuilder.Append("\r\n			<div class=\"postarea cl\">\r\n				<div class=\"postinner\">\r\n				");
	if (canposttopic)
	{

	templateBuilder.Append("\r\n				<textarea ");
	if (infloat!=1)
	{

	templateBuilder.Append("rows=\"5\"");
	}
	else
	{

	templateBuilder.Append("rows=\"7\"");
	}	//end if

	templateBuilder.Append(" cols=\"80\" name=\"");
	templateBuilder.Append(config.Antispampostmessage.ToString().Trim());
	templateBuilder.Append("\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("message\" onKeyDown=\"seditor_ctlent(event, 'fastvalidate($(\\'");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("form\\'),\\'");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("\\')','");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"3\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"6\"");
	}	//end if

	templateBuilder.Append("  style=\"background-image:url(" + quickbgad[1].ToString().Trim() + ");background-repeat:no-repeat;background-position:50% 50%;\" ");
	if (quickbgad[0].ToString().Trim()!="")
	{

	templateBuilder.Append(" onfocus=\"$('adlinkbtn').style.display='';$('closebtn').style.display='';this.onfocus=null;\"");
	}	//end if

	templateBuilder.Append("></textarea>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n				<div class=\"hm p_login cl\">你需要登录后才可以发帖 <a class=\"xg2\" onclick=\"hideWindow('register');showWindow('login', this.href);\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("login.aspx\">登录</a> | <a class=\"xg2\" onclick=\"hideWindow('login');showWindow('register', this.href);\" href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("register.aspx\">注册</a></div>\r\n				");
	}	//end if

	templateBuilder.Append("\r\n				</div>\r\n			</div>\r\n			</div>\r\n		</div>\r\n		");
	if (canposttopic)
	{

	templateBuilder.Append("\r\n        <div id=\"PrivateBTQuickQuickTopicSmilesShowDiv\" class=\"PrivateBTQuickSmilesShowDiv\">\r\n            <div class=\"smiliesgroup\" style=\"margin-right: 0pt;\">\r\n            <ul>\r\n                ");	string BTQuickQuickTopicdefaulttypname = string.Empty;
	

	int BTQuickQuickTopicstype__loop__id=0;
	foreach(DataRow BTQuickQuickTopicstype in Caches.GetSmilieTypesCache().Rows)
	{
		BTQuickQuickTopicstype__loop__id++;


	if (BTQuickQuickTopicstype__loop__id==1)
	{

	 BTQuickQuickTopicdefaulttypname = BTQuickQuickTopicstype["code"].ToString().Trim();
	

	}	//end if


	if (BTQuickQuickTopicstype__loop__id==1)
	{

	templateBuilder.Append("\r\n                  <li id=\"BTQuickQuickTopict_s_" + BTQuickQuickTopicstype__loop__id.ToString() + "\"><a id=\"BTQuickQuickTopics_" + BTQuickQuickTopicstype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"BTQuickshowsmiles(" + BTQuickQuickTopicstype__loop__id.ToString() + ", '" + BTQuickQuickTopicstype["code"].ToString().Trim() + "', 1, 'BTQuickQuickTopic', '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\" class=\"current\">" + BTQuickQuickTopicstype["code"].ToString().Trim() + "</a></li>\r\n                  ");
	}
	else
	{

	templateBuilder.Append("\r\n                  <li id=\"BTQuickQuickTopict_s_" + BTQuickQuickTopicstype__loop__id.ToString() + "\"><a id=\"BTQuickQuickTopics_" + BTQuickQuickTopicstype__loop__id.ToString() + "\" hidefocus=\"true\" href=\"javascript:;\" onclick=\"BTQuickshowsmiles(" + BTQuickQuickTopicstype__loop__id.ToString() + ", '" + BTQuickQuickTopicstype["code"].ToString().Trim() + "', 1, 'BTQuickQuickTopic', '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\">" + BTQuickQuickTopicstype["code"].ToString().Trim() + "</a></li>\r\n                  ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n            </ul>\r\n           </div>\r\n           <div style=\"clear: both;\" class=\"float_typeid\" id=\"BTQuickQuickTopicshowsmilie\"></div>\r\n           <div id=\"BTQuickQuickTopicshowsmilie_pagenum\" class=\"smilieslist_page\">&nbsp;</div>\r\n           <script type=\"text/javascript\" reload=\"1\">\r\n                var BTQuickQuickTopicsmilies_HASH = {};\r\n                var showsmiletimeout;\r\n                function BTQuickQuickTopicgetSmilies(func){\r\n\r\n                  if($('BTQuickQuickTopicshowsmilie').innerHTML !='' && $('BTQuickQuickTopicshowsmilie').innerHTML != '正在加载表情...')\r\n                    return;\r\n                  var c = \"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("tools/ajax.aspx?t=smilies\";\r\n                  _sendRequest(c,function(d){var e={};try{e=eval(\"(\"+d+\")\")}catch(f){e={}}var h=e?e:null;func(h);e=null;func=null},false,true);\r\n                  showsmiletimeout = setTimeout(\"if($('BTQuickQuickTopicshowsmilie').innerHTML=='')$('BTQuickQuickTopicshowsmilie').innerHTML = '正在加载表情...'\", 2000);\r\n                  \r\n                }\r\n                function BTQuickQuickTopicgetSmilies_callback(obj) {\r\n\r\n                  BTQuickQuickTopicsmilies_HASH = obj; \r\n                  BTQuickshowsmiles(1, '");
	templateBuilder.Append(BTQuickQuickTopicdefaulttypname.ToString());
	templateBuilder.Append("', 1, 'BTQuickQuickTopic', '");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\r\n                  clearTimeout(showsmiletimeout);\r\n                }\r\n                \r\n                BTQuickQuickTopicgetSmilies(BTQuickQuickTopicgetSmilies_callback);\r\n          </");
	templateBuilder.Append("script>\r\n        </div>\r\n    ");
	}	//end if


	if (isseccode)
	{

	templateBuilder.Append("\r\n		<div class=\"pbt\" style=\"position: relative;\">\r\n		");
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

	templateBuilder.Append("\r\n		</div>\r\n		");
	}	//end if

	templateBuilder.Append("\r\n		<div class=\"pbt\">\r\n		    ");
	if (canposttopic)
	{

	templateBuilder.Append("\r\n			<button type=\"button\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("submit\" onclick=\"fastsubmit('");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("');\" name=\"topicsubmit\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"5\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"8\"");
	}	//end if

	templateBuilder.Append(" class=\"pn\"><span>发表帖子</span></button> <span class=\"grayfont\">[Ctrl+Enter快速发布]</span>\r\n			");
	}
	else
	{

	templateBuilder.Append("\r\n			<button type=\"button\" id=\"");
	templateBuilder.Append(seditorid.ToString());
	templateBuilder.Append("submit\" name=\"topicsubmit\" ");
	if (infloat==1)
	{

	templateBuilder.Append("tabindex=\"5\"");
	}
	else
	{

	templateBuilder.Append("tabindex=\"8\"");
	}	//end if

	templateBuilder.Append(" onclick=\"hideWindow('register');showWindow('login', '");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("login.aspx');\" class=\"pn\"><span>发表帖子</span></button>\r\n			");
	}	//end if

	templateBuilder.Append("\r\n			<a href=\"###\" id=\"adlinkbtn\" style=\"display:none;\" onclick=\"window.open('" + quickbgad[0].ToString().Trim() + "','_blank');\">查看背景广告</a>\r\n			<a href=\"###\" id=\"closebtn\" style=\"display:none;\" onclick=\"$('quickpostmessage').style.background='';this.style.display='none';$('adlinkbtn').style.display='none';\">隐藏</a>\r\n			<input type=\"hidden\" id=\"postbytopictype\" name=\"postbytopictype\" value=\"");
	templateBuilder.Append(forum.Postbytopictype.ToString().Trim());
	templateBuilder.Append("\" tabindex=\"3\" />\r\n		</div>\r\n	</div>\r\n</div>\r\n</form>");


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
