using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DisableView : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        XRSettings.gameViewRenderMode = GameViewRenderMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
