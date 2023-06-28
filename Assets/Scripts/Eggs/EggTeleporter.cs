using Player;
using UnityEngine;

namespace Eggs
{
    public class EggTeleporter : MonoBehaviour
    {
        [Tooltip("Distance the teleport above the player")]
        public float teleportUpDistance = 1f;
        [Tooltip("Distance the teleport back the player")]
        public float teleportBackDistance = 1.2f;

        public void Teleport()
        {
            Basket basket = GameObject.FindGameObjectWithTag("Basket").GetComponent<Basket>();
            transform.SetParent(basket.transform);
            transform.localPosition = basket.GetNextEggPosition();
            basket.AddEgg();
        }

    }

}
