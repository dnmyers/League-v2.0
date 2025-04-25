using League.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace League.Server.Data
{
    public class LeagueContext : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options) { }

        public DbSet<Models.League> Leagues { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // League
            modelBuilder.Entity<Models.League>(e =>
            {
                e.ToTable("Leagues");
                e.HasKey(l => l.Id);
                e.Property(l => l.Name).IsRequired().HasMaxLength(100);

                // Relationship: League -> Conferences
                e.HasMany(l => l.Conferences)
                    .WithOne(c => c.League)
                    .HasForeignKey(c => c.LeagueId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Conference
            modelBuilder.Entity<Conference>(e =>
            {
                e.ToTable("Conferences");
                e.HasKey(c => c.Id);
                e.HasIndex(c => c.LeagueId);
                e.Property(c => c.LeagueId).IsRequired();
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);

                // Relationship: Conference -> Divisions
                e.HasMany(c => c.Divisions)
                    .WithOne(d => d.Conference)
                    .HasForeignKey(d => d.ConferenceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Division
            modelBuilder.Entity<Division>(e =>
            {
                e.ToTable("Divisions");
                e.HasKey(d => d.Id);
                e.HasIndex(d => d.ConferenceId);
                e.Property(d => d.ConferenceId).IsRequired();
                e.Property(d => d.Name).IsRequired().HasMaxLength(100);

                // Relationship: Division -> Teams
                e.HasMany(d => d.Teams)
                    .WithOne(t => t.Division)
                    .HasForeignKey(t => t.DivisionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Team
            modelBuilder.Entity<Team>(e =>
            {
                e.ToTable("Teams");
                e.HasKey(t => t.Id);
                e.HasIndex(t => t.DivisionId);
                e.Property(t => t.DivisionId).IsRequired();
                e.Property(t => t.Name).IsRequired().HasMaxLength(100);
                e.Property(t => t.City).IsRequired().HasMaxLength(100);

                // Relationship: Team -> Players
                e.HasMany(t => t.Players)
                    .WithOne(p => p.Team)
                    .HasForeignKey(p => p.TeamId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Player
            modelBuilder.Entity<Player>(e =>
            {
                e.ToTable("Players");
                e.HasKey(p => p.Id);
                e.HasIndex(p => p.TeamId);
                e.Property(p => p.TeamId).IsRequired();
                e.Property(p => p.Name).IsRequired().HasMaxLength(100);
                e.Property(p => p.Position).HasMaxLength(50);
            });
        }
    }
}
