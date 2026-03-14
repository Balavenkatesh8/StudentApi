using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentApi.Models;
using System.Data;

public class AppDbContext : DbContext
{

    private readonly ILogger<AppDbContext> _logger;
    public AppDbContext(DbContextOptions<AppDbContext> options,
         ILogger<AppDbContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    //public object? Countries;
    public DbSet<Country> Country { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentEducation> StudentEducations { get; set; }
    public DbSet<StudentDocument> StudentDocuments { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Branch> Branch { get; set; }
    public DbSet<University> University { get; set; }
    public DbSet<Serives> serives { get; set; }
    public DbSet<Address>Addresses { get; set; }
    public DbSet<DocumentMasterlist> DocumentMasterlist { get; set; }
    public DbSet<Mentor> Mentors { get; set; }
    public DbSet<StudentMentorLink> StudentMentorLinks { get; set; }

    public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            
            if (entry.State == EntityState.Added)
            {
                
                entry.Entity.CreatedBy = "Admin";
            }
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedAt = DateTime.UtcNow;
                entry.Entity.LastModifiedBy = "Admin";
            }
            _logger.LogInformation(
                    "NEW RECORD CREATED | Entity: {Entity} | CreatedBy: {User} | Time: {Time}",
                    entry.Entity.GetType().Name,
                    entry.Entity.CreatedBy,
                    DateTime.UtcNow

                );

            if (entry.State == EntityState.Modified)
            {
                // Update audit fields
                entry.Entity.LastModifiedAt = DateTime.UtcNow;
                entry.Entity.LastModifiedBy = "admin"; // or get current user

                _logger.LogInformation(
                    "RECORD UPDATED | Entity: {Entity} | ModifiedBy: {User} | Time: {Time}",
                    entry.Entity.GetType().Name,
                    entry.Entity.LastModifiedBy,
                    DateTime.UtcNow
                );
            }

            if (entry.State == EntityState.Deleted)
            {
                _logger.LogInformation(
                    "RECORD DELETED | Entity: {Entity} | Time: {Time}",
                    entry.Entity.GetType().Name,
                    DateTime.UtcNow
                );
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}















