using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
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
