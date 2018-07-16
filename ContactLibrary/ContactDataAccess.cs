using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactLibrary
{
    public static class ContactDataAccess
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);

        public static List<Person> contacts = new List<Person>();

        public static bool Add(string firstName = null,
                               string lastName = null,
                               string houseNum = null,
                               string street = null,
                               string city = null,
                               State state = State.NULL,
                               Country country = Country.NULL,
                               string zipcode = null,
                               string areaCode = null,
                               string number = null,
                               string ext = null)
        {
            try
            {
                // Create Person ID
                long Pid = DateTime.Now.Ticks;
                // Create Phone
                Phone phone = new Phone();
                phone.Pid = Pid;
                phone.CountryCode = country;
                phone.AreaCode = areaCode;
                phone.Number = number;
                phone.Ext = ext;
                // Create Address
                Address addr = new Address();
                addr.Pid = Pid;
                addr.HouseNum = houseNum;
                addr.Street = street;
                addr.City = city;
                addr.State = state;
                addr.Country = country;
                addr.Zipcode = zipcode;
                // Create Person
                Person person = new Person();
                person.Pid = Pid;
                person.Firstname = firstName;
                person.Lastname = lastName;
                person.Address = addr;
                person.Phone = phone;
                // Add new person to contact list
                contacts.Add(person);
                logger.Info($"Created Person: \n{person.Print()}");
                return true;
            }
            catch(Exception e)
            {
                logger.Info(e.Message);
                return false;
            }
        }

        public static bool Update(string firstName = null,
                                  string lastName = null,
                                  string houseNum = null,
                                  string street = null,
                                  string city = null,
                                  State state = State.NULL,
                                  Country country = Country.NULL,
                                  string zipcode = null,
                                  string areaCode = null,
                                  string number = null,
                                  string ext = null)
        {
            try
            {
                // Find the user by first name last name
                var query = (from p in contacts
                             where p.Firstname == firstName
                             && p.Lastname == lastName
                             select p).ToList();
                // Throw error if more than one result
                if (query.Count > 1)
                {
                    Console.WriteLine($"Error, multiple contacts with the name {firstName} {lastName}");
                    return false;
                }
                // Get Person
                Person person = query[0];
                // Edit Phone
                Phone phone = person.Phone;
                if (country != Country.NULL) phone.CountryCode = country;
                if (areaCode != null) phone.AreaCode = areaCode;
                if (number != null) phone.Number = number;
                if (ext != null) phone.Ext = ext;
                // Edit Address
                Address addr = person.Address;
                if (houseNum != null) addr.HouseNum = houseNum;
                if (street != null) addr.Street = street;
                if (city != null) addr.City = city;
                if (state != State.NULL) addr.State = state;
                if (country != Country.NULL) addr.Country = country;

                return true;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return false;
            }
        }

        public static bool Delete(string firstName = null,
                                  string lastName = null)
        {
            try
            {
                // Find the user by first name last name
                var query = (from p in contacts
                             where p.Firstname == firstName
                             && p.Lastname == lastName
                             select p).ToList();
                // Throw error if more than one result
                if (query.Count > 1)
                {
                    Console.WriteLine($"Error, multiple contacts with the name {firstName} {lastName}");
                    return false;
                }
                // Get Person
                Person person = query[0];
                // Remove Person from DB
                contacts.Remove(person);
                return true;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return false;
            }
        }

        public static List<Person> Search(string query)
        {
            // List to return query results
            List<Person> results = new List<Person>();
            // Search firstname, lastname, zipcode, city, and phone number for query            
            results = ( from p in contacts
                        where p.Firstname.Contains(query) ||
                              p.Lastname.Contains(query) ||
                              p.Address.City.Contains(query) ||
                              p.Address.Zipcode.Contains(query) ||
                              p.Phone.Number.Contains(query)
                        select p).ToList();
            // Return query results
            return results;
        }

        /*
        public static List<Person> Search(string firstName = null,
                                          string lastName = null,
                                          string zipcode = null,
                                          string city = null,
                                          string phoneNumber = null)
        {
            // List to return query results
            List<Person> results = new List<Person>();
            // Search by first name
            if (firstName != null)
            {
                results = (from p in contacts
                           where p.Firstname == firstName
                           select p).ToList();
            }
            // Search by last name
            else if (lastName != null)
            {
                results = (from p in contacts
                           where p.Lastname == lastName
                           select p).ToList();
            }
            // Search by zip code
            else if (zipcode != null)
            {
                results = (from p in contacts
                           where p.Address.Zipcode == zipcode
                           select p).ToList();
            }
            // Search by City
            else if (city != null)
            {
                results = (from p in contacts
                           where p.Address.City == city
                           select p).ToList();
            }
            // Search by Phone Number
            else if (phoneNumber != null)
            {
                results = (from p in contacts
                           where p.Phone.Number == phoneNumber
                           select p).ToList();
            }
            // Return query results
            return results;
        }
        */
    }
}
