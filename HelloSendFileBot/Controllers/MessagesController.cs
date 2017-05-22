using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;

namespace HelloSendFileBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //File that Bot will send to the user. You can keep a file in your local directory 
                //and can paste complete path here.
                string filePath = @"C:\BotDirectory\Image\IMG_4821.jpg";
                //Convert to Uri. Bot can send absolute Uri path only as attachment.
                var uri = new System.Uri(filePath);
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity message = activity.CreateReply("Please check the image you have requested for.");
                //Create attachment
                Attachment attachment = new Attachment();
                attachment.ContentType = "image/jpg";
                //Content Url for the attachment can only be Absolute Uri
                attachment.ContentUrl = uri.AbsoluteUri;
                //Add attachment to the message
                message.Attachments.Add(attachment);
                //Send Reply message along with the attachment
                await connector.Conversations.ReplyToActivityAsync(message);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}