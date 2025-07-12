using Core.Dto;
using Core.Entity;
using Core.ValueObjects;

namespace Core.UnitTests.Entity;

public class UserTest : IDisposable
{
    private readonly User _user;

    public UserTest()
    {
        _user = User.Create("user@mail.com", "password123", Role.Create(RoleType.Admin));
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
        Assert.Throws<ArgumentNullException>(() => _user.AssignRole(null));
    }

    [Fact]
    public void TestUpdateUserInfo_User_Must_Be_Update()
    {
        var updateUser = new UpdateUser("shkar@mail.com", "password", Role.Create(RoleType.Admin));
        _user.UpdateInfo(updateUser);
        Assert.Equal(_user.Email, updateUser.Email);
        Assert.Equal(_user.PasswordHash, updateUser.Password);
        Assert.Equal(_user.Role, updateUser.Role);
    }

    [Fact]
    public void TestUpdateUserInfo_User_Only_Non_Null_Values_Should_Be_Updated()
    {
        var updateUser = new UpdateUser("shkar@mail.com", null, null);
        _user.UpdateInfo(updateUser);
        Assert.Equal(_user.Email, updateUser.Email);
        Assert.NotNull(_user.Role);
        Assert.NotEmpty(_user.PasswordHash);
    }
}