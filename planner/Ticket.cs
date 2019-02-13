using System;

namespace Planner
{
    public class Ticket
    {
        public Guid TicketId { get; }
        public TicketStatus Status { get; set; }
        public int Points { get; private set; }

        public Sprint IterationPlanned { get; set; }

        public Ticket(Guid ticketId)
        {
            TicketId = ticketId;
            Status = TicketStatus.New;
        }

        public void AddEstimate(int points)
        {
            Points = points;
        }
    }
}