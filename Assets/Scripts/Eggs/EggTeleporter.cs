using Player;
using TMPro;
using UnityEngine;

namespace Eggs
{
    public class EggTeleporter : MonoBehaviour
    {
        public TextMeshProUGUI messageText;
        private bool isCollected = false;

        private void OnTriggerEnter(Collider other)
        {
            // Comprueba si el objeto que entró en el trigger es el jugador y el huevo no ha sido recolectado
            if (other.gameObject.CompareTag("Player") && !isCollected)
            {
                // Si es el jugador, muestra el mensaje
                messageText.text = "You are close to an egg. Press F to collect it.";
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Comprueba si el objeto que salió del trigger es el jugador
            if (other.gameObject.CompareTag("Player"))
            {
                // Si es el jugador, oculta el mensaje
                messageText.text = "";
            }
        }

        public void Teleport()
        {
            
            // Cuando se recoja el huevo, se marca como recolectado.
            isCollected = true;
            // Oculta el mensaje si estaba siendo mostrado.
            messageText.text = "";
            gameObject.tag = "CollectedEgg";

            
            Basket basket = GameObject.FindGameObjectWithTag("Basket").GetComponent<Basket>();
            if (!basket.IsFull) // Comprueba si la canasta está llena
            {
                transform.SetParent(basket.transform);
                transform.localPosition = basket.GetNextEggPosition();
                basket.AddEgg(this.gameObject); // Añade el huevo a la canasta
            }

        }
    }
}
