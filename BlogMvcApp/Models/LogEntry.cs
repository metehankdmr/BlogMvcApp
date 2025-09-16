using System;

namespace BlogMvcApp.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "";
        public string Text { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
