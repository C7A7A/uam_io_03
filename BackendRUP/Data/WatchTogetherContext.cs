using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WatchTogetherAPI.Models;

namespace WatchTogetherAPI.Data
{
    public class WatchTogetherContext : IdentityDbContext<AppUser>
    {
        public WatchTogetherContext(DbContextOptions<WatchTogetherContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Lobby>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (LobbyStatus)Enum.Parse(typeof(LobbyStatus), v));
            modelBuilder.Entity<Movies_genres>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<User>()
                .HasMany(e => e.LobbiesParticipated)
                .WithMany(e => e.Participants);

            modelBuilder.Entity<User>()
                .HasMany(e => e.LobbiesHosted)
                .WithOne(e => e.Host)
                .HasForeignKey(e => e.HostId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(e => e.VotedForMovie)
                .WithMany(e => e.alreadyVoted);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PreferredHours> PreferredHours { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movies_genres> Movies_Genres{get; set;}
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<MovieVotes> MovieVotes { get; set; }
        public DbSet<TermProposition> TermPropostions { get; set; }
        public DbSet<Snack> Snacks { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }


    }
}
