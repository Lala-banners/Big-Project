using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Valve.VR.InteractionSystem;

public class VersionManager : NetworkBehaviour
{
    [SerializeField] private GameObject pcUI;
    [SerializeField] private GameObject vrUI;

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_ANDROID
    pcUI.SetActive(true);
    vrUI.SetActive(false);
#else
        pcUI.SetActive(false);
        vrUI.SetActive(true);
#endif

    }

    // Update is called once per frame
    void Update()
    {

    }
}
