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
            // Retrieve the logged-in user
            LoggedInUser = SessionHelper.Get<User>(HttpContext);

            // Check if the user is logged in
            if (LoggedInUser != null)
            {
                // Initialize the list to store all bookings
                UserBookings = new List<Booking>();

                // Retrieve bookings for the logged-in user
                int userId = LoggedInUser.Id;

                // Retrieve all bookings for the logged-in user
                UserBookings = _bookingRepository.GetBookingByUserId(userId);

                // Check if any bookings were found
                if (UserBookings.Count == 0)
                {
                    TempData["Message"] = "No bookings found for this user.";
                }
            }
            else
            {
                TempData["Message"] = "Please log in to view your bookings.";
                // Redirect to the login page
                RedirectToPage("/Index");
            }
        }

        public IActionResult OnPostDelete(int bookingId)
        {
            // Retrieve the booking to delete
            Booking bookingToDelete = _bookingRepository.GetById(bookingId);

            // Check if the booking exists
            if (bookingToDelete != null)
            {
                    _bookingRepository.Delete(bookingId);
            }
            else
            {
                // If the booking does not exist, display an error message
                TempData["Message"] = "Booking not found.";
            }

            // Redirect back to the page to refresh the bookings list
            return RedirectToPage();
        }

    }
}
