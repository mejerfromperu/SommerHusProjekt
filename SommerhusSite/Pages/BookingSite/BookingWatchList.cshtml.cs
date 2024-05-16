using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using SommerhusSite.Services;

namespace SommerhusSite.Pages.BookingSite
{
    public class BookingWatchListModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;

        public List<Booking> UserBookings { get; set; }

        public User LoggedInUser { get; set; }

        public BookingWatchListModel(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void OnGet()
        {
            LoggedInUser = SessionHelper.Get<User>(HttpContext);

            if (LoggedInUser != null)
            {
                // list to store all the bookings
                UserBookings = new List<Booking>();

                // Retrieve bookings for the logged-in user
                int userId = LoggedInUser.Id;

                UserBookings = _bookingRepository.GetBookingByUserId(userId);

                // Check if any bookings
                if (UserBookings.Count == 0)
                {
                    TempData["Message"] = "No bookings found for this user.";
                }
            }
            else
            {
                TempData["Message"] = "Please log in to view your bookings.";
                RedirectToPage("Index");
            }
        }

        public IActionResult OnPostDelete(int Id)
        {
            _bookingRepository.Delete(Id);
            return RedirectToPage("BookingWatchList");
        }

    }
}
