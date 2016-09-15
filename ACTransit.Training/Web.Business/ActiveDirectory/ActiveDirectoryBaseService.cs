namespace ACTransit.Training.Web.Business.ActiveDirectory
{
    public abstract class ActiveDirectoryBaseService:BaseService
    {
        protected string ActiveDirectoryUrl { get; set; }
        protected string ActiveDirectoryUser { get; set; }
        protected string ActiveDirectoryPwd { get; set; }
    }
}
