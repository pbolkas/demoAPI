using System.Net;

namespace DemoAPI.Entities.Exceptions
{
  public class CountriesAPIException : Exception
  {

    public HttpStatusCode APICode { get; }

    public CountriesAPIException (string message) : base(message) {}
    public CountriesAPIException (string message, HttpStatusCode code) : base(message)
    {
      APICode = code; 
    }
  }

}