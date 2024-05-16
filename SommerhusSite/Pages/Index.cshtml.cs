using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;

namespace SommerhusSite.Pages
{
    public class IndexModel : PageModel
    {
        private ISummerHouseRepository _list;

        public IndexModel(ISummerHouseRepository list)
        {
            _list = list;
        }

        public List<SummerHouse> Huse { get; set; }

        [BindProperty]
        public int? SearchId { get; set; }
        [BindProperty]
        public string? SearchStreetName { get; set; }
        [BindProperty]
        public string? SearchHouseNumber { get; set; }
        [BindProperty]
        public string? SearchFloor { get; set; }
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


        public void OnGet()
        {
            Huse = _list.GetAll();
        }

        public IActionResult OnPostSearch()
        {
            Huse = _list.Search(SearchId,SearchStreetName, SearchHouseNumber, SearchPostalCode, SearchPrice, SearchAmountSleepingSpace, SearchDateFrom, SearchDateTo);
            return Page();
        }

        public IActionResult OnPostSortPrice()
        {
            Huse = _list.SortPrice();
            return Page();
        }

        public IActionResult OnPostSortAmountSleepingSpace()
        {
            Huse = _list.SortAmountSleepingSpace();
            return Page();
        }

    }
}
