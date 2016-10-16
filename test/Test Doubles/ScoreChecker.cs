using System.Collections.Generic;
using BowlingGame;

namespace test
{
  public class ScoreChecker : IScoreChecker
  {
    public IList<int> Expected { get; set; }

    public ScoreChecker(IList<int> scores)
    {
      Expected = scores;
    }

    public bool Verify(string rollId, IList<int> scores)
    {
      if (scores.Count != Expected.Count)
        throw new System.Exception($"Expected {Expected.Count} score values");

      for (int i = 0; i < scores.Count; i++)
        if (scores[i] != Expected[i]) return false;

      return true;
    }
  }
}
