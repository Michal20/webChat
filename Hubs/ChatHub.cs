using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace webChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, string userId, string contactId)
        {
            await Clients.User(userId).SendAsync("SenderMessage", message, contactId);
            await Clients.User(contactId).SendAsync("ReceiveMessage", message, userId);


        } 
    }
}
