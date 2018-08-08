using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.Contracts
{
    public interface IDelatable
    {
        bool IsDelete();
    }
}
