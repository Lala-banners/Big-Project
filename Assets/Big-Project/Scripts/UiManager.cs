using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UiManager : MonoBehaviour
{
	[Header("General UI")]
	//Bool that can be called from anywhere to pause the game.
	public static bool gameIsPaused;
	public GameObject pauseUI;
	private Button quitGameButton;
	
	//References to XR Controllers
	[Header("VR"), SerializeField] 
	private GameObject leftHand;
	private GameObject rightHand;

	private void Update()
	{
		quitGameButton.onClick.AddListener(QuitGame);

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

			//Get access to VR hands and deactivate hands
			DeactivateXRControllersOnPause();
		}
		else
		{
			Time.timeScale = 1;

			pauseUI.SetActive(false);

			ActivateXRControllers();
		}
	}

	public void InitialiseDumplingUI()
	{
		quitGameButton = GameObject.Find("Quit").GetComponent<Button>();
		pauseUI = GameObject.Find("Pause Canvas");

		//Turn off Pause UI
		pauseUI.SetActive(false);

		leftHand = GameObject.Find("Left Hand");
		rightHand = GameObject.Find("Right Hand");
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