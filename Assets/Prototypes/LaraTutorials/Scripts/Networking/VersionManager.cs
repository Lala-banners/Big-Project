using UnityEngine;
using Valve.VR;

namespace Lara
{
    public class VersionManager : MonoBehaviour
    {
        [SerializeField] private GameObject pcUI;
        [SerializeField] private GameObject pcPlayer;
        [SerializeField] private GameObject vrUI;
        [SerializeField] private GameObject vrPlayer;

        [SerializeField] private GameObject joinButton, hostButton;

        // Start is called before the first frame update
        void Start()
        {
            if (SteamVR.active)
            {
                SwitchToVR();
            }
            else
            {
                SwitchToPC();
            }
        }

        public void SwitchToVR()
        {
            joinButton.SetActive(false);
            hostButton.SetActive(true);
            pcPlayer.SetActive(false);
            pcUI.SetActive(false); //Deactivate pc/mobile player UI
            vrUI.SetActive(true); //Activate VR rig
            CustomNetworkManager.singleton.playerPrefab =
                vrPlayer.gameObject; //Set the player prefab of network manager to the VR player
        }

        public void SwitchToPC()
        {
            joinButton.SetActive(true);
            hostButton.SetActive(false);
            pcPlayer.SetActive(true);
            pcUI.SetActive(true);
            vrUI.SetActive(false);
            CustomNetworkManager.singleton.playerPrefab = CustomNetworkManager.singleton.playerPrefab;
        }
    }
}
