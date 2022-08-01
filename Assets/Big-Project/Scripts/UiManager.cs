using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UiManager : MonoBehaviour
{
	[Header("General UI")]
	//Bool that can be called from anywhere to pause the game.
	public static bool gameIsPaused;
	public GameObject pauseUI;
	public GameObject playerHUD;
	private Button quitGameButton;
	
	//References to XR Controllers
	[Header("VR")] 
	[SerializeField]private GameObject leftHand;
	[SerializeField]private GameObject rightHand;

	private void Start()
	{
		leftHand = GameObject.Find("Left Hand");
		rightHand = GameObject.Find("Right Hand");
	}

	private void Update()
	{
		//Turn to new input system.
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			gameIsPaused = !gameIsPaused;
			PauseGame();
		}
	}

	void PauseGame()
	{
		if(gameIsPaused)
		{
			Time.timeScale = 0f;

			pauseUI.SetActive(true);
			playerHUD.SetActive(false);

			//Get access to VR hands and deactivate hands
			DeactivateXRControllersOnPause();
		}
		else
		{
			Time.timeScale = 1;

			pauseUI.SetActive(false);
			
			playerHUD.SetActive(true);

			ActivateXRControllers();
		}
	}

	private void QuitGame()
	{
		Application.Quit();
	}

	/// <summary>
	/// Deactivate controller model on hands
	/// and make them not enabled.
	/// </summary>
	private void DeactivateXRControllersOnPause()
	{
		//Hide Models
		leftHand.GetComponent<XRController>().hideControllerModel = true;
		rightHand.GetComponent<XRController>().hideControllerModel = true;

		//Make hands not enabled
		leftHand.GetComponent<XRController>().enabled = false;
		rightHand.GetComponent<XRController>().enabled = false;
	}

	/// <summary>
	/// Reactivate off pause.
	/// </summary>
	private void ActivateXRControllers()
	{
		//Show Models
		leftHand.GetComponent<XRController>().hideControllerModel = false;
		rightHand.GetComponent<XRController>().hideControllerModel = false;

		//Make hands enabled again
		leftHand.GetComponent<XRController>().enabled = true;
		rightHand.GetComponent<XRController>().enabled = true;
	}
	
	private void ReturnToMain()
	{
		//Return to main menu (when that gets done)
	}
}