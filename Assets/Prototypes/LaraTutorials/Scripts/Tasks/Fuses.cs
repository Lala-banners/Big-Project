using System;
using System.Collections.Generic;
using UnityEngine;

public class Fuses : MonoBehaviour
{
    [Header("Power Fuses")]
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private List<Transform> hidingSpots = new List<Transform>();
    public GameObject fuseObject;

    [Header("Player Stuff")] 
    public GameObject player;
    public int index; //ensure that only one fuse at a time can be added to a hiding place

    private void Awake()
    {
        index = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetUp();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Fake Inventory to attach to player
    /// </summary>
    public void AddToPlayer(Transform newParent)
    {
        index++;
        gameObject.transform.SetParent(newParent.transform, true);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cube")
        {
            AddToPlayer(player.transform);
        }
    }
}
