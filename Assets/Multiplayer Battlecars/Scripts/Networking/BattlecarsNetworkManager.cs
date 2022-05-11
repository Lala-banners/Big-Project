using UnityEngine;

using Mirror;

using System.Linq;
using System.Collections.Generic;

namespace Battlecars.Networking
{
    public class BattlecarsNetworkManager : NetworkManager
    {
        /// <summary>
        /// A reference to the battlecars version of the network manager singleton.
        /// </summary>
        public static BattlecarsNetworkManager Instance => singleton as BattlecarsNetworkManager;

        /// <summary>
        /// Whether or not this NetworkManager is the host
        /// </summary>
        public bool IsHost { get; private set; } = false;

        public BattlecarsNetworkDiscovery discovery;

        private Dictionary<byte, BattlecarsPlayerNet> players = new Dictionary<byte, BattlecarsPlayerNet>();

        /// <summary>
        /// Runs only when connecting to an online scene as a host
        /// </summary>
        public override void OnStartHost()
        {
            IsHost = true;
            discovery.AdvertiseServer();
        }

        /// <summary>
        /// Attempts to return a player corresponding to the passed id.
        /// If no player found, returns null (which is concerning)
        /// </summary>
        public BattlecarsPlayerNet GetPlayerForId(byte _playerId)
        {
            BattlecarsPlayerNet player;
            players.TryGetValue(_playerId, out player);
            return player;
        }

        // Runs when a client connects to the server. This function is responsible for creating the player
        // object and placing it in the scene. It is also responsible for making sure the connection is aware
        // of what their player object is.
        public override void OnServerAddPlayer(NetworkConnection _connection)
        {
            // Give us the next spawn position depending on the spawnMode
            Transform spawnPos = GetStartPosition();

            // Spawn a player and try to use the spawnPos
            GameObject playerObj =
                spawnPos != null
                ? Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation)
                : Instantiate(playerPrefab);

            // Assign the players ID and add them to the server based on the connection
            AssignPlayerId(playerObj);
            // Associates the player GameObject to the network connection on the server
            NetworkServer.AddPlayerForConnection(_connection, playerObj);
        }

        /// <summary>
        /// Removes the player with the corresponding ID from the dictionary
        /// </summary>
        /// <param name="_id"></param>
        public void RemovePlayer(byte _id)
        {
            // If the player is present in the dictionary, remove them
            if(players.ContainsKey(_id))
            {
                players.Remove(_id);
            }
        }

        public void AddPlayer(BattlecarsPlayerNet _player)
        {
            if(!players.ContainsKey(_player.playerId))
            {
                players.Add(_player.playerId, _player);
            }
        }

        protected void AssignPlayerId(GameObject _playerObj)
        {
            byte id = 0;
            //List<string> playerUsernames = players.Values.Select(x => x.username).ToList();
            // Generate a list that is sorted by the keys value
            List<byte> playerIds = players.Keys.OrderBy(x => x).ToList();
            // Loop through all keys (playerID's) in the player dictionary
            foreach(byte key in playerIds)
            {
                // If the temp id matches this key, increment the id value
                if(id == key)
                    id++;
            }

            // Get the playernet component from the gameobject and assign it's playerid
            BattlecarsPlayerNet player = _playerObj.GetComponent<BattlecarsPlayerNet>();
            player.playerId = id;
            players.Add(id, player);
        }
    }
}