using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ExpLevelScore
{
    private static int score = 0;

    static ExpLevelScore()
    {
        score = PlayerPrefs.GetInt("Score", 0);
    }


    public static int Score
    {
        get { return score; }
        set { PlayerPrefs.SetInt("Score", (score = value)); }
    }

}
