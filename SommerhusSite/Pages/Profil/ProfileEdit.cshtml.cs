using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Repository07;
using System.ComponentModel.DataAnnotations;

namespace SommerhusSite.Pages.Profil
{
    public class ProfileEditModel : PageModel
    {
        private IUserRepository _userRepository;

        public ProfileEditModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //properties
        [BindProperty]
        [Required(ErrorMessage = "Fornavn skal udfyldes")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal være mindst to tegn i et fornavn")]
        public string UpdatedFirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Efternavn skal udfyldes")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal være mindst to tegn i et efternavn")]
        public string UpdatedLastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Telefonnummer skal udfyldes")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal være mindst otte cifre i et telefonnummer")]
        public string UpdatedPhone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email skal udfyldes")]
        [StringLength(100, ErrorMessage = "Dette er ikke en gyldig email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Emailen skal indeholde '@' tegnet, noget før @ og mellem @ og .")]
        public string UpdatedEmail { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password skal udfyldes")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal være mindst 8 tegn i et password")]
        public string UpdatedPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vejnavn skal udfyldes")]
        public string UpdatedStreetName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Hus nummer skal udfyldes")]
        public string UpdatedHouseNumber { get; set; }

        [BindProperty]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Der skal være mindst 0 tegn i et etagefelt")]
        public string UpdatedFloor { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Postnummer skal udfyldes")]
        public int UpdatedPostalCode { get; set; }

        [BindProperty]
        public bool UpdatedIsAdmin { get; set; }
        [BindProperty]
        public bool UpdatedIsLandLord { get; set; }

        public string ErrorMessage { get; private set; }


        public IActionResult OnPostUpdate(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = UpdatedFirstName;
            user.LastName = UpdatedLastName;
            user.Phone = UpdatedPhone;
            user.Email = UpdatedEmail;
            user.Password = UpdatedPassword;
            user.StreetName = UpdatedStreetName;
            user.HouseNumber = UpdatedHouseNumber;
            user.Floor = UpdatedFloor;
            user.PostalCode = UpdatedPostalCode;
            user.IsLandlord = UpdatedIsLandLord;

            _userRepository.Update(id, user);

            return RedirectToPage("Profile");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Profil/Index");
        }
    }
}
