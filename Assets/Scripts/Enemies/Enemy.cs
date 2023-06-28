    using System;
    using Interfaces;
    using UnityEngine;

    namespace Enemies
    {
        public class Enemy: MonoBehaviour, IDamageable
        {
            [SerializeField] private float initialHealth = 100f;
            
            public float Health { get; private set; }
            
            private void Awake()
            {
                Health = initialHealth;
            }

            public void TakeDamage(float amount)
            {
                Health -= amount;
                print(Health);
                if (Health <= 0)
                {
                    Die();
                }
            }

            private void Die()
            {
                // Aquí puedes poner lo que debe suceder cuando el enemigo muere.
                // Por ejemplo, puedes destruir este objeto enemigo:
                Destroy(gameObject);
            }
        }
    }