using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Quest curQuest;
    private GameObject player; //Reference to Dumpling
    
    [SerializeField] private Transform taskCanvasTransform;
    [SerializeField] private GameObject questUIPrefab;
    [SerializeField] private TMP_Text titleText, descriptionText, experienceText, goldText;

    public AudioSource chefWinsAudio;
    
    public void InitiateDumplingTasks()
    {
        //Find the dumpling players
        player = GameObject.Find("Dumpling");
        
        titleText.text = curQuest.title;
        descriptionText.text = curQuest.description;

        //Go through all tasks to see if any have been completed and update UI if necessary
        foreach(var goal in curQuest.goals)
        {
            GameObject goalObject = Instantiate(questUIPrefab, taskCanvasTransform);
			
            goalObject.transform.Find("Text").GetComponentInChildren<TMP_Text>().text = curQuest.description;
			
            GameObject countObject = goalObject.transform.Find("Count").gameObject;

            //If quests are completed and timer is still running = Dumplings win!
            //If above is true OR madness slider is 0 = dumplings also win!
            if(goal.isReached && MpCountdownTimer.timerIsRunning.Equals(true) || SliderController.Singleton.slider.value <= 0)
            {
                countObject.SetActive(false);
                goalObject.transform.Find("Done").gameObject.SetActive(true);
                Debug.Log("Quest Complete!");
                
                MpCountdownTimer.timerIsRunning = false; //Turn timer off
                WinLoseManager.Singleton.DumplingsWin();
                
                CloseWindow(); //Close quest window
            }
            else //Dumplings do not win! Chef wins if the reverse of above if statement is true
            {
                SliderController.Singleton.CheckMadnessBar();
                
                WinLoseManager.Singleton.ChefWins();
                
                chefWinsAudio.Play();
                
                countObject.GetComponentInChildren<TMP_Text>().text = goal.currentAmount + "/" + goal.requiredAmount;
            }
        }
        experienceText.text = curQuest.experienceReward.ToString();
        goldText.text = curQuest.goldReward.ToString();
    }

    private void Start()
    {
        //Used to be in start (remember where to put back in case doesn't work for some reason)
        //player = GameObject.Find("Dumpling");
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
