using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider slider;
    public Dropdown dropdown;

    public GameObject pauseMenu;

    bool showingPause = false;

    float volume = -32f;
    int qualityIndex = 2;
    public void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volume = PlayerPrefs.GetFloat("volume");
            audioMixer.SetFloat("volume", volume);
        } else
        {
            PlayerPrefs.SetFloat("volume", volume);
            audioMixer.SetFloat("Volume", volume);
        }

        if (PlayerPrefs.HasKey("Quality"))
        {
            qualityIndex = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(qualityIndex);
        } else
        {
            PlayerPrefs.SetInt("Quality", qualityIndex);
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        dropdown.value = qualityIndex;
        slider.value = volume;
        
    }

    public void Update()
    {
        if (pauseMenu == null)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && !showingPause)
        {
            Pause(true);
        } else if(Input.GetKeyDown(KeyCode.Escape) && showingPause)
        {
            Resume();
        }
    }

    public void Pause(bool showPause)
    {
        pauseMenu.SetActive(showPause);
        showingPause = true;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        showingPause = false;
        Time.timeScale = 1;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HubScene");
    }

}
