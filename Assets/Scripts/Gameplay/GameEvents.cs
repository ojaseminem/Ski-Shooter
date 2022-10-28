using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class GameEvents : MonoBehaviour
    {
        public Button btnPause;

        private void Update()
        {
            if (!PlayerManager.isGameStarted)
                return;

            if (PlayerManager.gameOver)
                btnPause.interactable = false;
        }
        public void ReplayGame()
        {
            SceneManager.LoadScene("GameScene");
        }
        public void GoToMenu()
        {
            SceneManager.LoadScene("MenuScene");
        }
        public void PauseGame()
        {
            if (!PlayerManager.isGamePaused && !PlayerManager.gameOver)
            {
                Time.timeScale = 0;
                PlayerManager.isGamePaused = true;
            }
        }
        public void ResumeGame()
        {
            if (PlayerManager.isGamePaused)
            {
                Time.timeScale = 1;
                PlayerManager.isGamePaused = false;
            }
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
