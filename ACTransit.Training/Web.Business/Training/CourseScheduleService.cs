using System;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ACTransit.Entities.Training;
using ACTransit.Framework.Extensions;

namespace ACTransit.Training.Web.Business.Training
{
    public class CourseScheduleService : TrainingServiceBase<CourseSchedule>
    {
        public CourseScheduleService(string currentUserName) : base(currentUserName) { }

        public CourseSchedule GetById(long entityId, params Expression<Func<CourseSchedule, object>>[] paths)
        {
            return Get(m => m.CourseScheduleId == entityId, paths).FirstOrDefault();
        }
        public IQueryable<CourseSchedule> GetCourseSchedulesByCourseType(string courseTypeName,params Expression<Func<CourseSchedule, object>>[] paths)
        {
            return Get(m => m.Course.CourseType.Name == courseTypeName,paths);
        }

        public IQueryable<CourseSchedule> GetCourseSchedules(DateTime date, params Expression<Func<CourseSchedule, object>>[] paths)
        {
            return Get(m => m.BeginEffDate >= date && m.EndEffDate <= date, paths);
        }

        public IQueryable<CourseSchedule> GetCourseSchedulesEndsBefore(DateTime date, params Expression<Func<CourseSchedule, object>>[] paths)
        {
            return Get(m => m.EndEffDate < date,paths);
        }

        public IQueryable<CourseSchedule> GetCourseSchedulesEndsAfter(DateTime date, params Expression<Func<CourseSchedule, object>>[] paths)
        {
            return Get(m => m.EndEffDate > date,paths);
        }

        public IQueryable<CourseSchedule> GetCourseSchedulesBetween(DateTime? startDate, DateTime? endDate, int? divisionId, params Expression<Func<CourseSchedule, object>>[] paths)
        {
            Expression<Func<CourseSchedule, bool>> whereClause = null;
            var exp = new Expression<Func<CourseSchedule, bool>>[3];

            if (startDate != null) exp[0] = m => (m.BeginEffDate>=startDate || m.EndEffDate>=startDate);
            if (endDate != null) exp[1] = m => (m.BeginEffDate <= endDate || m.EndEffDate <= endDate);

            if (divisionId != null) exp[1] = m => m.DivisionId == divisionId.Value;

            for (int i = 0; i < exp.Length; i++)
            {
                if (exp[i] != null)
                {
                    whereClause = whereClause == null
                        ? exp[i]
                        : whereClause.And(exp[i]);
                }
            }
            return Get(whereClause,paths);
        }

        public IQueryable<CourseSchedule> GetCourseSchedulesOfInstructor(long instructorId, params Expression<Func<CourseSchedule, object>>[] paths)
        {
            return Get(cs => cs.CourseScheduleInstructors.Any(ins => ins.InstructorId == instructorId),paths);
        }

        public int GetCourseScheduleCountBuCourse(long courseId)
        {
            return Get(m => m.CourseId == courseId).Count();
        }


        public long Add(CourseSchedule entity)
        {
            return (long)AddInternal(entity);
        }

        public long Add(CourseSchedule entity, long[] instructorIds)
        {
            return Add(entity, instructorIds, long.MinValue);
        }

        public long Add(CourseSchedule entity, long[] instructorIds, long primaryInstructorId)
        {
            long entityId;
            using (var transactionScope = new TransactionScope())
            {
                entity.CourseScheduleInstructors.Clear();
                entityId = (long)AddInternal(entity);  //Add and save
                for (var i = 0; i < instructorIds.Length; i++)
                    entity.CourseScheduleInstructors.Add(new CourseScheduleInstructor { CourseScheduleId = entityId, InstructorId = instructorIds[i], IsPrimary = instructorIds[i] ==primaryInstructorId});
                entityId = (long)UpdateInternal(entity);
                transactionScope.Complete();
            }
            return entityId;
        }

        public long Update(CourseSchedule entity)
        {
            return (long)UpdateInternal(entity);
        }

        public long Update(CourseSchedule entity, long[] instructorIds)
        {
            long entityId=entity.CourseScheduleId;
            var courseScheduleInstructorsToBeDeleted = UnitOfWork.Get<CourseScheduleInstructor>().Where(m => m.CourseScheduleId == entity.CourseScheduleId && !instructorIds.Contains(m.InstructorId)).ToList();

            var courseScheduleInstructorIdsExisted = UnitOfWork.Get<CourseScheduleInstructor>().Where(m => m.CourseScheduleId == entity.CourseScheduleId && instructorIds.Contains(m.InstructorId)).Select(m=>m.InstructorId).ToList();
            var courseScheduleInstructorIdsToBeAdded =instructorIds.Where(m =>!courseScheduleInstructorIdsExisted.Contains(m)).ToArray();
            entity.CourseScheduleInstructors.Clear();

            using (var transactionScope = new TransactionScope())
            {
                for (var i = 0; i < courseScheduleInstructorsToBeDeleted.Count; i++)
                    UnitOfWork.Delete(courseScheduleInstructorsToBeDeleted[i]);
                for (var i = 0; i < courseScheduleInstructorIdsToBeAdded.Length; i++)
                {
                    var instructorId = courseScheduleInstructorIdsToBeAdded[i];
                    UnitOfWork.Create(new CourseScheduleInstructor
                    {
                        CourseScheduleId = entityId,
                        InstructorId = instructorId,
                        IsPrimary = false
                    });

                }
                UnitOfWork.Update(entity);
                UnitOfWork.SaveChanges();
               
                transactionScope.Complete();
            }
            return entityId;
        }

        public void Delete(long entityId)
        {
            using (var transaction = new TransactionScope())
            {
                var csiS=UnitOfWork.Get<CourseScheduleInstructor>().Where(m=>m.CourseScheduleId==entityId).ToArray();
                foreach(var csi in csiS)
                    UnitOfWork.Delete(csi);
                base.Delete(entityId);
                transaction.Complete();
            }
        }
      
        public override void RefreshCache()
        {
            
        }
    }
}
