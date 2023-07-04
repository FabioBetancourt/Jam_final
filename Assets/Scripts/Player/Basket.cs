using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Player
{
    public class Basket : MonoBehaviour
    {
        public float stackHeight = 0.2f;  // La altura en la que se apilan los huevos.
        private List<GameObject> collectedEggs = new();  // Lista para mantener un registro de los huevos recogidos.
        public TextMeshProUGUI messageText;

        public bool IsFull => collectedEggs.Count == 1;
        public int EggCount => collectedEggs.Count;

        public Vector3 GetNextEggPosition()
        {
            Vector3 basePosition = new Vector3(0, 0.5f, -1);  // Cambia esto a la posición base para los huevos en la canasta.
            return basePosition + Vector3.up * (stackHeight * collectedEggs.Count);
        }

        public void AddEgg(GameObject egg)
        {
            if (IsFull)
            {
                messageText.text = "The basket is full!, go to the safe zone in the start point"; 
                return;
            }

            collectedEggs.Add(egg);
            if (IsFull)
            {
                messageText.text = "The basket is full!, go to the safe zone in the start point";  // Muestra el mensaje de canasta llena si la canasta se ha llenado con este huevo.
            }
        }

        public void Clear()
        {
            foreach (GameObject egg in collectedEggs)
            {
                Destroy(egg);  // Destruye cada huevo en la canasta.
            }
            collectedEggs.Clear();  // Limpia la lista de huevos recogidos.
            messageText.text = "";
        }
        
        public List<GameObject> GetCollectedEggs()
        {
            return new List<GameObject>(collectedEggs);
        }

    }
}