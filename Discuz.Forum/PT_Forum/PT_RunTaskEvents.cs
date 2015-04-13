using System;
using System.Text;
using Discuz.Forum.ScheduledEvents;
using Discuz.Forum;
using Discuz.Entity;

namespace Discuz.Forum
{
    public delegate void TaskEventDelegate();
    public class PTRunTaskEvents
    {
        public static void RunTaskEvents(TaskEventDelegate process)
        {
            DateTime tskstart = DateTime.Now;
            PTLog.InsertSystemLog(PTLog.LogType.ScheduledEvent, PTLog.LogStatus.Detail, "Task Start", "执行任务" + process.Method.Name);
            try
            {
                process.Invoke();
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.PeerTaskEvent, PTLog.LogStatus.Exception, "Task Exception", process.Method.Name + "异常" + ex.ToString());
            }
            PTLog.InsertSystemLog(PTLog.LogType.ScheduledEvent, PTLog.LogStatus.Detail, "Task End", "任务结束 " + process.Method.Name + " 耗时：" + (DateTime.Now - tskstart).TotalSeconds.ToString("0.0"));
        }
    }
}
