using ACTransit.Entities.Training;
using System.Collections.Generic;

namespace ACTransit.Training.Web.Business.Models
{
    public class TransactionEnrollmentsSaveViewModel:List<TransactionEnrollmentSaveViewModel>
    {
        public long CourseEnrollmentId { get; set; }
        public long CourseScheduleId { get; set; }
        public string Badge { get; set; }
        public long NonEmployeeId { get; set; }
    }
    public class TransactionEnrollmentSaveViewModel
    {
        public Enrollment Enrollment { get; set; }
        public List<EnrollmentVehicle> EnrollmentVehicles { get; set; }
        public List<EnrollmentTopic> EnrollmentTopics { get; set; }
        public List<EnrollmentInstructor> EnrollmentInstructors { get; set; }
    }

}


