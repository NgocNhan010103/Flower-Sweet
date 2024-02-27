using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSharedUI : MonoBehaviour
{
    #region Singleton class: GameSharedUI

    public static GameSharedUI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    #endregion


    [SerializeField] TMP_Text coinsUIText;

    void Start()
    {
        UpdateCoinUiText();
    }

    public void UpdateCoinUiText()
    {
        SetCoinsText(coinsUIText, DataManager.Coins);
    }

    void SetCoinsText(TMP_Text textMesh, int value)
    {
        if (value >= 1000 && value < 1000000)
            textMesh.text = string.Format("{0}K.{1}", (value / 1000), GetFirstDigitFromNumber(value / 1000));
        else if (value >= 1000000)
            textMesh.text = string.Format("{0}M.{1}", (value / 1000000), GetFirstDigitFromNumber(value / 1000000));
        else
            textMesh.text = value.ToString();
    }

    int GetFirstDigitFromNumber(int number)
    {
        return int.Parse(number.ToString()[0].ToString());
    }

}
