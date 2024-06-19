using Core.Entities;

namespace Web.ViewModel
{
    public class MouvementProduitViewModel

    {
        public Guid FactureId { get; set; }
        public int FactureNumero { get; set; }
        public DateOnly FactureDate { get; set; }
        public int Quantity { get; set; }
        public double TotalTTC { get; set; }
        public Client Client { get; set; }
        public Payment Payment { get; set; }
    }
}
