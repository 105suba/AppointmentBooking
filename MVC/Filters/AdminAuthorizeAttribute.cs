using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC.Filters
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("Role");

           
            if (string.IsNullOrEmpty(role))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

           
            if (role != "Admin")
            {
                context.Result = new RedirectToActionResult("Index", "Appointment", null);
            }
        }
    }
}