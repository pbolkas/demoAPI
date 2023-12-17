using System.Net;

namespace DemoAPI.WebClient
{
  
  public interface IWebClient
  {
    Task<HttpResponseMessage> GetRequest(string endpoint, string parameters);
  }

  public class WebClient : IWebClient
  {

    private string _baseUrl {get;set;}

    public WebClient(string url)
    {
      _baseUrl = url;
    }

    public async Task<HttpResponseMessage> GetRequest(string endpoint, string parameters)
    {
      using (HttpClient client = new HttpClient())
      {
        var response = await client.GetAsync($"{_baseUrl}{endpoint}?{parameters}");
        
        return response;
      }

    }

  }
}