using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class SyncService: TrainingServiceBase<SyncWithEnterprise_Result>
    {
        public SyncService(string currentUserName) : base(currentUserName) { }

        public void SyncWithEnterprise()
        {
            UnitOfWork.SyncWithEnterprise();
        }

        public override void RefreshCache()
        {
        }
    }
}
