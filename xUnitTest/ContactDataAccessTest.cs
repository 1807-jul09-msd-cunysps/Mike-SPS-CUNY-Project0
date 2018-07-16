using Xunit;
using ContactLibrary;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace xUnitTest
{
    /*
     *  All of tests must be run together; they rely on the previous functions' modifications to the dataset
     */

    public class ContactDataAccessTest
    {
        private readonly ITestOutputHelper output;
        public static string testFileName = "test-contacts.dat";
        public static List<Person> testContacts = new List<Person>();

        public ContactDataAccessTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        string[] firstnames = { "Mike", "Kate", "Sally" };
        string[] lastNames = { "Powell", "Sanders", "Smith" };
        string[] houseNums = { "123", "456", "789" };
        string[] streets = { "Main", "Broad", "Madison" };
        string[] cities = { "Miami", "Chesapeake", "Columbus" };
        State[] states = { State.FL, State.MD, State.OH };
        Country[] countries = { Country.Australia, Country.India, Country.Pakistan };
        string[] zipcodes = { "12345", "43564", "34567" };
        string[] areaCodes = { "321", "654", "987" };
        string[] numbers = { "123456", "234567", "6948747" };
        string[] exts = { "001", "002", "003" };

        [Fact]
        public void TestAdd()
        {
            // Add three people to contacts
            for (int i = 0; i < firstnames.Length; i++)
            {
                ContactDataAccess.Add( firstName: firstnames[i],
                                       lastName: lastNames[i],
                                       houseNum: houseNums[i],
                                       street: streets[i],
                                       city: cities[i],
                                       state: states[i],
                                       country: countries[i],
                                       zipcode: zipcodes[i],
                                       areaCode: areaCodes[i],
                                       number: numbers[i],
                                       ext: exts[i]);
            }
            output.WriteLine($"Contacts after TestAdd:");
            foreach (Person p in ContactDataAccess.contacts)
            {
                output.WriteLine(p.Print());
            }
            Assert.Equal(3, ContactDataAccess.contacts.Count);
        }

        [Fact]
        public void TestDelete()
        {
            // Delete Kate Sanders from contacts
            ContactDataAccess.Delete(firstName: "Kate", lastName: "Sanders");
            output.WriteLine($"Contacts after TestDelete:");
            foreach (Person p in ContactDataAccess.contacts)
            {
                output.WriteLine(p.Print());
            }
            Assert.Equal(2, ContactDataAccess.contacts.Count);
        }

        [Fact]
        public void TestUpdate()
        {
            // Edit Mike Powell
            Assert.True( ContactDataAccess.Update(firstName: "Mike",
                                           lastName: "Powell",
                                           houseNum: null,
                                           street: "999",
                                           city: null,
                                           state: ContactLibrary.State.NULL,
                                           country: ContactLibrary.Country.UK,
                                           zipcode: "99999",
                                           areaCode: "999",
                                           number: "9999999",
                                           ext: null));

            output.WriteLine($"Contacts after TestUpdate:");
            foreach (Person p in ContactDataAccess.contacts)
            {
                output.WriteLine(p.Print());
            }
        }

        [Theory]
        [InlineData("Mike")]        // First name
        [InlineData("Sanders")]     // Last name
        [InlineData("Chesapeake")]  // City
        [InlineData("43564")]       // Zipcode
        [InlineData("6948747")]     // Phone number
        public void TestSearch(string query)
        {
            List<Person> results = new List<Person>();
            results = ContactDataAccess.Search(query);
            output.WriteLine($"TestSearch results with query '{query}':");
            foreach (Person p in results)
            {
                output.WriteLine(p.Print());
            }
            Assert.Single(results);
        }

        [Fact]
        public void TestSerializationJSON()
        {
            string json = ContactDataIO.PersonListToJSON(ContactDataAccess.contacts);
            testContacts = ContactDataIO.JSONToPersonList(json);

            // Compare List<Person>s
            string listAsStringPreConversions = "", listAsStringPostConversions = "";
            for (int i = 0; i < testContacts.Count; i++)
            {
                listAsStringPreConversions += ContactDataAccess.contacts[i].Print();
                listAsStringPostConversions += testContacts[i].Print();
            }

            Assert.True(listAsStringPreConversions == listAsStringPostConversions);
        }

        [Fact]
        public void TestFileIO()
        {
            bool writeTest = ContactDataIO.WriteContacts(ContactDataAccess.contacts, testFileName);
            testContacts = ContactDataIO.GetContacts(testFileName);

            output.WriteLine($"ContactDataAccess.contacts:");
            foreach (Person p in ContactDataAccess.contacts)
            {
                output.WriteLine(p.Print());
            }

            output.WriteLine($"\ntestContacts:");
            foreach (Person p in testContacts)
            {
                output.WriteLine(p.Print());
            }
            

            // Compare List<Person>s
            string listAsStringPreConversions = "", listAsStringPostConversions = "";
            for (int i = 0; i < testContacts.Count; i++)
            {
                listAsStringPreConversions += ContactDataAccess.contacts[i].Print();
                listAsStringPostConversions += testContacts[i].Print();
            }

            Assert.True(listAsStringPreConversions == listAsStringPostConversions);
        }
    }
}
