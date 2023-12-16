using DemoAPI.Configurations;
using DemoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
      await _countriesService.GetCountryAsync("Greece");

      return Ok(123);
    }
    
  }

}