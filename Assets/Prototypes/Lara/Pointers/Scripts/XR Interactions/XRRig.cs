using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class XRRig : MonoBehaviour
{
	public static XRRig instance = null;

	#region Props

	public Transform LeftController => leftController;
	public Transform RightController => rightController;
	public Transform Headset => headset;
	public Transform PlayArea => playArea;

	#endregion

	#region Vars

	[SerializeField] private Transform leftController, rightController, headset, playArea;

	private XRController left, right;

	#endregion

	// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only)
	private void OnValidate()
	{
		//Check if the set object is not a Vr Controller, if it isn't, unset it and warn the user.
		if(leftController != null && leftController.GetComponent<XRController>() == null)
		{
			//The object set to this variable is not of type VrController
			leftController = null;
			Debug.LogWarning("The object you are trying to set to left controller does not have VrController component attached!");
		}

		//Check if the set object is not a Vr Controller, if it isn't, unset it and warn the user.
		if(rightController != null && rightController.GetComponent<XRController>() == null)
		{
			//The object set to this variable is not of type VrController
			rightController = null;
			Debug.LogWarning("The object you are trying to set to right controller does not have VrController component attached!");
		}
	}

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
			return;
		}

		//Validate all the variable Transform components
		//Stoopid us proofing
		ValidateComponent(leftController);
		ValidateComponent(rightController);
		ValidateComponent(headset);
		ValidateComponent(playArea);

		//Get the VrControllerComponents from the relevant controllers
		left = leftController.GetComponent<XRController>();
		right = rightController.GetComponent<XRController>();

		//Initialise the two controllers
		left.Initialise();
		right.Initialise();
	}

	/// <summary>
	/// Make sure components are set.
	/// </summary>
	/// <typeparam name="T">Name of the component.</typeparam>
	/// <param name="_component">Transform component param.</param>
	private void ValidateComponent<T>(T _component) where T : Component
	{
		//If the component is null, log out name of component in an error.
		if(_component == null)
		{
			Debug.LogError($"Component{nameof(_component)} is null! This has to be set!");
			#if UNITY_EDITOR
			//The component was null and we are in the editor so stop the editor from playing.
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
	}
}