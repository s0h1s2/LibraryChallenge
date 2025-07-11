using Core.Entity;
using Core.ValueObjects;

namespace Core.UnitTests;

public class UserTest
{
    [Fact]
    public void TestAssignRole_To_User_User_Must_Have_Role()
    {
        // Arrange
        var user = User.Create("user@mail.com", "password123", 1);
        var role = Role.Create(RoleType.Admin);
        user.AssignRole(role);
        // Assert
        Assert.Equal(role, user.Role);
    }

    [Fact]
    public void TestAssignRole_To_User_User_Must_Throw_Exception_When_Role_Is_Null()
    {
        // Arrange
        var user = User.Create("user@mail.com", "password123", 1);
        Role role = null;
        // Act & Assert
        Assert.Throws<DomainException>(() => user.AssignRole(role));
    }
}