using System;
using System.Collections.Generic;
using Models;
using DataAccess;

namespace proj0
{
    class Program
    {
        public static PersonModel MakePerson()
        {

            PersonModel person = new PersonModel
            {
                Firstname = "Sal",
                Lastname = "Corso",
                Address = new AddressModel
                {
                    HouseNum = "7",
                    Street = "East 14th St",
                    City = "New York",
                    State = Models.State.NY,
                    Country = Models.Country.US,
                    Zipcode = "10003"
                },
                Phone = new PhoneModel
                {
                    CountryCode = Models.Country.US,
                    AreaCode = "718",
                    Number = "8130773",
                    Ext = ""
                }
            };

            return person;
        }

        public static PersonModel UpdatePerson()
        {

            PersonModel person = new PersonModel
            {
                Id = 5,
                Firstname = "Jonathan",
                Lastname = "Corso",
                Address = new AddressModel
                {
                    PersonId = 5,
                    HouseNum = "7",
                    Street = "NEW STREET BREH",
                    City = "New York",
                    State = Models.State.NY,
                    Country = Models.Country.US,
                    Zipcode = "10003"
                },
                Phone = new PhoneModel
                {
                    PersonId = 5,
                    CountryCode = Models.Country.US,
                    AreaCode = "718",
                    Number = "1234567",
                    Ext = ""
                }
            };

            return person;
        }

        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);
        static void Main(string[] args)
        {
            //DBAccess.WipeItAll();
            string selection = "";
            // For storing query results
            List<PersonModel> results = new List<PersonModel>();
            // Load contacts from file
            MemDbAccess.contacts = FileDbAccess.GetContacts();
            while (true)
            {
                Console.WriteLine($"\nPhone Directory" +
                                  $"\n\t0: Save and Exit" +
                                  $"\n\t1: View Contacts" +
                                  $"\n\t2: Add Contact" +
                                  $"\n\t3: Edit Contact" +
                                  $"\n\t4: Delete Contact" +
                                  $"\n\t5: Search Contacts");
                selection = Console.ReadLine();
                if (selection == "0")
                {
                    FileDbAccess.WriteContacts(MemDbAccess.contacts);
                    break;
                }
                else if (selection == "1")
                {
                    if (MemDbAccess.contacts == null)
                    {
                        Console.WriteLine($"\nNo entries in Contacts.");
                    }
                    else if (MemDbAccess.contacts.Count > 0) {
                        Console.WriteLine($"\n{MemDbAccess.contacts.Count} Contacts\n");
                        foreach (PersonModel p in MemDbAccess.contacts)
                        {
                            Console.WriteLine(p.Print());
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nNo entries in Contacts.");
                    }
                }
                else if (selection == "2")
                {
                    Console.WriteLine($"First Name: ");
                    string fn = Console.ReadLine() ?? "";
                    Console.WriteLine($"Last Name: ");
                    string ln = Console.ReadLine() ?? "";
                    Console.WriteLine($"House Number: ");
                    string houseNum = Console.ReadLine() ?? "";
                    Console.WriteLine($"Street: ");
                    string street = Console.ReadLine() ?? "";
                    Console.WriteLine($"City: ");
                    string city = Console.ReadLine() ?? "";
                    State state = State.NULL;
                    Country country = Country.NULL;
                    Console.WriteLine($"Zip Code: ");
                    string zip = Console.ReadLine() ?? "";
                    Console.WriteLine($"Area Code: ");
                    string areacode = Console.ReadLine() ?? "";
                    Console.WriteLine($"Phone Number: ");
                    string num = Console.ReadLine() ?? "";
                    Console.WriteLine($"Extension: ");
                    string ext = Console.ReadLine() ?? "";

                    try
                    {
                        MemDbAccess.Add(firstName: fn,
                                              lastName: ln,
                                              houseNum: houseNum,
                                              street: street,
                                              city: city,
                                              state: state,
                                              country: country,
                                              zipcode: zip,
                                              areaCode: areacode,
                                              number: num,
                                              ext: ext);
                        Console.WriteLine($"\nContact successfully added.\n");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"\nFailed to add contact.\n{ex.Message}");
                    }
                }
                else if (selection == "3")
                {
                    Console.WriteLine($"Existing contact's first name: ");
                    string fn = Console.ReadLine() ?? "";
                    Console.WriteLine($"Existing contact's last name: ");
                    string ln = Console.ReadLine() ?? "";
                    Console.WriteLine($"New House Number: ");
                    string houseNum = Console.ReadLine() ?? "";
                    Console.WriteLine($"New Street: ");
                    string street = Console.ReadLine() ?? "";
                    Console.WriteLine($"New City: ");
                    string city = Console.ReadLine() ?? "";
                    State state = State.NULL;
                    Country country = Country.NULL;
                    Console.WriteLine($"New Zip Code: ");
                    string zip = Console.ReadLine() ?? "";
                    Console.WriteLine($"New Area Code: ");
                    string areacode = Console.ReadLine() ?? "";
                    Console.WriteLine($"New Phone Number: ");
                    string num = Console.ReadLine() ?? "";
                    Console.WriteLine($"New Extension: ");
                    string ext = Console.ReadLine() ?? "";

                    try
                    {
                        MemDbAccess.Update(firstName: fn,
                                              lastName: ln,
                                              houseNum: houseNum,
                                              street: street,
                                              city: city,
                                              state: state,
                                              country: country,
                                              zipcode: zip,
                                              areaCode: areacode,
                                              number: num,
                                              ext: ext);
                        Console.WriteLine($"\nContact successfully updated.\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nFailed to update contact.\n{ex.Message}");
                    }
                }
                else if (selection == "4")
                {
                    Console.WriteLine($"Contact's first name: ");
                    string fn = Console.ReadLine() ?? "";
                    Console.WriteLine($"Contact's last name: ");
                    string ln = Console.ReadLine() ?? "";
                    try
                    {
                        MemDbAccess.Delete(firstName: fn,
                                              lastName: ln);
                        Console.WriteLine($"\nContact successfully deleted.\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nFailed to update contact.\n{ex.Message}");
                    }
                }
                else if (selection == "5")
                {
                    Console.WriteLine($"Query string: ");
                    string query = Console.ReadLine() ?? "";
                    results = MemDbAccess.Search(query);

                    if (results.Count > 0)
                    {
                        Console.WriteLine($"\n{results.Count} Contacts in query \"{query}\"\n");
                        foreach (PersonModel p in results)
                        {
                            Console.WriteLine(p.Print());
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nNo entries in Contacts.");
                    }

                    results.Clear();
                }
                else
                {
                    Console.WriteLine($"\n{selection} is not a valid selection.");
                }
                selection = "";
            } 
        }
    }
}
