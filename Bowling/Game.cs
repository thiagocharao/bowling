using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling
{
    public class Game
    {
        public int ProcessGame(Dictionary<int, List<int>> gameScores)
        {
            if (gameScores.Count() > 10)
            {
                throw new Exception("You are cheating...");
            }
            
            int finalScore = 0;
            
            foreach (var (frameNumber, scores) in gameScores)
            {
                var isTenthFrame = scores.First() != 10;

                if (isTenthFrame)
                {
                    if (scores.Count == 1) throw new Exception($"Frame {frameNumber} is invalid due to number of throws");
                    if (scores.Count == 3 && (scores[0] + scores[1] < 10 )) throw new Exception($"You've thrown an illegal extra ball during 10th frame");
                }
                
                var frameScore = scores.Sum();

                var bonusScore = 0;

                if (frameNumber != 10)
                {
                    if (scores.First().Equals(10)) // STRIKE
                    {
                        if (gameScores.ContainsKey(frameNumber + 1))
                        {
                            bonusScore += gameScores[frameNumber + 1].Sum();
                        }
                    }
                    else if (frameScore == 10) // SPARE
                    {
                        if (gameScores.ContainsKey(frameNumber + 1))
                        {
                            bonusScore += gameScores[frameNumber + 1].First();
                        }
                    }
                }

                finalScore += frameScore + bonusScore;
            }

            return finalScore;
        }
    }
}