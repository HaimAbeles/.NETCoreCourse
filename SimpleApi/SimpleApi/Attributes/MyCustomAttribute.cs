using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MyCustomAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext _context, ActionExecutionDelegate _next)
        {
            bool valid = true;
            if(valid)
            {
                await _next();
            }
            else
            {
                _context.Result = new BadRequestObjectResult("error!");
            }
        }
    }
}
