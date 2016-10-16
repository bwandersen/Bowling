using System.Collections.Generic;

namespace BowlingGame
{
  public interface IScoreChecker
    {
      IList<int> Expected { get; set; }

      bool Verify(string rollId, IList<int> scores);
    }
}
