using Microsoft.AspNetCore.Mvc;
using DemoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DemoAPI.Contracts.Requests;
using DemoAPI.Entities;
using System.Text.Json;
using DemoAPI.Contracts.Responses;
using DemoAPI.Entities.Exceptions;

namespace DemoAPI.Controllers 
{

  [Route("api/[controller]")]
  [ApiController]
  public class RequestObjController : ControllerBase
  {
    private readonly IRequestObjService _requestObjService;

    public RequestObjController(IRequestObjService service) 
    {
      _requestObjService = service;
    }

    [AllowAnonymous]
    [HttpPost("secondLargest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult SecondLargest([FromBody] RequestObj requestObjArray)
    {
      try 
      {

        RequestObjDTO? requestObj = new RequestObjDTO { requestArrayObj = requestObjArray.RequestArrayObj };

        int secondLargest = _requestObjService.SecondLargest(requestObj);

        return Ok(secondLargest);
      }
      catch (JsonException ex)
      {
        return BadRequest(new Error { ErrorMessage = ex.Message });
      }
      catch(RequestObjEmptyArrayException ex)
      {
        return BadRequest(new Error { ErrorMessage = "Array is Empty" });
      }
      
    }

  }

}