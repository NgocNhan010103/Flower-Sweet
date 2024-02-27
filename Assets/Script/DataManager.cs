using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static int _coins = 0;

    static DataManager()
    {
        _coins = PlayerPrefs.GetInt("Coins", 0);
    }


    public static int Coins
    {
        get { return _coins; }
        set { PlayerPrefs.SetInt("Coins", (_coins = value)); }
    }

}
