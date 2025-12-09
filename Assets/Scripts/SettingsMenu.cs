using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider difficultySlider;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    void Start()
    {
        // Make sure we load saved PlayerData once
        PlayerData.Load();

        // VOLUME SLIDER
        if (volumeSlider == null)
        {
            // Fallback: first Slider under this object
            Slider[] sliders = GetComponentsInChildren<Slider>();
            if (sliders.Length > 0)
                volumeSlider = sliders[0];
        }

        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;

            // Start from saved volume
            volumeSlider.value = PlayerData.Volume;

            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // DIFFICULTY SLIDER
        if (difficultySlider == null)
        {
            // Fallback: second Slider under this object
            Slider[] sliders = GetComponentsInChildren<Slider>();
            if (sliders.Length > 1)
                difficultySlider = sliders[1];
        }

        if (difficultySlider != null)
        {
            difficultySlider.minValue = 0;
            difficultySlider.maxValue = 2;
            difficultySlider.wholeNumbers = true;

            // Start from saved difficulty
            difficultySlider.value = PlayerData.Difficulty;

            difficultySlider.onValueChanged.AddListener(OnDifficultyChanged);
        }
    }

    void OnDestroy()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);

        if (difficultySlider != null)
            difficultySlider.onValueChanged.RemoveListener(OnDifficultyChanged);
    }

    public void OnVolumeChanged(float value)
    {
        // Saves to PlayerPrefs
        PlayerData.SetVolume(value);
    }

    public void OnDifficultyChanged(float value)
    {
        int index = Mathf.RoundToInt(value);

        // Saves to PlayerPrefs
        PlayerData.SetDifficulty(index);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}