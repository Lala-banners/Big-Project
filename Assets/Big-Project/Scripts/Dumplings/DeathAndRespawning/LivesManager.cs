using System.Collections.Generic;
using UnityEngine;
namespace Big_Project.Scripts.Dumplings.DeathAndRespawning
{
    public sealed class LivesManager : MonoBehaviour
    {
        #region MakeSingletonAndSetLives

        private static LivesManager instance;

        public static LivesManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<LivesManager>();

                    if (instance == null)
                    {
                        GameObject tempGameObject = new GameObject();
                        tempGameObject.name = typeof(LivesManager).Name;
                        instance = tempGameObject.AddComponent<LivesManager>();
                    }
                }
                return instance;
            }
        }

        public void Awake()
        {
            if (instance == null)
            {
                instance = this as LivesManager;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            currentLives = startingLivesAmount;
        }
        #endregion
        [SerializeField] private int currentLives = 10;
        [SerializeField] private int startingLivesAmount = 10;

        public int LivesLeft => currentLives;

        public bool AreThereAnyLivesLeft()
        {
            return currentLives >= 0;
        }
        public void LoseLive()
        {
            currentLives--;

            DumplingControllerDeathAndRespawning[] allDumplingControllersDeathAndRespawning = FindObjectsOfType<DumplingControllerDeathAndRespawning>();
            
            foreach (DumplingControllerDeathAndRespawning dumplingLiveManager in allDumplingControllersDeathAndRespawning)
            {
                dumplingLiveManager.UpdateLivesUI();
            }
        }
    }
}
