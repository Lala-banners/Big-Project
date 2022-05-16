using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

/// <summary>
/// Players spawn in from server
/// </summary>
namespace Lara
{
    public class PlayerSpawnSystem : NetworkBehaviour
    {
        [SerializeField] private GameObject playerPrefab = null;

        //List of spawn positions for players to spawn in
        private static List<Transform> spawnPoints = new List<Transform>();

        //Server will increment number so only 1 player per spawn point
        private int nextIndex = 0;

        /// <summary>
        /// Adds spawn point to list
        /// Makes sure spawn points are added in the correct order
        /// </summary>
        public static void AddSpawnPoint(Transform t)
        {
            spawnPoints.Add(t);

            spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
        }

        public static void RemoveSpawnPoint(Transform t) => spawnPoints.Remove(t);

        //When players are ready, spawn this object
        public override void OnStartServer() => NetworkManager.OnServerReadied += SpawnPlayer;

        //When this obj gets destroyed, despawn player
        [ServerCallback]
        private void OnDestroy() => NetworkManager.OnServerReadied -= SpawnPlayer;

        [Server]
        public void SpawnPlayer(NetworkConnection conn)
        {
            Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

            if (spawnPoint == null)
            {
                Debug.LogError($"Missing spawn point for player {nextIndex}");
                return;
            }

            //Spawn the player
            GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
            NetworkServer.Spawn(playerInstance, conn);

            nextIndex++;
        }


    }
}
