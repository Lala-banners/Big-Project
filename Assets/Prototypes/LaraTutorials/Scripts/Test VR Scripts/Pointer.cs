using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AlleyOop;

namespace AlleyOop.VR
{
    public class Pointer : MonoBehaviour
    {
        private const float tracerWidth = 0.025f;
        public Vector3 Endpoint { get; private set; } = Vector3.zero;
        public bool Active { get; set; } = false;

        [SerializeField] private float cursorScaleFactor = 0.1f;
         public VrCtrl controller;

        [SerializeField] Color invalid = Color.red;
        [SerializeField] Color valid = Color.green;

        private Transform cursor;
        private Transform tracer;

        private Renderer cursorRender;
        private Renderer tracerRender;
        // Start is called before the first frame update
        void Start()
        {
            controller.Input.OnPointerPressed.AddListener(_args =>
            {
                Active = true;
                cursor.gameObject.SetActive(true);
                tracer.gameObject.SetActive(true);
            });
            controller.Input.OnPointerReleased.AddListener(_args =>
            {
                Active = false;
                cursor.gameObject.SetActive(false);
                tracer.gameObject.SetActive(false);
            });
            CreatePointer();
            cursor.gameObject.SetActive(false);
            tracer.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (!Active)
                return;

            bool didHit = Physics.Raycast(controller.transform.position, controller.transform.forward, out RaycastHit hit);
            Endpoint = didHit ? hit.point : Vector3.zero;
            UpdateScalePos(hit, didHit);
            SetValid(didHit);
        }
        
        public void SetValid(bool _valid)
        {
            cursorRender.material.color = _valid ? valid : invalid;
            tracerRender.material.color = _valid ? valid : invalid;
        }
        private void UpdateScalePos(RaycastHit _hit, bool _didHit)
        {
            if (_didHit)
            {
                CalculateDirAndDist(controller.transform.position, _hit.point, out Vector3 dir, out float distance);

                // set the tracer position to the midpoint of the parent pos and the endpoint
                Vector3 midPoint = Vector3.Lerp(controller.transform.position, controller.transform.position + dir * distance, .5f);
                tracer.position = midPoint;

                // scale the tracer to between the endpoint and this point
                tracer.localScale = new Vector3(tracerWidth, tracerWidth, distance);

                
                // set the cursor to the endpoint and scale it
                cursor.position = _hit.point;
                cursor.localScale = Vector3.one * cursorScaleFactor;
                Debug.Log(_hit.collider.gameObject);

              /*  if(_hit.collider.gameObject.CompareTag("Button"))
                {
                    Button newButton = gameObject.GetComponent<Button>();
                    newButton.onClick();
                }*/
            }
            else
            {
                // set the cursor and tracer positive / scale value based on an arbitrary endpoint
                CalculateDirAndDist(controller.transform.position, 
                    controller.transform.position + controller.transform.forward * 100,
                    out Vector3 dir,
                    out float distance);

                Vector3 midPoint = Vector3.Lerp(controller.transform.position, controller.transform.position + dir * distance, .5f);
                tracer.position = midPoint;
                tracer.localScale = new Vector3(tracerWidth, tracerWidth, distance);

                cursor.position = controller.transform.position + controller.transform.forward * 100f;
                cursor.localScale = Vector3.one * cursorScaleFactor;
            }

        }


        private void CalculateDirAndDist(Vector3 _start, Vector3 _end, out Vector3 _dir, out float _distance)
        {
            Vector3 heading = _end - _start;
            _distance = heading.magnitude;
            _dir = heading / _distance;
        }
        private void CreatePointer()
        {
            GameObject tracerObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject cursorObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            tracerObj.layer = 2;
            cursorObj.layer = 2;

            Destroy(tracerObj.GetComponent<BoxCollider>());
            Destroy(cursorObj.GetComponent<SphereCollider>());

            tracer = tracerObj.transform;
            cursor = cursorObj.transform;

            tracer.parent = controller.transform;
            cursor.parent = controller.transform;

            tracerRender = tracer.GetComponent<Renderer>();
            cursorRender = cursor.GetComponent<Renderer>();

            SetValid(false);


        }
    }
}

