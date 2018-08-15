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
    public class AddressManager:Manager<Address>,IAddressManager
    {
        private IAddressRepository _addressRepository;
        public AddressManager(IAddressRepository repository) : base(repository)
        {
            this._addressRepository = repository;
        }
    }
}
