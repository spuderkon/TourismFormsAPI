using System.Security.Principal;
using System.Net.Http;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TourismFormsAPI.Tools
{
    public class HasAdminClaim : AuthorizationHandler<HasAdminClaim>, IAuthorizationRequirement
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAdminClaim requirement)
        {
            if (context.User.HasClaim(c => c.Type == "isAdmin"))
            {
                if(context.User.FindFirst(c => c.Type == "isAdmin").Value == "True")
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                context.Fail();
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }

}
