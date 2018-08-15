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
    public class AddressRepository:Repository<Address>,IAddressRepository
    {
        public AddressRepository(DbContext db) : base(db)
        {
        }
    }
}
