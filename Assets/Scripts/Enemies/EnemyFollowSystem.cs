using UnityEngine;
using UnityEngine.AI;   
namespace Enemies
{
    public class EnemyFollowSystem : MonoBehaviour
    {
        public NavMeshAgent enemy;
        public Transform player;
        
        void Update()
        {
            enemy.SetDestination(player.position);
        }
    }
}
