using System.Reflection.Metadata;
using Core.Entity;
using Core.ValueObjects;

namespace Core.UnitTests;

public class RoleTest
{
    [Fact]
    public void TestAssignAttributeToRole_Attribute_MustBe_Assigned()
    {
        var role = Role.Create(RoleType.Liberian);
        var attribute = RoleAttribute.Create(PermissionType.CanViewBooks);
        role.AssignAttribute(attribute);
        Assert.True(role.HasPermission(attribute));
    }
    [Fact]
    public void TestRevokeAttribute_From_Role_Attribute_MustBe_Revoked()
    {
        var role = Role.Create(RoleType.Liberian);
        var attribute = RoleAttribute.Create(PermissionType.CanViewBooks);
        role.AssignAttribute(attribute);
        Assert.True(role.HasPermission(attribute));
        role.RevokeAttribute(attribute);
        Assert.False(role.HasPermission(attribute));
    }
    
}