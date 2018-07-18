using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace ContactLibrary
{
    public class ContactDataIO
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);
        private const string contactFile = "contacts.dat";

        // Read contacts file and return as List<Person>
        public static List<Person> GetContacts(string fileName = contactFile)
        {
            try
            {
                string contactsSerialized = ReadContactListFromFile(fileName);
                List<Person> p = new List<Person>(JSONToPersonList(contactsSerialized));
                return p;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return null;
            }
        }

        // Write contacts to file
        public static bool WriteContacts(List<Person> p, string fileName = contactFile)
        {
            try
            {
                string contactsSerialized = PersonListToJSON(p);
                if (contactsSerialized == null)
                    throw new Exception($"ContactDataIO.cs\nWriteContactsToFile (line 39).\nPersonListToJSON(line 53) returned null.");
                return WriteContactListToFile(contactsSerialized, fileName);
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return false;
            }
        }

        // JSON Serializer
        public static string PersonListToJSON(List<Person> contacts)
        {
            string json = "";
            try
            {
                json = JsonConvert.SerializeObject(contacts);
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
            }
            return json;
        }

        // JSON Deserializer
        public static List<Person> JSONToPersonList(string json)
        {
            List<Person> contacts = new List<Person>();
            try
            {
                contacts = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
            }
            return contacts;
        }

        // File Write
        public static bool WriteContactListToFile(string contactsSerialized, string fileName = contactFile)
        {
            try
            {
                // Overwrite file with updated contact list
                using (StreamWriter file = new StreamWriter(@fileName))
                {
                    file.Write(contactsSerialized);
                }
                return true;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return false;
            }
        }

        // File Read
        public static string ReadContactListFromFile(string fileName = contactFile)
        {
            try
            {
                // Get serialized contact list from file
                string contactsSerialized = File.ReadAllText(@fileName);
                return contactsSerialized;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return null;
            }
        }
    }
}
