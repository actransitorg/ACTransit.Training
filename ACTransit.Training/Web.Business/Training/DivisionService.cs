using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class DivisionService : TrainingServiceBase<Division>
    {
        public DivisionService(string currentUserName) : base(currentUserName) { }

        public Division GetDivision(long divisionId, params Expression<Func<Division, object>>[] paths)
        {
            return Get(m => m.DivisionId == divisionId, paths).FirstOrDefault();
        }

        public Division GetDivision(string name, params Expression<Func<Division, object>>[] paths)
        {
            return Get(m => m.Name == name, paths).FirstOrDefault();
        }

        public IQueryable<Division> GetDivisionsForCourseSchedule(long? courseScheduleId)
        {
            long? divisionId = null;
            if (courseScheduleId != null && courseScheduleId.Value != 0)
            {
                var cs = UnitOfWork.GetById<CourseSchedule, long>(courseScheduleId.Value);
                if (cs==null)
                    throw new Exception("Course Schedule " + courseScheduleId.Value + " not found.");
                divisionId = cs.DivisionId;
            }
            return (divisionId == null || divisionId.Value == 0) ?
                    Get(m => m.IsActive) :
                    Get(m=>m.IsActive || m.DivisionId==divisionId.Value);
        }

        public override void RefreshCache()
        {
            
        }
    }
}
