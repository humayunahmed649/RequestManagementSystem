using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.Repositories.Contracts
{
    public interface IRepository<T>:IDisposable where T:class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool AddOrUpdate(ICollection<T> entities);
        bool Remove(T entity);
        T FindById(int id);
        ICollection<T> GetAll();
        //ICollection<T> Get(Expression<Func<T, bool>> query);
    }
}
