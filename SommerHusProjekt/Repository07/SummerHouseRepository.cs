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
        //Add metode til sommerhus
        public SummerHouse Add(SummerHouse s)
        {

            try
            {
                //Connecter til database via connectionstring
                SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
                connection.Open();

                // checker om postnummeret eksitere
                string checkPostalCodeSql = "SELECT COUNT(1) FROM SommerPostalCode WHERE PostalCode = @PostalCode";
                using (SqlCommand checkCmd = new SqlCommand(checkPostalCodeSql, connection))
                {
                    checkCmd.Parameters.AddWithValue("@PostalCode", user.PostalCode);
                    int postalCodeExists = (int)checkCmd.ExecuteScalar();

                    if (postalCodeExists == 0)
                    {
                        throw new InvalidOperationException("Postnummeret findes ikke");
                    }
                }

                //Laver en query, det som står i "" og indsætter den i databasen. Så vi kan få indsat alt info på sommerhuset
                string insertSql = "INSERT INTO SommerSommerHouse (StreetName, HouseNumber, Floor, PostalCode, Description, Price, Picture, DateFrom, DateTo, AmountSleepingSpace) VALUES (@StreetName, @HouseNumber, @Floor, @PostalCode, @Description, @Price, @Picture, @DateFrom, @DateTo, @AmountSleepingSpace)";

                //Her kører vi den query til vores database med alle parameterne på sommerhuset
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
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // specifik exception for invalid input key i ms sql
                {
                    throw new InvalidOperationException("Postnummer findes ikke", ex);
                }
                throw;
            }

            //Lukker connectionen til database, så vi er sikre på der ikke sker mere.

            //Returnerer sommerhuset s
            return s;
        }

        //Slet metode til sommerhus
        public SummerHouse Delete(int id)
        {
            //Åbner en connection til database
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            //Laver query vi vil bruge som er slet sommerhus med bestemt id
            string deleteSql = "DELETE FROM SommerSommerHouse WHERE Id = @Id";

            //Kører query 
            SqlCommand cmd = new SqlCommand(deleteSql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            //Ændringerne sker til databasen
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);

            //Lukker connection til databse
            connection.Close();

            //Returnerer ikke noget
            return null;
        }

        //Metode til at få en lise på sommerhusene med alt info
        public List<SummerHouse> GetAll()
        {
            //Laver en ny liste
            List<SummerHouse> list = new List<SummerHouse>();

            //Laver connection til database og åbner den
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            //Query hvor vi får alt fra tabelen om sommerhuse
            string sql = "SELECT * FROM SommerSommerHouse";
            SqlCommand cmd = new SqlCommand(sql, connection);

            //En reader som læser hvad der står i databsen og tilføjer den til lisen
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SummerHouse sommerHouse = ReadSommerHouse(reader);
                list.Add(sommerHouse);
            }

            //Lukker connection til database og returnerer listen
            connection.Close();
            return list;
        }
        public List<SummerHouse> GetAllWithCity()
        {
            List<SummerHouse> list = new List<SummerHouse>();

            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            
                connection.Open();

                string sql = "SELECT SommerSommerHouse.Id, SommerSommerHouse.StreetName, SommerSommerHouse.HouseNumber, SommerSommerHouse.PostalCode, SommerPostalcode.City, SommerSommerHouse.Floor, SommerSommerHouse.Description, SommerSommerHouse.Price, SommerSommerHouse.Picture, SommerSommerHouse.DateFrom, SommerSommerHouse.DateTo, SommerSommerHouse.AmountSleepingSpace FROM SommerSommerHouse INNER JOIN SommerPostalcode ON SommerSommerHouse.PostalCode = SommerPostalcode.Postalcode";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SummerHouse s = new SummerHouse
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                StreetName = reader["StreetName"].ToString(),
                                HouseNumber = reader["HouseNumber"].ToString(),
                                PostalCode = Convert.ToInt32(reader["PostalCode"]),
                                City = reader["City"].ToString(),
                                Floor = reader["Floor"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Picture = reader["Picture"].ToString(),
                                DateFrom = Convert.ToDateTime(reader["DateFrom"]),
                                DateTo = Convert.ToDateTime(reader["DateTo"]),
                                AmountSleepingSpace = Convert.ToInt32(reader["AmountSleepingSpace"])
                            };
                            list.Add(s);
                        }
                    }
                }
            

            return list;
        }

        //Metode til at få nogle informationer om sommerhus (Brugt til lidt testning)
        public List<SummerHouse> GetSomething()
        {
            //Laver ny liste
            List<SummerHouse> list = new List<SummerHouse>();

            //Laver connection til databse og åbner den
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            //Laver query hvor vi gerne vil have info om id, vejnavn, husnummer, etage, postnummer, pris, datofra og datotil fra sommerhus tabel.
            string sql = "SELECT Id, StreetName, HouseNumber, Floor, PostalCode, Price, DateFrom, DateTo FROM SommerSommerHouse";
            SqlCommand cmd = new SqlCommand(sql, connection);

            //Kører reader som laver infoen og tilføjer den til listen
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SummerHouse sommerHouse = ReadSommerHouse2(reader);
                list.Add(sommerHouse);
            }

            //Lukker databsen og returnerer listen
            connection.Close();
            return list;
        }

        //Metode som er en reader til somemrhus, den skal læse hvad der er i databasen på felterne i tabelen 
        private SummerHouse ReadSommerHouse(SqlDataReader reader)
        {
            //Laver nyt objekt af sommerhus
            SummerHouse s = new SummerHouse();

            //Vi læser en værdi på hver felt og sætter den til at være s objektets parameter
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
            s.AmountSleepingSpace = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);

            //Vi returnere objektet s som sommerhus nu med alle værdierne fra databasen
            return s;
        }

        //Metode som er en reader til somemrhus, den skal læse hvad der er i databasen på felterne i tabelen (Lavet til noget testing)
        private SummerHouse ReadSommerHouse2(SqlDataReader reader)
        {
            //Laver nyt objent af sommerhus
            SummerHouse s = new SummerHouse();

            //Vi læser en værdi på hver felt og sætter den til at være s objektets parameter
            s.Id = reader.GetInt32(0);
            s.StreetName = reader.GetString(1);
            s.HouseNumber = reader.GetString(2);
            s.Floor = reader.IsDBNull(3) ? null : reader.GetString(3);
            s.PostalCode = reader.GetInt32(4);
            s.Price = reader.GetDecimal(5);

            //Vi returnere objektet s som sommerhus nu med alle værdierne fra databasen
            return s;
        }

        //Metode til at få et specifikt sommerhus udfra id
        public SummerHouse GetById(int id)
        {
            //Laver forbindelse til databasen og åbner den
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            //Laver query som vælger at fra sommerhus tabel, hvor id er det valgte id
            string selectSql = "SELECT * FROM SommerSommerHouse WHERE Id = @Id";

            //Kører query og bruger parameter iD 
            SqlCommand cmd = new SqlCommand(selectSql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            //executer en reader
            SqlDataReader reader = cmd.ExecuteReader();

            //Sætter sommerhus objekt s til null
            SummerHouse s = null;

            //If statement for at se om vi kan read et sommerhus 
            if (reader.Read())
            {
                s = ReadSommerHouse(reader);
            }
            //Lukker reader og connetion til databse
            reader.Close();
            connection.Close();

            //Returnere vores sommerhus objekt s
            return s;
        }

        //Vores opdater sommerhus metode som går udfra et id og et sommerhus.
        public SummerHouse Update(int id, SummerHouse s)
        {
            //Laver forbindelse til databasen og åbner den
            SqlConnection connection = new SqlConnection(Secret.GetConnectionString);
            connection.Open();

            //Laver query til at update sommerhus og sætter værdierne på simmerhuset til de nye værdier man giver, det sker udfra hvilket id man giver
            string updateSql = "UPDATE SommerSommerHouse SET StreetName = @StreetName, HouseNumber = @HouseNumber, Floor = @Floor, PostalCode = @PostalCode, Description = @Description, Price = @Price, Picture = @Picture, DateFrom = @DateFrom, DateTo = @DateTo, AmountSleepingSpace = @AmountSleepingSpace WHERE Id = @Id";

            //Kører query og tilføjer de nye værdier som vi vil give til det specifikke sommerhus
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

            //Executer det til rækkerne
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected: " + rowsAffected);
            
            //Lukker forbindelse til databse
            connection.Close();

            //Hvis man har påvirket nogle rækker, så kører vi metoden getbyid
            if (rowsAffected > 0)
            {
                return GetById(id);
            }
            //Returnerer null
            return null;
        }

        //Her har vi en metode til at søge i en liste af sommerhuse på forskellig information omkring sommerhusene
        public List<SummerHouse> Search(int? id, string? streetName, string? houseNumber, int? postalCode, decimal? price, int? amountSleepingSpace, DateTime? dateFrom, DateTime? dateTo)
        {
            //Vi laver en ny liste som får alle sommerhusene i sig
            List<SummerHouse> retSummerHouses = new List<SummerHouse>(GetAll());

            //Hvis ID ikke er null, så finder vi alle sommerhuse som har id'er der er lig med det valgte id
            if (id != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.Id == id);
            }

            //Hvis vejnavn ikke er null, så finder vi alle sommerhuse som indenholder de skrevende bogstaver i deres vejnavn, store og små bogstaver begynder ikke noget
            if (streetName != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.StreetName.Contains(streetName, StringComparison.OrdinalIgnoreCase));
            }

            //Hvis husnummer ikke er null, så finder vi alle sommerhuse som indeholder de skrevende bogstaver/tal i deres husnummer
            if (houseNumber != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.HouseNumber.Contains(houseNumber));
            }

            //Hvis postnummer ikke er null, så finder vi alle sommerhuse, hvor det skrevende postnummer er lig det på sommerhusene
            if (postalCode != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.PostalCode == postalCode);
            }

            //Hvis pris ikke er null, så finder vi alle sommerhuse, hvor sommerhusenes pris er lig eller lavere end det skrevet beløb
            if (price != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.Price <= price);
            }
            
            //Hvis antal gæster ikke er null, så finder vi alle sommerhuse, hvor sommerhusenes antalgæster er lig eller højere end det skrevet antal gæster
            if (amountSleepingSpace != null)
            {
                retSummerHouses = retSummerHouses.FindAll(s => s.AmountSleepingSpace >= amountSleepingSpace);
            }

            //Hvis dato fra eller dato til ikke er null, så sker en af de 3 nedre muligheder
            if (dateFrom != null || dateTo != null)
            {
                //Første har vi hvis begge ikke er null, så finder vi alle sommerhuse som er mellem til og fra datoen, hvor vi tæller de valgte datoer med
                if (dateFrom != null && dateTo != null)
                {
                    retSummerHouses = retSummerHouses.FindAll(s => s.DateFrom >= dateFrom && s.DateTo <= dateTo);
                }
                //Ellers hvis kun fra dato ikke er null, så finder vi alle sommerhuse som er efter eller på den valgte dato
                else if (dateFrom != null)
                {
                    retSummerHouses = retSummerHouses.FindAll(s => s.DateFrom >= dateFrom);
                }
                //Til sidst hvis dato til ikke er null, så finder vi alle sommerhuse som er på den dato eller før
                else if (dateTo != null)
                {
                    retSummerHouses = retSummerHouses.FindAll(s => s.DateTo <= dateTo);
                }
            }

            //Returner alle sommerhuse som passer iforhold til alle de søgte parameter
            return retSummerHouses;
        }

        //Boolean vi skal bruge til sorterings metode
        private bool NumberASC = true;

        //Sortering af id metode
        public List<SummerHouse> SortId()
        {
            //Laver liste som får alt data
            List<SummerHouse> retSummerHouses = GetAll();

            //Beder den sort efter sortbyid metode som vi laver længere nede
            retSummerHouses.Sort(new SortById());

            //Hvis vores boolean ikke er true så reverser den sorteringen 
            if (!NumberASC)
            {
                retSummerHouses.Reverse();
            }
            //Gør at booleanen skifter hver gang metoden bliver kaldt
            NumberASC = !NumberASC;

            //Returnerer listen med sommerhusene som så er sorteret
            return retSummerHouses;
        }

        //Vores Icomparer sortbyid metode
        private class SortById : IComparer<SummerHouse>
        {
            //Compare sommerhus x og somemrhus y
            public int Compare(SummerHouse? x, SummerHouse? y)
            {
                //hvis en af værdierne er null så returnerer vi ikke noget
                if (x == null || y == null)
                {
                    return 0;
                }

                //Ellers returnerer vi sommerhusene x-y udfra deres Id
                return x.Id - y.Id;
            }
        }

        //Ny boolean der skal bruges til at sorterer efter vejnavn
        private bool NameASC = true;

        //Metode til at sorterer sommerhuse efter vejnavn
        public List<SummerHouse> SortStreetName()
        {
            //Laver en liste som får alt info
            List<SummerHouse> retSummerHouses = GetAll();

            //Sætter op så vi kan sorterer x y sommerhus og de compare sig til hinanden
            retSummerHouses.Sort((x, y) => x.StreetName.CompareTo(y.StreetName));

            //Hvis booleanen ikke er true så reverse sorteringen
            if (!NameASC)
            {
                retSummerHouses.Reverse();
            }
            //Gør boolean skifter hver gang metoden bliver kaldt
            NameASC = !NameASC;

            //Returnerer listen som er sorteret
            return retSummerHouses;
        }

        //Vores icomparer sortbystreetname metode
        private class SortByStreetName : IComparer<SummerHouse>
        {
            //Sætter de sommerhuse op med en int hvor de kan se hinanden
            public int Compare(SummerHouse? x, SummerHouse? y)
            {
                //Hvis x eller y er null så returnerer vi ikke noget
                if (x == null || y == null)
                {
                    return 0;
                }

                //Returerer hvor vi comparer x vejnavn med y vejnavn
                return x.StreetName.CompareTo(y.StreetName);
            }
        }

        //sortering efter postnummer metode
        public List<SummerHouse> SortPostalCode()
        {
            //Laver en liste som henter alt info på sommerhsue
            List<SummerHouse> retSummerHouses = GetAll();

            //Kalder sorterings metode sortbypostalcode
            retSummerHouses.Sort(new SortByPostalCode());

            //Hvis boolean er false så reverse listen
            if (!NumberASC)
            {
                retSummerHouses.Reverse();
            }
            //Skift boolean værdier hver gang metoden bliver kaldt
            NumberASC = !NumberASC;

            //returnerer liste med sommerhuse sorteret
            return retSummerHouses;
        }

        //Vores icomparer sortbypostalcode metode
        private class SortByPostalCode : IComparer<SummerHouse>
        {
            //compare sommerhus x og sommerhus y
            public int Compare(SummerHouse? x, SummerHouse? y)
            {
                //Hvis en er null så returner ikke noget
                if (x == null || y == null)
                {
                    return 0;
                }
                //returner sommerhus x og y sorteret
                return x.PostalCode - y.PostalCode;
            }
        }

        //Metode til at sorter efter antal gæster
        public List<SummerHouse> SortAmountSleepingSpace()
        {
            //Laver en liste som får alt info på sommerhuse
            List<SummerHouse> retSummerHouses = GetAll();

            //Kalder sorterings metode sortbyamountsleepingspace for at sorterer sommerhuse
            retSummerHouses.Sort(new SortByAmountSleepingSpace());

            //Hvis Boolean er false reverse sorteringen
            if (!NumberASC)
            {
                retSummerHouses.Reverse();
            }
            //Skifter boolean værdier hver gang metode bliver kaldt
            NumberASC = !NumberASC;

            //Returner sorteret liste
            return retSummerHouses;
        }

        //Vores icomparer sortbyamountsleepingspace metode
        private class SortByAmountSleepingSpace: IComparer<SummerHouse>
        {
            //Compare sommerhus x og sommerhus y
            public int Compare(SummerHouse? x, SummerHouse? y)
            {
                //Hvis en af sommerhusene er null, så returnerer vi intet
                if (x == null || y == null)
                {
                    return 0;
                }

                //Ellers bliver sommerhusene sorteret 
                return x.AmountSleepingSpace - y.AmountSleepingSpace;
            }
        }

        //Vores metode til at sorterer sommerhuse efter pris
        public List<SummerHouse> SortPrice()
        {
            //Laver liste af sommerhuse som får alt info
            List<SummerHouse> retSummerHouses = GetAll();

            //Kalder sort metode sortbyprice på alle sommerhuse i listen
            retSummerHouses.Sort(new SortByPrice());

            //Hvis booleanen er false så reverse listen
            if (!NumberASC)
            {
                retSummerHouses.Reverse();
            }
            //Hver gang metode bliver kaldt så skifter boolean værdi
            NumberASC = !NumberASC;

            //returnerer liste med sorteret sommerhuse
            return retSummerHouses;
        }

        //Vores icomparer sortbyprice metode
        private class SortByPrice : IComparer<SummerHouse>
        {
            //Compare sommerhus x og sommerhus y
            public int Compare(SummerHouse? x, SummerHouse? y)
            {
                //Hvis x og y er null så returnerer vi intet
                if (x == null && y == null)
                {
                    return 0;
                }
                //Hvis x kun er null, så returnerer vi -1
                if (x == null)
                {
                    return -1; 
                }
                //Hvis y kun er null så returnerer vi 1
                if (y == null)
                {
                    return 1; 
                }

                //Returnerer at x pris og y pris er compared
                return x.Price.CompareTo(y.Price);
            }

        }


    }
}
