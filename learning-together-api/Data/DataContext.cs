namespace learning_together_api.Data
{
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInterest>()
                .HasKey(e => new { e.UserId, e.DisciplineId });
            modelBuilder.Entity<UserInterest>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserInterests)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<UserInterest>()
                .HasOne(e => e.Discipline)
                .WithMany(e => e.UserInterests)
                .HasForeignKey(e => e.DisciplineId);

            modelBuilder.Entity<WorkshopAttendee>()
                .HasKey(e => new { e.UserId, e.WorkshopId });
            modelBuilder.Entity<WorkshopAttendee>()
                .HasOne(e => e.User)
                .WithMany(e => e.WorkshopAttendees)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<WorkshopAttendee>()
                .HasOne(e => e.Workshop)
                .WithMany(e => e.WorkshopAttendees)
                .HasForeignKey(e => e.WorkshopId);
        }
    }
}