using System;

using UnityEngine;

[System.Serializable]
public class GatherQuestGoal : QuestGoal
{
    //Requirements for Gather goal type
    public string itemName;
    private QuestManager questManager;
    private SliderController chefMadnessSlider;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        chefMadnessSlider = FindObjectOfType<SliderController>();
    }

    public void ItemCollected(int id)
    {
        if (goalType == GoalType.Gather && id == itemId && item.Name == itemName)
        {
            currentAmount++;
            
            if(currentAmount >= requiredAmount)
            {
                isReached = true;
                questState = QuestState.Completed;
                gameObject.SetActive(false);
                Debug.Log("Gather Quest Complete!");
            }
        }
    }

    //Each quest will have different way of being completed
    public override bool isCompleted()
    {
        if (item == null)
        {
            return false;
        }

        currentAmount = item.Amount;
        
        if (item.Amount >= requiredAmount) //if amount is greater or equal to required amount then quest is completed
        {
            ItemCollected(itemId);
            print("You have gathered all the fuses!");
            chefMadnessSlider.RemoveProgress(); //Hit Chef Madness Bar
            return true;
        }

        return false;
    }
}
