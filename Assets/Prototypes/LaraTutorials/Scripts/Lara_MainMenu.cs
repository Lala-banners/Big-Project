using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Lara_MainMenu : MonoBehaviour
{
    [SerializeField] private Lara_NetworkManager networkManager = null;

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
