using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System.ComponentModel.DataAnnotations;

namespace SommerhusSite.Pages.AdminSite
{
    public class CreateAdminModel : PageModel
    {
        private IUserRepository _repo;

        // dependency injection
        public CreateAdminModel(IUserRepository userrepo)
        {
            _repo = userrepo;
        }

        // Properties til de nye brugere



        [BindProperty]
        [Required(ErrorMessage = "Fornavn skal udfyldes")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal v�re mindst to tegn i et fornavn")]
        public string NewUserFirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Efternavn skal udfyldes")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal v�re mindst to tegn i et efternavn")]
        public string NewUserLastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Telefonnummer skal udfyldes")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal v�re mindst otte cifre i et telefonnummer")]
        public string NewUserPhone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email skal udfyldes")]
        [StringLength(100, ErrorMessage = "Dette er ikke en gyldig email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Emailen skal indeholde '@' tegnet, noget f�r @ og mellem @ og .")]
        public string NewUserEmail { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adgangskode skal udfyldes")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal v�re mindst 8 tegn i et password")]
        public string NewUserPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vejnavn skal udfyldes")]
        public string NewUserStreetName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Hus nummer skal udfyldes")]
        public string NewUserHouseNumber { get; set; }

        [BindProperty]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Der skal v�re mindst 0 tegn i et etagefelt")]
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

            ErrorMessage = "Fejl kunne ikke oprette bruger";
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


            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }

            return RedirectToPage("Index");

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
