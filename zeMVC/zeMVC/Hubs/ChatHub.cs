using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace zeMVC.Hubs
{
    [HubName("ZEnd_Messenger")]
    public class ChatHub : Hub
    {

        internal static Dictionary<string, int> users = new Dictionary<string, int>();

        public void connect(int userId, List<Models.Chats> chats)
        {
            foreach (var chat in chats)
            {
                Groups.Add(Context.ConnectionId, chat.Id.ToString());
            }
            users.Add(Context.ConnectionId, userId);
        }

        public void newMessage(Models.Chats chats, Models.Messages messages)
        {
            Clients.Group(chats.Id.ToString()).newMessage(messages);
        }

        public void newChat(Models.Chats chats, List<Models.Users> users)
        {
            foreach (var user in users)
            {
                foreach (var us in ChatHub.users.Where(u => u.Value == user.Id))
                {
                    Groups.Add(us.Key, chats.Id.ToString()).Wait();
                }
            }
            Clients.Group(chats.Id.ToString()).newChat(chats);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (users.Keys.Contains(Context.ConnectionId))
            {
                users.Remove(Context.ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
        }

    }
}