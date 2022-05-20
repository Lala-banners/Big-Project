using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Lara
{
    public class VrWristUI : MonoBehaviour
    {
        public GameObject wristUI;
        public bool activeWristUI = true; //Whether or not wrist UI is enables or disabled
        public Button exitGameButton, returnToMenuButton;

        // Start is called before the first frame update
        void Start()
        {
            DisplayWristUI();
        }

        // Update is called once per frame
        void Update()
        {
            exitGameButton.onClick.AddListener(() =>
            {
                Debug.Log("Exiting Game...");
            });

            returnToMenuButton.onClick.AddListener(() =>
            {
                Debug.Log("Returning to main menu...");
            });
        }

        /// <summary>
        /// New input system 
        /// </summary>
        public void WristMenuPressed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                DisplayWristUI();
            }
        }

        public void DisplayWristUI()
        {
            if (activeWristUI)
            {
                wristUI.SetActive(false);
                activeWristUI = false;
            }
            else if (!activeWristUI)
            {
                wristUI.SetActive(true);
                activeWristUI = true;
            }
        }
    }
}
