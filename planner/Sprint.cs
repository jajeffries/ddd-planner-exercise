using System.Collections.Generic;

namespace Planner
{
    public class Sprint
    {
        public bool IsStarted { get; private set; } = false;

        public List<Task> Tasks = new List<Task>();
        public int SprintSize;

        public Sprint(int size)
        {
            this.SprintSize = size;
        }

        public void Start()
        {
            IsStarted = true;
        }

        public void AddTask(Task task, HttpContext httpContext)
        {
            Tasks.Add(task);
        }
    }
}