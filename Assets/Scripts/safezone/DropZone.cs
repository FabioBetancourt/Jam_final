using Player;
using UnityEngine;
using Score = Player.Score;

namespace safezone
{
    public class DropZone : MonoBehaviour
    {
        // Puntos que se suman por cada huevo entregado.
        public int pointsPerEgg = 100;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Obtener la canasta del jugador.
                Basket basket = other.gameObject.GetComponentInChildren<Basket>();
                if (basket != null)
                {
                    // Obtener la puntuación del jugador.
                    Score score = other.gameObject.GetComponent<Score>();
                    if (score != null)
                    {
                        // Añadir puntos por cada huevo en la canasta.
                        score.AddPoints(basket.EggCount * pointsPerEgg);

                        // Borrar los huevos de la canasta.
                        basket.Clear();
                    }
                }
            }
        }
    }
}