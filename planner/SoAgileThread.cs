using System;

namespace planner
{
    // We wanted to use "Thread" but there was a naming conflict
    public class SoAgileThread
    {
        public string Username { get; }
        public string Title { get; }
        public string Message { get; }
        public DateTime PostDate { get; set; }

        public SoAgileThread(string username, string title, string message)
        {
            Username = username;
            Title = title;
            Message = message;
            PostDate = DateTime.Now;
        }
    }
}