using UnityEngine;
using Valve.VR;

namespace Lara
{
    public class VersionManager : MonoBehaviour
    {
        [SerializeField] private GameObject pcUI;
        [SerializeField] private GameObject pcPlayer, vrPlayer;
        [SerializeField] private GameObject vrUI;
        [SerializeField] private GameObject joinButton, hostButton;

        [SerializeField]
        private bool isVrActive;

        // Start is called before the first frame update
        void Start()
        {
            if (isVrActive)
            {
                ActivateVR();
            }
            else
            {
                ActivatePC();
            }
        }

        public void ActivateVR()
        {
            vrPlayer.SetActive(true);
            joinButton.SetActive(false);
            hostButton.SetActive(true);
            pcUI.SetActive(false); //Deactivate pc/mobile player UI
            vrUI.SetActive(true); //Activate VR rig
            CustomNetworkManager.singleton.playerPrefab = vrPlayer.gameObject;
        }

        public void ActivatePC()
        {
            joinButton.SetActive(true);
            hostButton.SetActive(false);
            pcPlayer.SetActive(true);
            pcUI.SetActive(true);
            CustomNetworkManager.singleton.playerPrefab = CustomNetworkManager.singleton.playerPrefab.gameObject;
        }
    }
}