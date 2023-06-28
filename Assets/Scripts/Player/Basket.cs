using UnityEngine;

namespace Player
{
    public class Basket : MonoBehaviour
    {
        public float stackHeight = 0.2f;  // La altura en la que se apilan los huevos.
        private int eggCount = 0;

        public Vector3 GetNextEggPosition()
        {
            Vector3 basePosition = new Vector3(0, 0.5f, -1);  // Cambia esto a la posición base para los huevos en la canasta.
            return basePosition + Vector3.up * (stackHeight * eggCount);
        }

        public void AddEgg()
        {
            eggCount++;
        }
    }

}