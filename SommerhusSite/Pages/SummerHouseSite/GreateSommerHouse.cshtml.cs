using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using SommerHusProjekt.Repository07;
using SommerHusProjekt.Model07;

namespace SommerhusHjemmeside.Pages.SommerHouseFolder
{
    public class GreateSommerHouseModel : PageModel
    {
        private ISummerHouseRepository _repo;
        private DateTime _newSummerHouseFromDate;
        private DateTime _newSummerHouseToDate;

        // dependency injection
        public GreateSommerHouseModel(ISummerHouseRepository sommerhouserepo)
        {
            _repo = sommerhouserepo;
        }

        // Properties til de nye sommerhuse
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
        public string NewSummerHousePostalCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Beskrivelse skal udfyldes")]
        public string NewSummerHouseDescription { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Pris skal udfyldes")]
        public decimal NewSummerHousePrice { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Picture skal udfyldes")]
        public string NewSummerHousePicture { get; set ; }

        [BindProperty]
        [Required(ErrorMessage = "Antal Sovepladser skal udfyldes")]
        public int NewSummerHouseAmountSleepingSpace { get; set; }

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

        public void OnGet()
        {
            NewSummerHousePicture = "/images/sommer&solplaceholder.png";
        }

        public IActionResult OnPost()
        {

            ErrorMessage = "fEJL 404 KUNNE IKKE OPRETTE EN USER";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!int.TryParse(NewSummerHousePostalCode, out int postalCode))
            {
                ErrorMessage = "Invalid postal code format";
                return Page(); 
            }

            if (string.IsNullOrEmpty(NewSummerHousePicture))
            {
                NewSummerHousePicture = "/css/Images/Sommer&SOLPlaceHolder.png";
            }

            SummerHouse newsummerhouse = new SummerHouse(NewSummerHouseStreetName, NewSummerHouseHouseNumber, postalCode, NewSummerHouseFloor, NewSummerHouseDescription, NewSummerHousePrice, NewSummerHousePicture, NewSummerHouseFromDate, NewSummerHouseToDate, NewSummerHouseAmountSleepingSpace);

            try
            {
                _repo.Add(newsummerhouse);
                TempData["SuccessMessage"] = $"User {newsummerhouse} added successfully";

            }
            catch (ArgumentException ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }

            return RedirectToPage("/Index");

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Index");
        }

    }
}
