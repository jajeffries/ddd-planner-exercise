using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner
{
    public class PlanningService
    {
    	private readonly Dictionary<Guid, Task> tickets = new Dictionary<Guid, Task>();
        private readonly List<Sprint> sprints = new List<Sprint>();
        private readonly HttpContext httpContext;

        public PlanningService(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

    	public Task NewTicket()
    	{
    		var ticketId = Guid.NewGuid();
    		var ticket = new Task(ticketId);
    		tickets.Add(ticketId, ticket);
    		return ticket;
    	}

    	public void UpdateTicketStatus(User user, Guid ticketId, TaskStatus status)
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

        public void AssignToCurrentIteration(User user, Task ticket)
        {
            var currentSprint = sprints.First(iteration => iteration.IsStarted);
            ticket.IterationPlanned = currentSprint;
            var iterationPointsUsed  = currentSprint.Tasks.Sum(t => t.Points);
            if (iterationPointsUsed + ticket.Points > currentSprint.SprintSize)
            {
                httpContext.Redirect(302, "/iterations/");
            }
            currentSprint.Tasks.Add(ticket);
        }

        public void UpdateTicketWithUser(Task ticket, User user)
        {
            var theTicket = GetTaskById(ticket.TicketId);
            theTicket.Users.Add(user);
        }

        public Task GetTaskById(Guid id)
        {
            return tickets.First(t => t.Key.Equals(id)).Value;
        }

        public void DeleteTicket(Task ticket, User user)
        {
            if (user.IsAuthenticated() && user.HasPermission(Permission.RemoveFromIteration))
            {
                var currentSprint = GetCurrentSprint();
                currentSprint.Tasks.Remove(ticket);
                ticket.IterationPlanned = null;
            }
            else
            {
                throw new Exception("Only product owners can remove tickets from the current sprint");
            }
        }

        public Sprint GetCurrentSprint()
        {
            return sprints.First(iteration => iteration.IsStarted);
        }
    }
}

