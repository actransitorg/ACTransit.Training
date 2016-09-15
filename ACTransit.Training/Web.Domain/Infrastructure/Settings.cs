using System.Collections.Generic;
using System.Linq;
using ACTransit.Framework.Configurations;
using ACTransit.Framework.Extensions;

namespace ACTransit.Training.Web.Domain.Infrastructure
{
    public static class Settings
    {
        public static bool EmailEnabled { get; private set; }

        /// <summary>
        /// List of emails addresses to send exception alerts to.
        /// </summary>
        public static IEnumerable<string> ExceptionAlertToEmails { get; private set; }

        /// <summary>
        /// List of cc email addresses to send exception alerts to.
        /// </summary>
        public static IEnumerable<string> ExceptionAlertCcEmails { get; private set; }

        /// <summary>
        /// List of Bcc email addresses to send exception alerts to.
        /// </summary>
        public static IEnumerable<string> ExceptionAlertBccEmails { get; private set; }

        /// <summary>
        /// Url used to communicate with Active Directory
        /// </summary>
        public static string ActiveDirectoryUrl { get; private set; }

        /// <summary>
        /// Active Directory user used to communicate with Active Directory
        /// </summary>
        public static string ActiveDirectoryUser { get; private set; }

        /// <summary>
        /// Active Directory password used to communicate with Active Directory
        /// </summary>
        public static string ActiveDirectoryPwd { get; private set; }


        static Settings()
        {
            var emailEnabled = ConfigurationUtility.GetStringValue("EmailEnabled");
            var toEmails = ConfigurationUtility.GetStringValue("AppExceptionAlert_To");
            var ccEmails = ConfigurationUtility.GetStringValue("AppExceptionAlert_Cc");
            var bccEmails = ConfigurationUtility.GetStringValue("AppExceptionAlert_Bcc");
            var activeDirectoryUrl = ConfigurationUtility.GetStringValue("AD_URL");
            var activeDirectoryUser = ConfigurationUtility.GetStringValue("AD_User");
            var activeDirectoryPwd = ConfigurationUtility.GetStringValue("AD_Pwd");            

            EmailEnabled = emailEnabled != null && emailEnabled.ToBool().GetValueOrDefault();
            ExceptionAlertToEmails = toEmails == null ? Enumerable.Empty<string>() : toEmails.ToEnumerable<string>(";");
            ExceptionAlertCcEmails = ccEmails == null ? Enumerable.Empty<string>() : ccEmails.ToEnumerable<string>(";");
            ExceptionAlertBccEmails = ccEmails == null ? Enumerable.Empty<string>() : bccEmails.ToEnumerable<string>(";");
            ActiveDirectoryUrl = activeDirectoryUrl ?? string.Empty;
            ActiveDirectoryUser = activeDirectoryUser ?? string.Empty;
            ActiveDirectoryPwd = activeDirectoryPwd ?? string.Empty;

        }
    }
}
