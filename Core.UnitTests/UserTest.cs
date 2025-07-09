using Core.Entity;
using Core.Services;
using Core.UnitTests.Persistance;
using Core.ValueObjects;

namespace Core.UnitTests;

public class UserTest
{
    // [Fact]
    // public void TestCreateUser_User_MustBe_Created()
    // {
    //     var user = User.Create("john","password");
    //     var userRepo = new FakeUserRepository();
    //     var userDomainService= new UserDomainService(userRepo);
    //     userDomainService.CreateUserAsync(user);
    //     Assert.Equal(userRepo.Users.First(),user);
    // }

    [Fact]
    public void TestAssignRoleToUser_Role_MustBe_Assigned()
    {
        var user = User.Create("john", "password");
        var role = Role.Create(RoleType.Admin);
        user.AssignRole(role);
        Assert.Equal(user.Role, role);
    }
    
}