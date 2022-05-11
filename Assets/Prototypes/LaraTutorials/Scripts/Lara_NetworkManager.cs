using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

/// <summary> Custom Network Manager </summary>
public class Lara_NetworkManager : NetworkManager
{
    //Minimum amount of players
    [SerializeField] private int minPlayers = 2; //Will be changed to 4 for dumpling

    //Reference scene name by string
    public string menuScene;

    [Header("Room")]
    [SerializeField] private Lara_PlayerLobby roomPlayerPrefab = null;

    //Connect to Host
    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    //Display / loop all names of players
    public List<Lara_PlayerLobby> RoomPlayers { get; } = new List<Lara_PlayerLobby>();

    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    /// <summary>
    /// Monitors amount of players and disconnects when number of players exceeds the maximum,
    /// or if players not in the menu scene
    /// </summary>
    public override void OnServerConnect(NetworkConnection conn)
    {
        if(numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if(SceneManager.GetActiveScene().name != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    /// <summary>
    /// If player is in menu scene then spawn player prefab in the gameplay room
    /// </summary>
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if(SceneManager.GetActiveScene().name == menuScene)
        {
            //Figure out first person added to lobby
            bool isLeader = RoomPlayers.Count == 0;

            Lara_PlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

            //Tell client who the leader is
            roomPlayerInstance.IsLeader = isLeader; 

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    /// <summary>
    /// Find Room Player script on Server only NOT Client
    /// </summary>
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if(conn.identity != null)
        {
            var player = conn.identity.GetComponent<Lara_PlayerLobby>();
            
            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    /// <summary>
    /// Reset players for starting new game
    /// </summary>
    public override void OnStopServer()
    {
        RoomPlayers.Clear();
    }

    /// <summary>
    /// Handles whether all players are ready to play
    /// </summary>
    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    //Functions to handle readying up
    private bool IsReadyToStart()
    {
        //If current players then 
        if(numPlayers < minPlayers) { return false; }

        //Loop over all players and return false if 1 person is NOT ready
        foreach (var player in RoomPlayers)
        {
            if(!player.IsReady) { return false; }
        }

        return true;
    }

}
