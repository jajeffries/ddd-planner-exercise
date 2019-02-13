using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner
{
    public class PlanningService
    {
    	private readonly Dictionary<Guid, Ticket> tickets = new Dictionary<Guid, Ticket>();
        private readonly List<Sprint> sprints = new List<Sprint>();
        private readonly HttpContext httpContext;

        public PlanningService(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

    	public Ticket NewTicket()
    	{
    		var ticketId = Guid.NewGuid();
    		var ticket = new Ticket(ticketId);
    		tickets.Add(ticketId, ticket);
    		return ticket;
    	}

    	public void UpdateTicketStatus(User user, Guid ticketId, TicketStatus status)
    	{
            if(user.IsAuthenticated() && user.HasPermission(Permission.UpdateTicketStatus))
            {  
    		  tickets[ticketId].Status = status;
            }
    	}

        public Sprint CreateSprint(int capacity)
        {
            var sprint = new Sprint(capacity);
            sprints.Add(sprint);
            return sprint;
        }

        public void AssignToCurrentIteration(User user, Ticket ticket)
        {
            var currentSprint = sprints.First(iteration => iteration.IsStarted);
            ticket.IterationPlanned = currentSprint;
            currentSprint.AddTask(ticket, this.httpContext);
            var iterationPointsUsed  = currentSprint.Tasks.Sum(t => t.Points);
            if (iterationPointsUsed + ticket.Points > currentSprint.SprintSize)
            {
                httpContext.Redirect(302, "/iterations/");
            }
            currentSprint.Tasks.Add(ticket);
        }

        public void UpdateTicketWithUser(Ticket ticket, User user)
        {
            var theTicket = GetTaskById(ticket.TicketId);
            theTicket.Users.Add(user);
        }

        public Ticket GetTaskById(Guid id)
        {
            return tickets.First(t => t.Key.Equals(id)).Value;
        }
    }
}

