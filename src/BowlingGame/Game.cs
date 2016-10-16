using System;
using System.Collections.Generic;

namespace BowlingGame
{
  public class Game
  {
    private const int NofFrames = 10;
    private const int RollsPerFrame = 2;   // 10th frame of 3 rolls not relevant to the algorithm

    private IRollRepository _repository;
    private IScoreChecker _checker;
    private IList<int> Scores { get; set; } = new List<int>();

    public Game(IRollRepository repository, IScoreChecker checker)
    {
      _repository = repository;
      _checker = checker;
    }

    public IList<int> Score()
    {      
      for (int i = 0; i < NofRolls(); i += RollsPerFrame)
      {
        var canCalculate = true;
        var frameScore = GetFrameScore(i);

        if (IsStrike(Roll(i)) && (!IsLastFrame(i)))
        {
          canCalculate = (NofRolls() > i + 2);
          frameScore += TwoRolls(i + 2);

          if (IsStrike(Roll(i + 2)) && (!IsLastFrame(i + 2)))
          {
            canCalculate = (NofRolls() > i + 4);
            frameScore += Roll(i + 4);

            if (!IsLastFrame(i + 4))
              frameScore += Roll(i + 5);
          }
        }
        else if (IsSpare(frameScore))
        {
          canCalculate = (NofRolls() > i + 2);
          frameScore += Roll(i + 2);
        }

        if (canCalculate)
          Scores.Add(PreviousFrame() + frameScore);
      }

      return Scores;
    }

    private int NofRolls()
    {
      return Math.Min(_repository.Rolls.Count, NofFrames * RollsPerFrame);
    }

    private int PreviousFrame()
    {
      return (Scores.Count == 0) ? 0 : Scores[Scores.Count - 1];
    }

    private int GetFrameScore(int i)
    {
      return TwoRolls(i) + ((IsLastFrame(i)) ? Roll(i + 2) : 0);
    }

    private int TwoRolls(int i)
    {
      return Roll(i) + Roll(i + 1);
    }

    private int Roll(int i)
    {
      return ((i < _repository.Rolls.Count) ? _repository.Rolls[i] : 0);
    }

    private bool IsLastFrame(int i)
    {
      return (i >= 18);
    }

    private bool IsSpare(int frame)
    {
      return (frame == 10);
    }

    private bool IsStrike(int roll)
    {
      return (roll == 10);
    }
  }
}
