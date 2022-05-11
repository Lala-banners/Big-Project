// Property of TUNACORN STUDIOS PTY LTD 2018
// 
// Creator: James Mills
// Creation Time: 09/11/2021 1:54 PM

using NetworkGame.Networking;
using UnityEngine;
using UnityEngine.UI;
using NetworkPlayer = NetworkGame.Networking.NetworkPlayer;

namespace NetworkGame
{
	public class Lobby : MonoBehaviour
	{
		[SerializeField] private Button startButton;

		private void Awake()
		{
			startButton.interactable = CustomNetworkManager.Instance.IsHost;
		}

		public void OnClickStartMatch()
		{
			NetworkPlayer localPlayer = CustomNetworkManager.LocalPlayer;
			localPlayer.StartMatch();

			gameObject.SetActive(false);
		}
	}
}