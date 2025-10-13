using System.ComponentModel.DataAnnotations;
using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingSystemApi.Pages.Auth;

public class Register(
    UserManager<UserEntity> userManager,
    IAuthService authService) : PageModel
    {
        [BindProperty] public required InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public required string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public required string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public required string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public required string ConfirmPassword { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new UserEntity()
            {
                UserName = Input.UserName,
                Email = Input.Email
            };
            
            await authService.Register(user.UserName, Input.Email, Input.Password);
            return RedirectToPage("/Hotels/Index");
        }
    
}