using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GatherQuestGoal gather;
    private HideQuestGoal hide;
    public Transform hidingSpot1, hidingSpot2,hidingSpot3, hidingSpot4, hidingSpot5, hidingSpot6;
    [SerializeField] private List<GameObject> fuseObject;

    private const string
        hidingspot1 = "1",
        hidingspot2 = "2",
        hidingspot3 = "3",
        hidingspot4 = "4",
        hidingspot5 = "5",
        hidingspot6 = "6";
    
    private const string 
        fuse1 = "Fuse1",
        fuse2 = "Fuse2",
        fuse3 = "Fuse3",
        fuse4 = "Fuse4",
        fuse5 = "Fuse5",
        fuse6 = "Fuse6";

    private void Start()
    {
        gather = GameObject.Find("Gather Quest").GetComponent<GatherQuestGoal>();
        hide = GameObject.Find("Hide Quest").GetComponent<HideQuestGoal>();
        
        hidingSpot1 = GameObject.Find(hidingspot1).transform;
        hidingSpot2 = GameObject.Find(hidingspot2).transform;
        hidingSpot3 = GameObject.Find(hidingspot3).transform;
        hidingSpot4 = GameObject.Find(hidingspot4).transform;
        hidingSpot5 = GameObject.Find(hidingspot5).transform;
        hidingSpot6 = GameObject.Find(hidingspot6).transform;

        fuseObject = GameObject.FindGameObjectsWithTag("Fuse").ToList();
    }

    // Collect Fuse
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InWorldItem item))
        {
            item.transform.SetParent(gameObject.transform);
            gather.ItemCollected(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DropFuse(other, 0, hidingSpot1, hidingspot1);
        DropFuse(other, 1, hidingSpot2, hidingspot2);
        DropFuse(other, 2, hidingSpot3, hidingspot3);
        DropFuse(other, 3, hidingSpot4, hidingspot4);
        DropFuse(other, 4, hidingSpot5, hidingspot5);
        DropFuse(other, 5, hidingSpot6, hidingspot6);
    }

    private void DropFuse(Collider other, int index, Transform parent, string name)
    {
        if (other.gameObject.name == name)
        {
            fuseObject[index].layer = LayerMask.NameToLayer("ChefInteractable");
            fuseObject[index].gameObject.GetComponent<CapsuleCollider>().enabled = false;
            fuseObject[index].transform.SetParent(parent, false);
            fuseObject[index].transform.localPosition = new Vector3(0, 0, 0);
            hide.DropItem(0);
        }
    }
}
