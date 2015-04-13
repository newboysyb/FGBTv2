/*
	Newboysyb Studio 2011 Copyright(c)
	Futrue Garden BT System v2.1 Javascript
  
*/
document.onkeydown = esckeydownclosewin;
var openedwinlist = '';


/*pm notice*/

var scrollmark = null;
var scrollcount = 0;
var upcount = 0;
var checkcount = 0;
var checking = 0;
var buaabtoHttpReq1;
var buaabtoHttpReq2;
var needcheck = false;
var checkfail = false;


function setnewpmnoticeposion(newnotice, newpm)
{
    setMenuPosition('notice_ntc', 'notice_ntc_menu', '43');
    _attachEvent(window, "resize", function(){ setMenuPosition('notice_ntc', 'notice_ntc_menu', '43'); });
    if(newnotice > 0)
    {
        $("notice_ntc_menu").style.display='';
    }  
    setMenuPosition('pm_ntc', 'pm_ntc_menu', '43');
    _attachEvent(window, "resize", function(){ setMenuPosition('pm_ntc', 'pm_ntc_menu', '43'); });
    if(newpm > 0)
    {
        $("pm_ntc_menu").style.display='';
    }  

}

function initpmnoticeupdate()
{
    if(window.XMLHttpRequest)
    {
        buaabtoHttpReq1=new XMLHttpRequest();
    }
    else if(window.ActiveXObject)
    {
        buaabtoHttpReq1=new ActiveXObject("Microsoft.XMLHTTP");
    }

    checknewcount();
    setInterval(checknewmsg, 10000);
}


function checknewcount()
{
    if((pmcount > 0 || noticecount > 0))
    {
        document.title = "【您有新消息】--" + originalTitle;
        if(scrollmark == null) 
        {
            scrollcount = 0;
            setInterval(titlescroll, 500);
        }
        if(playpmsound == true)
        {
            if(navigator.userAgent.indexOf("Firefox")<1)
            {
              $("msgsoundplayerspanheader").innerHTML = "<embed id=\"msgsoundplayer\" src=\"/sound/pm{oluserinfo.PMSound}.wav\" height=\"0\" width=\"0\" style=\"display:hidden\">";
            }
        }
       

    }
    else
    {
        scrollcount = 15;
        if(scrollmark != null) clearInterval(scrollmark);
        document.title = originalTitle;
    }
    
    if(pmcount < 1000) $("pmcountshow").innerHTML = "您有" + pmcount + "条新消息";
    else  $("pmcountshow").innerHTML = "您有大于1000条新消息";
    setMenuPosition('pm_ntc', 'pm_ntc_menu', '43');
    _attachEvent(window, "resize", function(){ setMenuPosition('pm_ntc', 'pm_ntc_menu', '43'); });

    if(noticecount < 1000) $("noticecountshow").innerHTML = "您有" + noticecount + "条新通知";
    else  $("noticecountshow").innerHTML = "您有大于1000条新通知";                                
    setMenuPosition('notice_ntc', 'notice_ntc_menu', '43');
    _attachEvent(window, "resize", function(){ setMenuPosition('notice_ntc', 'notice_ntc_menu', '43'); });
        
    if(pmcount > 0) 
    {
        $("pm_ntc_menu").style.display='';
        $("pm_ntc").innerHTML = "短消息(" + pmcount + ")";
        if(pmcount > 1000) $("pm_ntc").title = "您有大于1000条新消息";
        $("pm_ntc").title = "您有" + pmcount + "条新短消息";
    }
    else 
    {                    
        $("pm_ntc_menu").style.display='none';
        $("pm_ntc").innerHTML = "短消息";
        $("pm_ntc").title = "您没有新短消息";
    }
    if(noticecount > 0) 
    {
        $("notice_ntc_menu").style.display='';
        $("notice_ntc").innerHTML = "通知(" + noticecount + ")";
        if(noticecount > 1000) $("notice_ntc").title = "您有大于1000条新通知";
        $("notice_ntc").title = "您有" + noticecount + "条新通知";
    }
    else 
    {
        $("notice_ntc_menu").style.display='none';
        $("notice_ntc").innerHTML = "通知";
        $("notice_ntc").title = "您没有新通知";
    }

    
}


