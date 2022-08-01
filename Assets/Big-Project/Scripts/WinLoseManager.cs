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

	[Header("VR Chef")]
	public Slider chefMadnessBar;

	[Header("Dumplings")]
	public Canvas dumplingsWinCanvas;
	public Canvas dumplingsLoseCanvas;

	public void LinkUI()
	{
		chefMadnessBar = GameObject.Find("Chef Madness Bar").GetComponent<Slider>();
		dumplingsWinCanvas = GameObject.Find("Dumplings Win Canvas").GetComponent<Canvas>();
		dumplingsLoseCanvas = GameObject.Find("Dumplings Lose Canvas").GetComponent<Canvas>();
	}

	/// <summary>
    /// Win screen for dumplings show up
    /// Only win if dumplings complete all tasks - lower chef madness bar to 0
    /// </summary>
    public void DumplingsWin()
    {
	    dumplingsWinCanvas.gameObject.SetActive(true);
    }
	
    /// <summary>
    /// Play audio for VR player
    /// Game over screens for dumplings show up
    /// </summary>
    public void ChefWins()
    {
		dumplingsLoseCanvas.gameObject.SetActive(true);
    }
}
