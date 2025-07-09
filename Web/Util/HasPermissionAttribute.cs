using Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Web.Util;

public class HasPermissionAttribute:AuthorizeAttribute
{
    public HasPermissionAttribute(AttributeType attributeType):base(policy:attributeType.ToString())
    {
        
    }
    
    
}