using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Quest currentQuest;
    //public Player player;
    public GameObject questWindow;
    public TMP_Text titleText, descriptionText, experienceText, goldText;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = currentQuest.title;
        descriptionText.text = currentQuest.description;
        experienceText.text = "Exp Reward: " + currentQuest.experienceReward.ToString();
        goldText.text = "Gold Reward: " + currentQuest.goldReward.ToString();
    }
}
