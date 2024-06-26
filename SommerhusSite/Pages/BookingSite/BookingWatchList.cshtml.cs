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
                UserBookings = new List<Booking>();

                int userId = LoggedInUser.Id;

                UserBookings = _bookingRepository.GetBookingByUserId(userId);

                if (UserBookings.Count == 0)
                {
                    TempData["Message"] = "Ingen bookinger fundet for denne profil";
                }
            }
            else
            {
                TempData["Message"] = "Login for at se dine bookinger";
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
