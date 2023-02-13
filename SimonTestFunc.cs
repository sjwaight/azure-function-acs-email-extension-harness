using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SiliconValve.Demo.CommunicationServiceEmail;
using Azure.Communication.Email.Models;
using System.Collections.Generic;

namespace testfunc
{
    public class SimonTestFunc
    {
        [FunctionName("SimonTestFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, 
            [CommunicationServiceEmail(ConnectionString = "YourAppSettingValue")] IAsyncCollector<EmailMessage> emailClient, 
            ILogger log)
        {
            
            var emailContent = new EmailContent(subject:"My Test Functions email")
                                {
                                    PlainText = "Azure Functios Custom Extensions test!"
                                };

            var recipients = new EmailRecipients(to: new List<EmailAddress>() { new EmailAddress("not.a.real.person@no-such-domain.example")});
            
            var emailMessage = new EmailMessage("DoNotReply@your_custom_email_subdomain.azurecomm.net", emailContent, recipients);

            await emailClient.AddAsync(emailMessage);

            return new OkObjectResult("Yes!");
        }
    }
}
