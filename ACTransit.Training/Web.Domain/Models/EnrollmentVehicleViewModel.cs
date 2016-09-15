namespace ACTransit.Training.Web.Domain.Models
{
    public class EnrollmentVehicleViewModel
    {
        public long EnrollmentVehicleId { get; set; }
        public long EnrollmentId { get; set; }
        public string VehicleId { get; set; }
        public string VehicleDescription { get; set; }
        public long? WheelTime { get; set; }
        public string WheelTimeStr { get; set; }
        public string[] RouteAlpha { get; set; }
        public bool QualifiedTraining { get; set; }
        public bool OugLiftRampOps { get; set; }
        public bool OugSecurement { get; set; }
    }
}
