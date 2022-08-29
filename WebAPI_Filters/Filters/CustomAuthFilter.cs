using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Filters.Filters
{
    public class CustomAuthFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //base.OnActionExecuting(context);
            if (context.HttpContext.Request.Headers.Keys.Contains("Authorization"))
            {
                if (!context.HttpContext.Request.Headers.Keys.Contains("Authorization").Equals("Bearer"))
                {
                    context.Result = new BadRequestObjectResult("Invalid request - Token present but Bearer unavailable");
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid request - No Auth token");
            }
        }
    }
}
