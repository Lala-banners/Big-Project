using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using System;

/// <summary>
/// Custom Network Manager
/// </summary>
public class Lara_LobbyManager : NetworkManager
{
    //Drag in scene and reference scene name by string
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayer roomPlayerPrefab = null;

    //Connect to Host
    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
}
