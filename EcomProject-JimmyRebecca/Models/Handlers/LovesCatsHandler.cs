using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Handlers
{
    public class LovesCatsHandler : AuthorizationHandler<LovesCatsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LovesCatsRequirement requirement)
        {
            var lovesCats = context.User.Claims.First(c => c.Type == "LovesCats").Value;

            if (!context.User.HasClaim(c => c.Type == "LovesCats"))
            {
                return Task.CompletedTask;
            }
            else if (lovesCats == "true")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
