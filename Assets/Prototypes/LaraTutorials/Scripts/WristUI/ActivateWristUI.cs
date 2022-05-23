using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActivateWristUI : MonoBehaviour
{
    public GameObject wristUI;
    public Transform wristAttach;

    private void Update()
    {
        wristUI.transform.SetParent(wristAttach, false);
    }
}
