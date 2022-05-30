using UnityEngine;

[System.Serializable]
public class GatherQuestGoal : QuestGoal
{
    //Requirements for Gather goal type
    public string itemName;

    private void Start()
    {
        /*playerInv = (Inventory)GameObject.FindObjectOfType<Inventory>();

        if (playerInv == null)
        {
            Debug.LogError("There is no player with inventory in the scene");
        }*/
    }

    /*private bool CheckPlayerInv()
    {
        if(playerInv == null)
        {
           return false;
            
        }
        return true;
    }*/

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

    //Each quest will have different way of being completed
    public override bool isCompleted()
    {
        /*if (CheckPlayerInv() == false)
        {
            return false;
        }

        if (item == null)
        {
            return false;
        }

        currentAmount = item.Amount;
        if (item.Amount >= requiredAmount) //if amount is greater or equal to required amount then quest is completed
        {
            ItemCollected(itemId);
            print("Quest has been completed");
            return true;
        }*/

        return false;
    }
}
