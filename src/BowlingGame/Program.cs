using System;

namespace BowlingGame
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("New game");

      var url = "http://37.139.2.74";
      var repo = new RemoteRollRepository(url);
      var game = new Game(repo);
      var score = game.Score();

      Console.WriteLine("Rolls = [{0}]", string.Join(",", game.Rolls));
      Console.WriteLine("Score = [{0}]", string.Join(",", score));

      var result = new RemoteScoreChecker(url).Verify(repo.RollId, score);

      Console.WriteLine("Score calculation was {0}", (result)?"correct":"not correct");
    }
  }
}
