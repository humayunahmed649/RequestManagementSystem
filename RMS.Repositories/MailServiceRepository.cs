using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class MailServiceRepository:Repository<MailService>,IMailServiceRepository
    {
        public MailServiceRepository(DbContext db) : base(db)
        {
        }
    }
}
