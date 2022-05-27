using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] hoops = new GameObject[4];

    public void SpawnHoop(int i)
    {
        Instantiate(hoops[i], transform.position, transform.rotation);
        Debug.Log($"{hoops[i].gameObject.name} is being spawned");
    }
}
