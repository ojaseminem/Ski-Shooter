using Gameplay.Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class MenuManager : MonoBehaviour
    {
        public UserData userData;
        public TextMeshProUGUI totalCoinText;
        public TextMeshProUGUI totalDiamondText;
        
        private void Start()
        {
            if (PlayerPrefs.HasKey("TotalUserCoinCount"))
            {
                userData.totalUserCoinCount = PlayerPrefs.GetInt("TotalUserCoinCount");
            }
            if (PlayerPrefs.HasKey("TotalUserDiamondCount"))
            {
                userData.totalUserDiamondCount = PlayerPrefs.GetInt("TotalUserDiamondCount");
            }
            
            totalCoinText.text = userData.totalUserCoinCount.ToString();
            totalDiamondText.text = userData.totalUserDiamondCount.ToString();
            
            Time.timeScale = 1;
        }
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
