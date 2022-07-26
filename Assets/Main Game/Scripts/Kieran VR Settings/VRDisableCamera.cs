using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRDisableCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($" XRSettings.gameViewRenderMode = {XRSettings.gameViewRenderMode}");
        XRSettings.gameViewRenderMode = GameViewRenderMode.BothEyes;
        Debug.Log($" XRSettings.gameViewRenderMode = {XRSettings.gameViewRenderMode}");
    }
}
