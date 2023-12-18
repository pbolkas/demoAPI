using System.Text.Json;
using DemoAPI.DataContext;
using DemoAPI.Entities;
using DemoAPI.Entities.Exceptions;
using DemoAPI.Models;
using DemoAPI.WebClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DemoAPI.Services
{
  public interface ICountriesService
  {
    Task<CountryDTO> GetCountryAsync(string countryName);
  }

  public class CountriesService : ICountriesService
  {

    private readonly MSSQLContext _dbContext;
    private readonly IConfiguration _apiConfiguration;
    private readonly IWebClient _webClient;
    private readonly MyMemoryCache _memoryCache;

    public CountriesService(MSSQLContext context, MyMemoryCache memoryCache, IConfiguration config)
    {
      _apiConfiguration = config;
      _memoryCache = memoryCache;
      _webClient = new WebClient.WebClient(_apiConfiguration["CountriesAPI"]);
      _dbContext = context;
    }

    public async Task<CountryDTO> GetCountryAsync(string countryName)
    {
      var country = await FindCountryAsync(countryName);

      if(country != null)
      {
        return country;
      }

      var response = await _webClient.GetRequest($"name/{countryName}", "fields=name,capital,borders&fullText=true");
    
      var strresponse = await response.Content.ReadAsStringAsync();

      if(response.StatusCode != System.Net.HttpStatusCode.OK)
      {
        throw new CountriesAPIException(strresponse, response.StatusCode);
      }

      JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
      var countries = JsonSerializer.Deserialize<CountryDTO[]>(strresponse, options);

      country = countries[0];

      try
      {

        await SaveCountryToCacheDbAsync(country);

      }
      catch(DbUpdateException ex)
      {
        Console.WriteLine(ex.Message);
      }
      
      return country;
    }

    private async Task SaveCountryToCacheDbAsync(CountryDTO country)
    {
      SaveCountryToCache(country);
      await SaveCountryDbAsync(country);
    }

    private async Task SaveCountryDbAsync(CountryDTO country)
    {

      var border = country.Borders;

      List<Border> borders = new List<Border>();

      foreach(var b in border)
      {
        borders.Add(new Border{
          BorderName = b
          });
      }

      _dbContext.Add(new CountryModel { 
        CommonName = country.Name.Common,
        Capital = country.Capital.First(),
        Borders = borders        
      });

      await _dbContext.SaveChangesAsync();
    }

    private void SaveCountryToCache(CountryDTO country)
    {
       _memoryCache.Cache.Set(country.Name.Common.ToLower(), country);
    }

    private async Task<CountryDTO> FindCountryAsync(string countryName)
    {
      CountryDTO country;

      country = FindCountryInCache(countryName);

      if(country != null)
      {
        return country;
      }

      country = await FindCountryInDBASync(countryName);

      if(country != null)
      {
        SaveCountryToCache(country);
      }

      return country;
    }

    private async Task<CountryDTO> FindCountryInDBASync(string countryName)
    {
      CountryModel? country = await _dbContext.Countries.Include(c => c.Borders).FirstOrDefaultAsync(c => c.CommonName == countryName);
      
      if(country!= null)
      {

        List<string> borders = new List<string>();

        foreach(var b in country.Borders)
        {
          borders.Add(b.BorderName);
        }

        return new CountryDTO{
          Name = new NameDTO{Common = country.CommonName},
          Capital = new List<string>{country.Capital},
          Borders = borders
        };
      }

      return null;
    }

    private CountryDTO FindCountryInCache(string countryName)
    {
      _memoryCache.Cache.TryGetValue(countryName.ToLower(), out CountryDTO country);

      return country;
    }

  }


}