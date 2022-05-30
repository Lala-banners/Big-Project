using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LobbyManagement
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private GameObject vrComponents;
        [SerializeField] private GameObject pcComponents;
        [SerializeField] private BuildVersion deviceToBuildOn= BuildVersion.Any;


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
                    Debug.LogWarning("No VR Headset found, enabling PC Components anyway");
                }
                vrComponents.gameObject.SetActive(true);
                pcComponents.gameObject.SetActive(false);
            } 
            else if (deviceToBuildOn == BuildVersion.PC)
            {
                if(VrUtils.IsVREnabled())
                {
                    Debug.LogWarning("VR found, enabling PC Components anyway");
                }
                vrComponents.gameObject.SetActive(false);
                pcComponents.gameObject.SetActive(true);
            } 
            else if(deviceToBuildOn == BuildVersion.Any)
            {
                if(VrUtils.IsVREnabled())
                {
                    Debug.Log("VR Headset found, enabling VR Components");
                    vrComponents.gameObject.SetActive(true);
                    pcComponents.gameObject.SetActive(false);
                } else
                {
                    Debug.Log("No VR Headset found, enabling VR Components");
                    vrComponents.gameObject.SetActive(false);
                    pcComponents.gameObject.SetActive(true);
                }
            }
        }
    }
}