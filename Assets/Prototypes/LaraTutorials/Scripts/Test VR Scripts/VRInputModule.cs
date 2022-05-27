using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;


// Add this script to EventSystem
namespace AlleyOop.VR
{
    public class VRInputModule : BaseInputModule
    {
        public Vector3 ControllerPosition { get; set; }
        public bool ControllerButtonDown { get; set; }
        public bool ControllerButtonUp { get; set; }

        private GameObject currentObject = null;
        private PointerEventData data = null;
        private new Camera camera;

        protected override void Awake()
        {
            base.Awake();

            data = new PointerEventData(eventSystem);
        }

        protected override void Start()
        {
            base.Start();
            
            camera = VrRig.instance.Headset.GetComponent<Camera>();
        }
        public override void Process()
        {
            data.Reset();
            data.position = camera.WorldToScreenPoint(ControllerPosition);
            
            // Raycast
            eventSystem.RaycastAll(data, m_RaycastResultCache);
            data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            currentObject = data.pointerCurrentRaycast.gameObject;
            
            // Clear the raycast data
            m_RaycastResultCache.Clear();
            
            // Handle hovering for selectable UI elements
            HandlePointerExitAndEnter(data, currentObject);
            
            // Handle press and releasing of the controller buttons
            if(ControllerButtonDown)
                ProcessPress();

            if (ControllerButtonUp)
                ProcessRelease();

            // Reset the button flags to prevent multiple calling of the event
            ControllerButtonDown = false;
            ControllerButtonUp = false;
        }
        private void ProcessPress()
        {
            //set the press raycast to the current raycast
            data.pointerPressRaycast = data.pointerCurrentRaycast;
            GameObject newPointerPress =
                ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

            if (newPointerPress == null)
            {
                newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
                
            }

            data.pressPosition = data.position;
            data.pointerPress = newPointerPress;
            data.rawPointerPress = currentObject;

        }
        private void ProcessRelease()
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

            GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

            if (data.pointerPress == pointerUpHandler)
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
            
            eventSystem.SetSelectedGameObject(null);
            
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;
        }
    }
}