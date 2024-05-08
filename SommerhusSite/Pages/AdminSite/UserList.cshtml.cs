using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;

namespace SommerhusSite.Pages.AdminSite
{
    public class UserListModel : PageModel
    {
        private IUserRepository _userRepo;

        //Dependency Injection
        public UserListModel(IUserRepository repository)
        {
            _userRepo = repository;
        }

        // property til View'et
        public List<User> Users { get; set; }

        // BindProperty til search funktion

        [BindProperty]
        public int? SearchId { get; set; }
        [BindProperty]
        public string? SearchFirstName { get; set; }
        [BindProperty]
        public string? SearchLastName { get; set; }
        [BindProperty]
        public string? SearchPhone { get; set; }
        [BindProperty]
        public string? SearchEmail { get; set; }


        //Hent alle kunder når siden læses
        public void OnGet()
        {
            Users = _userRepo.GetSomething();
        }

        //Gør at man søger når man trykker på knappen
        public IActionResult OnPostSearch()
        {
            Users = _userRepo.Search(SearchId, SearchFirstName, SearchLastName, SearchPhone, SearchEmail);
            return Page();
        }

        //Kalder Sort efter ID
        public IActionResult OnPostSortId()
        {
            Users = _userRepo.SortId();
            return Page();
        }

        //Kalder sort efter navn
        public IActionResult OnPostSortFirstName()
        {
            Users = _userRepo.SortFirstName();
            return Page();
        }

        public IActionResult OnPostSortLastName()
        {
            Users = _userRepo.SortLastName();
            return Page();
        }
    }
}
