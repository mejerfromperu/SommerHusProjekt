using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Model07
{
    public class Booking
    {

        // Properties uden manuelt at skrive instans felterne.
        public int Id { get; set; }
        public int UserId { get; set; } // Id på brugeren, der har foretaget bookingen
        public int SummerHouseId { get; set; } // Id på det sommerhus, der er booket
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        // Constructor
        public Booking() : this(1, 2, DateTime.Now, DateTime.UtcNow)
        {
            
        }

        public Booking(int userId, int summerHouseId, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            SummerHouseId = summerHouseId;
            StartDate = startDate;
            EndDate = endDate;

        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(UserId)}={UserId.ToString()}, {nameof(SummerHouseId)}={SummerHouseId.ToString()}, {nameof(StartDate)}={StartDate.ToString()}, {nameof(EndDate)}={EndDate.ToString()}}}";
        }
    }
}
