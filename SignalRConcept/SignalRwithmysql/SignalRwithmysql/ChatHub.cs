using Microsoft.AspNetCore.SignalR;
using Dapper;
using System.Threading.Tasks;
using crudoperation.utility;

namespace SignalRwithmysql
{
    public class ChatHub : Hub
    {
        private readonly DBGateway _DBGateway;

        public ChatHub(DBGateway dbGateway)
        {
            _DBGateway = dbGateway;
        }

        public async Task SendMessage(string username, string message)
        {
            var connectionId = Context.ConnectionId;

            var query = "INSERT INTO md_signalr (username, message, connectionid) VALUES (@Username, @Message, @ConnectionId)";
            var par = new DynamicParameters();
            par.Add("@Username", username);
            par.Add("@Message", message);
            par.Add("@ConnectionId", connectionId);
            await _DBGateway.ExeQuery(query, par);

            await Clients.All.SendAsync("ReceiveMessage", username, message, connectionId);
        }
    }
}
