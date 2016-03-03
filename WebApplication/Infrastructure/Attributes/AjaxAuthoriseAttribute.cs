////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	App\Attributes\AjaxAuthoriseAction.cs
//
// summary:	Implements the ajax authorise action class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Web.Mvc;

namespace Web.App.Attributes
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Attribute for ajax authorise. </summary>
    ///
    /// <remarks>   Jim, 10/10/2013. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public sealed class AjaxAuthoriseAttribute : AuthorizeAttribute
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Called when a process requests authorization. </summary>
        ///
        /// <remarks>   Jim, 10/10/2013. </remarks>
        ///
        /// <param name="filterContext">    The filter context, which encapsulates information for using
        ///                                 <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result == null) { return; }

            if (filterContext.Result.GetType() == typeof(HttpUnauthorizedResult) && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new ContentResult(); // AjaxAwareRedirectResult(filterContext.HttpContext.Request.Url);
                filterContext.HttpContext.Response.StatusCode = 403;
                //String url = System.Web.Security.FormsAuthentication.LoginUrl; 
                //filterContext.Result = new RedirectResult(url); 
            }
        }
    }
}