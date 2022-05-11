using UnityEngine;
using UnityEngine.SceneManagement;

using Mirror;

using System.Collections;

using Battlecars.UI;

namespace Battlecars.Networking
{
    public class BattlecarsPlayerNet : NetworkBehaviour
    {
        [SyncVar]
        public byte playerId;
        [SyncVar]
        public string username = "";

        private Lobby lobby;
        private bool hasJoinedLobby = false;

        public void SetUsername(string _name)
        {
            if(isLocalPlayer)
            {
                // Only localplayers can call Commands as localplayers are the only
                // ones who have the authority to talk to the server
                CmdSetUsername(_name);
            }
        }

        public void AssignPlayerToSlot(bool _left, int _slotId, byte _playerId)
        {
            if(isLocalPlayer)
            {
                CmdAssignPlayerToLobbySlot(_left, _slotId, _playerId);
            }
        }

        #region Commands
        [Command]
        public void CmdSetUsername(string _name) => username = _name;
        [Command]
        public void CmdAssignPlayerToLobbySlot(bool _left, int _slotId, byte _playerId) => RpcAssignPlayerToLobbySlot(_left, _slotId, _playerId);
        #endregion
        #region RPCs
        [ClientRpc]
        public void RpcAssignPlayerToLobbySlot(bool _left, int _slotId, byte _playerId)
        {
            // If this is running on the host client, we don't need to set the player
            // to the slot, so just ignore this call
            if(BattlecarsNetworkManager.Instance.IsHost)
                return;

            // Find the Lobby in the scene and set the player to the correct slot
            StartCoroutine(AssignPlayerToLobbySlotDelayed(BattlecarsNetworkManager.Instance.GetPlayerForId(_playerId), _left, _slotId));
        }
        #endregion
        #region Coroutines
        private IEnumerator AssignPlayerToLobbySlotDelayed(BattlecarsPlayerNet _player, bool _left, int _slotId)
        {
            // Keep trying to get the lobby until it's not null
            Lobby lobby = FindObjectOfType<Lobby>();
            while(lobby == null)
            {
                yield return null;

                lobby = FindObjectOfType<Lobby>();
            }

            // Lobby successfully got, so assign the player
            lobby.AssignPlayerToSlot(_player, _left, _slotId);
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Determine if we are on the host client
            if(BattlecarsNetworkManager.Instance.IsHost)
            {
                // Attempt to get the lobby if we haven't already joined a lobby
                if(lobby == null && !hasJoinedLobby)
                    lobby = FindObjectOfType<Lobby>();

                // Attempt to join the lobby if we haven't already and the lobby is set
                if(lobby != null && !hasJoinedLobby)
                {
                    hasJoinedLobby = true;
                    lobby.OnPlayerConnected(this);
                }
            }
        }

        public override void OnStartClient()
        {
            BattlecarsNetworkManager.Instance.AddPlayer(this);
        }

        // Runs only when the object is connected is the local player
        public override void OnStartLocalPlayer()
        {
            // Load the scene with the lobby
            SceneManager.LoadSceneAsync("InGameMenus", LoadSceneMode.Additive);
        }

        // Runs when the client is disconnected from the server
        public override void OnStopClient()
        {
            // Remove the playerID from the server
            BattlecarsNetworkManager.Instance.RemovePlayer(playerId);
        }
    }
}