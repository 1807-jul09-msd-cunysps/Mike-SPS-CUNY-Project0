using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

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
        public static string PersonListToJSON(List<Person> contactsAsObjects)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Person>));
                ser.WriteObject(stream, contactsAsObjects);
                byte[] json = stream.ToArray();
                stream.Close();
                return Encoding.UTF8.GetString(json, 0, json.Length);
            }
            catch(Exception e)
            {
                logger.Info(e.Message);
                return null;
            }
        }

        // JSON Deserializer
        public static List<Person> JSONToPersonList(string contactsSerialized)
        {
            try
            {
                List<Person> p = new List<Person>();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(contactsSerialized));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Person>));
                p = ser.ReadObject(ms) as List<Person>;
                ms.Close();
                return p;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return null;
            }
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
