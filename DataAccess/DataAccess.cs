using System;
using System.Collections.Generic;
using Models;
using System.Data;              // ADO.NET lib
using System.Data.SqlClient;    // Client in ADO.NET library

namespace DataAccessADOSQL
{
    public static class DBAccess
    {
        private static string connectionString = "Data Source=rev-training-mc-dbs.database.windows.net;" +   // SQL Server
                                                 "Initial Catalog=rev-training-mc-contacts-db;" +            // SQL DB
                                                 "Persist Security Info=True;" +                             // Security
                                                 "MultipleActiveResultSets=True;" +                          // MARS
                                                 "User ID=revature;" +                                       // User name
                                                 "Password=Password1";                                       // Password

        public static void InitTables()
        {
            string[] tables = new string[] {
                    "CREATE TABLE person(" +
                                          "id INT PRIMARY KEY IDENTITY(1,1), " +
                                          "firstname VARCHAR(35), " +
                                          "lastname VARCHAR(35));",
                    "CREATE TABLE address(" +
                                          "id INT PRIMARY KEY IDENTITY(1,1), " +
                                          "personID INT FOREIGN KEY REFERENCES person(id)," +
                                          "housenum VARCHAR(25), " +
                                          "street VARCHAR(25), " +
                                          "city VARCHAR(25), " +
                                          "state VARCHAR(4), " +
                                          "country VARCHAR(25), " +
                                          "zipcode VARCHAR(5));",
                    "CREATE TABLE phone(" +
                                          "id INT PRIMARY KEY IDENTITY(1,1), " +
                                          "personID INT FOREIGN KEY REFERENCES person(id)," +
                                          "country VARCHAR(25), " +
                                          "areacode VARCHAR(3)," +
                                          "number VARCHAR(7)," +
                                          "ext VARCHAR(5));"
            };
            // SQL connection object
            SqlConnection connection = null;
            try
            {
                // Try and create all tables
                foreach (string s in tables)
                {
                    connection = new SqlConnection(connectionString);               // Define connection
                    connection.Open();                                              // Open connection
                    try
                    {
                        SqlCommand command = new SqlCommand(s, connection);         // Define command
                        command.ExecuteNonQuery();                                  // Send command
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"InitTables(): Failed to create table with: '{s}'");
                        Console.WriteLine($"\n{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void Add(PersonModel person)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command
                command.Transaction = transaction;                          // Assign transaction to command

                try
                {
                    // INSERT for person
                    command.CommandText = $"INSERT INTO person VALUES (" +  
                                          $"'{person.Firstname}', " +
                                          $"'{person.Lastname}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Retrieve Person ID for person we just submitted
                    int pid;
                    command.CommandText = "SELECT SCOPE_IDENTITY()";        
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        pid = Int32.Parse(reader[0].ToString());
                    }
                    else
                    {
                        throw new Exception("Failed to ExecuteReader");
                    }
                    reader.Close();
                    
                    // INSERT for phone
                    command.CommandText = $"INSERT INTO phone VALUES (" +
                                          $"{pid}," +
                                          $"'{person.Phone.CountryCode}'," +
                                          $"'{person.Phone.AreaCode}'," +
                                          $"'{person.Phone.Number}'," +
                                          $"'{person.Phone.Ext}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // INSERT for address
                    command.CommandText = "INSERT INTO address VALUES (" +  
                                          $"{pid}, " +
                                          $"'{person.Address.HouseNum}', " +
                                          $"'{person.Address.Street}', " +
                                          $"'{person.Address.City}', " +
                                          $"'{person.Address.State}', " +
                                          $"'{person.Address.Country}', " +
                                          $"'{person.Address.Zipcode}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        // Roll back if exception
                        transaction.Rollback();                             
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        public static void Update(PersonModel oldInfo, PersonModel newInfo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command
                command.Transaction = transaction;                          // Assign transaction to command

                try
                {
                    // INSERT for person
                    command.CommandText = $"INSERT INTO person VALUES (" +
                                          $"'{newInfo.Firstname}', " +
                                          $"'{newInfo.Lastname}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Retrieve Person ID for person we just submitted
                    int pid;
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        pid = Int32.Parse(reader[0].ToString());
                    }
                    else
                    {
                        throw new Exception("Failed to ExecuteReader");
                    }
                    reader.Close();

                    // INSERT for phone
                    command.CommandText = $"INSERT INTO phone VALUES (" +
                                          $"{pid}," +
                                          $"'{newInfo.Phone.CountryCode}'," +
                                          $"'{newInfo.Phone.AreaCode}'," +
                                          $"'{newInfo.Phone.Number}'," +
                                          $"'{newInfo.Phone.Ext}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // INSERT for address
                    command.CommandText = "INSERT INTO address VALUES (" +
                                          $"{pid}, " +
                                          $"'{newInfo.Address.HouseNum}', " +
                                          $"'{newInfo.Address.Street}', " +
                                          $"'{newInfo.Address.City}', " +
                                          $"'{newInfo.Address.State}', " +
                                          $"'{newInfo.Address.Country}', " +
                                          $"'{newInfo.Address.Zipcode}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        // Roll back if exception
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        public static void Delete(PersonModel person)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlTransaction transaction = connection.BeginTransaction(); // Create transaction
                SqlCommand command = connection.CreateCommand();            // Create command
                command.Transaction = transaction;                          // Assign transaction to command

                try
                {
                    // INSERT for person
                    command.CommandText = $"INSERT INTO person VALUES (" +
                                          $"'{person.Firstname}', " +
                                          $"'{person.Lastname}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Retrieve Person ID for person we just submitted
                    int pid;
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        pid = Int32.Parse(reader[0].ToString());
                    }
                    else
                    {
                        throw new Exception("Failed to ExecuteReader");
                    }
                    reader.Close();

                    // INSERT for phone
                    command.CommandText = $"INSERT INTO phone VALUES (" +
                                          $"{pid}," +
                                          $"'{person.Phone.CountryCode}'," +
                                          $"'{person.Phone.AreaCode}'," +
                                          $"'{person.Phone.Number}'," +
                                          $"'{person.Phone.Ext}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // INSERT for address
                    command.CommandText = "INSERT INTO address VALUES (" +
                                          $"{pid}, " +
                                          $"'{person.Address.HouseNum}', " +
                                          $"'{person.Address.Street}', " +
                                          $"'{person.Address.City}', " +
                                          $"'{person.Address.State}', " +
                                          $"'{person.Address.Country}', " +
                                          $"'{person.Address.Zipcode}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        // Roll back if exception
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        public static List<PersonModel> Search(string s)
        {
            string query = s.ToLower();
            // List to return query results
            List<PersonModel> results = new List<PersonModel>();
            // SQL interaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                                          // Open connection
                SqlCommand command = connection.CreateCommand();            // Create command

                try
                {
                    // Search firstname, lastname, zipcode, city, and phone number for query            
                    command.CommandText = $"SELECT * FROM person " +
                                          $"LEFT JOIN phone ON person.ID = phone.personID " +
                                          $"LEFT JOIN address ON person.ID = address.personID " +
                                          $"WHERE LOWER(person.firstname) = '{query}'" +
                                          $";";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PersonModel p = new PersonModel()
                        {
                            Id = Int32.Parse(reader[0].ToString()),
                            Firstname = reader[1].ToString(),
                            Lastname = reader[2].ToString(),
                            Phone = new PhoneModel
                            {
                                Id = Int32.Parse(reader[3].ToString()),
                                PersonId = Int32.Parse(reader[4].ToString()),
                                CountryCode = (Country) Enum.Parse(typeof(Country), reader[5].ToString()),
                                AreaCode = reader[6].ToString(),
                                Number = reader[7].ToString(),
                                Ext = reader[8].ToString()
                            },
                            Address = new AddressModel
                            {
                                Id = Int32.Parse(reader[9].ToString()),
                                PersonId = Int32.Parse(reader[10].ToString()),
                                HouseNum = reader[11].ToString(),
                                Street = reader[12].ToString(),
                                City = reader[13].ToString(),
                                State = (State)Enum.Parse(typeof(State), reader[14].ToString()),
                                Country = (Country)Enum.Parse(typeof(Country), reader[15].ToString()),
                                Zipcode = reader[16].ToString()
                            }
                        };

                        Console.WriteLine(p.Print());
                    }

                    //DataTable schema = reader.GetSchemaTable();
                    //foreach (DataRow row in schema.Rows)
                    //{
                    //    Console.WriteLine($"");
                    //    foreach (DataColumn col in schema.Columns)
                    //    {
                    //        Console.Write($"{row[col].ToString()}");
                    //    }
                    //}

                    //if (reader.HasRows)
                    //{
                    //    while (reader.Read())
                    //    {
                    //        var t = reader.GetValues();
                    //    }
                    //}
                    //else
                    //{
                    //    throw new Exception("Failed to ExecuteReader");
                    //}
                    reader.Close();
                    //results = (from p in contacts
                    //           where p.Firstname.Contains(query) ||
                    //                 p.Lastname.Contains(query) ||
                    //                 p.Address.City.Contains(query) ||
                    //                 p.Address.Zipcode.Contains(query) ||
                    //                 p.Phone.Number.Contains(query)
                    //           select p).ToList();
                    // Return query results
                    /*
                    // INSERT for person
                    command.CommandText = $"INSERT INTO person VALUES (" +
                                          $"'{person.Firstname}', " +
                                          $"'{person.Lastname}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // Retrieve Person ID for person we just submitted
                    int pid;
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        pid = Int32.Parse(reader[0].ToString());
                    }
                    else
                    {
                        throw new Exception("Failed to ExecuteReader");
                    }
                    reader.Close();

                    // INSERT for phone
                    command.CommandText = $"INSERT INTO phone VALUES (" +
                                          $"{pid}," +
                                          $"'{person.Phone.CountryCode}'," +
                                          $"'{person.Phone.AreaCode}'," +
                                          $"'{person.Phone.Number}'," +
                                          $"'{person.Phone.Ext}'" +
                                          ");";
                    command.ExecuteNonQuery();

                    // INSERT for address
                    command.CommandText = "INSERT INTO address VALUES (" +
                                          $"{pid}, " +
                                          $"'{person.Address.HouseNum}', " +
                                          $"'{person.Address.Street}', " +
                                          $"'{person.Address.City}', " +
                                          $"'{person.Address.State}', " +
                                          $"'{person.Address.Country}', " +
                                          $"'{person.Address.Zipcode}'" +
                                          ");";
                    command.ExecuteNonQuery();
                    */
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        // Roll back if exception
                        
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
            return results;
        }
    }
}
