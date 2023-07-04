using Player;
using UnityEngine;
using Score = Player.Score;

namespace safezone
{
    public class DropZoneGold : MonoBehaviour
    {
        public int pointsPerEgg = 500;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Basket basket = other.gameObject.GetComponentInChildren<Basket>();
                if (basket != null)
                {
                    Score score = other.gameObject.GetComponent<Score>();
                    if (score != null)
                    {
                        score.AddPoints(basket.EggCount * pointsPerEgg);
                        basket.Clear();
                    }
                }
            }
        }
    }
}