using Xunit;
using ContactLibrary;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace xUnitTest
{

    public class ContactDataTest
    {
        private readonly ITestOutputHelper output;
        public static string testFileName = "test-contacts.dat";
        public static List<Person> testContacts = new List<Person>();

        public ContactDataTest(ITestOutputHelper output)
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
        string[] zipcodes = { "43564", "12o23", "34567" };
        string[] areaCodes = { "321", "654", "987" };
        string[] numbers = { "123456", "234567", "6948747" };
        string[] exts = { "001", "002", "003" };

        [Fact]
        public void TestAddUpdateDeleteSearch()
        {
            //
            // Add three people to contacts
            for (int i = 0; i < firstnames.Length; i++)
            {
                ContactDataAccess.Add(firstName: firstnames[i],
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

            // Debug output for Add
            output.WriteLine($"Contacts after Add:");
            foreach (Person p in ContactDataAccess.contacts)
                output.WriteLine(p.Print());

            // Assert 3 Person were added
            Assert.Equal(3, ContactDataAccess.contacts.Count);

            //
            // Assert Edit Mike Powell
            Assert.True(ContactDataAccess.Update(firstName: "Mike",
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

            // Debug output for Update
            output.WriteLine($"\nContacts after Update 'Mike Powell':");
            foreach (Person p in ContactDataAccess.contacts)
                output.WriteLine(p.Print());

            //
            // Delete Kate Sanders from contacts
            ContactDataAccess.Delete(firstName: "Kate", lastName: "Sanders");

            // Debug output for Delete
            output.WriteLine($"\nContacts after Deleting Kate Powell:");
            foreach (Person p in ContactDataAccess.contacts)
                output.WriteLine(p.Print());

            // Assert Delete
            Assert.Equal(2, ContactDataAccess.contacts.Count);

            //
            // Search Test
            List<Person> results = new List<Person>();
            // Queries to test first name, last name, city, zip code, and phone number
            string[] queries = new string[] { "Mike", "Smith", "Miami", "43564", "6948747" };
            foreach (string q in queries)
            {
                // Search for query string
                results = ContactDataAccess.Search(q);
                // Debug output for Query
                output.WriteLine($"\nSearch results with query '{q}':");
                foreach (Person p in results)
                    output.WriteLine(p.Print());
                // Assert one return
                Assert.Single(results);
            }

            //
            // Serialization Test
            string json = ContactDataIO.PersonListToJSON(ContactDataAccess.contacts);
            testContacts = ContactDataIO.JSONToPersonList(json);

            // Compare List<Person>s
            string listAsStringPreConversions = "", listAsStringPostConversions = "";
            for (int i = 0; i < testContacts.Count; i++)
            {
                listAsStringPreConversions += ContactDataAccess.contacts[i].Print();
                listAsStringPostConversions += testContacts[i].Print();
            }

            // Debug output for serialization
            output.WriteLine($"\nSerialization Test");
            output.WriteLine($"\nlistAsStringPreConversions = \n{listAsStringPreConversions}");
            output.WriteLine($"\nlistAsStringPostConversions = \n{listAsStringPostConversions}");

            // Serialization Assert
            Assert.True(listAsStringPreConversions == listAsStringPostConversions);

            //
            // File IO Test
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
            listAsStringPreConversions = listAsStringPostConversions = "";
            for (int i = 0; i < testContacts.Count; i++)
            {
                listAsStringPreConversions += ContactDataAccess.contacts[i].Print();
                listAsStringPostConversions += testContacts[i].Print();
            }
            output.WriteLine($"\nFile IO Test");
            output.WriteLine($"\nlistAsStringPreConversions = \n{listAsStringPreConversions}");
            output.WriteLine($"\nlistAsStringPostConversions = \n{listAsStringPostConversions}");

            // File IO Assert
            Assert.True(listAsStringPreConversions == listAsStringPostConversions);
        }
    }
}
