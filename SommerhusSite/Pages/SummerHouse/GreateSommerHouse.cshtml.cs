using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SommerhusHjemmeside.Pages.SommerHouseFolder
{
    public class GreateSommerHouseModel : PageModel
    {
        //private ISommerHouseRepository _repo;

        //// dependency injection
        //public GreateSommerHouseModel(ISommerHouseRepository sommerhouserepo)
        //{
        //    _repo = sommerhouserepo;
        //}

        //// Properties til de nye sommerhuse



        //[BindProperty]
        //[Required(ErrorMessage = "Vejnavn skal udfyldes")]
        //public string NewSommerHouseStreetName { get; set; }

        //[BindProperty]
        //[Required(ErrorMessage = "Hus nummer skal udfyldes")]
        //public string NewSommerHouseHouseNumber { get; set; }

        //[BindProperty]
        //public string NewSommerHouseFloor { get; set; }

        //[BindProperty]
        //[Required(ErrorMessage = "Postnummer skal udfyldes")]
        //public int NewSommerHousePostalCode { get; set; }

        //[BindProperty]
        //[Required(ErrorMessage = "Beskrivelse skal udfyldes")]
        //public string NewSommerHouseDescription { get; set; }

        //[BindProperty]
        //[Required(ErrorMessage = "Pris skal udfyldes")]
        //public decimal NewSommerHousePrice{ get; set; }

        //[BindProperty]
        //[Required(ErrorMessage = "Billede skal udfyldes")]
        //public string NewSommerHousePicture { get; set; }


        //public string ErrorMessage { get; private set; }

        //public void OnGet()
        //{
        //}

        //public IActionResult OnPost()
        //{

        //    ErrorMessage = "fEJL 404 KUNNE IKKE OPRETTE EN USER";
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    SommerHouse newsommerhouse = new SommerHouse(NewSommerHouseStreetName, NewSommerHouseHouseNumber, NewSommerHouseFloor, NewSommerHousePostalCode, NewSommerHouseDescription, NewSommerHousePrice, NewSommerHousePicture);

        //    try
        //    {
        //        _repo.Add(newsommerhouse);
        //        TempData["SuccessMessage"] = $"User {newsommerhouse} added successfully";

        //    }
        //    catch (ArgumentException ex)
        //    {
        //        ErrorMessage = ex.Message;
        //        return Page();
        //    }

        //    return RedirectToPage("/Index");

        //}

        //public IActionResult OnPostCancel()
        //{
        //    return RedirectToPage("/Index");
        //}

    }
}
