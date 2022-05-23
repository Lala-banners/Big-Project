using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

namespace Lara
{
    public class VersionManager : MonoBehaviour
    {
        [SerializeField] private GameObject pcUI;
        [SerializeField] private GameObject vrUI;
        [SerializeField] private Player vrPlayer;

        // Start is called before the first frame update
        void Start()
        {
            if (SteamVR.active)
            {
                pcUI.SetActive(false); //Deactivate pc/mobile player UI
                vrUI.SetActive(true); //Activate VR rig
                CustomNetworkManager.singleton.playerPrefab = vrPlayer.gameObject; //Set the player prefab of network manager to the VR player
            }
            else
            {
                pcUI.SetActive(true);
                vrUI.SetActive(false);
                CustomNetworkManager.singleton.playerPrefab = CustomNetworkManager.singleton.playerPrefab;
            }
        }
    }
}
