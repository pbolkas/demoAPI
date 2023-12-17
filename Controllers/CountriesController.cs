using DemoAPI.Contracts.Responses;
using DemoAPI.Entities;
using DemoAPI.Entities.Exceptions;
using DemoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CountriesController : ControllerBase
  {

    private readonly ICountriesService _countriesService;

    public CountriesController(ICountriesService countriesService)
    {
      _countriesService = countriesService;
    }


    [AllowAnonymous]
    [HttpGet("country")]
    public async Task<ActionResult> GetCountryDetails([FromQuery] string countryName)
    {

      if(countryName == null)
      {
        return BadRequest(new Error { ErrorMessage = "countryName parameter required" });
      }

      try
      {

        CountryDTO country = await _countriesService.GetCountryAsync(countryName);
      
        return Ok(new Country { 
          CommonName = country.Name.Common,
          Capital = country.Capital.First(),
          Borders = country.Borders
        });

      }
      catch (CountriesAPIException ex)
      {
        return StatusCode((int) ex.APICode, ex.Message);
      }
      catch (Exception )
      {
        return StatusCode(500);
      }
    }
    
  }

}