using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameEvents : MonoBehaviour
    {
        public Button btnPause;
        public GameObject gameOverPanel;

        private void Update()
        {
            if (!PlayerManager.isGameStarted)
                return;

            if (PlayerManager.gameOver)
            {
                gameOverPanel.SetActive(true);
                btnPause.interactable = false;
            }
            else
            {
                btnPause.interactable = true;
            }
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
