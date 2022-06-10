using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    Available,
    Active,
    Completed,
    Claimed
}

public enum GoalType
{
    Gather
}

[System.Serializable]
public abstract class QuestGoal : MonoBehaviour
{
    public QuestState questState;
    public GoalType goalType;
    public int itemId;
    public int requiredAmount;
    public int currentAmount;
    public bool isReached;
    
    [Header("Player")]
    public int requiredLevel;
    public ItemData item;

    public abstract bool isCompleted();

    public void ItemCollected(int id)
    {
        if (goalType == GoalType.Gather && id == itemId)
        {
            currentAmount++;
            
            if (currentAmount >= requiredAmount) //Added from Quest
            {
                isReached = true;
                questState = QuestState.Completed;
                Debug.Log("QUEST COMPLETE");
            }
        }
    }

}
