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
        SummerHouse GetById(int id);
        string? ToString();
        SummerHouse Update(int id, SummerHouse summerHouse);

        //  public List<SommerHouse> Search(int? id, string? name, string? team);
    }
}
