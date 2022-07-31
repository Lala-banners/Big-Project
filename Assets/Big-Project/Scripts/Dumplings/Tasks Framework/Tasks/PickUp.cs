using System;
using System.Linq;

using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GatherQuestGoal gather;
    private HideQuestGoal hide;
    public Transform hidingSpot1, hidingSpot2,hidingSpot3, hidingSpot4, hidingSpot5, hidingSpot6;
    public GameObject[] fuseObject;

    private const string
        hidingspot1 = "1",
        hidingspot2 = "2",
        hidingspot3 = "3",
        hidingspot4 = "4",
        hidingspot5 = "5",
        hidingspot6 = "6";

    private void Start()
    {
        gather = FindObjectOfType<GatherQuestGoal>();
        hide = FindObjectOfType<HideQuestGoal>();
    }

    // Collect Fuse
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InWorldItem item))
        {
            item.transform.SetParent(gameObject.transform);
            gather.ItemCollected(0);
        }
        
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
            fuseObject[index].transform.SetParent(parent, false);
            fuseObject[index].transform.localPosition = new Vector3(0, 0, 0);
            fuseObject[index].layer = LayerMask.NameToLayer("ChefInteractable");
            hide.DropItem(0);
        }
    }
}
