using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer
{
    public class ChatContext : DbContext
    {
        public ChatContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=A-104-12;Database=ChatDb;Trusted_Connection=True");
        }
    }
}
