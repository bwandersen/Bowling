using System.Collections.Generic;
using RestSharp;

namespace BowlingGame
{
  public class RemoteRollRepresentation
  {
    public string token { get; set; }
    public List<List<int>> points {get; set;}
  }

  public class RemoteRollRepository : IRollRepository
  {
    private string _url;
    public string RollId {get; set;}

    public RemoteRollRepository(string url)
    {
      _url = url;
    }

    public IList<int> GetRolls()
    {
      var restClient = new RestClient(_url);
      var request = new RestRequest(Method.GET);
      request.Resource = "api/points";

      var response = restClient.Execute<RemoteRollRepresentation>(request);
      if (response.ResponseStatus == ResponseStatus.Completed)
      {
        var rolls = new List<int>();
        RollId = response?.Data?.token;
        if (response?.Data?.points != null)
        {
          foreach (var frame in response.Data.points)
            rolls.AddRange(frame);
        }
        return rolls;
      }

      return null;      
    }
  }
}
