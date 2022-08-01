using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseManager : MonoBehaviour
{
	#region Singleton
	protected static WinLoseManager singleton;
	public static WinLoseManager Singleton
	{
		get => singleton;
		protected set
		{
			if(singleton == null)
			{
				singleton = value;
			}
    
			else if(singleton != value)
			{
				Debug.Log($"{nameof(WinLoseManager)} instance already exists, destroying duplicate!");
				Destroy(value);
			}            
		}
	}
	#endregion

	public bool dumplingsWinners = false;
	public bool vRWinner = false;

	private void Start()
	{
		if(dumplingsWinners)
		{
			
		}

		if(vRWinner)
		{
			
		}
	}
}
