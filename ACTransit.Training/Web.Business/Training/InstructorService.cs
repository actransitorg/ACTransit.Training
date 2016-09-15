using System;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class InstructorService : TrainingServiceBase<Instructor>
    {
        public InstructorService(string currentUserName) : base(currentUserName) { }

        public Instructor GetInstructor(string badge,params Expression<Func<Instructor, object>>[] paths)
        {
            return Get(m => m.Badge == badge,paths).FirstOrDefault();
        }

        public Instructor GetInstructorFromNonEmployeeId(long nonEmployeeId, params Expression<Func<Instructor, object>>[] paths)
        {
            return Get(m => m.NonEmployeeId == nonEmployeeId,paths).FirstOrDefault();
        }

        public IQueryable<Instructor> GetAvailableInstructorsForCourseSchedule(long? courseScheduleId, long courseTypeId, params Expression<Func<Instructor, object>>[] paths)
        {
            
            if (courseScheduleId != null && courseScheduleId.Value != 0)
            {
                var instructors = GetInstructorsBycourseSchedule(courseScheduleId.Value, null).Select(m => m.InstructorId).ToList();
                return Get(m => m.CourseTypeId==courseTypeId && ( m.IsActive == true || instructors.Contains(m.InstructorId)),paths);
            }
            return Get(m => m.CourseTypeId == courseTypeId && m.IsActive == true, paths);
        }

        private IQueryable<Instructor> GetInstructorsBycourseSchedule(long courseScheduleId, Expression<Func<Instructor, bool>> filter, params Expression<Func<Instructor, object>>[] paths)
        {
            var instructors = Get(null, paths);
            var courseScheduleInstructors = UnitOfWork.Get<CourseScheduleInstructor>();
            var query = from i in instructors
                        from csi in courseScheduleInstructors
                where csi.CourseScheduleId == courseScheduleId && i.InstructorId == csi.InstructorId
                select i;
            if (filter!=null)
                return query.Where(filter).AsQueryable();
            return query.AsQueryable();
        }

        public long Add(Instructor entity)
        {
            return (long)AddInternal(entity);
        }

        public long Add(Instructor entity,NonEmployee nonEmployee)
        {
            long result;
            using (var transaction = new TransactionScope())
            {
                nonEmployee=PrepareNonEmployee(nonEmployee);
                if (nonEmployee!=null)
                    entity.NonEmployeeId = nonEmployee.NonEmployeeId;
                result= (long)AddInternal(entity);   
                transaction.Complete();
            }
            return result;
        }

        public long Update(Instructor entity)
        {
            return (long)UpdateInternal(entity);
        }

        public long Update(Instructor entity, NonEmployee nonEmployee)
        {
            long result;
            using (var transaction = new TransactionScope())
            {
                nonEmployee = PrepareNonEmployee(nonEmployee);
                if (nonEmployee != null)
                    entity.NonEmployeeId = nonEmployee.NonEmployeeId;
                result = (long)UpdateInternal(entity);
                transaction.Complete();
            }
            return result;
        }

        public override void RefreshCache()
        {
            
        }

        private NonEmployee PrepareNonEmployee(NonEmployee nonEmployee)
        {
             if (nonEmployee != null)
             {
                 if (nonEmployee.NonEmployeeId != 0)
                     nonEmployee=UnitOfWork.Update(nonEmployee);
                 else
                     nonEmployee=UnitOfWork.Create(nonEmployee);
                 UnitOfWork.SaveChanges();
             }
            return nonEmployee;
        }
    }
}