function titlescroll()
{
    if(scrollcount > 12) return;
    if(pmcount == 0 && noticecount == 0) 
    {
        scrollcount = 15;
        document.title = originalTitle;
        clearInterval(scrollmark);
        scrollmark = null;
        return;
    }
    if(scrollcount == 0) document.title = "【您有新消息】--" + originalTitle;
    if(scrollcount == 1) document.title = "〖您有新消息〗--" + originalTitle;
    if(scrollcount == 2) document.title = "〖您有新消息〗--" + originalTitle;
    if(scrollcount == 3) document.title = "【您有新消息】--" + originalTitle;
    if(scrollcount == 4) document.title = "-【您有新消息】-" + originalTitle;
    if(scrollcount == 5) document.title = "--【您有新消息】" + originalTitle;
    if(scrollcount == 6) document.title = "--〖您有新消息〗" + originalTitle;
    if(scrollcount == 7) document.title = "--〖您有新消息〗" + originalTitle;
    if(scrollcount == 8) document.title = "--【您有新消息】" + originalTitle;
    if(scrollcount == 9) document.title = "-【您有新消息】-" + originalTitle;
    if(scrollcount == 10) document.title = "【您有新消息】--" + originalTitle;
    scrollcount++;
    if(scrollcount > 10) 
    {
        clearInterval(scrollmark);
        scrollmark = null;
    }
}


function checknewmsg()
{
    if(checkfail == true) return;
    if(checking == 1) return;
    checking = 1;
    
    try
    {
        
        upcount++;
        if(upcount > 11) 
        {
            needcheck = true;
            upcount = 0;
        }

        checkcount++;
        if(checkcount > 20000) checkcount = 721;
        if(checkcount > 720) /*2小时以上，每1小时检测一次*/
        {
            if(checkcount%360 == 0) needcheck = true;
            else {checking = 0; return;}
        }
        else if(checkcount > 360) /*1-2小时，每5分钟检测一次*/
        {
            if(checkcount%30 == 0) needcheck = true;
            else {checking = 0; return;}
        }
        else if(checkcount > 60) /*10-60分钟，每2分钟检测一次*/
        {
            if(checkcount%12 == 0) needcheck = true;
            else{checking = 0; return;}
        }
        else if(checkcount > 18) /*3-10分钟，每30秒检测一次*/
        {
            if(checkcount%3 == 0) needcheck = true;
            else{checking = 0; return;}
        }
        /*3分钟内，每10秒检测一次*/
        buaabtoHttpReq1.open("GET","/tools/sessionajax.aspx?t=newpmnoticecount",true);
        buaabtoHttpReq1.send(null); 
        buaabtoHttpReq1.onreadystatechange =function(){ getnewpmnoticecount(needcheck) }

    }
    catch(err)
    {
        buaabtoHttpReq1 = null;
        if(window.ActiveXObject)
        {
            buaabtoHttpReq1=new ActiveXObject("Microsoft.XMLHTTP");
        }
        else if(window.XMLHttpRequest)
        {
            buaabtoHttpReq1=new XMLHttpRequest();
        }
    } 
    checking = 0;       
} 


function getnewpmnoticecount()
{
    var rtv = buaabtoHttpReq1.responseText;
    if(buaabtoHttpReq1.readyState == 4 && buaabtoHttpReq1.status == 200 && rtv.indexOf("<PMCOUNT>") > -1 && rtv.indexOf("<NOTICECOUNT>") > -1)
    {
        var reg = /\<PMCOUNT\>\<(\d+)\>\<NOTICECOUNT\>\<(\d+)\>/g;
        var reg2 = /\<PMCOUNT\>\<-(\d+)\>\<NOTICECOUNT\>\<-(\d+)\>/g;
        if(reg.test(rtv))
        {
            var newpmcount = parseInt(RegExp.$1);
            var newnoticecount = parseInt(RegExp.$2);
            if(newpmcount != pmcount)
            {
                pmcount = newpmcount;
                needcheck = true;
            }
            if(newnoticecount != noticecount)
            {
                noticecount = newnoticecount;
                needcheck = true;
            }
        }
        else if(reg2.test(rtv))
        {
            var newpmcount = parseInt(RegExp.$1);
            var newnoticecount = parseInt(RegExp.$2);
            if(newpmcount == 1099 && newnoticecount == 1099)
            {
                checkfail = true;
            }
        }
    }
    else if(buaabtoHttpReq1.readyState == 4 && buaabtoHttpReq1.status == 200 && rtv == "")
    {
        checkfail = true;
    }
    if(needcheck) checknewcount();
    if(needcheck) checknewcount();
}

