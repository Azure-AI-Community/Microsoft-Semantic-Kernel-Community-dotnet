using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Microsoft.SemanticKernel.Plugins.MsGraph;
using Microsoft.SemanticKernel.Plugins.MsGraph.Models;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Google;

public class GMailConnector : IEmailConnector
{
    private readonly GmailService gMailService;

    private string fullName;

    private readonly GMailHelper gMailHelper = new GMailHelper();

    public GMailConnector(GmailService gMailService, string senderFullName = "")
    {
        this.gMailService = gMailService ?? throw new ArgumentNullException(nameof(gMailService));
        this.fullName = senderFullName;

        if(!string.IsNullOrEmpty(this.fullName))
            this.fullName = fullName.Trim();     
    }


    public Task<string> GetMyEmailAddressAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var profile = gMailService.Users.GetProfile("me").Execute();
        return Task.FromResult(profile.EmailAddress);
    }

    public async Task SendEmailAsync(string subject, string content, string[] recipients,
        CancellationToken cancellationToken = new CancellationToken())
    {

        if(string.IsNullOrEmpty(subject))
            throw new ArgumentNullException(nameof(subject));

        if(string.IsNullOrEmpty(content))
            throw new ArgumentNullException(nameof(content));

        if (recipients == null || recipients.Length == 0)
            throw new ArgumentNullException(nameof(recipients));

        var senderEmail = await GetMyEmailAddressAsync(cancellationToken);

        if(string.IsNullOrEmpty(fullName))
            fullName = senderEmail;

        foreach (var recipient in recipients)
        {
            var email = gMailHelper.CreateEmail(fullName,senderEmail,recipient, subject, content);
            await gMailHelper.SendEmail(gMailService, "me", email, cancellationToken);
        }
    }

    public async Task<IEnumerable<EmailMessage>> GetMessagesAsync(int? top, int? skip, string? select,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UsersResource.MessagesResource.ListRequest listRequest = this.gMailService.Users.Messages.List("me");

        if (top.HasValue)
        {
            listRequest.MaxResults = top.Value;
        }

        if (skip.HasValue)
        {
            listRequest.PageToken = skip.ToString();
        }

        ListMessagesResponse messages = await listRequest.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        List<EmailMessage> emailMessages = new List<EmailMessage>();

        if (messages.Messages != null)
        {
            foreach (var message in messages.Messages)
            {
                var messageInfo = gMailHelper.GetMessageInfo(gMailService, "me", message.Id);
                
                emailMessages.Add(messageInfo);
            }
        }

        return emailMessages;
    }

    
}