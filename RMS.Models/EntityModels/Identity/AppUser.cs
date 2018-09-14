using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RMS.Models.Identity.IdentityConfig;

namespace RMS.Models.EntityModels.Identity
{
    public class AppUser: IdentityUser<int,AppUserLogin,AppUserRole,AppUserClaim>
    {
        
     
    }
}
