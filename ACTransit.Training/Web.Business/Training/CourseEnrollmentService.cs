using System;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;

namespace ACTransit.Training.Web.Business.Training
{
    public class CourseEnrollmentService : TrainingServiceBase<CourseEnrollment>
    {
        public CourseEnrollmentService(string currentUserName) : base(currentUserName) { }

        public IQueryable<CourseEnrollment> GetCourseEnrollmentsOfTrainee(string badge, params Expression<Func<CourseEnrollment, object>>[] paths)
        {
            return Get(m => m.Badge == badge,paths);
        }

        public IQueryable<CourseEnrollment> GetCourseEnrollmentsOfTrainee(long nonEmployeeId, params Expression<Func<CourseEnrollment, object>>[] paths)
        {
            return Get(m => m.NonEmployeeId == nonEmployeeId,paths);
        }

        public IQueryable<CourseEnrollment> GetCourseEnrollments(long courseScheduleId, params Expression<Func<CourseEnrollment, object>>[] paths)
        {
            return Get(m => m.CourseScheduleId == courseScheduleId,paths);
        }

        public IQueryable<CourseScheduleDetail> GetCourseEnrollmentLists(long courseScheduleId, string badge)
        {
            var courseScheduleDetails = UnitOfWork.Get<CourseScheduleDetail>();
            var res = from cr in courseScheduleDetails
                      where cr.CourseScheduleId == courseScheduleId && cr.CourseEnrollmentId!=null 
                                && (badge == null || badge == "" || cr.Badge == badge)
                      select cr;

            return res;
        }

        public long Add(CourseEnrollment entity)
        {
            return (long)AddInternal(entity);
        }

        public long Update(CourseEnrollment entity)
        {
            using (var tran = new TransactionScope(TransactionScopeOption.Required,new TransactionOptions{IsolationLevel = IsolationLevel.Serializable}))
            {
                if (entity.IsUnenrolled)
                {
                    var count = UnitOfWork.Get<Enrollment>(null).Count(m => m.CourseEnrollmentId == entity.CourseEnrollmentId);
                    if (count>0)
                        throw new BusinessException("Can not apply NoShow to the Course Enrollment when enrollment record(s) exists.");
                }                
                var result= (long)UpdateInternal(entity);
                tran.Complete();
                return result;
            }
                        
        }
        public override void RefreshCache()
        {
            
        }
    }
}
