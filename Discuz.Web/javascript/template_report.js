var report_button_index = 0;
var enableinfo = '举报';

function show_report_button(fid,tid,pid,disableinfo)
{
	var reportid = reportgetCookie("reportid");
	if(disableinfo=="已举报" || (reportid !=null && reportid.indexOf("," + pid + ",") != -1))
	{
    output_html("已举报");
    return;
	}
	
	
	
    var html = '<a name="#r_' + report_button_index + '"></a>';
    html += '<span id="report_' + report_button_index + '_container">';
	//html += '	<a id="report_' + report_button_index + '" href="javascript:void(0);" onclick="send_report(' + report_button_index + ',\'' + disableinfo + '\');">' + enableinfo + '</a>';
    html += '	<a id="report_' + report_button_index + '" href="javascript:void(0);" onclick="createreportdiv(' + fid + ',this.id,' + report_button_index + ', \'' + disableinfo + '\',' + tid + ',' + pid + ')">' + enableinfo + '</a>';
	html += '</span>';
    report_button_index++;
	output_html(html);
}

function send_report(fid,id,index, disableinfo,tid,pid)
{

  var message=$('reportmessage'+index).value;
  if(message.length < 15)
  {
    alert("字数不足15，请补充详细信息后再次提交");
    return;
  }
  sendReportRequest('tools/ajax.aspx?t=report&fid='+fid, index, disableinfo,message,id,tid,pid);
  var reportid = reportgetCookie("reportid");
  if(reportid==null) reportid = "";
  reportsetCookie("reportid",reportid + "," + pid + ",",1);
  $(id + "_menu").style.display="none";
  $("report_" + index + "_container").innerHTML="已举报";
  checkcount = 0;
  upcount = 12;
}

function reportgetCookie(c_name)
{
  if (document.cookie.length > 0)
  {
     c_start = document.cookie.indexOf(c_name + "=")
     if (c_start != -1)
     {
      c_start = c_start + c_name.length + 1;
      c_end   = document.cookie.indexOf(";",c_start);
      if (c_end == -1)
      {
       c_end = document.cookie.length;
      }
      return unescape(document.cookie.substring(c_start,c_end));
     }
  }
  return null
}
function reportsetCookie(c_name,value,expiredays)
{
var exdate = new Date();
exdate.setDate(exdate.getDate() + expiredays);
// 使设置的有效时间正确。增加toGMTString()
document.cookie = c_name + "=" +escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
}


function output_html(html)
{
    document.write(html);
}

function sendReportRequest(action, button_index, disableinfo,message,id,tid,pid) {
	if (action && action != '')
	{	
		var oXmlHttp = createXMLHttp();
		oXmlHttp.open("post", action, true);
		oXmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		oXmlHttp.onreadystatechange = function () {
			if (oXmlHttp.readyState == 4) {
				if (oXmlHttp.status == 200) {rep
					RetrunResult(oXmlHttp.responseXML,button_index,disableinfo,id);
					//report over, disable report link.
					//$("report_" + button_index + "_container").innerHTML = disableinfo;					
				} else {
					alert("An error occurred: " + oXmlHttp.statusText);
				}
			}
		};
		var queryParm = 'reportmessage=' + message + '&report_url=' + encodeURIComponent('showtopic.aspx?topicid=' + tid + '&postid=' + pid + '#' + pid);
		oXmlHttp.send(queryParm);
	}
	

}

function RetrunResult(doc,button_index,disableinfo,id)
{
	   var err = doc.getElementsByTagName('error');
		if (err[0] != null && err[0] != undefined)
		{
			if (err[0].childNodes.length > 1) {
				alert(err[0].childNodes[1].nodeValue);
			} else {
				alert(err[0].firstChild.nodeValue);    		
			}
			return;
		}
		else
		{		
	    $('reportmessage'+button_index).value='';
		$(id + "_menu").style.display='none';
		$("report_" + button_index + "_container").innerHTML = disableinfo;				
		return;
			}
	
	
	}


function createreportdiv(fid,id,index, disableinfo,tid,pid)
{
	var reportid = reportgetCookie("reportid");
	if(disableinfo=="已举报" || (reportid !=null && reportid.indexOf("," + pid + ",") != -1))
	{
      if(!$(id + "_menu"))
    {
    var div = document.createElement("DIV");
    var reportid='reportmessage'+index;
    div.id = id + "_menu";
    div.style.display = "none";
    div.style.width='270px';
    div.className='popupmenu_popup';
    var html ='<form id="commentform" >';
        html +='<table border="0" cellpadding="0" cellspacing="0">';	
          html +='<tr>';
      html +='<td>&nbsp;</td>';
       html +=' <td><span style="color:red"><strong>您已举报此帖子，请勿重复提交</strong></span></td>';
     html +=' </tr>                                          ';     
      
      
      html +='<tr>';
      html +='<td>&nbsp;</td>';
     html +=' </tr>                                          ';            
     html +=' <tr>';
     html +='   <td>&nbsp;</td>';
     html += '  <td><input type="button" value="关闭" onclick="$("' + div.id + '").style.display="none""/></td>';
     html +=' </tr>';
    html +='</table>';
    html +='</form>';
    div.innerHTML =html;
    document.body.appendChild (div);
    }
    showMenu(id);
    return;
	}
	
	
	if(!$(id + "_menu"))
	{
	var div = document.createElement("DIV");
	var reportid='reportmessage'+index;
	div.id = id + "_menu";
	div.style.display = "none";
	div.style.width='270px';
	div.style.borderWidth='8px';
	div.style.borderColor='#CCC';
	div.className='popupmenu_popup';
	var html ='<form id="commentform">';
	    html +='<table border="0" cellpadding="0" cellspacing="0">';	
			  html +='<tr>';
		html +='<td>&nbsp;</td>';
	   html +=' <td>请输入举报理由（15字以上）<br/><span style="color:red"><strong>只需要点击一次提交即可</strong></span></td>';
	 html +=' </tr>                                          ';     
		
		
	  html +='<tr>';
		html +='<td>&nbsp;</td>';
	   html +=' <td><textarea name="'+reportid+'" cols="38" rows="3" id="'+reportid+'" style="width:250px;height:100px;resize:none"></textarea></td>';
	 html +=' </tr>                                          ';            
	 html +=' <tr>';
	 html +='   <td>&nbsp;</td>';
	 html += '  <td><input type="button" value="提交" onclick="send_report(' + fid + ',\'' + id + '\',' + index + ',\'' + disableinfo + '\',' + tid + ',' + pid + ')"/></td>';
	 html +=' </tr>';
	html +='</table>';
	html +='</form>';
  div.innerHTML =html;
  document.body.appendChild (div);
	}
	showMenu(id);
	}