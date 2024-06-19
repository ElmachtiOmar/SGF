using Core.Entities;

namespace Web.ViewModel
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Libelle { get; set; }
        public double PrixAchatHT { get; set; }
        public double PrixVenteHT { get; set; }
        public string Familles { get; set; }
        public int QuantityStock { get; set; }
        public List<MouvementProduitViewModel> Mouvements { get; set; } = new List<MouvementProduitViewModel>();

    }
}
