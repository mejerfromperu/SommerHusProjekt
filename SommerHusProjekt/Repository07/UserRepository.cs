using Microsoft.Data.SqlClient;
using SommerHusProjekt.Model07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
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
            if (user.Floor == null) 
            {
                user.Floor = string.Empty;
            }
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
            Console.WriteLine("Attempting to insert the following values into SommerUser table:");
            Console.WriteLine($"FirstName: {user.FirstName}");
            Console.WriteLine($"LastName: {user.LastName}");
            Console.WriteLine($"Phone: {user.Phone}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Password: {user.Password}");
            Console.WriteLine($"StreetName: {user.StreetName}");
            Console.WriteLine($"HouseNumber: {user.HouseNumber}");
            Console.WriteLine($"Floor: {user.Floor}");
            Console.WriteLine($"PostalCode: {user.PostalCode}");
            Console.WriteLine($"IsAdmin: {user.IsAdmin}");
            Console.WriteLine($"IsLandlord: {user.IsLandlord}");

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            connection.Close();
            return user;
        }

        public User Delete(int id)
        {
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string deleteSql = "DELETE FROM SommerUser WHERE Id = @Id;";

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

        public List<User> GetSomething()
        {
            List<User> list = new List<User>();

            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            string sql = "SELECT Id, FirstName, LastName, Phone, Email FROM SommerUser";
            SqlCommand cmd = new SqlCommand(sql, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                User user = ReadUser2(reader);
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
            user.IsAdmin = reader.GetBoolean(9);
            user.IsLandlord = reader.GetBoolean(10);
            user.PostalCode = reader.GetInt32(11);



            return user;
        }

        private User ReadUser2(SqlDataReader reader)
        {
            User user = new User();

            user.Id = reader.GetInt32(0);
            user.FirstName = reader.GetString(1);
            user.LastName = reader.GetString(2);
            user.Phone = reader.GetString(3);
            user.Email = reader.GetString(4);

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
                    IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                    IsLandlord = reader.GetBoolean(reader.GetOrdinal("IsLandlord"))
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

        public User GetByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(Secret.GetConnectionString))
            {
                connection.Open();

                string selectSql = "SELECT * FROM SommerUser WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(selectSql, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

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
        public string GeneratePasswordResetToken(User user)
        {
            // Generate a secure 4-character token
            var token = GenerateShortToken();

            using (SqlConnection connection = new SqlConnection(Secret.GetConnectionString))
            {
                connection.Open();

                string updateSql = "UPDATE SommerUser SET Token = @Token, TokenExpiry = @Expiry WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(updateSql, connection))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.Parameters.AddWithValue("@Expiry", DateTime.UtcNow.AddHours(1));
                    cmd.Parameters.AddWithValue("@Id", user.Id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine("Rows affected: " + rowsAffected);
                }

                connection.Close();
            }

            return token;
        }

        private string GenerateShortToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[4];
                rng.GetBytes(tokenData);
                var token = new char[4];

                for (int i = 0; i < token.Length; i++)
                {
                    token[i] = chars[tokenData[i] % chars.Length];
                }

                return new string(token);
            }
        }

        // Method to retrieve user by password reset token and handle expiry
        public User GetUserByPasswordResetToken(string token)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(Secret.GetConnectionString))
            {
                connection.Open();

                string selectSql = "SELECT * FROM SommerUser WHERE PasswordResetToken = @Token AND PasswordResetTokenExpiry > @CurrentTime";

                using (SqlCommand cmd = new SqlCommand(selectSql, connection))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.Parameters.AddWithValue("@CurrentTime", DateTime.UtcNow);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = ReadUser(reader);
                        }
                    }
                }

                connection.Close();
            }

            return user;
        }

        // Reset password method
        public void ResetPassword(User user, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(Secret.GetConnectionString))
            {
                connection.Open();

                string updateSql = "UPDATE SommerUser SET Password = @Password, PasswordResetToken = NULL, PasswordResetTokenExpiry = NULL WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(updateSql, connection))
                {
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.Parameters.AddWithValue("@Id", user.Id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine("Rows affected: " + rowsAffected);
                }

                connection.Close();
            }
        }



        public List<User> Search(int? id, string? firstName, string? lastName, string? phone, string? email)
        {
            List<User> retUsers = new List<User>(GetSomething());

            if (id != null)
            {
                retUsers = retUsers.FindAll(u => u.Id == id);
            }

            if (firstName != null)
            {
                retUsers = retUsers.FindAll(u => u.FirstName.Contains(firstName));
            }

            if (lastName != null)
            {
                retUsers = retUsers.FindAll(u => u.LastName.Contains(lastName));
            }

            if (phone != null)
            {
                retUsers = retUsers.FindAll(u => u.Phone.Contains(phone));
            }

            if (email != null)
            {
                retUsers = retUsers.FindAll(u => u.Email.Contains(email));
            }

            return retUsers;
        }

        private bool NumberASC = true;
        public List<User> SortId()
        {
            List<User> retUsers = GetSomething();

            retUsers.Sort(new SortById());

            if (!NumberASC)
            {
                retUsers.Reverse();
            }
            NumberASC = !NumberASC;

            return retUsers;
        }

        private class SortById : IComparer<User>
        {
            public int Compare(User? x, User? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                return x.Id - y.Id;
            }
        }


        public List<User> SortLastName()
        {
            List<User> retUsers = GetSomething();

            retUsers   .Sort((x, y) => x.LastName.CompareTo(y.LastName));

            if (!NameASC)
            {
                retUsers.Reverse();
            }
            NameASC = !NameASC;

            return retUsers;
        }

        private bool NameASC = true;
        public List<User> SortFirstName()
        {
            List<User> retUsers = GetSomething();

            retUsers.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));

            if (!NameASC)
            {
                retUsers.Reverse();
            }
            NameASC = !NameASC;

            return retUsers;
        }

        private class SortByFirstName : IComparer<User>
        {
            public int Compare(User? x, User? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                return x.FirstName.CompareTo(y.FirstName);
            }
        }

        private class SortByLastName : IComparer<User>
        {
            public int Compare(User? x, User? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                return x.LastName.CompareTo(y.LastName);
            }
        }
    }
}
