﻿using UnityEngine;
using Valve.VR;

public class Squishy : MonoBehaviour
{
	public InteractableObject interactable;
	public new SkinnedMeshRenderer renderer;

	public bool affectMaterial = true;

	public SteamVR_Action_Single gripSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");

	public SteamVR_Action_Single pinchSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");

	private new Rigidbody rigidbody;

	private void Start()
	{
		if(rigidbody == null)
			rigidbody = GetComponent<Rigidbody>();

		if(interactable == null)
			interactable = GetComponent<InteractableObject>();

		if(renderer == null)
			renderer = GetComponent<SkinnedMeshRenderer>();
	}

	private void Update()
	{
		float grip = 0;
		float pinch = 0;

		if(interactable.AttachPoint)
		{
			grip = gripSqueeze.GetAxis(interactable.Collider.GetComponent<SteamVR_Input_Sources>());
			pinch = pinchSqueeze.GetAxis(interactable.Collider.GetComponent<SteamVR_Input_Sources>());
		}

		renderer.SetBlendShapeWeight(0, Mathf.Lerp(renderer.GetBlendShapeWeight(0), grip * 100, Time.deltaTime * 10));

		if(renderer.sharedMesh.blendShapeCount > 1) // make sure there's a pinch blend shape
			renderer.SetBlendShapeWeight(1, Mathf.Lerp(renderer.GetBlendShapeWeight(1), pinch * 100, Time.deltaTime * 10));

		if(affectMaterial)
		{
			renderer.material.SetFloat("_Deform", Mathf.Pow(grip * 1f, 0.5f));
			if(renderer.material.HasProperty("_PinchDeform"))
			{
				renderer.material.SetFloat("_PinchDeform", Mathf.Pow(pinch * 1f, 0.5f));
			}
		}
	}
}