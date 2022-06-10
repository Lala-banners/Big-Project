using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private QuestManager questManager;

    private void Start()
    {
        
    }

    private void Update()
    {
        Interact();
    }

    public void Interact()
    {
        Ray ray = new Ray(transform.position + transform.forward * 0.5f, transform.forward); //Creates line from player to infinity 
        RaycastHit hitInfo; //Get back info about what we hit
        
        int layerMask = LayerMask.NameToLayer("Interactable");
        
        layerMask = 1 << layerMask;

        //If Ray hits something
        if (Physics.Raycast(ray, out hitInfo, 10f, layerMask))
        {
            if (hitInfo.collider.TryGetComponent(out InWorldItem item))
            {
                Debug.Log(item.name);
            }
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        int layerMask = LayerMask.NameToLayer("Interactable");
        layerMask = 1 << layerMask;
        if (other.gameObject.layer == layerMask)
        {
            Debug.Log("Hit item!");
        }
    }
}
