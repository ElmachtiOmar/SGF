using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table;
        public GenericRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();

        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(Guid id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            T entity = GetById(id);
            table.Remove(entity);
        }
        public IQueryable<Facture> GetAllWithIncludesFacture()
        {
            return _context.Set<Facture>()
                           .Include(f => f.Client)
                           .ThenInclude(f => f.Adress)
                           .Include(f => f.Payment)
                           .Include(f => f.LigneFactures)
                           .ThenInclude(lf => lf.Produit);
        }
        
        public IQueryable<Client> GetAllWithIncludesClient()
        {
            return _context.Set<Client>()
                           .Include(c => c.Adress);
        }
        public IQueryable<LigneFacture> GetAllWithIncludesLF()
        {
            return _context.Set<LigneFacture>()
                           .Include(c => c.Produit);
        }



        public IQueryable<T> GetAllWithIncludes()
        {
            throw new NotImplementedException();
        }

        
    }
}
