using UnityEngine;

namespace AlleyOop.VR.Interaction
{
    [RequireComponent(typeof(VrCtrlInput))]
    public class InteractableGrab : MonoBehaviour
    {
        //For if we want to do something that is specific to the object. Eg turns red or increment counter
        public InteractionEvent grabbed = new InteractionEvent();
        public InteractionEvent released = new InteractionEvent();

        private VrCtrlInput input;
        private InteractableObject collidingObject; //We are colliding with that we are not holding
        private InteractableObject heldObject; //Object we are holding

        //before held object gets parented to this controller
        private Transform heldOriginalParent;

        // Start is called before the first frame update
        void Start()
        {
            input = gameObject.GetComponent<VrCtrlInput>();

            input.OnGrabPressed.AddListener((_args) => { if (collidingObject != null) GrabObject(); });
            input.OnGrabReleased.AddListener((_args) => { if (heldObject != null) ReleaseObject(); });
        }

        private void SetCollidingObject(Collider _other)
        {
            InteractableObject interactable = _other.GetComponent<InteractableObject>();

            //Prevents overiding interactable object, only registers first held object
            if (collidingObject != null || interactable == null) return;
            collidingObject = interactable;
        }

        // OnTriggerEnter is called when the Collider other enters the trigger
        private void OnTriggerEnter(Collider _other) => SetCollidingObject(_other);

        // OnTriggerExit is called when the Collider other has stopped touching the trigger
        private void OnTriggerExit(Collider _other)
        {
            if (collidingObject == _other.GetComponent<InteractableObject>()) collidingObject = null;
        }

        private void GrabObject()
        {
            heldObject = collidingObject;
            collidingObject = null;

            heldOriginalParent = heldObject.transform.parent;
            heldObject.Rigidbody.isKinematic = true;
            SnapObject(heldObject.transform, heldObject.AttachPoint);

            heldObject.OnObjectGrabbed(input.Controller);
            grabbed.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
        }

        private void ReleaseObject()
        {
            heldObject.Rigidbody.isKinematic = false;
            heldObject.transform.SetParent(heldOriginalParent);

            heldObject.Rigidbody.velocity = input.Controller.Velocity;
            heldObject.Rigidbody.angularVelocity = input.Controller.AngularVelocity;

            heldObject.OnObjectReleased(input.Controller);
            released.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
            heldObject = null;
        }

        private void SnapObject(Transform _object, Transform _snapHandle)
        {
            Rigidbody attachPoint = input.Controller.Rigidbody;
            _object.transform.SetParent(transform);
            if(_snapHandle == null)
            {
                //Reset to same as controller pos + rotation
                _object.localPosition = Vector3.zero;
                _object.localRotation = Quaternion.identity;
            }
            else
            {
                //Calculate correct pos and rot based on snap handle
                _object.rotation = attachPoint.transform.rotation * Quaternion.Euler(_snapHandle.localEulerAngles);
                _object.position = attachPoint.transform.position - (_snapHandle.position - _object.position);

            }
        }
    }
}
