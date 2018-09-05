using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;
using RMS.Repositories.Contracts;

namespace RMS.BLL.Base
{
    public class Manager<T>:IDisposable,IManager<T> where T:class
    {
        protected IRepository<T> _Repository;

        public Manager(IRepository<T> repository)
        {
            this._Repository = repository;
        } 
        public void Dispose()
        {
            _Repository?.Dispose();
        }

        public bool Add(T entity)
        {
            return _Repository.Add(entity);
        }

        public virtual bool Update(T entity)
        {
            return _Repository.Update(entity);
        }

        public bool AddOrUpdate(ICollection<T> entities)
        {
            return _Repository.AddOrUpdate(entities);
        }
        public bool Remove(T entity)
        {
            return _Repository.Remove(entity);
        }

        public T FindById(int id)
        {
            return _Repository.FindById(id);
        }

        public ICollection<T> GetAll()
        {
            return _Repository.GetAll();
        }
    }
}
