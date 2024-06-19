using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LigneFacture : EntityBase
    {
        public double TotalHT {  get; set; }
        public int Quantite { get; set; }

        public Produit Produit { get; set; }
    }
}
