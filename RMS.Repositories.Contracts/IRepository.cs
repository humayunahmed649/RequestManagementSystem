using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Repositories.Contracts
{
    public interface IRepository<T>:IDisposable where T:class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Remove(T entity);
        T FindById(int id);
        ICollection<T> GetAll();
    }
}
