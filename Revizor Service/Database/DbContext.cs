using Microsoft.EntityFrameworkCore;
using Revizor_Service.Database.Models;

namespace Revizor_Service.Database;

public class AppDbContext (DbContextOptions<AppDbContext> options) : DbContext(options)
{

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<Record> Records => Set<Record>();
    public DbSet<PlanAssigment> PlanAssigments => Set<PlanAssigment>();
    public DbSet<Report> Reports => Set<Report>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Role>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Id).UseIdentityAlwaysColumn();
            e.Property(r => r.Name).HasMaxLength(50).IsRequired();
            e.HasIndex(r => r.Name).IsUnique();
        });

        mb.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");
            e.Property(u => u.Name).HasMaxLength(100).IsRequired();
            e.HasIndex(u => u.Name).IsUnique();
            e.Property(u => u.Password).IsRequired();
            e.Property(u => u.CreatedAt).HasDefaultValueSql("now()");
            e.Property(u => u.RefreshToken).HasMaxLength(50);
        });

        mb.Entity<Facility>(e =>
        {
            e.HasKey(f => f.Id);
            e.Property(f => f.Id).HasDefaultValueSql("gen_random_uuid()");
            e.Property(f => f.Title).HasMaxLength(150).IsRequired();
            e.Property(f => f.Location).HasMaxLength(150).IsRequired();
            e.HasIndex(f => new { f.Title, f.Location }).IsUnique();
        });

        mb.Entity<Plan>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");
            e.Property(p => p.Name).HasMaxLength(100).IsRequired();
            e.HasIndex(p => p.Name).IsUnique();
            e.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
            e.Property(p => p.PeriodEnd).HasDefaultValueSql("(now()::date + interval '1 months')");
            e.Property(p => p.IsArchived).HasDefaultValue(false);

            e.HasIndex(p => p.IsArchived)
                .HasFilter("\"IsArchived\" = true")
                .IsUnique()
                .HasDatabaseName("idx_only_true");
        });

        mb.Entity<Record>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Id).HasDefaultValueSql("gen_random_uuid()");
        });

        mb.Entity<PlanAssigment>(e =>
        {
            e.HasKey(pa => pa.Id);
            e.Property(pa => pa.Id).HasDefaultValueSql("gen_random_uuid()");
        });

        mb.Entity<Report>(e =>  
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Id).HasDefaultValueSql("gen_random_uuid()");
            e.Property(r => r.UserId).IsRequired();
            e.Property(r => r.PlanId).IsRequired();
            e.Property(r => r.FileName).HasMaxLength(200).IsRequired();
            e.Property(r => r.FilePath).HasMaxLength(500).IsRequired();
            e.Property(r => r.CreatedAt).HasDefaultValueSql("now()");
        });
}
}