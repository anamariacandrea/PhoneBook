using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Data
{
    public class PhoneBookContext:DbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext>options): base(options)
        { }
        public DbSet<Person> People { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Details> Details { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<PhoneNumber>().ToTable("PhoneNumber");
            modelBuilder.Entity<Mail>().ToTable("E-Mail Addres");
            modelBuilder.Entity<Details>().ToTable("Person Details");
        }
    }
}
