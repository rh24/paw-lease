using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Handlers
{
    public class LovesCatsHandler : AuthorizationHandler<LovesCatsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LovesCatsRequirement requirement)
        {
            throw new System.NotImplementedException();
        }
    }
}
