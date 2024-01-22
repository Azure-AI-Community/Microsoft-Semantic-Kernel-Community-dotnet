using System.Globalization;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Microsoft.SemanticKernel.Plugins.MsGraph.Models;
using MimeKit;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Google;

#pragma warning disable SKEXP0053
class GMailHelper
{
    public MimeMessage CreateEmail(string senderName, string fromAddress, string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName, fromAddress));
        message.To.Add(new MailboxAddress("", to));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        return message;
    }

    public async Task SendEmail(GmailService service, string userId, MimeMessage email, CancellationToken cancellationToken)
    {
        var msgStream = new MemoryStream();
        await email.WriteToAsync(msgStream);
        var rawMessage = Convert.ToBase64String(msgStream.GetBuffer());
        var message = new Message { Raw = rawMessage };

        await service.Users.Messages.Send(message, userId).ExecuteAsync(cancellationToken);
    }

    public EmailMessage GetMessageInfo(GmailService service, string userId, string messageId)
    {
        var message = service.Users.Messages.Get(userId, messageId).Execute();
        var emailMessage = new EmailMessage();

        emailMessage.Subject = message.Payload.Headers.FirstOrDefault(x => x.Name == "Subject")?.Value;

        emailMessage.From = new EmailAddress
        {
            Address = message.Payload.Headers.FirstOrDefault(x => x.Name == "From")?.Value
        };

        emailMessage.Recipients = GetToAddresses(message);

        emailMessage.Body = GetBodyFromMessage(message);

        var sendDateTime = message.Payload.Headers.FirstOrDefault(header => header.Name == "Date")?.Value;

        if (!string.IsNullOrEmpty(sendDateTime))
        {
            try
            {
                if (DateTimeOffset.TryParseExact(sendDateTime,
                        new[] { "ddd, d MMM yyyy HH:mm:ss zzz", "ddd, dd MMM yyyy HH:mm:ss zzz", "ddd, dd MMM yyyy HH:mm:ss zzz (UTC)" },
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsedDateTimeOffset))
                {
                    emailMessage.SentDateTime = parsedDateTimeOffset;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        var receiveDateTime = message.InternalDate;
        if (receiveDateTime != null)
            emailMessage.ReceivedDateTime = DateTimeOffset.FromUnixTimeMilliseconds(receiveDateTime.Value).DateTime;

        return emailMessage;
    }

    static List<EmailAddress> GetToAddresses(Message message)
    {
        var toAddresses = new List<EmailAddress>();

        var toHeaders = message.Payload.Headers.Where(x => x.Name == "To");

        foreach (var toHeader in toHeaders)
        {
            toAddresses.Add(new EmailAddress
            {
                Address = toHeader.Value
            });
        }

        return toAddresses;
    }

    static string GetBodyFromMessage(Message message)
    {
        var parts = message.Payload.Parts;
        if (parts != null)
        {
            foreach (var part in parts)
            {
                if (part.MimeType == "text/plain")
                {
                    var bodyData = part.Body.Data;
                    return Base64UrlDecode(bodyData);
                }
            }
        }
        return "";
    }

    static string Base64UrlDecode(string base64Url)
    {
        byte[] bytes = Convert.FromBase64String(base64Url.Replace('-', '+').Replace('_', '/'));
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}