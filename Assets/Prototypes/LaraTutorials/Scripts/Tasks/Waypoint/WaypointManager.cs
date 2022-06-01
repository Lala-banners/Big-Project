using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A waypoint spawner to test the waypoints in 3D space
/// </summary>
public class WaypointManager : MonoBehaviour
{
    [System.Serializable]
    private struct Data
    {
        public int totalWaypoints;
        public GameObject waypointCanvas;
        public GameObject worldWaypoints;
        public GameObject waypointUIPrefab;
        public List<GameObject> waypointPrefab;
    }

    [SerializeField] private Data data;

    private void Awake()
    {
        for (int i = 0; i < data.totalWaypoints; i++)
        {
            if (data.waypointPrefab.Count > 0)
            {
                for (int j = 0; j < data.waypointPrefab.Count; j++)
                {
                    GameObject tempWaypoint = Instantiate(data.waypointPrefab[j]);
                    WaypointController tempWpController = tempWaypoint.GetComponent<WaypointController>();
                    
                    GameObject tempWaypointUI = Instantiate(data.waypointUIPrefab);
                    //tempWaypointUI.GetComponent<Image>().sprite = t
                    //@TODO: https://www.youtube.com/watch?v=SKqz_TYkQyY (2:49)
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
