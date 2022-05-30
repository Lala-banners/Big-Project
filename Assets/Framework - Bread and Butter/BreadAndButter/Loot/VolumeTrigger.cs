using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButter
{
    [RequireComponent(typeof(Rigidbody))]
    public class VolumeTrigger : Trigger
    {
        // Start is called before the first frame update
        void Start()
        {
            //Validate there is a collider, if non present
            //Add BoxCollider
            Collider col = gameObject.GetComponent<Collider>();
            if (col == null)
                col = gameObject.AddComponent<BoxCollider>();

            //Force collider to be a trigger
            col.isTrigger = true;

            //Prevent this rigidbody from moving at all using physics
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        // OnTriggerEnter is called when the Collider other enters the trigger
        private void OnTriggerEnter(Collider other) => TriggerEvent();


    }
}
