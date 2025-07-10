using Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Web.Util;

public class HasPermissionAttribute:AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionType permissionType):base(policy:permissionType.ToString())
    {
        
    }
}