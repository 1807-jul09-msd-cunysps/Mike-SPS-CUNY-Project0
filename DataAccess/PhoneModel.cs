using System.Runtime.Serialization;

namespace DataAccessADOSQL
{
    [DataContract]
    public class PhoneModel
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
        public string Ext { get; set; }

        public string Print()
        {
            return $"{AreaCode} {Number}: {Ext}";
        }
    }
}
