using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GatherQuestGoal gatherGoal;
    public Transform hidingSpot1, hidingSpot2,hidingSpot3, hidingSpot4;
    public GameObject[] fuseObject;

    private const string hidingspot1 = "Hiding Spot 1",
        hidingspot2 = "Hiding Spot 2",
        hidingspot3 = "Hiding Spot 3",
        hidingspot4 = "Hiding Spot 4";

    private void Start()
    {
        gatherGoal = FindObjectOfType<GatherQuestGoal>();
    }

    public void Interact()
    {
        Ray ray = new Ray(transform.position + transform.forward * 0.5f, transform.forward); //Creates line from player to infinity 
        RaycastHit hitInfo; //Get back info about what we hit
        
        int layerMask = LayerMask.NameToLayer("Interactable");
        
        layerMask = 1 << layerMask;

        //If Ray hits something
        if (Physics.Raycast(ray, out hitInfo, 10f, layerMask))
        {
            if (hitInfo.collider.TryGetComponent(out InWorldItem item))
            {
                //Debug.Log(item.name);
            }
        }
    }

    // Collect Fuse
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InWorldItem item))
        {
            item.transform.SetParent(gameObject.transform);
            gatherGoal.ItemCollected(0);
        }
        
        DropFuse(other, 0, hidingSpot1, hidingspot1);
        DropFuse(other, 1, hidingSpot2, hidingspot2);
        DropFuse(other, 2, hidingSpot3, hidingspot3);
        DropFuse(other, 3, hidingSpot4, hidingspot4);
    }

    private void DropFuse(Collider other, int index, Transform parent, string name)
    {
        if (other.gameObject.name == name)
        {
            fuseObject[index].transform.SetParent(parent, false);
            fuseObject[index].transform.localPosition = new Vector3(0, 0, 0);
            fuseObject[index].layer = LayerMask.NameToLayer("ChefInteractable");
            //gatherGoal.DropItem(0);
        }
    }
}
