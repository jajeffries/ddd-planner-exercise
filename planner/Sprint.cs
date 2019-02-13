using System.Collections.Generic;

namespace Planner
{
    public class Sprint
    {
        public bool IsStarted { get; private set; } = false;

        public List<Ticket> Tasks = new List<Ticket>();
        public int SprintSize;

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
            Tasks.Add(task);
        }
    }
}