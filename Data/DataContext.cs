using MarqueesAssistant.API.Controllers;
using MarqueesAssistant.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options): base(options)
        {
            
        }

        public DbSet<Value> Values { get; set; }

        public DbSet<Marquee> Marquees { get; set;}
        public DbSet<Event> Events { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Worker> Workers { get; set;}
        public DbSet<Message> Messages { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Breakdown> Breakdowns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasOne(w => w.Sender)
                                        .WithMany(m => m.MessagesSent);

            modelBuilder.Entity<Message>().HasOne(w => w.Recipient)
                                        .WithMany(m => m.MessagesRecived);
                                        
        }

    }
}