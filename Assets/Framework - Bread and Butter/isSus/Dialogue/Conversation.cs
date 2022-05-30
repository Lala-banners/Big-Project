using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace isSus.Dialogue 
{
    [CreateAssetMenu(menuName = "isSus/Dialogue/Conversation", fileName = "New Conversation")]
    public class Conversation : ScriptableObject
    {
        [Serializable]
        public struct DLine //What will be in each line of dialogue
        {
            public Individual individual;
            [TextArea] public string dialogueText;
            public ActionInfo[] actions;
        }

        /// <summary>
        /// What each action does
        /// </summary>
        [Serializable]
        public struct ActionInfo
        {
            public DialogueAbilities action;
            public string label;
        }

        [SerializeField] private DLine[] dialogueLines;
        public DLine[] Lines => dialogueLines;

        public int lineIndex = 0;
    }
}
