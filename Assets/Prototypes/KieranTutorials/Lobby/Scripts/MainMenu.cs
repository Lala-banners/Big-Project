// Creator: Kieran
// Creation Time: 2022/05/19 16:10
using UnityEngine;

namespace MainProject.Prototypes.KieranTutorials.Lobby
{
	public class MainMenu : MonoBehaviour
	{
		// Reference to the Network Manager.
		[SerializeField] private NetworkManagerLobby networkManager = null;

		// Reference to the LandingPagePanel.
		[Header("UI")]
		[SerializeField] private GameObject landingPagePanel = null;

		// When we press the host button.
		public void HostLobby()
		{
			// Tell the Network Manager to start as a host.
			networkManager.StartHost();

			// Disable the LandingPagePanel.
			landingPagePanel.SetActive(false); // When we make the Lobby UI we will turn this on.
		}
	}
}