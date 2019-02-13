using System;
using System.Collections.Generic;

namespace Planner
{
    public class Ticket
    {
        public Guid TicketId { get; }
        public TicketStatus Status { get; set; }
        public int Points { get; private set; }

        public Sprint IterationPlanned { get; set; }
        public List<User> Users { get; set; }

        public Ticket(Guid ticketId)
        {
            TicketId = ticketId;
            Status = TicketStatus.New;
            Users = new List<User>();
        }

        public void AddPlannedEffort(int points)
        {
            Points = points;
        }
    }
}