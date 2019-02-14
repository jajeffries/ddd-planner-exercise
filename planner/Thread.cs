using System;
using System.Collections.Generic;

namespace planner
{
    public class Thread
    {
        public string Username { get; }
        public string Title { get; }
        public string Message { get; }
        public DateTime PostDate { get; set; }
        public Guid ThreadId { get; set; }
        public List<Comment> Comments { get; set; }

        public Thread(string username, string title, string message)
        {
            ThreadId = Guid.NewGuid();
            Username = username;
            Title = title;
            Message = message;
            PostDate = DateTime.Now;
            Comments = new List<Comment>();
        }
    }
}