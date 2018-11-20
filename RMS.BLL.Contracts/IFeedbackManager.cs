using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.BLL.Contracts
{
    public interface IFeedbackManager:IManager<Feedback>
    {
        ICollection<Feedback> GetAllByRequisitionId(int requisitionId);
        Reply GetReply(int feedbackId);
    }
}
