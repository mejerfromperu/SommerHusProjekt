using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System.ComponentModel.DataAnnotations;

namespace SommerhusHjemmeside.Pages.UserSite
{
    public class CreateModel : PageModel
    {

        private IUserRepository _repo;

        // dependency injection
        public CreateModel(IUserRepository userrepo)
        {
            _repo = userrepo;
        }

        // Properties til de nye brugere



        [BindProperty]
        [Required(ErrorMessage = "Fornavn skal udfyldes")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal være mindst to tegn i et fornavn")]
        public string NewUserFirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Efternavn skal udfyldes")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal være mindst to tegn i et efternavn")]
        public string NewUserLastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Telefonnummer skal udfyldes")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal være mindst otte cifre i et telefonnummer")]
        public string NewUserPhone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email skal udfyldes")]
        [StringLength(100, ErrorMessage = "Dette er ikke en gyldig email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Emailen skal indeholde '@' tegnet, noget før @ og mellem @ og .")]
        public string NewUserEmail { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password skal udfyldes")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal være mindst 8 tegn i et password")]
        public string NewUserPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vejnavn skal udfyldes")]
        public string NewUserStreetName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Hus nummer skal udfyldes")]
        public string NewUserHouseNumber { get; set; }

        [BindProperty]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Der skal være mindst 0 tegn i et etagefelt")]
        public string NewUserFloor { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Postnummer skal udfyldes")]
        public int NewUserPostalCode { get; set; }

        [BindProperty]
        public bool NewUserIsAdmin { get; set; }
        [BindProperty]
        public bool NewUserIsLandLord { get; set; }

        public string ErrorMessage { get; private set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            ErrorMessage = "fEJL 404 KUNNE IKKE OPRETTE EN USER";
            ModelState.Remove("NewUserFloor"); // Ignorer validering af NewUserFloor
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (NewUserFloor == null)
            {
                NewUserFloor = string.Empty;  
            }

            User newuser = new User(NewUserFirstName, NewUserLastName, NewUserPhone, NewUserEmail, NewUserPassword, NewUserStreetName, NewUserHouseNumber, NewUserFloor, NewUserPostalCode, NewUserIsLandLord, NewUserIsAdmin);
            try
            {
                _repo.Add(newuser);
                TempData["SuccessMessage"] = $"User {newuser} added successfully";

            }
            catch (ArgumentException ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }

            return RedirectToPage("LogIn");

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("LogIn");
        }

    }
}
