using System;
using Microsoft.AspNet.SignalR;
using PlanningPoker.Models;

namespace PlanningPoker
{
    public class PokeR
    {
        private IHubContext _context;

        public PokeR(IHubContext context)
        {
            _context = context;
        }

        public void PlayerJoined(Guid gameId, Player name)
        {
            _context.Clients.Group(gameId.ToString()).playerJoined(name);
        }
    }
}