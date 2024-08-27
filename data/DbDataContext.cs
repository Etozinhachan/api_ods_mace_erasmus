using Microsoft.EntityFrameworkCore;
using api_ods_mace_erasmus.models;

namespace api_ods_mace_erasmus.data;

public class DbDataContext : DbContext
{
    public DbDataContext(DbContextOptions<DbDataContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Activity> Activities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.submitted_activities)
            .WithOne(e => e.submited_by_user)
            .HasForeignKey(e => e.user_id)
            .HasPrincipalKey(e => e.id);

/*
        modelBuilder.Entity<UserPrompt>()
            .Property(u => u.prompt_number)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<ChatSucessfullResponse>()
            .Property(csr => csr.response_number)
            .ValueGeneratedOnAdd();
*/
        base.OnModelCreating(modelBuilder); 
    }
}