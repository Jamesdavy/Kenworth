using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using NLog;
using WebApplication.Infrastructure.Nlog.Targets;

namespace Mariner.SignalR
{
    public class SignalRTargetHub : Hub
    {
        private const String NLogGroup = "NLogGroup";


        public SignalRTargetHub()
        {
            SignalRTarget.Instance.LogEventHandler = Send;
        }


        public void Listen()
        {
            Groups.Add(Context.ConnectionId, NLogGroup);
        }


        public void Send(String message, LogEventInfo logEventInfo)
        {
            Clients.Group(NLogGroup).logEvent(message, logEventInfo);
        }
    }

}