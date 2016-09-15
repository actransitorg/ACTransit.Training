using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class GradeService:TrainingServiceBase<Grade>
    {
        public GradeService(string currentUserName) : base(currentUserName) { }

        public override void RefreshCache()
        {         
        }
    }
}
