using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class PersonModel
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public AddressModel Address { get; set; }
        [DataMember]
        public PhoneModel Phone { get; set; }

        public PersonModel()
        {
            Address = new AddressModel();
            Phone = new PhoneModel();
        }

        public string Print()
        {
            return $"\n{Firstname} {Lastname}\n{Address.Print()}\n{Phone.Print()}";
        }
    }
}
