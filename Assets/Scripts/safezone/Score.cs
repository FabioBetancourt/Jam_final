using TMPro;
using UnityEngine;

namespace Player
{
    public class Score : MonoBehaviour
    {
        // Texto que muestra la puntuación en la UI.
        public TextMeshProUGUI scoreText;
        public int score;

        public void AddPoints(int points)
        {
            score += points;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = $"Score: {score}";
        }
    }
}