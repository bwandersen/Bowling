using Xunit;
using BowlingGame;

namespace test
{
  [Trait("Category", "Integration")]
  public class RemoteRollRepositoryTest
  {
    [Fact]
    public void canCallRemoteRollRepository()
    {
      var target = new RemoteRollRepository("http://37.139.2.74");
      target.GetRolls();
      Assert.False(string.IsNullOrWhiteSpace(target.RollId));
    }
  }
}
