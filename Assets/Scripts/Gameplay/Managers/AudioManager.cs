using Gameplay.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public Sound[] sounds;

        public Slider volumeSlider;
        
        void Awake()
        {
            instance = this;
            
            SetVolume();
            
            foreach(Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
                s.source.pitch = s.pitch;
            }

            if (SceneManager.GetActiveScene().name == "MenuScene")
            {
                PlaySound("SFX_BG_Menu");
                PlaySound("SFX_Snow");
            }
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                PlaySound("SFX_BG_Gameplay");
                PlaySound("SFX_Snow");
            }
        }

        public void PlaySound(string name)
        {
            foreach (Sound s in sounds)
            {
                if (s.name == name)
                    s.source.Play();
            }
        }

        public void PauseSound(string name)
        {
            foreach (Sound s in sounds)
            {
                if (s.name == name)
                    s.source.Pause();
            }
        }

        private void SetVolume()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("OverallVolume", 1); 
            volumeSlider.value = AudioListener.volume;
        }
        
        public void UpdateVolume()
        {
            AudioListener.volume = volumeSlider.value;
            PlayerPrefs.SetFloat("OverallVolume", volumeSlider.value);
        }
    }
}
