using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Runtime.CompilerServices;

public class PickUp : MonoBehaviour
{
    private QuestGoal goal;
    public Image powerIcon;
    public TMP_Text countText;
    private int countIndex;

    [SerializeField] private List<GameObject> fuses;
    [SerializeField] private List<Transform> hidingSpots;
    private int nextHidingSpot;

    private void Start()
    {
        goal = FindObjectOfType<QuestGoal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InWorldItem worldItem))
        {
            if (worldItem != null)
            {
                //Update player HUD
                worldItem.transform.SetParent(transform);
                worldItem.gameObject.SetActive(false);
                countIndex++;
                powerIcon.color = Color.blue;
                countText.text = "x" + countIndex;
                print("Collectable has been picked up");

                //Update Goal
                goal.ItemCollected(0);
            }
        }

        if (other.gameObject.TryGetComponent(out HidingSpot spot))
        {
            if (spot != null)
            {
                Debug.Log("Collided with hiding spot");
                
                for (int i = 0; i < hidingSpots.Count; i++)
                {
                    Debug.Log("Parented to spot");
                    fuses[0].gameObject.transform.SetParent(hidingSpots[0]);
                    fuses[0].gameObject.SetActive(true);
                }
                
            }
        }
    }
}