using Microsoft.AspNetCore.SignalR;

namespace Socket
{
    public class RoomHub : Hub
    {

        public async Task JoinRoom(string roomName)
        {
            Console.WriteLine(Context.ConnectionId + " Joined room " + roomName);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("onJoin", "blabla");
        }

        public async Task LeaveRoom(string roomName)
        {
            Console.WriteLine(Context.ConnectionId + " Left room " + roomName);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("onLeave");
        }

        public async Task StartRoom(string roomName)
        {
            Console.WriteLine(Context.ConnectionId + " Started room " + roomName);
            await Clients.Group(roomName).SendAsync("onStart");
        }

        public async Task ListReady(string roomName)
        {
            Console.WriteLine(Context.ConnectionId + " ListReady room " + roomName);
            await Clients.Group(roomName).SendAsync("onListReady");
        }

        public async Task SendMessageToGroup(string roomName, string message)
        {
            await Clients.Group(roomName).SendAsync("message", message);
        }

    }
}
