using Lara;
using UnityEngine;

namespace LobbyManagement
{
    public class PlatformManager : MonoBehaviour
    {
        [Header("CHOOSE YOUR DEVICE TO BUILD/PLAY ON")]
        [SerializeField] private BuildVersion deviceToBuildOn  = BuildVersion.Any;
        [Header("VR")]
        [SerializeField] private GameObject vrComponents;
        [SerializeField] private GameObject vrPlayerPrefab;
        [Header("PC")]
        [SerializeField] private GameObject pcComponents;
        [SerializeField] private GameObject pcPlayerPrefab;


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
                TurnOnVR();
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
                TurnOnPC();
            } 
            else if(deviceToBuildOn == BuildVersion.Any)
            {
                if(VrUtils.IsVREnabled())
                {
                    Debug.Log("VR Headset found, enabling VR Components");
                    TurnOnVR();
                } else
                {
                    Debug.Log("No VR Headset found, enabling PC Components");
                    TurnOnPC();
                }
            }
        }

        private void TurnOnVR()
        {
            vrComponents.gameObject.SetActive(true);
            pcComponents.gameObject.SetActive(false);
            FindObjectOfType<CustomNetworkManager>().playerPrefab = vrPlayerPrefab.gameObject;
            //CustomNetworkManager.singleton.playerPrefab = vrPlayerPrefab.gameObject;
        }
        
        private void TurnOnPC()
        {
            vrComponents.gameObject.SetActive(false);
            pcComponents.gameObject.SetActive(true);
            FindObjectOfType<CustomNetworkManager>().playerPrefab = pcPlayerPrefab.gameObject;
            //CustomNetworkManager.singleton.playerPrefab = pcPlayerPrefab.gameObject;
        }
    }
}