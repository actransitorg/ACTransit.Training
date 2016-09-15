using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class TopicService : TrainingServiceBase<Topic>
    {
        private const string TopicName = "Topic";
        
        public TopicService(string currentUserName) : base(currentUserName) { }

        public Topic GetTopic(long topicId, params Expression<Func<Topic, object>>[] paths)
        {
            return Get(m => m.TopicId == topicId,paths).FirstOrDefault();
        }

        public IEnumerable<Topic> GetTopicsByCourseTypes(long[] courseTypeIds, params Expression<Func<Topic, object>>[] paths)
        {
            return Get(m => courseTypeIds.Contains(m.CourseTypeId) && m.TopicType.Name == TopicName, paths).OrderBy(m => m.Name);
        }

        public IQueryable<Topic> GetTopicsByCourseType(long courseTypeId, params Expression<Func<Topic, object>>[] paths)
        {
            return Get(m => m.CourseTypeId == courseTypeId && m.TopicType.Name == TopicName).OrderBy(m => m.Name);
        }
        public IQueryable<Topic> GetTopicsInCourse(long courseId, params Expression<Func<Topic, object>>[] paths)
        {
            var topicIds = UnitOfWork.Get<CourseTopic>().Where(m => m.CourseId == courseId && m.Topic.TopicType.Name == TopicName).Select(m => m.TopicId).ToList();
            return Get(m => topicIds.Contains(m.TopicId), paths).OrderBy(m=>m.Name);
        }

        public long GetTopicType(string topicName)
        {
            var topic = UnitOfWork.Get<TopicType>().FirstOrDefault(m => m.Name == topicName);
            return topic != null ? topic.TopicTypeId : 0;
        }

        public IQueryable<ComponentTopic> GetComponentTopicsInCourse(long courseId, params Expression<Func<ComponentTopic, object>>[] paths)
        {
            var topicIds = UnitOfWork.Get<CourseTopic>().Where(m => m.CourseId == courseId).Select(m => m.TopicId).ToList();
            return UnitOfWork.Get(paths).Where(m => topicIds.Contains(m.TopicId)).OrderBy(m => m.Name);
        }


        public IQueryable<ComponentTopic> GetComponentTopicsByCourseType(long courseTypeId, params Expression<Func<ComponentTopic, object>>[] paths)
        {
            return UnitOfWork.Get(paths).Where(m => m.CourseTypeId == courseTypeId).OrderBy(m => m.Name);
        }

        public IQueryable<ComponentTopic> GetComponentTopicsByCourse(long courseId, params Expression<Func<ComponentTopic, object>>[] paths)
        {
            var topicIds = UnitOfWork.Get<CourseTopic>().Where(m => m.CourseId == courseId).Select(m => m.TopicId).ToList();
            return UnitOfWork.Get(paths).Where(m => topicIds.Contains(m.TopicId)).OrderBy(m => m.Name);
        }


        public long Add(Topic entity)
        {
            return (long)AddInternal(entity);
        }
        
        public long Update(Topic entity)
        {
            return (long)UpdateInternal(entity,m=>m.Code);
        }

        public bool IsInUse(long topicId)
        {
            var isInUse = UnitOfWork.Get<CourseTopic>().Any(m => m.TopicId == topicId);
            if (!isInUse)            
                isInUse=UnitOfWork.Get<EnrollmentTopic>().Any(m => m.TopicId == topicId);
            return isInUse;
        }

        public override void RefreshCache()
        {

        }
    }
}
