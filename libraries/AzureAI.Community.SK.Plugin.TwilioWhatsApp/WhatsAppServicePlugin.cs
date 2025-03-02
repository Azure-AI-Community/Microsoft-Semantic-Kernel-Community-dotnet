using System.ComponentModel;
using Microsoft.SemanticKernel;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AzureAI.Community.SK.Plugin.TwilioWhatsApp
{
    [Description("A plugin service for sending WhatsApp messages using Twilio API.")]
    public class WhatsAppServicePlugin
    {
        private readonly string fromNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhatsAppServicePlugin"/> class with Twilio credentials and the from number.
        /// </summary>
        /// <param name="accountSid">The Account SID from Twilio.</param>
        /// <param name="authToken">The Auth Token from Twilio.</param>
        /// <param name="fromNumber">The Twilio sandbox number to send messages from.</param>
        public WhatsAppServicePlugin(string accountSid, string authToken, string fromNumber)
        {
            if (accountSid == null) throw new ArgumentNullException(nameof(accountSid));
            if (authToken == null) throw new ArgumentNullException(nameof(authToken));
            this.fromNumber = fromNumber ?? throw new ArgumentNullException(nameof(fromNumber));

            // Initialize Twilio Client
            TwilioClient.Init(accountSid, authToken);
        }

        /// <summary>
        /// Sends a message via WhatsApp.
        /// </summary>
        /// <param name="toNumber">The recipient's phone number in E.164 format (e.g., +1234567890).</param>
        /// <param name="messageBody">The body of the message to be sent.</param>
        [KernelFunction, Description("Sends a WhatsApp message to the specified recipient.")]
        public async Task<string> SendMessage(
            [Description("The recipient's phone number in the international format")]string toNumber, 
            [Description("The body of the message to be sent")]string messageBody)
        {
            if (string.IsNullOrWhiteSpace(toNumber))
            {
               return "Recipient's phone number cannot be null or empty.";
            }

            if (string.IsNullOrWhiteSpace(messageBody))
            {
                return "Message cannot be null or empty.";
            }

            try
            {
                // Send a WhatsApp message
                var message = await MessageResource.CreateAsync(
                    to: new PhoneNumber($"whatsapp:{toNumber}"),  
                    from: new PhoneNumber($"whatsapp:{fromNumber}"),  
                    body: messageBody
                );

                // Print the SID of the sent message
                Console.WriteLine("Message SID: " + message.Sid);
                return message.Sid;
            }
            catch (Exception ex)
            {
                // Handle errors
                Console.WriteLine($"Error: {ex.Message}");
            }

            return $"Failed to send the message to the recipient : {toNumber}";
        }
    }

}
