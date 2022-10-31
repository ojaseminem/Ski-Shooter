﻿using TMPro;
using UnityEngine;

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

        public GameObject camInitial, camMain, camDeath;

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

            if (coinCount <= 0)
                coinCount = 0;
            if (diamondCount <= 0)
                diamondCount = 0;
            
            if (gameOver)
            {
                camMain.SetActive(false);
                camDeath.SetActive(true);
                gameOverPanel.SetActive(true);
                diamondCountFinal.text = diamondCount.ToString();
                coinCountFinal.text = coinCount.ToString();
                //Destroy(gameObject);
            }

            if (SwipeManager.tap  && !isGameStarted)
            {
                camInitial.SetActive(false);
                camMain.SetActive(true);
                isGameStarted = true;
                TimeCalculator.instance.BeginTimer();
                Destroy(startingText);
            }
        }
    }
}