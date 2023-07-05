using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class DragonCharacterAnimator : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent agent;

        [SerializeField]
        private Animator animator;

        private void Update()
        {
            float speed = agent.velocity.magnitude / agent.speed;

            animator.SetFloat("VelocitiX_drake", agent.velocity.normalized.x * speed);
            animator.SetFloat("VelocitiY_drake", agent.velocity.normalized.z * speed); 
        }

    }
}
