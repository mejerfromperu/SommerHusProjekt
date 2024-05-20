using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using SommerhusSite.Services;

namespace SommerhusSite.Pages.BookingSite
{
    public class IndexModel : PageModel
    {
        private readonly ISummerHouseRepository _summerList;
        private readonly IBookingRepository _bookingList;

        [BindProperty]
        public Booking Booking { get; set; }

        public SummerHouse SelectedSummerhouse { get; set; }

        public User LoggedInUser { get; set; } 


        


        public IndexModel(ISummerHouseRepository summerlist, IBookingRepository bookinglist)
        {
            _summerList = summerlist;
            _bookingList = bookinglist;
        }

        public void OnGet(int summerhouseId)
        {
            SelectedSummerhouse = _summerList.GetById(summerhouseId);
            LoggedInUser = SessionHelper.Get<User>(HttpContext);

            SessionHelper.Set(SelectedSummerhouse, HttpContext);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User loggedInUser = SessionHelper.Get<User>(HttpContext);
            SummerHouse chosenHouse = SessionHelper.Get<SummerHouse>(HttpContext);

            // Validate booking dates
            if (Booking.StartDate < chosenHouse.DateFrom ||
    Booking.EndDate > chosenHouse.DateTo ||
    Booking.EndDate < Booking.StartDate)
            {
                ModelState.AddModelError("", "Sorry, the booking dates are not within the available period for the selected summer house or the end date is before the start date.");
                return Page();
            }


            var newBooking = new Booking
            {
                UserId = loggedInUser.Id,
                SummerHouseId = chosenHouse.Id,
                StartDate = Booking.StartDate,
                EndDate = Booking.EndDate
            };

            // Add the booking to the database
            _bookingList.Add(newBooking);

            return RedirectToPage("/BookingSite/Confirmation");
        }

        public IActionResult OnPostCancel()
        {
            SessionHelper.Clear<SummerHouse>(HttpContext);

            return RedirectToPage("/Index");
        }
    }
}
