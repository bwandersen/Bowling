using System.Collections.Generic;

namespace BowlingGame
{
  public interface IRollRepository
  {
    string RollId { get; set; }

    List<int> Rolls { get; } 
  }
}
