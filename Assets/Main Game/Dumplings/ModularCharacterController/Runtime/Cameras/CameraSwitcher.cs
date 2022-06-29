// Creator: Kieran
// Creation Time: 2022/06/08 16:29
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController.Cameras
{
	public class CameraSwitcher : MonoBehaviour
	{
		[SerializeField] private MultiCamera multiCamera;
		[SerializeField] private MultiMotor multiMotor;
		[SerializeField] private bool switchCameraInput;
		
		public void SwitchCameraInput()
		{
			Debug.Log("swap");
			//switchCameraInput = true;
		}
		
		[SerializeField] private InputActionReference switchAction;
		
		private void Start()
		{
			switchAction.action.Enable();

			switchAction.action.performed += _context =>
			{
				multiCamera.NextCamera();
				multiMotor.NextMotor();
			};
			
			multiCamera.ActivateCamera(0);
			multiMotor.ActivateMotor(0);
		}

		private void Update()
		{
			if (switchCameraInput)
			{
				SwitchCamera();
			}
		}
		public void SwitchCamera()
		{
			multiCamera.NextCamera();
			multiMotor.NextMotor();
		}
		private void Reset()
		{
			if(multiCamera == null)
			{
				multiCamera = GetComponentInChildren<MultiCamera>();

				if(multiCamera != null)
					Debug.Log($"Added attached {multiCamera}", this);
			}

			if(multiMotor == null)
			{
				multiMotor = GetComponentInChildren<MultiMotor>();

				if(multiMotor != null)
					Debug.Log($"Added attached {multiMotor}", this);
			}
		}
	}
}