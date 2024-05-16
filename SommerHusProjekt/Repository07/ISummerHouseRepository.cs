using SommerHusProjekt.Model07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public interface ISummerHouseRepository
    {
        SummerHouse Add(SummerHouse s);
        SummerHouse Delete(int id);
        List<SummerHouse> GetAll();
        List<SummerHouse> GetSomething();
        SummerHouse GetById(int id);
        string? ToString();
        SummerHouse Update(int id, SummerHouse summerHouse);
        List<SummerHouse> Search(int? id, string? streetName, string? houseNumber, int? postalCode, decimal? price, int? amountSleepingSpace, DateTime? dateFrom, DateTime? dateTo);
        List<SummerHouse> SortId();
        List<SummerHouse> SortStreetName();
        List<SummerHouse> SortPostalCode();

        //  public List<SommerHouse> Search(int? id, string? name, string? team);
    }
}
