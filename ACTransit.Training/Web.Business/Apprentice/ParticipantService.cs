using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ParticipantService : TrainingServiceBase<Participant>
    {
        public ParticipantService(string currentUserName) : base(currentUserName) { }

        public Participant GetParticipant(int id, params Expression<Func<Participant, object>>[] paths)
        {
            return Get(p => p.ParticipantId == id, paths).FirstOrDefault();
        }

        public Participant GetParticipant(string badge, params Expression<Func<Participant, object>>[] paths)
        {
            return Get(p => p.Badge == badge, paths).FirstOrDefault();
        }

        public IQueryable<Participant> GetParticipants(params Expression<Func<Participant, object>>[] paths)
        {
            return Get(p => p.Badge != null, paths);
        }

        public IQueryable<Participant> GetProgramParticipants(int id, params Expression<Func<Participant, object>>[] paths)
        {
            return Get(p => p.ProgramId == id, paths);
        }

        public IQueryable<Participant> GetProgramParticipants(string program, params Expression<Func<Participant, object>>[] paths)
        {
            return Get(p => p.Program.Name == program || p.Program.ProgramType == program, paths);
        }

        public int Add(Participant entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(Participant entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }
    }
}
