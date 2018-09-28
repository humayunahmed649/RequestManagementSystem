using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class ContactRepository:Repository<ContactModel>,IContactRepository
    {
        private RmsDbContext _db;
        public ContactRepository(RmsDbContext db) : base(db)
        {
            _db = db;
        }
        
    }
}
