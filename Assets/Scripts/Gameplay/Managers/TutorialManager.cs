using System;
using Gameplay.Scriptable_Objects;
using UnityEngine;

namespace Gameplay.Managers
{
    public class TutorialManager : MonoBehaviour
    {
        public UserData userData;

        public GameObject tutorialIntro;
        public GameObject startingText;

        public GameObject playerManager;

        private void Start()
        {
            if(PlayerPrefs.HasKey("TutorialComplete"))
            {
                if (PlayerPrefs.GetInt("TutorialComplete") == 0)
                {
                    userData.tutorialComplete = 0;
                    playerManager.SetActive(false);
                    startingText.SetActive(false);
                    ActivateTutorial();
                }
                if (PlayerPrefs.GetInt("TutorialComplete") == 1)
                {
                    userData.tutorialComplete = 1;
                    playerManager.SetActive(true);
                    startingText.SetActive(true);
                }
            }
            else
            {
                userData.tutorialComplete = 0;
                playerManager.SetActive(false);
                startingText.SetActive(false); 
                ActivateTutorial();
            }
        }
        private void ActivateTutorial()
        {
            tutorialIntro.SetActive(true);
            PauseTime();
        }
        private void PauseTime()
        {
            Time.timeScale = 0;
        }
        private void ResumeTime()
        {
            Time.timeScale = 1;
        }
        public void EndTutorial()
        {
            ResumeTime();
            playerManager.SetActive(true);
            startingText.SetActive(true);
            userData.tutorialComplete = 1;
            PlayerPrefs.SetInt("TutorialComplete", 1);
        }
    }
}