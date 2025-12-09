using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    private TMP_InputField inputField;

    void Start()
    {
        // Ensure PlayerData loads saved values once per session
        PlayerData.Load();

        inputField = GetComponent<TMP_InputField>();

        // Set the text field to the saved player name
        inputField.text = PlayerData.PlayerName;

        // Listen for changes and update PlayerData + PlayerPrefs
        inputField.onValueChanged.AddListener(OnNameChanged);
    }

    void OnDestroy()
    {
        // Clean up listener to avoid leaks when changing scenes
        if (inputField != null)
            inputField.onValueChanged.RemoveListener(OnNameChanged);
    }

    void OnNameChanged(string newName)
    {
        PlayerData.SetPlayerName(newName);
        Debug.Log("Player name updated: " + newName);
    }
}