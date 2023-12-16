namespace DemoAPI.Entities
{
  public class CountryDTO
  {
    public string CommonName {get;set;}
    public string Capital {get;set;}
    public IEnumerable<string> Borders {get;set;}    
  }
}