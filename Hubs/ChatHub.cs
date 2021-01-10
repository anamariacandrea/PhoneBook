using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PhoneBook.Hubs;
using Microsoft.AspNetCore.Authorization;

namespace PhoneBook.Hubs
{
    public class ChatHub: Hub
    {
        [Authorize]
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }
    }
}
