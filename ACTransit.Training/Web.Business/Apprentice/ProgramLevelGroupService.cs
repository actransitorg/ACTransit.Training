using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ProgramLevelGroupService : TrainingServiceBase<ProgramLevelGroup>
    {
        public ProgramLevelGroupService(string currentUserName) : base(currentUserName) { }

        public ProgramLevelGroup GetProgramLevelGroup(int id, params Expression<Func<ProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.ProgramLevelGroupId == id, paths).FirstOrDefault();
        }

        public ProgramLevelGroup GetProgramLevelGroup(string description, params Expression<Func<ProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.Description == description, paths).FirstOrDefault();
        }

        public IQueryable<ProgramLevelGroup> GetProgramLevelGroups(int id, params Expression<Func<ProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.ProgramId == id, paths);
        }

        public IQueryable<ProgramLevelGroup> GetProgramLevelGroups(string program, params Expression<Func<ProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.Program.Name == program || p.Program.ProgramType == program, paths);
        }

        // TODO: implement cloning

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
