namespace DemoAPI.Models
{
  public class CountryModel
  {
    public string CommonName {get;set;}
    public string Capital {get;set;}
    public IEnumerable<string> Borders {get;set;}
  }
}