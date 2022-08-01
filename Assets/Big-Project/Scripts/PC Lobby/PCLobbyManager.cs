using UnityEngine;

namespace Big_Project.Scripts.PC_Lobby
{
    public class PCLobbyManager : MonoBehaviour
    {
        [SerializeField] private GameObject player1NotReady;
        [SerializeField] private GameObject player1Ready;
        [SerializeField] private GameObject player2NotReady;
        [SerializeField] private GameObject player2Ready;
        [SerializeField] private GameObject player3NotReady;
        [SerializeField] private GameObject player3Ready;
        [SerializeField] private GameObject player4NotReady;
        [SerializeField] private GameObject player4Ready;
        
        public void StartGame()
        {
            this.gameObject.SetActive(false);
        }
        
        public void QuitGame()
        {
            Debug.Log("Game Over");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
        }
        
    }
}
