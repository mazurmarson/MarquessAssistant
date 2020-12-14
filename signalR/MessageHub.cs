using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace MarqueesAssistant.API.signalR
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        // private readonly IMessRepo _repo;
        // public MessageHub(IMessRepo repo)
        // {
        //     _repo = repo;
        // }

        public static List<WorkerIdAndIdConnection> clientsList = new List<WorkerIdAndIdConnection>();

        public string GetConnectionId(string test)
        {
            WorkerIdAndIdConnection workerIdAndIdConnection = new WorkerIdAndIdConnection();
            workerIdAndIdConnection.ConnectionId = Context.ConnectionId;
            workerIdAndIdConnection.WorkerId = test;
            clientsList.Add(workerIdAndIdConnection);

          return  test;
        }

        public string GetUserIdConnection(string id)
        {
          WorkerIdAndIdConnection workerIdAndIdConnection = clientsList.FirstOrDefault(x => x.WorkerId == id);
          if(workerIdAndIdConnection == null)
          {
              return "Uzytkownik nie jest zalogowany";
          }
          return workerIdAndIdConnection.ConnectionId;
        }

        public void DeleteUserFromList(string id)
        {
          WorkerIdAndIdConnection workerIdAndIdConnection = clientsList.FirstOrDefault(x => x.WorkerId == id);
          clientsList.Remove(workerIdAndIdConnection);
        }
    }
}