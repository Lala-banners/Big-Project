using Lara;
using UnityEngine;
using UnityEngine.XR;

namespace LobbyManagement
{
    /// <summary>
    /// This is just the platform we are playing on or using.
    /// </summary>
    public enum PlatformPlayingOn
    { 
        VR, PC, Any
    }
    public class PlatformManager : MonoBehaviour
    {
        [Header("CHOOSE YOUR DEVICE TO BUILD/PLAY ON")]
        [SerializeField] private PlatformPlayingOn deviceToBuildOn  = PlatformPlayingOn.Any;
        [Header("VR")]
        [SerializeField] private GameObject vrComponents;
        [Header("PC")]
        [SerializeField] private GameObject pcComponents;

        // To note the player prefab is being changed in the network manager.

        
        /// <summary>
        /// Used to do this on Validate but was causing issues.
        /// </summary>
        private void Awake()
        {
            CheckAndEnableVR();
        }

        /// <summary>
        /// This checks what build version is selected then will swap to the relevent platform.
        /// </summary>
        private void CheckAndEnableVR()
        {
            if(deviceToBuildOn == PlatformPlayingOn.VR)
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
            else if (deviceToBuildOn == PlatformPlayingOn.PC)
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
            else if(deviceToBuildOn == PlatformPlayingOn.Any)
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
            //XRSettings.enabled = true;
            
            VrUtils.SetVREnabled(true);
            
            if (pcComponents.gameObject != null) 
                pcComponents.gameObject.SetActive(false);
            
            if (vrComponents.gameObject != null) 
                vrComponents.gameObject.SetActive(true);

            CustomNetworkManager customNetworkManager = FindObjectOfType<CustomNetworkManager>();
            if (customNetworkManager != null)
                customNetworkManager.CurrentPlatformPlayingOn = PlatformPlayingOn.VR;
        }
        /// <summary>
        /// This will deactivate the VR Gameobjects,
        /// activate the PC Gameobjects and
        /// change the Player prefab to the PC one.  
        /// </summary>
        public void SwapToPC()
        {
            //XRSettings.enabled = false;
            
            VrUtils.SetVREnabled(false);

            if (vrComponents.gameObject != null)
                vrComponents.gameObject.SetActive(false);
            
            if (pcComponents.gameObject != null)
                pcComponents.gameObject.SetActive(true);
            
            CustomNetworkManager customNetworkManager = FindObjectOfType<CustomNetworkManager>();
            if (customNetworkManager != null)
                customNetworkManager.CurrentPlatformPlayingOn = PlatformPlayingOn.PC;
        }
    }
}