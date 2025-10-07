using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Persistence.Interfaces;

namespace BookingSystemApi.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task Register(string userName, string email, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, string userName, string email, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}