using Eggs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class EggCollection : MonoBehaviour
    {
        [Tooltip("Max distance to pick up an egg")]
        public float closestDistance = 1f;
        private PlayerInput playerInput;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if(playerInput.actions["Recolection"].triggered)
            {
                CollectEgg();
            }
        }

        private void CollectEgg()
        {
            GameObject egg = GetClosestEgg(); // Encuentra el "Egg" más cercano.
            if(egg != null)
            {
                egg.GetComponent<EggTeleporter>().Teleport();
            }
        }

        private GameObject GetClosestEgg()
        {
            GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");
            GameObject closestEgg = null;
            float smallestDistance = float.MaxValue;

            foreach (GameObject egg in eggs)
            {
                float distance = Vector3.Distance(transform.position, egg.transform.position);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestEgg = egg;
                }
            }

            return smallestDistance <= closestDistance ? closestEgg : null;
        }
    }


}
