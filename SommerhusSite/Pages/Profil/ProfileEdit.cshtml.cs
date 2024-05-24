using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using SommerhusSite.Services;
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
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal være mindst to tegn i et fornavn")]
        public string? UpdatedFirstName { get; set; }

        [BindProperty]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der skal være mindst to tegn i et efternavn")]
        public string? UpdatedLastName { get; set; }

        [BindProperty]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal være mindst otte cifre i et telefonnummer")]
        public string? UpdatedPhone { get; set; }

        [BindProperty]
        [StringLength(100, ErrorMessage = "Dette er ikke en gyldig email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Emailen skal indeholde '@' tegnet, noget før @ og mellem @ og .")]
        public string? UpdatedEmail { get; set; }

        [BindProperty]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Der skal være mindst 8 tegn i et password")]
        public string? UpdatedPassword { get; set; }

        [BindProperty]
        public string? UpdatedStreetName { get; set; }

        [BindProperty]
        public string? UpdatedHouseNumber { get; set; }

        [BindProperty]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Der skal være mindst 0 tegn i et etagefelt")]
        public string? UpdatedFloor { get; set; }

        [BindProperty]
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

            if (!string.IsNullOrWhiteSpace(UpdatedFirstName))
            {
                user.FirstName = UpdatedFirstName;
            }

            if (!string.IsNullOrWhiteSpace(UpdatedLastName))
            {
                user.LastName = UpdatedLastName;
            }

            if (!string.IsNullOrWhiteSpace(UpdatedPhone))
            {
                user.Phone = UpdatedPhone;
            }

            if (!string.IsNullOrWhiteSpace(UpdatedEmail))
            {
                user.Email = UpdatedEmail;
            }

            if (!string.IsNullOrWhiteSpace(UpdatedPassword))
            {
                user.Password = UpdatedPassword;
            }

            if (!string.IsNullOrWhiteSpace(UpdatedStreetName))
            {
                user.StreetName = UpdatedStreetName;
            }

            if (!string.IsNullOrWhiteSpace(UpdatedHouseNumber))
            {
                user.HouseNumber = UpdatedHouseNumber;
            }

            if (!string.IsNullOrWhiteSpace(UpdatedFloor))
            {
                user.Floor = UpdatedFloor;
            }

            if (UpdatedPostalCode != 0)
            {
                user.PostalCode = UpdatedPostalCode;
            }

            user.IsLandlord = UpdatedIsLandLord;

            _userRepository.Update(id, user);

            HttpContext.Session.Clear();
            SessionHelper.Set(user, HttpContext);

            return RedirectToPage("/Profil/Index");
        }


        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Profil/Index");
        }
    }
}
