using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

       public int PhoneID { get; set; }
      public int MailID { get; set; }
      public ICollection<Details> Details { get; set; }
       public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
