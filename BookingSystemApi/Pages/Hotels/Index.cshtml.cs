using BookingSystemApi.Application.Dto;
using BookingSystemApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingSystemApi.Pages.Hotels;

public class Index(IHotelService hotelService) : PageModel
{
    public List<HotelDto> Hotels { get; set; } = new();
    
    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Hotels = await hotelService.GetAllAsync(cancellationToken);
    }
}