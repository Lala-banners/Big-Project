using System;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Task curTask;
    public GameObject questWindow;
    public TMP_Text titleText, descriptionText, experienceText, goldText;

    private void Awake()
    {
        //curTask = GameObject.Find("Quest Goals").GetComponent<Task>();
        questWindow = GameObject.Find("PlayerHUD");
        OpenQuestWindow();
    }

    private void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = curTask.title;
        descriptionText.text = curTask.description;
        experienceText.text = "Exp Reward: " + curTask.experienceReward;
        goldText.text = "Gold Reward: " + curTask.goldReward;
    }
}
