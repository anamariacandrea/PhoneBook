using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBookModel.Models;

namespace PhoneBookModel.Data
{
    public class DbInitializer
    {
        public static void Initialize(PhoneBookContext context)
        {
            context.Database.EnsureCreated();
            if (context.People.Any())
            {
                return;
            }
            var people = new Person[]
            {
                new Person{LastName="Candrea", FirstName="Ana-Maria" },
                new Person{LastName="Pop", FirstName="Florin",PhoneID=255 }
             
                

            };
            foreach (Person p in people)
            {
                context.Add(p);
            }
            context.SaveChanges();
            var phoneNumbers = new PhoneNumber[]
            {
                new PhoneNumber{PhoneNumberID=255,MailID=456,PersonID=1,PhoneNumbers="5262659"},
                new PhoneNumber{PhoneNumberID=755, MailID=666,PersonID=2,PhoneNumbers="65598659"}
            };
            foreach (PhoneNumber pn in phoneNumbers)
            {
                context.PhoneNumbers.Add(pn);
            }
            context.SaveChanges();

            var mails = new Mail[]
                {
                new Mail { MailID = 456,MailAddres="candrea@mdfg.ro",PersonID=1,DetailsID=54 },
                new Mail { MailID = 666,MailAddres="jdnie@nj.eo", DetailsID=554, PersonID=2}
                };
            foreach(Mail m in mails)
            {
                context.Mails.Add(m);
            }
            context.SaveChanges();

            var details = new Details[]
                {
                new Details{DetailsID=54, PersonID=2, Organization="Medicover",BirthDate=DateTime.Parse("12-06-1998"), PhoneNumbersID=755},
                 new Details{DetailsID=554, PersonID=3, Organization="Medicover",BirthDate=DateTime.Parse("15-05-1998"), PhoneNumbersID=255}
                };
            foreach(Details d in details)
            {
                context.Details.Add(d);
            }
            context.SaveChanges();

              

        }
    }
}
