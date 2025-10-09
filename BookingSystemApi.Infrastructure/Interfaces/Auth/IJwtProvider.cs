using BookingSystemApi.Core.Entities;

namespace BookingSystemApi.Infrastructure.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(UserEntity user);
}