using TMPro;
using UnityEngine;

namespace Player
{
    public class Score : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI scoreText2;
        public int score;
        
        

        public void AddPoints(int points)
        {
            score += points;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText2.text = $"Score: {score}";
            scoreText.text = $"Score: {score}";
        }
    }
}