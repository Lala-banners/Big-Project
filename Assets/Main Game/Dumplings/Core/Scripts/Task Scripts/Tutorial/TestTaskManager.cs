using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTaskManager : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private Transform taskContents;
    [SerializeField] private GameObject taskHolder;

    public List<TestQuest> currentTasks;
    
    private void Awake()
    {
        foreach (TestQuest task in currentTasks)
        {
            task.Initialise();
            task.taskCompleted.AddListener(OnTaskCompleted);
            GameObject taskGO = Instantiate(taskPrefab, taskContents);
            taskGO.transform.Find("Icon").GetComponent<Image>().sprite = task.info.icon;
            
            taskGO.GetComponent<Button>().onClick.AddListener(delegate
            {
                taskHolder.SetActive(true);
            });
        }
    }

    public void Gather(string itemName)
    {
        EventManager.Instance.QueueEvent(new GatherGameEvent(itemName));
    }

    private void OnTaskCompleted(TestQuest _task)
    {
        taskContents.GetChild(currentTasks.IndexOf(_task)).Find("Checkmark").gameObject.SetActive(true);
    }
}
