using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

namespace MainProject.Prototypes.KieranTutorials.Lobby
{
    public class PlayerNameInput : MonoBehaviour
    {
        // Dragged in UI for inupting the Player Name.
        [Header("UI")]
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button continueButton = null;

        // You can grab this name anywhere else,
        // but you can only set it internally.
        public static string DisplayName { get; private set; }

        // Constant string for where the Player Prefs name will be saved.
        // START HERE IF YOU ARE CHANGING TO JSON.
        private const string PlayerPrefsNameKey = "PlayerName";

        // On Start, set up the input fields.
        private void Start() => SetUpInputField();
        
        // If you already have played the game before it loads up your name.
        private void SetUpInputField()
        {
            // If they don't have a SavedName, we don't need to set up the Fields.
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }
            
            // If they do have a SavedName serialize it as defaultName.
            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            // Make the Input UI = defaultName (the previously SavedName).
            nameInputField.text = defaultName;

            // Sets player name to the previously SavedName. 
            SetPlayerName(defaultName);
        }

        // Disables the Continue Button if the Player name is invalid.
        public void SetPlayerName(string name)
        {
            continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        // Save the players display name to the Input name,
        // then saves it to  PlayerPrefs.
        public void SavePlayerName()
        {
            DisplayName = nameInputField.text;

            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        }
    }
}