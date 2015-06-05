using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SynchronicWorld.Models
{
    public class DbEntities : DbContext
    {
        public DbSet<User> UserTable { get; set; }
        public DbSet<Contribution> ContributionTable { get; set; }
        public DbSet<ContributionType> ContributionTypeTable { get; set; }
        public DbSet<Event> EventTable { get; set; }
        public DbSet<EventType> EventTypeTable { get; set; }
        public DbSet<Friend> FriendsTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //one‐to‐many between event and eventType
            modelBuilder.Entity<Event>()
                .HasRequired<EventType>(e => e.EventType)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<Contribution>()
                .HasRequired<ContributionType>(c => c.ContributionType)
                .WithMany(c => c.Contributions)
                .HasForeignKey(c => c.ContributionTypeId);

            modelBuilder.Entity<User>().HasMany<Friend>(u => u.Friends).WithMany(u => u.Users).Map(cs =>
            {
                cs.MapLeftKey("UserId");
                cs.MapRightKey("FriendId");
                cs.ToTable("FriendUSer");
            });
        }
    }
}