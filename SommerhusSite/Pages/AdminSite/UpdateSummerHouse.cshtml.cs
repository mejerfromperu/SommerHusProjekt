using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using Microsoft.AspNetCore.Hosting;

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

        //Property til nye v�rdier
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
        [Required(ErrorMessage = "Picture skal udfyldes")]
        public string NewSummerHousePicture { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Dato Til skal udfyldes")]
        public DateTime NewSummerHouseToDate { get; set; }

        public string ErrorMessage { get; private set; }
        public bool Error { get; private set; }

        //G�r vi kan f� de spefikke oplysninger om sommerhuset
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
                NewSummerHousePicture = summerHouse.Picture;
                NewSummerHouseId = summerHouse.Id;
            }
            catch (KeyNotFoundException knfe)
            {
                ErrorMessage = knfe.Message;
                Error = true;
            }
        }

        //G�r vi kan lave v�rdierne om til de nye �ndrede v�rdier
        public IActionResult OnPostChange(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SummerHouse summerHouse = _summerHouseRepo.GetById(id);

            summerHouse.StreetName = NewSummerHouseStreetName;
            summerHouse.HouseNumber = NewSummerHouseHouseNumber;
            summerHouse.Floor = NewSummerHouseFloor;
            summerHouse.PostalCode = NewSummerHousePostalCode;
            summerHouse.Description = NewSummerHouseDescription;
            summerHouse.Price = NewSummerHousePrice;
            summerHouse.Picture = NewSummerHousePicture;
            summerHouse.DateFrom = NewSummerHouseFromDate;
            summerHouse.DateTo = NewSummerHouseToDate;

            _summerHouseRepo.Update(id, summerHouse);

            return RedirectToPage("SummerHouseList");
        }

        // G�r man kommer tilbage til sommerhuslisten, hvis man fortryder
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("SummerHouseList");
        }
    }
}