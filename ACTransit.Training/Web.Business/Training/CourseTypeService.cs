using System.Linq;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class CourseTypeService : TrainingServiceBase<CourseType>
    {
        public CourseTypeService(string currentUserName) : base(currentUserName)
        {
            
        }

        public CourseType GetCourseType(string name)
        {
            return Get(ct => ct.Name == name).FirstOrDefault();
        }

        public CourseType GetCourseTypeByCourseId(long courseId)
        {
            var course = UnitOfWork.Get<Course>( m => m.CourseType).FirstOrDefault(m=>m.CourseId==courseId);
            if (course != null)
                return course.CourseType;
            return null;
        }

        public IQueryable<CourseType> GetAvailableCourseTypesForCourseEnrollment(long courseEnrollmentId)
        {
            long courseTypeId = 0;
            var courseEnrollment =UnitOfWork.GetById<CourseEnrollment, long>(courseEnrollmentId);
            if (courseEnrollment != null && courseEnrollment.CourseSchedule != null &&
                courseEnrollment.CourseSchedule.Course != null)
                courseTypeId = courseEnrollment.CourseSchedule.Course.CourseTypeId;
            return Get(m => m.IsActive || m.CourseTypeId == courseTypeId);
        }

        public IQueryable<CourseType> GetCourseTypes()
        {
            return Get(m => m.IsActive).OrderBy(m => m.Name);
        }

        protected long Add(CourseType entity)
        {
            return (long)AddInternal(entity);
        }

        protected long Update(CourseType entity)
        {
            return (long)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            
        }
    }
}
