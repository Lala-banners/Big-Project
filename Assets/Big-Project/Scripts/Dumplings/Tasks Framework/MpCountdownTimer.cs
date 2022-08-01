using System;

using TMPro;
using UnityEngine;

public class MpCountdownTimer : MonoBehaviour
{
	[SerializeField] private float timeRemaining = 180;
	public static bool timerIsRunning = false;
	public TMP_Text timeText;

	private void Start()
	{
		//Start Timer automatically
		timerIsRunning = true;
	}

	void Update()
	{
		if(timerIsRunning)
		{
			if(timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
				DisplayTime(timeRemaining);
			}
			else
			{
				timeRemaining = 0;
				timerIsRunning = false;
			}
		}
	}

	void DisplayTime(float timeToDisplay)
	{
		timeToDisplay += 1;
		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	
}