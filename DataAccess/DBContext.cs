using System;
using System.Data.Entity;

namespace DataAccess
{
    public class Context1 : DbContext
    {
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<PhoneModel> Phones { get; set; }
        public DbSet<PersonModel> Persons { get; set; }
    }
}
