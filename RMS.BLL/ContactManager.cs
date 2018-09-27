using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.Base;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;
using RMS.Repositories.Contracts;

namespace RMS.BLL
{
    public class ContactManager:Manager<ContactModel>,IContactManager
    {
        private IContactRepository _contactRepository;
        public ContactManager(IContactRepository repository) : base(repository)
        {
            this._contactRepository = repository;
        }
    }
}
