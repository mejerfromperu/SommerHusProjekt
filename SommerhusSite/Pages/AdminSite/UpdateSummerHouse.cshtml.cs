using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using Microsoft.AspNetCore.Hosting;
using SommerhusSite.Services;

namespace SommerhusSite.Pages.AdminSite
{
    public class UpdateSummerHouseModel : PageModel
    {
        // instans af sommerhus repository
        private ISummerHouseRepository _summerHouseRepo;
        private DateTime _newSummerHouseFromDate;
        private DateTime _newSummerHouseToDate;

        //Dependency Injection
        public UpdateSummerHouseModel(ISummerHouseRepository repository)
        {
            _summerHouseRepo = repository;
        }

        public SummerHouse SelectedSummerhouse { get; set; }

        //Properties til nye værdier
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
        [Required(ErrorMessage = "Antal Sovepladser skal udfyldes")]
        public int NewSummerHouseAmountSleepingSpace { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Beskrivelse skal udfyldes")]
        public string NewSummerHouseDescription { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Pris skal udfyldes")]
        public decimal NewSummerHousePrice { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Dato Fra skal udfyldes")]
        public DateTime NewSummerHouseFromDate
        {
            get { return _newSummerHouseFromDate; }
            set
            {
                if (value < DateTime.Today)
                {
                    throw new ArgumentException("Dato Fra kan ikke være før dagens dato");
                }
                _newSummerHouseFromDate = value;
            }
        }


        [BindProperty]
        public IFormFile NewSummerHousePicture { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Dato Til skal udfyldes")]
        public DateTime NewSummerHouseToDate
        {
            get { return _newSummerHouseToDate; }
            set
            {
                if (value <= DateTime.Today)
                {
                    throw new ArgumentException("Dato Til kan ikke være før dagens dato");
                }
                _newSummerHouseToDate = value;
            }
        }


        public string ErrorMessage { get; private set; }
        public bool Error { get; private set; }

        //Gør vi kan få de spefikke oplysninger om sommerhuset
        public void OnGet(int id)
        {
            ErrorMessage = "";
            Error = false;

            try
            {
                SummerHouse summerHouse = _summerHouseRepo.GetById(id);

                if (summerHouse != null)
                {
                    NewSummerHouseStreetName = summerHouse.StreetName;
                    NewSummerHouseHouseNumber = summerHouse.HouseNumber;
                    NewSummerHouseFloor = summerHouse.Floor;
                    NewSummerHousePostalCode = summerHouse.PostalCode;
                    NewSummerHouseDescription = summerHouse.Description;
                    NewSummerHousePrice = summerHouse.Price;
                    NewSummerHouseAmountSleepingSpace = summerHouse.AmountSleepingSpace;
                    NewSummerHouseFromDate = summerHouse.DateFrom;
                    NewSummerHouseToDate = summerHouse.DateTo;
                    NewSummerHouseId = summerHouse.Id;
                }
            }
            catch (KeyNotFoundException knfe)
            {
                ErrorMessage = knfe.Message;
                Error = true;
            }

            SelectedSummerhouse = _summerHouseRepo.GetById(id);

            SessionHelper.Set(SelectedSummerhouse, HttpContext);
        }

        //Gør vi kan lave værdierne om til de nye ændrede værdier
        public IActionResult OnPostChange(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //SummerHouse summerHouse = _summerHouseRepo.GetById(id);

            SummerHouse summerHouse = SessionHelper.Get<SummerHouse>(HttpContext);

            if (summerHouse == null)
            {
                ErrorMessage = "Summer house not found";
                Error = true;
                return Page();
            }

            summerHouse.StreetName = NewSummerHouseStreetName;
            summerHouse.HouseNumber = NewSummerHouseHouseNumber;
            summerHouse.Floor = NewSummerHouseFloor;
            summerHouse.PostalCode = NewSummerHousePostalCode;
            summerHouse.Description = NewSummerHouseDescription;
            summerHouse.AmountSleepingSpace = NewSummerHouseAmountSleepingSpace;
            summerHouse.Price = NewSummerHousePrice;
            summerHouse.DateFrom = NewSummerHouseFromDate;
            summerHouse.DateTo = NewSummerHouseToDate;

            if (NewSummerHousePicture != null)
            {
                var fileName = Path.GetFileName(NewSummerHousePicture.FileName);
                var filePath = Path.Combine("wwwroot/images", NewSummerHousePicture.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    NewSummerHousePicture.CopyTo(fileStream);
                }
                summerHouse.Picture = NewSummerHousePicture.FileName;

                using (var memoryStream = new MemoryStream())
                {
                    NewSummerHousePicture.CopyTo(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    summerHouse.Picture = Convert.ToBase64String(fileBytes);
                }
            }

            _summerHouseRepo.Update(id, summerHouse);

            return RedirectToPage("SummerHouseList");
        }

        // Gør man kommer tilbage til sommerhuslisten, hvis man fortryder
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("SummerHouseList");
        }
    }
}
