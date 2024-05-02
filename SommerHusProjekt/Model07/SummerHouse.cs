using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Model07
{
    public class SummerHouse
    {
        // Properties 
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Floor { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        // Constructor 
        public SummerHouse()
        {
            Id = 0;
            StreetName = string.Empty;
            HouseNumber = string.Empty;
            PostalCode = 0;
            City = string.Empty;
            Floor = string.Empty;
            Description = string.Empty;
            Price = 0.0m;
            Picture = string.Empty;
        }

        public SummerHouse(int id, string streetName, string houseNumber, int postalCode, string city, string floor, string description, decimal price, string picture)
        {
            Id = id;
            StreetName = streetName;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
            Floor = floor;
            Description = description;
            Price = price;
            Picture = picture;
        }

        public SummerHouse(string streetName, string houseNumber, string floor, int postalCode, string description, decimal price, string picture)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            Floor = floor;
            PostalCode = postalCode;
            Description = description;
            Price = price;
            Picture = picture;
        }


    }
}
