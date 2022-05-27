using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Serializable = System.SerializableAttribute;

namespace AlleyOop.VR
{
    [Serializable]
    public class VRInputEvent : UnityEvent<InputEventArgs> 
    {

    }

    [Serializable]
    public class InputEventArgs 
    {
        /// <summary>
        /// The controller firing the event.
        /// </summary>
        public VrCtrl controller;

        /// <summary>
        /// The input source the event is coming from.
        /// </summary>
        public SteamVR_Input_Sources source;

        /// <summary>
        /// The position the player is touching the touchpad on.
        /// </summary>
        public Vector2 touchPadAxis;

        /// <summary>
        /// Constructor to hold the controller, source and touch pad axis events.
        /// </summary>
        public InputEventArgs(VrCtrl _controller, SteamVR_Input_Sources _source, Vector2 _touchPadAxis)
        {
            controller = _controller;
            source = _source;
            touchPadAxis = _touchPadAxis;
        }
    }
}
