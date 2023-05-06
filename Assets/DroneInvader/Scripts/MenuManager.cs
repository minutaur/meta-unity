using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DroneInvader.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject loadScreen;
        public TMP_Dropdown resolutionDropdown;

        private void Start()
        {
            resolutionDropdown.ClearOptions();
            
            Resolution[] resolutions = Screen.resolutions;
            foreach (Resolution resolution in resolutions)
            {
                resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
            }
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadSceneAsync(sceneIndex));
        }

        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
            Image loadingBar = Instantiate(loadScreen).transform.GetChild(0).GetChild(1).GetComponent<Image>();
        
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingBar.fillAmount = progress;
                yield return null;
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
    
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    
        public void SetResolution(int resolutionIndex)
        {
            var resolution = Screen.resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    
        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
        }
    }
}