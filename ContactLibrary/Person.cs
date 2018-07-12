using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class Person
    {
        public Person()
        {
            address = new Address();
            phone = new Phone();
        }
        public long Pid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Address address { get; set; }
        public Phone phone { get; set; }
    }

    public class Address
    {
        public long Pid { get; set; }
        public string houseNum { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public string zipcode { get; set; }
    }

    public class Phone
    {
        public long Pid { get; set; }
        public Country countrycode { get; set; }
        public string areaCode { get; set; }
        public string number { get; set; }
        public string ext { get; set; }
    }

}
