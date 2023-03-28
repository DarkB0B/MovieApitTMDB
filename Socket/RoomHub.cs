using Microsoft.AspNetCore.SignalR;

namespace Socket
{
    public class RoomHub : Hub
    {
        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public Task SendMessageToGroup(string roomName, string message)
        {
            return Clients.Group(roomName).SendAsync(message);
        }

    }
}
