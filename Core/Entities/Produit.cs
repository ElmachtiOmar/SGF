using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Produit : EntityBase
    {
        public string Libelle { get; set; }

        public string Reference {  get; set; }

        public List<Famille> Familles {  get; set; } 

        public double PrixAchatHT {  get; set; }

        public double PrixVenteHT {  get; set; }

        public int QuantityStock {  get; set; }
    }
}
