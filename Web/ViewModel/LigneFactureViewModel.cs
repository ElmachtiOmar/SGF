using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Entities;

namespace Web.ViewModel
{
    public class LigneFactureViewModel
    {
        public double TotalHT { get; set; }
        public int Quantite { get; set; }
        public Guid ProduitId { get; set; }
        public List<Produit> Produits { get; set; } = new List<Produit>();

        public Produit Produit { get; set; }


    }
}
