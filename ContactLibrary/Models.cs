namespace ContactLibrary
{
    public enum State
    {
        NULL, NY, FL, VA, MD, SF, OH
    }
    public enum Country
    {
        US = 1, UK = 44, India = 91, Pakistan = 92, Australia = 61, NULL = -1
    }

    public class Address
    {
        public long Pid { get; set; }
        public string HouseNum { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public string Zipcode { get; set; }
    }

    public class Phone
    {
        public long Pid { get; set; }
        public Country CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string Number { get; set; }
        public string Exp { get; set; }
    }

    public class Person
    {
        public Person()
        {
            Address = new Address();
            Phone = new Phone();
        }
        public long Pid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
        public Phone Phone { get; set; }
    }
}
