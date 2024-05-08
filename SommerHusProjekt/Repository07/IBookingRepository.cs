using SommerHusProjekt.Model07;

namespace SommerHusProjekt.Repository07
{
    public interface IBookingRepository
    {
        Booking Add(Booking b);
        Booking Delete(int id);
        List<Booking> GetAll();
        Booking GetById(int id);
        Booking Update(int id, Booking b);
    }
}