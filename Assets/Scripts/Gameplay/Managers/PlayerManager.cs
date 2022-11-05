using System.Collections;
using Gameplay.Managers;
using Gameplay.Misc;
using Gameplay.Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        private void Awake() => instance = this;

        public static bool gameOver;
        public GameObject gameOverPanel;
        public TextMeshProUGUI diamondCountFinal, coinCountFinal;

        public static bool isGameStarted;
        public GameObject startingText;

        private int _coinCount;
        private int _diamondCount;
        public TextMeshProUGUI coinText;
        public TextMeshProUGUI diamondText;
        public TextMeshProUGUI plusCoinText;
        public TextMeshProUGUI plusDiamondText;

        public static bool isGamePaused;
        public Button btnPause;

        public GameObject camInitial, camMain, camDeath;

        public UserData userData;

        private void Start()
        {
            _coinCount = 0;
            _diamondCount = 0;
            Time.timeScale = 1;
            gameOver = isGameStarted = isGamePaused= false;
            Application.targetFrameRate = 60;
        }

        private bool _callGameStarted, _callGameOver;
        
        private void Update()
        {
            if(_callGameOver)
            {
                if (gameOver)
                {
                    GameOver();
                }
            }
            if (SwipeManager.tap  && !isGameStarted)
            {
                _callGameStarted = true;
                if (_callGameStarted) GameStarted();
            }
        }
        public void PlusCoin(int coins, string plusOrMinus)
        {
            _coinCount += coins;
            if (_coinCount <= 0)
                _coinCount = 0;
            if (plusOrMinus == "+") plusCoinText.text = plusOrMinus + coins;
            if (plusOrMinus == "-") plusCoinText.text = coins.ToString();
            plusCoinText.gameObject.SetActive(false);
            plusCoinText.gameObject.SetActive(true);
            coinText.text = _coinCount.ToString();
        }
        public void PlusDiamond(int diamonds, string plusOrMinus)
        {
            _diamondCount += diamonds;
            if (_diamondCount <= 0)
                _diamondCount = 0;
            if (plusOrMinus == "+") plusDiamondText.text = plusOrMinus + diamonds;
            if (plusOrMinus == "-") plusDiamondText.text = diamonds.ToString();
            plusDiamondText.gameObject.SetActive(false);
            plusDiamondText.gameObject.SetActive(true);
            diamondText.text = _diamondCount.ToString();
        }
        private void GameStarted()
        {
            _callGameStarted = false;
            camInitial.SetActive(false);
            camMain.SetActive(true);
            isGameStarted = true;
            TimeCalculator.instance.BeginTimer();
            AudioManager.instance.PlaySound("SFX_Skiing");
            Destroy(startingText);
            _callGameOver = true;
        }
        private void GameOver()
        {
            _callGameOver = false;
            btnPause.interactable = false;
            camMain.SetActive(false);
            camDeath.SetActive(true);
            StartCoroutine(GameOverDelay());
            IEnumerator GameOverDelay()
            {
                yield return new WaitForSeconds(2f);
                AudioManager.instance.PauseSound("SFX_BG_Gameplay");
                AudioManager.instance.PlaySound("SFX_GameOver");
                gameOverPanel.SetActive(true);
                diamondCountFinal.text = _diamondCount.ToString();
                coinCountFinal.text = _coinCount.ToString();
                userData.totalUserCoinCount += _coinCount;
                userData.totalUserDiamondCount += _diamondCount;
                if(PlayerPrefs.HasKey("TotalUserCoinCount"))
                {
                    PlayerPrefs.SetInt("TotalUserCoinCount", PlayerPrefs.GetInt("TotalUserCoinCount") + _coinCount);
                }
                else
                {
                    PlayerPrefs.SetInt("TotalUserCoinCount", _coinCount);
                }
                if(PlayerPrefs.HasKey("TotalUserDiamondCount"))
                {
                    PlayerPrefs.SetInt("TotalUserDiamondCount", PlayerPrefs.GetInt("TotalUserDiamondCount") + _diamondCount);
                }
                else
                {
                    PlayerPrefs.SetInt("TotalUserDiamondCount", _diamondCount);
                }
            }
        }
    }
}
