using System.Collections.Generic;
using System.Web.Mvc;

namespace ACTransit.Training.Web.Domain.Interfaces
{
    public interface ICourseTypeRequired
    {
        List<SelectListItem> CourseTypes { get; set; }
    }
}
