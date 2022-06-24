using UnityEngine;
using Valve.VR.InteractionSystem;

public class SimpleGrab : MonoBehaviour
{
    private Interactable interactable;
    
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }

    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }
    
    private void OnHandHoverUpdate(Hand hand)
    {
        GrabTypes grabTypes = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        //Grab the object
        if(interactable.attachedToHand == null && grabTypes != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabTypes);
            hand.HoverLock(interactable);
            hand.HideGrabHint();
        }
        //Release the object
        else if(isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
    }
}
