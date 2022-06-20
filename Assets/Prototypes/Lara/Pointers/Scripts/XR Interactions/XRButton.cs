using AlleyOop.VR;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XRButton : MonoBehaviour
{
    private Button thisButton;

    private XRControllerInput[] controllers;
    
    // Start is called before the first frame update
    void Start()
    {
        thisButton = gameObject.GetComponent<Button>();

        controllers = FindObjectsOfType<XRControllerInput>();

        foreach (XRControllerInput input in controllers)
        {
            input.OnGrabPressed.AddListener(_args =>
            {
                // Raycast
                RaycastHit hit;
                if (Physics.Raycast(_args.controller.transform.position, _args.controller.transform.forward, out hit))
                {
                    if (hit.collider.gameObject == thisButton.gameObject)
                    {
                        thisButton.onClick.Invoke();
                    }
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
