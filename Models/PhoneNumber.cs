using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class PhoneNumber
    {
        public int PhoneNumberID { get; set; }
        public int PersonID { get; set; }
        public int MailID { get; set; }
        public string PhoneNumbers { get; set; }
        ICollection<Person> People { get; set; }
        
    }
}
