using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyFollowSystem : MonoBehaviour
    {
        public NavMeshAgent enemy;
        public Transform player;
        public float followRange = 10f;

        private Vector3 _originalPosition;

        private void Start()
        {
            _originalPosition = enemy.transform.position;
        }

        void Update()
        {

            float distanceToThePlayer = Vector3.Distance(player.position, enemy.transform.position);

            if (distanceToThePlayer <= followRange)
            {
                enemy.SetDestination(player.position);
            }
            else
            {

                enemy.SetDestination(_originalPosition);
            }
        }
    }
}
