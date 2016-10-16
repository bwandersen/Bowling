using System.Collections.Generic;
using BowlingGame;

namespace test
{
  public class RollRepository : IRollRepository
  {
    public string RollId { get; set; }

    public List<int> Rolls { get; } = new List<int>();

    public RollRepository(int[] rolls)
    {
      Rolls.AddRange(rolls);
    }

    public RollRepository(int value, int count)
    {
      for (int i = 0; i < count; i++) Rolls.Add(value);
    }
  }
}
