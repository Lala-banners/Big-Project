using UnityEngine;
using Valve.VR;

//Main VR Controller
[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
[RequireComponent(typeof(XRControllerInput))]
public class XRController : MonoBehaviour
{
	#region Props

	/// <summary>
	/// How fast the controller is moving in world space.
	/// </summary>
	public Vector3 Velocity => pose.GetVelocity();

	public Rigidbody Rigidbody => rb;

	/// <summary>
	/// How fast the controller is rotating and in which direction.
	/// </summary>
	public Vector3 AngularVelocity => pose.GetAngularVelocity();

	public SteamVR_Input_Sources InputSource => pose.inputSource;
	public XRControllerInput Input => input;

	#endregion

	#region Vars

	private SteamVR_Behaviour_Pose pose;
	private XRControllerInput input;
	private new Rigidbody rb;

	#endregion

	public void Initialise()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
		input = gameObject.GetComponent<XRControllerInput>();

		input.Initialise(this);
	}
}