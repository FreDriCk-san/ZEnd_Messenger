using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zeAPI
{
    public class Connection
    {
        internal static HubConnection hub;
        internal static IHubProxy hubProxy;

        public static void Connect(Users user, Action<Messages> newMessage, Action<Chats> newChat)
        {
            if (hub == null)
            {
                hub = new HubConnection(url:"http://localhost:58040/");
                hubProxy = hub.CreateHubProxy("ZEnd_Messenger");
                hub.Start().Wait();
                hubProxy.On("newMessage", newMessage);
                hubProxy.On("newChat", newChat);
                hubProxy.Invoke("connect", user.Id, user.GetChats());
            }
        }
        public static void Disconnect()
        {
            hub.Stop();
        }

    }
}
