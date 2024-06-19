using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Adress> Adresses { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Facture> Factures { get; set; }

        public DbSet<Produit> Produit { get; set; }

        public DbSet<LigneFacture> LigneFacture { get; set; }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Famille> Familles { get; set; }
    }
}
