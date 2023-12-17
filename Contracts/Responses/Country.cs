namespace DemoAPI.Contracts.Responses
{
  public class Country
  {
    public string CommonName {get;set;}
    public string Capital {get;set;}
    public IEnumerable<string> Borders {get;set;}    
  }

}