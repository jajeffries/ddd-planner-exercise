using NUnit.Framework;
using Planner;

namespace Tests
{
    public class PlannerTests
    {

        [Test]
        public void TestUpdatingTicketStatus()
        {
            var role = new Role(new [] { Permission.UpdateTicketStatus });
            var user = new User(role);
            user.Login();
            var planningService = new PlanningService(new HttpContext());
            var ticket = planningService.NewTicket();
            planningService.UpdateTicketStatus(user, ticket.TicketId, TicketStatus.InProgress);
            Assert.That(ticket.Status, Is.EqualTo(TicketStatus.InProgress));
        }


        [Test]
        public void TestAssignTicketToSprint()
        {
            var role = new Role(new [] { Permission.AssignSprint });
            var user = new User(role);
            user.Login();
            var planningService = new PlanningService(new HttpContext());
            var sprint = planningService.CreateSprint(10);
            sprint.Start();
            var ticket = planningService.NewTicket();
            planningService.AssignToCurrentIteration(user, ticket);
            Assert.That(ticket.IterationPlanned, Is.EqualTo(sprint));
        }

        [Test]
        public void TestSprintCapacity()
        {
            var role = new Role(new [] { Permission.AssignSprint });
            var user = new User(role);
            user.Login();
            var httpContext = new HttpContext();
            var planningService = new PlanningService(httpContext);
            var sprint = planningService.CreateSprint(10);
            sprint.Start();
            var ticket1 = planningService.NewTicket();
            ticket1.AddEstimate(3);
            planningService.AssignToCurrentIteration(user, ticket1);
            var ticket2 = planningService.NewTicket();
            ticket2.AddEstimate(3);
            planningService.AssignToCurrentIteration(user, ticket2);
            var ticket3 = planningService.NewTicket();
            ticket3.AddEstimate(5);
            planningService.AssignToCurrentIteration(user, ticket3);
            Assert.That(httpContext.StatusCode, Is.EqualTo(302));
            Assert.That(httpContext.Path, Is.EqualTo("/iterations/"));
        }
    }
}