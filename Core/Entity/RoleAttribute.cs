using Core.Dto;
using Core.ValueObjects;

namespace Core.Entity;

public class RoleAttribute
{
    private AttributeType _attribute;

    private RoleAttribute(AttributeType attribute)
    {
        _attribute = attribute;
    }
    public static RoleAttribute Create (AttributeType type)
    {
        return new RoleAttribute(type);
    }
}