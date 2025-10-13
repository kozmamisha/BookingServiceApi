using System.ComponentModel.DataAnnotations;
using BookingSystemApi.Application.Dto;
using BookingSystemApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingSystemApi.Pages.Bookings;

public class Index(IBookingService bookingService, IHotelService hotelService, IRoomService roomService) : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "You must select a hotel")]
    public Guid SelectedHotelId { get; set; }
    
    [BindProperty]
    [Required(ErrorMessage = "You must select a room")]
    public Guid SelectedRoomId { get; set; }

    [BindProperty, DataType(DataType.DateTime)]
    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartTime { get; set; } = DateTime.Now;

    [BindProperty, DataType(DataType.DateTime)]
    [Required(ErrorMessage = "End date is required")]
    public DateTime EndTime { get; set; } = DateTime.Now.AddDays(1);

    public List<HotelDto> Hotels { get; set; } = new();
    public List<RoomDto> Rooms { get; set; } = new();

    public async Task OnGet()
    {
        Hotels = await hotelService.GetAllAsync(HttpContext.RequestAborted);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Hotels = await hotelService.GetAllAsync(HttpContext.RequestAborted);
        
        var allRooms = await roomService.GetAllAsync(HttpContext.RequestAborted);
        Rooms = allRooms.Where(r => r.HotelId == SelectedHotelId).ToList();

        if (!Rooms.Any())
        {
            ModelState.AddModelError(string.Empty, "No rooms available for the selected hotel.");
            return Page();
        }

        if (!ModelState.IsValid)
            return Page();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Challenge();

        try
        {
            await bookingService.CreateAsync(StartTime, EndTime, SelectedRoomId, userId, HttpContext.RequestAborted);
            TempData["SuccessMessage"] = "Room booked successfully!";
            return RedirectToPage("/Bookings/Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}