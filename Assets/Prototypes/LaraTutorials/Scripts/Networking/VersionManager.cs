using UnityEngine;
using Valve.VR;
using UnityEngine.Android;
using AlleyOop.VR;

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
        }

        public void ActivatePC()
        {
            vrPlayer.SetActive(false);
            joinButton.SetActive(true);
            hostButton.SetActive(false);
            pcPlayer.SetActive(true);
            pcUI.SetActive(true);
            vrUI.SetActive(false);
        }
    }
}