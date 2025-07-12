using Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;

namespace Web.Util.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionType permissionType) : base(permissionType.ToString())
    {
    }
}