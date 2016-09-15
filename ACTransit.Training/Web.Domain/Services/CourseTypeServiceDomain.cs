using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Models;


namespace ACTransit.Training.Web.Domain.Services
{
    public class CourseTypeServiceDomain:BaseService
    {
        public CourseTypeViewModel GetCourseType(long id)
        {
            return Converter.ToViewModel(CourseTypeService.GetById(id));
        }

        public CourseType GetCourseType(string name)
        {
            return CourseTypeService.GetCourseType(name);
        }



        public long SaveCourseType(CourseTypeViewModel courseType)
        {
            return 0;// CourseTypeService.SaveCourseType(courseType);
        }

        public void DeleteCourseType(long courseTypeId)
        {
            CourseTypeService.Delete(courseTypeId);
        }
    }
}
