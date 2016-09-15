using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ParticipantStatusService : TrainingServiceBase<ParticipantStatus>
    {
        public ParticipantStatusService(string currentUserName) : base(currentUserName) { }

        public ParticipantStatus GetParticipantStatus(int id, params Expression<Func<ParticipantStatus, object>>[] paths)
        {
            return Get(p => p.ParticipantStatusId == id, paths).FirstOrDefault();
        }

        public ParticipantStatus GetParticipantStatus(string name, params Expression<Func<ParticipantStatus, object>>[] paths)
        {
            return Get(p => p.Name == name, paths).FirstOrDefault();
        }

        public IQueryable<ParticipantStatus> GetParticipantStatuses(params Expression<Func<ParticipantStatus, object>>[] paths)
        {
            return Get(null, paths);
        }

        public int Add(ParticipantStatus entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(ParticipantStatus entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }
    }
}
