using BookingSystemApi.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController(IBookingRepository bookingRepository) : ControllerBase
{

}