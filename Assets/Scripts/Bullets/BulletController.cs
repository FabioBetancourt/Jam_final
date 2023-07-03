using UnityEngine;
using Enemies;

namespace Bullets
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private AudioClip explosionSound;
        public float damage = 10f;

        private float speed = 30f;
        private float timeToLive = 8f;
        private float timeLived = 0f;
        public Vector3 target { get; set; }
        public bool hit { get; set; }

        private AudioSource audioSource;
        private Rigidbody rb; // Añade esta línea

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody>(); // Obtén una referencia al Rigidbody aquí
        }

        private void OnEnable()
        {
            timeLived = 0f;
        }

        private void Update()
        {
            if ((target - transform.position).magnitude < 0.1f)
            {
                ReturnToPool();
            }
            else
            {
                // Utiliza MovePosition para mover la bala
                rb.MovePosition(Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime));
            }

            timeLived += Time.deltaTime;
            if (timeLived > timeToLive)
            {
                ReturnToPool();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            ContactPoint contact = other.GetContact(0);

            // create the explosion
            GameObject explosion = Instantiate(explosionPrefab, contact.point, Quaternion.LookRotation(contact.normal));

            // the explosion animation
            Animator explosionAnimator = explosion.GetComponent<Animator>();
            //explosionAnimator.Play("ExplosionAnimation"); // replace "ExplosionAnimation" with the name of your animation
            // sound the explosion
            audioSource.PlayOneShot(explosionSound);

            // Check if the object we hit is an enemy
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // If it is, deal damage to it
                enemy.TakeDamage(damage);
            }

            //Destroy(gameObject);
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            BulletPool.Instance.ReturnToPool(gameObject);
        }
    }
}
