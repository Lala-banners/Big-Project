using UnityEngine;

[System.Serializable]
public class GatherQuestGoal : QuestGoal
{
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