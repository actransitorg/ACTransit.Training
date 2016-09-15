using System;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ParticipantProgramLevelGroupService : TrainingServiceBase<ParticipantProgramLevelGroup>
    {
        public ParticipantProgramLevelGroupService(string currentUserName) : base(currentUserName) { }

        public ParticipantProgramLevelGroup GetParticipantProgramLevelGroup(int id, params Expression<Func<ParticipantProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.ParticipantProgramLevelGroupId == id, paths).FirstOrDefault();
        }

        public ParticipantProgramLevelGroup GetCurrentParticipantProgramLevelGroup(int participantId, params Expression<Func<ParticipantProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId, paths).Where(p => !p.EndEffDate.HasValue).OrderByDescending(p => p.BeginEffDate).FirstOrDefault();
        }

        public ParticipantProgramLevelGroup GetCurrentParticipantProgramLevelGroup(string badge, params Expression<Func<ParticipantProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.Participant.Badge == badge, paths).Where(p => !p.EndEffDate.HasValue).OrderByDescending(p => p.BeginEffDate).FirstOrDefault();
        }

        public IQueryable<ParticipantProgramLevelGroup> GetParticipantProgramLevelGroups(int participantId, params Expression<Func<ParticipantProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId, paths).OrderBy(p => p.BeginEffDate);
        }

        public IQueryable<ParticipantProgramLevelGroup> GetParticipantProgramLevelGroups(int[] participantId, params Expression<Func<ParticipantProgramLevelGroup, object>>[] paths)
        {
            return Get(p => participantId.Contains(p.ParticipantId), paths).OrderBy(p => p.BeginEffDate);
        }

        public IQueryable<ParticipantProgramLevelGroup> GetParticipantProgramLevelGroups(string badge, params Expression<Func<ParticipantProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.Participant.Badge == badge, paths).OrderBy(p => p.BeginEffDate);
        }

        public int Add(ParticipantProgramLevelGroup entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(ParticipantProgramLevelGroup entity)
        {
            return (int)UpdateInternal(entity); ;
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }
    }
}
