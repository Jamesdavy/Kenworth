using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Results;

namespace WebApplication.Infrastructure.Attributes
{
    public static class ModelStateExtensions
    {
        public static JsonResult ToJsonResult(this ModelStateDictionary modelState)
        {
            return new JsonResult() { Data = new { KoValid = modelState.IsValid, ModelState = modelState.ToJSON() } };
        }

        public static object ToJSON(this ModelStateDictionary modelState)
        {
            return modelState.Where(x => x.Value.Errors.Count > 0).Select(ms => new
            {
                Key = ms.Key,
                //Value = string.Join(",", ms.Value.Errors.Select(x => x.ErrorMessage)),
                Value = ms.Value.Errors.First().ErrorMessage
            }).ToArray();
        }
    }

    public static class ValidationResultExtensions
    {
        public static object ToJsonResult(this ValidationResult validationResult)
        {
            //var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            var modelState = new { KoValid = validationResult.IsValid, ModelState = validationResult.ToJSON() };
            //var errors = JsonConvert.SerializeObject(modelState, Formatting.None, settings);
            return modelState;
        }

        public static object ToJSON(this ValidationResult modelState)
        {
            return modelState.Errors.Select(ms => new
            {
                Key = ms.PropertyName,
                Value = ms.ErrorMessage,
                CustomState = ms.CustomState
            }).ToArray();
        }
    }


    public class JsonValidateAttribute : ActionFilterAttribute
    {
        protected bool IgnoreValidation;

        public JsonValidateAttribute(bool ignoreValidation = false)
        {
            this.IgnoreValidation = ignoreValidation;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IgnoreValidation || !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                return;
            }
            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusDescription = "Validation error";
                filterContext.Result = modelState.ToJsonResult();
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.HttpContext.ClearError();
                filterContext.HttpContext.Response.End();
            }
        }
    }
}