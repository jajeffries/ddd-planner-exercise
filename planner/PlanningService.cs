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
        }
    }

    public class Sprint
    {
        public bool IsStarted { get; private set; } = false;

        private List<Ticket> tasks = new List<Ticket>();
        private int SprintSize;

        public Sprint(int size)
        {
            this.SprintSize = size;
        }

        public void Start()
        {
            IsStarted = true;
        }

        public void AddTask(Ticket task, HttpContext httpContext)
        {
            var iterationPointsUsed = tasks.Sum(t => t.Points);
            if(iterationPointsUsed + task.Points > SprintSize)
            {
                httpContext.Redirect(302, "/iterations/");
            }
            tasks.Add(task);
        }

    }

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

    public enum TicketStatus
    {
    	New,
    	InProgress,
    	Closed
    }

    public enum Permission
    {
        UpdateTicketStatus,
        AssignSprint
    }

    public class User
    {
        private readonly Role role;
        private bool IsLoggedIn = false;

        public User(Role role)
        {
            this.role = role;
        }

        public bool IsAuthenticated()
        {
            return this.IsLoggedIn;
        }

        public void Login()
        {
            this.IsLoggedIn = true;
        }

        public bool HasPermission(Permission permission)
        {
            return this.role.AllowedTo(permission);
        }
    }

    public class Role
    {
        private readonly IEnumerable<Permission> permissions;

        public Role(IEnumerable<Permission> permissions)
        {
            this.permissions = permissions;
        }

        public bool AllowedTo(Permission permission)
        {
            return this.permissions.Any(p => p == permission);
        }
    }

    public class HttpContext
    {
        public int StatusCode {get; private set;}
        public string Path {get; private set;}
        public void Redirect(int statusCode, string path)
        {
            StatusCode = statusCode;
            Path = path;
        }
    }
}

