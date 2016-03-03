﻿using System;
using NLog;
using NLog.Targets;

namespace WebApplication.Infrastructure.Nlog.Targets
{
    [Target("SignalR")]
    public class SignalRTarget : TargetWithLayout
    {
        public static SignalRTarget Instance { get; private set; }
        public Action<String, LogEventInfo> LogEventHandler;


        public SignalRTarget()
        {
            Instance = this;
        }


        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent);


            var handler = LogEventHandler;
            if (handler != null)
            {
                handler(logMessage, logEvent);
            }
        }
    }


}