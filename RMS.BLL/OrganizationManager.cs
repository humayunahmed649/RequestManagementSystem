using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.Base;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;
using RMS.Repositories.Contracts;

namespace RMS.BLL
{
    public class OrganizationManager:Manager<Organization>,IOrganizationManager
    {
        private IOrganizationRepository _organizationRepository;
        public OrganizationManager(IOrganizationRepository repository) : base(repository)
        {
            this._organizationRepository = repository;
        }


    }
}
