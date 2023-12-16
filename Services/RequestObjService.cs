using System.Diagnostics.CodeAnalysis;
using DemoAPI.Entities;
using DemoAPI.Entities.Exceptions;

namespace DemoAPI.Services
{
  public interface IRequestObjService
  {
    int SecondLargest(RequestObjDTO obj);
  }

  public class RequestObjService : IRequestObjService
  {
    public int SecondLargest(RequestObjDTO obj)
    {

      if(obj.requestArrayObj.Count() == 0)
      {
        throw new RequestObjEmptyArrayException("RequestObj array empty");
      }

      if (obj.requestArrayObj.Count() == 1)
      {
        return obj.requestArrayObj.First();
      }

      int secondLargest = obj.requestArrayObj.Order().ElementAt(obj.requestArrayObj.Count()-2);

      return secondLargest;
    }
  }
}