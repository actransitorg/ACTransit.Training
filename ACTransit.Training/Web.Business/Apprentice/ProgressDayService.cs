using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ProgressDayService : TrainingServiceBase<ProgressDay>
    {
        public ProgressDayService(string currentUserName) : base(currentUserName) { }

        public ProgressDay GetProgressDay(int id, params Expression<Func<ProgressDay, object>>[] paths)
        {
            return Get(p => p.ProgressDayId == id, paths).FirstOrDefault();
        }

        public IQueryable<ProgressDay> GetProgressDays(int progressId, params Expression<Func<ProgressDay, object>>[] paths)
        {
            return Get(p => p.ProgressId == progressId, paths);
        }

        public ProgressDay GetProgressDay(int progressId, DateTime calendarDate, params Expression<Func<ProgressDay, object>>[] paths)
        {
            return Get(p => p.ProgressId == progressId && p.CalendarDate == calendarDate, paths).FirstOrDefault();
        }

        public ProgressDay GetLastProgressDay(int progressId, params Expression<Func<ProgressDay, object>>[] paths)
        {
            return Get(p => p.ProgressId == progressId, paths).OrderByDescending(p => p.ProgressDayId).First();
        }

        public int Add(ProgressDay entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(ProgressDay entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