function playpmsoundheader()
{
    if(playpmsound == true)
    {
        if(navigator.userAgent.indexOf("Firefox")<1)
        {
          $("msgsoundplayerspanheader").innerHTML = "<embed id=\"msgsoundplayer\" src=\"/sound/pm{oluserinfo.PMSound}.wav\" height=\"0\" width=\"0\" style=\"display:hidden\">";
        }
    }
}




function addopenedwin(strName)
{
    //alert('add win:' + strName);
    if (openedwinlist == null || openedwinlist == '')
    {
        openedwinlist = strName;
    }
    else
    {   
        openedwinlist = openedwinlist + ',' + strName;
    }
    //alert('now win:' + openedwinlist);
	if(openedwinlist != "")
	{
		//window.onresize = adjustinfowindowlist;
	}
	else
	{
		//window.onresize = "";
	}
}

function removeopenedwin(strName)
{
    //alert('remove win:' + strName);
    if(openedwinlist != '')
    {
        var openedwinArray = openedwinlist.split(',');
        if(openedwinArray.length = 1)
        {
            if(openedwinlist == strName) openedwinlist = '';
        }
        else
        {
            openedwinlist = '';
            for(var i = 0; i < openedwinArray.length - 1; i++)
            {
                if(openedwinArray[i] != strName) openedwinlist = openedwinlist + ',' + openedwinArray[i];
            }
        }
        
         //alert('remain win:' + openedwinlist);
    }
}
function coloseopenedwin(scount)
{
    if(scount > 50) return;
    scount = scount + 1;
    if(openedwinlist != '')
    {
        var openedwinArray = openedwinlist.split(',');
        if(openedwinArray.length < 2)
        {
            var oldlist = openedwinlist;
            openedwinlist = '';
            //alert('1close win:' + oldlist);
            if(openedwinArray[openedwinArray.length - 1] == 'MOD_WIN_DISPLAYED')
            {
                if($('modlayer') != null)
                {
                    $('modlayer').style.display = 'none';
                    if($('moderate') != null)
                    {
                        for(i = 0; i < document.moderate.elements.length; i++)
                        {
                           f = document.moderate.elements[i];
                           if(f.checked == true)
                           {
                              f.checked = false;
                           }
                        } 
                        modclickcount = 0;
                    }
                }
                if($('modtopiclayer') != null)
                {
                    $('modtopiclayer').style.display = 'none';
                    if($('postsform') != null)
                    {
                        for(i = 0; i < document.postsform.elements.length; i++)
                        {
                           f = document.postsform.elements[i];
                           if(f.checked == true)
                           {
                              f.checked = false;
                           }
                        } 
                        $('postsform').pid.value = '';
                        modclickcount = 0;
                    }   
                }
            }
            else if(openedwinArray[openedwinArray.length - 1].length > 9 && openedwinArray[openedwinArray.length - 1].substr(0, 9) == 'FLOATWIN|')
            {
                //alert('now close win:' + openedwinArray[openedwinArray.length - 1]);
                var nowclosewinname = openedwinArray[openedwinArray.length - 1].substr(9);
                //alert('now close win name only:' + nowclosewinname);
                floatwinreset = 1;
                floatwin(nowclosewinname);
            }
            else if(openedwinArray[openedwinArray.length - 1].length > 9 && openedwinArray[openedwinArray.length - 1].substr(0, 9) == 'FLOATMEN|')
            {
                //alert('now close win:' + openedwinArray[openedwinArray.length - 1]);
                var nowclosewinname = openedwinArray[openedwinArray.length - 1].substr(9);
                //alert('now close win name only:' + nowclosewinname);
                hideMenu(nowclosewinname,'dialog');
            }
            else 
            {
                hideWindow(oldlist);
            }
        }
        else
        {
            openedwinlist = openedwinArray[0];
            for(var i = 1; i < openedwinArray.length - 1; i++)
            {
                openedwinlist = openedwinlist + ',' + openedwinArray[i];
            }
            //alert('2close win:' + openedwinArray[openedwinArray.length - 1] + '\ntotal:' + openedwinlist);
            if(openedwinArray[openedwinArray.length - 1] == '')
            {
                //空白，则继续执行下一个关闭
                coloseopenedwin(scount);
            }
            if(openedwinArray[openedwinArray.length - 1] == 'MOD_WIN_DISPLAYED')
            {
                if($('modlayer') != null)
                {
                    $('modlayer').style.display = 'none';
                    if($('moderate') != null)
                    {
                        for(i = 0; i < document.moderate.elements.length; i++)
                        {
                           f = document.moderate.elements[i];
                           if(f.checked == true)
                           {
                              f.checked = false;
                           }
                        } 
                        modclickcount = 0;
                    }
                }
                if($('modtopiclayer') != null)
                {
                    $('modtopiclayer').style.display = 'none';
                    if($('postsform') != null)
                    {
                        for(i = 0; i < document.postsform.elements.length; i++)
                        {
                           f = document.postsform.elements[i];
                           if(f.checked == true)
                           {
                              f.checked = false;
                           }
                        } 
                        $('postsform').pid.value = '';
                        modclickcount = 0;
                    }
                }
            }
            else if(openedwinArray[openedwinArray.length - 1].length > 9 && openedwinArray[openedwinArray.length - 1].substr(0, 9) == 'FLOATWIN|')
            {
                //alert('now close win:' + openedwinArray[openedwinArray.length - 1]);
                var nowclosewinname = openedwinArray[openedwinArray.length - 1].substr(9);
                //alert('now close win name only:' + nowclosewinname);
                floatwinreset = 1;
                floatwin(nowclosewinname);
                coloseopenedwin(scount);
            }
            else if(openedwinArray[openedwinArray.length - 1].length > 9 && openedwinArray[openedwinArray.length - 1].substr(0, 9) == 'FLOATMEN|')
            {
                //alert('now close win:' + openedwinArray[openedwinArray.length - 1]);
                var nowclosewinname = openedwinArray[openedwinArray.length - 1].substr(9);
                //alert('now close win name only:' + nowclosewinname);
                hideMenu(nowclosewinname,'dialog');
                coloseopenedwin(scount);
            }
            else 
            {
                hideWindow(openedwinArray[openedwinArray.length - 1]);
            }
        }
        
        
    }
}

