using UnityEngine;

namespace Enemies
{
    public class Tail : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;

        private void OnTriggerEnter(Collider other)
        {
            Player.PlayerController player = other.GetComponent<Player.PlayerController>();
            if (player != null)
            {
                // If it is the player, deal damage
                player.TakeDamage(damage);
            }
        }
    }
}
