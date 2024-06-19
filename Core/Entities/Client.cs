using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Client : EntityBase
    {
        public Client() 
        {
           Adress = new Adress();
        }
        public int Code { get; set; }

        public string Name {  get; set; }

        public string Email {  get; set; }

        public string Phone {  get; set; }

        public Adress Adress { get; set; }

        public List<Facture> Factures { get; set; }
    }
}
