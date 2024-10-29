using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Sanha.Web.Filters
{
    public class CustomAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var id = context.HttpContext.Session.GetString("SAN.ID");
            if (id == null)
            {
                context.Result = new RedirectToRouteResult
                (
                new RouteValueDictionary(new
                {
                    action = "index",
                    controller = "login"
                }));
            }

            string currentUserID = Convert.ToString(id);
            if (!string.IsNullOrEmpty(currentUserID))
            {
                if (currentUserID == "")
                {
                    context.Result = new RedirectToRouteResult
                    (
                    new RouteValueDictionary(new
                    {
                        action = "index",
                        controller = "login"
                    }));
                }

            }
            else
            {
                context.Result = new RedirectToRouteResult
                (
                new RouteValueDictionary(new
                {
                    action = "index",
                    controller = "login"
                }));

            }
        }
    }
}

