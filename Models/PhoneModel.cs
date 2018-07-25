using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class PhoneModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PersonId { get; set; }
        [DataMember]
        public Country CountryCode { get; set; }
        [DataMember]
        public string AreaCode { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Ext { get; set; }

        public string Print()
        {
            return $"{AreaCode} {Number}: {Ext}";
        }
    }
}
