using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRInput : BaseInput
{
	public Camera eventCamera = null;

	//VR Input 
	
	
	//VR Input Button
	
	protected override void Awake()
	{
		
	}

	public override bool GetMouseButton(int _button)
	{
		return true;
	}
	
	public override bool GetMouseButtonDown(int _button)
	{
		return true;
	}
	
	public override bool GetMouseButtonUp(int _button)
	{
		return true;
	}

	public override Vector2 mousePosition => Vector2.zero;
}
