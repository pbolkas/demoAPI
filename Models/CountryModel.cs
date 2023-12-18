using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI.Models
{
  public class CountryModel
  {
    [Key]
    public string CommonName {get;set;}
    public string Capital {get;set;}
    public IEnumerable<Border> Borders {get;set;}
  }

  public class Border 
  {
    [Key]
    public int Id {get;set;}
    public string BorderName {get;set;}
  }

}