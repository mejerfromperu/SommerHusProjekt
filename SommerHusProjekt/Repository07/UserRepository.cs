using Microsoft.Data.SqlClient;
using SommerHusProjekt.Model07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public class UserRepository : IUserRepository
    {
        public User Add(User user)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string insertSql = "INSERT INTO SommerUser (FirstName, LastName, Phone, Email, Password, StreetName, HouseNumber, Floor, PostalCode, IsAdmin, IsLandlord) VALUES (@FirstName, @LastName, @Phone, @Email, @Password, @StreetName, @HouseNumber, @Floor, @PostalCode, @IsAdmin, @IsLandlord)";

            SqlCommand cmd = new SqlCommand(insertSql, connection);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@StreetName", user.StreetName);
            cmd.Parameters.AddWithValue("@HouseNumber", user.HouseNumber);
            cmd.Parameters.AddWithValue("@Floor", user.Floor);
            cmd.Parameters.AddWithValue("@PostalCode", user.PostalCode);
            cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            cmd.Parameters.AddWithValue("@IsLandlord", user.IsLandlord);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();
            return user;
        }

        public User Delete(int id)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string deleteSql = "DELETE FROM SommerUser WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(deleteSql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();


            return null;
        }

        public List<User> GetAll()
        {
            List<User> list = new List<User>();

            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string sql = "SELECT * FROM SommerUser";
            SqlCommand cmd = new SqlCommand(sql, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                User user = ReadUser(reader);
                list.Add(user);
            }


            connection.Close();
            return list;


        }

        private User ReadUser(SqlDataReader reader)
        {
            User user = new User();

            user.Id = reader.GetInt32(0);
            user.FirstName = reader.GetString(1);
            user.LastName = reader.GetString(2);
            user.Phone = reader.GetString(3);
            user.Email = reader.GetString(4);
            user.Password = reader.GetString(5);
            user.StreetName = reader.GetString(6);
            user.HouseNumber = reader.GetString(7);
            user.Floor = reader.GetString(8);
            user.PostalCode = reader.GetInt32(9);
            user.IsAdmin = reader.GetBoolean(10);
            user.IsLandlord = reader.GetBoolean(11);



            return user;
        }

        public User GetById(int id)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string selectSql = "SELECT * FROM SommerUser WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(selectSql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            User user = null;

            if (reader.Read())
            {
                user = new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    StreetName = reader["StreetName"].ToString(),
                    HouseNumber = reader["HouseNumber"].ToString(),
                    Floor = reader["Floor"].ToString(),
                    PostalCode = Convert.ToInt32(reader["PostalCode"]),
                    IsAdmin = reader["IsAdmin"].ToString() == "false",
                    IsLandlord = reader["IsLandlord"].ToString() == "false"
                };
            }

            reader.Close();
            connection.Close();

            return user;
        }

        public User Update(int id, User user)
        {
            SqlConnection connection = new SqlConnection("Data Source=mssql16.unoeuro.com;Initial Catalog=isakgm_dk_db_test;User ID=isakgm_dk;Password=f2t9wHmFRDenbEA53ghp;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection.Open();

            string updateSql = "UPDATE SommerUser SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @Email, Password = @Password, StreetName = @StreetName, HouseNumber = @HouseNumber, Floor = @Floor, Postalcode = @Postalcode, IsLandlord = @IsLandlord  WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(updateSql, connection);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@StreetName", user.StreetName);
            cmd.Parameters.AddWithValue("@HouseNumber", user.HouseNumber);
            cmd.Parameters.AddWithValue("@Floor", user.Floor);
            cmd.Parameters.AddWithValue("@PostalCode", user.PostalCode);
            cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            cmd.Parameters.AddWithValue("@IsLandlord", user.IsLandlord);
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

        public User GetByEmailAndPassword(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(Secret.GetConnectionString))
            {
                connection.Open();

                string selectSql = "SELECT * FROM SommerUser WHERE Email = @Email AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(selectSql, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                StreetName = reader.GetString(reader.GetOrdinal("StreetName")),
                                HouseNumber = reader.GetString(reader.GetOrdinal("HouseNumber")),
                                Floor = reader.GetString(reader.GetOrdinal("Floor")),
                                PostalCode = reader.GetInt32(reader.GetOrdinal("PostalCode")),
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                                IsLandlord = reader.GetBoolean(reader.GetOrdinal("IsLandlord"))
                            };
                        }
                    }
                }

            }

            return null; // User not found
        }

        public List<User> Search(int? id, string? name, string? team)
        {
            throw new NotImplementedException();
        }
    }
}
