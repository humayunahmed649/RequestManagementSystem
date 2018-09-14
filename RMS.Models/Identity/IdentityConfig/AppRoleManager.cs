using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RMS.Models.EntityModels.Identity;

namespace RMS.Models.Identity.IdentityConfig
{
    public class AppRoleManager:RoleManager<AppRole,int>
    {
        public AppRoleManager(IRoleStore<AppRole, int> store) : base(store)
        {
        }
    }
}
