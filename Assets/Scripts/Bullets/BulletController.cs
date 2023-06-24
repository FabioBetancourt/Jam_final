using System;
using UnityEngine;

namespace Bullets
{
    public class BulletController: MonoBehaviour
    {
        [SerializeField] 
        private GameObject explosionPrefab; // Prefab de la explosión
        [SerializeField] 
        private AudioClip explosionSound; // Clip de audio de la explosión
        private AudioSource audioSource; // AudioSource para reproducir el sonido

        private float speed = 50f;
        private float timeToDestroy = 3f;
        public Vector3 target {get; set;}
        public bool hit { get; set; }
        
        private void Start()
        {
            // Obtén el componente AudioSource en este GameObject
            // Asegúrate de que tu GameObject tenga un componente AudioSource
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            Destroy(gameObject, timeToDestroy);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (hit && Vector3.Distance(transform.position, target) < 0.01f)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            //ContactPoint contact = other.GetContact(0);
            //GameObject.Instantiate(bulletDecal, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal));
            //Destroy(gameObject);
            ContactPoint contact = other.GetContact(0);

            // Crea la explosión
            GameObject explosion = Instantiate(explosionPrefab, contact.point, Quaternion.LookRotation(contact.normal));

            // Reproduce la animación de explosión
            // Asegúrate de que tu prefab de explosión tenga un componente Animator o Animation configurado
            Animator explosionAnimator = explosion.GetComponent<Animator>();
            explosionAnimator.Play("ExplosionAnimation"); // reemplace "ExplosionAnimation" con el nombre de tu animación

            // Reproduce el sonido de la explosión
            audioSource.PlayOneShot(explosionSound);

            Destroy(gameObject);
        }
        }
    }