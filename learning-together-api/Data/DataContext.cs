namespace learning_together_api.Data
{
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Discipline> Disciplines { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Workshop> Workshops { get; set; }

        public virtual DbSet<WorkshopAttendee> WorkshopAttendees { get; set; }

        public virtual DbSet<UserInterest> UserInterests { get; set; }

        public virtual DbSet<WorkshopTopic> WorkshopTopics { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

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
                .WithMany(e => e.WorkshopsAttending)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<WorkshopAttendee>()
                .HasOne(e => e.Workshop)
                .WithMany(e => e.WorkshopAttendees)
                .HasForeignKey(e => e.WorkshopId);

            modelBuilder.Entity<WorkshopTopic>()
                .HasKey(e => new { e.WorkshopId, e.DisciplineId });
            modelBuilder.Entity<WorkshopTopic>()
                .HasOne(e => e.Workshop)
                .WithMany(e => e.WorkshopTopics)
                .HasForeignKey(e => e.WorkshopId);
            modelBuilder.Entity<WorkshopTopic>()
                .HasOne(e => e.Discipline)
                .WithMany(e => e.WorkshopTopics)
                .HasForeignKey(e => e.DisciplineId);

            this.SeedCategories(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedLocations(modelBuilder);
        }

        private void SeedLocations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasData(
                new Location(2, "London"),
                new Location(1, "Brooklyn"),
                new Location(3, "Edinburgh"),
                new Location(4, "Dublin"),
                new Location(5, "Denver"),
                new Location(6, "Dallas")
            );
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role(1, "Creative Technologist"),
                new Role(2, "Frontend"),
                new Role(3, "Backend"),
                new Role(4, "Fullstack"),
                new Role(5, "Design"),
                new Role(6, "Product"),
                new Role(7, "Delivery"),
                new Role(8, "Leadership")
            );
        }

        private void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category(1, "Professional Development"),
                new Category(2, "Emotional Intelligence"),
                new Category(3, "Teamwork"),
                new Category(4, "Leadership"),
                new Category(5, "Design"),
                new Category(6, "Analytics"),
                new Category(7, "Culture"),
                new Category(8, "Agile / Lean"),
                new Category(9, "Artificial Intelligence"),
                new Category(10, "Technology")
            );
        }
    }
}