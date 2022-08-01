using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Unity.VisualScripting;

using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest curQuest;
    private GameObject player; //Reference to Dumpling
    [SerializeField] private Transform taskCanvasTransform;
    [SerializeField] private GameObject questUIPrefab;
    public TMP_Text titleText, descriptionText, experienceText, goldText, count;
    public AudioSource chefWinsAudio;
    private SliderController chefMadnessSlider;
    
    public Image dumplingsWinCanvas;
    public Image dumplingsLoseCanvas;

    public GameObject timerUI;

    public void InitiateDumplingTasks()
    {
        //Find the dumpling player prefab
        player = GameObject.Find("Dumpling");

        chefWinsAudio = GameObject.Find("Chef Wins Music").gameObject.GetComponent<AudioSource>();
        questUIPrefab = GameObject.FindGameObjectWithTag("HUD");
        
        dumplingsWinCanvas = questUIPrefab.transform.Find("DumplingsWinCanvas").GetComponent<Image>();
        dumplingsLoseCanvas = questUIPrefab.transform.Find("DumplingsLoseCanvas").GetComponent<Image>();

        chefMadnessSlider = FindObjectOfType<SliderController>();
        titleText = GameObject.Find("TaskName").GetComponent<TMP_Text>();
        descriptionText = GameObject.Find("TaskDescription").GetComponent<TMP_Text>();
        experienceText = GameObject.Find("TaskStatus").GetComponent<TMP_Text>();
        goldText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        count = GameObject.Find("Count").GetComponent<TMP_Text>();
        
        //Go through all tasks to see if any have been completed and update UI
        foreach(var goal in curQuest.goals)
        {
            OpenQuestWindow();

            //If quests are completed and timer is still running = Dumplings win!
            if(goal.isReached && MpCountdownTimer.timerIsRunning)
            {
                Time.timeScale = 0f;
                
                count.text = goal.currentAmount + "/" + goal.requiredAmount;

                dumplingsWinCanvas.gameObject.SetActive(true);

                CloseWindow(); //Close quest window
                
                Destroy(timerUI.gameObject);
                
                Destroy(chefWinsAudio);

                Debug.Log("Dumplings All Tasks Complete!");
            }
            else if(!goal.isReached && !MpCountdownTimer.timerIsRunning)
            {
                //chefMadnessSlider.CheckMadnessBar();
                
                dumplingsLoseCanvas.gameObject.SetActive(true);
                
                chefWinsAudio.Play();
                MpCountdownTimer.timerIsRunning = false; //Turn timer off
                UiManager.gameIsPaused = true;
                
                count.gameObject.SetActive(false);
            }
        }
    }
    
    public void OpenQuestWindow()
    {
        titleText.text = curQuest.title;
        descriptionText.text = curQuest.description;
        experienceText.text = "Exp Reward: " + curQuest.experienceReward;
        goldText.text = "Gold Reward: " + curQuest.goldReward;
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);

        for(int i = 0; i < taskCanvasTransform.childCount; i++)
        {
            Destroy(taskCanvasTransform.GetChild(i).gameObject);
        }
    }
}
