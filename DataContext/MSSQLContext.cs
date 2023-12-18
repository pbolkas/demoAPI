using DemoAPI.Contracts.Responses;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.DataContext
{
  public class MSSQLContext : DbContext
  {

    public DbSet<CountryModel> Countries {get;set;}

    public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options)
    {
      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Border>(entity =>{
        entity.HasKey(e => e.Id);
        entity.Property( e => e.Id).ValueGeneratedOnAdd();        
      });

    }

  }
}