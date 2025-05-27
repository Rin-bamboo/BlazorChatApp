using Microsoft.AspNetCore.SignalR;

namespace BlazorChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }
        public async Task SendMessage(string user, string message)
        {
            _logger.LogInformation("メッセージ受信: {User}: {Message}", user, message);
            // Send the message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }   
    }
}
