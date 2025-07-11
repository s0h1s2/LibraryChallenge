using Core.ValueObjects;

using Microsoft.AspNetCore.Authorization;

namespace Web.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionType permissionType) : base(permissionType.ToString())
    {
    }
}