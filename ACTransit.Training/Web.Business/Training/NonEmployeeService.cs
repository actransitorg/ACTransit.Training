using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class NonEmployeeService : TrainingServiceBase<NonEmployee>
    {
        public NonEmployeeService(string currentUserName) : base(currentUserName) { }

        public NonEmployee GetNonEmployee(long nonEmployeeId, params Expression<Func<NonEmployee, object>>[] paths)
        {
            return Get(m => m.NonEmployeeId == nonEmployeeId, paths).FirstOrDefault(m => m.NonEmployeeId == nonEmployeeId);
        }

        public IQueryable<NonEmployee> GetAvailableNonEmployeesForCourseEnrollment(long courseEnrollmentId)
        {
            long nonEmployeeId = 0;
            var courseEnrollment = UnitOfWork.GetById<CourseEnrollment, long>(courseEnrollmentId);
            if (courseEnrollment != null )                
                nonEmployeeId = courseEnrollment.NonEmployeeId.GetValueOrDefault(0);
            return Get(m => m.IsActive || m.NonEmployeeId == nonEmployeeId);
        }

        public long Add(NonEmployee entity)
        {
            return (long)AddInternal(entity);
        }

        public long Update(NonEmployee entity)
        {
            return (long)UpdateInternal(entity);
        }

        public void Delete(long nonEmployeeId)
        {
            base.Delete(nonEmployeeId);
        }

        public override void RefreshCache()
        {

        }

    }
}
