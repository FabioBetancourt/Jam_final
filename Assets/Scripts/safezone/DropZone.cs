using System;
using Eggs;
using Player;
using UnityEngine;
using Score = Player.Score;

namespace safezone
{
    public class DropZone : MonoBehaviour
    {
        public AudioSource pointSound;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Basket basket = other.gameObject.GetComponentInChildren<Basket>();
                if (basket != null)
                {
                    int totalPoints = 0;
                    foreach (var egg in basket.GetCollectedEggs())
                    {
                        Egg eggComponent = egg.GetComponent<Egg>();
                        if (eggComponent != null)
                        {
                            totalPoints += eggComponent.pointValue;
                        }
                    }

                    Score score = other.gameObject.GetComponent<Score>();
                    if (score != null)
                    {
                        score.AddPoints(totalPoints);

                        if (pointSound != null) 
                        {
                            pointSound.Play();
                        }

                        // Borrar los huevos de la canasta.
                        basket.Clear();
                    }
                }
            }
        }
    }
}