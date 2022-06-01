using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This is the data that is added on the waypoint target GO.
/// The target GO is the GO that needs a waypoint and data to use it.
/// </summary>
[System.Serializable]
public struct DestinationInfo
{
    public Sprite icon;
    public float heightOffset;

    [HideInInspector] public Image image;
    [HideInInspector] public TMP_Text message;
    [HideInInspector] public GameObject effect;
    [HideInInspector] public GameObject waypointUI;
    [HideInInspector] public GameObject target;
    [HideInInspector] public Transform transform;
}
