using System;
using System.Web;
using System.Web.Mvc;

namespace Shop.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SiteAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                //filterContext.Result = new HttpStatusCodeResult(403);
                throw new HttpException((int)System.Net.HttpStatusCode.Forbidden, "Forbidden");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}