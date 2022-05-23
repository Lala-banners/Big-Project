using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

namespace MainProject.Prototypes.KieranTutorials.Lobby
{
    public class NetworkManagerLobby : NetworkManager
    {
        [Header("Custom settings")] 
        [SerializeField] [Tooltip("What is the minimum number of Players to start the game?")] 
        private int minPlayers = 2;
        [SerializeField] [Tooltip("Do you want players to join games in progress?")] 
        private bool canJoinActiveGames = false;
        
        // This lets us reference the menu by dragging in inspector.
        // Drag in the menu scene.
        [Scene] [SerializeField] private string menuScene = string.Empty;

        // Reference to the player.
        [Header("Room")]
        [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;

        [Header("Game")]
        [SerializeField] private NetworkGamePlayerLobby gamePlayerPrefab = null;
        
        // These are created as "public static" to be listened in on the Menu UI.
        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;
        
        // List of all Players so [Client]s and [Server] can loop through them.
        // Let say we need to display the names of all these people, we can loop over them.
        public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();
        
        // When they get removed from the game they get removed from ^ and added to this list.
        public List<NetworkGamePlayerLobby> GamePlayers { get; } = new List<NetworkGamePlayerLobby>();

        // This is to reference prefabs we will be spawning in [For the Server].
        // We normally need to drag them in but we will load in all in "SpawnablePrefabs" (a folder we will create).
       public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

        public override void OnStartClient()
        {
            // This is the same as the Server but will make sure they are on the clients "spawnablePrefabs"
            // It is just a different way of doing the above but for the [Client] ranther than the [Server].
            var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

            foreach (var prefab in spawnablePrefabs)
            {
                ClientScene.RegisterPrefab(prefab);
            }
        }

        // This is for when the [Client] connects.
        public override void OnClientConnect(NetworkConnection conn)
        {
            // Do the base logic.
            base.OnClientConnect(conn);

            // Do my event (any logic I choose).
            OnClientConnected?.Invoke();
        }

        // Same as Connect.
        public override void OnClientDisconnect(NetworkConnection conn)
        {
            // Do the base logic.
            base.OnClientDisconnect(conn);

            // Add my  event (e.g. my logic) to when the Client Disconnects.
            OnClientDisconnected?.Invoke();
        }

        // Called on the [Server] when a [Client] connects.
        public override void OnServerConnect(NetworkConnection conn)
        {
            // If we have hit the max number of players.
            if(numPlayers >= maxConnections)
            {
                // If we have too many players, disconnect that person.
                conn.Disconnect();
                return;
            }

            // This will stop players joining games in progress.
            if(!canJoinActiveGames)
            {
                // If we are not in the menu scene.
                if(SceneManager.GetActiveScene().name != menuScene)
                {
                    // Disconnect this player.
                    conn.Disconnect();
                    return;
                }
            }
        }

        // This is called on "OnClientConnect" in is .base code.
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // If we are in the menuScene. 
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                //The leader is the first player added to server
                bool isLeader = RoomPlayers.Count == 0;
                
                // Spawn in the room player prefab (the thing with "NetworkRoomPlayerLobby" on it).
                NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);
                
                //Tell client who the leader is
                roomPlayerInstance.IsLeader = isLeader;
                
                // We are tying together the player we just instansiated and this connection (conn)
                NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
            }
        }
        
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();
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
            // If we have enough people. 
            if (numPlayers < minPlayers) { return false; }

            // Loop over all players and return false if 1 person is NOT ready
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
                    var gameplayInstance = Instantiate(gamePlayerPrefab);
                    gameplayInstance.SetDisplayName(RoomPlayers[i].DisplayName);

                    NetworkServer.Destroy(conn.identity.gameObject);

                    NetworkServer.ReplacePlayerForConnection(conn, gameplayInstance.gameObject);
                }
            }

            base.ServerChangeScene(newSceneName);
        }
    }
}
