using UnityEngine;
using Valve.VR;
using UnityEngine.Android;

namespace Lara
{
    public class VersionManager : MonoBehaviour
    {
        [SerializeField] private GameObject pcUI;
        [SerializeField] private GameObject pcPlayer;
        [SerializeField] private GameObject vrUI;
        [SerializeField] private GameObject joinButton, hostButton;

        // Start is called before the first frame update
        void Start()
        {
#if ENABLE_VR
            ActivateVR();
#elif !ENABLE_VR
            SwitchToPC();
#endif

            /*if (SteamVR.active) //TESTING
            {
                SwitchToVR();
            }
            else
            {
                SwitchToPC();
            }*/
        }

        public void ActivateVR()
        {
            joinButton.SetActive(false);
            hostButton.SetActive(true);
            pcUI.SetActive(false); //Deactivate pc/mobile player UI
            vrUI.SetActive(true); //Activate VR rig
        }

        public void ActivatePC()
        {
            joinButton.SetActive(true);
            hostButton.SetActive(false);
            pcPlayer.SetActive(true);
            pcUI.SetActive(true);
            vrUI.SetActive(false);
        }
    }
}