function esckeydownclosewin(event)
{
    event = event || window.event; 
    if(event.keyCode==27)
    {
        //alert('now win:' + openedwinlist);
        
        event.returnValue = null;
        window.returnValue = null;
        event.returnvalue = false;
        coloseopenedwin(1);
        
        return;
    }
    event.returnvalue = true;
}


function fgbtshowwindow(strName,strLink)
{
	showWindow(strName,strLink);
	adjustinfowindow(strName);
	//_attachEvent(window, "resize", function(){ adjustinfowindowlist(); });
	//alert('3finished');
}

function adjustinfowindowlist()
{
	//alert('resize');
    //if(openedwinlist != '')
    //alert(openedwinlist);
    if(true)
	{
        var openedwinArray = openedwinlist.split(',');

		for(var i = 0; i < openedwinArray.length - 1; i++)
		{
			if(openedwinArray[i].length > 12)
			{
				if(openedwinArray[i].substr(0, 12) == 'showseedfile' || openedwinArray[i].substr(0, 12) == 'showseedpeer' || openedwinArray[i].substr(0, 12) == 'showseedinfo')
				{
					//alert('adjust win:' + openedwinlist);
					adjustinfowindow(openedwinArray[i]);
				}
			}
		}
	}
	return true;
}
function adjustinfowindow(strName)
{
	//alert('in');
	if($('PrivateBTInfoListBorder2_' + strName) == null)
	{
		//alert('in1');
		setTimeout(function () { adjustinfowindow(strName);},100);
		return;
	}
	//alert('ok');
	
	
	var clientWidth = document.body.clientWidth;
	var clientHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight;
	var scrollTop = document.body.scrollTop ? document.body.scrollTop : document.documentElement.scrollTop;
	var setobj = $('fwin_' + strName);
	var settable = $('PrivateBTInfoListBorder_' + strName);
	var settable2 = $('PrivateBTInfoListBorder2_' + strName);
	var oldtableWidth = settable2.offsetWidth;
	var oldtableHeight = settable2.offsetHeight;
	var newsettableHeight = 50;
	var newsettableWidth = 50;
	
	settable2.style.width = (clientWidth * 0.8) + "px";
	newsettableWidth = (clientWidth * 0.8);
	if(settable.offsetWidth > settable2.offsetWidth)
	{
		settable2.style.width = settable.offsetWidth + "px";
		newsettableWidth = settable.offsetWidth;
	}
	setobj.width = (575 - clientWidth * 0.2) + "px";
	//setobj.style.marginLeft = "-" + (clientWidth * 0.8 - oldtableWidth) / 2 + "px";
	if(newsettableWidth  - oldtableWidth > 0)
	{
		setobj.style.marginLeft = "-" + (newsettableWidth  - oldtableWidth) / 2 + "px";
	}
	else
	{
		setobj.style.marginLeft = (oldtableWidth - newsettableWidth) / 2 + "px";
	}
	
						
			  
	                   if(settable2.offsetHeight  > clientHeight * 0.7 - 100)
                      {
                          if(clientHeight > 50)
                          {
                             settable.style.height = (clientHeight * 0.7 - 100) + "px";
							 newsettableHeight = (clientHeight * 0.7 - 100);
                          }
                          else
                          {
                              settable.style.height = "50px";
                          }
                      }
                      else
                      {
                         settable.style.height = settable2.offsetHeight + "px";
						 newsettableHeight = settable2.offsetHeight;
					  }
					  
						setobj.style.top = ((clientHeight - newsettableHeight) / 2 - 90) + "px";
						//setobj.style.top = "0px";
					  //setMenuPosition('fwin_' + strName, 'fwin_' + strName, '43');
					  
					  //alert($('append_parent').style.height);
                         
	
	//setobj.onresize = adjustinfowindow('\'' + strName + '\'');
	//alert('finished');
}

