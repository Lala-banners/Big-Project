using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Valve.VR;


// Add this script to the controllers

[RequireComponent(typeof(XRControllerInput))]
public class VRUGUIPointer : MonoBehaviour
{
	[SerializeField] private SteamVR_Action_Boolean clickAction;
	[SerializeField] private LayerMask uIMask;
	[SerializeField] private XRPointer pointer;

	private VRInputModule inputModule;

	// Start is called before the first frame update
	void Start()
	{
		inputModule = FindObjectOfType<VRInputModule>();
	}

	// Update is called once per frame
	private void Update()
	{
		inputModule.ControllerButtonDown = clickAction.stateDown;
		inputModule.ControllerButtonUp = clickAction.stateUp;

		Vector3 position = Vector3.zero;
		bool hitUI = false;
		if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, uIMask))
		{
			position = hit.point;
			hitUI = true;
		}

		inputModule.ControllerPosition = position;
		if(pointer != null)
		{
			pointer.Active = hitUI;
		}
	}
}