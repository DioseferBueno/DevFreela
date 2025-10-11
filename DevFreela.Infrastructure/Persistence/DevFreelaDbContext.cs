using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserrSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Skill>(e =>
            {
                e.HasKey(e => e.Id);
            });

            builder.Entity<UserSkill>(e =>
            {
                e.HasKey(us => us.Id);
                e.HasOne(e => e.Skill)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(u => u.IdUSkill)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ProjectComment>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasOne(p => p.Project)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(p => p.IdProject)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(p => p.IdUser);
            });

            builder.Entity<User>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasMany(u => u.Skills)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.IdUser);
            });

            builder.Entity<Project>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasOne(p => p.Freelancer)
                .WithMany(f => f.FreelanceProjects)
                .HasForeignKey(p => p.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Client)
                .WithMany(c => c.OwnedProjects)
                .HasForeignKey(c => c.IdClient)
                .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }

    }
}