//种子列表页面，按照相应的顺序排序
function PrivateBTChangeOrderby(ordertype)
{
	if(ordertype=='file')
	{
		if(document.getElementById('PrivateBTSeedSearchOrderBy').value == '1')
		{
			if(document.getElementById('PrivateBTSeedSearchAsc').value == 'True') document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
			else document.getElementById('PrivateBTSeedSearchAsc').value = 'True';
		}
		else 
		{
			document.getElementById('PrivateBTSeedSearchOrderBy').value = '1';
			document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
		}
	}
	if(ordertype=='size')
	{
		if(document.getElementById('PrivateBTSeedSearchOrderBy').value == '2')
		{
			if(document.getElementById('PrivateBTSeedSearchAsc').value == 'True') document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
			else document.getElementById('PrivateBTSeedSearchAsc').value = 'True';
		}
		else 
		{
			document.getElementById('PrivateBTSeedSearchOrderBy').value = '2';
			document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
		}       
	}
	if(ordertype=='seed')
	{
		if(document.getElementById('PrivateBTSeedSearchOrderBy').value == '3')
		{
			if(document.getElementById('PrivateBTSeedSearchAsc').value == 'True') document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
			else document.getElementById('PrivateBTSeedSearchAsc').value = 'True';
		}
		else 
		{
			document.getElementById('PrivateBTSeedSearchOrderBy').value = '3';
			document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
		}        
	}
	if(ordertype=='completed')
	{
		if(document.getElementById('PrivateBTSeedSearchOrderBy').value == '4')
		{
			if(document.getElementById('PrivateBTSeedSearchAsc').value == 'True') document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
			else document.getElementById('PrivateBTSeedSearchAsc').value = 'True';
		}
		else 
		{
			document.getElementById('PrivateBTSeedSearchOrderBy').value = '4';
			document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
		}        
	}
	if(ordertype=='finished')
	{
		if(document.getElementById('PrivateBTSeedSearchOrderBy').value == '5')
		{
			if(document.getElementById('PrivateBTSeedSearchAsc').value == 'True') document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
			else document.getElementById('PrivateBTSeedSearchAsc').value = 'True';
		}
		else 
		{
			document.getElementById('PrivateBTSeedSearchOrderBy').value = '5';
			document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
		}        
	}
	if(ordertype=='time')
	{
		if(document.getElementById('PrivateBTSeedSearchOrderBy').value == '0')
		{
			if(document.getElementById('PrivateBTSeedSearchAsc').value == 'True') document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
			else document.getElementById('PrivateBTSeedSearchAsc').value = 'True';
		}
		else 
		{
			document.getElementById('PrivateBTSeedSearchOrderBy').value = '0';
			document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
		}       
	}
	if(ordertype=='live')
	{
		if(document.getElementById('PrivateBTSeedSearchOrderBy').value == '7')
		{
			if(document.getElementById('PrivateBTSeedSearchAsc').value == 'True') document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
			else document.getElementById('PrivateBTSeedSearchAsc').value = 'True';
		}
		else 
		{
			document.getElementById('PrivateBTSeedSearchOrderBy').value = '7';
			document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
		}       
	}
	if(ordertype=='traffic')
	{
		if(document.getElementById('PrivateBTSeedSearchOrderBy').value == '6')
		{
			if(document.getElementById('PrivateBTSeedSearchAsc').value == 'True') document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
			else document.getElementById('PrivateBTSeedSearchAsc').value = 'True';
		}
		else 
		{
			document.getElementById('PrivateBTSeedSearchOrderBy').value = '6';
			document.getElementById('PrivateBTSeedSearchAsc').value = 'False';
		}       
	}
	PrivateBTSearchSubmit();
}


