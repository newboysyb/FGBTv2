using System;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
/// <summary>
/// 存储样式和正则字符串的结构体
/// </summary>
public struct Json
{
    public string cls;
    public Regex r;
}
/// <summary>
/// 执行匹配的字符串和样式对象
/// </summary>
public class RegExp
{
    /// <summary>
    /// 存储匹配项目的集合
    /// </summary>
    private ArrayList _rxs = new ArrayList();
    /// <summary>
    /// 添加要匹配的项目
    /// </summary>
    /// <param name="cls">样式</param>
    /// <param name="reg">正则字符串</param>
    /// <param name="op">匹配选项</param>
    public void Add(string cls, string reg, RegexOptions op)
    {
        Json j = new Json();
        j.cls = cls;
        j.r = new Regex(reg, op | RegexOptions.Compiled);
        _rxs.Add(j);
    }
    /// <summary>
    /// 获取匹配项目的集合，只读
    /// </summary>
    public ArrayList rxs
    {
        get { return _rxs; }
    }
}
/// <summary>
///代码高亮显示分析
/// </summary>
public class PTHightLight
{
    /// <summary>
    /// 获取显示的代码名称
    /// </summary>
    /// <param name="v">缩略名</param>
    /// <returns></returns>
    private static string Code(string v)
    {
        switch (v.ToLower())
        {
            case "as": return "ActionScript";
            case "css": return "CSS";
            case "cs": return "C#";
            case "js": return "JavaScript";
            case "php": return "PHP";
            case "sql": return "SQL";
            case "vbs": return "VBScript";
            default: return "";
        }
    }
    /// <summary>
    /// 初始化匹配语言
    /// </summary>
    /// <returns></returns>
    private static Hashtable Init()
    {
        if (HttpContext.Current.Application["HLRegex"] != null) return (Hashtable)HttpContext.Current.Application["HLRegex"];
        else
        {
            //添加匹配语言
            Hashtable ht = new Hashtable();
            RegExp re = new RegExp();
            #region vbs
            re.Add("str", "\"([^\"\\n]*?)\"", RegexOptions.None);
            re.Add("note", "'[^\r\n]*", RegexOptions.None);
            re.Add("kw", @"\b(elseif|if|then|else|select|case|end|for|while|wend|do|loop|until|abs|sgn|hex|oct|sqr|int|fix|round|log|"
                + "sin|cos|tan|len|mid|left|right|lcase|ucase|trim|ltrim|rtrim|replace|instr|instrrev|space|string|strreverse|split|cint"
                + "|cstr|clng|cbool|cdate|csng|cdbl|date|time|now|dateadd|datediff|dateserial|datevalue|year|month|day|hour|minute|second"
                + "|timer|timeserial|timevalue|weekday|monthname|array|asc|chr|filter|inputbox|join|msgbox|lbound|ubound|const|dim|erase|"
                + @"redim|randomize|rnd|isempty|mod|execute|not|and|or|xor|class(?!\s*=))\b", RegexOptions.IgnoreCase);
            ht.Add("vbs", re);//vbscript
            #endregion
            #region js
            re = new RegExp();
            re.Add("str", "\"[^\"\\n]*\"|'[^'\\n]*'", RegexOptions.None);
            re.Add("note", @"\/\/[^\n\r]*|\/\*[\s\S]*?\*\/", RegexOptions.None);
            re.Add("kw", @"\b(break|delete|function|return|typeof|case|do|if|switch|var|catch|else|in|this|void|continue|false|nstanceof|"
                + "throw|while|debugger|finally|new|true|with|default|for|null|try|abstract|double|goto|native|static|boolean|enum|implements"
                + "|package|super|byte|export|import|private|synchronized|char|extends|int|protected|throws|final|interface|public|transient"
                + @"|const|float|long|short|volatile|class(?!\s*=))\b", RegexOptions.None);
            ht.Add("js", re);//javascript
            #endregion
            #region sql
            re = new RegExp();
            re.Add("sqlstr", "'([^'\\n]*?)*'", RegexOptions.None);
            re.Add("note", @"--[^\n\r]*|\/\*[\s\S]*?\*\/", RegexOptions.None);
            re.Add("sqlconnect", @"\b(all|and|between|cross|exists|in|join|like|not|null|outer|or)\b", RegexOptions.IgnoreCase);
            re.Add("sqlfunc", @"\b(avg|case|checksum|current_timestamp|day|left|month|replace|year)\b", RegexOptions.IgnoreCase);
            re.Add("kw", @"\b(action|add|alter|after|as|asc|bigint|bit|binary|by|cascade|char|character|check|column|columns|constraint|create"
                + "|current_date|current_time|database|date|datetime|dec|decimal|default|delete|desc|distinct|double|drop|end|else|escape|file"
                + "|first|float|foreign|from|for|full|function|global|grant|group|having|hour|ignore|index|inner|insert|int|integer|into|if|is"
                + "|key|kill|load|local|max|minute|modify|numeric|no|on|option|order|partial|password|precision|primary|procedure|privileges"
                + "|read|real|references|restrict|returns|revoke|rows|second|select|set|shutdown|smallint|table|temporary|text|then|time"
                + @"|timestamp|tinyint|to|use|unique|update|values|varchar|varying|varbinary|with|when|where)\b", RegexOptions.IgnoreCase);
            ht.Add("sql", re);//sql
            #endregion
            #region php
            re = new RegExp();
            re.Add("str", "<<<([a-z0-9_]+)[\\r\\n]{1,2}[\\s\\S]*?[\\r\\n]{1,2}\\1;[\\r\\n]{1,2}|\"[^\"]*\"|'[^']*'", RegexOptions.IgnoreCase);
            re.Add("note", @"\/\/[^\n\r]*|\/\*[\s\S]*?\*\/", RegexOptions.None);
            re.Add("phpVar", @"(\$[a-z_0-9]+)", RegexOptions.IgnoreCase);
            //这里可以继续添加，但是不知道度少php，只加了一些
            re.Add("phpCls", @"\b(mysqli?_connect|mysqli?_select_db|mysqli?_query|mysqli?_fetch_array|mysqli?_connect_errno"
                + "|mysqli?_connect_error|mysqli?_num_rows|mysqli?_fetch_assoc|mysqli?_fetch_row|mysqli?_fetch_object|mysqli?_free_result"
                + "|mysqli?_close|session_start"
                + @"|mssql_data_seek|mssql_get_last_message|mssql_free_result|mssql_num_rows|mssql_fetch_array|mssql_select_db"
                + @"|mssql_fetch_array|mssql_rows_affected)\b", RegexOptions.IgnoreCase);
            re.Add("kw", @"\b(and|or|xor|__FILE__|exception|__LINE__|array|as|break|case|const|continue|declare|default|die|do|echo|else|elseif"
                + "|empty|enddeclare|endfor|endforeach|endif|endswitch|endwhile|eval|exit|extends|for|foreach|function|global|if|include"
                + "|include_once|isset|list|new|print|require|require_once|return|static|switch|unset|use|var|while|__FUNCTION__|__CLASS__"
                + "|__METHOD__|final|php_user_filter|interface|implements|extends|public|private|protected|abstract|clone|try|catch|throw"
                + @"|cfunction|old_function|this)\b", RegexOptions.None);
            ht.Add("php", re);//php
            #endregion
            #region csharp
            re = new RegExp();
            re.Add("str", "\"[^\"\\n]*\"", RegexOptions.None);
            re.Add("note", @"\/\/[^\n\r]*|\/\*[\s\S]*?\*\/", RegexOptions.None);
            re.Add("phpVar", @"(?<=\bclass\s+)([_a-z][_a-z0-9]*)(?=\s*[\{:])"
                + @"|(?<=\=\s*new\s+)([a-z_][a-z0-9_]*)(?=\s*\()"
                + @"|([a-z][a-z0-9_]*)(?=\s+[a-z_][a-z0-9_]*\s*=\s*new)", RegexOptions.IgnoreCase);
            re.Add("kw", @"\b(partial|abstract|event|get|set|value|new|struct|as|null|switch|base|object|this|bool|false|operator|throw|break"
                + "|finally|out|byte|fixed|override|try|case|float|params|typeof|catch|for|private|uint|char|foreach|protected|ulong|checked"
                + "|goto|public|unchecked|if|readonly|unsafe|const|implicit|ref|ushort|continue|in|return|using|decimal|int|sbyte|virtual"
                + "|default|interface|sealed|volatile|delegate|internal|short|void|do|is|sizeof|while|double|lock|stackalloc|else|long|static"
                + @"|enum|string|namespace|region|endregion|class(?!\s*=))\b", RegexOptions.None);
            ht.Add("cs", re);//c#
            #endregion
            #region css
            re = new RegExp();
            re.Add("note", @"\/\*[\s\S]*?\*\/", RegexOptions.None);
            re.Add("str", @"([\s\S]+)", RegexOptions.None);
            re.Add("kw", @"(\{[^\}]+\})", RegexOptions.None);
            re.Add("sqlstr", @"([a-z\-]+(?=\s*:))", RegexOptions.IgnoreCase);
            re.Add("black", @"([\{\}])", RegexOptions.None);
            ht.Add("css", re);//css
            #endregion
            #region xml
            re = new RegExp();
            re.Add("", @"<\?xml[^>]*>", RegexOptions.IgnoreCase);
            re.Add("", @"<!\[CDATA\[([\s\S]*?)\]\]>", RegexOptions.None);
            re.Add("", @"<!--([\s\S]*?)-->", RegexOptions.None);
            re.Add("", "<([^>]+)>", RegexOptions.IgnoreCase);
            ht.Add("xml", re);//xml
            #endregion
            #region html
            re = new RegExp();
            re.Add("", "<%@\\s*page[\\s\\S]*?language=['\"](.*?)[\"']", RegexOptions.IgnoreCase);
            re.Add("", @"<!--([\s\S]*?)-->", RegexOptions.None);
            re.Add("", @"(<script[^>]*>)([\s\S]*?)<\/script>", RegexOptions.IgnoreCase);
            re.Add("", @"<%(?!@)([\s\S]*?)%>", RegexOptions.None);
            re.Add("", @"<\?php\b([\s\S]*?)\?>", RegexOptions.IgnoreCase);
            re.Add("", @"(<style[^>]*>)([\s\S]*?)<\/style>", RegexOptions.IgnoreCase);
            re.Add("", @"&([a-z]+;)", RegexOptions.None);
            re.Add("", @"'.*?'", RegexOptions.None);
            re.Add("", "\".*?\"", RegexOptions.None);
            re.Add("", @"<([^>]+)>", RegexOptions.None);
            ht.Add("html", re);//html
            #endregion
            #region ubb
            re = new RegExp();
            re.Add("", @"\[(b)\]([\s\S]*?)\[/b\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(i)\]([\s\S]*?)\[/i\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(u)\]([\s\S]*?)\[/u\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(color)=([^\]]+)\]([\s\S]*?)\[/color\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(bcolor)=([^\]]+)\]([\s\S]*?)\[/bcolor\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(size)=([^\]]+)\]([\s\S]*?)\[/size\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(del)\]([\s\S]*?)\[/del\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(url)=([^\]]+)\]([\s\S]*?)\[/url\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(email)=([^\]]+)\]([\s\S]*?)\[/email\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(img)=([^\]]+)\]([\s\S]*?)\[/img\]", RegexOptions.IgnoreCase);
            re.Add("", @"\[(align)=([^\]]+)\]([\s\S]*?)\[/align\]", RegexOptions.IgnoreCase);
            ht.Add("ubb", re);
            #endregion
            HttpContext.Current.Application["HLRegex"] = ht;
            return ht;
        }
    }
    /// <summary>
    /// 将'或者"括起的字符串中的><字符替换为对应的实体
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    private static string NoteEntrityEval(Match m)
    {
        return "<span class='gray'>" + m.Groups[0].Value.Replace("<", "&lt;").Replace(">", "&gt;") + "</span>";
    }
    /// <summary>
    /// 替换字符串中的//假注释
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    private static string NoteStrEval(Match m)
    {
        return "<span class='gray'>" + m.Groups[0].Value.Replace("<", "&lt;").Replace(">", "&gt;") + "</span>";
    }
    /// <summary>
    /// 根据language参数分析指定，非xml和html用的
    /// </summary>
    /// <param name="v">要分析的字符串</param>
    /// <param name="language">何种语言</param>
    /// <param name="re">对应的正则对象</param>
    /// <returns>高亮显示后的代码</returns>
    private static string Parse(string v, string language, RegExp re)
    {
        //【】if (!UserCheck.IsNotNull(v)) return v;
        language = language.ToLower();
        v = v.Replace("<!--", "&lt;!--");
        Json json;
        ArrayList _string = new ArrayList(), _note = new ArrayList(), _xmlnote = new ArrayList();
        MatchCollection mc;
        int blockIndex = 0;
        #region 转义字符替换
        if (language != "css") v = v.Replace("\\\"", "__转义双引号__").Replace("\\'", "__转义单引号__");
        #endregion
        #region 字符串替换
        if (language != "css")
        {
            json = (Json)re.rxs[0];
            mc = json.r.Matches(v);
            foreach (Match m in mc)
            {
                _string.Add("<span class='" + json.cls + "'>" + m.Groups[0].Value.Replace("<", "&lt;") + "</span>");
                v = json.r.Replace(v, "__字符串" + blockIndex + "__", 1);
                blockIndex++;
            }
        }
        #endregion
        #region CS的xml注释替换
        blockIndex = 0;
        if (language == "cs")
        {
            Regex r = new Regex(@"((?<!/)///(?!/))([^\r\n]*)?"), attri = new Regex(@"(<[^>]+>)");
            mc = r.Matches(v);
            string tmp = "";
            foreach (Match m in mc)
            {
                tmp = m.Groups[2].Value;
                tmp = attri.Replace(tmp, NoteEntrityEval);
                _xmlnote.Add("<span class='note'><span class='gray'>///</span>" + tmp + "</span>");
                v = r.Replace(v, "__XML注释" + blockIndex + "__", 1);
                blockIndex++;
            }
        }
        #endregion
        #region 注释替换
        json = (Json)re.rxs[language == "css" ? 0 : 1];
        mc = json.r.Matches(v);
        blockIndex = 0;
        foreach (Match m in mc)
        {
            _note.Add("<span class='" + json.cls + "'>" + m.Groups[0].Value.Replace("<", "&lt;").Replace(">", "&gt;") + "</span>");
            v = json.r.Replace(v, "__注释" + blockIndex + "__", 1);
            blockIndex++;
        }
        #endregion
        #region 剩下的替换
        int i = language == "css" ? 1 : 2;
        for (; i < re.rxs.Count; i++)
        {
            json = (Json)re.rxs[i];
            if (language == "cs" && json.cls == "phpVar") v = json.r.Replace(v, "<span class='phpVar'>$1$2$3</span>");
            else v = json.r.Replace(v, "<span class='" + json.cls + "'>$1</span>");
        }
        #endregion
        #region 反替换处理
        if (language != "css") for (i = 0; i < _string.Count; i++) v = v.Replace("__字符串" + i + "__", _string[i].ToString());
        if (language == "cs") for (i = 0; i < _xmlnote.Count; i++) v = v.Replace("__XML注释" + i + "__", _xmlnote[i].ToString());
        for (i = 0; i < _note.Count; i++) v = v.Replace("__注释" + i + "__", _note[i].ToString());
        // HttpContext.Current.Response.Write(_xmlnote.Count + "---<br>");
        if (language != "css")
        {
            //【】替换字符串里面可能存在的xml注释或者注释
            //if (v.IndexOf("__XML注释") != -1)
            //    for (i = 0; i < _string.Count; i++) for (i = 0; i < _xmlnote.Count; i++)
            //            v = v.Replace("__XML注释" + i + "__", Format.RemoveHTML(_xmlnote[i].ToString()));
            //if (v.IndexOf("__注释") != -1)
            //    for (i = 0; i < _string.Count; i++) for (i = 0; i < _note.Count; i++) v = v.Replace("__注释" + i + "__", Format.RemoveHTML(_note[i].ToString()));
            //if (v.IndexOf("__字符串") != -1) for (i = 0; i < _string.Count; i++) v = v.Replace("__字符串" + i + "__", Format.RemoveHTML(_string[i].ToString()));

            if (v.IndexOf("__XML注释") != -1)
                for (i = 0; i < _xmlnote.Count; i++)
                    v = v.Replace("__XML注释" + i + "__", _xmlnote[i].ToString());
            v = v.Replace("__转义双引号__", "\\\"").Replace("__转义单引号__", "\\'");
        }
        #endregion
        return v;
    }
    /// <summary>
    /// xml替换函数
    /// </summary>
    /// <param name="m">匹配组</param>
    /// <returns>替换成的字符串</returns>
    private static string XMLEval(Match m)
    {
        string tmp = m.Groups[1].Value;
        if (tmp.StartsWith("/"))
            return "<span class='kw'>&lt;/<span class='str'>" + tmp.Substring(1) + "</span>&gt;</span>";
        else if (new Regex("^[_0-9a-z]+$", RegexOptions.IgnoreCase).IsMatch(tmp))
            return "<span class='kw'>&lt;<span class='str'>" + tmp + "</span>&gt;</span>";
        else
        {
            Regex r = new Regex("([a-z_][a-z_0-9\\.]*)\\s*=\\s*\"([^\"]*)\"", RegexOptions.IgnoreCase);
            tmp = r.Replace(tmp, "<span class=\"str\">$1</span>=\"<span class=\"black\">$2</span>\"");
            r = new Regex("([a-z_][a-z_0-9\\.]*)\\s*=\\s*'([^']*)'", RegexOptions.IgnoreCase);
            tmp = r.Replace(tmp, "<span class=\"str\">$1</span>='<span class=\"black\">$2</span>'");
            r = new Regex("^([a-z_0-9\\.]+)", RegexOptions.IgnoreCase);
            tmp = r.Replace(tmp, "<span class=\"str\">$1</span>");
            return "<span class='kw'>&lt;" + tmp + "&gt;</span>";
        }
    }
    /// <summary>
    /// 分析xml字符串
    /// </summary>
    /// <param name="v">要分析的字符串</param>
    /// <param name="re">对应的正则对象</param>
    /// <returns>高亮格式化后的字符</returns>
    private static string ParseXML(string v, RegExp re)
    {
        #region 变量定义
        string declare = "";//xml申明
        ArrayList cdata = new ArrayList(), note = new ArrayList();//cdata和注释内容
        Regex r;
        Match m;
        MatchCollection mc;
        int blockIndex = 0;//要替换的块编号
        #endregion
        #region 处理申明
        r = ((Json)re.rxs[0]).r;
        m = r.Match(v);
        declare = "<span class='kw'>" + m.Groups[0].Value.Replace("<", "&lt;") + "</span>";
        v = r.Replace(v, "__申明__", 1);
        #endregion
        #region 处理CDATA
        r = ((Json)re.rxs[1]).r;
        mc = r.Matches(v);
        foreach (Match _m in mc)
        {
            cdata.Add("<span class='kw'>&lt;![CDATA[<span class='gray'>" + _m.Groups[0].Value.Replace("<", "&lt;") + "</span>]]></span>");
            v = r.Replace(v, "__CDATA" + blockIndex + "__", 1);
            blockIndex++;
        }
        #endregion
        #region 处理注释
        r = ((Json)re.rxs[2]).r;
        mc = r.Matches(v);
        blockIndex = 0;
        foreach (Match _m in mc)
        {
            note.Add("<span class='kw'>&lt;!--<span class='gray'>" + _m.Groups[1].Value.Replace("<", "&lt;") + "</span>--&gt;</span>");
            v = r.Replace(v, "__注释" + blockIndex + "__", 1);
            blockIndex++;
        }
        #endregion
        #region 处理节点和属性
        r = ((Json)re.rxs[3]).r;
        v = r.Replace(v, XMLEval);
        #endregion
        #region 反替换处理
        v = v.Replace("__申明__", declare);
        int i;
        for (i = 0; i < note.Count; i++) v = v.Replace("__注释" + i + "__", note[i].ToString());
        for (i = 0; i < cdata.Count; i++) v = v.Replace("__CDATA" + i + "__", cdata[i].ToString());
        #endregion
        return v;
    }
    /// <summary>
    /// 将'或者"括起的字符串中的&lt;&gt;字符替换为对应的实体
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    private static string EntrityEval(Match m)
    {
        return m.Groups[0].Value.Replace("<", "&lt;").Replace(">", "&gt;");
    }
    /// <summary>
    /// html属性处理函数
    /// </summary>
    /// <param name="m">匹配组</param>
    /// <returns>替换成的字符串</returns>
    private static string HTMLEval(Match m)
    {
        string tmp = m.Groups[1].Value;
        if (tmp.StartsWith("/"))
            return "<span class='kw'>&lt;/<span class='str'>" + tmp.Substring(1) + "</span>&gt;</span>";
        else if (new Regex(@"^([_0-9a-z]+)\s*\/$", RegexOptions.IgnoreCase).IsMatch(tmp))
            return "<span class='kw'>&lt;<span class='str'>" + tmp.Substring(0, tmp.Length - 1) + "</span>&gt;</span>";
        else if (tmp.ToLower().StartsWith("!doctype"))
        {
            tmp = "<span class='kw'>" + m.Groups[0].Value.Substring(1) + "</span>";
            tmp = new Regex(@"\b(html|public)\b", RegexOptions.IgnoreCase).Replace(tmp, "<span class='sqlstr'>$1</span>");
            return "<span class='kw'>&lt;!" + tmp + "&gt;</span>";
        }
        else
        {
            Regex r = new Regex("([a-z_][a-z_0-9\\.\\-]*)\\s*=\\s*\"([^\"]*)\"", RegexOptions.IgnoreCase);
            tmp = r.Replace(tmp, "<span class=\"sqlstr\">$1</span><span class=\"kw\">=\"$2\"</span>");
            r = new Regex("([a-z_][a-z_0-9\\.\\-]*)\\s*=\\s*'([^']*)'", RegexOptions.IgnoreCase);
            tmp = r.Replace(tmp, "<span class=\"sqlstr\">$1</span><span class=\"kw\">='$2'</span>");
            r = new Regex("([a-z_][a-z_0-9\\-]*)\\s*=\\s*(?!['\"])(\\w+)", RegexOptions.IgnoreCase);
            tmp = r.Replace(tmp, "<span class=\"sqlstr\">$1</span><span class=\"kw\">=$2</span>");

            r = new Regex(@"^([a-z_0-9\-]+)", RegexOptions.IgnoreCase);
            tmp = r.Replace(tmp, "<span class='str'>$1</span>");
            if (tmp.StartsWith("%@")) return "<span class='str'><span class='declare'>&lt;%</span><span class='kw'>@</span>"
                    + tmp.Trim(new char[] { '%', '@' }) + "<span class='declare'>%&gt;</span></span>";
            return "<span class='kw'>&lt;" + tmp + "&gt;</span>";
        }
    }
    /// <summary>
    /// 分析html字符串，还要分解出代码块
    /// </summary>
    /// <param name="v">要分析的字符串</param>
    /// <param name="rxs">语言正则集合</param>
    /// <returns>高亮格式化后的字符</returns>
    private static string ParseHTML(string v, Hashtable rxs)
    {
        string lang = "VB";
        RegExp re = (RegExp)rxs["html"];
        Regex r = ((Json)re.rxs[0]).r, htmlR = ((Json)re.rxs[9]).r;
        Match m = r.Match(v);
        MatchCollection mc;
        ArrayList note = new ArrayList(), vb = new ArrayList(), js = new ArrayList(), php = new ArrayList()
            , cs = new ArrayList(), css = new ArrayList();
        int blockIndex = 0;
        if (m.Groups[1].Value.Trim() != "") lang = m.Groups[1].Value.ToUpper().Trim();//获取页面默认语言
        if (lang != "C#") lang = "VB";
        #region 转义字符替换
        v = v.Replace("\\\"", "__转义双引号__").Replace("\\'", "__转义单引号__");
        #endregion
        #region 处理script标签
        r = ((Json)re.rxs[2]).r;
        mc = r.Matches(v);
        foreach (Match _m in mc)
        {
            if (_m.Groups[1].Value.ToLower().IndexOf("runat") == -1)
            {
                if (_m.Groups[1].Value.ToLower().IndexOf("vbscript") == -1)//客户端js
                {
                    blockIndex = js.Count;
                    js.Add(htmlR.Replace(_m.Groups[1].Value, HTMLEval) +
                        (_m.Groups[2].Value.Trim() != "" ? Parse(_m.Groups[2].Value, "js", (RegExp)rxs["js"]) : "")
                       + "<span class=\"kw\">&lt;/<span class=\"str\">script</span>&gt;</span> ");
                    v = r.Replace(v, "__JS" + blockIndex + "__", 1);
                }
                else//客户端的vbs
                {
                    blockIndex = vb.Count;
                    vb.Add(htmlR.Replace(_m.Groups[1].Value, HTMLEval) +
                        (_m.Groups[2].Value.Trim() != "" ? Parse(_m.Groups[2].Value, "vbs", (RegExp)rxs["vbs"]) : "")
                       + "<span class=\"kw\">&lt;/<span class=\"str\">script</span>&gt;</span> ");
                    v = r.Replace(v, "__VB" + blockIndex + "__", 1);
                }
            }
            else//服务器端脚本
            {
                if (lang == "C#")//默认语言为c#
                {
                    if (_m.Groups[1].Value.ToLower().IndexOf("vb") == -1)//如果为执行语言为vb
                    {
                        blockIndex = cs.Count;
                        cs.Add(htmlR.Replace(_m.Groups[1].Value, HTMLEval) +
                            (_m.Groups[2].Value.Trim() != "" ? Parse(_m.Groups[2].Value, "cs", (RegExp)rxs["cs"]) : "")
                            + "<span class=\"kw\">&lt;/<span class=\"str\">script</span>&gt;</span> ");
                        v = r.Replace(v, "__C#" + blockIndex + "__", 1);
                    }
                    else
                    {
                        blockIndex = vb.Count;
                        vb.Add(htmlR.Replace(_m.Groups[1].Value, HTMLEval) +
                            (_m.Groups[2].Value.Trim() != "" ? Parse(_m.Groups[2].Value, "vbs", (RegExp)rxs["vbs"]) : "")
                            + "<span class=\"kw\">&lt;/<span class=\"str\">script</span>&gt;</span> ");
                        v = r.Replace(v, "__VB" + blockIndex + "__", 1);
                    }
                }
                else
                {
                    if (_m.Groups[1].Value.ToLower().IndexOf("C#") != -1)
                    {
                        blockIndex = cs.Count;
                        cs.Add(htmlR.Replace(_m.Groups[1].Value, HTMLEval) +
                            (_m.Groups[2].Value.Trim() != "" ? Parse(_m.Groups[2].Value, "cs", (RegExp)rxs["cs"]) : "")
                            + "<span class=\"kw\">&lt;/<span class=\"str\">script</span>&gt;</span> ");
                        v = r.Replace(v, "__C#" + blockIndex + "__", 1);
                    }
                    else
                    {
                        blockIndex = vb.Count;
                        vb.Add(htmlR.Replace(_m.Groups[1].Value, HTMLEval) +
                            (_m.Groups[2].Value.Trim() != "" ? Parse(_m.Groups[2].Value, "vbs", (RegExp)rxs["vbs"]) : "")
                            + "<span class=\"kw\">&lt;/<span class=\"str\">script</span>&gt;</span> ");
                        v = r.Replace(v, "__VB" + blockIndex + "__", 1);
                    }
                }

            }
        }
        #endregion
        #region 处理style代码块
        r = ((Json)re.rxs[5]).r;
        mc = r.Matches(v);
        blockIndex = 0;
        foreach (Match _m in mc)
        {
            css.Add(htmlR.Replace(_m.Groups[1].Value, HTMLEval) +
                    (_m.Groups[2].Value.Trim() != "" ? Parse(_m.Groups[2].Value, "css", (RegExp)rxs["css"]) : "")
                   + "<span class=\"kw\">&lt;/<span class=\"str\">style</span>&gt;</span> ");
            v = r.Replace(v, "__CSS" + blockIndex + "__", 1);
            blockIndex++;
        }
        #endregion
        #region 处理注释
        r = ((Json)re.rxs[1]).r;
        mc = r.Matches(v);
        blockIndex = 0;
        foreach (Match _m in mc)
        {
            note.Add("<span class='note'>&lt;!--" + _m.Groups[1].Value.Replace("<", "&lt;") + "--&gt;</span>");
            v = r.Replace(v, "__注释" + blockIndex + "__", 1);
            blockIndex++;
        }
        #endregion
        #region 处理<%%>包含的代码块
        r = ((Json)re.rxs[3]).r;
        mc = r.Matches(v);
        foreach (Match _m in mc)
        {
            if (lang == "VB")
            {
                blockIndex = vb.Count;
                vb.Add("<span class='declare'>&lt;%</span>" +
                        (_m.Groups[1].Value.Trim() != "" ? Parse(_m.Groups[1].Value, "vbs", (RegExp)rxs["vbs"]) : "")
                        + "<span class='declare'>%&gt;</span>");
            }
            else
            {
                blockIndex = cs.Count;
                cs.Add("<span class='declare'>&lt;%</span>" +
                        (_m.Groups[1].Value.Trim() != "" ? Parse(_m.Groups[1].Value, "cs", (RegExp)rxs["cs"]) : "")
                        + "<span class='declare'>%&gt;</span>");
            }
            v = r.Replace(v, "__" + lang + blockIndex + "__", 1);
        }
        #endregion
        #region 处理php代码块
        r = ((Json)re.rxs[4]).r;
        mc = r.Matches(v);
        blockIndex = 0;
        foreach (Match _m in mc)
        {
            php.Add("<span class='declare'>&lt;?php</span>" +
                        (_m.Groups[1].Value.Trim() != "" ? Parse(_m.Groups[1].Value, "php", (RegExp)rxs["php"]) : "")
                        + "<span class='declare'>?&gt;</span>");
            v = r.Replace(v, "__PHP" + blockIndex + "__", 1);
            blockIndex++;
        }
        #endregion
        #region html实体替换
        v = ((Json)re.rxs[6]).r.Replace(v, "&amp;$1");
        #endregion
        #region 处理html标签
        v = ((Json)re.rxs[7]).r.Replace(v, EntrityEval);
        v = ((Json)re.rxs[8]).r.Replace(v, EntrityEval);
        v = htmlR.Replace(v, HTMLEval);
        #endregion
        #region 反替换处理
        int i;
        //注释
        for (i = 0; i < note.Count; i++) v = v.Replace("__注释" + i + "__", note[i].ToString());
        v = v.Replace("__转义双引号__", "\\\"").Replace("__转义单引号__", "\\'");
        //样式表
        for (i = 0; i < css.Count; i++) v = v.Replace("__CSS" + i + "__", css[i].ToString());
        //C#
        for (i = 0; i < cs.Count; i++) v = v.Replace("__C#" + i + "__", cs[i].ToString());
        //VBScript或者vb
        for (i = 0; i < vb.Count; i++) v = v.Replace("__VB" + i + "__", vb[i].ToString());
        //php
        for (i = 0; i < php.Count; i++) v = v.Replace("__PHP" + i + "__", php[i].ToString());
        //javascript
        for (i = 0; i < js.Count; i++) v = v.Replace("__JS" + i + "__", js[i].ToString());
        #endregion

        return v;
    }
    /// <summary>
    /// UBB代码的替换
    /// </summary>
    /// <param name="m">匹配组</param>
    /// <returns></returns>
    public static string UBBEval(Match m)
    {
        switch (m.Groups[1].Value.ToLower())
        {
            case "color": return "<span style='color:#" + m.Groups[2].Value + "'>" + m.Groups[3] + "</span>";
            case "bcolor": return "<span style='background:#" + m.Groups[2].Value + "'>" + m.Groups[3] + "</span>";
            case "size": return "<span style='font-size:" + m.Groups[2].Value + "'>" + m.Groups[3] + "</span>";
            case "b": return "<b>" + m.Groups[2] + "</b>";
            case "i": return "<i>" + m.Groups[2] + "</i>";
            case "u": return "<span style='text-decoration:underline'>" + m.Groups[2] + "</span>";
            case "del": return "<span style='text-decoration:line-through'>" + m.Groups[2] + "</span>";
            case "url": return "<a target='_blank' href='" + m.Groups[2].Value + "'>" + m.Groups[3] + "</a>";
            case "email": return "<a href='mailto:" + m.Groups[2].Value + "'>" + m.Groups[3] + "</a>";
            case "img": return "<img src='" + m.Groups[2].Value + "'/>";
            default: return "<div style='text-align:" + m.Groups[2].Value + "'>" + m.Groups[3] + "</div>";
        }
    }
    /// <summary>
    /// 从字符串中的ubb代码中获取要分析的字符串和语言
    /// </summary>
    /// <param name="v">字符串</param>
    /// <param name="IsAdmin">是否为管理员，如果不是需要HTMLEncode</param>
    /// <returns>高亮格式化后的字符</returns>
    public static string Parse(string v, bool IsAdmin)
    {
        #region 代码部分
        //【】if (!UserCheck.IsNotNull(v)) return "";
        Hashtable rxs = Init(), langs = new Hashtable();
        ArrayList arr;
        string lang = "";
        Regex r = new Regex(@"\[code=(as|css|cs|html|js|php|sql|vbs|xml)\]([\s\S]*?)\[/code\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        MatchCollection mc = r.Matches(v);
        foreach (Match m in mc)
        {
            lang = m.Groups[1].Value;
            if (langs.Contains(lang))
            {
                arr = (ArrayList)langs[lang];
                arr.Add(m.Groups[2].Value);
            }
            else
            {
                arr = new ArrayList();
                arr.Add(m.Groups[2].Value);
                langs.Add(lang, arr);
            }
            v = r.Replace(v, "__" + lang + (arr.Count - 1) + "__", 1);
        }
        //在替换前把剩余的字符串替换为实体
        //【】v = Format.Encode(v);
        v = v.Replace("\r", "").Replace("\n", "<br/>");
        string tmpStr = "<div class='xcode'>"
            + "<span class='hide' onclick='CodeOp(this,true)' title='展开{0}代码块'>+展开</span>"
            + "<div class='codename' title='收缩' onclick='CodeOp(this)'>-{0}</div>"
            + "<div class='codebody'>{1}</div></div>", rpStr = "";
        foreach (DictionaryEntry de in langs)
        {
            lang = de.Key.ToString();
            arr = (ArrayList)de.Value;
            for (int i = 0; i < arr.Count; i++)
            {
                switch (lang.ToLower())
                {
                    case "html": rpStr = string.Format(tmpStr, "HTML", Encode(ParseHTML(arr[i].ToString(), rxs))); break;
                    case "xml": rpStr = string.Format(tmpStr, "XML", Encode(ParseXML(arr[i].ToString(), (RegExp)rxs["xml"]))); break;
                    default:
                        //as的脚本引擎和js1.5版本一样的
                        rpStr = string.Format(tmpStr, Code(lang)
                            , Encode(Parse(arr[i].ToString(), lang, (RegExp)rxs[lang.ToLower() == "as" ? "js" : lang.ToLower()])));
                        break;
                }
                v = v.Replace("__" + lang + i + "__", rpStr);
            }
        }
        #endregion
        #region ubb代码部分
        RegExp re = (RegExp)rxs["ubb"];
        for (int i = 0; i < re.rxs.Count; i++) v = ((Json)re.rxs[i]).r.Replace(v, UBBEval);
        #endregion
        return v;
    }
    /// <summary>
    /// 从字符串中的ubb代码中获取要分析的字符串和语言，默认为管理员使用的方法
    /// </summary>
    /// <param name="v">字符串</param>
    /// <returns>高亮格式化后的字符</returns>
    public static string Parse(string v)
    {
        return Parse(v, true);
    }
    /// <summary>
    /// 替换非格式化标签内的空白
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public static string GetSpace(Match m)
    {
        return m.Groups[1].Value.Replace(" ", "&nbsp;");
    }
    /// <summary>
    /// 将代码块中的\r,\n替换为html的br
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    private static string Encode(string v)
    {
        v = v.Replace("\r", "").Replace("\n", "<br/>");
        return Regex.Replace(v, "(?<!<span)( +)(?!class)", GetSpace, RegexOptions.Compiled);
    }
}