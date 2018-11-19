using Microsoft.AspNetCore.Authorization;

namespace EcomProject_JimmyRebecca.Models.Handlers
{
    public class LovesCatsRequirement : IAuthorizationRequirement
    {
        public bool ILoveCats { get; set; }

        public LovesCatsRequirement(bool iLoveCats)
        {
            ILoveCats = iLoveCats;
        }
    }
}
