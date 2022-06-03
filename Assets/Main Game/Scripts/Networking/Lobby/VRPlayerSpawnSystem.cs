using MainGame.Networking.Lobby;
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MainGame.Networking.Lobby
{
    public class VRPlayerSpawnSystem : NetworkBehaviour
    {
        [SerializeField] private GameObject VRPlayerPrefab = null;

        private static List<Transform> VRSpawnPoints = new List<Transform>();

        private int nextIndex = 0;

        public static void AddVRSpawnPoint(Transform transform)
        {
            VRSpawnPoints.Add(transform);

            VRSpawnPoints = VRSpawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
        }
        public static void RemoveVRSpawnPoint(Transform transform) => VRSpawnPoints.Remove(transform);

        public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnVRPlayer;

        public override void OnStartClient()
        {
            // TODO Add in action maps and enable them for the client;
            // InputManager.Add(ActionMapNames.Dumpling);
            // InputManager.Controls.Dumpling.Look.Enable();
        }

        [ServerCallback]
        private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnVRPlayer;

        [Server]
        public void SpawnVRPlayer(NetworkConnection conn)
        {
            Transform spawnPoint = VRSpawnPoints.ElementAtOrDefault(nextIndex);

            if (spawnPoint == null)
            {
                Debug.LogError($"Missing spawn point for player {nextIndex}");
                return;
            }

            GameObject playerInstance = Instantiate(VRPlayerPrefab, VRSpawnPoints[nextIndex].position, VRSpawnPoints[nextIndex].rotation);
            NetworkServer.Spawn(playerInstance, conn);

            nextIndex++;
        }
    }
}
