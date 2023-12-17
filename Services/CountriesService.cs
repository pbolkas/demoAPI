using System.Text.Json;
using DemoAPI.Entities;
using DemoAPI.Entities.Exceptions;
using DemoAPI.WebClient;

namespace DemoAPI.Services
{
  public interface ICountriesService
  {
    Task<CountryDTO> GetCountryAsync(string countryName);
  }

  public class CountriesService : ICountriesService
  {

    private readonly IConfiguration _apiConfiguration;

    private readonly IWebClient _webClient;

    public CountriesService(IConfiguration config)
    {
      _apiConfiguration = config;
      _webClient = new WebClient.WebClient(_apiConfiguration["CountriesAPI"]);
    }

    public async Task<CountryDTO> GetCountryAsync(string countryName)
    {
      var response = await _webClient.GetRequest($"name/{countryName}", "fields=name,capital,borders");
    
      var strresponse = await response.Content.ReadAsStringAsync();

      if(response.StatusCode != System.Net.HttpStatusCode.OK)
      {
        throw new CountriesAPIException(strresponse, response.StatusCode);
      }

      JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
      var countries = JsonSerializer.Deserialize<CountryDTO[]>(strresponse, options);
      
      return countries[0];
    }

  }
}