using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Custom Game Player Data
/// </summary>
public class Lara_GamePlayer : NetworkBehaviour
{
    //SyncVars can only be changed on Server, and when get changed update everywhere else
    //hook - calls function
    [SyncVar]
    public string displayName = "Loading...";

    //Cast singleton once to get the lobby room
    private Lara_NetworkManager room;
    private Lara_NetworkManager Room
    {
        get
        {
            if(room != null) { return room; }
            return room = Lara_NetworkManager.singleton as Lara_NetworkManager;
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
