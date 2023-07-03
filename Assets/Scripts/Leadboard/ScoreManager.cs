using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Leadboard
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputName;
        public UnityEvent<string, int> submitScoreEvent;
        public UnityEvent scoreSubmittedEvent;
        public Score playerScore;


        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }
        private IEnumerator LoadDefeatSceneAfterDelay(float delay)
        {
            yield return StartCoroutine(WaitForRealSeconds(delay));
            SceneManager.LoadScene("Defeat");
        }

        public void SubmitScore()
        {
            int score = playerScore.score;
            Debug.Log("Submitting score: " + score);
            submitScoreEvent.Invoke(inputName.text, score);
            scoreSubmittedEvent.Invoke();
            StartCoroutine(LoadDefeatSceneAfterDelay(5f)); 
        }
    }
}