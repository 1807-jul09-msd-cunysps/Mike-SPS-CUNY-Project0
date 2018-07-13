using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ContactLibrary
{
    class ContactDataIO
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();     //logger.Info(e.Message);
        private static string contactFile = "contacts.bin";

        // Read contacts file and return as List<Person>
        public static List<Person> GetContacts()
        {
            try
            {
                List<Person> p = new List<Person>();
                string contactsSerialized = ReadContactListFromFile();
                p = JSONToPersonList(contactsSerialized);
                return p;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return null;
            }
        }

        // Write contacts to file
        public static bool WriteContacts(List<Person> p)
        {
            try
            {
                string contactsSerialized = PersonListToJSON(p);
                if (contactsSerialized == null)
                    throw new Exception($"ContactDataIO.cs\nWriteContactsToFile (line 39).\nPersonListToJSON(line 53) returned null.");
                return WriteContactListToFile(contactsSerialized);
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
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
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
                DataContractJsonSerializer ser = new DataContractJsonSerializer(p.GetType());
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
        public static bool WriteContactListToFile(string contactsSerialized)
        {
            try
            {
                // Overwrite file with updated contact list
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@contactFile))
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
        public static string ReadContactListFromFile()
        {
            try
            {
                // Get serialized contact list from file
                string contactsSerialized = System.IO.File.ReadAllText(@contactFile);

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
