using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;
using System.Data.Entity.Core;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ParticipantWorkService : TrainingServiceBase<ParticipantWork>
    {
        public ParticipantWorkService(string currentUserName) : base(currentUserName) { }

        public ParticipantWork GetParticipantWork(int participantId, string workOrderNum, string taskNum, params Expression<Func<ParticipantWork, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId && p.WorkOrderNum == workOrderNum && p.TaskNum == taskNum, paths).FirstOrDefault();
        }

        public IQueryable<ParticipantWork> GetParticipantWorks(int participantId, params Expression<Func<ParticipantWork, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId, paths);
        }

        public IQueryable<ParticipantWork> GetParticipantWorks(int participantId, DateTime workDate, params Expression<Func<ParticipantWork, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId && p.WorkDate == workDate, paths);
        }

        public IQueryable<ParticipantWork> GetParticipantWorks(int participantId, DateTime startDate, DateTime endDate, params Expression<Func<ParticipantWork, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId && p.WorkDate >= startDate && p.WorkDate < endDate, paths);
        }

        public IQueryable<ParticipantWork> GetParticipantProgressWorks(int participantId, DateTime startDate, params Expression<Func<ParticipantWork, object>>[] paths)
        {
            return Get(p => p.ParticipantId == participantId && p.StartDate == startDate, paths);
        }

        public ParticipantWork Add(ParticipantWork entity)
        {
            ParticipantWork result;
            var resultInternal = (EntityKeyMember[])AddInternal(entity);
            result = new ParticipantWork
            {
                ParticipantId = (int)resultInternal[0].Value,
                WorkOrderNum = (string)resultInternal[1].Value,
                TaskNum = (string)resultInternal[2].Value,
            };
            return result;
        }

        public ParticipantWork Update(ParticipantWork entity)
        {
            ParticipantWork result;
            var resultInternal = (EntityKeyMember[])UpdateInternal(entity);
            result = new ParticipantWork
            {
                ParticipantId = (int)resultInternal[0].Value,
                WorkOrderNum = (string)resultInternal[1].Value,
                TaskNum = (string)resultInternal[2].Value,
            };
            return result;
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }
    }
}
