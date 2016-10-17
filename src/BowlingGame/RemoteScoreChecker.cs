using System.Collections.Generic;
using RestSharp;

namespace BowlingGame
{
  public class RemoteScoreRepresentation
  {
    public string token { get; set; }
    public IList<int> points { get; set; }
  }

  public class RemoteScoreChecker
  {
    private string _url;
    public RemoteScoreChecker(string url)
    {
      _url = url;
    }

    public bool Verify(string rollId, IList<int> score)
    {
      var body = new RemoteScoreRepresentation() { token = rollId, points = score };

      var restClient = new RestClient(_url);
      var request = new RestRequest(Method.POST);
      request.Resource = "api/points";
      request.AddJsonBody(body);

      var response = restClient.Execute<RemoteScoreRepresentation>(request);
      return (response.StatusCode == System.Net.HttpStatusCode.OK);
    }
  }
}
