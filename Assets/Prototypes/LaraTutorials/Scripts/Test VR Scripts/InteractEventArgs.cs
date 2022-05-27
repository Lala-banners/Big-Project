using UnityEngine;
using UnityEngine.Events;

using Serializable = System.SerializableAttribute;

namespace AlleyOop.VR.Interaction
{
    [Serializable]
    public class InteractionEvent : UnityEvent<InteractEventArgs> { }
    
    [Serializable]
    public class InteractEventArgs
    {
        /// <summary>
        /// The VR controller of the interactable object we are interacting with
        /// </summary>
        public VrCtrl controller;

        /// <summary>
        /// The rigidbody of the interactable object we are interacting with
        /// </summary>
        public Rigidbody rb;

        /// <summary>
        /// The collider of the interactable object we are interacting with
        /// </summary>
        public Collider collider;

        public InteractEventArgs(VrCtrl _controller, Rigidbody _rb, Collider _collider)
        {
            controller = _controller;
            rb = _rb;
            collider = _collider;
        }
    }
    
}
