using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Interfaces;

namespace ACTransit.Training.Web.Domain.Models
{
    public class EnrollmentsPageViewModel : IPagingRequired
    {
        public long CourseTypeId { get; set; }
        public long[] CourseIds { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Badge { get; set; }
        public List<SelectListItem> Courses { get; set; }
        public List<EnrollmentViewModel> Enrollments { get; set; }

        #region IPagingRequired
        public int RowsPerPage { get; set; }
        public int SkipRows { get; set; }
        public long TotalRows { get; set; }
        #endregion
    }
   
}
