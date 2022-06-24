using UnityEngine;
using UnityEngine.UI;

public class XRButton : MonoBehaviour
{
    private Button thisButton;

    private XRControllerInput[] controllers;
    
    void Start()
    {
        Debug.Log("This script is being called!");
        
        thisButton = gameObject.GetComponent<Button>();

        controllers = FindObjectsOfType<XRControllerInput>();

        foreach (XRControllerInput input in controllers)
        {
            Debug.Log("This script is still being called!");
            input.OnGrabPressed.AddListener(_args =>
            {
                Debug.Log("This script is still still being called!");
                // Raycast
                RaycastHit hit;
                if (Physics.Raycast(_args.controller.transform.position, _args.controller.transform.forward, out hit))
                {
                    Debug.Log("This script is still still still being called!");
                    if (hit.collider.gameObject == thisButton.gameObject)
                    {
                        thisButton.onClick.Invoke();
                    }
                }
            });
        }
    }
}
