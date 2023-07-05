using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class BoardCharacterAnimator : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent agent;

        [SerializeField]
        private Animator animator;

        private void Update()
        {
            float speed = agent.velocity.magnitude / agent.speed;

            animator.SetFloat("VelocityX_jabali", agent.velocity.normalized.x * speed);
            animator.SetFloat("VelocityY_jabali", agent.velocity.normalized.z * speed); 
        }

        public void SetAttack(bool isAttacking)
        {
            animator.SetBool("Attack_jabali", isAttacking);
        }
    }
}
