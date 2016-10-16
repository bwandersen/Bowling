using System.Collections.Generic;
using RestSharp;
using System.Threading;

namespace BowlingGame
{
  public class RemoteRollRepresentation
  {
    public string token { get; set; }
    public List<List<int>> points {get; set;}
  }
   
  public class RemoteRollRepository : IRollRepository
  {
    private EventWaitHandle Wait;

    public string RollId {get; set;}

    public List<int> Rolls { get; private set; }

    public void GetRolls()
    {
      var restClient = new RestClient("http://37.139.2.74");
      var request = new RestRequest(Method.GET);
      request.Resource = "api/points";

      Wait = new AutoResetEvent(false);
      var handle = restClient.ExecuteAsync<RemoteRollRepresentation>(request, SetRolls);
      Wait.WaitOne();      
    }

    private void SetRolls(IRestResponse<RemoteRollRepresentation> response)
    {
      Rolls = new List<int>();
      
      RollId = response?.Data?.token;
      if (response?.Data?.points != null)
      {
        foreach (var frame in response.Data.points)
          Rolls.AddRange(frame);
      }

      Wait.Set();
    }
  }
}
