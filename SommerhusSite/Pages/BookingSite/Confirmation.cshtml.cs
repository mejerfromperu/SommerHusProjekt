using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using SommerhusSite.Services;

namespace SommerhusSite.Pages.BookingSite
{
    public class ConfirmationModel : PageModel
    {

        public ISummerHouseRepository _summerhouse;
        private IBookingRepository _bookinglist;
        public List<Booking> bookings;
        public List<SummerHouse> sommerhuse;

        public int newuserid;
     
        public ConfirmationModel (IBookingRepository bookinglist, ISummerHouseRepository summerhouse)
        {

            _bookinglist = bookinglist;
            _summerhouse = summerhouse;

        }

        



        public void OnGet()
        {

            bookings = _bookinglist.GetAll();
            sommerhuse = _summerhouse.GetAll();
            User? user = SessionHelper.Get<User>(HttpContext);
            newuserid = user.Id;
            SessionHelper.Clear<SummerHouse>(HttpContext);
        }
    }
}
