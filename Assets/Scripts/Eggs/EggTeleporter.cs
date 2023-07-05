using Player;
using TMPro;
using UnityEngine;

namespace Eggs
{
    public class EggTeleporter : MonoBehaviour
    {
        public TextMeshProUGUI messageText;
        private bool isCollected = false;
        public AudioSource approachSound;
        public AudioSource collectSound;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !isCollected)
            {
                messageText.text = "You are close to an egg. Press F to collect it.";
                if (!approachSound.isPlaying)
                {
                    approachSound.Play();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                messageText.text = "";
                
                if (approachSound.isPlaying)
                {
                    approachSound.Stop();
                }
            }
        }

        public void Teleport()
        {
            isCollected = true;
            messageText.text = "";
            gameObject.tag = "CollectedEgg";

            if (approachSound.isPlaying)
            {
                approachSound.Stop();
            }

            collectSound.Play();

            
            Basket basket = GameObject.FindGameObjectWithTag("Basket").GetComponent<Basket>();
            if (!basket.IsFull)
            {
                transform.SetParent(basket.transform);
                transform.localPosition = basket.GetNextEggPosition();
                basket.AddEgg(this.gameObject); 
            }

        }
    }
}
