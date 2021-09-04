using Microsoft.AspNetCore.SignalR;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string name, string message)
        {
            string fullName = $"[{DateTime.Now.ToString("MMM-dd HH:mm:ss")}].[{UserHandler.ConnectedIds.Count}].|.{name}";
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", fullName, message);
        }

        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}