using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

namespace LobbyManagement
{
    public static class VrUtils
    {
        /// <summary>
        /// Create a new set of XRInput. XR = mixed reality
        /// </summary>
        private static List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();

        /// <summary>
        /// Enable or disable VR, to be called on any script.
        /// </summary>
        /// <param name="_enabled">Enable or disable VR.</param>
        public static void SetVREnabled(bool _enabled)
        {
            //Get all the connected XR devices.
            SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

            //Loop through all XR devices.
            foreach(XRInputSubsystem subsystem in subsystems)
            {
                //If we want to enable it, start it, otherwise stop it.
                if(_enabled)
                {
                    subsystem.Start();
                } else
                {
                    subsystem.Stop();
                }
            }
        }

        /// <summary>
        /// Returns true if VR is Enabled currently.
        /// </summary>
        /// <returns> True = VR Enabled || False = VR Not Enabled </returns>
        public static bool IsVREnabled()
        {
            //Get all the connected XR devices.
            SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
                
            //Loop through all XR devices.
            foreach(XRInputSubsystem subsystem in subsystems)
            {
                //Check if the subsystem is active, if so return true
                if(subsystem.running)
                {
                    return true;
                }
            }

            //No active XR device
            return false;
        }
    }
}