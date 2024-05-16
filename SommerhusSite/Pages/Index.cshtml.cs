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
        public int? SearchAmountGuest { get; set; }
        [BindProperty]
        public DateTime? SearchDateFrom { get; set; }
        [BindProperty]
        public DateTime? SearchDateTo { get; set; }


        public void OnGet()
        {
            Huse = _list.GetAll();
        }

        public void OnPost() { }

    }
}
