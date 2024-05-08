using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace SommerhusSite.Pages.AdminSite
{
    public class UpdateSummerHouseModel : PageModel
    {
        // instans af bil repository
        private ISummerHouseRepository _summerHouseRepo;

        //Dependency Injection
        public UpdateSummerHouseModel(ISummerHouseRepository repository)
        {
            _summerHouseRepo = repository;
        }

        //Property til nye værdier
        [BindProperty]
        public int NewSummerHouseId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Vejnavn skal udfyldes")]
        public string NewSummerHouseStreetName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Hus nummer skal udfyldes")]
        public string NewSummerHouseHouseNumber { get; set; }

        [BindProperty]
        public string NewSummerHouseFloor { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Postnummer skal udfyldes")]
        public int NewSummerHousePostalCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Beskrivelse skal udfyldes")]
        public string NewSummerHouseDescription { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Pris skal udfyldes")]
        public decimal NewSummerHousePrice { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Dato Fra skal udfyldes")]
        public DateTime NewSummerHouseFromDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Dato Til skal udfyldes")]
        public DateTime NewSummerHouseToDate { get; set; }

        public string ErrorMessage { get; private set; }
        public bool Error { get; private set; }

        //Gør vi kan få de spefikke oplysninger om bilen
        public void OnGet(int id)
        {
            ErrorMessage = "";
            Error = false;

            try
            {
                SummerHouse summerHouse = _summerHouseRepo.GetById(id);

                NewSummerHouseStreetName = summerHouse.StreetName;
                NewSummerHouseHouseNumber = summerHouse.HouseNumber;
                NewSummerHouseFloor = summerHouse.Floor;
                NewSummerHousePostalCode = summerHouse.PostalCode;
                NewSummerHouseDescription = summerHouse.Description;
                NewSummerHousePrice = summerHouse.Price;
                NewSummerHouseFromDate = summerHouse.DateFrom;
                NewSummerHouseToDate = summerHouse.DateTo;
            }
            catch (KeyNotFoundException knfe)
            {
                ErrorMessage = knfe.Message;
                Error = true;
            }
        }

        //Gør vi kan lave værdierne om til de nye ændrede værdier
        public IActionResult OnPostChange()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SummerHouse summerHouse = _summerHouseRepo.GetById(NewSummerHouseId);

            summerHouse.StreetName = NewSummerHouseStreetName;
            summerHouse.HouseNumber = NewSummerHouseHouseNumber;
            summerHouse.Floor = NewSummerHouseFloor;
            summerHouse.PostalCode = NewSummerHousePostalCode;
            summerHouse.Description = NewSummerHouseDescription;
            summerHouse.Price = NewSummerHousePrice;
            summerHouse.DateFrom = NewSummerHouseFromDate;
            summerHouse.DateTo = NewSummerHouseToDate;

            //_summerHouseRepo.WriteToJson();

            return RedirectToPage("SummerHouseList");
        }

        // Gør man kommer tilbage til sommerhuslisten, hvis man fortryder
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("SummerHouseList");
        }
    }
}
