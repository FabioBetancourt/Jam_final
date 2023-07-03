using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Leadboard
{
    public class LeaderBoard1 : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> names;
        [SerializeField] private List<TextMeshProUGUI> scores;
        public string leaderboardKey = "10eac5528ab7d37c95a405a07c42d2f8d266e99874c502d1c26558d10c9e1a09";
        public string[] badWords = new []{"Ass","ass"};
        public ScoreManager scoreManager;
        public float delayInSeconds = 5f;

        private void Start()
        {
            GetLeaderBoard();
            scoreManager.scoreSubmittedEvent.AddListener(() => StartCoroutine(DelayedGetLeaderBoard()));
        }

        private IEnumerator DelayedGetLeaderBoard()
        {
            yield return new WaitForSeconds(delayInSeconds);
            GetLeaderBoard();
            yield return new WaitForSeconds(delayInSeconds);
            SceneManager.LoadScene("Defeat");
        }

        public void GetLeaderBoard()
        {
            LeaderboardCreator.GetLeaderboard(publicKey: leaderboardKey, ((msg) =>
            {
                int loopLenght = (msg.Length < names.Count) ?  msg.Length : names.Count;
                for (int i = 0; i < loopLenght; i++)
                {
                    names[i].text = msg[i].Username;
                    scores[i].text = msg[i].Score.ToString();
                }
            }));
        }

        public void SetLeaderBoardEntry(string username, int score)
        {
            LeaderboardCreator.UploadNewEntry(publicKey: leaderboardKey, username: username, score: score, ((msg) =>
            {
                if (System.Array.IndexOf(badWords, name) != -1) return;
                GetLeaderBoard();
            }));
        }
    }
}