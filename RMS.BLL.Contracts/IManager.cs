using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.BLL.Contracts
{
    public interface IManager<T>:IDisposable where T:class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool AddOrUpdate(ICollection<T> entities);
        bool Remove(T entity);
        T FindById(int id);
        ICollection<T> GetAll();
    }
}
