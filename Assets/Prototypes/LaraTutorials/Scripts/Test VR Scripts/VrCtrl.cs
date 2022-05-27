using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace AlleyOop.VR
{
    //Main VR Controller
    [RequireComponent(typeof(SteamVR_Behaviour_Pose))]
    [RequireComponent(typeof(VrCtrlInput))]
    public class VrCtrl : MonoBehaviour
    {
        #region Props
        /// <summary>
        /// How fast the controller is moving in worldspace.
        /// </summary>
        public Vector3 Velocity => pose.GetVelocity();

        public Rigidbody Rigidbody => rb;
        /// <summary>
        /// How fast the controller is rotating and in which direction.
        /// </summary>
        public Vector3 AngularVelocity => pose.GetAngularVelocity();
        public SteamVR_Input_Sources InputSource => pose.inputSource;
        public VrCtrlInput Input => input;
        #endregion

        #region Vars
        private SteamVR_Behaviour_Pose pose;
        private VrCtrlInput input;
        private new Rigidbody rb;
        #endregion

        public void Initialise()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
            input = gameObject.GetComponent<VrCtrlInput>();

            input.Initialise(this);
        }
    }
}
