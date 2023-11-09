using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreRelationShip;

internal class User
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public List<Note> Notes { get; set; } = new List<Note>();
}

internal class Note
{
    public int NoteId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public User User { get; set; }

    public int FUserId { get; set; }
}

internal class TestingDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;port=3306;database=entityframeworkdb;user=vector;password=K/]zjUT)({?Xbdy?<+YEpsNzB38,*0$rc7DiAqvL",
            new MariaDbServerVersion(new Version(11, 1, 0)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(x => x.Notes)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.FUserId)
            .IsRequired();
    }
}
