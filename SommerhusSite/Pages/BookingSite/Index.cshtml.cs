using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;

namespace SommerhusSite.Pages.BookingSite
{
    public class IndexModel : PageModel
    {
        private readonly ISummerHouseRepository _summerList;
        private readonly IBookingRepository _bookingList;


        [BindProperty]
        //public BookingInfo BookingInfo { get; set; }


        public SummerHouse SelectedSummerhouse { get; set; }

        public IndexModel(ISummerHouseRepository summerlist, IBookingRepository bookinglist)
        {
            _summerList = summerlist;
            _bookingList = bookinglist;
        }

        public void OnGet(int summerhouseId)
        {
            SelectedSummerhouse = _summerList.GetById(summerhouseId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // If model state is not valid, return to the page with validation errors
                return Page();
            }

            // Create a new Booking object with the provided information
            //var newBooking = new Booking
            //{
            //    UserId = /* Get user ID from session or authentication */,
            //    SummerHouseId = BookingInfo.SummerHouseId,
            //    StartDate = BookingInfo.StartDate,
            //    EndDate = BookingInfo.EndDate
            //};

            // Add the booking to the database
            //_bookingRepository.Add(newBooking);

            // Redirect to a confirmation page or another relevant page after successful booking
            return RedirectToPage("/BookingSite/Confirmation");
        }
    }
}
