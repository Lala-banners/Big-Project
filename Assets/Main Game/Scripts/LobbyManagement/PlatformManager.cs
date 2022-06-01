using Lara;
using UnityEngine;
using UnityEngine.XR;

namespace LobbyManagement
{
    public class PlatformManager : MonoBehaviour
    {
        [Header("CHOOSE YOUR DEVICE TO BUILD/PLAY ON")]
        [SerializeField] private BuildVersion deviceToBuildOn  = BuildVersion.Any;
        [Header("VR")]
        [SerializeField] private GameObject vrComponents;
        [Header("PC")]
        [SerializeField] private GameObject pcComponents;

        // To note the player prefab is being changed in the network manager.
        

        private enum BuildVersion
        {
            VR, PC, Any
        }
        private void OnValidate()
        {
            CheckAndEnableVR();
        }

        private void OnEnable()
        {
            CheckAndEnableVR();
        }

        private void Awake()
        {
            CheckAndEnableVR();
        }

        /// <summary>
        /// This checks what build version is selected then will swap to the relevent platform.
        /// </summary>
        private void CheckAndEnableVR()
        {
            if(deviceToBuildOn == BuildVersion.VR)
            {
                if(!VrUtils.IsVREnabled())
                {
                    Debug.LogWarning("No VR Headset found, enabling VR Components anyway");
                } 
                else
                {
                    Debug.Log("VR enabled.");
                }
                SwapToVR();
            } 
            else if (deviceToBuildOn == BuildVersion.PC)
            {
                if(VrUtils.IsVREnabled())
                {
                    Debug.LogWarning("VR found, enabling PC Components anyway");
                } 
                else
                {
                    Debug.Log("PC enabled.");
                }
                SwapToPC();
            } 
            else if(deviceToBuildOn == BuildVersion.Any)
            {
                if(VrUtils.IsVREnabled())
                {
                    Debug.Log("VR Headset found, enabling VR Components");
                    SwapToVR();
                } else
                {
                    Debug.Log("No VR Headset found, enabling PC Components");
                    SwapToPC();
                }
            }
        }

        /// <summary>
        /// This will deactivate the PC Gameobjects,
        /// activate the VR Gameobjects and
        /// change the Player prefab to the VR one.
        /// </summary>
        public void SwapToVR()
        {
            XRSettings.enabled = true;
            pcComponents.gameObject.SetActive(false);
            vrComponents.gameObject.SetActive(true);
        }
        /// <summary>
        /// This will deactivate the VR Gameobjects,
        /// activate the PC Gameobjects and
        /// change the Player prefab to the PC one.  
        /// </summary>
        public void SwapToPC()
        {
            XRSettings.enabled = false;
            vrComponents.gameObject.SetActive(false);
            pcComponents.gameObject.SetActive(true);
        }
    }
}