using System.Threading.Tasks;
using System.Web.UI;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using TwitterWithSignalR.Models;

namespace SignalRChat
{
    public class TwignalRHub : Hub
    {
        static int _nbMessages;

        private readonly static ConnectionMapping<string> Connections = new ConnectionMapping<string>();

        public void SendToHub(string name, string message)
        {
            _nbMessages++;

            if (message.StartsWith("d "))
            {
                SendDm(name, message, _nbMessages);
            }
            else
            {
                var filteredMessage = message.Replace(',', ' ').Replace('.', ' ').Replace('!', ' ').Replace('?', ' ');
                var destinataires = filteredMessage.Split(' ').Where(w => w.StartsWith("@")).ToArray();

                Clients.All.sendToClients(name, message, _nbMessages, destinataires);
            }
            
        }

        private void SendDm(string from, string message, int messageId)
        {
            string[] words = message.Split(' ');
            if (words.Count() > 2)
            {
                string sendTo = words[1];
                string connectionId = Connections.GetConnectionFromKey(sendTo);
                if (!string.IsNullOrEmpty(connectionId))
                {
                    string messageToSend = string.Join(" ", words.Skip(2));
                    Clients.Client(connectionId).sendDm(from, messageToSend, messageId);
                    if (connectionId != Context.ConnectionId)
                    {
                        Clients.Caller.sendDm(from, messageToSend, messageId);                        
                    }
                }
                else
                {
                    Clients.Caller.systemNotification(sendTo + " n'est pas connecté.", "#FF0000");
                }
            }
        }

        public void NotifyConnection(string userName)
        {
            Connections.Add(userName, Context.ConnectionId);
            Clients.All.systemNotification(userName + " vient de se connecter.", "#81F781");
        }

        public override Task OnDisconnected()
        {
            Connections.Remove(Context.ConnectionId);

            return base.OnDisconnected();
        }
    }
}