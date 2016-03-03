using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Infrastructure.Extensions.General;

namespace WebApplication.Infrastructure.Extensions.Html
{
    public enum MessageType
    {
        [Description("alert-success")]
        Success,
        [Description("alert-danger")]
        Error,
        [Description("alert-warning")]
        Notice,
        [Description("alert-info")]
        Info
    }

    public static class Message
    {
        public static IHtmlString RenderMessages(this HtmlHelper htmlHelper)
        {
            var messages = String.Empty;
            foreach (var messageType in Enum.GetNames(typeof (MessageType)))
            {
                var message = htmlHelper.ViewContext.ViewData.ContainsKey(messageType)
                    ? htmlHelper.ViewContext.ViewData[messageType]
                    : htmlHelper.ViewContext.TempData.ContainsKey(messageType)
                        ? htmlHelper.ViewContext.TempData[messageType]
                        : null;
                if (message != null)
                {
                    messages = "<div class=\"alert " +
                               EnumHelpers.StringToEnum<MessageType>(messageType).ToDescriptionString() +
                               " alert-dismissable\">";
                    messages +=
                        "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>";
                    messages += message;
                    messages += "</div>";
                }
            }

            return MvcHtmlString.Create(messages);
        }
    }

}