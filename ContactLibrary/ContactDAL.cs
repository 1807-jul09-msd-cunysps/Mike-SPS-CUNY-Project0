using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactLibrary;

namespace ContactLibrary
{
    public class ContactDAL
    {
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
                phone.countrycode = country;
                phone.areaCode = areaCode;
                phone.number = number;
                phone.ext = ext;
                // Create Address
                Address addr = new Address();
                addr.Pid = Pid;
                addr.houseNum = houseNum;
                addr.street = street;
                addr.city = city;
                addr.State = state;
                addr.Country = country;
                // Create Person
                Person person = new Person();
                person.Pid = Pid;
                person.firstName = firstName;
                person.lastName = lastName;
                person.address = addr;
                person.phone = phone;
                // Add new person to contact list
                contacts.Add(Pid, person);
                return true;
            }
            catch
            {
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
                             where p.firstName == firstName
                             && p.lastName == lastName
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
                Phone phone = person.phone;
                if (country != null) phone.countrycode = country;
                if (areaCode != null) phone.areaCode = areaCode;
                if (number != null) phone.number = number;
                if (ext != null) phone.ext = ext;
                // Edit Address
                Address addr = person.address;
                if (houseNum != null) addr.houseNum = houseNum;
                if (street != null) addr.street = street;
                if (city != null) addr.city = city;
                if (state != null) addr.State = state;
                if (country != null) addr.Country = country;

                return true;
            }
            catch
            {
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
                             where p.firstName == firstName
                             && p.lastName == lastName
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
            catch
            {
                return false;
            }
        }

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
                           where p.firstName == firstName
                           select p).ToList();
            }
            // Search by last name
            else if (lastName != null)
            {
                results = (from p in contacts
                           where p.lastName == lastName
                           select p).ToList();
            }
            // Search by zip code
            else if (zipCode != null)
            {
                results = (from p in contacts
                           where p.address.zipcode == zipcode
                           select p).ToList();
            }
            // Search by city
            else if (city != null)
            {
                results = (from p in contacts
                           where p.address.city == city
                           select p).ToList();
            }
            // Search by phone number
            else if (phoneNumber != null)
            {
                results = (from p in contacts
                           where p.phone.number == phoneNumber
                           select p).ToList();
            }
            // Return query results
            return results;
        }
    }
}
