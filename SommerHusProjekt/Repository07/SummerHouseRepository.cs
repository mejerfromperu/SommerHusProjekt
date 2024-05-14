using SommerHusProjekt.Model07;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public class SummerHouseRepository : ISummerHouseRepository
    {
        public SummerHouse Add(SummerHouse s)
        {
            //HEJ
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string insertSql = "INSERT INTO SommerSommerHouse (StreetName, HouseNumber, Floor, PostalCode, Description, Price, Picture, DateFrom, DateTo, AmountSleepingSpace) VALUES (@StreetName, @HouseNumber, @Floor, @PostalCode, @Description, @Price, @Picture, @DateFrom, @DateTo, @AmountSleepingSpace)";

            SqlCommand cmd = new SqlCommand(insertSql, connection);
            cmd.Parameters.AddWithValue("@StreetName", s.StreetName);
            cmd.Parameters.AddWithValue("@HouseNumber", s.HouseNumber);
            cmd.Parameters.AddWithValue("@Floor", s.Floor);
            cmd.Parameters.AddWithValue("@PostalCode", s.PostalCode);
            cmd.Parameters.AddWithValue("@Description", s.Description);
            cmd.Parameters.AddWithValue("@Price", s.Price);
            cmd.Parameters.AddWithValue("@Picture", s.Picture);
            cmd.Parameters.AddWithValue("@DateFrom", s.DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", s.DateTo);
            cmd.Parameters.AddWithValue("@AmountSleepingSpace", s.AmountSleepingSpace);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();
            return s;
        }

        public SummerHouse Delete(int id)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string deleteSql = "DELETE FROM SommerSommerHouse WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(deleteSql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();


            return null;
        }

        public List<SummerHouse> GetAll()
        {
            List<SummerHouse> list = new List<SummerHouse>();

            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string sql = "SELECT * FROM SommerSommerHouse";
            SqlCommand cmd = new SqlCommand(sql, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SummerHouse sommerHouse = ReadSommerHouse(reader);
                list.Add(sommerHouse);
            }


            connection.Close();
            return list;
        }

        public List<SummerHouse> GetSomething()
        {
            List<SummerHouse> list = new List<SummerHouse>();

            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string sql = "SELECT Id, StreetName, HouseNumber, Floor, PostalCode, Price, DateFrom, DateTo FROM SommerSommerHouse";
            SqlCommand cmd = new SqlCommand(sql, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SummerHouse sommerHouse = ReadSommerHouse2(reader);
                list.Add(sommerHouse);
            }


            connection.Close();
            return list;
        }

        private SummerHouse ReadSommerHouse(SqlDataReader reader)
        {
            SummerHouse s = new SummerHouse();

            s.Id = reader.GetInt32(0);
            s.StreetName = reader.GetString(1);
            s.HouseNumber = reader.GetString(2);
            s.Floor = reader.IsDBNull(3) ? null : reader.GetString(3);
            s.PostalCode = reader.GetInt32(4);
            s.Description = reader.GetString(5);
            s.Price = reader.GetDecimal(6);
            s.Picture = reader.IsDBNull(7) ? null : reader.GetString(7);
            s.DateFrom = reader.GetDateTime(8);
            s.DateTo = reader.GetDateTime(9);
            s.AmountSleepingSpace = reader.GetInt32(10);

            return s;
        }

        private SummerHouse ReadSommerHouse2(SqlDataReader reader)
        {
            SummerHouse s = new SummerHouse();

            s.Id = reader.GetInt32(0);
            s.StreetName = reader.GetString(1);
            s.HouseNumber = reader.GetString(2);
            s.Floor = reader.IsDBNull(3) ? null : reader.GetString(3);
            s.PostalCode = reader.GetInt32(4);
            s.Price = reader.GetDecimal(5);

            return s;
        }

        public SummerHouse GetById(int id)
    {
        SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
        connection.Open();

        string selectSql = "SELECT * FROM SommerSommerHouse WHERE Id = @Id";

        SqlCommand cmd = new SqlCommand(selectSql, connection);
        cmd.Parameters.AddWithValue("@Id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        SummerHouse s = null;

        if (reader.Read())
        {
            s = ReadSommerHouse(reader);
        }

        reader.Close();
        connection.Close();

        return s;
    }

        public SummerHouse Update(int id, SummerHouse s)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string updateSql = "UPDATE SommerSommerHouse SET StreetName = @StreetName, HouseNumber = @HouseNumber, Floor = @Floor, PostalCode = @PostalCode, Description = @Description, Price = @Price, Picture = @Picture, DateFrom = @DateFrom, DateTo = @DateTo, AmountSleepingSpace = @AmountSleepingSpace WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(updateSql, connection);
            cmd.Parameters.AddWithValue("@StreetName", s.StreetName);
            cmd.Parameters.AddWithValue("@HouseNumber", s.HouseNumber);
            cmd.Parameters.AddWithValue("@Floor", s.Floor);
            cmd.Parameters.AddWithValue("@PostalCode", s.PostalCode);
            cmd.Parameters.AddWithValue("@Description", s.Description);
            cmd.Parameters.AddWithValue("@Price", s.Price);
            cmd.Parameters.AddWithValue("@Picture", s.Picture);
            cmd.Parameters.AddWithValue("@DateFrom", s.DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", s.DateTo);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@AmountSleepingSpace", s.AmountSleepingSpace);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();

            if (rowsAffected > 0)
            {
                return GetById(id);
            }
            return null;
        }

        public List<SummerHouse> Search(int? id, string? streetName, string? houseNumber, string? floor, int? postalCode, decimal? price)
        {
            List<SummerHouse> retSummerHouses = new List<SummerHouse>(GetSomething());

            if (id != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.Id == id);
            }

            if (streetName != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.StreetName.Contains(streetName));
            }

            if (houseNumber != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.HouseNumber.Contains(houseNumber));
            }

            if (floor != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.Floor.Contains(floor));
            }

            if (postalCode != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.PostalCode == postalCode);
            }

            if (price != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.Price == price);
            }

            return retSummerHouses;
        }

        private bool NumberASC = true;
        public List<SummerHouse> SortId()
        {
            List<SummerHouse> retSummerHouses = GetSomething();

            retSummerHouses.Sort(new SortById());

            if (!NumberASC)
            {
                retSummerHouses.Reverse();
            }
            NumberASC = !NumberASC;

            return retSummerHouses;
        }

        private class SortById : IComparer<SummerHouse>
        {
            public int Compare(SummerHouse? x, SummerHouse? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                return x.Id - y.Id;
            }
        }

        private bool NameASC = true;

        public List<SummerHouse> SortStreetName()
        {
            List<SummerHouse> retSummerHouses = GetSomething();

            retSummerHouses.Sort((x, y) => x.StreetName.CompareTo(y.StreetName));

            if (!NameASC)
            {
                retSummerHouses.Reverse();
            }
            NameASC = !NameASC;

            return retSummerHouses;
        }

        private class SortByFirstName : IComparer<SummerHouse>
        {
            public int Compare(SummerHouse? x, SummerHouse? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                return x.StreetName.CompareTo(y.StreetName);
            }
        }

        public List<SummerHouse> SortPostalCode()
        {
            List<SummerHouse> retSummerHouses = GetSomething();

            retSummerHouses.Sort(new SortByPostalCode());

            if (!NumberASC)
            {
                retSummerHouses.Reverse();
            }
            NumberASC = !NumberASC;

            return retSummerHouses;
        }

        private class SortByPostalCode : IComparer<SummerHouse>
        {
            public int Compare(SummerHouse? x, SummerHouse? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                return x.PostalCode - y.PostalCode;
            }
        }

        
    }
}
