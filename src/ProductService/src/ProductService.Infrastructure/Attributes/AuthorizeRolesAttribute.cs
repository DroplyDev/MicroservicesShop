region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace ProductService.Infrastructure.Attributes;

public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}
