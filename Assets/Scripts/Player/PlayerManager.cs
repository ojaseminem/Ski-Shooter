using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    //public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int coinCount;
    public static int diamondCount;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI diamondText;

    public static bool isGamePaused;

    public GameObject camInitial, camMain;

    private void Start()
    {
        coinCount = 0;
        diamondCount = 0;
        Time.timeScale = 1;
        gameOver = isGameStarted = isGamePaused= false;
    }

    private void Update()
    {
        coinText.text = coinCount.ToString();
        diamondText.text = diamondCount.ToString();
        
        //Game Over
        if (gameOver)
        {
            Time.timeScale = 0;
            //gameOverPanel.SetActive(true);
            Destroy(gameObject);
        }

        //Start Game
        if (SwipeManager.tap  && !isGameStarted)
        {
            camInitial.SetActive(false);
            camMain.SetActive(true);
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
