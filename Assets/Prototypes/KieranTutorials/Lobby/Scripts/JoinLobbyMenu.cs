// Creator: Kieran
// Creation Time: 2022/05/19 16:19
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainProject.Prototypes.KieranTutorials.Lobby
{
	// This is for when we connect with an IP.
	public class JoinLobbyMenu : MonoBehaviour
	{
		// Reference to the NetworkManager.
		[SerializeField] private NetworkManagerLobby networkManager = null;

		// Needed references for the UI to work.
		// Make sure they are dragged in.
		[Header("UI")]
		[SerializeField] private GameObject landingPagePanel = null;
		[SerializeField] private TMP_InputField ipAddressInputField = null;
		[SerializeField] private Button joinButton = null;

		// When this GameObject is enabled.
		// Subscribe to the Network Lobby Events for when client connects or disconnects.
		// This is only on OUR [Client].
		// This is on the [Client] not on the [Server] 
		private void OnEnable()
		{
			NetworkManagerLobby.OnClientConnected += HandleClientConnected;
			NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
		}

		// When this GameObject, same as above but unsubscribe from these events.
		private void OnDisable()
		{
			NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
			NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
		}

		// When we press the join button.
		public void JoinLobby()
		{
			// Sets our IP address.
			string ipAddress = ipAddressInputField.text;

			// We set the network address for the networkManager to be this IP Address.
			// This can be a localHost if you are trying to connect to your own network.
			networkManager.networkAddress = ipAddress;
			
			// Start as a [Client] using this ipAddress to connect to.
			networkManager.StartClient();

			// Disable the "Join Button" so they aren't trying to spam it mutiple times.
			joinButton.interactable = false;
		}

		// Called when we manage to join a [Server]
		// We want to reset the UI to how it used to be.
		private void HandleClientConnected()
		{
			joinButton.interactable = true;
			gameObject.SetActive(false);
			landingPagePanel.SetActive(false);
		}

		// Called when we fail to connect.
		// Basically let the player press the Join Button again.
		private void HandleClientDisconnected()
		{
			joinButton.interactable = true;
		}
	}
}