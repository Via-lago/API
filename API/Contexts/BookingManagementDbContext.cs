
using API.Models;
using API.Utility;
using Microsoft.EntityFrameworkCore;
namespace API.Contexts
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(new Role
            {
                Guid =Guid.Parse("02bf5069-f27e-40ba-6f98-08db60bf3fc7"),
                Name = nameof(RoleLevel.User),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Role
            {
                Guid = Guid.Parse("02bf5069-f27e-30ba-6f98-08db60bf3fc7"),
                Name = nameof(RoleLevel.Manager),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Role
            {
                Guid = Guid.Parse("02bf5069-f27e-20ba-6f98-08db60bf3fc7"),
                Name = nameof(RoleLevel.Admin),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });

            builder.Entity<Employee>().HasIndex(e => new { e.nik,e.Email, e.PhoneNumber})
                .IsUnique();

            // Relation between Education and University (1 to many)
            builder.Entity<Education>()
                .HasOne(u => u.University)
                .WithMany(e => e.Educations)
                .HasForeignKey(e => e.UniversityGuid);

            //Relation between Education and Employee (1 to 1)
            builder.Entity<Education>()
                .HasOne(e => e.Employee)
                .WithOne(e => e.Education)
                .HasForeignKey<Education>(e => e.Guid);

            builder.Entity<Room>()
                .HasMany(b => b.Bookings)
                .WithOne(r => r.Room)
                .HasForeignKey(r => r.RoomGuid);

            builder.Entity<AccountRole>()
                .HasOne(a => a.Account)
                .WithMany(ar => ar.accountRoles)
                .HasForeignKey(ar => ar.AccountGuid);

            builder.Entity<Employee>()
                .HasMany(b => b.Bookings)
                .WithOne(r => r.Employee)
                .HasForeignKey(b => b.EmployeeGuid);

            builder.Entity<Role>()
                .HasMany(a => a.AccountRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(a => a.RoleGuid);

            builder.Entity<Account>()
                .HasOne(e => e.Employee)
                .WithOne(a => a.Account)
                .HasForeignKey<Account>(a => a.Guid);
            
        }
    } 
}
