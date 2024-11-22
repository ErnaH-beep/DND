using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<PersonBase> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonBase>()
                .HasDiscriminator<string>("Role")
                .HasValue<Employee>("Employee")
                .HasValue<Manager>("Manager");

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired();
                entity.Property(t => t.Status).IsRequired();
            });

            modelBuilder.Entity<ProjectModel>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.ManagerId).IsRequired();
            });
        }
    }
}
