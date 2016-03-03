using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Infrastructure.Extensions.Html;

namespace WebApplication.Infrastructure.Extensions.Controllers
{
    public static class Message
    {
        public static void ShowMessage(this Controller controller, string message, MessageType messageType = MessageType.Notice, bool showAfterRedirect = true)
        {
            var messageTypeKey = messageType.ToString();
            if (showAfterRedirect)
            {
                controller.TempData[messageTypeKey] = message;
            }
            else
            {
                controller.ViewData[messageTypeKey] = message;
            }
        }
    }
}