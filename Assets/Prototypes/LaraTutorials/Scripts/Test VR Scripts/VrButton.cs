using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using AlleyOop.VR;

public class VrButton : MonoBehaviour
{


    private Button thisButton;

    private VrCtrlInput[] controllers;


    // Start is called before the first frame update
    void Start()
    {
        thisButton = gameObject.GetComponent<Button>();

        controllers = FindObjectsOfType<VrCtrlInput>();

        foreach (VrCtrlInput input in controllers)
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
}
