using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Repository07;
using SommerhusSite.Services;
using System.ComponentModel.DataAnnotations;

namespace SommerhusSite.Pages.UserSite
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;

        public ForgotPasswordModel(IUserRepository userRepository, IEmailRepository emailRepository)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public string Message { get; set; }


        [BindProperty]
        public string Token { get; set; }

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

        public IActionResult OnPostEmail()
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

            var token = _userRepository.GeneratePasswordResetToken(user);
            var resetLink = Url.Page("/ResetPassword", null, new { token }, Request.Scheme);

            _emailRepository.SendEmail(Email, "Password Reset", $"Your reset token is: {token}");

            Message = "En 4 cifret kode til at resette din adgangskode";
            Token = token; // For displaying the reset form
            return Page();
        }

        public IActionResult OnGet(string token)
        {
            Token = token;
            return Page();
        }

        public IActionResult OnPostUpdate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userRepository.GetUserByPasswordResetToken(Token);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid token.");
                return Page();
            }

            _userRepository.ResetPassword(user, NewPassword);
            Message = "Your password has been reset.";
            return Page();
        }
    }
}
