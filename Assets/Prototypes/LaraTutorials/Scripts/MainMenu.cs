using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Lara
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private CustomNetworkManager networkManager = null;

        [Header("UI")]
        [SerializeField] private GameObject landingPanel = null;

        /// <summary>
        /// When press host button, start the hosting and disable the main menu
        /// </summary>
        public void HostLobby()
        {
            networkManager.StartHost();

            landingPanel.SetActive(false);
        }
    }
}
