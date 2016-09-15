using ACTransit.Framework.Configurations;
using ACTransit.Framework.Logging;
using ACTransit.Training.Web.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ACTransit.Training.Web.App_Start
{
    public class DatabaseWarmup
    {
        private static Logger logger;
        private static SyncServiceDomain service;

        public static async void Startup()
        {
            //await Task.Run(() => initialize());
        }

        private static int RunEvery = Convert.ToInt32(ConfigurationUtility.GetStringValue("SyncWithEnterpriseEvery"));

        private static void initialize()
        {
            service = new SyncServiceDomain();
            log4net.NDC.Push("Background Task");
            logger = new Logger(typeof(DatabaseWarmup).Name);
            try
            {
                execute();
            }
            catch (Exception e)
            {
                logger.WriteFatal(e.Message, e);
            }
        }

        private static void execute()
        {
            while (true)
            {
                logger.WriteDebug(string.Format("Calling SyncWithEnterprise"));
                service.SyncWithEnterprise();
                var waitUntil = new TimeSpan(0, RunEvery, 0);
                logger.WriteDebug(string.Format("Waiting {0} minutes to execute SyncWithEnterprise", waitUntil.TotalMinutes));
                Thread.Sleep(waitUntil);
            }
        }
    }
}