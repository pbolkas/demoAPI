using DemoAPI.Configurations;
using DemoAPI.Entities;
using Microsoft.Extensions.Options;

namespace DemoAPI.Services
{
  public interface ICountriesService
  {
    Task<CountryDTO> GetCountryAsync(string countryName);
  }

  public class CountriesService : ICountriesService
  {

    private readonly IConfiguration _apiConfiguration;

    public CountriesService(IConfiguration config)
    {
      _apiConfiguration = config;
    }

    public async Task<CountryDTO> GetCountryAsync(string countryName)
    {

      await Task.Delay(100);

      string URL = _apiConfiguration["CountriesAPI"];
      
      return new CountryDTO();
    }

  }

  private async Task<CountryDTO> 
}