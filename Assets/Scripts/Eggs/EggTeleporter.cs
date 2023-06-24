using UnityEngine;

namespace Eggs
{
    public class EggTeleporter : MonoBehaviour
    {
        [Tooltip("Distance the teleport above the player")]
        public float teleportUpDistance = 1f;
        [Tooltip("Distance the teleport back the player")]
        public float teleportBackDistance = 0.8f;

        public void Teleport()
        {
            Rigidbody eggRigidbody = GetComponent<Rigidbody>();
            if(eggRigidbody != null)
            {
                eggRigidbody.isKinematic = true;  // Desactiva las físicas para el teletransporte.
                Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                transform.position = playerTransform.position + Vector3.up * teleportUpDistance - playerTransform.forward * teleportBackDistance;  // Teletransporta el "Egg" 2 unidades arriba de la cabeza del jugador y media unidad detrás.
                eggRigidbody.isKinematic = false;  // Reactiva las físicas para que el "Egg" caiga.
            }
        }
    }

}
