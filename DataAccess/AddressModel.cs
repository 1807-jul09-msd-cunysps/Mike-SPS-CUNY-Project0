using System.Runtime.Serialization;

namespace DataAccessADOSQL
{
    [DataContract]
    public enum State
    {
        NULL, NY, FL, VA, MD, SF, OH
    }

    [DataContract]
    public enum Country
    {
        NULL = -1, US = 1, UK = 44, India = 91, Pakistan = 92, Australia = 61
    }

    [DataContract]
    public class AddressModel
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

        public string Print()
        {
            return $"{HouseNum} {Street}\n{City}, {State} {Zipcode}";
        }
    }
}
