using System;
using System.Text;
using Discuz.Forum.ScheduledEvents;
using Discuz.Forum;

namespace Discuz.Event
{
    /// <summary>
    /// 有关论坛的计划任务 禁封2周无上传
    /// </summary>
    public class PrivateBTUserEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            //用户vip和2周无上传禁封
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTUsers.MoonEvents));
        }

        #endregion
    }
    /// <summary>
    /// 有关论坛的计划任务 发放邀请  按需执行
    /// </summary>
    public class PrivateBTUserAddInviteKey : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {

            //每人发放3只邀请
            //C//PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PrivateBTInvitation.EveryAddInviteCode));

        }

        #endregion
    }
    /// <summary>
    /// 有关论坛的计划任务 发送短消息 按需执行
    /// </summary>
    public class PrivateBTUserSendMsg : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {

            //快速发送论坛短消息
            //C//PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PrivateBTInvitation.EverySendMsg));

        }

        #endregion
    }
    /// <summary>
    /// 有关论坛的计划任务 邀请用户奖励
    /// </summary>
    public class PrivateBTInviteAward : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {

            //发放邀请用户奖励
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PrivateBTInvitation.InviteAward));

        }

        #endregion
    }
}