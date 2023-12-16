namespace DemoAPI.WebClient
{
  
  public interface IWebClient
  {

  }

  public class WebClient
  {

    private string _url {get;set;}

    public WebClient(string url)
    {
      _url = url;
    }

    public async Task GetRequest(string parameters)
    {
      using (HttpClient client = new HttpClient())
      {
        
      }

    }

  }
}