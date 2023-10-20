using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScore : MonoBehaviour
{
    public int highScore;
    public Text HighScoreText;
    public Text CoinTxt;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    void Update()
    {
        int coins = PlayerManager.numberOfCoins;
        CoinTxt.text = "Coins: " + coins.ToString();
        HighScoreText.text = "High score: " + highScore.ToString();

        if (coins > highScore)
            PlayerPrefs.SetInt("HighScore", coins);
    }
}

  