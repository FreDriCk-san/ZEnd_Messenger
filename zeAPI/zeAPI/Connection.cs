using Microsoft.AspNet.SignalR.Client;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zeAPI
{
    public class Connection
    {
        internal static HubConnection hubConnect;
        internal static IHubProxy hubProxy;
        static string url = "http://localhost:58040/";

        public static void Connect(Users user, Action<Messages> newMessage, Action<Chats> newChat)
        {
            if (hubConnect == null)
            {
                hubConnect = new HubConnection(url);
                hubProxy = hubConnect.CreateHubProxy("ZEnd_Messenger");
                //ServicePointManager.DefaultConnectionLimit = 10;
                hubConnect.Start().Wait();
                hubProxy.On("newMessage", newMessage);
                hubProxy.On("newChat", newChat);
                hubProxy.Invoke("connect", user.Id, user.GetChats());
            }
        }

        public static void Disconnect()
        {
            hubConnect.Stop();
        }

    }
}
