using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class HideQuestGoal : QuestGoal
{
    //Requirements for Hide goal type
    public string itemName;
    private QuestManager questManager;
    private SliderController chefMadnessSlider;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        chefMadnessSlider = FindObjectOfType<SliderController>();
    }
    
    public void DropItem(int id)
    {
        if (goalType == GoalType.Hide && id == itemId && item.Name == itemName)
        {
            requiredAmount = 0;
            currentAmount--;
            
            if (currentAmount <= 0)
            {
                isReached = true;
                questState = QuestState.Completed;
                gameObject.SetActive(false);
                questManager.dumplingsWinCanvas.gameObject.SetActive(true);
                UiManager.gameIsPaused = true;
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
            print("You have hidden all the fuses");
            chefMadnessSlider.UpdateProgress();
            return true;
        }

        return false;
    }
}
