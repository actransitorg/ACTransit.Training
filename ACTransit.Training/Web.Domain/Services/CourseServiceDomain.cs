using System.Collections.Generic;
using System.Linq;
using ACTransit.Entities.Training;
using ACTransit.Framework.Extensions;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Services
{
    public class CourseServiceDomain:BaseService
    {
        public CoursesViewModel GetModel(CoursesViewModel model)
        {
            if (model == null)
                model = new CoursesViewModel {JustShowActive = true};
            model = (CoursesViewModel)base.PrepareModel(model);
            var selectedCoureseTypes = model.CourseTypes.Where(m => m.Selected).Select(m => m.Value.ToLong()).ToList();
            var courseResult = CourseService.Get(m => (!model.JustShowActive || m.IsActive) && selectedCoureseTypes.Contains(m.CourseTypeId)).OrderBy(m => m.Name).ToList().Select(Converter.ToViewModel).ToList();
            model.Courses = courseResult;

            return model;
        }

        public CoursePageViewModel GetCoursePageViewModel(long? courseId)
        {
            var model = new CoursePageViewModel();
            if (courseId.HasValue && courseId != 0)
            {
                var course = CourseService.GetById(courseId.Value);
                if (course == null)
                    throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);
                model.Course = Converter.ToViewModel(course);
                model.HasEnrollment = EnrollmentService.GetEnrollmentsByCourse(course.CourseId).Any();
                model.State = ViewModelState.Edit;
            }
            else
                model.State = ViewModelState.New;


            model = PrepareCoursePageViewModel(model);


            return model;
        }

        public long SaveCourse(CoursePageViewModel model)
        {
            if (model.Course==null)
                throw new FriendlyException(FriendlyExceptionType.InvalidModelState);

            var course = Converter.ToDbObjType(model.Course);
            var topics = model.Course.Topics.Select(Converter.ToDbObjType).ToList();
            var componentTopics = model.Course.ComponentTopics.Select(Converter.ToDbObjType).ToList();

            var allTopics = new List<Topic>(topics);
            allTopics.AddRange(componentTopics);

            if (model.State == ViewModelState.Edit)
                return CourseService.UpdateCourse(course, allTopics);
            if (model.State == ViewModelState.New)
            {
                var exists = CourseService.GetCourses(course.Name, course.CourseTypeId).Any();
                if (exists)
                    throw new FriendlyException(FriendlyExceptionType.NameAlreadyExist);
                return CourseService.AddCourse(course, allTopics);
            }
                
            return -1;
        }

        public void DeleteCourse(long courseId)
        {
            var count = CourseScheduleService.GetCourseScheduleCountBuCourse(courseId);
            if (count > 0)
                throw new FriendlyException("Course in use, cannot delete.");
            CourseService.Delete(courseId);
        }

        private CoursePageViewModel PrepareCoursePageViewModel(CoursePageViewModel model)
        {
            model = (CoursePageViewModel)base.PrepareModel(model);
            if (model.State == ViewModelState.New)
                model.Course.IsActive = true;
            model.CourseTypes = GetCourseTypes();
            AddIfNotExistCourseType(model.Course.CourseTypeId, model.CourseTypes);

            if (model.State == ViewModelState.Edit)
            {
                var topics = TopicService.GetTopicsInCourse(model.Course.CourseId).ToList();
                var componentTopics = TopicService.GetComponentTopicsInCourse(model.Course.CourseId).ToList();
                model.Course.Topics = topics.Select(Converter.ToViewModel).ToList();
                model.Course.ComponentTopics = componentTopics.Select(Converter.ToViewModel).ToList();
            }

            return model;
        }

        public override List<TopicViewModel> GetTopics(long courseTypeId, long? courseId)
        {            
            var topics = TopicService.GetTopicsByCourseType(courseTypeId).ToList().Select(Converter.ToViewModel).ToList();
            if (courseId.HasValue)
            {
                var topicForCourseIds = TopicService.GetTopicsInCourse(courseId.Value).Where(m => m.CourseTypeId == courseTypeId).Select(m => m.TopicId);
                foreach (var topicId in topicForCourseIds)
                  AddIfNotExistTopic(topicId, topics);
            }
            return topics;
        }

        public List<TopicViewModel> GetComponentTopics(long courseTypeId, long? courseId)
        {
            var topics = TopicService.GetComponentTopicsByCourseType(courseTypeId).ToList().Select(Converter.ToViewModel).ToList();
            if (courseId.HasValue)
            {
                var topicForCourseIds = TopicService.GetTopicsInCourse(courseId.Value).Where(m => m.CourseTypeId == courseTypeId).Select(m => m.TopicId);
                foreach (var topicId in topicForCourseIds)
                    AddIfNotExistTopic(topicId, topics);
            }
            return topics;
        }

    }
}
