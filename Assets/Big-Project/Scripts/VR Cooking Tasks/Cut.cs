using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour
{
	private Rigidbody rb;
	//public int sliceIndex;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.useGravity = false;
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name == "Prop_Butcher'sKnife")
		{
			rb.constraints = RigidbodyConstraints.None;
			rb.useGravity = true;
			transform.parent = null;
		}
		
		//ResetRigidbody();
	}

	public void ResetRigidbody()
	{
		//sliceIndex = 0;
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;
	}
}
