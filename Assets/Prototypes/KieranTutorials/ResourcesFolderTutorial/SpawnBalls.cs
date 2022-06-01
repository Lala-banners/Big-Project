using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototypes.KieranTutorials.ResourcesFolderTutorial
{
    public class SpawnBalls : MonoBehaviour
    {
        [SerializeField] private GameObject[] allBalls;

        // Start is called before the first frame update
        void Start()
        {
            allBalls = Resources.LoadAll<GameObject>("BallPrefabs");

            for(int i = 0; i < allBalls.Length; i++)
            {
                Instantiate(allBalls[i],this.transform.position,this.transform.rotation);
            }
        }
    }
}