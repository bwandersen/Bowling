using RestSharp;
using System.Threading;

namespace BowlingGame
{
  //.Net core restsharp does not implement sync call (?!?)
  public static class RestSharpSync
  {
    public static IRestResponse<T> Execute<T>(this RestClient client, IRestRequest request) where T : new()
    {
      IRestResponse<T> response = null;
      EventWaitHandle wait;
      wait = new AutoResetEvent(false);
      client.ExecuteAsync<T>(request, r => { response = r; wait.Set(); });
      wait.WaitOne();
      return response;
    }
  }
}
