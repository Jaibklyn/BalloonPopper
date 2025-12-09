using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    private TMP_InputField inputField;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();

        // Load previous name if available
        inputField.text = PlayerData.playerName;

        // When text changes, save it
        inputField.onValueChanged.AddListener(OnNameChanged);
    }

    void OnNameChanged(string newName)
    {
        PlayerData.playerName = newName;
        Debug.Log("Player name updated: " + newName);
    }
}