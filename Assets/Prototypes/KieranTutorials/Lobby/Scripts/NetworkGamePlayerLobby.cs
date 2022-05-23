using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainProject.Prototypes.KieranTutorials.Lobby
{
    public class NetworkGamePlayerLobby : NetworkBehaviour
    {
//SyncVars can only be changed on Server, and when get changed update everywhere else
        //hook - calls function
        [SyncVar]
        public string displayName = "Loading...";

        //Cast singleton once to get the lobby room
        private NetworkManagerLobby room;
        private NetworkManagerLobby Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as NetworkManagerLobby;
            }
        }

        public override void OnStartClient()
        {
            DontDestroyOnLoad(gameObject);

            Room.GamePlayers.Add(this);
        }

        public override void OnStopClient()
        {
            Room.GamePlayers.Remove(this);
        }

        [Server]
        public void SetDisplayName(string displayName)
        {
            this.displayName = displayName;
        }

        
    }
}
