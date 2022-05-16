using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Lara
{
    public class PlayerNameInput : MonoBehaviour
    {
        //TODO: @Lara Convert to JSON saving and loading

        [Header("UI")]
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button continueButton = null;

        //Get player display name from any script internally
        public static string DisplayName { get; private set; }

        //Get player name from player prefs
        private const string PlayerPrefsNameKey = "PlayerName";

        //On start immediately set up UI for player name
        private void Start() => SetUpInputField();

        /// <summary>
        /// Check for player name key
        /// </summary>
        private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            nameInputField.text = defaultName;
        }

        /// <summary>
        /// Makes continue button interactable based on if player name has been entered or not
        /// </summary>
        /// <param name="name">Player name</param>
        public void SetPlayerName(string name)
        {
            continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        /// <summary>
        /// Set the display name to whatever the player inputs
        /// </summary>
        public void SavePlayerName()
        {
            DisplayName = nameInputField.text;

            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        }
    }
}
