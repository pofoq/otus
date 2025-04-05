using FileService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileService.DAL;

public class FileDbContext(DbContextOptions<FileDbContext> options) : DbContext(options)
{
    public DbSet<FileEntity> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FileEntity>()
            .HasKey(f => f.Id);
    }
}
