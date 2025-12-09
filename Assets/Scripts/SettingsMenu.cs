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
        // Volume Slider
        if (volumeSlider == null)
        {
            // fallback: first slider
            Slider[] sliders = GetComponentsInChildren<Slider>();
            if (sliders.Length > 0)
                volumeSlider = sliders[0];
        }

        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // Difficulty Slider
        if (difficultySlider == null)
        {
            // fallback: second Slider
            Slider[] sliders = GetComponentsInChildren<Slider>();
            if (sliders.Length > 1)
                difficultySlider = sliders[1];
        }

        if (difficultySlider != null)
        {
            difficultySlider.minValue = 0;
            difficultySlider.maxValue = 2;
            difficultySlider.wholeNumbers = true;

            // Sync initial slider position with current difficulty
            difficultySlider.value = (int)DifficultyManager.Current;
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

    // Called by the volume slider
    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }

    // Called by the difficulty slider (0 = Easy, 1 = Normal, 2 = Hard)
    public void OnDifficultyChanged(float value)
    {
        int index = Mathf.RoundToInt(value);
        DifficultyManager.SetDifficultyFromIndex(index);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}