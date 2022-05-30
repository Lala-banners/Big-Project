using UnityEngine.EventSystems; //Holds all dragging capabilities 
using UnityEngine;


namespace BreadAndButter.Mobile
{
    public enum JoystickAxis 
    {
        None,
        Horizontal,
        Vertical
    }

    public class JoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler //Need interfaces for event systems
    {
        //Joystick = dragging and changing position of things

        #region Properties
        /// <summary>
        /// The input axis value that the joystick represents.
        /// How far horizontal or vertical the joystick goes and direction on each axis.
        /// </summary>
        public Vector2 Axis { get; private set; } = Vector2.zero;
        #endregion

        #region Variables
        [SerializeField]
        private RectTransform handle;

        [SerializeField]
        private RectTransform background;

        [SerializeField, Range(0, 1)]
        private float deadzone = 0.25f;

        private Vector3 initialPosition = Vector3.zero;
        #endregion

        // Start is called before the first frame update
        void Start() => initialPosition = handle.transform.position; //Start() only do one thing = lambda time

        public void OnDrag(PointerEventData _eventData)
        {
            float xDifference = (background.rect.size.x - handle.rect.size.x) * 0.5f;
            float yDifference = (background.rect.size.y - handle.rect.size.y) * 0.5f;

            //Calculate the axis of the input based on event data and the relative position to the background centre
            Axis = new Vector2(
                (_eventData.position.x - background.position.x) / xDifference,
                (_eventData.position.y - background.position.y) / yDifference);

            Axis = (Axis.magnitude > 1.0f) ? Axis.normalized : Axis; //If not a normalised vector then keep it not normalised

            //Apply the axis position to the handle
            handle.transform.position = new Vector3(
                (Axis.x * xDifference) + background.position.x,
                (Axis.y * yDifference) + background.position.y);

            //Apply deadzone effect after the handle has been placed into its correct position
            Axis = (Axis.magnitude < deadzone) ? Vector2.zero : Axis;
            //Condition ? True : False;
        }

        public void OnEndDrag(PointerEventData _eventData)
        {
            //We have let go so reset the axis and set the initial position
            Axis = Vector2.zero;
            handle.transform.position = initialPosition;
        }

        public void OnPointerDown(PointerEventData _eventData) => OnDrag(_eventData);

        public void OnPointerUp(PointerEventData _eventData) => OnEndDrag(_eventData);
    }
}