function seedmodthreads(optgroup, operation) {
    var operation = !operation ? '' : operation;
    //var windowWidth = 250;
    //var windowHeight = 220;
    /* switch (operation) {
        case "split":
            windowHeight = 300;
            break;
        case "identify":
            windowHeight = 230;
            break;
        default:
            windowHeight = 220;
    } */
    //floatwin('open_mods', '', windowWidth, windowHeight);
	hideWindow('mods');
    $('seedmoderate').optgroup.value = optgroup;
    $('seedmoderate').operat.value = operation;
    //$('floatwin_mods').innerHTML = '';
    //ajaxpost('moderate', 'floatwin_mods', '');
	


	

	showWindow('mods', 'seedmoderate', 'post', 0);

	if(BROWSER.ie) {

		doane(event);

	}	
}
function seedmodthreadstopic(optgroup, operation) {
    var operation = !operation ? '' : operation;
    //var windowWidth = 250;
    //var windowHeight = 220;
    /* switch (operation) {
        case "split":
            windowHeight = 300;
            break;
        case "identify":
            windowHeight = 230;
            break;
        default:
            windowHeight = 220;
    } */
    //floatwin('open_mods', '', windowWidth, windowHeight);
	hideWindow('mods');
    $('moderate').optgroup.value = optgroup;
    $('moderate').operat.value = operation;
    //$('floatwin_mods').innerHTML = '';
    //ajaxpost('moderate', 'floatwin_mods', '');
	


	

	showWindow('mods', 'seedmoderate', 'post', 0);

	if(BROWSER.ie) {

		doane(event);

	}	
}




