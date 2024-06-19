using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(Guid id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(Guid id);

        IQueryable<Facture> GetAllWithIncludesFacture(); 
        IQueryable<Client> GetAllWithIncludesClient(); 
        IQueryable<LigneFacture> GetAllWithIncludesLF(); 

        
    }
}
