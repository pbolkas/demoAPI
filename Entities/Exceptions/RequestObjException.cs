namespace DemoAPI.Entities.Exceptions
{

    [System.Serializable]
  public class RequestObjEmptyArrayException : Exception
  {
    public RequestObjEmptyArrayException (string message) : base(message) { }    
  }
}