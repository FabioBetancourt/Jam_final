using UnityEngine;

namespace Player
{
    public class TeleportTrigger : MonoBehaviour
    {
        public float teleportHeight = 5f;
        public CharacterController playerController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerController.enabled = false;
                Vector3 teleportPosition = new Vector3(playerController.transform.position.x, teleportHeight, playerController.transform.position.z);
                playerController.transform.position = teleportPosition;
                playerController.enabled = true;
                Debug.Log("The player has been teleported to " + playerController.transform.position);
            }
        }
    }
}