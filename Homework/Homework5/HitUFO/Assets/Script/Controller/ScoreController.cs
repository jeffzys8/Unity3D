using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    记分管理器，不应该是Monobehavior，为RoundController所拥有 
*/

namespace Controller
{
    public class ScoreController : System.Object
    {
        int currentScore = 0;
        public int GetScore() { return currentScore; }
        public ScoreController()
        {
            currentScore = 0;
        }
        public void HitADisk(Disk a)
        {
            if (a == null)
            {
                //理应抛出异常，但我不会..UNFINISHED
                return;
            }
            int score = (int)((1/a.GetSizeRate()) * a.GetSpeedRate() * a.GetColor() * 2);
            currentScore += score;
        }
        public void Reset() { currentScore = 0; }

    }
}
