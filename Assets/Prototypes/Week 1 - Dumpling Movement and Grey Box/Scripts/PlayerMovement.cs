using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainProject.Prototypes.Week1
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 2f;
        private Rigidbody myRigidbody;
        // Start is called before the first frame update
        void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}