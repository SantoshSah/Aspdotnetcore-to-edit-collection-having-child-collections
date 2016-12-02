using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SampleWebApi.Models
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            : base(options)
        { }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Address { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Address> Addresses { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}