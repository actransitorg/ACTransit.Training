using ACTransit.Framework.Interfaces;

namespace ACTransit.Entities.Training
{
    public partial class CourseType : IAuditableEntity{}
    public partial class Course : IAuditableEntity { }
    public partial class CourseTopic : IAuditableEntity { }
    public partial class CourseSchedule : IAuditableEntity { }
    public partial class Topic : IAuditableEntity { }
    public partial class Instructor : IAuditableEntity { }
    public partial class NonEmployee : IAuditableEntity { }
    public partial class CourseEnrollment : IAuditableEntity { }
    public partial class EnrollmentTopic : IAuditableEntity { }
    public partial class EnrollmentInstructor : IAuditableEntity { }
    public partial class Grade : IAuditableEntity { }
    public partial class Enrollment : IAuditableEntity { }
    public partial class EnrollmentVehicle : IAuditableEntity { }
    public partial class EnrollmentVehicleRoute : IAuditableEntity { } 
}
