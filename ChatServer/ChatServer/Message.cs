using System;


namespace ChatServer
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime MessageDate { get; set; } = DateTime.Now;
        public string Who { get; set; }
        public string Text { get; set; }
    }
}