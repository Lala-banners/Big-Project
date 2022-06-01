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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InWorldItem worldItem))
        {
            if (worldItem != null)
            {
                //Update player HUD
                worldItem.transform.SetParent(transform);
                worldItem.gameObject.SetActive(false);
                countIndex++;
                /*powerIcon.color = Color.blue;
                countText.text = "x" + countIndex;
                print("Collectable has been picked up");*/

                //Update Goal
                goal.ItemCollected(0);
            }
        }

        if (other.gameObject.name == "Hiding Spot (1)")
        {
            fuses[0].gameObject.transform.SetParent(hidingSpots[0]);
            fuses[0].gameObject.transform.position = hidingSpots[0].transform.position;
            fuses[0].gameObject.SetActive(true);
        }

        if (other.gameObject.name == "Hiding Spot (2)")
        {
            fuses[1].gameObject.transform.SetParent(hidingSpots[1]);
            fuses[1].gameObject.transform.position = hidingSpots[1].transform.position;
            fuses[1].gameObject.SetActive(true);
        }

        if (other.gameObject.name == "Hiding Spot (3)")
        {
            fuses[2].gameObject.transform.SetParent(hidingSpots[2]);
            fuses[2].gameObject.transform.position = hidingSpots[2].transform.position;
            fuses[2].gameObject.SetActive(true);
        }

        if (other.gameObject.name == "Hiding Spot (4)")
        {
            fuses[3].gameObject.transform.SetParent(hidingSpots[3]);
            fuses[3].gameObject.transform.position = hidingSpots[3].transform.position;
            fuses[3].gameObject.SetActive(true);
        }
    }
}