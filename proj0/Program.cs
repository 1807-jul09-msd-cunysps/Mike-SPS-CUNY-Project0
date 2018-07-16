using System;
using ContactLibrary;

namespace proj0
{
    class Program
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);
        static void Main(string[] args)
        {
            string selection = "";
            while (true)
            {
                Console.WriteLine($"\nPhone Directory" +
                                  $"\n\t0: Exit" +
                                  $"\n\t1: View Contacts" +
                                  $"\n\t2: Add Contact" +
                                  $"\n\t3: Edit Contact" +
                                  $"\n\t4: Delete Contact" +
                                  $"\n\t5: Search Contacts");
                selection = Console.ReadLine();
                if (selection == "0")
                {
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
                        Console.WriteLine($"Contact successfully added.");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Failed to add contact.");
                    }
                }
                else if (selection == "3")
                {

                }
                else if (selection == "4")
                {

                }
                else if (selection == "5")
                {

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
