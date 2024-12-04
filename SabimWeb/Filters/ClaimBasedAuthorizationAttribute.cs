using Microsoft.AspNetCore.Authorization;

namespace Sabim.Web.Filters
{
    // The custom attribute that holds the claim type and value requirement
    public class ClaimBasedAuthorizationAttribute : Attribute, IAuthorizationRequirement
    {
        // Constructor that initializes the ClaimType and ClaimValue properties
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public ClaimBasedAuthorizationAttribute(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }
}
