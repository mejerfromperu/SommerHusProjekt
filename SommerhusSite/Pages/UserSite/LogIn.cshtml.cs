using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace SommerhusHjemmeside.Pages.UserSite
{
    public class LogInModel : PageModel
    {
    //    private IUserRepostiroy _repo;

    //    public LogInModel(IUserRepostiroy userRepo)
    //    {
    //        _repo = userRepo;
    //    }

    //    [BindProperty]
    //    [Required(ErrorMessage = "Email is required")]
    //    [EmailAddress(ErrorMessage = "Invalid email address")]
    //    public string Email { get; set; }

    //    [BindProperty]
    //    [Required(ErrorMessage = "Password is required")]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    public IActionResult OnPost()
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return Page();
    //        }

    //        // Fetch user by email and password
    //        var user = _repo.GetByEmailAndPassword(Email, Password);

    //        if (user != null)
    //        {
                

    //            User user2 = new User(user.Id, user.FirstName, user.LastName, user.Email, user.Password, user.Phone, user.StreetName, user.HouseNumber, user.Floor, user.PostalCode, user.IsLandlord, user.IsAdmin);
    //            SessionHelper.Set(user, HttpContext);


    //            return RedirectToPage("/Index");
    //        }

    //        // Authentication failed
    //        ModelState.AddModelError(string.Empty, "Invalid email or password");
    //        return Page();
    //    }

    }


}





