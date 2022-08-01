using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
	#region Singleton
	protected static SliderController singleton;
	public static SliderController Singleton
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
				Debug.Log($"{nameof(SliderController)} instance already exists, destroying duplicate!");
				Destroy(value);
			}            
		}
	}
	#endregion
	
	//public TMP_Text sliderValueText;
	private int progress = 0;
	public Slider slider;
	
	public void OnSliderChanged(float value)
	{
		slider.value = value;
	}

	public void UpdateProgress()
	{
		progress++;
		slider.value = progress;
	}
	
	public void RemoveProgress()
	{
		progress--;
		slider.value = progress;
	}
	
	public void CheckMadnessBar()
	{
		if(slider.value >= slider.maxValue)
		{
			slider.value = slider.maxValue;
		}
	}
}
