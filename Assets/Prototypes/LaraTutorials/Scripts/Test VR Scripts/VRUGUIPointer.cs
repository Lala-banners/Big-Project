using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


// Add this script to the controllers
namespace AlleyOop.VR
{
    [RequireComponent(typeof(VrCtrlInput))]
    public class VRUGUIPointer : MonoBehaviour
    {
        [SerializeField] private SteamVR_Action_Boolean clickAction;
        [SerializeField] private LayerMask uiMask;
        [SerializeField] private Pointer pointer;
        private VRInputModule inputModule;
        // Start is called before the first frame update
        void Start()
        {
            inputModule = FindObjectOfType<VRInputModule>();
        }

        // Update is called once per frame
        private void Update()
        {
            inputModule.ControllerButtonDown = clickAction.stateDown;
            inputModule.ControllerButtonUp = clickAction.stateUp;

            Vector3 position = Vector3.zero;
            bool hitUI = false;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, uiMask))
            {
                position = hit.point;
                hitUI = true;
            }

            inputModule.ControllerPosition = position;
            if (pointer != null)
            {
                pointer.Active = hitUI;
            }
        }
    }
}