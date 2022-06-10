// Creator: Kieran
// Creation Time: 2022/06/10 15:23
using BreadAndButter;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Main_Game.Scripts.MainMenu
{
	public class MainMenuManager : MonoBehaviour
	{
		[SerializeField] private string gamePlayScene;
		public void PlayGame_Button()	
		{
			SceneManager.LoadScene(gamePlayScene);
		}
		
		public void Options_Button()
		{
			Debug.Log("PLEASE CREATE THIS OPTIONS MENU");
		}
		
		public void ExitGame_Button()
		{
		#if UNITY_STANDALONE
			Application.Quit();
		#endif
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
		}
	}
}