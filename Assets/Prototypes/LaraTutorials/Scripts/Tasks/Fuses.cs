using System.Collections.Generic;
using UnityEngine;

public class Fuses : MonoBehaviour
{
    [Header("Power Fuses")]
    public GameObject fuseboxParent;
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private List<Transform> hidingSpots = new List<Transform>();
    public List<GameObject> fuseObject;
    
    [Header("Player Stuff")]
    public Transform newParent;
    public int index; //ensure that only one fuse at a time can be added to a hiding place

    private void Awake()
    {
        index = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetUp();
    }

    /// <summary>
    /// Set up the fuses as parented to the fuse box and prepared for interaction with dumpling player
    /// </summary>
    public void SetUp()
    {
        /*rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;*/
    }

    /// <summary>
    /// Fake Inventory to attach to player
    /// </summary>
    public void AddToPlayer()
    {
        index++;
        fuseObject[index].transform.SetParent(newParent);
        gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Remove fuses from player
    /// </summary>
    public void RemoveFromPlayer()
    {
        index--;
        fuseObject[index].transform.SetParent(null);
        gameObject.SetActive(true);
    }
}
