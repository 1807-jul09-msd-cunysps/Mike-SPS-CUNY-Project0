using System;
using System.Collections.Generic;
using ContactLibrary;

namespace proj0
{
    class Program
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);
        static void Main(string[] args)
        {
            string selection = "";
            // For storing query results
            List<Person> results = new List<Person>();
            // Load contacts from file
            ContactDataAccess.contacts = ContactDataIO.GetContacts();
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
                    ContactDataIO.WriteContacts(ContactDataAccess.contacts);
                    break;
                }
                else if (selection == "1")
                {
                    if (ContactDataAccess.contacts.Count > 0) {
                        Console.WriteLine($"\n{ContactDataAccess.contacts.Count} Contacts\n");
                        foreach (Person p in ContactDataAccess.contacts)
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
                        ContactDataAccess.Add(firstName: fn,
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
                        Console.WriteLine($"\nFailed to add contact.\n");
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
                        ContactDataAccess.Update(firstName: fn,
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
                        Console.WriteLine($"\nFailed to update contact.\n");
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
                        ContactDataAccess.Delete(firstName: fn,
                                              lastName: ln);
                        Console.WriteLine($"\nContact successfully deleted.\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nFailed to update contact.\n");
                    }
                }
                else if (selection == "5")
                {
                    Console.WriteLine($"Query string: ");
                    string query = Console.ReadLine() ?? "";
                    results = ContactDataAccess.Search(query);

                    if (results.Count > 0)
                    {
                        Console.WriteLine($"\n{results.Count} Contacts in query \"{query}\"\n");
                        foreach (Person p in results)
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
