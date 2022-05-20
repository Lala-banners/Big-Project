using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using Valve.VR;

/// <summary>
/// Custom Player Lobby Data
/// </summary>
namespace Lara
{
    public class PlayerLobby : NetworkBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject lobbyUI = null;
        [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
        [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[4];
        //[SerializeField] private Image[] playerFaces = new Image[4]; //To swap faces
        [SerializeField] private Button startGameButton = null;

        //SyncVars can only be changed on Server, and when get changed update everywhere else
        //hook - calls function
        [SyncVar(hook = nameof(HandleDisplayNameChanged))]
        public string DisplayName = "Loading...";

        [SyncVar(hook = nameof(HandleReadyStatusChanged))]
        public bool IsReady = false;

        //Leader is first client added in the server
        private bool isLeader;
        public bool IsLeader
        {
            set
            {
                //Activate start game button based on value of isLeader
                isLeader = value;
                startGameButton.gameObject.SetActive(value);
                //Initialise VR player
                InitialiseVrPlayer();
            }
        }

        //Only once instance of the Network Manager 
        private CustomNetworkManager room;
        private CustomNetworkManager Room
        {
            get
            {
                if (room != null) { return room; }
                return room = CustomNetworkManager.singleton as CustomNetworkManager;
            }
        }

        public override void OnStartAuthority()
        {
            CmdSetDisplayName(PlayerNameInput.DisplayName);

            lobbyUI.SetActive(true);
        }

        public override void OnStartClient()
        {
            Room.RoomPlayers.Add(this);

            UpdateDisplay();
        }

        public override void OnStopClient()
        {
            Room.RoomPlayers.Remove(this);

            UpdateDisplay();
        }

        public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();
        public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();

        /// <summary>
        /// When players change their name or UI,
        /// find player if not local player and update UI
        /// </summary>
        public void UpdateDisplay()
        {
            if (!hasAuthority)
            {
                foreach (var player in Room.RoomPlayers)
                {
                    if (player.hasAuthority)
                    {
                        player.UpdateDisplay();
                        break;
                    }
                }

                return;
            }

            //Loop through player name texts and adjust until player has selected name (and face LATER)
            for (int i = 0; i < playerNameTexts.Length; i++)
            {
                playerNameTexts[i].text = "Waiting For Dumpling...";
                playerReadyTexts[i].text = string.Empty;
            }

            //Loop through player name text and ready status as player display name and set colours according to if players are ready or not
            for (int i = 0; i < Room.RoomPlayers.Count; i++)
            {
                playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
                playerReadyTexts[i].text = Room.RoomPlayers[i].IsReady ?
                    "<color=green>Ready</color>" :
                    "<color=red>Not Ready</color>";
            }
        }

        /// <summary>
        /// Server will tell clients to update ready status for leader now
        /// </summary>
        public void HandleReadyToStart(bool readyToStart)
        {
            if (!isLeader) { return; }

            startGameButton.interactable = readyToStart;
        }

        public void InitialiseVrPlayer()
        {
            SteamVR.Initialize(isLeader == true);
            Debug.Log("VR Player is host");
        }

        //All commands below are called on server

        [Command]
        private void CmdSetDisplayName(string displayName)
        {
            DisplayName = displayName;
        }

        [Command]
        public void CmdReadyUp()
        {
            IsReady = !IsReady;

            Room.NotifyPlayersOfReadyState();
        }

        [Command]
        public void CmdStartGame()
        {
            if (Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }

            //Start the Game
            Room.StartGame();
        }
    }
}
