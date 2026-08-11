using K9UnitApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace K9UnitApi.Data
{
    public class K9UnitDbContext : DbContext
    {
        public K9UnitDbContext(DbContextOptions<K9UnitDbContext> options) : base(options)
        {
        }
        public DbSet<Handler> Handlers => Set<Handler>();
        public DbSet<Dog> Dogs => Set<Dog>();
        public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>().
                HasOne(e => e.Handler).
                WithOne(e => e.Dog).
                HasForeignKey<Dog>(e => e.HandlerId).
                OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Dog>().
                HasMany(e => e.TrainingSessions).
                WithOne(e => e.Dog).
                HasForeignKey(e => e.DogId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
