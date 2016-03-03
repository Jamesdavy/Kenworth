using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Infrastructure.ActionResults
{
    public class JsonResponseMessage
    {
        public JsonResponseMessage(HttpStatusCode statusCode, string message, object content) :
            this(statusCode, message, content, null)
        {
        }

        public JsonResponseMessage(HttpStatusCode statusCode, string message, object content, List<string> errors)
        {
            StatusCode = statusCode;
            Message = message;
            Content = content;
            Errors = errors;
            Serialize = true;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }
        public List<string> Errors { get; set; }
        public bool Serialize { get; set; }
    }

    public class JsonActionResult : ActionResult
    {
        JsonResponseMessage _response;

        public JsonActionResult(JsonResponseMessage response)
        {
            _response = response;
        }

        public JsonActionResult(HttpStatusCode statusCode, string message, object content)
        {
            _response = new JsonResponseMessage(statusCode, message, content);
        }

        public JsonActionResult(HttpStatusCode statusCode, string message, object content, List<string> errors)
        {
            _response = new JsonResponseMessage(statusCode, message, content, errors);
        }


        public object Response
        {
            get { return _response.Content; }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContextBase contextBase = context.HttpContext;
            contextBase.Response.StatusCode = (int)_response.StatusCode;
            if (_response.Serialize)
                contextBase.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(_response.Content));
            else
                contextBase.Response.Write(_response.Content);

            contextBase.Response.End();
        }
    }
}