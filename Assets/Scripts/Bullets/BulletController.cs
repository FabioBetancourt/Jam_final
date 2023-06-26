using UnityEngine;
namespace Bullets
{
    public class BulletController: MonoBehaviour
    {
        [SerializeField] 
        private GameObject explosionPrefab; // prefab of the explosion
        [SerializeField] 
        private AudioClip explosionSound; // explosion sound
        private AudioSource audioSource; // AudioSource to play the explosion sound

        private float speed = 30f;
        private float timeToLive = 5f;
        private float timeLived = 0f;
        public Vector3 target {get; set;}
        public bool hit { get; set; }
        
        private void Start()
        {
            // get the AudioSource component
            // required audiosource component
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            timeLived = 0f;
        }

        private void Update()
        {
            // If the bullet has reached its target, we return it to the pool
            if ((target - transform.position).magnitude < 0.1f)
            {
                ReturnToPool();
            }
            else
            {
                // Move the bullet towards its target
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }

            // If the bullet has lived longer than its lifetime, we return it to the pool
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

            //Destroy(gameObject);
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            BulletPool.Instance.ReturnToPool(gameObject);
        }
    }
}
