using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;

namespace SommerhusSite.Pages.AdminSite
{
    public class DeleteUserModel : PageModel
    {
        // instans af kunde repository
        private IUserRepository _userRepo;

        //Dependency Injection
        public DeleteUserModel(IUserRepository repository)
        {
            _userRepo = repository;
        }

        // property til View'et
        public User User { get; private set; }

        // Få den rigtige kunde ud fra kundenummer
        public IActionResult OnGet(int id)
        {
            User = _userRepo.GetById(id);
            return Page();
        }

        //Sletter Kunden ud fra kundenummer
        public IActionResult OnPostDelete(int id)
        {
            _userRepo.Delete(id);
            return RedirectToPage("UserList");
        }

        //Får en tilbage til index, hvis man fortryder
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("UserList");
        }
    }
}
