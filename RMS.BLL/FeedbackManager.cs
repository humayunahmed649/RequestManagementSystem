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
    public class FeedbackManager:Manager<Feedback>,IFeedbackManager
    {
        private IFeedbackRepository _feedbackRepository;
        public FeedbackManager(IFeedbackRepository repository) : base(repository)
        {
            this._feedbackRepository = repository;
        }

        public ICollection<Feedback> GetAllByRequisitionId(int requisitionId)
        {
            return _feedbackRepository.GetAllByRequisitionId(requisitionId);
        }

        public Reply GetReply(int feedbackId)
        {
            return _feedbackRepository.GetReply(feedbackId);
        }
    }
}
