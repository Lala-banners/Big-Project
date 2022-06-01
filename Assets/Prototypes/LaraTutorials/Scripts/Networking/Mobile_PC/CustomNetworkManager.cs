using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;
using LobbyManagement;

namespace Lara
{
    /// <summary> Custom Network Manager </summary>
    public class CustomNetworkManager : NetworkManager
    {
        [Header("<-- Start of custom options -->")]
        // The platform we are currently playing or building on.
        [SerializeField] private PlatformPlayingOn currentPlatformPlayingOn = PlatformPlayingOn.PC;
        
        public PlatformPlayingOn CurrentPlatformPlayingOn
        {
            get => currentPlatformPlayingOn;
            set => currentPlatformPlayingOn = value;
        }
        
        //Minimum amount of players
        [SerializeField] private int minPlayers = 2; //Will be changed to 4 for dumpling

        //Reference scene name by string (offline scene)
        public string menuScene;

        [Header("Room")]
        [SerializeField] private PlayerLobby roomPlayerPrefab = null; //VR player = player 0
        [SerializeField] private PlayerLobby vrRoomPlayerPrefab = null; // We aren't using right now.


        [Header("Game")]
        [SerializeField] private GamePlayer gamePlayerPrefab = null;
        [SerializeField] private GamePlayer vrGamePlayerPrefab = null;

        [Space]

        [SerializeField] private GameObject playerSpawnSystem = null;

        //Connect to Host
        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;
        //New Action for checking if server is ready to spawn players in
        //Check when player has connected to server
        public static event Action<NetworkConnection> OnServerReadied;

        //Display / loop all names of players
        public List<PlayerLobby> RoomPlayers { get; }
        public List<GamePlayer> GamePlayers { get; }

        public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("Assets/Prototypes/LaraTutorials/Resources/SpawnablePrefabs").ToList();
        
        public override void OnStartClient()
        {
            var spawnablePrefabs = Resources.LoadAll<GameObject>("Assets/Prototypes/LaraTutorials/Resources/SpawnablePrefabs");

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
            if (numPlayers >= maxConnections)
            {
                conn.Disconnect();
                return;
            }

            if (SceneManager.GetActiveScene().name != menuScene)
            {
                conn.Disconnect();
                return;
            }
        }

        /// <summary>
        /// If player is in menu scene then spawn player prefab in the gameplay room.
        /// </summary>
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                //The leader is the first player added to server
                bool isLeader = RoomPlayers.Count == 0;

                PlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

                //Tell client who the leader is
                roomPlayerInstance.IsLeader = isLeader;

                roomPlayerInstance.platformPlayerIsUsing = currentPlatformPlayingOn;
                
                NetworkServer.AddPlayerForConnection(conn, roomPlayerPrefab.gameObject);
                
                // NetworkServer.AddPlayerForConnection(conn, vrRoomPlayerPrefab.gameObject);
                //
                // NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
                //
                // //Instantiate VR Player Lobby UI
                // PlayerLobby vrPlayerObject = Instantiate(vrRoomPlayerPrefab);
                //
                // NetworkServer.AddPlayerForConnection(conn, vrPlayerObject.gameObject);
            }
        }

        /// <summary>
        /// Find Room Player script on Server only NOT Client
        /// </summary>
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<PlayerLobby>();

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
            if (numPlayers < minPlayers) { return false; }

            //Loop over all players and return false if 1 person is NOT ready
            foreach (var player in RoomPlayers)
            {
                if (!player.IsReady) { return false; }
            }

            return true;
        }

        public void StartGame()
        {
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                if (!IsReadyToStart()) { return; }

                ServerChangeScene("Game");
            }
        }

        /// <summary>
        /// Send game players to gameplay scene from the menu scene.
        /// </summary>s
        public override void ServerChangeScene(string newSceneName)
        {
            if (SceneManager.GetActiveScene().name == menuScene && newSceneName.StartsWith("Game"))
            {
                //Loop through all players (minus player 0 who is the leader/host/first added to server)
                for (int i = RoomPlayers.Count - 1; i >= 0; i--)
                {
                    var conn = RoomPlayers[i].connectionToClient;
                    GamePlayer gameplayInstance;
                    
                    
                    // If this is the VR Player/Host
                    if(RoomPlayers[i].platformPlayerIsUsing == PlatformPlayingOn.VR)
                    {
                        // Spawn in the VR player if we are the host.
                        gameplayInstance = Instantiate(vrGamePlayerPrefab);
                    } 
                    else if (RoomPlayers[i].platformPlayerIsUsing == PlatformPlayingOn.PC)
                    { 
                        // Spawn in the PC player if we are playing on PC.
                        gameplayInstance = Instantiate(gamePlayerPrefab);
                    } 
                    else
                    {
                        // If the player had no platform selected.
                        Debug.LogError("No platform chosen to play on, PLZ FIX!!!\n" + 
                                       "Assuming they are a PC Player and spawning them in");
                        gameplayInstance = Instantiate(gamePlayerPrefab);
                    }
                    
                    
                    gameplayInstance.SetDisplayName(RoomPlayers[i].DisplayName);
                    NetworkServer.Destroy(conn.identity.gameObject);
                    NetworkServer.ReplacePlayerForConnection(conn, gameplayInstance.gameObject);
                }
            }

            base.ServerChangeScene(newSceneName);
        }

        //As soon as scene has loaded, spawn the player spawn system
        public override void OnServerSceneChanged(string sceneName)
        {
            if (sceneName.StartsWith("Game"))
            {
                GameObject playerSpawnInstanceGO = Instantiate(playerSpawnSystem);
                NetworkServer.Spawn(playerSpawnInstanceGO);
            }
        }

        public override void OnServerReady(NetworkConnection conn)
        {
            base.OnServerReady(conn);

            //Know when client is ready
            OnServerReadied?.Invoke(conn);
        }
    }
}
