using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Custom Game Player Data
/// </summary>
namespace Lara
{
    public class GamePlayer : NetworkBehaviour
    {
        //SyncVars can only be changed on Server, and when get changed update everywhere else
        //hook - calls function
        [SyncVar]
        public string displayName = "Loading...";

        //Cast singleton once to get the lobby room
        private CustomNetworkManager room;
        private CustomNetworkManager Room
        {
            get
            {
                if (room != null) { return room; }
                return room = CustomNetworkManager.singleton as CustomNetworkManager;
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
