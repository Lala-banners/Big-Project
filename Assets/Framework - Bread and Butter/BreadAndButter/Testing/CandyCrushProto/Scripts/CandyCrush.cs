using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BreadAndButter.Mobile
{
    public class CandyCrush : MonoBehaviour
    {
        #region UI
        public TMP_Text scoreText;
        [SerializeField] private int score;
        public TMP_Text movesText;
        [SerializeField] private int moves;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            scoreText.text = "Score: " + score.ToString();
            movesText.text = "0" + moves.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}