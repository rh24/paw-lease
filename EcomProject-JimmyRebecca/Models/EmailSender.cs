using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration Configuration;

        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(Configuration["SendGrid_Api_Key"]);

            var fromEmail = new EmailAddress("notifications@ecomcats.com");

            EmailAddress to = new EmailAddress(email);

            var msg = MailHelper.CreateSingleEmail(fromEmail, to, subject, null, htmlMessage);

            var response = await client.SendEmailAsync(msg);

        }
    }
}
