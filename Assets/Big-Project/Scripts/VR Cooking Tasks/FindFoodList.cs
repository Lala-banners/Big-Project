using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;
using System;

public static class FindFoodList
{
	public static List<Transform> GetAllChildren(this Transform parent, List<Transform> transformList = null)
	{
		if (transformList == null) transformList = new List<Transform>();
          
		foreach (Transform child in parent) {
			transformList.Add(child);
			child.GetAllChildren(transformList);
		}
		return transformList;
	}
}