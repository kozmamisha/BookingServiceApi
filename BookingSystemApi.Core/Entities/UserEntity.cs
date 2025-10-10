using BookingSystemApi.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace BookingSystemApi.Core.Entities;

public class UserEntity : IdentityUser
{
    public ICollection<BookingEntity> Bookings { get; set; } = new List<BookingEntity>();
    public string Role { get; set; } = Roles.User;
}