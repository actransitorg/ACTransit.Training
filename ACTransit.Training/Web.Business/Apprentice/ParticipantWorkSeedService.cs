using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;
using System.Data.Entity.Core;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ParticipantWorkSeedService : TrainingServiceBase<ParticipantWorkSeed>
    {
        public ParticipantWorkSeedService(string currentUserName) : base(currentUserName) { }

        public ParticipantWorkSeed GetParticipantWorkSeed(int participantId, params Expression<Func<ParticipantWorkSeed, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId, paths).FirstOrDefault();
        }

        public IQueryable<ParticipantWorkSeed> GetParticipantWorkSeeds(int participantId, params Expression<Func<ParticipantWorkSeed, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId, paths);
        }

        public IQueryable<ParticipantWorkSeed> GetParticipantProgressWorks(int participantId, DateTime startDate, params Expression<Func<ParticipantWorkSeed, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId && p.StartDate == startDate, paths);
        }

        public ParticipantWorkSeed Add(ParticipantWorkSeed entity)
        {
            ParticipantWorkSeed result;
            var resultInternal = (EntityKeyMember[])AddInternal(entity);
            result = new ParticipantWorkSeed
            {
                ParticipantId = (int)resultInternal[0].Value,
            };
            return result;
        }

        public ParticipantWorkSeed Update(ParticipantWorkSeed entity)
        {
            ParticipantWorkSeed result;
            var resultInternal = (EntityKeyMember[])UpdateInternal(entity);
            result = new ParticipantWorkSeed
            {
                ParticipantId = (int)resultInternal[0].Value,
            };
            return result;
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }
    }
}
