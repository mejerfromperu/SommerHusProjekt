using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;

namespace SommerhusSite.Pages.AdminSite
{
    public class SummerHouseListModel : PageModel
    {
        private ISummerHouseRepository _summerHouseRepo;

        //Dependency Injection
        public SummerHouseListModel(ISummerHouseRepository repository)
        {
            _summerHouseRepo = repository;
        }

        // property til View'et
        public List<SummerHouse> SummerHouses { get; set; }

        // BindProperty til search funktion

        [BindProperty]
        public int? SearchId { get; set; }
        [BindProperty]
        public string? SearchStreetName { get; set; }
        [BindProperty]
        public string? SearchHouseNumber { get; set; }
        [BindProperty]
        public int? SearchPostalCode { get; set; }
        [BindProperty]
        public decimal? SearchPrice { get; set; }
        [BindProperty]
        public int? SearchAmountSleepingSpace { get; set; }
        [BindProperty]
        public DateTime? SearchDateFrom { get; set; }
        [BindProperty]
        public DateTime? SearchDateTo { get; set; }


        //Hent alle kunder når siden læses
        public void OnGet()
        {
            SummerHouses = _summerHouseRepo.GetAll();
        }

        //Gør at man søger når man trykker på knappen
        public IActionResult OnPostSearch()
        {
            SummerHouses = _summerHouseRepo.Search(SearchId, SearchStreetName, SearchHouseNumber, SearchPostalCode, SearchPrice, SearchAmountSleepingSpace, SearchDateFrom, SearchDateTo);
            return Page();
        }

        //Kalder Sort efter ID
        public IActionResult OnPostSortId()
        {
            SummerHouses = _summerHouseRepo.SortId();
            return Page();
        }

        public IActionResult OnPostSortStreetName()
        {
            SummerHouses = _summerHouseRepo.SortStreetName();
            return Page();
        }

        public IActionResult OnPostSortPostalCode()
        {
            SummerHouses = _summerHouseRepo.SortPostalCode();
            return Page();
        }
    }
}
