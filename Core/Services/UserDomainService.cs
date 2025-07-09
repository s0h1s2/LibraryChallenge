using Core.Entity;
using Core.Persistance;

namespace Core.Services;

public class UserDomainService
{
    private readonly IUserRepository _userRepository;

    public UserDomainService(IUserRepository _userRepository)
    {
        this._userRepository = _userRepository;
    }

    public void CreateUserAsync(User user)
    {
    }
}