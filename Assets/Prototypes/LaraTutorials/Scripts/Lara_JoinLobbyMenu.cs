using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.UI;
using System.Linq;

public class Lara_JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private Lara_NetworkManager networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPanel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;

    /// <summary>
    /// Subscribe from events and know if we (client) have connected and disconnected to the server.
    /// </summary>
    private void OnEnable()
    {
        Lara_NetworkManager.OnClientConnected += HandleClientConnected;
        Lara_NetworkManager.OnClientDisconnected += HandleClientDisconnected;
    }

    /// <summary>
    /// Unsubscribe from events and know if we (client) have connected and disconnected to the server.
    /// </summary>
    private void OnDisable()
    {
        Lara_NetworkManager.OnClientConnected -= HandleClientConnected;
        Lara_NetworkManager.OnClientDisconnected -= HandleClientDisconnected;
    }

    /// <summary>
    /// Function gets called when players have pressed Join Button.
    /// Set network address to localHost
    /// Start the client and use the current network address
    /// Stop spamming of start client functionality so make button non interactable
    /// </summary>
    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;
        
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = false;
    }

    /// <summary>
    /// Re-enable join button
    /// Deactivate main menu
    /// </summary>
    private void HandleClientConnected()
    {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        landingPanel.SetActive(false);
    }

    /// <summary>
    /// Re-enable join button
    /// </summary>
    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}
