using UnityEngine;
using System.Collections.Generic;
namespace Bullets
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] 
        private GameObject bulletPrefab; // prefab of the bullet

        private Queue<GameObject> bullets = new(); //store bullets in a queue

        public static BulletPool Instance { get; private set; } // Singleton

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public GameObject Get()
        {
            // if there are no bullets in the pool, add one
            if (bullets.Count == 0)
            {
                AddBullets(1);
            }

            // disable the bullet before using it
            GameObject bullet = bullets.Dequeue();
            bullet.SetActive(false);
            return bullet;
        }

        public void ReturnToPool(GameObject bullet)
        {
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
        }

        private void AddBullets(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bullets.Enqueue(bullet);
            }
        }
    }

}