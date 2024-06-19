using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Facture : EntityBase
    {
        public Facture()
        {
            Client = new Client(); // Initialize to avoid null reference
            LigneFactures = new List<LigneFacture>();
        }
        public int Numero {  get; set; }

        public Payment ?Payment { get; set; }

        public string ?NCheque {  get; set; }

        public double NetaPayerHT {  get; set; }

        public DateOnly Date {  get; set; }

        public double TotalTVA { get; set; }

        public double NetaPayerTTC {  get; set; }

        public List<LigneFacture> LigneFactures { get; set; } = new List<LigneFacture>();  

        public Client Client { get; set; }
    }
}
