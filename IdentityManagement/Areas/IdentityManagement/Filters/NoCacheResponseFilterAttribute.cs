using System;
using System.Web.Mvc;

namespace IdentityMgmt.Areas.IdentityManagement.Filters
{
    public class NoCacheResponseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.CacheControl = "no-cache";
            filterContext.HttpContext.Response.ExpiresAbsolute = DateTime.Now.AddYears(-1);
            filterContext.HttpContext.Response.Expires = -1;
            base.OnActionExecuting(filterContext);
        }
    }
}