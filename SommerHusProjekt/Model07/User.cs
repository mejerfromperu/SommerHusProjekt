using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Model07
{
    public class User
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _phone;
        private string _streetName;
        private string _houseNumber;
        private string _floor;
        private string _city;
        private int _postalCode;
        private bool _isLandlord;
        private bool _isAdmin;


        // Properties
        public int Id { get => _id; set => _id = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string StreetName { get => _streetName; set => _streetName = value; }
        public string HouseNumber { get => _houseNumber; set => _houseNumber = value; }
        public string Floor { get => _floor; set => _floor = value; }
        public string City { get => _city; set => _city = value; }
        public int PostalCode { get => _postalCode; set => _postalCode = value; }
        public bool IsLandlord { get => _isLandlord; set => _isLandlord = value; }
        public bool IsAdmin { get => _isAdmin; set => _isAdmin = value; }

        // Constructor

        public User() : this(666, "jonny", "mejer", "Jonny@gmail.com", "999", "18293922", "jonny street", "jonny number", "2", "jonnycity", 4000, false, true)
        {
            
        }

        public User(int id, string firstName, string lastName, string phone, string email, string password, string streetName, string houseNumber, string floor, string city, int postalCode, bool isLandLord, bool isadmin)
        {
            id = 0;
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _password = password;
            _phone = phone;
            _streetName = streetName;
            _houseNumber = houseNumber; 
            _floor = floor;
            _city = city;
            _postalCode = postalCode;
            _isLandlord = isLandLord;
            _isAdmin = isadmin;
        }

        public User( string firstName, string lastName, string phone, string email, string password, string streetName, string houseNumber, string floor, int postalCode, bool isLandLord, bool isadmin)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _password = password;
            _phone = phone;
            _streetName = streetName;
            _houseNumber = houseNumber;
            _floor = floor;
            _postalCode = postalCode;
            _isLandlord = isLandLord;
            _isAdmin = isadmin;
        }

        public User(string firstName, string lastName, string phone, string email, string password, string streetName, string houseNumber, string floor, string city, int postalCode)
        {
            _id = 0;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _password = password;
            _phone = phone;
            _streetName = streetName;
            _houseNumber = houseNumber;
            _floor = floor;
            _city = city;
            _postalCode = postalCode;

        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(FirstName)}={FirstName}, {nameof(LastName)}={LastName}, {nameof(Email)}={Email}, {nameof(Password)}={Password}, {nameof(Phone)}={Phone}, {nameof(StreetName)}={StreetName}, {nameof(HouseNumber)}={HouseNumber}, {nameof(Floor)}={Floor}, {nameof(City)}={City}, {nameof(PostalCode)}={PostalCode.ToString()}, {nameof(IsLandlord)}={IsLandlord.ToString()}, {nameof(IsAdmin)}={IsAdmin.ToString()}}}";
        }
    }
}
