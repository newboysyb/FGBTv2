using System;
using System.Text;
using System.Data;

using Discuz.Common;
using Discuz.Data;

namespace Discuz.Forum
{
    /// <summary>
    /// 积分转帐历史记录操作类
    /// </summary>
    public class CreditsLogs
    {

        /// <summary>
        /// 添加积分转帐兑换和充值记录
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="fromto">来自/到</param>
        /// <param name="sendcredits">付出积分类型</param>
        /// <param name="receivecredits">得到积分类型</param>
        /// <param name="send">付出积分数额</param>
        /// <param name="receive">得到积分数额</param>
        /// <param name="paydate">时间</param>
        /// <param name="operation">积分操作(1=兑换, 2=转帐, 3=购买邀请, 4=种子管理, 5=系统处罚, 6=邀请用户奖励 ,7=邀请用户处罚, 8=管理转账，9=管理操作，11=发布悬赏，12=结束悬赏，13=无答案结束悬赏，14=管理减扣流量，15博彩投注，16博彩发布，17复活赠送，18下载分享种子)</param>
        /// <returns>执行影响的行</returns>
        public static int AddCreditsLog(int uid, int fromto, int sendcredits, int receivecredits, float send, float receive, string paydate, int operation)
        {
            return uid > 0 ? Discuz.Data.CreditsLogs.AddCreditsLog(uid, fromto, sendcredits, receivecredits, send, receive, paydate, operation) : 0;
        }

