using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolProject.Service.AuthServices.Interfaces;

namespace SchoolProject.Core.Filters
{
    public class AuthFilter : IAsyncActionFilter
    {
        private readonly ICurrentUserService _currentUserService;
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == true)
            {
                var user = await _currentUserService.GetUserAsync();
                var roles = await _currentUserService.GetUserRolesAsync();
                if (roles.All(x => x != "User"))
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                    };
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
