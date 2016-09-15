using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ProgramLevelService : TrainingServiceBase<ProgramLevel>
    {
        public ProgramLevelService(string currentUserName) : base(currentUserName) { }

        public ProgramLevel GetProgramLevel(int id, params Expression<Func<ProgramLevel, object>>[] paths)
        {
            return Get(p => p.ProgramLevelId == id, paths).FirstOrDefault();
        }

        public IQueryable<ProgramLevel> GetProgramLevels(params Expression<Func<ProgramLevel, object>>[] paths)
        {
            return Get(null, paths);
        }

        // TODO: implement cloning

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
