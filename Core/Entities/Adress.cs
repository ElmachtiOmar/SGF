using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Adress : EntityBase
    {
        public string Street { get; set; }

        public string City { get; set; }

        public int Numbre { get; set; }
    }
}