function BTQuickshowsmiles(index, typename, pageindex, seditorKey, textmessagename)
{
  var BTQuickcolCount = 12;
  var smilies_hash;
  
		  if(seditorKey == 'BTQuick'){
          smilies_hash = BTQuicksmilies_HASH;
		  }
		  else if(seditorKey == 'BTQuickFloat'){
          smilies_hash = BTQuickFloatsmilies_HASH;
		  }
		  else if(seditorKey == 'BTQuickQuickTopic'){
          smilies_hash = BTQuickQuickTopicsmilies_HASH;
		  }
		  else if(seditorKey == 'BTQuickTopic'){
          smilies_hash = BTQuickTopicsmilies_HASH;
		  }
		  else{
          smilies_hash = BTQuicksmilies_HASH;
			}
  
	$(seditorKey + "s_" + index).className = "current";
	var cIndex = 1;
	for (i in smilies_hash) {
		if (cIndex != index) {
			$(seditorKey + "s_" + cIndex).className = "";
		}
		$(seditorKey + "s_" + cIndex).style.display = "";
		cIndex ++;
	}

	var pagesize = 24;//(typeof smiliesCount) == 'undefined' ? 12 : smiliesCount;
	var url = (typeof forumurl) == 'undefined' ? '' : forumurl;
	var s = smilies_hash[typename];
	var pagecount = Math.ceil(s.length/pagesize);
	var inseditor = typeof seditorKey != 'undefined';
			
			if(seditorKey == 'BTQuick'){
          inseditor = true;
		  }
		  else if(seditorKey == 'BTQuickFloat'){
          inseditor = true;
		  }
		  else if(seditorKey == 'BTQuickQuickTopic'){
          inseditor = true;
		  }
		  else if(seditorKey == 'BTQuickTopic'){
          inseditor = false;
		  }
		  else{
          inseditor = BTQuicksmilies_HASH;
			}

	if (isUndefined(pageindex)) {
		pageindex = 1;
	}
	if (pageindex > pagecount) {
		pageindex = pagecount;
	}

	var maxIndex = pageindex*pagesize;
	if (maxIndex > s.length) {
		maxIndex = s.length;
	}
	maxIndex = maxIndex - 1;

	var minIndex = (pageindex-1)*pagesize;

	var html = '<table id="' + index + '_table" cellpadding="0" cellspacing="0" style="clear: both" class="BTQuickTable"><tr>';

	var ci = 1;
	for (var id = minIndex; id <= maxIndex; id++) {
		var clickevt = 'insertSmiley(\'' + addslashes(s[id]['code']) + '\');';
		if (inseditor) {
		  if(seditorKey == 'BTQuick'){
		  clickevt = 'seditor_insertunit(\'quickpost\', \'' + s[id]['code'] + '\');';
		  }
		  else if(seditorKey == 'BTQuickFloat'){
		  clickevt = 'seditor_insertunit(\'\', \'' + s[id]['code'] + '\');';
		  }
		  else if(seditorKey == 'BTQuickQuickTopic'){
		  clickevt = 'seditor_insertunit(\'' + textmessagename + '\', \'' + s[id]['code'] + '\');';
		  }
		  else{
			clickevt = 'seditor_insertunit(\'' + seditorKey + '\', \'' + s[id]['code'] + '\');';
			}
		}

		html += '<td class="BTQuickTableTD" valign="middle"><img class="BTQuickTableIMAGE" src="' + url + 'editor/images/smilies/' + s[id]['url'] + '" id="smilie_' + s[id]['code'] + '" alt="' + s[id]['code'] + '" onclick="' + clickevt + '" title="" border="0"/></td>';
		if (ci%BTQuickcolCount == 0) {
			html += '</tr><tr>'
		}
		ci ++;
	}

	html += '<td colspan="' + (BTQuickcolCount - ((ci-1) % BTQuickcolCount)) + '"></td>';
	html += '</tr>';
	html += '</table>';
	$(seditorKey+"showsmilie").innerHTML = html;

	if (pagecount > 1) {
		html = '<div class="p_bar">';
		for (var i = 1; i <= pagecount; i++) {
			if (i == pageindex) {
				html += "<a class=\"p_curpage\">" + i + "</a>";
			}
			else {
				html += "<a class=\"p_num\" href='#smiliyanchor' onclick=\"BTQuickshowsmiles(" + index + ", '" + typename + "', " + i + ", '"+seditorKey+"', '"+textmessagename+"')\">" + i + "</a>"
			}
		}
		html += '</div>'
		$(seditorKey+"showsmilie_pagenum").innerHTML = html;
	}
	else {
		$(seditorKey+"showsmilie_pagenum").innerHTML = "";
	}
}

var fgbt_do_checkin_send = 0;
function fgbt_do_checkin(){
  if(fgbt_do_checkin_send==0){
    fgbt_do_checkin_send = 1;
    _sendRequest("tools/ajax.aspx?t=checkin",function(doc){if(typeof(doc)!="undefined"){$("btnfgbtCheckIn").innerHTML = doc;showCreditPrompt();}},false);
  }
}