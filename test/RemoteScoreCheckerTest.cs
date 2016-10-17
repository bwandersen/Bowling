using Xunit;
using BowlingGame;

namespace test
{
  [Trait("Category", "Integration")]
  public class RemoteScoreCheckerTest
  {
    [Fact]
    public void canCallRemoteScoreChecker()
    {
      var url = "http://37.139.2.74";
      var repo = new RemoteRollRepository(url);
      var game = new Game(repo);
      var scores = game.Score();
      var target = new RemoteScoreChecker(url);
      var result = target.Verify(repo.RollId, scores);
      Assert.True(result);
    }
  }
}
