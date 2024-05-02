using SommerHusProjekt.Model07;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public class BookingRepository
    {
        public Booking Add(Booking b)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string insertSql = "INSERT INTO SommerBooking (StreetName, HouseNumber, Floor, PostalCode, Description, Price, Picture) VALUES (@StreetName, @HouseNumber, @Floor, @PostalCode, @Description, @Price, @Picture)";

            SqlCommand cmd = new SqlCommand(insertSql, connection);
            cmd.Parameters.AddWithValue("@StreetName", b.StreetName);
            cmd.Parameters.AddWithValue("@HouseNumber", b.HouseNumber);
            cmd.Parameters.AddWithValue("@Floor", b.Floor);
            cmd.Parameters.AddWithValue("@PostalCode", b.PostalCode);
            cmd.Parameters.AddWithValue("@Description", b.Description);
            cmd.Parameters.AddWithValue("@Price", b.Price);
            cmd.Parameters.AddWithValue("@Picture", b.Picture);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();
            return b;
        }

        public Booking Delete(int id)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string deleteSql = "DELETE FROM SommerBooking WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(deleteSql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();


            return null;
        }
        public List<Booking> GetAll()
        {
            List<Booking> list = new List<Booking>();

            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string sql = "SELECT * FROM SommerBooking";
            SqlCommand cmd = new SqlCommand(sql, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Booking booking = ReadBooking(reader);
                list.Add(booking);
            }


            connection.Close();
            return list;
        }

        private Booking ReadBooking(SqlDataReader reader)
        {
            Booking b = new Booking();

            b.Id = reader.GetInt32(0);
            b.StreetName = reader.GetString(1);
            b.HouseNumber = reader.GetString(2);
            b.Floor = reader.GetString(3);
            b.PostalCode = reader.GetInt32(4);
            b.Description = reader.GetString(5);
            b.Price = reader.GetDecimal(6);
            b.Picture = reader.GetString(7);



            return b;
        }

        public Booking GetById(int id)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string selectSql = "SELECT * FROM SommerBooking WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(selectSql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            Booking b = null;

            if (reader.Read())
            {
                b = new Booking
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    StreetName = reader["StreetName"].ToString(),
                    HouseNumber = reader["HouseNumber"].ToString(),
                    Floor = reader["Floor"].ToString(),
                    PostalCode = Convert.ToInt32(reader["PostalCode"]),
                    Description = reader["Description"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Picture = reader["Picture"].ToString(),
                };
            }

            reader.Close();
            connection.Close();

            return b;
        }

        public Booking Update(int id, Booking b)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string updateSql = "UPDATE SommerBooking SET StreetName = @StreetName, HouseNumber = @HouseNumber, Floor = @Floor, Postalcode = @Postalcode, Description = @Description, Price = @Price, Picture = @Picture  WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(updateSql, connection);
            cmd.Parameters.AddWithValue("@StreetName", b.StreetName);
            cmd.Parameters.AddWithValue("@HouseNumber", b.HouseNumber);
            cmd.Parameters.AddWithValue("@Floor", b.Floor);
            cmd.Parameters.AddWithValue("@PostalCode", b.PostalCode);
            cmd.Parameters.AddWithValue("@Description", b.Description);
            cmd.Parameters.AddWithValue("@Price", b.Price);
            cmd.Parameters.AddWithValue("@Picture", b.Picture);
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
