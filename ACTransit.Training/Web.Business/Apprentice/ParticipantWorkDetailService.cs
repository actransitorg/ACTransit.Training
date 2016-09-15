using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ParticipantWorkDetailService : TrainingServiceBase<ParticipantWorkDetail>
    {
        public ParticipantWorkDetailService(string currentUserName) : base(currentUserName) { }

        public IQueryable<ParticipantWorkDetail> GetParticipantWorkDetails(int participantId, params Expression<Func<ParticipantWorkDetail, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId, paths);
        }

        public int Add(ParticipantWorkDetail entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(ParticipantWorkDetail entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }
    }
}
