using System;
using System.Collections.Generic;

namespace Planner
{
    public class Task
    {
        public Guid TicketId { get; }
        public TaskStatus Status { get; set; }
        public int Points { get; private set; }

        public Sprint IterationPlanned { get; set; }
        public List<User> Users { get; set; }

        public Task(Guid ticketId)
        {
            TicketId = ticketId;
            Status = TaskStatus.New;
            Users = new List<User>();
        }

        public void AddPlannedEffort(int points)
        {
            Points = points;
        }
    }
}