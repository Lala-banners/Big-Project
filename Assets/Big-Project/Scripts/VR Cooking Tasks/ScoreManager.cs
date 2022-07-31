using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static int scoreAmount;
	private TMP_Text scoreText;

	// Start is called before the first frame update
	void Start()
	{
		scoreText = GetComponent<TMP_Text>();
		scoreAmount = 0;
	}

	public void AddPoint(int scoreValue)
	{
		scoreAmount += scoreValue;
	}

	public void LessPoint(int scoreValue)
	{
		scoreAmount -= scoreValue;
	}

	public void CheckScores()
	{
		if(SliderController.Singleton.slider.value >= SliderController.Singleton.slider.maxValue)
		{
			SliderController.Singleton.slider.value = SliderController.Singleton.slider.maxValue;
		}
	}

	public void UpdateUI()
	{
		scoreText.text = "Score: " + scoreAmount;
	}
}