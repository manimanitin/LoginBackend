using LoginBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginBackend;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions option) : base(option)
    {
    }
    public DbSet<Decks> Decks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var converter = new ValueConverter<int[], string>(
                   v => string.Join(";", v),
                   v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(val => int.Parse(val)).ToArray());

        modelBuilder.Entity<Decks>()
                .Property(e => e.DeckList)
                .HasConversion(converter);
        modelBuilder.Entity<Decks>()
               .Property(e => e.ExtraDeckList)
               .HasConversion(converter);
        base.OnModelCreating(modelBuilder);
    }

}

