using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            DateFrom = DateTime.MinValue;
            DateTo = DateTime.MinValue;
        }

        public SummerHouse(int id, string streetName, string houseNumber, int postalCode, string city, string floor, string description, decimal price, string picture, DateTime dateFrom, DateTime dateTo)
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
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public SummerHouse(string streetName, string houseNumber, int postalCode, string floor, string description, decimal price, string picture, DateTime dateFrom, DateTime dateTo)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            Floor = floor;
            Description = description;
            Price = price;
            Picture= picture;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(StreetName)}={StreetName}, {nameof(HouseNumber)}={HouseNumber}, {nameof(PostalCode)}={PostalCode.ToString()}, {nameof(City)}={City}, {nameof(Floor)}={Floor}, {nameof(Description)}={Description}, {nameof(Price)}={Price.ToString()}, {nameof(Picture)}={Picture}, {nameof(DateFrom)}={DateFrom.ToString()}, {nameof(DateTo)}={DateTo.ToString()}}}";
        }
    }

}
