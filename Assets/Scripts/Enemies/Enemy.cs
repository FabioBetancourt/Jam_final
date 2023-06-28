using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float initialHealth = 100f;
        private NavMeshAgent _agent;
        private bool _isImmune;
        public float Health { get; private set; }

        private void Awake()
        {
            Health = initialHealth;
            _agent = GetComponent<NavMeshAgent>();
            _agent.enabled = true;
        }

        public void TakeDamage(float amount)
        {
            if (_isImmune) // Comprueba si el enemigo es inmune
            {
                return; // Si es inmune, no le hagas daño
            }
            Health -= amount;
            print(Health);
            if (Health <= 0)
            {
                Die();
                Health = initialHealth;
            }
        }

        private void Die()
        {
            _isImmune = true;
            // Desactiva el NavMeshAgent inmediatamente.
            _agent.enabled = false;
            // Llama al método StopForSeconds después de 10 segundos.
            Invoke(nameof(StopForSeconds), 10f);
        }

        private void StopForSeconds()
        {
            _isImmune = false;
            // Re-activa el NavMeshAgent después de que el método es llamado.
            _agent.enabled = true;
        }
    }
}