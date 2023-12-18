using Microsoft.Extensions.Caching.Memory;

namespace DemoAPI.DataContext
{
  public class MyMemoryCache
  {
    public MemoryCache Cache { get; } = new MemoryCache(
        new MemoryCacheOptions
        {
        });
  }
}