using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectiveMarker : MonoBehaviour
{
    [Header("Objective Marker")] 
    [SerializeField] private Camera mainCam;
    public TMP_Text distanceText;
    [SerializeField] private Transform player;
    [SerializeField] private Image img;

    private void Start()
    {
        player = GameObject.Find("Cube").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = mainCam.WorldToScreenPoint(transform.position);

        distanceText.text = ((int)Vector3.Distance(player.position, transform.position)) + "m";
        
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        if (Vector3.Dot((player.position - transform.position), transform.forward) < 0)
        {
            //Target is behind player
            pos.x = pos.x < Screen.width / 2 ? maxX : minX;
        }
        
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
    }
}