        /// <summary>
        /// 返回指定范围的积分日志
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="currentpage">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <returns>积分日志</returns>
        public static DataTable GetCreditsLogList(int pagesize, int currentpage, int uid)
        {
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】替换整个函数，更详细的分类
            //return (uid > 0 && currentpage > 0) ? Discuz.Data.CreditsLogs.GetCreditsLogList(pagesize, currentpage, uid) : new DataTable();

            if(uid > 0 && currentpage > 0)
            {
                DataTable dt = Discuz.Data.CreditsLogs.GetCreditsLogList(pagesize, currentpage, uid);

                string payintype = "";
                string payouttype = "";
                if (dt != null)
                {

                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "operationinfo";
                    dc.DataType = System.Type.GetType("System.String");
                    dc.DefaultValue = "";
                    dc.AllowDBNull = false;
                    dt.Columns.Add(dc);
                    dc = new DataColumn();
                    dc.ColumnName = "sendstr";
                    dc.DataType = System.Type.GetType("System.String");
                    dc.DefaultValue = "";
                    dc.AllowDBNull = false;
                    dt.Columns.Add(dc);
                    dc = new DataColumn();
                    dc.ColumnName = "receivestr";
                    dc.DataType = System.Type.GetType("System.String");
                    dc.DefaultValue = "";
                    dc.AllowDBNull = false;
                    dt.Columns.Add(dc);
                    foreach (DataRow dr in dt.Rows)
                    {
                        //判断日期：新系统更细后的记录
                        decimal ValueMulti = 1M;
                        if (TypeConverter.ObjectToDateTime(dr["paydate"]) < DateTime.Parse("2012-04-30"))
                        {
                            ValueMulti = 1024 * 1024M;
                        }

                        //计算付出和获得数值
                        if (dr["sendcredits"].ToString() == "2")
                        { 
                            payouttype = "金币" + dr["send"].ToString();
                        }
                        if (dr["sendcredits"].ToString() == "3")
                        {
                            payouttype = "上传" + PTTools.Upload2Str(TypeConverter.ObjectToDecimal(dr["send"]) * ValueMulti);
                        }
                        if (dr["receivecredits"].ToString() == "2")
                        {
                            payintype = "金币" + dr["receive"].ToString();
                        }
                        if (dr["receivecredits"].ToString() == "3")
                        {
                            payintype = "上传" + PTTools.Upload2Str(TypeConverter.ObjectToDecimal(dr["receive"]) * ValueMulti);
                        }
                        dr["sendstr"] = payouttype;
                        dr["receivestr"] = payintype;
                        


                        if (dr["operation"].ToString() == "1")
                        {
                            dr["operationinfo"] = string.Format("[兑换]，将{0}兑换为{1}", payouttype, payintype);
                        }
                        if (dr["operation"].ToString() == "2")
                        {
                            if (dr["uid"].ToString() == uid.ToString())
                            {
                                dr["operationinfo"] = string.Format("[转账：转出]，用户 {0} 收到 {1}", dr["touser"], payouttype);
                                dr["receivestr"] = "-";
                            }
                            else if (dr["fromto"].ToString() == uid.ToString())
                            {
                                dr["operationinfo"] = string.Format("[转账：转入]，由用户 {0} 支付 {1}", dr["fromuser"], payintype);
                                dr["sendstr"] = "-";
                            }

                        }
                        if (dr["operation"].ToString() == "3")
                        {
                            dr["operationinfo"] = string.Format("[购买邀请]，支出  {0}", payouttype);
                            dr["receivestr"] = "-";
                        }
                        if (dr["operation"].ToString() == "4")
                        {
                            if (dr["fromto"].ToString() == dr["uid"].ToString() && dr["uid"].ToString() == uid.ToString())
                            {
                                if (dr["send"].ToString() == "0")
                                {
                                    dr["operationinfo"] = "[种子管理]，奖励自己 " + payintype;
                                    dr["sendstr"] = "-";
                                }
                                else if (dr["receive"].ToString() == "0")
                                {
                                    dr["operationinfo"] = "[种子管理]，扣除自己 " + payouttype;
                                    dr["receivestr"] = "-";
                                }
                            }
                            else if (dr["fromto"].ToString() == uid.ToString())
                            {
                                if (dr["send"].ToString() == "0")
                                {
                                    dr["operationinfo"] = "[种子管理]，奖励用户 " + dr["fromuser"].ToString() + " " + payintype;
                                    dr["sendstr"] = "-";
                                    dr["receivestr"] = "-";
                                }
                                else if (dr["receive"].ToString() == "0")
                                {
                                    dr["operationinfo"] = "[种子管理]，扣除用户 " + dr["fromuser"].ToString() + " " + payouttype;
                                    dr["sendstr"] = "-";
                                    dr["receivestr"] = "-";
                                }
                            }
                            else if (dr["uid"].ToString() == uid.ToString())
                            {
                                if (dr["send"].ToString() == "0")
                                {
                                    dr["operationinfo"] = "[发布种子奖励]，获得奖励 " + payintype;
                                    dr["sendstr"] = "-";
                                }
                                else if (dr["receive"].ToString() == "0")
                                {
                                    dr["operationinfo"] = "[发布种子处罚]，被扣除 " + payouttype;
                                    dr["receivestr"] = "-";
                                }
                            }
                        }
                        if (dr["operation"].ToString() == "5")
                        {
                            dr["operationinfo"] = "[系统处罚]，被自动扣除 " + payouttype;
                            dr["receivestr"] = "-";
                        }
                        if (dr["operation"].ToString() == "6")
                        {
                            dr["operationinfo"] = "[邀请用户奖励]，获得奖励 " + payintype;
                            dr["sendstr"] = "-";
                        }
                        if (dr["operation"].ToString() == "7")
                        {
                            dr["operationinfo"] = "[邀请用户处罚]，被扣除" + payouttype;
                            dr["receivestr"] = "-";
                        }
                        if (dr["operation"].ToString() == "8")
                        {
                            if (dr["fromto"].ToString() == uid.ToString())
                            {
                                if (dr["receive"].ToString() == "0")
                                {
                                    dr["operationinfo"] = "[管理转账]，支付来源";
                                    dr["receivestr"] = "-";
                                    dr["sendstr"] = "-";
                                }
                                else
                                {
                                    dr["operationinfo"] = "[管理转账]，收到来自管理员" + dr["touser"].ToString() + " " + payintype;
                                    dr["sendstr"] = "-";
                                }
                            }
                            else
                            {
                                dr["operationinfo"] = "[管理转账]，转给用户" + dr["touser"].ToString() + " " + payouttype;
                                if (dr["receive"].ToString() == "0")
                                {
                                    dr["sendstr"] = "-";
                                    dr["receivestr"] = "-";
                                }
                                else
                                {
                                    dr["sendstr"] = "-";
                                    dr["receivestr"] = "-";
                                }
                            }
                        }
                        if (dr["operation"].ToString() == "9")
                        {
                            dr["operationinfo"] = "[管理操作] ";
                            dr["sendstr"] = "-";
                            dr["receivestr"] = "-";
                        }
                        if (dr["operation"].ToString() == "11")
                        {
                            dr["operationinfo"] = "[发布悬赏]，支付悬赏及税金保证金 " + payouttype;
                            dr["receivestr"] = "-";
                        }
                        if (dr["operation"].ToString() == "12")
                        {
                            if (dr["uid"].ToString() == uid.ToString() && dr["fromto"].ToString() == uid.ToString())
                            {
                                dr["operationinfo"] = "[结束悬赏]，返还保证金 " + payintype;
                                dr["sendstr"] = "-";
                            }
                            else if (dr["uid"].ToString() == uid.ToString() && dr["fromto"].ToString() != uid.ToString())
                            {
                                dr["operationinfo"] = "[结束悬赏]，支付赏金 " + payouttype;
                                dr["sendstr"] = "-";
                                dr["receivestr"] = "-";
                            }
                            else
                            {
                                dr["operationinfo"] = "[结束悬赏]，悬赏收益" + payintype;
                                dr["sendstr"] = "-";
                            }
                        }
                        if (dr["operation"].ToString() == "13")
                        {
                            if (dr["uid"].ToString() == uid.ToString() && dr["fromto"].ToString() == uid.ToString())
                            {
                                dr["operationinfo"] = "[无答案结束悬赏]，返还保证金及悬赏 " + payintype;
                                dr["sendstr"] = "-";
                            }
                            if (dr["uid"].ToString() != uid.ToString() && dr["fromto"].ToString() == uid.ToString())
                            {
                                dr["operationinfo"] = "[管理操作]，无答案结束悬赏，返还保证金及悬赏" + payintype;
                                dr["sendstr"] = "-";
                                dr["receivestr"] = "-";
                            }
                        }
                        if (dr["operation"].ToString() == "14")
                        {
                            if (dr["uid"].ToString() == uid.ToString() && dr["fromto"].ToString() == uid.ToString())
                            {
                                dr["operationinfo"] = "[系统维护]，系统维护操作 " + payouttype;
                                dr["receivestr"] = "-";
                            }
                            if (dr["uid"].ToString() == uid.ToString() && dr["fromto"].ToString() != uid.ToString())
                            {
                                dr["operationinfo"] = "[管理操作]，减扣流量 " + payouttype;
                                dr["receivestr"] = "-";
                            }
                            if (dr["uid"].ToString() != uid.ToString() && dr["fromto"].ToString() == uid.ToString())
                            {
                                dr["operationinfo"] = "[管理操作]，减扣用户流量 " + payouttype;
                                dr["sendstr"] = "-";
                                dr["receivestr"] = "-";
                            }
                        }
                        if (dr["operation"].ToString() == "18")
                        {
                            if (dr["fromto"].ToString() == uid.ToString())
                            {
                                dr["operationinfo"] = "[文件交换种子]，收到 " + dr["sendstr"];
                                dr["receivestr"] = dr["sendstr"];
                                dr["sendstr"] = "-";
                            }
                            else
                            {
                                dr["operationinfo"] = "[文件交换种子]，支付 " + dr["sendstr"];
                                dr["receivestr"] = "-";
                            }
                        }
                    }
                }
                dt.Dispose();

                return dt;
            }
            else return new DataTable();

            
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
        }

        /// <summary>
        /// 获得指定用户的积分交易历史记录总条数
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>历史记录总条数</returns>
        public static int GetCreditsLogRecordCount(int uid)
        {
            return uid > 0 ? Discuz.Data.CreditsLogs.GetCreditsLogRecordCount(uid) : 0;
        }
    }

}
