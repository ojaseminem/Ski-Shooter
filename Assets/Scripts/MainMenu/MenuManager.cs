using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MenuManager : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Scenes/GameScene");
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
