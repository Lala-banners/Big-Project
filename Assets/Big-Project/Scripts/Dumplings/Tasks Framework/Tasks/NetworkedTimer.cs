using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//using Photon.Pun;

using System;

using TMPro;

using UnityEngine.UI;
//using ExitGames.Client.Photon;

public class NetworkedTimer : MonoBehaviour
{
	bool startTimer = false;
	public double timerIncrementValue;
	public double decTimer;
	double startTime;
	[SerializeField] private double timeLeft = 20;
	//ExitGames.Client.Photon.Hashtable CustomValue;
	public TMP_Text timerText;

	void Start()
	{
		/*if (PhotonNetwork.NetworkingClient.IsConnected)
		{
			timerText = GetComponent<TMP_Text>();
			CustomValue = new ExitGames.Client.Photon.Hashtable();
			startTime = PhotonNetwork.Time;
			startTimer = true;
			CustomValue.Add("StartTime", startTime);
			PhotonNetwork.CurrentRoom.SetCustomProperties(CustomValue);
		}
		else
		{
			startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
			startTimer = true;
		}*/
	}
 
	void Update()
	{
		/*UpdateTimerText();
		
		if (!startTimer) return;
 
		timerIncrementValue = PhotonNetwork.Time - startTime;
		
		// Example for a decreasing timer
		//double roundTime = 300.0;
		//decTimer = roundTime - timerIncrementValue;

		if (timeLeft >= 0)
		{
			//Timer Completed
			//Do What Ever You What to Do Here
			startTimer = false;
			//print("Game Over");
			UpdateTimerText();
			
		}*/
	}

	public void UpdateTimerText()
	{
		timerText.text = "Time Remaining: " + timeLeft;
	}
}