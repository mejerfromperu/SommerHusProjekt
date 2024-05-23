using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using SommerhusSite.Services;
using System.ComponentModel.DataAnnotations;

namespace SommerhusSite.Pages.UserSite
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public ForgotPasswordModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adgangskode skal udfyldes")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal være mindst 8 tegn i et password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adgangskode skal udfyldes")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal være mindst 8 tegn i et password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Adgangskode passer ikke")]
        public string ConfirmPassword { get; set; }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userRepository.GetByEmail(Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email not found.");
                return Page();
            }

            _userRepository.UpdatePassword(Email, NewPassword);

            TempData["SuccessMessage"] = "Password has been reset successfully.";
            return RedirectToPage("/UserSite/Login");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/UserSite/Login");
        }
    }
}
