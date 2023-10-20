using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject PauseBtn;
    public GameObject PauseMenuPanel;
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject StartingText;
    public static int numberOfCoins;
    public Text textCoins;
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
    }

    void Update()
    {
        //điều khiển panel GameOver (ẩn nút pause game)
        if (gameOver)
        {
            Time.timeScale = 0;
            PauseBtn.SetActive(false);
            textCoins.gameObject.SetActive(false);
            gameOverPanel.SetActive(true);
        }

        //điểm (coins)
        textCoins.text = "Coins: " + numberOfCoins; 
        //Nhấn nút Tap to play bắt đầu game và xuất hiện nút Pause
        if (SwipeManager.tap)
        {
            isGameStarted = true;
            PauseBtn.SetActive(true);
            Destroy(StartingText);
        }
    }

    // Sự kiện UI
        public void Pause()
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void Resume()
        {
            PauseMenuPanel.SetActive(false);
            Time.timeScale = 1;
        }
        public void Restart()
        {
            SceneManager.LoadScene("Level");
        }
}
