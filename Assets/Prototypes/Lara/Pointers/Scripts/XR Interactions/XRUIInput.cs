using UnityEngine;

public class XRUIInput : MonoBehaviour
{
	public static GameObject currentObject;
	private int currentID;

	private void Start()
	{
		currentObject = null;
		currentID = 0;
	}

	private void Update()
	{
		//Sends out raycast and returns an array with everything it hit
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
		
		//Goes through all the hit objects and checks if any of them are buttons
		for(int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			
			//Use the object ID to determine if this object has already been found
			int id = hit.collider.gameObject.GetInstanceID();
			
			//If not, then run code again
			if(currentID != id)
			{
				currentID = id;
				currentObject = hit.collider.gameObject;
				
				//Checks based off the gameobject name
				string name = currentObject.name;
				if(name == "Button")
				{
					Debug.Log($"Hit {hit.collider.gameObject.name}");
				}

				#region Checks based off tag
				/*string tag = currentObject.tag;
				if(tag == "Button")
				{
					Debug.Log($"Hit {hit.collider.gameObject.name}");
				}*/
				#endregion
			}
		}
	}
}
