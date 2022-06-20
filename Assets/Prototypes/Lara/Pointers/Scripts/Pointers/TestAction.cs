using UnityEngine;
using Valve.VR;

public class TestAction : MonoBehaviour
{
	// a reference to the action
	public SteamVR_Action_Boolean buttonClick;

	// a reference to the hand
	public SteamVR_Input_Sources handType;

	//reference to the interacting sphere
	public GameObject cube;

	void Start()
	{
		buttonClick.AddOnStateDownListener(TriggerDown, handType);
		buttonClick.AddOnStateUpListener(TriggerUp, handType);
	}

	public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		print("Trigger is up");
		cube.GetComponent<MeshRenderer>().enabled = false;
	}

	public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		print("Trigger is down");
		cube.GetComponent<MeshRenderer>().enabled = true;
	}
}