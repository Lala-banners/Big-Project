using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HideQuest : QuestGoal
{
    public void DropItem(int id)
    {
        if (goalType == GoalType.Hide && id == itemId)
        {
            currentAmount--;
            requiredAmount = 0;
            if (currentAmount <= 0)
            {
                isReached = true;
                questState = QuestState.Completed;
                Debug.Log("Drop item completed");
            }
        }
    }
    
    public override bool isCompleted()
    {
        if (item == null)
        {
            return false;
        }

        currentAmount = item.Amount;
        if (item.Amount <= requiredAmount) //if amount is less than or equal to required amount then quest is completed
        {
            DropItem(itemId);
            print("You have hidden all fuses");
            return true;
        }

        return false;
    }
}
