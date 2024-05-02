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

            string insertSql = "INSERT INTO SommerBooking (UserId, SummerHouseId, StartDate, EndDate) VALUES (@UserId, @SummerHouseId, @StartDate, @EndDate)";

            SqlCommand cmd = new SqlCommand(insertSql, connection);
            cmd.Parameters.AddWithValue("@UserId", b.UserId);
            cmd.Parameters.AddWithValue("@SummerHouseId", b.SummerHouseId);
            cmd.Parameters.AddWithValue("@StartDate", b.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", b.EndDate);

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
            b.UserId = reader.GetInt32(1);
            b.SummerHouseId = reader.GetInt32(2);
            b.StartDate = reader.GetDateTime(3);
            b.EndDate = reader.GetDateTime(4);
            



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
                    UserId = Convert.ToInt32(reader["UserId"]),
                    SummerHouseId = Convert.ToInt32(reader["SummerHouseId"]),
                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                    EndDate = Convert.ToDateTime(reader["EndDate"])
                   
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

            string updateSql = "UPDATE SommerBooking SET UserId = @UserId, SummerHouseId = @SummerHouseId, StartDate = @StartDate, EndDate = @EndDate  WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(updateSql, connection);
            cmd.Parameters.AddWithValue("@UserId", b.UserId);
            cmd.Parameters.AddWithValue("@SummerHouseId", b.SummerHouseId);
            cmd.Parameters.AddWithValue("@StartDate", b.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", b.EndDate);
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
