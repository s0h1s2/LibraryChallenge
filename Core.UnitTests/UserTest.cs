using Core.Entity;
using Core.ValueObjects;

namespace Core.UnitTests;

public class UserTest : IDisposable
{
    private User _user;

    public UserTest()
    {
        _user = User.Create("user@mail.com", "password123", 1);
    }

    public void Dispose()
    {
    }

    [Fact]
    public void TestAssignRole_To_User_User_Must_Have_Role()
    {
        var role = Role.Create(RoleType.Admin);
        _user.AssignRole(role);
        Assert.Equal(role, _user.Role);
    }

    [Fact]
    public void TestAssignRole_To_User_User_Must_Throw_Exception_When_Role_Is_Null()
    {
        Role role = null;
        Assert.Throws<DomainException>(() => _user.AssignRole(role));
    }
}