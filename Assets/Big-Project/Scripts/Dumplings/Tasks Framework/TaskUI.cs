using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskUI : MonoBehaviour
{
	[Tooltip("The Icon representing each task in the HUD.")] public Image taskItem; 
	[Tooltip("The colour the TaskItem image turns when the task has been completed.")] public Color completedColor;
	[Tooltip("The colour the TaskItem image turns when the task is active.")] public Color activeColor;
	[Tooltip("The colour the TaskItem image turns when the task has started.")] public Color currentColor;
	[Tooltip("A list of all the tasks in the HUD.")] public TaskUI[] allTasks;

	void Start()
	{
		allTasks = FindObjectsOfType<TaskUI>();
		currentColor = taskItem.color;
	}

	/// <summary>
	/// Call this function when any task is completed.
	/// </summary>
	private void FinishTask()
	{
		taskItem.GetComponent<Button>().interactable = false;
		currentColor = completedColor;
		taskItem.color = completedColor;
	}

	/// <summary>
	/// When the Dumplings want to see the status of their tasks.
	/// </summary>
	public void OnTaskClick()
	{
		currentColor = completedColor;

		foreach(TaskUI task in allTasks)
		{
			task.taskItem.color = task.currentColor;
		}

		taskItem.color = activeColor;
	}
}
