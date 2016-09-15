using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class DailyPerformanceProgramLevelGroupService : TrainingServiceBase<DailyPerformanceProgramLevelGroup>
    {
        public DailyPerformanceProgramLevelGroupService(string currentUserName) : base(currentUserName) { }

        public DailyPerformanceProgramLevelGroup GetDailyPerformanceProgramLevelGroup(int id, params Expression<Func<DailyPerformanceProgramLevelGroup, object>>[] paths)
        {
            return Get(p => p.DailyPerformanceProgramLevelGroupId == id, paths).FirstOrDefault();
        }

        public int Add(DailyPerformanceProgramLevelGroup entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(DailyPerformanceProgramLevelGroup entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }
    }
}
