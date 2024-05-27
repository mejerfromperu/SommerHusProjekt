using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using SommerHusProjekt.Repository07;
using System.Data.SqlClient;
using SommerHusProjekt.Model07;
using SommerhusSite.Services;
using System.ComponentModel;

namespace SommerhusHjemmeside.Pages.UserSite
{
    public class LogInModel : PageModel
    {
        private IUserRepository _repo;
        private User user;
        public LogInModel(IUserRepository userRepo)
        {
            _repo = userRepo;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email skal udfyldes")]
        [EmailAddress(ErrorMessage = "Forkert email")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adgangskode skal udfyldes")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        //[BindProperty]
        //public string ErrorMsg { get; set; }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            User user = _repo.GetByEmailAndPassword(Email, Password);

            if (user != null)
            {
                // Set user in to session
                SessionHelper.Set(user, HttpContext);

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "forkert email eller adgangskode");
            return Page();
        }

    }


}





