using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;

namespace SommerhusSite.Pages.AdminSite
{
    public class DeleteSummerHouseModel : PageModel
    {
        private ISummerHouseRepository _summerHouseRepo;

        //Dependency Injection
        public DeleteSummerHouseModel(ISummerHouseRepository repository)
        {
            _summerHouseRepo = repository;
        }

        // property til View'et
        public SummerHouse SummerHouse { get; private set; }

        // Få den rigtige kunde ud fra kundenummer
        public IActionResult OnGet(int id)
        {
            SummerHouse = _summerHouseRepo.GetById(id);
            return Page();
        }

        //Sletter Kunden ud fra kundenummer
        public IActionResult OnPostDelete(int id)
        {
            _summerHouseRepo.Delete(id);
            return RedirectToPage("SummerHouseList");
        }

        //Får en tilbage til index, hvis man fortryder
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("SummerHouseList");
        }
    }
}
