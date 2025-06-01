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

        public async Task JoinGroup(string groupName)
        {
            _logger.LogInformation("グループ参加: {GroupName}", groupName);
            // Add the caller to the specified group
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            _logger.LogInformation("グループ離脱: {GroupName}", groupName);
            // Remove the caller from the specified group
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            _logger.LogInformation("グループメッセージ送信: {GroupName} - {User}: {Message}", groupName, user, message);
            // Send the message to all clients in the specified group
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

    }
}
