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


        public void OnGet()
        {
            Huse = _list.GetAll();
        }

        public void OnPost() { }

    }
}
