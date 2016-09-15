using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ProgressService : TrainingServiceBase<Progress>
    {
        public ProgressService(string currentUserName) : base(currentUserName) { }

        public Progress GetProgress(int id, params Expression<Func<Progress, object>>[] paths)
        {
            return Get(p => p.ProgressId == id, paths).FirstOrDefault();
        }

        public Progress GetProgress(int participantProgramLevelGroupId, DateTime startDate, params Expression<Func<Progress, object>>[] paths)
        {
            return Get(p => p.ParticipantProgramLevelGroupId == participantProgramLevelGroupId && p.StartDate == startDate, paths).FirstOrDefault();
        }

        public IQueryable<Progress> GetProgresses(int participantProgramLevelGroupId, params Expression<Func<Progress, object>>[] paths)
        {
            return Get(p => p.ParticipantProgramLevelGroupId == participantProgramLevelGroupId, paths);
        }

        public IQueryable<Progress> GetProgresses(int participantProgramLevelGroupId, DateTime startDate, DateTime endDate, params Expression<Func<Progress, object>>[] paths)
        {
            return Get(p => p.ParticipantProgramLevelGroupId == participantProgramLevelGroupId && p.StartDate >= startDate && p.StartDate <= endDate, paths);
        }

        public Progress GetLastProgress(int participantProgramLevelGroupId, params Expression<Func<Progress, object>>[] paths)
        {
            return Get(p => p.ParticipantProgramLevelGroupId == participantProgramLevelGroupId, paths).OrderByDescending(p => p.ParticipantProgramLevelGroupId).FirstOrDefault();
        }

        public int Add(Progress entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(Progress entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
