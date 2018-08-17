using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;
using RMS.Repositories.Contracts;

namespace RMS.Repositories.Base
{
    public abstract class Repository<T>:IDisposable, IRepository<T> where T:class 
    {
        protected DbContext db;

        public Repository(DbContext db)
        {
            this.db = db;
        }
        public void Dispose()
        {
            db?.Dispose();
        }
        public virtual bool Add(T entity)
        {
            db.Set<T>().Add(entity);
            return db.SaveChanges() > 0;
        }

        public virtual bool Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public virtual bool Remove(T entity)
        {
            db.Set<T>().Remove(entity);
            return db.SaveChanges() > 0;
        }

        public virtual ICollection<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        

        public virtual T FindById(int id)
        {
            return db.Set<T>().Find(id);
        }
    }
}
