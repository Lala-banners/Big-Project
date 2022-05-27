using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace AlleyOop.VR.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour
    {
        //Properties for rb, collider and attach point 
        public Rigidbody Rigidbody => rigidbody;
        public Collider Collider => collider;
        public Transform AttachPoint => attachPoint;

        //These need to be on EVERY object that is interactable
        [SerializeField] private bool isGrabbable = true;
        [SerializeField] private bool isTouchable = false;
        [SerializeField] private bool isUsable = false;

        //The specified interactable object sources are any source in the SteamVR Input Sources
        [SerializeField] private SteamVR_Input_Sources allowedSource = SteamVR_Input_Sources.Any;

        [Space]

        [SerializeField, Tooltip("The point on the interactable object we want to grab, if not set, will use origin")]
        private Transform attachPoint;

        [Space]

        [Header("Interaction Events for interactable objects")]
        public InteractionEvent onGrabbed = new InteractionEvent();
        public InteractionEvent onReleased = new InteractionEvent();
        public InteractionEvent onTouched = new InteractionEvent();
        //CAN'T TOUCH DIS! DA DA DA DA.. DUN DUN.. DUN!
        public InteractionEvent onStopTouching = new InteractionEvent();
        public InteractionEvent onUsed = new InteractionEvent();
        public InteractionEvent onStopUsing = new InteractionEvent();
        

        private new Collider collider;
        private new Rigidbody rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            collider = gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                collider = gameObject.GetComponent<BoxCollider>();

                //Finds specific gameObject that does not have a collider... good way of writing error messages
                Debug.LogError($"Object {name} does not have a collider... adding BoxCollider", gameObject);
            }

            rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private InteractEventArgs GenerateArgs(VrCtrl _controller) => new InteractEventArgs(_controller, rigidbody, collider);

        public void OnObjectGrabbed(VrCtrl _controller)
        {
            //If the object is grabbable or if it is any input source
            if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onGrabbed.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectReleased(VrCtrl _controller)
        {
            //If the object is grabbable or if it is any input source
            if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onReleased.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectTouched(VrCtrl _controller)
        {
            //If the object is being touched or if it is any input source
            if (isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onTouched.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectStopTouching(VrCtrl _controller)
        {
            //If the object is not being touched or if it is any input source
            if (isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onStopTouching.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectUsed(VrCtrl _controller)
        {
            //If the object is being used or if it is any input source
            if (isUsable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onUsed.Invoke(GenerateArgs(_controller));
        }

        public void OnObjectStopUsing(VrCtrl _controller)
        {
            //If the object has stopped being using or if it is any input source
            if (isUsable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
                onStopUsing.Invoke(GenerateArgs(_controller));
        }
    }
}
