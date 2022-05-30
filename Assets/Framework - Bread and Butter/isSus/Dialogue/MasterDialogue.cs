using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace isSus.Dialogue
{
    public class MasterDialogue : MonoBehaviour
    {
        #region Master Instance
        public static MasterDialogue instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        #endregion

        [SerializeField] private Image faceIcon;
        [SerializeField] private Button nextButton, byeButton, accButton, decButton;
        [SerializeField] private TMP_Text nameText, dialogueText;

        private Conversation activeDialogue;

        public void UpdateDisplay()
        {
            Conversation.DLine lines = activeDialogue.Lines[activeDialogue.lineIndex];
            faceIcon.sprite = lines.individual.Face;
            nameText.text = lines.individual.name;
            dialogueText.text = lines.dialogueText;

            for (int i = 0; i < lines.actions.Length; i++)
            {

            }
        }

        /// <summary>
        /// Change active index to go to a specific line of dialogue.
        /// </summary>
        private void GoToLine(int _index)
        {
            activeDialogue.lineIndex = _index;
            UpdateDisplay();
        }
    }

    /// <summary>
    /// To keep track of actions made by player.
    /// </summary>
    public enum DialogueAbilities
    {
        Next,
        Bye,
        AccDec,
    }
}
