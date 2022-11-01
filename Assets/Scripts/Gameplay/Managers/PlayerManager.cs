using Gameplay.Managers;
using Gameplay.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class PlayerManager : MonoBehaviour
    {
        public static bool gameOver;
        public GameObject gameOverPanel;
        public TextMeshProUGUI diamondCountFinal, coinCountFinal;

        public static bool isGameStarted;
        public GameObject startingText;

        public static int coinCount;
        public static int diamondCount;
        public TextMeshProUGUI coinText;
        public TextMeshProUGUI diamondText;

        public static bool isGamePaused;
        public Button btnPause;

        public GameObject camInitial, camMain, camDeath;

        private void Start()
        {
            coinCount = 0;
            diamondCount = 0;
            Time.timeScale = 1;
            gameOver = isGameStarted = isGamePaused= false;
            Application.targetFrameRate = 30;
        }

        private bool _callGameStarted, _callGameOver;
        
        private void Update()
        {
            coinText.text = coinCount.ToString();
            diamondText.text = diamondCount.ToString();

            if (coinCount <= 0)
                coinCount = 0;
            if (diamondCount <= 0)
                diamondCount = 0;
            
            if (gameOver)
            {
                _callGameOver = true;
                if (_callGameOver) GameOver();
            }

            if (SwipeManager.tap  && !isGameStarted)
            {
                _callGameStarted = true;
                if (_callGameStarted) GameStarted();
            }
        }

        private void GameStarted()
        {
            _callGameStarted = false;
            camInitial.SetActive(false);
            camMain.SetActive(true);
            isGameStarted = true;
            TimeCalculator.instance.BeginTimer();
            Destroy(startingText);
        }
        private void GameOver()
        {
            _callGameOver = false;
            btnPause.interactable = false;
            camMain.SetActive(false);
            camDeath.SetActive(true);
            gameOverPanel.SetActive(true);
            diamondCountFinal.text = diamondCount.ToString();
            coinCountFinal.text = coinCount.ToString();
        }
    }
}
