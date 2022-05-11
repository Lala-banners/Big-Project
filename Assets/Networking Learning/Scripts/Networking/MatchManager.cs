using Mirror;
using UnityEngine.SceneManagement;

namespace NetworkGame.Networking
{
	public class MatchManager : NetworkBehaviour
	{
		public static MatchManager instance = null;

		[SyncVar(hook = nameof(OnReceivedMatchStarted))]
		public bool matchStarted = false;

		// Any match settings you want here
		[SyncVar] public bool doubleSpeed = false;

		private void OnReceivedMatchStarted(bool _old, bool _new)
		{
			// If you want a countdown or some sort of match starting
			// indicator, replace the contents of this function
			// with that and then call the Unload
			
			if(_new)
			{
				SceneManager.UnloadSceneAsync("Lobby");
			}
		}

		protected void Awake()
		{
			if(instance == null)
			{
				instance = this;
			}
			else if(instance != this)
			{
				Destroy(gameObject);
				return;
			}

			// Anything else you need to do in awake
		}

		[Server]
		public void StartMatch() => matchStarted = true;
	}
}