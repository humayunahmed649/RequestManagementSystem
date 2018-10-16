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
    public class ReplyManager:Manager<Reply>, IReplyManager
    {
        private IReplyRepository _replyRepository;
        public ReplyManager(IReplyRepository repository) : base(repository)
        {
            this._replyRepository = repository;
        }

        public Reply GetAllByFeedbackId(int feedbackId)
        {
            return _replyRepository.GetAllByFeedbackId(feedbackId);
        }
    }
}
