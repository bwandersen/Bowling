using System;
using System.Collections.Generic;

namespace BowlingGame
{
  public class Game
  {
    private const int NofFrames = 10;
    private const int RollsPerFrame = 2;   // 10th frame of 3 rolls not relevant to the algorithm

    private IRollRepository _repository;    
    private IList<int> Scores { get; set; } = new List<int>();

    public IList<int> Rolls { get; set; }

    public Game(IRollRepository repository)
    {
      _repository = repository;      
    }

    public IList<int> Score()
    {
      Rolls = _repository.GetRolls();
      for (int i = 0; i < NofRolls(); i += RollsPerFrame)
      {
        var nextFrameStart = i + RollsPerFrame;
        var frameScore = GetFrameScore(i);
        
        if (IsStrike(Roll(i)) && (!IsLastFrame(i)))
        {
          if (!CanCalculate(nextFrameStart)) break;
          frameScore += TwoRolls(nextFrameStart);

          if (IsStrike(Roll(nextFrameStart)) && (!IsLastFrame(nextFrameStart)))
          {
            var twoFramesAheadStart = nextFrameStart + RollsPerFrame;

            if (!CanCalculate(twoFramesAheadStart)) break;
            frameScore += Roll(twoFramesAheadStart);

            if (!IsLastFrame(twoFramesAheadStart))
              frameScore += Roll(twoFramesAheadStart + 1);
          }
        }
        else if (IsSpare(frameScore))
        {
          if (!CanCalculate(nextFrameStart)) break;
          frameScore += Roll(nextFrameStart);
        }
        
        Scores.Add(PreviousFrame() + frameScore);
      }

      return Scores;
    }

    private bool CanCalculate(int rollIndex) => (NofRolls() > rollIndex);

    private int NofRolls() => Math.Min(Rolls.Count, NofFrames * RollsPerFrame);

    private int PreviousFrame() => (Scores.Count == 0) ? 0 : Scores[Scores.Count - 1];

    private int GetFrameScore(int i) => TwoRolls(i) + ((IsLastFrame(i)) ? Roll(i + 2) : 0);

    private int TwoRolls(int i) => Roll(i) + Roll(i + 1);

    private int Roll(int i) => ((i < Rolls.Count) ? Rolls[i] : 0);

    private bool IsLastFrame(int i) => (i >= 18);

    private bool IsSpare(int frame) => (frame == 10);

    private bool IsStrike(int roll) => (roll == 10);
  }
}
