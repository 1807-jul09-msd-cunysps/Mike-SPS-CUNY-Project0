using System.Runtime.Serialization;

namespace ContactLibrary
{
    [DataContract]
    public enum State
    {
        NULL, NY, FL, VA, MD, SF, OH
    }

    [DataContract]
    public enum Country
    {
        US = 1, UK = 44, India = 91, Pakistan = 92, Australia = 61, NULL = -1
    }

    [DataContract]
    public class Address
    {
        [DataMember]
        public long Pid { get; set; }
        [DataMember]
        public string HouseNum { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public State State { get; set; }
        [DataMember]
        public Country Country { get; set; }
        [DataMember]
        public string Zipcode { get; set; }
    }

    [DataContract]
    public class Phone
    {
        [DataMember]
        public long Pid { get; set; }
        [DataMember]
        public Country CountryCode { get; set; }
        [DataMember]
        public string AreaCode { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Exp { get; set; }
    }

    [DataContract]
    public class Person
    {
        public Person()
        {
            Address = new Address();
            Phone = new Phone();
        }

        [DataMember]
        public long Pid { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public Address Address { get; set; }
        [DataMember]
        public Phone Phone { get; set; }
    }
}
