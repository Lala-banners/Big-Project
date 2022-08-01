using System;
using System.Collections.Generic;

using UnityEngine;

public class FinalIngredientsLocation : MonoBehaviour
{
	//public ScoreManager scoreManager;
	public GameObject tomato, egg, cucumber, onion, shallot, carrot;
	[SerializeField] private Transform potLocation;
	private SliderController chefMadnessSlider;

	private void Start()
	{
		potLocation.position = transform.position;
		chefMadnessSlider = FindObjectOfType<SliderController>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject == tomato)
		{
			//scoreManager.AddPoint(1);
			chefMadnessSlider.UpdateProgress();
			//scoreManager.UpdateUI();
			Destroy(tomato);
		}

		if(other.gameObject == egg)
		{
			//scoreManager.AddPoint(1);
			chefMadnessSlider.UpdateProgress();
			//scoreManager.UpdateUI();
			Destroy(egg);
		}

		if(other.gameObject == cucumber)
		{
			//scoreManager.AddPoint(1);
			chefMadnessSlider.UpdateProgress();
			//scoreManager.UpdateUI();
			Destroy(cucumber);
		}

		if(other.gameObject == onion)
		{
			//scoreManager.AddPoint(1);
			chefMadnessSlider.UpdateProgress();
			//scoreManager.UpdateUI();
			Destroy(onion);
		}

		if(other.gameObject == shallot)
		{
			//scoreManager.AddPoint(1);
			chefMadnessSlider.UpdateProgress();
			//scoreManager.UpdateUI();
			Destroy(shallot);
		}

		if(other.gameObject == carrot)
		{
			//scoreManager.AddPoint(1);
			chefMadnessSlider.UpdateProgress();
			//scoreManager.UpdateUI();
			Destroy(carrot);
		}
		
		//Supposed to check if the madness bar is still higher than 0
		chefMadnessSlider.CheckMadnessBar();
	}
}