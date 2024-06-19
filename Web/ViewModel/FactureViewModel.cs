using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModel
{
    public class FactureViewModel
    {
        public int Numero { get; set; }
        public double NetaPayerHT { get; set; }
        public double NetaPayerTTC { get; set; }

        public string? NCheque { get; set; }
        public DateOnly Date { get; set; }
        public List<LigneFactureViewModel> LigneFactures { get; set; } = new List<LigneFactureViewModel>();
        public Guid ClientId { get; set; }
        public Guid PaymentId { get; set; }


        public List<Client> Clients { get; set; }

        public Client Client { get; set; }
        public List<Payment> Payments { get; set; }

        public Payment Payment { get; set; }
    }
}