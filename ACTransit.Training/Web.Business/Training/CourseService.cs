using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class CourseService : TrainingServiceBase<Course>
    {
        public CourseService(string currentUserName) : base(currentUserName) { }

        public IQueryable<Course> GetCourses(string name, params Expression<Func<Course, Object>>[] paths)
        {
            return Get(m => m.Name == name,paths);
        }

        public IQueryable<Course> GetCourses(string name, long? courseTypeId, params Expression<Func<Course, Object>>[] paths)
        {
            var typeId = courseTypeId.GetValueOrDefault();
            return Get(m => m.Name == name && (!courseTypeId.HasValue || m.CourseTypeId == typeId), paths);
        }

        public IQueryable<Course> GetAvailableCoursesForCourseSchedule(long? courseScheduleId)
        {
            long courseId =-1;
            if (courseScheduleId!=null && courseScheduleId != 0)
            {
                var cs = UnitOfWork.GetById<CourseSchedule, long>(courseScheduleId.Value);
                if (cs != null)
                    courseId = cs.CourseId;                
            }

            return courseId==-1?
                Get(m => m.IsActive):
                Get(m => m.IsActive || m.CourseId==courseId);
        }

       
        public IQueryable<Course> GetCoursesByCourseType(long courseTypeId)
        {
            return Get(m => m.CourseTypeId == courseTypeId);
        }

        public IQueryable<Course> GetCoursesByCourseTypes(long[] courseTypeIds)
        {
            return Get(m => courseTypeIds.Contains(m.CourseTypeId));
        }

        public IQueryable<Course> GetCoursesByCourseType(string courseTypeName)
        {
            return Get(m => m.CourseType.Name == courseTypeName);
        }

        public IQueryable<Course> GetCoursesHasCourseSchedule(long[] courseTypeIds, DateTime? startDate, DateTime? endDate,string badge, params Expression<Func<Course, object>>[] paths)
        {
            var courses = UnitOfWork.Get<Course>();
            var courseScheduleDetails = UnitOfWork.Get<CourseScheduleDetail>();

            var courseIds = (from cr in courseScheduleDetails
                      where
                        courseTypeIds.Contains(cr.CourseTypeId)
                        && (badge == null || (cr.Badge==badge))
                        && (startDate == null || (cr.BeginEffDate >= startDate || cr.EndEffDate >= startDate))
                        && (endDate == null || (cr.BeginEffDate <= endDate || cr.EndEffDate <= endDate))
                      select cr.CourseId).ToArray();

            var res = from course in courses
                    where
                        courseIds.Contains(course.CourseId)
                    select course;                   

            foreach (var p in paths)
                res=res.Include(p);
            
            return res.Distinct();            
        }

        public long AddCourse(Course course)
        {
            return (long)AddInternal(course);
        }

        public long AddCourse(Course course, IEnumerable<Topic> topics)
        {
            using (var transaction = new TransactionScope())
            {
                var courseId= (long)AddInternal(course);
                foreach (var topic in topics)
                {
                    var ct = new CourseTopic {TopicId = topic.TopicId, CourseId = courseId};
                    UnitOfWork.Create(ct);
                }
                UnitOfWork.SaveChanges();
                transaction.Complete();
                return courseId;
            }
            
        }

        public long UpdateCourse(Course course, IEnumerable<Topic> topics)
        {
            var coureTopics = UnitOfWork.Get<CourseTopic>().Where(m => m.CourseId == course.CourseId).Select(m=>new {m.CourseTopicId, m.TopicId}).ToList();
            using (var transaction = new TransactionScope())
            {
                var courseId = (long) UpdateInternal(course);
                if (topics == null)
                    topics = new List<Topic>();

                foreach (var topic in topics)
                {
                    if (!coureTopics.Any(m => m.TopicId == topic.TopicId))
                    {
                        var ct = new CourseTopic {TopicId = topic.TopicId, CourseId = courseId};
                        UnitOfWork.Create(ct);
                    }
                }
                var shouldBeDeleted =
                    coureTopics.Where(m => !topics.Any(m1 => m1.TopicId == m.TopicId))
                        .Select(m => m.CourseTopicId)
                        .ToList();
                foreach (long courseTopicId in shouldBeDeleted)
                    UnitOfWork.Delete<CourseTopic, long>(courseTopicId);


                UnitOfWork.SaveChanges();
                transaction.Complete();
                return courseId;
            }
        }

        public override void Delete<TId>(TId entityId) 
        {
            var cts = UnitOfWork.Get<CourseTopic>().Where(m => m.CourseId.Equals((long)((object)entityId))).Select(m => m.CourseTopicId).ToList();
            using (var transaction = new TransactionScope())
            {
                foreach (var ctId in cts)
                    UnitOfWork.Delete<CourseTopic, long>(ctId);
                UnitOfWork.SaveChanges();
                base.Delete(entityId);     
                transaction.Complete();
            }
        }

        public override void RefreshCache()
        {
            
        }
    }
}
