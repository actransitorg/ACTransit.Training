using System;
using System.Linq;
using System.Web.Mvc;
using ACTransit.Entities.Training;
using ACTransit.Framework.Extensions;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Services
{
    public class TopicServiceDomain:BaseService
    {
        public TopicPageViewModel GetTopicPageViewModel(long? topicId)
        {
            TopicPageViewModel model;
            if (topicId.HasValue)
            {
                var topic = TopicService.GetTopic(topicId.Value, null);
                if (topic == null)
                    throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);
                model = new TopicPageViewModel
                {
                    TopicId = topicId.Value,
                    Name = topic.Name,
                    CourseTypeId = topic.CourseTypeId,
                    Description = topic.Description,
                    IsActive = topic.IsActive,
                    State = ViewModelState.Edit
                };
            }
            else
            {
                model = new TopicPageViewModel { State = ViewModelState.New, IsActive = true };
            }

            model = (TopicPageViewModel)PrepareModel(model);

            if (model.State == ViewModelState.Edit)
            {
                model.CourseTypeName =
                    model.CourseTypes.Where(ct => ct.Value == model.CourseTypeId.ToString())
                        .Select(ct => ct.Text)
                        .FirstOrDefault();
            }




            return model;
        }

        public TopicsPageViewModel GetTopicsPageViewModel(TopicsPageViewModel model)
        {
            if (model == null)
                model = new TopicsPageViewModel();

            model = (TopicsPageViewModel)PrepareModel(model);
            var selectedCouseTypeIds = model.CourseTypes.Where(m => m.Selected).Select(m => m.Value).ToArray();
            model.Topics = TopicService.GetTopicsByCourseTypes(selectedCouseTypeIds
                                                                    .Select(m => m.ToLong())
                                                                    .Where(m => m.HasValue)
                                                                    .Select(m => m.Value)
                                                                    .ToArray()
                                                                ).OrderBy(m => m.Name).Select(m => new TopicViewModel
                                                                {
                                                                    TopicId = m.TopicId,
                                                                    Name = m.Name,
                                                                    CourseTypeId = m.CourseTypeId,
                                                                    Description = m.Description,
                                                                    IsActive = m.IsActive,
                                                                    CourseTypeName = model.CourseTypes.Where(ct => ct.Value == m.CourseTypeId.ToString()).Select(ct => ct.Text).FirstOrDefault()
                                                                }).ToList();
            return model;
        }

        public TopicPageViewModel SaveTopic(TopicPageViewModel model)
        {
            if (model == null)
                throw new Exception("Parameter model can't be null.");

            var topicTypeId = TopicService.GetTopicType("Topic");

            var topic = new Topic
            {
                TopicId = model.TopicId,
                Name = model.Name,
                Description = model.Description,
                CourseTypeId = model.CourseTypeId,
                IsActive = model.IsActive,
                TopicTypeId = topicTypeId
            };
            long topicId;
            if (model.State == ViewModelState.New)
                topicId = TopicService.Add(topic);
            else
                topicId = TopicService.Update(topic);
            return GetTopicPageViewModel(topicId);
        }

        public void DeleteTopic(long topicId)
        {
            if (TopicService.IsInUse(topicId))
                throw new FriendlyException(FriendlyExceptionType.InUseCanNotDelete);
            TopicService.Delete(topicId);
        }


        public bool ValidateModel(TopicPageViewModel model, ModelStateDictionary modelState)
        {
            bool isValid = true;
            if (model == null)
                throw new Exception("Parameter model can't be null.");
            model.Name = Trim(model.Name);
            model.Description = Trim(model.Description);

            if (model.CourseTypeId == 0)
                modelState.AddModelError("CourseTypeId", "Course Type must be selected.");

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                modelState.AddModelError("Name", "Name can't be empty.");
                isValid = false;
            }
            else
            {
                var topics = TopicService.Get(m => m.Name == model.Name).ToList();
                if (topics.Any())
                {
                    //todo needs to check the state of the viewmodel
                    if (model.TopicId == 0)
                    {
                        modelState.AddModelError("Name", "Name already exist.");
                        isValid = false;
                    }
                    else if (model.TopicId != 0 && topics[0].TopicId != model.TopicId)
                    {
                        modelState.AddModelError("Name", "Name already exist.");
                        isValid = false;
                    }
                }
            }
            model = (TopicPageViewModel)PrepareModel(model);
            return isValid;
        }
    }
}
