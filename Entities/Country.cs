namespace DemoAPI.Entities
{
  public class CountryDTO
  {
    public NameDTO Name {get;set;}
    public IEnumerable<string> Capital {get;set;}
    public IEnumerable<string> Borders {get;set;}    
  }

  public class NameDTO
  {
    public string Common {get;set;}
    public string Official {get;set;}
    public IDictionary<string, object> NativeName {get;set;}
  }

}