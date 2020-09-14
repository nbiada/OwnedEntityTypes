using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnedEntityTypes.Data
{
    [Owned]
    public class Phone
    {
        public string Primary { get; set; }
        public string Secondary { get; set; }
    }
}
