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
    public class MailServiceManager:Manager<MailService>,IMailServiceManager
    {
        protected IMailServiceRepository _mailServiceRepository;
        public MailServiceManager(IMailServiceRepository mailServiceRepository) : base(mailServiceRepository)
        {
            this._mailServiceRepository = mailServiceRepository;
        }
    }
}
