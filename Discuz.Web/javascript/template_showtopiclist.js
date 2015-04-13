var loadmoreimage_loading = 0;
var wfobj = null;
var new_addnewbarstr = "";
function loadmoreimage()
{
  if(loadmoreimage_loading == 0)
  {
    loadmoreimage_loading = 1;
    if(curid < 10)
    {
       $("addnewbar").innerHTML = '<img src="' + IMGDIR + '/loading.gif"> 加载中，请稍候...';
       _sendRequest("tools/ajax.aspx?t=imagewaterfall&reason=" + curid + "&fidlist=" + fidlist, addnewimage, false);
    }
    else
    {
      $("addnewbar").innerHTML = "没有了...";
    }
  }
}
function addnewimage(doc)
{
  docadd = doc + "";
  //if($('wf-main').style.height == null || $('wf-main').style.height == "")
  //{
  //    $('wf-main').style.height = 
  //    alert("no!");
  //}
  if(docadd.length > 0 && curid < 10)
  {
      $("wf-main").innerHTML = $("wf-main").innerHTML + docadd;
      new_addnewbarstr = "点击或下拉加载更多";
      curid = curid + 1;
  }
  else if(docadd.length > 0 && curid == 10)
  {
     new_addnewbarstr = "没有了...";
  }
  else
  {
     new_addnewbarstr = "没有了...";
  }
  setTimeout(DoOnWindowResize, 500);
  setTimeout(setaddnewbarstring, 1500);
  loadmoreimage_loading = 0;
}
function setaddnewbarstring()
{
    $("addnewbar").innerHTML = new_addnewbarstr;
}
function Waterfall(param)
{
    this.id = typeof param.container == 'string' ? document.getElementById(param.container) : param.container;
    this.colWidth = param.colWidth;
    this.colCount = param.colCount || 4;
    this.cls = param.cls && param.cls != '' ? param.cls : 'wf-cld';
    this.imgcls = param.imgcls && param.imgcls != '' ? param.imgcls : 'fgbtf_roundimg';
    this.lastsetid = 0;
    this.init();
}
Waterfall.prototype = 
{
    getByClass:function(cls,p){
        var arr = [],reg = new RegExp("(^|\\s+)" + cls + "(\\s+|$)","g");
        var nodes = p.getElementsByTagName("*"),len = nodes.length;
        for(var i = 0; i < len; i++){
            if(reg.test(nodes[i].className)){
                arr.push(nodes[i]);
                reg.lastIndex = 0;
            }
        }
        return arr;
    },
    maxArr:function(arr)
    {
        var len = arr.length,temp = arr[0];
        for(var ii= 1; ii < len; ii++){
            if(temp < arr[ii]){
                temp = arr[ii];
            }
        }
        return temp;
    },
    getMar:function(node)
    {
        var dis = 0;
        if(node.currentStyle)
        {
            dis = parseInt(node.currentStyle.marginBottom);
        }else if(document.defaultView){
            dis = parseInt(document.defaultView.getComputedStyle(node,null).marginBottom);
        }
        return dis;
    },
    getMinCol:function(arr)
    {
        var ca = arr,cl = ca.length,temp = ca[0],minc = 0;
        for(var ci = 0; ci < cl; ci++){
            if(temp > ca[ci]){
                temp = ca[ci];
                minc = ci;
            }
        }
        return minc;
    },
    init:function()
    {
        var _this = this;
        var col = [],
            iArr = [];
        var nodes = _this.getByClass(_this.cls,_this.id),len = nodes.length;
        var imgnodes = _this.getByClass(_this.imgcls,_this.id),imglen = imgnodes.length;
        for(var i = _this.lastsetid; i < imglen; i++)
        {
            imgnodes[i].style.width = (_this.colWidth - 20) + "px";
            imgnodes[i].style.height = "auto";
        }
        for(var i = _this.lastsetid; i < len; i++)
        {
            nodes[i].style.width = (_this.colWidth - 20) + "px";
            nodes[i].style.height = "auto";
        }
        for(var i = _this.lastsetid; i < _this.colCount; i++)
        {
            col[i] = 0;
        }
        for(var i = _this.lastsetid; i < len; i++)
        {
            nodes[i].h = nodes[i].offsetHeight + _this.getMar(nodes[i]);
            iArr[i] = i;
        }
         
        for(var i = _this.lastsetid; i < len; i++)
        {
            var ming = _this.getMinCol(col);
            nodes[i].style.left = ming * _this.colWidth + "px";
            nodes[i].style.top = col[ming] + "px";
            var test = nodes[i].offsetHeight + _this.getMar(nodes[i]);
            //alert( "ming:" + ming + " left:" + nodes[i].style.left+ " top:" + nodes[i].style.top+ " colming:" + col[ming] + " addcolming:" + nodes[i].h + " addcoltest:" + test);
            //col[ming] += nodes[i].h;
            col[ming] += test;
            //_this.id.style.height = (_this.maxArr(col) + 200) + "px";
            
        }
         //alert("done!");
         _this.id.style.height = _this.maxArr(col) + "px";
         //alert("done!");
        
    }
};

function DoOnWindowResize()
{
    var ccount = parseInt($('wf-main').clientWidth / 224);
    var cwidth = parseInt($('wf-main').clientWidth / ccount);
    //alert("ccount:" + ccount + " cwidth:" + cwidth + " clientw:" + $('wf-main').clientWidth);
    if(wfobj == null || wfobj.colCount != ccount || wfobj.colWidth != cwidth )
    {
      wfobj = new Waterfall({
          "container":"wf-main",
          "colWidth":cwidth,
          "colCount":ccount})
    }
    else{wfobj.init();}
}
function getScrollTop(){
　　var scrollTop = 0, bodyScrollTop = 0, documentScrollTop = 0;
　　if(document.body){
　　　　bodyScrollTop = document.body.scrollTop;
　　}
　　if(document.documentElement){
　　　　documentScrollTop = document.documentElement.scrollTop;
　　}
　　scrollTop = (bodyScrollTop - documentScrollTop > 0) ? bodyScrollTop : documentScrollTop;
　　return scrollTop;
}
function getScrollHeight(){
　　var scrollHeight = 0, bodyScrollHeight = 0, documentScrollHeight = 0;
　　if(document.body){
　　　　bodyScrollHeight = document.body.scrollHeight;
　　}
　　if(document.documentElement){
　　　　documentScrollHeight = document.documentElement.scrollHeight;
　　}
　　scrollHeight = (bodyScrollHeight - documentScrollHeight > 0) ? bodyScrollHeight : documentScrollHeight;
　　return scrollHeight;
}
function getWindowHeight(){
　　var windowHeight = 0;
　　if(document.compatMode == "CSS1Compat"){
　　　　windowHeight = document.documentElement.clientHeight;
　　}else{
　　　　windowHeight = document.body.clientHeight;
　　}
　　return windowHeight;
}

var curid = 1;
window.onscroll = function()
{
  　if(getScrollTop() + getWindowHeight() == getScrollHeight())
    {
        if($("addnewbar").innerHTML == "点击或下拉加载更多")
        {
　　　　    loadmoreimage();
        }
　　}
}
window.addEventListener("resize", DoOnWindowResize, false);