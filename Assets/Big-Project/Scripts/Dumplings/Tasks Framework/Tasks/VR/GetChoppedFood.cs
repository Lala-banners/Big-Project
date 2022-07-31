using System.Collections.Generic;
using UnityEngine;

public class GetChoppedFood : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		List<GameObject> childObjects = new List<GameObject>();
		foreach(Transform child in allChildren)
		{
			childObjects.Add(child.gameObject);
		}
	}
}