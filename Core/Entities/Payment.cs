using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Payment : EntityBase
    {
        public string ModeDePayment {  get; set; }

        public string ModeDeLivraison {  get; set; }

        public string PaymentState { get; set; }

    }
}
