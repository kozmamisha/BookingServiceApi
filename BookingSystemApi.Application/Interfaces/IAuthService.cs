namespace BookingSystemApi.Application.Interfaces;

public interface IAuthService
{
    Task Register(string userName, string email, string password, CancellationToken cancellationToken);
    Task<string> Login(string email, string password, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, string userName, string email, string password, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}