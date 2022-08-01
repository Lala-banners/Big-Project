using System;
using TMPro;
using UnityEngine;
namespace Big_Project.Scripts.Dumplings.DeathAndRespawning
{
    public class DumplingControllerDeathAndRespawning : MonoBehaviour
    {
        [Header("Lives Manager")]
        [SerializeField] private GameObject livesCanvas;
        [SerializeField] private TMP_Text livesTMPText;
        [SerializeField] private GameObject happyDumpling;
        [SerializeField] private GameObject scaredDumpling;
        private Rigidbody dumplingRigidbody;
        private LivesManager livesManagerInScene;

        private void OnTriggerEnter(Collider _other)
        {
            DeathCollider deathCollider = _other.gameObject.GetComponentInChildren<DeathCollider>();
            
            if(deathCollider != null) Die();
        }
        
        private void Start()
        {
            dumplingRigidbody = GetComponent<Rigidbody>();
            livesManagerInScene = FindObjectOfType<LivesManager>();
            livesCanvas.SetActive(true);
            UpdateLivesUI();
        }
        
        public void Die()
        {
            if (livesManagerInScene.AreThereAnyLivesLeft())
            {
                Respawn();
                livesManagerInScene.LoseLive();
            }
            else
            {
                QuitGame();
            }
            UpdateLivesUI();
        }
        
        public void UpdateLivesUI()
        {
            int currentLives = livesManagerInScene.LivesLeft;
            if (currentLives > 1)
            {
                happyDumpling.SetActive(true);
                scaredDumpling.SetActive(false);
                livesTMPText.text = ($"{currentLives} Lives");
            }
            else
            {
                happyDumpling.SetActive(false);
                scaredDumpling.SetActive(true);
                livesTMPText.text = ($"{currentLives} Live");
            }
        }
        
        public void Respawn()
        {
            dumplingRigidbody.velocity = Vector3.zero;
            dumplingRigidbody.angularVelocity = Vector3.zero;
            SpawnPoint spawnPoint = FindObjectOfType<SpawnPoint>();
            gameObject.transform.position = spawnPoint.SpawnPointTransform.position;
            gameObject.transform.rotation = spawnPoint.SpawnPointTransform.rotation;    
        }
        
        private void QuitGame()
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
