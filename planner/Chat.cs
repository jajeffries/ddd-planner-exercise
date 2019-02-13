using System;
using System.Collections.Generic;

namespace planner
{
    public class Chat
    {
        public Guid Id { get; }
        public List<SoAgileThread> Threads { get; set; }
        public DateTime PostDate { get; }
        public string CreatedBy;

        public Chat(Guid id, string createdBy)
        {
            Id = id;
            CreatedBy = createdBy;
            PostDate = DateTime.Now;
            Threads = new List<SoAgileThread>();
        }
    }
}