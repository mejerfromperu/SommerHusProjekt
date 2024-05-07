using SommerHusProjekt.Model07;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public class SummerHouseRepository : ISummerHouseRepository
    {
        public SummerHouse Add(SummerHouse s)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string insertSql = "INSERT INTO SommerSommerHouse (StreetName, HouseNumber, Floor, PostalCode, Description, Price, Picture, DateFrom, DateTo) VALUES (@StreetName, @HouseNumber, @Floor, @PostalCode, @Description, @Price, @Picture, @DateFrom, @DateTo)";

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

            string updateSql = "UPDATE SommerSommerHouse SET StreetName = @StreetName, HouseNumber = @HouseNumber, Floor = @Floor, PostalCode = @PostalCode, Description = @Description, Price = @Price, Picture = @Picture, DateFrom = @DateFrom, DateTo = @DateTo WHERE Id = @Id";

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

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();

            if (rowsAffected > 0)
            {
                return GetById(id);
            }
            return null;
        }
    }
}
