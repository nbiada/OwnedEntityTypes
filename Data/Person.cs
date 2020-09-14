using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnedEntityTypes.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public StreetAddress MainAddress { get; set; }
        public StreetAddress SecondaryAddress { get; set; }
        public Phone Phone { get; set; }
    }
}
