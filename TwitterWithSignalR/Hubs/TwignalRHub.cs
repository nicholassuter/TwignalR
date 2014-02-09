using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;

namespace SignalRChat
{
    public class TwignalRHub : Hub
    {
        static int nbMessages;
        public void SendToHub(string name, string message)
        {
            var filteredMessage = message.Replace(',', ' ').Replace('.', ' ').Replace('!', ' ').Replace('?', ' ');
            var destinataires = filteredMessage.Split(' ').Where(w => w.StartsWith("@")).ToArray();

            nbMessages++;
            Clients.All.sendToClients(name, message, nbMessages, destinataires);
        }
    }
}