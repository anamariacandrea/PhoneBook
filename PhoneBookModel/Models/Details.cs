using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookModel.Models
{
    public class Details
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int DetailsID { get; set; }
            public int PersonID { get; set; }
        public int PhoneNumbersID { get; set; }
            public string Organization { get; set; }
            public DateTime BirthDate { get; set; }
        public ICollection<Person> People { get; set; }
            public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
