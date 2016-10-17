using System.Collections.Generic;
using Xunit;
using BowlingGame;

namespace test
{
  [Trait("Category", "Unit")]
  public class GameTest
  {
    private void AssertExpectedScore(IRollRepository rolls, IScoreChecker checker)
    {
      var target = new Game(rolls);
      Assert.True(checker.Verify(rolls.RollId, target.Score()));
    }

    [Fact]
    public void ZeroGame()
    {
      var rolls = new RollRepository(0, 21);
      var checker = new ScoreChecker(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void TenPointGame()
    {
      var rolls = new RollRepository(new int[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 });
      var checker = new ScoreChecker(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void SingleSpareGame()
    {
      var rolls = new RollRepository(new int[] { 9, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 });
      var checker = new ScoreChecker(new List<int> { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void SpareInLastFrame()
    {
      var rolls = new RollRepository(new int[] { 9, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 9, 1, 9 });
      var checker = new ScoreChecker(new List<int> { 11, 12, 13, 14, 15, 16, 17, 18, 19, 38 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void DoubleSpareGame()
    {
      var rolls = new RollRepository(new int[] { 9, 1, 9, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 });
      var checker = new ScoreChecker(new List<int> { 19, 30, 31, 32, 33, 34, 35, 36, 37, 38 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void SingleStrikeInGame()
    {
      var rolls = new RollRepository(new int[] { 10, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 });
      var checker = new ScoreChecker(new List<int> { 12, 14, 15, 16, 17, 18, 19, 20, 21, 22 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void DoubleStrikeInGame()
    {
      var rolls = new RollRepository(new int[] { 10, 0, 10, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 });
      var checker = new ScoreChecker(new List<int> { 21, 32, 33, 34, 35, 36, 37, 38, 39, 40 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void PerfectGame()
    {
      var rolls = new RollRepository(new int[] { 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 10, 10 });
      var checker = new ScoreChecker(new List<int> { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void IncompleteGame()
    {
      var rolls = new RollRepository(new int[] { 1, 2, 3, 4 });
      var checker = new ScoreChecker(new List<int> { 3, 10 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void IncompleteEndingWithSpareGame()
    {
      var rolls = new RollRepository(new int[] { 1, 2, 9, 1 });
      var checker = new ScoreChecker(new List<int> { 3 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void IncompleteEndingWithStrikeGame()
    {
      var rolls = new RollRepository(new int[] { 1, 2, 10, 0 });
      var checker = new ScoreChecker(new List<int> { 3 });

      AssertExpectedScore(rolls, checker);
    }

    [Fact]
    public void IncompleteEndingWithDoubleStrikeGame()
    {
      var rolls = new RollRepository(new int[] { 1, 2, 10, 0 , 10, 0 });
      var checker = new ScoreChecker(new List<int> { 3 });

      AssertExpectedScore(rolls, checker);
    }
  }
}
