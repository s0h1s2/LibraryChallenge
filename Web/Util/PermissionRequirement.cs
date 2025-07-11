using Microsoft.AspNetCore.Authorization;

namespace Web.Util;

public class PermissionRequirement : IAuthorizationRequirement
{

    public PermissionRequirement(string permission)
    {
        Permission = permission ?? throw new ArgumentNullException(nameof(permission));
    }

    public string Permission { get; }
}