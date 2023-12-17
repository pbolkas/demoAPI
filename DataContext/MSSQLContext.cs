using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.DataContext
{
  public class MSSQLContext : DbContext
  {

    public DbSet<CountryModel> Countries {get;set;}

    public MSSQLContext() : base()
    {
      
    }

  }
}