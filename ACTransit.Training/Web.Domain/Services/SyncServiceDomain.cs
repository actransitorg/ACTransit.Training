namespace ACTransit.Training.Web.Domain.Services
{
    public class SyncServiceDomain: BaseService
    {
        public void SyncWithEnterprise()
        {
            SyncService.SyncWithEnterprise();
        }
    }
}
