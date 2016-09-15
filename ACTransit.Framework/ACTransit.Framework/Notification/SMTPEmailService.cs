using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using ACTransit.Framework.Notification.Interface;

namespace ACTransit.Framework.Notification
{
    public class SMTPEmailService : IEmailService
    {
        public string ServerAddress { get; private set; }

        public SMTPEmailService( string smtpServerAddress )
        {
            ServerAddress = smtpServerAddress;
        }

        /// <summary>
        /// used with config file's mailSettings section
        /// </summary>
        public SMTPEmailService() { }

        public void Send( EmailPayload payload )
        {
            ValidatePayload( payload );

            using( var mailClient = new SmtpClient() )
            {
                if (!string.IsNullOrEmpty(ServerAddress))
                    mailClient.Host = ServerAddress;

                if (payload.UseSSL.HasValue)
                    mailClient.EnableSsl = payload.UseSSL.Value;

                var message = new MailMessage
                {
                    Body = payload.Body,
                    Subject = payload.Subject,
                    From = new MailAddress( payload.FromEmailAddress ),
                    IsBodyHtml = payload.IsBodyHtml,
                };

                if (payload.To != null && payload.To.Any())
                {
                    foreach (string s in payload.To)
                        message.To.Add(s);
                }

                if( payload.CC != null && payload.CC.Any() )
                {
                    foreach (string s in payload.CC)
                        message.CC.Add(s);       
                }

                if( payload.BCC != null && payload.BCC.Any() )
                {
                    foreach (string s in payload.BCC)
                        message.Bcc.Add(s);   
                }

                if (!string.IsNullOrEmpty(payload.LoginName) || !string.IsNullOrEmpty(payload.Password))
                {
                    mailClient.Credentials = new NetworkCredential(payload.LoginName, payload.Password);
                }

                if (payload.ReplyToList != null && payload.ReplyToList.Any())
                    foreach (var item in payload.ReplyToList)
                        message.ReplyToList.Add(item);

                if (payload.Headers != null && payload.Headers.HasKeys())
                    message.Headers.Add(payload.Headers);

                mailClient.Send( message );
            }
        }

        private void ValidatePayload( EmailPayload payload )
        {
            if( payload == null )
                throw new ArgumentNullException( "payload" );

            if( string.IsNullOrEmpty( payload.FromEmailAddress ) )
                throw new InvalidOperationException( "FromEmailAddress is a required field." );

            if( ( payload.To == null || !payload.To.Any() ) && ( payload.CC == null || !payload.CC.Any() ) && ( payload.BCC == null || !payload.BCC.Any() ) )
                throw new InvalidOperationException( "At least one email address must be in the To, CC or BCC fields." );
        }
    }
}