using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MainProject.Prototypes.Week1
{
    public class PlayerMovement : MonoBehaviour, Dumpling.IPlayerActions
    {
        [SerializeField] private float movementSpeed = 2f;
        private Rigidbody myRigidbody;
        private Vector3 horizontalInput;
        private Vector3 verticalInput;
        private Vector3 direction;
        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
            if (myRigidbody is null)
                Debug.LogError("Rigidbody is NULL!");
        }

        private void FixedUpdate()
        {
            Vector3 finalDirection = new Vector3(direction.x, 0, direction.y);
            myRigidbody.velocity = finalDirection * movementSpeed;
        }

        /// <summary>
        /// I want this to move forward, back left and right.
        /// </summary>
        /// <param name="context">The input system WASD.</param>
        public void OnMove(InputAction.CallbackContext context)
        {
            direction = context.ReadValue<Vector2>();
            Debug.Log(direction);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
    }
}