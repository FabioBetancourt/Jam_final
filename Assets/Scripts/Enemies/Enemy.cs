using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float initialHealth = 100f;
        private NavMeshAgent _agent;
        private bool _isImmune;
        public float Health { get; private set; }
        private bool _isPlayerInTrigger = false;
        private bool _isSpinning = false;
        private bool _isResting = false;

        private void Awake()
        {
            Health = initialHealth;
            _agent = GetComponent<NavMeshAgent>();
            _agent.enabled = true;
        }

        public void TakeDamage(float amount)
        {
            if (_isImmune)
            {
                return;
            }
            Health -= amount;
            Debug.Log(Health);
            if (Health <= 0)
            {
                Die();
                Health = initialHealth;
            }
        }

        private void Die()
        {
            _isImmune = true;
            _agent.enabled = false;
            Invoke(nameof(StopForSeconds), 10f);
        }

        private void StopForSeconds()
        {
            _isImmune = false;
            _agent.enabled = true;
        }

        public void PlayerInTrigger(bool isInTrigger)
        {
            _isPlayerInTrigger = isInTrigger;
            if(_isPlayerInTrigger && !_isSpinning && !_isResting)
            {
                StartCoroutine(SpinAndRest());
            }
        }

        private IEnumerator SpinAndRest()
        {
            _isSpinning = true;
            float spinStartTime = Time.time;
            while (Time.time - spinStartTime < 10f)
            {
                transform.Rotate(Vector3.up, 360 * Time.deltaTime);
                yield return null;
            }
            _isSpinning = false;

            _isResting = true;
            yield return new WaitForSeconds(5f);
            _isResting = false;

            if (_isPlayerInTrigger)
            {
                StartCoroutine(SpinAndRest());
            }
        }
    }
}
