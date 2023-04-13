using Microsoft.AspNetCore.SignalR;

namespace Socket
{
    public class RoomHub : Hub
    {

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("New user has joined the room");
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("User left the room");
        }

        public async Task SendMessageToGroup(string roomName, string message)
        {
            await Clients.Group(roomName).SendAsync(message);
        }

    }
}
